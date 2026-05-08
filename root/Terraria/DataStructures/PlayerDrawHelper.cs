using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Shaders;

namespace Terraria.DataStructures
{
	// Token: 0x0200059A RID: 1434
	public class PlayerDrawHelper
	{
		// Token: 0x0600387D RID: 14461 RVA: 0x00632B40 File Offset: 0x00630D40
		public static int PackShader(int localShaderIndex, PlayerDrawHelper.ShaderConfiguration shaderType)
		{
			return (int)(localShaderIndex + shaderType * (PlayerDrawHelper.ShaderConfiguration)1000);
		}

		// Token: 0x0600387E RID: 14462 RVA: 0x00632B4B File Offset: 0x00630D4B
		public static void UnpackShader(int packedShaderIndex, out int localShaderIndex, out PlayerDrawHelper.ShaderConfiguration shaderType)
		{
			shaderType = (PlayerDrawHelper.ShaderConfiguration)(packedShaderIndex / 1000);
			localShaderIndex = packedShaderIndex % 1000;
		}

		// Token: 0x0600387F RID: 14463 RVA: 0x00632B60 File Offset: 0x00630D60
		public static void SetShaderForData(Player player, int cHead, ref DrawData cdd)
		{
			int num;
			PlayerDrawHelper.ShaderConfiguration shaderConfiguration;
			PlayerDrawHelper.UnpackShader(cdd.shader, out num, out shaderConfiguration);
			switch (shaderConfiguration)
			{
			case PlayerDrawHelper.ShaderConfiguration.ArmorShader:
				GameShaders.Hair.Apply(0, player, new DrawData?(cdd));
				GameShaders.Armor.Apply(num, player, new DrawData?(cdd));
				return;
			case PlayerDrawHelper.ShaderConfiguration.HairShader:
				if (player.head == 0)
				{
					GameShaders.Hair.Apply(0, player, new DrawData?(cdd));
					GameShaders.Armor.Apply(cHead, player, new DrawData?(cdd));
					return;
				}
				GameShaders.Armor.Apply(0, player, new DrawData?(cdd));
				GameShaders.Hair.Apply((short)num, player, new DrawData?(cdd));
				return;
			case PlayerDrawHelper.ShaderConfiguration.TileShader:
				Main.tileShader.CurrentTechnique.Passes[num].Apply();
				return;
			case PlayerDrawHelper.ShaderConfiguration.TilePaintID:
			{
				int num2 = Main.ConvertPaintIdToTileShaderIndex(num, false, false);
				Main.tileShader.CurrentTechnique.Passes[num2].Apply();
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x06003880 RID: 14464 RVA: 0x0000357B File Offset: 0x0000177B
		public PlayerDrawHelper()
		{
		}

		// Token: 0x06003881 RID: 14465 RVA: 0x00632C67 File Offset: 0x00630E67
		// Note: this type is marked as 'beforefieldinit'.
		static PlayerDrawHelper()
		{
		}

		// Token: 0x04005CA3 RID: 23715
		public static Color DISPLAY_DOLL_DEFAULT_SKIN_COLOR = new Color(163, 121, 92);

		// Token: 0x020009C2 RID: 2498
		public enum ShaderConfiguration
		{
			// Token: 0x040076DE RID: 30430
			ArmorShader,
			// Token: 0x040076DF RID: 30431
			HairShader,
			// Token: 0x040076E0 RID: 30432
			TileShader,
			// Token: 0x040076E1 RID: 30433
			TilePaintID
		}
	}
}
