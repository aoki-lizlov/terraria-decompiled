using System;
using System.Collections.Generic;
using Terraria.Achievements;

namespace Terraria.GameContent.Achievements
{
	// Token: 0x02000289 RID: 649
	public class NPCKilledCondition : AchievementCondition
	{
		// Token: 0x06002506 RID: 9478 RVA: 0x005533AC File Offset: 0x005515AC
		private NPCKilledCondition(short npcId)
			: base("NPC_KILLED_" + npcId)
		{
			this._npcIds = new short[] { npcId };
			NPCKilledCondition.ListenForPickup(this);
		}

		// Token: 0x06002507 RID: 9479 RVA: 0x005533DA File Offset: 0x005515DA
		private NPCKilledCondition(short[] npcIds)
			: base("NPC_KILLED_" + npcIds[0])
		{
			this._npcIds = npcIds;
			NPCKilledCondition.ListenForPickup(this);
		}

		// Token: 0x06002508 RID: 9480 RVA: 0x00553404 File Offset: 0x00551604
		private static void ListenForPickup(NPCKilledCondition condition)
		{
			if (!NPCKilledCondition._isListenerHooked)
			{
				AchievementsHelper.OnNPCKilled += NPCKilledCondition.NPCKilledListener;
				NPCKilledCondition._isListenerHooked = true;
			}
			for (int i = 0; i < condition._npcIds.Length; i++)
			{
				if (!NPCKilledCondition._listeners.ContainsKey(condition._npcIds[i]))
				{
					NPCKilledCondition._listeners[condition._npcIds[i]] = new List<NPCKilledCondition>();
				}
				NPCKilledCondition._listeners[condition._npcIds[i]].Add(condition);
			}
		}

		// Token: 0x06002509 RID: 9481 RVA: 0x00553488 File Offset: 0x00551688
		private static void NPCKilledListener(Player player, short npcId)
		{
			if (player.whoAmI != Main.myPlayer)
			{
				return;
			}
			if (NPCKilledCondition._listeners.ContainsKey(npcId))
			{
				foreach (NPCKilledCondition npckilledCondition in NPCKilledCondition._listeners[npcId])
				{
					npckilledCondition.Complete();
				}
			}
		}

		// Token: 0x0600250A RID: 9482 RVA: 0x005534F8 File Offset: 0x005516F8
		public static AchievementCondition Create(params short[] npcIds)
		{
			return new NPCKilledCondition(npcIds);
		}

		// Token: 0x0600250B RID: 9483 RVA: 0x00553500 File Offset: 0x00551700
		public static AchievementCondition Create(short npcId)
		{
			return new NPCKilledCondition(npcId);
		}

		// Token: 0x0600250C RID: 9484 RVA: 0x00553508 File Offset: 0x00551708
		public static AchievementCondition[] CreateMany(params short[] npcs)
		{
			AchievementCondition[] array = new AchievementCondition[npcs.Length];
			for (int i = 0; i < npcs.Length; i++)
			{
				array[i] = new NPCKilledCondition(npcs[i]);
			}
			return array;
		}

		// Token: 0x0600250D RID: 9485 RVA: 0x00553538 File Offset: 0x00551738
		// Note: this type is marked as 'beforefieldinit'.
		static NPCKilledCondition()
		{
		}

		// Token: 0x04004F7B RID: 20347
		private const string Identifier = "NPC_KILLED";

		// Token: 0x04004F7C RID: 20348
		private static Dictionary<short, List<NPCKilledCondition>> _listeners = new Dictionary<short, List<NPCKilledCondition>>();

		// Token: 0x04004F7D RID: 20349
		private static bool _isListenerHooked;

		// Token: 0x04004F7E RID: 20350
		private short[] _npcIds;
	}
}
