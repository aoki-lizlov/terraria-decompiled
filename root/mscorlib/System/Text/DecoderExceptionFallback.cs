using System;

namespace System.Text
{
	// Token: 0x02000363 RID: 867
	[Serializable]
	public sealed class DecoderExceptionFallback : DecoderFallback
	{
		// Token: 0x0600255E RID: 9566 RVA: 0x00086057 File Offset: 0x00084257
		public DecoderExceptionFallback()
		{
		}

		// Token: 0x0600255F RID: 9567 RVA: 0x0008605F File Offset: 0x0008425F
		public override DecoderFallbackBuffer CreateFallbackBuffer()
		{
			return new DecoderExceptionFallbackBuffer();
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06002560 RID: 9568 RVA: 0x0000408A File Offset: 0x0000228A
		public override int MaxCharCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06002561 RID: 9569 RVA: 0x00086066 File Offset: 0x00084266
		public override bool Equals(object value)
		{
			return value is DecoderExceptionFallback;
		}

		// Token: 0x06002562 RID: 9570 RVA: 0x00086073 File Offset: 0x00084273
		public override int GetHashCode()
		{
			return 879;
		}
	}
}
