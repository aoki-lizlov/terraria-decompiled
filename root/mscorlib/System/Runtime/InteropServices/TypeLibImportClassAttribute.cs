using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006BC RID: 1724
	[AttributeUsage(AttributeTargets.Interface, Inherited = false)]
	[ComVisible(true)]
	public sealed class TypeLibImportClassAttribute : Attribute
	{
		// Token: 0x06004003 RID: 16387 RVA: 0x000E09F2 File Offset: 0x000DEBF2
		public TypeLibImportClassAttribute(Type importClass)
		{
			this._importClassName = importClass.ToString();
		}

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x06004004 RID: 16388 RVA: 0x000E0A06 File Offset: 0x000DEC06
		public string Value
		{
			get
			{
				return this._importClassName;
			}
		}

		// Token: 0x040029BD RID: 10685
		internal string _importClassName;
	}
}
