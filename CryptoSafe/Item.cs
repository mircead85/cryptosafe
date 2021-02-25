using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CryptoSafe
{
	/// <summary>
	/// Clasa support pt. item criptat
	/// </summary>
	public class Item:object
	{
		private RSACryptoServiceProvider rsa;
		private RC2CryptoServiceProvider rc2;
		private DESCryptoServiceProvider des;
		private TripleDESCryptoServiceProvider tdes;
		private SymmetricAlgorithm rijn;

		public enum AlgoTypes{RIJN, DES, TDES, RC2, RSA};
		public enum DataType{Encrypted, Decrypted};

		public string Name;
		public string Descr;//Path(relativ)

		public string GetFullName()
		{
			string s="\""+Name+"\"";
			if(Descr!=null) if(Descr.Length>0)
				s+=" in \\"+Descr;
			return s;
		}

		protected AlgoTypes Algo;

		public AlgoTypes algo
		{
			get{return Algo;}
			set
			{
				if(datatype==DataType.Decrypted) 
				{
					Algo=value;
				}
				else throw new GenericException("Cannot change algorithm for already encrypted data");
			}
		}
		protected DataType datatype;

		public DataType GetDataType() {return datatype;}

		public MemoryStream data;
		public MemoryStream res;
		long CRC_data;
		long CRC_data_read;
		long CRC_res;
		long CRC_res_read;

		public bool CRC_data_OK {get{return CRC_data==CRC_data_read;}}
		public bool CRC_res_OK {get{return CRC_res==CRC_res_read;}}

		protected byte [] IV;
		protected int IVseed;
		protected byte [] Key;

		public override string ToString()
		{
			if(datatype==DataType.Encrypted)
			{
				if(res.Length==0)
				{
					Decrypt();
				}
			  return Encoding.ASCII.GetString(res.ToArray());
			}
		   return Encoding.ASCII.GetString(data.ToArray());
		}

		bool bKeySet;

		public bool IsKeySet() {return bKeySet;}

		public string key 
		{
			set 
			{
				int len;
				KeySizes ks;
				switch(Algo)
				{
					case AlgoTypes.DES:
						ks=des.LegalKeySizes[0];
						break;
					case AlgoTypes.RC2:
						ks=rc2.LegalKeySizes[0];
						break;
					case AlgoTypes.RIJN:
						ks=rijn.LegalKeySizes[0];
						break;
					case AlgoTypes.TDES:
						ks=tdes.LegalKeySizes[0];
						break;
					default:
						throw(new Exception("Could not generate key: Unkown Algorithm Requested"));
				}
				len=ks.MinSize;
				while(len<value.Length*8 && len<ks.MaxSize) len+=ks.SkipSize;
				Key = new byte[len/8];
				for(int i=0;i<len/8;i++)
					Key[i]=(byte)value[i%value.Length];
				bKeySet=true;
			}
		}

		protected void GenIV(int seed)
		{
			if(seed==0)
			{
				seed = (int)DateTime.Now.Ticks*10245585;
				IVseed=seed;
			}
			Random R=new Random(seed);
			switch(Algo)
			{
				case AlgoTypes.DES:
					des.GenerateIV();
					IV=new byte[des.IV.Length];
					break;
				case AlgoTypes.RC2:
					rc2.GenerateIV();
					IV=new byte[rc2.IV.Length];
					break;
				case AlgoTypes.RIJN:
					rijn.GenerateIV();
					IV=new byte[rijn.IV.Length];
					break;
				case AlgoTypes.TDES:
					tdes.GenerateIV();
					IV=new byte[tdes.IV.Length];
					break;
				default:
					throw(new Exception("Could not generate IV: Unkown Algorithm Requested"));
			}
			R.NextBytes(IV);
		}

		public long DataLength
		{
			get{return data.Length;}
			set{data.SetLength(value);}
		}

		public long ResLength 
		{
			get{return res.Length;}
			set{res.SetLength(value);}
		}

		public Item()
		{
			Algo=AlgoTypes.TDES;
			Name="";
			Descr="";
			bKeySet=false;
			data = new MemoryStream();
			res = new MemoryStream();
			rsa = new RSACryptoServiceProvider();
			rc2=new RC2CryptoServiceProvider();
			des=new DESCryptoServiceProvider();
			tdes=new TripleDESCryptoServiceProvider();
			rijn = SymmetricAlgorithm.Create(); //Creates the default implementation, which is RijndaelManaged.

		}
		public Item(AlgoTypes a)
		{
			Algo=a;
			Name="";
			Descr="";
			bKeySet=false;
			data = new MemoryStream();
			res = new MemoryStream();
			rsa = new RSACryptoServiceProvider();
			rc2=new RC2CryptoServiceProvider();
			des=new DESCryptoServiceProvider();
			tdes=new TripleDESCryptoServiceProvider();
			rijn = SymmetricAlgorithm.Create(); //Creates the default implementation, which is RijndaelManaged.
		}

		public Item(AlgoTypes s, string name, string descr)
		{
			Algo=s;
			Name=name;
			Descr=descr;
			bKeySet=false;
			data = new MemoryStream();
			res = new MemoryStream();
			rsa = new RSACryptoServiceProvider();
			rc2=new RC2CryptoServiceProvider();
			des=new DESCryptoServiceProvider();
			tdes=new TripleDESCryptoServiceProvider();
			rijn = SymmetricAlgorithm.Create(); //Creates the default implementation, which is RijndaelManaged.
		}

		public Item(AlgoTypes s, string name)
		{
			Algo=s;
			Name=name;
			Descr="";
			bKeySet=false;
			data = new MemoryStream();
			res = new MemoryStream();
			rsa = new RSACryptoServiceProvider();
			rc2=new RC2CryptoServiceProvider();
			des=new DESCryptoServiceProvider();
			tdes=new TripleDESCryptoServiceProvider();
			rijn = SymmetricAlgorithm.Create(); //Creates the default implementation, which is RijndaelManaged.
		}
		public Item(Stream s, DataType t)
		{
			Name="";
			Descr="";
			bKeySet=false;
			data = new MemoryStream();
			res = new MemoryStream();
			rsa = new RSACryptoServiceProvider();
			rc2=new RC2CryptoServiceProvider();
			des=new DESCryptoServiceProvider();
			tdes=new TripleDESCryptoServiceProvider();
			rijn = SymmetricAlgorithm.Create(); //Creates the default implementation, which is RijndaelManaged.
			ReadFrom(s,t);
		}
		public void Encrypt()
		{    
			if(!bKeySet) throw new CRCException("No key set");
			GenIV(0);
			res.SetLength(0);
       
			byte[] bin = new byte[50010];
			long rdlen = 0;              
			long totlen = data.Length;    
			int len;                     
 
			data.Seek(0,SeekOrigin.Begin);
			res.Seek(0,SeekOrigin.Begin);

			CryptoStream encStream=null;
			switch(Algo)
			{
				case AlgoTypes.RIJN:
					encStream = new CryptoStream(res, rijn.CreateEncryptor(Key, IV), CryptoStreamMode.Write);
					break;
				case AlgoTypes.DES:
					encStream = new CryptoStream(res, des.CreateEncryptor(Key, IV), CryptoStreamMode.Write);
					break;
				case AlgoTypes.TDES:
					encStream = new CryptoStream(res, tdes.CreateEncryptor(Key, IV), CryptoStreamMode.Write);
					break;
				case AlgoTypes.RC2:
					encStream = new CryptoStream(res, rc2.CreateEncryptor(Key, IV), CryptoStreamMode.Write);
					break;
				default:
					throw(new Exception("Unknown Algorithm Used"));
			}
             
			try
			{
				CRC_data=0;
				while(rdlen < totlen)
				{
					len = data.Read(bin, 0, 50000);
					for(int i=0;i<len;i++) {CRC_data*=7;CRC_data+=bin[i];}
					encStream.Write(bin, 0, len);
					rdlen = rdlen + len;
				}
				encStream.FlushFinalBlock();
			}
			catch(Exception E)
			{
				res.SetLength(0);
				throw E;
			}
			res.Seek(0,SeekOrigin.Begin);
			int b;
			CRC_res=0;
			while ((b=res.ReadByte())!=-1) {CRC_res*=7;CRC_res+=b;}
			res.Seek(0,SeekOrigin.Begin);
			CRC_res_read=CRC_res;
		}

		public void Decrypt()
		{    
			if(!bKeySet) throw new CRCException("No key set");
			res.SetLength(0);
       
			GenIV(IVseed);

			byte[] bin = new byte[50010];
			long rdlen = 0;              
			long totlen = data.Length;    
			int len;                     
 
			data.Seek(0,SeekOrigin.Begin);
			res.Seek(0,SeekOrigin.Begin);

			CryptoStream encStream;
			switch(Algo)
			{
				case AlgoTypes.RIJN:
					encStream = new CryptoStream(res, rijn.CreateDecryptor(Key, IV), CryptoStreamMode.Write);
					break;
				case AlgoTypes.DES:
					encStream = new CryptoStream(res, des.CreateDecryptor(Key, IV), CryptoStreamMode.Write);
					break;
				case AlgoTypes.TDES:
					encStream = new CryptoStream(res, tdes.CreateDecryptor(Key, IV), CryptoStreamMode.Write);
					break;
				case AlgoTypes.RC2:
					encStream = new CryptoStream(res, rc2.CreateDecryptor(Key, IV), CryptoStreamMode.Write);
					break;
				default:
					throw(new GenericException("Unknown Algorithm Used"));
			}
             
			try
			{
				CRC_data=0;
				while(rdlen < totlen)
				{
					len = data.Read(bin, 0, 50000);
					for(int i=0;i<len;i++) {CRC_data*=7;CRC_data+=bin[i];}
					encStream. Write(bin, 0, len);
					rdlen = rdlen + len;
				}
				encStream.FlushFinalBlock();
				res.Seek(0,SeekOrigin.Begin);
			}
			catch(Exception E)
			{
				res.SetLength(0);
				if(E is CryptographicException)
					if(CRC_data_OK)
					 throw new CRCException("Invalid key");
					else
					 throw new FileCorruptException("File has been corupted - CRC Fail");
				else throw E;
			}

			int b;
			CRC_res=0;
			while ((b=res.ReadByte())!=-1) {
				CRC_res*=7;
				CRC_res+=b;}
			res.Seek(0,SeekOrigin.Begin);
			if(CRC_data!=CRC_data_read) 
			{
				res.SetLength(0);
				throw new FileCorruptException("File has been corupted - CRC Fail");
			}
			if(CRC_res!=CRC_res_read) 
			{
				res.SetLength(0);
				throw new CRCException("Invalid key");
			}
			
		}

		public void WriteTo(Stream S, DataType what)
		{
			switch(datatype)
			{
				case DataType.Encrypted:
					if(what==DataType.Decrypted)
					{
						if(res.Length==0) 
							Decrypt();
						res.WriteTo(S);
					}
					else
					{
						//if(IV[0]==0)
						//	throw new GenericException("IV information lost");
						BinaryWriter wt=new BinaryWriter(S,Encoding.ASCII);
						wt.Write(CRC_data_read);
						wt.Write(CRC_res_read);
						wt.Write(Name);
						wt.Write(Descr);
						wt.Write((byte)Algo);
						wt.Write(IVseed);
						//wt.Write(IV.Length);
						//S.Write(IV,0,IV.Length);
						wt.Write(data.Length); 
						data.WriteTo(S);
					}
					break;

				case DataType.Decrypted:
					if(what==DataType.Encrypted)
					{
						if(res.Length==0) Encrypt();
						BinaryWriter wt=new BinaryWriter(S,Encoding.ASCII);
						wt.Write(CRC_res);
						wt.Write(CRC_data);
						wt.Write(Name);
						wt.Write(Descr);
						wt.Write((byte)Algo);
						wt.Write(IVseed);
						//wt.Write(IV.Length);
						//S.Write(IV,0,IV.Length);
						wt.Write(res.Length); 
						res.WriteTo(S);
					}
					else
					{
						data.WriteTo(S);
					}
					break;
			}
		}

		public void ReadFrom(Stream S, DataType what)
		{
			datatype=what;
			byte[] bin = new byte[50010];
			long rdlen = 0;              
			long totlen = S.Length;    
			int len;
			data.SetLength(0);
			res.SetLength(0);
			switch(datatype)
			{
				case DataType.Decrypted:
					CRC_data=0;
					S.Seek(0,SeekOrigin.Begin);
					while(rdlen < totlen)
					{
						len = S.Read(bin, 0, 50000);
						for(int i=0;i<len;i++) {CRC_data*=7;CRC_data+=bin[i];}
						data.Write(bin, 0, len);
						rdlen = rdlen + len;
						if(rdlen < totlen && len==0) 
							throw new IOException("Unexpected end of stream");
					}
					CRC_data_read=CRC_data;
					data.Seek(0,SeekOrigin.Begin);
					break;
				case DataType.Encrypted:
					BinaryReader wt = new BinaryReader(S,Encoding.ASCII);
					CRC_data_read=wt.ReadInt64();
					CRC_res_read=wt.ReadInt64();
					Name=wt.ReadString();
					Descr=wt.ReadString();
					Algo=(AlgoTypes)wt.ReadByte();
					IVseed=wt.ReadInt32();
					//IV=new byte[totlen];
					//S.Read(IV,0,(int)totlen);
					totlen=wt.ReadInt64(); 
					CRC_data=0;
					while(rdlen < totlen)
					{
						len = S.Read(bin, 0, (50000<(totlen-rdlen))?50000:(int)(totlen-rdlen));
						for(int i=0;i<len;i++) {CRC_data*=7;CRC_data+=bin[i];}
						data.Write(bin, 0, len);
						rdlen = rdlen + len;
						if(rdlen < totlen && len==0) 
							throw new IOException("Unexpected end of stream");
					}
					data.Seek(0,SeekOrigin.Begin);
					break;
			}
		}
	}
}
