using System;
using System.Collections.Generic;
using Terraria.Achievements;

namespace Terraria.GameContent.Achievements
{
	// Token: 0x0200028A RID: 650
	public class ProgressionEventCondition : AchievementCondition
	{
		// Token: 0x0600250E RID: 9486 RVA: 0x00553544 File Offset: 0x00551744
		private ProgressionEventCondition(int eventID)
			: base("PROGRESSION_EVENT_" + eventID)
		{
			this._eventIDs = new int[] { eventID };
			ProgressionEventCondition.ListenForPickup(this);
		}

		// Token: 0x0600250F RID: 9487 RVA: 0x00553572 File Offset: 0x00551772
		private ProgressionEventCondition(int[] eventIDs)
			: base("PROGRESSION_EVENT_" + eventIDs[0])
		{
			this._eventIDs = eventIDs;
			ProgressionEventCondition.ListenForPickup(this);
		}

		// Token: 0x06002510 RID: 9488 RVA: 0x0055359C File Offset: 0x0055179C
		private static void ListenForPickup(ProgressionEventCondition condition)
		{
			if (!ProgressionEventCondition._isListenerHooked)
			{
				AchievementsHelper.OnProgressionEvent += ProgressionEventCondition.ProgressionEventListener;
				ProgressionEventCondition._isListenerHooked = true;
			}
			for (int i = 0; i < condition._eventIDs.Length; i++)
			{
				if (!ProgressionEventCondition._listeners.ContainsKey(condition._eventIDs[i]))
				{
					ProgressionEventCondition._listeners[condition._eventIDs[i]] = new List<ProgressionEventCondition>();
				}
				ProgressionEventCondition._listeners[condition._eventIDs[i]].Add(condition);
			}
		}

		// Token: 0x06002511 RID: 9489 RVA: 0x00553620 File Offset: 0x00551820
		private static void ProgressionEventListener(int eventID)
		{
			if (ProgressionEventCondition._listeners.ContainsKey(eventID))
			{
				foreach (ProgressionEventCondition progressionEventCondition in ProgressionEventCondition._listeners[eventID])
				{
					progressionEventCondition.Complete();
				}
			}
		}

		// Token: 0x06002512 RID: 9490 RVA: 0x00553684 File Offset: 0x00551884
		public static ProgressionEventCondition Create(params int[] eventIDs)
		{
			return new ProgressionEventCondition(eventIDs);
		}

		// Token: 0x06002513 RID: 9491 RVA: 0x0055368C File Offset: 0x0055188C
		public static ProgressionEventCondition Create(int eventID)
		{
			return new ProgressionEventCondition(eventID);
		}

		// Token: 0x06002514 RID: 9492 RVA: 0x00553694 File Offset: 0x00551894
		public static ProgressionEventCondition[] CreateMany(params int[] eventIDs)
		{
			ProgressionEventCondition[] array = new ProgressionEventCondition[eventIDs.Length];
			for (int i = 0; i < eventIDs.Length; i++)
			{
				array[i] = new ProgressionEventCondition(eventIDs[i]);
			}
			return array;
		}

		// Token: 0x06002515 RID: 9493 RVA: 0x005536C4 File Offset: 0x005518C4
		// Note: this type is marked as 'beforefieldinit'.
		static ProgressionEventCondition()
		{
		}

		// Token: 0x04004F7F RID: 20351
		private const string Identifier = "PROGRESSION_EVENT";

		// Token: 0x04004F80 RID: 20352
		private static Dictionary<int, List<ProgressionEventCondition>> _listeners = new Dictionary<int, List<ProgressionEventCondition>>();

		// Token: 0x04004F81 RID: 20353
		private static bool _isListenerHooked;

		// Token: 0x04004F82 RID: 20354
		private int[] _eventIDs;
	}
}
