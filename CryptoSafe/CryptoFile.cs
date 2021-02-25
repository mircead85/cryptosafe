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

namespace CryptoSafe
{
	/// <summary>
	/// Clasa support fisier criptat
	/// </summary>
	class CryptoFile:IEnumerable, IEnumerator
	{
		const int MAX_FILES_IN_CHUNK=1000;

		protected Item [] items;
		bool [] deld;
		protected long noItems;
		protected long noDeldItems;
		protected DateTime modified;

		private int EnumPos=0;

		public DateTime GetDateTime()
		{
			return modified;
		}


		public object Current
		{
			get
			{
				if(EnumPos<=0 || EnumPos>=noItems) throw new InvalidOperationException();
				return items[EnumPos];
			}
		}

		public bool MoveNext()
		{
			while(deld[EnumPos] && EnumPos<noItems) EnumPos++;
			if(EnumPos<noItems) return true;
			else return false;
		}

		public void Reset()
		{
			EnumPos=0;
		}

		public IEnumerator GetEnumerator()
		{
			return this;
		}

		public void Clear()
		{
			for(int i=1;i<noItems;i++)
			{
				items[i]=null;
			}
			noItems=1;
		}

		public Item GetItem(long i) 
		{
			if(i<0 || i>=noItems) throw new IndexOutOfRangeException("Item does not exist");
			return items[i];
		}

		public long ItemCount 
		{
			get
			{
				return noItems;
			}
		}
		
		public CryptoFile()
		{
			toExtract = new bool[MAX_FILES_IN_CHUNK];
			items = new Item[MAX_FILES_IN_CHUNK];
			deld = new bool[MAX_FILES_IN_CHUNK];
			items[0]=null;
			deld[0]=true;
			noItems=1;
			noDeldItems=1;
		}

		public CryptoFile(Stream S)
		{
			toExtract = new bool[MAX_FILES_IN_CHUNK];
			items = new Item[MAX_FILES_IN_CHUNK];
			deld = new bool[MAX_FILES_IN_CHUNK];
			ReadFrom(S);
		}

		public void ReadFrom(Stream S)
		{
			ReadFrom(S,new OpenFileOptions());
		}

		protected void ReadFrom(Stream S, OpenFileOptions ofo)
		{
			BinaryReader sr=new BinaryReader(S,Encoding.ASCII);
			Clear();
			try
			{
				byte [] bb=new byte[5];
				if(sr.Read(bb,0,5)<5 || bb[0]!=4) throw new FileCorruptException("Unrecognized file format");
				string sg=Encoding.ASCII.GetString(bb,1,4);
				if(sg!="CSF-") throw new FileCorruptException("Unrecognized file format");
				noItems = sr.ReadInt64();
				noDeldItems=0;
				if(noItems>MAX_FILES_IN_CHUNK) throw new FileCorruptException("File has been corrupted");
				try 
				{
				 modified = DateTime.Parse(sr.ReadString());
				}
				catch (Exception E)
				{
				 modified = DateTime.Now;
				 if(E is System.FormatException) E=new GenericException("Local date and time settings differ from ones used when creating file. Date and time will not be available.",GenericException.Types.Warrning);
				 App.ShowError(E,"reading date and time");
				}
			}
			catch(Exception E)
			{
				ofo.Done();
				throw E;
			}
			for(int i=0;i<noItems;i++)
			{
				while(true)
				{
					try
					{
						items[i]=new Item(S,Item.DataType.Encrypted);
						deld[i]=false;
						if(i==0)
							if(items[i].Name!="%$Comment/$%") //Does not contain a comment file
							{
								items[1]=items[0];
								items[0]=null;
								DelItem(0);
								noItems++;
								i++;
							}
							else
							{
								items[0].key="CSFComment";
							}
						break;
					}
					catch(Exception E)
					{
						if(E is ThreadInterruptedException || E is ThreadAbortException)
						{Clear();ofo.Done();return;}

						if(ofo.HaltOnFail)
						{
							FileOptions.Result j=ofo.OnException(E,(S is FileStream)?((FileStream)S).Name:"reading file",true);
							if(j==FileOptions.Result.Retry) continue;
							if(j==FileOptions.Result.Abort) 
							{
								Clear();
								ofo.Done();
								return;
							}
							if(j==FileOptions.Result.Ignore) 
							{
								noItems=i;
								break;
							}
						}
						else break;
					}
				}
				ofo.SetProgress((int)(100*S.Position/S.Length));
			}
			ofo.Done();
		}


		public long NoItems
		{
			get
			{
				return noItems-noDeldItems;
			}
		}

		public int WriteTo(Stream S)
		{
			return WriteTo(S,new SaveFileOptions());
		}

