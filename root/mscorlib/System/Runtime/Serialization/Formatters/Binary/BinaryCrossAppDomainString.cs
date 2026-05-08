using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200066D RID: 1645
	internal sealed class BinaryCrossAppDomainString : IStreamable
	{
		// Token: 0x06003DD8 RID: 15832 RVA: 0x000025BE File Offset: 0x000007BE
		internal BinaryCrossAppDomainString()
		{
		}

		// Token: 0x06003DD9 RID: 15833 RVA: 0x000D6A1D File Offset: 0x000D4C1D
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(19);
			sout.WriteInt32(this.objectId);
			sout.WriteInt32(this.value);
		}

		// Token: 0x06003DDA RID: 15834 RVA: 0x000D6A3F File Offset: 0x000D4C3F
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.objectId = input.ReadInt32();
			this.value = input.ReadInt32();
		}

		// Token: 0x06003DDB RID: 15835 RVA: 0x00004088 File Offset: 0x00002288
		public void Dump()
		{
		}

		// Token: 0x06003DDC RID: 15836 RVA: 0x000D5BA5 File Offset: 0x000D3DA5
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x040027E2 RID: 10210
		internal int objectId;

		// Token: 0x040027E3 RID: 10211
		internal int value;
	}
}
