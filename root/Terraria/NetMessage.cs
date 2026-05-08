using System;
using System.IO;
using Ionic.Zlib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Events;
using Terraria.GameContent.Items;
using Terraria.GameContent.Tile_Entities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Map;
using Terraria.Net.Sockets;
using Terraria.Social;
using Terraria.Testing;

namespace Terraria
{
	// Token: 0x02000030 RID: 48
	public class NetMessage
	{
		// Token: 0x060002A7 RID: 679 RVA: 0x0003C410 File Offset: 0x0003A610
		public static bool TrySendData(int msgType, int remoteClient = -1, int ignoreClient = -1, NetworkText text = null, int number = 0, float number2 = 0f, float number3 = 0f, float number4 = 0f, int number5 = 0, int number6 = 0, int number7 = 0)
		{
			try
			{
				NetMessage.SendData(msgType, remoteClient, ignoreClient, text, number, number2, number3, number4, number5, number6, number7);
			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0003C450 File Offset: 0x0003A650
		public static void SendData(int msgType, int remoteClient = -1, int ignoreClient = -1, NetworkText text = null, int number = 0, float number2 = 0f, float number3 = 0f, float number4 = 0f, int number5 = 0, int number6 = 0, int number7 = 0)
		{
			if (Main.netMode == 0)
			{
				return;
			}
			if (msgType == 21 && (Main.item[number].shimmerTime > 0f || Main.item[number].shimmered))
			{
				msgType = 145;
			}
			if (msgType == 21 && Main.item[number].type == 0)
			{
				msgType = 151;
			}
			int num = 256;
			if (text == null)
			{
				text = NetworkText.Empty;
			}
			if (Main.netMode == 2 && remoteClient >= 0)
			{
				num = remoteClient;
			}
			MessageBuffer messageBuffer = NetMessage.buffer[num];
			lock (messageBuffer)
			{
				BinaryWriter binaryWriter = NetMessage.buffer[num].writer;
				if (binaryWriter == null)
				{
					NetMessage.buffer[num].ResetWriter();
					binaryWriter = NetMessage.buffer[num].writer;
				}
				binaryWriter.BaseStream.Position = 0L;
				long position = binaryWriter.BaseStream.Position;
				binaryWriter.BaseStream.Position += 2L;
				binaryWriter.Write((byte)msgType);
				switch (msgType)
				{
				case 1:
					binaryWriter.Write("Terraria" + 319);
					break;
				case 2:
					text.Serialize(binaryWriter);
					if (Main.dedServ)
					{
						Console.WriteLine(Language.GetTextValue("CLI.ClientWasBooted", Netplay.Clients[num].Socket.GetRemoteAddress().ToString(), text));
					}
					break;
				case 3:
					binaryWriter.Write((byte)remoteClient);
					binaryWriter.Write(false);
					break;
				case 4:
				{
					Player player = Main.player[number];
					binaryWriter.Write((byte)number);
					binaryWriter.Write((byte)player.skinVariant);
					binaryWriter.Write((byte)player.voiceVariant);
					binaryWriter.Write(player.voicePitchOffset);
					binaryWriter.Write((byte)player.hair);
					binaryWriter.Write(player.name);
					binaryWriter.Write(player.hairDye);
					NetMessage.WriteAccessoryVisibility(binaryWriter, player.hideVisibleAccessory);
					binaryWriter.Write(player.hideMisc);
					binaryWriter.WriteRGB(player.hairColor);
					binaryWriter.WriteRGB(player.skinColor);
					binaryWriter.WriteRGB(player.eyeColor);
					binaryWriter.WriteRGB(player.shirtColor);
					binaryWriter.WriteRGB(player.underShirtColor);
					binaryWriter.WriteRGB(player.pantsColor);
					binaryWriter.WriteRGB(player.shoeColor);
					BitsByte bitsByte = 0;
					if (player.difficulty == 1)
					{
						bitsByte[0] = true;
					}
					else if (player.difficulty == 2)
					{
						bitsByte[1] = true;
					}
					else if (player.difficulty == 3)
					{
						bitsByte[3] = true;
					}
					bitsByte[2] = player.extraAccessory;
					binaryWriter.Write(bitsByte);
					BitsByte bitsByte2 = 0;
					bitsByte2[0] = player.UsingBiomeTorches;
					bitsByte2[1] = player.happyFunTorchTime;
					bitsByte2[2] = player.unlockedBiomeTorches;
					bitsByte2[3] = player.unlockedSuperCart;
					bitsByte2[4] = player.enabledSuperCart;
					binaryWriter.Write(bitsByte2);
					BitsByte bitsByte3 = 0;
					bitsByte3[0] = player.usedAegisCrystal;
					bitsByte3[1] = player.usedAegisFruit;
					bitsByte3[2] = player.usedArcaneCrystal;
					bitsByte3[3] = player.usedGalaxyPearl;
					bitsByte3[4] = player.usedGummyWorm;
					bitsByte3[5] = player.usedAmbrosia;
					bitsByte3[6] = player.ateArtisanBread;
					binaryWriter.Write(bitsByte3);
					break;
				}
				case 5:
				{
					binaryWriter.Write((byte)number);
					binaryWriter.Write((short)number2);
					PlayerItemSlotID.SlotReference slotReference = new PlayerItemSlotID.SlotReference(Main.player[number], (int)number2);
					Item item = slotReference.Item;
					if (item.Name == "" || item.stack == 0 || item.type == 0)
					{
						item.SetDefaults(0, null);
					}
					int num2 = item.stack;
					int type = item.type;
					if (num2 < 0)
					{
						num2 = 0;
					}
					binaryWriter.Write((short)num2);
					binaryWriter.Write(item.prefix);
					binaryWriter.Write((short)type);
					BitsByte bitsByte4 = default(BitsByte);
					bitsByte4[0] = item.favorited;
					bitsByte4[1] = number3 != 0f;
					binaryWriter.Write(bitsByte4);
					break;
				}
				case 7:
				{
					binaryWriter.Write((int)Main.time);
					BitsByte bitsByte5 = 0;
					bitsByte5[0] = Main.dayTime;
					bitsByte5[1] = Main.bloodMoon;
					bitsByte5[2] = Main.eclipse;
					binaryWriter.Write(bitsByte5);
					binaryWriter.Write((byte)Main.moonPhase);
					binaryWriter.Write((short)Main.maxTilesX);
					binaryWriter.Write((short)Main.maxTilesY);
					binaryWriter.Write((short)Main.spawnTileX);
					binaryWriter.Write((short)Main.spawnTileY);
					binaryWriter.Write((short)Main.worldSurface);
					binaryWriter.Write((short)Main.rockLayer);
					binaryWriter.Write(Main.ActiveWorldFileData.WorldId);
					binaryWriter.Write(Main.worldName);
					binaryWriter.Write((byte)Main.GameMode);
					binaryWriter.Write(Main.ActiveWorldFileData.UniqueId.ToByteArray());
					binaryWriter.Write(Main.ActiveWorldFileData.WorldGeneratorVersion);
					binaryWriter.Write((byte)Main.moonType);
					binaryWriter.Write((byte)WorldGen.treeBG1);
					binaryWriter.Write((byte)WorldGen.treeBG2);
					binaryWriter.Write((byte)WorldGen.treeBG3);
					binaryWriter.Write((byte)WorldGen.treeBG4);
					binaryWriter.Write((byte)WorldGen.corruptBG);
					binaryWriter.Write((byte)WorldGen.jungleBG);
					binaryWriter.Write((byte)WorldGen.snowBG);
					binaryWriter.Write((byte)WorldGen.hallowBG);
					binaryWriter.Write((byte)WorldGen.crimsonBG);
					binaryWriter.Write((byte)WorldGen.desertBG);
					binaryWriter.Write((byte)WorldGen.oceanBG);
					binaryWriter.Write((byte)WorldGen.mushroomBG);
					binaryWriter.Write((byte)WorldGen.underworldBG);
					binaryWriter.Write((byte)Main.iceBackStyle);
					binaryWriter.Write((byte)Main.jungleBackStyle);
					binaryWriter.Write((byte)Main.hellBackStyle);
					binaryWriter.Write(Main.windSpeedTarget);
					binaryWriter.Write((byte)Main.numClouds);
					for (int i = 0; i < 3; i++)
					{
						binaryWriter.Write(Main.treeX[i]);
					}
					for (int j = 0; j < 4; j++)
					{
						binaryWriter.Write((byte)Main.treeStyle[j]);
					}
					for (int k = 0; k < 3; k++)
					{
						binaryWriter.Write(Main.caveBackX[k]);
					}
					for (int l = 0; l < 4; l++)
					{
						binaryWriter.Write((byte)Main.caveBackStyle[l]);
					}
					WorldGen.TreeTops.SyncSend(binaryWriter);
					if (!Main.raining)
					{
						Main.maxRaining = 0f;
					}
					binaryWriter.Write(Main.maxRaining);
					BitsByte bitsByte6 = 0;
					bitsByte6[0] = WorldGen.shadowOrbSmashed;
					bitsByte6[1] = NPC.downedBoss1;
					bitsByte6[2] = NPC.downedBoss2;
					bitsByte6[3] = NPC.downedBoss3;
					bitsByte6[4] = Main.hardMode;
					bitsByte6[5] = NPC.downedClown;
					bitsByte6[7] = NPC.downedPlantBoss;
					binaryWriter.Write(bitsByte6);
					BitsByte bitsByte7 = 0;
					bitsByte7[0] = NPC.downedMechBoss1;
					bitsByte7[1] = NPC.downedMechBoss2;
					bitsByte7[2] = NPC.downedMechBoss3;
					bitsByte7[3] = NPC.downedMechBossAny;
					bitsByte7[4] = Main.cloudBGActive >= 1f;
					bitsByte7[5] = WorldGen.crimson;
					bitsByte7[6] = Main.pumpkinMoon;
					bitsByte7[7] = Main.snowMoon;
					binaryWriter.Write(bitsByte7);
					BitsByte bitsByte8 = 0;
					bitsByte8[1] = Main.fastForwardTimeToDawn;
					bitsByte8[2] = Main.slimeRain;
					bitsByte8[3] = NPC.downedSlimeKing;
					bitsByte8[4] = NPC.downedQueenBee;
					bitsByte8[5] = NPC.downedFishron;
					bitsByte8[6] = NPC.downedMartians;
					bitsByte8[7] = NPC.downedAncientCultist;
					binaryWriter.Write(bitsByte8);
					BitsByte bitsByte9 = 0;
					bitsByte9[0] = NPC.downedMoonlord;
					bitsByte9[1] = NPC.downedHalloweenKing;
					bitsByte9[2] = NPC.downedHalloweenTree;
					bitsByte9[3] = NPC.downedChristmasIceQueen;
					bitsByte9[4] = NPC.downedChristmasSantank;
					bitsByte9[5] = NPC.downedChristmasTree;
					bitsByte9[6] = NPC.downedGolemBoss;
					bitsByte9[7] = BirthdayParty.PartyIsUp;
					binaryWriter.Write(bitsByte9);
					BitsByte bitsByte10 = 0;
					bitsByte10[0] = NPC.downedPirates;
					bitsByte10[1] = NPC.downedFrost;
					bitsByte10[2] = NPC.downedGoblins;
					bitsByte10[3] = Sandstorm.Happening;
					bitsByte10[4] = DD2Event.Ongoing;
					bitsByte10[5] = DD2Event.DownedInvasionT1;
					bitsByte10[6] = DD2Event.DownedInvasionT2;
					bitsByte10[7] = DD2Event.DownedInvasionT3;
					binaryWriter.Write(bitsByte10);
					BitsByte bitsByte11 = 0;
					bitsByte11[0] = NPC.combatBookWasUsed;
					bitsByte11[1] = LanternNight.LanternsUp;
					bitsByte11[2] = NPC.downedTowerSolar;
					bitsByte11[3] = NPC.downedTowerVortex;
					bitsByte11[4] = NPC.downedTowerNebula;
					bitsByte11[5] = NPC.downedTowerStardust;
					bitsByte11[6] = Main.forceHalloweenForToday;
					bitsByte11[7] = Main.forceXMasForToday;
					binaryWriter.Write(bitsByte11);
					BitsByte bitsByte12 = 0;
					bitsByte12[0] = NPC.boughtCat;
					bitsByte12[1] = NPC.boughtDog;
					bitsByte12[2] = NPC.boughtBunny;
					bitsByte12[3] = NPC.freeCake;
					bitsByte12[4] = Main.drunkWorld;
					bitsByte12[5] = NPC.downedEmpressOfLight;
					bitsByte12[6] = NPC.downedQueenSlime;
					bitsByte12[7] = Main.getGoodWorld;
					binaryWriter.Write(bitsByte12);
					BitsByte bitsByte13 = 0;
					bitsByte13[0] = Main.tenthAnniversaryWorld;
					bitsByte13[1] = Main.dontStarveWorld;
					bitsByte13[2] = NPC.downedDeerclops;
					bitsByte13[3] = Main.notTheBeesWorld;
					bitsByte13[4] = Main.remixWorld;
					bitsByte13[5] = NPC.unlockedSlimeBlueSpawn;
					bitsByte13[6] = NPC.combatBookVolumeTwoWasUsed;
					bitsByte13[7] = NPC.peddlersSatchelWasUsed;
					binaryWriter.Write(bitsByte13);
					BitsByte bitsByte14 = 0;
					bitsByte14[0] = NPC.unlockedSlimeGreenSpawn;
					bitsByte14[1] = NPC.unlockedSlimeOldSpawn;
					bitsByte14[2] = NPC.unlockedSlimePurpleSpawn;
					bitsByte14[3] = NPC.unlockedSlimeRainbowSpawn;
					bitsByte14[4] = NPC.unlockedSlimeRedSpawn;
					bitsByte14[5] = NPC.unlockedSlimeYellowSpawn;
					bitsByte14[6] = NPC.unlockedSlimeCopperSpawn;
					bitsByte14[7] = Main.fastForwardTimeToDusk;
					binaryWriter.Write(bitsByte14);
					BitsByte bitsByte15 = 0;
					bitsByte15[0] = Main.noTrapsWorld;
					bitsByte15[1] = Main.zenithWorld;
					bitsByte15[2] = NPC.unlockedTruffleSpawn;
					bitsByte15[3] = Main.vampireSeed;
					bitsByte15[4] = Main.infectedSeed;
					bitsByte15[5] = Main.teamBasedSpawnsSeed;
					bitsByte15[6] = Main.skyblockWorld;
					bitsByte15[7] = Main.dualDungeonsSeed;
					binaryWriter.Write(bitsByte15);
					BitsByte bitsByte16 = 0;
					bitsByte16[0] = WorldGen.Skyblock.lowTiles;
					binaryWriter.Write(bitsByte16);
					binaryWriter.Write((byte)Main.sundialCooldown);
					binaryWriter.Write((byte)Main.moondialCooldown);
					binaryWriter.Write((short)WorldGen.SavedOreTiers.Copper);
					binaryWriter.Write((short)WorldGen.SavedOreTiers.Iron);
					binaryWriter.Write((short)WorldGen.SavedOreTiers.Silver);
					binaryWriter.Write((short)WorldGen.SavedOreTiers.Gold);
					binaryWriter.Write((short)WorldGen.SavedOreTiers.Cobalt);
					binaryWriter.Write((short)WorldGen.SavedOreTiers.Mythril);
					binaryWriter.Write((short)WorldGen.SavedOreTiers.Adamantite);
					binaryWriter.Write((sbyte)Main.invasionType);
					if (SocialAPI.Network != null)
					{
						binaryWriter.Write(SocialAPI.Network.GetLobbyId());
					}
					else
					{
						binaryWriter.Write(0UL);
					}
					binaryWriter.Write(Sandstorm.IntendedSeverity);
					ExtraSpawnPointManager.Write(binaryWriter, true);
					break;
				}
				case 8:
					binaryWriter.Write(number);
					binaryWriter.Write((int)number2);
					binaryWriter.Write((byte)number3);
					break;
				case 9:
				{
					binaryWriter.Write(number);
					text.Serialize(binaryWriter);
					BitsByte bitsByte17 = (byte)number2;
					binaryWriter.Write(bitsByte17);
					break;
				}
				case 10:
					NetMessage.CompressTileBlock(number, (int)number2, (short)number3, (short)number4, binaryWriter.BaseStream);
					break;
				case 11:
					binaryWriter.Write((short)number);
					binaryWriter.Write((short)number2);
					binaryWriter.Write((short)number3);
					binaryWriter.Write((short)number4);
					break;
				case 12:
				{
					Player player2 = Main.player[number];
					binaryWriter.Write((byte)number);
					binaryWriter.Write((short)player2.SpawnX);
					binaryWriter.Write((short)player2.SpawnY);
					binaryWriter.Write(player2.respawnTimer);
					binaryWriter.Write((short)player2.numberOfDeathsPVE);
					binaryWriter.Write((short)player2.numberOfDeathsPVP);
					binaryWriter.Write((byte)player2.team);
					binaryWriter.Write((byte)number2);
					break;
				}
				case 13:
				{
					Player player3 = Main.player[number];
					binaryWriter.Write((byte)number);
					BitsByte bitsByte18 = 0;
					bitsByte18[0] = player3.controlUp;
					bitsByte18[1] = player3.controlDown;
					bitsByte18[2] = player3.controlLeft;
					bitsByte18[3] = player3.controlRight;
					bitsByte18[4] = player3.controlJump;
					bitsByte18[5] = player3.controlUseItem;
					bitsByte18[6] = player3.direction == 1;
					binaryWriter.Write(bitsByte18);
					BitsByte bitsByte19 = 0;
					bitsByte19[0] = player3.pulley;
					bitsByte19[1] = player3.pulley && player3.pulleyDir == 2;
					bitsByte19[2] = player3.velocity != Vector2.Zero;
					bitsByte19[3] = player3.vortexStealthActive;
					bitsByte19[4] = player3.gravDir == 1f;
					bitsByte19[5] = player3.shieldRaised;
					bitsByte19[6] = player3.ghost;
					bitsByte19[7] = player3.mount.Active;
					binaryWriter.Write(bitsByte19);
					BitsByte bitsByte20 = 0;
					bitsByte20[0] = player3.tryKeepingHoveringUp;
					bitsByte20[1] = player3.IsVoidVaultEnabled;
					bitsByte20[2] = player3.sitting.isSitting;
					bitsByte20[3] = player3.downedDD2EventAnyDifficulty;
					bitsByte20[4] = player3.petting.isPetting;
					bitsByte20[5] = player3.petting.isPetSmall;
					bitsByte20[6] = player3.PotionOfReturnOriginalUsePosition != null;
					bitsByte20[7] = player3.tryKeepingHoveringDown;
					binaryWriter.Write(bitsByte20);
					BitsByte bitsByte21 = 0;
					bitsByte21[0] = player3.sleeping.isSleeping;
					bitsByte21[1] = player3.autoReuseAllWeapons;
					bitsByte21[2] = player3.controlDownHold;
					bitsByte21[3] = player3.isOperatingAnotherEntity;
					bitsByte21[4] = player3.controlUseTile;
					bitsByte21[5] = player3.netCameraTarget != null;
					bitsByte21[6] = player3.lastItemUseAttemptSuccess;
					binaryWriter.Write(bitsByte21);
					binaryWriter.Write((byte)player3.selectedItem);
					binaryWriter.WriteVector2(player3.position);
					if (bitsByte19[2])
					{
						binaryWriter.WriteVector2(player3.velocity);
					}
					if (bitsByte19[7])
					{
						binaryWriter.Write((ushort)player3.mount.Type);
					}
					if (bitsByte20[6])
					{
						binaryWriter.WriteVector2(player3.PotionOfReturnOriginalUsePosition.Value);
						binaryWriter.WriteVector2(player3.PotionOfReturnHomePosition.Value);
					}
					if (bitsByte21[5])
					{
						binaryWriter.WriteVector2(player3.netCameraTarget.Value);
					}
					if (player3 == Main.LocalPlayer)
					{
						player3.lastSyncedNetCameraTarget = player3.netCameraTarget;
					}
					break;
				}
				case 14:
					binaryWriter.Write((byte)number);
					binaryWriter.Write((byte)number2);
					break;
				case 16:
					binaryWriter.Write((byte)number);
					binaryWriter.Write((short)Main.player[number].statLife);
					binaryWriter.Write((short)Main.player[number].statLifeMax);
					break;
				case 17:
					binaryWriter.Write((byte)number);
					binaryWriter.Write((short)number2);
					binaryWriter.Write((short)number3);
					binaryWriter.Write((short)number4);
					binaryWriter.Write((byte)number5);
					break;
				case 18:
					binaryWriter.Write(Main.dayTime ? 1 : 0);
					binaryWriter.Write((int)Main.time);
					binaryWriter.Write(Main.sunModY);
					binaryWriter.Write(Main.moonModY);
					break;
				case 19:
					binaryWriter.Write((byte)number);
					binaryWriter.Write((short)number2);
					binaryWriter.Write((short)number3);
					binaryWriter.Write((number4 == 1f) ? 1 : 0);
					break;
				case 20:
				{
					int num3 = number;
					int num4 = (int)number2;
					int num5 = (int)number3;
					if (num5 < 0)
					{
						num5 = 0;
					}
					int num6 = (int)number4;
					if (num6 < 0)
					{
						num6 = 0;
					}
					if (num3 < num5)
					{
						num3 = num5;
					}
					if (num3 >= Main.maxTilesX + num5)
					{
						num3 = Main.maxTilesX - num5 - 1;
					}
					if (num4 < num6)
					{
						num4 = num6;
					}
					if (num4 >= Main.maxTilesY + num6)
					{
						num4 = Main.maxTilesY - num6 - 1;
					}
					binaryWriter.Write((short)num3);
					binaryWriter.Write((short)num4);
					binaryWriter.Write((byte)num5);
					binaryWriter.Write((byte)num6);
					binaryWriter.Write((byte)number5);
					for (int m = num3; m < num3 + num5; m++)
					{
						for (int n = num4; n < num4 + num6; n++)
						{
							BitsByte bitsByte22 = 0;
							BitsByte bitsByte23 = 0;
							BitsByte bitsByte24 = 0;
							byte b = 0;
							byte b2 = 0;
							Tile tile = Main.tile[m, n];
							bitsByte22[0] = tile.active();
							bitsByte22[2] = tile.wall > 0;
							bitsByte22[3] = tile.liquid > 0 && Main.netMode == 2;
							bitsByte22[4] = tile.wire();
							bitsByte22[5] = tile.halfBrick();
							bitsByte22[6] = tile.actuator();
							bitsByte22[7] = tile.inActive();
							bitsByte23[0] = tile.wire2();
							bitsByte23[1] = tile.wire3();
							if (tile.active() && tile.color() > 0)
							{
								bitsByte23[2] = true;
								b = tile.color();
							}
							if (tile.wall > 0 && tile.wallColor() > 0)
							{
								bitsByte23[3] = true;
								b2 = tile.wallColor();
							}
							bitsByte23 += (byte)(tile.slope() << 4);
							bitsByte23[7] = tile.wire4();
							bitsByte24[0] = tile.fullbrightBlock();
							bitsByte24[1] = tile.fullbrightWall();
							bitsByte24[2] = tile.invisibleBlock();
							bitsByte24[3] = tile.invisibleWall();
							binaryWriter.Write(bitsByte22);
							binaryWriter.Write(bitsByte23);
							binaryWriter.Write(bitsByte24);
							if (b > 0)
							{
								binaryWriter.Write(b);
							}
							if (b2 > 0)
							{
								binaryWriter.Write(b2);
							}
							if (tile.active())
							{
								binaryWriter.Write(tile.type);
								if (Main.tileFrameImportant[(int)tile.type])
								{
									binaryWriter.Write(tile.frameX);
									binaryWriter.Write(tile.frameY);
								}
							}
							if (tile.wall > 0)
							{
								binaryWriter.Write(tile.wall);
							}
							if (tile.liquid > 0 && Main.netMode == 2)
							{
								binaryWriter.Write(tile.liquid);
								binaryWriter.Write(tile.liquidType());
							}
						}
					}
					break;
				}
				case 21:
				case 90:
				case 145:
				case 148:
				{
					WorldItem worldItem = Main.item[number];
					Item inner = worldItem.inner;
					binaryWriter.Write((short)number);
					binaryWriter.WriteVector2(worldItem.position);
					binaryWriter.WriteVector2(worldItem.velocity);
					binaryWriter.Write((short)inner.stack);
					binaryWriter.Write(inner.prefix);
					binaryWriter.Write((byte)number2);
					short num7 = 0;
					if (worldItem.active && worldItem.stack > 0)
					{
						num7 = (short)worldItem.type;
					}
					binaryWriter.Write(num7);
					if (msgType == 145)
					{
						binaryWriter.Write(worldItem.shimmered);
						binaryWriter.Write(worldItem.shimmerTime);
					}
					if (msgType == 148)
					{
						binaryWriter.Write((byte)MathHelper.Clamp((float)worldItem.timeLeftInWhichTheItemCannotBeTakenByEnemies, 0f, 255f));
					}
					break;
				}
				case 22:
				{
					WorldItem worldItem2 = Main.item[number];
					binaryWriter.Write((short)number);
					binaryWriter.Write((byte)worldItem2.playerIndexTheItemIsReservedFor);
					binaryWriter.WriteVector2(worldItem2.position);
					break;
				}
				case 23:
				{
					NPC npc = Main.npc[number];
					binaryWriter.Write((short)number);
					binaryWriter.WriteVector2(npc.position);
					binaryWriter.WriteVector2(npc.velocity);
					binaryWriter.Write((ushort)npc.target);
					int num8 = npc.life;
					if (!npc.active)
					{
						num8 = 0;
					}
					short num9 = (short)npc.netID;
					bool[] array = new bool[4];
					BitsByte bitsByte25 = 0;
					bitsByte25[0] = npc.direction > 0;
					bitsByte25[1] = npc.directionY > 0;
					bitsByte25[2] = (array[0] = npc.ai[0] != 0f);
					bitsByte25[3] = (array[1] = npc.ai[1] != 0f);
					bitsByte25[4] = (array[2] = npc.ai[2] != 0f);
					bitsByte25[5] = (array[3] = npc.ai[3] != 0f);
					bitsByte25[6] = npc.spriteDirection > 0;
					bitsByte25[7] = num8 == npc.lifeMax;
					binaryWriter.Write(bitsByte25);
					BitsByte bitsByte26 = 0;
					bitsByte26[0] = npc.statsAreScaledForThisManyPlayers > 1;
					bitsByte26[1] = npc.SpawnedFromStatue;
					bitsByte26[2] = npc.difficulty != 1f;
					bitsByte26[3] = npc.spawnNeedsSyncing;
					bitsByte26[4] = npc.spawnNeedsSyncing && npc.shimmerTransparency > 0f;
					binaryWriter.Write(bitsByte26);
					for (int num10 = 0; num10 < NPC.maxAI; num10++)
					{
						if (array[num10])
						{
							binaryWriter.Write(npc.ai[num10]);
						}
					}
					binaryWriter.Write(num9);
					if (bitsByte26[0])
					{
						binaryWriter.Write((byte)npc.statsAreScaledForThisManyPlayers);
					}
					if (bitsByte26[2])
					{
						binaryWriter.Write(npc.difficulty);
					}
					if (!bitsByte25[7])
					{
						byte b3 = 1;
						if (npc.lifeMax > 32767)
						{
							b3 = 4;
						}
						else if (npc.lifeMax > 127)
						{
							b3 = 2;
						}
						binaryWriter.Write(b3);
						if (b3 == 2)
						{
							binaryWriter.Write((short)num8);
						}
						else if (b3 == 4)
						{
							binaryWriter.Write(num8);
						}
						else
						{
							binaryWriter.Write((sbyte)num8);
						}
					}
					if (npc.type >= 0 && npc.type < (int)NPCID.Count && Main.npcCatchable[npc.type])
					{
						binaryWriter.Write((byte)npc.releaseOwner);
					}
					break;
				}
				case 24:
					binaryWriter.Write((short)number);
					binaryWriter.Write((byte)number2);
					break;
				case 27:
				{
					Projectile projectile = Main.projectile[number];
					binaryWriter.Write((short)projectile.identity);
					binaryWriter.WriteVector2(projectile.position);
					binaryWriter.WriteVector2(projectile.velocity);
					binaryWriter.Write((byte)projectile.owner);
					binaryWriter.Write((short)projectile.type);
					BitsByte bitsByte27 = 0;
					BitsByte bitsByte28 = 0;
					bitsByte27[0] = projectile.ai[0] != 0f;
					bitsByte27[1] = projectile.ai[1] != 0f;
					bitsByte28[0] = projectile.ai[2] != 0f;
					if (projectile.bannerIdToRespondTo != 0)
					{
						bitsByte27[3] = true;
					}
					if (projectile.damage != 0)
					{
						bitsByte27[4] = true;
					}
					if (projectile.knockBack != 0f)
					{
						bitsByte27[5] = true;
					}
					if (projectile.type > 0 && projectile.type < (int)ProjectileID.Count && ProjectileID.Sets.NeedsUUID[projectile.type])
					{
						bitsByte27[7] = true;
					}
					if (projectile.originalDamage != 0)
					{
						bitsByte27[6] = true;
					}
					if (bitsByte28 != 0)
					{
						bitsByte27[2] = true;
					}
					binaryWriter.Write(bitsByte27);
					if (bitsByte27[2])
					{
						binaryWriter.Write(bitsByte28);
					}
					if (bitsByte27[0])
					{
						binaryWriter.Write(projectile.ai[0]);
					}
					if (bitsByte27[1])
					{
						binaryWriter.Write(projectile.ai[1]);
					}
					if (bitsByte27[3])
					{
						binaryWriter.Write((ushort)projectile.bannerIdToRespondTo);
					}
					if (bitsByte27[4])
					{
						binaryWriter.Write((short)projectile.damage);
					}
					if (bitsByte27[5])
					{
						binaryWriter.Write(projectile.knockBack);
					}
					if (bitsByte27[6])
					{
						binaryWriter.Write((short)projectile.originalDamage);
					}
					if (bitsByte27[7])
					{
						binaryWriter.Write((short)projectile.projUUID);
					}
					if (bitsByte28[0])
					{
						binaryWriter.Write(projectile.ai[2]);
					}
					break;
				}
				case 28:
					binaryWriter.Write((short)number);
					binaryWriter.Write((short)number2);
					binaryWriter.Write(number3);
					binaryWriter.Write((byte)(number4 + 1f));
					binaryWriter.Write((byte)number5);
					break;
				case 29:
					binaryWriter.Write((short)number);
					binaryWriter.Write((byte)number2);
					break;
				case 30:
					binaryWriter.Write((byte)number);
					binaryWriter.Write(Main.player[number].hostile);
					break;
				case 31:
					binaryWriter.Write((short)number);
					binaryWriter.Write((short)number2);
					break;
				case 32:
				{
					Item item2 = Main.chest[number].item[(int)((byte)number2)];
					binaryWriter.Write((short)number);
					binaryWriter.Write((byte)number2);
					short num11 = (short)item2.type;
					if (item2.Name == null)
					{
						num11 = 0;
					}
					binaryWriter.Write((short)item2.stack);
					binaryWriter.Write(item2.prefix);
					binaryWriter.Write(num11);
					break;
				}
				case 33:
				{
					int num12 = 0;
					int num13 = 0;
					int num14 = 0;
					string text2 = null;
					if (number > -1)
					{
						num12 = Main.chest[number].x;
						num13 = Main.chest[number].y;
					}
					if (number2 == 1f)
					{
						string text3 = text.ToString();
						num14 = (int)((byte)text3.Length);
						if (num14 == 0 || num14 > 20)
						{
							num14 = 255;
						}
						else
						{
							text2 = text3;
						}
					}
					binaryWriter.Write((short)number);
					binaryWriter.Write((short)num12);
					binaryWriter.Write((short)num13);
					binaryWriter.Write((byte)num14);
					if (text2 != null)
					{
						binaryWriter.Write(text2);
					}
					break;
				}
				case 34:
					binaryWriter.Write((byte)number);
					binaryWriter.Write((short)number2);
					binaryWriter.Write((short)number3);
					binaryWriter.Write((short)number4);
					if (Main.netMode == 2)
					{
						Netplay.GetSectionX((int)number2);
						Netplay.GetSectionY((int)number3);
						binaryWriter.Write((short)number5);
					}
					else
					{
						binaryWriter.Write(0);
					}
					break;
				case 35:
					binaryWriter.Write((byte)number);
					binaryWriter.Write((short)number2);
					break;
				case 36:
				{
					Player player4 = Main.player[number];
					binaryWriter.Write((byte)number);
					binaryWriter.Write(player4.zone1);
					binaryWriter.Write(player4.zone2);
					binaryWriter.Write(player4.zone3);
					binaryWriter.Write(player4.zone4);
					binaryWriter.Write(player4.zone5);
					binaryWriter.Write((byte)player4.townNPCs);
					break;
				}
				case 38:
					binaryWriter.Write(Netplay.ServerPassword);
					break;
				case 39:
					binaryWriter.Write((short)number);
					break;
				case 40:
					binaryWriter.Write((byte)number);
					binaryWriter.Write((short)Main.player[number].talkNPC);
					break;
				case 41:
					binaryWriter.Write((byte)number);
					binaryWriter.Write(Main.player[number].itemRotation);
					binaryWriter.Write((short)Main.player[number].itemAnimation);
					break;
				case 42:
					binaryWriter.Write((byte)number);
					binaryWriter.Write((short)Main.player[number].statMana);
					binaryWriter.Write((short)Main.player[number].statManaMax);
					break;
				case 43:
					binaryWriter.Write((byte)number);
					binaryWriter.Write((short)number2);
					break;
				case 45:
				case 157:
					binaryWriter.Write((byte)number);
					binaryWriter.Write((byte)Main.player[number].team);
					break;
				case 46:
					binaryWriter.Write((short)number);
					binaryWriter.Write((short)number2);
					break;
				case 47:
					binaryWriter.Write((short)number);
					binaryWriter.Write((short)Main.sign[number].x);
					binaryWriter.Write((short)Main.sign[number].y);
					binaryWriter.Write(Main.sign[number].text);
					binaryWriter.Write((byte)number2);
					binaryWriter.Write((byte)number3);
					break;
				case 48:
				{
					Tile tile2 = Main.tile[number, (int)number2];
					binaryWriter.Write((short)number);
					binaryWriter.Write((short)number2);
					binaryWriter.Write(tile2.liquid);
					binaryWriter.Write(tile2.liquidType());
					break;
				}
				case 50:
				{
					binaryWriter.Write((byte)number);
					Player player5 = Main.player[number];
					for (int num15 = 0; num15 < Player.maxBuffs; num15++)
					{
						if (player5.buffType[num15] > 0)
						{
							binaryWriter.Write((ushort)player5.buffType[num15]);
						}
					}
					binaryWriter.Write(0);
					break;
				}
				case 51:
					binaryWriter.Write((byte)number);
					binaryWriter.Write((byte)number2);
					break;
				case 52:
					binaryWriter.Write((byte)number2);
					binaryWriter.Write((short)number3);
					binaryWriter.Write((short)number4);
					break;
				case 53:
					binaryWriter.Write((short)number);
					binaryWriter.Write((ushort)number2);
					binaryWriter.Write((short)number3);
					break;
				case 54:
				{
					NPC npc2 = Main.npc[number];
					binaryWriter.Write((short)number);
					for (int num16 = 0; num16 < NPC.maxBuffs; num16++)
					{
						int num17 = npc2.buffType[num16];
						int num18 = npc2.buffTime[num16];
						if (num17 > 0 && num18 > 0)
						{
							binaryWriter.Write((ushort)num17);
							binaryWriter.Write((ushort)num18);
						}
					}
					binaryWriter.Write(0);
					break;
				}
				case 55:
					binaryWriter.Write((byte)number);
					binaryWriter.Write((ushort)number2);
					binaryWriter.Write((int)number3);
					break;
				case 56:
					binaryWriter.Write((short)number);
					if (Main.netMode == 2)
					{
						string givenName = Main.npc[number].GivenName;
						binaryWriter.Write(givenName);
						binaryWriter.Write(Main.npc[number].townNpcVariationIndex);
					}
					break;
				case 57:
					binaryWriter.Write(WorldGen.tGood);
					binaryWriter.Write(WorldGen.tEvil);
					binaryWriter.Write(WorldGen.tBlood);
					break;
				case 58:
					binaryWriter.Write((byte)number);
					binaryWriter.Write(number2);
					break;
				case 59:
					binaryWriter.Write((short)number);
					binaryWriter.Write((short)number2);
					break;
				case 60:
					binaryWriter.Write((short)number);
					binaryWriter.Write((short)number2);
					binaryWriter.Write((short)number3);
					binaryWriter.Write((byte)number4);
					break;
				case 61:
					binaryWriter.Write((short)number);
					binaryWriter.Write((short)number2);
					break;
				case 62:
					binaryWriter.Write((byte)number);
					binaryWriter.Write((byte)number2);
					break;
				case 63:
				case 64:
					binaryWriter.Write((short)number);
					binaryWriter.Write((short)number2);
					binaryWriter.Write((byte)number3);
					binaryWriter.Write((byte)number4);
					break;
				case 65:
				{
					BitsByte bitsByte29 = 0;
					bitsByte29[0] = (number & 1) == 1;
					bitsByte29[1] = (number & 2) == 2;
					bitsByte29[2] = number6 == 1;
					bitsByte29[3] = number7 != 0;
					binaryWriter.Write(bitsByte29);
					binaryWriter.Write((short)number2);
					binaryWriter.Write(number3);
					binaryWriter.Write(number4);
					binaryWriter.Write((byte)number5);
					if (bitsByte29[3])
					{
						binaryWriter.Write(number7);
					}
					if (Main.netMode == 2 && number == 0 && number2 != (float)ignoreClient)
					{
						Main.player[(int)number2].unacknowledgedTeleports++;
					}
					break;
				}
				case 66:
					binaryWriter.Write((byte)number);
					binaryWriter.Write((short)number2);
					break;
				case 68:
					binaryWriter.Write(Main.clientUUID);
					break;
				case 69:
					Netplay.GetSectionX((int)number2);
					Netplay.GetSectionY((int)number3);
					binaryWriter.Write((short)number);
					binaryWriter.Write((short)number2);
					binaryWriter.Write((short)number3);
					binaryWriter.Write(Main.chest[(int)((short)number)].name);
					break;
				case 70:
					binaryWriter.Write((short)number);
					binaryWriter.Write((byte)number2);
					break;
				case 71:
					binaryWriter.Write(number);
					binaryWriter.Write((int)number2);
					binaryWriter.Write((short)number3);
					binaryWriter.Write((byte)number4);
					break;
				case 72:
				{
					for (int num19 = 0; num19 < Main.TravelShopMaxSlots; num19++)
					{
						binaryWriter.Write((short)Main.travelShop[num19]);
					}
					break;
				}
				case 73:
					binaryWriter.Write((byte)number);
					break;
				case 74:
				{
					binaryWriter.Write((byte)Main.anglerQuest);
					bool flag2 = Main.anglerWhoFinishedToday.Contains(text.ToString());
					binaryWriter.Write(flag2);
					break;
				}
				case 76:
					binaryWriter.Write((byte)number);
					binaryWriter.Write(Main.player[number].anglerQuestsFinished);
					binaryWriter.Write(Main.player[number].golferScoreAccumulated);
					break;
				case 77:
					binaryWriter.Write((short)number);
					binaryWriter.Write((ushort)number2);
					binaryWriter.Write((short)number3);
					binaryWriter.Write((short)number4);
					break;
				case 78:
					binaryWriter.Write(number);
					binaryWriter.Write((int)number2);
					binaryWriter.Write((sbyte)number3);
					binaryWriter.Write((sbyte)number4);
					break;
				case 79:
					binaryWriter.Write((short)number);
					binaryWriter.Write((short)number2);
					binaryWriter.Write((short)number3);
					binaryWriter.Write((short)number4);
					binaryWriter.Write((byte)number5);
					binaryWriter.Write((sbyte)number6);
					binaryWriter.Write(number7 == 1);
					break;
				case 80:
					binaryWriter.Write((byte)number);
					binaryWriter.Write((short)number2);
					break;
				case 81:
					binaryWriter.Write(number2);
					binaryWriter.Write(number3);
					binaryWriter.WriteRGB(new Color
					{
						PackedValue = (uint)number
					});
					binaryWriter.Write((int)number4);
					break;
				case 84:
				{
					byte b4 = (byte)number;
					float stealth = Main.player[(int)b4].stealth;
					binaryWriter.Write(b4);
					binaryWriter.Write(stealth);
					break;
				}
				case 85:
					if (Main.netMode == 1)
					{
						QuickStacking.WriteNetInventorySlots(binaryWriter);
						binaryWriter.Write((byte)number);
					}
					else
					{
						QuickStacking.WriteBlockedChestList(binaryWriter);
					}
					break;
				case 86:
				{
					binaryWriter.Write(number);
					TileEntity tileEntity;
					bool flag3 = TileEntity.TryGet<TileEntity>(number, out tileEntity);
					binaryWriter.Write(flag3);
					if (flag3)
					{
						TileEntity.Write(binaryWriter, tileEntity, true);
					}
					break;
				}
				case 87:
					binaryWriter.Write((short)number);
					binaryWriter.Write((short)number2);
					binaryWriter.Write((byte)number3);
					break;
				case 88:
				{
					BitsByte bitsByte30 = (byte)number2;
					BitsByte bitsByte31 = (byte)number3;
					binaryWriter.Write((short)number);
					binaryWriter.Write(bitsByte30);
					WorldItem worldItem3 = Main.item[number];
					if (bitsByte30[0])
					{
						binaryWriter.Write(worldItem3.color.PackedValue);
					}
					if (bitsByte30[1])
					{
						binaryWriter.Write((ushort)worldItem3.damage);
					}
					if (bitsByte30[2])
					{
						binaryWriter.Write(worldItem3.knockBack);
					}
					if (bitsByte30[3])
					{
						binaryWriter.Write((ushort)worldItem3.useAnimation);
					}
					if (bitsByte30[4])
					{
						binaryWriter.Write((ushort)worldItem3.useTime);
					}
					if (bitsByte30[5])
					{
						binaryWriter.Write((short)worldItem3.shoot);
					}
					if (bitsByte30[6])
					{
						binaryWriter.Write(worldItem3.shootSpeed);
					}
					if (bitsByte30[7])
					{
						binaryWriter.Write(bitsByte31);
						if (bitsByte31[0])
						{
							binaryWriter.Write((ushort)worldItem3.width);
						}
						if (bitsByte31[1])
						{
							binaryWriter.Write((ushort)worldItem3.height);
						}
						if (bitsByte31[2])
						{
							binaryWriter.Write(worldItem3.scale);
						}
						if (bitsByte31[3])
						{
							binaryWriter.Write((short)worldItem3.ammo);
						}
						if (bitsByte31[4])
						{
							binaryWriter.Write((short)worldItem3.useAmmo);
						}
						if (bitsByte31[5])
						{
							binaryWriter.Write(worldItem3.notAmmo);
						}
					}
					break;
				}
				case 89:
				{
					binaryWriter.Write((short)number);
					binaryWriter.Write((short)number2);
					Item item3 = Main.player[(int)number4].inventory[(int)number3];
					binaryWriter.Write((short)item3.type);
					binaryWriter.Write(item3.prefix);
					binaryWriter.Write((short)number5);
					break;
				}
				case 91:
					binaryWriter.Write(number);
					binaryWriter.Write((byte)number2);
					if (number2 != 255f)
					{
						binaryWriter.Write((ushort)number3);
						binaryWriter.Write((ushort)number4);
						binaryWriter.Write((byte)number5);
						if (number5 < 0)
						{
							binaryWriter.Write((short)number6);
						}
					}
					break;
				case 92:
					binaryWriter.Write((short)number);
					binaryWriter.Write((int)number2);
					binaryWriter.Write(number3);
					binaryWriter.Write(number4);
					break;
				case 95:
					binaryWriter.Write((ushort)number);
					binaryWriter.Write((byte)number2);
					break;
				case 96:
				{
					binaryWriter.Write((byte)number);
					Player player6 = Main.player[number];
					binaryWriter.Write((short)number4);
					binaryWriter.Write(number2);
					binaryWriter.Write(number3);
					binaryWriter.WriteVector2(player6.velocity);
					break;
				}
				case 97:
					binaryWriter.Write((short)number);
					break;
				case 98:
					binaryWriter.Write((short)number);
					break;
				case 99:
					binaryWriter.Write((byte)number);
					binaryWriter.WriteVector2(Main.player[number].MinionRestTargetPoint);
					break;
				case 100:
				{
					binaryWriter.Write((ushort)number);
					NPC npc3 = Main.npc[number];
					binaryWriter.Write((short)number4);
					binaryWriter.Write(number2);
					binaryWriter.Write(number3);
					binaryWriter.WriteVector2(npc3.velocity);
					break;
				}
				case 101:
					binaryWriter.Write((ushort)NPC.ShieldStrengthTowerSolar);
					binaryWriter.Write((ushort)NPC.ShieldStrengthTowerVortex);
					binaryWriter.Write((ushort)NPC.ShieldStrengthTowerNebula);
					binaryWriter.Write((ushort)NPC.ShieldStrengthTowerStardust);
					break;
				case 102:
					binaryWriter.Write((byte)number);
					binaryWriter.Write((ushort)number2);
					binaryWriter.Write(number3);
					binaryWriter.Write(number4);
					break;
				case 103:
					binaryWriter.Write(NPC.MaxMoonLordCountdown);
					binaryWriter.Write(NPC.MoonLordCountdown);
					break;
				case 104:
					binaryWriter.Write((byte)number);
					binaryWriter.Write((short)number2);
					binaryWriter.Write(((short)number3 < 0) ? 0f : number3);
					binaryWriter.Write((byte)number4);
					binaryWriter.Write(number5);
					binaryWriter.Write((byte)number6);
					break;
				case 105:
					binaryWriter.Write((short)number);
					binaryWriter.Write((short)number2);
					binaryWriter.Write(number3 == 1f);
					break;
				case 106:
				{
					HalfVector2 halfVector = new HalfVector2((float)number, number2);
					binaryWriter.Write(halfVector.PackedValue);
					break;
				}
				case 107:
					binaryWriter.Write((byte)number2);
					binaryWriter.Write((byte)number3);
					binaryWriter.Write((byte)number4);
					text.Serialize(binaryWriter);
					binaryWriter.Write((short)number5);
					break;
				case 108:
					binaryWriter.Write((short)number);
					binaryWriter.Write(number2);
					binaryWriter.Write((short)number3);
					binaryWriter.Write((short)number4);
					binaryWriter.Write((short)number5);
					binaryWriter.Write((short)number6);
					binaryWriter.Write((byte)number7);
					break;
				case 109:
					binaryWriter.Write((short)number);
					binaryWriter.Write((short)number2);
					binaryWriter.Write((short)number3);
					binaryWriter.Write((short)number4);
					binaryWriter.Write((byte)number5);
					break;
				case 110:
					binaryWriter.Write((short)number);
					binaryWriter.Write((short)number2);
					binaryWriter.Write((byte)number3);
					break;
				case 112:
					binaryWriter.Write((byte)number);
					binaryWriter.Write((int)number2);
					binaryWriter.Write((int)number3);
					binaryWriter.Write((byte)number4);
					binaryWriter.Write((short)number5);
					binaryWriter.Write((byte)number6);
					break;
				case 113:
					binaryWriter.Write((short)number);
					binaryWriter.Write((short)number2);
					break;
				case 115:
					binaryWriter.Write((byte)number);
					binaryWriter.Write((short)Main.player[number].MinionAttackTargetNPC);
					break;
				case 116:
					binaryWriter.Write(number);
					break;
				case 117:
					binaryWriter.Write((byte)number);
					NetMessage._currentPlayerDeathReason.WriteSelfTo(binaryWriter);
					binaryWriter.Write((short)number2);
					binaryWriter.Write((byte)(number3 + 1f));
					binaryWriter.Write((byte)number4);
					binaryWriter.Write((sbyte)number5);
					break;
				case 118:
					binaryWriter.Write((byte)number);
					NetMessage._currentPlayerDeathReason.WriteSelfTo(binaryWriter);
					binaryWriter.Write((short)number2);
					binaryWriter.Write((byte)(number3 + 1f));
					binaryWriter.Write((byte)number4);
					break;
				case 119:
					binaryWriter.Write(number2);
					binaryWriter.Write(number3);
					binaryWriter.WriteRGB(new Color
					{
						PackedValue = (uint)number
					});
					text.Serialize(binaryWriter);
					break;
				case 120:
					binaryWriter.Write((byte)number);
					binaryWriter.Write((byte)number2);
					break;
				case 121:
				{
					int num20 = (int)number3;
					binaryWriter.Write((byte)number);
					binaryWriter.Write((int)number2);
					binaryWriter.Write((byte)num20);
					binaryWriter.Write((byte)number4);
					TEDisplayDoll tedisplayDoll;
					if (TileEntity.TryGet<TEDisplayDoll>((int)number2, out tedisplayDoll))
					{
						tedisplayDoll.WriteData((int)number3, (int)number4, binaryWriter);
					}
					else
					{
						TEDisplayDoll.WriteDummySync((int)number3, (int)number4, binaryWriter);
					}
					break;
				}
				case 122:
					binaryWriter.Write(number);
					binaryWriter.Write((byte)number2);
					break;
				case 123:
				{
					binaryWriter.Write((short)number);
					binaryWriter.Write((short)number2);
					Item item4 = Main.player[(int)number4].inventory[(int)number3];
					binaryWriter.Write((short)item4.type);
					binaryWriter.Write(item4.prefix);
					binaryWriter.Write((short)number5);
					break;
				}
				case 124:
				{
					int num21 = (int)number3;
					bool flag4 = number4 == 1f;
					if (flag4)
					{
						num21 += 2;
					}
					binaryWriter.Write((byte)number);
					binaryWriter.Write((int)number2);
					binaryWriter.Write((byte)num21);
					TEHatRack tehatRack;
					if (TileEntity.TryGet<TEHatRack>((int)number2, out tehatRack))
					{
						tehatRack.WriteItem((int)number3, binaryWriter, flag4);
					}
					else
					{
						binaryWriter.Write(0);
						binaryWriter.Write(0);
					}
					break;
				}
				case 125:
					binaryWriter.Write((byte)number);
					binaryWriter.Write((short)number2);
					binaryWriter.Write((short)number3);
					binaryWriter.Write((byte)number4);
					break;
				case 126:
					NetMessage._currentRevengeMarker.WriteSelfTo(binaryWriter);
					break;
				case 127:
					binaryWriter.Write(number);
					break;
				case 128:
					binaryWriter.Write((byte)number);
					binaryWriter.Write((ushort)number5);
					binaryWriter.Write((ushort)number6);
					binaryWriter.Write((ushort)number2);
					binaryWriter.Write((ushort)number3);
					break;
				case 130:
					binaryWriter.Write((ushort)number);
					binaryWriter.Write((ushort)number2);
					binaryWriter.Write((short)number3);
					break;
				case 131:
				{
					binaryWriter.Write((ushort)number);
					binaryWriter.Write((byte)number2);
					byte b5 = (byte)number2;
					if (b5 == 1)
					{
						binaryWriter.Write((int)number3);
						binaryWriter.Write((short)number4);
					}
					break;
				}
				case 132:
					NetMessage._currentNetSoundInfo.WriteSelfTo(binaryWriter);
					break;
				case 133:
				{
					binaryWriter.Write((short)number);
					binaryWriter.Write((short)number2);
					Item item5 = Main.player[(int)number4].inventory[(int)number3];
					binaryWriter.Write((short)item5.type);
					binaryWriter.Write(item5.prefix);
					binaryWriter.Write((short)number5);
					break;
				}
				case 134:
				{
					binaryWriter.Write((byte)number);
					Player player7 = Main.player[number];
					binaryWriter.Write(player7.ladyBugLuckTimeLeft);
					binaryWriter.Write(player7.torchLuck);
					binaryWriter.Write(player7.luckPotion);
					binaryWriter.Write(player7.HasGardenGnomeNearby);
					binaryWriter.Write(player7.brokenMirrorBadLuck);
					binaryWriter.Write(player7.equipmentBasedLuckBonus);
					binaryWriter.Write(player7.coinLuck);
					binaryWriter.Write(player7.kiteLuckLevel);
					break;
				}
				case 135:
					binaryWriter.Write((byte)number);
					break;
				case 136:
				{
					for (int num22 = 0; num22 < 2; num22++)
					{
						for (int num23 = 0; num23 < 3; num23++)
						{
							binaryWriter.Write((ushort)NPC.cavernMonsterType[num22, num23]);
						}
					}
					break;
				}
				case 137:
					binaryWriter.Write((short)number);
					binaryWriter.Write((ushort)number2);
					break;
				case 139:
				{
					binaryWriter.Write((byte)number);
					bool flag5 = number2 == 1f;
					binaryWriter.Write(flag5);
					break;
				}
				case 140:
					binaryWriter.Write((byte)number);
					binaryWriter.Write((int)number2);
					break;
				case 141:
					binaryWriter.Write((byte)number);
					binaryWriter.Write((byte)number2);
					binaryWriter.Write(number3);
					binaryWriter.Write(number4);
					binaryWriter.Write(number5);
					binaryWriter.Write(number6);
					break;
				case 142:
				{
					binaryWriter.Write((byte)number);
					Player player8 = Main.player[number];
					player8.piggyBankProjTracker.Write(binaryWriter);
					player8.voidLensChest.Write(binaryWriter);
					break;
				}
				case 146:
					binaryWriter.Write((byte)number);
					if (number == 0)
					{
						binaryWriter.WriteVector2(new Vector2((float)((int)number2), (float)((int)number3)));
					}
					else if (number == 1)
					{
						binaryWriter.WriteVector2(new Vector2((float)((int)number2), (float)((int)number3)));
						binaryWriter.Write((int)number4);
					}
					break;
				case 147:
					binaryWriter.Write((byte)number);
					binaryWriter.Write((byte)number2);
					NetMessage.WriteAccessoryVisibility(binaryWriter, Main.player[number].hideVisibleAccessory);
					break;
				case 149:
				{
					binaryWriter.Write((short)number);
					binaryWriter.Write((short)number2);
					Item item6 = Main.player[(int)number4].inventory[(int)number3];
					binaryWriter.Write((short)item6.type);
					binaryWriter.Write(item6.prefix);
					binaryWriter.Write((short)number5);
					break;
				}
				case 150:
					binaryWriter.Write((byte)number);
					binaryWriter.Write((short)number2);
					break;
				case 151:
					Main.item[number].playerIndexTheItemIsReservedFor = 255;
					binaryWriter.Write((short)number);
					break;
				case 152:
					binaryWriter.Write((byte)number);
					break;
				case 153:
					binaryWriter.Write((byte)number);
					binaryWriter.Write((short)number2);
					break;
				case 155:
					binaryWriter.Write((short)number);
					binaryWriter.Write((short)number2);
					break;
				case 156:
					binaryWriter.Write((short)number);
					binaryWriter.Write((short)number2);
					binaryWriter.Write((short)number3);
					break;
				case 158:
					binaryWriter.Write((byte)number);
					break;
				case 159:
					binaryWriter.Write((short)number);
					binaryWriter.Write((short)number2);
					break;
				case 160:
				{
					WorldItem worldItem4 = Main.item[number];
					binaryWriter.Write((short)number);
					binaryWriter.WriteVector2(worldItem4.position);
					break;
				}
				case 161:
					binaryWriter.Write(text.ToString());
					break;
				}
				int num24 = (int)binaryWriter.BaseStream.Position;
				if (num24 > 65535)
				{
					throw new Exception(string.Concat(new object[] { "Maximum packet length exceeded. id: ", msgType, " length: ", num24 }));
				}
				binaryWriter.BaseStream.Position = position;
				binaryWriter.Write((ushort)num24);
				binaryWriter.BaseStream.Position = (long)num24;
				if (Main.netMode == 1)
				{
					if (Netplay.Connection.IsConnected())
					{
						NetMessage.SendPacketToServer(NetMessage.buffer[num].writeBuffer);
					}
				}
				else if (remoteClient == -1)
				{
					if (msgType == 34 || msgType == 69)
					{
						for (int num25 = 0; num25 < 256; num25++)
						{
							if (num25 != ignoreClient && NetMessage.buffer[num25].broadcast && Netplay.Clients[num25].IsConnected())
							{
								NetMessage.SendPacket(NetMessage.buffer[num].writeBuffer, num25);
							}
						}
					}
					else if (msgType == 20)
					{
						for (int num26 = 0; num26 < 256; num26++)
						{
							if (num26 != ignoreClient && NetMessage.buffer[num26].broadcast && Netplay.Clients[num26].IsConnected() && Netplay.Clients[num26].SectionRange((int)Math.Max(number3, number4), number, (int)number2))
							{
								NetMessage.SendPacket(NetMessage.buffer[num].writeBuffer, num26);
							}
						}
					}
					else if (msgType == 23)
					{
						NPC npc4 = Main.npc[number];
						bool flag6 = npc4.boss || npc4.netAlways || npc4.townNPC || !npc4.active || npc4.life <= 0 || npc4.spawnNeedsSyncing;
						if (flag6)
						{
							npc4.spawnNeedsSyncing = false;
							npc4.netStream = 0;
							npc4.netUpdate = false;
							npc4.netUpdatePendingSpamCooldown = false;
							npc4.netUpdatePendingFullSpamCooldown = false;
							Array.Clear(npc4.playerNetSyncState, 0, npc4.playerNetSyncState.Length);
						}
						for (int num27 = 0; num27 < 256; num27++)
						{
							if (num27 != ignoreClient && NetMessage.buffer[num27].broadcast && Netplay.Clients[num27].IsConnected())
							{
								if (!flag6)
								{
									if (npc4.playerNetSyncState[num27].skippedSyncs < 4 && !Netplay.Clients[num27].IsSectionActive(npc4.NetSectionCoordinates))
									{
										NPC.PlayerNetSyncState[] playerNetSyncState = npc4.playerNetSyncState;
										int num28 = num27;
										playerNetSyncState[num28].skippedSyncs = playerNetSyncState[num28].skippedSyncs + 1;
										goto IL_3526;
									}
									npc4.playerNetSyncState[num27] = default(NPC.PlayerNetSyncState);
								}
								NetMessage.SendPacket(NetMessage.buffer[num].writeBuffer, num27);
							}
							IL_3526:;
						}
					}
					else if (msgType == 28)
					{
						NPC npc5 = Main.npc[number];
						for (int num29 = 0; num29 < 256; num29++)
						{
							if (num29 != ignoreClient && NetMessage.buffer[num29].broadcast && Netplay.Clients[num29].IsConnected() && (npc5.life <= 0 || Netplay.Clients[num29].IsSectionActive(npc5.NetSectionCoordinates)))
							{
								NetMessage.SendPacket(NetMessage.buffer[num].writeBuffer, num29);
							}
						}
					}
					else if (msgType == 13)
					{
						for (int num30 = 0; num30 < 256; num30++)
						{
							if (num30 != ignoreClient && NetMessage.buffer[num30].broadcast && Netplay.Clients[num30].IsConnected())
							{
								NetMessage.SendPacket(NetMessage.buffer[num].writeBuffer, num30);
							}
						}
					}
					else if (msgType == 27)
					{
						Projectile projectile2 = Main.projectile[number];
						bool flag7 = projectile2.type == 12 || Main.projPet[projectile2.type] || projectile2.aiStyle == 11 || projectile2.netImportant;
						if (flag7)
						{
							Array.Clear(projectile2.netSyncSkippedForPlayer, 0, projectile2.netSyncSkippedForPlayer.Length);
						}
						for (int num31 = 0; num31 < 256; num31++)
						{
							if (num31 != ignoreClient && NetMessage.buffer[num31].broadcast && Netplay.Clients[num31].IsConnected())
							{
								if (!flag7)
								{
									if (!Netplay.Clients[num31].IsSectionActive(projectile2.NetSectionCoordinates))
									{
										projectile2.netSyncSkippedForPlayer[num31] = true;
										goto IL_36DC;
									}
									projectile2.netSyncSkippedForPlayer[num31] = false;
								}
								NetMessage.SendPacket(NetMessage.buffer[num].writeBuffer, num31);
							}
							IL_36DC:;
						}
					}
					else
					{
						for (int num32 = 0; num32 < 256; num32++)
						{
							if (num32 != ignoreClient && (NetMessage.buffer[num32].broadcast || (Netplay.Clients[num32].State >= 3 && msgType == 10)) && Netplay.Clients[num32].IsConnected())
							{
								NetMessage.SendPacket(NetMessage.buffer[num].writeBuffer, num32);
							}
						}
					}
				}
				else if (Netplay.Clients[remoteClient].IsConnected())
				{
					if (msgType == 23)
					{
						Main.npc[number].playerNetSyncState[remoteClient] = default(NPC.PlayerNetSyncState);
					}
					else if (msgType == 27)
					{
						Main.projectile[number].netSyncSkippedForPlayer[remoteClient] = false;
					}
					NetMessage.SendPacket(NetMessage.buffer[num].writeBuffer, remoteClient);
				}
				if (Main.verboseNetplay)
				{
					for (int num33 = 0; num33 < num24; num33++)
					{
					}
					for (int num34 = 0; num34 < num24; num34++)
					{
						byte b6 = NetMessage.buffer[num].writeBuffer[num34];
					}
				}
				NetMessage.buffer[num].writeLocked = false;
				if (msgType == 2 && Main.netMode == 2)
				{
					Netplay.Clients[num].PendingTermination = true;
				}
			}
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0003FC8C File Offset: 0x0003DE8C
		private static void SendPacketToServer(byte[] data)
		{
			NetMessage.SendPacket(data, 256);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0003FC9C File Offset: 0x0003DE9C
		private static void SendPacket(byte[] data, int remoteClient)
		{
			try
			{
				ushort num = BitConverter.ToUInt16(data, 0);
				byte b = data[2];
				NetMessage.buffer[remoteClient].spamCount++;
				Main.ActiveNetDiagnosticsUI.CountSentMessage((int)b, (int)num);
				if (!Main.dedServ)
				{
					Netplay.Connection.Socket.AsyncSend(data, 0, (int)num, new SocketSendCallback(Netplay.Connection.ClientWriteCallBack), null);
				}
				else
				{
					Netplay.Clients[remoteClient].Socket.AsyncSend(data, 0, (int)num, new SocketSendCallback(Netplay.Clients[remoteClient].ServerWriteCallBack), null);
				}
			}
			catch
			{
				bool dedServ = Main.dedServ;
			}
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0003FD44 File Offset: 0x0003DF44
		public static void SendChestContentsTo(int chest, int targetPlayer)
		{
			NetMessage.TrySendData(155, targetPlayer, -1, null, chest, (float)Main.chest[chest].maxItems, 0f, 0f, 0, 0, 0);
			for (int i = 0; i < Main.chest[chest].maxItems; i++)
			{
				NetMessage.TrySendData(32, targetPlayer, -1, null, chest, (float)i, 0f, 0f, 0, 0, 0);
			}
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0003FDAC File Offset: 0x0003DFAC
		private static void WriteAccessoryVisibility(BinaryWriter writer, bool[] hideVisibleAccessory)
		{
			ushort num = 0;
			for (int i = 0; i < hideVisibleAccessory.Length; i++)
			{
				if (hideVisibleAccessory[i])
				{
					num |= (ushort)(1 << i);
				}
			}
			writer.Write(num);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0003FDE0 File Offset: 0x0003DFE0
		public static void CompressTileBlock(int xStart, int yStart, short width, short height, Stream stream)
		{
			using (DeflateStream deflateStream = new DeflateStream(stream, 0, true))
			{
				BinaryWriter binaryWriter = new BinaryWriter(deflateStream);
				binaryWriter.Write(xStart);
				binaryWriter.Write(yStart);
				binaryWriter.Write(width);
				binaryWriter.Write(height);
				NetMessage.CompressTileBlock_Inner(binaryWriter, xStart, yStart, (int)width, (int)height);
			}
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0003FE40 File Offset: 0x0003E040
		public static void CompressTileBlock_Inner(BinaryWriter writer, int xStart, int yStart, int width, int height)
		{
			short num = 0;
			short num2 = 0;
			short num3 = 0;
			short num4 = 0;
			int num5 = 0;
			int num6 = 0;
			byte b = 0;
			byte[] array = new byte[16];
			Tile tile = null;
			for (int i = yStart; i < yStart + height; i++)
			{
				for (int j = xStart; j < xStart + width; j++)
				{
					Tile tile2 = Main.tile[j, i];
					if (tile2.isTheSameAs(tile) && TileID.Sets.AllowsSaveCompressionBatching[(int)tile2.type])
					{
						num4 += 1;
					}
					else
					{
						if (tile != null)
						{
							if (num4 > 0)
							{
								array[num5] = (byte)(num4 & 255);
								num5++;
								if (num4 > 255)
								{
									b |= 128;
									array[num5] = (byte)(((int)num4 & 65280) >> 8);
									num5++;
								}
								else
								{
									b |= 64;
								}
							}
							array[num6] = b;
							writer.Write(array, num6, num5 - num6);
							num4 = 0;
						}
						num5 = 4;
						byte b4;
						byte b3;
						byte b2 = (b = (b3 = (b4 = 0)));
						if (tile2.active())
						{
							b |= 2;
							array[num5] = (byte)tile2.type;
							num5++;
							if (tile2.type > 255)
							{
								array[num5] = (byte)(tile2.type >> 8);
								num5++;
								b |= 32;
							}
							if (TileID.Sets.BasicChest[(int)tile2.type] && tile2.frameX % 36 == 0 && tile2.frameY % 36 == 0)
							{
								short num7 = (short)Chest.FindChest(j, i);
								if (num7 != -1)
								{
									NetMessage._compressChestList[(int)num] = num7;
									num += 1;
								}
							}
							if (tile2.type == 88 && tile2.frameX % 54 == 0 && tile2.frameY % 36 == 0)
							{
								short num8 = (short)Chest.FindChest(j, i);
								if (num8 != -1)
								{
									NetMessage._compressChestList[(int)num] = num8;
									num += 1;
								}
							}
							if (tile2.type == 85 && tile2.frameX % 36 == 0 && tile2.frameY % 36 == 0)
							{
								short num9 = (short)Sign.ReadSign(j, i, true);
								if (num9 != -1)
								{
									short[] compressSignList = NetMessage._compressSignList;
									short num10 = num2;
									num2 = num10 + 1;
									compressSignList[(int)num10] = num9;
								}
							}
							if (tile2.type == 55 && tile2.frameX % 36 == 0 && tile2.frameY % 36 == 0)
							{
								short num11 = (short)Sign.ReadSign(j, i, true);
								if (num11 != -1)
								{
									short[] compressSignList2 = NetMessage._compressSignList;
									short num12 = num2;
									num2 = num12 + 1;
									compressSignList2[(int)num12] = num11;
								}
							}
							if (tile2.type == 425 && tile2.frameX % 36 == 0 && tile2.frameY % 36 == 0)
							{
								short num13 = (short)Sign.ReadSign(j, i, true);
								if (num13 != -1)
								{
									short[] compressSignList3 = NetMessage._compressSignList;
									short num14 = num2;
									num2 = num14 + 1;
									compressSignList3[(int)num14] = num13;
								}
							}
							if (tile2.type == 573 && tile2.frameX % 36 == 0 && tile2.frameY % 36 == 0)
							{
								short num15 = (short)Sign.ReadSign(j, i, true);
								if (num15 != -1)
								{
									short[] compressSignList4 = NetMessage._compressSignList;
									short num16 = num2;
									num2 = num16 + 1;
									compressSignList4[(int)num16] = num15;
								}
							}
							if (tile2.type == 378 && tile2.frameX % 36 == 0 && tile2.frameY == 0)
							{
								int num17 = TileEntityType<TETrainingDummy>.Find(j, i);
								if (num17 != -1)
								{
									short[] compressEntities = NetMessage._compressEntities;
									short num18 = num3;
									num3 = num18 + 1;
									compressEntities[(int)num18] = (short)num17;
								}
							}
							if (tile2.type == 395 && tile2.frameX % 36 == 0 && tile2.frameY == 0)
							{
								int num19 = TileEntityType<TEItemFrame>.Find(j, i);
								if (num19 != -1)
								{
									short[] compressEntities2 = NetMessage._compressEntities;
									short num20 = num3;
									num3 = num20 + 1;
									compressEntities2[(int)num20] = (short)num19;
								}
							}
							if (tile2.type == 698 && tile2.frameX % 18 == 0 && tile2.frameY == 0)
							{
								int num21 = TileEntityType<TEDeadCellsDisplayJar>.Find(j, i);
								if (num21 != -1)
								{
									short[] compressEntities3 = NetMessage._compressEntities;
									short num22 = num3;
									num3 = num22 + 1;
									compressEntities3[(int)num22] = (short)num21;
								}
							}
							if (tile2.type == 520 && tile2.frameX % 18 == 0 && tile2.frameY == 0)
							{
								int num23 = TileEntityType<TEFoodPlatter>.Find(j, i);
								if (num23 != -1)
								{
									short[] compressEntities4 = NetMessage._compressEntities;
									short num24 = num3;
									num3 = num24 + 1;
									compressEntities4[(int)num24] = (short)num23;
								}
							}
							if (tile2.type == 471 && tile2.frameX % 54 == 0 && tile2.frameY == 0)
							{
								int num25 = TileEntityType<TEWeaponsRack>.Find(j, i);
								if (num25 != -1)
								{
									short[] compressEntities5 = NetMessage._compressEntities;
									short num26 = num3;
									num3 = num26 + 1;
									compressEntities5[(int)num26] = (short)num25;
								}
							}
							if (tile2.type == 470 && tile2.frameX % 36 == 0 && tile2.frameY == 0)
							{
								int num27 = TileEntityType<TEDisplayDoll>.Find(j, i);
								if (num27 != -1)
								{
									short[] compressEntities6 = NetMessage._compressEntities;
									short num28 = num3;
									num3 = num28 + 1;
									compressEntities6[(int)num28] = (short)num27;
								}
							}
							if (tile2.type == 475 && tile2.frameX % 54 == 0 && tile2.frameY == 0)
							{
								int num29 = TileEntityType<TEHatRack>.Find(j, i);
								if (num29 != -1)
								{
									short[] compressEntities7 = NetMessage._compressEntities;
									short num30 = num3;
									num3 = num30 + 1;
									compressEntities7[(int)num30] = (short)num29;
								}
							}
							if (tile2.type == 597 && tile2.frameX % 54 == 0 && tile2.frameY % 72 == 0)
							{
								int num31 = TileEntityType<TETeleportationPylon>.Find(j, i);
								if (num31 != -1)
								{
									short[] compressEntities8 = NetMessage._compressEntities;
									short num32 = num3;
									num3 = num32 + 1;
									compressEntities8[(int)num32] = (short)num31;
								}
							}
							if (Main.tileFrameImportant[(int)tile2.type])
							{
								array[num5] = (byte)(tile2.frameX & 255);
								num5++;
								array[num5] = (byte)(((int)tile2.frameX & 65280) >> 8);
								num5++;
								array[num5] = (byte)(tile2.frameY & 255);
								num5++;
								array[num5] = (byte)(((int)tile2.frameY & 65280) >> 8);
								num5++;
							}
							if (tile2.color() != 0)
							{
								b3 |= 8;
								array[num5] = tile2.color();
								num5++;
							}
						}
						if (tile2.wall != 0)
						{
							b |= 4;
							array[num5] = (byte)tile2.wall;
							num5++;
							if (tile2.wallColor() != 0)
							{
								b3 |= 16;
								array[num5] = tile2.wallColor();
								num5++;
							}
						}
						if (tile2.liquid != 0)
						{
							if (tile2.shimmer())
							{
								b3 |= 128;
								b |= 8;
							}
							else if (tile2.lava())
							{
								b |= 16;
							}
							else if (tile2.honey())
							{
								b |= 24;
							}
							else
							{
								b |= 8;
							}
							array[num5] = tile2.liquid;
							num5++;
						}
						if (tile2.wire())
						{
							b2 |= 2;
						}
						if (tile2.wire2())
						{
							b2 |= 4;
						}
						if (tile2.wire3())
						{
							b2 |= 8;
						}
						int num33;
						if (tile2.halfBrick())
						{
							num33 = 16;
						}
						else if (tile2.slope() != 0)
						{
							num33 = (int)(tile2.slope() + 1) << 4;
						}
						else
						{
							num33 = 0;
						}
						b2 |= (byte)num33;
						if (tile2.actuator())
						{
							b3 |= 2;
						}
						if (tile2.inActive())
						{
							b3 |= 4;
						}
						if (tile2.wire4())
						{
							b3 |= 32;
						}
						if (tile2.wall > 255)
						{
							array[num5] = (byte)(tile2.wall >> 8);
							num5++;
							b3 |= 64;
						}
						if (tile2.invisibleBlock())
						{
							b4 |= 2;
						}
						if (tile2.invisibleWall())
						{
							b4 |= 4;
						}
						if (tile2.fullbrightBlock())
						{
							b4 |= 8;
						}
						if (tile2.fullbrightWall())
						{
							b4 |= 16;
						}
						num6 = 3;
						if (b4 != 0)
						{
							b3 |= 1;
							array[num6] = b4;
							num6--;
						}
						if (b3 != 0)
						{
							b2 |= 1;
							array[num6] = b3;
							num6--;
						}
						if (b2 != 0)
						{
							b |= 1;
							array[num6] = b2;
							num6--;
						}
						tile = tile2;
					}
				}
			}
			if (num4 > 0)
			{
				array[num5] = (byte)(num4 & 255);
				num5++;
				if (num4 > 255)
				{
					b |= 128;
					array[num5] = (byte)(((int)num4 & 65280) >> 8);
					num5++;
				}
				else
				{
					b |= 64;
				}
			}
			array[num6] = b;
			writer.Write(array, num6, num5 - num6);
			writer.Write(num);
			for (int k = 0; k < (int)num; k++)
			{
				Chest chest = Main.chest[(int)NetMessage._compressChestList[k]];
				writer.Write(NetMessage._compressChestList[k]);
				writer.Write((short)chest.x);
				writer.Write((short)chest.y);
				writer.Write(chest.name);
			}
			writer.Write(num2);
			for (int l = 0; l < (int)num2; l++)
			{
				Sign sign = Main.sign[(int)NetMessage._compressSignList[l]];
				writer.Write(NetMessage._compressSignList[l]);
				writer.Write((short)sign.x);
				writer.Write((short)sign.y);
				writer.Write(sign.text);
			}
			writer.Write(num3);
			for (int m = 0; m < (int)num3; m++)
			{
				TileEntity.Write(writer, TileEntity.ByID[(int)NetMessage._compressEntities[m]], false);
			}
		}

		// Token: 0x060002AF RID: 687 RVA: 0x000406DC File Offset: 0x0003E8DC
		public static void DecompressTileBlock(Stream stream)
		{
			using (DeflateStream deflateStream = new DeflateStream(stream, 1, true))
			{
				BinaryReader binaryReader = new BinaryReader(deflateStream);
				NetMessage.DecompressTileBlock_Inner(binaryReader, binaryReader.ReadInt32(), binaryReader.ReadInt32(), (int)binaryReader.ReadInt16(), (int)binaryReader.ReadInt16());
			}
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00040734 File Offset: 0x0003E934
		public static void DecompressTileBlock_Inner(BinaryReader reader, int xStart, int yStart, int width, int height)
		{
			Tile tile = null;
			int num = 0;
			for (int i = yStart; i < yStart + height; i++)
			{
				for (int j = xStart; j < xStart + width; j++)
				{
					if (num != 0)
					{
						num--;
						if (Main.tile[j, i] == null)
						{
							Main.tile[j, i] = new Tile(tile);
						}
						else
						{
							Main.tile[j, i].CopyFrom(tile);
						}
					}
					else
					{
						byte b3;
						byte b2;
						byte b = (b2 = (b3 = 0));
						tile = Main.tile[j, i];
						if (tile == null)
						{
							tile = new Tile();
							Main.tile[j, i] = tile;
						}
						else
						{
							tile.ClearEverything();
						}
						byte b4 = reader.ReadByte();
						bool flag = false;
						if ((b4 & 1) == 1)
						{
							flag = true;
							b2 = reader.ReadByte();
						}
						bool flag2 = false;
						if (flag && (b2 & 1) == 1)
						{
							flag2 = true;
							b = reader.ReadByte();
						}
						if (flag2 && (b & 1) == 1)
						{
							b3 = reader.ReadByte();
						}
						bool flag3 = tile.active();
						byte b5;
						if ((b4 & 2) == 2)
						{
							tile.active(true);
							ushort type = tile.type;
							int num2;
							if ((b4 & 32) == 32)
							{
								b5 = reader.ReadByte();
								num2 = (int)reader.ReadByte();
								num2 = (num2 << 8) | (int)b5;
							}
							else
							{
								num2 = (int)reader.ReadByte();
							}
							tile.type = (ushort)num2;
							if (Main.tileFrameImportant[num2])
							{
								tile.frameX = reader.ReadInt16();
								tile.frameY = reader.ReadInt16();
							}
							else if (!flag3 || tile.type != type)
							{
								tile.frameX = -1;
								tile.frameY = -1;
							}
							if ((b & 8) == 8)
							{
								tile.color(reader.ReadByte());
							}
						}
						if ((b4 & 4) == 4)
						{
							tile.wall = (ushort)reader.ReadByte();
							if ((b & 16) == 16)
							{
								tile.wallColor(reader.ReadByte());
							}
						}
						b5 = (byte)((b4 & 24) >> 3);
						if (b5 != 0)
						{
							tile.liquid = reader.ReadByte();
							if ((b & 128) == 128)
							{
								tile.shimmer(true);
							}
							else if (b5 > 1)
							{
								if (b5 == 2)
								{
									tile.lava(true);
								}
								else
								{
									tile.honey(true);
								}
							}
						}
						if (b2 > 1)
						{
							if ((b2 & 2) == 2)
							{
								tile.wire(true);
							}
							if ((b2 & 4) == 4)
							{
								tile.wire2(true);
							}
							if ((b2 & 8) == 8)
							{
								tile.wire3(true);
							}
							b5 = (byte)((b2 & 112) >> 4);
							if (b5 != 0 && Main.tileSolid[(int)tile.type])
							{
								if (b5 == 1)
								{
									tile.halfBrick(true);
								}
								else
								{
									tile.slope(b5 - 1);
								}
							}
						}
						if (b > 1)
						{
							if ((b & 2) == 2)
							{
								tile.actuator(true);
							}
							if ((b & 4) == 4)
							{
								tile.inActive(true);
							}
							if ((b & 32) == 32)
							{
								tile.wire4(true);
							}
							if ((b & 64) == 64)
							{
								b5 = reader.ReadByte();
								tile.wall = (ushort)(((int)b5 << 8) | (int)tile.wall);
							}
						}
						if (b3 > 1)
						{
							if ((b3 & 2) == 2)
							{
								tile.invisibleBlock(true);
							}
							if ((b3 & 4) == 4)
							{
								tile.invisibleWall(true);
							}
							if ((b3 & 8) == 8)
							{
								tile.fullbrightBlock(true);
							}
							if ((b3 & 16) == 16)
							{
								tile.fullbrightWall(true);
							}
						}
						b5 = (byte)((b4 & 192) >> 6);
						if (b5 == 0)
						{
							num = 0;
						}
						else if (b5 == 1)
						{
							num = (int)reader.ReadByte();
						}
						else
						{
							num = (int)reader.ReadInt16();
						}
					}
				}
			}
			short num3 = reader.ReadInt16();
			for (int k = 0; k < (int)num3; k++)
			{
				short num4 = reader.ReadInt16();
				short num5 = reader.ReadInt16();
				short num6 = reader.ReadInt16();
				string text = reader.ReadString();
				if (num4 >= 0 && num4 < 8000)
				{
					Chest.CreateWorldChest((int)num4, (int)num5, (int)num6).name = text;
				}
			}
			num3 = reader.ReadInt16();
			for (int l = 0; l < (int)num3; l++)
			{
				short num7 = reader.ReadInt16();
				short num8 = reader.ReadInt16();
				short num9 = reader.ReadInt16();
				string text2 = reader.ReadString();
				if (num7 >= 0 && num7 < 32000)
				{
					if (Main.sign[(int)num7] == null)
					{
						Main.sign[(int)num7] = new Sign();
					}
					Main.sign[(int)num7].text = text2;
					Main.sign[(int)num7].x = (int)num8;
					Main.sign[(int)num7].y = (int)num9;
				}
			}
			num3 = reader.ReadInt16();
			for (int m = 0; m < (int)num3; m++)
			{
				TileEntity.Add(TileEntity.Read(reader, 319, false));
			}
			MapUpdateQueue.Add(new Rectangle(xStart, yStart, width, height));
			Main.sectionManager.SetTilesLoaded(xStart, yStart, xStart + width - 1, yStart + height - 1);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00040BA0 File Offset: 0x0003EDA0
		public static void ReceiveBytes(byte[] bytes, int streamLength, int i = 256)
		{
			MessageBuffer messageBuffer = NetMessage.buffer[i];
			lock (messageBuffer)
			{
				try
				{
					Buffer.BlockCopy(bytes, 0, NetMessage.buffer[i].readBuffer, NetMessage.buffer[i].totalData, streamLength);
					NetMessage.buffer[i].totalData += streamLength;
					NetMessage.buffer[i].checkBytes = true;
				}
				catch
				{
					if (Main.netMode == 1)
					{
						Main.menuMode = 15;
						Main.statusText = Language.GetTextValue("Error.BadHeaderBufferOverflow");
						Netplay.Disconnect = true;
					}
					else
					{
						Netplay.Clients[i].PendingTermination = true;
					}
				}
			}
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00040C60 File Offset: 0x0003EE60
		public static void CheckBytes(int bufferIndex = 256)
		{
			if (Main.dedServ && Netplay.Clients[bufferIndex].PendingTermination)
			{
				Netplay.Clients[bufferIndex].PendingTerminationApproved = true;
				return;
			}
			if (!Main.dedServ && !Netplay.Connection.IsConnected() && !Netplay.Connection.IsReading && !NetMessage.buffer[bufferIndex].checkBytes)
			{
				Netplay.Disconnect = true;
				Main.statusText = Language.GetTextValue("Net.LostConnection");
			}
			if (!NetMessage.buffer[bufferIndex].checkBytes)
			{
				return;
			}
			MessageBuffer messageBuffer = NetMessage.buffer[bufferIndex];
			lock (messageBuffer)
			{
				NetMessage.buffer[bufferIndex].checkBytes = false;
				int num = 0;
				int i = NetMessage.buffer[bufferIndex].totalData;
				try
				{
					while (i >= 2)
					{
						int num2 = (int)BitConverter.ToUInt16(NetMessage.buffer[bufferIndex].readBuffer, num);
						if (num2 < 3)
						{
							throw new IndexOutOfRangeException("Invalid packet. Message size too small (" + num2 + ")");
						}
						if (i < num2)
						{
							break;
						}
						long position = NetMessage.buffer[bufferIndex].reader.BaseStream.Position;
						int num3;
						NetMessage.buffer[bufferIndex].GetData(num + 2, num2 - 2, out num3);
						NetMessage.buffer[bufferIndex].reader.BaseStream.Position = position + (long)num2;
						i -= num2;
						num += num2;
					}
				}
				catch (Exception)
				{
					if (Main.dedServ && num < NetMessage.buffer.Length - 100)
					{
						Console.WriteLine(Language.GetTextValue("Error.NetMessageError", NetMessage.buffer[num + 2]));
					}
					i = 0;
					num = 0;
				}
				if (i != NetMessage.buffer[bufferIndex].totalData)
				{
					for (int j = 0; j < i; j++)
					{
						NetMessage.buffer[bufferIndex].readBuffer[j] = NetMessage.buffer[bufferIndex].readBuffer[j + num];
					}
					NetMessage.buffer[bufferIndex].totalData = i;
				}
			}
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00040E6C File Offset: 0x0003F06C
		public static void BootPlayer(int plr, NetworkText msg)
		{
			NetMessage.SendData(2, plr, -1, msg, 0, 0f, 0f, 0f, 0, 0, 0);
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00040E98 File Offset: 0x0003F098
		public static void SendObjectPlacement(int whoAmi, int x, int y, int type, int style, int alternative, int random, int direction)
		{
			int num;
			int num2;
			if (Main.netMode == 2)
			{
				num = -1;
				num2 = whoAmi;
			}
			else
			{
				num = whoAmi;
				num2 = -1;
			}
			NetMessage.SendData(79, num, num2, null, x, (float)y, (float)type, (float)style, alternative, random, direction);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00040ED0 File Offset: 0x0003F0D0
		public static void SendTemporaryAnimation(int whoAmi, int animationType, int tileType, int xCoord, int yCoord)
		{
			if (Main.netMode == 2)
			{
				NetMessage.SendData(77, whoAmi, -1, null, animationType, (float)tileType, (float)xCoord, (float)yCoord, 0, 0, 0);
			}
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00040EFC File Offset: 0x0003F0FC
		public static void SendPlayerHurt(int playerTargetIndex, PlayerDeathReason reason, int damage, int direction, bool critical, bool pvp, int hitContext, int remoteClient = -1, int ignoreClient = -1)
		{
			NetMessage._currentPlayerDeathReason = reason;
			BitsByte bitsByte = 0;
			bitsByte[0] = critical;
			bitsByte[1] = pvp;
			NetMessage.SendData(117, remoteClient, ignoreClient, null, playerTargetIndex, (float)damage, (float)direction, (float)bitsByte, hitContext, 0, 0);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00040F48 File Offset: 0x0003F148
		public static void SendPlayerDeath(int playerTargetIndex, PlayerDeathReason reason, int damage, int direction, bool pvp, int remoteClient = -1, int ignoreClient = -1)
		{
			NetMessage._currentPlayerDeathReason = reason;
			BitsByte bitsByte = 0;
			bitsByte[0] = pvp;
			NetMessage.SendData(118, remoteClient, ignoreClient, null, playerTargetIndex, (float)damage, (float)direction, (float)bitsByte, 0, 0, 0);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00040F88 File Offset: 0x0003F188
		public static void PlayNetSound(NetMessage.NetSoundInfo info, int remoteClient = -1, int ignoreClient = -1)
		{
			NetMessage._currentNetSoundInfo = info;
			NetMessage.SendData(132, remoteClient, ignoreClient, null, 0, 0f, 0f, 0f, 0, 0, 0);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00040FBC File Offset: 0x0003F1BC
		public static void SendCoinLossRevengeMarker(CoinLossRevengeSystem.RevengeMarker marker, int remoteClient = -1, int ignoreClient = -1)
		{
			NetMessage._currentRevengeMarker = marker;
			NetMessage.SendData(126, remoteClient, ignoreClient, null, 0, 0f, 0f, 0f, 0, 0, 0);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00040FEC File Offset: 0x0003F1EC
		public static void SendTileSquare(int whoAmi, int tileX, int tileY, int xSize, int ySize, TileChangeType changeType = TileChangeType.None)
		{
			NetMessage.SendData(20, whoAmi, -1, null, tileX, (float)tileY, (float)xSize, (float)ySize, (int)changeType, 0, 0);
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00041010 File Offset: 0x0003F210
		public static void SendTileSquare(int whoAmi, int tileX, int tileY, int centeredSquareSize, TileChangeType changeType = TileChangeType.None)
		{
			int num = (centeredSquareSize - 1) / 2;
			NetMessage.SendTileSquare(whoAmi, tileX - num, tileY - num, centeredSquareSize, centeredSquareSize, changeType);
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00041034 File Offset: 0x0003F234
		public static void SendTileSquare(int whoAmi, int tileX, int tileY, TileChangeType changeType = TileChangeType.None)
		{
			int num = 1;
			int num2 = (num - 1) / 2;
			NetMessage.SendTileSquare(whoAmi, tileX - num2, tileY - num2, num, num, changeType);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00041058 File Offset: 0x0003F258
		public static void SendTravelShop(int remoteClient)
		{
			if (Main.netMode == 2)
			{
				NetMessage.SendData(72, remoteClient, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0004108C File Offset: 0x0003F28C
		public static void SendAnglerQuest(int remoteClient)
		{
			if (Main.netMode != 2)
			{
				return;
			}
			if (remoteClient == -1)
			{
				for (int i = 0; i < 255; i++)
				{
					if (Netplay.Clients[i].State == 10)
					{
						NetMessage.SendData(74, i, -1, NetworkText.FromLiteral(Main.player[i].name), Main.anglerQuest, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				return;
			}
			if (Netplay.Clients[remoteClient].State == 10)
			{
				NetMessage.SendData(74, remoteClient, -1, NetworkText.FromLiteral(Main.player[remoteClient].name), Main.anglerQuest, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0004113C File Offset: 0x0003F33C
		public static void ResyncTiles(Rectangle area)
		{
			for (int i = 0; i < Netplay.Clients.Length; i++)
			{
				if (Netplay.Clients[i].IsActive)
				{
					NetMessage.ResyncTiles(i, area);
				}
			}
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00041170 File Offset: 0x0003F370
		private static void ResyncTiles(int clientId, Rectangle area)
		{
			for (int i = area.Left; i < area.Right; i += 200)
			{
				for (int j = area.Top; j < area.Bottom; j += 150)
				{
					NetMessage.SendData(10, clientId, -1, null, i, (float)j, (float)Math.Min(area.Right - i, 200), (float)Math.Min(area.Bottom - j, 150), 0, 0, 0);
				}
			}
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x000411F0 File Offset: 0x0003F3F0
		public static void SendSection(int whoAmi, int sectionX, int sectionY)
		{
			if (Main.netMode != 2)
			{
				return;
			}
			try
			{
				if (sectionX >= 0 && sectionY >= 0 && sectionX < Main.maxSectionsX && sectionY < Main.maxSectionsY)
				{
					if (!Netplay.Clients[whoAmi].TileSections[sectionX, sectionY])
					{
						Netplay.Clients[whoAmi].TileSections[sectionX, sectionY] = true;
						int num = sectionX * 200;
						int num2 = sectionY * 150;
						int num3 = 150;
						for (int i = num2; i < num2 + 150; i += num3)
						{
							NetMessage.SendData(10, whoAmi, -1, null, num, (float)i, 200f, (float)num3, 0, 0, 0);
						}
						NetMessage.SyncNPCsForSection(whoAmi, sectionX, sectionY);
						NetMessage.SyncChestContentsForSection(whoAmi, sectionX, sectionY);
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x000412B0 File Offset: 0x0003F4B0
		private static void SyncChestContentsForSection(int whoAmi, int sectionX, int sectionY)
		{
			for (int i = 0; i < 8000; i++)
			{
				Chest chest = Main.chest[i];
				if (chest != null)
				{
					int sectionX2 = Netplay.GetSectionX(chest.x);
					int sectionY2 = Netplay.GetSectionY(chest.y);
					if (sectionX == sectionX2 && sectionY == sectionY2)
					{
						NetMessage.SendChestContentsTo(i, whoAmi);
					}
				}
			}
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00041300 File Offset: 0x0003F500
		private static void SyncNPCsForSection(int whoAmi, int sectionX, int sectionY)
		{
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				if (Main.npc[i].active && Main.npc[i].townNPC)
				{
					int sectionX2 = Netplay.GetSectionX((int)(Main.npc[i].position.X / 16f));
					int sectionY2 = Netplay.GetSectionY((int)(Main.npc[i].position.Y / 16f));
					if (sectionX2 == sectionX && sectionY2 == sectionY)
					{
						NetMessage.SendData(23, whoAmi, -1, null, i, 0f, 0f, 0f, 0, 0, 0);
					}
				}
			}
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0004139C File Offset: 0x0003F59C
		public static void greetPlayer(int plr)
		{
			if (Main.motd == "")
			{
				ChatHelper.SendChatMessageToClient(NetworkText.FromFormattable("{0} {1}!", new object[]
				{
					Lang.mp[18].ToNetworkText(),
					Main.worldName
				}), new Color(255, 240, 20), plr);
			}
			else
			{
				ChatHelper.SendChatMessageToClient(NetworkText.FromLiteral(Main.motd), new Color(255, 240, 20), plr);
			}
			string text = "";
			for (int i = 0; i < 255; i++)
			{
				if (Main.player[i].active)
				{
					if (text == "")
					{
						text += Main.player[i].name;
					}
					else
					{
						text = text + ", " + Main.player[i].name;
					}
				}
			}
			ChatHelper.SendChatMessageToClient(NetworkText.FromKey("Game.JoinGreeting", new object[] { text }), new Color(255, 240, 20), plr);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x000414A8 File Offset: 0x0003F6A8
		public static void sendWater(int x, int y)
		{
			if (Main.netMode == 1)
			{
				NetMessage.SendData(48, -1, -1, null, x, (float)y, 0f, 0f, 0, 0, 0);
				return;
			}
			for (int i = 0; i < 256; i++)
			{
				if ((NetMessage.buffer[i].broadcast || Netplay.Clients[i].State >= 3) && Netplay.Clients[i].IsConnected())
				{
					int num = x / 200;
					int num2 = y / 150;
					if (Netplay.Clients[i].TileSections[num, num2])
					{
						NetMessage.SendData(48, i, -1, null, x, (float)y, 0f, 0f, 0, 0, 0);
					}
				}
			}
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00041552 File Offset: 0x0003F752
		public static void SyncDisconnectedPlayer(int plr)
		{
			NetMessage.SyncOnePlayer(plr, -1, plr);
			NetMessage.EnsureLocalPlayerIsPresent();
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00041564 File Offset: 0x0003F764
		public static void SyncConnectedPlayer(int plr)
		{
			NetMessage.SyncOnePlayer(plr, -1, plr);
			for (int i = 0; i < 255; i++)
			{
				if (plr != i && Main.player[i].active)
				{
					NetMessage.SyncOnePlayer(i, plr, -1);
				}
			}
			NetMessage.SendNPCHousesAndTravelShop(plr);
			NetMessage.SendAnglerQuest(plr);
			CreditsRollEvent.SendCreditsRollRemainingTimeToPlayer(plr);
			NPC.RevengeManager.SendAllMarkersToPlayer(plr);
			NetMessage.EnsureLocalPlayerIsPresent();
			DebugOptions.SyncToJoiningPlayer(plr);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x000415CC File Offset: 0x0003F7CC
		private static void SendNPCHousesAndTravelShop(int plr)
		{
			bool flag = false;
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC npc = Main.npc[i];
				if (npc.active)
				{
					bool flag2 = npc.townNPC && NPC.TypeToDefaultHeadIndex(npc.type) > 0;
					if (npc.aiStyle == 7)
					{
						flag2 = true;
					}
					if (flag2)
					{
						if (!flag && npc.type == 368)
						{
							flag = true;
						}
						byte householdStatus = WorldGen.TownManager.GetHouseholdStatus(npc);
						NetMessage.SendData(60, plr, -1, null, i, (float)npc.homeTileX, (float)npc.homeTileY, (float)householdStatus, 0, 0, 0);
					}
				}
			}
			if (flag)
			{
				NetMessage.SendTravelShop(plr);
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00041670 File Offset: 0x0003F870
		private static void EnsureLocalPlayerIsPresent()
		{
			if (!Main.autoShutdown)
			{
				return;
			}
			bool flag = false;
			for (int i = 0; i < 255; i++)
			{
				if (NetMessage.DoesPlayerSlotCountAsAHost(i))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				Console.WriteLine(Language.GetTextValue("Net.ServerAutoShutdown"));
				Netplay.Disconnect = true;
			}
		}

		// Token: 0x060002CA RID: 714 RVA: 0x000416BB File Offset: 0x0003F8BB
		public static bool DoesPlayerSlotCountAsAHost(int plr)
		{
			return Netplay.Clients[plr].State == 10 && Netplay.Clients[plr].Socket.GetRemoteAddress().IsLocalHost();
		}

		// Token: 0x060002CB RID: 715 RVA: 0x000416E8 File Offset: 0x0003F8E8
		private static void SyncOnePlayer(int plr, int toWho, int fromWho)
		{
			int num = 0;
			if (Main.player[plr].active)
			{
				num = 1;
			}
			if (Netplay.Clients[plr].State == 10)
			{
				NetMessage.SendData(14, toWho, fromWho, null, plr, (float)num, 0f, 0f, 0, 0, 0);
				NetMessage.SendData(4, toWho, fromWho, null, plr, 0f, 0f, 0f, 0, 0, 0);
				NetMessage.SendData(13, toWho, fromWho, null, plr, 0f, 0f, 0f, 0, 0, 0);
				if (Main.player[plr].statLife <= 0)
				{
					NetMessage.SendData(135, toWho, fromWho, null, plr, 0f, 0f, 0f, 0, 0, 0);
				}
				NetMessage.SendData(16, toWho, fromWho, null, plr, 0f, 0f, 0f, 0, 0, 0);
				NetMessage.SendData(30, toWho, fromWho, null, plr, 0f, 0f, 0f, 0, 0, 0);
				NetMessage.SendData(45, toWho, fromWho, null, plr, 0f, 0f, 0f, 0, 0, 0);
				NetMessage.SendData(42, toWho, fromWho, null, plr, 0f, 0f, 0f, 0, 0, 0);
				NetMessage.SendData(50, toWho, fromWho, null, plr, 0f, 0f, 0f, 0, 0, 0);
				NetMessage.SendData(80, toWho, fromWho, null, plr, (float)Main.player[plr].chest, 0f, 0f, 0, 0, 0);
				NetMessage.SendData(142, toWho, fromWho, null, plr, 0f, 0f, 0f, 0, 0, 0);
				NetMessage.SendData(147, toWho, fromWho, null, plr, (float)Main.player[plr].CurrentLoadoutIndex, 0f, 0f, 0, 0, 0);
				TagEffectState.NetModule.SyncStateIfNecessary(Main.player[plr].TagEffectState, toWho, fromWho);
				for (int i = 0; i < 59; i++)
				{
					NetMessage.SendData(5, toWho, fromWho, null, plr, (float)(PlayerItemSlotID.Inventory0 + i), 0f, 0f, 0, 0, 0);
				}
				for (int j = 0; j < Main.player[plr].armor.Length; j++)
				{
					NetMessage.SendData(5, toWho, fromWho, null, plr, (float)(PlayerItemSlotID.Armor0 + j), 0f, 0f, 0, 0, 0);
				}
				for (int k = 0; k < Main.player[plr].dye.Length; k++)
				{
					NetMessage.SendData(5, toWho, fromWho, null, plr, (float)(PlayerItemSlotID.Dye0 + k), 0f, 0f, 0, 0, 0);
				}
				NetMessage.SyncOnePlayer_ItemArray(plr, toWho, fromWho, Main.player[plr].miscEquips, PlayerItemSlotID.Misc0);
				NetMessage.SyncOnePlayer_ItemArray(plr, toWho, fromWho, Main.player[plr].miscDyes, PlayerItemSlotID.MiscDye0);
				NetMessage.SyncOnePlayer_ItemArray(plr, toWho, fromWho, Main.player[plr].Loadouts[0].Armor, PlayerItemSlotID.Loadout1_Armor_0);
				NetMessage.SyncOnePlayer_ItemArray(plr, toWho, fromWho, Main.player[plr].Loadouts[0].Dye, PlayerItemSlotID.Loadout1_Dye_0);
				NetMessage.SyncOnePlayer_ItemArray(plr, toWho, fromWho, Main.player[plr].Loadouts[1].Armor, PlayerItemSlotID.Loadout2_Armor_0);
				NetMessage.SyncOnePlayer_ItemArray(plr, toWho, fromWho, Main.player[plr].Loadouts[1].Dye, PlayerItemSlotID.Loadout2_Dye_0);
				NetMessage.SyncOnePlayer_ItemArray(plr, toWho, fromWho, Main.player[plr].Loadouts[2].Armor, PlayerItemSlotID.Loadout3_Armor_0);
				NetMessage.SyncOnePlayer_ItemArray(plr, toWho, fromWho, Main.player[plr].Loadouts[2].Dye, PlayerItemSlotID.Loadout3_Dye_0);
				if (!Netplay.Clients[plr].IsAnnouncementCompleted)
				{
					Netplay.Clients[plr].IsAnnouncementCompleted = true;
					ChatHelper.BroadcastChatMessage(NetworkText.FromKey(Lang.mp[19].Key, new object[] { Main.player[plr].name }), new Color(255, 240, 20), plr);
					if (Main.dedServ)
					{
						Console.WriteLine(Lang.mp[19].Format(Main.player[plr].name));
					}
				}
				for (int l = 0; l < 1000; l++)
				{
					Projectile projectile = Main.projectile[l];
					if (projectile.active && projectile.owner == plr)
					{
						NetMessage.SendData(27, toWho, -1, null, l, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				return;
			}
			num = 0;
			NetMessage.SendData(14, -1, plr, null, plr, (float)num, 0f, 0f, 0, 0, 0);
			if (Netplay.Clients[plr].IsAnnouncementCompleted)
			{
				Netplay.Clients[plr].IsAnnouncementCompleted = false;
				ChatHelper.BroadcastChatMessage(NetworkText.FromKey(Lang.mp[20].Key, new object[] { Netplay.Clients[plr].Name }), new Color(255, 240, 20), plr);
				if (Main.dedServ)
				{
					Console.WriteLine(Lang.mp[20].Format(Netplay.Clients[plr].Name));
				}
				Netplay.Clients[plr].Name = "Anonymous";
			}
			Player.Hooks.PlayerDisconnect(plr);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00041BC0 File Offset: 0x0003FDC0
		private static void SyncOnePlayer_ItemArray(int plr, int toWho, int fromWho, Item[] arr, int slot)
		{
			for (int i = 0; i < arr.Length; i++)
			{
				NetMessage.SendData(5, toWho, fromWho, null, plr, (float)(slot + i), 0f, 0f, 0, 0, 0);
			}
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000357B File Offset: 0x0000177B
		public NetMessage()
		{
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00041BF7 File Offset: 0x0003FDF7
		// Note: this type is marked as 'beforefieldinit'.
		static NetMessage()
		{
		}

		// Token: 0x040001F4 RID: 500
		public static MessageBuffer[] buffer = new MessageBuffer[257];

		// Token: 0x040001F5 RID: 501
		private static short[] _compressChestList = new short[8000];

		// Token: 0x040001F6 RID: 502
		private static short[] _compressSignList = new short[32000];

		// Token: 0x040001F7 RID: 503
		private static short[] _compressEntities = new short[1000];

		// Token: 0x040001F8 RID: 504
		private static PlayerDeathReason _currentPlayerDeathReason;

		// Token: 0x040001F9 RID: 505
		private static NetMessage.NetSoundInfo _currentNetSoundInfo;

		// Token: 0x040001FA RID: 506
		private static CoinLossRevengeSystem.RevengeMarker _currentRevengeMarker;

		// Token: 0x02000605 RID: 1541
		public struct NetSoundInfo
		{
			// Token: 0x06003BBC RID: 15292 RVA: 0x0065B65B File Offset: 0x0065985B
			public NetSoundInfo(Vector2 position, ushort soundIndex, int style = -1, float volume = -1f, float pitchOffset = -1f)
			{
				this.position = position;
				this.soundIndex = soundIndex;
				this.style = style;
				this.volume = volume;
				this.pitchOffset = pitchOffset;
			}

			// Token: 0x06003BBD RID: 15293 RVA: 0x0065B684 File Offset: 0x00659884
			public void WriteSelfTo(BinaryWriter writer)
			{
				writer.WriteVector2(this.position);
				writer.Write(this.soundIndex);
				BitsByte bitsByte = new BitsByte(this.style != -1, this.volume != -1f, this.pitchOffset != -1f, false, false, false, false, false);
				writer.Write(bitsByte);
				if (bitsByte[0])
				{
					writer.Write(this.style);
				}
				if (bitsByte[1])
				{
					writer.Write(this.volume);
				}
				if (bitsByte[2])
				{
					writer.Write(this.pitchOffset);
				}
			}

			// Token: 0x04006435 RID: 25653
			public Vector2 position;

			// Token: 0x04006436 RID: 25654
			public ushort soundIndex;

			// Token: 0x04006437 RID: 25655
			public int style;

			// Token: 0x04006438 RID: 25656
			public float volume;

			// Token: 0x04006439 RID: 25657
			public float pitchOffset;
		}
	}
}
