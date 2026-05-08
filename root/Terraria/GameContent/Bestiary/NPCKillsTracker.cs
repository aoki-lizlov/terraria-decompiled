using System;
using System.Collections.Generic;
using System.IO;
using Terraria.GameContent.Achievements;
using Terraria.GameContent.NetModules;
using Terraria.ID;
using Terraria.Net;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000334 RID: 820
	public class NPCKillsTracker : IPersistentPerWorldContent, IOnPlayerJoining
	{
		// Token: 0x06002815 RID: 10261 RVA: 0x00571D5C File Offset: 0x0056FF5C
		public NPCKillsTracker()
		{
			this._killCountsByNpcId = new Dictionary<string, int>();
		}

		// Token: 0x06002816 RID: 10262 RVA: 0x00571D7C File Offset: 0x0056FF7C
		public void RegisterKill(NPC npc)
		{
			string bestiaryCreditId = npc.GetBestiaryCreditId();
			int num;
			this._killCountsByNpcId.TryGetValue(bestiaryCreditId, out num);
			num++;
			this.SetKillCountDirectly(bestiaryCreditId, num);
			if (Main.netMode == 2)
			{
				NetManager.Instance.Broadcast(NetBestiaryModule.SerializeKillCount(npc.netID, num), -1);
			}
		}

		// Token: 0x06002817 RID: 10263 RVA: 0x00571DCC File Offset: 0x0056FFCC
		public int GetKillCount(NPC npc)
		{
			string bestiaryCreditId = npc.GetBestiaryCreditId();
			return this.GetKillCount(bestiaryCreditId);
		}

		// Token: 0x06002818 RID: 10264 RVA: 0x00571DE8 File Offset: 0x0056FFE8
		public void SetKillCountDirectly(string persistentId, int killCount)
		{
			object entryCreationLock = this._entryCreationLock;
			lock (entryCreationLock)
			{
				bool flag2 = this._killCountsByNpcId.ContainsKey(persistentId);
				this._killCountsByNpcId[persistentId] = Utils.Clamp<int>(killCount, 0, 999999999);
				if (!flag2)
				{
					AchievementsHelper.TryGrantingBestiary100PercentAchievement();
				}
			}
		}

		// Token: 0x06002819 RID: 10265 RVA: 0x00571E50 File Offset: 0x00570050
		public int GetKillCount(string persistentId)
		{
			int num;
			this._killCountsByNpcId.TryGetValue(persistentId, out num);
			return num;
		}

		// Token: 0x0600281A RID: 10266 RVA: 0x00571E70 File Offset: 0x00570070
		public void Save(BinaryWriter writer)
		{
			Dictionary<string, int> killCountsByNpcId = this._killCountsByNpcId;
			lock (killCountsByNpcId)
			{
				writer.Write(this._killCountsByNpcId.Count);
				foreach (KeyValuePair<string, int> keyValuePair in this._killCountsByNpcId)
				{
					writer.Write(keyValuePair.Key);
					writer.Write(keyValuePair.Value);
				}
			}
		}

		// Token: 0x0600281B RID: 10267 RVA: 0x00571F10 File Offset: 0x00570110
		public void Load(BinaryReader reader, int gameVersionSaveWasMadeOn)
		{
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				string text = reader.ReadString();
				int num2 = reader.ReadInt32();
				this._killCountsByNpcId[text] = num2;
			}
		}

		// Token: 0x0600281C RID: 10268 RVA: 0x00571F4C File Offset: 0x0057014C
		public void ValidateWorld(BinaryReader reader, int gameVersionSaveWasMadeOn)
		{
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				reader.ReadString();
				reader.ReadInt32();
			}
		}

		// Token: 0x0600281D RID: 10269 RVA: 0x00571F7A File Offset: 0x0057017A
		public void Reset()
		{
			this._killCountsByNpcId.Clear();
		}

		// Token: 0x0600281E RID: 10270 RVA: 0x00571F88 File Offset: 0x00570188
		public void OnPlayerJoining(int playerIndex)
		{
			foreach (KeyValuePair<string, int> keyValuePair in this._killCountsByNpcId)
			{
				int num;
				if (ContentSamples.NpcNetIdsByPersistentIds.TryGetValue(keyValuePair.Key, out num))
				{
					NetManager.Instance.SendToClient(NetBestiaryModule.SerializeKillCount(num, keyValuePair.Value), playerIndex);
				}
			}
		}

		// Token: 0x0400511E RID: 20766
		private object _entryCreationLock = new object();

		// Token: 0x0400511F RID: 20767
		public const int POSITIVE_KILL_COUNT_CAP = 999999999;

		// Token: 0x04005120 RID: 20768
		private Dictionary<string, int> _killCountsByNpcId;
	}
}
