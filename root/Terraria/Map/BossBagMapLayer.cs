using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using Terraria.UI;

namespace Terraria.Map
{
	// Token: 0x02000178 RID: 376
	public class BossBagMapLayer : IMapLayer
	{
		// Token: 0x06001E2C RID: 7724 RVA: 0x00503528 File Offset: 0x00501728
		public void Draw(ref MapOverlayDrawContext context, ref string text)
		{
			for (int i = 0; i < 400; i++)
			{
				WorldItem worldItem = Main.item[i];
				if (worldItem != null && worldItem.active && ItemID.Sets.BossBag[worldItem.type])
				{
					Main.instance.LoadItem(worldItem.type);
					RenderTarget2D renderTarget2D;
					if (Main.ItemMapIconRenderer.RequestAndTryGet(worldItem.type, out renderTarget2D) && context.Draw(renderTarget2D, worldItem.Center.ToTileCoordinates().ToVector2() + new Vector2(0.5f, 0.5f), Alignment.Center).IsMouseOver)
					{
						text = worldItem.Name;
					}
				}
			}
		}

		// Token: 0x06001E2D RID: 7725 RVA: 0x0000357B File Offset: 0x0000177B
		public BossBagMapLayer()
		{
		}
	}
}
