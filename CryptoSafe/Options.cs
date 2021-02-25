using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Microsoft.Win32;

namespace CryptoSafe
{
	/// <summary>
	/// Clasa suport pt. optiuni de decryptare
	/// </summary>
	class ExtractOptions:FileOptions
	{
		//Generic options
		public static string to="";
		public static bool usePaths=true; //Use relative-paths specified in Descr
		public static bool bBackground=false;
		
		public ExtractOptions()
		{
			t.IsBackground=false;
			newkey=null;
			o.Text="Extracting...";
			HaltOnFail=AppOptions.ExtractHaltOnFail;
		}

		public ExtractOptions(string s)
		{
			t.IsBackground=false;
			newkey=null;
			o.Text=s;
			HaltOnFail=AppOptions.ExtractHaltOnFail;
		}
	}

	class FileOptions:object
	{
		public enum Result {Ignore, Abort, Retry};
		public bool HaltOnFail;
		protected Thread t,p;

		protected FileingDialog o;

		protected string newkey;

		public string NewKey
		{
			get
			{
				if(newkey==null) return null;
                string s=newkey;
				newkey=null;
				return s;
			}
			set
			{
				newkey=value;
			}
		}

		protected void ListenCancel()
		{
			o.DialogResult=DialogResult.OK;
			if(o.ShowDialog()==DialogResult.Cancel)
			  OnCancel();
		}

		void OnCancel()
		{
			Monitor.Enter(this);
			o.Close();
			try
			{
				if(p.ThreadState==ThreadState.Running) 
					p.Abort();
			}
			catch(Exception E)
			{
			}
			Monitor.Exit(this);
		}

		virtual protected void InitDialog()
		{
			o = new FileingDialog();
			t=new Thread(new ThreadStart(ListenCancel));
			t.IsBackground=true;
			p=Thread.CurrentThread;
			t.Start();
		}

		public FileOptions()
		{
			HaltOnFail=true;
			newkey=null;
			InitDialog();
		}

		public FileOptions(string s)
		{
			InitDialog();
            o.Text=s;			
			newkey=null;
		}

		virtual public Result OnException(Exception E, string where, bool bD)
		{
			if(E is CRCException)
			{
				if(((CRCException)E).Descr=="Invalid key")
				  MessageBox.Show(where+":\n"+((CRCException)E).Descr, "Key error",MessageBoxButtons.OK,MessageBoxIcon.Error);
				if(!bD) 
				{
					SetKeyDialog kd=new SetKeyDialog();
					kd.Text="Set key ("+where+")"; 
					if(kd.ShowDialog()!=DialogResult.OK)
						return Result.Ignore;
					newkey=kd.GetKey();
					return Result.Retry;
				}
				else
				{
					SetDKeyDialog kd=new SetDKeyDialog(false);
					kd.Text="Set key ("+where+")"; 
					if(kd.ShowDialog()!=DialogResult.OK)
						return Result.Ignore;
					newkey=kd.GetKey();
					return Result.Retry;
				}
			}
			string text = null;
			bool bRet=true;
			if(E is IOException)
			{
				text = "An IO error ocurred.\n("+((IOException)E).Message+")";
				if(!(E is FileNotFoundException) && !(E is DirectoryNotFoundException)) 
					bRet=false;
			}
			else
				if(E is FileCorruptException)
			{
				text="File is corrupted.\n("+((FileCorruptException)E).Descr+")";
				bRet=false;
			}
			else
				if(E is ArgumentException)
			{
				text = "Invalid argument passed.\n("+E.Message+")";
				bRet=false;
			}
			else 
				if(E is IndexOutOfRangeException)
			{
				text="Current context may have been corrupted.\n("+E.Message+")";
				bRet=false;
			}
			else
				if(E is GenericException)
			{
				text = "Generic error.\n("+((GenericException)E).Descr+")";
			}
			else
			{
				text="";
				text+=E.GetType().ToString();
				text=text.Substring(0,text.LastIndexOf("Exception"));
				text+="\n("+E.Message+")";
			}
			ExceptionDialog ed = new ExceptionDialog(where,text,bRet,E.ToString());
			switch(ed.ShowDialog())
			{
				case DialogResult.Retry: return Result.Retry;
				case DialogResult.Abort: return Result.Abort;
				case DialogResult.Ignore: return Result.Ignore;
				default: return Result.Ignore;
			}
		}

		virtual public void SetProgress(int i) //in %
		{
		 o.SetProgress(i);
		}

		public void Done()
		{
			Monitor.Enter(this);
			Thread.Sleep(40);
			o.DialogResult=DialogResult.OK;
			if(o.Created) o.Close();
			Monitor.Exit(this);
		}
	}

	class OpenFileOptions:FileOptions
	{
		public OpenFileOptions()
		{
		  o.Text="Reading file...";
		}

		public OpenFileOptions(string s)
		{
            o.Text=s;			
		}
	}

	class SaveFileOptions:FileOptions
	{
		public SaveFileOptions()
		{
			t.IsBackground=false;
			o.Text="Writing file...";
		}

