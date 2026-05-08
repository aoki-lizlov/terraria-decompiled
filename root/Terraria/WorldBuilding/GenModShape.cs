using System;

namespace Terraria.WorldBuilding
{
	// Token: 0x020000AB RID: 171
	public abstract class GenModShape : GenShape
	{
		// Token: 0x06001756 RID: 5974 RVA: 0x004DE01C File Offset: 0x004DC21C
		public GenModShape(ShapeData data)
		{
			this._data = data;
		}

		// Token: 0x040011E3 RID: 4579
		protected ShapeData _data;
	}
}
