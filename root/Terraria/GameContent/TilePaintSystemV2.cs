using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Terraria.GameContent
{
	// Token: 0x0200025A RID: 602
	public class TilePaintSystemV2
	{
		// Token: 0x06002352 RID: 9042 RVA: 0x0053E108 File Offset: 0x0053C308
		public void Reset()
		{
			foreach (TilePaintSystemV2.TileRenderTargetHolder tileRenderTargetHolder in this._tilesRenders.Values)
			{
				tileRenderTargetHolder.Clear();
			}
			this._tilesRenders.Clear();
			foreach (TilePaintSystemV2.CageTopRenderTargetHolder cageTopRenderTargetHolder in this._cageTopRenders.Values)
			{
				cageTopRenderTargetHolder.Clear();
			}
			this._cageTopRenders.Clear();
			foreach (TilePaintSystemV2.WallRenderTargetHolder wallRenderTargetHolder in this._wallsRenders.Values)
			{
				wallRenderTargetHolder.Clear();
			}
			this._wallsRenders.Clear();
			foreach (TilePaintSystemV2.TreeTopRenderTargetHolder treeTopRenderTargetHolder in this._treeTopRenders.Values)
			{
				treeTopRenderTargetHolder.Clear();
			}
			this._treeTopRenders.Clear();
			foreach (TilePaintSystemV2.TreeBranchTargetHolder treeBranchTargetHolder in this._treeBranchRenders.Values)
			{
				treeBranchTargetHolder.Clear();
			}
			this._treeBranchRenders.Clear();
			foreach (TilePaintSystemV2.ARenderTargetHolder arenderTargetHolder in this._requests)
			{
				arenderTargetHolder.Clear();
			}
			this._requests.Clear();
		}

		// Token: 0x06002353 RID: 9043 RVA: 0x0053E2F0 File Offset: 0x0053C4F0
		public void RequestTile(ref TilePaintSystemV2.TileVariationkey lookupKey)
		{
			TilePaintSystemV2.TileRenderTargetHolder tileRenderTargetHolder;
			if (!this._tilesRenders.TryGetValue(lookupKey, out tileRenderTargetHolder))
			{
				tileRenderTargetHolder = new TilePaintSystemV2.TileRenderTargetHolder
				{
					Key = lookupKey
				};
				this._tilesRenders.Add(lookupKey, tileRenderTargetHolder);
			}
			if (tileRenderTargetHolder.IsReady)
			{
				return;
			}
			this._requests.Add(tileRenderTargetHolder);
		}

		// Token: 0x06002354 RID: 9044 RVA: 0x0053E34C File Offset: 0x0053C54C
		public void RequestCageTop(ref TilePaintSystemV2.CageTopVariationkey lookupKey)
		{
			TilePaintSystemV2.CageTopRenderTargetHolder cageTopRenderTargetHolder;
			if (!this._cageTopRenders.TryGetValue(lookupKey, out cageTopRenderTargetHolder))
			{
				cageTopRenderTargetHolder = new TilePaintSystemV2.CageTopRenderTargetHolder
				{
					Key = lookupKey
				};
				this._cageTopRenders.Add(lookupKey, cageTopRenderTargetHolder);
			}
			if (cageTopRenderTargetHolder.IsReady)
			{
				return;
			}
			this._requests.Add(cageTopRenderTargetHolder);
		}

		// Token: 0x06002355 RID: 9045 RVA: 0x0053E3A8 File Offset: 0x0053C5A8
		private void RequestTile_CheckForRelatedTileRequests(ref TilePaintSystemV2.TileVariationkey lookupKey)
		{
			if (lookupKey.TileType == 83)
			{
				TilePaintSystemV2.TileVariationkey tileVariationkey = new TilePaintSystemV2.TileVariationkey
				{
					TileType = 84,
					TileStyle = lookupKey.TileStyle,
					PaintColor = lookupKey.PaintColor
				};
				this.RequestTile(ref tileVariationkey);
			}
		}

		// Token: 0x06002356 RID: 9046 RVA: 0x0053E3F4 File Offset: 0x0053C5F4
		public void RequestWall(ref TilePaintSystemV2.WallVariationKey lookupKey)
		{
			TilePaintSystemV2.WallRenderTargetHolder wallRenderTargetHolder;
			if (!this._wallsRenders.TryGetValue(lookupKey, out wallRenderTargetHolder))
			{
				wallRenderTargetHolder = new TilePaintSystemV2.WallRenderTargetHolder
				{
					Key = lookupKey
				};
				this._wallsRenders.Add(lookupKey, wallRenderTargetHolder);
			}
			if (wallRenderTargetHolder.IsReady)
			{
				return;
			}
			this._requests.Add(wallRenderTargetHolder);
		}

		// Token: 0x06002357 RID: 9047 RVA: 0x0053E450 File Offset: 0x0053C650
		public void RequestTreeTop(ref TilePaintSystemV2.TreeFoliageVariantKey lookupKey)
		{
			TilePaintSystemV2.TreeTopRenderTargetHolder treeTopRenderTargetHolder;
			if (!this._treeTopRenders.TryGetValue(lookupKey, out treeTopRenderTargetHolder))
			{
				treeTopRenderTargetHolder = new TilePaintSystemV2.TreeTopRenderTargetHolder
				{
					Key = lookupKey
				};
				this._treeTopRenders.Add(lookupKey, treeTopRenderTargetHolder);
			}
			if (treeTopRenderTargetHolder.IsReady)
			{
				return;
			}
			this._requests.Add(treeTopRenderTargetHolder);
		}

		// Token: 0x06002358 RID: 9048 RVA: 0x0053E4AC File Offset: 0x0053C6AC
		public void RequestTreeBranch(ref TilePaintSystemV2.TreeFoliageVariantKey lookupKey)
		{
			TilePaintSystemV2.TreeBranchTargetHolder treeBranchTargetHolder;
			if (!this._treeBranchRenders.TryGetValue(lookupKey, out treeBranchTargetHolder))
			{
				treeBranchTargetHolder = new TilePaintSystemV2.TreeBranchTargetHolder
				{
					Key = lookupKey
				};
				this._treeBranchRenders.Add(lookupKey, treeBranchTargetHolder);
			}
			if (treeBranchTargetHolder.IsReady)
			{
				return;
			}
			this._requests.Add(treeBranchTargetHolder);
		}

		// Token: 0x06002359 RID: 9049 RVA: 0x0053E508 File Offset: 0x0053C708
		public Texture2D TryGetTileAndRequestIfNotReady(int tileType, int tileStyle, int paintColor)
		{
			TilePaintSystemV2.TileVariationkey tileVariationkey = new TilePaintSystemV2.TileVariationkey
			{
				TileType = tileType,
				TileStyle = tileStyle,
				PaintColor = paintColor
			};
			TilePaintSystemV2.TileRenderTargetHolder tileRenderTargetHolder;
			if (this._tilesRenders.TryGetValue(tileVariationkey, out tileRenderTargetHolder) && tileRenderTargetHolder.IsReady)
			{
				return tileRenderTargetHolder.Target;
			}
			this.RequestTile(ref tileVariationkey);
			return null;
		}

		// Token: 0x0600235A RID: 9050 RVA: 0x0053E560 File Offset: 0x0053C760
		public Texture2D TryGetCageTopAndRequestIfNotReady(int cageStyle, int paintColor)
		{
			TilePaintSystemV2.CageTopVariationkey cageTopVariationkey = new TilePaintSystemV2.CageTopVariationkey
			{
				CageStyle = cageStyle,
				PaintColor = paintColor
			};
			TilePaintSystemV2.CageTopRenderTargetHolder cageTopRenderTargetHolder;
			if (this._cageTopRenders.TryGetValue(cageTopVariationkey, out cageTopRenderTargetHolder) && cageTopRenderTargetHolder.IsReady)
			{
				return cageTopRenderTargetHolder.Target;
			}
			this.RequestCageTop(ref cageTopVariationkey);
			return null;
		}

		// Token: 0x0600235B RID: 9051 RVA: 0x0053E5B0 File Offset: 0x0053C7B0
		public Texture2D TryGetWallAndRequestIfNotReady(int wallType, int paintColor)
		{
			TilePaintSystemV2.WallVariationKey wallVariationKey = new TilePaintSystemV2.WallVariationKey
			{
				WallType = wallType,
				PaintColor = paintColor
			};
			TilePaintSystemV2.WallRenderTargetHolder wallRenderTargetHolder;
			if (this._wallsRenders.TryGetValue(wallVariationKey, out wallRenderTargetHolder) && wallRenderTargetHolder.IsReady)
			{
				return wallRenderTargetHolder.Target;
			}
			this.RequestWall(ref wallVariationKey);
			return null;
		}

		// Token: 0x0600235C RID: 9052 RVA: 0x0053E600 File Offset: 0x0053C800
		public Texture2D TryGetTreeTopAndRequestIfNotReady(int treeTopIndex, int treeTopStyle, int paintColor)
		{
			TilePaintSystemV2.TreeFoliageVariantKey treeFoliageVariantKey = new TilePaintSystemV2.TreeFoliageVariantKey
			{
				TextureIndex = treeTopIndex,
				TextureStyle = treeTopStyle,
				PaintColor = paintColor
			};
			TilePaintSystemV2.TreeTopRenderTargetHolder treeTopRenderTargetHolder;
			if (this._treeTopRenders.TryGetValue(treeFoliageVariantKey, out treeTopRenderTargetHolder) && treeTopRenderTargetHolder.IsReady)
			{
				return treeTopRenderTargetHolder.Target;
			}
			this.RequestTreeTop(ref treeFoliageVariantKey);
			return null;
		}

		// Token: 0x0600235D RID: 9053 RVA: 0x0053E658 File Offset: 0x0053C858
		public Texture2D TryGetTreeBranchAndRequestIfNotReady(int treeTopIndex, int treeTopStyle, int paintColor)
		{
			TilePaintSystemV2.TreeFoliageVariantKey treeFoliageVariantKey = new TilePaintSystemV2.TreeFoliageVariantKey
			{
				TextureIndex = treeTopIndex,
				TextureStyle = treeTopStyle,
				PaintColor = paintColor
			};
			TilePaintSystemV2.TreeBranchTargetHolder treeBranchTargetHolder;
			if (this._treeBranchRenders.TryGetValue(treeFoliageVariantKey, out treeBranchTargetHolder) && treeBranchTargetHolder.IsReady)
			{
				return treeBranchTargetHolder.Target;
			}
			this.RequestTreeBranch(ref treeFoliageVariantKey);
			return null;
		}

		// Token: 0x0600235E RID: 9054 RVA: 0x0053E6B0 File Offset: 0x0053C8B0
		public void PrepareAllRequests()
		{
			if (this._requests.Count == 0)
			{
				return;
			}
			for (int i = 0; i < this._requests.Count; i++)
			{
				this._requests[i].Prepare();
			}
			this._requests.Clear();
		}

		// Token: 0x0600235F RID: 9055 RVA: 0x0053E700 File Offset: 0x0053C900
		public TilePaintSystemV2()
		{
		}

		// Token: 0x04004D84 RID: 19844
		private Dictionary<TilePaintSystemV2.CageTopVariationkey, TilePaintSystemV2.CageTopRenderTargetHolder> _cageTopRenders = new Dictionary<TilePaintSystemV2.CageTopVariationkey, TilePaintSystemV2.CageTopRenderTargetHolder>();

		// Token: 0x04004D85 RID: 19845
		private Dictionary<TilePaintSystemV2.TileVariationkey, TilePaintSystemV2.TileRenderTargetHolder> _tilesRenders = new Dictionary<TilePaintSystemV2.TileVariationkey, TilePaintSystemV2.TileRenderTargetHolder>();

		// Token: 0x04004D86 RID: 19846
		private Dictionary<TilePaintSystemV2.WallVariationKey, TilePaintSystemV2.WallRenderTargetHolder> _wallsRenders = new Dictionary<TilePaintSystemV2.WallVariationKey, TilePaintSystemV2.WallRenderTargetHolder>();

		// Token: 0x04004D87 RID: 19847
		private Dictionary<TilePaintSystemV2.TreeFoliageVariantKey, TilePaintSystemV2.TreeTopRenderTargetHolder> _treeTopRenders = new Dictionary<TilePaintSystemV2.TreeFoliageVariantKey, TilePaintSystemV2.TreeTopRenderTargetHolder>();

		// Token: 0x04004D88 RID: 19848
		private Dictionary<TilePaintSystemV2.TreeFoliageVariantKey, TilePaintSystemV2.TreeBranchTargetHolder> _treeBranchRenders = new Dictionary<TilePaintSystemV2.TreeFoliageVariantKey, TilePaintSystemV2.TreeBranchTargetHolder>();

		// Token: 0x04004D89 RID: 19849
		private List<TilePaintSystemV2.ARenderTargetHolder> _requests = new List<TilePaintSystemV2.ARenderTargetHolder>();

		// Token: 0x020007DD RID: 2013
		public abstract class ARenderTargetHolder
		{
			// Token: 0x17000536 RID: 1334
			// (get) Token: 0x0600424A RID: 16970 RVA: 0x006BE700 File Offset: 0x006BC900
			public bool IsReady
			{
				get
				{
					return this._wasPrepared;
				}
			}

			// Token: 0x0600424B RID: 16971
			public abstract void Prepare();

			// Token: 0x0600424C RID: 16972
			public abstract void PrepareShader();

			// Token: 0x0600424D RID: 16973 RVA: 0x006BE708 File Offset: 0x006BC908
			public void Clear()
			{
				if (this.Target != null && !this.Target.IsDisposed)
				{
					this.Target.Dispose();
				}
			}

			// Token: 0x0600424E RID: 16974 RVA: 0x006BE72C File Offset: 0x006BC92C
			protected void PrepareTextureIfNecessary(Texture2D originalTexture, Rectangle? sourceRect = null)
			{
				if (this.Target != null && !this.Target.IsContentLost)
				{
					return;
				}
				Main instance = Main.instance;
				if (sourceRect == null)
				{
					sourceRect = new Rectangle?(originalTexture.Frame(1, 1, 0, 0, 0, 0));
				}
				this.Target = new RenderTarget2D(instance.GraphicsDevice, sourceRect.Value.Width, sourceRect.Value.Height, false, instance.GraphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.None, 0, RenderTargetUsage.PreserveContents);
				this.Target.ContentLost += this.Target_ContentLost;
				this.Target.Disposing += this.Target_Disposing;
				this.Target.Name = originalTexture.Name;
				instance.GraphicsDevice.SetRenderTarget(this.Target);
				instance.GraphicsDevice.Clear(Color.Transparent);
				Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
				this.PrepareShader();
				Rectangle value = sourceRect.Value;
				value.X = 0;
				value.Y = 0;
				Main.spriteBatch.Draw(originalTexture, value, Color.White);
				Main.spriteBatch.End();
				instance.GraphicsDevice.SetRenderTarget(null);
				this._wasPrepared = true;
			}

			// Token: 0x0600424F RID: 16975 RVA: 0x006BE86A File Offset: 0x006BCA6A
			private void Target_Disposing(object sender, EventArgs e)
			{
				this._wasPrepared = false;
				this.Target = null;
			}

			// Token: 0x06004250 RID: 16976 RVA: 0x006BE87A File Offset: 0x006BCA7A
			private void Target_ContentLost(object sender, EventArgs e)
			{
				this._wasPrepared = false;
			}

			// Token: 0x06004251 RID: 16977 RVA: 0x006BE884 File Offset: 0x006BCA84
			protected void PrepareShader(int paintColor, TreePaintingSettings settings)
			{
				Effect tileShader = Main.tileShader;
				tileShader.Parameters["leafHueTestOffset"].SetValue(settings.HueTestOffset);
				tileShader.Parameters["leafMinHue"].SetValue(settings.SpecialGroupMinimalHueValue);
				tileShader.Parameters["leafMaxHue"].SetValue(settings.SpecialGroupMaximumHueValue);
				tileShader.Parameters["leafMinSat"].SetValue(settings.SpecialGroupMinimumSaturationValue);
				tileShader.Parameters["leafMaxSat"].SetValue(settings.SpecialGroupMaximumSaturationValue);
				tileShader.Parameters["invertSpecialGroupResult"].SetValue(settings.InvertSpecialGroupResult);
				int num = Main.ConvertPaintIdToTileShaderIndex(paintColor, settings.UseSpecialGroups, settings.UseWallShaderHacks);
				tileShader.CurrentTechnique.Passes[num].Apply();
				RenderTarget2D target = this.Target;
				target.Name = target.Name + " paint: " + paintColor;
			}

			// Token: 0x06004252 RID: 16978 RVA: 0x0000357B File Offset: 0x0000177B
			protected ARenderTargetHolder()
			{
			}

			// Token: 0x04007122 RID: 28962
			public RenderTarget2D Target;

			// Token: 0x04007123 RID: 28963
			protected bool _wasPrepared;
		}

		// Token: 0x020007DE RID: 2014
		public class TreeTopRenderTargetHolder : TilePaintSystemV2.ARenderTargetHolder
		{
			// Token: 0x06004253 RID: 16979 RVA: 0x006BE984 File Offset: 0x006BCB84
			public override void Prepare()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>(TextureAssets.TreeTop[this.Key.TextureIndex].Name, 1);
				base.PrepareTextureIfNecessary(asset.Value, null);
			}

			// Token: 0x06004254 RID: 16980 RVA: 0x006BE9C8 File Offset: 0x006BCBC8
			public override void PrepareShader()
			{
				base.PrepareShader(this.Key.PaintColor, TreePaintSystemData.GetTreeFoliageSettings(this.Key.TextureIndex, this.Key.TextureStyle));
			}

			// Token: 0x06004255 RID: 16981 RVA: 0x006BE9F6 File Offset: 0x006BCBF6
			public TreeTopRenderTargetHolder()
			{
			}

			// Token: 0x04007124 RID: 28964
			public TilePaintSystemV2.TreeFoliageVariantKey Key;
		}

		// Token: 0x020007DF RID: 2015
		public class TreeBranchTargetHolder : TilePaintSystemV2.ARenderTargetHolder
		{
			// Token: 0x06004256 RID: 16982 RVA: 0x006BEA00 File Offset: 0x006BCC00
			public override void Prepare()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>(TextureAssets.TreeBranch[this.Key.TextureIndex].Name, 1);
				base.PrepareTextureIfNecessary(asset.Value, null);
			}

			// Token: 0x06004257 RID: 16983 RVA: 0x006BEA44 File Offset: 0x006BCC44
			public override void PrepareShader()
			{
				base.PrepareShader(this.Key.PaintColor, TreePaintSystemData.GetTreeFoliageSettings(this.Key.TextureIndex, this.Key.TextureStyle));
			}

			// Token: 0x06004258 RID: 16984 RVA: 0x006BE9F6 File Offset: 0x006BCBF6
			public TreeBranchTargetHolder()
			{
			}

			// Token: 0x04007125 RID: 28965
			public TilePaintSystemV2.TreeFoliageVariantKey Key;
		}

		// Token: 0x020007E0 RID: 2016
		public class TileRenderTargetHolder : TilePaintSystemV2.ARenderTargetHolder
		{
			// Token: 0x06004259 RID: 16985 RVA: 0x006BEA74 File Offset: 0x006BCC74
			public override void Prepare()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>(TextureAssets.Tile[this.Key.TileType].Name, 1);
				base.PrepareTextureIfNecessary(asset.Value, null);
			}

			// Token: 0x0600425A RID: 16986 RVA: 0x006BEAB8 File Offset: 0x006BCCB8
			public override void PrepareShader()
			{
				base.PrepareShader(this.Key.PaintColor, TreePaintSystemData.GetTileSettings(this.Key.TileType, this.Key.TileStyle));
			}

			// Token: 0x0600425B RID: 16987 RVA: 0x006BE9F6 File Offset: 0x006BCBF6
			public TileRenderTargetHolder()
			{
			}

			// Token: 0x04007126 RID: 28966
			public TilePaintSystemV2.TileVariationkey Key;
		}

		// Token: 0x020007E1 RID: 2017
		public class CageTopRenderTargetHolder : TilePaintSystemV2.ARenderTargetHolder
		{
			// Token: 0x0600425C RID: 16988 RVA: 0x006BEAE8 File Offset: 0x006BCCE8
			public override void Prepare()
			{
				base.PrepareTextureIfNecessary(TextureAssets.CageTop[this.Key.CageStyle].Value, null);
			}

			// Token: 0x0600425D RID: 16989 RVA: 0x006BEB1A File Offset: 0x006BCD1A
			public override void PrepareShader()
			{
				base.PrepareShader(this.Key.PaintColor, TreePaintSystemData.GetCageTopSettings());
			}

			// Token: 0x0600425E RID: 16990 RVA: 0x006BE9F6 File Offset: 0x006BCBF6
			public CageTopRenderTargetHolder()
			{
			}

			// Token: 0x04007127 RID: 28967
			public TilePaintSystemV2.CageTopVariationkey Key;
		}

		// Token: 0x020007E2 RID: 2018
		public class WallRenderTargetHolder : TilePaintSystemV2.ARenderTargetHolder
		{
			// Token: 0x0600425F RID: 16991 RVA: 0x006BEB34 File Offset: 0x006BCD34
			public override void Prepare()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>(TextureAssets.Wall[this.Key.WallType].Name, 1);
				base.PrepareTextureIfNecessary(asset.Value, null);
			}

			// Token: 0x06004260 RID: 16992 RVA: 0x006BEB78 File Offset: 0x006BCD78
			public override void PrepareShader()
			{
				base.PrepareShader(this.Key.PaintColor, TreePaintSystemData.GetWallSettings(this.Key.WallType));
			}

			// Token: 0x06004261 RID: 16993 RVA: 0x006BE9F6 File Offset: 0x006BCBF6
			public WallRenderTargetHolder()
			{
			}

			// Token: 0x04007128 RID: 28968
			public TilePaintSystemV2.WallVariationKey Key;
		}

		// Token: 0x020007E3 RID: 2019
		public struct TileVariationkey : IEquatable<TilePaintSystemV2.TileVariationkey>
		{
			// Token: 0x06004262 RID: 16994 RVA: 0x006BEB9B File Offset: 0x006BCD9B
			public bool Equals(TilePaintSystemV2.TileVariationkey other)
			{
				return this.TileType == other.TileType && this.TileStyle == other.TileStyle && this.PaintColor == other.PaintColor;
			}

			// Token: 0x06004263 RID: 16995 RVA: 0x006BEBC9 File Offset: 0x006BCDC9
			public override bool Equals(object obj)
			{
				return obj is TilePaintSystemV2.TileVariationkey && this.Equals((TilePaintSystemV2.TileVariationkey)obj);
			}

			// Token: 0x06004264 RID: 16996 RVA: 0x006BEBE1 File Offset: 0x006BCDE1
			public override int GetHashCode()
			{
				return (((this.TileType * 397) ^ this.TileStyle) * 397) ^ this.PaintColor;
			}

			// Token: 0x06004265 RID: 16997 RVA: 0x006BEC03 File Offset: 0x006BCE03
			public static bool operator ==(TilePaintSystemV2.TileVariationkey left, TilePaintSystemV2.TileVariationkey right)
			{
				return left.Equals(right);
			}

			// Token: 0x06004266 RID: 16998 RVA: 0x006BEC0D File Offset: 0x006BCE0D
			public static bool operator !=(TilePaintSystemV2.TileVariationkey left, TilePaintSystemV2.TileVariationkey right)
			{
				return !left.Equals(right);
			}

			// Token: 0x04007129 RID: 28969
			public int TileType;

			// Token: 0x0400712A RID: 28970
			public int TileStyle;

			// Token: 0x0400712B RID: 28971
			public int PaintColor;
		}

		// Token: 0x020007E4 RID: 2020
		public struct WallVariationKey : IEquatable<TilePaintSystemV2.WallVariationKey>
		{
			// Token: 0x06004267 RID: 16999 RVA: 0x006BEC1A File Offset: 0x006BCE1A
			public bool Equals(TilePaintSystemV2.WallVariationKey other)
			{
				return this.WallType == other.WallType && this.PaintColor == other.PaintColor;
			}

			// Token: 0x06004268 RID: 17000 RVA: 0x006BEC3A File Offset: 0x006BCE3A
			public override bool Equals(object obj)
			{
				return obj is TilePaintSystemV2.WallVariationKey && this.Equals((TilePaintSystemV2.WallVariationKey)obj);
			}

			// Token: 0x06004269 RID: 17001 RVA: 0x006BEC52 File Offset: 0x006BCE52
			public override int GetHashCode()
			{
				return (this.WallType * 397) ^ this.PaintColor;
			}

			// Token: 0x0600426A RID: 17002 RVA: 0x006BEC67 File Offset: 0x006BCE67
			public static bool operator ==(TilePaintSystemV2.WallVariationKey left, TilePaintSystemV2.WallVariationKey right)
			{
				return left.Equals(right);
			}

			// Token: 0x0600426B RID: 17003 RVA: 0x006BEC71 File Offset: 0x006BCE71
			public static bool operator !=(TilePaintSystemV2.WallVariationKey left, TilePaintSystemV2.WallVariationKey right)
			{
				return !left.Equals(right);
			}

			// Token: 0x0400712C RID: 28972
			public int WallType;

			// Token: 0x0400712D RID: 28973
			public int PaintColor;
		}

		// Token: 0x020007E5 RID: 2021
		public struct TreeFoliageVariantKey : IEquatable<TilePaintSystemV2.TreeFoliageVariantKey>
		{
			// Token: 0x0600426C RID: 17004 RVA: 0x006BEC7E File Offset: 0x006BCE7E
			public bool Equals(TilePaintSystemV2.TreeFoliageVariantKey other)
			{
				return this.TextureIndex == other.TextureIndex && this.TextureStyle == other.TextureStyle && this.PaintColor == other.PaintColor;
			}

			// Token: 0x0600426D RID: 17005 RVA: 0x006BECAC File Offset: 0x006BCEAC
			public override bool Equals(object obj)
			{
				return obj is TilePaintSystemV2.TreeFoliageVariantKey && this.Equals((TilePaintSystemV2.TreeFoliageVariantKey)obj);
			}

			// Token: 0x0600426E RID: 17006 RVA: 0x006BECC4 File Offset: 0x006BCEC4
			public override int GetHashCode()
			{
				return (((this.TextureIndex * 397) ^ this.TextureStyle) * 397) ^ this.PaintColor;
			}

			// Token: 0x0600426F RID: 17007 RVA: 0x006BECE6 File Offset: 0x006BCEE6
			public static bool operator ==(TilePaintSystemV2.TreeFoliageVariantKey left, TilePaintSystemV2.TreeFoliageVariantKey right)
			{
				return left.Equals(right);
			}

			// Token: 0x06004270 RID: 17008 RVA: 0x006BECF0 File Offset: 0x006BCEF0
			public static bool operator !=(TilePaintSystemV2.TreeFoliageVariantKey left, TilePaintSystemV2.TreeFoliageVariantKey right)
			{
				return !left.Equals(right);
			}

			// Token: 0x0400712E RID: 28974
			public int TextureIndex;

			// Token: 0x0400712F RID: 28975
			public int TextureStyle;

			// Token: 0x04007130 RID: 28976
			public int PaintColor;
		}

		// Token: 0x020007E6 RID: 2022
		public struct CageTopVariationkey
		{
			// Token: 0x06004271 RID: 17009 RVA: 0x006BECFD File Offset: 0x006BCEFD
			public bool Equals(TilePaintSystemV2.CageTopVariationkey other)
			{
				return this.CageStyle == other.CageStyle && this.PaintColor == other.PaintColor;
			}

			// Token: 0x06004272 RID: 17010 RVA: 0x006BED1D File Offset: 0x006BCF1D
			public override bool Equals(object obj)
			{
				return obj is TilePaintSystemV2.CageTopVariationkey && this.Equals((TilePaintSystemV2.CageTopVariationkey)obj);
			}

			// Token: 0x06004273 RID: 17011 RVA: 0x006BED35 File Offset: 0x006BCF35
			public override int GetHashCode()
			{
				return (this.CageStyle * 397) ^ this.PaintColor;
			}

			// Token: 0x06004274 RID: 17012 RVA: 0x006BED4A File Offset: 0x006BCF4A
			public static bool operator ==(TilePaintSystemV2.CageTopVariationkey left, TilePaintSystemV2.CageTopVariationkey right)
			{
				return left.Equals(right);
			}

			// Token: 0x06004275 RID: 17013 RVA: 0x006BED54 File Offset: 0x006BCF54
			public static bool operator !=(TilePaintSystemV2.CageTopVariationkey left, TilePaintSystemV2.CageTopVariationkey right)
			{
				return !left.Equals(right);
			}

			// Token: 0x04007131 RID: 28977
			public int CageStyle;

			// Token: 0x04007132 RID: 28978
			public int PaintColor;
		}
	}
}
