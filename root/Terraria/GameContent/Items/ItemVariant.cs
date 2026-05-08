using System;
using Terraria.Localization;

namespace Terraria.GameContent.Items
{
	// Token: 0x02000475 RID: 1141
	public class ItemVariant
	{
		// Token: 0x06003318 RID: 13080 RVA: 0x005F3629 File Offset: 0x005F1829
		public ItemVariant(NetworkText description)
		{
			this.Description = description;
		}

		// Token: 0x06003319 RID: 13081 RVA: 0x005F3638 File Offset: 0x005F1838
		public override string ToString()
		{
			return this.Description.ToString();
		}

		// Token: 0x04005884 RID: 22660
		public readonly NetworkText Description;
	}
}
