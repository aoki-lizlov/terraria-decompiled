using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x020001AF RID: 431
	internal ref struct ByReference<T>
	{
		// Token: 0x0600148A RID: 5258 RVA: 0x00047E00 File Offset: 0x00046000
		[Intrinsic]
		public ByReference(ref T value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x0600148B RID: 5259 RVA: 0x00047E00 File Offset: 0x00046000
		public ref T Value
		{
			[Intrinsic]
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x04001356 RID: 4950
		private IntPtr _value;
	}
}
