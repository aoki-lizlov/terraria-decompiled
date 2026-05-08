using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x02000218 RID: 536
	public class MapHeadRenderer : INeedRenderTargetContent
	{
		// Token: 0x060021B7 RID: 8631 RVA: 0x00532320 File Offset: 0x00530520
		public MapHeadRenderer()
		{
			for (int i = 0; i < this._playerRenders.Length; i++)
			{
				this._playerRenders[i] = new PlayerHeadDrawRenderTargetContent();
			}
		}

		// Token: 0x060021B8 RID: 8632 RVA: 0x00532370 File Offset: 0x00530570
		public void Reset()
		{
			this._anyDirty = false;
			this._drawData.Clear();
			for (int i = 0; i < this._playerRenders.Length; i++)
			{
				this._playerRenders[i].Reset();
			}
		}

		// Token: 0x060021B9 RID: 8633 RVA: 0x005323B0 File Offset: 0x005305B0
		public void DrawPlayerHead(Camera camera, Player drawPlayer, Vector2 position, float alpha = 1f, float scale = 1f, Color borderColor = default(Color))
		{
			PlayerHeadDrawRenderTargetContent playerHeadDrawRenderTargetContent = this._playerRenders[drawPlayer.whoAmI];
			playerHeadDrawRenderTargetContent.UsePlayer(drawPlayer);
			playerHeadDrawRenderTargetContent.UseColor(borderColor);
			playerHeadDrawRenderTargetContent.Request();
			this._anyDirty = true;
			this._drawData.Clear();
			if (playerHeadDrawRenderTargetContent.IsReady)
			{
				RenderTarget2D target = playerHeadDrawRenderTargetContent.GetTarget();
				this._drawData.Add(new DrawData(target, position, null, Color.White, 0f, target.Size() / 2f, scale, SpriteEffects.None, 0f));
				this.RenderDrawData(drawPlayer);
			}
		}

		// Token: 0x060021BA RID: 8634 RVA: 0x00532448 File Offset: 0x00530648
		private void RenderDrawData(Player drawPlayer)
		{
			Effect pixelShader = Main.pixelShader;
			SpriteBatch spriteBatch = Main.spriteBatch;
			for (int i = 0; i < this._drawData.Count; i++)
			{
				DrawData drawData = this._drawData[i];
				if (drawData.sourceRect == null)
				{
					drawData.sourceRect = new Rectangle?(drawData.texture.Frame(1, 1, 0, 0, 0, 0));
				}
				PlayerDrawHelper.SetShaderForData(drawPlayer, drawPlayer.cHead, ref drawData);
				if (drawData.texture != null)
				{
					drawData.Draw(spriteBatch);
				}
			}
			pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x060021BB RID: 8635 RVA: 0x005324E3 File Offset: 0x005306E3
		public bool IsReady
		{
			get
			{
				return !this._anyDirty;
			}
		}

		// Token: 0x060021BC RID: 8636 RVA: 0x005324F0 File Offset: 0x005306F0
		public void PrepareRenderTarget(GraphicsDevice device, SpriteBatch spriteBatch)
		{
			if (!this._anyDirty)
			{
				return;
			}
			for (int i = 0; i < this._playerRenders.Length; i++)
			{
				this._playerRenders[i].PrepareRenderTarget(device, spriteBatch);
			}
			this._anyDirty = false;
		}

		// Token: 0x060021BD RID: 8637 RVA: 0x00532530 File Offset: 0x00530730
		private void CreateOutlines(float alpha, float scale, Color borderColor)
		{
			if (borderColor != Color.Transparent)
			{
				List<DrawData> list = new List<DrawData>(this._drawData);
				List<DrawData> list2 = new List<DrawData>(this._drawData);
				this._drawData.Clear();
				float num = 2f * scale;
				Color color = borderColor * (alpha * alpha);
				Color color2 = Color.Black;
				color2 *= alpha * alpha;
				int colorOnlyShaderIndex = ContentSamples.DyeShaderIDs.ColorOnlyShaderIndex;
				for (int i = 0; i < list2.Count; i++)
				{
					DrawData drawData = list2[i];
					drawData.shader = colorOnlyShaderIndex;
					drawData.color = color2;
					list2[i] = drawData;
				}
				int num2 = 2;
				Vector2 zero;
				for (int j = -num2; j <= num2; j++)
				{
					for (int k = -num2; k <= num2; k++)
					{
						if (Math.Abs(j) + Math.Abs(k) == num2)
						{
							zero = new Vector2((float)j * num, (float)k * num);
							for (int l = 0; l < list2.Count; l++)
							{
								DrawData drawData2 = list2[l];
								drawData2.position += zero;
								this._drawData.Add(drawData2);
							}
						}
					}
				}
				for (int m = 0; m < list2.Count; m++)
				{
					DrawData drawData3 = list2[m];
					drawData3.shader = colorOnlyShaderIndex;
					drawData3.color = color;
					list2[m] = drawData3;
				}
				zero = Vector2.Zero;
				num2 = 1;
				for (int n = -num2; n <= num2; n++)
				{
					for (int num3 = -num2; num3 <= num2; num3++)
					{
						if (Math.Abs(n) + Math.Abs(num3) == num2)
						{
							zero = new Vector2((float)n * num, (float)num3 * num);
							for (int num4 = 0; num4 < list2.Count; num4++)
							{
								DrawData drawData4 = list2[num4];
								drawData4.position += zero;
								this._drawData.Add(drawData4);
							}
						}
					}
				}
				this._drawData.AddRange(list);
			}
		}

		// Token: 0x04004C33 RID: 19507
		private bool _anyDirty;

		// Token: 0x04004C34 RID: 19508
		private PlayerHeadDrawRenderTargetContent[] _playerRenders = new PlayerHeadDrawRenderTargetContent[255];

		// Token: 0x04004C35 RID: 19509
		private readonly List<DrawData> _drawData = new List<DrawData>();
	}
}
