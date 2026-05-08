using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006E1 RID: 1761
	[AttributeUsage(AttributeTargets.Module, Inherited = false)]
	[ComVisible(true)]
	public sealed class DefaultCharSetAttribute : Attribute
	{
		// Token: 0x06004058 RID: 16472 RVA: 0x000E105F File Offset: 0x000DF25F
		public DefaultCharSetAttribute(CharSet charSet)
		{
			this._CharSet = charSet;
		}

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x06004059 RID: 16473 RVA: 0x000E106E File Offset: 0x000DF26E
		public CharSet CharSet
		{
			get
			{
				return this._CharSet;
			}
		}

		// Token: 0x04002A71 RID: 10865
		internal CharSet _CharSet;
	}
}
