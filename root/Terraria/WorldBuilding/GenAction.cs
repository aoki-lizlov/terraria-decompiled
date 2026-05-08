using System;
using Microsoft.Xna.Framework;

namespace Terraria.WorldBuilding
{
	// Token: 0x020000A4 RID: 164
	public abstract class GenAction : GenBase
	{
		// Token: 0x06001736 RID: 5942
		public abstract bool Apply(Point origin, int x, int y, params object[] args);

		// Token: 0x06001737 RID: 5943 RVA: 0x004DDD26 File Offset: 0x004DBF26
		protected bool UnitApply(Point origin, int x, int y, params object[] args)
		{
			if (this.OutputData != null)
			{
				this.OutputData.Add(x - origin.X, y - origin.Y);
			}
			return this.NextAction == null || this.NextAction.Apply(origin, x, y, args);
		}

		// Token: 0x06001738 RID: 5944 RVA: 0x004DDD65 File Offset: 0x004DBF65
		public GenAction IgnoreFailures()
		{
			this._returnFalseOnFailure = false;
			return this;
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x004DDD6F File Offset: 0x004DBF6F
		protected bool Fail()
		{
			return !this._returnFalseOnFailure;
		}

		// Token: 0x0600173A RID: 5946 RVA: 0x004DDD7A File Offset: 0x004DBF7A
		public GenAction Output(ShapeData data)
		{
			this.OutputData = data;
			return this;
		}

		// Token: 0x0600173B RID: 5947 RVA: 0x004DDD84 File Offset: 0x004DBF84
		protected GenAction()
		{
		}

		// Token: 0x040011D7 RID: 4567
		public GenAction NextAction;

		// Token: 0x040011D8 RID: 4568
		public ShapeData OutputData;

		// Token: 0x040011D9 RID: 4569
		private bool _returnFalseOnFailure = true;
	}
}
