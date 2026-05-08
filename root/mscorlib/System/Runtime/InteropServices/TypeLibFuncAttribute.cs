using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006CA RID: 1738
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	[ComVisible(true)]
	public sealed class TypeLibFuncAttribute : Attribute
	{
		// Token: 0x0600401A RID: 16410 RVA: 0x000E0B84 File Offset: 0x000DED84
		public TypeLibFuncAttribute(TypeLibFuncFlags flags)
		{
			this._val = flags;
		}

		// Token: 0x0600401B RID: 16411 RVA: 0x000E0B84 File Offset: 0x000DED84
		public TypeLibFuncAttribute(short flags)
		{
			this._val = (TypeLibFuncFlags)flags;
		}

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x0600401C RID: 16412 RVA: 0x000E0B93 File Offset: 0x000DED93
		public TypeLibFuncFlags Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x040029F3 RID: 10739
		internal TypeLibFuncFlags _val;
	}
}
