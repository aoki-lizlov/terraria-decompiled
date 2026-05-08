using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000669 RID: 1641
	internal sealed class BinaryObject : IStreamable
	{
		// Token: 0x06003DBD RID: 15805 RVA: 0x000025BE File Offset: 0x000007BE
		internal BinaryObject()
		{
		}

		// Token: 0x06003DBE RID: 15806 RVA: 0x000D5DF6 File Offset: 0x000D3FF6
		internal void Set(int objectId, int mapId)
		{
			this.objectId = objectId;
			this.mapId = mapId;
		}

		// Token: 0x06003DBF RID: 15807 RVA: 0x000D5E06 File Offset: 0x000D4006
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(1);
			sout.WriteInt32(this.objectId);
			sout.WriteInt32(this.mapId);
		}

		// Token: 0x06003DC0 RID: 15808 RVA: 0x000D5E27 File Offset: 0x000D4027
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.objectId = input.ReadInt32();
			this.mapId = input.ReadInt32();
		}

		// Token: 0x06003DC1 RID: 15809 RVA: 0x00004088 File Offset: 0x00002288
		public void Dump()
		{
		}

		// Token: 0x06003DC2 RID: 15810 RVA: 0x000D5BA5 File Offset: 0x000D3DA5
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x040027C5 RID: 10181
		internal int objectId;

		// Token: 0x040027C6 RID: 10182
		internal int mapId;
	}
}
