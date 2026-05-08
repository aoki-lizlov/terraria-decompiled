using System;
using Steamworks;

namespace Terraria.Net
{
	// Token: 0x0200016A RID: 362
	public class SteamAddress : RemoteAddress
	{
		// Token: 0x06001DC2 RID: 7618 RVA: 0x00502015 File Offset: 0x00500215
		public SteamAddress(CSteamID steamId)
		{
			this.Type = AddressType.Steam;
			this.SteamId = steamId;
		}

		// Token: 0x06001DC3 RID: 7619 RVA: 0x0050202C File Offset: 0x0050022C
		public override string ToString()
		{
			string text = (this.SteamId.m_SteamID % 2UL).ToString();
			string text2 = ((this.SteamId.m_SteamID - (76561197960265728UL + this.SteamId.m_SteamID % 2UL)) / 2UL).ToString();
			return "STEAM_0:" + text + ":" + text2;
		}

		// Token: 0x06001DC4 RID: 7620 RVA: 0x0050200D File Offset: 0x0050020D
		public override string GetIdentifier()
		{
			return this.ToString();
		}

		// Token: 0x06001DC5 RID: 7621 RVA: 0x00502094 File Offset: 0x00500294
		public override bool IsLocalHost()
		{
			return Program.LaunchParameters.ContainsKey("-localsteamid") && Program.LaunchParameters["-localsteamid"].Equals(this.SteamId.m_SteamID.ToString());
		}

		// Token: 0x06001DC6 RID: 7622 RVA: 0x005020DB File Offset: 0x005002DB
		public override string GetFriendlyName()
		{
			if (this._friendlyName == null)
			{
				this._friendlyName = SteamFriends.GetFriendPersonaName(this.SteamId);
			}
			return this._friendlyName;
		}

		// Token: 0x04001667 RID: 5735
		public readonly CSteamID SteamId;

		// Token: 0x04001668 RID: 5736
		private string _friendlyName;
	}
}
