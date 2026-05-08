using System;

namespace System.Text
{
	// Token: 0x0200036E RID: 878
	[Serializable]
	public sealed class EncoderExceptionFallback : EncoderFallback
	{
		// Token: 0x060025BB RID: 9659 RVA: 0x00087167 File Offset: 0x00085367
		public EncoderExceptionFallback()
		{
		}

		// Token: 0x060025BC RID: 9660 RVA: 0x0008716F File Offset: 0x0008536F
		public override EncoderFallbackBuffer CreateFallbackBuffer()
		{
			return new EncoderExceptionFallbackBuffer();
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x060025BD RID: 9661 RVA: 0x0000408A File Offset: 0x0000228A
		public override int MaxCharCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060025BE RID: 9662 RVA: 0x00087176 File Offset: 0x00085376
		public override bool Equals(object value)
		{
			return value is EncoderExceptionFallback;
		}

		// Token: 0x060025BF RID: 9663 RVA: 0x00087183 File Offset: 0x00085383
		public override int GetHashCode()
		{
			return 654;
		}
	}
}
