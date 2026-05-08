using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006CB RID: 1739
	[AttributeUsage(AttributeTargets.Field, Inherited = false)]
	[ComVisible(true)]
	public sealed class TypeLibVarAttribute : Attribute
	{
		// Token: 0x0600401D RID: 16413 RVA: 0x000E0B9B File Offset: 0x000DED9B
		public TypeLibVarAttribute(TypeLibVarFlags flags)
		{
			this._val = flags;
		}

		// Token: 0x0600401E RID: 16414 RVA: 0x000E0B9B File Offset: 0x000DED9B
		public TypeLibVarAttribute(short flags)
		{
			this._val = (TypeLibVarFlags)flags;
		}

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x0600401F RID: 16415 RVA: 0x000E0BAA File Offset: 0x000DEDAA
		public TypeLibVarFlags Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x040029F4 RID: 10740
		internal TypeLibVarFlags _val;
	}
}
