using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.WorldBuilding;

namespace Terraria.Map
{
	// Token: 0x02000179 RID: 377
	public class MapUpdateQueue
	{
		// Token: 0x06001E2E RID: 7726 RVA: 0x005035D0 File Offset: 0x005017D0
		public static void Add(Rectangle area)
		{
			if (Main.dedServ || WorldGen.generatingWorld || !Main.mapEnabled)
			{
				return;
			}
			area = WorldUtils.ClampToWorld(area, 0);
			object @lock = MapUpdateQueue._lock;
			lock (@lock)
			{
				MapUpdateQueue._areaUpdateQueue.Add(area);
				for (int i = area.Left; i < area.Right; i++)
				{
					for (int j = area.Top; j < area.Bottom; j++)
					{
						Main.Map.QueueUpdate(i, j);
					}
				}
			}
		}

		// Token: 0x06001E2F RID: 7727 RVA: 0x00503670 File Offset: 0x00501870
		public static void Add(int x, int y)
		{
			if (Main.dedServ || WorldGen.generatingWorld || !Main.mapEnabled)
			{
				return;
			}
			if (!Main.Map.QueueUpdate(x, y))
			{
				return;
			}
			object @lock = MapUpdateQueue._lock;
			lock (@lock)
			{
				if (MapUpdateQueue._updateCount == MapUpdateQueue._updateQueue.Length)
				{
					if (MapUpdateQueue._updateCount >= 262144)
					{
						return;
					}
					Array.Resize<Point16>(ref MapUpdateQueue._updateQueue, MapUpdateQueue._updateCount * 2);
				}
				MapUpdateQueue._updateQueue[MapUpdateQueue._updateCount++] = new Point16(x, y);
			}
		}

		// Token: 0x06001E30 RID: 7728 RVA: 0x0050371C File Offset: 0x0050191C
		public static void Update()
		{
			TimeLogger.StartTimestamp startTimestamp = TimeLogger.Start();
			object @lock = MapUpdateQueue._lock;
			lock (@lock)
			{
				MapUpdateQueue.UpdateTiles();
				MapUpdateQueue.UpdateAreas();
			}
			TimeLogger.MapChanges.AddTime(startTimestamp);
		}

		// Token: 0x06001E31 RID: 7729 RVA: 0x00503770 File Offset: 0x00501970
		private static void UpdateAreas()
		{
			foreach (Rectangle rectangle in MapUpdateQueue._areaUpdateQueue)
			{
				for (int i = rectangle.Left; i < rectangle.Right; i++)
				{
					for (int j = rectangle.Top; j < rectangle.Bottom; j++)
					{
						if (Main.Map.UpdateType(i, j))
						{
							MapRenderer.QueueChange(i, j);
						}
					}
				}
			}
			MapUpdateQueue._areaUpdateQueue.Clear();
		}

		// Token: 0x06001E32 RID: 7730 RVA: 0x0050380C File Offset: 0x00501A0C
		private static void UpdateTiles()
		{
			for (int i = 0; i < MapUpdateQueue._updateCount; i++)
			{
				Point16 point = MapUpdateQueue._updateQueue[i];
				if (Main.Map.UpdateType((int)point.X, (int)point.Y))
				{
					MapRenderer.QueueChange((int)point.X, (int)point.Y);
				}
			}
			MapUpdateQueue._updateCount = 0;
		}

		// Token: 0x06001E33 RID: 7731 RVA: 0x0000357B File Offset: 0x0000177B
		public MapUpdateQueue()
		{
		}

		// Token: 0x06001E34 RID: 7732 RVA: 0x00503864 File Offset: 0x00501A64
		// Note: this type is marked as 'beforefieldinit'.
		static MapUpdateQueue()
		{
		}

		// Token: 0x04001694 RID: 5780
		private const int MAX_QUEUED_UPDATES = 262144;

		// Token: 0x04001695 RID: 5781
		private static List<Rectangle> _areaUpdateQueue = new List<Rectangle>();

		// Token: 0x04001696 RID: 5782
		private static Point16[] _updateQueue = new Point16[1024];

		// Token: 0x04001697 RID: 5783
		private static int _updateCount = 0;

		// Token: 0x04001698 RID: 5784
		private static readonly object _lock = new object();
	}
}
