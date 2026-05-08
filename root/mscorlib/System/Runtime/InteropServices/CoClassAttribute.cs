using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006DC RID: 1756
	[AttributeUsage(AttributeTargets.Interface, Inherited = false)]
	[ComVisible(true)]
	public sealed class CoClassAttribute : Attribute
	{
		// Token: 0x06004049 RID: 16457 RVA: 0x000E0FA0 File Offset: 0x000DF1A0
		public CoClassAttribute(Type coClass)
		{
			this._CoClass = coClass;
		}

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x0600404A RID: 16458 RVA: 0x000E0FAF File Offset: 0x000DF1AF
		public Type CoClass
		{
			get
			{
				return this._CoClass;
			}
		}

		// Token: 0x04002A66 RID: 10854
		internal Type _CoClass;
	}
}
