using System;
using System.Windows.Forms;

namespace CryptoSafe
{
	/// <summary>
	/// Summary description for AddFiles.
	/// </summary>
	public class AddFiles:FileDialog
	{
		public AddFiles()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		protected override IntPtr OwnerWndProc(
			IntPtr hWnd,
			int msg,
			IntPtr wparam,
			IntPtr lparam
			)
		{
			
		}
	}
}
