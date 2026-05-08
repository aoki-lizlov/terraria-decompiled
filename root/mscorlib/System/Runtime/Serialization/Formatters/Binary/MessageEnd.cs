using System;
using System.Diagnostics;
using System.IO;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000676 RID: 1654
	internal sealed class MessageEnd : IStreamable
	{
		// Token: 0x06003E0D RID: 15885 RVA: 0x000025BE File Offset: 0x000007BE
		internal MessageEnd()
		{
		}

		// Token: 0x06003E0E RID: 15886 RVA: 0x000D7400 File Offset: 0x000D5600
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(11);
		}

		// Token: 0x06003E0F RID: 15887 RVA: 0x00004088 File Offset: 0x00002288
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
		}

		// Token: 0x06003E10 RID: 15888 RVA: 0x00004088 File Offset: 0x00002288
		public void Dump()
		{
		}

		// Token: 0x06003E11 RID: 15889 RVA: 0x00004088 File Offset: 0x00002288
		public void Dump(Stream sout)
		{
		}

		// Token: 0x06003E12 RID: 15890 RVA: 0x000D740A File Offset: 0x000D560A
		[Conditional("_LOGGING")]
		private void DumpInternal(Stream sout)
		{
			if (BCLDebug.CheckEnabled("BINARY") && sout != null && sout.CanSeek)
			{
				long length = sout.Length;
			}
		}
	}
}
