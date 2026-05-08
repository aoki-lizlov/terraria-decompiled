using System;

namespace System
{
	// Token: 0x020001A1 RID: 417
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class LoaderOptimizationAttribute : Attribute
	{
		// Token: 0x060013A2 RID: 5026 RVA: 0x0004F28D File Offset: 0x0004D48D
		public LoaderOptimizationAttribute(byte value)
		{
			this._val = value;
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x0004F29C File Offset: 0x0004D49C
		public LoaderOptimizationAttribute(LoaderOptimization value)
		{
			this._val = (byte)value;
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060013A4 RID: 5028 RVA: 0x0004F2AC File Offset: 0x0004D4AC
		public LoaderOptimization Value
		{
			get
			{
				return (LoaderOptimization)this._val;
			}
		}

		// Token: 0x0400133C RID: 4924
		private readonly byte _val;
	}
}
