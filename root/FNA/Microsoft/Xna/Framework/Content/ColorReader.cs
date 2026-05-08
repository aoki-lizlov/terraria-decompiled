using System;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x02000110 RID: 272
	internal class ColorReader : ContentTypeReader<Color>
	{
		// Token: 0x06001729 RID: 5929 RVA: 0x00038DE0 File Offset: 0x00036FE0
		internal ColorReader()
		{
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x00038DE8 File Offset: 0x00036FE8
		protected internal override Color Read(ContentReader input, Color existingInstance)
		{
			int num = (int)input.ReadByte();
			byte b = input.ReadByte();
			byte b2 = input.ReadByte();
			byte b3 = input.ReadByte();
			return new Color(num, (int)b, (int)b2, (int)b3);
		}
	}
}
