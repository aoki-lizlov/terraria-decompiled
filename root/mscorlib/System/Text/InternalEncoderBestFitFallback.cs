using System;

namespace System.Text
{
	// Token: 0x0200036C RID: 876
	[Serializable]
	internal class InternalEncoderBestFitFallback : EncoderFallback
	{
		// Token: 0x060025AD RID: 9645 RVA: 0x00086E78 File Offset: 0x00085078
		internal InternalEncoderBestFitFallback(Encoding encoding)
		{
			this._encoding = encoding;
		}

		// Token: 0x060025AE RID: 9646 RVA: 0x00086E87 File Offset: 0x00085087
		public override EncoderFallbackBuffer CreateFallbackBuffer()
		{
			return new InternalEncoderBestFitFallbackBuffer(this);
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x060025AF RID: 9647 RVA: 0x00003FB7 File Offset: 0x000021B7
		public override int MaxCharCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x060025B0 RID: 9648 RVA: 0x00086E90 File Offset: 0x00085090
		public override bool Equals(object value)
		{
			InternalEncoderBestFitFallback internalEncoderBestFitFallback = value as InternalEncoderBestFitFallback;
			return internalEncoderBestFitFallback != null && this._encoding.CodePage == internalEncoderBestFitFallback._encoding.CodePage;
		}

		// Token: 0x060025B1 RID: 9649 RVA: 0x00086EC1 File Offset: 0x000850C1
		public override int GetHashCode()
		{
			return this._encoding.CodePage;
		}

		// Token: 0x04001C71 RID: 7281
		internal Encoding _encoding;

		// Token: 0x04001C72 RID: 7282
		internal char[] _arrayBestFit;
	}
}
