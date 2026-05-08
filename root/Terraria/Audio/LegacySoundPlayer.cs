using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using ReLogic.Content;
using ReLogic.Utilities;
using Terraria.ID;

namespace Terraria.Audio
{
	// Token: 0x020005D5 RID: 1493
	public class LegacySoundPlayer
	{
		// Token: 0x06003AA3 RID: 15011 RVA: 0x00655D78 File Offset: 0x00653F78
		public LegacySoundPlayer(IServiceProvider services)
		{
			this._services = services;
			this._trackedInstances = new List<SoundEffectInstance>();
			this.LoadAll();
		}

		// Token: 0x06003AA4 RID: 15012 RVA: 0x00655F3D File Offset: 0x0065413D
		public void Reload()
		{
			this.CreateAllSoundInstances();
		}

		// Token: 0x06003AA5 RID: 15013 RVA: 0x00655F48 File Offset: 0x00654148
		private void LoadAll()
		{
			this.SoundMech[0] = this.Load("Sounds/Mech_0");
			this.SoundGrab = this.Load("Sounds/Grab");
			this.SoundPixie = this.Load("Sounds/Pixie");
			this.SoundDig[0] = this.Load("Sounds/Dig_0");
			this.SoundDig[1] = this.Load("Sounds/Dig_1");
			this.SoundDig[2] = this.Load("Sounds/Dig_2");
			this.SoundThunder[0] = this.Load("Sounds/Thunder_0");
			this.SoundThunder[1] = this.Load("Sounds/Thunder_1");
			this.SoundThunder[2] = this.Load("Sounds/Thunder_2");
			this.SoundThunder[3] = this.Load("Sounds/Thunder_3");
			this.SoundThunder[4] = this.Load("Sounds/Thunder_4");
			this.SoundThunder[5] = this.Load("Sounds/Thunder_5");
			this.SoundResearch[0] = this.Load("Sounds/Research_0");
			this.SoundResearch[1] = this.Load("Sounds/Research_1");
			this.SoundResearch[2] = this.Load("Sounds/Research_2");
			this.SoundResearch[3] = this.Load("Sounds/Research_3");
			this.SoundTink[0] = this.Load("Sounds/Tink_0");
			this.SoundTink[1] = this.Load("Sounds/Tink_1");
			this.SoundTink[2] = this.Load("Sounds/Tink_2");
			this.SoundPlayerHit[0] = this.Load("Sounds/Player_Hit_0");
			this.SoundPlayerHit[1] = this.Load("Sounds/Player_Hit_1");
			this.SoundPlayerHit[2] = this.Load("Sounds/Player_Hit_2");
			this.SoundFemaleHit[0] = this.Load("Sounds/Female_Hit_0");
			this.SoundFemaleHit[1] = this.Load("Sounds/Female_Hit_1");
			this.SoundFemaleHit[2] = this.Load("Sounds/Female_Hit_2");
			this.SoundPlayerKilled = this.Load("Sounds/Player_Killed");
			this.SoundChat = this.Load("Sounds/Chat");
			this.SoundGrass = this.Load("Sounds/Grass");
			this.SoundDoorOpen = this.Load("Sounds/Door_Opened");
			this.SoundDoorClosed = this.Load("Sounds/Door_Closed");
			this.SoundMenuTick = this.Load("Sounds/Menu_Tick");
			this.SoundMenuOpen = this.Load("Sounds/Menu_Open");
			this.SoundMenuClose = this.Load("Sounds/Menu_Close");
			this.SoundShatter = this.Load("Sounds/Shatter");
			this.SoundCamera = this.Load("Sounds/Camera");
			for (int i = 0; i < this.SoundCoin.Length; i++)
			{
				this.SoundCoin[i] = this.Load("Sounds/Coin_" + i);
			}
			for (int j = 0; j < this.SoundDrip.Length; j++)
			{
				this.SoundDrip[j] = this.Load("Sounds/Drip_" + j);
			}
			for (int k = 0; k < this.SoundZombie.Length; k++)
			{
				this.SoundZombie[k] = this.Load("Sounds/Zombie_" + k);
			}
			for (int l = 0; l < this.SoundLiquid.Length; l++)
			{
				this.SoundLiquid[l] = this.Load("Sounds/Liquid_" + l);
			}
			for (int m = 0; m < this.SoundRoar.Length; m++)
			{
				this.SoundRoar[m] = this.Load("Sounds/Roar_" + m);
			}
			for (int n = 0; n < this.SoundSplash.Length; n++)
			{
				this.SoundSplash[n] = this.Load("Sounds/Splash_" + n);
			}
			this.SoundDoubleJump = this.Load("Sounds/Double_Jump");
			this.SoundRun = this.Load("Sounds/Run");
			this.SoundCoins = this.Load("Sounds/Coins");
			this.SoundUnlock = this.Load("Sounds/Unlock");
			this.SoundMaxMana = this.Load("Sounds/MaxMana");
			this.SoundDrown = this.Load("Sounds/Drown");
			for (int num = 1; num < this.SoundItem.Length; num++)
			{
				this.SoundItem[num] = this.Load("Sounds/Item_" + num);
			}
			for (int num2 = 1; num2 < this.SoundNpcHit.Length; num2++)
			{
				this.SoundNpcHit[num2] = this.Load("Sounds/NPC_Hit_" + num2);
			}
			for (int num3 = 1; num3 < this.SoundNpcKilled.Length; num3++)
			{
				this.SoundNpcKilled[num3] = this.Load("Sounds/NPC_Killed_" + num3);
			}
			this.TrackableSounds = new Asset<SoundEffect>[SoundID.TrackableLegacySoundCount];
			this.TrackableSoundInstances = new SoundEffectInstance[this.TrackableSounds.Length];
			for (int num4 = 0; num4 < this.TrackableSounds.Length; num4++)
			{
				this.TrackableSounds[num4] = this.Load("Sounds/Custom" + Path.DirectorySeparatorChar.ToString() + SoundID.GetTrackableLegacySoundPath(num4));
			}
		}

