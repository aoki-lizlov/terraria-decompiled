using System;

namespace System.Text
{
	// Token: 0x02000361 RID: 865
	[Serializable]
	internal sealed class InternalDecoderBestFitFallback : DecoderFallback
	{
		// Token: 0x06002550 RID: 9552 RVA: 0x00085D9C File Offset: 0x00083F9C
		internal InternalDecoderBestFitFallback(Encoding encoding)
		{
			this._encoding = encoding;
		}

		// Token: 0x06002551 RID: 9553 RVA: 0x00085DB3 File Offset: 0x00083FB3
		public override DecoderFallbackBuffer CreateFallbackBuffer()
		{
			return new InternalDecoderBestFitFallbackBuffer(this);
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06002552 RID: 9554 RVA: 0x00003FB7 File Offset: 0x000021B7
		public override int MaxCharCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06002553 RID: 9555 RVA: 0x00085DBC File Offset: 0x00083FBC
		public override bool Equals(object value)
		{
			InternalDecoderBestFitFallback internalDecoderBestFitFallback = value as InternalDecoderBestFitFallback;
			return internalDecoderBestFitFallback != null && this._encoding.CodePage == internalDecoderBestFitFallback._encoding.CodePage;
		}

		// Token: 0x06002554 RID: 9556 RVA: 0x00085DED File Offset: 0x00083FED
		public override int GetHashCode()
		{
			return this._encoding.CodePage;
		}

		// Token: 0x04001C59 RID: 7257
		internal Encoding _encoding;

		// Token: 0x04001C5A RID: 7258
		internal char[] _arrayBestFit;

		// Token: 0x04001C5B RID: 7259
		internal char _cReplacement = '?';
	}
}
