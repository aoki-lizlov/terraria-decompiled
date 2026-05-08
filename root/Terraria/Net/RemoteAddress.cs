using System;

namespace Terraria.Net
{
	// Token: 0x02000168 RID: 360
	public abstract class RemoteAddress
	{
		// Token: 0x06001DB9 RID: 7609
		public abstract string GetIdentifier();

		// Token: 0x06001DBA RID: 7610
		public abstract string GetFriendlyName();

		// Token: 0x06001DBB RID: 7611
		public abstract bool IsLocalHost();

		// Token: 0x06001DBC RID: 7612 RVA: 0x0000357B File Offset: 0x0000177B
		protected RemoteAddress()
		{
		}

		// Token: 0x04001664 RID: 5732
		public AddressType Type;
	}
}
