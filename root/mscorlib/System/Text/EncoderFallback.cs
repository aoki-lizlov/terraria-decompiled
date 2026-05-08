using System;
using System.Threading;

namespace System.Text
{
	// Token: 0x02000371 RID: 881
	[Serializable]
	public abstract class EncoderFallback
	{
		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x060025D1 RID: 9681 RVA: 0x00087317 File Offset: 0x00085517
		public static EncoderFallback ReplacementFallback
		{
			get
			{
				if (EncoderFallback.s_replacementFallback == null)
				{
					Interlocked.CompareExchange<EncoderFallback>(ref EncoderFallback.s_replacementFallback, new EncoderReplacementFallback(), null);
				}
				return EncoderFallback.s_replacementFallback;
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x060025D2 RID: 9682 RVA: 0x00087336 File Offset: 0x00085536
		public static EncoderFallback ExceptionFallback
		{
			get
			{
				if (EncoderFallback.s_exceptionFallback == null)
				{
					Interlocked.CompareExchange<EncoderFallback>(ref EncoderFallback.s_exceptionFallback, new EncoderExceptionFallback(), null);
				}
				return EncoderFallback.s_exceptionFallback;
			}
		}

		// Token: 0x060025D3 RID: 9683
		public abstract EncoderFallbackBuffer CreateFallbackBuffer();

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x060025D4 RID: 9684
		public abstract int MaxCharCount { get; }

		// Token: 0x060025D5 RID: 9685 RVA: 0x000025BE File Offset: 0x000007BE
		protected EncoderFallback()
		{
		}

		// Token: 0x04001C7C RID: 7292
		private static EncoderFallback s_replacementFallback;

		// Token: 0x04001C7D RID: 7293
		private static EncoderFallback s_exceptionFallback;
	}
}
