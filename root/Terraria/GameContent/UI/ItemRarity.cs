using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Terraria.GameContent.UI
{
	// Token: 0x0200037D RID: 893
	public class ItemRarity
	{
		// Token: 0x06002992 RID: 10642 RVA: 0x0057DA14 File Offset: 0x0057BC14
		public static void Initialize()
		{
			ItemRarity._rarities.Clear();
			ItemRarity._rarities.Add(-11, Colors.RarityAmber);
			ItemRarity._rarities.Add(-1, Colors.RarityTrash);
			ItemRarity._rarities.Add(1, Colors.RarityBlue);
			ItemRarity._rarities.Add(2, Colors.RarityGreen);
			ItemRarity._rarities.Add(3, Colors.RarityOrange);
			ItemRarity._rarities.Add(4, Colors.RarityRed);
			ItemRarity._rarities.Add(5, Colors.RarityPink);
			ItemRarity._rarities.Add(6, Colors.RarityPurple);
			ItemRarity._rarities.Add(7, Colors.RarityLime);
			ItemRarity._rarities.Add(8, Colors.RarityYellow);
			ItemRarity._rarities.Add(9, Colors.RarityCyan);
		}

		// Token: 0x06002993 RID: 10643 RVA: 0x0057DAE0 File Offset: 0x0057BCE0
		public static Color GetColor(int rarity)
		{
			Color color = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
			if (ItemRarity._rarities.ContainsKey(rarity))
			{
				return ItemRarity._rarities[rarity];
			}
			return color;
		}

		// Token: 0x06002994 RID: 10644 RVA: 0x0000357B File Offset: 0x0000177B
		public ItemRarity()
		{
		}

		// Token: 0x06002995 RID: 10645 RVA: 0x0057DB22 File Offset: 0x0057BD22
		// Note: this type is marked as 'beforefieldinit'.
		static ItemRarity()
		{
		}

		// Token: 0x040052A0 RID: 21152
		private static Dictionary<int, Color> _rarities = new Dictionary<int, Color>();
	}
}
