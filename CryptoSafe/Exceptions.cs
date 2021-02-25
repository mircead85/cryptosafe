using System;

namespace CryptoSafe
{
	public class GenericException:Exception
	{
		public enum Types {Error, Warrning};
		protected Types type;

		public Types Type{get{return type;}}

		public string Descr;


		protected GenericException() {}
		public GenericException(string s){Descr=s;type=Types.Error;}
		public GenericException(string s, Types t) {Descr=s;type=t;}

		public override string ToString()
		{
			return Descr+"; "+base.ToString();
		}
	}

	public class FileCorruptException:GenericException
	{
		public FileCorruptException(string s){Descr=s;type=Types.Error;}
		public FileCorruptException(string s, Types t) {Descr=s;type=t;}
	}
	
	public class CRCException:GenericException
	{
		public CRCException(string s){Descr=s;type=Types.Error;}
		public CRCException(string s, Types t) {Descr=s;type=t;}
	}

	public class ToManyItemsException:GenericException
	{
		public ToManyItemsException(string s){Descr=s;type=Types.Error;}
		public ToManyItemsException(string s, Types t) {Descr=s;type=t;}
	}
}
