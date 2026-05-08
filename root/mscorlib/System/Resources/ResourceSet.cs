using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Resources
{
	// Token: 0x0200083E RID: 2110
	[ComVisible(true)]
	[Serializable]
	public class ResourceSet : IDisposable, IEnumerable
	{
		// Token: 0x06004777 RID: 18295 RVA: 0x000EBA89 File Offset: 0x000E9C89
		protected ResourceSet()
		{
			this.CommonInit();
		}

		// Token: 0x06004778 RID: 18296 RVA: 0x000025BE File Offset: 0x000007BE
		internal ResourceSet(bool junk)
		{
		}

		// Token: 0x06004779 RID: 18297 RVA: 0x000EBA97 File Offset: 0x000E9C97
		public ResourceSet(string fileName)
		{
			this.Reader = new ResourceReader(fileName);
			this.CommonInit();
			this.ReadResources();
		}

		// Token: 0x0600477A RID: 18298 RVA: 0x000EBAB7 File Offset: 0x000E9CB7
		[SecurityCritical]
		public ResourceSet(Stream stream)
		{
			this.Reader = new ResourceReader(stream);
			this.CommonInit();
			this.ReadResources();
		}

		// Token: 0x0600477B RID: 18299 RVA: 0x000EBAD7 File Offset: 0x000E9CD7
		public ResourceSet(IResourceReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this.Reader = reader;
			this.CommonInit();
			this.ReadResources();
		}

		// Token: 0x0600477C RID: 18300 RVA: 0x000EBB00 File Offset: 0x000E9D00
		private void CommonInit()
		{
			this.Table = new Hashtable();
		}

		// Token: 0x0600477D RID: 18301 RVA: 0x000EBB0D File Offset: 0x000E9D0D
		public virtual void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x0600477E RID: 18302 RVA: 0x000EBB18 File Offset: 0x000E9D18
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				IResourceReader reader = this.Reader;
				this.Reader = null;
				if (reader != null)
				{
					reader.Close();
				}
			}
			this.Reader = null;
			this._caseInsensitiveTable = null;
			this.Table = null;
		}

		// Token: 0x0600477F RID: 18303 RVA: 0x000EBB0D File Offset: 0x000E9D0D
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06004780 RID: 18304 RVA: 0x000EBB54 File Offset: 0x000E9D54
		public virtual Type GetDefaultReader()
		{
			return typeof(ResourceReader);
		}

		// Token: 0x06004781 RID: 18305 RVA: 0x000EBB60 File Offset: 0x000E9D60
		public virtual Type GetDefaultWriter()
		{
			return typeof(ResourceWriter);
		}

		// Token: 0x06004782 RID: 18306 RVA: 0x000EBB6C File Offset: 0x000E9D6C
		[ComVisible(false)]
		public virtual IDictionaryEnumerator GetEnumerator()
		{
			return this.GetEnumeratorHelper();
		}

		// Token: 0x06004783 RID: 18307 RVA: 0x000EBB6C File Offset: 0x000E9D6C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumeratorHelper();
		}

		// Token: 0x06004784 RID: 18308 RVA: 0x000EBB74 File Offset: 0x000E9D74
		private IDictionaryEnumerator GetEnumeratorHelper()
		{
			Hashtable table = this.Table;
			if (table == null)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("Cannot access a closed resource set."));
			}
			return table.GetEnumerator();
		}

		// Token: 0x06004785 RID: 18309 RVA: 0x000EBB98 File Offset: 0x000E9D98
		public virtual string GetString(string name)
		{
			object objectInternal = this.GetObjectInternal(name);
			string text;
			try
			{
				text = (string)objectInternal;
			}
			catch (InvalidCastException)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Resource '{0}' was not a String - call GetObject instead.", new object[] { name }));
			}
			return text;
		}

		// Token: 0x06004786 RID: 18310 RVA: 0x000EBBE4 File Offset: 0x000E9DE4
		public virtual string GetString(string name, bool ignoreCase)
		{
			object obj = this.GetObjectInternal(name);
			string text;
			try
			{
				text = (string)obj;
			}
			catch (InvalidCastException)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Resource '{0}' was not a String - call GetObject instead.", new object[] { name }));
			}
			if (text != null || !ignoreCase)
			{
				return text;
			}
			obj = this.GetCaseInsensitiveObjectInternal(name);
			string text2;
			try
			{
				text2 = (string)obj;
			}
			catch (InvalidCastException)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Resource '{0}' was not a String - call GetObject instead.", new object[] { name }));
			}
			return text2;
		}

		// Token: 0x06004787 RID: 18311 RVA: 0x000EBC70 File Offset: 0x000E9E70
		public virtual object GetObject(string name)
		{
			return this.GetObjectInternal(name);
		}

		// Token: 0x06004788 RID: 18312 RVA: 0x000EBC7C File Offset: 0x000E9E7C
		public virtual object GetObject(string name, bool ignoreCase)
		{
			object objectInternal = this.GetObjectInternal(name);
			if (objectInternal != null || !ignoreCase)
			{
				return objectInternal;
			}
			return this.GetCaseInsensitiveObjectInternal(name);
		}

		// Token: 0x06004789 RID: 18313 RVA: 0x000EBCA0 File Offset: 0x000E9EA0
		protected virtual void ReadResources()
		{
			IDictionaryEnumerator enumerator = this.Reader.GetEnumerator();
			while (enumerator.MoveNext())
			{
				object value = enumerator.Value;
				this.Table.Add(enumerator.Key, value);
			}
		}

		// Token: 0x0600478A RID: 18314 RVA: 0x000EBCDC File Offset: 0x000E9EDC
		private object GetObjectInternal(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			Hashtable table = this.Table;
			if (table == null)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("Cannot access a closed resource set."));
			}
			return table[name];
		}

		// Token: 0x0600478B RID: 18315 RVA: 0x000EBD0C File Offset: 0x000E9F0C
		private object GetCaseInsensitiveObjectInternal(string name)
		{
			Hashtable table = this.Table;
			if (table == null)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("Cannot access a closed resource set."));
			}
			Hashtable hashtable = this._caseInsensitiveTable;
			if (hashtable == null)
			{
				hashtable = new Hashtable(StringComparer.OrdinalIgnoreCase);
				IDictionaryEnumerator enumerator = table.GetEnumerator();
				while (enumerator.MoveNext())
				{
					hashtable.Add(enumerator.Key, enumerator.Value);
				}
				this._caseInsensitiveTable = hashtable;
			}
			return hashtable[name];
		}

		// Token: 0x04002D83 RID: 11651
		[NonSerialized]
		protected IResourceReader Reader;

		// Token: 0x04002D84 RID: 11652
		protected Hashtable Table;

		// Token: 0x04002D85 RID: 11653
		private Hashtable _caseInsensitiveTable;
	}
}
