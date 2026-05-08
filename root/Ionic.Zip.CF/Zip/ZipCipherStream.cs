using System;
using System.IO;

namespace Ionic.Zip
{
	// Token: 0x0200001C RID: 28
	internal class ZipCipherStream : Stream
	{
		// Token: 0x06000094 RID: 148 RVA: 0x000032A6 File Offset: 0x000014A6
		public ZipCipherStream(Stream s, ZipCrypto cipher, CryptoMode mode)
		{
			this._cipher = cipher;
			this._s = s;
			this._mode = mode;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000032C4 File Offset: 0x000014C4
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this._mode == CryptoMode.Encrypt)
			{
				throw new NotSupportedException("This stream does not encrypt via Read()");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			byte[] array = new byte[count];
			int num = this._s.Read(array, 0, count);
			byte[] array2 = this._cipher.DecryptMessage(array, num);
			for (int i = 0; i < num; i++)
			{
				buffer[offset + i] = array2[i];
			}
			return num;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000332C File Offset: 0x0000152C
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this._mode == CryptoMode.Decrypt)
			{
				throw new NotSupportedException("This stream does not Decrypt via Write()");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count == 0)
			{
				return;
			}
			byte[] array;
			if (offset != 0)
			{
				array = new byte[count];
				for (int i = 0; i < count; i++)
				{
					array[i] = buffer[offset + i];
				}
			}
			else
			{
				array = buffer;
			}
			byte[] array2 = this._cipher.EncryptMessage(array, count);
			this._s.Write(array2, 0, array2.Length);
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000097 RID: 151 RVA: 0x000033A1 File Offset: 0x000015A1
		public override bool CanRead
		{
			get
			{
				return this._mode == CryptoMode.Decrypt;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000033AC File Offset: 0x000015AC
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000099 RID: 153 RVA: 0x000033AF File Offset: 0x000015AF
		public override bool CanWrite
		{
			get
			{
				return this._mode == CryptoMode.Encrypt;
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000033BA File Offset: 0x000015BA
		public override void Flush()
		{
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600009B RID: 155 RVA: 0x000033BC File Offset: 0x000015BC
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600009C RID: 156 RVA: 0x000033C3 File Offset: 0x000015C3
		// (set) Token: 0x0600009D RID: 157 RVA: 0x000033CA File Offset: 0x000015CA
		public override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000033D1 File Offset: 0x000015D1
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000033D8 File Offset: 0x000015D8
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04000049 RID: 73
		private ZipCrypto _cipher;

		// Token: 0x0400004A RID: 74
		private Stream _s;

		// Token: 0x0400004B RID: 75
		private CryptoMode _mode;
	}
}
