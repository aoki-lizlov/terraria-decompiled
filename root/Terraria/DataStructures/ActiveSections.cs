using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Xna.Framework;

namespace Terraria.DataStructures
{
	// Token: 0x02000538 RID: 1336
	public static class ActiveSections
	{
		// Token: 0x14000057 RID: 87
		// (add) Token: 0x0600374A RID: 14154 RVA: 0x0062E9E0 File Offset: 0x0062CBE0
		// (remove) Token: 0x0600374B RID: 14155 RVA: 0x0062EA14 File Offset: 0x0062CC14
		public static event Action<Point> SectionActivated
		{
			[CompilerGenerated]
			add
			{
				Action<Point> action = ActiveSections.SectionActivated;
				Action<Point> action2;
				do
				{
					action2 = action;
					Action<Point> action3 = (Action<Point>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<Point>>(ref ActiveSections.SectionActivated, action3, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<Point> action = ActiveSections.SectionActivated;
				Action<Point> action2;
				do
				{
					action2 = action;
					Action<Point> action3 = (Action<Point>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<Point>>(ref ActiveSections.SectionActivated, action3, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x0600374C RID: 14156 RVA: 0x0062EA48 File Offset: 0x0062CC48
		public static void CheckSection(Vector2 position, int fluff = 1)
		{
			int sectionX = Netplay.GetSectionX((int)(position.X / 16f));
			int sectionY = Netplay.GetSectionY((int)(position.Y / 16f));
			for (int i = sectionX - fluff; i < sectionX + fluff + 1; i++)
			{
				for (int j = sectionY - fluff; j < sectionY + fluff + 1; j++)
				{
					if (i >= 0 && i < Main.maxSectionsX && j >= 0 && j < Main.maxSectionsY)
					{
						bool flag = ActiveSections.IsSectionActive(new Point(i, j));
						ActiveSections.LastActiveTime[i, j] = Main.GameUpdateCount;
						if (!flag)
						{
							ActiveSections.SectionActivated(new Point(i, j));
						}
					}
				}
			}
		}

		// Token: 0x0600374D RID: 14157 RVA: 0x0062EAE7 File Offset: 0x0062CCE7
		public static bool IsSectionActive(Point sectionCoords)
		{
			sectionCoords = sectionCoords.ClampSectionCoords();
			return ActiveSections.LastActiveTime[sectionCoords.X, sectionCoords.Y] + ActiveSections.SectionInactiveTime >= Main.GameUpdateCount;
		}

		// Token: 0x0600374E RID: 14158 RVA: 0x0062EB17 File Offset: 0x0062CD17
		public static int TimeTillInactive(Point sectionCoords)
		{
			sectionCoords = sectionCoords.ClampSectionCoords();
			return (int)Math.Max(0L, (long)((ulong)(ActiveSections.LastActiveTime[sectionCoords.X, sectionCoords.Y] + ActiveSections.SectionInactiveTime) - (ulong)Main.GameUpdateCount));
		}

		// Token: 0x0600374F RID: 14159 RVA: 0x0062EB4D File Offset: 0x0062CD4D
		public static void Reset()
		{
			Array.Clear(ActiveSections.LastActiveTime, 0, ActiveSections.LastActiveTime.Length);
		}

		// Token: 0x06003750 RID: 14160 RVA: 0x0062EB64 File Offset: 0x0062CD64
		public static Point ClampSectionCoords(this Point point)
		{
			return new Point(Utils.Clamp<int>(point.X, 0, Main.maxSectionsX), Utils.Clamp<int>(point.Y, 0, Main.maxSectionsY));
		}

		// Token: 0x06003751 RID: 14161 RVA: 0x0062EB8D File Offset: 0x0062CD8D
		// Note: this type is marked as 'beforefieldinit'.
		static ActiveSections()
		{
		}

		// Token: 0x04005B87 RID: 23431
		public static readonly uint SectionInactiveTime = 60U;

		// Token: 0x04005B88 RID: 23432
		private static uint[,] LastActiveTime = new uint[Main.maxTilesX / 200 + 1, Main.maxTilesY / 150 + 1];

		// Token: 0x04005B89 RID: 23433
		[CompilerGenerated]
		private static Action<Point> SectionActivated;
	}
}
