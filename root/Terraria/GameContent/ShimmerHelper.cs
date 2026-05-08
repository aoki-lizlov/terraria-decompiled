using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent
{
	// Token: 0x02000248 RID: 584
	public class ShimmerHelper
	{
		// Token: 0x060022F9 RID: 8953 RVA: 0x0053BED8 File Offset: 0x0053A0D8
		public static Vector2? FindSpotWithoutShimmer(Entity entity, int startX, int startY, int expand, bool allowSolidTop)
		{
			Vector2 vector = new Vector2((float)(-(float)entity.width / 2), (float)(-(float)entity.height));
			for (int i = 0; i < expand; i++)
			{
				float num = (float)(startX - i);
				int num2 = startY - expand;
				Vector2 vector2 = new Vector2(num * (float)16, (float)(num2 * 16)) + vector;
				if (ShimmerHelper.IsSpotShimmerFree(entity, vector2, allowSolidTop))
				{
					return new Vector2?(vector2);
				}
				vector2 = new Vector2((float)((startX + i) * 16), (float)(num2 * 16)) + vector;
				if (ShimmerHelper.IsSpotShimmerFree(entity, vector2, allowSolidTop))
				{
					return new Vector2?(vector2);
				}
				float num3 = (float)(startX - i);
				num2 = startY + expand;
				vector2 = new Vector2(num3 * (float)16, (float)(num2 * 16)) + vector;
				if (ShimmerHelper.IsSpotShimmerFree(entity, vector2, allowSolidTop))
				{
					return new Vector2?(vector2);
				}
				vector2 = new Vector2((float)((startX + i) * 16), (float)(num2 * 16)) + vector;
				if (ShimmerHelper.IsSpotShimmerFree(entity, vector2, allowSolidTop))
				{
					return new Vector2?(vector2);
				}
			}
			for (int j = 0; j < expand; j++)
			{
				float num4 = (float)(startX - expand);
				int num5 = startY - j;
				Vector2 vector3 = new Vector2(num4 * (float)16, (float)(num5 * 16)) + vector;
				if (ShimmerHelper.IsSpotShimmerFree(entity, vector3, allowSolidTop))
				{
					return new Vector2?(vector3);
				}
				vector3 = new Vector2((float)((startX + expand) * 16), (float)(num5 * 16)) + vector;
				if (ShimmerHelper.IsSpotShimmerFree(entity, vector3, allowSolidTop))
				{
					return new Vector2?(vector3);
				}
				float num6 = (float)(startX - expand);
				num5 = startY + j;
				vector3 = new Vector2(num6 * (float)16, (float)(num5 * 16)) + vector;
				if (ShimmerHelper.IsSpotShimmerFree(entity, vector3, allowSolidTop))
				{
					return new Vector2?(vector3);
				}
				vector3 = new Vector2((float)((startX + expand) * 16), (float)(num5 * 16)) + vector;
				if (ShimmerHelper.IsSpotShimmerFree(entity, vector3, allowSolidTop))
				{
					return new Vector2?(vector3);
				}
			}
			return null;
		}

		// Token: 0x060022FA RID: 8954 RVA: 0x0053C0A4 File Offset: 0x0053A2A4
		private static bool IsSpotShimmerFree(Entity entity, Vector2 landingPosition, bool allowSolidTop)
		{
			return !Collision.SolidCollision(landingPosition, entity.width, entity.height) && Collision.SolidCollision(landingPosition + new Vector2(0f, (float)entity.height), entity.width, 100, allowSolidTop) && (!Collision.WetCollision(landingPosition, entity.width, entity.height + 100) || !Collision.shimmer);
		}

		// Token: 0x060022FB RID: 8955 RVA: 0x0000357B File Offset: 0x0000177B
		public ShimmerHelper()
		{
		}
	}
}
