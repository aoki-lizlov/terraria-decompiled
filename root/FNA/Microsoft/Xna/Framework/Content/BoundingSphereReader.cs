using System;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x0200010D RID: 269
	internal class BoundingSphereReader : ContentTypeReader<BoundingSphere>
	{
		// Token: 0x06001723 RID: 5923 RVA: 0x00038D98 File Offset: 0x00036F98
		internal BoundingSphereReader()
		{
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x00038DA0 File Offset: 0x00036FA0
		protected internal override BoundingSphere Read(ContentReader input, BoundingSphere existingInstance)
		{
			Vector3 vector = input.ReadVector3();
			float num = input.ReadSingle();
			return new BoundingSphere(vector, num);
		}
	}
}
