using PowRxVar._Internal.Subjects;
using System.Collections;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.CompilerServices;

namespace PowRxVar;

public sealed class Disp : ICollection<IDisposable>, ICancelable, IRwDispBase
{
	private const string Strings_Core_DISPOSABLES_CANT_CONTAIN_NULL = "Disposables collection can not contain null values.";
	private const int ShrinkThreshold = 64;
	private const int DefaultCapacity = 16;

	private readonly object _gate = new();
	private readonly ISubject<Unit> whenDisposed = new AsyncSubjectReverse<Unit>();
	private bool _disposed;
	private List<IDisposable?> _disposables;
	private int _count;

	public IObservable<Unit> WhenDisposed => whenDisposed.AsObservable();
	public int Count => Volatile.Read(ref _count);
	public bool IsReadOnly => false;
	public IEnumerator<IDisposable> GetEnumerator()
	{
		lock (_gate) {
			if (_disposed || _count == 0) return EmptyEnumerator;
			return new CompositeEnumerator(_disposables.ToArray());
		}
	}
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	public bool IsDisposed => Volatile.Read(ref _disposed);
	private static readonly CompositeEnumerator EmptyEnumerator = new(Array.Empty<IDisposable?>());

	private readonly int statsId = DispStats.GetNextDispId();
	private void InitStats(string? dbgExpr = null) => DispStats.DispCreated(statsId, dbgExpr);
	private void DoneStats() => DispStats.DispDisposed(statsId);

	public Disp(string? dbgExpr = null, [CallerFilePath] string srcFile = "", [CallerLineNumber] int srcLine = 0)
	{
		InitStats(dbgExpr ?? $@"{srcFile}:{srcLine}  @ ""new Disp()""");
		_disposables = new List<IDisposable?>();
	}

	public Disp(int capacity)
	{
		InitStats();
		if (capacity < 0) throw new ArgumentOutOfRangeException(nameof(capacity));
		_disposables = new List<IDisposable?>(capacity);
	}

	public Disp(params IDisposable[] disposables)
	{
		InitStats();
		if (disposables == null) throw new ArgumentNullException(nameof(disposables));
		_disposables = ToList(disposables);
		Volatile.Write(ref _count, _disposables.Count);
	}

	public Disp(IEnumerable<IDisposable> disposables)
	{
		InitStats();
		if (disposables == null) throw new ArgumentNullException(nameof(disposables));
		_disposables = ToList(disposables);
		Volatile.Write(ref _count, _disposables.Count);
	}

	private static List<IDisposable?> ToList(IEnumerable<IDisposable> disposables)
	{
		var capacity = disposables switch {
			IDisposable[] a => a.Length,
			ICollection<IDisposable> c => c.Count,
			_ => DefaultCapacity
		};

		var list = new List<IDisposable?>(capacity);
		foreach (var d in disposables) {
			if (d == null) throw new ArgumentException(Strings_Core_DISPOSABLES_CANT_CONTAIN_NULL, nameof(disposables));
			list.Add(d);
		}
		return list;
	}

	public void Dispose()
	{
		lock (_gate)
		{
			if (_disposed)
				return;
			Volatile.Write(ref _disposed, true);
		}

		List<IDisposable?>? currentDisposables;

		lock (_gate) {
			DoneStats();
			currentDisposables = _disposables;
			whenDisposed.OnNext(Unit.Default);
			whenDisposed.OnCompleted();
			_disposables = null!;
			Volatile.Write(ref _count, 0);
		}

		for (var i = currentDisposables.Count - 1; i >= 0; i--)
			currentDisposables[i]?.Dispose();
	}



	public void Add(IDisposable item)
	{
		if (item == null) throw new ArgumentNullException(nameof(item));
		lock (_gate) {
			if (!_disposed) {
				_disposables.Add(item);
				Volatile.Write(ref _count, _count + 1);
				return;
			}
		}

		item.Dispose();
	}

	public bool Remove(IDisposable item)
	{
		if (item == null) throw new ArgumentNullException(nameof(item));

		lock (_gate) {
			if (_disposed) return false;
			var current = _disposables;
			var i = current.IndexOf(item);
			if (i < 0) return false;
			current[i] = null;
			if (current.Capacity > ShrinkThreshold && _count < current.Capacity / 2) {
				var fresh = new List<IDisposable?>(current.Capacity / 2);
				fresh.AddRange(current.Where(e => e != null));
				_disposables = fresh;
			}
			Volatile.Write(ref _count, _count - 1);
		}
		item.Dispose();

		return true;
	}

	public void Clear()
	{
		IDisposable?[] previousDisposables;

		lock (_gate) {
			if (_disposed) return;
			var current = _disposables;
			previousDisposables = current.ToArray();
			current.Clear();
			Volatile.Write(ref _count, 0);
		}

		for (var i = previousDisposables.Length - 1; i >= 0; i--)
			previousDisposables[i]?.Dispose();
	}

	public bool Contains(IDisposable item)
	{
		if (item == null) throw new ArgumentNullException(nameof(item));

		lock (_gate) {
			if (_disposed) return false;
			return _disposables.Contains(item);
		}
	}

	public void CopyTo(IDisposable[] array, int arrayIndex)
	{
		if (array == null) throw new ArgumentNullException(nameof(array));
		if (arrayIndex < 0 || arrayIndex >= array.Length) throw new ArgumentOutOfRangeException(nameof(arrayIndex));

		lock (_gate) {
			if (_disposed) return;

			if (arrayIndex + _count > array.Length) throw new ArgumentOutOfRangeException(nameof(arrayIndex));

			var i = arrayIndex;

			foreach (var d in _disposables) {
				if (d != null)
					array[i++] = d;
			}
		}
	}



	private sealed class CompositeEnumerator : IEnumerator<IDisposable>
	{
		private readonly IDisposable?[] _disposables;
		private int _index;

		object IEnumerator.Current => _disposables[_index]!;

		public IDisposable Current => _disposables[_index]!;
		public void Dispose()
		{
			var disposables = _disposables;
			Array.Clear(disposables, 0, disposables.Length);
		}

		public CompositeEnumerator(IDisposable?[] disposables)
		{
			_disposables = disposables;
			_index = -1;
		}

		public bool MoveNext()
		{
			var disposables = _disposables;

			for (; ; )
			{
				var idx = ++_index;
				if (idx >= disposables.Length) return false;
				if (disposables[idx] != null)
					return true;
			}
		}
		public void Reset() => _index = -1;
	}
}