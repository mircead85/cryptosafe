using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace CryptoSafe
{
	/// <summary>
	/// Summary description for ExceptionDialog.
	/// </summary>
	public class ExceptionDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonAbort;
		private System.Windows.Forms.Button buttonRetry;
		private System.Windows.Forms.Button buttonIgnore;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button buttonMore;
		private System.Windows.Forms.Label labelError;
		private System.Windows.Forms.Label labelWhere;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ExceptionDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			txtMore = "No detailed information available";
		}

		string txtMore;

		public ExceptionDialog(string wh, string pr, bool bRet, string more)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			labelWhere.Text=wh;
			labelError.Text=pr;
			buttonRetry.Enabled=bRet;
			txtMore=more;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ExceptionDialog));
			this.label1 = new System.Windows.Forms.Label();
			this.buttonAbort = new System.Windows.Forms.Button();
			this.buttonRetry = new System.Windows.Forms.Button();
			this.buttonIgnore = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.buttonMore = new System.Windows.Forms.Button();
			this.labelError = new System.Windows.Forms.Label();
			this.labelWhere = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(24, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(184, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "An error ocurred while processing:";
			// 
			// buttonAbort
			// 
			this.buttonAbort.DialogResult = System.Windows.Forms.DialogResult.Abort;
			this.buttonAbort.Location = new System.Drawing.Point(97, 144);
			this.buttonAbort.Name = "buttonAbort";
			this.buttonAbort.Size = new System.Drawing.Size(64, 24);
			this.buttonAbort.TabIndex = 1;
			this.buttonAbort.Text = "&Abort";
			// 
			// buttonRetry
			// 
			this.buttonRetry.DialogResult = System.Windows.Forms.DialogResult.Retry;
			this.buttonRetry.Location = new System.Drawing.Point(193, 144);
			this.buttonRetry.Name = "buttonRetry";
			this.buttonRetry.Size = new System.Drawing.Size(64, 24);
			this.buttonRetry.TabIndex = 2;
			this.buttonRetry.Text = "&Retry";
			// 
			// buttonIgnore
			// 
			this.buttonIgnore.DialogResult = System.Windows.Forms.DialogResult.Ignore;
			this.buttonIgnore.Location = new System.Drawing.Point(289, 144);
			this.buttonIgnore.Name = "buttonIgnore";
			this.buttonIgnore.Size = new System.Drawing.Size(64, 24);
			this.buttonIgnore.TabIndex = 3;
			this.buttonIgnore.Text = "I&gnore";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(272, 16);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(96, 16);
			this.label3.TabIndex = 6;
			this.label3.Text = "The problem was:";
			// 
			// buttonMore
			// 
			this.buttonMore.Location = new System.Drawing.Point(385, 144);
			this.buttonMore.Name = "buttonMore";
			this.buttonMore.Size = new System.Drawing.Size(56, 24);
			this.buttonMore.TabIndex = 8;
			this.buttonMore.Text = "&More...";
			this.buttonMore.Click += new System.EventHandler(this.buttonMore_Click);
			// 
			// labelError
			// 
			this.labelError.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.labelError.Location = new System.Drawing.Point(272, 40);
			this.labelError.Name = "labelError";
			this.labelError.Size = new System.Drawing.Size(256, 80);
			this.labelError.TabIndex = 7;
			// 
			// labelWhere
			// 
			this.labelWhere.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.labelWhere.Location = new System.Drawing.Point(24, 40);
			this.labelWhere.Name = "labelWhere";
			this.labelWhere.Size = new System.Drawing.Size(200, 80);
			this.labelWhere.TabIndex = 5;
			// 
			// ExceptionDialog
			// 
			this.AcceptButton = this.buttonRetry;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonAbort;
			this.ClientSize = new System.Drawing.Size(538, 183);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonMore,
																		  this.labelError,
																		  this.label3,
																		  this.labelWhere,
																		  this.buttonIgnore,
																		  this.buttonRetry,
																		  this.buttonAbort,
																		  this.label1});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ExceptionDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Error";
			this.Load += new System.EventHandler(this.ExceptionDialog_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void ExceptionDialog_Load(object sender, System.EventArgs e)
		{
			this.Icon=App.Res.Icon;
			//BEEP!!!!!
		}

		private void buttonMore_Click(object sender, System.EventArgs e)
		{
			MessageBox.Show(txtMore,"Detailed information",MessageBoxButtons.OK,MessageBoxIcon.Information);
		}
	}
}
