using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006BA RID: 1722
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, Inherited = false)]
	[ComVisible(true)]
	public sealed class ClassInterfaceAttribute : Attribute
	{
		// Token: 0x06003FFE RID: 16382 RVA: 0x000E09C4 File Offset: 0x000DEBC4
		public ClassInterfaceAttribute(ClassInterfaceType classInterfaceType)
		{
			this._val = classInterfaceType;
		}

		// Token: 0x06003FFF RID: 16383 RVA: 0x000E09C4 File Offset: 0x000DEBC4
		public ClassInterfaceAttribute(short classInterfaceType)
		{
			this._val = (ClassInterfaceType)classInterfaceType;
		}

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x06004000 RID: 16384 RVA: 0x000E09D3 File Offset: 0x000DEBD3
		public ClassInterfaceType Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x040029BB RID: 10683
		internal ClassInterfaceType _val;
	}
}
