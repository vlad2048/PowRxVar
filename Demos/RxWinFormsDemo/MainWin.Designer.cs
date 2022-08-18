namespace RxWinFormsDemo;

partial class MainWin
{
	/// <summary>
	///  Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	/// <summary>
	///  Clean up any resources being used.
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
	///  Required method for Designer support - do not modify
	///  the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
	{
			this.tabControl = new System.Windows.Forms.TabControl();
			this.listBoxTabPage = new System.Windows.Forms.TabPage();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.listRefMasterClearBtn = new System.Windows.Forms.Button();
			this.listRefMasterLoadBtn = new System.Windows.Forms.Button();
			this.listRefSlaveClearBtn = new System.Windows.Forms.Button();
			this.listRefSlaveLabel = new System.Windows.Forms.Label();
			this.listRefSlaveList = new System.Windows.Forms.ListBox();
			this.listRefMasterLabel = new System.Windows.Forms.Label();
			this.listRefMasterList = new System.Windows.Forms.ListBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.cacheRefMasterClearBtn = new System.Windows.Forms.Button();
			this.cacheRefMasterLoadBtn = new System.Windows.Forms.Button();
			this.cacheRefSlaveClearBtn = new System.Windows.Forms.Button();
			this.cacheRefSlaveLabel = new System.Windows.Forms.Label();
			this.cacheRefSlaveList = new System.Windows.Forms.ListBox();
			this.cacheRefMasterLabel = new System.Windows.Forms.Label();
			this.cacheRefMasterList = new System.Windows.Forms.ListBox();
			this.comboBoxTabPage = new System.Windows.Forms.TabPage();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.listValMasterClearBtn = new System.Windows.Forms.Button();
			this.listValMasterLoadBtn = new System.Windows.Forms.Button();
			this.listValSlaveClearBtn = new System.Windows.Forms.Button();
			this.listValSlaveLabel = new System.Windows.Forms.Label();
			this.listValSlaveList = new System.Windows.Forms.ListBox();
			this.listValMasterLabel = new System.Windows.Forms.Label();
			this.listValMasterList = new System.Windows.Forms.ListBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.cacheValMasterClearBtn = new System.Windows.Forms.Button();
			this.cacheValMasterLoadBtn = new System.Windows.Forms.Button();
			this.cacheValSlaveClearBtn = new System.Windows.Forms.Button();
			this.cacheValSlaveLabel = new System.Windows.Forms.Label();
			this.cacheValSlaveList = new System.Windows.Forms.ListBox();
			this.cacheValMasterLabel = new System.Windows.Forms.Label();
			this.cacheValMasterList = new System.Windows.Forms.ListBox();
			this.tabControl.SuspendLayout();
			this.listBoxTabPage.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.listBoxTabPage);
			this.tabControl.Controls.Add(this.comboBoxTabPage);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(0, 0);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(687, 463);
			this.tabControl.TabIndex = 0;
			// 
			// listBoxTabPage
			// 
			this.listBoxTabPage.Controls.Add(this.groupBox3);
			this.listBoxTabPage.Controls.Add(this.groupBox4);
			this.listBoxTabPage.Controls.Add(this.groupBox2);
			this.listBoxTabPage.Controls.Add(this.groupBox1);
			this.listBoxTabPage.Location = new System.Drawing.Point(4, 24);
			this.listBoxTabPage.Name = "listBoxTabPage";
			this.listBoxTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.listBoxTabPage.Size = new System.Drawing.Size(679, 435);
			this.listBoxTabPage.TabIndex = 0;
			this.listBoxTabPage.Text = "ListBox";
			this.listBoxTabPage.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.listRefMasterClearBtn);
			this.groupBox2.Controls.Add(this.listRefMasterLoadBtn);
			this.groupBox2.Controls.Add(this.listRefSlaveClearBtn);
			this.groupBox2.Controls.Add(this.listRefSlaveLabel);
			this.groupBox2.Controls.Add(this.listRefSlaveList);
			this.groupBox2.Controls.Add(this.listRefMasterLabel);
			this.groupBox2.Controls.Add(this.listRefMasterList);
			this.groupBox2.Location = new System.Drawing.Point(8, 218);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(327, 206);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Source List Reference";
			// 
			// listRefMasterClearBtn
			// 
			this.listRefMasterClearBtn.Location = new System.Drawing.Point(71, 167);
			this.listRefMasterClearBtn.Name = "listRefMasterClearBtn";
			this.listRefMasterClearBtn.Size = new System.Drawing.Size(55, 23);
			this.listRefMasterClearBtn.TabIndex = 6;
			this.listRefMasterClearBtn.Text = "Clear";
			this.listRefMasterClearBtn.UseVisualStyleBackColor = true;
			// 
			// listRefMasterLoadBtn
			// 
			this.listRefMasterLoadBtn.Location = new System.Drawing.Point(6, 167);
			this.listRefMasterLoadBtn.Name = "listRefMasterLoadBtn";
			this.listRefMasterLoadBtn.Size = new System.Drawing.Size(55, 23);
			this.listRefMasterLoadBtn.TabIndex = 5;
			this.listRefMasterLoadBtn.Text = "Load";
			this.listRefMasterLoadBtn.UseVisualStyleBackColor = true;
			// 
			// listRefSlaveClearBtn
			// 
			this.listRefSlaveClearBtn.Location = new System.Drawing.Point(174, 167);
			this.listRefSlaveClearBtn.Name = "listRefSlaveClearBtn";
			this.listRefSlaveClearBtn.Size = new System.Drawing.Size(120, 23);
			this.listRefSlaveClearBtn.TabIndex = 4;
			this.listRefSlaveClearBtn.Text = "Clear";
			this.listRefSlaveClearBtn.UseVisualStyleBackColor = true;
			// 
			// listRefSlaveLabel
			// 
			this.listRefSlaveLabel.AutoSize = true;
			this.listRefSlaveLabel.Location = new System.Drawing.Point(174, 19);
			this.listRefSlaveLabel.Name = "listRefSlaveLabel";
			this.listRefSlaveLabel.Size = new System.Drawing.Size(34, 15);
			this.listRefSlaveLabel.TabIndex = 3;
			this.listRefSlaveLabel.Text = "Slave";
			// 
			// listRefSlaveList
			// 
			this.listRefSlaveList.FormattingEnabled = true;
			this.listRefSlaveList.ItemHeight = 15;
			this.listRefSlaveList.Location = new System.Drawing.Point(174, 37);
			this.listRefSlaveList.Name = "listRefSlaveList";
			this.listRefSlaveList.Size = new System.Drawing.Size(120, 124);
			this.listRefSlaveList.TabIndex = 2;
			// 
			// listRefMasterLabel
			// 
			this.listRefMasterLabel.AutoSize = true;
			this.listRefMasterLabel.Location = new System.Drawing.Point(6, 19);
			this.listRefMasterLabel.Name = "listRefMasterLabel";
			this.listRefMasterLabel.Size = new System.Drawing.Size(43, 15);
			this.listRefMasterLabel.TabIndex = 1;
			this.listRefMasterLabel.Text = "Master";
			// 
			// listRefMasterList
			// 
			this.listRefMasterList.FormattingEnabled = true;
			this.listRefMasterList.ItemHeight = 15;
			this.listRefMasterList.Location = new System.Drawing.Point(6, 37);
			this.listRefMasterList.Name = "listRefMasterList";
			this.listRefMasterList.Size = new System.Drawing.Size(120, 124);
			this.listRefMasterList.TabIndex = 0;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.cacheRefMasterClearBtn);
			this.groupBox1.Controls.Add(this.cacheRefMasterLoadBtn);
			this.groupBox1.Controls.Add(this.cacheRefSlaveClearBtn);
			this.groupBox1.Controls.Add(this.cacheRefSlaveLabel);
			this.groupBox1.Controls.Add(this.cacheRefSlaveList);
			this.groupBox1.Controls.Add(this.cacheRefMasterLabel);
			this.groupBox1.Controls.Add(this.cacheRefMasterList);
			this.groupBox1.Location = new System.Drawing.Point(8, 6);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(327, 206);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Source Cache Reference";
			// 
			// cacheRefMasterClearBtn
			// 
			this.cacheRefMasterClearBtn.Location = new System.Drawing.Point(71, 168);
			this.cacheRefMasterClearBtn.Name = "cacheRefMasterClearBtn";
			this.cacheRefMasterClearBtn.Size = new System.Drawing.Size(55, 23);
			this.cacheRefMasterClearBtn.TabIndex = 6;
			this.cacheRefMasterClearBtn.Text = "Clear";
			this.cacheRefMasterClearBtn.UseVisualStyleBackColor = true;
			// 
			// cacheRefMasterLoadBtn
			// 
			this.cacheRefMasterLoadBtn.Location = new System.Drawing.Point(6, 168);
			this.cacheRefMasterLoadBtn.Name = "cacheRefMasterLoadBtn";
			this.cacheRefMasterLoadBtn.Size = new System.Drawing.Size(55, 23);
			this.cacheRefMasterLoadBtn.TabIndex = 5;
			this.cacheRefMasterLoadBtn.Text = "Load";
			this.cacheRefMasterLoadBtn.UseVisualStyleBackColor = true;
			// 
			// cacheRefSlaveClearBtn
			// 
			this.cacheRefSlaveClearBtn.Location = new System.Drawing.Point(174, 168);
			this.cacheRefSlaveClearBtn.Name = "cacheRefSlaveClearBtn";
			this.cacheRefSlaveClearBtn.Size = new System.Drawing.Size(120, 23);
			this.cacheRefSlaveClearBtn.TabIndex = 4;
			this.cacheRefSlaveClearBtn.Text = "Clear";
			this.cacheRefSlaveClearBtn.UseVisualStyleBackColor = true;
			// 
			// cacheRefSlaveLabel
			// 
			this.cacheRefSlaveLabel.AutoSize = true;
			this.cacheRefSlaveLabel.Location = new System.Drawing.Point(174, 19);
			this.cacheRefSlaveLabel.Name = "cacheRefSlaveLabel";
			this.cacheRefSlaveLabel.Size = new System.Drawing.Size(34, 15);
			this.cacheRefSlaveLabel.TabIndex = 3;
			this.cacheRefSlaveLabel.Text = "Slave";
			// 
			// cacheRefSlaveList
			// 
			this.cacheRefSlaveList.FormattingEnabled = true;
			this.cacheRefSlaveList.ItemHeight = 15;
			this.cacheRefSlaveList.Location = new System.Drawing.Point(174, 37);
			this.cacheRefSlaveList.Name = "cacheRefSlaveList";
			this.cacheRefSlaveList.Size = new System.Drawing.Size(120, 124);
			this.cacheRefSlaveList.TabIndex = 2;
			// 
			// cacheRefMasterLabel
			// 
			this.cacheRefMasterLabel.AutoSize = true;
			this.cacheRefMasterLabel.Location = new System.Drawing.Point(6, 19);
			this.cacheRefMasterLabel.Name = "cacheRefMasterLabel";
			this.cacheRefMasterLabel.Size = new System.Drawing.Size(43, 15);
			this.cacheRefMasterLabel.TabIndex = 1;
			this.cacheRefMasterLabel.Text = "Master";
			// 
			// cacheRefMasterList
			// 
			this.cacheRefMasterList.FormattingEnabled = true;
			this.cacheRefMasterList.ItemHeight = 15;
			this.cacheRefMasterList.Location = new System.Drawing.Point(6, 37);
			this.cacheRefMasterList.Name = "cacheRefMasterList";
			this.cacheRefMasterList.Size = new System.Drawing.Size(120, 124);
			this.cacheRefMasterList.TabIndex = 0;
			// 
			// comboBoxTabPage
			// 
			this.comboBoxTabPage.Location = new System.Drawing.Point(4, 24);
			this.comboBoxTabPage.Name = "comboBoxTabPage";
			this.comboBoxTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.comboBoxTabPage.Size = new System.Drawing.Size(696, 469);
			this.comboBoxTabPage.TabIndex = 1;
			this.comboBoxTabPage.Text = "ComboBox";
			this.comboBoxTabPage.UseVisualStyleBackColor = true;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.listValMasterClearBtn);
			this.groupBox3.Controls.Add(this.listValMasterLoadBtn);
			this.groupBox3.Controls.Add(this.listValSlaveClearBtn);
			this.groupBox3.Controls.Add(this.listValSlaveLabel);
			this.groupBox3.Controls.Add(this.listValSlaveList);
			this.groupBox3.Controls.Add(this.listValMasterLabel);
			this.groupBox3.Controls.Add(this.listValMasterList);
			this.groupBox3.Location = new System.Drawing.Point(341, 218);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(327, 206);
			this.groupBox3.TabIndex = 3;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Source List Value";
			// 
			// listValMasterClearBtn
			// 
			this.listValMasterClearBtn.Location = new System.Drawing.Point(71, 167);
			this.listValMasterClearBtn.Name = "listValMasterClearBtn";
			this.listValMasterClearBtn.Size = new System.Drawing.Size(55, 23);
			this.listValMasterClearBtn.TabIndex = 6;
			this.listValMasterClearBtn.Text = "Clear";
			this.listValMasterClearBtn.UseVisualStyleBackColor = true;
			// 
			// listValMasterLoadBtn
			// 
			this.listValMasterLoadBtn.Location = new System.Drawing.Point(6, 167);
			this.listValMasterLoadBtn.Name = "listValMasterLoadBtn";
			this.listValMasterLoadBtn.Size = new System.Drawing.Size(55, 23);
			this.listValMasterLoadBtn.TabIndex = 5;
			this.listValMasterLoadBtn.Text = "Load";
			this.listValMasterLoadBtn.UseVisualStyleBackColor = true;
			// 
			// listValSlaveClearBtn
			// 
			this.listValSlaveClearBtn.Location = new System.Drawing.Point(174, 167);
			this.listValSlaveClearBtn.Name = "listValSlaveClearBtn";
			this.listValSlaveClearBtn.Size = new System.Drawing.Size(120, 23);
			this.listValSlaveClearBtn.TabIndex = 4;
			this.listValSlaveClearBtn.Text = "Clear";
			this.listValSlaveClearBtn.UseVisualStyleBackColor = true;
			// 
			// listValSlaveLabel
			// 
			this.listValSlaveLabel.AutoSize = true;
			this.listValSlaveLabel.Location = new System.Drawing.Point(174, 19);
			this.listValSlaveLabel.Name = "listValSlaveLabel";
			this.listValSlaveLabel.Size = new System.Drawing.Size(34, 15);
			this.listValSlaveLabel.TabIndex = 3;
			this.listValSlaveLabel.Text = "Slave";
			// 
			// listValSlaveList
			// 
			this.listValSlaveList.FormattingEnabled = true;
			this.listValSlaveList.ItemHeight = 15;
			this.listValSlaveList.Location = new System.Drawing.Point(174, 37);
			this.listValSlaveList.Name = "listValSlaveList";
			this.listValSlaveList.Size = new System.Drawing.Size(120, 124);
			this.listValSlaveList.TabIndex = 2;
			// 
			// listValMasterLabel
			// 
			this.listValMasterLabel.AutoSize = true;
			this.listValMasterLabel.Location = new System.Drawing.Point(6, 19);
			this.listValMasterLabel.Name = "listValMasterLabel";
			this.listValMasterLabel.Size = new System.Drawing.Size(43, 15);
			this.listValMasterLabel.TabIndex = 1;
			this.listValMasterLabel.Text = "Master";
			// 
			// listValMasterList
			// 
			this.listValMasterList.FormattingEnabled = true;
			this.listValMasterList.ItemHeight = 15;
			this.listValMasterList.Location = new System.Drawing.Point(6, 37);
			this.listValMasterList.Name = "listValMasterList";
			this.listValMasterList.Size = new System.Drawing.Size(120, 124);
			this.listValMasterList.TabIndex = 0;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.cacheValMasterClearBtn);
			this.groupBox4.Controls.Add(this.cacheValMasterLoadBtn);
			this.groupBox4.Controls.Add(this.cacheValSlaveClearBtn);
			this.groupBox4.Controls.Add(this.cacheValSlaveLabel);
			this.groupBox4.Controls.Add(this.cacheValSlaveList);
			this.groupBox4.Controls.Add(this.cacheValMasterLabel);
			this.groupBox4.Controls.Add(this.cacheValMasterList);
			this.groupBox4.Location = new System.Drawing.Point(341, 6);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(327, 206);
			this.groupBox4.TabIndex = 2;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Source Cache Value";
			// 
			// cacheValMasterClearBtn
			// 
			this.cacheValMasterClearBtn.Location = new System.Drawing.Point(71, 168);
			this.cacheValMasterClearBtn.Name = "cacheValMasterClearBtn";
			this.cacheValMasterClearBtn.Size = new System.Drawing.Size(55, 23);
			this.cacheValMasterClearBtn.TabIndex = 6;
			this.cacheValMasterClearBtn.Text = "Clear";
			this.cacheValMasterClearBtn.UseVisualStyleBackColor = true;
			// 
			// cacheValMasterLoadBtn
			// 
			this.cacheValMasterLoadBtn.Location = new System.Drawing.Point(6, 168);
			this.cacheValMasterLoadBtn.Name = "cacheValMasterLoadBtn";
			this.cacheValMasterLoadBtn.Size = new System.Drawing.Size(55, 23);
			this.cacheValMasterLoadBtn.TabIndex = 5;
			this.cacheValMasterLoadBtn.Text = "Load";
			this.cacheValMasterLoadBtn.UseVisualStyleBackColor = true;
			// 
			// cacheValSlaveClearBtn
			// 
			this.cacheValSlaveClearBtn.Location = new System.Drawing.Point(174, 168);
			this.cacheValSlaveClearBtn.Name = "cacheValSlaveClearBtn";
			this.cacheValSlaveClearBtn.Size = new System.Drawing.Size(120, 23);
			this.cacheValSlaveClearBtn.TabIndex = 4;
			this.cacheValSlaveClearBtn.Text = "Clear";
			this.cacheValSlaveClearBtn.UseVisualStyleBackColor = true;
			// 
			// cacheValSlaveLabel
			// 
			this.cacheValSlaveLabel.AutoSize = true;
			this.cacheValSlaveLabel.Location = new System.Drawing.Point(174, 19);
			this.cacheValSlaveLabel.Name = "cacheValSlaveLabel";
			this.cacheValSlaveLabel.Size = new System.Drawing.Size(34, 15);
			this.cacheValSlaveLabel.TabIndex = 3;
			this.cacheValSlaveLabel.Text = "Slave";
			// 
			// cacheValSlaveList
			// 
			this.cacheValSlaveList.FormattingEnabled = true;
			this.cacheValSlaveList.ItemHeight = 15;
			this.cacheValSlaveList.Location = new System.Drawing.Point(174, 37);
			this.cacheValSlaveList.Name = "cacheValSlaveList";
			this.cacheValSlaveList.Size = new System.Drawing.Size(120, 124);
			this.cacheValSlaveList.TabIndex = 2;
			// 
			// cacheValMasterLabel
			// 
			this.cacheValMasterLabel.AutoSize = true;
			this.cacheValMasterLabel.Location = new System.Drawing.Point(6, 19);
			this.cacheValMasterLabel.Name = "cacheValMasterLabel";
			this.cacheValMasterLabel.Size = new System.Drawing.Size(43, 15);
			this.cacheValMasterLabel.TabIndex = 1;
			this.cacheValMasterLabel.Text = "Master";
			// 
			// cacheValMasterList
			// 
			this.cacheValMasterList.FormattingEnabled = true;
			this.cacheValMasterList.ItemHeight = 15;
			this.cacheValMasterList.Location = new System.Drawing.Point(6, 37);
			this.cacheValMasterList.Name = "cacheValMasterList";
			this.cacheValMasterList.Size = new System.Drawing.Size(120, 124);
			this.cacheValMasterList.TabIndex = 0;
			// 
			// MainWin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(687, 463);
			this.Controls.Add(this.tabControl);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainWin";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Rx WinForms Demo";
			this.tabControl.ResumeLayout(false);
			this.listBoxTabPage.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.ResumeLayout(false);

	}

	#endregion

	private TabControl tabControl;
	private TabPage listBoxTabPage;
	private TabPage comboBoxTabPage;
	private GroupBox groupBox1;
	private Button cacheRefSlaveClearBtn;
	private Label cacheRefSlaveLabel;
	private ListBox cacheRefSlaveList;
	private Label cacheRefMasterLabel;
	private ListBox cacheRefMasterList;
	private Button cacheRefMasterClearBtn;
	private Button cacheRefMasterLoadBtn;
	private GroupBox groupBox2;
	private Button listRefMasterClearBtn;
	private Button listRefMasterLoadBtn;
	private Button listRefSlaveClearBtn;
	private Label listRefSlaveLabel;
	private ListBox listRefSlaveList;
	private Label listRefMasterLabel;
	private ListBox listRefMasterList;
	private GroupBox groupBox3;
	private Button listValMasterClearBtn;
	private Button listValMasterLoadBtn;
	private Button listValSlaveClearBtn;
	private Label listValSlaveLabel;
	private ListBox listValSlaveList;
	private Label listValMasterLabel;
	private ListBox listValMasterList;
	private GroupBox groupBox4;
	private Button cacheValMasterClearBtn;
	private Button cacheValMasterLoadBtn;
	private Button cacheValSlaveClearBtn;
	private Label cacheValSlaveLabel;
	private ListBox cacheValSlaveList;
	private Label cacheValMasterLabel;
	private ListBox cacheValMasterList;
}
