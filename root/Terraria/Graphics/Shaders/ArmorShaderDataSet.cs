using System;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace Terraria.Graphics.Shaders
{
	// Token: 0x020001DF RID: 479
	public class ArmorShaderDataSet
	{
		// Token: 0x0600201F RID: 8223 RVA: 0x005212A0 File Offset: 0x0051F4A0
		public T BindShader<T>(int itemId, T shaderData) where T : ArmorShaderData
		{
			Dictionary<int, int> shaderLookupDictionary = this._shaderLookupDictionary;
			int num = this._shaderDataCount + 1;
			this._shaderDataCount = num;
			shaderLookupDictionary[itemId] = num;
			this._shaderData.Add(shaderData);
			return shaderData;
		}

		// Token: 0x06002020 RID: 8224 RVA: 0x005212DC File Offset: 0x0051F4DC
		public void Apply(int shaderId, Entity entity, DrawData? drawData = null)
		{
			if (shaderId >= 1 && shaderId <= this._shaderDataCount)
			{
				this._shaderData[shaderId - 1].Apply(entity, drawData);
				return;
			}
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		// Token: 0x06002021 RID: 8225 RVA: 0x0052131C File Offset: 0x0051F51C
		public void ApplySecondary(int shaderId, Entity entity, DrawData? drawData = null)
		{
			if (shaderId >= 1 && shaderId <= this._shaderDataCount)
			{
				this._shaderData[shaderId - 1].GetSecondaryShader(entity).Apply(entity, drawData);
				return;
			}
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		// Token: 0x06002022 RID: 8226 RVA: 0x0052136C File Offset: 0x0051F56C
		public ArmorShaderData GetShaderFromItemId(int type)
		{
			if (this._shaderLookupDictionary.ContainsKey(type))
			{
				return this._shaderData[this._shaderLookupDictionary[type] - 1];
			}
			return null;
		}

		// Token: 0x06002023 RID: 8227 RVA: 0x00521397 File Offset: 0x0051F597
		public int GetShaderIdFromItemId(int type)
		{
			if (this._shaderLookupDictionary.ContainsKey(type))
			{
				return this._shaderLookupDictionary[type];
			}
			return 0;
		}

		// Token: 0x06002024 RID: 8228 RVA: 0x005213B5 File Offset: 0x0051F5B5
		public ArmorShaderData GetSecondaryShader(int id, Player player)
		{
			if (id != 0 && id <= this._shaderDataCount && this._shaderData[id - 1] != null)
			{
				return this._shaderData[id - 1].GetSecondaryShader(player);
			}
			return null;
		}

		// Token: 0x06002025 RID: 8229 RVA: 0x005213E9 File Offset: 0x0051F5E9
		public ArmorShaderDataSet()
		{
		}

		// Token: 0x04004A94 RID: 19092
		protected List<ArmorShaderData> _shaderData = new List<ArmorShaderData>();

		// Token: 0x04004A95 RID: 19093
		protected Dictionary<int, int> _shaderLookupDictionary = new Dictionary<int, int>();

		// Token: 0x04004A96 RID: 19094
		protected int _shaderDataCount;
	}
}
