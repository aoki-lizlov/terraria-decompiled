using System;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Generation
{
	// Token: 0x0200048D RID: 1165
	public class ShapeRunner : GenShape
	{
		// Token: 0x06003382 RID: 13186 RVA: 0x005F7C3C File Offset: 0x005F5E3C
		public ShapeRunner(double strength, int steps, Vector2D velocity)
		{
			this._startStrength = strength;
			this._steps = steps;
			this._startVelocity = velocity;
		}

		// Token: 0x06003383 RID: 13187 RVA: 0x005F7C5C File Offset: 0x005F5E5C
		public override bool Perform(Point origin, GenAction action)
		{
			double num = (double)this._steps;
			double num2 = (double)this._steps;
			double num3 = this._startStrength;
			Vector2D vector2D;
			vector2D..ctor((double)origin.X, (double)origin.Y);
			Vector2D vector2D2 = ((this._startVelocity == Vector2D.Zero) ? Utils.RandomVector2D(GenBase._random, -1.0, 1.0) : this._startVelocity);
			while (num > 0.0 && num3 > 0.0)
			{
				num3 = this._startStrength * (num / num2);
				num -= 1.0;
				int num4 = Math.Max(1, (int)(vector2D.X - num3 * 0.5));
				int num5 = Math.Max(1, (int)(vector2D.Y - num3 * 0.5));
				int num6 = Math.Min(GenBase._worldWidth, (int)(vector2D.X + num3 * 0.5));
				int num7 = Math.Min(GenBase._worldHeight, (int)(vector2D.Y + num3 * 0.5));
				for (int i = num4; i < num6; i++)
				{
					for (int j = num5; j < num7; j++)
					{
						if (Math.Abs((double)i - vector2D.X) + Math.Abs((double)j - vector2D.Y) < num3 * 0.5 * (1.0 + (double)GenBase._random.Next(-10, 11) * 0.015))
						{
							base.UnitApply(action, origin, i, j, new object[0]);
						}
					}
				}
				int num8 = (int)(num3 / 50.0) + 1;
				num -= (double)num8;
				vector2D += vector2D2;
				for (int k = 0; k < num8; k++)
				{
					vector2D += vector2D2;
					vector2D2 += Utils.RandomVector2D(GenBase._random, -0.5, 0.5);
				}
				vector2D2 += Utils.RandomVector2D(GenBase._random, -0.5, 0.5);
				vector2D2 = Vector2D.Clamp(vector2D2, -Vector2D.One, Vector2D.One);
			}
			return true;
		}

		// Token: 0x040058E4 RID: 22756
		private double _startStrength;

		// Token: 0x040058E5 RID: 22757
		private int _steps;

		// Token: 0x040058E6 RID: 22758
		private Vector2D _startVelocity;
	}
}
