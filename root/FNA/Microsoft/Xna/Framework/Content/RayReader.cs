using System;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x02000127 RID: 295
	internal class RayReader : ContentTypeReader<Ray>
	{
		// Token: 0x0600175E RID: 5982 RVA: 0x00039A48 File Offset: 0x00037C48
		internal RayReader()
		{
		}

		// Token: 0x0600175F RID: 5983 RVA: 0x00039A50 File Offset: 0x00037C50
		protected internal override Ray Read(ContentReader input, Ray existingInstance)
		{
			Vector3 vector = input.ReadVector3();
			Vector3 vector2 = input.ReadVector3();
			return new Ray(vector, vector2);
		}
	}
}
