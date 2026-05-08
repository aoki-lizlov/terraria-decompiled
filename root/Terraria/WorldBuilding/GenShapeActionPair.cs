using System;

namespace Terraria.WorldBuilding
{
	// Token: 0x020000B9 RID: 185
	public struct GenShapeActionPair
	{
		// Token: 0x0600178A RID: 6026 RVA: 0x004DE46B File Offset: 0x004DC66B
		public GenShapeActionPair(GenShape shape, GenAction action)
		{
			this.Shape = shape;
			this.Action = action;
		}

		// Token: 0x04001280 RID: 4736
		public readonly GenShape Shape;

		// Token: 0x04001281 RID: 4737
		public readonly GenAction Action;
	}
}
