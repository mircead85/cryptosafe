using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace CryptoSafe
{
	/// <summary>
	/// Summary description for AbouDialog.
	/// </summary>
	public class AbouDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox pictureBox;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label labelInfo;
		private System.Windows.Forms.Label label7;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public AbouDialog()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(AbouDialog));
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.pictureBox = new System.Windows.Forms.PictureBox();
			this.buttonOK = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.labelInfo = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(112, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(216, 56);
			this.label2.TabIndex = 7;
			this.label2.Text = "Copyright Mircea Digulescu  , 2003. All rights reserved. This program and all aco" +
				"mpaning files are intelectual property of ";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(112, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(216, 16);
			this.label1.TabIndex = 6;
			this.label1.Text = "CryptoSafe Encryption Utility - version 2.1";
			// 
			// pictureBox
			// 
			this.pictureBox.Image = ((System.Drawing.Bitmap)(resources.GetObject("pictureBox.Image")));
			this.pictureBox.Location = new System.Drawing.Point(16, 16);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(88, 80);
			this.pictureBox.TabIndex = 5;
			this.pictureBox.TabStop = false;
			// 
			// buttonOK
			// 
			this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOK.Location = new System.Drawing.Point(127, 212);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(64, 24);
			this.buttonOK.TabIndex = 4;
			this.buttonOK.Text = "&OK";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(129, 79);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 12);
			this.label3.TabIndex = 8;
			this.label3.Text = "label3";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(159, 39);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(92, 15);
			this.label4.TabIndex = 9;
			this.label4.Text = "label4";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(16, 104);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(165, 17);
			this.label5.TabIndex = 10;
			this.label5.Text = "Software completed April 2003.";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(19, 134);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(120, 17);
			this.label6.TabIndex = 11;
			this.label6.Text = "Assembly information:";
			// 
			// labelInfo
			// 
			this.labelInfo.Location = new System.Drawing.Point(20, 153);
			this.labelInfo.Name = "labelInfo";
			this.labelInfo.Size = new System.Drawing.Size(297, 47);
			this.labelInfo.TabIndex = 12;
			this.labelInfo.Text = "Assembly information:";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(214, 77);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(35, 16);
			this.label7.TabIndex = 13;
			this.label7.Text = ".";
			// 
			// AbouDialog
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonOK;
			this.ClientSize = new System.Drawing.Size(329, 248);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.label7,
																		  this.labelInfo,
																		  this.label6,
																		  this.label5,
																		  this.label4,
																		  this.label3,
																		  this.label2,
																		  this.label1,
																		  this.pictureBox,
																		  this.buttonOK});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AbouDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Advanced About Dialog";
			this.Load += new System.EventHandler(this.AbouDialog_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void AbouDialog_Load(object sender, System.EventArgs e)
		{
			this.Icon=App.Res.Icon;
			label3.Text="";
			label3.Text+="M";
			label3.Text+="i";
			label3.Text+="r";
			label3.Text+="c";
			label3.Text+="e";
			label3.Text+="a";
			label3.Text+=" ";
			label3.Text+="D";
			label3.Text+="i";
			label3.Text+="g";
			label3.Text+="u";
			label3.Text+="l";
			label3.Text+="e";
			label3.Text+="s";
			label3.Text+="c";
			label3.Text+="u";
			

			label4.Text=label3.Text+",";
			labelInfo.Text=System.Reflection.Assembly.GetExecutingAssembly().ToString();
		}
	}
}