		protected int WriteTo(Stream S, SaveFileOptions oso)
		{
			BinaryWriter sw=new BinaryWriter(S,Encoding.ASCII);
			sw.Write("CSF-");
			sw.Write(NoItems);
			sw.Write(DateTime.Now.ToString());
			for(int i=0;i<noItems;i++) 
				if(!deld[i])
					   {
						   while(true)
							   {
								   try
								   {
								   items[i].WriteTo(S,Item.DataType.Encrypted);
								   break;
								   }
								   catch(Exception E)
								   {
									   if(E is ThreadInterruptedException || E is	ThreadAbortException)
									   {oso.Done();return -1;}
										   if(oso.HaltOnFail)
									   {
										   FileOptions.Result j=oso.OnException(E,items[i].GetFullName(),false);
											   if(j==FileOptions.Result.Retry) 
											   {
												   string s=null;
												   if((s=oso.NewKey)!=null)
												    items[i].key=s;
												   continue;
											   }
										   if(j==FileOptions.Result.Abort) 
										   {
											   oso.Done();
											   return -1;
										   }
										   if(j==FileOptions.Result.Ignore) break;
									   }
									   else break;
								   }
							   }
						   oso.SetProgress((int)(i*100/noItems));
					   }
			oso.Done();
			return 0;
		}


		public void SetKey(string key)
		{
			for(int i=0;i<noItems;i++) if(!deld[i]) 
										   if(items[i].GetDataType()==Item.DataType.Decrypted)
										   {
											   items[i].key=key;
											   items[i].ResLength=0;
										   }
		}

		public void SetDecryptKey(string key)
		{
			for(int i=0;i<noItems;i++) if(!deld[i]) 
										   if(items[i].GetDataType()==Item.DataType.Encrypted)
										   {
											   items[i].key=key;
											   items[i].ResLength=0;
										   }
		}

		public void SetAlgo(Item.AlgoTypes a)
		{
			for(int i=0;i<noItems;i++) if(!deld[i]) 
										   if(items[i].GetDataType()==Item.DataType.Decrypted)
										   {
											   items[i].algo=a;
											   items[i].ResLength=0;
										   }
		}

		
		public void DelItem(long ind)
		{
			if(ind<0 || ind>=noItems) throw new IndexOutOfRangeException("Item does not exist");
			if(!deld[ind]) noDeldItems++;
			deld[ind]=true;
		}
		public void UnDel(long ind)
		{
			if(ind<0 || ind>=noItems) throw new IndexOutOfRangeException("Item does not exist");
			if(deld[ind]) noDeldItems--;
			deld[ind]=false;
		}
		public bool IsDeld(long ind)
		{
			if(ind<0 || ind>=noItems) throw new IndexOutOfRangeException("Item does not exist");
			return deld[ind];
		}


		public void SetItemAt(Item i, long ind)
		{
			if(ind<0 || ind>=noItems) throw new IndexOutOfRangeException("Item does not exist");
			items[ind]=i;
		}

		public long AddItem(Stream S, Item.AlgoTypes A)
		{
			if(noItems>=MAX_FILES_IN_CHUNK-1) throw new ToManyItemsException("Too many items processed in group");
			items[noItems] = new Item(S,Item.DataType.Decrypted);
			items[noItems].algo=A;
			deld[noItems]=false;
			noItems++;
			return noItems-1;
		}

		public int Extract()
		{
		  return Extract(new ExtractOptions());
		}

		protected bool [] toExtract;

		public void SetToExtract(long i)
		{
		 if(i<0 || i>=noItems) throw new IndexOutOfRangeException("Item does not exist");
		 toExtract[i]=true;
		}

		public void ClearToExtract(long i)
		{
			if(i<0 || i>=noItems) throw new IndexOutOfRangeException("Item does not exist");
			toExtract[i]=false;
		}

		public void SelectAll()
		{
			for(int i=1;i<noItems;i++) 
				toExtract[i]=true;
		}

		public void DeselectAll()
		{
			for(int i=1;i<noItems;i++) 
				toExtract[i]=false;
		}

		protected int Extract(ExtractOptions eo)
		{
			for(int i=0;i<noItems;i++) 
				if(!deld[i]) if(toExtract[i])
				{
					string s=ExtractOptions.to;
								 if(ExtractOptions.usePaths) 
									 if(items[i].Descr.Length>0) 
												 {
													 if(s.Length>0) s+="\\";
													 s+=items[i].Descr;
													 if(!System.IO.Directory.Exists(s))
													 System.IO.Directory.CreateDirectory(s);
												 }
					if(s.Length>0) s+="\\";
					s+=items[i].Name;
					FileStream fs=null;
					while(true)
						try
						{
							fs = File.OpenWrite(s);
							items[i].WriteTo(fs,Item.DataType.Decrypted);
							fs.Close();
							break;
						}
						catch(Exception E)
						{
							if(fs!=null) fs.Close();
							if(E is ThreadInterruptedException || E is ThreadAbortException)
							{Clear();eo.Done();return -1;}

							if(eo.HaltOnFail)
							{
								FileOptions.Result j=eo.OnException(E,items[i].GetFullName(),true);
								if(j==FileOptions.Result.Abort) 
								{eo.Done();return -1;}
								if(j==FileOptions.Result.Retry) 
								{
									string s2=null;
									if((s2=eo.NewKey)!=null)
									 items[i].key=s2;
									continue;
								}
								if(j==FileOptions.Result.Ignore) break;
							}
							else break;
						}
					eo.SetProgress((int)(100*i/noItems));
				}
			eo.Done();
			return 0;
		}
	}
	
}
