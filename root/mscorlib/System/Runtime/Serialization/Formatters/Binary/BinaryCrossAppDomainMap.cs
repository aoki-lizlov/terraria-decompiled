using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200066E RID: 1646
	internal sealed class BinaryCrossAppDomainMap : IStreamable
	{
		// Token: 0x06003DDD RID: 15837 RVA: 0x000025BE File Offset: 0x000007BE
		internal BinaryCrossAppDomainMap()
		{
		}

		// Token: 0x06003DDE RID: 15838 RVA: 0x000D6A59 File Offset: 0x000D4C59
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(18);
			sout.WriteInt32(this.crossAppDomainArrayIndex);
		}

		// Token: 0x06003DDF RID: 15839 RVA: 0x000D6A6F File Offset: 0x000D4C6F
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.crossAppDomainArrayIndex = input.ReadInt32();
		}

		// Token: 0x06003DE0 RID: 15840 RVA: 0x00004088 File Offset: 0x00002288
		public void Dump()
		{
		}

		// Token: 0x06003DE1 RID: 15841 RVA: 0x000D5BA5 File Offset: 0x000D3DA5
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x040027E4 RID: 10212
		internal int crossAppDomainArrayIndex;
	}
}
