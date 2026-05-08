using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000668 RID: 1640
	internal sealed class BinaryCrossAppDomainAssembly : IStreamable
	{
		// Token: 0x06003DB8 RID: 15800 RVA: 0x000025BE File Offset: 0x000007BE
		internal BinaryCrossAppDomainAssembly()
		{
		}

		// Token: 0x06003DB9 RID: 15801 RVA: 0x000D5DBA File Offset: 0x000D3FBA
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(20);
			sout.WriteInt32(this.assemId);
			sout.WriteInt32(this.assemblyIndex);
		}

		// Token: 0x06003DBA RID: 15802 RVA: 0x000D5DDC File Offset: 0x000D3FDC
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.assemId = input.ReadInt32();
			this.assemblyIndex = input.ReadInt32();
		}

		// Token: 0x06003DBB RID: 15803 RVA: 0x00004088 File Offset: 0x00002288
		public void Dump()
		{
		}

		// Token: 0x06003DBC RID: 15804 RVA: 0x000D5BA5 File Offset: 0x000D3DA5
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x040027C3 RID: 10179
		internal int assemId;

		// Token: 0x040027C4 RID: 10180
		internal int assemblyIndex;
	}
}
