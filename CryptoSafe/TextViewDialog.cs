using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace CryptoSafe
{
	/// <summary>
	/// Summary description for TextViewDialog.
	/// </summary>
	public class TextViewDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.TextBox textBoxText;
		private System.Windows.Forms.Button buttonClear;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TextViewDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			bModified=false;
			ReadOnly=true;
			text="";
		}

		public TextViewDialog(string s)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			ReadOnly=true;
			bModified=false;
			text=s;
		}

		public TextViewDialog(string s, bool bReadOnly)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			ReadOnly=bReadOnly;
			bModified=false;
			text=s;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TextViewDialog));
			this.textBoxText = new System.Windows.Forms.TextBox();
			this.buttonClose = new System.Windows.Forms.Button();
			this.buttonSave = new System.Windows.Forms.Button();
			this.buttonClear = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textBoxText
			// 
			this.textBoxText.AcceptsReturn = true;
			this.textBoxText.AcceptsTab = true;
			this.textBoxText.Location = new System.Drawing.Point(8, 16);
			this.textBoxText.Multiline = true;
			this.textBoxText.Name = "textBoxText";
			this.textBoxText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBoxText.Size = new System.Drawing.Size(344, 320);
			this.textBoxText.TabIndex = 0;
			this.textBoxText.Text = "";
			this.textBoxText.TextChanged += new System.EventHandler(this.textBoxText_TextChanged);
			// 
			// buttonClose
			// 
			this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonClose.Location = new System.Drawing.Point(368, 24);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(56, 24);
			this.buttonClose.TabIndex = 1;
			this.buttonClose.Text = "&Close";
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// buttonSave
			// 
			this.buttonSave.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonSave.Location = new System.Drawing.Point(368, 64);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(56, 24);
			this.buttonSave.TabIndex = 3;
			this.buttonSave.Text = "&Save";
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// buttonClear
			// 
			this.buttonClear.Location = new System.Drawing.Point(368, 104);
			this.buttonClear.Name = "buttonClear";
			this.buttonClear.Size = new System.Drawing.Size(56, 24);
			this.buttonClear.TabIndex = 4;
			this.buttonClear.Text = "Cl&ear";
			this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
			// 
			// TextViewDialog
			// 
			this.AcceptButton = this.buttonClose;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonClose;
			this.ClientSize = new System.Drawing.Size(432, 349);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonClear,
																		  this.buttonSave,
																		  this.buttonClose,
																		  this.textBoxText});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "TextViewDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Text Viewer";
			this.Load += new System.EventHandler(this.TextViewDialog_Load);
			this.ResumeLayout(false);

		}
		#endregion

		public bool bModified;

		protected bool bReadOnly;
		
		public bool ReadOnly
		{
			get
			{
				return bReadOnly;
			}
			set
			{
				bReadOnly=value;
				textBoxText.ReadOnly=value;
				buttonSave.Enabled=!value;
				buttonClear.Enabled=!value;
			}
		}

		public string text
		{
			get
			{
				return textBoxText.Text;
			}
			set
			{
				if(value.Length<30000)
				{
					textBoxText.Text=value;
					buttonSave.Enabled=!bReadOnly;
				}
				else
				{
					textBoxText.Text=value.Substring(0,30000);
					buttonSave.Enabled=false;
				}
			}
		}

		private void buttonClose_Click(object sender, System.EventArgs e)
		{
		 this.DialogResult=DialogResult.Cancel;
		 this.Close();
		}

		private void buttonSave_Click(object sender, System.EventArgs e)
		{
		 if(textBoxText.ReadOnly) return;
		 this.DialogResult=DialogResult.OK;
		 this.Close();
		}

		private void textBoxText_TextChanged(object sender, System.EventArgs e)
		{
			bModified=true;
		}

		private void buttonClear_Click(object sender, System.EventArgs e)
		{
			if(textBoxText.ReadOnly) return;
			bModified=true;
			textBoxText.Text="";
		}

		private void TextViewDialog_Load(object sender, System.EventArgs e)
		{
			this.Icon=App.Res.Icon;
		}
	}
}
