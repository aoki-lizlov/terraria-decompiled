using System;

namespace System.Text
{
	// Token: 0x0200036A RID: 874
	public sealed class DecoderReplacementFallbackBuffer : DecoderFallbackBuffer
	{
		// Token: 0x06002597 RID: 9623 RVA: 0x000868EB File Offset: 0x00084AEB
		public DecoderReplacementFallbackBuffer(DecoderReplacementFallback fallback)
		{
			this._strDefault = fallback.DefaultString;
		}

		// Token: 0x06002598 RID: 9624 RVA: 0x0008690D File Offset: 0x00084B0D
		public override bool Fallback(byte[] bytesUnknown, int index)
		{
			if (this._fallbackCount >= 1)
			{
				base.ThrowLastBytesRecursive(bytesUnknown);
			}
			if (this._strDefault.Length == 0)
			{
				return false;
			}
			this._fallbackCount = this._strDefault.Length;
			this._fallbackIndex = -1;
			return true;
		}

		// Token: 0x06002599 RID: 9625 RVA: 0x00086948 File Offset: 0x00084B48
		public override char GetNextChar()
		{
			this._fallbackCount--;
			this._fallbackIndex++;
			if (this._fallbackCount < 0)
			{
				return '\0';
			}
			if (this._fallbackCount == 2147483647)
			{
				this._fallbackCount = -1;
				return '\0';
			}
			return this._strDefault[this._fallbackIndex];
		}

		// Token: 0x0600259A RID: 9626 RVA: 0x000869A3 File Offset: 0x00084BA3
		public override bool MovePrevious()
		{
			if (this._fallbackCount >= -1 && this._fallbackIndex >= 0)
			{
				this._fallbackIndex--;
				this._fallbackCount++;
				return true;
			}
			return false;
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x0600259B RID: 9627 RVA: 0x000869D6 File Offset: 0x00084BD6
		public override int Remaining
		{
			get
			{
				if (this._fallbackCount >= 0)
				{
					return this._fallbackCount;
				}
				return 0;
			}
		}

		// Token: 0x0600259C RID: 9628 RVA: 0x000869E9 File Offset: 0x00084BE9
		public override void Reset()
		{
			this._fallbackCount = -1;
			this._fallbackIndex = -1;
			this.byteStart = null;
		}

		// Token: 0x0600259D RID: 9629 RVA: 0x00086A01 File Offset: 0x00084C01
		internal unsafe override int InternalFallback(byte[] bytes, byte* pBytes)
		{
			return this._strDefault.Length;
		}

		// Token: 0x04001C6C RID: 7276
		private string _strDefault;

		// Token: 0x04001C6D RID: 7277
		private int _fallbackCount = -1;

		// Token: 0x04001C6E RID: 7278
		private int _fallbackIndex = -1;
	}
}
