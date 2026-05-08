using System;
using Microsoft.Xna.Framework;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.Map
{
	// Token: 0x0200017F RID: 383
	public class SpawnMapLayer : IMapLayer
	{
		// Token: 0x06001E46 RID: 7750 RVA: 0x00504438 File Offset: 0x00502638
		public void Draw(ref MapOverlayDrawContext context, ref string text)
		{
			Player localPlayer = Main.LocalPlayer;
			Vector2 vector = new Vector2((float)localPlayer.SpawnX + 0.5f, (float)localPlayer.SpawnY);
			Vector2 vector2 = new Vector2((float)Main.spawnTileX + 0.5f, (float)Main.spawnTileY);
			if (!Main.teamBasedSpawnsSeed && context.Draw(TextureAssets.SpawnPoint.Value, vector2, Alignment.Bottom).IsMouseOver)
			{
				text = Language.GetTextValue("UI.SpawnPoint");
			}
			if (localPlayer.SpawnX == -1)
			{
				return;
			}
			if (context.Draw(TextureAssets.SpawnBed.Value, vector, Alignment.Bottom).IsMouseOver)
			{
				text = Language.GetTextValue("UI.SpawnBed");
			}
		}

		// Token: 0x06001E47 RID: 7751 RVA: 0x0000357B File Offset: 0x0000177B
		public SpawnMapLayer()
		{
		}
	}
}
