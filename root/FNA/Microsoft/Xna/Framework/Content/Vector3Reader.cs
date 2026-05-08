using System;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x0200013A RID: 314
	internal class Vector3Reader : ContentTypeReader<Vector3>
	{
		// Token: 0x0600178B RID: 6027 RVA: 0x0003A788 File Offset: 0x00038988
		internal Vector3Reader()
		{
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x0003A790 File Offset: 0x00038990
		protected internal override Vector3 Read(ContentReader input, Vector3 existingInstance)
		{
			return input.ReadVector3();
		}
	}
}
