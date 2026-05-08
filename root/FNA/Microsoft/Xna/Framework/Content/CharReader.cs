using System;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x0200010F RID: 271
	internal class CharReader : ContentTypeReader<char>
	{
		// Token: 0x06001727 RID: 5927 RVA: 0x00038DD0 File Offset: 0x00036FD0
		internal CharReader()
		{
		}

		// Token: 0x06001728 RID: 5928 RVA: 0x00038DD8 File Offset: 0x00036FD8
		protected internal override char Read(ContentReader input, char existingInstance)
		{
			return input.ReadChar();
		}
	}
}
