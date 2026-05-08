using System;
using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x02000134 RID: 308
	internal class TextureReader : ContentTypeReader<Texture>
	{
		// Token: 0x0600177F RID: 6015 RVA: 0x0003A728 File Offset: 0x00038928
		protected internal override Texture Read(ContentReader reader, Texture existingInstance)
		{
			return existingInstance;
		}

		// Token: 0x06001780 RID: 6016 RVA: 0x0003A72B File Offset: 0x0003892B
		public TextureReader()
		{
		}
	}
}
