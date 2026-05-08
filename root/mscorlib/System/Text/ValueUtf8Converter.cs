using System;
using System.Buffers;

namespace System.Text
{
	// Token: 0x0200038B RID: 907
	internal ref struct ValueUtf8Converter
	{
		// Token: 0x0600274C RID: 10060 RVA: 0x000904AE File Offset: 0x0008E6AE
		public ValueUtf8Converter(Span<byte> initialBuffer)
		{
			this._arrayToReturnToPool = null;
			this._bytes = initialBuffer;
		}

		// Token: 0x0600274D RID: 10061 RVA: 0x000904C0 File Offset: 0x0008E6C0
		public unsafe Span<byte> ConvertAndTerminateString(ReadOnlySpan<char> value)
		{
			int num = Encoding.UTF8.GetMaxByteCount(value.Length) + 1;
			if (this._bytes.Length < num)
			{
				this.Dispose();
				this._arrayToReturnToPool = ArrayPool<byte>.Shared.Rent(num);
				this._bytes = new Span<byte>(this._arrayToReturnToPool);
			}
			int bytes = Encoding.UTF8.GetBytes(value, this._bytes);
			*this._bytes[bytes] = 0;
			return this._bytes.Slice(0, bytes + 1);
		}

		// Token: 0x0600274E RID: 10062 RVA: 0x00090548 File Offset: 0x0008E748
		public void Dispose()
		{
			byte[] arrayToReturnToPool = this._arrayToReturnToPool;
			if (arrayToReturnToPool != null)
			{
				this._arrayToReturnToPool = null;
				ArrayPool<byte>.Shared.Return(arrayToReturnToPool, false);
			}
		}

		// Token: 0x04001CDD RID: 7389
		private byte[] _arrayToReturnToPool;

		// Token: 0x04001CDE RID: 7390
		private Span<byte> _bytes;
	}
}
