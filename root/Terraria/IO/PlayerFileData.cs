using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Terraria.Social;
using Terraria.Utilities;

namespace Terraria.IO
{
	// Token: 0x02000070 RID: 112
	public class PlayerFileData : FileData
	{
		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060014D4 RID: 5332 RVA: 0x004BC699 File Offset: 0x004BA899
		// (set) Token: 0x060014D5 RID: 5333 RVA: 0x004BC6A1 File Offset: 0x004BA8A1
		public Player Player
		{
			get
			{
				return this._player;
			}
			set
			{
				this._player = value;
				if (value != null)
				{
					this.Name = this._player.name;
				}
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060014D6 RID: 5334 RVA: 0x004BC6BE File Offset: 0x004BA8BE
		// (set) Token: 0x060014D7 RID: 5335 RVA: 0x004BC6C6 File Offset: 0x004BA8C6
		public bool ServerSideCharacter
		{
			[CompilerGenerated]
			get
			{
				return this.<ServerSideCharacter>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ServerSideCharacter>k__BackingField = value;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060014D8 RID: 5336 RVA: 0x004BC6CF File Offset: 0x004BA8CF
		public DateTime LastPlayed
		{
			get
			{
				return DateTime.FromBinary(this.Player.lastTimePlayerWasSaved);
			}
		}

		// Token: 0x060014D9 RID: 5337 RVA: 0x004BC6E1 File Offset: 0x004BA8E1
		public PlayerFileData()
			: base("Player")
		{
		}

		// Token: 0x060014DA RID: 5338 RVA: 0x004BC704 File Offset: 0x004BA904
		public PlayerFileData(string path, bool cloudSave)
			: base("Player", path, cloudSave)
		{
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x004BC72C File Offset: 0x004BA92C
		public static PlayerFileData CreateAndSave(Player player)
		{
			PlayerFileData playerFileData = new PlayerFileData();
			playerFileData.Metadata = FileMetadata.FromCurrentSettings(FileType.Player);
			playerFileData.Player = player;
			playerFileData._isCloudSave = SocialAPI.Cloud != null && SocialAPI.Cloud.EnabledByDefault;
			playerFileData._path = Main.GetPlayerPathFromName(player.name, playerFileData.IsCloudSave);
			(playerFileData.IsCloudSave ? Main.CloudFavoritesData : Main.LocalFavoriteData).ClearEntry(playerFileData);
			Player.SavePlayer(playerFileData, true);
			return playerFileData;
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x004BC7A5 File Offset: 0x004BA9A5
		public override void SetAsActive()
		{
			Main.ActivePlayerFileData = this;
			Main.player[Main.myPlayer] = this.Player;
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x004BC7BE File Offset: 0x004BA9BE
		public void MarkAsServerSide()
		{
			this.ServerSideCharacter = true;
		}

		// Token: 0x060014DE RID: 5342 RVA: 0x004BC7C8 File Offset: 0x004BA9C8
		public override void MoveToCloud()
		{
			if (base.IsCloudSave || SocialAPI.Cloud == null)
			{
				return;
			}
			string playerPathFromName = Main.GetPlayerPathFromName(this.Name, true);
			if (FileUtilities.MoveToCloud(base.Path, playerPathFromName))
			{
				string fileName = base.GetFileName(false);
				string text = Main.PlayerPath + global::System.IO.Path.DirectorySeparatorChar.ToString() + fileName + global::System.IO.Path.DirectorySeparatorChar.ToString();
				string text2 = playerPathFromName.Substring(0, playerPathFromName.Length - 4);
				if (Directory.Exists(text))
				{
					string[] files = Directory.GetFiles(text);
					for (int i = 0; i < files.Length; i++)
					{
						string text3 = text2 + "/" + FileUtilities.GetFileName(files[i], true);
						FileUtilities.MoveToCloud(files[i], text3);
					}
				}
				Main.LocalFavoriteData.ClearEntry(this);
				this._isCloudSave = true;
				this._path = playerPathFromName;
				Main.CloudFavoritesData.SaveFavorite(this);
			}
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x004BC8B0 File Offset: 0x004BAAB0
		public override void MoveToLocal()
		{
			if (!base.IsCloudSave || SocialAPI.Cloud == null)
			{
				return;
			}
			string playerPathFromName = Main.GetPlayerPathFromName(this.Name, false);
			if (FileUtilities.MoveToLocal(base.Path, playerPathFromName))
			{
				string fileName = base.GetFileName(false);
				string mapPath = global::System.IO.Path.Combine(Main.CloudPlayerPath, fileName);
				foreach (string text in (from path in SocialAPI.Cloud.GetFiles().ToList<string>()
					where this.MapBelongsToPath(mapPath, path)
					select path).ToList<string>())
				{
					string text2 = global::System.IO.Path.Combine(Main.PlayerPath, fileName, FileUtilities.GetFileName(text, true));
					FileUtilities.MoveToLocal(text, text2);
				}
				Main.CloudFavoritesData.ClearEntry(this);
				this._isCloudSave = false;
				this._path = playerPathFromName;
				Main.LocalFavoriteData.SaveFavorite(this);
			}
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x004BC9B4 File Offset: 0x004BABB4
		private bool MapBelongsToPath(string mapPath, string filePath)
		{
			if (!filePath.EndsWith(".map", StringComparison.CurrentCultureIgnoreCase))
			{
				return false;
			}
			string text = mapPath.Replace('\\', '/');
			return filePath.StartsWith(text, StringComparison.CurrentCultureIgnoreCase);
		}

		// Token: 0x060014E1 RID: 5345 RVA: 0x004BC9E4 File Offset: 0x004BABE4
		public void UpdatePlayTimer()
		{
			if (FocusHelper.AllowCountingPlayerTime)
			{
				this.StartPlayTimer();
				return;
			}
			this.PausePlayTimer();
		}

		// Token: 0x060014E2 RID: 5346 RVA: 0x004BC9FA File Offset: 0x004BABFA
		public void StartPlayTimer()
		{
			if (this._isTimerActive)
			{
				return;
			}
			this._isTimerActive = true;
			if (!this._timer.IsRunning)
			{
				this._timer.Start();
			}
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x004BCA24 File Offset: 0x004BAC24
		public void PausePlayTimer()
		{
			this.StopPlayTimer();
		}

		// Token: 0x060014E4 RID: 5348 RVA: 0x004BCA2C File Offset: 0x004BAC2C
		public TimeSpan GetPlayTime()
		{
			if (this._timer.IsRunning)
			{
				return this._playTime + this._timer.Elapsed;
			}
			return this._playTime;
		}

		// Token: 0x060014E5 RID: 5349 RVA: 0x004BCA58 File Offset: 0x004BAC58
		public void UpdatePlayTimerAndKeepState()
		{
			bool isRunning = this._timer.IsRunning;
			this._playTime += this._timer.Elapsed;
			this._timer.Reset();
			if (isRunning)
			{
				this._timer.Start();
			}
		}

		// Token: 0x060014E6 RID: 5350 RVA: 0x004BCAA4 File Offset: 0x004BACA4
		public void StopPlayTimer()
		{
			if (!this._isTimerActive)
			{
				return;
			}
			this._isTimerActive = false;
			if (this._timer.IsRunning)
			{
				this._playTime += this._timer.Elapsed;
				this._timer.Reset();
			}
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x004BCAF5 File Offset: 0x004BACF5
		public void SetPlayTime(TimeSpan time)
		{
			this._playTime = time;
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x004BCAFE File Offset: 0x004BACFE
		public void Rename(string newName)
		{
			if (this.Player != null)
			{
				this.Player.name = newName.Trim();
			}
			Player.SavePlayer(this, false);
		}

		// Token: 0x0400109F RID: 4255
		private Player _player;

		// Token: 0x040010A0 RID: 4256
		private TimeSpan _playTime = TimeSpan.Zero;

		// Token: 0x040010A1 RID: 4257
		private readonly Stopwatch _timer = new Stopwatch();

		// Token: 0x040010A2 RID: 4258
		private bool _isTimerActive;

		// Token: 0x040010A3 RID: 4259
		[CompilerGenerated]
		private bool <ServerSideCharacter>k__BackingField;

		// Token: 0x02000665 RID: 1637
		[CompilerGenerated]
		private sealed class <>c__DisplayClass19_0
		{
			// Token: 0x06003DAF RID: 15791 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass19_0()
			{
			}

			// Token: 0x06003DB0 RID: 15792 RVA: 0x006931FF File Offset: 0x006913FF
			internal bool <MoveToLocal>b__0(string path)
			{
				return this.<>4__this.MapBelongsToPath(this.mapPath, path);
			}

			// Token: 0x04006675 RID: 26229
			public string mapPath;

			// Token: 0x04006676 RID: 26230
			public PlayerFileData <>4__this;
		}
	}
}
