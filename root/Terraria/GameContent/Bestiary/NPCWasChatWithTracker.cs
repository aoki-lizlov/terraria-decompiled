using System;
using System.Collections.Generic;
using System.IO;
using Terraria.GameContent.Achievements;
using Terraria.GameContent.NetModules;
using Terraria.ID;
using Terraria.Net;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000336 RID: 822
	public class NPCWasChatWithTracker : IPersistentPerWorldContent, IOnPlayerJoining
	{
		// Token: 0x0600282B RID: 10283 RVA: 0x0057235C File Offset: 0x0057055C
		public NPCWasChatWithTracker()
		{
			this._chattedWithPlayer = new HashSet<string>();
		}

		// Token: 0x0600282C RID: 10284 RVA: 0x0057237C File Offset: 0x0057057C
		public void RegisterChatStartWith(NPC npc)
		{
			string bestiaryCreditId = npc.GetBestiaryCreditId();
			bool flag = !this._chattedWithPlayer.Contains(bestiaryCreditId);
			this.SetWasChatWithDirectly(bestiaryCreditId);
			if (Main.netMode == 2 && flag)
			{
				NetManager.Instance.Broadcast(NetBestiaryModule.SerializeChat(npc.netID), -1);
			}
		}

		// Token: 0x0600282D RID: 10285 RVA: 0x005723CC File Offset: 0x005705CC
		public void SetWasChatWithDirectly(string persistentId)
		{
			object entryCreationLock = this._entryCreationLock;
			lock (entryCreationLock)
			{
				if (this._chattedWithPlayer.Add(persistentId))
				{
					AchievementsHelper.TryGrantingBestiary100PercentAchievement();
				}
			}
		}

		// Token: 0x0600282E RID: 10286 RVA: 0x0057241C File Offset: 0x0057061C
		public bool GetWasChatWith(NPC npc)
		{
			string bestiaryCreditId = npc.GetBestiaryCreditId();
			return this._chattedWithPlayer.Contains(bestiaryCreditId);
		}

		// Token: 0x0600282F RID: 10287 RVA: 0x0057243C File Offset: 0x0057063C
		public bool GetWasChatWith(string persistentId)
		{
			return this._chattedWithPlayer.Contains(persistentId);
		}

		// Token: 0x06002830 RID: 10288 RVA: 0x0057244C File Offset: 0x0057064C
		public void Save(BinaryWriter writer)
		{
			object entryCreationLock = this._entryCreationLock;
			lock (entryCreationLock)
			{
				writer.Write(this._chattedWithPlayer.Count);
				foreach (string text in this._chattedWithPlayer)
				{
					writer.Write(text);
				}
			}
		}

		// Token: 0x06002831 RID: 10289 RVA: 0x005724D8 File Offset: 0x005706D8
		public void Load(BinaryReader reader, int gameVersionSaveWasMadeOn)
		{
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				string text = reader.ReadString();
				this._chattedWithPlayer.Add(text);
			}
		}

		// Token: 0x06002832 RID: 10290 RVA: 0x0057250C File Offset: 0x0057070C
		public void ValidateWorld(BinaryReader reader, int gameVersionSaveWasMadeOn)
		{
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				reader.ReadString();
			}
		}

		// Token: 0x06002833 RID: 10291 RVA: 0x00572533 File Offset: 0x00570733
		public void Reset()
		{
			this._chattedWithPlayer.Clear();
		}

		// Token: 0x06002834 RID: 10292 RVA: 0x00572540 File Offset: 0x00570740
		public void OnPlayerJoining(int playerIndex)
		{
			foreach (string text in this._chattedWithPlayer)
			{
				int num;
				if (ContentSamples.NpcNetIdsByPersistentIds.TryGetValue(text, out num))
				{
					NetManager.Instance.SendToClient(NetBestiaryModule.SerializeChat(num), playerIndex);
				}
			}
		}

		// Token: 0x04005125 RID: 20773
		private object _entryCreationLock = new object();

		// Token: 0x04005126 RID: 20774
		private HashSet<string> _chattedWithPlayer;
	}
}
