using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006BB RID: 1723
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false)]
	[ComVisible(true)]
	public sealed class ComVisibleAttribute : Attribute
	{
		// Token: 0x06004001 RID: 16385 RVA: 0x000E09DB File Offset: 0x000DEBDB
		public ComVisibleAttribute(bool visibility)
		{
			this._val = visibility;
		}

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x06004002 RID: 16386 RVA: 0x000E09EA File Offset: 0x000DEBEA
		public bool Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x040029BC RID: 10684
		internal bool _val;
	}
}
