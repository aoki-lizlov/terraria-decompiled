using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.GameContent.Achievements;
using Terraria.GameContent.Creative;
using Terraria.GameContent.Events;
using Terraria.GameContent.Golf;
using Terraria.GameContent.Tile_Entities;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Map;
using Terraria.Net;
using Terraria.Net.Sockets;
using Terraria.Testing;
using Terraria.UI;

namespace Terraria
{
	// Token: 0x0200002D RID: 45
	public class MessageBuffer
	{
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000215 RID: 533 RVA: 0x00021F41 File Offset: 0x00020141
		public int RemainingReadBufferLength
		{
			get
			{
				return this.readBuffer.Length - this.totalData;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000216 RID: 534 RVA: 0x00021F54 File Offset: 0x00020154
		// (remove) Token: 0x06000217 RID: 535 RVA: 0x00021F88 File Offset: 0x00020188
		public static event TileChangeReceivedEvent OnTileChangeReceived
		{
			[CompilerGenerated]
			add
			{
				TileChangeReceivedEvent tileChangeReceivedEvent = MessageBuffer.OnTileChangeReceived;
				TileChangeReceivedEvent tileChangeReceivedEvent2;
				do
				{
					tileChangeReceivedEvent2 = tileChangeReceivedEvent;
					TileChangeReceivedEvent tileChangeReceivedEvent3 = (TileChangeReceivedEvent)Delegate.Combine(tileChangeReceivedEvent2, value);
					tileChangeReceivedEvent = Interlocked.CompareExchange<TileChangeReceivedEvent>(ref MessageBuffer.OnTileChangeReceived, tileChangeReceivedEvent3, tileChangeReceivedEvent2);
				}
				while (tileChangeReceivedEvent != tileChangeReceivedEvent2);
			}
			[CompilerGenerated]
			remove
			{
				TileChangeReceivedEvent tileChangeReceivedEvent = MessageBuffer.OnTileChangeReceived;
				TileChangeReceivedEvent tileChangeReceivedEvent2;
				do
				{
					tileChangeReceivedEvent2 = tileChangeReceivedEvent;
					TileChangeReceivedEvent tileChangeReceivedEvent3 = (TileChangeReceivedEvent)Delegate.Remove(tileChangeReceivedEvent2, value);
					tileChangeReceivedEvent = Interlocked.CompareExchange<TileChangeReceivedEvent>(ref MessageBuffer.OnTileChangeReceived, tileChangeReceivedEvent3, tileChangeReceivedEvent2);
				}
				while (tileChangeReceivedEvent != tileChangeReceivedEvent2);
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00021FBC File Offset: 0x000201BC
		public void Reset()
		{
			Array.Clear(this.readBuffer, 0, this.readBuffer.Length);
			Array.Clear(this.writeBuffer, 0, this.writeBuffer.Length);
			this.writeLocked = false;
			this.messageLength = 0;
			this.totalData = 0;
			this.spamCount = 0;
			this.broadcast = false;
			this.checkBytes = false;
			this.ResetReader();
			this.ResetWriter();
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00022027 File Offset: 0x00020227
		public void ResetReader()
		{
			if (this.readerStream != null)
			{
				this.readerStream.Close();
			}
			this.readerStream = new MemoryStream(this.readBuffer);
			this.reader = new BinaryReader(this.readerStream);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0002205E File Offset: 0x0002025E
		public void ResetWriter()
		{
			if (this.writerStream != null)
			{
				this.writerStream.Close();
			}
			this.writerStream = new MemoryStream(this.writeBuffer);
			this.writer = new BinaryWriter(this.writerStream);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00022098 File Offset: 0x00020298
		private float[] ReUseTemporaryProjectileAI()
		{
			for (int i = 0; i < this._temporaryProjectileAI.Length; i++)
			{
				this._temporaryProjectileAI[i] = 0f;
			}
			return this._temporaryProjectileAI;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x000220CC File Offset: 0x000202CC
		private float[] ReUseTemporaryNPCAI()
		{
			for (int i = 0; i < this._temporaryNPCAI.Length; i++)
			{
				this._temporaryNPCAI[i] = 0f;
			}
			return this._temporaryNPCAI;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00022100 File Offset: 0x00020300
		public void GetData(int start, int length, out int messageType)
		{
			if (this.whoAmI < 256)
			{
				Netplay.Clients[this.whoAmI].TimeOutTimer = 0;
			}
			else
			{
				Netplay.Connection.TimeOutTimer = 0;
			}
			int num = start + 1;
			byte b = this.readBuffer[start];
			messageType = (int)b;
			if (b >= MessageID.Count)
			{
				return;
			}
			Main.ActiveNetDiagnosticsUI.CountReadMessage((int)b, length);
			if (Main.netMode == 1 && Netplay.Connection.StatusMax > 0)
			{
				Netplay.Connection.StatusCount++;
			}
			if (Main.verboseNetplay)
			{
				for (int i = start; i < start + length; i++)
				{
				}
				for (int j = start; j < start + length; j++)
				{
					byte b2 = this.readBuffer[j];
				}
			}
			if (Main.netMode == 2 && b != 38 && Netplay.Clients[this.whoAmI].State == -1)
			{
				NetMessage.TrySendData(2, this.whoAmI, -1, Lang.mp[1].ToNetworkText(), 0, 0f, 0f, 0f, 0, 0, 0);
				return;
			}
			if (Main.netMode == 2)
			{
				if (Netplay.Clients[this.whoAmI].State < 10 && b > 12 && b != 93 && b != 16 && b != 42 && b != 50 && b != 38 && b != 68 && b != 147 && b != 161)
				{
					NetMessage.BootPlayer(this.whoAmI, Lang.mp[2].ToNetworkText());
				}
				if (Netplay.Clients[this.whoAmI].State == 0 && b != 1)
				{
					NetMessage.BootPlayer(this.whoAmI, Lang.mp[2].ToNetworkText());
				}
			}
			if (this.reader == null)
			{
				this.ResetReader();
			}
			this.reader.BaseStream.Position = (long)num;
			switch (b)
			{
			case 1:
				if (Main.netMode != 2)
				{
					return;
				}
				if (Main.dedServ && Netplay.IsBanned(Netplay.Clients[this.whoAmI].Socket.GetRemoteAddress()))
				{
					NetMessage.TrySendData(2, this.whoAmI, -1, Lang.mp[3].ToNetworkText(), 0, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				if (Netplay.Clients[this.whoAmI].State != 0)
				{
					return;
				}
				if (!(this.reader.ReadString() == "Terraria" + 319))
				{
					NetMessage.TrySendData(2, this.whoAmI, -1, Lang.mp[4].ToNetworkText(), 0, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				if (string.IsNullOrEmpty(Netplay.ServerPassword))
				{
					Netplay.Clients[this.whoAmI].State = 1;
					NetMessage.TrySendData(3, this.whoAmI, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				Netplay.Clients[this.whoAmI].State = -1;
				NetMessage.TrySendData(37, this.whoAmI, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				return;
			case 2:
				if (Main.netMode != 1)
				{
					return;
				}
				Netplay.Disconnect = true;
				Main.statusText = NetworkText.Deserialize(this.reader).ToString();
				return;
			case 3:
			{
				if (Main.netMode != 1)
				{
					return;
				}
				if (Netplay.Connection.State == 1)
				{
					Netplay.Connection.State = 2;
				}
				int num2 = (int)this.reader.ReadByte();
				bool flag = this.reader.ReadBoolean();
				Netplay.Connection.ServerSpecialFlags[2] = flag;
				if (num2 != Main.myPlayer)
				{
					Main.player[num2] = Main.ActivePlayerFileData.Player;
					Main.player[Main.myPlayer] = new Player();
				}
				Main.player[num2].whoAmI = num2;
				Main.myPlayer = num2;
				Player player = Main.player[num2];
				NetMessage.TrySendData(4, -1, -1, null, num2, 0f, 0f, 0f, 0, 0, 0);
				NetMessage.TrySendData(68, -1, -1, null, num2, 0f, 0f, 0f, 0, 0, 0);
				NetMessage.TrySendData(16, -1, -1, null, num2, 0f, 0f, 0f, 0, 0, 0);
				NetMessage.TrySendData(42, -1, -1, null, num2, 0f, 0f, 0f, 0, 0, 0);
				NetMessage.TrySendData(50, -1, -1, null, num2, 0f, 0f, 0f, 0, 0, 0);
				NetMessage.TrySendData(147, -1, -1, null, num2, (float)player.CurrentLoadoutIndex, 0f, 0f, 0, 0, 0);
				for (int k = 0; k < 59; k++)
				{
					NetMessage.TrySendData(5, -1, -1, null, num2, (float)(PlayerItemSlotID.Inventory0 + k), 0f, 0f, 0, 0, 0);
				}
				MessageBuffer.TrySendingItemArray(num2, player.armor, PlayerItemSlotID.Armor0);
				MessageBuffer.TrySendingItemArray(num2, player.dye, PlayerItemSlotID.Dye0);
				MessageBuffer.TrySendingItemArray(num2, player.miscEquips, PlayerItemSlotID.Misc0);
				MessageBuffer.TrySendingItemArray(num2, player.miscDyes, PlayerItemSlotID.MiscDye0);
				MessageBuffer.TrySendingItemArray(num2, player.bank.item, PlayerItemSlotID.Bank1_0);
				MessageBuffer.TrySendingItemArray(num2, player.bank2.item, PlayerItemSlotID.Bank2_0);
				NetMessage.TrySendData(5, -1, -1, null, num2, (float)PlayerItemSlotID.TrashItem, 0f, 0f, 0, 0, 0);
				MessageBuffer.TrySendingItemArray(num2, player.bank3.item, PlayerItemSlotID.Bank3_0);
				MessageBuffer.TrySendingItemArray(num2, player.bank4.item, PlayerItemSlotID.Bank4_0);
				MessageBuffer.TrySendingItemArray(num2, player.Loadouts[0].Armor, PlayerItemSlotID.Loadout1_Armor_0);
				MessageBuffer.TrySendingItemArray(num2, player.Loadouts[0].Dye, PlayerItemSlotID.Loadout1_Dye_0);
				MessageBuffer.TrySendingItemArray(num2, player.Loadouts[1].Armor, PlayerItemSlotID.Loadout2_Armor_0);
				MessageBuffer.TrySendingItemArray(num2, player.Loadouts[1].Dye, PlayerItemSlotID.Loadout2_Dye_0);
				MessageBuffer.TrySendingItemArray(num2, player.Loadouts[2].Armor, PlayerItemSlotID.Loadout3_Armor_0);
				MessageBuffer.TrySendingItemArray(num2, player.Loadouts[2].Dye, PlayerItemSlotID.Loadout3_Dye_0);
				if (!string.IsNullOrWhiteSpace(Netplay.HostToken))
				{
					NetMessage.TrySendData(161, -1, -1, NetworkText.FromLiteral(Netplay.HostToken), 0, 0f, 0f, 0f, 0, 0, 0);
				}
				NetMessage.TrySendData(6, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				if (Netplay.Connection.State == 2)
				{
					Netplay.Connection.State = 3;
					return;
				}
				return;
			}
			case 4:
			{
				int num3 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num3 = this.whoAmI;
				}
				if (num3 == Main.myPlayer && !Main.ServerSideCharacter)
				{
					return;
				}
				Player player2 = Main.player[num3];
				player2.whoAmI = num3;
				player2.skinVariant = (int)this.reader.ReadByte();
				player2.skinVariant = (int)MathHelper.Clamp((float)player2.skinVariant, 0f, (float)(PlayerVariantID.Count - 1));
				player2.voiceVariant = (int)this.reader.ReadByte();
				player2.voiceVariant = Utils.Clamp<int>(player2.voiceVariant, 1, 4);
				player2.voicePitchOffset = this.reader.ReadSingle();
				if (float.IsNaN(player2.voicePitchOffset))
				{
					player2.voicePitchOffset = 0f;
				}
				player2.voicePitchOffset = Utils.Clamp<float>(player2.voicePitchOffset, -1f, 1f);
				player2.hair = (int)this.reader.ReadByte();
				if (player2.hair >= 228)
				{
					player2.hair = 0;
				}
				player2.name = this.reader.ReadString().Trim().Trim();
				player2.hairDye = this.reader.ReadByte();
				MessageBuffer.ReadAccessoryVisibility(this.reader, player2.hideVisibleAccessory);
				player2.hideMisc = this.reader.ReadByte();
				player2.hairColor = this.reader.ReadRGB();
				player2.skinColor = this.reader.ReadRGB();
				player2.eyeColor = this.reader.ReadRGB();
				player2.shirtColor = this.reader.ReadRGB();
				player2.underShirtColor = this.reader.ReadRGB();
				player2.pantsColor = this.reader.ReadRGB();
				player2.shoeColor = this.reader.ReadRGB();
				BitsByte bitsByte = this.reader.ReadByte();
				player2.difficulty = 0;
				if (bitsByte[0])
				{
					player2.difficulty = 1;
				}
				if (bitsByte[1])
				{
					player2.difficulty = 2;
				}
				if (bitsByte[3])
				{
					player2.difficulty = 3;
				}
				if (player2.difficulty > 3)
				{
					player2.difficulty = 3;
				}
				player2.extraAccessory = bitsByte[2];
				BitsByte bitsByte2 = this.reader.ReadByte();
				player2.UsingBiomeTorches = bitsByte2[0];
				player2.happyFunTorchTime = bitsByte2[1];
				player2.unlockedBiomeTorches = bitsByte2[2];
				player2.unlockedSuperCart = bitsByte2[3];
				player2.enabledSuperCart = bitsByte2[4];
				BitsByte bitsByte3 = this.reader.ReadByte();
				player2.usedAegisCrystal = bitsByte3[0];
				player2.usedAegisFruit = bitsByte3[1];
				player2.usedArcaneCrystal = bitsByte3[2];
				player2.usedGalaxyPearl = bitsByte3[3];
				player2.usedGummyWorm = bitsByte3[4];
				player2.usedAmbrosia = bitsByte3[5];
				player2.ateArtisanBread = bitsByte3[6];
				if (Main.netMode != 2)
				{
					return;
				}
				bool flag2 = false;
				if (Netplay.Clients[this.whoAmI].State < 10)
				{
					for (int l = 0; l < 255; l++)
					{
						if (l != num3 && player2.name == Main.player[l].name && Netplay.Clients[l].IsActive)
						{
							flag2 = true;
						}
					}
				}
				if (flag2)
				{
					NetMessage.TrySendData(2, this.whoAmI, -1, NetworkText.FromKey(Lang.mp[5].Key, new object[] { player2.name }), 0, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				if (player2.name.Length > Player.nameLen)
				{
					NetMessage.TrySendData(2, this.whoAmI, -1, NetworkText.FromKey("Net.NameTooLong", new object[0]), 0, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				if (player2.name == "")
				{
					NetMessage.TrySendData(2, this.whoAmI, -1, NetworkText.FromKey("Net.EmptyName", new object[0]), 0, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				if (player2.difficulty == 3 && !Main.IsJourneyMode)
				{
					NetMessage.TrySendData(2, this.whoAmI, -1, NetworkText.FromKey("Net.PlayerIsCreativeAndWorldIsNotCreative", new object[0]), 0, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				if (player2.difficulty != 3 && Main.IsJourneyMode)
				{
					NetMessage.TrySendData(2, this.whoAmI, -1, NetworkText.FromKey("Net.PlayerIsNotCreativeAndWorldIsCreative", new object[0]), 0, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				Netplay.Clients[this.whoAmI].Name = player2.name;
				Netplay.Clients[this.whoAmI].Name = player2.name;
				NetMessage.TrySendData(4, -1, this.whoAmI, null, num3, 0f, 0f, 0f, 0, 0, 0);
				return;
			}
			case 5:
			{
				int num4 = (int)this.reader.ReadByte();
				int num5 = (int)this.reader.ReadInt16();
				int num6 = (int)this.reader.ReadInt16();
				int num7 = (int)this.reader.ReadByte();
				int num8 = (int)this.reader.ReadInt16();
				BitsByte bitsByte4 = this.reader.ReadByte();
				bool flag3 = bitsByte4[0];
				bool flag4 = bitsByte4[1];
				if (Main.netMode == 2)
				{
					num4 = this.whoAmI;
				}
				if (num4 == Main.myPlayer && !Main.ServerSideCharacter && !Main.player[num4].HasLockedInventory())
				{
					return;
				}
				Player player3 = Main.player[num4];
				Player player4 = player3;
				lock (player4)
				{
					PlayerItemSlotID.SlotReference slotReference = new PlayerItemSlotID.SlotReference(player3, num5);
					PlayerItemSlotID.SlotReference slotReference2 = new PlayerItemSlotID.SlotReference(Main.clientPlayer, num5);
					Item item = new Item();
					item.SetDefaults(num8, null);
					item.stack = num6;
					item.Prefix(num7);
					item.favorited = flag3;
					slotReference.Item = item;
					if (num4 == Main.myPlayer && !Main.ServerSideCharacter)
					{
						slotReference2.Item = item.Clone();
					}
					if (num5 >= PlayerItemSlotID.Bank4_0 && num5 < PlayerItemSlotID.Loadout1_Armor_0)
					{
						if (Main.netMode == 1 && player3.disableVoidBag == num5 - PlayerItemSlotID.Bank4_0)
						{
							player3.disableVoidBag = -1;
						}
					}
					else if (num5 <= 58)
					{
						if (num4 == Main.myPlayer && num5 == 58)
						{
							Main.mouseItem = item.Clone();
						}
						if (num4 == Main.myPlayer && Main.netMode == 1)
						{
							Main.player[num4].inventoryChestStack[num5] = false;
						}
					}
					if (Main.netMode == 1 && num4 == Main.myPlayer && flag4)
					{
						ItemSlot.IndicateBlockedSlot(slotReference);
					}
					bool[] canRelay = PlayerItemSlotID.CanRelay;
					if (Main.netMode == 2 && num4 == this.whoAmI && canRelay.IndexInRange(num5) && canRelay[num5])
					{
						NetMessage.TrySendData(5, -1, this.whoAmI, null, num4, (float)num5, 0f, 0f, 0, 0, 0);
					}
					return;
				}
				break;
			}
			case 6:
				break;
			case 7:
			{
				if (Main.netMode != 1)
				{
					return;
				}
				Main.time = (double)this.reader.ReadInt32();
				BitsByte bitsByte5 = this.reader.ReadByte();
				Main.dayTime = bitsByte5[0];
				Main.bloodMoon = bitsByte5[1];
				Main.eclipse = bitsByte5[2];
				Main.moonPhase = (int)this.reader.ReadByte();
				Main.maxTilesX = (int)this.reader.ReadInt16();
				Main.maxTilesY = (int)this.reader.ReadInt16();
				Main.spawnTileX = (int)this.reader.ReadInt16();
				Main.spawnTileY = (int)this.reader.ReadInt16();
				Main.worldSurface = (double)this.reader.ReadInt16();
				Main.rockLayer = (double)this.reader.ReadInt16();
				Main.ActiveWorldFileData.WorldId = this.reader.ReadInt32();
				Main.worldName = this.reader.ReadString();
				Main.GameMode = (int)this.reader.ReadByte();
				Main.ActiveWorldFileData.UniqueId = new Guid(this.reader.ReadBytes(16));
				Main.ActiveWorldFileData.WorldGeneratorVersion = this.reader.ReadUInt64();
				Main.moonType = (int)this.reader.ReadByte();
				WorldGen.setBG(0, (int)this.reader.ReadByte());
				WorldGen.setBG(10, (int)this.reader.ReadByte());
				WorldGen.setBG(11, (int)this.reader.ReadByte());
				WorldGen.setBG(12, (int)this.reader.ReadByte());
				WorldGen.setBG(1, (int)this.reader.ReadByte());
				WorldGen.setBG(2, (int)this.reader.ReadByte());
				WorldGen.setBG(3, (int)this.reader.ReadByte());
				WorldGen.setBG(4, (int)this.reader.ReadByte());
				WorldGen.setBG(5, (int)this.reader.ReadByte());
				WorldGen.setBG(6, (int)this.reader.ReadByte());
				WorldGen.setBG(7, (int)this.reader.ReadByte());
				WorldGen.setBG(8, (int)this.reader.ReadByte());
				WorldGen.setBG(9, (int)this.reader.ReadByte());
				Main.iceBackStyle = (int)this.reader.ReadByte();
				Main.jungleBackStyle = (int)this.reader.ReadByte();
				Main.hellBackStyle = (int)this.reader.ReadByte();
				Main.windSpeedTarget = this.reader.ReadSingle();
				Main.numClouds = (int)this.reader.ReadByte();
				for (int m = 0; m < 3; m++)
				{
					Main.treeX[m] = this.reader.ReadInt32();
				}
				for (int n = 0; n < 4; n++)
				{
					Main.treeStyle[n] = (int)this.reader.ReadByte();
				}
				for (int num9 = 0; num9 < 3; num9++)
				{
					Main.caveBackX[num9] = this.reader.ReadInt32();
				}
				for (int num10 = 0; num10 < 4; num10++)
				{
					Main.caveBackStyle[num10] = (int)this.reader.ReadByte();
				}
				WorldGen.TreeTops.SyncReceive(this.reader);
				WorldGen.BackgroundsCache.UpdateCache();
				Main.maxRaining = this.reader.ReadSingle();
				Main.raining = Main.maxRaining > 0f;
				BitsByte bitsByte6 = this.reader.ReadByte();
				WorldGen.shadowOrbSmashed = bitsByte6[0];
				NPC.downedBoss1 = bitsByte6[1];
				NPC.downedBoss2 = bitsByte6[2];
				NPC.downedBoss3 = bitsByte6[3];
				Main.hardMode = bitsByte6[4];
				NPC.downedClown = bitsByte6[5];
				Main.ServerSideCharacter = bitsByte6[6];
				NPC.downedPlantBoss = bitsByte6[7];
				if (Main.ServerSideCharacter)
				{
					Main.ActivePlayerFileData.MarkAsServerSide();
				}
				BitsByte bitsByte7 = this.reader.ReadByte();
				NPC.downedMechBoss1 = bitsByte7[0];
				NPC.downedMechBoss2 = bitsByte7[1];
				NPC.downedMechBoss3 = bitsByte7[2];
				NPC.downedMechBossAny = bitsByte7[3];
				Main.cloudBGActive = (float)(bitsByte7[4] ? 1 : 0);
				WorldGen.crimson = bitsByte7[5];
				Main.pumpkinMoon = bitsByte7[6];
				Main.snowMoon = bitsByte7[7];
				BitsByte bitsByte8 = this.reader.ReadByte();
				Main.fastForwardTimeToDawn = bitsByte8[1];
				Main.UpdateTimeRate();
				bool flag6 = bitsByte8[2];
				NPC.downedSlimeKing = bitsByte8[3];
				NPC.downedQueenBee = bitsByte8[4];
				NPC.downedFishron = bitsByte8[5];
				NPC.downedMartians = bitsByte8[6];
				NPC.downedAncientCultist = bitsByte8[7];
				BitsByte bitsByte9 = this.reader.ReadByte();
				NPC.downedMoonlord = bitsByte9[0];
				NPC.downedHalloweenKing = bitsByte9[1];
				NPC.downedHalloweenTree = bitsByte9[2];
				NPC.downedChristmasIceQueen = bitsByte9[3];
				NPC.downedChristmasSantank = bitsByte9[4];
				NPC.downedChristmasTree = bitsByte9[5];
				NPC.downedGolemBoss = bitsByte9[6];
				BirthdayParty.ManualParty = bitsByte9[7];
				BitsByte bitsByte10 = this.reader.ReadByte();
				NPC.downedPirates = bitsByte10[0];
				NPC.downedFrost = bitsByte10[1];
				NPC.downedGoblins = bitsByte10[2];
				Sandstorm.Happening = bitsByte10[3];
				DD2Event.Ongoing = bitsByte10[4];
				DD2Event.DownedInvasionT1 = bitsByte10[5];
				DD2Event.DownedInvasionT2 = bitsByte10[6];
				DD2Event.DownedInvasionT3 = bitsByte10[7];
				BitsByte bitsByte11 = this.reader.ReadByte();
				NPC.combatBookWasUsed = bitsByte11[0];
				LanternNight.ManualLanterns = bitsByte11[1];
				NPC.downedTowerSolar = bitsByte11[2];
				NPC.downedTowerVortex = bitsByte11[3];
				NPC.downedTowerNebula = bitsByte11[4];
				NPC.downedTowerStardust = bitsByte11[5];
				Main.forceHalloweenForToday = bitsByte11[6];
				Main.forceXMasForToday = bitsByte11[7];
				BitsByte bitsByte12 = this.reader.ReadByte();
				NPC.boughtCat = bitsByte12[0];
				NPC.boughtDog = bitsByte12[1];
				NPC.boughtBunny = bitsByte12[2];
				NPC.freeCake = bitsByte12[3];
				Main.drunkWorld = bitsByte12[4];
				NPC.downedEmpressOfLight = bitsByte12[5];
				NPC.downedQueenSlime = bitsByte12[6];
				Main.getGoodWorld = bitsByte12[7];
				BitsByte bitsByte13 = this.reader.ReadByte();
				Main.tenthAnniversaryWorld = bitsByte13[0];
				Main.dontStarveWorld = bitsByte13[1];
				NPC.downedDeerclops = bitsByte13[2];
				Main.notTheBeesWorld = bitsByte13[3];
				Main.remixWorld = bitsByte13[4];
				NPC.unlockedSlimeBlueSpawn = bitsByte13[5];
				NPC.combatBookVolumeTwoWasUsed = bitsByte13[6];
				NPC.peddlersSatchelWasUsed = bitsByte13[7];
				BitsByte bitsByte14 = this.reader.ReadByte();
				NPC.unlockedSlimeGreenSpawn = bitsByte14[0];
				NPC.unlockedSlimeOldSpawn = bitsByte14[1];
				NPC.unlockedSlimePurpleSpawn = bitsByte14[2];
				NPC.unlockedSlimeRainbowSpawn = bitsByte14[3];
				NPC.unlockedSlimeRedSpawn = bitsByte14[4];
				NPC.unlockedSlimeYellowSpawn = bitsByte14[5];
				NPC.unlockedSlimeCopperSpawn = bitsByte14[6];
				Main.fastForwardTimeToDusk = bitsByte14[7];
				BitsByte bitsByte15 = this.reader.ReadByte();
				Main.noTrapsWorld = bitsByte15[0];
				Main.zenithWorld = bitsByte15[1];
				NPC.unlockedTruffleSpawn = bitsByte15[2];
				Main.vampireSeed = bitsByte15[3];
				Main.infectedSeed = bitsByte15[4];
				Main.teamBasedSpawnsSeed = bitsByte15[5];
				Main.skyblockWorld = bitsByte15[6];
				Main.dualDungeonsSeed = bitsByte15[7];
				WorldGen.Skyblock.lowTiles = this.reader.ReadByte()[0];
				Main.sundialCooldown = (int)this.reader.ReadByte();
				Main.moondialCooldown = (int)this.reader.ReadByte();
				WorldGen.SavedOreTiers.Copper = (int)this.reader.ReadInt16();
				WorldGen.SavedOreTiers.Iron = (int)this.reader.ReadInt16();
				WorldGen.SavedOreTiers.Silver = (int)this.reader.ReadInt16();
				WorldGen.SavedOreTiers.Gold = (int)this.reader.ReadInt16();
				WorldGen.SavedOreTiers.Cobalt = (int)this.reader.ReadInt16();
				WorldGen.SavedOreTiers.Mythril = (int)this.reader.ReadInt16();
				WorldGen.SavedOreTiers.Adamantite = (int)this.reader.ReadInt16();
				if (flag6)
				{
					Main.StartSlimeRain(false);
				}
				else
				{
					Main.StopSlimeRain(true);
				}
				Main.invasionType = (int)this.reader.ReadSByte();
				Main.LobbyId = this.reader.ReadUInt64();
				Sandstorm.IntendedSeverity = this.reader.ReadSingle();
				ExtraSpawnPointManager.Read(this.reader, true);
				if (Netplay.Connection.State == 3)
				{
					Main.windSpeedCurrent = Main.windSpeedTarget;
					Netplay.Connection.State = 4;
				}
				Main.checkHalloween();
				Main.checkXMas();
				return;
			}
			case 8:
			{
				if (Main.netMode != 2)
				{
					return;
				}
				NetMessage.TrySendData(7, this.whoAmI, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				int num11 = this.reader.ReadInt32();
				int num12 = this.reader.ReadInt32();
				int num13 = (int)this.reader.ReadByte();
				bool flag7 = true;
				if (num11 == -1 || num12 == -1)
				{
					flag7 = false;
				}
				else if (num11 < 10 || num11 > Main.maxTilesX - 10)
				{
					flag7 = false;
				}
				else if (num12 < 10 || num12 > Main.maxTilesY - 10)
				{
					flag7 = false;
				}
				bool flag8 = false;
				if (Main.teamBasedSpawnsSeed && num13 != 0)
				{
					flag8 = true;
				}
				int num14 = Netplay.GetSectionX(Main.spawnTileX) - 2;
				int num15 = Netplay.GetSectionY(Main.spawnTileY) - 1;
				int num16 = num14 + 5;
				int num17 = num15 + 3;
				if (num14 < 0)
				{
					num14 = 0;
				}
				if (num16 >= Main.maxSectionsX)
				{
					num16 = Main.maxSectionsX;
				}
				if (num15 < 0)
				{
					num15 = 0;
				}
				if (num17 >= Main.maxSectionsY)
				{
					num17 = Main.maxSectionsY;
				}
				int num18 = (num16 - num14) * (num17 - num15);
				List<Point> list = new List<Point>();
				for (int num19 = num14; num19 < num16; num19++)
				{
					for (int num20 = num15; num20 < num17; num20++)
					{
						list.Add(new Point(num19, num20));
					}
				}
				int num21 = -1;
				int num22 = -1;
				if (flag7)
				{
					num11 = Netplay.GetSectionX(num11) - 2;
					num12 = Netplay.GetSectionY(num12) - 1;
					num21 = num11 + 5;
					num22 = num12 + 3;
					if (num11 < 0)
					{
						num11 = 0;
					}
					if (num21 >= Main.maxSectionsX)
					{
						num21 = Main.maxSectionsX - 1;
					}
					if (num12 < 0)
					{
						num12 = 0;
					}
					if (num22 >= Main.maxSectionsY)
					{
						num22 = Main.maxSectionsY - 1;
					}
					for (int num23 = num11; num23 <= num21; num23++)
					{
						for (int num24 = num12; num24 <= num22; num24++)
						{
							if (num23 < num14 || num23 >= num16 || num24 < num15 || num24 >= num17)
							{
								list.Add(new Point(num23, num24));
								num18++;
							}
						}
					}
				}
				int num25 = -1;
				int num26 = -1;
				int num27 = -1;
				int num28 = -1;
				if (flag8)
				{
					Point zero = Point.Zero;
					if (ExtraSpawnPointManager.TryGetExtraSpawnPointForTeam(num13, out zero))
					{
						num25 = zero.X;
						num26 = zero.Y;
						num25 = Netplay.GetSectionX(num25) - 2;
						num26 = Netplay.GetSectionY(num26) - 1;
						num27 = num25 + 5;
						num28 = num26 + 3;
						if (num25 < 0)
						{
							num25 = 0;
						}
						if (num27 >= Main.maxSectionsX)
						{
							num27 = Main.maxSectionsX - 1;
						}
						if (num26 < 0)
						{
							num26 = 0;
						}
						if (num28 >= Main.maxSectionsY)
						{
							num28 = Main.maxSectionsY - 1;
						}
						for (int num29 = num25; num29 <= num27; num29++)
						{
							for (int num30 = num26; num30 <= num28; num30++)
							{
								if ((num29 < num14 || num29 >= num16 || num30 < num15 || num30 >= num17) && (num29 < num11 || num29 >= num21 || num30 < num12 || num30 >= num22))
								{
									list.Add(new Point(num29, num30));
									num18++;
								}
							}
						}
					}
					else
					{
						flag8 = false;
					}
				}
				List<Point> list2;
				PortalHelper.SyncPortalsOnPlayerJoin(this.whoAmI, 1, list, out list2);
				num18 += list2.Count;
				if (Netplay.Clients[this.whoAmI].State == 2)
				{
					Netplay.Clients[this.whoAmI].State = 3;
				}
				NetMessage.TrySendData(9, this.whoAmI, -1, Lang.inter[44].ToNetworkText(), num18, 0f, 0f, 0f, 0, 0, 0);
				Netplay.Clients[this.whoAmI].StatusText2 = Language.GetTextValue("Net.IsReceivingTileData");
				Netplay.Clients[this.whoAmI].StatusMax += num18;
				for (int num31 = num14; num31 < num16; num31++)
				{
					for (int num32 = num15; num32 < num17; num32++)
					{
						NetMessage.SendSection(this.whoAmI, num31, num32);
					}
				}
				if (flag7)
				{
					for (int num33 = num11; num33 <= num21; num33++)
					{
						for (int num34 = num12; num34 <= num22; num34++)
						{
							NetMessage.SendSection(this.whoAmI, num33, num34);
						}
					}
				}
				if (flag8)
				{
					for (int num35 = num25; num35 <= num27; num35++)
					{
						for (int num36 = num26; num36 <= num28; num36++)
						{
							NetMessage.SendSection(this.whoAmI, num35, num36);
						}
					}
				}
				for (int num37 = 0; num37 < list2.Count; num37++)
				{
					NetMessage.SendSection(this.whoAmI, list2[num37].X, list2[num37].Y);
				}
				for (int num38 = 0; num38 < 400; num38++)
				{
					if (Main.item[num38].active)
					{
						NetMessage.TrySendData(21, this.whoAmI, -1, null, num38, 0f, 0f, 0f, 0, 0, 0);
						NetMessage.TrySendData(22, this.whoAmI, -1, null, num38, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				for (int num39 = 0; num39 < Main.maxNPCs; num39++)
				{
					if (Main.npc[num39].active)
					{
						NetMessage.TrySendData(23, this.whoAmI, -1, null, num39, 0f, 0f, 0f, 0, 0, 0);
						NetMessage.TrySendData(54, this.whoAmI, -1, null, num39, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				for (int num40 = 0; num40 < 1000; num40++)
				{
					if (Main.projectile[num40].active && (Main.projPet[Main.projectile[num40].type] || Main.projectile[num40].netImportant))
					{
						NetMessage.TrySendData(27, this.whoAmI, -1, null, num40, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				NetManager.Instance.SendToClient(BannerSystem.NetBannersModule.WriteFullState(), this.whoAmI);
				NetMessage.TrySendData(57, this.whoAmI, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				NetMessage.TrySendData(103, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				NetMessage.TrySendData(101, this.whoAmI, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				NetMessage.TrySendData(136, this.whoAmI, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				Main.BestiaryTracker.OnPlayerJoining(this.whoAmI);
				CreativePowerManager.Instance.SyncThingsToJoiningPlayer(this.whoAmI);
				Main.PylonSystem.OnPlayerJoining(this.whoAmI);
				NetMessage.TrySendData(49, this.whoAmI, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				return;
			}
			case 9:
			{
				if (Main.netMode != 1)
				{
					return;
				}
				Netplay.Connection.StatusMax += this.reader.ReadInt32();
				Netplay.Connection.StatusText = NetworkText.Deserialize(this.reader).ToString();
				BitsByte bitsByte16 = this.reader.ReadByte();
				BitsByte serverSpecialFlags = Netplay.Connection.ServerSpecialFlags;
				serverSpecialFlags[0] = bitsByte16[0];
				serverSpecialFlags[1] = bitsByte16[1];
				Netplay.Connection.ServerSpecialFlags = serverSpecialFlags;
				return;
			}
			case 10:
				if (Main.netMode != 1)
				{
					return;
				}
				NetMessage.DecompressTileBlock(this.reader.BaseStream);
				return;
			case 11:
				if (Main.netMode != 1)
				{
					return;
				}
				WorldGen.SectionTileFrame((int)this.reader.ReadInt16(), (int)this.reader.ReadInt16(), (int)this.reader.ReadInt16(), (int)this.reader.ReadInt16());
				return;
			case 12:
			{
				int num41 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num41 = this.whoAmI;
				}
				Player player5 = Main.player[num41];
				player5.SpawnX = (int)this.reader.ReadInt16();
				player5.SpawnY = (int)this.reader.ReadInt16();
				player5.respawnTimer = this.reader.ReadInt32();
				player5.numberOfDeathsPVE = (int)this.reader.ReadInt16();
				player5.numberOfDeathsPVP = (int)this.reader.ReadInt16();
				player5.team = (int)this.reader.ReadByte();
				if (player5.respawnTimer > 0)
				{
					player5.dead = true;
				}
				PlayerSpawnContext playerSpawnContext = (PlayerSpawnContext)this.reader.ReadByte();
				player5.Spawn(playerSpawnContext);
				if (Main.netMode != 2 || Netplay.Clients[this.whoAmI].State < 3)
				{
					return;
				}
				if (Netplay.Clients[this.whoAmI].State != 3)
				{
					NetMessage.TrySendData(12, -1, this.whoAmI, null, this.whoAmI, (float)((byte)playerSpawnContext), 0f, 0f, 0, 0, 0);
					return;
				}
				Netplay.Clients[this.whoAmI].State = 10;
				NetMessage.buffer[this.whoAmI].broadcast = true;
				NetMessage.SyncConnectedPlayer(this.whoAmI);
				bool flag9 = NetMessage.DoesPlayerSlotCountAsAHost(this.whoAmI);
				Main.countsAsHostForGameplay[this.whoAmI] = flag9;
				if (NetMessage.DoesPlayerSlotCountAsAHost(this.whoAmI))
				{
					NetMessage.TrySendData(139, this.whoAmI, -1, null, this.whoAmI, (float)flag9.ToInt(), 0f, 0f, 0, 0, 0);
				}
				NetMessage.TrySendData(12, -1, this.whoAmI, null, this.whoAmI, (float)((byte)playerSpawnContext), 0f, 0f, 0, 0, 0);
				NetMessage.TrySendData(129, this.whoAmI, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				NetMessage.greetPlayer(this.whoAmI);
				if (Main.player[num41].unlockedBiomeTorches)
				{
					NPC npc = new NPC();
					npc.SetDefaults(664, default(NPCSpawnParams));
					Main.BestiaryTracker.Kills.RegisterKill(npc);
					return;
				}
				return;
			}
			case 13:
			{
				int num42 = (int)this.reader.ReadByte();
				if (num42 == Main.myPlayer && !Main.ServerSideCharacter)
				{
					return;
				}
				if (Main.netMode == 2)
				{
					num42 = this.whoAmI;
				}
				Player player6 = Main.player[num42];
				BitsByte bitsByte17 = this.reader.ReadByte();
				BitsByte bitsByte18 = this.reader.ReadByte();
				BitsByte bitsByte19 = this.reader.ReadByte();
				BitsByte bitsByte20 = this.reader.ReadByte();
				player6.controlUp = bitsByte17[0];
				player6.controlDown = bitsByte17[1];
				player6.controlLeft = bitsByte17[2];
				player6.controlRight = bitsByte17[3];
				player6.controlJump = bitsByte17[4];
				player6.controlUseItem = bitsByte17[5];
				player6.direction = (bitsByte17[6] ? 1 : (-1));
				if (bitsByte18[0])
				{
					player6.pulley = true;
					player6.pulleyDir = (bitsByte18[1] ? 2 : 1);
				}
				else
				{
					player6.pulley = false;
				}
				player6.vortexStealthActive = bitsByte18[3];
				player6.gravDir = (float)(bitsByte18[4] ? 1 : (-1));
				player6.TryTogglingShield(bitsByte18[5]);
				player6.ghost = bitsByte18[6];
				player6.selectedItemState.Select((int)this.reader.ReadByte());
				Vector2 vector = this.reader.ReadVector2();
				Vector2 vector2 = Vector2.Zero;
				if (bitsByte18[2])
				{
					vector2 = this.reader.ReadVector2();
				}
				if (player6.unacknowledgedTeleports > 0)
				{
					vector = player6.position;
					vector2 = player6.velocity;
				}
				if (Main.netMode == 1 && player6.position != Vector2.Zero)
				{
					player6.netOffset += player6.position - vector;
					if (player6.netOffset.Length() > (float)Main.multiplayerNPCSmoothingRange)
					{
						player6.netOffset = Vector2.Zero;
					}
					if (player6.netOffset != Vector2.Zero && DebugOptions.ShowNetOffsetDust && Vector2.Distance(vector, player6.position) > 4f)
					{
						Dust.QuickDustLine(vector, player6.position, 20f, Color.Red);
					}
				}
				player6.position = vector;
				player6.velocity = vector2;
				Vector2 position = player6.position;
				if (bitsByte18[7])
				{
					player6.mount.SetMount((int)this.reader.ReadUInt16(), player6);
				}
				else
				{
					player6.mount.Dismount(player6, false);
				}
				if (bitsByte19[6])
				{
					player6.PotionOfReturnOriginalUsePosition = new Vector2?(this.reader.ReadVector2());
					player6.PotionOfReturnHomePosition = new Vector2?(this.reader.ReadVector2());
				}
				else
				{
					player6.PotionOfReturnOriginalUsePosition = null;
					player6.PotionOfReturnHomePosition = null;
				}
				player6.tryKeepingHoveringUp = bitsByte19[0];
				player6.IsVoidVaultEnabled = bitsByte19[1];
				player6.sitting.isSitting = bitsByte19[2];
				player6.downedDD2EventAnyDifficulty = bitsByte19[3];
				player6.petting.isPetting = bitsByte19[4];
				player6.petting.isPetSmall = bitsByte19[5];
				player6.tryKeepingHoveringDown = bitsByte19[7];
				player6.sleeping.SetIsSleepingAndAdjustPlayerRotation(player6, bitsByte20[0]);
				player6.autoReuseAllWeapons = bitsByte20[1];
				player6.controlDownHold = bitsByte20[2];
				player6.isOperatingAnotherEntity = bitsByte20[3];
				player6.controlUseTile = bitsByte20[4];
				player6.netCameraTarget = (bitsByte20[5] ? new Vector2?(this.reader.ReadVector2()) : null);
				player6.lastItemUseAttemptSuccess = bitsByte20[6];
				Utils.Swap<Vector2>(ref position, ref player6.position);
				if (Main.netMode == 2 && Netplay.Clients[this.whoAmI].State == 10)
				{
					NetMessage.TrySendData(13, -1, this.whoAmI, null, num42, 0f, 0f, 0f, 0, 0, 0);
				}
				Utils.Swap<Vector2>(ref position, ref player6.position);
				return;
			}
			case 14:
			{
				int num43 = (int)this.reader.ReadByte();
				int num44 = (int)this.reader.ReadByte();
				if (Main.netMode != 1)
				{
					return;
				}
				bool active = Main.player[num43].active;
				if (num44 == 1)
				{
					if (!Main.player[num43].active)
					{
						Main.player[num43] = new Player();
					}
					Main.player[num43].active = true;
				}
				else
				{
					Main.player[num43].active = false;
				}
				if (active == Main.player[num43].active)
				{
					return;
				}
				if (Main.player[num43].active)
				{
					Player.Hooks.PlayerConnect(num43);
					return;
				}
				Player.Hooks.PlayerDisconnect(num43);
				return;
			}
			case 15:
			case 25:
			case 26:
			case 44:
			case 67:
			case 83:
			case 93:
				return;
			case 16:
			{
				int num45 = (int)this.reader.ReadByte();
				if (num45 == Main.myPlayer && !Main.ServerSideCharacter)
				{
					return;
				}
				if (Main.netMode == 2)
				{
					num45 = this.whoAmI;
				}
				Player player7 = Main.player[num45];
				player7.statLife = (int)this.reader.ReadInt16();
				player7.statLifeMax = (int)this.reader.ReadInt16();
				if (player7.statLifeMax < 20)
				{
					player7.statLifeMax = 20;
				}
				player7.dead = player7.statLife <= 0;
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(16, -1, this.whoAmI, null, num45, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 17:
			{
				byte b3 = this.reader.ReadByte();
				int num46 = (int)this.reader.ReadInt16();
				int num47 = (int)this.reader.ReadInt16();
				short num48 = this.reader.ReadInt16();
				int num49 = (int)this.reader.ReadByte();
				bool flag10 = num48 == 1;
				if (!WorldGen.InWorld(num46, num47, 3))
				{
					return;
				}
				if (Main.tile[num46, num47] == null)
				{
					Main.tile[num46, num47] = new Tile();
				}
				if (Main.netMode == 2)
				{
					if (!flag10)
					{
						if (b3 == 0 || b3 == 2 || b3 == 4)
						{
							Netplay.Clients[this.whoAmI].SpamDeleteBlock += 1f;
						}
						if (b3 == 1 || b3 == 3)
						{
							Netplay.Clients[this.whoAmI].SpamAddBlock += 1f;
						}
					}
					if (!Netplay.Clients[this.whoAmI].TileSections[Netplay.GetSectionX(num46), Netplay.GetSectionY(num47)])
					{
						flag10 = true;
					}
				}
				MapUpdateQueue.Add(num46, num47);
				if (b3 == 0)
				{
					WorldGen.KillTile(num46, num47, flag10, false, false);
					if (Main.netMode == 1 && !flag10)
					{
						HitTile.ClearAllTilesAtThisLocation(num46, num47);
					}
				}
				bool flag11 = false;
				if (b3 == 1)
				{
					bool flag12 = true;
					if (WorldGen.CheckTileBreakability2_ShouldTileSurvive(num46, num47))
					{
						flag11 = true;
						flag12 = false;
					}
					WorldGen.PlaceTile(num46, num47, (int)num48, false, flag12, -1, num49);
				}
				if (b3 == 2)
				{
					WorldGen.KillWall(num46, num47, flag10);
				}
				if (b3 == 3)
				{
					WorldGen.PlaceWall(num46, num47, (int)num48, false);
				}
				if (b3 == 4)
				{
					WorldGen.KillTile(num46, num47, flag10, false, true);
				}
				if (b3 == 5)
				{
					WorldGen.PlaceWire(num46, num47);
				}
				if (b3 == 6)
				{
					WorldGen.KillWire(num46, num47);
				}
				if (b3 == 7)
				{
					WorldGen.PoundTile(num46, num47);
				}
				if (b3 == 8)
				{
					WorldGen.PlaceActuator(num46, num47);
				}
				if (b3 == 9)
				{
					WorldGen.KillActuator(num46, num47);
				}
				if (b3 == 10)
				{
					WorldGen.PlaceWire2(num46, num47);
				}
				if (b3 == 11)
				{
					WorldGen.KillWire2(num46, num47);
				}
				if (b3 == 12)
				{
					WorldGen.PlaceWire3(num46, num47);
				}
				if (b3 == 13)
				{
					WorldGen.KillWire3(num46, num47);
				}
				if (b3 == 14)
				{
					WorldGen.SlopeTile(num46, num47, (int)num48, false, true);
				}
				if (b3 == 15)
				{
					Minecart.FrameTrack(num46, num47, true, false);
				}
				if (b3 == 16)
				{
					WorldGen.PlaceWire4(num46, num47);
				}
				if (b3 == 17)
				{
					WorldGen.KillWire4(num46, num47);
				}
				if (b3 == 18)
				{
					Wiring.SetCurrentUser(this.whoAmI);
					Wiring.PokeLogicGate(num46, num47);
					Wiring.SetCurrentUser(-1);
					return;
				}
				if (b3 == 19)
				{
					Wiring.SetCurrentUser(this.whoAmI);
					Wiring.Actuate(num46, num47);
					Wiring.SetCurrentUser(-1);
					return;
				}
				if (b3 == 20)
				{
					if (!WorldGen.InWorld(num46, num47, 2))
					{
						return;
					}
					int type = (int)Main.tile[num46, num47].type;
					WorldGen.KillTile(num46, num47, flag10, false, false);
					num48 = ((Main.tile[num46, num47].active() && (int)Main.tile[num46, num47].type == type) ? 1 : 0);
					if (Main.netMode == 2)
					{
						NetMessage.TrySendData(17, -1, -1, null, (int)b3, (float)num46, (float)num47, (float)num48, num49, 0, 0);
						return;
					}
					return;
				}
				else
				{
					if (b3 == 21)
					{
						WorldGen.ReplaceTile(num46, num47, (int)((ushort)num48), num49);
					}
					if (b3 == 22)
					{
						WorldGen.ReplaceWall(num46, num47, (ushort)num48);
					}
					if (b3 == 23 && WorldGen.CanPoundTile(num46, num47))
					{
						Main.tile[num46, num47].slope((byte)num48);
						WorldGen.PoundTile(num46, num47);
					}
					if (Main.netMode != 2)
					{
						return;
					}
					if (flag11)
					{
						NetMessage.SendTileSquare(-1, num46, num47, 5, TileChangeType.None);
						return;
					}
					if ((b3 != 1 && b3 != 21) || !TileID.Sets.Falling[(int)num48] || Main.tile[num46, num47].active())
					{
						NetMessage.TrySendData(17, -1, this.whoAmI, null, (int)b3, (float)num46, (float)num47, (float)num48, num49, 0, 0);
						return;
					}
					return;
				}
				break;
			}
			case 18:
				if (Main.netMode != 1)
				{
					return;
				}
				Main.dayTime = this.reader.ReadByte() == 1;
				Main.time = (double)this.reader.ReadInt32();
				Main.sunModY = this.reader.ReadInt16();
				Main.moonModY = this.reader.ReadInt16();
				return;
			case 19:
			{
				byte b4 = this.reader.ReadByte();
				int num50 = (int)this.reader.ReadInt16();
				int num51 = (int)this.reader.ReadInt16();
				if (!WorldGen.InWorld(num50, num51, 3))
				{
					return;
				}
				int num52 = ((this.reader.ReadByte() == 0) ? (-1) : 1);
				if (b4 == 0)
				{
					WorldGen.OpenDoor(num50, num51, num52);
				}
				else if (b4 == 1)
				{
					WorldGen.CloseDoor(num50, num51, true);
				}
				else if (b4 == 2)
				{
					WorldGen.ShiftTrapdoor(num50, num51, num52 == 1, 1);
				}
				else if (b4 == 3)
				{
					WorldGen.ShiftTrapdoor(num50, num51, num52 == 1, 0);
				}
				else if (b4 == 4)
				{
					WorldGen.ShiftTallGate(num50, num51, false, true);
				}
				else if (b4 == 5)
				{
					WorldGen.ShiftTallGate(num50, num51, true, true);
				}
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(19, -1, this.whoAmI, null, (int)b4, (float)num50, (float)num51, (float)((num52 == 1) ? 1 : 0), 0, 0, 0);
					return;
				}
				return;
			}
			case 20:
			{
				int num53 = (int)this.reader.ReadInt16();
				int num54 = (int)this.reader.ReadInt16();
				ushort num55 = (ushort)this.reader.ReadByte();
				ushort num56 = (ushort)this.reader.ReadByte();
				byte b5 = this.reader.ReadByte();
				if (!WorldGen.InWorld(num53, num54, 3))
				{
					return;
				}
				TileChangeType tileChangeType = TileChangeType.None;
				if (Enum.IsDefined(typeof(TileChangeType), b5))
				{
					tileChangeType = (TileChangeType)b5;
				}
				if (MessageBuffer.OnTileChangeReceived != null)
				{
					MessageBuffer.OnTileChangeReceived(num53, num54, (int)Math.Max(num55, num56), tileChangeType);
				}
				BitsByte bitsByte21 = 0;
				BitsByte bitsByte22 = 0;
				BitsByte bitsByte23 = 0;
				for (int num57 = num53; num57 < num53 + (int)num55; num57++)
				{
					for (int num58 = num54; num58 < num54 + (int)num56; num58++)
					{
						if (Main.tile[num57, num58] == null)
						{
							Main.tile[num57, num58] = new Tile();
						}
						Tile tile = Main.tile[num57, num58];
						bool flag13 = tile.active();
						bitsByte21 = this.reader.ReadByte();
						bitsByte22 = this.reader.ReadByte();
						bitsByte23 = this.reader.ReadByte();
						tile.active(bitsByte21[0]);
						tile.wall = (ushort)(bitsByte21[2] ? 1 : 0);
						bool flag14 = bitsByte21[3];
						if (Main.netMode != 2)
						{
							tile.liquid = (flag14 ? 1 : 0);
						}
						tile.wire(bitsByte21[4]);
						tile.halfBrick(bitsByte21[5]);
						tile.actuator(bitsByte21[6]);
						tile.inActive(bitsByte21[7]);
						tile.wire2(bitsByte22[0]);
						tile.wire3(bitsByte22[1]);
						if (bitsByte22[2])
						{
							tile.color(this.reader.ReadByte());
						}
						if (bitsByte22[3])
						{
							tile.wallColor(this.reader.ReadByte());
						}
						if (tile.active())
						{
							int type2 = (int)tile.type;
							tile.type = this.reader.ReadUInt16();
							if (Main.tileFrameImportant[(int)tile.type])
							{
								tile.frameX = this.reader.ReadInt16();
								tile.frameY = this.reader.ReadInt16();
							}
							else if (!flag13 || (int)tile.type != type2)
							{
								tile.frameX = -1;
								tile.frameY = -1;
							}
							byte b6 = 0;
							if (bitsByte22[4])
							{
								b6 += 1;
							}
							if (bitsByte22[5])
							{
								b6 += 2;
							}
							if (bitsByte22[6])
							{
								b6 += 4;
							}
							tile.slope(b6);
						}
						tile.wire4(bitsByte22[7]);
						tile.fullbrightBlock(bitsByte23[0]);
						tile.fullbrightWall(bitsByte23[1]);
						tile.invisibleBlock(bitsByte23[2]);
						tile.invisibleWall(bitsByte23[3]);
						if (tile.wall > 0)
						{
							tile.wall = this.reader.ReadUInt16();
						}
						if (flag14)
						{
							tile.liquid = this.reader.ReadByte();
							tile.liquidType((int)this.reader.ReadByte());
						}
					}
				}
				WorldGen.RangeFrame(num53, num54, num53 + (int)num55, num54 + (int)num56);
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData((int)b, -1, this.whoAmI, null, num53, (float)num54, (float)num55, (float)num56, (int)b5, 0, 0);
					return;
				}
				return;
			}
			case 21:
			case 90:
			case 145:
			case 148:
			{
				int num59 = (int)this.reader.ReadInt16();
				Vector2 vector3 = this.reader.ReadVector2();
				Vector2 vector4 = this.reader.ReadVector2();
				int num60 = (int)this.reader.ReadInt16();
				int num61 = (int)this.reader.ReadByte();
				BitsByte bitsByte24 = this.reader.ReadByte();
				bool flag15 = bitsByte24[0];
				bool flag16 = bitsByte24[1];
				int num62 = (int)this.reader.ReadInt16();
				bool flag17 = false;
				float num63 = 0f;
				int num64 = 0;
				if (b == 145)
				{
					flag17 = this.reader.ReadBoolean();
					num63 = this.reader.ReadSingle();
				}
				if (b == 148)
				{
					num64 = (int)this.reader.ReadByte();
				}
				WorldItem worldItem = Main.item[num59];
				if (Main.netMode == 1)
				{
					ItemSyncPersistentStats itemSyncPersistentStats = default(ItemSyncPersistentStats);
					itemSyncPersistentStats.CopyFrom(worldItem);
					bool flag18 = (worldItem.newAndShiny || worldItem.type != num62) && ItemSlot.Options.HighlightNewItems && (num62 < 0 || num62 >= (int)ItemID.Count || !ItemID.Sets.NeverAppearsAsNewInInventory[num62]);
					worldItem.SetDefaults(num62);
					worldItem.newAndShiny = flag18;
					worldItem.Prefix(num61);
					worldItem.stack = num60;
					worldItem.position = vector3;
					worldItem.velocity = vector4;
					worldItem.shimmered = flag17;
					worldItem.shimmerTime = num63;
					if (b == 90)
					{
						worldItem.instanced = true;
						worldItem.playerIndexTheItemIsReservedFor = Main.myPlayer;
						worldItem.keepTime = 600;
					}
					else if (flag16)
					{
						worldItem.keepTime = 100;
					}
					worldItem.timeLeftInWhichTheItemCannotBeTakenByEnemies = num64;
					worldItem.wet = Collision.WetCollision(worldItem.position, worldItem.width, worldItem.height);
					itemSyncPersistentStats.PasteInto(worldItem);
					return;
				}
				if (Main.timeItemSlotCannotBeReusedFor[num59] > 0)
				{
					return;
				}
				bool flag19 = num59 == 400;
				if (flag19)
				{
					Item item2 = new Item();
					item2.SetDefaults(num62, null);
					num59 = Item.NewItem(new EntitySource_Sync(), (int)vector3.X, (int)vector3.Y, item2.width, item2.height, item2.type, num60, true, 0, false);
					worldItem = Main.item[num59];
					flag16 = (bitsByte24[1] = !flag15);
				}
				else
				{
					int num65 = worldItem.timeSinceTheItemHasBeenReservedForSomeone;
					if (worldItem.playerIndexTheItemIsReservedFor != this.whoAmI)
					{
						num65 = 0;
					}
					worldItem.playerIndexTheItemIsReservedFor = 255;
					worldItem.SetDefaults(num62);
					worldItem.playerIndexTheItemIsReservedFor = this.whoAmI;
					worldItem.timeSinceTheItemHasBeenReservedForSomeone = num65;
				}
				worldItem.Prefix(num61);
				worldItem.stack = num60;
				worldItem.position = vector3;
				worldItem.velocity = vector4;
				worldItem.timeLeftInWhichTheItemCannotBeTakenByEnemies = num64;
				if (b == 145)
				{
					worldItem.shimmered = flag17;
					worldItem.shimmerTime = num63;
				}
				if (flag16)
				{
					worldItem.ownIgnore = this.whoAmI;
					worldItem.ownTime = 100;
				}
				if (flag19)
				{
					NetMessage.TrySendData((int)b, -1, -1, null, num59, (float)bitsByte24, 0f, 0f, 0, 0, 0);
					Main.item[num59].FindOwner();
					return;
				}
				NetMessage.TrySendData((int)b, -1, this.whoAmI, null, num59, 0f, 0f, 0f, 0, 0, 0);
				return;
			}
			case 22:
			{
				int num66 = (int)this.reader.ReadInt16();
				int num67 = (int)this.reader.ReadByte();
				Vector2 vector5 = this.reader.ReadVector2();
				WorldItem worldItem2 = Main.item[num66];
				if (Main.netMode == 2)
				{
					return;
				}
				worldItem2.playerIndexTheItemIsReservedFor = num67;
				worldItem2.position = vector5;
				if (num67 == Main.myPlayer)
				{
					worldItem2.keepTime = Math.Max(worldItem2.keepTime, 15);
					return;
				}
				worldItem2.keepTime = 0;
				return;
			}
			case 23:
			{
				if (Main.netMode != 1)
				{
					return;
				}
				int num68 = (int)this.reader.ReadInt16();
				Vector2 vector6 = this.reader.ReadVector2();
				Vector2 vector7 = this.reader.ReadVector2();
				int num69 = (int)this.reader.ReadUInt16();
				if (num69 == 65535)
				{
					num69 = 0;
				}
				BitsByte bitsByte25 = this.reader.ReadByte();
				BitsByte bitsByte26 = this.reader.ReadByte();
				float[] array = this.ReUseTemporaryNPCAI();
				for (int num70 = 0; num70 < NPC.maxAI; num70++)
				{
					if (bitsByte25[num70 + 2])
					{
						array[num70] = this.reader.ReadSingle();
					}
					else
					{
						array[num70] = 0f;
					}
				}
				int num71 = (int)this.reader.ReadInt16();
				int? num72 = new int?(1);
				if (bitsByte26[0])
				{
					num72 = new int?((int)this.reader.ReadByte());
				}
				float num73 = 1f;
				if (bitsByte26[2])
				{
					num73 = this.reader.ReadSingle();
				}
				int num74 = 0;
				if (!bitsByte25[7])
				{
					byte b7 = this.reader.ReadByte();
					if (b7 == 2)
					{
						num74 = (int)this.reader.ReadInt16();
					}
					else if (b7 == 4)
					{
						num74 = this.reader.ReadInt32();
					}
					else
					{
						num74 = (int)this.reader.ReadSByte();
					}
				}
				NPC npc2 = Main.npc[num68];
				bool flag20 = bitsByte26[3] || !npc2.active;
				int num75 = -1;
				if (flag20 || npc2.netID != num71)
				{
					if (flag20)
					{
						npc2.ResetForNewNPC();
					}
					else
					{
						num75 = npc2.type;
					}
					npc2.active = true;
					npc2.SetDefaults(num71, new NPCSpawnParams
					{
						playerCountForMultiplayerDifficultyOverride = num72,
						difficultyOverride = new float?(num73)
					});
				}
				if (!flag20 && Vector2.DistanceSquared(npc2.position, vector6) <= (float)(Main.multiplayerNPCSmoothingRange * Main.multiplayerNPCSmoothingRange))
				{
					npc2.netOffset += npc2.position - vector6;
					if (npc2.netOffset != Vector2.Zero && DebugOptions.ShowNetOffsetDust && Vector2.Distance(vector6, npc2.position) > 4f)
					{
						Dust.QuickDustLine(vector6, npc2.position, 20f, Color.Red);
					}
				}
				npc2.position = vector6;
				npc2.velocity = vector7;
				npc2.target = num69;
				npc2.direction = (bitsByte25[0] ? 1 : (-1));
				npc2.directionY = (bitsByte25[1] ? 1 : (-1));
				npc2.spriteDirection = (bitsByte25[6] ? 1 : (-1));
				if (bitsByte25[7])
				{
					num74 = (npc2.life = npc2.lifeMax);
				}
				else
				{
					npc2.life = num74;
				}
				if (num74 <= 0)
				{
					npc2.active = false;
				}
				npc2.SpawnedFromStatue = bitsByte26[1];
				if (npc2.SpawnedFromStatue)
				{
					npc2.value = 0f;
				}
				if (bitsByte26[4])
				{
					npc2.shimmerTransparency = 1f;
				}
				for (int num76 = 0; num76 < NPC.maxAI; num76++)
				{
					npc2.ai[num76] = array[num76];
				}
				if (num75 > -1)
				{
					npc2.TransformVisuals(num75, npc2.type);
				}
				if (num71 == 262)
				{
					NPC.plantBoss = num68;
				}
				if (num71 == 245)
				{
					NPC.golemBoss = num68;
				}
				if (num71 == 668)
				{
					NPC.deerclopsBoss = num68;
				}
				if (npc2.type >= 0 && npc2.type < (int)NPCID.Count && Main.npcCatchable[npc2.type])
				{
					npc2.releaseOwner = (short)this.reader.ReadByte();
					return;
				}
				return;
			}
			case 24:
			{
				int num77 = (int)this.reader.ReadInt16();
				int num78 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num78 = this.whoAmI;
				}
				Player player8 = Main.player[num78];
				Main.npc[num77].StrikeNPC(player8.inventory[player8.selectedItem].damage, player8.inventory[player8.selectedItem].knockBack, player8.direction, false, false, false, -1);
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(24, -1, this.whoAmI, null, num77, (float)num78, 0f, 0f, 0, 0, 0);
					NetMessage.TrySendData(23, -1, -1, null, num77, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 27:
			{
				int num79 = (int)this.reader.ReadInt16();
				Vector2 vector8 = this.reader.ReadVector2();
				Vector2 vector9 = this.reader.ReadVector2();
				int num80 = (int)this.reader.ReadByte();
				int num81 = (int)this.reader.ReadInt16();
				BitsByte bitsByte27 = this.reader.ReadByte();
				BitsByte bitsByte28 = (bitsByte27[2] ? this.reader.ReadByte() : 0);
				float[] array2 = this.ReUseTemporaryProjectileAI();
				array2[0] = (bitsByte27[0] ? this.reader.ReadSingle() : 0f);
				array2[1] = (bitsByte27[1] ? this.reader.ReadSingle() : 0f);
				int num82 = (int)(bitsByte27[3] ? this.reader.ReadUInt16() : 0);
				int num83 = (int)(bitsByte27[4] ? this.reader.ReadInt16() : 0);
				float num84 = (bitsByte27[5] ? this.reader.ReadSingle() : 0f);
				int num85 = (int)(bitsByte27[6] ? this.reader.ReadInt16() : 0);
				int num86 = (int)(bitsByte27[7] ? this.reader.ReadInt16() : -1);
				if (num86 >= 1000)
				{
					num86 = -1;
				}
				array2[2] = (bitsByte28[0] ? this.reader.ReadSingle() : 0f);
				if (Main.netMode == 2)
				{
					if (num81 == 949)
					{
						num80 = 255;
					}
					else
					{
						num80 = this.whoAmI;
						if (Main.projHostile[num81])
						{
							return;
						}
					}
				}
				int num87 = 1000;
				for (int num88 = 0; num88 < 1000; num88++)
				{
					if (Main.projectile[num88].owner == num80 && Main.projectile[num88].identity == num79 && Main.projectile[num88].active)
					{
						num87 = num88;
						break;
					}
				}
				if (num87 == 1000)
				{
					for (int num89 = 0; num89 < 1000; num89++)
					{
						if (!Main.projectile[num89].active)
						{
							num87 = num89;
							break;
						}
					}
				}
				if (num87 == 1000)
				{
					num87 = Projectile.FindOldestProjectile();
				}
				Projectile projectile = Main.projectile[num87];
				if (!projectile.active || projectile.type != num81)
				{
					projectile.SetDefaults(num81);
					if (Main.netMode == 2)
					{
						Netplay.Clients[this.whoAmI].SpamProjectile += 1f;
					}
				}
				projectile.identity = num79;
				projectile.position = vector8;
				projectile.velocity = vector9;
				projectile.type = num81;
				projectile.damage = num83;
				projectile.bannerIdToRespondTo = num82;
				projectile.originalDamage = num85;
				projectile.knockBack = num84;
				projectile.owner = num80;
				for (int num90 = 0; num90 < Projectile.maxAI; num90++)
				{
					projectile.ai[num90] = array2[num90];
				}
				if (num86 >= 0)
				{
					projectile.projUUID = num86;
					Main.projectileIdentity[num80, num86] = num87;
				}
				projectile.ProjectileFixDesperation();
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(27, -1, this.whoAmI, null, num87, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 28:
			{
				int num91 = (int)this.reader.ReadInt16();
				int num92 = (int)this.reader.ReadInt16();
				float num93 = this.reader.ReadSingle();
				int num94 = (int)(this.reader.ReadByte() - 1);
				byte b8 = this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					if (num92 < 0)
					{
						num92 = 0;
					}
					Main.npc[num91].PlayerInteraction(this.whoAmI);
				}
				if (num92 >= 0)
				{
					Main.npc[num91].StrikeNPC(num92, num93, num94, b8 == 1, false, true, (Main.netMode == 2) ? this.whoAmI : 255);
				}
				else
				{
					Main.npc[num91].life = 0;
					Main.npc[num91].HitEffect(0, 10.0);
					Main.npc[num91].active = false;
				}
				if (Main.netMode != 2)
				{
					return;
				}
				NetMessage.TrySendData(28, -1, this.whoAmI, null, num91, (float)num92, num93, (float)num94, (int)b8, 0, 0);
				if (Main.npc[num91].life <= 0)
				{
					NetMessage.TrySendData(23, -1, -1, null, num91, 0f, 0f, 0f, 0, 0, 0);
				}
				if (Main.npc[num91].realLife >= 0 && Main.npc[Main.npc[num91].realLife].life <= 0)
				{
					NetMessage.TrySendData(23, -1, -1, null, Main.npc[num91].realLife, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 29:
			{
				int num95 = (int)this.reader.ReadInt16();
				int num96 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num96 = this.whoAmI;
				}
				for (int num97 = 0; num97 < 1000; num97++)
				{
					if (Main.projectile[num97].owner == num96 && Main.projectile[num97].identity == num95 && Main.projectile[num97].active)
					{
						Main.projectile[num97].Kill();
						break;
					}
				}
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(29, -1, this.whoAmI, null, num95, (float)num96, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 30:
			{
				int num98 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num98 = this.whoAmI;
				}
				bool flag21 = this.reader.ReadBoolean();
				Main.player[num98].hostile = flag21;
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(30, -1, this.whoAmI, null, num98, 0f, 0f, 0f, 0, 0, 0);
					LocalizedText localizedText = (flag21 ? Lang.mp[11] : Lang.mp[12]);
					Color color = Main.teamColor[Main.player[num98].team];
					ChatHelper.BroadcastChatMessage(NetworkText.FromKey(localizedText.Key, new object[] { Main.player[num98].name }), color, -1);
					return;
				}
				return;
			}
			case 31:
			{
				if (Main.netMode != 2)
				{
					return;
				}
				int num99 = (int)this.reader.ReadInt16();
				int num100 = (int)this.reader.ReadInt16();
				int num101 = Chest.FindChest(num99, num100);
				if (num101 <= -1 || Chest.UsingChest(num101) != -1)
				{
					return;
				}
				NetMessage.SendChestContentsTo(num101, this.whoAmI);
				NetMessage.TrySendData(33, this.whoAmI, -1, null, num101, 0f, 0f, 0f, 0, 0, 0);
				Main.player[this.whoAmI].chest = num101;
				if (Main.myPlayer == this.whoAmI)
				{
					Main.PipsUseGrid = false;
				}
				NetMessage.TrySendData(80, -1, this.whoAmI, null, this.whoAmI, (float)num101, 0f, 0f, 0, 0, 0);
				if (Main.netMode == 2 && WorldGen.IsChestRigged(num99, num100))
				{
					Wiring.SetCurrentUser(this.whoAmI);
					Wiring.HitSwitch(num99, num100);
					Wiring.SetCurrentUser(-1);
					NetMessage.TrySendData(59, -1, this.whoAmI, null, num99, (float)num100, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 32:
			{
				int num102 = (int)this.reader.ReadInt16();
				int num103 = (int)this.reader.ReadByte();
				int num104 = (int)this.reader.ReadInt16();
				int num105 = (int)this.reader.ReadByte();
				int num106 = (int)this.reader.ReadInt16();
				if (num102 < 0 || num102 >= 8000 || Main.chest[num102] == null)
				{
					return;
				}
				if (Main.chest[num102].item[num103] == null)
				{
					Main.chest[num102].item[num103] = new Item();
				}
				Main.chest[num102].item[num103].SetDefaults(num106, null);
				Main.chest[num102].item[num103].Prefix(num105);
				Main.chest[num102].item[num103].stack = num104;
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(32, -1, this.whoAmI, null, num102, (float)num103, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 33:
			{
				int num107 = (int)this.reader.ReadInt16();
				int num108 = (int)this.reader.ReadInt16();
				int num109 = (int)this.reader.ReadInt16();
				int num110 = (int)this.reader.ReadByte();
				string text = string.Empty;
				if (num110 != 0)
				{
					if (num110 <= 20)
					{
						text = this.reader.ReadString();
					}
					else if (num110 != 255)
					{
						num110 = 0;
					}
				}
				if (Main.netMode != 1)
				{
					if (num110 != 0)
					{
						int chest = Main.player[this.whoAmI].chest;
						Chest chest2 = Main.chest[chest];
						chest2.name = text;
						NetMessage.TrySendData(69, -1, this.whoAmI, null, chest, (float)chest2.x, (float)chest2.y, 0f, 0, 0, 0);
					}
					Main.player[this.whoAmI].chest = num107;
					NetMessage.TrySendData(80, -1, this.whoAmI, null, this.whoAmI, (float)num107, 0f, 0f, 0, 0, 0);
					return;
				}
				Player player9 = Main.player[Main.myPlayer];
				if (player9.chest == -1)
				{
					Main.playerInventory = true;
					SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
					if (num107 != -1)
					{
						ItemSlot.SetGlowForChest(Main.chest[num107]);
					}
				}
				else if (player9.chest != num107 && num107 != -1)
				{
					Main.playerInventory = true;
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
					Main.PipsUseGrid = false;
					ItemSlot.SetGlowForChest(Main.chest[num107]);
				}
				else if (player9.chest != -1 && num107 == -1)
				{
					SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
					Main.PipsUseGrid = false;
				}
				player9.chest = num107;
				player9.chestX = num108;
				player9.chestY = num109;
				if (Main.tile[num108, num109].frameX >= 36 && Main.tile[num108, num109].frameX < 72)
				{
					AchievementsHelper.HandleSpecialEvent(Main.player[Main.myPlayer], 16);
					return;
				}
				return;
			}
			case 34:
			{
				byte b9 = this.reader.ReadByte();
				int num111 = (int)this.reader.ReadInt16();
				int num112 = (int)this.reader.ReadInt16();
				int num113 = (int)this.reader.ReadInt16();
				int num114 = (int)this.reader.ReadInt16();
				if (Main.netMode == 2)
				{
					num114 = 0;
				}
				if (Main.netMode == 2)
				{
					if (b9 == 0)
					{
						int num115 = WorldGen.PlaceChest(num111, num112, 21, false, num113);
						if (num115 != -1)
						{
							NetMessage.TrySendData(34, -1, -1, null, (int)b9, (float)num111, (float)num112, (float)num113, num115, 0, 0);
							return;
						}
						NetMessage.TrySendData(34, this.whoAmI, -1, null, (int)b9, (float)num111, (float)num112, (float)num113, num115, 0, 0);
						int itemDrop_Chests = WorldGen.GetItemDrop_Chests(num113, false);
						if (itemDrop_Chests > 0)
						{
							Item.NewItem(new EntitySource_TileBreak(num111, num112), num111 * 16, num112 * 16, 32, 32, itemDrop_Chests, 1, true, 0, false);
							return;
						}
						return;
					}
					else if (b9 == 1 && Main.tile[num111, num112].type == 21)
					{
						Tile tile2 = Main.tile[num111, num112];
						if (tile2.frameX % 36 != 0)
						{
							num111--;
						}
						if (tile2.frameY % 36 != 0)
						{
							num112--;
						}
						int num116 = Chest.FindChest(num111, num112);
						WorldGen.KillTile(num111, num112, false, false, false);
						if (!tile2.active())
						{
							NetMessage.TrySendData(34, -1, -1, null, (int)b9, (float)num111, (float)num112, 0f, num116, 0, 0);
							return;
						}
						return;
					}
					else if (b9 == 2)
					{
						int num117 = WorldGen.PlaceChest(num111, num112, 88, false, num113);
						if (num117 == -1)
						{
							NetMessage.TrySendData(34, this.whoAmI, -1, null, (int)b9, (float)num111, (float)num112, (float)num113, num117, 0, 0);
							Item.NewItem(new EntitySource_TileBreak(num111, num112), num111 * 16, num112 * 16, 32, 32, WorldGen.GetItemDrop_Dressers(num113), 1, true, 0, false);
							return;
						}
						NetMessage.TrySendData(34, -1, -1, null, (int)b9, (float)num111, (float)num112, (float)num113, num117, 0, 0);
						return;
					}
					else if (b9 == 3 && Main.tile[num111, num112].type == 88)
					{
						Tile tile3 = Main.tile[num111, num112];
						num111 -= (int)(tile3.frameX % 54 / 18);
						if (tile3.frameY % 36 != 0)
						{
							num112--;
						}
						int num118 = Chest.FindChest(num111, num112);
						WorldGen.KillTile(num111, num112, false, false, false);
						if (!tile3.active())
						{
							NetMessage.TrySendData(34, -1, -1, null, (int)b9, (float)num111, (float)num112, 0f, num118, 0, 0);
							return;
						}
						return;
					}
					else if (b9 == 4)
					{
						int num119 = WorldGen.PlaceChest(num111, num112, 467, false, num113);
						if (num119 != -1)
						{
							NetMessage.TrySendData(34, -1, -1, null, (int)b9, (float)num111, (float)num112, (float)num113, num119, 0, 0);
							return;
						}
						NetMessage.TrySendData(34, this.whoAmI, -1, null, (int)b9, (float)num111, (float)num112, (float)num113, num119, 0, 0);
						int itemDrop_Chests2 = WorldGen.GetItemDrop_Chests(num113, true);
						if (itemDrop_Chests2 > 0)
						{
							Item.NewItem(new EntitySource_TileBreak(num111, num112), num111 * 16, num112 * 16, 32, 32, itemDrop_Chests2, 1, true, 0, false);
							return;
						}
						return;
					}
					else
					{
						if (b9 != 5 || Main.tile[num111, num112].type != 467)
						{
							return;
						}
						Tile tile4 = Main.tile[num111, num112];
						if (tile4.frameX % 36 != 0)
						{
							num111--;
						}
						if (tile4.frameY % 36 != 0)
						{
							num112--;
						}
						int num120 = Chest.FindChest(num111, num112);
						WorldGen.KillTile(num111, num112, false, false, false);
						if (!tile4.active())
						{
							NetMessage.TrySendData(34, -1, -1, null, (int)b9, (float)num111, (float)num112, 0f, num120, 0, 0);
							return;
						}
						return;
					}
				}
				else if (b9 == 0)
				{
					if (num114 == -1)
					{
						WorldGen.KillTile(num111, num112, false, false, false);
						return;
					}
					SoundEngine.PlaySound(0, num111 * 16, num112 * 16, 1, 1f, 0f);
					WorldGen.PlaceChestDirect(num111, num112, 21, num113, num114);
					return;
				}
				else if (b9 == 2)
				{
					if (num114 == -1)
					{
						WorldGen.KillTile(num111, num112, false, false, false);
						return;
					}
					SoundEngine.PlaySound(0, num111 * 16, num112 * 16, 1, 1f, 0f);
					WorldGen.PlaceDresserDirect(num111, num112, 88, num113, num114);
					return;
				}
				else
				{
					if (b9 != 4)
					{
						Chest.DestroyChestDirect(num111, num112, num114);
						WorldGen.KillTile(num111, num112, false, false, false);
						return;
					}
					if (num114 == -1)
					{
						WorldGen.KillTile(num111, num112, false, false, false);
						return;
					}
					SoundEngine.PlaySound(0, num111 * 16, num112 * 16, 1, 1f, 0f);
					WorldGen.PlaceChestDirect(num111, num112, 467, num113, num114);
					return;
				}
				break;
			}
			case 35:
			{
				int num121 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num121 = this.whoAmI;
				}
				int num122 = (int)this.reader.ReadInt16();
				if (num121 != Main.myPlayer || Main.ServerSideCharacter)
				{
					Main.player[num121].HealEffect(num122, true);
				}
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(35, -1, this.whoAmI, null, num121, (float)num122, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 36:
			{
				int num123 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num123 = this.whoAmI;
				}
				Player player10 = Main.player[num123];
				bool flag22 = player10.zone5[0];
				player10.zone1 = this.reader.ReadByte();
				player10.zone2 = this.reader.ReadByte();
				player10.zone3 = this.reader.ReadByte();
				player10.zone4 = this.reader.ReadByte();
				player10.zone5 = this.reader.ReadByte();
				player10.townNPCs = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					if (!flag22 && player10.zone5[0])
					{
						NPC.Spawner.SpawnFaelings(player10);
					}
					NetMessage.TrySendData(36, -1, this.whoAmI, null, num123, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 37:
				if (Main.netMode != 1)
				{
					return;
				}
				if (Main.autoPass)
				{
					NetMessage.TrySendData(38, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					Main.autoPass = false;
					return;
				}
				Netplay.ServerPassword = "";
				Main.menuMode = 31;
				return;
			case 38:
				if (Main.netMode != 2)
				{
					return;
				}
				if (this.reader.ReadString() == Netplay.ServerPassword)
				{
					Netplay.Clients[this.whoAmI].State = 1;
					NetMessage.TrySendData(3, this.whoAmI, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				NetMessage.TrySendData(2, this.whoAmI, -1, Lang.mp[1].ToNetworkText(), 0, 0f, 0f, 0f, 0, 0, 0);
				return;
			case 39:
			{
				int num124 = (int)this.reader.ReadInt16();
				WorldItem worldItem3 = Main.item[num124];
				if (Main.netMode == 1)
				{
					if (worldItem3.playerIndexTheItemIsReservedFor != Main.myPlayer)
					{
						return;
					}
					worldItem3.playerIndexTheItemIsReservedFor = 255;
					NetMessage.TrySendData(39, -1, -1, null, num124, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				else
				{
					if (worldItem3.playerIndexTheItemIsReservedFor != this.whoAmI)
					{
						return;
					}
					worldItem3.playerIndexTheItemIsReservedFor = 255;
					worldItem3.FindOwner();
					if (worldItem3.playerIndexTheItemIsReservedFor == 255)
					{
						NetMessage.TrySendData(22, -1, this.whoAmI, null, num124, 0f, 0f, 0f, 0, 0, 0);
						return;
					}
					return;
				}
				break;
			}
			case 40:
			{
				int num125 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num125 = this.whoAmI;
				}
				int num126 = (int)this.reader.ReadInt16();
				Main.player[num125].SetTalkNPC(num126);
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(40, -1, this.whoAmI, null, num125, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 41:
			{
				int num127 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num127 = this.whoAmI;
				}
				Player player11 = Main.player[num127];
				float num128 = this.reader.ReadSingle();
				int num129 = (int)this.reader.ReadInt16();
				player11.itemRotation = num128;
				player11.itemAnimation = num129;
				player11.channel = player11.inventory[player11.selectedItem].channel;
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(41, -1, this.whoAmI, null, num127, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 42:
			{
				int num130 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num130 = this.whoAmI;
				}
				else if (Main.myPlayer == num130 && !Main.ServerSideCharacter)
				{
					return;
				}
				int num131 = (int)this.reader.ReadInt16();
				int num132 = (int)this.reader.ReadInt16();
				Main.player[num130].statMana = num131;
				Main.player[num130].statManaMax = num132;
				return;
			}
			case 43:
			{
				int num133 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num133 = this.whoAmI;
				}
				int num134 = (int)this.reader.ReadInt16();
				if (num133 != Main.myPlayer)
				{
					Main.player[num133].ManaEffect(num134);
				}
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(43, -1, this.whoAmI, null, num133, (float)num134, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 45:
			case 157:
			{
				int num135 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num135 = this.whoAmI;
				}
				int num136 = (int)this.reader.ReadByte();
				Player player12 = Main.player[num135];
				int team = player12.team;
				player12.team = num136;
				Color color2 = Main.teamColor[num136];
				if (Main.netMode != 2)
				{
					return;
				}
				NetMessage.TrySendData(45, -1, this.whoAmI, null, num135, 0f, 0f, 0f, 0, 0, 0);
				LocalizedText localizedText2 = Lang.mp[13 + num136];
				if (num136 == 5)
				{
					localizedText2 = Lang.mp[22];
				}
				for (int num137 = 0; num137 < 255; num137++)
				{
					if (num137 == this.whoAmI || (team > 0 && Main.player[num137].team == team) || (num136 > 0 && Main.player[num137].team == num136))
					{
						ChatHelper.SendChatMessageToClient(NetworkText.FromKey(localizedText2.Key, new object[] { player12.name }), color2, num137);
					}
				}
				if (b != 157 || !Main.teamBasedSpawnsSeed)
				{
					return;
				}
				Point zero2 = Point.Zero;
				if (ExtraSpawnPointManager.TryGetExtraSpawnPointForTeam(num136, out zero2))
				{
					RemoteClient.CheckSection(this.whoAmI, zero2.ToWorldCoordinates(8f, 8f), 1);
					NetMessage.SendData(158, num135, -1, null, num135, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 46:
			{
				if (Main.netMode != 2)
				{
					return;
				}
				int num138 = (int)this.reader.ReadInt16();
				int num139 = (int)this.reader.ReadInt16();
				int num140 = Sign.ReadSign(num138, num139, true);
				if (num140 >= 0)
				{
					NetMessage.TrySendData(47, this.whoAmI, -1, null, num140, (float)this.whoAmI, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 47:
			{
				int num141 = (int)this.reader.ReadInt16();
				int num142 = (int)this.reader.ReadInt16();
				int num143 = (int)this.reader.ReadInt16();
				string text2 = this.reader.ReadString();
				int num144 = (int)this.reader.ReadByte();
				BitsByte bitsByte29 = this.reader.ReadByte();
				if (num141 < 0 || num141 >= 32000)
				{
					return;
				}
				string text3 = null;
				if (Main.sign[num141] != null)
				{
					text3 = Main.sign[num141].text;
				}
				Main.sign[num141] = new Sign();
				Main.sign[num141].x = num142;
				Main.sign[num141].y = num143;
				Sign.TextSign(num141, text2);
				if (Main.netMode == 2 && text3 != text2)
				{
					num144 = this.whoAmI;
					NetMessage.TrySendData(47, -1, this.whoAmI, null, num141, (float)num144, 0f, 0f, 0, 0, 0);
				}
				if (Main.netMode == 1 && num144 == Main.myPlayer && Main.sign[num141] != null && !bitsByte29[0])
				{
					Main.LocalPlayer.OpenSign(num141);
					return;
				}
				return;
			}
			case 48:
			{
				int num145 = (int)this.reader.ReadInt16();
				int num146 = (int)this.reader.ReadInt16();
				byte b10 = this.reader.ReadByte();
				byte b11 = this.reader.ReadByte();
				if (Main.netMode == 2 && Netplay.SpamCheck)
				{
					int num147 = this.whoAmI;
					int num148 = (int)(Main.player[num147].position.X + (float)(Main.player[num147].width / 2));
					int num149 = (int)(Main.player[num147].position.Y + (float)(Main.player[num147].height / 2));
					int num150 = 10;
					int num151 = num148 - num150;
					int num152 = num148 + num150;
					int num153 = num149 - num150;
					int num154 = num149 + num150;
					if (num145 < num151 || num145 > num152 || num146 < num153 || num146 > num154)
					{
						Netplay.Clients[this.whoAmI].SpamWater += 1f;
					}
				}
				if (Main.tile[num145, num146] == null)
				{
					Main.tile[num145, num146] = new Tile();
				}
				Tile tile5 = Main.tile[num145, num146];
				lock (tile5)
				{
					Main.tile[num145, num146].liquid = b10;
					Main.tile[num145, num146].liquidType((int)b11);
					if (Main.netMode == 2)
					{
						WorldGen.SquareTileFrame(num145, num146, true);
						if (b10 == 0)
						{
							NetMessage.SendData(48, -1, this.whoAmI, null, num145, (float)num146, 0f, 0f, 0, 0, 0);
						}
					}
					return;
				}
				goto IL_550B;
			}
			case 49:
				goto IL_550B;
			case 50:
			{
				int num155 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num155 = this.whoAmI;
				}
				else if (num155 == Main.myPlayer && !Main.ServerSideCharacter)
				{
					return;
				}
				Player player13 = Main.player[num155];
				int num156 = 0;
				int num157;
				while ((num157 = (int)this.reader.ReadUInt16()) > 0)
				{
					player13.buffType[num156] = num157;
					player13.buffTime[num156] = 60;
					num156++;
				}
				Array.Clear(player13.buffType, num156, player13.buffType.Length - num156);
				Array.Clear(player13.buffTime, num156, player13.buffTime.Length - num156);
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(50, -1, this.whoAmI, null, num155, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 51:
			{
				byte b12 = this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					b12 = (byte)this.whoAmI;
				}
				byte b13 = this.reader.ReadByte();
				if (b13 == 1)
				{
					NPC.SpawnSkeletron((int)b12, false);
					return;
				}
				if (b13 == 2)
				{
					if (Main.netMode == 2)
					{
						NetMessage.TrySendData(51, -1, this.whoAmI, null, (int)b12, (float)b13, 0f, 0f, 0, 0, 0);
						return;
					}
					SoundEngine.PlaySound(SoundID.Item1, (int)Main.player[(int)b12].position.X, (int)Main.player[(int)b12].position.Y, 0f, 1f);
					return;
				}
				else if (b13 == 3)
				{
					if (Main.netMode == 2)
					{
						Main.Sundialing();
						return;
					}
					return;
				}
				else
				{
					if (b13 == 4)
					{
						Main.npc[(int)b12].BigMimicSpawnSmoke();
						return;
					}
					if (b13 == 5)
					{
						if (Main.netMode == 2)
						{
							NPC npc3 = new NPC();
							npc3.SetDefaults(664, default(NPCSpawnParams));
							Main.BestiaryTracker.Kills.RegisterKill(npc3);
							return;
						}
						return;
					}
					else
					{
						if (b13 == 6 && Main.netMode == 2)
						{
							Main.Moondialing();
							return;
						}
						return;
					}
				}
				break;
			}
			case 52:
			{
				int num158 = (int)this.reader.ReadByte();
				int num159 = (int)this.reader.ReadInt16();
				int num160 = (int)this.reader.ReadInt16();
				if (num158 == 1)
				{
					Chest.Unlock(num159, num160);
					if (Main.netMode == 2)
					{
						NetMessage.TrySendData(52, -1, this.whoAmI, null, 0, (float)num158, (float)num159, (float)num160, 0, 0, 0);
						NetMessage.SendTileSquare(-1, num159, num160, 2, TileChangeType.None);
					}
				}
				if (num158 == 2)
				{
					WorldGen.UnlockDoor(num159, num160);
					if (Main.netMode == 2)
					{
						NetMessage.TrySendData(52, -1, this.whoAmI, null, 0, (float)num158, (float)num159, (float)num160, 0, 0, 0);
						NetMessage.SendTileSquare(-1, num159, num160, 2, TileChangeType.None);
					}
				}
				if (num158 != 3)
				{
					return;
				}
				Chest.Lock(num159, num160);
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(52, -1, this.whoAmI, null, 0, (float)num158, (float)num159, (float)num160, 0, 0, 0);
					NetMessage.SendTileSquare(-1, num159, num160, 2, TileChangeType.None);
					return;
				}
				return;
			}
			case 53:
			{
				int num161 = (int)this.reader.ReadInt16();
				int num162 = (int)this.reader.ReadUInt16();
				int num163 = (int)this.reader.ReadInt16();
				Main.npc[num161].AddBuff(num162, num163, true);
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(54, -1, -1, null, num161, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 54:
			{
				if (Main.netMode != 1)
				{
					return;
				}
				int num164 = (int)this.reader.ReadInt16();
				NPC npc4 = Main.npc[num164];
				int num165 = 0;
				int num166;
				while ((num166 = (int)this.reader.ReadUInt16()) > 0)
				{
					npc4.buffType[num165] = num166;
					npc4.buffTime[num165] = (int)this.reader.ReadUInt16();
					num165++;
				}
				Array.Clear(npc4.buffType, num165, npc4.buffType.Length - num165);
				Array.Clear(npc4.buffTime, num165, npc4.buffTime.Length - num165);
				return;
			}
			case 55:
			{
				int num167 = (int)this.reader.ReadByte();
				int num168 = (int)this.reader.ReadUInt16();
				int num169 = this.reader.ReadInt32();
				if (Main.netMode == 2 && !Main.pvpBuff[num168])
				{
					return;
				}
				if (Main.netMode == 1 && num167 != Main.myPlayer)
				{
					return;
				}
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(55, num167, -1, null, num167, (float)num168, (float)num169, 0f, 0, 0, 0);
					return;
				}
				Main.player[num167].AddBuff(num168, num169, true);
				return;
			}
			case 56:
			{
				int num170 = (int)this.reader.ReadInt16();
				if (num170 < 0 || num170 >= Main.maxNPCs)
				{
					return;
				}
				if (Main.netMode == 1)
				{
					string text4 = this.reader.ReadString();
					Main.npc[num170].GivenName = text4;
					int num171 = this.reader.ReadInt32();
					Main.npc[num170].townNpcVariationIndex = num171;
					return;
				}
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(56, this.whoAmI, -1, null, num170, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 57:
				if (Main.netMode != 1)
				{
					return;
				}
				WorldGen.tGood = this.reader.ReadByte();
				WorldGen.tEvil = this.reader.ReadByte();
				WorldGen.tBlood = this.reader.ReadByte();
				return;
			case 58:
			{
				int num172 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num172 = this.whoAmI;
				}
				float num173 = this.reader.ReadSingle();
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(58, -1, this.whoAmI, null, this.whoAmI, num173, 0f, 0f, 0, 0, 0);
					return;
				}
				Player player14 = Main.player[num172];
				int type3 = player14.inventory[player14.selectedItem].type;
				if (type3 == 4372 || type3 == 4057 || type3 == 4715)
				{
					player14.PlayGuitarChord(num173);
					return;
				}
				if (type3 == 4673)
				{
					player14.PlayDrums(num173);
					return;
				}
				Main.musicPitch = num173;
				LegacySoundStyle legacySoundStyle = SoundID.Item26;
				if (type3 == 507)
				{
					legacySoundStyle = SoundID.Item35;
				}
				if (type3 == 1305)
				{
					legacySoundStyle = SoundID.Item47;
				}
				SoundEngine.PlaySound(legacySoundStyle, player14.position, 0f, 1f);
				return;
			}
			case 59:
			{
				int num174 = (int)this.reader.ReadInt16();
				int num175 = (int)this.reader.ReadInt16();
				Wiring.SetCurrentUser(this.whoAmI);
				Wiring.HitSwitch(num174, num175);
				Wiring.SetCurrentUser(-1);
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(59, -1, this.whoAmI, null, num174, (float)num175, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 60:
			{
				int num176 = (int)this.reader.ReadInt16();
				int num177 = (int)this.reader.ReadInt16();
				int num178 = (int)this.reader.ReadInt16();
				byte b14 = this.reader.ReadByte();
				if (num176 >= Main.maxNPCs)
				{
					NetMessage.BootPlayer(this.whoAmI, NetworkText.FromKey("Net.CheatingInvalid", new object[0]));
					return;
				}
				NPC npc5 = Main.npc[num176];
				bool isLikeATownNPC = npc5.isLikeATownNPC;
				if (Main.netMode == 1)
				{
					npc5.homeless = b14 == 1;
					npc5.homeTileX = num177;
					npc5.homeTileY = num178;
				}
				if (!isLikeATownNPC)
				{
					return;
				}
				if (Main.netMode == 1)
				{
					if (b14 == 1)
					{
						WorldGen.TownManager.KickOut(npc5.type);
						return;
					}
					if (b14 == 2)
					{
						WorldGen.TownManager.SetRoom(npc5.type, num177, num178);
						return;
					}
					return;
				}
				else
				{
					if (b14 == 1)
					{
						WorldGen.kickOut(num176);
						return;
					}
					WorldGen.moveRoom(num177, num178, num176);
					return;
				}
				break;
			}
			case 61:
			{
				int num179 = (int)this.reader.ReadInt16();
				int num180 = (int)this.reader.ReadInt16();
				if (Main.netMode != 2)
				{
					return;
				}
				if (num180 >= 0 && num180 < (int)NPCID.Count && NPCID.Sets.MPAllowedEnemies[num180])
				{
					if (!NPC.AnyNPCs(num180))
					{
						NPC.SpawnOnPlayer(num179, num180, 0f, 0f, 0f, 0f);
						return;
					}
					return;
				}
				else if (num180 == -4)
				{
					if (!Main.dayTime && !DD2Event.Ongoing)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromKey(Lang.misc[31].Key, new object[0]), ChatColors.World, -1);
						Main.startPumpkinMoon();
						NetMessage.TrySendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
						NetMessage.TrySendData(78, -1, -1, null, 0, 1f, 2f, 1f, 0, 0, 0);
						return;
					}
					return;
				}
				else if (num180 == -5)
				{
					if (!Main.dayTime && !DD2Event.Ongoing)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromKey(Lang.misc[34].Key, new object[0]), ChatColors.World, -1);
						Main.startSnowMoon();
						NetMessage.TrySendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
						NetMessage.TrySendData(78, -1, -1, null, 0, 1f, 1f, 1f, 0, 0, 0);
						return;
					}
					return;
				}
				else if (num180 == -6)
				{
					if (Main.dayTime && !Main.eclipse)
					{
						if (Main.remixWorld)
						{
							ChatHelper.BroadcastChatMessage(NetworkText.FromKey(Lang.misc[106].Key, new object[0]), ChatColors.World, -1);
						}
						else
						{
							ChatHelper.BroadcastChatMessage(NetworkText.FromKey(Lang.misc[20].Key, new object[0]), ChatColors.World, -1);
						}
						Main.eclipse = true;
						NetMessage.TrySendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
						return;
					}
					return;
				}
				else
				{
					if (num180 == -7)
					{
						Main.invasionDelay = 0;
						Main.StartInvasion(4);
						NetMessage.TrySendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
						NetMessage.TrySendData(78, -1, -1, null, 0, 1f, (float)(Main.invasionType + 3), 0f, 0, 0, 0);
						return;
					}
					if (num180 == -8)
					{
						if (NPC.downedGolemBoss && Main.hardMode && !NPC.AnyDanger(false, false) && !NPC.AnyoneNearCultists())
						{
							WorldGen.StartImpendingDoom(720);
							NetMessage.TrySendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
							return;
						}
						return;
					}
					else if (num180 == -10)
					{
						if (!Main.dayTime && !Main.bloodMoon)
						{
							ChatHelper.BroadcastChatMessage(NetworkText.FromKey(Lang.misc[8].Key, new object[0]), ChatColors.World, -1);
							Main.bloodMoon = true;
							if (Main.GetMoonPhase() == MoonPhase.Empty)
							{
								Main.moonPhase = 5;
							}
							AchievementsHelper.NotifyProgressionEvent(4);
							NetMessage.TrySendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
							return;
						}
						return;
					}
					else
					{
						if (num180 == -11)
						{
							ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Misc.CombatBookUsed", new object[0]), ChatColors.World, -1);
							NPC.combatBookWasUsed = true;
							NetMessage.TrySendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
							return;
						}
						if (num180 == -12)
						{
							NPC.UnlockOrExchangePet(ref NPC.boughtCat, 637, "Misc.LicenseCatUsed", num180);
							return;
						}
						if (num180 == -13)
						{
							NPC.UnlockOrExchangePet(ref NPC.boughtDog, 638, "Misc.LicenseDogUsed", num180);
							return;
						}
						if (num180 == -14)
						{
							NPC.UnlockOrExchangePet(ref NPC.boughtBunny, 656, "Misc.LicenseBunnyUsed", num180);
							return;
						}
						if (num180 == -15)
						{
							NPC.UnlockOrExchangePet(ref NPC.unlockedSlimeBlueSpawn, 670, "Misc.LicenseSlimeUsed", num180);
							return;
						}
						if (num180 == -16)
						{
							NPC.SpawnMechQueen(num179);
							return;
						}
						if (num180 == -17)
						{
							ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Misc.CombatBookVolumeTwoUsed", new object[0]), ChatColors.World, -1);
							NPC.combatBookVolumeTwoWasUsed = true;
							NetMessage.TrySendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
							return;
						}
						if (num180 == -18)
						{
							ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Misc.PeddlersSatchelUsed", new object[0]), ChatColors.World, -1);
							NPC.peddlersSatchelWasUsed = true;
							NetMessage.TrySendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
							return;
						}
						if (num180 == -19)
						{
							Main.StartSlimeRain(true);
							return;
						}
						if (num180 < 0)
						{
							int num181 = 1;
							if (num180 > (int)(-(int)InvasionID.Count))
							{
								num181 = -num180;
							}
							if (num181 > 0 && Main.invasionType == 0)
							{
								Main.invasionDelay = 0;
								Main.StartInvasion(num181);
							}
							NetMessage.TrySendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
							NetMessage.TrySendData(78, -1, -1, null, 0, 1f, (float)(Main.invasionType + 3), 0f, 0, 0, 0);
							return;
						}
						return;
					}
				}
				break;
			}
			case 62:
			{
				int num182 = (int)this.reader.ReadByte();
				int num183 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num182 = this.whoAmI;
				}
				if (num183 == 1)
				{
					Main.player[num182].NinjaDodge();
				}
				if (num183 == 2)
				{
					Main.player[num182].ShadowDodge();
				}
				if (num183 == 4)
				{
					Main.player[num182].BrainOfConfusionDodge();
				}
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(62, -1, this.whoAmI, null, num182, (float)num183, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 63:
			{
				int num184 = (int)this.reader.ReadInt16();
				int num185 = (int)this.reader.ReadInt16();
				byte b15 = this.reader.ReadByte();
				byte b16 = this.reader.ReadByte();
				if (b16 == 0)
				{
					WorldGen.paintTile(num184, num185, b15, false, true);
				}
				else
				{
					WorldGen.paintCoatTile(num184, num185, b15, false, true);
				}
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(63, -1, this.whoAmI, null, num184, (float)num185, (float)b15, (float)b16, 0, 0, 0);
					return;
				}
				return;
			}
			case 64:
			{
				int num186 = (int)this.reader.ReadInt16();
				int num187 = (int)this.reader.ReadInt16();
				byte b17 = this.reader.ReadByte();
				byte b18 = this.reader.ReadByte();
				if (b18 == 0)
				{
					WorldGen.paintWall(num186, num187, b17, false, true);
				}
				else
				{
					WorldGen.paintCoatWall(num186, num187, b17, false, true);
				}
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(64, -1, this.whoAmI, null, num186, (float)num187, (float)b17, (float)b18, 0, 0, 0);
					return;
				}
				return;
			}
			case 65:
			{
				BitsByte bitsByte30 = this.reader.ReadByte();
				int num188 = (int)this.reader.ReadInt16();
				if (Main.netMode == 2)
				{
					num188 = this.whoAmI;
				}
				Vector2 vector10 = this.reader.ReadVector2();
				int num189 = (int)this.reader.ReadByte();
				int num190 = 0;
				if (bitsByte30[0])
				{
					num190++;
				}
				if (bitsByte30[1])
				{
					num190 += 2;
				}
				bool flag23 = false;
				if (bitsByte30[2])
				{
					flag23 = true;
				}
				int num191 = 0;
				if (bitsByte30[3])
				{
					num191 = this.reader.ReadInt32();
				}
				if (flag23)
				{
					vector10 = Main.player[num188].position;
				}
				if (num190 == 0)
				{
					Main.player[num188].Teleport(vector10, num189, num191);
					if (Main.netMode == 2)
					{
						NetMessage.TrySendData(65, -1, this.whoAmI, null, 0, (float)num188, vector10.X, vector10.Y, num189, flag23.ToInt(), num191);
					}
					if (Main.netMode == 1 && num188 == Main.myPlayer)
					{
						NetMessage.TrySendData(65, -1, -1, null, 3, (float)num188, 0f, 0f, 0, 0, 0);
						return;
					}
					return;
				}
				else
				{
					if (num190 == 1)
					{
						Main.npc[num188].Teleport(vector10, num189, num191);
						Main.npc[num188].netOffset *= 0f;
						return;
					}
					if (num190 == 2)
					{
						Main.player[num188].Teleport(vector10, num189, num191);
						if (Main.netMode != 2)
						{
							return;
						}
						RemoteClient.CheckSection(this.whoAmI, vector10, 1);
						NetMessage.TrySendData(65, -1, -1, null, 0, (float)num188, vector10.X, vector10.Y, num189, flag23.ToInt(), num191);
						int num192 = -1;
						float num193 = 9999f;
						for (int num194 = 0; num194 < 255; num194++)
						{
							if (Main.player[num194].active && num194 != this.whoAmI)
							{
								Vector2 vector11 = Main.player[num194].position - Main.player[this.whoAmI].position;
								if (vector11.Length() < num193)
								{
									num193 = vector11.Length();
									num192 = num194;
								}
							}
						}
						if (num192 >= 0)
						{
							ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Game.HasTeleportedTo", new object[]
							{
								Main.player[this.whoAmI].name,
								Main.player[num192].name
							}), new Color(250, 250, 0), -1);
							return;
						}
						return;
					}
					else
					{
						if (num190 == 3)
						{
							Player player15 = Main.player[num188];
							int unacknowledgedTeleports = player15.unacknowledgedTeleports;
							player15.unacknowledgedTeleports = unacknowledgedTeleports - 1;
							return;
						}
						return;
					}
				}
				break;
			}
			case 66:
			{
				int num195 = (int)this.reader.ReadByte();
				int num196 = (int)this.reader.ReadInt16();
				if (num196 <= 0)
				{
					return;
				}
				Player player16 = Main.player[num195];
				player16.statLife += num196;
				if (player16.statLife > player16.statLifeMax2)
				{
					player16.statLife = player16.statLifeMax2;
				}
				player16.HealEffect(num196, false);
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(66, -1, this.whoAmI, null, num195, (float)num196, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 68:
				this.reader.ReadString();
				return;
			case 69:
			{
				int num197 = (int)this.reader.ReadInt16();
				int num198 = (int)this.reader.ReadInt16();
				int num199 = (int)this.reader.ReadInt16();
				if (Main.netMode == 1)
				{
					if (num197 < 0 || num197 >= 8000)
					{
						return;
					}
					Chest chest3 = Main.chest[num197];
					if (chest3 == null)
					{
						chest3 = Chest.CreateWorldChest(num197, num198, num199);
					}
					else if (chest3.x != num198 || chest3.y != num199)
					{
						return;
					}
					chest3.name = this.reader.ReadString();
					return;
				}
				else
				{
					if (num197 < -1 || num197 >= 8000)
					{
						return;
					}
					if (num197 == -1)
					{
						num197 = Chest.FindChest(num198, num199);
						if (num197 == -1)
						{
							return;
						}
					}
					Chest chest4 = Main.chest[num197];
					if (chest4.x != num198 || chest4.y != num199)
					{
						return;
					}
					NetMessage.TrySendData(69, this.whoAmI, -1, null, num197, (float)num198, (float)num199, 0f, 0, 0, 0);
					return;
				}
				break;
			}
			case 70:
			{
				if (Main.netMode != 2)
				{
					return;
				}
				int num200 = (int)this.reader.ReadInt16();
				int num201 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num201 = this.whoAmI;
				}
				if (num200 < Main.maxNPCs && num200 >= 0)
				{
					NPC.CatchNPC(num200, num201);
					return;
				}
				return;
			}
			case 71:
			{
				if (Main.netMode != 2)
				{
					return;
				}
				int num202 = this.reader.ReadInt32();
				int num203 = this.reader.ReadInt32();
				int num204 = (int)this.reader.ReadInt16();
				byte b19 = this.reader.ReadByte();
				NPC.ReleaseNPC(num202, num203, num204, (int)b19, this.whoAmI);
				return;
			}
			case 72:
			{
				if (Main.netMode != 1)
				{
					return;
				}
				for (int num205 = 0; num205 < Main.TravelShopMaxSlots; num205++)
				{
					Main.travelShop[num205] = (int)this.reader.ReadInt16();
				}
				return;
			}
			case 73:
				switch (this.reader.ReadByte())
				{
				case 0:
					Main.player[this.whoAmI].TeleportationPotion();
					return;
				case 1:
					Main.player[this.whoAmI].MagicConch();
					return;
				case 2:
					Main.player[this.whoAmI].DemonConch();
					return;
				case 3:
					Main.player[this.whoAmI].Shellphone_Spawn();
					return;
				case 4:
					Main.player[this.whoAmI].PlayerNoSpaceTeleport();
					return;
				default:
					return;
				}
				break;
			case 74:
				if (Main.netMode != 1)
				{
					return;
				}
				Main.anglerQuest = (int)this.reader.ReadByte();
				Main.anglerQuestFinished = this.reader.ReadBoolean();
				return;
			case 75:
			{
				if (Main.netMode != 2)
				{
					return;
				}
				string name = Main.player[this.whoAmI].name;
				if (!Main.anglerWhoFinishedToday.Contains(name))
				{
					Main.anglerWhoFinishedToday.Add(name);
					return;
				}
				return;
			}
			case 76:
			{
				int num206 = (int)this.reader.ReadByte();
				if (num206 == Main.myPlayer && !Main.ServerSideCharacter)
				{
					return;
				}
				if (Main.netMode == 2)
				{
					num206 = this.whoAmI;
				}
				Player player17 = Main.player[num206];
				player17.anglerQuestsFinished = this.reader.ReadInt32();
				player17.golferScoreAccumulated = this.reader.ReadInt32();
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(76, -1, this.whoAmI, null, num206, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 77:
			{
				int num207 = (int)this.reader.ReadInt16();
				ushort num208 = this.reader.ReadUInt16();
				short num209 = this.reader.ReadInt16();
				short num210 = this.reader.ReadInt16();
				Animation.NewTemporaryAnimation(num207, num208, (int)num209, (int)num210);
				return;
			}
			case 78:
				if (Main.netMode != 1)
				{
					return;
				}
				Main.ReportInvasionProgress(this.reader.ReadInt32(), this.reader.ReadInt32(), (int)this.reader.ReadSByte(), (int)this.reader.ReadSByte());
				return;
			case 79:
			{
				int num211 = (int)this.reader.ReadInt16();
				int num212 = (int)this.reader.ReadInt16();
				short num213 = this.reader.ReadInt16();
				int num214 = (int)this.reader.ReadInt16();
				int num215 = (int)this.reader.ReadByte();
				int num216 = (int)this.reader.ReadSByte();
				int num217;
				if (this.reader.ReadBoolean())
				{
					num217 = 1;
				}
				else
				{
					num217 = -1;
				}
				if (Main.netMode == 2)
				{
					Netplay.Clients[this.whoAmI].SpamAddBlock += 1f;
					if (!WorldGen.InWorld(num211, num212, 10) || !Netplay.Clients[this.whoAmI].TileSections[Netplay.GetSectionX(num211), Netplay.GetSectionY(num212)])
					{
						return;
					}
				}
				WorldGen.PlaceObject(num211, num212, (int)num213, false, num214, num215, num216, num217);
				if (Main.netMode == 2)
				{
					NetMessage.SendObjectPlacement(this.whoAmI, num211, num212, (int)num213, num214, num215, num216, num217);
					return;
				}
				return;
			}
			case 80:
			{
				if (Main.netMode != 1)
				{
					return;
				}
				int num218 = (int)this.reader.ReadByte();
				int num219 = (int)this.reader.ReadInt16();
				if (num219 >= -3 && num219 < 8000)
				{
					Main.player[num218].chest = num219;
					return;
				}
				return;
			}
			case 81:
			{
				if (Main.netMode != 1)
				{
					return;
				}
				int num220 = (int)this.reader.ReadSingle();
				int num221 = (int)this.reader.ReadSingle();
				Color color3 = this.reader.ReadRGB();
				int num222 = this.reader.ReadInt32();
				CombatText.NewText(new Rectangle(num220, num221, 0, 0), color3, num222, false, false);
				return;
			}
			case 82:
				NetManager.Instance.Read(this.reader, this.whoAmI, length);
				return;
			case 84:
			{
				int num223 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num223 = this.whoAmI;
				}
				float num224 = this.reader.ReadSingle();
				Main.player[num223].stealth = num224;
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(84, -1, this.whoAmI, null, num223, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 85:
				if (Main.netMode == 2 && this.whoAmI < 255)
				{
					Player player18 = Main.player[this.whoAmI];
					QuickStacking.SourceInventory sourceInventory = QuickStacking.ReadNetInventory(player18, this.reader);
					bool flag24 = this.reader.ReadBoolean();
					QuickStacking.QuickStackToNearbyChests(player18, sourceInventory, flag24);
					return;
				}
				if (Main.netMode == 1)
				{
					QuickStacking.IndicateBlockedChests(Main.LocalPlayer, QuickStacking.ReadBlockedChestList(this.reader));
					return;
				}
				return;
			case 86:
			{
				if (Main.netMode != 1)
				{
					return;
				}
				int num225 = this.reader.ReadInt32();
				if (this.reader.ReadBoolean())
				{
					TileEntity tileEntity = TileEntity.Read(this.reader, 319, true);
					tileEntity.ID = num225;
					TileEntity.Add(tileEntity);
					return;
				}
				TileEntity tileEntity2;
				if (TileEntity.TryGet<TileEntity>(num225, out tileEntity2))
				{
					TileEntity.Remove(tileEntity2, false);
					return;
				}
				return;
			}
			case 87:
			{
				if (Main.netMode != 2)
				{
					return;
				}
				int num226 = (int)this.reader.ReadInt16();
				int num227 = (int)this.reader.ReadInt16();
				int num228 = (int)this.reader.ReadByte();
				if (!WorldGen.InWorld(num226, num227, 0))
				{
					return;
				}
				TileEntity tileEntity3;
				if (TileEntity.TryGetAt<TileEntity>(num226, num227, out tileEntity3))
				{
					return;
				}
				TileEntity.PlaceEntityNet(num226, num227, num228);
				return;
			}
			case 88:
			{
				if (Main.netMode != 1)
				{
					return;
				}
				int num229 = (int)this.reader.ReadInt16();
				if (num229 < 0 || num229 > 400)
				{
					return;
				}
				Item inner = Main.item[num229].inner;
				BitsByte bitsByte31 = this.reader.ReadByte();
				if (bitsByte31[0])
				{
					inner.color.PackedValue = this.reader.ReadUInt32();
				}
				if (bitsByte31[1])
				{
					inner.damage = (int)this.reader.ReadUInt16();
				}
				if (bitsByte31[2])
				{
					inner.knockBack = this.reader.ReadSingle();
				}
				if (bitsByte31[3])
				{
					inner.useAnimation = (int)this.reader.ReadUInt16();
				}
				if (bitsByte31[4])
				{
					inner.useTime = (int)this.reader.ReadUInt16();
				}
				if (bitsByte31[5])
				{
					inner.shoot = (int)this.reader.ReadInt16();
				}
				if (bitsByte31[6])
				{
					inner.shootSpeed = this.reader.ReadSingle();
				}
				if (!bitsByte31[7])
				{
					return;
				}
				bitsByte31 = this.reader.ReadByte();
				if (bitsByte31[0])
				{
					inner.width = (int)this.reader.ReadInt16();
				}
				if (bitsByte31[1])
				{
					inner.height = (int)this.reader.ReadInt16();
				}
				if (bitsByte31[2])
				{
					inner.scale = this.reader.ReadSingle();
				}
				if (bitsByte31[3])
				{
					inner.ammo = (int)this.reader.ReadInt16();
				}
				if (bitsByte31[4])
				{
					inner.useAmmo = (int)this.reader.ReadInt16();
				}
				if (bitsByte31[5])
				{
					inner.notAmmo = this.reader.ReadBoolean();
					return;
				}
				return;
			}
			case 89:
			{
				if (Main.netMode != 2)
				{
					return;
				}
				int num230 = (int)this.reader.ReadInt16();
				int num231 = (int)this.reader.ReadInt16();
				int num232 = (int)this.reader.ReadInt16();
				int num233 = (int)this.reader.ReadByte();
				int num234 = (int)this.reader.ReadInt16();
				TEItemFrame.TryPlacing(num230, num231, num232, num233, num234);
				return;
			}
			case 91:
			{
				if (Main.netMode != 1)
				{
					return;
				}
				int num235 = this.reader.ReadInt32();
				int num236 = (int)this.reader.ReadByte();
				if (num236 != 255)
				{
					int num237 = (int)this.reader.ReadUInt16();
					int num238 = (int)this.reader.ReadUInt16();
					int num239 = (int)this.reader.ReadByte();
					int num240 = 0;
					if (num239 < 0)
					{
						num240 = (int)this.reader.ReadInt16();
					}
					WorldUIAnchor worldUIAnchor = EmoteBubble.DeserializeNetAnchor(num236, num237);
					if (num236 == 1)
					{
						Main.player[num237].emoteTime = 360;
					}
					Dictionary<int, EmoteBubble> byID = EmoteBubble.byID;
					lock (byID)
					{
						if (!EmoteBubble.byID.ContainsKey(num235))
						{
							EmoteBubble.byID[num235] = new EmoteBubble(num239, worldUIAnchor, num238);
						}
						else
						{
							EmoteBubble.byID[num235].lifeTime = num238;
							EmoteBubble.byID[num235].lifeTimeStart = num238;
							EmoteBubble.byID[num235].emote = num239;
							EmoteBubble.byID[num235].anchor = worldUIAnchor;
						}
						EmoteBubble.byID[num235].ID = num235;
						EmoteBubble.byID[num235].metadata = num240;
						EmoteBubble.OnBubbleChange(num235);
						return;
					}
					goto IL_79D1;
				}
				if (EmoteBubble.byID.ContainsKey(num235))
				{
					EmoteBubble.byID.Remove(num235);
					return;
				}
				return;
			}
			case 92:
				goto IL_79D1;
			case 94:
			{
				string text5 = this.reader.ReadString();
				this.reader.ReadInt32();
				int num241 = (int)this.reader.ReadSingle();
				this.reader.ReadSingle();
				if (!DebugOptions.enableDebugCommands)
				{
					return;
				}
				if (text5 == "/showdebug")
				{
					DebugOptions.Shared_ReportCommandUsage = num241 == 1;
					return;
				}
				if (text5 == "/setserverping")
				{
					DebugOptions.Shared_ServerPing = num241;
					DebugNetworkStream.Latency = (uint)(num241 / 2);
					return;
				}
				return;
			}
			case 95:
			{
				ushort num242 = this.reader.ReadUInt16();
				int num243 = (int)this.reader.ReadByte();
				if (Main.netMode != 2)
				{
					return;
				}
				for (int num244 = 0; num244 < 1000; num244++)
				{
					if (Main.projectile[num244].owner == (int)num242 && Main.projectile[num244].active && Main.projectile[num244].type == 602 && Main.projectile[num244].ai[1] == (float)num243)
					{
						Main.projectile[num244].Kill();
						NetMessage.TrySendData(29, -1, -1, null, Main.projectile[num244].identity, (float)num242, 0f, 0f, 0, 0, 0);
						return;
					}
				}
				return;
			}
			case 96:
			{
				int num245 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num245 = this.whoAmI;
				}
				Player player19 = Main.player[num245];
				int num246 = (int)this.reader.ReadInt16();
				Vector2 vector12 = this.reader.ReadVector2();
				Vector2 vector13 = this.reader.ReadVector2();
				int num247 = num246 + ((num246 % 2 == 0) ? 1 : (-1));
				player19.lastPortalColorIndex = num247;
				player19.Teleport(vector12, 4, num246);
				player19.velocity = vector13;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(96, -1, num245, null, num245, vector12.X, vector12.Y, (float)num246, 0, 0, 0);
					return;
				}
				return;
			}
			case 97:
				if (Main.netMode != 1)
				{
					return;
				}
				AchievementsHelper.NotifyNPCKilledDirect(Main.player[Main.myPlayer], (int)this.reader.ReadInt16());
				return;
			case 98:
				if (Main.netMode != 1)
				{
					return;
				}
				AchievementsHelper.NotifyProgressionEvent((int)this.reader.ReadInt16());
				return;
			case 99:
			{
				int num248 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num248 = this.whoAmI;
				}
				Main.player[num248].MinionRestTargetPoint = this.reader.ReadVector2();
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(99, -1, this.whoAmI, null, num248, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 100:
			{
				int num249 = (int)this.reader.ReadUInt16();
				NPC npc6 = Main.npc[num249];
				int num250 = (int)this.reader.ReadInt16();
				Vector2 vector14 = this.reader.ReadVector2();
				Vector2 vector15 = this.reader.ReadVector2();
				int num251 = num250 + ((num250 % 2 == 0) ? 1 : (-1));
				npc6.lastPortalColorIndex = num251;
				npc6.Teleport(vector14, 4, num250);
				npc6.velocity = vector15;
				npc6.netOffset *= 0f;
				return;
			}
			case 101:
				if (Main.netMode == 2)
				{
					return;
				}
				NPC.ShieldStrengthTowerSolar = (int)this.reader.ReadUInt16();
				NPC.ShieldStrengthTowerVortex = (int)this.reader.ReadUInt16();
				NPC.ShieldStrengthTowerNebula = (int)this.reader.ReadUInt16();
				NPC.ShieldStrengthTowerStardust = (int)this.reader.ReadUInt16();
				if (NPC.ShieldStrengthTowerSolar < 0)
				{
					NPC.ShieldStrengthTowerSolar = 0;
				}
				if (NPC.ShieldStrengthTowerVortex < 0)
				{
					NPC.ShieldStrengthTowerVortex = 0;
				}
				if (NPC.ShieldStrengthTowerNebula < 0)
				{
					NPC.ShieldStrengthTowerNebula = 0;
				}
				if (NPC.ShieldStrengthTowerStardust < 0)
				{
					NPC.ShieldStrengthTowerStardust = 0;
				}
				if (NPC.ShieldStrengthTowerSolar > NPC.LunarShieldPowerMax)
				{
					NPC.ShieldStrengthTowerSolar = NPC.LunarShieldPowerMax;
				}
				if (NPC.ShieldStrengthTowerVortex > NPC.LunarShieldPowerMax)
				{
					NPC.ShieldStrengthTowerVortex = NPC.LunarShieldPowerMax;
				}
				if (NPC.ShieldStrengthTowerNebula > NPC.LunarShieldPowerMax)
				{
					NPC.ShieldStrengthTowerNebula = NPC.LunarShieldPowerMax;
				}
				if (NPC.ShieldStrengthTowerStardust > NPC.LunarShieldPowerMax)
				{
					NPC.ShieldStrengthTowerStardust = NPC.LunarShieldPowerMax;
					return;
				}
				return;
			case 102:
			{
				int num252 = (int)this.reader.ReadByte();
				ushort num253 = this.reader.ReadUInt16();
				Vector2 vector16 = this.reader.ReadVector2();
				if (Main.netMode == 2)
				{
					num252 = this.whoAmI;
					NetMessage.TrySendData(102, -1, -1, null, num252, (float)num253, vector16.X, vector16.Y, 0, 0, 0);
					return;
				}
				Player player20 = Main.player[num252];
				for (int num254 = 0; num254 < 255; num254++)
				{
					Player player21 = Main.player[num254];
					if (player21.active && !player21.dead && (player20.team == 0 || player20.team == player21.team) && player21.Distance(vector16) < 700f)
					{
						Vector2 vector17 = player20.Center - player21.Center;
						Vector2 vector18 = Vector2.Normalize(vector17);
						if (!vector18.HasNaNs())
						{
							int num255 = 90;
							float num256 = 0f;
							float num257 = 0.20943952f;
							Vector2 vector19 = new Vector2(0f, -8f);
							Vector2 vector20 = new Vector2(-3f);
							float num258 = 0f;
							float num259 = 0.005f;
							if (num253 != 173)
							{
								if (num253 != 176)
								{
									if (num253 == 179)
									{
										num255 = 86;
									}
								}
								else
								{
									num255 = 88;
								}
							}
							else
							{
								num255 = 90;
							}
							int num260 = 0;
							while ((float)num260 < vector17.Length() / 6f)
							{
								Vector2 vector21 = player21.Center + 6f * (float)num260 * vector18 + vector19.RotatedBy((double)num256, default(Vector2)) + vector20;
								num256 += num257;
								int num261 = Dust.NewDust(vector21, 6, 6, num255, 0f, 0f, 100, default(Color), 1.5f);
								Main.dust[num261].noGravity = true;
								Main.dust[num261].velocity = Vector2.Zero;
								num258 = (Main.dust[num261].fadeIn = num258 + num259);
								Main.dust[num261].velocity += vector18 * 1.5f;
								num260++;
							}
						}
						player21.NebulaLevelup((int)num253);
					}
				}
				return;
			}
			case 103:
				if (Main.netMode == 1)
				{
					NPC.MaxMoonLordCountdown = this.reader.ReadInt32();
					NPC.MoonLordCountdown = this.reader.ReadInt32();
					return;
				}
				return;
			case 104:
			{
				if (Main.netMode != 1 || Main.npcShop <= 0)
				{
					return;
				}
				Item[] item3 = Main.instance.shop[Main.npcShop].item;
				int num262 = (int)this.reader.ReadByte();
				int num263 = (int)this.reader.ReadInt16();
				int num264 = (int)this.reader.ReadInt16();
				int num265 = (int)this.reader.ReadByte();
				int num266 = this.reader.ReadInt32();
				BitsByte bitsByte32 = this.reader.ReadByte();
				if (num262 < item3.Length)
				{
					item3[num262] = new Item();
					item3[num262].SetDefaults(num263, null);
					item3[num262].stack = num264;
					item3[num262].Prefix(num265);
					item3[num262].value = num266;
					item3[num262].buyOnce = bitsByte32[0];
					return;
				}
				return;
			}
			case 105:
			{
				if (Main.netMode == 1)
				{
					return;
				}
				int num267 = (int)this.reader.ReadInt16();
				int num268 = (int)this.reader.ReadInt16();
				bool flag25 = this.reader.ReadBoolean();
				WorldGen.ToggleGemLock(num267, num268, flag25);
				return;
			}
			case 106:
			{
				if (Main.netMode != 1)
				{
					return;
				}
				HalfVector2 halfVector = default(HalfVector2);
				halfVector.PackedValue = this.reader.ReadUInt32();
				Utils.PoofOfSmoke(halfVector.ToVector2());
				return;
			}
			case 107:
			{
				if (Main.netMode != 1)
				{
					return;
				}
				Color color4 = this.reader.ReadRGB();
				string text6 = NetworkText.Deserialize(this.reader).ToString();
				int num269 = (int)this.reader.ReadInt16();
				Main.NewTextMultiline(text6, false, color4, num269);
				return;
			}
			case 108:
			{
				if (Main.netMode != 1)
				{
					return;
				}
				int num270 = (int)this.reader.ReadInt16();
				float num271 = this.reader.ReadSingle();
				int num272 = (int)this.reader.ReadInt16();
				int num273 = (int)this.reader.ReadInt16();
				int num274 = (int)this.reader.ReadInt16();
				int num275 = (int)this.reader.ReadInt16();
				int num276 = (int)this.reader.ReadByte();
				if (num276 != Main.myPlayer)
				{
					return;
				}
				WorldGen.ShootFromCannon(num272, num273, num274, num275, num270, num271, num276, true);
				return;
			}
			case 109:
			{
				if (Main.netMode != 2)
				{
					return;
				}
				int num277 = (int)this.reader.ReadInt16();
				int num278 = (int)this.reader.ReadInt16();
				int num279 = (int)this.reader.ReadInt16();
				int num280 = (int)this.reader.ReadInt16();
				WiresUI.Settings.MultiToolMode multiToolMode = (WiresUI.Settings.MultiToolMode)this.reader.ReadByte();
				int num281 = this.whoAmI;
				WiresUI.Settings.MultiToolMode toolMode = WiresUI.Settings.ToolMode;
				WiresUI.Settings.ToolMode = multiToolMode;
				Wiring.MassWireOperation(new Point(num277, num278), new Point(num279, num280), Main.player[num281]);
				WiresUI.Settings.ToolMode = toolMode;
				return;
			}
			case 110:
			{
				if (Main.netMode != 1)
				{
					return;
				}
				int num282 = (int)this.reader.ReadInt16();
				int num283 = (int)this.reader.ReadInt16();
				int num284 = (int)this.reader.ReadByte();
				if (num284 != Main.myPlayer)
				{
					return;
				}
				Player player22 = Main.player[num284];
				for (int num285 = 0; num285 < num283; num285++)
				{
					player22.ConsumeItem(num282, false, false);
				}
				player22.wireOperationsCooldown = 0;
				return;
			}
			case 111:
				if (Main.netMode != 2)
				{
					return;
				}
				BirthdayParty.ToggleManualParty();
				return;
			case 112:
			{
				int num286 = (int)this.reader.ReadByte();
				int num287 = this.reader.ReadInt32();
				int num288 = this.reader.ReadInt32();
				int num289 = (int)this.reader.ReadByte();
				int num290 = (int)this.reader.ReadInt16();
				bool flag26 = this.reader.ReadByte() == 1;
				if (num286 == 1)
				{
					if (Main.netMode == 1)
					{
						WorldGen.TreeGrowFX(num287, num288, num289, num290, flag26);
					}
					if (Main.netMode == 2)
					{
						NetMessage.TrySendData((int)b, -1, -1, null, num286, (float)num287, (float)num288, (float)num289, num290, flag26 ? 1 : 0, 0);
						return;
					}
					return;
				}
				else
				{
					if (num286 == 2)
					{
						NPC.FairyEffects(new Vector2((float)num287, (float)num288), num290);
						return;
					}
					return;
				}
				break;
			}
			case 113:
			{
				int num291 = (int)this.reader.ReadInt16();
				int num292 = (int)this.reader.ReadInt16();
				if (Main.netMode == 2 && !Main.snowMoon && !Main.pumpkinMoon)
				{
					if (DD2Event.WouldFailSpawningHere(num291, num292))
					{
						DD2Event.FailureMessage(this.whoAmI);
					}
					DD2Event.SummonCrystal(num291, num292, this.whoAmI);
					return;
				}
				return;
			}
			case 114:
				if (Main.netMode != 1)
				{
					return;
				}
				DD2Event.WipeEntities();
				return;
			case 115:
			{
				int num293 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num293 = this.whoAmI;
				}
				Main.player[num293].MinionAttackTargetNPC = (int)this.reader.ReadInt16();
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(115, -1, this.whoAmI, null, num293, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 116:
				if (Main.netMode != 1)
				{
					return;
				}
				DD2Event.TimeLeftBetweenWaves = this.reader.ReadInt32();
				return;
			case 117:
			{
				int num294 = (int)this.reader.ReadByte();
				if (Main.netMode == 2 && this.whoAmI != num294 && (!Main.player[num294].hostile || !Main.player[this.whoAmI].hostile))
				{
					return;
				}
				PlayerDeathReason playerDeathReason = PlayerDeathReason.FromReader(this.reader);
				int num295 = (int)this.reader.ReadInt16();
				int num296 = (int)(this.reader.ReadByte() - 1);
				BitsByte bitsByte33 = this.reader.ReadByte();
				bool flag27 = bitsByte33[0];
				bool flag28 = bitsByte33[1];
				int num297 = (int)this.reader.ReadSByte();
				Main.player[num294].Hurt(playerDeathReason, num295, num296, flag28, true, flag27, num297, true);
				if (Main.netMode == 2)
				{
					NetMessage.SendPlayerHurt(num294, playerDeathReason, num295, num296, flag27, flag28, num297, -1, this.whoAmI);
					return;
				}
				return;
			}
			case 118:
			{
				int num298 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num298 = this.whoAmI;
				}
				PlayerDeathReason playerDeathReason2 = PlayerDeathReason.FromReader(this.reader);
				int num299 = (int)this.reader.ReadInt16();
				int num300 = (int)(this.reader.ReadByte() - 1);
				bool flag29 = this.reader.ReadByte()[0];
				Main.player[num298].KillMe(playerDeathReason2, (double)num299, num300, flag29);
				if (Main.netMode == 2)
				{
					NetMessage.SendPlayerDeath(num298, playerDeathReason2, num299, num300, flag29, -1, this.whoAmI);
					return;
				}
				return;
			}
			case 119:
			{
				if (Main.netMode != 1)
				{
					return;
				}
				int num301 = (int)this.reader.ReadSingle();
				int num302 = (int)this.reader.ReadSingle();
				Color color5 = this.reader.ReadRGB();
				NetworkText networkText = NetworkText.Deserialize(this.reader);
				CombatText.NewText(new Rectangle(num301, num302, 0, 0), color5, networkText.ToString(), false, false);
				return;
			}
			case 120:
			{
				int num303 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num303 = this.whoAmI;
				}
				int num304 = (int)this.reader.ReadByte();
				if (num304 >= 0 && num304 < EmoteID.Count && Main.netMode == 2)
				{
					EmoteBubble.NewBubble(num304, new WorldUIAnchor(Main.player[num303]), 360);
					EmoteBubble.CheckForNPCsToReactToEmoteBubble(num304, Main.player[num303]);
					return;
				}
				return;
			}
			case 121:
			{
				int num305 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num305 = this.whoAmI;
				}
				int num306 = this.reader.ReadInt32();
				int num307 = (int)this.reader.ReadByte();
				int num308 = (int)this.reader.ReadByte();
				TEDisplayDoll tedisplayDoll;
				if (!TileEntity.TryGet<TEDisplayDoll>(num306, out tedisplayDoll))
				{
					TEDisplayDoll.ReadDummySync(num307, num308, this.reader);
					return;
				}
				tedisplayDoll.ReadData(num307, num308, this.reader);
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData((int)b, -1, num305, null, num305, (float)num306, (float)num307, (float)num308, 0, 0, 0);
					return;
				}
				return;
			}
			case 122:
			{
				int num309 = this.reader.ReadInt32();
				int num310 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num310 = this.whoAmI;
				}
				if (Main.netMode == 2)
				{
					if (num309 == -1)
					{
						Main.player[num310].tileEntityAnchor.Clear();
						NetMessage.TrySendData((int)b, -1, -1, null, num309, (float)num310, 0f, 0f, 0, 0, 0);
						return;
					}
					int num311;
					TileEntity tileEntity4;
					if (!TileEntity.IsOccupied(num309, out num311) && TileEntity.TryGet<TileEntity>(num309, out tileEntity4))
					{
						Main.player[num310].tileEntityAnchor.Set(num309, (int)tileEntity4.Position.X, (int)tileEntity4.Position.Y);
						NetMessage.TrySendData((int)b, -1, -1, null, num309, (float)num310, 0f, 0f, 0, 0, 0);
					}
				}
				if (Main.netMode != 1)
				{
					return;
				}
				if (num309 == -1)
				{
					Main.player[num310].tileEntityAnchor.Clear();
					return;
				}
				TileEntity tileEntity5;
				if (TileEntity.TryGet<TileEntity>(num309, out tileEntity5))
				{
					TileEntity.SetInteractionAnchor(Main.player[num310], (int)tileEntity5.Position.X, (int)tileEntity5.Position.Y, num309);
					return;
				}
				return;
			}
			case 123:
			{
				if (Main.netMode != 2)
				{
					return;
				}
				int num312 = (int)this.reader.ReadInt16();
				int num313 = (int)this.reader.ReadInt16();
				int num314 = (int)this.reader.ReadInt16();
				int num315 = (int)this.reader.ReadByte();
				int num316 = (int)this.reader.ReadInt16();
				TEWeaponsRack.TryPlacing(num312, num313, num314, num315, num316);
				return;
			}
			case 124:
			{
				int num317 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num317 = this.whoAmI;
				}
				int num318 = this.reader.ReadInt32();
				int num319 = (int)this.reader.ReadByte();
				bool flag30 = false;
				if (num319 >= 2)
				{
					flag30 = true;
					num319 -= 2;
				}
				TEHatRack tehatRack;
				if (!TileEntity.TryGet<TEHatRack>(num318, out tehatRack) || num319 >= 2)
				{
					this.reader.ReadInt32();
					this.reader.ReadByte();
					return;
				}
				tehatRack.ReadItem(num319, this.reader, flag30);
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData((int)b, -1, num317, null, num317, (float)num318, (float)num319, (float)flag30.ToInt(), 0, 0, 0);
					return;
				}
				return;
			}
			case 125:
			{
				int num320 = (int)this.reader.ReadByte();
				int num321 = (int)this.reader.ReadInt16();
				int num322 = (int)this.reader.ReadInt16();
				int num323 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num320 = this.whoAmI;
				}
				if (Main.netMode == 1)
				{
					Main.player[Main.myPlayer].GetOtherPlayersPickTile(num321, num322, num323);
				}
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(125, -1, num320, null, num320, (float)num321, (float)num322, (float)num323, 0, 0, 0);
					return;
				}
				return;
			}
			case 126:
				if (Main.netMode != 1)
				{
					return;
				}
				NPC.RevengeManager.AddMarkerFromReader(this.reader);
				return;
			case 127:
			{
				int num324 = this.reader.ReadInt32();
				if (Main.netMode != 1)
				{
					return;
				}
				NPC.RevengeManager.DestroyMarker(num324);
				return;
			}
			case 128:
			{
				int num325 = (int)this.reader.ReadByte();
				int num326 = (int)this.reader.ReadUInt16();
				int num327 = (int)this.reader.ReadUInt16();
				int num328 = (int)this.reader.ReadUInt16();
				int num329 = (int)this.reader.ReadUInt16();
				if (Main.netMode == 2)
				{
					NetMessage.SendData(128, -1, num325, null, num325, (float)num328, (float)num329, 0f, num326, num327, 0);
					return;
				}
				GolfHelper.ContactListener.PutBallInCup_TextAndEffects(new Point(num326, num327), num325, num328, num329);
				return;
			}
			case 129:
				if (Main.netMode != 1)
				{
					return;
				}
				if (Main.LocalPlayer.team > 0)
				{
					NetMessage.SendData(45, -1, -1, null, Main.myPlayer, 0f, 0f, 0f, 0, 0, 0);
				}
				Main.FixUIScale();
				Main.TrySetPreparationState(Main.WorldPreparationState.ProcessingData);
				return;
			case 130:
			{
				if (Main.netMode != 2)
				{
					return;
				}
				int num330 = (int)this.reader.ReadUInt16();
				int num331 = (int)this.reader.ReadUInt16();
				int num332 = (int)this.reader.ReadInt16();
				if (num332 == 682)
				{
					if (NPC.unlockedSlimeRedSpawn)
					{
						return;
					}
					NPC.unlockedSlimeRedSpawn = true;
					NetMessage.TrySendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
				num330 *= 16;
				num331 *= 16;
				NPC npc7 = new NPC();
				npc7.SetDefaults(num332, default(NPCSpawnParams));
				int type4 = npc7.type;
				int netID = npc7.netID;
				int num333 = NPC.NewNPC(new EntitySource_FishedOut(Main.player[this.whoAmI]), num330, num331, num332, 0, 0f, 0f, 0f, 0f, 255);
				if (netID != type4)
				{
					Main.npc[num333].SetDefaults(netID, default(NPCSpawnParams));
					NetMessage.TrySendData(23, -1, -1, null, num333, 0f, 0f, 0f, 0, 0, 0);
				}
				if (num332 == 682)
				{
					WorldGen.CheckAchievement_RealEstateAndTownSlimes();
					return;
				}
				return;
			}
			case 131:
			{
				if (Main.netMode != 1)
				{
					return;
				}
				int num334 = (int)this.reader.ReadUInt16();
				NPC npc8;
				if (num334 < Main.maxNPCs)
				{
					npc8 = Main.npc[num334];
				}
				else
				{
					npc8 = new NPC();
				}
				int num335 = (int)this.reader.ReadByte();
				if (num335 == 1)
				{
					int num336 = this.reader.ReadInt32();
					int num337 = (int)this.reader.ReadInt16();
					npc8.GetImmuneTime(num337, num336);
					return;
				}
				return;
			}
			case 132:
			{
				if (Main.netMode != 1)
				{
					return;
				}
				Point point = this.reader.ReadVector2().ToPoint();
				ushort num338 = this.reader.ReadUInt16();
				LegacySoundStyle legacySoundStyle2 = SoundID.SoundByIndex[num338];
				BitsByte bitsByte34 = this.reader.ReadByte();
				int num339;
				if (bitsByte34[0])
				{
					num339 = this.reader.ReadInt32();
				}
				else
				{
					num339 = legacySoundStyle2.Style;
				}
				float num340;
				if (bitsByte34[1])
				{
					num340 = MathHelper.Clamp(this.reader.ReadSingle(), 0f, 1f);
				}
				else
				{
					num340 = legacySoundStyle2.Volume;
				}
				float num341;
				if (bitsByte34[2])
				{
					num341 = MathHelper.Clamp(this.reader.ReadSingle(), -1f, 1f);
				}
				else
				{
					num341 = legacySoundStyle2.GetRandomPitch();
				}
				SoundEngine.PlaySound(legacySoundStyle2.SoundId, point.X, point.Y, num339, num340, num341);
				return;
			}
			case 133:
			{
				if (Main.netMode != 2)
				{
					return;
				}
				int num342 = (int)this.reader.ReadInt16();
				int num343 = (int)this.reader.ReadInt16();
				int num344 = (int)this.reader.ReadInt16();
				int num345 = (int)this.reader.ReadByte();
				int num346 = (int)this.reader.ReadInt16();
				TEFoodPlatter.TryPlacing(num342, num343, num344, num345, num346);
				return;
			}
			case 134:
			{
				int num347 = (int)this.reader.ReadByte();
				int num348 = this.reader.ReadInt32();
				float num349 = this.reader.ReadSingle();
				byte b20 = this.reader.ReadByte();
				bool flag31 = this.reader.ReadBoolean();
				bool flag32 = this.reader.ReadBoolean();
				float num350 = this.reader.ReadSingle();
				float num351 = this.reader.ReadSingle();
				byte b21 = this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num347 = this.whoAmI;
				}
				Player player23 = Main.player[num347];
				player23.ladyBugLuckTimeLeft = num348;
				player23.torchLuck = num349;
				player23.luckPotion = b20;
				player23.HasGardenGnomeNearby = flag31;
				player23.brokenMirrorBadLuck = flag32;
				player23.equipmentBasedLuckBonus = num350;
				player23.coinLuck = num351;
				player23.kiteLuckLevel = b21;
				player23.RecalculateLuck();
				if (Main.netMode == 2)
				{
					NetMessage.SendData(134, -1, num347, null, num347, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 135:
			{
				int num352 = (int)this.reader.ReadByte();
				if (Main.netMode == 1)
				{
					Main.player[num352].immuneAlpha = 255;
					return;
				}
				return;
			}
			case 136:
			{
				for (int num353 = 0; num353 < 2; num353++)
				{
					for (int num354 = 0; num354 < 3; num354++)
					{
						NPC.cavernMonsterType[num353, num354] = (int)this.reader.ReadUInt16();
					}
				}
				return;
			}
			case 137:
			{
				if (Main.netMode != 2)
				{
					return;
				}
				int num355 = (int)this.reader.ReadInt16();
				int num356 = (int)this.reader.ReadUInt16();
				if (num355 >= 0 && num355 < Main.maxNPCs)
				{
					Main.npc[num355].RequestBuffRemoval(num356);
					return;
				}
				return;
			}
			case 138:
				goto IL_9FE8;
			case 139:
				if (Main.netMode != 2)
				{
					int num357 = (int)this.reader.ReadByte();
					bool flag33 = this.reader.ReadBoolean();
					Main.countsAsHostForGameplay[num357] = flag33;
					return;
				}
				return;
			case 140:
			{
				int num358 = (int)this.reader.ReadByte();
				int num359 = this.reader.ReadInt32();
				switch (num358)
				{
				case 0:
					if (Main.netMode != 1)
					{
						return;
					}
					CreditsRollEvent.SetRemainingTimeDirect(num359);
					return;
				case 1:
					if (Main.netMode != 2)
					{
						return;
					}
					NPC.TransformCopperSlime(num359);
					return;
				case 2:
					if (Main.netMode != 2)
					{
						return;
					}
					NPC.TransformElderSlime(num359);
					return;
				default:
					return;
				}
				break;
			}
			case 141:
			{
				LucyAxeMessage.MessageSource messageSource = (LucyAxeMessage.MessageSource)this.reader.ReadByte();
				byte b22 = this.reader.ReadByte();
				Vector2 vector22 = this.reader.ReadVector2();
				int num360 = this.reader.ReadInt32();
				int num361 = this.reader.ReadInt32();
				if (Main.netMode == 2)
				{
					NetMessage.SendData(141, -1, this.whoAmI, null, (int)messageSource, (float)b22, vector22.X, vector22.Y, num360, num361, 0);
					return;
				}
				LucyAxeMessage.CreateFromNet(messageSource, b22, new Vector2((float)num360, (float)num361), vector22);
				return;
			}
			case 142:
			{
				int num362 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num362 = this.whoAmI;
				}
				Player player24 = Main.player[num362];
				player24.piggyBankProjTracker.TryReading(this.reader);
				player24.voidLensChest.TryReading(this.reader);
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(142, -1, this.whoAmI, null, num362, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 143:
				if (Main.netMode != 2)
				{
					return;
				}
				DD2Event.AttemptToSkipWaitTime();
				return;
			case 144:
				if (Main.netMode != 2)
				{
					return;
				}
				NPC.HaveDryadDoStardewAnimation();
				return;
			case 146:
			{
				int num363 = (int)this.reader.ReadByte();
				if (num363 == 0)
				{
					WorldItem.ShimmerEffect(this.reader.ReadVector2());
					return;
				}
				if (num363 == 1)
				{
					Vector2 vector23 = this.reader.ReadVector2();
					int num364 = this.reader.ReadInt32();
					Main.player[Main.myPlayer].AddCoinLuck(vector23, num364);
					return;
				}
				return;
			}
			case 147:
			{
				int num365 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num365 = this.whoAmI;
				}
				int num366 = (int)this.reader.ReadByte();
				Main.player[num365].TrySwitchingLoadout(num366);
				MessageBuffer.ReadAccessoryVisibility(this.reader, Main.player[num365].hideVisibleAccessory);
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData((int)b, -1, num365, null, num365, (float)num366, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 149:
			{
				if (Main.netMode != 2)
				{
					return;
				}
				int num367 = (int)this.reader.ReadInt16();
				int num368 = (int)this.reader.ReadInt16();
				int num369 = (int)this.reader.ReadInt16();
				int num370 = (int)this.reader.ReadByte();
				int num371 = (int)this.reader.ReadInt16();
				TEDeadCellsDisplayJar.TryPlacing(num367, num368, num369, num370, num371);
				return;
			}
			case 150:
			{
				int num372 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num372 = this.whoAmI;
				}
				int num373 = (int)this.reader.ReadInt16();
				Player player25 = Main.player[num372];
				if (Main.netMode == 2)
				{
					if (num373 >= 0)
					{
						player25.SetOrRequestSpectating(num373);
						return;
					}
					player25.spectating = -1;
					NetMessage.SendData(150, -1, this.whoAmI, null, this.whoAmI, (float)num373, 0f, 0f, 0, 0, 0);
					return;
				}
				else
				{
					if (player25 != Main.LocalPlayer || player25.spectating >= 0)
					{
						player25.spectating = num373;
						return;
					}
					return;
				}
				break;
			}
			case 151:
			{
				int num374 = (int)this.reader.ReadInt16();
				WorldItem worldItem4 = Main.item[num374];
				if (Main.netMode == 2 && Main.timeItemSlotCannotBeReusedFor[num374] > 0)
				{
					return;
				}
				if (Main.netMode == 2 && worldItem4.playerIndexTheItemIsReservedFor != this.whoAmI)
				{
					return;
				}
				worldItem4.playerIndexTheItemIsReservedFor = 255;
				worldItem4.TurnToAir(false);
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(151, -1, this.whoAmI, null, num374, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 152:
			{
				int num375 = (int)this.reader.ReadByte();
				if (Main.netMode == 2)
				{
					num375 = this.whoAmI;
				}
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(152, -1, this.whoAmI, null, num375, 0f, 0f, 0f, 0, 0, 0);
				}
				if (Main.netMode != 1)
				{
					return;
				}
				Player player26 = Main.player[num375];
				Item item4 = player26.inventory[player26.selectedItem];
				if (item4.UseSound != null)
				{
					SoundEngine.PlaySound(item4.UseSound, player26.Center, item4.useSoundPitch, 1f);
					return;
				}
				return;
			}
			case 153:
			{
				int num376 = (int)this.reader.ReadByte();
				int num377 = (int)this.reader.ReadInt16();
				Main.npc[num376].GetHurtByDebuff(num377);
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(153, -1, this.whoAmI, null, num376, (float)num377, 0f, 0f, 0, 0, 0);
					return;
				}
				return;
			}
			case 154:
				if (Main.netMode == 2)
				{
					NetMessage.TrySendData(154, this.whoAmI, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				Ping.PingRecieved();
				return;
			case 155:
			{
				short num378 = this.reader.ReadInt16();
				short num379 = this.reader.ReadInt16();
				if (num378 < 0 || num378 >= 8000)
				{
					return;
				}
				Main.chest[(int)num378].Resize((int)num379);
				return;
			}
			case 156:
			{
				if (Main.netMode != 2)
				{
					return;
				}
				Point16 point2 = new Point16(this.reader.ReadInt16(), this.reader.ReadInt16());
				int num380 = (int)this.reader.ReadInt16();
				TELeashedEntityAnchorWithItem teleashedEntityAnchorWithItem;
				if (TileEntity.TryGetAt<TELeashedEntityAnchorWithItem>((int)point2.X, (int)point2.Y, out teleashedEntityAnchorWithItem))
				{
					teleashedEntityAnchorWithItem.InsertItem(num380);
					return;
				}
				return;
			}
			case 158:
			{
				if (Main.netMode == 2)
				{
					return;
				}
				byte b23 = this.reader.ReadByte();
				Main.player[(int)b23].Spawn(PlayerSpawnContext.TeamSwap);
				return;
			}
			case 159:
			{
				if (Main.netMode != 2)
				{
					return;
				}
				int num381 = (int)this.reader.ReadUInt16();
				int num382 = (int)this.reader.ReadUInt16();
				NetMessage.SendSection(this.whoAmI, num381, num382);
				return;
			}
			case 160:
			{
				if (Main.netMode == 2)
				{
					return;
				}
				int num383 = (int)this.reader.ReadInt16();
				Vector2 vector24 = this.reader.ReadVector2();
				Main.item[num383].position = vector24;
				return;
			}
			case 161:
			{
				string text7 = this.reader.ReadString();
				Main.player[this.whoAmI].host = !string.IsNullOrWhiteSpace(Netplay.HostToken) && Netplay.HostToken == text7;
				return;
			}
			default:
				goto IL_9FE8;
			}
			if (Main.netMode != 2)
			{
				return;
			}
			if (Netplay.Clients[this.whoAmI].State == 1)
			{
				Netplay.Clients[this.whoAmI].State = 2;
			}
			NetMessage.TrySendData(7, this.whoAmI, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			Main.SyncAnInvasion(this.whoAmI);
			return;
			IL_550B:
			if (Netplay.Connection.State == 6)
			{
				Netplay.Connection.State = 10;
				Main.player[Main.myPlayer].Spawn(PlayerSpawnContext.SpawningIntoWorld);
				return;
			}
			return;
			IL_79D1:
			int num384 = (int)this.reader.ReadInt16();
			int num385 = this.reader.ReadInt32();
			float num386 = this.reader.ReadSingle();
			float num387 = this.reader.ReadSingle();
			if (num384 < 0 || num384 > Main.maxNPCs)
			{
				return;
			}
			if (Main.netMode == 1)
			{
				Main.npc[num384].moneyPing(new Vector2(num386, num387));
				Main.npc[num384].extraValue = num385;
				return;
			}
			Main.npc[num384].extraValue += num385;
			NetMessage.TrySendData(92, -1, -1, null, num384, (float)Main.npc[num384].extraValue, num386, num387, 0, 0, 0);
			return;
			IL_9FE8:
			if (Main.netMode == 2 && Netplay.Clients[this.whoAmI].State == 0)
			{
				NetMessage.BootPlayer(this.whoAmI, Lang.mp[2].ToNetworkText());
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0002C174 File Offset: 0x0002A374
		private static void ReadAccessoryVisibility(BinaryReader reader, bool[] hideVisibleAccessory)
		{
			ushort num = reader.ReadUInt16();
			for (int i = 0; i < hideVisibleAccessory.Length; i++)
			{
				hideVisibleAccessory[i] = ((int)num & (1 << i)) != 0;
			}
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0002C1A4 File Offset: 0x0002A3A4
		private static void TrySendingItemArray(int plr, Item[] array, int slotStartIndex)
		{
			for (int i = 0; i < array.Length; i++)
			{
				NetMessage.TrySendData(5, -1, -1, null, plr, (float)(slotStartIndex + i), 0f, 0f, 0, 0, 0);
			}
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0002C1DC File Offset: 0x0002A3DC
		public MessageBuffer()
		{
		}

		// Token: 0x04000187 RID: 391
		public const int readBufferMax = 131070;

		// Token: 0x04000188 RID: 392
		public const int writeBufferMax = 131070;

		// Token: 0x04000189 RID: 393
		public bool broadcast;

		// Token: 0x0400018A RID: 394
		public byte[] readBuffer = new byte[131070];

		// Token: 0x0400018B RID: 395
		public byte[] writeBuffer = new byte[131070];

		// Token: 0x0400018C RID: 396
		public bool writeLocked;

		// Token: 0x0400018D RID: 397
		public int messageLength;

		// Token: 0x0400018E RID: 398
		public int totalData;

		// Token: 0x0400018F RID: 399
		public int whoAmI;

		// Token: 0x04000190 RID: 400
		public int spamCount;

		// Token: 0x04000191 RID: 401
		public int maxSpam;

		// Token: 0x04000192 RID: 402
		public bool checkBytes;

		// Token: 0x04000193 RID: 403
		public MemoryStream readerStream;

		// Token: 0x04000194 RID: 404
		public MemoryStream writerStream;

		// Token: 0x04000195 RID: 405
		public BinaryReader reader;

		// Token: 0x04000196 RID: 406
		public BinaryWriter writer;

		// Token: 0x04000197 RID: 407
		public PacketHistory History = new PacketHistory(100, 65535);

		// Token: 0x04000198 RID: 408
		[CompilerGenerated]
		private static TileChangeReceivedEvent OnTileChangeReceived;

		// Token: 0x04000199 RID: 409
		private float[] _temporaryProjectileAI = new float[Projectile.maxAI];

		// Token: 0x0400019A RID: 410
		private float[] _temporaryNPCAI = new float[NPC.maxAI];
	}
}
