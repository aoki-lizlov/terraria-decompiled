using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020007BD RID: 1981
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	[Serializable]
	public sealed class DecimalConstantAttribute : Attribute
	{
		// Token: 0x06004589 RID: 17801 RVA: 0x000E51A8 File Offset: 0x000E33A8
		[CLSCompliant(false)]
		public DecimalConstantAttribute(byte scale, byte sign, uint hi, uint mid, uint low)
		{
			this._dec = new decimal((int)low, (int)mid, (int)hi, sign > 0, scale);
		}

		// Token: 0x0600458A RID: 17802 RVA: 0x000E51A8 File Offset: 0x000E33A8
		public DecimalConstantAttribute(byte scale, byte sign, int hi, int mid, int low)
		{
			this._dec = new decimal(low, mid, hi, sign > 0, scale);
		}

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x0600458B RID: 17803 RVA: 0x000E51C5 File Offset: 0x000E33C5
		public decimal Value
		{
			get
			{
				return this._dec;
			}
		}

		// Token: 0x04002CB8 RID: 11448
		private decimal _dec;
	}
}
