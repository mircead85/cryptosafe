using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace CryptoSafe
{
	/// <summary>
	/// Summary description for OpenDirectoryDialog.
	/// </summary>
	public class OpenDirectoryDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.TextBox textBoxDir;
		private System.Windows.Forms.ErrorProvider errorProvider;
		private System.Windows.Forms.Button buttonGoto;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public OpenDirectoryDialog()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(OpenDirectoryDialog));
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.textBoxDir = new System.Windows.Forms.TextBox();
			this.errorProvider = new System.Windows.Forms.ErrorProvider();
			this.buttonGoto = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point(368, 8);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(56, 24);
			this.buttonOK.TabIndex = 0;
			this.buttonOK.Text = "&OK";
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(368, 40);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(56, 24);
			this.buttonCancel.TabIndex = 1;
			this.buttonCancel.Text = "&Cancel";
			// 
			// textBoxDir
			// 
			this.textBoxDir.Location = new System.Drawing.Point(8, 24);
			this.textBoxDir.Name = "textBoxDir";
			this.textBoxDir.Size = new System.Drawing.Size(320, 20);
			this.textBoxDir.TabIndex = 2;
			this.textBoxDir.Text = "D:\\T2";
			// 
			// buttonGoto
			// 
			this.buttonGoto.Font = new System.Drawing.Font("Arial Black", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.buttonGoto.Location = new System.Drawing.Point(304, 46);
			this.buttonGoto.Name = "buttonGoto";
			this.buttonGoto.Size = new System.Drawing.Size(24, 16);
			this.buttonGoto.TabIndex = 3;
			this.buttonGoto.Text = "...";
			// 
			// OpenDirectoryDialog
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(432, 69);
			this.ControlBox = false;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonGoto,
																		  this.textBoxDir,
																		  this.buttonCancel,
																		  this.buttonOK});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "OpenDirectoryDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Select Directory...";
			this.ResumeLayout(false);

		}
		#endregion

		string dname;

		public string DirName
		{
			get
			{
				if(dname[dname.Length-1]=='\\') dname=dname.Substring(0,dname.Length-1);
				return dname;
			}
			set
			{
				dname=value;
				textBoxDir.Text=value;
			}
		}

		private void buttonOK_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(!System.IO.Directory.Exists(textBoxDir.Text)) 
				{
					errorProvider.SetError(textBoxDir,"Path does not exist");
					return;
				}
			}
			catch(Exception E)
			{
				errorProvider.SetError(textBoxDir,"Invalid path");
				return;
			}
			dname=textBoxDir.Text;
			this.DialogResult=DialogResult.OK;
			this.Close();
		}
	}
}
