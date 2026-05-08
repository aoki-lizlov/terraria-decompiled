using System;
using System.Runtime.InteropServices;

namespace Internal.Cryptography
{
	// Token: 0x02000B65 RID: 2917
	internal struct PinAndClear : IDisposable
	{
		// Token: 0x06006AD1 RID: 27345 RVA: 0x0016F108 File Offset: 0x0016D308
		internal static PinAndClear Track(byte[] data)
		{
			return new PinAndClear
			{
				_gcHandle = GCHandle.Alloc(data, GCHandleType.Pinned),
				_data = data
			};
		}

		// Token: 0x06006AD2 RID: 27346 RVA: 0x0016F134 File Offset: 0x0016D334
		public void Dispose()
		{
			Array.Clear(this._data, 0, this._data.Length);
			this._gcHandle.Free();
		}

		// Token: 0x04003D88 RID: 15752
		private byte[] _data;

		// Token: 0x04003D89 RID: 15753
		private GCHandle _gcHandle;
	}
}
