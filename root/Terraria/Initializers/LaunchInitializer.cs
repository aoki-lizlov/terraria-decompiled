using System;
using System.Diagnostics;
using Terraria.IO;
using Terraria.Localization;
using Terraria.Social;
using Terraria.Utilities;

namespace Terraria.Initializers
{
	// Token: 0x02000084 RID: 132
	public static class LaunchInitializer
	{
		// Token: 0x06001594 RID: 5524 RVA: 0x004CBB95 File Offset: 0x004C9D95
		public static void LoadParameters(Main game)
		{
			LaunchInitializer.LoadSharedParameters(game);
			if (Main.dedServ)
			{
				LaunchInitializer.LoadServerParameters(game);
			}
			LaunchInitializer.LoadClientParameters(game);
		}

		// Token: 0x06001595 RID: 5525 RVA: 0x004CBBB0 File Offset: 0x004C9DB0
		private static void LoadSharedParameters(Main game)
		{
			string text;
			if ((text = LaunchInitializer.TryParameter(new string[] { "-loadlib" })) != null)
			{
				game.loadLib(text);
			}
			string text2;
			int num;
			if ((text2 = LaunchInitializer.TryParameter(new string[] { "-p", "-port" })) != null && int.TryParse(text2, out num))
			{
				Netplay.ListenPort = num;
			}
		}

		// Token: 0x06001596 RID: 5526 RVA: 0x004CBC0C File Offset: 0x004C9E0C
		private static void LoadClientParameters(Main game)
		{
			string text;
			if ((text = LaunchInitializer.TryParameter(new string[] { "-j", "-join" })) != null)
			{
				game.AutoJoin(text);
			}
			string text2;
			if ((text2 = LaunchInitializer.TryParameter(new string[] { "-pass", "-password" })) != null)
			{
				Netplay.ServerPassword = Main.ConvertFromSafeArgument(text2);
				game.AutoPass();
			}
			if (LaunchInitializer.HasParameter(new string[] { "-host" }))
			{
				game.AutoHost();
			}
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x004CBC8C File Offset: 0x004C9E8C
		private static void LoadServerParameters(Main game)
		{
			try
			{
				string text;
				if ((text = LaunchInitializer.TryParameter(new string[] { "-forcepriority" })) != null)
				{
					Process currentProcess = Process.GetCurrentProcess();
					int num;
					if (int.TryParse(text, out num))
					{
						switch (num)
						{
						case 0:
							currentProcess.PriorityClass = ProcessPriorityClass.RealTime;
							break;
						case 1:
							currentProcess.PriorityClass = ProcessPriorityClass.High;
							break;
						case 2:
							currentProcess.PriorityClass = ProcessPriorityClass.AboveNormal;
							break;
						case 3:
							currentProcess.PriorityClass = ProcessPriorityClass.Normal;
							break;
						case 4:
							currentProcess.PriorityClass = ProcessPriorityClass.BelowNormal;
							break;
						case 5:
							currentProcess.PriorityClass = ProcessPriorityClass.Idle;
							break;
						default:
							currentProcess.PriorityClass = ProcessPriorityClass.High;
							break;
						}
					}
					else
					{
						currentProcess.PriorityClass = ProcessPriorityClass.High;
					}
				}
				else
				{
					Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
				}
			}
			catch
			{
			}
			string text2;
			if ((text2 = LaunchInitializer.TryParameter(new string[] { "-maxplayers", "-players" })) != null)
			{
				int num2 = Convert.ToInt32(text2);
				if (num2 <= 255 && num2 >= 1)
				{
					game.SetNetPlayers(num2);
				}
			}
			string text3;
			if ((text3 = LaunchInitializer.TryParameter(new string[] { "-pass", "-password" })) != null)
			{
				Netplay.ServerPassword = Main.ConvertFromSafeArgument(text3);
			}
			string text4;
			int num3;
			if ((text4 = LaunchInitializer.TryParameter(new string[] { "-lang" })) != null && int.TryParse(text4, out num3))
			{
				LanguageManager.Instance.SetLanguage(num3);
			}
			if ((text4 = LaunchInitializer.TryParameter(new string[] { "-language" })) != null)
			{
				LanguageManager.Instance.SetLanguage(text4);
			}
			string text5;
			if ((text5 = LaunchInitializer.TryParameter(new string[] { "-worldname" })) != null)
			{
				game.SetWorldName(text5);
			}
			string text6;
			if ((text6 = LaunchInitializer.TryParameter(new string[] { "-motd" })) != null)
			{
				game.NewMOTD(text6);
			}
			string text7;
			if ((text7 = LaunchInitializer.TryParameter(new string[] { "-banlist" })) != null)
			{
				Netplay.BanFilePath = text7;
			}
			if (LaunchInitializer.HasParameter(new string[] { "-autoshutdown" }))
			{
				game.EnableAutoShutdown();
			}
			string text8;
			if ((text8 = LaunchInitializer.TryParameter(new string[] { "-hosttoken" })) != null)
			{
				Netplay.HostToken = text8;
			}
			if (LaunchInitializer.HasParameter(new string[] { "-secure" }))
			{
				Netplay.SpamCheck = true;
			}
			string text9;
			if ((text9 = LaunchInitializer.TryParameter(new string[] { "-worldrollbackstokeep" })) != null)
			{
				game.setServerWorldRollbacks(text9);
			}
			string text10;
			if ((text10 = LaunchInitializer.TryParameter(new string[] { "-autocreate" })) != null)
			{
				game.autoCreate(text10);
			}
			if (LaunchInitializer.HasParameter(new string[] { "-noupnp" }))
			{
				Netplay.UseUPNP = false;
			}
			if (LaunchInitializer.HasParameter(new string[] { "-experimental" }))
			{
				Main.UseExperimentalFeatures = true;
			}
			string text11;
			if ((text11 = LaunchInitializer.TryParameter(new string[] { "-world" })) != null)
			{
				if (FileUtilities.Exists(text11, false) || !Main.autoGen)
				{
					game.SetWorld(text11, false);
				}
				else
				{
					new WorldFileData(text11, false).SetAsActive();
					Main.autoGenFileLocation = text11;
				}
			}
			else if (SocialAPI.Mode == SocialMode.Steam && (text11 = LaunchInitializer.TryParameter(new string[] { "-cloudworld" })) != null)
			{
				if (FileUtilities.Exists(text11, true) || !Main.autoGen)
				{
					game.SetWorld(text11, true);
				}
				else
				{
					new WorldFileData(text11, true).SetAsActive();
					Main.autoGenFileLocation = text11;
				}
			}
			string text12;
			if ((text12 = LaunchInitializer.TryParameter(new string[] { "-config" })) != null)
			{
				game.LoadDedConfig(text12);
			}
			string text13;
			if ((text13 = LaunchInitializer.TryParameter(new string[] { "-seed" })) != null)
			{
				Main.AutogenSeedName = text13;
			}
		}

		// Token: 0x06001598 RID: 5528 RVA: 0x004CC02C File Offset: 0x004CA22C
		private static bool HasParameter(params string[] keys)
		{
			for (int i = 0; i < keys.Length; i++)
			{
				if (Program.LaunchParameters.ContainsKey(keys[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001599 RID: 5529 RVA: 0x004CC05C File Offset: 0x004CA25C
		private static string TryParameter(params string[] keys)
		{
			for (int i = 0; i < keys.Length; i++)
			{
				string text;
				if (Program.LaunchParameters.TryGetValue(keys[i], out text))
				{
					if (text == null)
					{
						text = "";
					}
					return text;
				}
			}
			return null;
		}
	}
}
