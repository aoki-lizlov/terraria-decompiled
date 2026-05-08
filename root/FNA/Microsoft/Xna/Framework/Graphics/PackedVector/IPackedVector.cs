using System;

namespace Microsoft.Xna.Framework.Graphics.PackedVector
{
	// Token: 0x020000EB RID: 235
	public interface IPackedVector
	{
		// Token: 0x06001631 RID: 5681
		void PackFromVector4(Vector4 vector);

		// Token: 0x06001632 RID: 5682
		Vector4 ToVector4();
	}
}
