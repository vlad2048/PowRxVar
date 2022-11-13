namespace RxWinFormsDemo.Wins;

partial class AutoSelectWin
{
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	/// <summary>
	/// Clean up any resources being used.
	/// </summary>
	/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	protected override void Dispose(bool disposing)
	{
		if (disposing && (components != null)) {
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	#region Windows Form Designer generated code

	/// <summary>
	/// Required method for Designer support - do not modify
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
	{
			this.noneGroupBox = new System.Windows.Forms.GroupBox();
			this.noneList = new System.Windows.Forms.ListBox();
			this.noneLabel = new System.Windows.Forms.Label();
			this.nonePopBtn = new System.Windows.Forms.Button();
			this.noneClearBtn = new System.Windows.Forms.Button();
			this.noneUnselectBtn = new System.Windows.Forms.Button();
			this.onceGroupBox = new System.Windows.Forms.GroupBox();
			this.onceUnselectBtn = new System.Windows.Forms.Button();
			this.onceClearBtn = new System.Windows.Forms.Button();
			this.oncePopBtn = new System.Windows.Forms.Button();
			this.onceLabel = new System.Windows.Forms.Label();
			this.onceList = new System.Windows.Forms.ListBox();
			this.alwaysGroupBox = new System.Windows.Forms.GroupBox();
			this.alwaysUnselectBtn = new System.Windows.Forms.Button();
			this.alwaysClearBtn = new System.Windows.Forms.Button();
			this.alwaysPopBtn = new System.Windows.Forms.Button();
			this.alwaysLabel = new System.Windows.Forms.Label();
			this.alwaysList = new System.Windows.Forms.ListBox();
			this.noneGroupBox.SuspendLayout();
			this.onceGroupBox.SuspendLayout();
			this.alwaysGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// noneGroupBox
			// 
			this.noneGroupBox.Controls.Add(this.noneUnselectBtn);
			this.noneGroupBox.Controls.Add(this.noneClearBtn);
			this.noneGroupBox.Controls.Add(this.nonePopBtn);
			this.noneGroupBox.Controls.Add(this.noneLabel);
			this.noneGroupBox.Controls.Add(this.noneList);
			this.noneGroupBox.Location = new System.Drawing.Point(12, 12);
			this.noneGroupBox.Name = "noneGroupBox";
			this.noneGroupBox.Size = new System.Drawing.Size(237, 157);
			this.noneGroupBox.TabIndex = 0;
			this.noneGroupBox.TabStop = false;
			this.noneGroupBox.Text = "None";
			// 
			// noneList
			// 
			this.noneList.FormattingEnabled = true;
			this.noneList.ItemHeight = 15;
			this.noneList.Location = new System.Drawing.Point(6, 22);
			this.noneList.Name = "noneList";
			this.noneList.Size = new System.Drawing.Size(120, 109);
			this.noneList.TabIndex = 0;
			// 
			// noneLabel
			// 
			this.noneLabel.AutoSize = true;
			this.noneLabel.Location = new System.Drawing.Point(6, 134);
			this.noneLabel.Name = "noneLabel";
			this.noneLabel.Size = new System.Drawing.Size(39, 15);
			this.noneLabel.TabIndex = 1;
			this.noneLabel.Text = "None:";
			// 
			// nonePopBtn
			// 
			this.nonePopBtn.Location = new System.Drawing.Point(132, 22);
			this.nonePopBtn.Name = "nonePopBtn";
			this.nonePopBtn.Size = new System.Drawing.Size(75, 23);
			this.nonePopBtn.TabIndex = 2;
			this.nonePopBtn.Text = "Pop";
			this.nonePopBtn.UseVisualStyleBackColor = true;
			// 
			// noneClearBtn
			// 
			this.noneClearBtn.Location = new System.Drawing.Point(132, 51);
			this.noneClearBtn.Name = "noneClearBtn";
			this.noneClearBtn.Size = new System.Drawing.Size(75, 23);
			this.noneClearBtn.TabIndex = 3;
			this.noneClearBtn.Text = "Clear";
			this.noneClearBtn.UseVisualStyleBackColor = true;
			// 
			// noneUnselectBtn
			// 
			this.noneUnselectBtn.Location = new System.Drawing.Point(132, 80);
			this.noneUnselectBtn.Name = "noneUnselectBtn";
			this.noneUnselectBtn.Size = new System.Drawing.Size(75, 23);
			this.noneUnselectBtn.TabIndex = 4;
			this.noneUnselectBtn.Text = "Unselect";
			this.noneUnselectBtn.UseVisualStyleBackColor = true;
			// 
			// onceGroupBox
			// 
			this.onceGroupBox.Controls.Add(this.onceUnselectBtn);
			this.onceGroupBox.Controls.Add(this.onceClearBtn);
			this.onceGroupBox.Controls.Add(this.oncePopBtn);
			this.onceGroupBox.Controls.Add(this.onceLabel);
			this.onceGroupBox.Controls.Add(this.onceList);
			this.onceGroupBox.Location = new System.Drawing.Point(12, 175);
			this.onceGroupBox.Name = "onceGroupBox";
			this.onceGroupBox.Size = new System.Drawing.Size(237, 157);
			this.onceGroupBox.TabIndex = 1;
			this.onceGroupBox.TabStop = false;
			this.onceGroupBox.Text = "Once";
			// 
			// onceUnselectBtn
			// 
			this.onceUnselectBtn.Location = new System.Drawing.Point(132, 80);
			this.onceUnselectBtn.Name = "onceUnselectBtn";
			this.onceUnselectBtn.Size = new System.Drawing.Size(75, 23);
			this.onceUnselectBtn.TabIndex = 4;
			this.onceUnselectBtn.Text = "Unselect";
			this.onceUnselectBtn.UseVisualStyleBackColor = true;
			// 
			// onceClearBtn
			// 
			this.onceClearBtn.Location = new System.Drawing.Point(132, 51);
			this.onceClearBtn.Name = "onceClearBtn";
			this.onceClearBtn.Size = new System.Drawing.Size(75, 23);
			this.onceClearBtn.TabIndex = 3;
			this.onceClearBtn.Text = "Clear";
			this.onceClearBtn.UseVisualStyleBackColor = true;
			// 
			// oncePopBtn
			// 
			this.oncePopBtn.Location = new System.Drawing.Point(132, 22);
			this.oncePopBtn.Name = "oncePopBtn";
			this.oncePopBtn.Size = new System.Drawing.Size(75, 23);
			this.oncePopBtn.TabIndex = 2;
			this.oncePopBtn.Text = "Pop";
			this.oncePopBtn.UseVisualStyleBackColor = true;
			// 
			// onceLabel
			// 
			this.onceLabel.AutoSize = true;
			this.onceLabel.Location = new System.Drawing.Point(6, 134);
			this.onceLabel.Name = "onceLabel";
			this.onceLabel.Size = new System.Drawing.Size(38, 15);
			this.onceLabel.TabIndex = 1;
			this.onceLabel.Text = "Once:";
			// 
			// onceList
			// 
			this.onceList.FormattingEnabled = true;
			this.onceList.ItemHeight = 15;
			this.onceList.Location = new System.Drawing.Point(6, 22);
			this.onceList.Name = "onceList";
			this.onceList.Size = new System.Drawing.Size(120, 109);
			this.onceList.TabIndex = 0;
			// 
			// alwaysGroupBox
			// 
			this.alwaysGroupBox.Controls.Add(this.alwaysUnselectBtn);
			this.alwaysGroupBox.Controls.Add(this.alwaysClearBtn);
			this.alwaysGroupBox.Controls.Add(this.alwaysPopBtn);
			this.alwaysGroupBox.Controls.Add(this.alwaysLabel);
			this.alwaysGroupBox.Controls.Add(this.alwaysList);
			this.alwaysGroupBox.Location = new System.Drawing.Point(12, 338);
			this.alwaysGroupBox.Name = "alwaysGroupBox";
			this.alwaysGroupBox.Size = new System.Drawing.Size(237, 157);
			this.alwaysGroupBox.TabIndex = 2;
			this.alwaysGroupBox.TabStop = false;
			this.alwaysGroupBox.Text = "Always";
			// 
			// alwaysUnselectBtn
			// 
			this.alwaysUnselectBtn.Location = new System.Drawing.Point(132, 80);
			this.alwaysUnselectBtn.Name = "alwaysUnselectBtn";
			this.alwaysUnselectBtn.Size = new System.Drawing.Size(75, 23);
			this.alwaysUnselectBtn.TabIndex = 4;
			this.alwaysUnselectBtn.Text = "Unselect";
			this.alwaysUnselectBtn.UseVisualStyleBackColor = true;
			// 
			// alwaysClearBtn
			// 
			this.alwaysClearBtn.Location = new System.Drawing.Point(132, 51);
			this.alwaysClearBtn.Name = "alwaysClearBtn";
			this.alwaysClearBtn.Size = new System.Drawing.Size(75, 23);
			this.alwaysClearBtn.TabIndex = 3;
			this.alwaysClearBtn.Text = "Clear";
			this.alwaysClearBtn.UseVisualStyleBackColor = true;
			// 
			// alwaysPopBtn
			// 
			this.alwaysPopBtn.Location = new System.Drawing.Point(132, 22);
			this.alwaysPopBtn.Name = "alwaysPopBtn";
			this.alwaysPopBtn.Size = new System.Drawing.Size(75, 23);
			this.alwaysPopBtn.TabIndex = 2;
			this.alwaysPopBtn.Text = "Pop";
			this.alwaysPopBtn.UseVisualStyleBackColor = true;
			// 
			// alwaysLabel
			// 
			this.alwaysLabel.AutoSize = true;
			this.alwaysLabel.Location = new System.Drawing.Point(6, 134);
			this.alwaysLabel.Name = "alwaysLabel";
			this.alwaysLabel.Size = new System.Drawing.Size(47, 15);
			this.alwaysLabel.TabIndex = 1;
			this.alwaysLabel.Text = "Always:";
			// 
			// alwaysList
			// 
			this.alwaysList.FormattingEnabled = true;
			this.alwaysList.ItemHeight = 15;
			this.alwaysList.Location = new System.Drawing.Point(6, 22);
			this.alwaysList.Name = "alwaysList";
			this.alwaysList.Size = new System.Drawing.Size(120, 109);
			this.alwaysList.TabIndex = 0;
			// 
			// AutoSelectWin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(261, 511);
			this.Controls.Add(this.alwaysGroupBox);
			this.Controls.Add(this.onceGroupBox);
			this.Controls.Add(this.noneGroupBox);
			this.MaximumSize = new System.Drawing.Size(277, 550);
			this.MinimumSize = new System.Drawing.Size(277, 550);
			this.Name = "AutoSelectWin";
			this.Text = "AutoSelectWin";
			this.noneGroupBox.ResumeLayout(false);
			this.noneGroupBox.PerformLayout();
			this.onceGroupBox.ResumeLayout(false);
			this.onceGroupBox.PerformLayout();
			this.alwaysGroupBox.ResumeLayout(false);
			this.alwaysGroupBox.PerformLayout();
			this.ResumeLayout(false);

	}

	#endregion

	private GroupBox noneGroupBox;
	private Button noneUnselectBtn;
	private Button noneClearBtn;
	private Button nonePopBtn;
	private Label noneLabel;
	private ListBox noneList;
	private GroupBox onceGroupBox;
	private Button onceUnselectBtn;
	private Button onceClearBtn;
	private Button oncePopBtn;
	private Label onceLabel;
	private ListBox onceList;
	private GroupBox alwaysGroupBox;
	private Button alwaysUnselectBtn;
	private Button alwaysClearBtn;
	private Button alwaysPopBtn;
	private Label alwaysLabel;
	private ListBox alwaysList;
}