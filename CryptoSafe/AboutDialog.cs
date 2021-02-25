using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;

namespace CryptoSafe
{
	/// <summary>
	/// Summary description for AboutDialog.
	/// </summary>
	public class AboutDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PictureBox pictureBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.LinkLabel linkLabelVisit;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public AboutDialog()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(AboutDialog));
			this.buttonOK = new System.Windows.Forms.Button();
			this.pictureBox = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.linkLabelVisit = new System.Windows.Forms.LinkLabel();
			this.SuspendLayout();
			// 
			// buttonOK
			// 
			this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOK.Location = new System.Drawing.Point(169, 120);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(64, 24);
			this.buttonOK.TabIndex = 0;
			this.buttonOK.Text = "&OK";
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// pictureBox
			// 
			this.pictureBox.Image = ((System.Drawing.Bitmap)(resources.GetObject("pictureBox.Image")));
			this.pictureBox.Location = new System.Drawing.Point(24, 24);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(88, 80);
			this.pictureBox.TabIndex = 1;
			this.pictureBox.TabStop = false;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(120, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(216, 16);
			this.label1.TabIndex = 2;
			this.label1.Text = "CryptoSafe Encryption Utility - version 2.1";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(120, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(224, 56);
			this.label2.TabIndex = 3;
			this.label2.Text = "Copyright Mircea Digulescu, 2003. All rights reserved. This program and all acomp" +
				"aning files (including any pictures or logos) are intelectual property of Mircea" +
				" Digulescu.";
			this.label2.Click += new System.EventHandler(this.label2_Click);
			// 
			// linkLabelVisit
			// 
			this.linkLabelVisit.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabelVisit.Location = new System.Drawing.Point(32, 104);
			this.linkLabelVisit.Name = "linkLabelVisit";
			this.linkLabelVisit.Size = new System.Drawing.Size(56, 16);
			this.linkLabelVisit.TabIndex = 4;
			this.linkLabelVisit.TabStop = true;
			this.linkLabelVisit.Text = "Visit site";
			this.linkLabelVisit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.linkLabelVisit.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelVisit_LinkClicked);
			// 
			// AboutDialog
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonOK;
			this.ClientSize = new System.Drawing.Size(402, 159);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.linkLabelVisit,
																		  this.label2,
																		  this.label1,
																		  this.pictureBox,
																		  this.buttonOK});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AboutDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "About";
			this.Load += new System.EventHandler(this.AboutDialog_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void linkLabelVisit_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
		 Process.Start(new ProcessStartInfo("www.matsoft.ro"));
		}

		private void label2_Click(object sender, System.EventArgs e)
		{
		
		}

		private void AboutDialog_Load(object sender, System.EventArgs e)
		{
			this.Icon=App.Res.Icon;
		}

		private void buttonOK_Click(object sender, System.EventArgs e)
		{
		
		}
	}
}
