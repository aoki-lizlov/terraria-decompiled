using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000675 RID: 1653
	internal sealed class ObjectNull : IStreamable
	{
		// Token: 0x06003E06 RID: 15878 RVA: 0x000025BE File Offset: 0x000007BE
		internal ObjectNull()
		{
		}

		// Token: 0x06003E07 RID: 15879 RVA: 0x000D7323 File Offset: 0x000D5523
		internal void SetNullCount(int nullCount)
		{
			this.nullCount = nullCount;
		}

		// Token: 0x06003E08 RID: 15880 RVA: 0x000D732C File Offset: 0x000D552C
		public void Write(__BinaryWriter sout)
		{
			if (this.nullCount == 1)
			{
				sout.WriteByte(10);
				return;
			}
			if (this.nullCount < 256)
			{
				sout.WriteByte(13);
				sout.WriteByte((byte)this.nullCount);
				return;
			}
			sout.WriteByte(14);
			sout.WriteInt32(this.nullCount);
		}

		// Token: 0x06003E09 RID: 15881 RVA: 0x000D7382 File Offset: 0x000D5582
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.Read(input, BinaryHeaderEnum.ObjectNull);
		}

		// Token: 0x06003E0A RID: 15882 RVA: 0x000D7390 File Offset: 0x000D5590
		public void Read(__BinaryParser input, BinaryHeaderEnum binaryHeaderEnum)
		{
			switch (binaryHeaderEnum)
			{
			case BinaryHeaderEnum.ObjectNull:
				this.nullCount = 1;
				return;
			case BinaryHeaderEnum.MessageEnd:
			case BinaryHeaderEnum.Assembly:
				break;
			case BinaryHeaderEnum.ObjectNullMultiple256:
				this.nullCount = (int)input.ReadByte();
				return;
			case BinaryHeaderEnum.ObjectNullMultiple:
				this.nullCount = input.ReadInt32();
				break;
			default:
				return;
			}
		}

		// Token: 0x06003E0B RID: 15883 RVA: 0x00004088 File Offset: 0x00002288
		public void Dump()
		{
		}

		// Token: 0x06003E0C RID: 15884 RVA: 0x000D73DC File Offset: 0x000D55DC
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			if (BCLDebug.CheckEnabled("BINARY") && this.nullCount != 1)
			{
				int num = this.nullCount;
			}
		}

		// Token: 0x04002802 RID: 10242
		internal int nullCount;
	}
}
