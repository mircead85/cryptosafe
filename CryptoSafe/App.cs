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
using System.Collections.Specialized;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace CryptoSafe
{

	/// <summary>
	/// Folosit pt. sortarea item-urilor
	/// </summary>
	public class MyListViewItemComparer:IComparer
	{
		public MyListViewItemComparer(){}
		public int Compare(object x, object y)
		{
			if((long)((ListViewItem)x).Tag<=0 && (long)((ListViewItem)y).Tag>0) return -1;
			if((long)((ListViewItem)x).Tag>0 && (long)((ListViewItem)y).Tag<=0) return 1;
			return string.Compare(((ListViewItem)x).Text,((ListViewItem)y).Text);
		}
	}

	class ExecuteFileClass:object
	{
		public Item it;
		public string startFileName;
		public bool bExecuteFileDelAfter;

		public delegate void ItemUpdatedHandler(Item it);
		public event ItemUpdatedHandler ItemUpdated;

		public static void FileDelete(string f)
		{
			if(AppOptions.DestroyFiles)
			{
				byte [] bin = new byte[50000];
				FileStream S = new FileStream(f,FileMode.Open,FileAccess.ReadWrite);
				long l=S.Length;
				S.Seek(0,SeekOrigin.Begin);
				long i=0;
				while(i<l)
				{
					S.Write(bin,0,50000);
					i+=50000;
				}
				S.Close();
			}
			System.IO.File.Delete(f);
		}

		public ExecuteFileClass(string fn, Item lit, bool bD)
		{
			startFileName=fn;
			it=lit;
			bExecuteFileDelAfter=bD;
		}
        
		public void Do()
		{
			Thread.CurrentThread.IsBackground=false;
			
			Process P = new Process();
			P.StartInfo.UseShellExecute=true;
			P.StartInfo.ErrorDialog=true;
			P.StartInfo.FileName=startFileName;
			P.StartInfo.CreateNoWindow=false;

			P.EnableRaisingEvents=true;
			P.Start();

			P.WaitForExit();

			try
			{
				if(AppOptions.ReverseUpdate)
				{
					FileStream S = new FileStream(startFileName,FileMode.Open,FileAccess.Read);
					it.ReadFrom(S,Item.DataType.Decrypted);
					S.Close();
					if(ItemUpdated!=null)
					 ItemUpdated(it);
				}
			}
			catch(ThreadAbortException E)
			{
			}
			catch(ThreadInterruptedException E)
			{
			}
			catch(Exception E)
			{
				MessageBox.Show("Error runnig file: "+E.ToString(),"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
			finally
			{
 			 if(bExecuteFileDelAfter)
			  {
			   FileDelete(startFileName);
			  }
			}
		}
	}

	/// <summary>
	/// Clasa aplicatiei.
	/// </summary>
	public class App : System.Windows.Forms.Form
	{
		private System.ComponentModel.IContainer components=null;
		private System.Windows.Forms.MainMenu muMainMenu;
		private System.Windows.Forms.MenuItem muFileNew;
		private System.Windows.Forms.MenuItem muFileOpen;
		private System.Windows.Forms.MenuItem muFileSave;
		private System.Windows.Forms.MenuItem muFileSaveAs;
		private System.Windows.Forms.MenuItem muFileInformation;
		private System.Windows.Forms.MenuItem muFileExit;
		private System.Windows.Forms.MenuItem muFile;
		private System.Windows.Forms.MenuItem muFileSep1;
		private System.Windows.Forms.MenuItem muFileSep2;
		private System.Windows.Forms.MenuItem muFileSep3;
		private System.Windows.Forms.ColumnHeader colName;
		private System.Windows.Forms.ColumnHeader colSize;
		private System.Windows.Forms.ColumnHeader colAlgorithm;
		private System.Windows.Forms.ColumnHeader colPath;
		private System.Windows.Forms.ColumnHeader colCRC;
		private System.Windows.Forms.ListView listViewFisiere;
		private System.Windows.Forms.MenuItem muFileClose;
		private System.Windows.Forms.MenuItem muAction;
		private System.Windows.Forms.MenuItem muActionSetKey;
		private System.Windows.Forms.MenuItem muActionSetKeyForAll;
		private System.Windows.Forms.MenuItem muActionSetKeyForSelection;
		private System.Windows.Forms.MenuItem muActionSep1;
		private System.Windows.Forms.MenuItem muActionAddFiles;
		private System.Windows.Forms.MenuItem muActionAddComment;
		private System.Windows.Forms.MenuItem muActionRemove;
		private System.Windows.Forms.MenuItem muActionSep2;
		private System.Windows.Forms.MenuItem muActionExtractTo;
		private System.Windows.Forms.MenuItem muActionView;
		private System.Windows.Forms.MenuItem muActionExecute;
		private System.Windows.Forms.MenuItem muTools;
		private System.Windows.Forms.MenuItem muActionSep3;
		private System.Windows.Forms.MenuItem muToolsSep1;
		private System.Windows.Forms.MenuItem muToolsCreateSFX;
		private System.Windows.Forms.MenuItem muToolsSendEmail;
		private System.Windows.Forms.MenuItem muToolsSendMsg;
		private System.Windows.Forms.MenuItem muHelp;
		private System.Windows.Forms.MenuItem muHelpContents;
		private System.Windows.Forms.MenuItem muHelpSearch;
		private System.Windows.Forms.MenuItem muHelpSep1;
		private System.Windows.Forms.MenuItem muHelpContext;
		private System.Windows.Forms.MenuItem muHelpSep2;
		private System.Windows.Forms.MenuItem muHelpAbout;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.ToolBar toolBar;
		private System.Windows.Forms.ToolBarButton toolBarButtonNew;
		private System.Windows.Forms.MenuItem muActionSetAlgo;
		
		CryptoFile File;
		bool bModified;
		string fn;
		private System.Windows.Forms.StatusBar statusBar;
		private System.Windows.Forms.MenuItem muActionSetAlgoAll;
		private System.Windows.Forms.MenuItem muActionSetAlgoSelection;
		private System.Windows.Forms.MenuItem muToolsReceiveMsg;
		private System.Windows.Forms.MenuItem muActionSep4;
		private System.Windows.Forms.MenuItem muSetDecryptionKey;
		private System.Windows.Forms.MenuItem muSetDecryptionKeyForAll;
		private System.Windows.Forms.MenuItem muSetDecryptionKeyForSelection;
		private System.Windows.Forms.MenuItem muActionExtractSelectionTo;
		private System.Windows.Forms.ToolBarButton toolBarButtonOpen;
		private System.Windows.Forms.ToolBarButton toolBarButtonSave;
		private System.Windows.Forms.ToolBarButton toolBarButtonAdd;
		private System.Windows.Forms.ToolBarButton toolBarButtonExtract;
		private System.Windows.Forms.ToolBarButton toolBarButtonView;
		private System.Windows.Forms.ContextMenu contextMenu;
		private System.Windows.Forms.MenuItem menuItemAddFiles;
		private System.Windows.Forms.MenuItem menuItemSetDecrKey;
		private System.Windows.Forms.MenuItem menuItemSetEncKey;
		private System.Windows.Forms.MenuItem menuItemSetEncAlgo;
		private System.Windows.Forms.MenuItem menuItemExecute;
		private System.Windows.Forms.MenuItem menuItemView;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItemSep7;
		private System.Windows.Forms.MenuItem menuItemExtractSelection;
		private System.Windows.Forms.MenuItem muActionAddDirectory;
		private System.Windows.Forms.MenuItem menuItemAddDir;
		private System.Windows.Forms.ToolBarButton toolBarButtonAddDir;
		private System.Windows.Forms.ToolBarButton toolBarSep1;
		private System.Windows.Forms.ToolBarButton toolBarSep2;
		private System.Windows.Forms.MenuItem menuOptionsPreferences;
		private System.Windows.Forms.MenuItem menuOptionsDirectories;
		private System.Windows.Forms.MenuItem menuViewDetails;
		private System.Windows.Forms.MenuItem menuViewSmall;
		private System.Windows.Forms.MenuItem menuViewLarge;
		private System.Windows.Forms.MenuItem menuViewList;
		private System.Windows.Forms.MenuItem menuView;
		private System.Windows.Forms.ToolBarButton toolBarButtonComment;
		
		[DllImport("shell32.dll",
			 CharSet=CharSet.Auto, 
			 EntryPoint="ExtractAssociatedIcon")]
		extern static IntPtr ExtractAssociatedIcon(
			IntPtr hInst,    // application instance handle
			string lpIconPath,  // file name
			ref int lpiIcon      // icon index
			);

		static IntPtr HINSTANCE;
		public static ResourceDialog Res;

		public App()
		{
			InitializeComponent();
			Res = new ResourceDialog();
			muActionSep4.Click+=new EventHandler(Abou2);
			this.Icon=Res.Icon;


			bExtracting=false;
			File = null;
			fn=null;
			etmre=new AutoResetEvent(true);
			ofmre=new AutoResetEvent(true);
			sfmre=new AutoResetEvent(true);
			curPath="";
			dirName="";
			giFn=null;

			HINSTANCE = System.Diagnostics.Process.GetCurrentProcess().Handle;

			Closing+=new CancelEventHandler(OnClosing);

			listViewFisiere.SmallImageList=new ImageList();
			listViewFisiere.LargeImageList=new ImageList();

			listViewFisiere.GridLines=AppOptions.ShowGridLines;
			listViewFisiere.BeforeLabelEdit+=new LabelEditEventHandler(OnLabelEditBegin);
			listViewFisiere.AfterLabelEdit+=new LabelEditEventHandler(OnLabelEditFinish);
			listViewFisiere.DoubleClick+=new EventHandler(OnDoubleClick);
			listViewFisiere.KeyPress+=new KeyPressEventHandler(OnKeyPressed);
			listViewFisiere.AllowDrop=true;
			listViewFisiere.DragEnter+=new DragEventHandler(OnDragEnter);
			listViewFisiere.DragDrop+=new DragEventHandler(OnDragDrop);
			//listViewFisiere.MouseMove+=new MouseEventHandler(OnMouseM);
			listViewFisiere.ListViewItemSorter=new MyListViewItemComparer();

			listViewFisiere.ColumnClick+=new ColumnClickEventHandler(OnColumnClick);
		}

		static string giFn;

		static Image GetFileIcon(string S,bool bLarge)
		{
			try
			{
				Icon I=null;
				if(S=="..")
				{
					return Image.FromFile(AppOptions.AppPath+"\\Images\\IconUp2.png");
				}
				if(AppOptions.DrawIcons)
				{
					string s="";
					int i=0;
					if(S=="\\^$FOLDER")
					{
						s+=Path.GetTempPath();
						IntPtr j=ExtractAssociatedIcon(HINSTANCE,s,ref i);
						I = Icon.FromHandle(j);
					}
					else
					{
						if(giFn==null)
							giFn=Path.GetTempFileName();
						string giTFn=null;
				
						giTFn=giFn+System.IO.Path.GetExtension(S);
						bool moved=false;
						if(!System.IO.File.Exists(giTFn))
						{
							System.IO.File.Move(giFn,giTFn);
							moved=true;
						}
				
						s+=giTFn;

						IntPtr j=ExtractAssociatedIcon(HINSTANCE,s,ref i);
						I = Icon.FromHandle(j);

						if(moved)
						 System.IO.File.Move(giTFn,giFn);
					}
				}
				else
				{
				 if(S=="\\^$FOLDER")
					 I = new Icon(AppOptions.AppPath+"\\Images\\IconFolder.ico");
				 else
					 return Image.FromFile(AppOptions.AppPath+"\\Images\\IconFile.png");
				}
				return I.ToBitmap();
			}
			catch(Exception E)
			{
			 return Image.FromFile(AppOptions.AppPath+"\\Images\\IconFile.png");
			}
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
			this.muMainMenu = new System.Windows.Forms.MainMenu();
			this.muFile = new System.Windows.Forms.MenuItem();
			this.muFileNew = new System.Windows.Forms.MenuItem();
			this.muFileSep1 = new System.Windows.Forms.MenuItem();
			this.muFileOpen = new System.Windows.Forms.MenuItem();
			this.muFileSave = new System.Windows.Forms.MenuItem();
			this.muFileSaveAs = new System.Windows.Forms.MenuItem();
			this.muFileClose = new System.Windows.Forms.MenuItem();
			this.muFileSep2 = new System.Windows.Forms.MenuItem();
			this.muFileInformation = new System.Windows.Forms.MenuItem();
			this.muFileSep3 = new System.Windows.Forms.MenuItem();
			this.muFileExit = new System.Windows.Forms.MenuItem();
			this.muAction = new System.Windows.Forms.MenuItem();
			this.muActionSetKey = new System.Windows.Forms.MenuItem();
			this.muActionSetKeyForAll = new System.Windows.Forms.MenuItem();
			this.muActionSetKeyForSelection = new System.Windows.Forms.MenuItem();
			this.muActionSetAlgo = new System.Windows.Forms.MenuItem();
			this.muActionSetAlgoAll = new System.Windows.Forms.MenuItem();
			this.muActionSetAlgoSelection = new System.Windows.Forms.MenuItem();
			this.muSetDecryptionKey = new System.Windows.Forms.MenuItem();
			this.muSetDecryptionKeyForAll = new System.Windows.Forms.MenuItem();
			this.muSetDecryptionKeyForSelection = new System.Windows.Forms.MenuItem();
			this.muActionSep1 = new System.Windows.Forms.MenuItem();
			this.muActionAddDirectory = new System.Windows.Forms.MenuItem();
			this.muActionAddFiles = new System.Windows.Forms.MenuItem();
			this.muActionAddComment = new System.Windows.Forms.MenuItem();
			this.muActionSep4 = new System.Windows.Forms.MenuItem();
			this.muActionRemove = new System.Windows.Forms.MenuItem();
			this.muActionSep2 = new System.Windows.Forms.MenuItem();
			this.muActionExtractTo = new System.Windows.Forms.MenuItem();
			this.muActionExtractSelectionTo = new System.Windows.Forms.MenuItem();
			this.muActionSep3 = new System.Windows.Forms.MenuItem();
			this.muActionView = new System.Windows.Forms.MenuItem();
			this.muActionExecute = new System.Windows.Forms.MenuItem();
			this.muTools = new System.Windows.Forms.MenuItem();
			this.muToolsCreateSFX = new System.Windows.Forms.MenuItem();
			this.muToolsSep1 = new System.Windows.Forms.MenuItem();
			this.muToolsSendEmail = new System.Windows.Forms.MenuItem();
			this.muToolsSendMsg = new System.Windows.Forms.MenuItem();
			this.muToolsReceiveMsg = new System.Windows.Forms.MenuItem();
			this.menuView = new System.Windows.Forms.MenuItem();
			this.menuViewDetails = new System.Windows.Forms.MenuItem();
			this.menuViewSmall = new System.Windows.Forms.MenuItem();
			this.menuViewLarge = new System.Windows.Forms.MenuItem();
			this.menuViewList = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuOptionsPreferences = new System.Windows.Forms.MenuItem();
			this.menuOptionsDirectories = new System.Windows.Forms.MenuItem();
			this.muHelp = new System.Windows.Forms.MenuItem();
			this.muHelpContents = new System.Windows.Forms.MenuItem();
			this.muHelpSearch = new System.Windows.Forms.MenuItem();
			this.muHelpSep1 = new System.Windows.Forms.MenuItem();
			this.muHelpContext = new System.Windows.Forms.MenuItem();
			this.muHelpSep2 = new System.Windows.Forms.MenuItem();
			this.muHelpAbout = new System.Windows.Forms.MenuItem();
			this.listViewFisiere = new System.Windows.Forms.ListView();
			this.colName = new System.Windows.Forms.ColumnHeader();
			this.colSize = new System.Windows.Forms.ColumnHeader();
			this.colAlgorithm = new System.Windows.Forms.ColumnHeader();
			this.colPath = new System.Windows.Forms.ColumnHeader();
			this.colCRC = new System.Windows.Forms.ColumnHeader();
			this.contextMenu = new System.Windows.Forms.ContextMenu();
			this.menuItemAddDir = new System.Windows.Forms.MenuItem();
			this.menuItemAddFiles = new System.Windows.Forms.MenuItem();
			this.menuItemExtractSelection = new System.Windows.Forms.MenuItem();
			this.menuItemSep7 = new System.Windows.Forms.MenuItem();
			this.menuItemSetDecrKey = new System.Windows.Forms.MenuItem();
			this.menuItemSetEncKey = new System.Windows.Forms.MenuItem();
			this.menuItemSetEncAlgo = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItemExecute = new System.Windows.Forms.MenuItem();
			this.menuItemView = new System.Windows.Forms.MenuItem();
			this.toolBar = new System.Windows.Forms.ToolBar();
			this.toolBarButtonNew = new System.Windows.Forms.ToolBarButton();
			this.toolBarButtonOpen = new System.Windows.Forms.ToolBarButton();
			this.toolBarButtonSave = new System.Windows.Forms.ToolBarButton();
			this.toolBarSep1 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButtonAdd = new System.Windows.Forms.ToolBarButton();
			this.toolBarButtonAddDir = new System.Windows.Forms.ToolBarButton();
			this.toolBarSep2 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButtonExtract = new System.Windows.Forms.ToolBarButton();
			this.toolBarButtonView = new System.Windows.Forms.ToolBarButton();
			this.toolBarButtonComment = new System.Windows.Forms.ToolBarButton();
			this.statusBar = new System.Windows.Forms.StatusBar();
			this.SuspendLayout();
			// 
			// muMainMenu
			// 
			this.muMainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this.muFile,
																					   this.muAction,
																					   this.muTools,
																					   this.menuView,
																					   this.menuItem1,
																					   this.muHelp});
			// 
			// muFile
			// 
			this.muFile.Index = 0;
			this.muFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																				   this.muFileNew,
																				   this.muFileSep1,
																				   this.muFileOpen,
																				   this.muFileSave,
																				   this.muFileSaveAs,
																				   this.muFileClose,
																				   this.muFileSep2,
																				   this.muFileInformation,
																				   this.muFileSep3,
																				   this.muFileExit});
			this.muFile.Text = "&File";
			// 
			// muFileNew
			// 
			this.muFileNew.Index = 0;
			this.muFileNew.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
			this.muFileNew.Text = "&New";
			this.muFileNew.Click += new System.EventHandler(this.muFileNew_Click);
			// 
			// muFileSep1
			// 
			this.muFileSep1.Index = 1;
			this.muFileSep1.Text = "-";
			// 
			// muFileOpen
			// 
			this.muFileOpen.Index = 2;
			this.muFileOpen.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
			this.muFileOpen.Text = "&Open...";
			this.muFileOpen.Click += new System.EventHandler(this.muFileOpen_Click);
			// 
			// muFileSave
			// 
			this.muFileSave.Index = 3;
			this.muFileSave.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
			this.muFileSave.Text = "&Save";
			this.muFileSave.Click += new System.EventHandler(this.muFileSave_Click);
			// 
			// muFileSaveAs
			// 
			this.muFileSaveAs.Index = 4;
			this.muFileSaveAs.Text = "Save &As..";
			this.muFileSaveAs.Click += new System.EventHandler(this.muFileSaveAs_Click);
			// 
			// muFileClose
			// 
			this.muFileClose.Index = 5;
			this.muFileClose.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
			this.muFileClose.Text = "&Close";
			this.muFileClose.Click += new System.EventHandler(this.muFileClose_Click);
			// 
			// muFileSep2
			// 
			this.muFileSep2.Index = 6;
			this.muFileSep2.Text = "-";
			// 
			// muFileInformation
			// 
			this.muFileInformation.Index = 7;
			this.muFileInformation.Shortcut = System.Windows.Forms.Shortcut.CtrlI;
			this.muFileInformation.Text = "&Information...";
			this.muFileInformation.Click += new System.EventHandler(this.muFileInformation_Click);
			// 
			// muFileSep3
			// 
			this.muFileSep3.Index = 8;
			this.muFileSep3.Text = "-";
			// 
			// muFileExit
			// 
			this.muFileExit.Index = 9;
			this.muFileExit.Shortcut = System.Windows.Forms.Shortcut.CtrlX;
			this.muFileExit.Text = "E&xit";
			this.muFileExit.Click += new System.EventHandler(this.muFileExit_Click);
			// 
			// muAction
			// 
			this.muAction.Index = 1;
			this.muAction.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.muActionSetKey,
																					 this.muActionSetAlgo,
																					 this.muSetDecryptionKey,
																					 this.muActionSep1,
																					 this.muActionAddDirectory,
																					 this.muActionAddFiles,
																					 this.muActionAddComment,
																					 this.muActionSep4,
																					 this.muActionRemove,
																					 this.muActionSep2,
																					 this.muActionExtractTo,
																					 this.muActionExtractSelectionTo,
																					 this.muActionSep3,
																					 this.muActionView,
																					 this.muActionExecute});
			this.muAction.Text = "&Action";
			// 
			// muActionSetKey
			// 
			this.muActionSetKey.Index = 0;
			this.muActionSetKey.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						   this.muActionSetKeyForAll,
																						   this.muActionSetKeyForSelection});
			this.muActionSetKey.Text = "Set Encryption &Key";
			// 
			// muActionSetKeyForAll
			// 
			this.muActionSetKeyForAll.Index = 0;
			this.muActionSetKeyForAll.Shortcut = System.Windows.Forms.Shortcut.CtrlK;
			this.muActionSetKeyForAll.Text = "For &All...";
			this.muActionSetKeyForAll.Click += new System.EventHandler(this.muActionSetKeyForAll_Click);
			// 
			// muActionSetKeyForSelection
			// 
			this.muActionSetKeyForSelection.Index = 1;
			this.muActionSetKeyForSelection.Text = "For &Selection...";
			this.muActionSetKeyForSelection.Click += new System.EventHandler(this.muActionSetKeyForSelection_Click);
			// 
			// muActionSetAlgo
			// 
			this.muActionSetAlgo.Index = 1;
			this.muActionSetAlgo.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																							this.muActionSetAlgoAll,
																							this.muActionSetAlgoSelection});
			this.muActionSetAlgo.Text = "Set Encryption Al&gorithm";
			this.muActionSetAlgo.Click += new System.EventHandler(this.muActionSetAlgo_Click);
			// 
			// muActionSetAlgoAll
			// 
			this.muActionSetAlgoAll.Index = 0;
			this.muActionSetAlgoAll.Text = "For &All...";
			this.muActionSetAlgoAll.Click += new System.EventHandler(this.muActionSetAlgoAll_Click);
			// 
			// muActionSetAlgoSelection
			// 
			this.muActionSetAlgoSelection.Index = 1;
			this.muActionSetAlgoSelection.Text = "For &Selection...";
			this.muActionSetAlgoSelection.Click += new System.EventHandler(this.muActionSetAlgoSelection_Click);
			// 
			// muSetDecryptionKey
			// 
			this.muSetDecryptionKey.Index = 2;
			this.muSetDecryptionKey.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																							   this.muSetDecryptionKeyForAll,
																							   this.muSetDecryptionKeyForSelection});
			this.muSetDecryptionKey.Text = "Set Decryption Ke&y";
			this.muSetDecryptionKey.Click += new System.EventHandler(this.muSetDecryptionKey_Click);
			// 
			// muSetDecryptionKeyForAll
			// 
			this.muSetDecryptionKeyForAll.Index = 0;
			this.muSetDecryptionKeyForAll.Shortcut = System.Windows.Forms.Shortcut.CtrlD;
			this.muSetDecryptionKeyForAll.Text = "For &All...";
			this.muSetDecryptionKeyForAll.Click += new System.EventHandler(this.muSetDecryptionKeyForAll_Click);
			// 
			// muSetDecryptionKeyForSelection
			// 
			this.muSetDecryptionKeyForSelection.Index = 1;
			this.muSetDecryptionKeyForSelection.Text = "For &Selection...";
			this.muSetDecryptionKeyForSelection.Click += new System.EventHandler(this.muSetDecryptionKeyForSelection_Click);
			// 
			// muActionSep1
			// 
			this.muActionSep1.Index = 3;
			this.muActionSep1.Text = "-";
			// 
			// muActionAddDirectory
			// 
			this.muActionAddDirectory.Index = 4;
			this.muActionAddDirectory.Shortcut = System.Windows.Forms.Shortcut.CtrlR;
			this.muActionAddDirectory.Text = "Add &Directory...";
			this.muActionAddDirectory.Click += new System.EventHandler(this.muActionAddDirectory_Click);
			// 
			// muActionAddFiles
			// 
			this.muActionAddFiles.Index = 5;
			this.muActionAddFiles.Shortcut = System.Windows.Forms.Shortcut.CtrlA;
			this.muActionAddFiles.Text = "&Add Files...";
			this.muActionAddFiles.Click += new System.EventHandler(this.muActionAddFiles_Click);
			// 
			// muActionAddComment
			// 
			this.muActionAddComment.Index = 6;
			this.muActionAddComment.Text = "&Comment";
			this.muActionAddComment.Click += new System.EventHandler(this.muActionAddComment_Click);
			// 
			// muActionSep4
			// 
			this.muActionSep4.Index = 7;
			this.muActionSep4.Shortcut = System.Windows.Forms.Shortcut.ShiftF7;
			this.muActionSep4.ShowShortcut = false;
			this.muActionSep4.Text = "-";
			// 
			// muActionRemove
			// 
			this.muActionRemove.Index = 8;
			this.muActionRemove.Shortcut = System.Windows.Forms.Shortcut.Del;
			this.muActionRemove.Text = "&Remove selected";
			this.muActionRemove.Click += new System.EventHandler(this.muActionRemove_Click);
			// 
			// muActionSep2
			// 
			this.muActionSep2.Index = 9;
			this.muActionSep2.Text = "-";
			// 
			// muActionExtractTo
			// 
			this.muActionExtractTo.Index = 10;
			this.muActionExtractTo.Shortcut = System.Windows.Forms.Shortcut.CtrlE;
			this.muActionExtractTo.Text = "E&xtract All To...";
			this.muActionExtractTo.Click += new System.EventHandler(this.muActionExtractTo_Click);
			// 
			// muActionExtractSelectionTo
			// 
			this.muActionExtractSelectionTo.Index = 11;
			this.muActionExtractSelectionTo.Shortcut = System.Windows.Forms.Shortcut.CtrlT;
			this.muActionExtractSelectionTo.Text = "Ex&tract Selection To...";
			this.muActionExtractSelectionTo.Click += new System.EventHandler(this.muActionExtractSelectionTo_Click);
			// 
			// muActionSep3
			// 
			this.muActionSep3.Index = 12;
			this.muActionSep3.Text = "-";
			// 
			// muActionView
			// 
			this.muActionView.Index = 13;
			this.muActionView.Shortcut = System.Windows.Forms.Shortcut.CtrlV;
			this.muActionView.Text = "&View (internal)";
			this.muActionView.Click += new System.EventHandler(this.muActionView_Click);
			// 
			// muActionExecute
			// 
			this.muActionExecute.Index = 14;
			this.muActionExecute.Text = "Exec&ute (decrypt && execute)";
			this.muActionExecute.Click += new System.EventHandler(this.muActionExecute_Click);
			// 
			// muTools
			// 
			this.muTools.Index = 2;
			this.muTools.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					this.muToolsCreateSFX,
																					this.muToolsSep1,
																					this.muToolsSendEmail,
																					this.muToolsSendMsg,
																					this.muToolsReceiveMsg});
			this.muTools.Text = "&Tools";
			this.muTools.Visible = false;
			// 
			// muToolsCreateSFX
			// 
			this.muToolsCreateSFX.Index = 0;
			this.muToolsCreateSFX.Text = "Create &SFX...";
			// 
			// muToolsSep1
			// 
			this.muToolsSep1.Index = 1;
			this.muToolsSep1.Text = "-";
			// 
			// muToolsSendEmail
			// 
			this.muToolsSendEmail.Index = 2;
			this.muToolsSendEmail.Text = "Send as &e-mail...";
			// 
			// muToolsSendMsg
			// 
			this.muToolsSendMsg.Index = 3;
			this.muToolsSendMsg.Text = "Send &safe msg...";
			// 
			// muToolsReceiveMsg
			// 
			this.muToolsReceiveMsg.Index = 4;
			this.muToolsReceiveMsg.Text = "Receive s&afe msg...";
			// 
			// menuView
			// 
			this.menuView.Index = 3;
			this.menuView.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.menuViewDetails,
																					 this.menuViewSmall,
																					 this.menuViewLarge,
																					 this.menuViewList});
			this.menuView.Text = "&View";
			// 
			// menuViewDetails
			// 
			this.menuViewDetails.Checked = true;
			this.menuViewDetails.Index = 0;
			this.menuViewDetails.Text = "&Details";
			this.menuViewDetails.Click += new System.EventHandler(this.menuViewDetails_Click);
			// 
			// menuViewSmall
			// 
			this.menuViewSmall.Index = 1;
			this.menuViewSmall.Text = "&Small";
			this.menuViewSmall.Click += new System.EventHandler(this.menuViewSmall_Click);
			// 
			// menuViewLarge
			// 
			this.menuViewLarge.Index = 2;
			this.menuViewLarge.Text = "&Large";
			this.menuViewLarge.Click += new System.EventHandler(this.menuViewLarge_Click);
			// 
			// menuViewList
			// 
			this.menuViewList.Index = 3;
			this.menuViewList.Text = "L&ist";
			this.menuViewList.Click += new System.EventHandler(this.menuViewList_Click);
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 4;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuOptionsPreferences,
																					  this.menuOptionsDirectories});
			this.menuItem1.Text = "&Options";
			// 
			// menuOptionsPreferences
			// 
			this.menuOptionsPreferences.Index = 0;
			this.menuOptionsPreferences.Text = "&Preferences...";
			this.menuOptionsPreferences.Click += new System.EventHandler(this.menuOptionsPreferences_Click);
			// 
			// menuOptionsDirectories
			// 
			this.menuOptionsDirectories.Index = 1;
			this.menuOptionsDirectories.Text = "&Directories...";
			this.menuOptionsDirectories.Click += new System.EventHandler(this.menuOptionsDirectories_Click);
			// 
			// muHelp
			// 
			this.muHelp.Index = 5;
			this.muHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																				   this.muHelpContents,
																				   this.muHelpSearch,
																				   this.muHelpSep1,
																				   this.muHelpContext,
																				   this.muHelpSep2,
																				   this.muHelpAbout});
			this.muHelp.Text = "&Help";
			// 
			// muHelpContents
			// 
			this.muHelpContents.Index = 0;
			this.muHelpContents.Text = "&Contents";
			this.muHelpContents.Click += new System.EventHandler(this.muHelpContents_Click);
			// 
			// muHelpSearch
			// 
			this.muHelpSearch.Index = 1;
			this.muHelpSearch.Text = "S&earch";
			this.muHelpSearch.Visible = false;
			// 
			// muHelpSep1
			// 
			this.muHelpSep1.Index = 2;
			this.muHelpSep1.Text = "-";
			this.muHelpSep1.Visible = false;
			// 
			// muHelpContext
			// 
			this.muHelpContext.Index = 3;
			this.muHelpContext.Text = "Conte&xt Help";
			this.muHelpContext.Visible = false;
			// 
			// muHelpSep2
			// 
			this.muHelpSep2.Index = 4;
			this.muHelpSep2.Text = "-";
			// 
			// muHelpAbout
			// 
			this.muHelpAbout.Index = 5;
			this.muHelpAbout.Text = "&About CryptoSafe...";
			this.muHelpAbout.Click += new System.EventHandler(this.muHelpAbout_Click);
			// 
			// listViewFisiere
			// 
			this.listViewFisiere.AllowColumnReorder = true;
			this.listViewFisiere.AllowDrop = true;
			this.listViewFisiere.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listViewFisiere.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							  this.colName,
																							  this.colSize,
																							  this.colAlgorithm,
																							  this.colPath,
																							  this.colCRC});
			this.listViewFisiere.ContextMenu = this.contextMenu;
			this.listViewFisiere.FullRowSelect = true;
			this.listViewFisiere.LabelEdit = true;
			this.listViewFisiere.Location = new System.Drawing.Point(0, 42);
			this.listViewFisiere.Name = "listViewFisiere";
			this.listViewFisiere.Size = new System.Drawing.Size(432, 343);
			this.listViewFisiere.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.listViewFisiere.TabIndex = 0;
			this.listViewFisiere.View = System.Windows.Forms.View.Details;
			this.listViewFisiere.SelectedIndexChanged += new System.EventHandler(this.listViewFisiere_SelectedIndexChanged);
			// 
			// colName
			// 
			this.colName.Text = "Name";
			this.colName.Width = 110;
			// 
			// colSize
			// 
			this.colSize.Text = "Size";
			this.colSize.Width = 70;
			// 
			// colAlgorithm
			// 
			this.colAlgorithm.Text = "Algorithm";
			// 
			// colPath
			// 
			this.colPath.Text = "Path";
			this.colPath.Width = 120;
			// 
			// colCRC
			// 
			this.colCRC.Text = "CRC";
			this.colCRC.Width = 68;
			// 
			// contextMenu
			// 
			this.contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						this.menuItemAddDir,
																						this.menuItemAddFiles,
																						this.menuItemExtractSelection,
																						this.menuItemSep7,
																						this.menuItemSetDecrKey,
																						this.menuItemSetEncKey,
																						this.menuItemSetEncAlgo,
																						this.menuItem5,
																						this.menuItemExecute,
																						this.menuItemView});
			// 
			// menuItemAddDir
			// 
			this.menuItemAddDir.Index = 0;
			this.menuItemAddDir.Text = "Add &Directory...";
			this.menuItemAddDir.Click += new System.EventHandler(this.menuItemAddDir_Click);
			// 
			// menuItemAddFiles
			// 
			this.menuItemAddFiles.Index = 1;
			this.menuItemAddFiles.Text = "&Add Files...";
			this.menuItemAddFiles.Click += new System.EventHandler(this.menuItemAddFiles_Click);
			// 
			// menuItemExtractSelection
			// 
			this.menuItemExtractSelection.Index = 2;
			this.menuItemExtractSelection.Text = "Ex&tract To...";
			this.menuItemExtractSelection.Click += new System.EventHandler(this.menuItemExtractSeletion_Click);
			// 
			// menuItemSep7
			// 
			this.menuItemSep7.Index = 3;
			this.menuItemSep7.Text = "-";
			// 
			// menuItemSetDecrKey
			// 
			this.menuItemSetDecrKey.Index = 4;
			this.menuItemSetDecrKey.Text = "Set Decryption &Key...";
			this.menuItemSetDecrKey.Click += new System.EventHandler(this.menuItemSetDecrKey_Click);
			// 
			// menuItemSetEncKey
			// 
			this.menuItemSetEncKey.Index = 5;
			this.menuItemSetEncKey.Text = "Set Encryption Ke&y...";
			this.menuItemSetEncKey.Click += new System.EventHandler(this.menuItemSetEncKey_Click);
			// 
			// menuItemSetEncAlgo
			// 
			this.menuItemSetEncAlgo.Index = 6;
			this.menuItemSetEncAlgo.Text = "Set Encryption Al&gorithm...";
			this.menuItemSetEncAlgo.Click += new System.EventHandler(this.menuItemSetEncAlgo_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 7;
			this.menuItem5.Text = "-";
			// 
			// menuItemExecute
			// 
			this.menuItemExecute.Index = 8;
			this.menuItemExecute.Text = "Exec&ute (decrypt && execute)";
			this.menuItemExecute.Click += new System.EventHandler(this.menuItemExecute_Click);
			// 
			// menuItemView
			// 
			this.menuItemView.Index = 9;
			this.menuItemView.Text = "&View (internal)";
			this.menuItemView.Click += new System.EventHandler(this.menuItemView_Click);
			// 
			// toolBar
			// 
			this.toolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																					   this.toolBarButtonNew,
																					   this.toolBarButtonOpen,
																					   this.toolBarButtonSave,
																					   this.toolBarSep1,
																					   this.toolBarButtonAdd,
																					   this.toolBarButtonAddDir,
																					   this.toolBarSep2,
																					   this.toolBarButtonExtract,
																					   this.toolBarButtonView,
																					   this.toolBarButtonComment});
			this.toolBar.ButtonSize = new System.Drawing.Size(30, 30);
			this.toolBar.DropDownArrows = true;
			this.toolBar.Name = "toolBar";
			this.toolBar.ShowToolTips = true;
			this.toolBar.Size = new System.Drawing.Size(434, 33);
			this.toolBar.TabIndex = 1;
			this.toolBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar_ButtonClick);
			// 
			// toolBarButtonNew
			// 
			this.toolBarButtonNew.ImageIndex = 0;
			this.toolBarButtonNew.ToolTipText = "Create a new file";
			// 
			// toolBarButtonOpen
			// 
			this.toolBarButtonOpen.ImageIndex = 1;
			this.toolBarButtonOpen.ToolTipText = "Open a file";
			// 
			// toolBarButtonSave
			// 
			this.toolBarButtonSave.ImageIndex = 2;
			this.toolBarButtonSave.ToolTipText = "Save the current file";
			// 
			// toolBarSep1
			// 
			this.toolBarSep1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// toolBarButtonAdd
			// 
			this.toolBarButtonAdd.ImageIndex = 3;
			this.toolBarButtonAdd.ToolTipText = "Add files";
			// 
			// toolBarButtonAddDir
			// 
			this.toolBarButtonAddDir.ImageIndex = 4;
			this.toolBarButtonAddDir.ToolTipText = "Add directory";
			// 
			// toolBarSep2
			// 
			this.toolBarSep2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// toolBarButtonExtract
			// 
			this.toolBarButtonExtract.ImageIndex = 5;
			this.toolBarButtonExtract.ToolTipText = "Extract selected files";
			// 
			// toolBarButtonView
			// 
			this.toolBarButtonView.ImageIndex = 6;
			this.toolBarButtonView.ToolTipText = "View selected file";
			// 
			// toolBarButtonComment
			// 
			this.toolBarButtonComment.ImageIndex = 7;
			this.toolBarButtonComment.ToolTipText = "Comment";
			// 
			// statusBar
			// 
			this.statusBar.Location = new System.Drawing.Point(0, 388);
			this.statusBar.Name = "statusBar";
			this.statusBar.Size = new System.Drawing.Size(434, 22);
			this.statusBar.TabIndex = 2;
			this.statusBar.Text = "Ready";
			// 
			// App
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(434, 410);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.statusBar,
																		  this.toolBar,
																		  this.listViewFisiere});
			this.Menu = this.muMainMenu;
			this.MinimumSize = new System.Drawing.Size(300, 350);
			this.Name = "App";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "CryptoSafe";
			this.Load += new System.EventHandler(this.App_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string [] S) 
		{
			if(S.Length==0)
				Application.Run(new App());
			else
			{
			 App THIS=new App();
			 THIS.Visible=false;
			 int doWhat=1;
			
				switch(S[0])
				{
					case "e"://Extract To... / In place
					case "E":
						doWhat=0;
						break;
					case "A"://Add files
					case "a":
						doWhat=1;
						break;
					case "O"://Open
					case "o":
						doWhat=2;
						break;
					case "X"://Execute
					case "x":
						doWhat=3;
						break;
					case "i"://Add in place (single file)
					case "I":
						doWhat=4;
						break;
					default:
						return;
				}

				switch(doWhat)
				{
					case 1:
						THIS.fileNames=new StringCollection();
						THIS.muFileNew_Click(null,new EventArgs());
						SetKeyAlgoDialog kad = new SetKeyAlgoDialog();

						if(kad.ShowDialog()!=DialogResult.OK) return;

						THIS.k=kad.GetKey();
						THIS.a=kad.GetAlgo();

						int i=0;
						while(i<S.Length-1)
							{
							i++;
							string s=S[i];
							if(System.IO.Directory.Exists(s))
							 { 
								THIS.dirName=s;
								THIS.AddDir();
							 }
							}
						i=0;
						while(i<S.Length-1)
						{
							i++;
							string s=S[i];
							if(System.IO.File.Exists(s))
								THIS.fileNames.Add(s);
						}
						THIS.AddFiles();
						THIS.Visible=true;
						THIS.AddItems();
						THIS.Focus();
						Application.Run(THIS);
					break;
					
					case 2:
						if(S.Length<2) return;
						THIS.fn=S[1];
						THIS.Visible=true;
						THIS.OpenFile();
						THIS.Focus();
						Application.Run(THIS);
						break;
					
					case 0:
					 if(S.Length<2) return;
					 THIS.Visible=false;
					 THIS.fn=S[1];
					 THIS.OpenFile();
					 if(THIS.File==null) return;
						if(S.Length<3 || (S[2]!="!" && S[2]!="P"))
						{
							THIS.muActionExtractTo_Click(null,null);
						}
						else
						{
							SetDKeyDialog kad3 = new SetDKeyDialog(false);
							kad3.Activate();
							if(kad3.ShowDialog()!=DialogResult.OK) return;
							THIS.File.SetDecryptKey(kad3.GetKey());
							ExtractOptions.bBackground=false;
							ExtractOptions.to=System.IO.Path.GetDirectoryName(S[1]);
							THIS.File.SelectAll();
							THIS.DoExtract();
						}
					 if(S.Length>2)
					  if(THIS.statusBar.Text=="Extract completed")
					    if(S[2]=="!") ExecuteFileClass.FileDelete(S[1]);
					break;
					
					case 3:
						if(S.Length<2) return;
						THIS.Visible=false;
						THIS.fn=S[1];
						THIS.OpenFile();
						if(THIS.File==null) return;
						if(THIS.File.NoItems-(THIS.File.IsDeld(0)?0:1)!=1) return;
						ExtractOptions.bBackground=false;
						ExtractOptions.to=System.IO.Path.GetDirectoryName(S[1]);
						
						SetDKeyDialog kad2 = new SetDKeyDialog(false);
						kad2.Activate();
						if(kad2.ShowDialog()!=DialogResult.OK) return;

						THIS.File.SetDecryptKey(kad2.GetKey());

						Item it=THIS.File.GetItem(1);
						
						if(it.GetDataType()==Item.DataType.Encrypted) 
							if(it.ResLength==0)
							{
								try
								{
									it.Decrypt();
								}
								catch(Exception E)
								{
									App.ShowError(E,"decrypting");
									return;
								}
							}

						string fn=Path.GetTempFileName();

						try
						{
							FileStream St=new FileStream(fn,FileMode.Create);
							it.WriteTo(St,Item.DataType.Decrypted);
							St.Close();

							string ex=null;
							int j=it.Name.LastIndexOf('.');
							if(j>=0) ex=it.Name.Substring(j+1);

							System.IO.File.Move(fn,fn+"."+ex);
							fn+="."+ex;

							ExecuteFileClass C=new ExecuteFileClass(fn,it,false);
							AppOptions.ReverseUpdate=true;
							AppOptions.NotifyUpdate=false;
							Thread t = new Thread(new ThreadStart(C.Do));
							t.Start();
							t.Join();
						}
						catch(Exception E)
						{
							App.ShowError(E,"running file");
						}
						try
						{
							THIS.muFileSave_Click(THIS,new System.EventArgs());
						}
						catch(Exception E)
						{
						  App.ShowError(E,"saving file");
						}
						break;
					case 4:
						if(S.Length<2) return;
						if(!System.IO.File.Exists(S[1])) 
						{
						 App.ShowError(new IOException("File "+S[1]+" does not exist."),"adding file");
						 return;
						}
						THIS.fileNames=new StringCollection();
						THIS.muFileNew_Click(null,new EventArgs());
						SetKeyAlgoDialog kad4 = new SetKeyAlgoDialog();

						if(kad4.ShowDialog()!=DialogResult.OK) return;

						THIS.k=kad4.GetKey();
						THIS.a=kad4.GetAlgo();
	
						THIS.fileNames.Add(S[1]);
						THIS.AddFiles();
						THIS.fn=S[1]+".csm";
						THIS.muFileSave_Click(THIS,new EventArgs());
						if(S.Length>2)
							if(S[2]=="!")
								ExecuteFileClass.FileDelete(S[1]);
						break;
				}
			}
		}

		private void WinText()
		{
			if(File==null) 
			{
				this.Text="CryptoSafe 2.1";
                muFileSave.Enabled=muFileSaveAs.Enabled=muFileInformation.Enabled=muFileClose.Enabled=false;
				//muAction.Enabled=false;
				muToolsCreateSFX.Enabled=muToolsSendEmail.Enabled=false;
				listViewFisiere.Enabled=false;

				muActionAddFiles.Enabled=false;
				muActionAddDirectory.Enabled=false;
				muActionSetKeyForSelection.Enabled=false;
				muActionSetAlgoSelection.Enabled=false;
				muActionRemove.Enabled=false;
				muActionExtractSelectionTo.Enabled=false;
				muSetDecryptionKeyForSelection.Enabled=false;
				muActionView.Enabled=false;
				muActionExecute.Enabled=false;

				muActionSetKeyForAll.Enabled=false;
				muActionSetAlgoAll.Enabled=false;
				muSetDecryptionKeyForAll.Enabled=false;
				muActionAddComment.Enabled=false;
				muActionExtractTo.Enabled=false;

				menuItemAddFiles.Enabled=false;
				menuItemAddDir.Enabled=false;
				menuItemExtractSelection.Enabled=false;
				menuItemExecute.Enabled=false;
				menuItemSetDecrKey.Enabled=false;
				menuItemSetEncAlgo.Enabled=false;
				menuItemSetEncKey.Enabled=false;
				menuItemView.Enabled=false;
				menuItemExecute.Enabled=false;

				toolBarButtonSave.Enabled=false;
				toolBarButtonAdd.Enabled=false;
				toolBarButtonAddDir.Enabled=false;
				toolBarButtonExtract.Enabled=false;
				toolBarButtonView.Enabled=false;
				toolBarButtonComment.Enabled=false;
			}
			else 
			{
				this.Text="CryptoSafe 2.1 - "+Path.GetFileName(fn);
				muFileSave.Enabled=muFileSaveAs.Enabled=muFileInformation.Enabled=muFileClose.Enabled=true;
				muAction.Enabled=true;
				muToolsCreateSFX.Enabled=muToolsSendEmail.Enabled=true;
				muActionAddFiles.Enabled=true;
				muActionAddDirectory.Enabled=true;

				muActionSetKeyForAll.Enabled=true;
				muActionSetAlgoAll.Enabled=true;
				muSetDecryptionKeyForAll.Enabled=true;
				muActionAddComment.Enabled=true;
				muActionExtractTo.Enabled=true;
				
				listViewFisiere.Enabled=true;

				menuItemAddFiles.Enabled=true;
				menuItemAddDir.Enabled=true;

				toolBarButtonSave.Enabled=true;
				toolBarButtonAdd.Enabled=true;
				toolBarButtonAddDir.Enabled=true;
				toolBarButtonComment.Enabled=true;

				if(listViewFisiere.SelectedIndices.Count<1)
				{
					muActionSetKeyForSelection.Enabled=false;
					muActionSetAlgoSelection.Enabled=false;
					muActionRemove.Enabled=false;
					muActionExtractSelectionTo.Enabled=false;
					muSetDecryptionKeyForSelection.Enabled=false;
					muActionView.Enabled=false;
					muActionExecute.Enabled=false;

					menuItemExtractSelection.Enabled=false;
					menuItemExecute.Enabled=false;
					menuItemSetDecrKey.Enabled=false;
					menuItemSetEncAlgo.Enabled=false;
					menuItemSetEncKey.Enabled=false;
					menuItemView.Enabled=false;
					menuItemExecute.Enabled=false;
					toolBarButtonExtract.Enabled=false;
					toolBarButtonView.Enabled=false;
				}
				else
				{
					muActionSetKeyForSelection.Enabled=true;
					muActionSetAlgoSelection.Enabled=true;
					muActionRemove.Enabled=true;
					muActionExtractSelectionTo.Enabled=true;
					muSetDecryptionKeyForSelection.Enabled=true;
					muActionView.Enabled=listViewFisiere.SelectedIndices.Count==1;
					muActionExecute.Enabled=listViewFisiere.SelectedIndices.Count==1;

					menuItemExtractSelection.Enabled=true;
					menuItemExecute.Enabled=true;
					menuItemSetDecrKey.Enabled=true;
					menuItemSetEncAlgo.Enabled=true;
					menuItemSetEncKey.Enabled=true;
					menuItemView.Enabled=listViewFisiere.SelectedIndices.Count==1;
					menuItemExecute.Enabled=listViewFisiere.SelectedIndices.Count==1;

					toolBarButtonExtract.Enabled=true;
					toolBarButtonView.Enabled=listViewFisiere.SelectedIndices.Count==1;
				}
			}
		}

		private void muFileNew_Click(object sender, System.EventArgs e)
		{
		 muFileClose_Click(sender,e);
		 File = new CryptoFile();
		 fn = "Untitled";
		 WinText();
		 statusBar.Text="New file";
		}

		private void muFileClose_Click(object sender, System.EventArgs e)
		{
			if(bModified)
			{
			 if(MessageBox.Show("Save Changes Made?","File Modified",MessageBoxButtons.YesNo,MessageBoxIcon.Question)
				 == DialogResult.Yes)
				 muFileSave_Click(sender, e);
			}
			listViewFisiere.Items.Clear();
			bModified=false;
			listViewFisiere.SmallImageList.Images.Clear();
			listViewFisiere.LargeImageList.Images.Clear();
			fn=null;
			File=null;
			curPath="";
			dirName="";
			WinText();
			statusBar.Text="Ready";
		}

		int SelectSaveFile()
		{
			SaveFileDialog o = new SaveFileDialog();
            o.AddExtension=true;
			o.CheckPathExists=true;
			o.DefaultExt="csf";
			o.AddExtension=true;
			o.DereferenceLinks=true;
			o.ValidateNames=true;
			o.Filter="CrypoSafe Files (*.csf)|*.csf|All Files (*.*)|*.*";
			o.FilterIndex=0;
			o.InitialDirectory=AppOptions.SaveDir;
			o.Title="Save CryptoSafe File...";
			o.FileOk+=new CancelEventHandler(SingleFileOk);
			if(o.ShowDialog() != DialogResult.OK) return 0;
			fn=o.FileName;
			return 1;
		}

		int SelectOpenFile()
		{
			OpenFileDialog o = new OpenFileDialog();
			o.AddExtension=true;
			o.CheckPathExists=true;
			o.DefaultExt="csf";
			o.DereferenceLinks=true;
			o.ValidateNames=true;
			o.Filter="CrypoSafe Files (*.csf)|*.csf|CryptoSafe Self-Extracting (*.exe)|*.exe|CryptoSafe Single File (*.cmg)|*.cmg|All Files (*.*)|*.*";
			o.FilterIndex=0;
			o.InitialDirectory=AppOptions.OpenDir;
			o.Title="Open CryptoSafe File...";
			o.FileOk+=new CancelEventHandler(SingleFileOk);
			if(o.ShowDialog() != DialogResult.OK) return 0;
			fn=o.FileName;
			return 1;
		}

		void SingleFileOk(object sender, CancelEventArgs e)
		{
			if(((FileDialog)sender).FileNames.Length!=1)
			{
				MessageBox.Show("Please select one file","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
				e.Cancel=true;
			}
			e.Cancel=false;
		}

		private void SaveFileThread()
		{
			statusBar.Text="Saving file...";
			FileStream S=null;
			try 
			{
				S=new FileStream(fn,FileMode.Create);
				if(File.WriteTo(S)==0) 
				 statusBar.Text="File saved";
				else
				{statusBar.Text="File save failed";fn="Untitled";}
			}
			catch(Exception E)
			{
				if(E is ThreadAbortException || E is ThreadInterruptedException)
					statusBar.Text="Canceled";
				else 
				{
					ShowError(E,"saving file");
					statusBar.Text="An error has occured while saving file";
				}
			}
			finally
			{
				this.Cursor=Cursors.Default;
				if(S!=null) 
					S.Close();
				WinText();
			}
		}

		AutoResetEvent sfmre;

		private void muFileSave_Click(object sender, System.EventArgs e)
		{
		 if(fn==null || fn=="Untitled") 
			 if(SelectSaveFile()==0) return;
		 sfmre.WaitOne();
		 this.Cursor=Cursors.WaitCursor;
			Thread t=new Thread(new ThreadStart(SaveFileThread));
			t.IsBackground=false;
			t.Start();
			t.Join();
			if(t.ThreadState==System.Threading.ThreadState.Aborted)
				statusBar.Text="Canceled";
		this.Cursor=Cursors.Default;
		bModified=false;
		sfmre.Set();
		}

		public static void ShowError(Exception e, string process)
		{
			ExceptionMessageBox.Show(e,process);
		}

		private void muFileSaveAs_Click(object sender, System.EventArgs e)
		{
		 if(SelectSaveFile()==0) return;
		 muFileSave_Click(sender,e);
		}

		static public string GetNiceSize(long s)
		{
			if(s<1024) return s.ToString()+" bytes";
			if(s<1024*1024) return (s/1024).ToString()+" Kb";
			return (s/(1024*1024)).ToString() + " MB";
		}

		private void AddItems()
		{
			if(File==null) return;
			this.Cursor=Cursors.WaitCursor;
			listViewFisiere.Items.Clear();
			listViewFisiere.SmallImageList = new ImageList();
			listViewFisiere.SmallImageList.ImageSize=new Size(17,17);
			listViewFisiere.SmallImageList.ColorDepth=ColorDepth.Depth24Bit;
			listViewFisiere.LargeImageList = new ImageList();
			listViewFisiere.LargeImageList.ImageSize=new Size(32,32);
			listViewFisiere.SmallImageList.ColorDepth=ColorDepth.Depth24Bit;

			listViewFisiere.BeginUpdate();

			StringCollection sc = new StringCollection();
			long i=1,k=1;

			if(curPath.Length>0)
			{
				ListViewItem lit = new ListViewItem("[..]",(int)k);
				lit.Tag=(long)0;
				lit.UseItemStyleForSubItems=false;
				k++;
				lit.SubItems.Add("DIR");
				lit.SubItems.Add(" ");
				lit.SubItems.Add(curPath);
				Image R = GetFileIcon("..",false);
				if(R!=null)
				{
					lit.ImageIndex=listViewFisiere.SmallImageList.Images.Add(R,Color.Transparent);
					listViewFisiere.LargeImageList.Images.Add(R);
				}
				listViewFisiere.Items.Add(lit);
			}

			for(i=1;i<File.ItemCount;i++) 
				if(!File.IsDeld(i))
						{

					     Item it=File.GetItem(i);
					     string dir = GetNDir(curPath,it.Descr);
					     if(dir==null || dir.Length==0) continue;
						 if(sc.Contains(dir)) continue;
						 ListViewItem lit = new ListViewItem(dir,(int)k);
						 lit.UseItemStyleForSubItems=false;
						 lit.Tag=-i;
						 k++;
						 lit.SubItems.Add("DIR");
						 lit.SubItems.Add(" ");
						 lit.SubItems.Add(curPath);
						 Image R=GetFileIcon("\\^$FOLDER",true);
						 if(R!=null)
							{
								lit.ImageIndex=listViewFisiere.SmallImageList.Images.Add(R,Color.Transparent);
								listViewFisiere.LargeImageList.Images.Add(R);
							}	 
						 listViewFisiere.Items.Add(lit);
						 sc.Add(dir);
                        }

			for(i=1;i<File.ItemCount;i++) if(!File.IsDeld(i))
										  {
											  Item it=File.GetItem(i);
											  string nd=GetNDir(curPath,it.Descr);
											  if(nd==null || nd.Length>0) continue;
											  ListViewItem lit = new ListViewItem(it.Name);
											  lit.UseItemStyleForSubItems=false;
											  lit.Tag=i;
											  string nsize = GetNiceSize(it.DataLength);
											  if(it.GetDataType()==Item.DataType.Encrypted)
												  nsize+=" (encrypted)";
											  lit.SubItems.Add(nsize);
											  lit.SubItems.Add(it.algo.ToString());
											  lit.SubItems.Add(it.Descr);
											  if(it.CRC_data_OK) lit.SubItems.Add("OK",Color.Green,lit.BackColor,new Font(lit.Font,FontStyle.Bold));
											  else lit.SubItems.Add("FAILED",Color.Red,lit.BackColor,new Font(lit.Font,FontStyle.Bold));

											  Image R=GetFileIcon(it.Name,true);
											  if(R!=null)
											  {
												  lit.ImageIndex=listViewFisiere.SmallImageList.Images.Add(R,Color.Transparent);
												  listViewFisiere.LargeImageList.Images.Add(R);
											  }
											  listViewFisiere.Items.Add(lit);
										  }
		 sc.Clear();
		 listViewFisiere.EndUpdate();
			if(listViewFisiere.Items.Count>0)
			{
				listViewFisiere.Items[0].Selected=true;
				listViewFisiere.Items[0].Focused=true;
			}
		 this.Cursor=Cursors.Default;
		}

		private void OpenFileThread()
		{
			FileStream S=null;
			try 
			{
				S = new FileStream(fn,FileMode.Open,FileAccess.Read);
				File = new CryptoFile(S);
				S.Close();
				if(AppOptions.SetDKeyOnOpen)
					muSetDecryptionKeyForAll_Click(this,new System.EventArgs());
			}
			catch(Exception E)
			{
				if(E is ThreadAbortException || E is ThreadInterruptedException)
				 statusBar.Text="Canceled";
				else {
					ShowError(E,"opening file");
					statusBar.Text="An error has occured while opening file";
					 }
				fn=null;
				File=null;
				return;
			}
			finally
			{
				if(S!=null) S.Close();
			}

			try
			{
				AddItems();
				statusBar.Text="File opened";
			}
			catch(Exception E)
			{
				statusBar.Text="An error has occured while opening file";
			    File=null;
				fn=null;
			}
			WinText();
		}

		void OpenFile()
		{
			ofmre.WaitOne();
			this.Cursor=Cursors.WaitCursor;
			Thread t = new Thread(new ThreadStart(OpenFileThread));
			t.Name="Open File";
			t.Start();
			t.Join();
			if(t.ThreadState==System.Threading.ThreadState.Aborted)
				statusBar.Text="Canceled";
			this.Cursor=Cursors.Default;
			ofmre.Set();
		}

		AutoResetEvent ofmre;

		private void muFileOpen_Click(object sender, System.EventArgs e)
		{
		 muFileClose_Click(sender, e);
		 if(SelectOpenFile()==0) return;
		 statusBar.Text="Opening file...";

		 OpenFile();
		}

		private void muFileExit_Click(object sender, System.EventArgs e)
		{
		 muFileClose_Click(sender, e);
		 Application.Exit();
		}

		void AddFile(string s, Item.AlgoTypes a, string key)
		{
		 FileStream S = new FileStream(s,FileMode.Open,FileAccess.Read);
		 long i=File.AddItem(S,a);
		 S.Close();
		 Item it = File.GetItem(i);
		 
		 string s1=string.Empty;
		 int j = s.LastIndexOf('\\'); 
		 it.Name = s.Substring(j+1);
  		 it.Descr = curPath;
			if(dirName.Length>0)
			{
				if(curPath.Length>0) it.Descr+='\\';
				j = dirName.LastIndexOf('\\');
				j++;
				it.Descr+=s.Substring(j,s.LastIndexOf('\\')-j);
			}
		 it.key=key;

		 /*
		 ListViewItem lit = new ListViewItem(it.Name);
		 lit.Tag=i;
		 lit.UseItemStyleForSubItems=false;
		 string nsize = GetNiceSize(it.DataLength);
		 lit.SubItems.Add(nsize,Color.Green,Color.Green,lit.Font);
		 lit.SubItems.Add(it.algo.ToString());
		 lit.SubItems.Add(it.Descr);
		 if(it.CRC_data_OK) 
			 lit.SubItems.Add("OK",Color.Green,Color.Green,lit.Font);
		 else 
			 lit.SubItems.Add("FAILED",Color.Red,Color.Red,lit.Font);

		 lit.ImageIndex=listViewFisiere.SmallImageList.Images.Add(GetFileIcon(it.Name,false),Color.Transparent);
		 listViewFisiere.LargeImageList.Images.Add(GetFileIcon(it.Name,true),Color.Transparent);
		 
		 listViewFisiere.Items.Add(lit);
		 /**/
		}

		
        string k;
		Item.AlgoTypes a;
		StringCollection fileNames;

		private void AddingFilesThread()
		{
			AddFileOptions afo;
			afo = new AddFileOptions();
			int i=0;
			foreach(string fn in fileNames)
			{
				i++;
				try
				{
					if(System.IO.File.Exists(fn))
					{
						AddFile(fn,a,k);
						if(AppOptions.DelAfterAdding)
							ExecuteFileClass.FileDelete(fn);
					}
				}
				catch(Exception E)
				{
					if(E is ThreadAbortException || E is ThreadInterruptedException)
						statusBar.Text="Canceled";
					else 
					{
						if(afo.HaltOnFail)
							if(afo.OnException(E,fn,false)!=FileOptions.Result.Ignore)
							{
								statusBar.Text="An error has occured while adding files";
								afo.Done();
								return;
							}
					}
				}
				afo.SetProgress(i*100/fileNames.Count);
			}
			statusBar.Text="Files added";
			afo.Done();
		}

		private void AddFiles()
		{
			Thread t=new Thread(new ThreadStart(AddingFilesThread));
			t.Start();
			t.Join();
			if(t.ThreadState==System.Threading.ThreadState.Aborted)
				statusBar.Text="An error has occured while adding files";
		}

		private void muActionAddFiles_Click(object sender, System.EventArgs e)
		{
          OpenFileDialog o = new OpenFileDialog();
		 	o.CheckFileExists=true;
			o.DereferenceLinks=true;
			o.ValidateNames=true;
			o.Filter="All Files (*.*)|*.*|Application Files (*.exe; *.com; *.dll)|*.exe;*.com;*.dll|Text Files (*.txt)|*.txt|Already encrypted files (*.csf;*.csm)|*.csf;*.csm";
			o.FilterIndex=0;
			o.InitialDirectory=AppOptions.AddDir;
			o.Title="Add Files...";
			o.Multiselect=true;
			if(o.ShowDialog() != DialogResult.OK) return;

			SetKeyAlgoDialog kad = new SetKeyAlgoDialog();

			if(kad.ShowDialog(this)!=DialogResult.OK) return;

			k=kad.GetKey();
			a=kad.GetAlgo();
			fileNames=new StringCollection();
			fileNames.AddRange(o.FileNames);
			this.Cursor=Cursors.WaitCursor;
			AddFiles();
			AddItems();
			this.Cursor=Cursors.Default;
			bModified=true;
			k=null;
		    fileNames=null;
		}

		private void listViewFisiere_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			WinText();
		}

		private void muActionSetKeyForAll_Click(object sender, System.EventArgs e)
		{
			if(File==null) return;
			SetKeyDialog kad = new SetKeyDialog();
			if(kad.ShowDialog(this)!=DialogResult.OK) return;

			try
			{
				File.SetKey(kad.GetKey());
				statusBar.Text="Key set for all nonecrypted items";
			}
			catch(Exception E)
			{
				ShowError(E,"setting key");
				statusBar.Text="Some errors occured while setting key";
			}
		}

		private void muActionSetAlgoAll_Click(object sender, System.EventArgs e)
		{
			if(File==null) return;
			SetAlgoDialog kad = new SetAlgoDialog();
			if(kad.ShowDialog(this)!=DialogResult.OK) return;

			try
			{
				File.SetAlgo(kad.GetAlgo());
				AddItems();
				statusBar.Text="Algorithm set for all nonecrypted items";
			}
			catch(Exception E)
			{
				ShowError(E,"setting algorithm");
				statusBar.Text="Some errors occured while setting algorithm";
			}
		}

		private void muActionSetKeyForSelection_Click(object sender, System.EventArgs e)
		{
			if(listViewFisiere.SelectedItems.Count<1) return;
			SetKeyDialog kad = new SetKeyDialog();
			if(kad.ShowDialog(this)!=DialogResult.OK) return;

            bool hadE=false;
			foreach(ListViewItem lit in listViewFisiere.SelectedItems)
			{
				long i=(long)lit.Tag;
				if(i==0) continue;
				if(i<0)
				{
					string s=curPath;
					if(curPath.Length>0) s+='\\';
					s+=lit.Text;
					if(SetEncKeyDir(s,kad.GetKey()))
						 hadE=true;
					continue;
				}
				Item it=File.GetItem(i);
				if(it.GetDataType()==Item.DataType.Decrypted)
				{
					it.key=kad.GetKey();
					it.ResLength=0;
				}
				else hadE=true;
			}
			if(hadE) statusBar.Text=statusBar.Text="Some items were not modified because they were already encrypted";
			else statusBar.Text="Key set successfuly";
		}

		private void muActionSetAlgoSelection_Click(object sender, System.EventArgs e)
		{
			if(listViewFisiere.SelectedItems.Count<1) return;
			SetAlgoDialog kad = new SetAlgoDialog();
			if(kad.ShowDialog(this)!=DialogResult.OK) return;

			bool hadE=false;
			foreach(ListViewItem lit in listViewFisiere.SelectedItems)
			{
				long i=(long)lit.Tag;
				if(i<=0)
				{
					string s=curPath;
					if(curPath.Length>0) s+='\\';
					s+=lit.Text;
					if(SetAlgoDir(s,kad.GetAlgo()))
						hadE=true;
					continue;
				}
				Item it=File.GetItem(i);
				if(it.GetDataType()==Item.DataType.Decrypted)
				{
					it.algo=kad.GetAlgo();
					it.ResLength=0;
					lit.SubItems.RemoveAt(2);
					lit.SubItems.Insert(2,new ListViewItem.ListViewSubItem(lit,it.algo.ToString()));
				}
				else hadE=true;
			}
			if(hadE) statusBar.Text=statusBar.Text="Some items were not modified because they were already encrypted";
			else statusBar.Text="Algorithm set successfuly";
		}

		private void muActionRemove_Click(object sender, System.EventArgs e)
		{
			foreach(ListViewItem lit in listViewFisiere.SelectedItems)
			{
				long i=(long)lit.Tag;
				if(i==0) continue;
				if(i<0)
				{
					string s=curPath;
					if(curPath.Length>0) s+='\\';
					s+=lit.Text;
					RemoveDir(s);
					lit.Remove();
					continue;
				}
				File.DelItem(i);
				lit.Remove();
			}
		}

		AutoResetEvent etmre;
		bool bExtracting;

		private void ExtractThread()
		{
			bExtracting=true;
			try
			{
				if(File.Extract()<0)
					statusBar.Text="An error occured while extracting";
				else
					statusBar.Text="Extract completed";
			}
			catch(GenericException E)
			{
				if(E.Type==GenericException.Types.Warrning)
				{
					statusBar.Text=E.Descr;
				}
				else ShowError(E,"extracting files");
			}
			catch(Exception E)
			{
				if(E is ThreadAbortException || E is ThreadInterruptedException)
					statusBar.Text="Canceled";
				else ShowError(E,"extracting files");
			}
			finally
			{
				bExtracting=false;
				etmre.Set();
			}
		}

		private void DoExtract()
		{
			etmre.WaitOne();
			this.Cursor=Cursors.WaitCursor;
			Thread t = new Thread(new ThreadStart(ExtractThread));
			t.IsBackground=false;
			t.Start();
			if(!ExtractOptions.bBackground) 
			{
				t.Join();
				if(t.ThreadState==System.Threading.ThreadState.Aborted)
					statusBar.Text="Canceled";
			}
			this.Cursor=Cursors.Default;
		}

		private void muActionExtractTo_Click(object sender, System.EventArgs e)
		{
			if(File==null) return;
			if(bExtracting)
			{
				MessageBox.Show("A decrypt operation is already in progress. Wait for it to finish or cancel it before starting a new one!","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
				return;
			}

			CancelEventArgs Ea=new CancelEventArgs(false);
			muSetDecryptionKeyForAll_Click(sender,Ea);
			if(Ea.Cancel) return;
			
			ExtractDialog ed;
			ed = new ExtractDialog();
			ed.SetOptions();
			if(ed.ShowDialog(this)!=DialogResult.OK)
				return;
			ed.GetOptions();

            File.SelectAll();
            
			DoExtract();
		}

		private void muSetDecryptionKeyForAll_Click(object sender, System.EventArgs e)
		{
			if(File==null) return;
			SetDKeyDialog kad = new SetDKeyDialog(true);
			switch(kad.ShowDialog(this))
			{
				case DialogResult.Cancel:
					if(e is CancelEventArgs) ((CancelEventArgs)e).Cancel=true;
					return;
				case DialogResult.OK:
					try
					{
						File.SetDecryptKey(kad.GetKey());
						statusBar.Text="Key set successfuly";
					}
					catch(Exception E)
					{
						ShowError(E,"setting key");
						statusBar.Text="Some errors occured while setting key";
					}
					break;
				case DialogResult.Ignore: break;
			}
		}

		private void muSetDecryptionKeyForSelection_Click(object sender, System.EventArgs e)
		{
			if(listViewFisiere.SelectedItems.Count<1) return;
			SetDKeyDialog kad = new SetDKeyDialog(true);
			if(kad.ShowDialog(this)!=DialogResult.OK) return;

			bool hadE=false;
			foreach(ListViewItem lit in listViewFisiere.SelectedItems)
			{
				long i=(long)lit.Tag;
				if(i==0) continue;
				if(i<0)
				{
					string s=curPath;
					if(curPath.Length>0) s+='\\';
					s+=lit.Text;
					if(SetDecKeyDir(s,kad.GetKey()))
						hadE=true;
					continue;
				}
				Item it=File.GetItem(i);
				if(it.GetDataType()==Item.DataType.Encrypted)
				{
					it.key=kad.GetKey();
					it.ResLength=0;
				}
				else hadE=true;
			}
			if(hadE) statusBar.Text=statusBar.Text="Some items were not modified because they were never encrypted";
			else statusBar.Text="Key set successfuly";
		}

		private void muActionExtractSelectionTo_Click(object sender, System.EventArgs e)
		{
			if(bExtracting)
			{
				MessageBox.Show("A decrypt operation is already in progress. Wait for it to finish or cancel it before starting a new one","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
				return;
			}

			CancelEventArgs Ea=new CancelEventArgs(false);
			muSetDecryptionKeyForAll_Click(sender,Ea);
			if(Ea.Cancel) return;
			
			ExtractDialog ed;
			ed = new ExtractDialog();
			ed.SetOptions();
			if(ed.ShowDialog(this)!=DialogResult.OK)
				return;
			ed.GetOptions();

			File.DeselectAll();

			foreach(ListViewItem lit in listViewFisiere.SelectedItems)
			{
				long i=(long)lit.Tag;
				if(i==0) continue;
				if(i<0)
				{
					string s=curPath;
					if(curPath.Length>0) s+='\\';
					s+=lit.Text;
					SetDirToExtract(s);
				}
				else
				File.SetToExtract(i);
			}

			DoExtract();
		}

		bool bDecryptSelectionOK;

		void RemoveDir(string s)
		{
			for(long i=1;i<File.ItemCount;i++) 
				if(!File.IsDeld(i))
				{
					Item it=File.GetItem(i);
					string nd=GetNDir(s,it.Descr);
					if(nd==null) continue;
					File.DelItem(i);
					i--;
				}
		}

		bool SetEncKeyDir(string s, string k)
		{
			bool hadE=false;
			for(long i=1;i<File.ItemCount;i++) 
				if(!File.IsDeld(i))
				{
					Item it=File.GetItem(i);
					string nd=GetNDir(s,it.Descr);
					if(nd==null) continue;
					if(it.GetDataType()==Item.DataType.Encrypted)
						hadE=true;
					else
					{
						it.key=k;
						it.ResLength=0;
					}
				}
			return hadE;
		}

		bool SetAlgoDir(string s, Item.AlgoTypes a)
		{
			bool hadE=false;
			for(long i=1;i<File.ItemCount;i++) 
				if(!File.IsDeld(i))
				{
					Item it=File.GetItem(i);
					string nd=GetNDir(s,it.Descr);
					if(nd==null) continue;
					if(it.GetDataType()==Item.DataType.Encrypted)
						hadE=true;
					else
					{
						it.algo=a;
						it.ResLength=0;
					}
				}
			return hadE;
		}

		bool SetDecKeyDir(string s, string k)
		{
			bool hadE=false;
			for(long i=1;i<File.ItemCount;i++) 
				if(!File.IsDeld(i))
				{
					Item it=File.GetItem(i);
					string nd=GetNDir(s,it.Descr);
					if(nd==null) continue;
					if(it.GetDataType()==Item.DataType.Decrypted)
						hadE=true;
					else
					{
						it.key=k;
						it.ResLength=0;
					}
				}
			return hadE;
		}

		int DecryptDir(string s, ExtractOptions eo)
		{
		long i=1;
			for(i=1;i<File.NoItems;i++)
			{
				Item it=File.GetItem(i);

				string nd=GetNDir(s,it.Descr);
				if(nd==null) continue;
				if(it.GetDataType()==Item.DataType.Decrypted) continue;

				if(!it.IsKeySet())
				{
					SetDKeyDialog kad = new SetDKeyDialog(false);
					kad.Focus();
					if(kad.ShowDialog()!=DialogResult.OK) 
					{
						bDecryptSelectionOK=false;
						continue;
					}
					else it.key=kad.GetKey();
				}

				while(true)
					try
					{
						it.Decrypt();
						break;
					}
					catch(Exception E)
					{
						if(E is ThreadInterruptedException || E is	ThreadAbortException)
						{eo.Done();bDecryptSelectionOK=false;return -1;}
						if(eo.HaltOnFail)
						{
							FileOptions.Result j=eo.OnException(E,it.GetFullName(),true);
							if(j==FileOptions.Result.Retry)
							{
								string s2=null;
								if((s2=eo.NewKey)!=null)
									it.key=s2;
								continue;
							}
							if(j==FileOptions.Result.Abort) 
							{
								bDecryptSelectionOK=false;
								eo.Done();
								return -1;
							}
							if(j==FileOptions.Result.Ignore) 
							{
								bDecryptSelectionOK=false;
								break;
							}
						}
						else break;
					}
			}
			return 0;
		}

		void SetDirToExtract(string s)
		{
		 for(long i=1;i<File.ItemCount;i++) 
			 if(!File.IsDeld(i))
			 {
				 Item it=File.GetItem(i);
				 string nd=GetNDir(s,it.Descr);
				 if(nd==null) continue;
				 File.SetToExtract(i);
			 }
		}

		void DecryptSelectionThread()
		{
			ExtractOptions eo = new ExtractOptions("Decrypting...");
			Thread.Sleep(100);
			try
			{
				int ind=0;
				bDecryptSelectionOK=true;
				foreach(ListViewItem lit in listViewFisiere.SelectedItems)
				{
					ind++;
					long i=(long)lit.Tag;
					if(i<=0)
					{
						string s=curPath;
						if(curPath.Length>0) s+='\\';
						s+=lit.Text;
						if(DecryptDir(s,eo)<0) 
							return; //Operation aborted
						continue;
					}
					Item it=File.GetItem(i);
					if(it.GetDataType()==Item.DataType.Decrypted) continue;

					if(!it.IsKeySet())
					{
						SetDKeyDialog kad = new SetDKeyDialog(false);
						kad.Focus();
						if(kad.ShowDialog()!=DialogResult.OK) 
						{
							bDecryptSelectionOK=false;
							continue;
						}
						else it.key=kad.GetKey();
					} 

					while(true)
						try
						{
							it.Decrypt();
							break;
						}
						catch(Exception E)
						{
							if(E is ThreadInterruptedException || E is	ThreadAbortException)
							{eo.Done();bDecryptSelectionOK=false;return;}
							if(eo.HaltOnFail)
							{
								FileOptions.Result j=eo.OnException(E,it.GetFullName(),true);
								if(j==FileOptions.Result.Retry)
								{
									string s=null;
									if((s=eo.NewKey)!=null)
										it.key=s;
									continue;
								}
								if(j==FileOptions.Result.Abort) 
								{
									bDecryptSelectionOK=false;
									eo.Done();
									return;
								}
								if(j==FileOptions.Result.Ignore) 
								{
									bDecryptSelectionOK=false;
									break;
								}
							}
							else break;
						}
					eo.SetProgress((int)(100*ind/listViewFisiere.SelectedItems.Count));
				}
				eo.Done();
			}
			catch(Exception E)
			{
			bDecryptSelectionOK=false;
			 if(E is ThreadInterruptedException || E is	ThreadAbortException)
			  return;
			}
		}

		bool DecryptSelection()
		{
		  this.Cursor=Cursors.WaitCursor;
          Thread t = new Thread(new ThreadStart(DecryptSelectionThread));
		  t.Start();
		  t.Join();
		  this.Cursor=Cursors.Default;
		  return bDecryptSelectionOK;
		}

		private void muActionView_Click(object sender, System.EventArgs e)
		{
			if(listViewFisiere.SelectedItems.Count!=1) return;

            TextViewDialog td = null;
			long jj=(long)listViewFisiere.SelectedItems[0].Tag;
			if(jj<=0) return;
			Item it = File.GetItem(jj);

			if(it.GetDataType()==Item.DataType.Encrypted) if(it.ResLength==0)
				if(!DecryptSelection())
				{
					statusBar.Text="Error occurred while decrypting";
					return;
				}

			td = new TextViewDialog(it.ToString(),it.GetDataType()==Item.DataType.Encrypted);
			td.Text="View item:";
			if(td.ShowDialog(this)!=DialogResult.OK || !td.bModified) return;

			if(td.text.Length>0)
			{
				MemoryStream ms = new MemoryStream();
				byte [] bin = Encoding.ASCII.GetBytes(td.text);
				ms.Write(bin,0,bin.Length);
		
				Item itt=new Item(ms,Item.DataType.Decrypted);
				itt.Name=it.Name;
				itt.Descr=it.Descr;
				itt.algo=it.algo;

				File.SetItemAt(itt,jj);
				listViewFisiere.SelectedItems[0].SubItems[1].Text=GetNiceSize(itt.DataLength);
			}
			else
			{
				listViewFisiere.SelectedItems[0].Remove();
				File.DelItem(jj);
			}
		}

		private void muActionAddComment_Click(object sender, System.EventArgs e)
		{
			if(File==null) return;
			TextViewDialog td = null;
			if(File.IsDeld(0))
			{
				td = new TextViewDialog("",false);
				td.Text="Comment:";
				if(td.ShowDialog()!=DialogResult.OK) return;

			}
			else
			{
				td = new TextViewDialog(File.GetItem(0).ToString(),false);
				td.Text="Comment:";
				if(td.ShowDialog()!=DialogResult.OK || !td.bModified) return;
			}

			if(td.text.Length>0)
			{
				MemoryStream ms = new MemoryStream();
				byte [] bin = Encoding.ASCII.GetBytes(td.text);
				ms.Write(bin,0,bin.Length);
		
				Item it=new Item(ms,Item.DataType.Decrypted);
				it.key="CSFComment";
				it.Name="%$Comment/$%";
				it.Descr="";

				File.UnDel(0);
				File.SetItemAt(it,0);
			}
			else 
			 File.DelItem(0);
		}

		protected void OnLabelEditFinish(object sender, LabelEditEventArgs e)
		{
			muActionRemove.Enabled=true;
			if(e.Label==null || e.Label.Length==0) {e.CancelEdit=true;return;}
			ListViewItem it = listViewFisiere.Items[e.Item];
			if((long)it.Tag<=0) {e.CancelEdit=true;return;}
			File.GetItem((long)it.Tag).Name=e.Label;

			Image R=GetFileIcon(e.Label,false);
			if(R!=null)
			{
				listViewFisiere.SmallImageList.Images[listViewFisiere.Items[e.Item].ImageIndex]=R;
				listViewFisiere.LargeImageList.Images[listViewFisiere.Items[e.Item].ImageIndex]=R;
			}
		}

		protected void OnLabelEditBegin(object sender, LabelEditEventArgs e)
		{
			if((long)listViewFisiere.Items[e.Item].Tag<=0)
			{
				e.CancelEdit=true;
				return;
			}
            muActionRemove.Enabled=false;
		}


		private void muActionExecute_Click(object sender, System.EventArgs e)
		{
			if(listViewFisiere.SelectedItems.Count!=1) return;

			long jj=(long)listViewFisiere.SelectedItems[0].Tag;
			if(jj<=0) return;
			Item it = File.GetItem(jj);

			if(it.GetDataType()==Item.DataType.Encrypted) 
				if(it.ResLength==0)
							  if(!DecryptSelection())
										  {
											  statusBar.Text="Error occurred while decrypting";
											  return;
										  }
			string fn=Path.GetTempFileName();

			this.Cursor=Cursors.WaitCursor;
			try
			{
				FileStream S=new FileStream(fn,FileMode.Create);
				it.WriteTo(S,Item.DataType.Decrypted);
				S.Close();

				string ex=null;
				int j=it.Name.LastIndexOf('.');
				if(j>=0) ex=it.Name.Substring(j+1);

				System.IO.File.Move(fn,fn+"."+ex);
				fn+="."+ex;

				ExecuteFileClass C=new ExecuteFileClass(fn,it,sender!=null);
				C.ItemUpdated+=new ExecuteFileClass.ItemUpdatedHandler(OnItemUpdated);
				Thread t = new Thread(new ThreadStart(C.Do));
				t.Start();
			}
			catch(Exception E)
			{
				ShowError(E,"running file");
			}
			this.Cursor=Cursors.Default;
		}

		private void muActionSetAlgo_Click(object sender, System.EventArgs e)
		{
        muActionSetAlgoSelection_Click(sender,e);		
		}

		private void muSetDecryptionKey_Click(object sender, System.EventArgs e)
		{
         muSetDecryptionKeyForSelection_Click(sender,e);
		}

		private void menuItemAddFiles_Click(object sender, System.EventArgs e)
		{
		 muActionAddFiles_Click(sender,e);
		}

		private void menuItemSetEncKey_Click(object sender, System.EventArgs e)
		{
		 muActionSetKeyForSelection_Click(sender,e);
		}

		private void menuItemView_Click(object sender, System.EventArgs e)
		{
			muActionView_Click(sender,e);
		}

		private void menuItemExtractSeletion_Click(object sender, System.EventArgs e)
		{
			muActionExtractSelectionTo_Click(sender,e);
		}

		private void menuItemSetDecrKey_Click(object sender, System.EventArgs e)
		{
			muSetDecryptionKeyForSelection_Click(sender,e);
		}

		private void menuItemSetEncAlgo_Click(object sender, System.EventArgs e)
		{
			muActionSetAlgoSelection_Click(sender,e);
		}

		private void menuItemExecute_Click(object sender, System.EventArgs e)
		{
			muActionExecute_Click(sender,e);
		}


		string curPath;

		static string GetNDir(string path, string cp)
		{
			if(!cp.StartsWith(path)) return null;
			else if(path==cp) return "";
			int l=path.Length;
			if(l>0 && cp[l]!='\\') return null;
			string p1=null;
			if(l>0) p1=cp.Substring(l+1);
			else p1=cp;
			int j=p1.IndexOf('\\');
			if(j==-1) return p1;
			else return p1.Substring(0,j);
		}

		void RefreshItems()
		{
		 AddItems();
		}

		private void OnDoubleClick(object sender, System.EventArgs e)
		{
		 if(File==null) return;
		  if(listViewFisiere.SelectedItems.Count==0) {muActionAddFiles_Click(sender,e);return;}
		  if(listViewFisiere.SelectedItems.Count>1) return;
		 ListViewItem lit=listViewFisiere.SelectedItems[0];
			if((long)lit.Tag<0)
			{
				if(curPath.Length>0) curPath+='\\';
				curPath+=lit.Text;
				AddItems();
				return;
			}
			else if((long)lit.Tag==0)
			{
				int j=curPath.LastIndexOf('\\');
				if(j==-1) curPath="";
				else curPath=curPath.Substring(0,j);
				AddItems();
				return;
			}
		 muActionExecute_Click(sender,e);
        }

		/// <summary>
		/// Completeaza colectia fileNames cu numele fisierelor din director
		/// </summary>
		/// <param name="dir">Directorul in care sa caute fisiere cu care sa completeze fileNames</param>
		private void AddDirectory(string dir)
		{
		 fileNames.AddRange(System.IO.Directory.GetFiles(dir));
			foreach(string s in System.IO.Directory.GetDirectories(dir))
			{
				AddDirectory(s);
			}
		}

		string dirName="";

		void AddDir()
		{
			fileNames=new StringCollection();
			AddDirectory(dirName);

			Thread t=new Thread(new ThreadStart(AddingFilesThread));
			t.Start();
			t.Join();
			if(t.ThreadState==System.Threading.ThreadState.Aborted)
				statusBar.Text="An error has occured while adding files";
			fileNames=null;
			dirName="";
		}

		private void muActionAddDirectory_Click(object sender, System.EventArgs e)
		{
			OpenDirectoryDialog o = new OpenDirectoryDialog();
			//o.InitialDirectory=AppOptions.AddDir;
			//o.Title="Add Directory...";
			if(o.ShowDialog() != DialogResult.OK) return;

			SetKeyAlgoDialog kad = new SetKeyAlgoDialog();

			if(kad.ShowDialog(this)!=DialogResult.OK) return;

			k=kad.GetKey();
			a=kad.GetAlgo();

			dirName=o.DirName;

			this.Cursor=Cursors.WaitCursor;
			AddDir();
			this.Cursor=Cursors.Default;
			bModified=true;
			k=null;
			fileNames=null;
			AddItems();
		}

		private void menuItemAddDir_Click(object sender, System.EventArgs e)
		{
			muActionAddDirectory_Click(sender,e);
		}

		private void OnKeyPressed(object sender, KeyPressEventArgs e)
		{
			if(e.KeyChar==(char)13)
			 OnDoubleClick(sender,e);
		}

		private void muFileInformation_Click(object sender, System.EventArgs e)
		{
			if(fn!="Untitled")
			{
				FileInformationDialog d=new FileInformationDialog(fn,File.GetDateTime().ToString(),File.NoItems,!File.IsDeld(0),File.ItemCount-File.NoItems-(File.IsDeld(0)?1:0));
				d.ShowDialog();
			}
			else
			{
				FileInformationDialog d=new FileInformationDialog(fn,DateTime.Now.ToString(),File.NoItems,!File.IsDeld(0),File.ItemCount-File.NoItems-(File.IsDeld(0)?1:0));
				d.ShowDialog();
			}
		}


		void OnDragEnter(object sender, DragEventArgs e)
		{
 		 if(e.Data.GetDataPresent(DataFormats.FileDrop))
			e.Effect = DragDropEffects.Copy;
		}
		
		void OnDragDrop(object sender, DragEventArgs e)
		{
			if(!e.Data.GetDataPresent(DataFormats.FileDrop)) return;

			this.Activate();
			SetKeyAlgoDialog kad = new SetKeyAlgoDialog();

			if(kad.ShowDialog(this)!=DialogResult.OK) return;

			k=kad.GetKey();
			a=kad.GetAlgo();

			string [] s=(string [])e.Data.GetData(DataFormats.FileDrop);
			foreach(string fn in s)
			{
				if(!System.IO.File.Exists(fn) &&
                    System.IO.Directory.Exists(fn))
					{
						dirName=fn;
						AddDir();
					}
			}
			fileNames=new StringCollection();

			foreach(string fn in s)
			{
				if(System.IO.File.Exists(fn))
				{
				 fileNames.Add(fn);
				}
			}
			this.Cursor=Cursors.WaitCursor;
			AddFiles();
			this.Cursor=Cursors.Default;
			fileNames=null;
			k=null;
			bModified=true;
			AddItems();
		}
		
		void OnItemUpdated(Item it)
		{
			if(AppOptions.NotifyUpdate)
			{
				string txt=it.GetFullName();
				MessageBox.Show(txt,"Item Updated",MessageBoxButtons.OK,MessageBoxIcon.Information);
			}
			if(it.Descr==curPath)
				AddItems();
			bModified=true;
		}

		void OnColumnClick(object sender, ColumnClickEventArgs e)
		{
			switch(e.Column)
			{
				case 0:
					;
					break;
				case 2:
					muActionSetAlgoSelection_Click(sender, new EventArgs());
					break;
			}
		}

		void OnMouseM(object sender, MouseEventArgs e)
		{
			if(e.Button==MouseButtons.Left)
			{
				ListViewItem lit=listViewFisiere.GetItemAt(e.X,e.Y);
				if(lit==null) return;
				long jj=(long)lit.Tag;
				if(jj<=0) return;
				Item it = File.GetItem(jj);

				if(it.GetDataType()==Item.DataType.Encrypted) 
					if(it.ResLength==0)
						if(!DecryptSelection())
						{
							statusBar.Text="Error occurred while decrypting";
							return;
						}
				string fn=Path.GetTempPath();
				fn+="\\"+it.Name;
				FileStream S=null;
				try
				{
					S=new FileStream(fn,FileMode.Create,FileAccess.Write);
					it.WriteTo(S,Item.DataType.Decrypted);
					S.Close();
				}
				catch(Exception E)
				{
					if(S!=null) S.Close();
					ShowError(E,"writting temp file");
					return;
				}
				listViewFisiere.DoDragDrop(fn,DragDropEffects.Copy);
				ExecuteFileClass.FileDelete(fn);
				jj=0;
			}
		}

		private void toolBar_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			if(e.Button==toolBarButtonNew) muFileNew_Click(sender,e);
			else 
			if(e.Button==toolBarButtonSave) muFileSave_Click(sender,e);
			else
			if(e.Button==toolBarButtonOpen) muFileOpen_Click(sender,e);
			else
			if(e.Button==toolBarButtonAdd) muActionAddFiles_Click(sender,e);
			else
			if(e.Button==toolBarButtonAddDir) muActionAddDirectory_Click(sender,e);
			else
			if(e.Button==toolBarButtonExtract) muActionExtractSelectionTo_Click(sender,e);
			else
			if(e.Button==toolBarButtonView) muActionView_Click(sender,e);
			else
            if(e.Button==toolBarButtonComment) muActionAddComment_Click(sender,e);
		}

		private void menuOptionsPreferences_Click(object sender, System.EventArgs e)
		{
			PreferencesDialog er = new PreferencesDialog();
			bool b=AppOptions.DrawIcons;
			if(er.ShowDialog()==DialogResult.OK)
			{
				listViewFisiere.GridLines=AppOptions.ShowGridLines;
				if(b==AppOptions.DrawIcons)
				 listViewFisiere.Refresh();
				else AddItems();
			}
		}

		private void OnClosing(object sender, CancelEventArgs e)
		{
		 muFileClose_Click(sender, e);
		}

		private void menuOptionsDirectories_Click(object sender, System.EventArgs e)
		{
		 DirectoriesDialog d = new DirectoriesDialog();
         d.ShowDialog();		
		}

		private void menuViewDetails_Click(object sender, System.EventArgs e)
		{
			menuViewDetails.Checked=true;
			menuViewLarge.Checked=false;
			menuViewSmall.Checked=false;
			menuViewList.Checked=false;

			listViewFisiere.View=View.Details;
		}

		private void menuViewSmall_Click(object sender, System.EventArgs e)
		{
			menuViewDetails.Checked=false;
			menuViewLarge.Checked=false;
			menuViewSmall.Checked=true;
			menuViewList.Checked=false;

			listViewFisiere.View=View.SmallIcon;
		}

		private void menuViewLarge_Click(object sender, System.EventArgs e)
		{
			menuViewDetails.Checked=false;
			menuViewLarge.Checked=true;
			menuViewSmall.Checked=false;
			menuViewList.Checked=false;

			listViewFisiere.View=View.LargeIcon;
		}

		private void menuViewList_Click(object sender, System.EventArgs e)
		{
			menuViewDetails.Checked=false;
			menuViewLarge.Checked=false;
			menuViewSmall.Checked=false;
			menuViewList.Checked=true;

			listViewFisiere.View=View.List;
		}

		private void App_Load(object sender, System.EventArgs e)
		{
				try
				{
					AppOptions.Load();
					if(AppOptions.AppPath=="%TARGETDIR%")
					{
						AppOptions.AppPath=Application.StartupPath;
						AppOptions.Save();
					}
				}
				catch(Exception E) {ShowError(E,"loading settings");}

				try
				{
					toolBar.ImageList=new ImageList();
					toolBar.ImageList.ImageSize=new System.Drawing.Size(30,30);
					toolBar.ImageList.Images.Add(Image.FromFile(AppOptions.AppPath+"\\Images\\New.png"));
					toolBar.ImageList.Images.Add(Image.FromFile(AppOptions.AppPath+"\\Images\\Open.png"));
					toolBar.ImageList.Images.Add(Image.FromFile(AppOptions.AppPath+"\\Images\\Save.png"));
					toolBar.ImageList.Images.Add(Image.FromFile(AppOptions.AppPath+"\\Images\\Add.png"));
					toolBar.ImageList.Images.Add(Image.FromFile(AppOptions.AppPath+"\\Images\\AddDir.png"));
					toolBar.ImageList.Images.Add(Image.FromFile(AppOptions.AppPath+"\\Images\\Extract.png"));
					toolBar.ImageList.Images.Add(Image.FromFile(AppOptions.AppPath+"\\Images\\View.png"));
					toolBar.ImageList.Images.Add(Image.FromFile(AppOptions.AppPath+"\\Images\\Comment.png"));
					toolBarButtonNew.ImageIndex=0;			  
				}
				catch(Exception E) {ShowError(E,"drawing icons");}
				listViewFisiere.GridLines=AppOptions.ShowGridLines;
				WinText();
		}

		private void muHelpAbout_Click(object sender, System.EventArgs e)
		{
			AboutDialog d=new AboutDialog();
			d.ShowDialog();
		}

		private void Abou2(object sender, System.EventArgs e)
		{
			AbouDialog d=new AbouDialog();
			d.ShowDialog();
		}

		private void muHelpContents_Click(object sender, System.EventArgs e)
		{
			try
			{
				Process.Start(new ProcessStartInfo(AppOptions.AppPath+"\\Users Guide (RO).doc"));
			}
			catch(Exception E)
			{
				ShowError(E,"loading users guide");
			}
		}

	}
}
