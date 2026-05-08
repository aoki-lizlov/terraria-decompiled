using System;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x02000137 RID: 311
	internal class UInt32Reader : ContentTypeReader<uint>
	{
		// Token: 0x06001785 RID: 6021 RVA: 0x0003A758 File Offset: 0x00038958
		internal UInt32Reader()
		{
		}

		// Token: 0x06001786 RID: 6022 RVA: 0x0003A760 File Offset: 0x00038960
		protected internal override uint Read(ContentReader input, uint existingInstance)
		{
			return input.ReadUInt32();
		}
	}
}
