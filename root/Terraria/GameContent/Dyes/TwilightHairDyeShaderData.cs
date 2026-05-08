using System;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;

namespace Terraria.GameContent.Dyes
{
	// Token: 0x0200028F RID: 655
	public class TwilightHairDyeShaderData : HairShaderData
	{
		// Token: 0x0600252D RID: 9517 RVA: 0x005539A3 File Offset: 0x00551BA3
		public TwilightHairDyeShaderData(Asset<Effect> shader, string passName)
			: base(shader, passName)
		{
		}

		// Token: 0x0600252E RID: 9518 RVA: 0x005539AD File Offset: 0x00551BAD
		public override void Apply(Player player, DrawData? drawData = null)
		{
			if (drawData != null)
			{
				base.UseTargetPosition(Main.screenPosition + drawData.Value.position);
			}
			base.Apply(player, drawData);
		}
	}
}
