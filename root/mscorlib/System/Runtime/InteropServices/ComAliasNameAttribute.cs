using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006D9 RID: 1753
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = false)]
	[ComVisible(true)]
	public sealed class ComAliasNameAttribute : Attribute
	{
		// Token: 0x06004042 RID: 16450 RVA: 0x000E0F4C File Offset: 0x000DF14C
		public ComAliasNameAttribute(string alias)
		{
			this._val = alias;
		}

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x06004043 RID: 16451 RVA: 0x000E0F5B File Offset: 0x000DF15B
		public string Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002A62 RID: 10850
		internal string _val;
	}
}
