using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006CF RID: 1743
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false)]
	[ComVisible(true)]
	public sealed class GuidAttribute : Attribute
	{
		// Token: 0x06004023 RID: 16419 RVA: 0x000E0BDA File Offset: 0x000DEDDA
		public GuidAttribute(string guid)
		{
			this._val = guid;
		}

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x06004024 RID: 16420 RVA: 0x000E0BE9 File Offset: 0x000DEDE9
		public string Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002A49 RID: 10825
		internal string _val;
	}
}
