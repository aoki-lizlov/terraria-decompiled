using System;
using System.Globalization;

namespace System.Text
{
	// Token: 0x02000367 RID: 871
	public abstract class DecoderFallbackBuffer
	{
		// Token: 0x06002575 RID: 9589
		public abstract bool Fallback(byte[] bytesUnknown, int index);

		// Token: 0x06002576 RID: 9590
		public abstract char GetNextChar();

		// Token: 0x06002577 RID: 9591
		public abstract bool MovePrevious();

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06002578 RID: 9592
		public abstract int Remaining { get; }

		// Token: 0x06002579 RID: 9593 RVA: 0x000861C3 File Offset: 0x000843C3
		public virtual void Reset()
		{
			while (this.GetNextChar() != '\0')
			{
			}
		}

		// Token: 0x0600257A RID: 9594 RVA: 0x000861CD File Offset: 0x000843CD
		internal void InternalReset()
		{
			this.byteStart = null;
			this.Reset();
		}

		// Token: 0x0600257B RID: 9595 RVA: 0x000861DD File Offset: 0x000843DD
		internal unsafe void InternalInitialize(byte* byteStart, char* charEnd)
		{
			this.byteStart = byteStart;
			this.charEnd = charEnd;
		}

		// Token: 0x0600257C RID: 9596 RVA: 0x000861F0 File Offset: 0x000843F0
		internal unsafe virtual bool InternalFallback(byte[] bytes, byte* pBytes, ref char* chars)
		{
			if (this.Fallback(bytes, (int)((long)(pBytes - this.byteStart) - (long)bytes.Length)))
			{
				char* ptr = chars;
				bool flag = false;
				char nextChar;
				while ((nextChar = this.GetNextChar()) != '\0')
				{
					if (char.IsSurrogate(nextChar))
					{
						if (char.IsHighSurrogate(nextChar))
						{
							if (flag)
							{
								throw new ArgumentException("String contains invalid Unicode code points.");
							}
							flag = true;
						}
						else
						{
							if (!flag)
							{
								throw new ArgumentException("String contains invalid Unicode code points.");
							}
							flag = false;
						}
					}
					if (ptr >= this.charEnd)
					{
						return false;
					}
					*(ptr++) = nextChar;
				}
				if (flag)
				{
					throw new ArgumentException("String contains invalid Unicode code points.");
				}
				chars = ptr;
			}
			return true;
		}

		// Token: 0x0600257D RID: 9597 RVA: 0x00086280 File Offset: 0x00084480
		internal unsafe virtual int InternalFallback(byte[] bytes, byte* pBytes)
		{
			if (!this.Fallback(bytes, (int)((long)(pBytes - this.byteStart) - (long)bytes.Length)))
			{
				return 0;
			}
			int num = 0;
			bool flag = false;
			char nextChar;
			while ((nextChar = this.GetNextChar()) != '\0')
			{
				if (char.IsSurrogate(nextChar))
				{
					if (char.IsHighSurrogate(nextChar))
					{
						if (flag)
						{
							throw new ArgumentException("String contains invalid Unicode code points.");
						}
						flag = true;
					}
					else
					{
						if (!flag)
						{
							throw new ArgumentException("String contains invalid Unicode code points.");
						}
						flag = false;
					}
				}
				num++;
			}
			if (flag)
			{
				throw new ArgumentException("String contains invalid Unicode code points.");
			}
			return num;
		}

		// Token: 0x0600257E RID: 9598 RVA: 0x00086300 File Offset: 0x00084500
		internal void ThrowLastBytesRecursive(byte[] bytesUnknown)
		{
			StringBuilder stringBuilder = new StringBuilder(bytesUnknown.Length * 3);
			int num = 0;
			while (num < bytesUnknown.Length && num < 20)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "\\x{0:X2}", bytesUnknown[num]);
				num++;
			}
			if (num == 20)
			{
				stringBuilder.Append(" ...");
			}
			throw new ArgumentException(SR.Format("Recursive fallback not allowed for bytes {0}.", stringBuilder.ToString()), "bytesUnknown");
		}

		// Token: 0x0600257F RID: 9599 RVA: 0x000025BE File Offset: 0x000007BE
		protected DecoderFallbackBuffer()
		{
		}

		// Token: 0x04001C65 RID: 7269
		internal unsafe byte* byteStart;

		// Token: 0x04001C66 RID: 7270
		internal unsafe char* charEnd;
	}
}
