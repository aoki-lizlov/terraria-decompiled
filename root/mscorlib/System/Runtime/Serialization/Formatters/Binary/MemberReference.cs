using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000674 RID: 1652
	internal sealed class MemberReference : IStreamable
	{
		// Token: 0x06003E00 RID: 15872 RVA: 0x000025BE File Offset: 0x000007BE
		internal MemberReference()
		{
		}

		// Token: 0x06003E01 RID: 15873 RVA: 0x000D72F6 File Offset: 0x000D54F6
		internal void Set(int idRef)
		{
			this.idRef = idRef;
		}

		// Token: 0x06003E02 RID: 15874 RVA: 0x000D72FF File Offset: 0x000D54FF
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(9);
			sout.WriteInt32(this.idRef);
		}

		// Token: 0x06003E03 RID: 15875 RVA: 0x000D7315 File Offset: 0x000D5515
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.idRef = input.ReadInt32();
		}

		// Token: 0x06003E04 RID: 15876 RVA: 0x00004088 File Offset: 0x00002288
		public void Dump()
		{
		}

		// Token: 0x06003E05 RID: 15877 RVA: 0x000D5BA5 File Offset: 0x000D3DA5
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x04002801 RID: 10241
		internal int idRef;
	}
}
