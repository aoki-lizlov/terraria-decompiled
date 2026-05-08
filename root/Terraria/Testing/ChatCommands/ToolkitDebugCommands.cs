using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Xna.Framework;
using Terraria.Chat;
using Terraria.GameContent.UI.States;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.Map;
using Terraria.Net.Sockets;
using Terraria.UI;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace Terraria.Testing.ChatCommands
{
	// Token: 0x02000119 RID: 281
	public static class ToolkitDebugCommands
	{
		// Token: 0x06001B1D RID: 6941 RVA: 0x004F9E21 File Offset: 0x004F8021
		[DebugCommand("hh", "Opens a list of all the debug commands", CommandRequirement.Client)]
		public static bool HelpCommand(DebugMessage message)
		{
			IngameFancyUI.OpenUIState(new UIDebugCommandsList());
			return true;
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x004F9E2E File Offset: 0x004F802E
		[DebugCommand("memo", "Creates a shortcut command with a given name. Opens the file to write in. One command per line. Accepts arg substitions ({0}, {1}, etc)", CommandRequirement.Client, HelpText = "Usage: /memo <custom-command-name>")]
		public static bool MemoCommand(DebugMessage message)
		{
			if (string.IsNullOrWhiteSpace(message.Arguments) || message.Arguments.Contains(" "))
			{
				return false;
			}
			DebugCommandProcessor.OpenMemo(message.Arguments);
			return true;
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x004F9E60 File Offset: 0x004F8060
		[DebugCommand("memonum", "Creates a memo for a numpad key (0-9). Shorthand for /memo numpad{i}", CommandRequirement.Client, HelpText = "Usage: /memonum <0-9>")]
		public static bool MemoNumCommand(DebugMessage message)
		{
			int num;
			if (!int.TryParse(message.Arguments, out num) || num < 0 || num > 9)
			{
				message.ReplyError("Invalid numpad key number");
				return false;
			}
			DebugCommandProcessor.OpenMemo("numpad" + num);
			return true;
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x004F9EA8 File Offset: 0x004F80A8
		[DebugCommand("setserverping", "Sets a target ping for all players on the server. Clients will automatically adjust /latency to achieve it.", CommandRequirement.MultiplayerRPC, HelpText = "Usage: /setserverping <ms>")]
		public static bool SetServerPingCommand(DebugMessage message)
		{
			int num;
			if (!int.TryParse(message.Arguments, out num) || num < 0 || num > 10000)
			{
				return false;
			}
			DebugOptions.Shared_ServerPing = num;
			ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(string.Format("Target ping set {0}ms", num)), new Color(250, 250, 0), -1);
			NetMessage.SendData(94, -1, -1, NetworkText.FromLiteral("/setserverping"), 0, (float)DebugOptions.Shared_ServerPing, 0f, 0f, 0, 0, 0);
			return true;
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x004F9F2C File Offset: 0x004F812C
		[DebugCommand("latency", "Adds latency to incoming and outgoing packets sent by this client.", CommandRequirement.MultiplayerClient, HelpText = "Usage: /latency <ms>")]
		public static bool LatencyCommand(DebugMessage message)
		{
			uint num = 0U;
			if (!uint.TryParse(message.Arguments, out num))
			{
				return false;
			}
			DebugNetworkStream.Latency = num;
			message.Reply(string.Format("Latency set to {0}ms", num));
			return true;
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x004F9F6C File Offset: 0x004F816C
		[DebugCommand("setdrawwait", "Sets a fixed waiting period to occur during each engine draw call.", CommandRequirement.Client, HelpText = "Usage: /setdrawwait <delay in ms>")]
		public static bool SetDrawWaitCommand(DebugMessage message)
		{
			double num;
			if (!double.TryParse(message.Arguments, out num) || num < 0.0 || num > 100.0)
			{
				return false;
			}
			DebugOptions.DrawWaitInMs = num;
			message.Reply(string.Format("Draw wait time set to {0}ms", num));
			return true;
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x004F9FC0 File Offset: 0x004F81C0
		[DebugCommand("setupdatewait", "Sets a fixed waiting period to occur during each engine update call.", CommandRequirement.Client, HelpText = "Usage: /setupdatewait <delay in ms>")]
		public static bool SetUpdateWaitCommand(DebugMessage message)
		{
			double num;
			if (!double.TryParse(message.Arguments, out num) || num < 0.0 || num > 100.0)
			{
				return false;
			}
			DebugOptions.UpdateWaitInMs = num;
			message.Reply(string.Format("Update wait time set to {0}ms", num));
			return true;
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x004FA013 File Offset: 0x004F8213
		[DebugCommand("toggleinactivewait", "Toggles main thread sleeping when window is unfocused (this setting is saved).", CommandRequirement.Client)]
		public static bool ToggleInactiveWait(DebugMessage message)
		{
			Main.ThrottleWhenInactive = !Main.ThrottleWhenInactive;
			Main.SaveSettings();
			message.Reply("Inactive CPU throttling " + (Main.ThrottleWhenInactive ? "enabled" : "disabled"));
			return true;
		}

		// Token: 0x06001B25 RID: 6949 RVA: 0x004FA04C File Offset: 0x004F824C
		[DebugCommand("quickload", "Automatically rejoin this world/server with this player whenever the game is launched. Executes /onquickload memo when joining the world. Will relaunch all local clients when used with  Host & Play", CommandRequirement.Client, HelpText = "/quickload  [stop]")]
		public static bool QuickLoadRejoinCommand(DebugMessage message)
		{
			string text = message.Arguments.ToLowerInvariant().Trim();
			if (text == "stop" || text == "disable" || text == "clear" || text == "cancel" || text == "exit")
			{
				QuickLoad.Clear();
				message.Reply("Quick Load configuration cleared.");
				return true;
			}
			QuickLoad.Set(new QuickLoad.JoinWorld().WithCurrentState());
			message.Reply("Quick Load configuration set. Hold shift while loading to clear it.");
			return true;
		}

		// Token: 0x06001B26 RID: 6950 RVA: 0x004FA0D8 File Offset: 0x004F82D8
		[DebugCommand("quickload-regen", "Automatically regenerate this world whenever the game is launched. Executes /onquickload memo when joining the world", CommandRequirement.SinglePlayer)]
		public static bool QuickLoadRegenCommand(DebugMessage message)
		{
			QuickLoad.Set(new QuickLoad.RegenWorld().WithCurrentState());
			message.Reply("Quick Load configuration set. Hold shift while loading to clear it.");
			return true;
		}

		// Token: 0x06001B27 RID: 6951 RVA: 0x004FA0F5 File Offset: 0x004F82F5
		[DebugCommand("light", "Toggles the lighting system between active and fullbright.", CommandRequirement.Client)]
		public static bool LightCommand(DebugMessage message)
		{
			if (DebugOptions.devLightTilesCheat)
			{
				DebugOptions.devLightTilesCheat = false;
				message.Reply("Lighting enabled");
			}
			else
			{
				DebugOptions.devLightTilesCheat = true;
				message.Reply("Lighting disabled");
			}
			return true;
		}

		// Token: 0x06001B28 RID: 6952 RVA: 0x004FA123 File Offset: 0x004F8323
		[DebugCommand("nolimits", "No border restrictions", CommandRequirement.Client)]
		public static bool NoLimitsCommand(DebugMessage message)
		{
			if (DebugOptions.noLimits)
			{
				DebugOptions.noLimits = false;
				message.Reply("No limits disabled");
			}
			else
			{
				DebugOptions.noLimits = true;
				message.Reply("No limits enabled");
			}
			return true;
		}

		// Token: 0x06001B29 RID: 6953 RVA: 0x004FA151 File Offset: 0x004F8351
		[DebugCommand("save", "Save the player (and the world if single player)", CommandRequirement.Client)]
		public static bool SaveCommand(DebugMessage message)
		{
			Player.SavePlayer(Main.ActivePlayerFileData, false);
			if (Main.netMode == 0)
			{
				WorldFile.SaveWorld(false, false, false);
				message.Reply("Player and world saved!");
			}
			else
			{
				message.Reply("Player saved!");
			}
			return true;
		}

		// Token: 0x06001B2A RID: 6954 RVA: 0x004FA188 File Offset: 0x004F8388
		[DebugCommand("reload", "Reloads the last save", CommandRequirement.SinglePlayer)]
		public static bool ReloadCommand(DebugMessage message)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			WorldFile.LoadWorld();
			Main.sectionManager.SetAllSectionsLoaded();
			message.Reply(string.Format("Reloaded in {0}ms", stopwatch.ElapsedMilliseconds));
			return true;
		}

		// Token: 0x06001B2B RID: 6955 RVA: 0x004FA1C6 File Offset: 0x004F83C6
		[DebugCommand("quit", "Exit world without saving.", CommandRequirement.Client)]
		public static bool QuitCommand(DebugMessage message)
		{
			WorldGen.JustQuit();
			return true;
		}

		// Token: 0x06001B2C RID: 6956 RVA: 0x004FA1CE File Offset: 0x004F83CE
		[DebugCommand("reloadpacks", "Reloads resource packs.", CommandRequirement.Client)]
		public static bool ReloadPacksCommand(DebugMessage message)
		{
			Main.instance.ResetAllContentBasedRenderTargets();
			Main.AssetSourceController.Refresh();
			message.Reply("Resource Packs Reloaded.");
			return true;
		}

		// Token: 0x06001B2D RID: 6957 RVA: 0x004FA1F0 File Offset: 0x004F83F0
		[DebugCommand("frame", "Resets all frame data", CommandRequirement.Client)]
		public static bool FrameCommand(DebugMessage message)
		{
			Main.sectionManager.SetAllSectionsLoaded();
			message.Reply("World frame data cleared");
			return true;
		}

		// Token: 0x06001B2E RID: 6958 RVA: 0x004FA208 File Offset: 0x004F8408
		[DebugCommand("hash", "Prints out the hash of all saved (non-volatile) tile data", CommandRequirement.AnyAuthority)]
		public static bool HashCommand(DebugMessage message)
		{
			message.Reply(string.Format("Tile data hash: {0:X8}", WorldGenerator.HashWorld()));
			return true;
		}

		// Token: 0x06001B2F RID: 6959 RVA: 0x004FA225 File Offset: 0x004F8425
		[DebugCommand("snapshot", "Creates a snapshot of the current tile state for the world.", CommandRequirement.AnyAuthority)]
		public static bool SnapshotCommand(DebugMessage message)
		{
			TileSnapshot.Create(null);
			message.Reply("Tile Snapshot Created.");
			return true;
		}

		// Token: 0x06001B30 RID: 6960 RVA: 0x004FA239 File Offset: 0x004F8439
		[DebugCommand("snapclear", "Clears previously created snapshot.", CommandRequirement.AnyAuthority)]
		public static bool SnapshotClearCommand(DebugMessage message)
		{
			TileSnapshot.Clear();
			message.Reply("Tile Snapshot Cleared.");
			return true;
		}

		// Token: 0x06001B31 RID: 6961 RVA: 0x004FA24C File Offset: 0x004F844C
		[DebugCommand("snapsave", "Saves a snapshot in dev-snapshots.", CommandRequirement.AnyAuthority, HelpText = "Usage: /snapsave <name>")]
		public static bool SnapshotSaveCommand(DebugMessage message)
		{
			if (string.IsNullOrWhiteSpace(message.Arguments))
			{
				message.ReplyError("Snapshot name required");
				return true;
			}
			if (!TileSnapshot.IsCreated)
			{
				TileSnapshot.Create(null);
			}
			Directory.CreateDirectory(Path.Combine(Main.SavePath, "dev-snapshots"));
			TileSnapshot.Save(Path.Combine(Main.SavePath, "dev-snapshots", message.Arguments + ".gensnapshot"));
			message.Reply("Tile Snapshot Saved to dev-snapshots/" + message.Arguments + ".gensnapshot");
			return true;
		}

		// Token: 0x06001B32 RID: 6962 RVA: 0x004FA2D8 File Offset: 0x004F84D8
		[DebugCommand("snapload", "Loads a snapshot in dev-snapshots.", CommandRequirement.AnyAuthority, HelpText = "Usage: /snapsave <name>")]
		public static bool SnapshotLoadCommand(DebugMessage message)
		{
			if (string.IsNullOrWhiteSpace(message.Arguments))
			{
				message.ReplyError("Snapshot name required");
				return true;
			}
			if (!TileSnapshot.IsCreated)
			{
				TileSnapshot.Create(null);
			}
			string text = Path.Combine(Main.SavePath, "dev-snapshots", message.Arguments + ".gensnapshot");
			if (!File.Exists(text))
			{
				message.ReplyError("File not found: dev-snapshots/" + message.Arguments + ".gensnapshot");
				return true;
			}
			TileSnapshot.Load(text, null);
			message.Reply("Tile Snapshot Loaded. Use /swap or /restore to apply.");
			return true;
		}

		// Token: 0x06001B33 RID: 6963 RVA: 0x004FA364 File Offset: 0x004F8564
		[DebugCommand("restore", "Restores the world's tiles to the previously created snapshot.", CommandRequirement.AnyAuthority)]
		public static bool RestoreCommand(DebugMessage message)
		{
			if (!TileSnapshot.IsCreated)
			{
				message.ReplyError("No snapshot to restore");
			}
			else if (!TileSnapshot.SizeMatches)
			{
				message.ReplyError("Tile snapshot does not match current world size");
			}
			else
			{
				Stopwatch stopwatch = Stopwatch.StartNew();
				TileSnapshot.Restore();
				message.Reply(string.Format("Tile snapshot restored in {0}ms", stopwatch.ElapsedMilliseconds));
			}
			return true;
		}

		// Token: 0x06001B34 RID: 6964 RVA: 0x004FA3C0 File Offset: 0x004F85C0
		[DebugCommand("swap", "Swaps the world's tiles with the previously created snapshot.", CommandRequirement.AnyAuthority)]
		public static bool SwapCommand(DebugMessage message)
		{
			if (!TileSnapshot.IsCreated)
			{
				message.ReplyError("No snapshot to restore");
			}
			else if (!TileSnapshot.SizeMatches)
			{
				message.ReplyError("Tile snapshot does not match current world size");
			}
			else
			{
				Stopwatch stopwatch = Stopwatch.StartNew();
				TileSnapshot.Swap();
				message.Reply(string.Format("Tile snapshot swapped in {0}ms", stopwatch.ElapsedMilliseconds));
			}
			return true;
		}

		// Token: 0x06001B35 RID: 6965 RVA: 0x004FA41C File Offset: 0x004F861C
		[DebugCommand("snapshotdiff", "Finds differences between the current map and saved snapshot. Use /next to skip through them.", CommandRequirement.SinglePlayer)]
		public static bool SnapshotDiffCommand(DebugMessage message)
		{
			if (!TileSnapshot.IsCreated)
			{
				message.ReplyError("No snapshot to compare");
			}
			else if (!TileSnapshot.SizeMatches)
			{
				message.ReplyError("Tile snapshot does not match current world size");
			}
			else
			{
				ToolkitDebugCommands.FindNextEnumerable = TileSnapshot.Compare();
				ToolkitDebugCommands.FindNext();
			}
			return true;
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06001B36 RID: 6966 RVA: 0x004FA456 File Offset: 0x004F8656
		// (set) Token: 0x06001B37 RID: 6967 RVA: 0x004FA45D File Offset: 0x004F865D
		public static IEnumerable<Point> FindNextEnumerable
		{
			get
			{
				return ToolkitDebugCommands._findNextEnumerable;
			}
			set
			{
				ToolkitDebugCommands._findNextEnumerable = value;
				ToolkitDebugCommands.FindNextEnumerator = null;
			}
		}

		// Token: 0x06001B38 RID: 6968 RVA: 0x004FA46C File Offset: 0x004F866C
		[DebugCommand("find", "Iterates through all instances of a tile in the world. Use /next to skip through them", CommandRequirement.Client, HelpText = "Usage: /find <id>")]
		public static bool FindCommand(DebugMessage message)
		{
			int tileType;
			if (!int.TryParse(message.Arguments, out tileType))
			{
				return false;
			}
			if (tileType < 0 || tileType >= (int)TileID.Count)
			{
				return false;
			}
			string text = string.Empty;
			if (MapHelper.HasOption(tileType, 0))
			{
				text = Lang.GetMapObjectName(MapHelper.TileToLookup(tileType, 0));
			}
			if (text == string.Empty)
			{
				text = "#" + tileType;
			}
			ToolkitDebugCommands.FindNextEnumerable = ToolkitDebugCommands.FindTiles(message, (Tile t) => (int)t.type == tileType, "Tile " + text, 3);
			ToolkitDebugCommands.FindNext();
			return true;
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x004FA520 File Offset: 0x004F8720
		[DebugCommand("findwall", "Iterates through all instances of a wall in the world. Use /next to skip through them", CommandRequirement.Client, HelpText = "Usage: /findwall <id>")]
		public static bool FindWallCommand(DebugMessage message)
		{
			int wallType;
			if (!int.TryParse(message.Arguments, out wallType))
			{
				return false;
			}
			if (wallType <= 0 || wallType >= (int)WallID.Count)
			{
				return false;
			}
			ToolkitDebugCommands.FindNextEnumerable = ToolkitDebugCommands.FindTiles(message, (Tile t) => (int)t.wall == wallType, "Wall #" + wallType, 10);
			ToolkitDebugCommands.FindNext();
			return true;
		}

		// Token: 0x06001B3A RID: 6970 RVA: 0x004FA594 File Offset: 0x004F8794
		[DebugCommand("next", "Finds the next instance of a tile/wall/object. Requires calling /find, /findwall or /snapshotdiff first", CommandRequirement.Client)]
		public static bool NextCommand(DebugMessage message)
		{
			if (ToolkitDebugCommands.FindNextEnumerable == null)
			{
				message.ReplyError("Scan not started. Nothing to find.");
				return true;
			}
			ToolkitDebugCommands.FindNext();
			return true;
		}

		// Token: 0x06001B3B RID: 6971 RVA: 0x004FA5B0 File Offset: 0x004F87B0
		public static void FindNext()
		{
			if (ToolkitDebugCommands.FindNextEnumerator == null)
			{
				ToolkitDebugCommands.FindNextEnumerator = ToolkitDebugCommands.FindNextEnumerable.GetEnumerator();
			}
			if (ToolkitDebugCommands.FindNextEnumerator.MoveNext())
			{
				ToolkitDebugCommands.GoToTile(ToolkitDebugCommands.FindNextEnumerator.Current);
				return;
			}
			ToolkitDebugCommands.FindNextEnumerator = null;
		}

		// Token: 0x06001B3C RID: 6972 RVA: 0x004FA5EA File Offset: 0x004F87EA
		private static IEnumerable<Point> FindTiles(DebugMessage message, Func<Tile, bool> predicate, string descriptor, int skipDistance)
		{
			Point lastPoint = Point.Zero;
			int num;
			for (int x = 0; x < Main.maxTilesX; x = num + 1)
			{
				for (int y = 0; y < Main.maxTilesY; y = num + 1)
				{
					Tile tile = Main.tile[x, y];
					if (tile != null && predicate(tile) && (x - lastPoint.X >= skipDistance || Math.Abs(y - lastPoint.Y) >= skipDistance))
					{
						lastPoint = new Point(x, y);
						message.Reply(descriptor + " found at " + lastPoint);
						yield return lastPoint;
					}
					num = y;
				}
				num = x;
			}
			message.Reply(descriptor + " scan complete.");
			yield break;
		}

		// Token: 0x06001B3D RID: 6973 RVA: 0x004FA60F File Offset: 0x004F880F
		private static void GoToTile(Point coordinates)
		{
			Main.mapFullscreenPos = coordinates.ToVector2() + new Vector2(0.5f, 0.5f);
			Main.Pings.Add(Main.mapFullscreenPos);
		}

		// Token: 0x06001B3E RID: 6974 RVA: 0x004FA63F File Offset: 0x004F883F
		[DebugCommand("showsections", "Toggles net section overlay.", CommandRequirement.Client)]
		public static bool ShowNetSectionsCommand(DebugMessage message)
		{
			DebugOptions.ShowSections = !DebugOptions.ShowSections;
			return true;
		}

		// Token: 0x06001B3F RID: 6975 RVA: 0x004FA64F File Offset: 0x004F884F
		[DebugCommand("nopause", "Makes the game not pause when focus is lost", CommandRequirement.SinglePlayer)]
		public static bool NoPause(DebugMessage message)
		{
			DebugOptions.noPause = !DebugOptions.noPause;
			if (DebugOptions.noPause)
			{
				message.Reply("Pause on focus loss disabled");
			}
			else
			{
				message.Reply("Pause on focus loss enabled");
			}
			return true;
		}

		// Token: 0x06001B40 RID: 6976 RVA: 0x004FA680 File Offset: 0x004F8880
		[DebugCommand("map", "Reveals the full map for the world.", CommandRequirement.Client, HelpText = "Usage: /map [pretty]")]
		public static bool MapCommand(DebugMessage message)
		{
			Main.clearMap = true;
			if (DebugOptions.unlockMap == 0)
			{
				DebugOptions.unlockMap = ((message.Arguments.ToLower().Trim() == "pretty") ? 2 : 1);
				Main.refreshMap = true;
			}
			else
			{
				DebugOptions.unlockMap = 0;
			}
			return true;
		}

		// Token: 0x06001B41 RID: 6977 RVA: 0x004FA6CE File Offset: 0x004F88CE
		[DebugCommand("clearmap", "Deletes the full map for the world.", CommandRequirement.Client)]
		public static bool ClearMapCommand(DebugMessage message)
		{
			Main.clearMap = true;
			Main.Map.Clear();
			Main.refreshMap = true;
			return true;
		}

		// Token: 0x06001B42 RID: 6978 RVA: 0x004FA6E8 File Offset: 0x004F88E8
		[DebugCommand("hideall", "Stops tiles, walls, and water from drawing", CommandRequirement.Client)]
		public static bool HideAll(DebugMessage message)
		{
			int num = 0;
			bool flag = false;
			if (!DebugOptions.hideTiles)
			{
				num++;
			}
			if (!DebugOptions.hideTiles2)
			{
				num++;
			}
			if (!DebugOptions.hideWalls)
			{
				num++;
			}
			if (!DebugOptions.hideWater)
			{
				num++;
			}
			if (num >= 2)
			{
				flag = true;
			}
			DebugOptions.hideTiles = flag;
			DebugOptions.hideTiles2 = flag;
			DebugOptions.hideWalls = flag;
			DebugOptions.hideWater = flag;
			if (flag)
			{
				message.Reply("Everything is hidden");
			}
			else
			{
				message.Reply("Everything is shown");
			}
			return true;
		}

		// Token: 0x06001B43 RID: 6979 RVA: 0x004FA75F File Offset: 0x004F895F
		[DebugCommand("hidetiles", "Stops tiles from drawing on the screen", CommandRequirement.Client)]
		public static bool HideTiles(DebugMessage message)
		{
			DebugOptions.hideTiles = !DebugOptions.hideTiles;
			if (DebugOptions.hideTiles)
			{
				message.Reply("Tiles are hidden");
			}
			else
			{
				message.Reply("Tiles are shown");
			}
			return true;
		}

		// Token: 0x06001B44 RID: 6980 RVA: 0x004FA78E File Offset: 0x004F898E
		[DebugCommand("hidetiles2", "Stops non-solid tiles from drawing on the screen", CommandRequirement.Client)]
		public static bool HideTiles2(DebugMessage message)
		{
			DebugOptions.hideTiles2 = !DebugOptions.hideTiles2;
			if (DebugOptions.hideTiles2)
			{
				message.Reply("Secondary tiles are hidden");
			}
			else
			{
				message.Reply("Secondary tiles are shown");
			}
			return true;
		}

		// Token: 0x06001B45 RID: 6981 RVA: 0x004FA7BD File Offset: 0x004F89BD
		[DebugCommand("hidewalls", "Stops walls from drawing on the screen", CommandRequirement.Client)]
		public static bool HideWalls(DebugMessage message)
		{
			DebugOptions.hideWalls = !DebugOptions.hideWalls;
			if (DebugOptions.hideWalls)
			{
				message.Reply("Walls are hidden");
			}
			else
			{
				message.Reply("Walls are shown");
			}
			return true;
		}

		// Token: 0x06001B46 RID: 6982 RVA: 0x004FA7EC File Offset: 0x004F89EC
		[DebugCommand("hidewater", "Stops water from drawing on the screen", CommandRequirement.Client)]
		public static bool HideWater(DebugMessage message)
		{
			DebugOptions.hideWater = !DebugOptions.hideWater;
			if (DebugOptions.hideWater)
			{
				message.Reply("Water is hidden");
			}
			else
			{
				message.Reply("Water is shown");
			}
			return true;
		}

		// Token: 0x06001B47 RID: 6983 RVA: 0x004FA81B File Offset: 0x004F8A1B
		[DebugCommand("showunbreakablewalls", "Forces unbreakable walls to be visible even when covered by tiles", CommandRequirement.Client)]
		public static bool ShowUnbreakableWall(DebugMessage message)
		{
			DebugOptions.ShowUnbreakableWall = !DebugOptions.ShowUnbreakableWall;
			if (DebugOptions.ShowUnbreakableWall)
			{
				message.Reply("Unbreakable walls are shown");
			}
			else
			{
				message.Reply("Unbreakable walls are hidden");
			}
			return true;
		}

		// Token: 0x06001B48 RID: 6984 RVA: 0x004FA84A File Offset: 0x004F8A4A
		[DebugCommand("showlinks", "Draws gamepad link points as an interface overlay", CommandRequirement.Client)]
		public static bool DrawLinkPoints(DebugMessage message)
		{
			DebugOptions.DrawLinkPoints = !DebugOptions.DrawLinkPoints;
			message.Reply("Gamepad link points are " + (DebugOptions.DrawLinkPoints ? "shown" : "hidden"));
			return true;
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x004FA87D File Offset: 0x004F8A7D
		[DebugCommand("shownetoffset", "Draws dust for debugging netOffset", CommandRequirement.Client)]
		public static bool ShowNetOffset(DebugMessage message)
		{
			DebugOptions.ShowNetOffsetDust = !DebugOptions.ShowNetOffsetDust;
			message.Reply("netOffset dust " + (DebugOptions.ShowNetOffsetDust ? "shown" : "hidden"));
			return true;
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x004FA8B0 File Offset: 0x004F8AB0
		[DebugCommand("fakenetoffset", "Sets the netOffset for all entities to a given value (in pixels).", CommandRequirement.Client, HelpText = "Usage: /fakenetoffset <dx> <dy>")]
		public static bool FakeNetOffset(DebugMessage message)
		{
			string[] array = message.Arguments.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
			float num;
			float num2;
			if (array.Length < 2 || !float.TryParse(array[0], out num) || !float.TryParse(array[1], out num2))
			{
				return false;
			}
			DebugOptions.FakeNetOffset = new Vector2(num, num2);
			message.Reply(string.Concat(new object[] { "netOffset set to (", num, ", ", num2, ")" }));
			return true;
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x004FA942 File Offset: 0x004F8B42
		[DebugCommand("nodamagevar", "Removes damage variation (the inherent +/- 15% from damage). Useful for gathering specific data on true damage.", CommandRequirement.Client)]
		public static bool NoDamageVarCommand(DebugMessage message)
		{
			DebugOptions.NoDamageVar = !DebugOptions.NoDamageVar;
			message.Reply("No Damage Vars: " + (DebugOptions.NoDamageVar ? "On" : "Off"));
			return true;
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x004FA975 File Offset: 0x004F8B75
		[DebugCommand("hurtdummies", "Allows projectiles to aim at target dummies.", CommandRequirement.Client)]
		public static bool HurtDummiesCommand(DebugMessage message)
		{
			DebugOptions.LetProjectilesAimAtTargetDummies = !DebugOptions.LetProjectilesAimAtTargetDummies;
			message.Reply("Aim At Dummies: " + (DebugOptions.LetProjectilesAimAtTargetDummies ? "Enabled" : "Disabled"));
			return true;
		}

		// Token: 0x06001B4D RID: 6989 RVA: 0x004FA9A8 File Offset: 0x004F8BA8
		[DebugCommand("practice", "Toggles practice mode, which resets boss fights when you would take lethal damage.", CommandRequirement.SinglePlayer)]
		public static bool PracticeCommand(DebugMessage message)
		{
			DebugOptions.PracticeMode = !DebugOptions.PracticeMode;
			message.Reply("Practice Mode " + (DebugOptions.PracticeMode ? "enabled" : "disabled"));
			return true;
		}

		// Token: 0x06001B4E RID: 6990 RVA: 0x004FA9DC File Offset: 0x004F8BDC
		[DebugCommand("showdebug", "Toggles command reporting.", CommandRequirement.MultiplayerRPC | CommandRequirement.LocalServer)]
		public static bool ShowDebugCommand(DebugMessage message)
		{
			if (message.Author != 255 && !Main.player[(int)message.Author].host)
			{
				message.ReplyError("/showdebug can only be toggled by the host or server console.");
				return true;
			}
			if (DebugOptions.Shared_ReportCommandUsage)
			{
				DebugOptions.Shared_ReportCommandUsage = false;
				ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Command reporting disabled"), new Color(250, 250, 0), -1);
			}
			else
			{
				DebugOptions.Shared_ReportCommandUsage = true;
				ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Command reporting enabled"), new Color(250, 250, 0), -1);
			}
			NetMessage.SendData(94, -1, -1, NetworkText.FromLiteral("/showdebug"), 0, (float)(DebugOptions.Shared_ReportCommandUsage ? 1 : 0), 0f, 0f, 0, 0, 0);
			return true;
		}

		// Token: 0x06001B4F RID: 6991 RVA: 0x00009E46 File Offset: 0x00008046
		// Note: this type is marked as 'beforefieldinit'.
		static ToolkitDebugCommands()
		{
		}

		// Token: 0x0400155D RID: 5469
		private static IEnumerable<Point> _findNextEnumerable;

		// Token: 0x0400155E RID: 5470
		private static IEnumerator<Point> FindNextEnumerator;

		// Token: 0x0200072B RID: 1835
		[CompilerGenerated]
		private sealed class <>c__DisplayClass30_0
		{
			// Token: 0x06004082 RID: 16514 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass30_0()
			{
			}

			// Token: 0x06004083 RID: 16515 RVA: 0x0069E76E File Offset: 0x0069C96E
			internal bool <FindCommand>b__0(Tile t)
			{
				return (int)t.type == this.tileType;
			}

			// Token: 0x04006985 RID: 27013
			public int tileType;
		}

		// Token: 0x0200072C RID: 1836
		[CompilerGenerated]
		private sealed class <>c__DisplayClass31_0
		{
			// Token: 0x06004084 RID: 16516 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass31_0()
			{
			}

			// Token: 0x06004085 RID: 16517 RVA: 0x0069E77E File Offset: 0x0069C97E
			internal bool <FindWallCommand>b__0(Tile t)
			{
				return (int)t.wall == this.wallType;
			}

			// Token: 0x04006986 RID: 27014
			public int wallType;
		}

		// Token: 0x0200072D RID: 1837
		[CompilerGenerated]
		private sealed class <FindTiles>d__34 : IEnumerable<Point>, IEnumerable, IEnumerator<Point>, IDisposable, IEnumerator
		{
			// Token: 0x06004086 RID: 16518 RVA: 0x0069E78E File Offset: 0x0069C98E
			[DebuggerHidden]
			public <FindTiles>d__34(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
			}

			// Token: 0x06004087 RID: 16519 RVA: 0x00009E46 File Offset: 0x00008046
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06004088 RID: 16520 RVA: 0x0069E7B0 File Offset: 0x0069C9B0
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num == 0)
				{
					this.<>1__state = -1;
					lastPoint = Point.Zero;
					x = 0;
					goto IL_012D;
				}
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				IL_00FD:
				int num2 = y;
				y = num2 + 1;
				IL_010D:
				if (y >= Main.maxTilesY)
				{
					num2 = x;
					x = num2 + 1;
				}
				else
				{
					Tile tile = Main.tile[x, y];
					if (tile != null && predicate(tile) && (x - lastPoint.X >= skipDistance || Math.Abs(y - lastPoint.Y) >= skipDistance))
					{
						lastPoint = new Point(x, y);
						message.Reply(descriptor + " found at " + lastPoint);
						this.<>2__current = lastPoint;
						this.<>1__state = 1;
						return true;
					}
					goto IL_00FD;
				}
				IL_012D:
				if (x >= Main.maxTilesX)
				{
					message.Reply(descriptor + " scan complete.");
					return false;
				}
				y = 0;
				goto IL_010D;
			}

			// Token: 0x17000516 RID: 1302
			// (get) Token: 0x06004089 RID: 16521 RVA: 0x0069E916 File Offset: 0x0069CB16
			Point IEnumerator<Point>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600408A RID: 16522 RVA: 0x0066E2F4 File Offset: 0x0066C4F4
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000517 RID: 1303
			// (get) Token: 0x0600408B RID: 16523 RVA: 0x0069E91E File Offset: 0x0069CB1E
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600408C RID: 16524 RVA: 0x0069E92C File Offset: 0x0069CB2C
			[DebuggerHidden]
			IEnumerator<Point> IEnumerable<Point>.GetEnumerator()
			{
				ToolkitDebugCommands.<FindTiles>d__34 <FindTiles>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.<>1__state = 0;
					<FindTiles>d__ = this;
				}
				else
				{
					<FindTiles>d__ = new ToolkitDebugCommands.<FindTiles>d__34(0);
				}
				<FindTiles>d__.message = message;
				<FindTiles>d__.predicate = predicate;
				<FindTiles>d__.descriptor = descriptor;
				<FindTiles>d__.skipDistance = skipDistance;
				return <FindTiles>d__;
			}

			// Token: 0x0600408D RID: 16525 RVA: 0x0069E998 File Offset: 0x0069CB98
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<Microsoft.Xna.Framework.Point>.GetEnumerator();
			}

			// Token: 0x04006987 RID: 27015
			private int <>1__state;

			// Token: 0x04006988 RID: 27016
			private Point <>2__current;

			// Token: 0x04006989 RID: 27017
			private int <>l__initialThreadId;

			// Token: 0x0400698A RID: 27018
			private Func<Tile, bool> predicate;

			// Token: 0x0400698B RID: 27019
			public Func<Tile, bool> <>3__predicate;

			// Token: 0x0400698C RID: 27020
			private int skipDistance;

			// Token: 0x0400698D RID: 27021
			public int <>3__skipDistance;

			// Token: 0x0400698E RID: 27022
			private DebugMessage message;

			// Token: 0x0400698F RID: 27023
			public DebugMessage <>3__message;

			// Token: 0x04006990 RID: 27024
			private string descriptor;

			// Token: 0x04006991 RID: 27025
			public string <>3__descriptor;

			// Token: 0x04006992 RID: 27026
			private Point <lastPoint>5__2;

			// Token: 0x04006993 RID: 27027
			private int <x>5__3;

			// Token: 0x04006994 RID: 27028
			private int <y>5__4;
		}
	}
}
