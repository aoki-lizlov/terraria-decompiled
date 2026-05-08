using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using ReLogic.OS;
using Terraria.GameContent.UI.States;
using Terraria.IO;
using Terraria.Localization;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace Terraria.Testing
{
	// Token: 0x02000114 RID: 276
	public abstract class QuickLoad
	{
		// Token: 0x06001AF1 RID: 6897
		protected abstract void Start();

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06001AF2 RID: 6898 RVA: 0x004F9048 File Offset: 0x004F7248
		public static bool QuickLoading
		{
			get
			{
				return QuickLoad._loadedConfig != null;
			}
		}

		// Token: 0x06001AF3 RID: 6899 RVA: 0x004F9054 File Offset: 0x004F7254
		public static void Load()
		{
			if (!QuickLoad.TryRead(out QuickLoad._loadedConfig))
			{
				return;
			}
			if (QuickLoad.ShiftHeld())
			{
				QuickLoad._loadedConfig = null;
				Platform.Get<IWindowService>().Activate(Main.instance.Window);
				if (MessageBox.Show("Quick Load skipped. Do you want to delete the configuration?", "", MessageBoxButtons.YesNo, MessageBoxIcon.None) == DialogResult.Yes)
				{
					QuickLoad.Clear();
				}
			}
		}

		// Token: 0x06001AF4 RID: 6900 RVA: 0x004F90A8 File Offset: 0x004F72A8
		public static void OnContentLoaded()
		{
			try
			{
				if (QuickLoad._loadedConfig != null)
				{
					QuickLoad._loadedConfig.Start();
				}
			}
			catch (Exception ex)
			{
				if (MessageBox.Show("Do you want to delete the configuration?\n\n" + ex, "Quickload Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
				{
					QuickLoad.Clear();
				}
			}
		}

		// Token: 0x06001AF5 RID: 6901 RVA: 0x004F90FC File Offset: 0x004F72FC
		public static QuickLoad Deserialize(string json)
		{
			return JsonConvert.DeserializeObject<QuickLoad>(json, QuickLoad.SerializerSettings);
		}

		// Token: 0x06001AF6 RID: 6902 RVA: 0x004F9109 File Offset: 0x004F7309
		public static string Serialize(QuickLoad config)
		{
			return JsonConvert.SerializeObject(config, typeof(QuickLoad), QuickLoad.SerializerSettings);
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x004F9120 File Offset: 0x004F7320
		public static bool TryRead(out QuickLoad config)
		{
			config = null;
			bool flag;
			try
			{
				if (!File.Exists(QuickLoad.FilePath))
				{
					flag = false;
				}
				else
				{
					config = QuickLoad.Deserialize(File.ReadAllText(QuickLoad.FilePath));
					flag = true;
				}
			}
			catch (Exception)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001AF8 RID: 6904 RVA: 0x004F916C File Offset: 0x004F736C
		public static void Set(QuickLoad config)
		{
			File.WriteAllText(QuickLoad.FilePath, QuickLoad.Serialize(config));
		}

		// Token: 0x06001AF9 RID: 6905 RVA: 0x004F917E File Offset: 0x004F737E
		public static void Clear()
		{
			if (File.Exists(QuickLoad.FilePath))
			{
				File.Delete(QuickLoad.FilePath);
			}
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x004F9198 File Offset: 0x004F7398
		private static bool ShiftHeld()
		{
			if (Keyboard.GetState().PressingShift())
			{
				return true;
			}
			try
			{
				if (Platform.IsWindows)
				{
					return QuickLoad.ShiftHeldWin();
				}
				if (Platform.IsOSX)
				{
					return QuickLoad.ShiftHeldOSX();
				}
				if (Platform.IsLinux)
				{
					return QuickLoad.ShiftHeldX11();
				}
			}
			catch (Exception)
			{
			}
			return false;
		}

		// Token: 0x06001AFB RID: 6907
		[DllImport("user32.dll")]
		private static extern short GetAsyncKeyState(int vKey);

		// Token: 0x06001AFC RID: 6908 RVA: 0x004F91FC File Offset: 0x004F73FC
		private static bool ShiftHeldWin()
		{
			return ((int)QuickLoad.GetAsyncKeyState(160) & 32768) != 0 || ((int)QuickLoad.GetAsyncKeyState(161) & 32768) != 0;
		}

		// Token: 0x06001AFD RID: 6909
		[DllImport("/System/Library/Frameworks/CoreGraphics.framework/CoreGraphics")]
		private static extern ulong CGEventSourceFlagsState(uint stateID);

		// Token: 0x06001AFE RID: 6910 RVA: 0x004F9225 File Offset: 0x004F7425
		private static bool ShiftHeldOSX()
		{
			return (QuickLoad.CGEventSourceFlagsState(1U) & 131072UL) > 0UL;
		}

		// Token: 0x06001AFF RID: 6911
		[DllImport("libX11")]
		private static extern IntPtr XOpenDisplay(IntPtr display);

		// Token: 0x06001B00 RID: 6912
		[DllImport("libX11")]
		private static extern void XCloseDisplay(IntPtr display);

		// Token: 0x06001B01 RID: 6913
		[DllImport("libX11")]
		private static extern int XQueryKeymap(IntPtr display, byte[] keys_return);

		// Token: 0x06001B02 RID: 6914
		[DllImport("libX11")]
		private static extern int XKeysymToKeycode(IntPtr display, ulong keysym);

		// Token: 0x06001B03 RID: 6915 RVA: 0x004F9238 File Offset: 0x004F7438
		private static bool ShiftHeldX11()
		{
			IntPtr intPtr = QuickLoad.XOpenDisplay(IntPtr.Zero);
			if (intPtr == IntPtr.Zero)
			{
				return false;
			}
			bool flag3;
			try
			{
				int num = QuickLoad.XKeysymToKeycode(intPtr, 65505UL);
				int num2 = QuickLoad.XKeysymToKeycode(intPtr, 65506UL);
				byte[] array = new byte[32];
				QuickLoad.XQueryKeymap(intPtr, array);
				bool flag = ((int)array[num / 8] & (1 << num % 8)) != 0;
				bool flag2 = ((int)array[num2 / 8] & (1 << num2 % 8)) != 0;
				flag3 = flag || flag2;
			}
			finally
			{
				QuickLoad.XCloseDisplay(intPtr);
			}
			return flag3;
		}

		// Token: 0x06001B04 RID: 6916 RVA: 0x0000357B File Offset: 0x0000177B
		protected QuickLoad()
		{
		}

		// Token: 0x06001B05 RID: 6917 RVA: 0x004F92D0 File Offset: 0x004F74D0
		// Note: this type is marked as 'beforefieldinit'.
		static QuickLoad()
		{
		}

		// Token: 0x0400153B RID: 5435
		private static readonly string FilePath = Path.Combine(Main.SavePath, "dev-quickload.json");

		// Token: 0x0400153C RID: 5436
		private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
		{
			ContractResolver = new EasyDeserializationJsonContractResolver(),
			TypeNameHandling = 4
		};

		// Token: 0x0400153D RID: 5437
		private static QuickLoad _loadedConfig;

		// Token: 0x02000726 RID: 1830
		public class JoinWorld : QuickLoad
		{
			// Token: 0x0600406C RID: 16492 RVA: 0x0069E168 File Offset: 0x0069C368
			protected override void Start()
			{
				string text;
				int num;
				if (Program.LaunchParameters.TryGetValue("-quickloadclient", out text) && int.TryParse(text, out num) && num < this.ExtraClients.Count)
				{
					this.ExtraClients[num].Start();
					return;
				}
				if (this.ExtraClients != null)
				{
					this.LaunchExtraClients();
				}
				this.RestoreWindowBounds();
				this.SelectPlayerAndWorld();
				this.PlayWorldOrJoinServer();
			}

			// Token: 0x0600406D RID: 16493 RVA: 0x0069E1D4 File Offset: 0x0069C3D4
			private void RestoreWindowBounds()
			{
				if (this.WindowedBounds == null)
				{
					return;
				}
				Rectangle value = this.WindowedBounds.Value;
				if (Platform.Get<IWindowService>().IsSizeable(Main.instance.Window))
				{
					Main.SetResolution(value.Width, value.Height);
					Platform.Get<IWindowService>().SetPosition(Main.instance.Window, value.X, value.Y);
				}
			}

			// Token: 0x0600406E RID: 16494 RVA: 0x0069E242 File Offset: 0x0069C442
			private void SaveWindowBounds()
			{
				if (Platform.Get<IWindowService>().IsSizeable(Main.instance.Window))
				{
					this.WindowedBounds = new Rectangle?(Platform.Get<IWindowService>().GetBounds(Main.instance.Window));
				}
			}

			// Token: 0x0600406F RID: 16495 RVA: 0x0069E27C File Offset: 0x0069C47C
			private void LaunchExtraClients()
			{
				for (int i = 0; i < this.ExtraClients.Count; i++)
				{
					Process.Start(Process.GetCurrentProcess().ProcessName, "-quickloadclient " + i);
				}
			}

			// Token: 0x06004070 RID: 16496 RVA: 0x0069E2C0 File Offset: 0x0069C4C0
			protected void SelectPlayerAndWorld()
			{
				Main.LoadPlayers();
				Main.SelectPlayer(Main.PlayerList.Single((PlayerFileData p) => p.Path == this.PlayerPath));
				if (!string.IsNullOrEmpty(this.WorldPath))
				{
					Main.WorldList.Single((WorldFileData w) => w.Path == this.WorldPath).SetAsActive();
				}
			}

			// Token: 0x06004071 RID: 16497 RVA: 0x0069E318 File Offset: 0x0069C518
			protected void PlayWorldOrJoinServer()
			{
				WorldGen.Hooks.OnWorldLoad += this.OnWorldLoad;
				Main.menuMode = 10;
				if (this.ServerIPText == null)
				{
					WorldGen.playWorld();
					return;
				}
				Netplay.ServerPassword = this.ServerPassword;
				if (this.IsHostAndPlay)
				{
					Main.HostAndPlay();
					return;
				}
				Main.autoPass = true;
				Netplay.SetRemoteIP(this.ServerIPText);
				Netplay.StartTcpClient();
				Main.statusText = Language.GetTextValue("Net.ConnectingTo", this.ServerIPText);
			}

			// Token: 0x06004072 RID: 16498 RVA: 0x0069E390 File Offset: 0x0069C590
			private void OnWorldLoad()
			{
				WorldGen.Hooks.OnWorldLoad -= this.OnWorldLoad;
				if (Main.ActiveWorldFileData.Path != this.WorldPath || Main.ActivePlayerFileData.Path != this.PlayerPath || (this.ServerIPText != null && Main.netMode != 1) || (Main.netMode == 1 && this.ServerIPText != Netplay.ServerIPText))
				{
					return;
				}
				if (this.PlayerPosition != Vector2.Zero)
				{
					Main.LocalPlayer.position = this.PlayerPosition;
				}
				if (this.MountType != 0)
				{
					Main.LocalPlayer.mount.SetMount(this.MountType, Main.LocalPlayer);
				}
				DebugUtils.QuickSPMessage("/onquickload");
			}

			// Token: 0x06004073 RID: 16499 RVA: 0x0069E458 File Offset: 0x0069C658
			public virtual QuickLoad.JoinWorld WithCurrentState()
			{
				this.SaveWindowBounds();
				Main.SaveSettings();
				if (!Main.gameMenu)
				{
					Player.SavePlayer(Main.ActivePlayerFileData, false);
				}
				if (Main.WorldFileMetadata == null)
				{
					Main.WorldFileMetadata = FileMetadata.FromCurrentSettings(FileType.World);
				}
				if (Main.netMode != 1)
				{
					WorldFile.SaveWorld(false, false, false);
				}
				this.PlayerPath = Main.ActivePlayerFileData.Path;
				this.WorldPath = Main.ActiveWorldFileData.Path;
				if (Main.netMode == 1)
				{
					this.ServerIPText = Netplay.ServerIPText;
					this.ServerPassword = Netplay.ServerPassword;
					this.IsHostAndPlay = Netplay.IsHostAndPlay;
					if (this.IsHostAndPlay)
					{
						NetMessage.SendData(94, -1, -1, NetworkText.FromLiteral("/quickload-clientprobe"), 0, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				if (!Main.gameMenu)
				{
					this.PlayerPosition = Main.LocalPlayer.position;
					this.MountType = Main.LocalPlayer.mount.Type;
				}
				this.InDebugRegenUI = UIWorldGenDebug.ActiveInstance != null;
				if (this.InDebugRegenUI)
				{
					if (UIWorldGenDebug.CurrentTargetOrLatestPass != null)
					{
						this.RegenTargetPassName = UIWorldGenDebug.CurrentTargetOrLatestPass.Name;
					}
					this.RegenSnapshotFrequency = WorldGenerator.CurrentController.SnapshotFrequency;
					this.RegenPauseOnHashMismatch = WorldGenerator.CurrentController.PauseOnHashMismatch;
				}
				return this;
			}

			// Token: 0x06004074 RID: 16500 RVA: 0x0069E598 File Offset: 0x0069C798
			protected WorldGenerator.Controller CreateRegenController()
			{
				WorldGen.PrepForRegen();
				return new WorldGenerator.Controller(WorldGen.Manifest)
				{
					Paused = (this.InDebugRegenUI && this.RegenTargetPassName == null),
					SnapshotFrequency = this.RegenSnapshotFrequency,
					PauseOnHashMismatch = this.RegenPauseOnHashMismatch,
					OnPassesLoaded = delegate(WorldGenerator.Controller c)
					{
						c.PauseAfterPass = c.Passes.FirstOrDefault((GenPass p) => p.Name == this.RegenTargetPassName);
						if (c.PauseAfterPass != null)
						{
							c.TryRunToEndOfPass(c.PauseAfterPass, true, true);
						}
					}
				};
			}

			// Token: 0x06004075 RID: 16501 RVA: 0x0069E5F8 File Offset: 0x0069C7F8
			public JoinWorld()
			{
			}

			// Token: 0x06004076 RID: 16502 RVA: 0x0069E60B File Offset: 0x0069C80B
			[CompilerGenerated]
			private bool <SelectPlayerAndWorld>b__17_0(PlayerFileData p)
			{
				return p.Path == this.PlayerPath;
			}

			// Token: 0x06004077 RID: 16503 RVA: 0x0069E61E File Offset: 0x0069C81E
			[CompilerGenerated]
			private bool <SelectPlayerAndWorld>b__17_1(WorldFileData w)
			{
				return w.Path == this.WorldPath;
			}

			// Token: 0x06004078 RID: 16504 RVA: 0x0069E631 File Offset: 0x0069C831
			[CompilerGenerated]
			private void <CreateRegenController>b__21_0(WorldGenerator.Controller c)
			{
				c.PauseAfterPass = c.Passes.FirstOrDefault((GenPass p) => p.Name == this.RegenTargetPassName);
				if (c.PauseAfterPass != null)
				{
					c.TryRunToEndOfPass(c.PauseAfterPass, true, true);
				}
			}

			// Token: 0x06004079 RID: 16505 RVA: 0x0069E667 File Offset: 0x0069C867
			[CompilerGenerated]
			private bool <CreateRegenController>b__21_1(GenPass p)
			{
				return p.Name == this.RegenTargetPassName;
			}

			// Token: 0x04006969 RID: 26985
			public Rectangle? WindowedBounds;

			// Token: 0x0400696A RID: 26986
			public string PlayerPath;

			// Token: 0x0400696B RID: 26987
			public string WorldPath;

			// Token: 0x0400696C RID: 26988
			public string ServerIPText;

			// Token: 0x0400696D RID: 26989
			public string ServerPassword;

			// Token: 0x0400696E RID: 26990
			public bool IsHostAndPlay;

			// Token: 0x0400696F RID: 26991
			public List<QuickLoad.JoinWorld> ExtraClients = new List<QuickLoad.JoinWorld>();

			// Token: 0x04006970 RID: 26992
			public Vector2 PlayerPosition;

			// Token: 0x04006971 RID: 26993
			public int MountType;

			// Token: 0x04006972 RID: 26994
			public bool InDebugRegenUI;

			// Token: 0x04006973 RID: 26995
			public string RegenTargetPassName;

			// Token: 0x04006974 RID: 26996
			public WorldGenerator.SnapshotFrequency RegenSnapshotFrequency;

			// Token: 0x04006975 RID: 26997
			public bool RegenPauseOnHashMismatch;
		}

		// Token: 0x02000727 RID: 1831
		public class RegenWorld : QuickLoad.JoinWorld
		{
			// Token: 0x0600407A RID: 16506 RVA: 0x0069E67A File Offset: 0x0069C87A
			protected override void Start()
			{
				base.SelectPlayerAndWorld();
				this.GenerateWorld();
			}

			// Token: 0x0600407B RID: 16507 RVA: 0x0069E688 File Offset: 0x0069C888
			private void GenerateWorld()
			{
				WorldGen.CreateNewWorld(null, base.CreateRegenController(), new WorldGen.WorldGenerationFinishCallback(this.OnGenerationFinished));
			}

			// Token: 0x0600407C RID: 16508 RVA: 0x0069E6A3 File Offset: 0x0069C8A3
			private void OnGenerationFinished(bool playable)
			{
				if (!playable)
				{
					return;
				}
				this.InDebugRegenUI = false;
				base.PlayWorldOrJoinServer();
			}

			// Token: 0x0600407D RID: 16509 RVA: 0x0069E6B6 File Offset: 0x0069C8B6
			public RegenWorld()
			{
			}
		}
	}
}
