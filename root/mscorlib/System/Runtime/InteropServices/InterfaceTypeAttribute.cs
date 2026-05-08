using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006B7 RID: 1719
	[AttributeUsage(AttributeTargets.Interface, Inherited = false)]
	[ComVisible(true)]
	public sealed class InterfaceTypeAttribute : Attribute
	{
		// Token: 0x06003FF9 RID: 16377 RVA: 0x000E0996 File Offset: 0x000DEB96
		public InterfaceTypeAttribute(ComInterfaceType interfaceType)
		{
			this._val = interfaceType;
		}

		// Token: 0x06003FFA RID: 16378 RVA: 0x000E0996 File Offset: 0x000DEB96
		public InterfaceTypeAttribute(short interfaceType)
		{
			this._val = (ComInterfaceType)interfaceType;
		}

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x06003FFB RID: 16379 RVA: 0x000E09A5 File Offset: 0x000DEBA5
		public ComInterfaceType Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x040029B5 RID: 10677
		internal ComInterfaceType _val;
	}
}