		// Token: 0x06003AA6 RID: 15014 RVA: 0x0065647C File Offset: 0x0065467C
		public void CreateAllSoundInstances()
		{
			foreach (SoundEffectInstance soundEffectInstance in this._trackedInstances)
			{
				soundEffectInstance.Dispose();
			}
			this._trackedInstances.Clear();
			this.SoundInstanceMech[0] = this.CreateInstance(this.SoundMech[0]);
			this.SoundInstanceGrab = this.CreateInstance(this.SoundGrab);
			this.SoundInstancePixie = this.CreateInstance(this.SoundGrab);
			this.SoundInstanceDig[0] = this.CreateInstance(this.SoundDig[0]);
			this.SoundInstanceDig[1] = this.CreateInstance(this.SoundDig[1]);
			this.SoundInstanceDig[2] = this.CreateInstance(this.SoundDig[2]);
			this.SoundInstanceTink[0] = this.CreateInstance(this.SoundTink[0]);
			this.SoundInstanceTink[1] = this.CreateInstance(this.SoundTink[1]);
			this.SoundInstanceTink[2] = this.CreateInstance(this.SoundTink[2]);
			this.SoundInstancePlayerHit[0] = this.CreateInstance(this.SoundPlayerHit[0]);
			this.SoundInstancePlayerHit[1] = this.CreateInstance(this.SoundPlayerHit[1]);
			this.SoundInstancePlayerHit[2] = this.CreateInstance(this.SoundPlayerHit[2]);
			this.SoundInstanceFemaleHit[0] = this.CreateInstance(this.SoundFemaleHit[0]);
			this.SoundInstanceFemaleHit[1] = this.CreateInstance(this.SoundFemaleHit[1]);
			this.SoundInstanceFemaleHit[2] = this.CreateInstance(this.SoundFemaleHit[2]);
			this.SoundInstancePlayerKilled = this.CreateInstance(this.SoundPlayerKilled);
			this.SoundInstanceChat = this.CreateInstance(this.SoundChat);
			this.SoundInstanceGrass = this.CreateInstance(this.SoundGrass);
			this.SoundInstanceDoorOpen = this.CreateInstance(this.SoundDoorOpen);
			this.SoundInstanceDoorClosed = this.CreateInstance(this.SoundDoorClosed);
			this.SoundInstanceMenuTick = this.CreateInstance(this.SoundMenuTick);
			this.SoundInstanceMenuOpen = this.CreateInstance(this.SoundMenuOpen);
			this.SoundInstanceMenuClose = this.CreateInstance(this.SoundMenuClose);
			this.SoundInstanceShatter = this.CreateInstance(this.SoundShatter);
			this.SoundInstanceCamera = this.CreateInstance(this.SoundCamera);
			this.SoundInstanceSplash[0] = this.CreateInstance(this.SoundRoar[0]);
			this.SoundInstanceSplash[1] = this.CreateInstance(this.SoundSplash[1]);
			this.SoundInstanceDoubleJump = this.CreateInstance(this.SoundRoar[0]);
			this.SoundInstanceRun = this.CreateInstance(this.SoundRun);
			this.SoundInstanceCoins = this.CreateInstance(this.SoundCoins);
			this.SoundInstanceUnlock = this.CreateInstance(this.SoundUnlock);
			this.SoundInstanceMaxMana = this.CreateInstance(this.SoundMaxMana);
			this.SoundInstanceDrown = this.CreateInstance(this.SoundDrown);
			this.SoundInstanceMoonlordCry = this.CreateInstance(this.SoundNpcKilled[10]);
			for (int i = 0; i < this.SoundThunder.Length; i++)
			{
				this.SoundInstanceThunder[i] = this.CreateInstance(this.SoundThunder[i]);
			}
			for (int j = 0; j < this.SoundResearch.Length; j++)
			{
				this.SoundInstanceResearch[j] = this.CreateInstance(this.SoundResearch[j]);
			}
			for (int k = 0; k < this.SoundCoin.Length; k++)
			{
				this.SoundInstanceCoin[k] = this.CreateInstance(this.SoundCoin[k]);
			}
			for (int l = 0; l < this.SoundDrip.Length; l++)
			{
				this.SoundInstanceDrip[l] = this.CreateInstance(this.SoundDrip[l]);
			}
			for (int m = 0; m < this.SoundZombie.Length; m++)
			{
				this.SoundInstanceZombie[m] = this.CreateInstance(this.SoundZombie[m]);
			}
			for (int n = 0; n < this.SoundLiquid.Length; n++)
			{
				this.SoundInstanceLiquid[n] = this.CreateInstance(this.SoundLiquid[n]);
			}
			for (int num = 0; num < this.SoundRoar.Length; num++)
			{
				this.SoundInstanceRoar[num] = this.CreateInstance(this.SoundRoar[num]);
			}
			for (int num2 = 1; num2 < this.SoundItem.Length; num2++)
			{
				this.SoundInstanceItem[num2] = this.CreateInstance(this.SoundItem[num2]);
			}
			for (int num3 = 1; num3 < this.SoundNpcHit.Length; num3++)
			{
				this.SoundInstanceNpcHit[num3] = this.CreateInstance(this.SoundNpcHit[num3]);
			}
			for (int num4 = 1; num4 < this.SoundNpcKilled.Length; num4++)
			{
				this.SoundInstanceNpcKilled[num4] = this.CreateInstance(this.SoundNpcKilled[num4]);
			}
			for (int num5 = 0; num5 < this.TrackableSounds.Length; num5++)
			{
				this.TrackableSoundInstances[num5] = this.CreateInstance(this.TrackableSounds[num5]);
			}
		}

		// Token: 0x06003AA7 RID: 15015 RVA: 0x00656970 File Offset: 0x00654B70
		private SoundEffectInstance CreateInstance(Asset<SoundEffect> asset)
		{
			SoundEffectInstance soundEffectInstance = asset.Value.CreateInstance();
			this._trackedInstances.Add(soundEffectInstance);
			return soundEffectInstance;
		}

		// Token: 0x06003AA8 RID: 15016 RVA: 0x00656996 File Offset: 0x00654B96
		private Asset<SoundEffect> Load(string assetName)
		{
			return XnaExtensions.Get<IAssetRepository>(this._services).Request<SoundEffect>(assetName, 2);
		}

