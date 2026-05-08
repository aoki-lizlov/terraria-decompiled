using System;
using System.Collections.Generic;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x020000AA RID: 170
	internal sealed class StateHashComparer : IEqualityComparer<StateHash>
	{
		// Token: 0x060013F5 RID: 5109 RVA: 0x0002E23B File Offset: 0x0002C43B
		public bool Equals(StateHash x, StateHash y)
		{
			return x.Equals(y);
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x0002E245 File Offset: 0x0002C445
		public int GetHashCode(StateHash obj)
		{
			return obj.GetHashCode();
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x000136F5 File Offset: 0x000118F5
		public StateHashComparer()
		{
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x0002E254 File Offset: 0x0002C454
		// Note: this type is marked as 'beforefieldinit'.
		static StateHashComparer()
		{
		}

		// Token: 0x04000917 RID: 2327
		public static readonly StateHashComparer Instance = new StateHashComparer();
	}
}
