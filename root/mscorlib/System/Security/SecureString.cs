using System;
using System.Runtime.ExceptionServices;

namespace System.Security
{
	// Token: 0x020003B4 RID: 948
	[MonoTODO("work in progress - encryption is missing")]
	public sealed class SecureString : IDisposable
	{
		// Token: 0x060028AC RID: 10412 RVA: 0x00004088 File Offset: 0x00002288
		static SecureString()
		{
		}

		// Token: 0x060028AD RID: 10413 RVA: 0x00094A08 File Offset: 0x00092C08
		public SecureString()
		{
			this.Alloc(8, false);
		}

		// Token: 0x060028AE RID: 10414 RVA: 0x00094A18 File Offset: 0x00092C18
		[CLSCompliant(false)]
		public unsafe SecureString(char* value, int length)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (length < 0 || length > 65536)
			{
				throw new ArgumentOutOfRangeException("length", "< 0 || > 65536");
			}
			this.length = length;
			this.Alloc(length, false);
			int num = 0;
			for (int i = 0; i < length; i++)
			{
				char c = *(value++);
				this.data[num++] = (byte)(c >> 8);
				this.data[num++] = (byte)c;
			}
			this.Encrypt();
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x060028AF RID: 10415 RVA: 0x00094AA0 File Offset: 0x00092CA0
		public int Length
		{
			get
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException("SecureString");
				}
				return this.length;
			}
		}

		// Token: 0x060028B0 RID: 10416 RVA: 0x00094ABC File Offset: 0x00092CBC
		[HandleProcessCorruptedStateExceptions]
		public void AppendChar(char c)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("SecureString");
			}
			if (this.read_only)
			{
				throw new InvalidOperationException(Locale.GetText("SecureString is read-only."));
			}
			if (this.length == 65536)
			{
				throw new ArgumentOutOfRangeException("length", "> 65536");
			}
			try
			{
				this.Decrypt();
				int num = this.length * 2;
				int num2 = this.length + 1;
				this.length = num2;
				this.Alloc(num2, true);
				this.data[num++] = (byte)(c >> 8);
				this.data[num++] = (byte)c;
			}
			finally
			{
				this.Encrypt();
			}
		}

		// Token: 0x060028B1 RID: 10417 RVA: 0x00094B70 File Offset: 0x00092D70
		public void Clear()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("SecureString");
			}
			if (this.read_only)
			{
				throw new InvalidOperationException(Locale.GetText("SecureString is read-only."));
			}
			Array.Clear(this.data, 0, this.data.Length);
			this.length = 0;
		}

		// Token: 0x060028B2 RID: 10418 RVA: 0x00094BC3 File Offset: 0x00092DC3
		public SecureString Copy()
		{
			return new SecureString
			{
				data = (byte[])this.data.Clone(),
				length = this.length
			};
		}

		// Token: 0x060028B3 RID: 10419 RVA: 0x00094BEC File Offset: 0x00092DEC
		public void Dispose()
		{
			this.disposed = true;
			if (this.data != null)
			{
				Array.Clear(this.data, 0, this.data.Length);
				this.data = null;
			}
			this.length = 0;
		}

		// Token: 0x060028B4 RID: 10420 RVA: 0x00094C20 File Offset: 0x00092E20
		[HandleProcessCorruptedStateExceptions]
		public void InsertAt(int index, char c)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("SecureString");
			}
			if (this.read_only)
			{
				throw new InvalidOperationException(Locale.GetText("SecureString is read-only."));
			}
			if (index < 0 || index > this.length)
			{
				throw new ArgumentOutOfRangeException("index", "< 0 || > length");
			}
			if (this.length >= 65536)
			{
				string text = Locale.GetText("Maximum string size is '{0}'.", new object[] { 65536 });
				throw new ArgumentOutOfRangeException("index", text);
			}
			try
			{
				this.Decrypt();
				int num = this.length + 1;
				this.length = num;
				this.Alloc(num, true);
				int num2 = index * 2;
				Buffer.BlockCopy(this.data, num2, this.data, num2 + 2, this.data.Length - num2 - 2);
				this.data[num2++] = (byte)(c >> 8);
				this.data[num2] = (byte)c;
			}
			finally
			{
				this.Encrypt();
			}
		}

		// Token: 0x060028B5 RID: 10421 RVA: 0x00094D24 File Offset: 0x00092F24
		public bool IsReadOnly()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("SecureString");
			}
			return this.read_only;
		}

		// Token: 0x060028B6 RID: 10422 RVA: 0x00094D3F File Offset: 0x00092F3F
		public void MakeReadOnly()
		{
			this.read_only = true;
		}

		// Token: 0x060028B7 RID: 10423 RVA: 0x00094D48 File Offset: 0x00092F48
		[HandleProcessCorruptedStateExceptions]
		public void RemoveAt(int index)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("SecureString");
			}
			if (this.read_only)
			{
				throw new InvalidOperationException(Locale.GetText("SecureString is read-only."));
			}
			if (index < 0 || index >= this.length)
			{
				throw new ArgumentOutOfRangeException("index", "< 0 || > length");
			}
			try
			{
				this.Decrypt();
				Buffer.BlockCopy(this.data, index * 2 + 2, this.data, index * 2, this.data.Length - index * 2 - 2);
				int num = this.length - 1;
				this.length = num;
				this.Alloc(num, true);
			}
			finally
			{
				this.Encrypt();
			}
		}

		// Token: 0x060028B8 RID: 10424 RVA: 0x00094DFC File Offset: 0x00092FFC
		[HandleProcessCorruptedStateExceptions]
		public void SetAt(int index, char c)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("SecureString");
			}
			if (this.read_only)
			{
				throw new InvalidOperationException(Locale.GetText("SecureString is read-only."));
			}
			if (index < 0 || index >= this.length)
			{
				throw new ArgumentOutOfRangeException("index", "< 0 || > length");
			}
			try
			{
				this.Decrypt();
				int num = index * 2;
				this.data[num++] = (byte)(c >> 8);
				this.data[num] = (byte)c;
			}
			finally
			{
				this.Encrypt();
			}
		}

		// Token: 0x060028B9 RID: 10425 RVA: 0x00094E90 File Offset: 0x00093090
		private void Encrypt()
		{
			if (this.data != null)
			{
				int num = this.data.Length;
			}
		}

		// Token: 0x060028BA RID: 10426 RVA: 0x00094E90 File Offset: 0x00093090
		private void Decrypt()
		{
			if (this.data != null)
			{
				int num = this.data.Length;
			}
		}

		// Token: 0x060028BB RID: 10427 RVA: 0x00094EA4 File Offset: 0x000930A4
		private void Alloc(int length, bool realloc)
		{
			if (length < 0 || length > 65536)
			{
				throw new ArgumentOutOfRangeException("length", "< 0 || > 65536");
			}
			int num = (length >> 3) + (((length & 7) == 0) ? 0 : 1) << 4;
			if (realloc && this.data != null && num == this.data.Length)
			{
				return;
			}
			if (realloc)
			{
				byte[] array = new byte[num];
				Array.Copy(this.data, 0, array, 0, Math.Min(this.data.Length, array.Length));
				Array.Clear(this.data, 0, this.data.Length);
				this.data = array;
				return;
			}
			this.data = new byte[num];
		}

		// Token: 0x060028BC RID: 10428 RVA: 0x00094F44 File Offset: 0x00093144
		internal byte[] GetBuffer()
		{
			byte[] array = new byte[this.length << 1];
			try
			{
				this.Decrypt();
				Buffer.BlockCopy(this.data, 0, array, 0, array.Length);
			}
			finally
			{
				this.Encrypt();
			}
			return array;
		}

		// Token: 0x04001D97 RID: 7575
		private const int BlockSize = 16;

		// Token: 0x04001D98 RID: 7576
		private const int MaxSize = 65536;

		// Token: 0x04001D99 RID: 7577
		private int length;

		// Token: 0x04001D9A RID: 7578
		private bool disposed;

		// Token: 0x04001D9B RID: 7579
		private bool read_only;

		// Token: 0x04001D9C RID: 7580
		private byte[] data;
	}
}
