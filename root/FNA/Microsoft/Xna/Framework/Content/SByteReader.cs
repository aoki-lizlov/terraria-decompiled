using System;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x0200012A RID: 298
	internal class SByteReader : ContentTypeReader<sbyte>
	{
		// Token: 0x06001767 RID: 5991 RVA: 0x00039EDB File Offset: 0x000380DB
		internal SByteReader()
		{
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x00039EE3 File Offset: 0x000380E3
		protected internal override sbyte Read(ContentReader input, sbyte existingInstance)
		{
			return input.ReadSByte();
		}
	}
}
