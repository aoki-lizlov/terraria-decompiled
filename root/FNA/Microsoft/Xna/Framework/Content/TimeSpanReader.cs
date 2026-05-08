using System;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x02000135 RID: 309
	internal class TimeSpanReader : ContentTypeReader<TimeSpan>
	{
		// Token: 0x06001781 RID: 6017 RVA: 0x0003A733 File Offset: 0x00038933
		internal TimeSpanReader()
		{
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x0003A73B File Offset: 0x0003893B
		protected internal override TimeSpan Read(ContentReader input, TimeSpan existingInstance)
		{
			return new TimeSpan(input.ReadInt64());
		}
	}
}
