using System;
using System.Collections.Generic;

namespace Terraria.Net
{
	// Token: 0x02000166 RID: 358
	public class NetGroupInfo
	{
		// Token: 0x06001DB4 RID: 7604 RVA: 0x00501E38 File Offset: 0x00500038
		public NetGroupInfo()
		{
			this._infoProviders = new List<NetGroupInfo.INetGroupInfoProvider>();
			this._infoProviders.Add(new NetGroupInfo.IPAddressInfoProvider());
			this._infoProviders.Add(new NetGroupInfo.SteamLobbyInfoProvider());
		}

		// Token: 0x06001DB5 RID: 7605 RVA: 0x00501EA0 File Offset: 0x005000A0
		public string ComposeInfo()
		{
			List<string> list = new List<string>();
			foreach (NetGroupInfo.INetGroupInfoProvider netGroupInfoProvider in this._infoProviders)
			{
				if (netGroupInfoProvider.HasValidInfo)
				{
					string text = (int)netGroupInfoProvider.Id + this._separatorBetweenIdAndInfo[0] + netGroupInfoProvider.ProvideInfoNeededToJoin();
					string text2 = this.ConvertToSafeInfo(text);
					list.Add(text2);
				}
			}
			return string.Join(this._separatorBetweenInfos[0], list.ToArray());
		}

		// Token: 0x06001DB6 RID: 7606 RVA: 0x00501F40 File Offset: 0x00500140
		public Dictionary<NetGroupInfo.InfoProviderId, string> DecomposeInfo(string info)
		{
			Dictionary<NetGroupInfo.InfoProviderId, string> dictionary = new Dictionary<NetGroupInfo.InfoProviderId, string>();
			foreach (string text in info.Split(this._separatorBetweenInfos, StringSplitOptions.RemoveEmptyEntries))
			{
				string[] array2 = this.ConvertFromSafeInfo(text).Split(this._separatorBetweenIdAndInfo, StringSplitOptions.RemoveEmptyEntries);
				int num;
				if (array2.Length == 2 && int.TryParse(array2[0], out num))
				{
					dictionary[(NetGroupInfo.InfoProviderId)num] = array2[1];
				}
			}
			return dictionary;
		}

		// Token: 0x06001DB7 RID: 7607 RVA: 0x00501FA9 File Offset: 0x005001A9
		private string ConvertToSafeInfo(string text)
		{
			return Uri.EscapeDataString(text);
		}

		// Token: 0x06001DB8 RID: 7608 RVA: 0x00501FB1 File Offset: 0x005001B1
		private string ConvertFromSafeInfo(string text)
		{
			return Uri.UnescapeDataString(text);
		}

		// Token: 0x0400165D RID: 5725
		private readonly string[] _separatorBetweenInfos = new string[] { ", " };

		// Token: 0x0400165E RID: 5726
		private readonly string[] _separatorBetweenIdAndInfo = new string[] { ":" };

		// Token: 0x0400165F RID: 5727
		private List<NetGroupInfo.INetGroupInfoProvider> _infoProviders;

		// Token: 0x02000746 RID: 1862
		public enum InfoProviderId
		{
			// Token: 0x040069CD RID: 27085
			IPAddress,
			// Token: 0x040069CE RID: 27086
			Steam
		}

		// Token: 0x02000747 RID: 1863
		private interface INetGroupInfoProvider
		{
			// Token: 0x1700051C RID: 1308
			// (get) Token: 0x060040C7 RID: 16583
			NetGroupInfo.InfoProviderId Id { get; }

			// Token: 0x1700051D RID: 1309
			// (get) Token: 0x060040C8 RID: 16584
			bool HasValidInfo { get; }

			// Token: 0x060040C9 RID: 16585
			string ProvideInfoNeededToJoin();
		}

		// Token: 0x02000748 RID: 1864
		private class IPAddressInfoProvider : NetGroupInfo.INetGroupInfoProvider
		{
			// Token: 0x1700051E RID: 1310
			// (get) Token: 0x060040CA RID: 16586 RVA: 0x001DAC3B File Offset: 0x001D8E3B
			public NetGroupInfo.InfoProviderId Id
			{
				get
				{
					return NetGroupInfo.InfoProviderId.IPAddress;
				}
			}

			// Token: 0x1700051F RID: 1311
			// (get) Token: 0x060040CB RID: 16587 RVA: 0x001DAC3B File Offset: 0x001D8E3B
			public bool HasValidInfo
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060040CC RID: 16588 RVA: 0x004FD75A File Offset: 0x004FB95A
			public string ProvideInfoNeededToJoin()
			{
				return "";
			}

			// Token: 0x060040CD RID: 16589 RVA: 0x0000357B File Offset: 0x0000177B
			public IPAddressInfoProvider()
			{
			}
		}

		// Token: 0x02000749 RID: 1865
		private class SteamLobbyInfoProvider : NetGroupInfo.INetGroupInfoProvider
		{
			// Token: 0x17000520 RID: 1312
			// (get) Token: 0x060040CE RID: 16590 RVA: 0x000379E9 File Offset: 0x00035BE9
			public NetGroupInfo.InfoProviderId Id
			{
				get
				{
					return NetGroupInfo.InfoProviderId.Steam;
				}
			}

			// Token: 0x17000521 RID: 1313
			// (get) Token: 0x060040CF RID: 16591 RVA: 0x0069ED69 File Offset: 0x0069CF69
			public bool HasValidInfo
			{
				get
				{
					return Main.LobbyId > 0UL;
				}
			}

			// Token: 0x060040D0 RID: 16592 RVA: 0x0069ED74 File Offset: 0x0069CF74
			public string ProvideInfoNeededToJoin()
			{
				return Main.LobbyId.ToString();
			}

			// Token: 0x060040D1 RID: 16593 RVA: 0x0000357B File Offset: 0x0000177B
			public SteamLobbyInfoProvider()
			{
			}
		}
	}
}
