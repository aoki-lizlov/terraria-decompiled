using System;
using System.Runtime.Serialization;

namespace System.Text
{
	// Token: 0x02000370 RID: 880
	[Serializable]
	public sealed class EncoderFallbackException : ArgumentException
	{
		// Token: 0x060025C6 RID: 9670 RVA: 0x00086113 File Offset: 0x00084313
		public EncoderFallbackException()
			: base("Value does not fall within the expected range.")
		{
			base.HResult = -2147024809;
		}

		// Token: 0x060025C7 RID: 9671 RVA: 0x0008612B File Offset: 0x0008432B
		public EncoderFallbackException(string message)
			: base(message)
		{
			base.HResult = -2147024809;
		}

		// Token: 0x060025C8 RID: 9672 RVA: 0x0008613F File Offset: 0x0008433F
		public EncoderFallbackException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2147024809;
		}

		// Token: 0x060025C9 RID: 9673 RVA: 0x00087249 File Offset: 0x00085449
		internal EncoderFallbackException(string message, char charUnknown, int index)
			: base(message)
		{
			this._charUnknown = charUnknown;
			this._index = index;
		}

		// Token: 0x060025CA RID: 9674 RVA: 0x00087260 File Offset: 0x00085460
		internal EncoderFallbackException(string message, char charUnknownHigh, char charUnknownLow, int index)
			: base(message)
		{
			if (!char.IsHighSurrogate(charUnknownHigh))
			{
				throw new ArgumentOutOfRangeException("charUnknownHigh", SR.Format("Valid values are between {0} and {1}, inclusive.", 55296, 56319));
			}
			if (!char.IsLowSurrogate(charUnknownLow))
			{
				throw new ArgumentOutOfRangeException("CharUnknownLow", SR.Format("Valid values are between {0} and {1}, inclusive.", 56320, 57343));
			}
			this._charUnknownHigh = charUnknownHigh;
			this._charUnknownLow = charUnknownLow;
			this._index = index;
		}

		// Token: 0x060025CB RID: 9675 RVA: 0x00018A9F File Offset: 0x00016C9F
		private EncoderFallbackException(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x060025CC RID: 9676 RVA: 0x000872EC File Offset: 0x000854EC
		public char CharUnknown
		{
			get
			{
				return this._charUnknown;
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x060025CD RID: 9677 RVA: 0x000872F4 File Offset: 0x000854F4
		public char CharUnknownHigh
		{
			get
			{
				return this._charUnknownHigh;
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x060025CE RID: 9678 RVA: 0x000872FC File Offset: 0x000854FC
		public char CharUnknownLow
		{
			get
			{
				return this._charUnknownLow;
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x060025CF RID: 9679 RVA: 0x00087304 File Offset: 0x00085504
		public int Index
		{
			get
			{
				return this._index;
			}
		}

		// Token: 0x060025D0 RID: 9680 RVA: 0x0008730C File Offset: 0x0008550C
		public bool IsUnknownSurrogate()
		{
			return this._charUnknownHigh > '\0';
		}

		// Token: 0x04001C78 RID: 7288
		private char _charUnknown;

		// Token: 0x04001C79 RID: 7289
		private char _charUnknownHigh;

		// Token: 0x04001C7A RID: 7290
		private char _charUnknownLow;

		// Token: 0x04001C7B RID: 7291
		private int _index;
	}
}
