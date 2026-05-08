using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using ReLogic.Utilities;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace Terraria.IO
{
	// Token: 0x02000071 RID: 113
	public class WorldFileData : FileData
	{
		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060014E9 RID: 5353 RVA: 0x004BCB20 File Offset: 0x004BAD20
		public string SeedText
		{
			get
			{
				return this._seedText;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060014EA RID: 5354 RVA: 0x004BCB28 File Offset: 0x004BAD28
		public int Seed
		{
			get
			{
				return this._seed;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060014EB RID: 5355 RVA: 0x004BCB30 File Offset: 0x004BAD30
		public bool IsValid
		{
			get
			{
				return this.LoadStatus == StatusID.Ok;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060014EC RID: 5356 RVA: 0x004BCB3F File Offset: 0x004BAD3F
		public string WorldSizeName
		{
			get
			{
				return this._worldSizeName.Value;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060014ED RID: 5357 RVA: 0x004BCB4C File Offset: 0x004BAD4C
		// (set) Token: 0x060014EE RID: 5358 RVA: 0x004BCB57 File Offset: 0x004BAD57
		public bool HasCrimson
		{
			get
			{
				return !this.HasCorruption;
			}
			set
			{
				this.HasCorruption = !value;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060014EF RID: 5359 RVA: 0x004BCB63 File Offset: 0x004BAD63
		public bool HasValidSeed
		{
			get
			{
				return this.WorldGeneratorVersion > 0UL;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060014F0 RID: 5360 RVA: 0x004BCB6F File Offset: 0x004BAD6F
		public bool UseGuidAsMapName
		{
			get
			{
				return this.WorldGeneratorVersion >= 777389080577UL;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060014F1 RID: 5361 RVA: 0x004BCB85 File Offset: 0x004BAD85
		public string MapFileName
		{
			get
			{
				if (!this.UseGuidAsMapName)
				{
					return this.WorldId.ToString();
				}
				return this.UniqueId.ToString();
			}
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x004BCBAC File Offset: 0x004BADAC
		public string GetWorldName(bool allowCropping = false)
		{
			string text = this.Name;
			if (text == null)
			{
				return text;
			}
			if (allowCropping)
			{
				int num = 494;
				text = FontAssets.MouseText.Value.CreateCroppedText(text, (float)num);
			}
			return text;
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x004BCBE4 File Offset: 0x004BADE4
		public string GetFullSeedText(bool allowCropping = false)
		{
			int num = 0;
			if (this.WorldSizeX == 4200 && this.WorldSizeY == 1200)
			{
				num = 1;
			}
			if (this.WorldSizeX == 6400 && this.WorldSizeY == 1800)
			{
				num = 2;
			}
			if (this.WorldSizeX == 8400 && this.WorldSizeY == 2400)
			{
				num = 3;
			}
			int num2 = 0;
			if (this.HasCorruption)
			{
				num2 = 1;
			}
			if (this.HasCrimson)
			{
				num2 = 2;
			}
			int num3 = this.GameMode + 1;
			string text = this._seedText;
			if (allowCropping)
			{
				int num4 = 340;
				text = FontAssets.MouseText.Value.CreateCroppedText(text, (float)num4);
			}
			int serializedSeedsSum = this.GetSerializedSeedsSum();
			return string.Format("{0}.{1}.{2}.{3}.{4}", new object[] { num, num3, num2, serializedSeedsSum, text });
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x004BCCCC File Offset: 0x004BAECC
		public int GetSerializedSeedsSum()
		{
			int num = 0;
			if (this.DrunkWorld)
			{
				num++;
			}
			if (this.NotTheBees)
			{
				num += 2;
			}
			if (this.ForTheWorthy)
			{
				num += 4;
			}
			if (this.Anniversary)
			{
				num += 8;
			}
			if (this.DontStarve)
			{
				num += 16;
			}
			if (this.RemixWorld)
			{
				num += 32;
			}
			if (this.NoTrapsWorld)
			{
				num += 64;
			}
			if (this.ZenithWorld)
			{
				num += 128;
			}
			if (this.SkyblockWorld)
			{
				num += 256;
			}
			return num;
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x004BCD53 File Offset: 0x004BAF53
		public List<string> GetSecretSeedCodes()
		{
			if (string.IsNullOrWhiteSpace(this._seedText))
			{
				return new List<string>();
			}
			return this._seedText.Split(new char[] { '|' }).ToList<string>();
		}

		// Token: 0x060014F6 RID: 5366 RVA: 0x004BCD84 File Offset: 0x004BAF84
		private static void EnableSeedOptions(int serializedSeedSum)
		{
			for (int i = 0; i < WorldFileData.seedOptionsInOrder.Count; i++)
			{
				if (((serializedSeedSum >> i) & 1) == 1)
				{
					WorldFileData.seedOptionsInOrder[i].Enabled = true;
				}
			}
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x004BCDC4 File Offset: 0x004BAFC4
		public static bool TryApplyingCopiedSeed(string input, bool playSound, out string processedSeed, out string seedTextIncludingSecrets, out List<string> secretSeedTexts)
		{
			processedSeed = input;
			seedTextIncludingSecrets = input;
			secretSeedTexts = null;
			if (string.IsNullOrWhiteSpace(input))
			{
				return false;
			}
			int num;
			int num2;
			int num3;
			if (!WorldFileData.TryParseSeedOptionValue(ref processedSeed, out num) || !WorldFileData.TryParseSeedOptionValue(ref processedSeed, out num2) || !WorldFileData.TryParseSeedOptionValue(ref processedSeed, out num3))
			{
				return false;
			}
			if (num <= 0 || num > 3)
			{
				return false;
			}
			if (num2 <= 0 || num2 > 4)
			{
				return false;
			}
			if (num3 <= 0 || num3 > 2)
			{
				return false;
			}
			int num4;
			if (!WorldFileData.TryParseSeedOptionValue(ref processedSeed, out num4))
			{
				num4 = 0;
			}
			seedTextIncludingSecrets = processedSeed;
			secretSeedTexts = new List<string>();
			List<WorldGen.SecretSeed> list = new List<WorldGen.SecretSeed>();
			string text;
			WorldGen.SecretSeed secretSeed;
			while (WorldFileData.TryParseSecretSeed(ref processedSeed, out text, out secretSeed))
			{
				secretSeedTexts.Add(text);
				list.Add(secretSeed);
			}
			if (processedSeed.Length > WorldFileData.MAX_USER_SEED_TEXT_LENGTH)
			{
				return false;
			}
			WorldGen.SetWorldSize(num - 1);
			Main.GameMode = num2 - 1;
			WorldGen.WorldGenParam_Evil = num3 - 1;
			WorldGenerationOptions.Reset();
			WorldFileData.EnableSeedOptions(num4);
			WorldGen.SecretSeed.ClearAllSeeds();
			foreach (WorldGen.SecretSeed secretSeed2 in list)
			{
				WorldGen.SecretSeed.Enable(secretSeed2, playSound);
				playSound = false;
			}
			return true;
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x004BCEE0 File Offset: 0x004BB0E0
		private static bool TryParseSeedOptionValue(ref string processedSeed, out int value)
		{
			int num = processedSeed.IndexOf('.');
			if (num < 0)
			{
				value = 0;
				return false;
			}
			if (!int.TryParse(processedSeed.Substring(0, num), out value))
			{
				return false;
			}
			processedSeed = processedSeed.Substring(num + 1);
			return true;
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x004BCF20 File Offset: 0x004BB120
		private static bool TryParseSecretSeed(ref string processedSeed, out string secretSeedText, out WorldGen.SecretSeed secretSeed)
		{
			int num = processedSeed.IndexOf('|');
			if (num < 0)
			{
				secretSeedText = null;
				secretSeed = null;
				return false;
			}
			secretSeedText = processedSeed.Substring(0, num);
			if (!WorldGen.SecretSeed.CheckInputForSecretSeed(secretSeedText, out secretSeed))
			{
				return false;
			}
			processedSeed = processedSeed.Substring(num + 1);
			return true;
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x004BCF67 File Offset: 0x004BB167
		public WorldFileData()
			: base("World")
		{
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x004BCF91 File Offset: 0x004BB191
		public WorldFileData(string path, bool cloudSave)
			: base("World", path, cloudSave)
		{
		}

		// Token: 0x060014FC RID: 5372 RVA: 0x004BCFBD File Offset: 0x004BB1BD
		public override void SetAsActive()
		{
			if (this.LoadException != null)
			{
				throw this.LoadException;
			}
			Main.ActiveWorldFileData = this;
		}

		// Token: 0x060014FD RID: 5373 RVA: 0x004BCFD4 File Offset: 0x004BB1D4
		public void SetWorldSize(int x, int y)
		{
			this.WorldSizeX = x;
			this.WorldSizeY = y;
			if (x == 4200)
			{
				this._worldSizeName = Language.GetText("UI.WorldSizeSmall");
				return;
			}
			if (x == 6400)
			{
				this._worldSizeName = Language.GetText("UI.WorldSizeMedium");
				return;
			}
			if (x != 8400)
			{
				this._worldSizeName = Language.GetText("UI.WorldSizeUnknown");
				return;
			}
			this._worldSizeName = Language.GetText("UI.WorldSizeLarge");
		}

		// Token: 0x060014FE RID: 5374 RVA: 0x004BD04C File Offset: 0x004BB24C
		public static WorldFileData FromInvalidWorld(string path, bool cloudSave, int statusCode, Exception exception)
		{
			WorldFileData worldFileData = new WorldFileData(path, cloudSave);
			worldFileData.GameMode = 0;
			worldFileData.SetSeedToEmpty();
			worldFileData.WorldGeneratorVersion = 0UL;
			worldFileData.Metadata = FileMetadata.FromCurrentSettings(FileType.World);
			worldFileData.SetWorldSize(1, 1);
			worldFileData.HasCorruption = true;
			worldFileData.IsHardMode = false;
			worldFileData.LoadStatus = statusCode;
			worldFileData.LoadException = exception;
			worldFileData.Name = FileUtilities.GetFileName(path, false);
			worldFileData.UniqueId = Guid.Empty;
			if (!cloudSave)
			{
				worldFileData.CreationTime = File.GetCreationTime(path);
			}
			else
			{
				worldFileData.CreationTime = DateTime.Now;
			}
			return worldFileData;
		}

		// Token: 0x060014FF RID: 5375 RVA: 0x004BD0DB File Offset: 0x004BB2DB
		public void SetSeedToEmpty()
		{
			this.SetSeed("");
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x004BD0E8 File Offset: 0x004BB2E8
		public void SetSeed(string seedText)
		{
			this._seedText = seedText;
			this._seed = WorldFileData.TranslateSeed(seedText);
		}

		// Token: 0x06001501 RID: 5377 RVA: 0x004BD100 File Offset: 0x004BB300
		public static int TranslateSeed(string seedText)
		{
			int num;
			if (!int.TryParse(seedText, out num))
			{
				return Crc32.Calculate(seedText);
			}
			if (num != -2147483648)
			{
				return Math.Abs(num);
			}
			return int.MaxValue;
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x004BD134 File Offset: 0x004BB334
		public void SetSeedToRandom()
		{
			this.SetSeed(new UnifiedRandom().Next().ToString());
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x004BD159 File Offset: 0x004BB359
		public void SetSeedToRandomWithCurrentEvents()
		{
			this.SetSeedToRandom();
			if (Main.isHalloweenDateNow())
			{
				WorldGen.SecretSeed.Enable(WorldGen.SecretSeed.halloweenGen, false);
				this.SetSeed("pumpkinseason|" + this.SeedText);
			}
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x004BD18C File Offset: 0x004BB38C
		public override void MoveToCloud()
		{
			if (base.IsCloudSave)
			{
				return;
			}
			string worldPathFromName = Main.GetWorldPathFromName(this.Name, true);
			if (FileUtilities.MoveToCloud(base.Path, worldPathFromName))
			{
				Main.LocalFavoriteData.ClearEntry(this);
				this._isCloudSave = true;
				this._path = worldPathFromName;
				Main.CloudFavoritesData.SaveFavorite(this);
			}
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x004BD1E4 File Offset: 0x004BB3E4
		public override void MoveToLocal()
		{
			if (!base.IsCloudSave)
			{
				return;
			}
			string worldPathFromName = Main.GetWorldPathFromName(this.Name, false);
			if (FileUtilities.MoveToLocal(base.Path, worldPathFromName))
			{
				Main.CloudFavoritesData.ClearEntry(this);
				this._isCloudSave = false;
				this._path = worldPathFromName;
				Main.LocalFavoriteData.SaveFavorite(this);
			}
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x004BD239 File Offset: 0x004BB439
		public void Rename(string newDisplayName)
		{
			if (newDisplayName == null)
			{
				return;
			}
			WorldGen.RenameWorld(this, newDisplayName, this.GetRenameCallback(delegate
			{
				Main.menuMode = 6;
			}));
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x004BD26C File Offset: 0x004BB46C
		public void CopyToLocal(string newDisplayName, Action onCompleted)
		{
			if (base.IsCloudSave)
			{
				return;
			}
			string worldPathFromName = Main.GetWorldPathFromName(Guid.NewGuid().ToString(), false);
			FileUtilities.Copy(base.Path, worldPathFromName, false);
			this._path = worldPathFromName;
			WorldGen.RenameWorld(this, newDisplayName, this.GetRenameCallback(onCompleted));
		}

		// Token: 0x06001508 RID: 5384 RVA: 0x004BD2BE File Offset: 0x004BB4BE
		private Action<string> GetRenameCallback(Action returnToMenu)
		{
			Action <>9__1;
			return delegate(string newWorldName)
			{
				this.Name = newWorldName;
				Action action;
				if ((action = <>9__1) == null)
				{
					action = (<>9__1 = delegate
					{
						SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
						returnToMenu();
					});
				}
				Main.QueueMainThreadAction(action);
			};
		}

		// Token: 0x06001509 RID: 5385 RVA: 0x004BD2E0 File Offset: 0x004BB4E0
		// Note: this type is marked as 'beforefieldinit'.
		static WorldFileData()
		{
		}

		// Token: 0x040010A4 RID: 4260
		private const ulong GUID_IN_WORLD_FILE_VERSION = 777389080577UL;

		// Token: 0x040010A5 RID: 4261
		public static readonly int MAX_USER_SEED_TEXT_LENGTH = 40;

		// Token: 0x040010A6 RID: 4262
		public DateTime CreationTime;

		// Token: 0x040010A7 RID: 4263
		public DateTime LastPlayed;

		// Token: 0x040010A8 RID: 4264
		public int WorldSizeX;

		// Token: 0x040010A9 RID: 4265
		public int WorldSizeY;

		// Token: 0x040010AA RID: 4266
		public ulong WorldGeneratorVersion;

		// Token: 0x040010AB RID: 4267
		private string _seedText = "";

		// Token: 0x040010AC RID: 4268
		private int _seed;

		// Token: 0x040010AD RID: 4269
		public int LoadStatus = StatusID.Ok;

		// Token: 0x040010AE RID: 4270
		public Exception LoadException;

		// Token: 0x040010AF RID: 4271
		public Guid UniqueId;

		// Token: 0x040010B0 RID: 4272
		public int WorldId;

		// Token: 0x040010B1 RID: 4273
		public LocalizedText _worldSizeName;

		// Token: 0x040010B2 RID: 4274
		public int GameMode;

		// Token: 0x040010B3 RID: 4275
		public bool DrunkWorld;

		// Token: 0x040010B4 RID: 4276
		public bool NotTheBees;

		// Token: 0x040010B5 RID: 4277
		public bool ForTheWorthy;

		// Token: 0x040010B6 RID: 4278
		public bool Anniversary;

		// Token: 0x040010B7 RID: 4279
		public bool DontStarve;

		// Token: 0x040010B8 RID: 4280
		public bool RemixWorld;

		// Token: 0x040010B9 RID: 4281
		public bool NoTrapsWorld;

		// Token: 0x040010BA RID: 4282
		public bool ZenithWorld;

		// Token: 0x040010BB RID: 4283
		public bool SkyblockWorld;

		// Token: 0x040010BC RID: 4284
		public bool HasCorruption = true;

		// Token: 0x040010BD RID: 4285
		public bool IsHardMode;

		// Token: 0x040010BE RID: 4286
		public bool DefeatedMoonlord;

		// Token: 0x040010BF RID: 4287
		private static List<AWorldGenerationOption> seedOptionsInOrder = new List<AWorldGenerationOption>
		{
			WorldGenerationOptions.Get<WorldSeedOption_Drunk>(),
			WorldGenerationOptions.Get<WorldSeedOption_NotTheBees>(),
			WorldGenerationOptions.Get<WorldSeedOption_ForTheWorthy>(),
			WorldGenerationOptions.Get<WorldSeedOption_Anniversary>(),
			WorldGenerationOptions.Get<WorldSeedOption_DontStarve>(),
			WorldGenerationOptions.Get<WorldSeedOption_Remix>(),
			WorldGenerationOptions.Get<WorldSeedOption_NoTraps>(),
			WorldGenerationOptions.Get<WorldSeedOption_Everything>(),
			WorldGenerationOptions.Get<WorldSeedOption_Skyblock>()
		};

		// Token: 0x02000666 RID: 1638
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06003DB1 RID: 15793 RVA: 0x00693213 File Offset: 0x00691413
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06003DB2 RID: 15794 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x06003DB3 RID: 15795 RVA: 0x0069321F File Offset: 0x0069141F
			internal void <Rename>b__65_0()
			{
				Main.menuMode = 6;
			}

			// Token: 0x04006677 RID: 26231
			public static readonly WorldFileData.<>c <>9 = new WorldFileData.<>c();

			// Token: 0x04006678 RID: 26232
			public static Action <>9__65_0;
		}

		// Token: 0x02000667 RID: 1639
		[CompilerGenerated]
		private sealed class <>c__DisplayClass67_0
		{
			// Token: 0x06003DB4 RID: 15796 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass67_0()
			{
			}

			// Token: 0x06003DB5 RID: 15797 RVA: 0x00693228 File Offset: 0x00691428
			internal void <GetRenameCallback>b__0(string newWorldName)
			{
				this.<>4__this.Name = newWorldName;
				Action action;
				if ((action = this.<>9__1) == null)
				{
					action = (this.<>9__1 = delegate
					{
						SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
						this.returnToMenu();
					});
				}
				Main.QueueMainThreadAction(action);
			}

			// Token: 0x06003DB6 RID: 15798 RVA: 0x00693265 File Offset: 0x00691465
			internal void <GetRenameCallback>b__1()
			{
				SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
				this.returnToMenu();
			}

			// Token: 0x04006679 RID: 26233
			public WorldFileData <>4__this;

			// Token: 0x0400667A RID: 26234
			public Action returnToMenu;

			// Token: 0x0400667B RID: 26235
			public Action <>9__1;
		}
	}
}
