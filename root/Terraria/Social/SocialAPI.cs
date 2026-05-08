using System;
using System.Collections.Generic;
using ReLogic.OS;
using Terraria.Social.Base;
using Terraria.Social.Steam;
using Terraria.Social.WeGame;

namespace Terraria.Social
{
	// Token: 0x02000123 RID: 291
	public static class SocialAPI
	{
		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06001B75 RID: 7029 RVA: 0x004FB0DB File Offset: 0x004F92DB
		public static SocialMode Mode
		{
			get
			{
				return SocialAPI._mode;
			}
		}

		// Token: 0x06001B76 RID: 7030 RVA: 0x004FB0E4 File Offset: 0x004F92E4
		public static void Initialize(SocialMode? mode = null)
		{
			if (mode == null)
			{
				mode = new SocialMode?(SocialMode.None);
				bool dedServ = Main.dedServ;
			}
			SocialAPI._mode = mode.Value;
			SocialAPI._modules = new List<ISocialModule>();
			SocialAPI.JoinRequests = new ServerJoinRequestsManager();
			Main.OnTickForInternalCodeOnly += SocialAPI.JoinRequests.Update;
			SocialMode mode2 = SocialAPI.Mode;
			if (mode2 != SocialMode.Steam)
			{
				if (mode2 == SocialMode.WeGame)
				{
					SocialAPI.LoadWeGame();
				}
			}
			else
			{
				SocialAPI.LoadSteam();
			}
			foreach (ISocialModule socialModule in SocialAPI._modules)
			{
				socialModule.Initialize();
			}
		}

		// Token: 0x06001B77 RID: 7031 RVA: 0x004FB19C File Offset: 0x004F939C
		public static void Shutdown()
		{
			SocialAPI._modules.Reverse();
			foreach (ISocialModule socialModule in SocialAPI._modules)
			{
				socialModule.Shutdown();
			}
		}

		// Token: 0x06001B78 RID: 7032 RVA: 0x004FB1F8 File Offset: 0x004F93F8
		private static T LoadModule<T>() where T : ISocialModule, new()
		{
			T t = new T();
			SocialAPI._modules.Add(t);
			return t;
		}

		// Token: 0x06001B79 RID: 7033 RVA: 0x004FB21C File Offset: 0x004F941C
		private static T LoadModule<T>(T module) where T : ISocialModule
		{
			SocialAPI._modules.Add(module);
			return module;
		}

		// Token: 0x06001B7A RID: 7034 RVA: 0x004FB22F File Offset: 0x004F942F
		private static void LoadDiscord()
		{
			if (Main.dedServ)
			{
				return;
			}
			if (ReLogic.OS.Platform.IsWindows || Environment.Is64BitOperatingSystem)
			{
				bool is64BitProcess = Environment.Is64BitProcess;
			}
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x004FB250 File Offset: 0x004F9450
		private static void LoadSteam()
		{
			SocialAPI.LoadModule<Terraria.Social.Steam.CoreSocialModule>();
			SocialAPI.Friends = SocialAPI.LoadModule<Terraria.Social.Steam.FriendsSocialModule>();
			SocialAPI.Achievements = SocialAPI.LoadModule<Terraria.Social.Steam.AchievementsSocialModule>();
			SocialAPI.Cloud = SocialAPI.LoadModule<Terraria.Social.Steam.CloudSocialModule>();
			SocialAPI.Overlay = SocialAPI.LoadModule<Terraria.Social.Steam.OverlaySocialModule>();
			SocialAPI.Workshop = SocialAPI.LoadModule<Terraria.Social.Steam.WorkshopSocialModule>();
			SocialAPI.Platform = SocialAPI.LoadModule<Terraria.Social.Steam.PlatformSocialModule>();
			if (Main.dedServ)
			{
				SocialAPI.Network = SocialAPI.LoadModule<Terraria.Social.Steam.NetServerSocialModule>();
			}
			else
			{
				SocialAPI.Network = SocialAPI.LoadModule<Terraria.Social.Steam.NetClientSocialModule>();
			}
			WeGameHelper.WriteDebugString("LoadSteam modules", new object[0]);
		}

		// Token: 0x06001B7C RID: 7036 RVA: 0x004FB2CC File Offset: 0x004F94CC
		private static void LoadWeGame()
		{
			SocialAPI.LoadModule<Terraria.Social.WeGame.CoreSocialModule>();
			SocialAPI.Cloud = SocialAPI.LoadModule<Terraria.Social.WeGame.CloudSocialModule>();
			SocialAPI.Friends = SocialAPI.LoadModule<Terraria.Social.WeGame.FriendsSocialModule>();
			SocialAPI.Overlay = SocialAPI.LoadModule<Terraria.Social.WeGame.OverlaySocialModule>();
			if (Main.dedServ)
			{
				SocialAPI.Network = SocialAPI.LoadModule<Terraria.Social.WeGame.NetServerSocialModule>();
			}
			else
			{
				SocialAPI.Network = SocialAPI.LoadModule<Terraria.Social.WeGame.NetClientSocialModule>();
			}
			WeGameHelper.WriteDebugString("LoadWeGame modules", new object[0]);
		}

		// Token: 0x06001B7D RID: 7037 RVA: 0x00009E46 File Offset: 0x00008046
		// Note: this type is marked as 'beforefieldinit'.
		static SocialAPI()
		{
		}

		// Token: 0x0400157C RID: 5500
		private static SocialMode _mode;

		// Token: 0x0400157D RID: 5501
		public static Terraria.Social.Base.FriendsSocialModule Friends;

		// Token: 0x0400157E RID: 5502
		public static Terraria.Social.Base.AchievementsSocialModule Achievements;

		// Token: 0x0400157F RID: 5503
		public static Terraria.Social.Base.CloudSocialModule Cloud;

		// Token: 0x04001580 RID: 5504
		public static Terraria.Social.Base.NetSocialModule Network;

		// Token: 0x04001581 RID: 5505
		public static Terraria.Social.Base.OverlaySocialModule Overlay;

		// Token: 0x04001582 RID: 5506
		public static Terraria.Social.Base.WorkshopSocialModule Workshop;

		// Token: 0x04001583 RID: 5507
		public static ServerJoinRequestsManager JoinRequests;

		// Token: 0x04001584 RID: 5508
		public static Terraria.Social.Base.PlatformSocialModule Platform;

		// Token: 0x04001585 RID: 5509
		private static List<ISocialModule> _modules;
	}
}
