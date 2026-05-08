using System;
using System.Threading;

namespace System.Text
{
	// Token: 0x02000362 RID: 866
	internal sealed class InternalDecoderBestFitFallbackBuffer : DecoderFallbackBuffer
	{
		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06002555 RID: 9557 RVA: 0x00085DFC File Offset: 0x00083FFC
		private static object InternalSyncObject
		{
			get
			{
				if (InternalDecoderBestFitFallbackBuffer.s_InternalSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange<object>(ref InternalDecoderBestFitFallbackBuffer.s_InternalSyncObject, obj, null);
				}
				return InternalDecoderBestFitFallbackBuffer.s_InternalSyncObject;
			}
		}

		// Token: 0x06002556 RID: 9558 RVA: 0x00085E28 File Offset: 0x00084028
		public InternalDecoderBestFitFallbackBuffer(InternalDecoderBestFitFallback fallback)
		{
			this._oFallback = fallback;
			if (this._oFallback._arrayBestFit == null)
			{
				object internalSyncObject = InternalDecoderBestFitFallbackBuffer.InternalSyncObject;
				lock (internalSyncObject)
				{
					if (this._oFallback._arrayBestFit == null)
					{
						this._oFallback._arrayBestFit = fallback._encoding.GetBestFitBytesToUnicodeData();
					}
				}
			}
		}

		// Token: 0x06002557 RID: 9559 RVA: 0x00085EA8 File Offset: 0x000840A8
		public override bool Fallback(byte[] bytesUnknown, int index)
		{
			this._cBestFit = this.TryBestFit(bytesUnknown);
			if (this._cBestFit == '\0')
			{
				this._cBestFit = this._oFallback._cReplacement;
			}
			this._iCount = (this._iSize = 1);
			return true;
		}

		// Token: 0x06002558 RID: 9560 RVA: 0x00085EEC File Offset: 0x000840EC
		public override char GetNextChar()
		{
			this._iCount--;
			if (this._iCount < 0)
			{
				return '\0';
			}
			if (this._iCount == 2147483647)
			{
				this._iCount = -1;
				return '\0';
			}
			return this._cBestFit;
		}

		// Token: 0x06002559 RID: 9561 RVA: 0x00085F23 File Offset: 0x00084123
		public override bool MovePrevious()
		{
			if (this._iCount >= 0)
			{
				this._iCount++;
			}
			return this._iCount >= 0 && this._iCount <= this._iSize;
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x0600255A RID: 9562 RVA: 0x00085F58 File Offset: 0x00084158
		public override int Remaining
		{
			get
			{
				if (this._iCount <= 0)
				{
					return 0;
				}
				return this._iCount;
			}
		}

		// Token: 0x0600255B RID: 9563 RVA: 0x00085F6B File Offset: 0x0008416B
		public override void Reset()
		{
			this._iCount = -1;
			this.byteStart = null;
		}

		// Token: 0x0600255C RID: 9564 RVA: 0x00003FB7 File Offset: 0x000021B7
		internal unsafe override int InternalFallback(byte[] bytes, byte* pBytes)
		{
			return 1;
		}

		// Token: 0x0600255D RID: 9565 RVA: 0x00085F7C File Offset: 0x0008417C
		private char TryBestFit(byte[] bytesCheck)
		{
			int num = 0;
			int num2 = this._oFallback._arrayBestFit.Length;
			if (num2 == 0)
			{
				return '\0';
			}
			if (bytesCheck.Length == 0 || bytesCheck.Length > 2)
			{
				return '\0';
			}
			char c;
			if (bytesCheck.Length == 1)
			{
				c = (char)bytesCheck[0];
			}
			else
			{
				c = (char)(((int)bytesCheck[0] << 8) + (int)bytesCheck[1]);
			}
			if (c < this._oFallback._arrayBestFit[0] || c > this._oFallback._arrayBestFit[num2 - 2])
			{
				return '\0';
			}
			int num3;
			while ((num3 = num2 - num) > 6)
			{
				int i = (num3 / 2 + num) & 65534;
				char c2 = this._oFallback._arrayBestFit[i];
				if (c2 == c)
				{
					return this._oFallback._arrayBestFit[i + 1];
				}
				if (c2 < c)
				{
					num = i;
				}
				else
				{
					num2 = i;
				}
			}
			for (int i = num; i < num2; i += 2)
			{
				if (this._oFallback._arrayBestFit[i] == c)
				{
					return this._oFallback._arrayBestFit[i + 1];
				}
			}
			return '\0';
		}

		// Token: 0x04001C5C RID: 7260
		private char _cBestFit;

		// Token: 0x04001C5D RID: 7261
		private int _iCount = -1;

		// Token: 0x04001C5E RID: 7262
		private int _iSize;

		// Token: 0x04001C5F RID: 7263
		private InternalDecoderBestFitFallback _oFallback;

		// Token: 0x04001C60 RID: 7264
		private static object s_InternalSyncObject;
	}
}
