using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000667 RID: 1639
	internal sealed class BinaryAssembly : IStreamable
	{
		// Token: 0x06003DB2 RID: 15794 RVA: 0x000025BE File Offset: 0x000007BE
		internal BinaryAssembly()
		{
		}

		// Token: 0x06003DB3 RID: 15795 RVA: 0x000D5D6E File Offset: 0x000D3F6E
		internal void Set(int assemId, string assemblyString)
		{
			this.assemId = assemId;
			this.assemblyString = assemblyString;
		}

		// Token: 0x06003DB4 RID: 15796 RVA: 0x000D5D7E File Offset: 0x000D3F7E
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(12);
			sout.WriteInt32(this.assemId);
			sout.WriteString(this.assemblyString);
		}

		// Token: 0x06003DB5 RID: 15797 RVA: 0x000D5DA0 File Offset: 0x000D3FA0
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.assemId = input.ReadInt32();
			this.assemblyString = input.ReadString();
		}

		// Token: 0x06003DB6 RID: 15798 RVA: 0x00004088 File Offset: 0x00002288
		public void Dump()
		{
		}

		// Token: 0x06003DB7 RID: 15799 RVA: 0x000D5BA5 File Offset: 0x000D3DA5
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x040027C1 RID: 10177
		internal int assemId;

		// Token: 0x040027C2 RID: 10178
		internal string assemblyString;
	}
}
