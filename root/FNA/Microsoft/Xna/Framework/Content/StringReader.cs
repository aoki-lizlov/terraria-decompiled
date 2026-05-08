using System;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x02000130 RID: 304
	internal class StringReader : ContentTypeReader<string>
	{
		// Token: 0x06001777 RID: 6007 RVA: 0x0003A37A File Offset: 0x0003857A
		internal StringReader()
		{
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x0003A382 File Offset: 0x00038582
		protected internal override string Read(ContentReader input, string existingInstance)
		{
			return input.ReadString();
		}
	}
}
