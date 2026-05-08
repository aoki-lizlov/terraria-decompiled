using System;

namespace Microsoft.Xna.Framework.Graphics.PackedVector
{
	// Token: 0x020000EC RID: 236
	public interface IPackedVector<TPacked> : IPackedVector
	{
		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06001633 RID: 5683
		// (set) Token: 0x06001634 RID: 5684
		TPacked PackedValue { get; set; }
	}
}
