﻿using System.Linq.Expressions;
using System.Reactive;
using System.Reactive.Linq;
using System.Reflection;

namespace PowRxVar._Internal.Expressions.Utils;

static class Reflex
{
	private static readonly MethodInfo toUnitMethodOpen;

	static Reflex()
	{
		var extType = typeof(CommonRxExtensions);
		toUnitMethodOpen = extType.GetMethod("ToUnit")!;
	}

	public static Type? IsVarVAccessor(MemberExpression node)
	{
		if (node.Member is not PropertyInfo propInfo) return null;
		if (propInfo.Name != "V") return null;
		var propType = propInfo.PropertyType;
		return propType;
	}

	public static IObservable<Unit> GetWhenVarChange(object roVar)
	{
		var varType = roVar.GetType();
		var valType = varType.GenericTypeArguments.Single();
		var toUnitMethod = toUnitMethodOpen.MakeGenericMethod(valType);

		var whenChangeUntyped = toUnitMethod.Invoke(null, new [] { roVar });
		if (whenChangeUntyped is not IObservable<Unit> whenChanged) throw new ArgumentException();

		return whenChanged.Skip(1);
	}
}