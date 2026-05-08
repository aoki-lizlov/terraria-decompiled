using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000673 RID: 1651
	internal sealed class MemberPrimitiveUnTyped : IStreamable
	{
		// Token: 0x06003DF9 RID: 15865 RVA: 0x000025BE File Offset: 0x000007BE
		internal MemberPrimitiveUnTyped()
		{
		}

		// Token: 0x06003DFA RID: 15866 RVA: 0x000D729B File Offset: 0x000D549B
		internal void Set(InternalPrimitiveTypeE typeInformation, object value)
		{
			this.typeInformation = typeInformation;
			this.value = value;
		}

		// Token: 0x06003DFB RID: 15867 RVA: 0x000D72AB File Offset: 0x000D54AB
		internal void Set(InternalPrimitiveTypeE typeInformation)
		{
			this.typeInformation = typeInformation;
		}

		// Token: 0x06003DFC RID: 15868 RVA: 0x000D72B4 File Offset: 0x000D54B4
		public void Write(__BinaryWriter sout)
		{
			sout.WriteValue(this.typeInformation, this.value);
		}

		// Token: 0x06003DFD RID: 15869 RVA: 0x000D72C8 File Offset: 0x000D54C8
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.value = input.ReadValue(this.typeInformation);
		}

		// Token: 0x06003DFE RID: 15870 RVA: 0x00004088 File Offset: 0x00002288
		public void Dump()
		{
		}

		// Token: 0x06003DFF RID: 15871 RVA: 0x000D72DC File Offset: 0x000D54DC
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			if (BCLDebug.CheckEnabled("BINARY"))
			{
				Converter.ToComType(this.typeInformation);
			}
		}

		// Token: 0x040027FF RID: 10239
		internal InternalPrimitiveTypeE typeInformation;

		// Token: 0x04002800 RID: 10240
		internal object value;
	}
}
