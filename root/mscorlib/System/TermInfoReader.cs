using System;
using System.IO;
using System.Text;

namespace System
{
	// Token: 0x0200022A RID: 554
	internal class TermInfoReader
	{
		// Token: 0x06001B7A RID: 7034 RVA: 0x00068160 File Offset: 0x00066360
		public TermInfoReader(string term, string filename)
		{
			using (FileStream fileStream = File.OpenRead(filename))
			{
				long length = fileStream.Length;
				if (length > 4096L)
				{
					throw new Exception("File must be smaller than 4K");
				}
				this.buffer = new byte[(int)length];
				if (fileStream.Read(this.buffer, 0, this.buffer.Length) != this.buffer.Length)
				{
					throw new Exception("Short read");
				}
				this.ReadHeader(this.buffer, ref this.booleansOffset);
				this.ReadNames(this.buffer, ref this.booleansOffset);
			}
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x0006820C File Offset: 0x0006640C
		public TermInfoReader(string term, byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			this.buffer = buffer;
			this.ReadHeader(buffer, ref this.booleansOffset);
			this.ReadNames(buffer, ref this.booleansOffset);
		}

		// Token: 0x06001B7C RID: 7036 RVA: 0x00068243 File Offset: 0x00066443
		private void DetermineVersion(short magic)
		{
			if (magic == 282)
			{
				this.intOffset = 2;
				return;
			}
			if (magic == 542)
			{
				this.intOffset = 4;
				return;
			}
			throw new Exception(string.Format("Magic number is unexpected: {0}", magic));
		}

		// Token: 0x06001B7D RID: 7037 RVA: 0x0006827C File Offset: 0x0006647C
		private void ReadHeader(byte[] buffer, ref int position)
		{
			short @int = this.GetInt16(buffer, position);
			position += 2;
			this.DetermineVersion(@int);
			this.GetInt16(buffer, position);
			position += 2;
			this.boolSize = (int)this.GetInt16(buffer, position);
			position += 2;
			this.numSize = (int)this.GetInt16(buffer, position);
			position += 2;
			this.strOffsets = (int)this.GetInt16(buffer, position);
			position += 2;
			this.GetInt16(buffer, position);
			position += 2;
		}

		// Token: 0x06001B7E RID: 7038 RVA: 0x00068300 File Offset: 0x00066500
		private void ReadNames(byte[] buffer, ref int position)
		{
			string @string = this.GetString(buffer, position);
			position += @string.Length + 1;
		}

		// Token: 0x06001B7F RID: 7039 RVA: 0x00068324 File Offset: 0x00066524
		public bool Get(TermInfoBooleans boolean)
		{
			if (boolean < TermInfoBooleans.AutoLeftMargin || boolean >= TermInfoBooleans.Last || boolean >= (TermInfoBooleans)this.boolSize)
			{
				return false;
			}
			int num = this.booleansOffset;
			num = (int)(num + boolean);
			return this.buffer[num] > 0;
		}

		// Token: 0x06001B80 RID: 7040 RVA: 0x00068360 File Offset: 0x00066560
		public int Get(TermInfoNumbers number)
		{
			if (number < TermInfoNumbers.Columns || number >= TermInfoNumbers.Last || number > (TermInfoNumbers)this.numSize)
			{
				return -1;
			}
			int num = this.booleansOffset + this.boolSize;
			if (num % 2 == 1)
			{
				num++;
			}
			num = (int)(num + number * (TermInfoNumbers)this.intOffset);
			return (int)this.GetInt16(this.buffer, num);
		}

		// Token: 0x06001B81 RID: 7041 RVA: 0x000683B4 File Offset: 0x000665B4
		public string Get(TermInfoStrings tstr)
		{
			if (tstr < TermInfoStrings.BackTab || tstr >= TermInfoStrings.Last || tstr > (TermInfoStrings)this.strOffsets)
			{
				return null;
			}
			int num = this.booleansOffset + this.boolSize;
			if (num % 2 == 1)
			{
				num++;
			}
			num += this.numSize * this.intOffset;
			int @int = (int)this.GetInt16(this.buffer, (int)(num + tstr * TermInfoStrings.CarriageReturn));
			if (@int == -1)
			{
				return null;
			}
			return this.GetString(this.buffer, num + this.strOffsets * 2 + @int);
		}

		// Token: 0x06001B82 RID: 7042 RVA: 0x00068434 File Offset: 0x00066634
		public byte[] GetStringBytes(TermInfoStrings tstr)
		{
			if (tstr < TermInfoStrings.BackTab || tstr >= TermInfoStrings.Last || tstr > (TermInfoStrings)this.strOffsets)
			{
				return null;
			}
			int num = this.booleansOffset + this.boolSize;
			if (num % 2 == 1)
			{
				num++;
			}
			num += this.numSize * this.intOffset;
			int @int = (int)this.GetInt16(this.buffer, (int)(num + tstr * TermInfoStrings.CarriageReturn));
			if (@int == -1)
			{
				return null;
			}
			return this.GetStringBytes(this.buffer, num + this.strOffsets * 2 + @int);
		}

		// Token: 0x06001B83 RID: 7043 RVA: 0x000684B4 File Offset: 0x000666B4
		private short GetInt16(byte[] buffer, int offset)
		{
			int num = (int)buffer[offset];
			int num2 = (int)buffer[offset + 1];
			if (num == 255 && num2 == 255)
			{
				return -1;
			}
			return (short)(num + num2 * 256);
		}

		// Token: 0x06001B84 RID: 7044 RVA: 0x000684E8 File Offset: 0x000666E8
		private string GetString(byte[] buffer, int offset)
		{
			int num = 0;
			int num2 = offset;
			while (buffer[num2++] != 0)
			{
				num++;
			}
			return Encoding.ASCII.GetString(buffer, offset, num);
		}

		// Token: 0x06001B85 RID: 7045 RVA: 0x00068518 File Offset: 0x00066718
		private byte[] GetStringBytes(byte[] buffer, int offset)
		{
			int num = 0;
			int num2 = offset;
			while (buffer[num2++] != 0)
			{
				num++;
			}
			byte[] array = new byte[num];
			Buffer.InternalBlockCopy(buffer, offset, array, 0, num);
			return array;
		}

		// Token: 0x06001B86 RID: 7046 RVA: 0x0006854C File Offset: 0x0006674C
		internal static string Escape(string s)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in s)
			{
				if (char.IsControl(c))
				{
					stringBuilder.AppendFormat("\\x{0:X2}", (int)c);
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040016E0 RID: 5856
		private int boolSize;

		// Token: 0x040016E1 RID: 5857
		private int numSize;

		// Token: 0x040016E2 RID: 5858
		private int strOffsets;

		// Token: 0x040016E3 RID: 5859
		private byte[] buffer;

		// Token: 0x040016E4 RID: 5860
		private int booleansOffset;

		// Token: 0x040016E5 RID: 5861
		private int intOffset;
	}
}
