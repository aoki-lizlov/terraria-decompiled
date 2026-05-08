using System;
using System.Net;

namespace Terraria.Net
{
	// Token: 0x02000169 RID: 361
	public class TcpAddress : RemoteAddress
	{
		// Token: 0x06001DBD RID: 7613 RVA: 0x00501FB9 File Offset: 0x005001B9
		public TcpAddress(IPAddress address, int port)
		{
			this.Type = AddressType.Tcp;
			this.Address = address;
			this.Port = port;
		}

		// Token: 0x06001DBE RID: 7614 RVA: 0x00501FD6 File Offset: 0x005001D6
		public override string GetIdentifier()
		{
			return this.Address.ToString();
		}

		// Token: 0x06001DBF RID: 7615 RVA: 0x00501FE3 File Offset: 0x005001E3
		public override bool IsLocalHost()
		{
			return this.Address.Equals(IPAddress.Loopback);
		}

		// Token: 0x06001DC0 RID: 7616 RVA: 0x00501FF5 File Offset: 0x005001F5
		public override string ToString()
		{
			return new IPEndPoint(this.Address, this.Port).ToString();
		}

		// Token: 0x06001DC1 RID: 7617 RVA: 0x0050200D File Offset: 0x0050020D
		public override string GetFriendlyName()
		{
			return this.ToString();
		}

		// Token: 0x04001665 RID: 5733
		public IPAddress Address;

		// Token: 0x04001666 RID: 5734
		public int Port;
	}
}
