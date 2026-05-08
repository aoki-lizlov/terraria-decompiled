using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000696 RID: 1686
	public readonly struct HandleRef
	{
		// Token: 0x06003F6D RID: 16237 RVA: 0x000DF873 File Offset: 0x000DDA73
		public HandleRef(object wrapper, IntPtr handle)
		{
			this._wrapper = wrapper;
			this._handle = handle;
		}

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x06003F6E RID: 16238 RVA: 0x000DF883 File Offset: 0x000DDA83
		public object Wrapper
		{
			get
			{
				return this._wrapper;
			}
		}

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x06003F6F RID: 16239 RVA: 0x000DF88B File Offset: 0x000DDA8B
		public IntPtr Handle
		{
			get
			{
				return this._handle;
			}
		}

		// Token: 0x06003F70 RID: 16240 RVA: 0x000DF88B File Offset: 0x000DDA8B
		public static explicit operator IntPtr(HandleRef value)
		{
			return value._handle;
		}

		// Token: 0x06003F71 RID: 16241 RVA: 0x000DF88B File Offset: 0x000DDA8B
		public static IntPtr ToIntPtr(HandleRef value)
		{
			return value._handle;
		}

		// Token: 0x04002977 RID: 10615
		private readonly object _wrapper;

		// Token: 0x04002978 RID: 10616
		private readonly IntPtr _handle;
	}
}
