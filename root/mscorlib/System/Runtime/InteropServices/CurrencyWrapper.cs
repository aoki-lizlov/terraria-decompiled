using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006A4 RID: 1700
	public sealed class CurrencyWrapper
	{
		// Token: 0x06003FC0 RID: 16320 RVA: 0x000E0666 File Offset: 0x000DE866
		public CurrencyWrapper(decimal obj)
		{
			this.m_WrappedObject = obj;
		}

		// Token: 0x06003FC1 RID: 16321 RVA: 0x000E0675 File Offset: 0x000DE875
		public CurrencyWrapper(object obj)
		{
			if (!(obj is decimal))
			{
				throw new ArgumentException("Object must be of type Decimal.", "obj");
			}
			this.m_WrappedObject = (decimal)obj;
		}

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x06003FC2 RID: 16322 RVA: 0x000E06A1 File Offset: 0x000DE8A1
		public decimal WrappedObject
		{
			get
			{
				return this.m_WrappedObject;
			}
		}

		// Token: 0x0400299E RID: 10654
		private decimal m_WrappedObject;
	}
}
