using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria.GameContent
{
	// Token: 0x0200022D RID: 557
	public abstract class NPCDamageTracker
	{
		// Token: 0x060021E0 RID: 8672 RVA: 0x00532EAC File Offset: 0x005310AC
		public static NPCDamageTracker.CustomDefinition RegisterCompositeTypeBoss(params int[] types)
		{
			NPCDamageTracker.CustomDefinition customDefinition = new NPCDamageTracker.CustomDefinition
			{
				NPCTypes = types.ToList<int>()
			};
			foreach (int num in types)
			{
				NPCDamageTracker.CustomBossDefinitions[num] = customDefinition;
			}
			return customDefinition;
		}

		// Token: 0x060021E1 RID: 8673 RVA: 0x00532EE8 File Offset: 0x005310E8
		public static void RegisterMobsForBoss(int bossType, params int[] mobTypes)
		{
			foreach (int num in mobTypes)
			{
				NPCDamageTracker.BossTypeForMob[num] = bossType;
			}
		}

		// Token: 0x060021E2 RID: 8674 RVA: 0x00532F14 File Offset: 0x00531114
		static NPCDamageTracker()
		{
			NPCDamageTracker.RegisterMobsForBoss(50, new int[] { 1, 535 });
			NPCDamageTracker.RegisterMobsForBoss(4, new int[] { 5 });
			NPCDamageTracker.RegisterMobsForBoss(222, new int[] { 210, 211 });
			NPCDamageTracker.RegisterCompositeTypeBoss(new int[] { 13, 14, 15 });
			NPCDamageTracker.RegisterCompositeTypeBoss(new int[] { 266, 267 });
			NPCDamageTracker.RegisterCompositeTypeBoss(new int[] { 35, 36 });
			NPCDamageTracker.RegisterMobsForBoss(113, new int[] { 115, 116, 117, 118, 119 });
			NPCDamageTracker.RegisterMobsForBoss(657, new int[] { 658, 659, 660 });
			NPCDamageTracker.RegisterCompositeTypeBoss(new int[] { 126, 125 }).Name = Language.GetText("Enemies.TheTwins");
			NPCDamageTracker.RegisterCompositeTypeBoss(new int[] { 127, 128, 129, 130, 131 });
			NPCDamageTracker.RegisterMobsForBoss(134, new int[] { 139 });
			NPCDamageTracker.RegisterMobsForBoss(262, new int[] { 264 });
			NPCDamageTracker.RegisterCompositeTypeBoss(new int[] { 245, 246, 247, 248 });
			NPCDamageTracker.RegisterMobsForBoss(370, new int[] { 372, 373 });
			NPCDamageTracker.RegisterMobsForBoss(439, new int[] { 454, 455, 456, 457, 458, 459 });
			NPCDamageTracker.RegisterCompositeTypeBoss(new int[] { 398, 396, 397 }).Name = Language.GetText("Enemies.MoonLord");
		}

		// Token: 0x060021E3 RID: 8675 RVA: 0x00533117 File Offset: 0x00531317
		private static bool GetRealActiveNPC(ref NPC npc)
		{
			if (npc.realLife >= 0)
			{
				npc = Main.npc[npc.realLife];
			}
			return npc.active;
		}

		// Token: 0x060021E4 RID: 8676 RVA: 0x00533140 File Offset: 0x00531340
		private static bool TryGetTrackerFor(NPC npc, out NPCDamageTracker tracker)
		{
			tracker = null;
			if (!NPCDamageTracker.GetRealActiveNPC(ref npc))
			{
				return false;
			}
			foreach (NPCDamageTracker npcdamageTracker in NPCDamageTracker._activeTrackers)
			{
				if (npcdamageTracker.IncludeDamageFor(npc))
				{
					tracker = npcdamageTracker;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060021E5 RID: 8677 RVA: 0x005331AC File Offset: 0x005313AC
		private static bool CreateTrackerFor(NPC npc, out NPCDamageTracker tracker)
		{
			tracker = null;
			NPCDamageTracker.CustomDefinition customDefinition = NPCDamageTracker.CustomBossDefinitions[npc.type];
			if (customDefinition == null && !npc.boss)
			{
				return false;
			}
			tracker = new BossDamageTracker(npc.type, customDefinition);
			return true;
		}

		// Token: 0x060021E6 RID: 8678 RVA: 0x005331E8 File Offset: 0x005313E8
		public static void AddDamage(NPC npc, int owner, int damage)
		{
			if (!NPCDamageTracker.GetRealActiveNPC(ref npc) || npc.life <= 0)
			{
				return;
			}
			NPCDamageTracker npcdamageTracker;
			if (!NPCDamageTracker.TryGetTrackerFor(npc, out npcdamageTracker))
			{
				if (!NPCDamageTracker.CreateTrackerFor(npc, out npcdamageTracker))
				{
					return;
				}
				NPCDamageTracker.Start(npcdamageTracker);
			}
			npcdamageTracker.AddDamage(owner, Math.Min(damage, npc.life));
		}

		// Token: 0x060021E7 RID: 8679 RVA: 0x00533238 File Offset: 0x00531438
		public static void AddDamageToLastAttack(NPC npc, int damage)
		{
			if (!NPCDamageTracker.GetRealActiveNPC(ref npc) || npc.life <= 0)
			{
				return;
			}
			NPCDamageTracker npcdamageTracker;
			if (!NPCDamageTracker.TryGetTrackerFor(npc, out npcdamageTracker))
			{
				return;
			}
			npcdamageTracker.AddDamageToLastAttack(Math.Min(damage, npc.life));
		}

		// Token: 0x060021E8 RID: 8680 RVA: 0x00533278 File Offset: 0x00531478
		public static void BossKilled(NPC npc)
		{
			NPCDamageTracker npcdamageTracker;
			if (NPCDamageTracker.TryGetTrackerFor(npc, out npcdamageTracker))
			{
				npcdamageTracker.OnBossKilled(npc);
			}
		}

		// Token: 0x060021E9 RID: 8681 RVA: 0x00533296 File Offset: 0x00531496
		public static void Start(NPCDamageTracker tracker)
		{
			NPCDamageTracker._activeTrackers.Add(tracker);
		}

		// Token: 0x060021EA RID: 8682 RVA: 0x005332A3 File Offset: 0x005314A3
		public static void Reset()
		{
			NPCDamageTracker._activeTrackers.Clear();
			NPCDamageTracker._recentFinishedTrackers.Clear();
		}

		// Token: 0x060021EB RID: 8683 RVA: 0x005332B9 File Offset: 0x005314B9
		public static IEnumerable<NPCDamageTracker> RecentAttempts()
		{
			return NPCDamageTracker._recentFinishedTrackers;
		}

		// Token: 0x060021EC RID: 8684 RVA: 0x005332C0 File Offset: 0x005314C0
		public static void Update()
		{
			foreach (NPCDamageTracker npcdamageTracker in NPCDamageTracker._activeTrackers)
			{
				npcdamageTracker.Tick();
			}
			foreach (NPCDamageTracker npcdamageTracker2 in NPCDamageTracker._recentFinishedTrackers)
			{
				npcdamageTracker2.Tick();
			}
			for (int i = NPCDamageTracker._activeTrackers.Count - 1; i >= 0; i--)
			{
				NPCDamageTracker._activeTrackers[i].CheckActive();
			}
			while (NPCDamageTracker._recentFinishedTrackers.Count > 1 && NPCDamageTracker._recentFinishedTrackers[0].TimeSinceLastHit > NPCDamageTracker.EXTRA_RECENT_TRACKER_EXPIRY_TIME)
			{
				NPCDamageTracker._recentFinishedTrackers.RemoveAt(0);
			}
		}

		// Token: 0x060021ED RID: 8685 RVA: 0x005333A8 File Offset: 0x005315A8
		private static void StopTracking(NPCDamageTracker tracker)
		{
			if (!NPCDamageTracker._activeTrackers.Remove(tracker) || tracker.IsEmpty)
			{
				return;
			}
			NPCDamageTracker._recentFinishedTrackers.Add(tracker);
			if (NPCDamageTracker._recentFinishedTrackers.Count > NPCDamageTracker.MAX_RECENT_TRACKERS)
			{
				NPCDamageTracker._recentFinishedTrackers.RemoveAt(0);
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x060021EE RID: 8686 RVA: 0x005333E7 File Offset: 0x005315E7
		public bool IsEmpty
		{
			get
			{
				return this._list.Count == 0;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x060021EF RID: 8687 RVA: 0x005333F7 File Offset: 0x005315F7
		public int Duration
		{
			get
			{
				return this._lastHitTime;
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x060021F0 RID: 8688 RVA: 0x005333FF File Offset: 0x005315FF
		public int TimeSinceLastHit
		{
			get
			{
				return this._ticks - this._lastHitTime;
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x060021F1 RID: 8689
		public abstract LocalizedText Name { get; }

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x060021F2 RID: 8690
		public abstract LocalizedText KillTimeMessage { get; }

		// Token: 0x060021F3 RID: 8691
		protected abstract bool IncludeDamageFor(NPC npc);

		// Token: 0x060021F4 RID: 8692 RVA: 0x00009E46 File Offset: 0x00008046
		protected virtual void CheckActive()
		{
		}

		// Token: 0x060021F5 RID: 8693 RVA: 0x00009E46 File Offset: 0x00008046
		protected virtual void OnBossKilled(NPC npc)
		{
		}

		// Token: 0x060021F6 RID: 8694 RVA: 0x0053340E File Offset: 0x0053160E
		private void Tick()
		{
			this._ticks++;
		}

		// Token: 0x060021F7 RID: 8695 RVA: 0x0053341E File Offset: 0x0053161E
		protected void Stop()
		{
			NPCDamageTracker.StopTracking(this);
		}

		// Token: 0x060021F8 RID: 8696 RVA: 0x00533428 File Offset: 0x00531628
		public void AddDamage(int owner, int damage)
		{
			this._lastHitTime = this._ticks;
			NPCDamageTracker.CreditEntry orAddEntry = this.GetOrAddEntry(owner);
			orAddEntry.Damage += damage;
			this._lastAttacker = orAddEntry;
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x0053345E File Offset: 0x0053165E
		public void AddDamageToLastAttack(int damage)
		{
			this._lastAttacker.Damage += damage;
		}

		// Token: 0x060021FA RID: 8698 RVA: 0x00533474 File Offset: 0x00531674
		private NPCDamageTracker.CreditEntry GetOrAddEntry(int owner)
		{
			if (owner < 0 || owner >= 255)
			{
				if (this._worldCredit == null)
				{
					this._worldCredit = new NPCDamageTracker.WorldCreditEntry();
					this._list.Add(this._worldCredit);
				}
				return this._worldCredit;
			}
			string name = Main.player[owner].name;
			foreach (NPCDamageTracker.CreditEntry creditEntry in this._list)
			{
				NPCDamageTracker.PlayerCreditEntry playerCreditEntry = creditEntry as NPCDamageTracker.PlayerCreditEntry;
				if (playerCreditEntry != null && playerCreditEntry.PlayerName == name)
				{
					return playerCreditEntry;
				}
			}
			NPCDamageTracker.PlayerCreditEntry playerCreditEntry2 = new NPCDamageTracker.PlayerCreditEntry(name);
			this._list.Add(playerCreditEntry2);
			return playerCreditEntry2;
		}

		// Token: 0x060021FB RID: 8699 RVA: 0x00533538 File Offset: 0x00531738
		public NetworkText GetReport(Player forPlayer = null)
		{
			this._list.Sort();
			int[] array = this._list.Select((NPCDamageTracker.CreditEntry x) => x.Damage).ToArray<int>();
			int[] array2 = NPCDamageTracker.CalculatePercentages(array);
			int length = array.Max().ToString().Length;
			List<NetworkText> list = new List<NetworkText>(this._list.Count + 2);
			StringBuilder stringBuilder = new StringBuilder("{0}");
			list.Add(NetworkText.FromKey("BossDamageCommand.Title", new object[] { this.Name.ToNetworkText() }));
			LocalizedText killTimeMessage = this.KillTimeMessage;
			if (killTimeMessage != null)
			{
				stringBuilder.Append("\n{1}");
				TimeSpan timeSpan = TimeSpan.FromSeconds((double)this.Duration / 60.0);
				string text = string.Format("{0}:{1:00}", (int)timeSpan.TotalMinutes, timeSpan.Seconds);
				list.Add(killTimeMessage.ToNetworkText(new object[] { text }));
			}
			for (int i = 0; i < this._list.Count; i++)
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				stringBuilder2.Append(array2[i]).Append('%');
				while (stringBuilder2.Length < 6)
				{
					stringBuilder2.Append(' ');
				}
				stringBuilder2.Append(array[i]);
				while (stringBuilder2.Length < 8 + length)
				{
					stringBuilder2.Append(' ');
				}
				NPCDamageTracker.CreditEntry creditEntry = this._list[i];
				stringBuilder2.Append('{').Append(list.Count).Append('}');
				list.Add(creditEntry.Name);
				string text2 = stringBuilder2.ToString();
				if (forPlayer != null && creditEntry is NPCDamageTracker.PlayerCreditEntry && ((NPCDamageTracker.PlayerCreditEntry)creditEntry).PlayerName == forPlayer.name)
				{
					text2 = "[c/FFAF00:" + text2 + "]";
				}
				stringBuilder.Append('\n').Append(text2);
			}
			string text3 = stringBuilder.ToString();
			object[] array3 = list.ToArray();
			return NetworkText.FromFormattable(text3, array3);
		}

		// Token: 0x060021FC RID: 8700 RVA: 0x00533768 File Offset: 0x00531968
		private static int[] CalculatePercentages(int[] damages)
		{
			int num = damages.Sum();
			int[] array = new int[damages.Length];
			double[] array2 = new double[damages.Length];
			int i = 0;
			for (int j = 0; j < damages.Length; j++)
			{
				double num2 = (double)(damages[j] * 100) / (double)num;
				int num3 = (int)num2;
				array[j] = num3;
				array2[j] = num2 - (double)num3;
				i += num3;
			}
			while (i < 100)
			{
				int num4 = 0;
				double num5 = 0.0;
				for (int k = 0; k < damages.Length; k++)
				{
					if (array2[k] > num5)
					{
						num5 = array2[k];
						num4 = k;
					}
				}
				array2[num4] = 0.0;
				array[num4]++;
				i++;
			}
			return array;
		}

		// Token: 0x060021FD RID: 8701 RVA: 0x00533821 File Offset: 0x00531A21
		protected NPCDamageTracker()
		{
		}

		// Token: 0x04004CB3 RID: 19635
		public static NPCDamageTracker.CustomDefinition[] CustomBossDefinitions = NPCID.Sets.Factory.CreateCustomSet<NPCDamageTracker.CustomDefinition>(null, new object[0]);

		// Token: 0x04004CB4 RID: 19636
		public static int[] BossTypeForMob = NPCID.Sets.Factory.CreateIntSet(new int[0]);

		// Token: 0x04004CB5 RID: 19637
		private static List<NPCDamageTracker> _activeTrackers = new List<NPCDamageTracker>();

		// Token: 0x04004CB6 RID: 19638
		private static List<NPCDamageTracker> _recentFinishedTrackers = new List<NPCDamageTracker>();

		// Token: 0x04004CB7 RID: 19639
		private static readonly int MAX_RECENT_TRACKERS = 3;

		// Token: 0x04004CB8 RID: 19640
		private static readonly int EXTRA_RECENT_TRACKER_EXPIRY_TIME = 54000;

		// Token: 0x04004CB9 RID: 19641
		private readonly List<NPCDamageTracker.CreditEntry> _list = new List<NPCDamageTracker.CreditEntry>(255);

		// Token: 0x04004CBA RID: 19642
		private NPCDamageTracker.WorldCreditEntry _worldCredit;

		// Token: 0x04004CBB RID: 19643
		private NPCDamageTracker.CreditEntry _lastAttacker;

		// Token: 0x04004CBC RID: 19644
		private int _ticks;

		// Token: 0x04004CBD RID: 19645
		private int _lastHitTime;

		// Token: 0x020007AD RID: 1965
		public class CustomDefinition
		{
			// Token: 0x060041BD RID: 16829 RVA: 0x0000357B File Offset: 0x0000177B
			public CustomDefinition()
			{
			}

			// Token: 0x040070AE RID: 28846
			public List<int> NPCTypes;

			// Token: 0x040070AF RID: 28847
			public LocalizedText Name;
		}

		// Token: 0x020007AE RID: 1966
		private abstract class CreditEntry : IComparable<NPCDamageTracker.CreditEntry>
		{
			// Token: 0x1700052B RID: 1323
			// (get) Token: 0x060041BE RID: 16830 RVA: 0x006BCA37 File Offset: 0x006BAC37
			// (set) Token: 0x060041BF RID: 16831 RVA: 0x006BCA3F File Offset: 0x006BAC3F
			public int Damage
			{
				[CompilerGenerated]
				get
				{
					return this.<Damage>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<Damage>k__BackingField = value;
				}
			}

			// Token: 0x1700052C RID: 1324
			// (get) Token: 0x060041C0 RID: 16832
			public abstract NetworkText Name { get; }

			// Token: 0x060041C1 RID: 16833 RVA: 0x006BCA48 File Offset: 0x006BAC48
			public int CompareTo(NPCDamageTracker.CreditEntry other)
			{
				return -this.Damage.CompareTo(other.Damage);
			}

			// Token: 0x060041C2 RID: 16834 RVA: 0x0000357B File Offset: 0x0000177B
			protected CreditEntry()
			{
			}

			// Token: 0x040070B0 RID: 28848
			[CompilerGenerated]
			private int <Damage>k__BackingField;
		}

		// Token: 0x020007AF RID: 1967
		private class PlayerCreditEntry : NPCDamageTracker.CreditEntry
		{
			// Token: 0x060041C3 RID: 16835 RVA: 0x006BCA6A File Offset: 0x006BAC6A
			public PlayerCreditEntry(string name)
			{
				this.PlayerName = name;
			}

			// Token: 0x1700052D RID: 1325
			// (get) Token: 0x060041C4 RID: 16836 RVA: 0x006BCA79 File Offset: 0x006BAC79
			public override NetworkText Name
			{
				get
				{
					return NetworkText.FromLiteral(this.PlayerName);
				}
			}

			// Token: 0x040070B1 RID: 28849
			public readonly string PlayerName;
		}

		// Token: 0x020007B0 RID: 1968
		private class WorldCreditEntry : NPCDamageTracker.CreditEntry
		{
			// Token: 0x1700052E RID: 1326
			// (get) Token: 0x060041C5 RID: 16837 RVA: 0x006BCA86 File Offset: 0x006BAC86
			public override NetworkText Name
			{
				get
				{
					return NetworkText.FromKey("BossDamageCommand.WorldCreditName", new object[0]);
				}
			}

			// Token: 0x060041C6 RID: 16838 RVA: 0x006BCA98 File Offset: 0x006BAC98
			public WorldCreditEntry()
			{
			}
		}

		// Token: 0x020007B1 RID: 1969
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060041C7 RID: 16839 RVA: 0x006BCAA0 File Offset: 0x006BACA0
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060041C8 RID: 16840 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x060041C9 RID: 16841 RVA: 0x006BCAAC File Offset: 0x006BACAC
			internal int <GetReport>b__47_0(NPCDamageTracker.CreditEntry x)
			{
				return x.Damage;
			}

			// Token: 0x040070B2 RID: 28850
			public static readonly NPCDamageTracker.<>c <>9 = new NPCDamageTracker.<>c();

			// Token: 0x040070B3 RID: 28851
			public static Func<NPCDamageTracker.CreditEntry, int> <>9__47_0;
		}
	}
}
