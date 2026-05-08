using System;
using System.Collections.Generic;
using Terraria.Achievements;

namespace Terraria.GameContent.Achievements
{
	// Token: 0x0200028B RID: 651
	public class TileDestroyedCondition : AchievementCondition
	{
		// Token: 0x06002516 RID: 9494 RVA: 0x005536D0 File Offset: 0x005518D0
		private TileDestroyedCondition(ushort[] tileIds)
			: base("TILE_DESTROYED_" + tileIds[0])
		{
			this._tileIds = tileIds;
			TileDestroyedCondition.ListenForDestruction(this);
		}

		// Token: 0x06002517 RID: 9495 RVA: 0x005536F8 File Offset: 0x005518F8
		private static void ListenForDestruction(TileDestroyedCondition condition)
		{
			if (!TileDestroyedCondition._isListenerHooked)
			{
				AchievementsHelper.OnTileDestroyed += TileDestroyedCondition.TileDestroyedListener;
				TileDestroyedCondition._isListenerHooked = true;
			}
			for (int i = 0; i < condition._tileIds.Length; i++)
			{
				if (!TileDestroyedCondition._listeners.ContainsKey(condition._tileIds[i]))
				{
					TileDestroyedCondition._listeners[condition._tileIds[i]] = new List<TileDestroyedCondition>();
				}
				TileDestroyedCondition._listeners[condition._tileIds[i]].Add(condition);
			}
		}

		// Token: 0x06002518 RID: 9496 RVA: 0x0055377C File Offset: 0x0055197C
		private static void TileDestroyedListener(Player player, ushort tileId)
		{
			if (player.whoAmI != Main.myPlayer)
			{
				return;
			}
			if (TileDestroyedCondition._listeners.ContainsKey(tileId))
			{
				foreach (TileDestroyedCondition tileDestroyedCondition in TileDestroyedCondition._listeners[tileId])
				{
					tileDestroyedCondition.Complete();
				}
			}
		}

		// Token: 0x06002519 RID: 9497 RVA: 0x005537EC File Offset: 0x005519EC
		public static AchievementCondition Create(params ushort[] tileIds)
		{
			return new TileDestroyedCondition(tileIds);
		}

		// Token: 0x0600251A RID: 9498 RVA: 0x005537F4 File Offset: 0x005519F4
		// Note: this type is marked as 'beforefieldinit'.
		static TileDestroyedCondition()
		{
		}

		// Token: 0x04004F83 RID: 20355
		private const string Identifier = "TILE_DESTROYED";

		// Token: 0x04004F84 RID: 20356
		private static Dictionary<ushort, List<TileDestroyedCondition>> _listeners = new Dictionary<ushort, List<TileDestroyedCondition>>();

		// Token: 0x04004F85 RID: 20357
		private static bool _isListenerHooked;

		// Token: 0x04004F86 RID: 20358
		private ushort[] _tileIds;
	}
}
