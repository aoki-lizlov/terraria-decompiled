using System;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x02000124 RID: 292
	internal class PlaneReader : ContentTypeReader<Plane>
	{
		// Token: 0x06001758 RID: 5976 RVA: 0x000399E8 File Offset: 0x00037BE8
		internal PlaneReader()
		{
		}

		// Token: 0x06001759 RID: 5977 RVA: 0x000399F0 File Offset: 0x00037BF0
		protected internal override Plane Read(ContentReader input, Plane existingInstance)
		{
			existingInstance.Normal = input.ReadVector3();
			existingInstance.D = input.ReadSingle();
			return existingInstance;
		}
	}
}