		public SaveFileOptions(string s)
		{
			t.IsBackground=false;
			o.Text=s;	
		}
	}

	class AddFileOptions:FileOptions
	{
		public AddFileOptions()
		{
			o.Text="Adding files...";
		}

		public AddFileOptions(string s)
		{
			o.Text=s;		
		}
	}

	class AppOptions
	{
		//Directories
		
		static public string SaveDir=""; 
		static public string OpenDir="";
		static public string AddDir="D:\\My Documents";

		static public string AppPath="D:\\My Documents\\Visual Studio Projects\\CryptoSafe";

		static public bool ExtractHaltOnFail=true;
	
		/// <summary>
		/// Destroy files instead of just deleting them
		/// </summary>
		static public bool DestroyFiles=false;

		/// <summary>
		/// Delete the temporary files created by application (recommanded)
		/// </summary>
		static public bool DeleteTempFiles=true;

		/// <summary>
		/// After executing an item from the application, update the respective item
		/// with the new content.
		/// </summary>
		static public bool ReverseUpdate=true;

		/// <summary>
		/// Show a message box when an item is updated (see ReverseUpdate).
		/// </summary>
		static public bool NotifyUpdate=true;

		static public bool ShowGridLines=false;

		/// <summary>
		/// Get associated icons from Windows Registry instead of using build-it icons.
		/// </summary>
		static public bool DrawIcons=true;

		/// <summary>
		/// Save the settings to registry when clicking OK
		/// </summary>
		static public bool SaveSettingsOnOK=false;

		/// <summary>
		/// Delete/Destroy a file once it has been successfully added.
		/// </summary>
		static public bool DelAfterAdding=false;

		/// <summary>
		/// Prompt to set a default decryption key for all items on opening archive
		/// </summary>
		static public bool SetDKeyOnOpen=false;

		/// <summary>
		/// If LastKey is available use it by default
		/// </summary>
		static public bool UseLastKeyByDefault=false;
		
		static public string GetTempDir()
		{
			return Path.GetTempPath();
		}

		public AppOptions()
		{
		}

		public AppOptions(string s)
		{
			using(BinaryReader br = new BinaryReader(new FileStream(s,FileMode.Open),Encoding.ASCII))
			{
				SaveDir=br.ReadString();
				OpenDir=br.ReadString();
				AppPath=br.ReadString();
			}
		}

		public static void Save()
		{
				RegistryKey k=Registry.CurrentUser.CreateSubKey("Software\\MAT\\CryptoSafe");
				k.SetValue("Application Path",AppPath);
				k.SetValue("AddDir",AddDir);
				k.SetValue("DeleteTempFiles",DeleteTempFiles);
				k.SetValue("DestroyFiles",DestroyFiles);
				k.SetValue("DrawIcons",DrawIcons);
				k.SetValue("ExtractHaltOnFail",ExtractHaltOnFail);
				k.SetValue("NotifyUpdate",NotifyUpdate);
				k.SetValue("OpenDir",OpenDir);
				k.SetValue("ReverseUpdate",ReverseUpdate);
				k.SetValue("SaveDir",SaveDir);
				k.SetValue("SaveSettingsOnOK",SaveSettingsOnOK);
				k.SetValue("ShowGridLines",ShowGridLines);
				k.SetValue("DelAfterAdding",DelAfterAdding);
				k.SetValue("SetDKeyOnOpen", SetDKeyOnOpen);
				k.SetValue("UseLastKeyByDefault",UseLastKeyByDefault);
				k.Flush();
		}

		public static void Load()
		{
			RegistryKey k=Registry.CurrentUser.OpenSubKey("Software\\MAT\\CryptoSafe");
			AppPath=(string)k.GetValue("Application Path");
			AddDir=(string)k.GetValue("AddDir");
			DeleteTempFiles=bool.Parse(((string)k.GetValue("DeleteTempFiles")));
			DestroyFiles=bool.Parse(((string)k.GetValue("DestroyFiles")));
			DrawIcons=bool.Parse(((string)k.GetValue("DrawIcons")));
			ExtractHaltOnFail=bool.Parse(((string)k.GetValue("ExtractHaltOnFail")));
			NotifyUpdate=bool.Parse(((string)k.GetValue("NotifyUpdate")));
			OpenDir=(string)k.GetValue("OpenDir");
			ReverseUpdate=bool.Parse(((string)k.GetValue("ReverseUpdate")));
			SaveDir=(string)k.GetValue("SaveDir");
			SaveSettingsOnOK=bool.Parse(((string)k.GetValue("SaveSettingsOnOK")));
			ShowGridLines=bool.Parse(((string)k.GetValue("ShowGridLines")));
			DelAfterAdding=bool.Parse((string)k.GetValue("DelAfterAdding"));
			SetDKeyOnOpen=bool.Parse((string)k.GetValue("SetDKeyOnOpen"));
			UseLastKeyByDefault=bool.Parse((string)k.GetValue("UseLastKeyByDefault"));
		}

		public static void SaveTo(string s)
		{
		
		}
	}
}
