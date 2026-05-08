using System;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x0200010A RID: 266
	internal class BooleanReader : ContentTypeReader<bool>
	{
		// Token: 0x0600171D RID: 5917 RVA: 0x00038D58 File Offset: 0x00036F58
		internal BooleanReader()
		{
		}

		// Token: 0x0600171E RID: 5918 RVA: 0x00038D60 File Offset: 0x00036F60
		protected internal override bool Read(ContentReader input, bool existingInstance)
		{
			return input.ReadBoolean();
		}
	}
}
