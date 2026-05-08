using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200066C RID: 1644
	internal sealed class BinaryObjectString : IStreamable
	{
		// Token: 0x06003DD2 RID: 15826 RVA: 0x000025BE File Offset: 0x000007BE
		internal BinaryObjectString()
		{
		}

		// Token: 0x06003DD3 RID: 15827 RVA: 0x000D69D2 File Offset: 0x000D4BD2
		internal void Set(int objectId, string value)
		{
			this.objectId = objectId;
			this.value = value;
		}

		// Token: 0x06003DD4 RID: 15828 RVA: 0x000D69E2 File Offset: 0x000D4BE2
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(6);
			sout.WriteInt32(this.objectId);
			sout.WriteString(this.value);
		}

		// Token: 0x06003DD5 RID: 15829 RVA: 0x000D6A03 File Offset: 0x000D4C03
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.objectId = input.ReadInt32();
			this.value = input.ReadString();
		}

		// Token: 0x06003DD6 RID: 15830 RVA: 0x00004088 File Offset: 0x00002288
		public void Dump()
		{
		}

		// Token: 0x06003DD7 RID: 15831 RVA: 0x000D5BA5 File Offset: 0x000D3DA5
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x040027E0 RID: 10208
		internal int objectId;

		// Token: 0x040027E1 RID: 10209
		internal string value;
	}
}
