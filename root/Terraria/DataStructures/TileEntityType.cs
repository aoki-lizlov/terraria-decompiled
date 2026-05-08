using System;

namespace Terraria.DataStructures
{
	// Token: 0x020005A7 RID: 1447
	public abstract class TileEntityType<T> : TileEntity where T : TileEntity, new()
	{
		// Token: 0x06003919 RID: 14617 RVA: 0x00651472 File Offset: 0x0064F672
		public override void RegisterTileEntityID(int assignedID)
		{
			TileEntityType<T>.EntityTypeID = (byte)assignedID;
		}

		// Token: 0x0600391A RID: 14618 RVA: 0x0065147B File Offset: 0x0064F67B
		public override TileEntity GenerateInstance()
		{
			return new T();
		}

		// Token: 0x0600391B RID: 14619 RVA: 0x00651488 File Offset: 0x0064F688
		public override void NetPlaceEntityAttempt(int x, int y)
		{
			int num = TileEntityType<T>.Place(x, y);
			NetMessage.SendData(86, -1, -1, null, num, (float)x, (float)y, 0f, 0, 0, 0);
		}

		// Token: 0x0600391C RID: 14620 RVA: 0x006514B4 File Offset: 0x0064F6B4
		public static int Place(int x, int y)
		{
			return TileEntity.Place(x, y, (int)TileEntityType<T>.EntityTypeID);
		}

		// Token: 0x0600391D RID: 14621 RVA: 0x006514C2 File Offset: 0x0064F6C2
		public static void Kill(int x, int y)
		{
			TileEntity.Kill(x, y, (int)TileEntityType<T>.EntityTypeID);
		}

		// Token: 0x0600391E RID: 14622 RVA: 0x006514D0 File Offset: 0x0064F6D0
		public static int Find(int x, int y)
		{
			T t;
			if (!TileEntity.TryGetAt<T>(x, y, out t))
			{
				return -1;
			}
			return t.ID;
		}

		// Token: 0x0600391F RID: 14623 RVA: 0x005B4E9E File Offset: 0x005B309E
		protected TileEntityType()
		{
		}

		// Token: 0x04005D78 RID: 23928
		protected static byte EntityTypeID;
	}
}
