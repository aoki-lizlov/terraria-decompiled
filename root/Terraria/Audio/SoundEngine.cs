using System;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using ReLogic.Utilities;

namespace Terraria.Audio
{
	// Token: 0x020005DF RID: 1503
	public static class SoundEngine
	{
		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06003AF2 RID: 15090 RVA: 0x00659C33 File Offset: 0x00657E33
		// (set) Token: 0x06003AF3 RID: 15091 RVA: 0x00659C3A File Offset: 0x00657E3A
		public static bool IsAudioSupported
		{
			[CompilerGenerated]
			get
			{
				return SoundEngine.<IsAudioSupported>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				SoundEngine.<IsAudioSupported>k__BackingField = value;
			}
		}

		// Token: 0x06003AF4 RID: 15092 RVA: 0x00659C44 File Offset: 0x00657E44
		public static IAudioSystem Initialize()
		{
			SoundEngine.IsAudioSupported = SoundEngine.TestAudioSupport();
			try
			{
				if (SoundEngine.IsAudioSupported)
				{
					return new LegacyAudioSystem();
				}
			}
			catch (Exception)
			{
				SoundEngine.IsAudioSupported = false;
			}
			return new DisabledAudioSystem();
		}

		// Token: 0x06003AF5 RID: 15093 RVA: 0x00659C8C File Offset: 0x00657E8C
		public static void Load(IServiceProvider services)
		{
			if (!SoundEngine.IsAudioSupported)
			{
				return;
			}
			SoundEngine.LegacySoundPlayer = new LegacySoundPlayer(services);
			SoundEngine.SoundPlayer = new SoundPlayer();
		}

		// Token: 0x06003AF6 RID: 15094 RVA: 0x00659CAC File Offset: 0x00657EAC
		public static void Update()
		{
			if (!SoundEngine.IsAudioSupported)
			{
				return;
			}
			if (Main.audioSystem != null)
			{
				Main.audioSystem.UpdateAudioEngine();
			}
			SoundInstanceGarbageCollector.Update();
			bool pauseSounds = FocusHelper.PauseSounds;
			if (!SoundEngine.AreSoundsPaused && pauseSounds)
			{
				SoundEngine.SoundPlayer.PauseAll();
			}
			else if (SoundEngine.AreSoundsPaused && !pauseSounds)
			{
				SoundEngine.SoundPlayer.ResumeAll();
			}
			SoundEngine.AreSoundsPaused = pauseSounds;
			SoundEngine.SoundPlayer.Update();
		}

		// Token: 0x06003AF7 RID: 15095 RVA: 0x00659D19 File Offset: 0x00657F19
		public static void Reload()
		{
			if (!SoundEngine.IsAudioSupported)
			{
				return;
			}
			if (SoundEngine.LegacySoundPlayer != null)
			{
				SoundEngine.LegacySoundPlayer.Reload();
			}
			if (SoundEngine.SoundPlayer != null)
			{
				SoundEngine.SoundPlayer.Reload();
			}
		}

		// Token: 0x06003AF8 RID: 15096 RVA: 0x00659D45 File Offset: 0x00657F45
		public static void PlaySound(int type, Vector2 position, int style = 1, float pitchOffset = 0f)
		{
			SoundEngine.PlaySound(type, (int)position.X, (int)position.Y, style, 1f, pitchOffset);
		}

		// Token: 0x06003AF9 RID: 15097 RVA: 0x00659D63 File Offset: 0x00657F63
		public static SoundEffectInstance PlaySound(LegacySoundStyle type, Vector2 position, float pitchOffset = 0f, float volumeScale = 1f)
		{
			return SoundEngine.PlaySound(type, (int)position.X, (int)position.Y, pitchOffset, volumeScale);
		}

		// Token: 0x06003AFA RID: 15098 RVA: 0x00659D7B File Offset: 0x00657F7B
		public static SoundEffectInstance PlaySound(LegacySoundStyle type, int x = -1, int y = -1, float pitchOffset = 0f, float volumeScale = 1f)
		{
			if (type == null)
			{
				return null;
			}
			return SoundEngine.PlaySound(type.SoundId, x, y, type.Style, type.Volume * volumeScale, pitchOffset + type.GetRandomPitch());
		}

		// Token: 0x06003AFB RID: 15099 RVA: 0x00659DA6 File Offset: 0x00657FA6
		public static SoundEffectInstance PlaySound(int type, int x = -1, int y = -1, int Style = 1, float volumeScale = 1f, float pitchOffset = 0f)
		{
			if (Main.dedServ || !SoundEngine.IsAudioSupported)
			{
				return null;
			}
			return SoundEngine.LegacySoundPlayer.PlaySound(type, x, y, Style, volumeScale, pitchOffset);
		}

		// Token: 0x06003AFC RID: 15100 RVA: 0x00659DCA File Offset: 0x00657FCA
		public static ActiveSound GetActiveSound(SlotId id)
		{
			if (Main.dedServ || !SoundEngine.IsAudioSupported)
			{
				return null;
			}
			return SoundEngine.SoundPlayer.GetActiveSound(id);
		}

