using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace Terraria.Graphics.Shaders
{
	// Token: 0x020001E0 RID: 480
	public class HairShaderDataSet
	{
		// Token: 0x06002026 RID: 8230 RVA: 0x00521408 File Offset: 0x0051F608
		public T BindShader<T>(int itemId, T shaderData) where T : HairShaderData
		{
			if (this._shaderDataCount == 255)
			{
				throw new Exception("Too many shaders bound.");
			}
			Dictionary<int, short> shaderLookupDictionary = this._shaderLookupDictionary;
			byte b = this._shaderDataCount + 1;
			this._shaderDataCount = b;
			shaderLookupDictionary[itemId] = (short)b;
			this._shaderData.Add(shaderData);
			return shaderData;
		}

		// Token: 0x06002027 RID: 8231 RVA: 0x0052145D File Offset: 0x0051F65D
		public void Apply(short shaderId, Player player, DrawData? drawData = null)
		{
			if (shaderId != 0 && shaderId <= (short)this._shaderDataCount)
			{
				this._shaderData[(int)(shaderId - 1)].Apply(player, drawData);
				return;
			}
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		// Token: 0x06002028 RID: 8232 RVA: 0x0052149B File Offset: 0x0051F69B
		public Color GetColor(short shaderId, Player player, Color lightColor)
		{
			if (shaderId != 0 && shaderId <= (short)this._shaderDataCount)
			{
				return this._shaderData[(int)(shaderId - 1)].GetColor(player, lightColor);
			}
			return new Color(lightColor.ToVector4() * player.hairColor.ToVector4());
		}

		// Token: 0x06002029 RID: 8233 RVA: 0x005214DB File Offset: 0x0051F6DB
		public HairShaderData GetShaderFromItemId(int type)
		{
			if (this._shaderLookupDictionary.ContainsKey(type))
			{
				return this._shaderData[(int)(this._shaderLookupDictionary[type] - 1)];
			}
			return null;
		}

		// Token: 0x0600202A RID: 8234 RVA: 0x00521506 File Offset: 0x0051F706
		public short GetShaderIdFromItemId(int type)
		{
			if (this._shaderLookupDictionary.ContainsKey(type))
			{
				return this._shaderLookupDictionary[type];
			}
			return -1;
		}

		// Token: 0x0600202B RID: 8235 RVA: 0x00521524 File Offset: 0x0051F724
		public HairShaderDataSet()
		{
		}

		// Token: 0x04004A97 RID: 19095
		protected List<HairShaderData> _shaderData = new List<HairShaderData>();

		// Token: 0x04004A98 RID: 19096
		protected Dictionary<int, short> _shaderLookupDictionary = new Dictionary<int, short>();

		// Token: 0x04004A99 RID: 19097
		protected byte _shaderDataCount;
	}
}
