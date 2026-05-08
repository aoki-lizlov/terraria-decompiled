using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Terraria.DataStructures
{
	// Token: 0x02000588 RID: 1416
	public class AnchoredEntitiesCollection
	{
		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x0600380F RID: 14351 RVA: 0x006307A6 File Offset: 0x0062E9A6
		public int AnchoredPlayersAmount
		{
			get
			{
				return this._anchoredPlayers.Count;
			}
		}

		// Token: 0x06003810 RID: 14352 RVA: 0x006307B3 File Offset: 0x0062E9B3
		public AnchoredEntitiesCollection()
		{
			this._anchoredNPCs = new List<AnchoredEntitiesCollection.IndexPointPair>();
			this._anchoredPlayers = new List<AnchoredEntitiesCollection.IndexPointPair>();
		}

		// Token: 0x06003811 RID: 14353 RVA: 0x006307D1 File Offset: 0x0062E9D1
		public void ClearNPCAnchors()
		{
			this._anchoredNPCs.Clear();
		}

		// Token: 0x06003812 RID: 14354 RVA: 0x006307DE File Offset: 0x0062E9DE
		public void ClearPlayerAnchors()
		{
			this._anchoredPlayers.Clear();
		}

		// Token: 0x06003813 RID: 14355 RVA: 0x006307EC File Offset: 0x0062E9EC
		public void AddNPC(int npcIndex, Point coords)
		{
			this._anchoredNPCs.Add(new AnchoredEntitiesCollection.IndexPointPair
			{
				index = npcIndex,
				coords = coords
			});
		}

		// Token: 0x06003814 RID: 14356 RVA: 0x0063081D File Offset: 0x0062EA1D
		public int GetNextPlayerStackIndexInCoords(Point coords)
		{
			return this.GetEntitiesInCoords(coords);
		}

		// Token: 0x06003815 RID: 14357 RVA: 0x00630828 File Offset: 0x0062EA28
		public void AddPlayerAndGetItsStackedIndexInCoords(int playerIndex, Point coords, out int stackedIndexInCoords)
		{
			stackedIndexInCoords = this.GetEntitiesInCoords(coords);
			this._anchoredPlayers.Add(new AnchoredEntitiesCollection.IndexPointPair
			{
				index = playerIndex,
				coords = coords
			});
		}

		// Token: 0x06003816 RID: 14358 RVA: 0x00630864 File Offset: 0x0062EA64
		private int GetEntitiesInCoords(Point coords)
		{
			int num = 0;
			for (int i = 0; i < this._anchoredNPCs.Count; i++)
			{
				if (this._anchoredNPCs[i].coords == coords)
				{
					num++;
				}
			}
			for (int j = 0; j < this._anchoredPlayers.Count; j++)
			{
				if (this._anchoredPlayers[j].coords == coords)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x04005C3C RID: 23612
		private List<AnchoredEntitiesCollection.IndexPointPair> _anchoredNPCs;

		// Token: 0x04005C3D RID: 23613
		private List<AnchoredEntitiesCollection.IndexPointPair> _anchoredPlayers;

		// Token: 0x020009BE RID: 2494
		private struct IndexPointPair
		{
			// Token: 0x040076D4 RID: 30420
			public int index;

			// Token: 0x040076D5 RID: 30421
			public Point coords;
		}
	}
}
