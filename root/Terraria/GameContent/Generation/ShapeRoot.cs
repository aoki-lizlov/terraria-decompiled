using System;
using Microsoft.Xna.Framework;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Generation
{
	// Token: 0x0200048B RID: 1163
	public class ShapeRoot : GenShape
	{
		// Token: 0x0600336D RID: 13165 RVA: 0x005F6CA0 File Offset: 0x005F4EA0
		public ShapeRoot(double angle, double distance = 10.0, double startingSize = 4.0, double endingSize = 1.0)
		{
			this._angle = angle;
			this._distance = distance;
			this._startingSize = startingSize;
			this._endingSize = endingSize;
		}

		// Token: 0x0600336E RID: 13166 RVA: 0x005F6CC8 File Offset: 0x005F4EC8
		private bool DoRoot(Point origin, GenAction action, double angle, double distance, double startingSize)
		{
			double num = (double)origin.X;
			double num2 = (double)origin.Y;
			for (double num3 = 0.0; num3 < distance * 0.85; num3 += 1.0)
			{
				double num4 = num3 / distance;
				double num5 = Utils.Lerp(startingSize, this._endingSize, num4);
				num += Math.Cos(angle);
				num2 += Math.Sin(angle);
				angle += (double)GenBase._random.NextFloat() - 0.5 + (double)GenBase._random.NextFloat() * (this._angle - 1.5707963705062866) * 0.1 * (1.0 - num4);
				angle = angle * 0.4 + 0.45 * Utils.Clamp<double>(angle, this._angle - 2.0 * (1.0 - 0.5 * num4), this._angle + 2.0 * (1.0 - 0.5 * num4)) + Utils.Lerp(this._angle, 1.5707963705062866, num4) * 0.15;
				for (int i = 0; i < (int)num5; i++)
				{
					for (int j = 0; j < (int)num5; j++)
					{
						if (!base.UnitApply(action, origin, (int)num + i, (int)num2 + j, new object[0]) && this._quitOnFail)
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x0600336F RID: 13167 RVA: 0x005F6E58 File Offset: 0x005F5058
		public override bool Perform(Point origin, GenAction action)
		{
			return this.DoRoot(origin, action, this._angle, this._distance, this._startingSize);
		}

		// Token: 0x040058D9 RID: 22745
		private double _angle;

		// Token: 0x040058DA RID: 22746
		private double _startingSize;

		// Token: 0x040058DB RID: 22747
		private double _endingSize;

		// Token: 0x040058DC RID: 22748
		private double _distance;
	}
}
