using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace CryptoSafe
{
	/// <summary>
	/// Summary description for ResourceDialog.
	/// </summary>
	public class ResourceDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ImageList toolBarImageList;
		private System.ComponentModel.IContainer components;

		public ResourceDialog()
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ResourceDialog));
			this.toolBarImageList = new System.Windows.Forms.ImageList(this.components);
			// 
			// toolBarImageList
			// 
			this.toolBarImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth16Bit;
			this.toolBarImageList.ImageSize = new System.Drawing.Size(16, 16);
			this.toolBarImageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// ResourceDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(104, 19);
			this.ControlBox = false;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ResourceDialog";
			this.Opacity = 0;
			this.ShowInTaskbar = false;
			this.Text = "ResourceDialog";

		}
		#endregion
	}
}
