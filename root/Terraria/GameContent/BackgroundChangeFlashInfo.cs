using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent
{
	// Token: 0x0200026F RID: 623
	public class BackgroundChangeFlashInfo
	{
		// Token: 0x06002417 RID: 9239 RVA: 0x0054A790 File Offset: 0x00548990
		public void UpdateCache()
		{
			this.UpdateVariation(0, WorldGen.treeBG1);
			this.UpdateVariation(1, WorldGen.treeBG2);
			this.UpdateVariation(2, WorldGen.treeBG3);
			this.UpdateVariation(3, WorldGen.treeBG4);
			this.UpdateVariation(4, WorldGen.corruptBG);
			this.UpdateVariation(5, WorldGen.jungleBG);
			this.UpdateVariation(6, WorldGen.snowBG);
			this.UpdateVariation(7, WorldGen.hallowBG);
			this.UpdateVariation(8, WorldGen.crimsonBG);
			this.UpdateVariation(9, WorldGen.desertBG);
			this.UpdateVariation(10, WorldGen.oceanBG);
			this.UpdateVariation(11, WorldGen.mushroomBG);
			this.UpdateVariation(12, WorldGen.underworldBG);
		}

		// Token: 0x06002418 RID: 9240 RVA: 0x0054A83D File Offset: 0x00548A3D
		private void UpdateVariation(int areaId, int newVariationValue)
		{
			int num = this._variations[areaId];
			this._variations[areaId] = newVariationValue;
			if (num != newVariationValue)
			{
				this.ValueChanged(areaId);
			}
		}

		// Token: 0x06002419 RID: 9241 RVA: 0x0054A85A File Offset: 0x00548A5A
		private void ValueChanged(int areaId)
		{
			if (Main.gameMenu)
			{
				return;
			}
			this._flashPower[areaId] = 1f;
		}

		// Token: 0x0600241A RID: 9242 RVA: 0x0054A874 File Offset: 0x00548A74
		public void UpdateFlashValues()
		{
			for (int i = 0; i < this._flashPower.Length; i++)
			{
				this._flashPower[i] = MathHelper.Clamp(this._flashPower[i] - 0.05f, 0f, 1f);
			}
		}

		// Token: 0x0600241B RID: 9243 RVA: 0x0054A8B9 File Offset: 0x00548AB9
		public float GetFlashPower(int areaId)
		{
			return this._flashPower[areaId];
		}

		// Token: 0x0600241C RID: 9244 RVA: 0x0054A8C3 File Offset: 0x00548AC3
		public BackgroundChangeFlashInfo()
		{
		}

		// Token: 0x04004DD3 RID: 19923
		private int[] _variations = new int[TreeTopsInfo.AreaId.Count];

		// Token: 0x04004DD4 RID: 19924
		private float[] _flashPower = new float[TreeTopsInfo.AreaId.Count];
	}
}
