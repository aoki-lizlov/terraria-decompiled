using System;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;

namespace Terraria.GameContent.Dyes
{
	// Token: 0x02000292 RID: 658
	public class TeamArmorShaderData : ArmorShaderData
	{
		// Token: 0x06002534 RID: 9524 RVA: 0x00553AD8 File Offset: 0x00551CD8
		public TeamArmorShaderData(Asset<Effect> shader, string passName)
			: base(shader, passName)
		{
			if (!TeamArmorShaderData.isInitialized)
			{
				TeamArmorShaderData.isInitialized = true;
				TeamArmorShaderData.dustShaderData = new ArmorShaderData[Main.teamColor.Length];
				for (int i = 1; i < Main.teamColor.Length; i++)
				{
					TeamArmorShaderData.dustShaderData[i] = new ArmorShaderData(shader, passName).UseColor(Main.teamColor[i]);
				}
				TeamArmorShaderData.dustShaderData[0] = new ArmorShaderData(shader, "Default");
			}
		}

		// Token: 0x06002535 RID: 9525 RVA: 0x00553B50 File Offset: 0x00551D50
		public override void Apply(Entity entity, DrawData? drawData)
		{
			Player player = entity as Player;
			if (player == null)
			{
				Projectile projectile = entity as Projectile;
				if (projectile != null && projectile.OwnedBySomeone)
				{
					player = Main.player[projectile.owner];
				}
			}
			if (player == null || player.team == 0 || Main.netMode == 0)
			{
				TeamArmorShaderData.dustShaderData[0].Apply(player, drawData);
				return;
			}
			base.UseColor(Main.teamColor[player.team]);
			base.Apply(player, drawData);
		}

		// Token: 0x06002536 RID: 9526 RVA: 0x00553BC8 File Offset: 0x00551DC8
		public override ArmorShaderData GetSecondaryShader(Entity entity)
		{
			Player player = entity as Player;
			return TeamArmorShaderData.dustShaderData[player.team];
		}

		// Token: 0x04004F90 RID: 20368
		private static bool isInitialized;

		// Token: 0x04004F91 RID: 20369
		private static ArmorShaderData[] dustShaderData;
	}
}
