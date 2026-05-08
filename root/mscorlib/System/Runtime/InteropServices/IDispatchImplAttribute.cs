using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006C3 RID: 1731
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, Inherited = false)]
	[Obsolete("This attribute is deprecated and will be removed in a future version.", false)]
	[ComVisible(true)]
	public sealed class IDispatchImplAttribute : Attribute
	{
		// Token: 0x0600400D RID: 16397 RVA: 0x000E0A53 File Offset: 0x000DEC53
		public IDispatchImplAttribute(IDispatchImplType implType)
		{
			this._val = implType;
		}

		// Token: 0x0600400E RID: 16398 RVA: 0x000E0A53 File Offset: 0x000DEC53
		public IDispatchImplAttribute(short implType)
		{
			this._val = (IDispatchImplType)implType;
		}

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x0600400F RID: 16399 RVA: 0x000E0A62 File Offset: 0x000DEC62
		public IDispatchImplType Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x040029C5 RID: 10693
		internal IDispatchImplType _val;
	}
}
