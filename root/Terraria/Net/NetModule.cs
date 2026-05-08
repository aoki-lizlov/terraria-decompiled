using System;
using System.IO;

namespace Terraria.Net
{
	// Token: 0x0200016E RID: 366
	public abstract class NetModule
	{
		// Token: 0x06001DD3 RID: 7635
		public abstract bool Deserialize(BinaryReader reader, int userId);

		// Token: 0x06001DD4 RID: 7636 RVA: 0x005025B8 File Offset: 0x005007B8
		protected static NetPacket CreatePacket<T>(int maxSize = 65530) where T : NetModule
		{
			ushort id = NetManager.Instance.GetId<T>();
			return new NetPacket(id, maxSize);
		}

		// Token: 0x06001DD5 RID: 7637 RVA: 0x0000357B File Offset: 0x0000177B
		protected NetModule()
		{
		}
	}
}
