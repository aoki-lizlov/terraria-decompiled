using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200066F RID: 1647
	internal sealed class MemberPrimitiveTyped : IStreamable
	{
		// Token: 0x06003DE2 RID: 15842 RVA: 0x000025BE File Offset: 0x000007BE
		internal MemberPrimitiveTyped()
		{
		}

		// Token: 0x06003DE3 RID: 15843 RVA: 0x000D6A7D File Offset: 0x000D4C7D
		internal void Set(InternalPrimitiveTypeE primitiveTypeEnum, object value)
		{
			this.primitiveTypeEnum = primitiveTypeEnum;
			this.value = value;
		}

		// Token: 0x06003DE4 RID: 15844 RVA: 0x000D6A8D File Offset: 0x000D4C8D
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(8);
			sout.WriteByte((byte)this.primitiveTypeEnum);
			sout.WriteValue(this.primitiveTypeEnum, this.value);
		}

		// Token: 0x06003DE5 RID: 15845 RVA: 0x000D6AB5 File Offset: 0x000D4CB5
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.primitiveTypeEnum = (InternalPrimitiveTypeE)input.ReadByte();
			this.value = input.ReadValue(this.primitiveTypeEnum);
		}

		// Token: 0x06003DE6 RID: 15846 RVA: 0x00004088 File Offset: 0x00002288
		public void Dump()
		{
		}

		// Token: 0x06003DE7 RID: 15847 RVA: 0x000D5BA5 File Offset: 0x000D3DA5
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x040027E5 RID: 10213
		internal InternalPrimitiveTypeE primitiveTypeEnum;

		// Token: 0x040027E6 RID: 10214
		internal object value;
	}
}
