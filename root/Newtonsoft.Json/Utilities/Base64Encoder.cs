using System;
using System.IO;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000047 RID: 71
	internal class Base64Encoder
	{
		// Token: 0x060003D6 RID: 982 RVA: 0x0000F9CD File Offset: 0x0000DBCD
		public Base64Encoder(TextWriter writer)
		{
			ValidationUtils.ArgumentNotNull(writer, "writer");
			this._writer = writer;
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000F9F4 File Offset: 0x0000DBF4
		private void ValidateEncode(byte[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (count > buffer.Length - index)
			{
				throw new ArgumentOutOfRangeException("count");
			}
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000FA40 File Offset: 0x0000DC40
		public void Encode(byte[] buffer, int index, int count)
		{
			this.ValidateEncode(buffer, index, count);
			if (this._leftOverBytesCount > 0)
			{
				if (this.FulfillFromLeftover(buffer, index, ref count))
				{
					return;
				}
				int num = Convert.ToBase64CharArray(this._leftOverBytes, 0, 3, this._charsLine, 0);
				this.WriteChars(this._charsLine, 0, num);
			}
			this.StoreLeftOverBytes(buffer, index, ref count);
			int num2 = index + count;
			int num3 = 57;
			while (index < num2)
			{
				if (index + num3 > num2)
				{
					num3 = num2 - index;
				}
				int num4 = Convert.ToBase64CharArray(buffer, index, num3, this._charsLine, 0);
				this.WriteChars(this._charsLine, 0, num4);
				index += num3;
			}
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000FAD4 File Offset: 0x0000DCD4
		private void StoreLeftOverBytes(byte[] buffer, int index, ref int count)
		{
			int num = count % 3;
			if (num > 0)
			{
				count -= num;
				if (this._leftOverBytes == null)
				{
					this._leftOverBytes = new byte[3];
				}
				for (int i = 0; i < num; i++)
				{
					this._leftOverBytes[i] = buffer[index + count + i];
				}
			}
			this._leftOverBytesCount = num;
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000FB28 File Offset: 0x0000DD28
		private bool FulfillFromLeftover(byte[] buffer, int index, ref int count)
		{
			int leftOverBytesCount = this._leftOverBytesCount;
			while (leftOverBytesCount < 3 && count > 0)
			{
				this._leftOverBytes[leftOverBytesCount++] = buffer[index++];
				count--;
			}
			if (count == 0 && leftOverBytesCount < 3)
			{
				this._leftOverBytesCount = leftOverBytesCount;
				return true;
			}
			return false;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000FB74 File Offset: 0x0000DD74
		public void Flush()
		{
			if (this._leftOverBytesCount > 0)
			{
				int num = Convert.ToBase64CharArray(this._leftOverBytes, 0, this._leftOverBytesCount, this._charsLine, 0);
				this.WriteChars(this._charsLine, 0, num);
				this._leftOverBytesCount = 0;
			}
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000FBB9 File Offset: 0x0000DDB9
		private void WriteChars(char[] chars, int index, int count)
		{
			this._writer.Write(chars, index, count);
		}

		// Token: 0x040001DF RID: 479
		private const int Base64LineSize = 76;

		// Token: 0x040001E0 RID: 480
		private const int LineSizeInBytes = 57;

		// Token: 0x040001E1 RID: 481
		private readonly char[] _charsLine = new char[76];

		// Token: 0x040001E2 RID: 482
		private readonly TextWriter _writer;

		// Token: 0x040001E3 RID: 483
		private byte[] _leftOverBytes;

		// Token: 0x040001E4 RID: 484
		private int _leftOverBytesCount;
	}
}
