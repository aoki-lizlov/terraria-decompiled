using System;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x02000125 RID: 293
	internal class PointReader : ContentTypeReader<Point>
	{
		// Token: 0x0600175A RID: 5978 RVA: 0x00039A0D File Offset: 0x00037C0D
		internal PointReader()
		{
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x00039A18 File Offset: 0x00037C18
		protected internal override Point Read(ContentReader input, Point existingInstance)
		{
			int num = input.ReadInt32();
			int num2 = input.ReadInt32();
			return new Point(num, num2);
		}
	}
}