		// Token: 0x06003AA9 RID: 15017 RVA: 0x006569AC File Offset: 0x00654BAC
		public SoundEffectInstance PlaySound(int type, int x = -1, int y = -1, int Style = 1, float volumeScale = 1f, float pitchOffset = 0f)
		{
			int num = Style;
			try
			{
				if (Main.dedServ)
				{
					return null;
				}
				if (Main.soundVolume == 0f && (type < 30 || type > 35))
				{
					return null;
				}
				bool flag = false;
				float num2 = 1f;
				float num3 = 0f;
				if (x == -1 || y == -1)
				{
					flag = true;
				}
				else
				{
					if (WorldGen.isGeneratingOrLoadingWorld)
					{
						return null;
					}
					if (Main.netMode == 2)
					{
						return null;
					}
					Vector2 vector = new Vector2((float)x, (float)y) - Main.Camera.Center;
					float num4 = vector.Length();
					if (num4 < LegacySoundPlayer.SoundAttenuationDistance)
					{
						flag = true;
						num3 = MathHelper.Clamp(vector.X / ((float)Main.MaxWorldViewSize.X * 0.5f), -1f, 1f);
						num2 = 1f - num4 / LegacySoundPlayer.SoundAttenuationDistance;
					}
				}
				if (num3 < -1f)
				{
					num3 = -1f;
				}
				if (num3 > 1f)
				{
					num3 = 1f;
				}
				if (num2 > 1f)
				{
					num2 = 1f;
				}
				if (num2 <= 0f && (type < 34 || type > 35 || type > 39))
				{
					return null;
				}
				if (flag)
				{
					if (this.DoesSoundScaleWithAmbientVolume(type))
					{
						num2 *= Main.ambientVolume * (float)(FocusHelper.QuietAmbientSounds ? 0 : 1);
						if (Main.gameMenu)
						{
							num2 = 0f;
						}
					}
					else
					{
						num2 *= Main.soundVolume;
					}
					if (num2 > 1f)
					{
						num2 = 1f;
					}
					if (num2 <= 0f && (type < 30 || type > 35) && type != 39)
					{
						return null;
					}
					SoundEffectInstance soundEffectInstance = null;
					if (type == 0)
					{
						int num5 = Main.rand.Next(3);
						if (this.SoundInstanceDig[num5] != null)
						{
							this.SoundInstanceDig[num5].Stop();
						}
						this.SoundInstanceDig[num5] = this.SoundDig[num5].Value.CreateInstance();
						this.SoundInstanceDig[num5].Volume = num2;
						this.SoundInstanceDig[num5].Pan = num3;
						this.SoundInstanceDig[num5].Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
						soundEffectInstance = this.SoundInstanceDig[num5];
					}
					else if (type == 43)
					{
						int num6 = Main.rand.Next(this.SoundThunder.Length);
						int num7 = 0;
						while (num7 < this.SoundThunder.Length && this.SoundInstanceThunder[num6] != null && this.SoundInstanceThunder[num6].State == SoundState.Playing)
						{
							num6 = Main.rand.Next(this.SoundThunder.Length);
							num7++;
						}
						if (this.SoundInstanceThunder[num6] != null)
						{
							this.SoundInstanceThunder[num6].Stop();
						}
						this.SoundInstanceThunder[num6] = this.SoundThunder[num6].Value.CreateInstance();
						this.SoundInstanceThunder[num6].Volume = num2;
						this.SoundInstanceThunder[num6].Pan = num3;
						this.SoundInstanceThunder[num6].Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
						soundEffectInstance = this.SoundInstanceThunder[num6];
					}
					else if (type == 63)
					{
						int num8 = Main.rand.Next(1, 4);
						if (this.SoundInstanceResearch[num8] != null)
						{
							this.SoundInstanceResearch[num8].Stop();
						}
						this.SoundInstanceResearch[num8] = this.SoundResearch[num8].Value.CreateInstance();
						this.SoundInstanceResearch[num8].Volume = num2;
						this.SoundInstanceResearch[num8].Pan = num3;
						soundEffectInstance = this.SoundInstanceResearch[num8];
					}
					else if (type == 64)
					{
						if (this.SoundInstanceResearch[0] != null)
						{
							this.SoundInstanceResearch[0].Stop();
						}
						this.SoundInstanceResearch[0] = this.SoundResearch[0].Value.CreateInstance();
						this.SoundInstanceResearch[0].Volume = num2;
						this.SoundInstanceResearch[0].Pan = num3;
						soundEffectInstance = this.SoundInstanceResearch[0];
					}
					else if (type == 1)
					{
						int num9 = Main.rand.Next(3);
						if (this.SoundInstancePlayerHit[num9] != null)
						{
							this.SoundInstancePlayerHit[num9].Stop();
						}
						this.SoundInstancePlayerHit[num9] = this.SoundPlayerHit[num9].Value.CreateInstance();
						this.SoundInstancePlayerHit[num9].Volume = num2;
						this.SoundInstancePlayerHit[num9].Pan = num3;
						soundEffectInstance = this.SoundInstancePlayerHit[num9];
					}
					else if (type == 2)
					{
						if (num == 176)
						{
							num2 *= 0.9f;
						}
						if (num == 129)
						{
							num2 *= 0.6f;
						}
						if (num == 123)
						{
							num2 *= 0.5f;
						}
						if (num == 124 || num == 125)
						{
							num2 *= 0.65f;
						}
						if (num == 116)
						{
							num2 *= 0.5f;
						}
						if (num == 1)
						{
							int num10 = Main.rand.Next(3);
							if (num10 == 1)
							{
								num = 18;
							}
							if (num10 == 2)
							{
								num = 19;
							}
						}
						else if (num == 55 || num == 53)
						{
							num2 *= 0.75f;
							if (num == 55)
							{
								num2 *= 0.75f;
							}
							if (this.SoundInstanceItem[num] != null && this.SoundInstanceItem[num].State == SoundState.Playing)
							{
								return null;
							}
						}
						else if (num == 37)
						{
							num2 *= 0.5f;
						}
						else if (num == 52)
						{
							num2 *= 0.35f;
						}
						else if (num == 157)
						{
							num2 *= 0.7f;
						}
						else if (num == 158)
						{
							num2 *= 0.8f;
						}
						if (num == 159)
						{
							if (this.SoundInstanceItem[num] != null && this.SoundInstanceItem[num].State == SoundState.Playing)
							{
								return null;
							}
							num2 *= 0.75f;
						}
						else if (num != 9 && num != 10 && num != 24 && num != 26 && num != 34 && num != 43 && num != 103 && num != 156 && num != 162 && this.SoundInstanceItem[num] != null)
						{
							this.SoundInstanceItem[num].Stop();
						}
						this.SoundInstanceItem[num] = this.SoundItem[num].Value.CreateInstance();
						this.SoundInstanceItem[num].Volume = num2;
						this.SoundInstanceItem[num].Pan = num3;
						if (num == 53)
						{
							this.SoundInstanceItem[num].Pitch = (float)Main.rand.Next(-20, -11) * 0.02f;
						}
						else if (num == 55)
						{
							this.SoundInstanceItem[num].Pitch = (float)(-(float)Main.rand.Next(-20, -11)) * 0.02f;
						}
						else if (num == 132)
						{
							this.SoundInstanceItem[num].Pitch = (float)Main.rand.Next(-20, 21) * 0.001f;
						}
						else if (num == 153)
						{
							this.SoundInstanceItem[num].Pitch = (float)Main.rand.Next(-50, 51) * 0.003f;
						}
						else if (num == 156)
						{
							this.SoundInstanceItem[num].Pitch = (float)Main.rand.Next(-50, 51) * 0.002f;
							this.SoundInstanceItem[num].Volume *= 0.6f;
						}
						else if (num == 192)
						{
							this.SoundInstanceItem[num].Pitch = Projectile.kiteSoundPitch;
						}
						else
						{
							this.SoundInstanceItem[num].Pitch = (float)Main.rand.Next(-6, 7) * 0.01f;
						}
						if (num == 26 || num == 35 || num == 47)
						{
							this.SoundInstanceItem[num].Volume = num2 * 0.75f;
							this.SoundInstanceItem[num].Pitch = Main.musicPitch;
						}
						if (num == 169)
						{
							this.SoundInstanceItem[num].Pitch -= 0.8f;
						}
						soundEffectInstance = this.SoundInstanceItem[num];
					}
					else if (type == 3)
					{
						if (num >= 20 && num <= 54)
						{
							num2 *= 0.5f;
						}
						if (num == 57 && this.SoundInstanceNpcHit[num] != null && this.SoundInstanceNpcHit[num].State == SoundState.Playing)
						{
							return null;
						}
						if (num == 57)
						{
							num2 *= 0.6f;
						}
						if (num == 55 || num == 56)
						{
							num2 *= 0.5f;
						}
						if (this.SoundInstanceNpcHit[num] != null)
						{
							this.SoundInstanceNpcHit[num].Stop();
						}
						this.SoundInstanceNpcHit[num] = this.SoundNpcHit[num].Value.CreateInstance();
						this.SoundInstanceNpcHit[num].Volume = num2;
						this.SoundInstanceNpcHit[num].Pan = num3;
						this.SoundInstanceNpcHit[num].Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
						soundEffectInstance = this.SoundInstanceNpcHit[num];
					}
					else if (type == 4)
					{
						if (num >= 23 && num <= 57)
						{
							num2 *= 0.5f;
						}
						if (num == 61)
						{
							num2 *= 0.6f;
						}
						if (num == 62)
						{
							num2 *= 0.6f;
						}
						if (num == 10 && this.SoundInstanceNpcKilled[num] != null && this.SoundInstanceNpcKilled[num].State == SoundState.Playing)
						{
							return null;
						}
						this.SoundInstanceNpcKilled[num] = this.SoundNpcKilled[num].Value.CreateInstance();
						this.SoundInstanceNpcKilled[num].Volume = num2;
						this.SoundInstanceNpcKilled[num].Pan = num3;
						this.SoundInstanceNpcKilled[num].Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
						soundEffectInstance = this.SoundInstanceNpcKilled[num];
					}
					else if (type == 5)
					{
						if (this.SoundInstancePlayerKilled != null)
						{
							this.SoundInstancePlayerKilled.Stop();
						}
						this.SoundInstancePlayerKilled = this.SoundPlayerKilled.Value.CreateInstance();
						this.SoundInstancePlayerKilled.Volume = num2;
						this.SoundInstancePlayerKilled.Pan = num3;
						soundEffectInstance = this.SoundInstancePlayerKilled;
					}
					else if (type == 6)
					{
						if (this.SoundInstanceGrass != null)
						{
							this.SoundInstanceGrass.Stop();
						}
						this.SoundInstanceGrass = this.SoundGrass.Value.CreateInstance();
						this.SoundInstanceGrass.Volume = num2;
						this.SoundInstanceGrass.Pan = num3;
						this.SoundInstanceGrass.Pitch = (float)Main.rand.Next(-30, 31) * 0.01f;
						soundEffectInstance = this.SoundInstanceGrass;
					}
					else if (type == 7)
					{
						if (this.SoundInstanceGrab != null)
						{
							this.SoundInstanceGrab.Stop();
						}
						this.SoundInstanceGrab = this.SoundGrab.Value.CreateInstance();
						this.SoundInstanceGrab.Volume = num2;
						this.SoundInstanceGrab.Pan = num3;
						this.SoundInstanceGrab.Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
						soundEffectInstance = this.SoundInstanceGrab;
					}
					else if (type == 8)
					{
						if (this.SoundInstanceDoorOpen != null)
						{
							this.SoundInstanceDoorOpen.Stop();
						}
						this.SoundInstanceDoorOpen = this.SoundDoorOpen.Value.CreateInstance();
						this.SoundInstanceDoorOpen.Volume = num2;
						this.SoundInstanceDoorOpen.Pan = num3;
						this.SoundInstanceDoorOpen.Pitch = (float)Main.rand.Next(-20, 21) * 0.01f;
						soundEffectInstance = this.SoundInstanceDoorOpen;
					}
					else if (type == 9)
					{
						if (this.SoundInstanceDoorClosed != null)
						{
							this.SoundInstanceDoorClosed.Stop();
						}
						this.SoundInstanceDoorClosed = this.SoundDoorClosed.Value.CreateInstance();
						this.SoundInstanceDoorClosed.Volume = num2;
						this.SoundInstanceDoorClosed.Pan = num3;
						this.SoundInstanceDoorClosed.Pitch = (float)Main.rand.Next(-20, 21) * 0.01f;
						soundEffectInstance = this.SoundInstanceDoorClosed;
					}
					else if (type == 10)
					{
						if (this.SoundInstanceMenuOpen != null)
						{
							this.SoundInstanceMenuOpen.Stop();
						}
						this.SoundInstanceMenuOpen = this.SoundMenuOpen.Value.CreateInstance();
						this.SoundInstanceMenuOpen.Volume = num2;
						this.SoundInstanceMenuOpen.Pan = num3;
						soundEffectInstance = this.SoundInstanceMenuOpen;
					}
					else if (type == 11)
					{
						if (this.SoundInstanceMenuClose != null)
						{
							this.SoundInstanceMenuClose.Stop();
						}
						this.SoundInstanceMenuClose = this.SoundMenuClose.Value.CreateInstance();
						this.SoundInstanceMenuClose.Volume = num2;
						this.SoundInstanceMenuClose.Pan = num3;
						soundEffectInstance = this.SoundInstanceMenuClose;
					}
					else if (type == 12)
					{
						if (FocusHelper.AllowUIInputs)
						{
							if (this.SoundInstanceMenuTick != null)
							{
								this.SoundInstanceMenuTick.Stop();
							}
							this.SoundInstanceMenuTick = this.SoundMenuTick.Value.CreateInstance();
							this.SoundInstanceMenuTick.Volume = num2;
							this.SoundInstanceMenuTick.Pan = num3;
							soundEffectInstance = this.SoundInstanceMenuTick;
						}
					}
					else if (type == 13)
					{
						if (this.SoundInstanceShatter != null)
						{
							this.SoundInstanceShatter.Stop();
						}
						this.SoundInstanceShatter = this.SoundShatter.Value.CreateInstance();
						this.SoundInstanceShatter.Volume = num2;
						this.SoundInstanceShatter.Pan = num3;
						soundEffectInstance = this.SoundInstanceShatter;
					}
					else if (type == 14)
					{
						if (Style == 542)
						{
							int num11 = 7;
							this.SoundInstanceZombie[num11] = this.SoundZombie[num11].Value.CreateInstance();
							this.SoundInstanceZombie[num11].Volume = num2 * 0.4f;
							this.SoundInstanceZombie[num11].Pan = num3;
							soundEffectInstance = this.SoundInstanceZombie[num11];
						}
						else if (Style == 489 || Style == 586)
						{
							int num12 = Main.rand.Next(21, 24);
							this.SoundInstanceZombie[num12] = this.SoundZombie[num12].Value.CreateInstance();
							this.SoundInstanceZombie[num12].Volume = num2 * 0.4f;
							this.SoundInstanceZombie[num12].Pan = num3;
							soundEffectInstance = this.SoundInstanceZombie[num12];
						}
						else
						{
							int num13 = Main.rand.Next(3);
							this.SoundInstanceZombie[num13] = this.SoundZombie[num13].Value.CreateInstance();
							this.SoundInstanceZombie[num13].Volume = num2 * 0.4f;
							this.SoundInstanceZombie[num13].Pan = num3;
							soundEffectInstance = this.SoundInstanceZombie[num13];
						}
					}
					else if (type == 15)
					{
						float num14 = 1f;
						if (num == 4)
						{
							num = 1;
							num14 = 0.25f;
						}
						if (this.SoundInstanceRoar[num] == null || this.SoundInstanceRoar[num].State == SoundState.Stopped)
						{
							this.SoundInstanceRoar[num] = this.SoundRoar[num].Value.CreateInstance();
							this.SoundInstanceRoar[num].Volume = num2 * num14;
							this.SoundInstanceRoar[num].Pan = num3;
							soundEffectInstance = this.SoundInstanceRoar[num];
						}
					}
					else if (type == 16)
					{
						if (this.SoundInstanceDoubleJump != null)
						{
							this.SoundInstanceDoubleJump.Stop();
						}
						this.SoundInstanceDoubleJump = this.SoundDoubleJump.Value.CreateInstance();
						this.SoundInstanceDoubleJump.Volume = num2;
						this.SoundInstanceDoubleJump.Pan = num3;
						this.SoundInstanceDoubleJump.Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
						soundEffectInstance = this.SoundInstanceDoubleJump;
					}
					else if (type == 17)
					{
						if (this.SoundInstanceRun != null)
						{
							this.SoundInstanceRun.Stop();
						}
						this.SoundInstanceRun = this.SoundRun.Value.CreateInstance();
						this.SoundInstanceRun.Volume = num2;
						this.SoundInstanceRun.Pan = num3;
						this.SoundInstanceRun.Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
						soundEffectInstance = this.SoundInstanceRun;
					}
					else if (type == 18)
					{
						this.SoundInstanceCoins = this.SoundCoins.Value.CreateInstance();
						this.SoundInstanceCoins.Volume = num2;
						this.SoundInstanceCoins.Pan = num3;
						soundEffectInstance = this.SoundInstanceCoins;
					}
					else if (type == 19)
					{
						if (this.SoundInstanceSplash[num] == null || this.SoundInstanceSplash[num].State == SoundState.Stopped)
						{
							this.SoundInstanceSplash[num] = this.SoundSplash[num].Value.CreateInstance();
							if (num == 2 || num == 3)
							{
								num2 *= 0.75f;
							}
							if (num == 4 || num == 5)
							{
								num2 *= 0.75f;
								this.SoundInstanceSplash[num].Pitch = (float)Main.rand.Next(-20, 1) * 0.01f;
							}
							else
							{
								this.SoundInstanceSplash[num].Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
							}
							this.SoundInstanceSplash[num].Volume = num2;
							this.SoundInstanceSplash[num].Pan = num3;
							if (num == 4)
							{
								if (this.SoundInstanceSplash[5] == null || this.SoundInstanceSplash[5].State == SoundState.Stopped)
								{
									soundEffectInstance = this.SoundInstanceSplash[num];
								}
							}
							else if (num == 5)
							{
								if (this.SoundInstanceSplash[4] == null || this.SoundInstanceSplash[4].State == SoundState.Stopped)
								{
									soundEffectInstance = this.SoundInstanceSplash[num];
								}
							}
							else
							{
								soundEffectInstance = this.SoundInstanceSplash[num];
							}
						}
					}
					else if (type == 20)
					{
						int num15 = Main.rand.Next(3);
						if (this.SoundInstanceFemaleHit[num15] != null)
						{
							this.SoundInstanceFemaleHit[num15].Stop();
						}
						this.SoundInstanceFemaleHit[num15] = this.SoundFemaleHit[num15].Value.CreateInstance();
						this.SoundInstanceFemaleHit[num15].Volume = num2;
						this.SoundInstanceFemaleHit[num15].Pan = num3;
						soundEffectInstance = this.SoundInstanceFemaleHit[num15];
					}
					else if (type == 21)
					{
						int num16 = Main.rand.Next(3);
						if (this.SoundInstanceTink[num16] != null)
						{
							this.SoundInstanceTink[num16].Stop();
						}
						this.SoundInstanceTink[num16] = this.SoundTink[num16].Value.CreateInstance();
						this.SoundInstanceTink[num16].Volume = num2;
						this.SoundInstanceTink[num16].Pan = num3;
						soundEffectInstance = this.SoundInstanceTink[num16];
					}
					else if (type == 22)
					{
						if (this.SoundInstanceUnlock != null)
						{
							this.SoundInstanceUnlock.Stop();
						}
						this.SoundInstanceUnlock = this.SoundUnlock.Value.CreateInstance();
						this.SoundInstanceUnlock.Volume = num2;
						this.SoundInstanceUnlock.Pan = num3;
						soundEffectInstance = this.SoundInstanceUnlock;
					}
					else if (type == 23)
					{
						if (this.SoundInstanceDrown != null)
						{
							this.SoundInstanceDrown.Stop();
						}
						this.SoundInstanceDrown = this.SoundDrown.Value.CreateInstance();
						this.SoundInstanceDrown.Volume = num2;
						this.SoundInstanceDrown.Pan = num3;
						soundEffectInstance = this.SoundInstanceDrown;
					}
					else if (type == 24)
					{
						this.SoundInstanceChat = this.SoundChat.Value.CreateInstance();
						this.SoundInstanceChat.Volume = num2;
						this.SoundInstanceChat.Pan = num3;
						soundEffectInstance = this.SoundInstanceChat;
					}
					else if (type == 25)
					{
						this.SoundInstanceMaxMana = this.SoundMaxMana.Value.CreateInstance();
						this.SoundInstanceMaxMana.Volume = num2;
						this.SoundInstanceMaxMana.Pan = num3;
						soundEffectInstance = this.SoundInstanceMaxMana;
					}
					else if (type == 26)
					{
						int num17 = Main.rand.Next(3, 5);
						this.SoundInstanceZombie[num17] = this.SoundZombie[num17].Value.CreateInstance();
						this.SoundInstanceZombie[num17].Volume = num2 * 0.9f;
						this.SoundInstanceZombie[num17].Pan = num3;
						this.SoundInstanceZombie[num17].Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
						soundEffectInstance = this.SoundInstanceZombie[num17];
					}
					else if (type == 27)
					{
						if (this.SoundInstancePixie != null && this.SoundInstancePixie.State == SoundState.Playing)
						{
							this.SoundInstancePixie.Volume = num2;
							this.SoundInstancePixie.Pan = num3;
							this.SoundInstancePixie.Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
							return null;
						}
						if (this.SoundInstancePixie != null)
						{
							this.SoundInstancePixie.Stop();
						}
						this.SoundInstancePixie = this.SoundPixie.Value.CreateInstance();
						this.SoundInstancePixie.Volume = num2;
						this.SoundInstancePixie.Pan = num3;
						this.SoundInstancePixie.Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
						soundEffectInstance = this.SoundInstancePixie;
					}
					else if (type == 28)
					{
						if (this.SoundInstanceMech[num] != null && this.SoundInstanceMech[num].State == SoundState.Playing)
						{
							return null;
						}
						this.SoundInstanceMech[num] = this.SoundMech[num].Value.CreateInstance();
						this.SoundInstanceMech[num].Volume = num2;
						this.SoundInstanceMech[num].Pan = num3;
						this.SoundInstanceMech[num].Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
						soundEffectInstance = this.SoundInstanceMech[num];
					}
					else if (type == 29)
					{
						if (num >= 24 && num <= 87)
						{
							num2 *= 0.5f;
						}
						if (num >= 88 && num <= 91)
						{
							num2 *= 0.7f;
						}
						if (num >= 93 && num <= 99)
						{
							num2 *= 0.4f;
						}
						if (num == 92)
						{
							num2 *= 0.5f;
						}
						if (num == 103)
						{
							num2 *= 0.4f;
						}
						if (num == 104)
						{
							num2 *= 0.55f;
						}
						if (num == 100 || num == 101)
						{
							num2 *= 0.25f;
						}
						if (num == 102)
						{
							num2 *= 0.4f;
						}
						if (this.SoundInstanceZombie[num] != null && this.SoundInstanceZombie[num].State == SoundState.Playing)
						{
							return null;
						}
						this.SoundInstanceZombie[num] = this.SoundZombie[num].Value.CreateInstance();
						this.SoundInstanceZombie[num].Volume = num2;
						this.SoundInstanceZombie[num].Pan = num3;
						this.SoundInstanceZombie[num].Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
						soundEffectInstance = this.SoundInstanceZombie[num];
					}
					else if (type == 44)
					{
						num = Main.rand.Next(106, 109);
						this.SoundInstanceZombie[num] = this.SoundZombie[num].Value.CreateInstance();
						this.SoundInstanceZombie[num].Volume = num2 * 0.2f;
						this.SoundInstanceZombie[num].Pan = num3;
						this.SoundInstanceZombie[num].Pitch = (float)Main.rand.Next(-70, 1) * 0.01f;
						soundEffectInstance = this.SoundInstanceZombie[num];
					}
					else if (type == 45)
					{
						num = 109;
						if (this.SoundInstanceZombie[num] != null && this.SoundInstanceZombie[num].State == SoundState.Playing)
						{
							return null;
						}
						this.SoundInstanceZombie[num] = this.SoundZombie[num].Value.CreateInstance();
						this.SoundInstanceZombie[num].Volume = num2 * 0.3f;
						this.SoundInstanceZombie[num].Pan = num3;
						this.SoundInstanceZombie[num].Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
						soundEffectInstance = this.SoundInstanceZombie[num];
					}
					else if (type == 46)
					{
						if (this.SoundInstanceZombie[110] != null && this.SoundInstanceZombie[110].State == SoundState.Playing)
						{
							return null;
						}
						if (this.SoundInstanceZombie[111] != null && this.SoundInstanceZombie[111].State == SoundState.Playing)
						{
							return null;
						}
						num = Main.rand.Next(110, 112);
						if (Main.rand.Next(300) == 0)
						{
							if (Main.rand.Next(3) == 0)
							{
								num = 114;
							}
							else if (Main.rand.Next(2) == 0)
							{
								num = 113;
							}
							else
							{
								num = 112;
							}
						}
						this.SoundInstanceZombie[num] = this.SoundZombie[num].Value.CreateInstance();
						this.SoundInstanceZombie[num].Volume = num2 * 0.9f;
						this.SoundInstanceZombie[num].Pan = num3;
						this.SoundInstanceZombie[num].Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
						soundEffectInstance = this.SoundInstanceZombie[num];
					}
					else if (type == 45)
					{
						num = 109;
						this.SoundInstanceZombie[num] = this.SoundZombie[num].Value.CreateInstance();
						this.SoundInstanceZombie[num].Volume = num2 * 0.2f;
						this.SoundInstanceZombie[num].Pan = num3;
						this.SoundInstanceZombie[num].Pitch = (float)Main.rand.Next(-70, 1) * 0.01f;
						soundEffectInstance = this.SoundInstanceZombie[num];
					}
					else if (type == 30)
					{
						num = Main.rand.Next(10, 12);
						if (Main.rand.Next(300) == 0)
						{
							num = 12;
							if (this.SoundInstanceZombie[num] != null && this.SoundInstanceZombie[num].State == SoundState.Playing)
							{
								return null;
							}
						}
						this.SoundInstanceZombie[num] = this.SoundZombie[num].Value.CreateInstance();
						this.SoundInstanceZombie[num].Volume = num2 * 0.75f;
						this.SoundInstanceZombie[num].Pan = num3;
						if (num != 12)
						{
							this.SoundInstanceZombie[num].Pitch = (float)Main.rand.Next(-70, 1) * 0.01f;
						}
						else
						{
							this.SoundInstanceZombie[num].Pitch = (float)Main.rand.Next(-40, 21) * 0.01f;
						}
						soundEffectInstance = this.SoundInstanceZombie[num];
					}
					else if (type == 31)
					{
						num = 13;
						this.SoundInstanceZombie[num] = this.SoundZombie[num].Value.CreateInstance();
						this.SoundInstanceZombie[num].Volume = num2 * 0.35f;
						this.SoundInstanceZombie[num].Pan = num3;
						this.SoundInstanceZombie[num].Pitch = (float)Main.rand.Next(-40, 21) * 0.01f;
						soundEffectInstance = this.SoundInstanceZombie[num];
					}
					else if (type == 32)
					{
						if (this.SoundInstanceZombie[num] != null && this.SoundInstanceZombie[num].State == SoundState.Playing)
						{
							return null;
						}
						this.SoundInstanceZombie[num] = this.SoundZombie[num].Value.CreateInstance();
						this.SoundInstanceZombie[num].Volume = num2 * 0.15f;
						this.SoundInstanceZombie[num].Pan = num3;
						this.SoundInstanceZombie[num].Pitch = (float)Main.rand.Next(-70, 26) * 0.01f;
						soundEffectInstance = this.SoundInstanceZombie[num];
					}
					else if (type == 67)
					{
						num = Main.rand.Next(118, 121);
						if (this.SoundInstanceZombie[num] != null && this.SoundInstanceZombie[num].State == SoundState.Playing)
						{
							return null;
						}
						this.SoundInstanceZombie[num] = this.SoundZombie[num].Value.CreateInstance();
						this.SoundInstanceZombie[num].Volume = num2 * 0.3f;
						this.SoundInstanceZombie[num].Pan = num3;
						this.SoundInstanceZombie[num].Pitch = (float)Main.rand.Next(-5, 6) * 0.01f;
						soundEffectInstance = this.SoundInstanceZombie[num];
					}
					else if (type == 68)
					{
						num = Main.rand.Next(126, 129);
						if (this.SoundInstanceZombie[num] != null && this.SoundInstanceZombie[num].State == SoundState.Playing)
						{
							return null;
						}
						this.SoundInstanceZombie[num] = this.SoundZombie[num].Value.CreateInstance();
						this.SoundInstanceZombie[num].Volume = num2 * 0.22f;
						this.SoundInstanceZombie[num].Pan = num3;
						this.SoundInstanceZombie[num].Pitch = (float)Main.rand.Next(-5, 6) * 0.01f;
						soundEffectInstance = this.SoundInstanceZombie[num];
					}
					else if (type == 69)
					{
						num = Main.rand.Next(129, 131);
						if (this.SoundInstanceZombie[num] != null && this.SoundInstanceZombie[num].State == SoundState.Playing)
						{
							return null;
						}
						this.SoundInstanceZombie[num] = this.SoundZombie[num].Value.CreateInstance();
						this.SoundInstanceZombie[num].Volume = num2 * 0.2f;
						this.SoundInstanceZombie[num].Pan = num3;
						this.SoundInstanceZombie[num].Pitch = (float)Main.rand.Next(-5, 6) * 0.01f;
						soundEffectInstance = this.SoundInstanceZombie[num];
					}
					else if (type == 66)
					{
						num = Main.rand.Next(121, 124);
						if (this.SoundInstanceZombie[121] != null && this.SoundInstanceZombie[121].State == SoundState.Playing)
						{
							return null;
						}
						if (this.SoundInstanceZombie[122] != null && this.SoundInstanceZombie[122].State == SoundState.Playing)
						{
							return null;
						}
						if (this.SoundInstanceZombie[123] != null && this.SoundInstanceZombie[123].State == SoundState.Playing)
						{
							return null;
						}
						this.SoundInstanceZombie[num] = this.SoundZombie[num].Value.CreateInstance();
						this.SoundInstanceZombie[num].Volume = num2 * 0.45f;
						this.SoundInstanceZombie[num].Pan = num3;
						this.SoundInstanceZombie[num].Pitch = (float)Main.rand.Next(-15, 16) * 0.01f;
						soundEffectInstance = this.SoundInstanceZombie[num];
					}
					else if (type == 33)
					{
						num = 15;
						if (this.SoundInstanceZombie[num] != null && this.SoundInstanceZombie[num].State == SoundState.Playing)
						{
							return null;
						}
						this.SoundInstanceZombie[num] = this.SoundZombie[num].Value.CreateInstance();
						this.SoundInstanceZombie[num].Volume = num2 * 0.2f;
						this.SoundInstanceZombie[num].Pan = num3;
						this.SoundInstanceZombie[num].Pitch = (float)Main.rand.Next(-10, 31) * 0.01f;
						soundEffectInstance = this.SoundInstanceZombie[num];
					}
					else if (type >= 47 && type <= 52)
					{
						num = 133 + type - 47;
						for (int i = 133; i <= 138; i++)
						{
							if (this.SoundInstanceItem[i] != null && this.SoundInstanceItem[i].State == SoundState.Playing)
							{
								this.SoundInstanceItem[i].Stop();
							}
						}
						this.SoundInstanceItem[num] = this.SoundItem[num].Value.CreateInstance();
						this.SoundInstanceItem[num].Volume = num2 * 0.45f;
						this.SoundInstanceItem[num].Pan = num3;
						soundEffectInstance = this.SoundInstanceItem[num];
					}
					else if (type >= 53 && type <= 62)
					{
						num = 139 + type - 53;
						if (this.SoundInstanceItem[num] != null && this.SoundInstanceItem[num].State == SoundState.Playing)
						{
							this.SoundInstanceItem[num].Stop();
						}
						this.SoundInstanceItem[num] = this.SoundItem[num].Value.CreateInstance();
						this.SoundInstanceItem[num].Volume = num2 * 0.7f;
						this.SoundInstanceItem[num].Pan = num3;
						soundEffectInstance = this.SoundInstanceItem[num];
					}
					else if (type == 34)
					{
						float num18 = (float)num / 50f;
						if (num18 > 1f)
						{
							num18 = 1f;
						}
						num2 *= num18;
						num2 *= 0.2f;
						num2 *= 1f - Main.shimmerAlpha;
						if (num2 <= 0f || x == -1 || y == -1)
						{
							if (this.SoundInstanceLiquid[0] != null && this.SoundInstanceLiquid[0].State == SoundState.Playing)
							{
								this.SoundInstanceLiquid[0].Stop();
							}
						}
						else if (this.SoundInstanceLiquid[0] != null && this.SoundInstanceLiquid[0].State == SoundState.Playing)
						{
							this.SoundInstanceLiquid[0].Volume = num2;
							this.SoundInstanceLiquid[0].Pan = num3;
							this.SoundInstanceLiquid[0].Pitch = -0.2f;
						}
						else
						{
							this.SoundInstanceLiquid[0] = this.SoundLiquid[0].Value.CreateInstance();
							this.SoundInstanceLiquid[0].Volume = num2;
							this.SoundInstanceLiquid[0].Pan = num3;
							soundEffectInstance = this.SoundInstanceLiquid[0];
						}
					}
					else if (type == 35)
					{
						float num19 = (float)num / 50f;
						if (num19 > 1f)
						{
							num19 = 1f;
						}
						num2 *= num19;
						num2 *= 0.65f;
						num2 *= 1f - Main.shimmerAlpha;
						if (num2 <= 0f || x == -1 || y == -1)
						{
							if (this.SoundInstanceLiquid[1] != null && this.SoundInstanceLiquid[1].State == SoundState.Playing)
							{
								this.SoundInstanceLiquid[1].Stop();
							}
						}
						else if (this.SoundInstanceLiquid[1] != null && this.SoundInstanceLiquid[1].State == SoundState.Playing)
						{
							this.SoundInstanceLiquid[1].Volume = num2;
							this.SoundInstanceLiquid[1].Pan = num3;
							this.SoundInstanceLiquid[1].Pitch = -0f;
						}
						else
						{
							this.SoundInstanceLiquid[1] = this.SoundLiquid[1].Value.CreateInstance();
							this.SoundInstanceLiquid[1].Volume = num2;
							this.SoundInstanceLiquid[1].Pan = num3;
							soundEffectInstance = this.SoundInstanceLiquid[1];
						}
					}
					else if (type == 36)
					{
						int num20 = Style;
						if (Style == -1)
						{
							num20 = 0;
						}
						this.SoundInstanceRoar[num20] = this.SoundRoar[num20].Value.CreateInstance();
						this.SoundInstanceRoar[num20].Volume = num2;
						this.SoundInstanceRoar[num20].Pan = num3;
						if (Style == -1)
						{
							this.SoundInstanceRoar[num20].Pitch += 0.6f;
						}
						soundEffectInstance = this.SoundInstanceRoar[num20];
					}
					else if (type == 37)
					{
						int num21 = Main.rand.Next(57, 59);
						if (Main.starGame)
						{
							num2 *= 0.15f;
						}
						else
						{
							num2 *= (float)Style * 0.05f;
						}
						this.SoundInstanceItem[num21] = this.SoundItem[num21].Value.CreateInstance();
						this.SoundInstanceItem[num21].Volume = num2;
						this.SoundInstanceItem[num21].Pan = num3;
						this.SoundInstanceItem[num21].Pitch = (float)Main.rand.Next(-40, 41) * 0.01f;
						soundEffectInstance = this.SoundInstanceItem[num21];
					}
					else if (type == 38)
					{
						if (Main.starGame)
						{
							num2 *= 0.15f;
						}
						int num22 = Main.rand.Next(5);
						this.SoundInstanceCoin[num22] = this.SoundCoin[num22].Value.CreateInstance();
						this.SoundInstanceCoin[num22].Volume = num2;
						this.SoundInstanceCoin[num22].Pan = num3;
						this.SoundInstanceCoin[num22].Pitch = (float)Main.rand.Next(-40, 41) * 0.002f;
						soundEffectInstance = this.SoundInstanceCoin[num22];
					}
					else if (type == 39)
					{
						this.SoundInstanceDrip[Style] = this.SoundDrip[Style].Value.CreateInstance();
						this.SoundInstanceDrip[Style].Volume = num2 * 0.5f;
						this.SoundInstanceDrip[Style].Pan = num3;
						this.SoundInstanceDrip[Style].Pitch = (float)Main.rand.Next(-30, 31) * 0.01f;
						soundEffectInstance = this.SoundInstanceDrip[Style];
					}
					else if (type == 40)
					{
						if (this.SoundInstanceCamera != null)
						{
							this.SoundInstanceCamera.Stop();
						}
						this.SoundInstanceCamera = this.SoundCamera.Value.CreateInstance();
						this.SoundInstanceCamera.Volume = num2;
						this.SoundInstanceCamera.Pan = num3;
						soundEffectInstance = this.SoundInstanceCamera;
					}
					else if (type == 41)
					{
						this.SoundInstanceMoonlordCry = this.SoundNpcKilled[10].Value.CreateInstance();
						this.SoundInstanceMoonlordCry.Volume = 1f / (1f + (new Vector2((float)x, (float)y) - Main.player[Main.myPlayer].position).Length());
						this.SoundInstanceMoonlordCry.Pan = num3;
						this.SoundInstanceMoonlordCry.Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
						soundEffectInstance = this.SoundInstanceMoonlordCry;
					}
					else if (type == 42)
					{
						soundEffectInstance = this.TrackableSounds[num].Value.CreateInstance();
						soundEffectInstance.Volume = num2;
						soundEffectInstance.Pan = num3;
						this.TrackableSoundInstances[num] = soundEffectInstance;
					}
					else if (type == 65)
					{
						if (this.SoundInstanceZombie[115] != null && this.SoundInstanceZombie[115].State == SoundState.Playing)
						{
							return null;
						}
						if (this.SoundInstanceZombie[116] != null && this.SoundInstanceZombie[116].State == SoundState.Playing)
						{
							return null;
						}
						if (this.SoundInstanceZombie[117] != null && this.SoundInstanceZombie[117].State == SoundState.Playing)
						{
							return null;
						}
						int num23 = Main.rand.Next(115, 118);
						this.SoundInstanceZombie[num23] = this.SoundZombie[num23].Value.CreateInstance();
						this.SoundInstanceZombie[num23].Volume = num2 * 0.5f;
						this.SoundInstanceZombie[num23].Pan = num3;
						soundEffectInstance = this.SoundInstanceZombie[num23];
					}
					if (soundEffectInstance != null)
					{
						soundEffectInstance.Pitch = MathHelper.Clamp(soundEffectInstance.Pitch + pitchOffset, -1f, 1f);
						soundEffectInstance.Volume *= volumeScale;
						soundEffectInstance.Play();
						SoundInstanceGarbageCollector.Track(soundEffectInstance);
					}
					return soundEffectInstance;
				}
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x06003AAA RID: 15018 RVA: 0x00658F2C File Offset: 0x0065712C
		public SoundEffect GetTrackableSoundByStyleId(int id)
		{
			return this.TrackableSounds[id].Value;
		}

		// Token: 0x06003AAB RID: 15019 RVA: 0x00658F3C File Offset: 0x0065713C
		public void StopAmbientSounds()
		{
			for (int i = 0; i < this.SoundInstanceLiquid.Length; i++)
			{
				if (this.SoundInstanceLiquid[i] != null)
				{
					this.SoundInstanceLiquid[i].Stop();
				}
			}
		}

		// Token: 0x06003AAC RID: 15020 RVA: 0x00658F74 File Offset: 0x00657174
		public bool DoesSoundScaleWithAmbientVolume(int soundType)
		{
			switch (soundType)
			{
			case 30:
			case 31:
			case 32:
			case 33:
			case 34:
			case 35:
			case 39:
			case 43:
			case 44:
			case 45:
			case 46:
				break;
			case 36:
			case 37:
			case 38:
			case 40:
			case 41:
			case 42:
				return false;
			default:
				if (soundType - 67 > 2)
				{
					return false;
				}
				break;
			}
			return true;
		}

		// Token: 0x06003AAD RID: 15021 RVA: 0x00658FD8 File Offset: 0x006571D8
		// Note: this type is marked as 'beforefieldinit'.
		static LegacySoundPlayer()
		{
		}

		// Token: 0x04005DF1 RID: 24049
		public Asset<SoundEffect>[] SoundDrip = new Asset<SoundEffect>[3];

		// Token: 0x04005DF2 RID: 24050
		public SoundEffectInstance[] SoundInstanceDrip = new SoundEffectInstance[3];

		// Token: 0x04005DF3 RID: 24051
		public Asset<SoundEffect>[] SoundLiquid = new Asset<SoundEffect>[2];

		// Token: 0x04005DF4 RID: 24052
		public SoundEffectInstance[] SoundInstanceLiquid = new SoundEffectInstance[2];

		// Token: 0x04005DF5 RID: 24053
		public Asset<SoundEffect>[] SoundMech = new Asset<SoundEffect>[1];

		// Token: 0x04005DF6 RID: 24054
		public SoundEffectInstance[] SoundInstanceMech = new SoundEffectInstance[1];

		// Token: 0x04005DF7 RID: 24055
		public Asset<SoundEffect>[] SoundDig = new Asset<SoundEffect>[3];

		// Token: 0x04005DF8 RID: 24056
		public SoundEffectInstance[] SoundInstanceDig = new SoundEffectInstance[3];

		// Token: 0x04005DF9 RID: 24057
		public Asset<SoundEffect>[] SoundThunder = new Asset<SoundEffect>[6];

		// Token: 0x04005DFA RID: 24058
		public SoundEffectInstance[] SoundInstanceThunder = new SoundEffectInstance[6];

		// Token: 0x04005DFB RID: 24059
		public Asset<SoundEffect>[] SoundResearch = new Asset<SoundEffect>[4];

		// Token: 0x04005DFC RID: 24060
		public SoundEffectInstance[] SoundInstanceResearch = new SoundEffectInstance[4];

		// Token: 0x04005DFD RID: 24061
		public Asset<SoundEffect>[] SoundTink = new Asset<SoundEffect>[3];

		// Token: 0x04005DFE RID: 24062
		public SoundEffectInstance[] SoundInstanceTink = new SoundEffectInstance[3];

		// Token: 0x04005DFF RID: 24063
		public Asset<SoundEffect>[] SoundCoin = new Asset<SoundEffect>[5];

		// Token: 0x04005E00 RID: 24064
		public SoundEffectInstance[] SoundInstanceCoin = new SoundEffectInstance[5];

		// Token: 0x04005E01 RID: 24065
		public Asset<SoundEffect>[] SoundPlayerHit = new Asset<SoundEffect>[3];

		// Token: 0x04005E02 RID: 24066
		public SoundEffectInstance[] SoundInstancePlayerHit = new SoundEffectInstance[3];

		// Token: 0x04005E03 RID: 24067
		public Asset<SoundEffect>[] SoundFemaleHit = new Asset<SoundEffect>[3];

		// Token: 0x04005E04 RID: 24068
		public SoundEffectInstance[] SoundInstanceFemaleHit = new SoundEffectInstance[3];

		// Token: 0x04005E05 RID: 24069
		public Asset<SoundEffect> SoundPlayerKilled;

		// Token: 0x04005E06 RID: 24070
		public SoundEffectInstance SoundInstancePlayerKilled;

		// Token: 0x04005E07 RID: 24071
		public Asset<SoundEffect> SoundGrass;

		// Token: 0x04005E08 RID: 24072
		public SoundEffectInstance SoundInstanceGrass;

		// Token: 0x04005E09 RID: 24073
		public Asset<SoundEffect> SoundGrab;

		// Token: 0x04005E0A RID: 24074
		public SoundEffectInstance SoundInstanceGrab;

		// Token: 0x04005E0B RID: 24075
		public Asset<SoundEffect> SoundPixie;

		// Token: 0x04005E0C RID: 24076
		public SoundEffectInstance SoundInstancePixie;

		// Token: 0x04005E0D RID: 24077
		public Asset<SoundEffect>[] SoundItem = new Asset<SoundEffect>[(int)SoundID.ItemSoundCount];

		// Token: 0x04005E0E RID: 24078
		public SoundEffectInstance[] SoundInstanceItem = new SoundEffectInstance[(int)SoundID.ItemSoundCount];

		// Token: 0x04005E0F RID: 24079
		public Asset<SoundEffect>[] SoundNpcHit = new Asset<SoundEffect>[59];

		// Token: 0x04005E10 RID: 24080
		public SoundEffectInstance[] SoundInstanceNpcHit = new SoundEffectInstance[59];

		// Token: 0x04005E11 RID: 24081
		public Asset<SoundEffect>[] SoundNpcKilled = new Asset<SoundEffect>[(int)SoundID.NPCDeathCount];

		// Token: 0x04005E12 RID: 24082
		public SoundEffectInstance[] SoundInstanceNpcKilled = new SoundEffectInstance[(int)SoundID.NPCDeathCount];

		// Token: 0x04005E13 RID: 24083
		public SoundEffectInstance SoundInstanceMoonlordCry;

		// Token: 0x04005E14 RID: 24084
		public Asset<SoundEffect> SoundDoorOpen;

		// Token: 0x04005E15 RID: 24085
		public SoundEffectInstance SoundInstanceDoorOpen;

		// Token: 0x04005E16 RID: 24086
		public Asset<SoundEffect> SoundDoorClosed;

		// Token: 0x04005E17 RID: 24087
		public SoundEffectInstance SoundInstanceDoorClosed;

		// Token: 0x04005E18 RID: 24088
		public Asset<SoundEffect> SoundMenuOpen;

		// Token: 0x04005E19 RID: 24089
		public SoundEffectInstance SoundInstanceMenuOpen;

		// Token: 0x04005E1A RID: 24090
		public Asset<SoundEffect> SoundMenuClose;

		// Token: 0x04005E1B RID: 24091
		public SoundEffectInstance SoundInstanceMenuClose;

		// Token: 0x04005E1C RID: 24092
		public Asset<SoundEffect> SoundMenuTick;

		// Token: 0x04005E1D RID: 24093
		public SoundEffectInstance SoundInstanceMenuTick;

		// Token: 0x04005E1E RID: 24094
		public Asset<SoundEffect> SoundShatter;

		// Token: 0x04005E1F RID: 24095
		public SoundEffectInstance SoundInstanceShatter;

		// Token: 0x04005E20 RID: 24096
		public Asset<SoundEffect> SoundCamera;

		// Token: 0x04005E21 RID: 24097
		public SoundEffectInstance SoundInstanceCamera;

		// Token: 0x04005E22 RID: 24098
		public Asset<SoundEffect>[] SoundZombie = new Asset<SoundEffect>[131];

		// Token: 0x04005E23 RID: 24099
		public SoundEffectInstance[] SoundInstanceZombie = new SoundEffectInstance[131];

		// Token: 0x04005E24 RID: 24100
		public Asset<SoundEffect>[] SoundRoar = new Asset<SoundEffect>[3];

		// Token: 0x04005E25 RID: 24101
		public SoundEffectInstance[] SoundInstanceRoar = new SoundEffectInstance[3];

		// Token: 0x04005E26 RID: 24102
		public Asset<SoundEffect>[] SoundSplash = new Asset<SoundEffect>[6];

		// Token: 0x04005E27 RID: 24103
		public SoundEffectInstance[] SoundInstanceSplash = new SoundEffectInstance[6];

		// Token: 0x04005E28 RID: 24104
		public Asset<SoundEffect> SoundDoubleJump;

		// Token: 0x04005E29 RID: 24105
		public SoundEffectInstance SoundInstanceDoubleJump;

		// Token: 0x04005E2A RID: 24106
		public Asset<SoundEffect> SoundRun;

		// Token: 0x04005E2B RID: 24107
		public SoundEffectInstance SoundInstanceRun;

		// Token: 0x04005E2C RID: 24108
		public Asset<SoundEffect> SoundCoins;

		// Token: 0x04005E2D RID: 24109
		public SoundEffectInstance SoundInstanceCoins;

		// Token: 0x04005E2E RID: 24110
		public Asset<SoundEffect> SoundUnlock;

		// Token: 0x04005E2F RID: 24111
		public SoundEffectInstance SoundInstanceUnlock;

		// Token: 0x04005E30 RID: 24112
		public Asset<SoundEffect> SoundChat;

		// Token: 0x04005E31 RID: 24113
		public SoundEffectInstance SoundInstanceChat;

		// Token: 0x04005E32 RID: 24114
		public Asset<SoundEffect> SoundMaxMana;

		// Token: 0x04005E33 RID: 24115
		public SoundEffectInstance SoundInstanceMaxMana;

		// Token: 0x04005E34 RID: 24116
		public Asset<SoundEffect> SoundDrown;

		// Token: 0x04005E35 RID: 24117
		public SoundEffectInstance SoundInstanceDrown;

		// Token: 0x04005E36 RID: 24118
		public Asset<SoundEffect>[] TrackableSounds;

		// Token: 0x04005E37 RID: 24119
		public SoundEffectInstance[] TrackableSoundInstances;

		// Token: 0x04005E38 RID: 24120
		private readonly IServiceProvider _services;

		// Token: 0x04005E39 RID: 24121
		private List<SoundEffectInstance> _trackedInstances;

		// Token: 0x04005E3A RID: 24122
		public static readonly float SoundAttenuationDistance = 2500f;
	}
}
