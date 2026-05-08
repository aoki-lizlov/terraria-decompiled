using System;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x02000139 RID: 313
	internal class Vector2Reader : ContentTypeReader<Vector2>
	{
		// Token: 0x06001789 RID: 6025 RVA: 0x0003A778 File Offset: 0x00038978
		internal Vector2Reader()
		{
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x0003A780 File Offset: 0x00038980
		protected internal override Vector2 Read(ContentReader input, Vector2 existingInstance)
		{
			return input.ReadVector2();
		}
	}
}
