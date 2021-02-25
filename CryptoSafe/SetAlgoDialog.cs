using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace CryptoSafe
{
	/// <summary>
	/// Summary description for SetAlgoDialog.
	/// </summary>
	public class SetAlgoDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Button buttonHelp;
		private System.Windows.Forms.ComboBox comboBoxAlgo;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.ErrorProvider errorProvider;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SetAlgoDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			
			comboBoxAlgo.SelectedIndex=0;
		}

		public SetAlgoDialog(Item.AlgoTypes a)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			
			comboBoxAlgo.SelectedIndex=(int) a;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SetAlgoDialog));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.buttonHelp = new System.Windows.Forms.Button();
			this.comboBoxAlgo = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOk = new System.Windows.Forms.Button();
			this.errorProvider = new System.Windows.Forms.ErrorProvider();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Bitmap)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(8, 64);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(96, 64);
			this.pictureBox1.TabIndex = 13;
			this.pictureBox1.TabStop = false;
			// 
			// buttonHelp
			// 
			this.buttonHelp.Location = new System.Drawing.Point(120, 96);
			this.buttonHelp.Name = "buttonHelp";
			this.buttonHelp.Size = new System.Drawing.Size(64, 24);
			this.buttonHelp.TabIndex = 4;
			this.buttonHelp.Text = "&Help";
			this.buttonHelp.Click += new System.EventHandler(this.buttonHelp_Click);
			// 
			// comboBoxAlgo
			// 
			this.comboBoxAlgo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxAlgo.Items.AddRange(new object[] {
															  "RJIN",
															  "DES",
															  "TDES",
															  "RC2"});
			this.comboBoxAlgo.Location = new System.Drawing.Point(8, 32);
			this.comboBoxAlgo.Name = "comboBoxAlgo";
			this.comboBoxAlgo.Size = new System.Drawing.Size(100, 21);
			this.comboBoxAlgo.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 16);
			this.label2.TabIndex = 11;
			this.label2.Text = "Algorithm:";
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(120, 56);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(64, 24);
			this.buttonCancel.TabIndex = 3;
			this.buttonCancel.Text = "&Cancel";
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// buttonOk
			// 
			this.buttonOk.Location = new System.Drawing.Point(120, 16);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.Size = new System.Drawing.Size(64, 24);
			this.buttonOk.TabIndex = 2;
			this.buttonOk.Text = "O&K";
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// errorProvider
			// 
			this.errorProvider.DataMember = null;
			// 
			// SetAlgoDialog
			// 
			this.AcceptButton = this.buttonOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(192, 133);
			this.ControlBox = false;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.pictureBox1,
																		  this.buttonHelp,
																		  this.comboBoxAlgo,
																		  this.label2,
																		  this.buttonCancel,
																		  this.buttonOk});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SetAlgoDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Set algorithm...";
			this.Load += new System.EventHandler(this.SetAlgoDialog_Load);
			this.ResumeLayout(false);

		}
		#endregion

		protected Item.AlgoTypes al;
		public Item.AlgoTypes GetAlgo(){return al;}

		private void buttonOk_Click(object sender, System.EventArgs e)
		{
			if(comboBoxAlgo.SelectedIndex==-1)
			{errorProvider.SetError(comboBoxAlgo,"Select an algorithm");return;}

			al=(Item.AlgoTypes)comboBoxAlgo.SelectedIndex;
			this.DialogResult=DialogResult.OK;
			this.Close();
		}

		private void buttonCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult=DialogResult.Cancel;
			this.Close();
		}

		private void buttonHelp_Click(object sender, System.EventArgs e)
		{
			MessageBox.Show("You will get help soon","Help",MessageBoxButtons.OK,MessageBoxIcon.Information);
		}

		private void SetAlgoDialog_Load(object sender, System.EventArgs e)
		{
			this.Icon=App.Res.Icon;
			comboBoxAlgo.Focus();
		}
	}
}
