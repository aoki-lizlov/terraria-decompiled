using System;
using rail;

namespace Terraria.Net
{
	// Token: 0x0200016B RID: 363
	public class WeGameAddress : RemoteAddress
	{
		// Token: 0x06001DC7 RID: 7623 RVA: 0x005020FC File Offset: 0x005002FC
		public WeGameAddress(RailID id, string name)
		{
			this.Type = AddressType.WeGame;
			this.rail_id = id;
			this.nickname = name;
		}

		// Token: 0x06001DC8 RID: 7624 RVA: 0x00502119 File Offset: 0x00500319
		public override string ToString()
		{
			return "WEGAME_0:" + this.rail_id.id_.ToString();
		}

		// Token: 0x06001DC9 RID: 7625 RVA: 0x0050200D File Offset: 0x0050020D
		public override string GetIdentifier()
		{
			return this.ToString();
		}

		// Token: 0x06001DCA RID: 7626 RVA: 0x00502135 File Offset: 0x00500335
		public override bool IsLocalHost()
		{
			return Program.LaunchParameters.ContainsKey("-localwegameid") && Program.LaunchParameters["-localwegameid"].Equals(this.rail_id.id_.ToString());
		}

		// Token: 0x06001DCB RID: 7627 RVA: 0x0050216E File Offset: 0x0050036E
		public override string GetFriendlyName()
		{
			return this.nickname;
		}

		// Token: 0x04001669 RID: 5737
		public readonly RailID rail_id;

		// Token: 0x0400166A RID: 5738
		private string nickname;
	}
}
