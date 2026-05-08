using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Achievements;
using Terraria.GameContent.NetModules;
using Terraria.ID;
using Terraria.Net;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000335 RID: 821
	public class NPCWasNearPlayerTracker : IPersistentPerWorldContent, IOnPlayerJoining
	{
		// Token: 0x0600281F RID: 10271 RVA: 0x00009E46 File Offset: 0x00008046
		public void PrepareSamplesBasedOptimizations()
		{
		}

		// Token: 0x06002820 RID: 10272 RVA: 0x00572004 File Offset: 0x00570204
		public NPCWasNearPlayerTracker()
		{
			this._wasNearPlayer = new HashSet<string>();
			this._playerHitboxesForBestiary = new List<Rectangle>();
			this._wasSeenNearPlayerByNetId = new List<int>();
		}

		// Token: 0x06002821 RID: 10273 RVA: 0x00572038 File Offset: 0x00570238
		public void RegisterWasNearby(NPC npc)
		{
			string bestiaryCreditId = npc.GetBestiaryCreditId();
			bool flag = !this._wasNearPlayer.Contains(bestiaryCreditId);
			this.SetWasSeenDirectly(bestiaryCreditId);
			if (Main.netMode == 2 && flag)
			{
				NetManager.Instance.Broadcast(NetBestiaryModule.SerializeSight(npc.netID), -1);
			}
		}

		// Token: 0x06002822 RID: 10274 RVA: 0x00572088 File Offset: 0x00570288
		public void SetWasSeenDirectly(string persistentId)
		{
			object entryCreationLock = this._entryCreationLock;
			lock (entryCreationLock)
			{
				if (this._wasNearPlayer.Add(persistentId))
				{
					AchievementsHelper.TryGrantingBestiary100PercentAchievement();
				}
			}
		}

		// Token: 0x06002823 RID: 10275 RVA: 0x005720D8 File Offset: 0x005702D8
		public bool GetWasNearbyBefore(NPC npc)
		{
			string bestiaryCreditId = npc.GetBestiaryCreditId();
			return this.GetWasNearbyBefore(bestiaryCreditId);
		}

		// Token: 0x06002824 RID: 10276 RVA: 0x005720F3 File Offset: 0x005702F3
		public bool GetWasNearbyBefore(string persistentIdentifier)
		{
			return this._wasNearPlayer.Contains(persistentIdentifier);
		}

		// Token: 0x06002825 RID: 10277 RVA: 0x00572104 File Offset: 0x00570304
		public void Save(BinaryWriter writer)
		{
			object entryCreationLock = this._entryCreationLock;
			lock (entryCreationLock)
			{
				writer.Write(this._wasNearPlayer.Count);
				foreach (string text in this._wasNearPlayer)
				{
					writer.Write(text);
				}
			}
		}

		// Token: 0x06002826 RID: 10278 RVA: 0x00572190 File Offset: 0x00570390
		public void Load(BinaryReader reader, int gameVersionSaveWasMadeOn)
		{
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				string text = reader.ReadString();
				this._wasNearPlayer.Add(text);
			}
		}

		// Token: 0x06002827 RID: 10279 RVA: 0x005721C4 File Offset: 0x005703C4
		public void ValidateWorld(BinaryReader reader, int gameVersionSaveWasMadeOn)
		{
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				reader.ReadString();
			}
		}

		// Token: 0x06002828 RID: 10280 RVA: 0x005721EB File Offset: 0x005703EB
		public void Reset()
		{
			this._wasNearPlayer.Clear();
			this._playerHitboxesForBestiary.Clear();
			this._wasSeenNearPlayerByNetId.Clear();
		}

		// Token: 0x06002829 RID: 10281 RVA: 0x00572210 File Offset: 0x00570410
		public void ScanWorldForFinds()
		{
			this._playerHitboxesForBestiary.Clear();
			for (int i = 0; i < 255; i++)
			{
				Player player = Main.player[i];
				if (player.active)
				{
					this._playerHitboxesForBestiary.Add(player.HitboxForBestiaryNearbyCheck);
				}
			}
			for (int j = 0; j < Main.maxNPCs; j++)
			{
				NPC npc = Main.npc[j];
				if (npc.active && npc.CountsAsACritter && !this._wasSeenNearPlayerByNetId.Contains(npc.netID))
				{
					Rectangle hitbox = npc.Hitbox;
					for (int k = 0; k < this._playerHitboxesForBestiary.Count; k++)
					{
						Rectangle rectangle = this._playerHitboxesForBestiary[k];
						if (hitbox.Intersects(rectangle))
						{
							this._wasSeenNearPlayerByNetId.Add(npc.netID);
							this.RegisterWasNearby(npc);
						}
					}
				}
			}
		}

		// Token: 0x0600282A RID: 10282 RVA: 0x005722F0 File Offset: 0x005704F0
		public void OnPlayerJoining(int playerIndex)
		{
			foreach (string text in this._wasNearPlayer)
			{
				int num;
				if (ContentSamples.NpcNetIdsByPersistentIds.TryGetValue(text, out num))
				{
					NetManager.Instance.SendToClient(NetBestiaryModule.SerializeSight(num), playerIndex);
				}
			}
		}

		// Token: 0x04005121 RID: 20769
		private object _entryCreationLock = new object();

		// Token: 0x04005122 RID: 20770
		private HashSet<string> _wasNearPlayer;

		// Token: 0x04005123 RID: 20771
		private List<Rectangle> _playerHitboxesForBestiary;

		// Token: 0x04005124 RID: 20772
		private List<int> _wasSeenNearPlayerByNetId;
	}
}
