using System;

namespace Terraria.DataStructures
{
	// Token: 0x020005AA RID: 1450
	public struct PlacementHook
	{
		// Token: 0x0600395E RID: 14686 RVA: 0x00652019 File Offset: 0x00650219
		public PlacementHook(PlacementHook.HookFormat hook, int badReturn, int badResponse, bool processedCoordinates)
		{
			this.hook = hook;
			this.badResponse = badResponse;
			this.badReturn = badReturn;
			this.processedCoordinates = processedCoordinates;
		}

		// Token: 0x0600395F RID: 14687 RVA: 0x00652038 File Offset: 0x00650238
		public static bool operator ==(PlacementHook first, PlacementHook second)
		{
			return first.hook == second.hook && first.badResponse == second.badResponse && first.badReturn == second.badReturn && first.processedCoordinates == second.processedCoordinates;
		}

		// Token: 0x06003960 RID: 14688 RVA: 0x00652084 File Offset: 0x00650284
		public static bool operator !=(PlacementHook first, PlacementHook second)
		{
			return first.hook != second.hook || first.badResponse != second.badResponse || first.badReturn != second.badReturn || first.processedCoordinates != second.processedCoordinates;
		}

		// Token: 0x06003961 RID: 14689 RVA: 0x006520D3 File Offset: 0x006502D3
		public override bool Equals(object obj)
		{
			return obj is PlacementHook && this == (PlacementHook)obj;
		}

		// Token: 0x06003962 RID: 14690 RVA: 0x006520F0 File Offset: 0x006502F0
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06003963 RID: 14691 RVA: 0x00652102 File Offset: 0x00650302
		// Note: this type is marked as 'beforefieldinit'.
		static PlacementHook()
		{
		}

		// Token: 0x04005D96 RID: 23958
		public PlacementHook.HookFormat hook;

		// Token: 0x04005D97 RID: 23959
		public int badReturn;

		// Token: 0x04005D98 RID: 23960
		public int badResponse;

		// Token: 0x04005D99 RID: 23961
		public bool processedCoordinates;

		// Token: 0x04005D9A RID: 23962
		public static PlacementHook Empty = new PlacementHook(null, 0, 0, false);

		// Token: 0x04005D9B RID: 23963
		public const int Response_AllInvalid = 0;

		// Token: 0x020009C3 RID: 2499
		// (Invoke) Token: 0x06004A3F RID: 19007
		public delegate int HookFormat(int x, int y, int type, int style, int direction, int alternate);
	}
}
