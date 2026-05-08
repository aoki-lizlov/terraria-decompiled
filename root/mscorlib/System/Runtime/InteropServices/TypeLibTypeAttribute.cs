using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006C9 RID: 1737
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface, Inherited = false)]
	[ComVisible(true)]
	public sealed class TypeLibTypeAttribute : Attribute
	{
		// Token: 0x06004017 RID: 16407 RVA: 0x000E0B6D File Offset: 0x000DED6D
		public TypeLibTypeAttribute(TypeLibTypeFlags flags)
		{
			this._val = flags;
		}

		// Token: 0x06004018 RID: 16408 RVA: 0x000E0B6D File Offset: 0x000DED6D
		public TypeLibTypeAttribute(short flags)
		{
			this._val = (TypeLibTypeFlags)flags;
		}

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x06004019 RID: 16409 RVA: 0x000E0B7C File Offset: 0x000DED7C
		public TypeLibTypeFlags Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x040029F2 RID: 10738
		internal TypeLibTypeFlags _val;
	}
}
