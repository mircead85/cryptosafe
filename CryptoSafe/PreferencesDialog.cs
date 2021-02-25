using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace CryptoSafe
{
	/// <summary>
	/// Summary description for PreferencesDialog.
	/// </summary>
	public class PreferencesDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.CheckBox checkBoxGridLines;
		private System.Windows.Forms.CheckBox checkBoxDrawIcons;
		private System.Windows.Forms.CheckBox checkBoxDestroyFiles;
		private System.Windows.Forms.CheckBox checkBoxDeleteTempFiles;
		private System.Windows.Forms.CheckBox checkBoxRevUpdate;
		private System.Windows.Forms.CheckBox checkBoxNotifyUpdate;
		private System.Windows.Forms.CheckBox checkBoxDefHaltOnErr;
		private System.Windows.Forms.CheckBox checkBoxSaveSettings;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.CheckBox checkBoxRemoveOnAdd;
		private System.Windows.Forms.CheckBox checkBoxPromptforKey;
		private System.Windows.Forms.CheckBox checkBoxUseLastKey;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public PreferencesDialog()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PreferencesDialog));
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonSave = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.checkBoxDrawIcons = new System.Windows.Forms.CheckBox();
			this.checkBoxGridLines = new System.Windows.Forms.CheckBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.checkBoxNotifyUpdate = new System.Windows.Forms.CheckBox();
			this.checkBoxRevUpdate = new System.Windows.Forms.CheckBox();
			this.checkBoxDeleteTempFiles = new System.Windows.Forms.CheckBox();
			this.checkBoxDestroyFiles = new System.Windows.Forms.CheckBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.checkBoxSaveSettings = new System.Windows.Forms.CheckBox();
			this.checkBoxDefHaltOnErr = new System.Windows.Forms.CheckBox();
			this.checkBoxRemoveOnAdd = new System.Windows.Forms.CheckBox();
			this.checkBoxPromptforKey = new System.Windows.Forms.CheckBox();
			this.checkBoxUseLastKey = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonOK
			// 
			this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOK.Location = new System.Drawing.Point(112, 312);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(64, 24);
			this.buttonOK.TabIndex = 1;
			this.buttonOK.Text = "O&K";
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(216, 312);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(64, 24);
			this.buttonCancel.TabIndex = 2;
			this.buttonCancel.Text = "&Cancel";
			// 
			// buttonSave
			// 
			this.buttonSave.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonSave.Location = new System.Drawing.Point(328, 312);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(64, 24);
			this.buttonSave.TabIndex = 3;
			this.buttonSave.Text = "&Save";
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.checkBoxDrawIcons,
																					this.checkBoxGridLines});
			this.groupBox1.Location = new System.Drawing.Point(24, 24);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(208, 88);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Apperance";
			// 
			// checkBoxDrawIcons
			// 
			this.checkBoxDrawIcons.Location = new System.Drawing.Point(16, 48);
			this.checkBoxDrawIcons.Name = "checkBoxDrawIcons";
			this.checkBoxDrawIcons.Size = new System.Drawing.Size(152, 32);
			this.checkBoxDrawIcons.TabIndex = 1;
			this.checkBoxDrawIcons.Text = "Sh&ow associated icons for files (takes longer)";
			// 
			// checkBoxGridLines
			// 
			this.checkBoxGridLines.Location = new System.Drawing.Point(16, 24);
			this.checkBoxGridLines.Name = "checkBoxGridLines";
			this.checkBoxGridLines.Size = new System.Drawing.Size(184, 16);
			this.checkBoxGridLines.TabIndex = 0;
			this.checkBoxGridLines.Text = "Show &grid lines in main list view";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.checkBoxRemoveOnAdd,
																					this.checkBoxNotifyUpdate,
																					this.checkBoxRevUpdate,
																					this.checkBoxDeleteTempFiles,
																					this.checkBoxDestroyFiles});
			this.groupBox2.Location = new System.Drawing.Point(264, 24);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(208, 264);
			this.groupBox2.TabIndex = 5;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Operation";
			// 
			// checkBoxNotifyUpdate
			// 
			this.checkBoxNotifyUpdate.Location = new System.Drawing.Point(16, 164);
			this.checkBoxNotifyUpdate.Name = "checkBoxNotifyUpdate";
			this.checkBoxNotifyUpdate.Size = new System.Drawing.Size(184, 32);
			this.checkBoxNotifyUpdate.TabIndex = 3;
			this.checkBoxNotifyUpdate.Text = "Inform when the content of an item has been &updated";
			// 
			// checkBoxRevUpdate
			// 
			this.checkBoxRevUpdate.Location = new System.Drawing.Point(16, 112);
			this.checkBoxRevUpdate.Name = "checkBoxRevUpdate";
			this.checkBoxRevUpdate.Size = new System.Drawing.Size(184, 32);
			this.checkBoxRevUpdate.TabIndex = 2;
			this.checkBoxRevUpdate.Text = "After e&xecuting an item, update the file with it\'s new content";
			this.checkBoxRevUpdate.CheckedChanged += new System.EventHandler(this.checkBoxRevUpdate_CheckedChanged);
			// 
			// checkBoxDeleteTempFiles
			// 
			this.checkBoxDeleteTempFiles.Location = new System.Drawing.Point(16, 76);
			this.checkBoxDeleteTempFiles.Name = "checkBoxDeleteTempFiles";
			this.checkBoxDeleteTempFiles.Size = new System.Drawing.Size(152, 16);
			this.checkBoxDeleteTempFiles.TabIndex = 1;
			this.checkBoxDeleteTempFiles.Text = "Delete &temporary files";
			// 
			// checkBoxDestroyFiles
			// 
			this.checkBoxDestroyFiles.Location = new System.Drawing.Point(16, 24);
			this.checkBoxDestroyFiles.Name = "checkBoxDestroyFiles";
			this.checkBoxDestroyFiles.Size = new System.Drawing.Size(184, 32);
			this.checkBoxDestroyFiles.TabIndex = 0;
			this.checkBoxDestroyFiles.Text = "&Destroy files instead of just deleting (takes longer)";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.checkBoxUseLastKey,
																					this.checkBoxPromptforKey,
																					this.checkBoxSaveSettings,
																					this.checkBoxDefHaltOnErr});
			this.groupBox3.Location = new System.Drawing.Point(24, 120);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(208, 168);
			this.groupBox3.TabIndex = 5;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Default settings";
			// 
			// checkBoxSaveSettings
			// 
			this.checkBoxSaveSettings.Location = new System.Drawing.Point(16, 48);
			this.checkBoxSaveSettings.Name = "checkBoxSaveSettings";
			this.checkBoxSaveSettings.Size = new System.Drawing.Size(152, 32);
			this.checkBoxSaveSettings.TabIndex = 1;
			this.checkBoxSaveSettings.Text = "&Save settings when clicking OK";
			// 
			// checkBoxDefHaltOnErr
			// 
			this.checkBoxDefHaltOnErr.Location = new System.Drawing.Point(16, 24);
			this.checkBoxDefHaltOnErr.Name = "checkBoxDefHaltOnErr";
			this.checkBoxDefHaltOnErr.Size = new System.Drawing.Size(184, 16);
			this.checkBoxDefHaltOnErr.TabIndex = 0;
			this.checkBoxDefHaltOnErr.Text = "&Halt on erros by default";
			// 
			// checkBoxRemoveOnAdd
			// 
			this.checkBoxRemoveOnAdd.Location = new System.Drawing.Point(16, 216);
			this.checkBoxRemoveOnAdd.Name = "checkBoxRemoveOnAdd";
			this.checkBoxRemoveOnAdd.Size = new System.Drawing.Size(184, 32);
			this.checkBoxRemoveOnAdd.TabIndex = 4;
			this.checkBoxRemoveOnAdd.Text = "&Remove file once it has been successfully added";
			// 
			// checkBoxPromptforKey
			// 
			this.checkBoxPromptforKey.Location = new System.Drawing.Point(16, 88);
			this.checkBoxPromptforKey.Name = "checkBoxPromptforKey";
			this.checkBoxPromptforKey.Size = new System.Drawing.Size(152, 32);
			this.checkBoxPromptforKey.TabIndex = 2;
			this.checkBoxPromptforKey.Text = "&Prompt for key on file open";
			// 
			// checkBoxUseLastKey
			// 
			this.checkBoxUseLastKey.Location = new System.Drawing.Point(16, 128);
			this.checkBoxUseLastKey.Name = "checkBoxUseLastKey";
			this.checkBoxUseLastKey.Size = new System.Drawing.Size(152, 32);
			this.checkBoxUseLastKey.TabIndex = 3;
			this.checkBoxUseLastKey.Text = "Use last &key by default on add";
			// 
			// PreferencesDialog
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(506, 351);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.groupBox1,
																		  this.buttonSave,
																		  this.buttonCancel,
																		  this.buttonOK,
																		  this.groupBox2,
																		  this.groupBox3});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PreferencesDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Preferences";
			this.Load += new System.EventHandler(this.PreferencesDialog_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void PreferencesDialog_Load(object sender, System.EventArgs e)
		{
			checkBoxGridLines.Checked=AppOptions.ShowGridLines;
			checkBoxDeleteTempFiles.Checked=AppOptions.DeleteTempFiles;
			checkBoxDeleteTempFiles.Enabled=false;
			checkBoxDestroyFiles.Checked=AppOptions.DestroyFiles;
			checkBoxDrawIcons.Checked=AppOptions.DrawIcons;
			checkBoxNotifyUpdate.Checked=AppOptions.NotifyUpdate;
			checkBoxRevUpdate.Checked=AppOptions.ReverseUpdate;
			checkBoxDefHaltOnErr.Checked=AppOptions.ExtractHaltOnFail;
			checkBoxSaveSettings.Checked=AppOptions.SaveSettingsOnOK;
			checkBoxRemoveOnAdd.Checked=AppOptions.DelAfterAdding;
			checkBoxPromptforKey.Checked=AppOptions.SetDKeyOnOpen;
			checkBoxUseLastKey.Checked=AppOptions.UseLastKeyByDefault;

			checkBoxNotifyUpdate.Enabled=checkBoxRevUpdate.Checked;
		}

		private void buttonOK_Click(object sender, System.EventArgs e)
		{
			if(AppOptions.SaveSettingsOnOK) buttonSave_Click(sender,e);
			AppOptions.ShowGridLines=checkBoxGridLines.Checked;
			AppOptions.DeleteTempFiles=checkBoxDeleteTempFiles.Checked;
			AppOptions.DestroyFiles=checkBoxDestroyFiles.Checked;
			AppOptions.DrawIcons=checkBoxDrawIcons.Checked;
			AppOptions.NotifyUpdate=checkBoxNotifyUpdate.Checked;
			AppOptions.ReverseUpdate=checkBoxRevUpdate.Checked;
			AppOptions.ExtractHaltOnFail=checkBoxDefHaltOnErr.Checked;
			AppOptions.SaveSettingsOnOK=checkBoxSaveSettings.Checked;
			AppOptions.DelAfterAdding=checkBoxRemoveOnAdd.Checked;
			AppOptions.SetDKeyOnOpen=checkBoxPromptforKey.Checked;
			AppOptions.UseLastKeyByDefault=checkBoxUseLastKey.Checked;

		}

		private void buttonSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				AppOptions.ShowGridLines=checkBoxGridLines.Checked;
				AppOptions.DeleteTempFiles=checkBoxDeleteTempFiles.Checked;
				AppOptions.DestroyFiles=checkBoxDestroyFiles.Checked;
				AppOptions.DrawIcons=checkBoxDrawIcons.Checked;
				AppOptions.NotifyUpdate=checkBoxNotifyUpdate.Checked;
				AppOptions.ReverseUpdate=checkBoxRevUpdate.Checked;
				AppOptions.ExtractHaltOnFail=checkBoxDefHaltOnErr.Checked;
				AppOptions.SaveSettingsOnOK=checkBoxSaveSettings.Checked;
				AppOptions.DelAfterAdding=checkBoxRemoveOnAdd.Checked;
				AppOptions.SetDKeyOnOpen=checkBoxPromptforKey.Checked;
				AppOptions.UseLastKeyByDefault=checkBoxUseLastKey.Checked;
				AppOptions.Save();
			}
			catch(Exception E)
			{
				App.ShowError(E,"saving settings");
			}
		}

		private void checkBoxRevUpdate_CheckedChanged(object sender, System.EventArgs e)
		{
			checkBoxNotifyUpdate.Enabled=checkBoxRevUpdate.Checked;
		}
	}
}
