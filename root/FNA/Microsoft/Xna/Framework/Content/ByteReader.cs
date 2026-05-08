using System;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x0200010E RID: 270
	internal class ByteReader : ContentTypeReader<byte>
	{
		// Token: 0x06001725 RID: 5925 RVA: 0x00038DC0 File Offset: 0x00036FC0
		internal ByteReader()
		{
		}

		// Token: 0x06001726 RID: 5926 RVA: 0x00038DC8 File Offset: 0x00036FC8
		protected internal override byte Read(ContentReader input, byte existingInstance)
		{
			return input.ReadByte();
		}
	}
}
