using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace CryptoSafe
{
	/// <summary>
	/// Dialog pentru setarea keii si a algoritmului
	/// </summary>
	public class SetKeyAlgoDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxKey;
		private System.Windows.Forms.CheckBox checkBoxUseLast;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxKey2;
		private System.Windows.Forms.Button buttonHelp;
		private System.Windows.Forms.ErrorProvider errorProvider;
		private System.Windows.Forms.ComboBox comboBoxAlgo;
		private System.Windows.Forms.HelpProvider helpProvider;
		private System.Windows.Forms.PictureBox pictureBox1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SetKeyAlgoDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			if(lastKey!=null) checkBoxUseLast.Enabled=true;
			else checkBoxUseLast.Enabled=false;

			comboBoxAlgo.SelectedIndex=2;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SetKeyAlgoDialog));
			this.buttonOk = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxKey = new System.Windows.Forms.TextBox();
			this.checkBoxUseLast = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.comboBoxAlgo = new System.Windows.Forms.ComboBox();
			this.textBoxKey2 = new System.Windows.Forms.TextBox();
			this.buttonHelp = new System.Windows.Forms.Button();
			this.errorProvider = new System.Windows.Forms.ErrorProvider();
			this.helpProvider = new System.Windows.Forms.HelpProvider();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.SuspendLayout();
			// 
			// buttonOk
			// 
			this.buttonOk.Location = new System.Drawing.Point(368, 16);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.Size = new System.Drawing.Size(64, 24);
			this.buttonOk.TabIndex = 2;
			this.buttonOk.Text = "O&K";
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(368, 56);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(64, 24);
			this.buttonCancel.TabIndex = 4;
			this.buttonCancel.Text = "&Cancel";
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(24, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(32, 16);
			this.label1.TabIndex = 2;
			this.label1.Text = "Key:";
			// 
			// textBoxKey
			// 
			this.textBoxKey.Location = new System.Drawing.Point(24, 40);
			this.textBoxKey.Name = "textBoxKey";
			this.textBoxKey.PasswordChar = '*';
			this.textBoxKey.Size = new System.Drawing.Size(144, 20);
			this.textBoxKey.TabIndex = 0;
			this.textBoxKey.Text = "";
			// 
			// checkBoxUseLast
			// 
			this.checkBoxUseLast.Location = new System.Drawing.Point(24, 104);
			this.checkBoxUseLast.Name = "checkBoxUseLast";
			this.checkBoxUseLast.Size = new System.Drawing.Size(128, 16);
			this.checkBoxUseLast.TabIndex = 5;
			this.checkBoxUseLast.Text = "Use last key entered";
			this.checkBoxUseLast.CheckedChanged += new System.EventHandler(this.checkBoxUseLast_CheckedChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(224, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 16);
			this.label2.TabIndex = 5;
			this.label2.Text = "Algorithm:";
			// 
			// comboBoxAlgo
			// 
			this.comboBoxAlgo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxAlgo.Items.AddRange(new object[] {
															  "RJIN",
															  "DES",
															  "TDES",
															  "RC2"});
			this.comboBoxAlgo.Location = new System.Drawing.Point(224, 40);
			this.comboBoxAlgo.Name = "comboBoxAlgo";
			this.comboBoxAlgo.Size = new System.Drawing.Size(98, 21);
			this.comboBoxAlgo.TabIndex = 3;
			// 
			// textBoxKey2
			// 
			this.textBoxKey2.Location = new System.Drawing.Point(24, 72);
			this.textBoxKey2.Name = "textBoxKey2";
			this.textBoxKey2.PasswordChar = '*';
			this.textBoxKey2.Size = new System.Drawing.Size(144, 20);
			this.textBoxKey2.TabIndex = 1;
			this.textBoxKey2.Text = "";
			// 
			// buttonHelp
			// 
			this.buttonHelp.Location = new System.Drawing.Point(368, 96);
			this.buttonHelp.Name = "buttonHelp";
			this.buttonHelp.Size = new System.Drawing.Size(64, 24);
			this.buttonHelp.TabIndex = 6;
			this.buttonHelp.Text = "&Help";
			this.buttonHelp.Click += new System.EventHandler(this.buttonHelp_Click);
			// 
			// errorProvider
			// 
			this.errorProvider.DataMember = null;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Bitmap)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(220, 65);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(96, 64);
			this.pictureBox1.TabIndex = 14;
			this.pictureBox1.TabStop = false;
			// 
			// SetKeyAlgoDialog
			// 
			this.AcceptButton = this.buttonOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(440, 135);
			this.ControlBox = false;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.pictureBox1,
																		  this.buttonHelp,
																		  this.textBoxKey2,
																		  this.comboBoxAlgo,
																		  this.label2,
																		  this.checkBoxUseLast,
																		  this.textBoxKey,
																		  this.label1,
																		  this.buttonCancel,
																		  this.buttonOk});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SetKeyAlgoDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Encrypt...";
			this.Load += new System.EventHandler(this.SetKeyAlgoDialog_Load);
			this.ResumeLayout(false);

		}
		#endregion

		public static string lastKey=null;

		protected string key;
		protected Item.AlgoTypes al;

		public string GetKey() {return key;}
		public Item.AlgoTypes GetAlgo(){return al;}

		private void buttonOk_Click(object sender, EventArgs e)
		{
			if(!checkBoxUseLast.Checked)
			{			 
				if(textBoxKey.Text!=textBoxKey2.Text)
				{errorProvider.SetError(textBoxKey2,"Password re-entered incorectlly.");return;}
				
				if(textBoxKey.Text.Length<7)
				{errorProvider.SetError(textBoxKey,"Password must be at least 7 characters long");return;}
				
				if(textBoxKey.Text.Length>20)
				{errorProvider.SetError(textBoxKey,"Password must be at most 20 characters long");return;}

				key = lastKey = textBoxKey.Text;
			}
			else key=lastKey;

			if(comboBoxAlgo.SelectedIndex==-1)
			{errorProvider.SetError(comboBoxAlgo,"Select an algorithm");return;}

            al=(Item.AlgoTypes)comboBoxAlgo.SelectedIndex;
		this.DialogResult=DialogResult.OK;
		this.Close();
		}

		private void checkBoxUseLast_CheckedChanged(object sender, System.EventArgs e)
		{
		textBoxKey.Enabled=!textBoxKey.Enabled;
		textBoxKey2.Enabled=!textBoxKey2.Enabled;
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

		private void SetKeyAlgoDialog_Load(object sender, System.EventArgs e)
		{
			this.Icon=App.Res.Icon;
            if(AppOptions.UseLastKeyByDefault)
				if(checkBoxUseLast.Enabled)
                    checkBoxUseLast.Checked=true;
			textBoxKey.Focus();
		}
	}
}
