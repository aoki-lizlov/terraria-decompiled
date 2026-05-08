using System;
using System.Globalization;

namespace System.Text
{
	// Token: 0x02000364 RID: 868
	public sealed class DecoderExceptionFallbackBuffer : DecoderFallbackBuffer
	{
		// Token: 0x06002563 RID: 9571 RVA: 0x0008607A File Offset: 0x0008427A
		public override bool Fallback(byte[] bytesUnknown, int index)
		{
			this.Throw(bytesUnknown, index);
			return true;
		}

		// Token: 0x06002564 RID: 9572 RVA: 0x0000408A File Offset: 0x0000228A
		public override char GetNextChar()
		{
			return '\0';
		}

		// Token: 0x06002565 RID: 9573 RVA: 0x0000408A File Offset: 0x0000228A
		public override bool MovePrevious()
		{
			return false;
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06002566 RID: 9574 RVA: 0x0000408A File Offset: 0x0000228A
		public override int Remaining
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06002567 RID: 9575 RVA: 0x00086088 File Offset: 0x00084288
		private void Throw(byte[] bytesUnknown, int index)
		{
			StringBuilder stringBuilder = new StringBuilder(bytesUnknown.Length * 3);
			int num = 0;
			while (num < bytesUnknown.Length && num < 20)
			{
				stringBuilder.Append('[');
				stringBuilder.Append(bytesUnknown[num].ToString("X2", CultureInfo.InvariantCulture));
				stringBuilder.Append(']');
				num++;
			}
			if (num == 20)
			{
				stringBuilder.Append(" ...");
			}
			throw new DecoderFallbackException(SR.Format("Unable to translate bytes {0} at index {1} from specified code page to Unicode.", stringBuilder, index), bytesUnknown, index);
		}

		// Token: 0x06002568 RID: 9576 RVA: 0x0008610B File Offset: 0x0008430B
		public DecoderExceptionFallbackBuffer()
		{
		}
	}
}
