using System;
using System.Runtime.Serialization;

namespace System.Text
{
	// Token: 0x02000365 RID: 869
	[Serializable]
	public sealed class DecoderFallbackException : ArgumentException
	{
		// Token: 0x06002569 RID: 9577 RVA: 0x00086113 File Offset: 0x00084313
		public DecoderFallbackException()
			: base("Value does not fall within the expected range.")
		{
			base.HResult = -2147024809;
		}

		// Token: 0x0600256A RID: 9578 RVA: 0x0008612B File Offset: 0x0008432B
		public DecoderFallbackException(string message)
			: base(message)
		{
			base.HResult = -2147024809;
		}

		// Token: 0x0600256B RID: 9579 RVA: 0x0008613F File Offset: 0x0008433F
		public DecoderFallbackException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2147024809;
		}

		// Token: 0x0600256C RID: 9580 RVA: 0x00086154 File Offset: 0x00084354
		public DecoderFallbackException(string message, byte[] bytesUnknown, int index)
			: base(message)
		{
			this._bytesUnknown = bytesUnknown;
			this._index = index;
		}

		// Token: 0x0600256D RID: 9581 RVA: 0x00018A9F File Offset: 0x00016C9F
		private DecoderFallbackException(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x0600256E RID: 9582 RVA: 0x0008616B File Offset: 0x0008436B
		public byte[] BytesUnknown
		{
			get
			{
				return this._bytesUnknown;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x0600256F RID: 9583 RVA: 0x00086173 File Offset: 0x00084373
		public int Index
		{
			get
			{
				return this._index;
			}
		}

		// Token: 0x04001C61 RID: 7265
		private byte[] _bytesUnknown;

		// Token: 0x04001C62 RID: 7266
		private int _index;
	}
}
