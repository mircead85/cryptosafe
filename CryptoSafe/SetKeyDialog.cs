using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace CryptoSafe
{
	/// <summary>
	/// Summary description for SetKeyDialog.
	/// </summary>
	public class SetKeyDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button buttonHelp;
		private System.Windows.Forms.TextBox textBoxKey2;
		private System.Windows.Forms.CheckBox checkBoxUseLast;
		private System.Windows.Forms.TextBox textBoxKey;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.ErrorProvider errorProvider;
		private System.Windows.Forms.HelpProvider helpProvider;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SetKeyDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			if(SetKeyAlgoDialog.lastKey!=null) checkBoxUseLast.Enabled=true;
			else checkBoxUseLast.Enabled=false;

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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SetKeyDialog));
			this.buttonHelp = new System.Windows.Forms.Button();
			this.textBoxKey2 = new System.Windows.Forms.TextBox();
			this.checkBoxUseLast = new System.Windows.Forms.CheckBox();
			this.textBoxKey = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOk = new System.Windows.Forms.Button();
			this.errorProvider = new System.Windows.Forms.ErrorProvider();
			this.helpProvider = new System.Windows.Forms.HelpProvider();
			this.SuspendLayout();
			// 
			// buttonHelp
			// 
			this.buttonHelp.Location = new System.Drawing.Point(176, 88);
			this.buttonHelp.Name = "buttonHelp";
			this.buttonHelp.Size = new System.Drawing.Size(64, 24);
			this.buttonHelp.TabIndex = 13;
			this.buttonHelp.Text = "&Help";
			this.buttonHelp.Click += new System.EventHandler(this.buttonHelp_Click);
			// 
			// textBoxKey2
			// 
			this.textBoxKey2.Location = new System.Drawing.Point(8, 64);
			this.textBoxKey2.Name = "textBoxKey2";
			this.textBoxKey2.PasswordChar = '*';
			this.textBoxKey2.Size = new System.Drawing.Size(144, 20);
			this.textBoxKey2.TabIndex = 8;
			this.textBoxKey2.Text = "";
			// 
			// checkBoxUseLast
			// 
			this.checkBoxUseLast.Location = new System.Drawing.Point(8, 96);
			this.checkBoxUseLast.Name = "checkBoxUseLast";
			this.checkBoxUseLast.Size = new System.Drawing.Size(128, 16);
			this.checkBoxUseLast.TabIndex = 12;
			this.checkBoxUseLast.Text = "Use last key entered";
			this.checkBoxUseLast.CheckedChanged += new System.EventHandler(this.checkBoxUseLast_CheckedChanged);
			// 
			// textBoxKey
			// 
			this.textBoxKey.Location = new System.Drawing.Point(8, 32);
			this.textBoxKey.Name = "textBoxKey";
			this.textBoxKey.PasswordChar = '*';
			this.textBoxKey.Size = new System.Drawing.Size(144, 20);
			this.textBoxKey.TabIndex = 7;
			this.textBoxKey.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(32, 16);
			this.label1.TabIndex = 9;
			this.label1.Text = "Key:";
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(176, 48);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(64, 24);
			this.buttonCancel.TabIndex = 11;
			this.buttonCancel.Text = "&Cancel";
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// buttonOk
			// 
			this.buttonOk.Location = new System.Drawing.Point(176, 8);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.Size = new System.Drawing.Size(64, 24);
			this.buttonOk.TabIndex = 10;
			this.buttonOk.Text = "O&K";
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// errorProvider
			// 
			this.errorProvider.DataMember = null;
			// 
			// SetKeyDialog
			// 
			this.AcceptButton = this.buttonOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(248, 125);
			this.ControlBox = false;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonHelp,
																		  this.textBoxKey2,
																		  this.checkBoxUseLast,
																		  this.textBoxKey,
																		  this.label1,
																		  this.buttonCancel,
																		  this.buttonOk});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SetKeyDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Set key...";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.SetKeyDialog_Load);
			this.ResumeLayout(false);

		}
		#endregion

		protected string key;

		public string GetKey() {return key;}

		private void buttonOk_Click(object sender, System.EventArgs e)
		{
			if(!checkBoxUseLast.Checked)
			{			 
				if(textBoxKey.Text!=textBoxKey2.Text)
				{errorProvider.SetError(textBoxKey2,"Password re-entered incorectlly.");return;}
				
				if(textBoxKey.Text.Length<7)
				{errorProvider.SetError(textBoxKey,"Password must be at least 7 characters long");return;}
				
				if(textBoxKey.Text.Length>20)
				{errorProvider.SetError(textBoxKey,"Password must be at most 20 characters long");return;}

				key = SetKeyAlgoDialog.lastKey = textBoxKey.Text;
			}
			else key=SetKeyAlgoDialog.lastKey;
        this.DialogResult=DialogResult.OK;
		this.Close();
		}

		private void checkBoxUseLast_CheckedChanged(object sender, System.EventArgs e)
		{
			textBoxKey.Enabled=!textBoxKey.Enabled;
			textBoxKey2.Enabled=!textBoxKey2.Enabled;
		}

		private void buttonHelp_Click(object sender, System.EventArgs e)
		{
			MessageBox.Show("You will get help soon","Help",MessageBoxButtons.OK,MessageBoxIcon.Information);
			this.DialogResult=DialogResult.Cancel;
			return;
		}

		private void buttonCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult=DialogResult.Cancel;
			return;
		}

		private void SetKeyDialog_Load(object sender, System.EventArgs e)
		{
			this.Icon=App.Res.Icon;
			textBoxKey.Focus();
		}
	}
}
