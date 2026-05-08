using System;

namespace System.Text
{
	// Token: 0x0200036F RID: 879
	public sealed class EncoderExceptionFallbackBuffer : EncoderFallbackBuffer
	{
		// Token: 0x060025C0 RID: 9664 RVA: 0x0008718A File Offset: 0x0008538A
		public EncoderExceptionFallbackBuffer()
		{
		}

		// Token: 0x060025C1 RID: 9665 RVA: 0x00087192 File Offset: 0x00085392
		public override bool Fallback(char charUnknown, int index)
		{
			throw new EncoderFallbackException(SR.Format("Unable to translate Unicode character \\\\u{0:X4} at index {1} to specified code page.", (int)charUnknown, index), charUnknown, index);
		}

		// Token: 0x060025C2 RID: 9666 RVA: 0x000871B4 File Offset: 0x000853B4
		public override bool Fallback(char charUnknownHigh, char charUnknownLow, int index)
		{
			if (!char.IsHighSurrogate(charUnknownHigh))
			{
				throw new ArgumentOutOfRangeException("charUnknownHigh", SR.Format("Valid values are between {0} and {1}, inclusive.", 55296, 56319));
			}
			if (!char.IsLowSurrogate(charUnknownLow))
			{
				throw new ArgumentOutOfRangeException("charUnknownLow", SR.Format("Valid values are between {0} and {1}, inclusive.", 56320, 57343));
			}
			int num = char.ConvertToUtf32(charUnknownHigh, charUnknownLow);
			throw new EncoderFallbackException(SR.Format("Unable to translate Unicode character \\\\u{0:X4} at index {1} to specified code page.", num, index), charUnknownHigh, charUnknownLow, index);
		}

		// Token: 0x060025C3 RID: 9667 RVA: 0x0000408A File Offset: 0x0000228A
		public override char GetNextChar()
		{
			return '\0';
		}

		// Token: 0x060025C4 RID: 9668 RVA: 0x0000408A File Offset: 0x0000228A
		public override bool MovePrevious()
		{
			return false;
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x060025C5 RID: 9669 RVA: 0x0000408A File Offset: 0x0000228A
		public override int Remaining
		{
			get
			{
				return 0;
			}
		}
	}
}
