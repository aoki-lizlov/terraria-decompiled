using System;
using Microsoft.Xna.Framework;

namespace Terraria.WorldBuilding
{
	// Token: 0x020000B1 RID: 177
	public abstract class GenStructure : GenBase
	{
		// Token: 0x06001765 RID: 5989 RVA: 0x004DE100 File Offset: 0x004DC300
		public virtual bool Place(Point origin, StructureMap structures)
		{
			return this.Place(origin, structures, null);
		}

		// Token: 0x06001766 RID: 5990
		public abstract bool Place(Point origin, StructureMap structures, GenerationProgress progress);

		// Token: 0x06001767 RID: 5991 RVA: 0x004DE10B File Offset: 0x004DC30B
		protected GenStructure()
		{
		}
	}
}
