namespace RxWinFormsDemo;

partial class LinkedComboWin
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
			this.masterCombo = new System.Windows.Forms.ComboBox();
			this.slaveCombo = new System.Windows.Forms.ComboBox();
			this.masterLabel = new System.Windows.Forms.Label();
			this.slaveLabel = new System.Windows.Forms.Label();
			this.loadBtn = new System.Windows.Forms.Button();
			this.clearBtn = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// masterCombo
			// 
			this.masterCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.masterCombo.FormattingEnabled = true;
			this.masterCombo.Location = new System.Drawing.Point(12, 12);
			this.masterCombo.Name = "masterCombo";
			this.masterCombo.Size = new System.Drawing.Size(121, 23);
			this.masterCombo.TabIndex = 0;
			// 
			// slaveCombo
			// 
			this.slaveCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.slaveCombo.FormattingEnabled = true;
			this.slaveCombo.Location = new System.Drawing.Point(191, 12);
			this.slaveCombo.Name = "slaveCombo";
			this.slaveCombo.Size = new System.Drawing.Size(121, 23);
			this.slaveCombo.TabIndex = 1;
			// 
			// masterLabel
			// 
			this.masterLabel.AutoSize = true;
			this.masterLabel.Location = new System.Drawing.Point(12, 64);
			this.masterLabel.Name = "masterLabel";
			this.masterLabel.Size = new System.Drawing.Size(43, 15);
			this.masterLabel.TabIndex = 2;
			this.masterLabel.Text = "Master";
			// 
			// slaveLabel
			// 
			this.slaveLabel.AutoSize = true;
			this.slaveLabel.Location = new System.Drawing.Point(191, 64);
			this.slaveLabel.Name = "slaveLabel";
			this.slaveLabel.Size = new System.Drawing.Size(34, 15);
			this.slaveLabel.TabIndex = 3;
			this.slaveLabel.Text = "Slave";
			// 
			// loadBtn
			// 
			this.loadBtn.Location = new System.Drawing.Point(191, 93);
			this.loadBtn.Name = "loadBtn";
			this.loadBtn.Size = new System.Drawing.Size(43, 23);
			this.loadBtn.TabIndex = 4;
			this.loadBtn.Text = "Load";
			this.loadBtn.UseVisualStyleBackColor = true;
			// 
			// clearBtn
			// 
			this.clearBtn.Location = new System.Drawing.Point(269, 93);
			this.clearBtn.Name = "clearBtn";
			this.clearBtn.Size = new System.Drawing.Size(43, 23);
			this.clearBtn.TabIndex = 5;
			this.clearBtn.Text = "Clear";
			this.clearBtn.UseVisualStyleBackColor = true;
			// 
			// LinkedComboWin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(331, 142);
			this.Controls.Add(this.clearBtn);
			this.Controls.Add(this.loadBtn);
			this.Controls.Add(this.slaveLabel);
			this.Controls.Add(this.masterLabel);
			this.Controls.Add(this.slaveCombo);
			this.Controls.Add(this.masterCombo);
			this.Name = "LinkedComboWin";
			this.Text = "Linked Combo Win";
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private ComboBox masterCombo;
	private ComboBox slaveCombo;
	private Label masterLabel;
	private Label slaveLabel;
	private Button loadBtn;
	private Button clearBtn;
}