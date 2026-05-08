using System;
using System.Threading;

namespace System.Text
{
	// Token: 0x02000366 RID: 870
	[Serializable]
	public abstract class DecoderFallback
	{
		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06002570 RID: 9584 RVA: 0x0008617B File Offset: 0x0008437B
		public static DecoderFallback ReplacementFallback
		{
			get
			{
				DecoderFallback decoderFallback;
				if ((decoderFallback = DecoderFallback.s_replacementFallback) == null)
				{
					decoderFallback = Interlocked.CompareExchange<DecoderFallback>(ref DecoderFallback.s_replacementFallback, new DecoderReplacementFallback(), null) ?? DecoderFallback.s_replacementFallback;
				}
				return decoderFallback;
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06002571 RID: 9585 RVA: 0x0008619F File Offset: 0x0008439F
		public static DecoderFallback ExceptionFallback
		{
			get
			{
				DecoderFallback decoderFallback;
				if ((decoderFallback = DecoderFallback.s_exceptionFallback) == null)
				{
					decoderFallback = Interlocked.CompareExchange<DecoderFallback>(ref DecoderFallback.s_exceptionFallback, new DecoderExceptionFallback(), null) ?? DecoderFallback.s_exceptionFallback;
				}
				return decoderFallback;
			}
		}

		// Token: 0x06002572 RID: 9586
		public abstract DecoderFallbackBuffer CreateFallbackBuffer();

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06002573 RID: 9587
		public abstract int MaxCharCount { get; }

		// Token: 0x06002574 RID: 9588 RVA: 0x000025BE File Offset: 0x000007BE
		protected DecoderFallback()
		{
		}

		// Token: 0x04001C63 RID: 7267
		private static DecoderFallback s_replacementFallback;

		// Token: 0x04001C64 RID: 7268
		private static DecoderFallback s_exceptionFallback;
	}
}