		// Token: 0x06003AFD RID: 15101 RVA: 0x00659DE8 File Offset: 0x00657FE8
		public static SlotId PlayTrackedSound(SoundStyle style, Vector2 position, SoundPlayOverrides overrides = default(SoundPlayOverrides))
		{
			if (Main.dedServ || !SoundEngine.IsAudioSupported)
			{
				return SlotId.Invalid;
			}
			if (style.MaxTrackedInstances > 0 && SoundEngine.SoundPlayer.GetActiveSoundCount(style) >= style.MaxTrackedInstances)
			{
				return SlotId.Invalid;
			}
			return SoundEngine.SoundPlayer.Play(style, position, overrides);
		}

		// Token: 0x06003AFE RID: 15102 RVA: 0x00659E38 File Offset: 0x00658038
		public static SlotId PlayTrackedLoopedSound(SoundStyle style, Vector2 position, ActiveSound.LoopedPlayCondition loopingCondition = null, SoundPlayOverrides overrides = default(SoundPlayOverrides))
		{
			if (Main.dedServ || !SoundEngine.IsAudioSupported)
			{
				return SlotId.Invalid;
			}
			return SoundEngine.SoundPlayer.PlayLooped(style, position, loopingCondition, overrides);
		}

		// Token: 0x06003AFF RID: 15103 RVA: 0x00659E5C File Offset: 0x0065805C
		public static SlotId PlayTrackedSound(SoundStyle style)
		{
			if (Main.dedServ || !SoundEngine.IsAudioSupported)
			{
				return SlotId.Invalid;
			}
			return SoundEngine.SoundPlayer.Play(style);
		}

		// Token: 0x06003B00 RID: 15104 RVA: 0x00659E7D File Offset: 0x0065807D
		public static void StopTrackedSounds()
		{
			if (!Main.dedServ && SoundEngine.IsAudioSupported)
			{
				SoundEngine.SoundPlayer.StopAll();
			}
		}

		// Token: 0x06003B01 RID: 15105 RVA: 0x00659E97 File Offset: 0x00658097
		public static SoundEffect GetTrackableSoundByStyleId(int id)
		{
			if (Main.dedServ || !SoundEngine.IsAudioSupported)
			{
				return null;
			}
			return SoundEngine.LegacySoundPlayer.GetTrackableSoundByStyleId(id);
		}

		// Token: 0x06003B02 RID: 15106 RVA: 0x00659EB4 File Offset: 0x006580B4
		public static void StopAmbientSounds()
		{
			if (Main.dedServ || !SoundEngine.IsAudioSupported)
			{
				return;
			}
			if (SoundEngine.LegacySoundPlayer != null)
			{
				SoundEngine.LegacySoundPlayer.StopAmbientSounds();
			}
		}

		// Token: 0x06003B03 RID: 15107 RVA: 0x00659ED6 File Offset: 0x006580D6
		public static ActiveSound FindActiveSound(SoundStyle style)
		{
			if (Main.dedServ || !SoundEngine.IsAudioSupported)
			{
				return null;
			}
			return SoundEngine.SoundPlayer.FindActiveSound(style);
		}

		// Token: 0x06003B04 RID: 15108 RVA: 0x00659EF4 File Offset: 0x006580F4
		private static bool TestAudioSupport()
		{
			byte[] array = new byte[]
			{
				82, 73, 70, 70, 158, 0, 0, 0, 87, 65,
				86, 69, 102, 109, 116, 32, 16, 0, 0, 0,
				1, 0, 1, 0, 68, 172, 0, 0, 136, 88,
				1, 0, 2, 0, 16, 0, 76, 73, 83, 84,
				26, 0, 0, 0, 73, 78, 70, 79, 73, 83,
				70, 84, 14, 0, 0, 0, 76, 97, 118, 102,
				53, 54, 46, 52, 48, 46, 49, 48, 49, 0,
				100, 97, 116, 97, 88, 0, 0, 0, 0, 0,
				126, 4, 240, 8, 64, 13, 95, 17, 67, 21,
				217, 24, 23, 28, 240, 30, 94, 33, 84, 35,
				208, 36, 204, 37, 71, 38, 64, 38, 183, 37,
				180, 36, 58, 35, 79, 33, 1, 31, 86, 28,
				92, 25, 37, 22, 185, 18, 42, 15, 134, 11,
				222, 7, 68, 4, 196, 0, 112, 253, 86, 250,
				132, 247, 6, 245, 230, 242, 47, 241, 232, 239,
				25, 239, 194, 238, 231, 238, 139, 239, 169, 240,
				61, 242, 67, 244, 180, 246
			};
			try
			{
				using (MemoryStream memoryStream = new MemoryStream(array))
				{
					SoundEffect.FromStream(memoryStream);
				}
			}
			catch (NoAudioHardwareException)
			{
				Console.WriteLine("No audio hardware found. Disabling all audio.");
				return false;
			}
			catch
			{
				return false;
			}
			return true;
		}

		// Token: 0x04005E57 RID: 24151
		public static LegacySoundPlayer LegacySoundPlayer;

		// Token: 0x04005E58 RID: 24152
		public static SoundPlayer SoundPlayer;

		// Token: 0x04005E59 RID: 24153
		public static bool AreSoundsPaused;

		// Token: 0x04005E5A RID: 24154
		[CompilerGenerated]
		private static bool <IsAudioSupported>k__BackingField;
	}
}
