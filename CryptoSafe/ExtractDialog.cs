using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace CryptoSafe
{
	/// <summary>
	/// Summary description for Extract.
	/// </summary>
	public class ExtractDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button buttonHelp;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxTo;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox checkBoxUsePaths;
		private System.Windows.Forms.CheckBox checkBoxIgnoreErrors;
		private System.Windows.Forms.Button buttonGoto;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.CheckBox checkBoxBackground;
		private System.Windows.Forms.ErrorProvider errorProvider;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ExtractDialog()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ExtractDialog));
			this.buttonHelp = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOk = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxTo = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.checkBoxBackground = new System.Windows.Forms.CheckBox();
			this.checkBoxIgnoreErrors = new System.Windows.Forms.CheckBox();
			this.checkBoxUsePaths = new System.Windows.Forms.CheckBox();
			this.buttonGoto = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.errorProvider = new System.Windows.Forms.ErrorProvider();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonHelp
			// 
			this.buttonHelp.Location = new System.Drawing.Point(248, 216);
			this.buttonHelp.Name = "buttonHelp";
			this.buttonHelp.Size = new System.Drawing.Size(64, 24);
			this.buttonHelp.TabIndex = 5;
			this.buttonHelp.Text = "&Help";
			this.buttonHelp.Click += new System.EventHandler(this.buttonHelp_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(144, 216);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(64, 24);
			this.buttonCancel.TabIndex = 4;
			this.buttonCancel.Text = "&Cancel";
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// buttonOk
			// 
			this.buttonOk.Location = new System.Drawing.Point(32, 216);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.Size = new System.Drawing.Size(64, 24);
			this.buttonOk.TabIndex = 3;
			this.buttonOk.Text = "O&K";
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(64, 16);
			this.label1.TabIndex = 10;
			this.label1.Text = "Extract To:";
			// 
			// textBoxTo
			// 
			this.textBoxTo.Location = new System.Drawing.Point(16, 40);
			this.textBoxTo.Name = "textBoxTo";
			this.textBoxTo.Size = new System.Drawing.Size(304, 20);
			this.textBoxTo.TabIndex = 0;
			this.textBoxTo.Text = "";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.checkBoxBackground,
																					this.checkBoxIgnoreErrors,
																					this.checkBoxUsePaths});
			this.groupBox1.Location = new System.Drawing.Point(16, 72);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(114, 120);
			this.groupBox1.TabIndex = 12;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Options";
			// 
			// checkBoxBackground
			// 
			this.checkBoxBackground.Location = new System.Drawing.Point(16, 88);
			this.checkBoxBackground.Name = "checkBoxBackground";
			this.checkBoxBackground.Size = new System.Drawing.Size(95, 24);
			this.checkBoxBackground.TabIndex = 8;
			this.checkBoxBackground.Text = "&Background";
			// 
			// checkBoxIgnoreErrors
			// 
			this.checkBoxIgnoreErrors.Location = new System.Drawing.Point(16, 56);
			this.checkBoxIgnoreErrors.Name = "checkBoxIgnoreErrors";
			this.checkBoxIgnoreErrors.Size = new System.Drawing.Size(95, 24);
			this.checkBoxIgnoreErrors.TabIndex = 7;
			this.checkBoxIgnoreErrors.Text = "I&gnore errors";
			// 
			// checkBoxUsePaths
			// 
			this.checkBoxUsePaths.Checked = true;
			this.checkBoxUsePaths.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxUsePaths.Location = new System.Drawing.Point(16, 24);
			this.checkBoxUsePaths.Name = "checkBoxUsePaths";
			this.checkBoxUsePaths.Size = new System.Drawing.Size(88, 24);
			this.checkBoxUsePaths.TabIndex = 6;
			this.checkBoxUsePaths.Text = "Use &Paths";
			// 
			// buttonGoto
			// 
			this.buttonGoto.Font = new System.Drawing.Font("Arial Black", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.buttonGoto.Location = new System.Drawing.Point(296, 64);
			this.buttonGoto.Name = "buttonGoto";
			this.buttonGoto.Size = new System.Drawing.Size(24, 16);
			this.buttonGoto.TabIndex = 1;
			this.buttonGoto.Text = "...";
			this.buttonGoto.Click += new System.EventHandler(this.buttonGoto_Click);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Bitmap)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(152, 80);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(144, 120);
			this.pictureBox1.TabIndex = 15;
			this.pictureBox1.TabStop = false;
			// 
			// errorProvider
			// 
			this.errorProvider.DataMember = null;
			// 
			// ExtractDialog
			// 
			this.AcceptButton = this.buttonOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(344, 253);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.pictureBox1,
																		  this.buttonGoto,
																		  this.groupBox1,
																		  this.textBoxTo,
																		  this.label1,
																		  this.buttonHelp,
																		  this.buttonCancel,
																		  this.buttonOk});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ExtractDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Extract...";
			this.Load += new System.EventHandler(this.ExtractDialog_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonCancel_Click(object sender, System.EventArgs e)
		{
		this.DialogResult=DialogResult.Cancel;
		this.Close();
		}

		private void buttonOk_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(textBoxTo.Text.Length>0 && !System.IO.Directory.Exists(textBoxTo.Text)) 
				{
				 errorProvider.SetError(textBoxTo,"Path does not exist");
				 return;
				}
			}
			catch(Exception E)
			{
				errorProvider.SetError(textBoxTo,"Invalid path");
				return;
			}
		   this.DialogResult=DialogResult.OK;
		   this.Close();
		}

		private void buttonHelp_Click(object sender, System.EventArgs e)
		{
		
		}

		public void SetOptions()
		{
			checkBoxUsePaths.Checked=ExtractOptions.usePaths;
			checkBoxIgnoreErrors.Checked=!AppOptions.ExtractHaltOnFail;
			checkBoxBackground.Checked=ExtractOptions.bBackground;
			textBoxTo.Text=ExtractOptions.to;
			buttonGoto_Click(this,new System.EventArgs());
		}

		public void GetOptions()
		{
		 ExtractOptions.bBackground=checkBoxBackground.Checked;
		 ExtractOptions.to=textBoxTo.Text;
		 ExtractOptions.usePaths=checkBoxUsePaths.Checked;
   		 AppOptions.ExtractHaltOnFail=!checkBoxIgnoreErrors.Checked;
		}

		private void buttonGoto_Click(object sender, System.EventArgs e)
		{
		
		}

		private void ExtractDialog_Load(object sender, System.EventArgs e)
		{
			this.Icon=App.Res.Icon;
			textBoxTo.Focus();
		}
	}
}
