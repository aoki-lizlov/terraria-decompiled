using System;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x02000136 RID: 310
	internal class UInt16Reader : ContentTypeReader<ushort>
	{
		// Token: 0x06001783 RID: 6019 RVA: 0x0003A748 File Offset: 0x00038948
		internal UInt16Reader()
		{
		}

		// Token: 0x06001784 RID: 6020 RVA: 0x0003A750 File Offset: 0x00038950
		protected internal override ushort Read(ContentReader input, ushort existingInstance)
		{
			return input.ReadUInt16();
		}
	}
}
