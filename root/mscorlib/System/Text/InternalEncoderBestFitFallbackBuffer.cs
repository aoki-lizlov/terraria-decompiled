using System;
using System.Threading;

namespace System.Text
{
	// Token: 0x0200036D RID: 877
	internal sealed class InternalEncoderBestFitFallbackBuffer : EncoderFallbackBuffer
	{
		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x060025B2 RID: 9650 RVA: 0x00086ED0 File Offset: 0x000850D0
		private static object InternalSyncObject
		{
			get
			{
				if (InternalEncoderBestFitFallbackBuffer.s_InternalSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange<object>(ref InternalEncoderBestFitFallbackBuffer.s_InternalSyncObject, obj, null);
				}
				return InternalEncoderBestFitFallbackBuffer.s_InternalSyncObject;
			}
		}

		// Token: 0x060025B3 RID: 9651 RVA: 0x00086EFC File Offset: 0x000850FC
		public InternalEncoderBestFitFallbackBuffer(InternalEncoderBestFitFallback fallback)
		{
			this._oFallback = fallback;
			if (this._oFallback._arrayBestFit == null)
			{
				object internalSyncObject = InternalEncoderBestFitFallbackBuffer.InternalSyncObject;
				lock (internalSyncObject)
				{
					if (this._oFallback._arrayBestFit == null)
					{
						this._oFallback._arrayBestFit = fallback._encoding.GetBestFitUnicodeToBytesData();
					}
				}
			}
		}

		// Token: 0x060025B4 RID: 9652 RVA: 0x00086F7C File Offset: 0x0008517C
		public override bool Fallback(char charUnknown, int index)
		{
			this._iCount = (this._iSize = 1);
			this._cBestFit = this.TryBestFit(charUnknown);
			if (this._cBestFit == '\0')
			{
				this._cBestFit = '?';
			}
			return true;
		}

		// Token: 0x060025B5 RID: 9653 RVA: 0x00086FB8 File Offset: 0x000851B8
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
			this._cBestFit = '?';
			this._iCount = (this._iSize = 2);
			return true;
		}

		// Token: 0x060025B6 RID: 9654 RVA: 0x00087040 File Offset: 0x00085240
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

		// Token: 0x060025B7 RID: 9655 RVA: 0x00087077 File Offset: 0x00085277
		public override bool MovePrevious()
		{
			if (this._iCount >= 0)
			{
				this._iCount++;
			}
			return this._iCount >= 0 && this._iCount <= this._iSize;
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x060025B8 RID: 9656 RVA: 0x000870AC File Offset: 0x000852AC
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

		// Token: 0x060025B9 RID: 9657 RVA: 0x000870BF File Offset: 0x000852BF
		public override void Reset()
		{
			this._iCount = -1;
			this.charStart = null;
			this.bFallingBack = false;
		}

		// Token: 0x060025BA RID: 9658 RVA: 0x000870D8 File Offset: 0x000852D8
		private char TryBestFit(char cUnknown)
		{
			int num = 0;
			int num2 = this._oFallback._arrayBestFit.Length;
			int num3;
			while ((num3 = num2 - num) > 6)
			{
				int i = (num3 / 2 + num) & 65534;
				char c = this._oFallback._arrayBestFit[i];
				if (c == cUnknown)
				{
					return this._oFallback._arrayBestFit[i + 1];
				}
				if (c < cUnknown)
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
				if (this._oFallback._arrayBestFit[i] == cUnknown)
				{
					return this._oFallback._arrayBestFit[i + 1];
				}
			}
			return '\0';
		}

		// Token: 0x04001C73 RID: 7283
		private char _cBestFit;

		// Token: 0x04001C74 RID: 7284
		private InternalEncoderBestFitFallback _oFallback;

		// Token: 0x04001C75 RID: 7285
		private int _iCount = -1;

		// Token: 0x04001C76 RID: 7286
		private int _iSize;

		// Token: 0x04001C77 RID: 7287
		private static object s_InternalSyncObject;
	}
}
