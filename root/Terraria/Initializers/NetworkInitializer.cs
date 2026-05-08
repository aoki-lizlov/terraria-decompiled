using System;
using Terraria.GameContent;
using Terraria.GameContent.Items;
using Terraria.GameContent.NetModules;
using Terraria.Net;

namespace Terraria.Initializers
{
	// Token: 0x02000086 RID: 134
	public static class NetworkInitializer
	{
		// Token: 0x0600159B RID: 5531 RVA: 0x004CC1CC File Offset: 0x004CA3CC
		public static void Load()
		{
			NetManager.Instance.Register<NetLiquidModule>();
			NetManager.Instance.Register<NetTextModule>();
			NetManager.Instance.Register<NetPingModule>();
			NetManager.Instance.Register<NetAmbienceModule>();
			NetManager.Instance.Register<NetBestiaryModule>();
			NetManager.Instance.Register<NetCreativePowersModule>();
			NetManager.Instance.Register<NetCreativeUnlocksPlayerReportModule>();
			NetManager.Instance.Register<NetTeleportPylonModule>();
			NetManager.Instance.Register<NetParticlesModule>();
			NetManager.Instance.Register<NetCreativePowerPermissionsModule>();
			NetManager.Instance.Register<BannerSystem.NetBannersModule>();
			NetManager.Instance.Register<CraftingRequests.NetCraftingRequestsModule>();
			NetManager.Instance.Register<TagEffectState.NetModule>();
			NetManager.Instance.Register<LeashedEntity.NetModule>();
			NetManager.Instance.Register<UnbreakableWallScan.NetModule>();
		}
	}
}
