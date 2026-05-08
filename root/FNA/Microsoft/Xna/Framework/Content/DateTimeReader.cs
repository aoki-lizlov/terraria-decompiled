using System;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x02000112 RID: 274
	internal class DateTimeReader : ContentTypeReader<DateTime>
	{
		// Token: 0x0600172D RID: 5933 RVA: 0x00038EA4 File Offset: 0x000370A4
		internal DateTimeReader()
		{
		}

		// Token: 0x0600172E RID: 5934 RVA: 0x00038EAC File Offset: 0x000370AC
		protected internal override DateTime Read(ContentReader input, DateTime existingInstance)
		{
			ulong num = input.ReadUInt64();
			ulong num2 = 13835058055282163712UL;
			long num3 = (long)(num & ~(long)num2);
			DateTimeKind dateTimeKind = (DateTimeKind)((num >> 62) & 3UL);
			return new DateTime(num3, dateTimeKind);
		}
	}
}
