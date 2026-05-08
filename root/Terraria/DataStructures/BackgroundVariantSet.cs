using System;

namespace Terraria.DataStructures
{
	// Token: 0x0200053A RID: 1338
	public class BackgroundVariantSet
	{
		// Token: 0x06003758 RID: 14168 RVA: 0x0062ECB1 File Offset: 0x0062CEB1
		public void Clear()
		{
			this.Pure.Clear();
			this.Corrupt.Clear();
			this.Crimson.Clear();
			this.Hallow.Clear();
		}

		// Token: 0x06003759 RID: 14169 RVA: 0x0062ECDF File Offset: 0x0062CEDF
		public BackgroundVariantSet()
		{
		}

		// Token: 0x04005B8E RID: 23438
		public BackgroundVariant Pure = new BackgroundVariant();

		// Token: 0x04005B8F RID: 23439
		public BackgroundVariant Corrupt = new BackgroundVariant();

		// Token: 0x04005B90 RID: 23440
		public BackgroundVariant Crimson = new BackgroundVariant();

		// Token: 0x04005B91 RID: 23441
		public BackgroundVariant Hallow = new BackgroundVariant();
	}
}
