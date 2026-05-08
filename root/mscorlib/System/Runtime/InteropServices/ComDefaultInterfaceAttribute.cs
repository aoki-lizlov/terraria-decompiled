using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006B8 RID: 1720
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	[ComVisible(true)]
	public sealed class ComDefaultInterfaceAttribute : Attribute
	{
		// Token: 0x06003FFC RID: 16380 RVA: 0x000E09AD File Offset: 0x000DEBAD
		public ComDefaultInterfaceAttribute(Type defaultInterface)
		{
			this._val = defaultInterface;
		}

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x06003FFD RID: 16381 RVA: 0x000E09BC File Offset: 0x000DEBBC
		public Type Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x040029B6 RID: 10678
		internal Type _val;
	}
}
