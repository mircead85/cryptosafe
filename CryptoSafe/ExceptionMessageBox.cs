using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace CryptoSafe
{
	/// <summary>
	/// Summary description for ExceptionMessageBox.
	/// </summary>
	public class ExceptionMessageBox : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button buttonMore;
		private System.Windows.Forms.Label labelError;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Label labelMessage;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		string txtMore;

		public ExceptionMessageBox()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

		}

		public ExceptionMessageBox(Exception E, string doing)
		{
			InitializeComponent();
			labelMessage.Text="An error ocurred while "+doing+":";
			string text = null;
			if(E is System.IO.IOException)
			{
				text = "An IO error ocurred.\n("+((System.IO.IOException)E).Message+")";
			}
			else
				if(E is FileCorruptException)
			{
				text="File is corrupted.\n("+((FileCorruptException)E).Descr+")";
			}
			else
				if(E is ArgumentException)
			{
				text = "Invalid argument passed.\n("+E.Message+")";
			}
			else 
				if(E is IndexOutOfRangeException)
			{
				text="Current context may have been corrupted.\n("+E.Message+")";
			}
			else
				if(E is GenericException)
			{
				text = ((GenericException)E).Descr;
			}
			else
			{
				text="";
				text+=E.GetType().ToString();
				text=text.Substring(0,text.LastIndexOf("Exception"));
				text+="\n("+E.Message+")";
			}
			labelError.Text=text;
			txtMore=E.ToString();
		}

		public static void Show(Exception E, string doing)
		{
			ExceptionMessageBox em=new ExceptionMessageBox(E,doing);
			em.ShowDialog();
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ExceptionMessageBox));
			this.buttonMore = new System.Windows.Forms.Button();
			this.labelError = new System.Windows.Forms.Label();
			this.labelMessage = new System.Windows.Forms.Label();
			this.buttonOK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// buttonMore
			// 
			this.buttonMore.Location = new System.Drawing.Point(168, 144);
			this.buttonMore.Name = "buttonMore";
			this.buttonMore.Size = new System.Drawing.Size(56, 24);
			this.buttonMore.TabIndex = 16;
			this.buttonMore.Text = "&More...";
			this.buttonMore.Click += new System.EventHandler(this.buttonMore_Click);
			// 
			// labelError
			// 
			this.labelError.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.labelError.Location = new System.Drawing.Point(8, 40);
			this.labelError.Name = "labelError";
			this.labelError.Size = new System.Drawing.Size(280, 80);
			this.labelError.TabIndex = 15;
			// 
			// labelMessage
			// 
			this.labelMessage.Location = new System.Drawing.Point(8, 16);
			this.labelMessage.Name = "labelMessage";
			this.labelMessage.Size = new System.Drawing.Size(272, 16);
			this.labelMessage.TabIndex = 14;
			this.labelMessage.Text = "An error ocurred while...: ";
			// 
			// buttonOK
			// 
			this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOK.Location = new System.Drawing.Point(64, 144);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(64, 24);
			this.buttonOK.TabIndex = 10;
			this.buttonOK.Text = "O&K";
			// 
			// ExceptionMessageBox
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonOK;
			this.ClientSize = new System.Drawing.Size(296, 181);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonMore,
																		  this.labelError,
																		  this.labelMessage,
																		  this.buttonOK});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ExceptionMessageBox";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Error";
			this.Load += new System.EventHandler(this.ExceptionMessageBox_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void ExceptionMessageBox_Load(object sender, System.EventArgs e)
		{
			this.Icon=App.Res.Icon;
		}

		private void buttonMore_Click(object sender, System.EventArgs e)
		{
			MessageBox.Show(txtMore,"Detailed information",MessageBoxButtons.OK,MessageBoxIcon.Information);
		}
	}
}
