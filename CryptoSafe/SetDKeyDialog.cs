using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace CryptoSafe
{
	/// <summary>
	/// Summary description for SetDKeyDialog.
	/// </summary>
	public class SetDKeyDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button buttonHelp;
		private System.Windows.Forms.CheckBox checkBoxUseLast;
		private System.Windows.Forms.TextBox textBoxKey;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOk;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.CheckBox checkBoxUseCurrrentKeys;
		private System.Windows.Forms.ErrorProvider errorProvider;
		private System.Windows.Forms.HelpProvider helpProvider;

		protected string key;

		public string GetKey() {return key;}

		public SetDKeyDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			if(SetKeyAlgoDialog.lastKey!=null) checkBoxUseLast.Enabled=true;
			else checkBoxUseLast.Enabled=false;

			AllowMaintainKeys=true;

			textBoxKey.Focus();
		}

		public SetDKeyDialog(bool b)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			if(SetKeyAlgoDialog.lastKey!=null) checkBoxUseLast.Enabled=true;
			else checkBoxUseLast.Enabled=false;

			AllowMaintainKeys=b;

			textBoxKey.Focus();
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SetDKeyDialog));
			this.buttonHelp = new System.Windows.Forms.Button();
			this.checkBoxUseLast = new System.Windows.Forms.CheckBox();
			this.textBoxKey = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOk = new System.Windows.Forms.Button();
			this.checkBoxUseCurrrentKeys = new System.Windows.Forms.CheckBox();
			this.errorProvider = new System.Windows.Forms.ErrorProvider();
			this.helpProvider = new System.Windows.Forms.HelpProvider();
			this.SuspendLayout();
			// 
			// buttonHelp
			// 
			this.buttonHelp.Location = new System.Drawing.Point(176, 96);
			this.buttonHelp.Name = "buttonHelp";
			this.buttonHelp.Size = new System.Drawing.Size(64, 24);
			this.buttonHelp.TabIndex = 20;
			this.buttonHelp.Text = "&Help";
			this.buttonHelp.Click += new System.EventHandler(this.buttonHelp_Click);
			// 
			// checkBoxUseLast
			// 
			this.checkBoxUseLast.Location = new System.Drawing.Point(8, 72);
			this.checkBoxUseLast.Name = "checkBoxUseLast";
			this.checkBoxUseLast.Size = new System.Drawing.Size(128, 16);
			this.checkBoxUseLast.TabIndex = 19;
			this.checkBoxUseLast.Text = "Use &last key entered";
			this.checkBoxUseLast.CheckedChanged += new System.EventHandler(this.checkBoxUseLast_CheckedChanged);
			// 
			// textBoxKey
			// 
			this.textBoxKey.Location = new System.Drawing.Point(8, 32);
			this.textBoxKey.Name = "textBoxKey";
			this.textBoxKey.PasswordChar = '*';
			this.textBoxKey.Size = new System.Drawing.Size(144, 20);
			this.textBoxKey.TabIndex = 14;
			this.textBoxKey.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(32, 16);
			this.label1.TabIndex = 16;
			this.label1.Text = "Key:";
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(176, 56);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(64, 24);
			this.buttonCancel.TabIndex = 18;
			this.buttonCancel.Text = "&Cancel";
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// buttonOk
			// 
			this.buttonOk.Location = new System.Drawing.Point(176, 16);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.Size = new System.Drawing.Size(64, 24);
			this.buttonOk.TabIndex = 17;
			this.buttonOk.Text = "O&K";
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// checkBoxUseCurrrentKeys
			// 
			this.checkBoxUseCurrrentKeys.Location = new System.Drawing.Point(8, 96);
			this.checkBoxUseCurrrentKeys.Name = "checkBoxUseCurrrentKeys";
			this.checkBoxUseCurrrentKeys.Size = new System.Drawing.Size(128, 16);
			this.checkBoxUseCurrrentKeys.TabIndex = 21;
			this.checkBoxUseCurrrentKeys.Text = "Use cu&rrent keys";
			this.checkBoxUseCurrrentKeys.CheckedChanged += new System.EventHandler(this.checkBoxUseCurrrentKeys_CheckedChanged);
			// 
			// SetDKeyDialog
			// 
			this.AcceptButton = this.buttonOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(248, 125);
			this.ControlBox = false;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.checkBoxUseCurrrentKeys,
																		  this.buttonHelp,
																		  this.checkBoxUseLast,
																		  this.textBoxKey,
																		  this.label1,
																		  this.buttonCancel,
																		  this.buttonOk});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SetDKeyDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Set decryption key";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.SetDKeyDialog_Load);
			this.ResumeLayout(false);

		}
		#endregion

		bool bMaintainKeys;

		public bool AllowMaintainKeys
		{
			get
			{
				return bMaintainKeys;
			}
			set
			{
				bMaintainKeys=value;
				checkBoxUseCurrrentKeys.Enabled=value;
			}
		}
	
		private void buttonOk_Click(object sender, System.EventArgs e)
		{
			if(!checkBoxUseLast.Checked)
			{	
				if(!checkBoxUseCurrrentKeys.Checked)
				{
					if(textBoxKey.Text.Length<7)
					{errorProvider.SetError(textBoxKey,"Password must be at least 7 characters long");return;}
				
					if(textBoxKey.Text.Length>32)
					{errorProvider.SetError(textBoxKey,"Password must be at most 32 characters long");return;}

					key = SetKeyAlgoDialog.lastKey = textBoxKey.Text;
				}
				else
				{
					key=null;
					this.DialogResult=DialogResult.Ignore;
					this.Close();
					return;
				}
			}
			else key=SetKeyAlgoDialog.lastKey;
			this.DialogResult=DialogResult.OK;
			this.Close();
		}

		private void buttonHelp_Click(object sender, System.EventArgs e)
		{
			MessageBox.Show("You will get help soon","Help",MessageBoxButtons.OK,MessageBoxIcon.Information);
			return;
		}

		private void buttonCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult=DialogResult.Cancel;
			return;
		}

		private void checkBoxUseLast_CheckedChanged(object sender, System.EventArgs e)
		{
			if(checkBoxUseLast.Checked || checkBoxUseCurrrentKeys.Checked)
				textBoxKey.Enabled=false;
			else
				textBoxKey.Enabled=true;
		}

		private void checkBoxUseCurrrentKeys_CheckedChanged(object sender, System.EventArgs e)
		{
			if(checkBoxUseLast.Checked || checkBoxUseCurrrentKeys.Checked)
				textBoxKey.Enabled=false;
			else
				textBoxKey.Enabled=true;
		}

		private void SetDKeyDialog_Load(object sender, System.EventArgs e)
		{
			this.Icon=App.Res.Icon;
			textBoxKey.Focus();
		}
	}
}
