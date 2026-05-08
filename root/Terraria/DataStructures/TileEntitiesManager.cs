using System;
using System.Collections.Generic;
using Terraria.GameContent.Tile_Entities;

namespace Terraria.DataStructures
{
	// Token: 0x020005A6 RID: 1446
	public class TileEntitiesManager
	{
		// Token: 0x06003911 RID: 14609 RVA: 0x00651308 File Offset: 0x0064F508
		private int AssignNewID()
		{
			int nextEntityID = this._nextEntityID;
			this._nextEntityID = nextEntityID + 1;
			return nextEntityID;
		}

		// Token: 0x06003912 RID: 14610 RVA: 0x00651326 File Offset: 0x0064F526
		private bool InvalidEntityID(int id)
		{
			return id < 0 || id >= this._nextEntityID;
		}

		// Token: 0x06003913 RID: 14611 RVA: 0x0065133C File Offset: 0x0064F53C
		public void RegisterAll()
		{
			this.Register(new TETrainingDummy());
			this.Register(new TEItemFrame());
			this.Register(new TELogicSensor());
			this.Register(new TEDisplayDoll());
			this.Register(new TEWeaponsRack());
			this.Register(new TEHatRack());
			this.Register(new TEFoodPlatter());
			this.Register(new TETeleportationPylon());
			this.Register(new TEDeadCellsDisplayJar());
			this.Register(new TEKiteAnchor());
			this.Register(new TECritterAnchor());
		}

		// Token: 0x06003914 RID: 14612 RVA: 0x006513C4 File Offset: 0x0064F5C4
		public void Register(TileEntity entity)
		{
			int num = this.AssignNewID();
			this._types[num] = entity;
			entity.RegisterTileEntityID(num);
		}

		// Token: 0x06003915 RID: 14613 RVA: 0x006513EC File Offset: 0x0064F5EC
		public bool CheckValidTile(int id, int x, int y)
		{
			return !this.InvalidEntityID(id) && this._types[id].IsTileValidForEntity(x, y);
		}

		// Token: 0x06003916 RID: 14614 RVA: 0x0065140C File Offset: 0x0064F60C
		public void NetPlaceEntity(int id, int x, int y)
		{
			if (this.InvalidEntityID(id))
			{
				return;
			}
			if (!this._types[id].IsTileValidForEntity(x, y))
			{
				return;
			}
			this._types[id].NetPlaceEntityAttempt(x, y);
		}

		// Token: 0x06003917 RID: 14615 RVA: 0x00651441 File Offset: 0x0064F641
		public TileEntity GenerateInstance(int id)
		{
			if (this.InvalidEntityID(id))
			{
				return null;
			}
			return this._types[id].GenerateInstance();
		}

		// Token: 0x06003918 RID: 14616 RVA: 0x0065145F File Offset: 0x0064F65F
		public TileEntitiesManager()
		{
		}

		// Token: 0x04005D76 RID: 23926
		private int _nextEntityID;

		// Token: 0x04005D77 RID: 23927
		private Dictionary<int, TileEntity> _types = new Dictionary<int, TileEntity>();
	}
}
