using System;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;

namespace Terraria.GameContent.Dyes
{
	// Token: 0x02000290 RID: 656
	public class TwilightDyeShaderData : ArmorShaderData
	{
		// Token: 0x0600252F RID: 9519 RVA: 0x005539DD File Offset: 0x00551BDD
		public TwilightDyeShaderData(Asset<Effect> shader, string passName)
			: base(shader, passName)
		{
		}

		// Token: 0x06002530 RID: 9520 RVA: 0x005539E8 File Offset: 0x00551BE8
		public override void Apply(Entity entity, DrawData? drawData)
		{
			if (drawData != null)
			{
				Player player = entity as Player;
				if (player != null && !player.isDisplayDollOrInanimate && !player.isHatRackDoll)
				{
					base.UseTargetPosition(Main.screenPosition + drawData.Value.position);
				}
				else if (entity is Projectile)
				{
					base.UseTargetPosition(Main.screenPosition + drawData.Value.position);
				}
				else
				{
					base.UseTargetPosition(drawData.Value.position);
				}
			}
			base.Apply(entity, drawData);
		}
	}
}
