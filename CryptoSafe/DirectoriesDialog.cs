using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace CryptoSafe
{
	/// <summary>
	/// Summary description for DirectoriesDialog.
	/// </summary>
	public class DirectoriesDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBoxOpen;
		private System.Windows.Forms.TextBox textBoxSave;
		private System.Windows.Forms.TextBox textBoxAdd;
		private System.Windows.Forms.TextBox textBoxAppDir;
		private System.Windows.Forms.ErrorProvider errorProvider;
		private System.Windows.Forms.CheckBox checkBoxReadFromReg;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DirectoriesDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(DirectoriesDialog));
			this.buttonSave = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOK = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textBoxOpen = new System.Windows.Forms.TextBox();
			this.textBoxSave = new System.Windows.Forms.TextBox();
			this.textBoxAdd = new System.Windows.Forms.TextBox();
			this.textBoxAppDir = new System.Windows.Forms.TextBox();
			this.errorProvider = new System.Windows.Forms.ErrorProvider();
			this.checkBoxReadFromReg = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// buttonSave
			// 
			this.buttonSave.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonSave.Location = new System.Drawing.Point(205, 280);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(64, 24);
			this.buttonSave.TabIndex = 6;
			this.buttonSave.Text = "&Save";
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(117, 280);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(64, 24);
			this.buttonCancel.TabIndex = 5;
			this.buttonCancel.Text = "&Cancel";
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point(29, 280);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(64, 24);
			this.buttonOK.TabIndex = 4;
			this.buttonOK.Text = "O&K";
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(184, 16);
			this.label1.TabIndex = 7;
			this.label1.Text = "Default directory to open file from:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 80);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(184, 16);
			this.label2.TabIndex = 8;
			this.label2.Text = "Default directory to save file to:";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 136);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(184, 16);
			this.label3.TabIndex = 9;
			this.label3.Text = "Default directory to add  files from:";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(16, 192);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(184, 16);
			this.label4.TabIndex = 10;
			this.label4.Text = "Application Directory:";
			// 
			// textBoxOpen
			// 
			this.textBoxOpen.Location = new System.Drawing.Point(16, 48);
			this.textBoxOpen.Name = "textBoxOpen";
			this.textBoxOpen.Size = new System.Drawing.Size(272, 20);
			this.textBoxOpen.TabIndex = 11;
			this.textBoxOpen.Text = "";
			// 
			// textBoxSave
			// 
			this.textBoxSave.Location = new System.Drawing.Point(16, 104);
			this.textBoxSave.Name = "textBoxSave";
			this.textBoxSave.Size = new System.Drawing.Size(272, 20);
			this.textBoxSave.TabIndex = 12;
			this.textBoxSave.Text = "";
			// 
			// textBoxAdd
			// 
			this.textBoxAdd.Location = new System.Drawing.Point(16, 160);
			this.textBoxAdd.Name = "textBoxAdd";
			this.textBoxAdd.Size = new System.Drawing.Size(272, 20);
			this.textBoxAdd.TabIndex = 13;
			this.textBoxAdd.Text = "";
			// 
			// textBoxAppDir
			// 
			this.textBoxAppDir.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.textBoxAppDir.Location = new System.Drawing.Point(16, 216);
			this.textBoxAppDir.Name = "textBoxAppDir";
			this.textBoxAppDir.ReadOnly = true;
			this.textBoxAppDir.Size = new System.Drawing.Size(272, 20);
			this.textBoxAppDir.TabIndex = 14;
			this.textBoxAppDir.Text = "";
			// 
			// errorProvider
			// 
			this.errorProvider.DataMember = null;
			// 
			// checkBoxReadFromReg
			// 
			this.checkBoxReadFromReg.Checked = true;
			this.checkBoxReadFromReg.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxReadFromReg.Enabled = false;
			this.checkBoxReadFromReg.Location = new System.Drawing.Point(176, 240);
			this.checkBoxReadFromReg.Name = "checkBoxReadFromReg";
			this.checkBoxReadFromReg.Size = new System.Drawing.Size(120, 16);
			this.checkBoxReadFromReg.TabIndex = 15;
			this.checkBoxReadFromReg.Text = "Read from registry";
			this.checkBoxReadFromReg.CheckedChanged += new System.EventHandler(this.checkBoxReadFromReg_CheckedChanged);
			// 
			// DirectoriesDialog
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(306, 319);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.checkBoxReadFromReg,
																		  this.textBoxAppDir,
																		  this.textBoxAdd,
																		  this.textBoxSave,
																		  this.textBoxOpen,
																		  this.label4,
																		  this.label3,
																		  this.label2,
																		  this.label1,
																		  this.buttonSave,
																		  this.buttonCancel,
																		  this.buttonOK});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DirectoriesDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Directories";
			this.Load += new System.EventHandler(this.DirectoriesDialog_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void DirectoriesDialog_Load(object sender, System.EventArgs e)
		{
			this.Icon=App.Res.Icon;
			textBoxOpen.Text=AppOptions.OpenDir;
			textBoxSave.Text=AppOptions.SaveDir;
			textBoxAdd.Text=AppOptions.AddDir;
			textBoxAppDir.Text=AppOptions.AppPath;
		}

		bool CheckError()
		{
			try
			{
				if(textBoxOpen.Text!="" && !System.IO.Directory.Exists(textBoxOpen.Text)) 
				{
					errorProvider.SetError(textBoxOpen,"Path does not exist");
					return false;
				}
			}
			catch(Exception E)
			{
				errorProvider.SetError(textBoxOpen,"Path is not valid");
				return false;
			}
			try
			{
				if(textBoxSave.Text!="" && !System.IO.Directory.Exists(textBoxSave.Text)) 
				{
					errorProvider.SetError(textBoxSave,"Path does not exist");
					return false;
				}
			}
			catch(Exception E)
			{
				errorProvider.SetError(textBoxSave,"Path is not valid");
				return false;
			}
			try
			{
				if(textBoxAdd.Text!="" && !System.IO.Directory.Exists(textBoxAdd.Text)) 
				{
					errorProvider.SetError(textBoxAdd,"Path does not exist");
					return false;
				}
			}
			catch(Exception E)
			{
				errorProvider.SetError(textBoxAdd,"Path is not valid");
				return false;
			}
			try
			{
				if(textBoxAppDir.Text!="" && !System.IO.Directory.Exists(textBoxAppDir.Text)) 
				{
					errorProvider.SetError(textBoxAppDir,"Path does not exist");
					return false;
				}
			}
			catch(Exception E)
			{
				errorProvider.SetError(textBoxAppDir,"Path is not valid");
				return false;
			}
			return true;
		}

		private void buttonOK_Click(object sender, System.EventArgs e)
		{
			if(AppOptions.SaveSettingsOnOK) 
				buttonSave_Click(sender,e);
			else
			{
				if(!CheckError()) return;
				AppOptions.OpenDir=textBoxOpen.Text;
				AppOptions.SaveDir=textBoxSave.Text;
				AppOptions.AddDir=textBoxAdd.Text;
				//AppOptions.AppPath=textBoxAppDir.Text;
			}
			this.DialogResult=DialogResult.OK;
			this.Close();
		}

		private void buttonSave_Click(object sender, System.EventArgs e)
		{
			if(!CheckError()) return;
			AppOptions.OpenDir=textBoxOpen.Text;
			AppOptions.SaveDir=textBoxSave.Text;
			AppOptions.AddDir=textBoxAdd.Text;
			//AppOptions.AppPath=textBoxAppDir.Text;
			AppOptions.Save();
		}

		private void checkBoxReadFromReg_CheckedChanged(object sender, System.EventArgs e)
		{
		
		}
	}
}
