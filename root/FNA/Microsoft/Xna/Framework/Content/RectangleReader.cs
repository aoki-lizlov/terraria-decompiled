using System;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x02000128 RID: 296
	internal class RectangleReader : ContentTypeReader<Rectangle>
	{
		// Token: 0x06001760 RID: 5984 RVA: 0x00039A70 File Offset: 0x00037C70
		internal RectangleReader()
		{
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x00039A78 File Offset: 0x00037C78
		protected internal override Rectangle Read(ContentReader input, Rectangle existingInstance)
		{
			int num = input.ReadInt32();
			int num2 = input.ReadInt32();
			int num3 = input.ReadInt32();
			int num4 = input.ReadInt32();
			return new Rectangle(num, num2, num3, num4);
		}
	}
}
