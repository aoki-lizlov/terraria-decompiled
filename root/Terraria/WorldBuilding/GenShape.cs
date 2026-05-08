using System;
using Microsoft.Xna.Framework;

namespace Terraria.WorldBuilding
{
	// Token: 0x020000B7 RID: 183
	public abstract class GenShape : GenBase
	{
		// Token: 0x06001785 RID: 6021
		public abstract bool Perform(Point origin, GenAction action);

		// Token: 0x06001786 RID: 6022 RVA: 0x004DE425 File Offset: 0x004DC625
		protected bool UnitApply(GenAction action, Point origin, int x, int y, params object[] args)
		{
			if (this._outputData != null)
			{
				this._outputData.Add(x - origin.X, y - origin.Y);
			}
			return action.Apply(origin, x, y, args);
		}

		// Token: 0x06001787 RID: 6023 RVA: 0x004DE457 File Offset: 0x004DC657
		public GenShape Output(ShapeData outputData)
		{
			this._outputData = outputData;
			return this;
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x004DE461 File Offset: 0x004DC661
		public GenShape QuitOnFail(bool value = true)
		{
			this._quitOnFail = value;
			return this;
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x004DE10B File Offset: 0x004DC30B
		protected GenShape()
		{
		}

		// Token: 0x0400127E RID: 4734
		private ShapeData _outputData;

		// Token: 0x0400127F RID: 4735
		protected bool _quitOnFail;
	}
}
