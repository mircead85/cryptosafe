using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace CryptoSafe
{
	/// <summary>
	/// Summary description for FileInformationDialog.
	/// </summary>
	public class FileInformationDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textBoxFileName;
		private System.Windows.Forms.TextBox textBoxFileDate;
		private System.Windows.Forms.Label labelNoItems;
		private System.Windows.Forms.Label labelComment;
		private System.Windows.Forms.Label labelDeldItems;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FileInformationDialog(string fn, string fd, long noIt, bool com, long delIt)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
            textBoxFileName.Text=fn;
			textBoxFileDate.Text=fd;

			labelNoItems.Text=noIt.ToString();
			labelComment.Text=(com)?"YES":"NO";
			labelDeldItems.Text=delIt.ToString();
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FileInformationDialog));
			this.buttonOK = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxFileName = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxFileDate = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.labelNoItems = new System.Windows.Forms.Label();
			this.labelComment = new System.Windows.Forms.Label();
			this.labelDeldItems = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// buttonOK
			// 
			this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOK.Location = new System.Drawing.Point(93, 216);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(64, 24);
			this.buttonOK.TabIndex = 0;
			this.buttonOK.Text = "O&K";
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "Full File Name:";
			// 
			// textBoxFileName
			// 
			this.textBoxFileName.Location = new System.Drawing.Point(8, 24);
			this.textBoxFileName.Name = "textBoxFileName";
			this.textBoxFileName.ReadOnly = true;
			this.textBoxFileName.Size = new System.Drawing.Size(232, 20);
			this.textBoxFileName.TabIndex = 2;
			this.textBoxFileName.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(104, 16);
			this.label2.TabIndex = 3;
			this.label2.Text = "File date and time:";
			// 
			// textBoxFileDate
			// 
			this.textBoxFileDate.Location = new System.Drawing.Point(8, 80);
			this.textBoxFileDate.Name = "textBoxFileDate";
			this.textBoxFileDate.ReadOnly = true;
			this.textBoxFileDate.Size = new System.Drawing.Size(232, 20);
			this.textBoxFileDate.TabIndex = 4;
			this.textBoxFileDate.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 120);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(128, 16);
			this.label3.TabIndex = 5;
			this.label3.Text = "Number of items in file:";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 152);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(128, 16);
			this.label4.TabIndex = 7;
			this.label4.Text = "Comment present:";
			// 
			// labelNoItems
			// 
			this.labelNoItems.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.labelNoItems.Location = new System.Drawing.Point(144, 120);
			this.labelNoItems.Name = "labelNoItems";
			this.labelNoItems.Size = new System.Drawing.Size(32, 16);
			this.labelNoItems.TabIndex = 8;
			this.labelNoItems.Text = "125";
			this.labelNoItems.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// labelComment
			// 
			this.labelComment.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.labelComment.Location = new System.Drawing.Point(144, 152);
			this.labelComment.Name = "labelComment";
			this.labelComment.Size = new System.Drawing.Size(32, 16);
			this.labelComment.TabIndex = 9;
			this.labelComment.Text = "YES";
			this.labelComment.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// labelDeldItems
			// 
			this.labelDeldItems.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.labelDeldItems.Location = new System.Drawing.Point(144, 184);
			this.labelDeldItems.Name = "labelDeldItems";
			this.labelDeldItems.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.labelDeldItems.Size = new System.Drawing.Size(32, 16);
			this.labelDeldItems.TabIndex = 11;
			this.labelDeldItems.Text = "7";
			this.labelDeldItems.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(8, 184);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(134, 16);
			this.label8.TabIndex = 10;
			this.label8.Text = "Number of deleted items:";
			// 
			// FileInformationDialog
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonOK;
			this.ClientSize = new System.Drawing.Size(250, 247);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.labelDeldItems,
																		  this.label8,
																		  this.labelComment,
																		  this.labelNoItems,
																		  this.label4,
																		  this.label3,
																		  this.textBoxFileDate,
																		  this.label2,
																		  this.textBoxFileName,
																		  this.label1,
																		  this.buttonOK});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FileInformationDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "File Information";
			this.Load += new System.EventHandler(this.FileInformationDialog_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonOK_Click(object sender, System.EventArgs e)
		{
		
		}

		private void FileInformationDialog_Load(object sender, System.EventArgs e)
		{
			this.Icon=App.Res.Icon;
		}
	}
}
