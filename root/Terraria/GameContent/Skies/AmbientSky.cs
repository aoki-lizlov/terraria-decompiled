using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Utilities;
using Terraria.DataStructures;
using Terraria.GameContent.Ambience;
using Terraria.Graphics;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.Utilities;

namespace Terraria.GameContent.Skies
{
	// Token: 0x02000449 RID: 1097
	public class AmbientSky : CustomSky
	{
		// Token: 0x060031DC RID: 12764 RVA: 0x005E4370 File Offset: 0x005E2570
		public override void Activate(Vector2 position, params object[] args)
		{
			this._isActive = true;
		}

		// Token: 0x060031DD RID: 12765 RVA: 0x005E4379 File Offset: 0x005E2579
		public override void Deactivate(params object[] args)
		{
			this._isActive = false;
		}

		// Token: 0x060031DE RID: 12766 RVA: 0x005E4382 File Offset: 0x005E2582
		private bool AnActiveSkyConflictsWithAmbience()
		{
			return SkyManager.Instance["MonolithMoonLord"].IsActive() || SkyManager.Instance["MoonLord"].IsActive();
		}

		// Token: 0x060031DF RID: 12767 RVA: 0x005E43B0 File Offset: 0x005E25B0
		public override void Update(GameTime gameTime)
		{
			if (Main.gamePaused)
			{
				return;
			}
			this._frameCounter++;
			if (Main.netMode != 2 && this.AnActiveSkyConflictsWithAmbience() && SkyManager.Instance["Ambience"].IsActive())
			{
				SkyManager.Instance.Deactivate("Ambience", new object[0]);
			}
			foreach (SlotVector<AmbientSky.SkyEntity>.ItemPair itemPair in this._entities)
			{
				AmbientSky.SkyEntity value = itemPair.Value;
				value.Update(this._frameCounter);
				if (!value.IsActive)
				{
					this._entities.Remove(itemPair.Id);
					if (Main.netMode != 2 && this._entities.Count == 0 && SkyManager.Instance["Ambience"].IsActive())
					{
						SkyManager.Instance.Deactivate("Ambience", new object[0]);
					}
				}
			}
		}

		// Token: 0x060031E0 RID: 12768 RVA: 0x005E44B4 File Offset: 0x005E26B4
		public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
		{
			if (Main.gameMenu && Main.netMode == 0 && SkyManager.Instance["Ambience"].IsActive())
			{
				this._entities.Clear();
				SkyManager.Instance.Deactivate("Ambience", new object[0]);
			}
			foreach (SlotVector<AmbientSky.SkyEntity>.ItemPair itemPair in this._entities)
			{
				itemPair.Value.Draw(spriteBatch, 3f, minDepth, maxDepth);
			}
		}

		// Token: 0x060031E1 RID: 12769 RVA: 0x005E4550 File Offset: 0x005E2750
		public override bool IsActive()
		{
			return this._isActive;
		}

		// Token: 0x060031E2 RID: 12770 RVA: 0x00009E46 File Offset: 0x00008046
		public override void Reset()
		{
		}

		// Token: 0x060031E3 RID: 12771 RVA: 0x005E4558 File Offset: 0x005E2758
		public void Spawn(Player player, SkyEntityType type, int seed)
		{
			FastRandom fastRandom = new FastRandom(seed);
			switch (type)
			{
			case SkyEntityType.BirdsV:
				this._entities.Add(new AmbientSky.BirdsPackSkyEntity(player, fastRandom));
				break;
			case SkyEntityType.Wyvern:
				this._entities.Add(new AmbientSky.WyvernSkyEntity(player, fastRandom));
				break;
			case SkyEntityType.Airship:
				this._entities.Add(new AmbientSky.AirshipSkyEntity(player, fastRandom));
				break;
			case SkyEntityType.AirBalloon:
				this._entities.Add(new AmbientSky.AirBalloonSkyEntity(player, fastRandom));
				break;
			case SkyEntityType.Eyeball:
				this._entities.Add(new AmbientSky.EOCSkyEntity(player, fastRandom));
				break;
			case SkyEntityType.Meteor:
				this._entities.Add(new AmbientSky.MeteorSkyEntity(player, fastRandom));
				break;
			case SkyEntityType.Bats:
			{
				List<AmbientSky.BatsGroupSkyEntity> list = AmbientSky.BatsGroupSkyEntity.CreateGroup(player, fastRandom);
				for (int i = 0; i < list.Count; i++)
				{
					this._entities.Add(list[i]);
				}
				break;
			}
			case SkyEntityType.Butterflies:
				this._entities.Add(new AmbientSky.ButterfliesSkyEntity(player, fastRandom));
				break;
			case SkyEntityType.LostKite:
				this._entities.Add(new AmbientSky.LostKiteSkyEntity(player, fastRandom));
				break;
			case SkyEntityType.Vulture:
				this._entities.Add(new AmbientSky.VultureSkyEntity(player, fastRandom));
				break;
			case SkyEntityType.PixiePosse:
				this._entities.Add(new AmbientSky.PixiePosseSkyEntity(player, fastRandom));
				break;
			case SkyEntityType.Seagulls:
			{
				List<AmbientSky.SeagullsGroupSkyEntity> list2 = AmbientSky.SeagullsGroupSkyEntity.CreateGroup(player, fastRandom);
				for (int j = 0; j < list2.Count; j++)
				{
					this._entities.Add(list2[j]);
				}
				break;
			}
			case SkyEntityType.SlimeBalloons:
			{
				List<AmbientSky.SlimeBalloonGroupSkyEntity> list3 = AmbientSky.SlimeBalloonGroupSkyEntity.CreateGroup(player, fastRandom);
				for (int k = 0; k < list3.Count; k++)
				{
					this._entities.Add(list3[k]);
				}
				break;
			}
			case SkyEntityType.Gastropods:
			{
				List<AmbientSky.GastropodGroupSkyEntity> list4 = AmbientSky.GastropodGroupSkyEntity.CreateGroup(player, fastRandom);
				for (int l = 0; l < list4.Count; l++)
				{
					this._entities.Add(list4[l]);
				}
				break;
			}
			case SkyEntityType.Pegasus:
				this._entities.Add(new AmbientSky.PegasusSkyEntity(player, fastRandom));
				break;
			case SkyEntityType.EaterOfSouls:
				this._entities.Add(new AmbientSky.EOSSkyEntity(player, fastRandom));
				break;
			case SkyEntityType.Crimera:
				this._entities.Add(new AmbientSky.CrimeraSkyEntity(player, fastRandom));
				break;
			case SkyEntityType.Hellbats:
			{
				List<AmbientSky.HellBatsGoupSkyEntity> list5 = AmbientSky.HellBatsGoupSkyEntity.CreateGroup(player, fastRandom);
				for (int m = 0; m < list5.Count; m++)
				{
					this._entities.Add(list5[m]);
				}
				break;
			}
			}
			if (Main.netMode != 2 && !this.AnActiveSkyConflictsWithAmbience() && !SkyManager.Instance["Ambience"].IsActive())
			{
				SkyManager.Instance.Activate("Ambience", default(Vector2), new object[0]);
			}
		}

		// Token: 0x060031E4 RID: 12772 RVA: 0x005E4845 File Offset: 0x005E2A45
		public AmbientSky()
		{
		}

		// Token: 0x040057C7 RID: 22471
		private bool _isActive;

		// Token: 0x040057C8 RID: 22472
		private readonly SlotVector<AmbientSky.SkyEntity> _entities = new SlotVector<AmbientSky.SkyEntity>(500);

		// Token: 0x040057C9 RID: 22473
		private int _frameCounter;

		// Token: 0x0200094A RID: 2378
		private abstract class SkyEntity
		{
			// Token: 0x1700057F RID: 1407
			// (get) Token: 0x06004861 RID: 18529 RVA: 0x006CCFCD File Offset: 0x006CB1CD
			public Rectangle SourceRectangle
			{
				get
				{
					return this.Frame.GetSourceRectangle(this.Texture.Value);
				}
			}

			// Token: 0x06004862 RID: 18530 RVA: 0x006CCFE5 File Offset: 0x006CB1E5
			protected void NextFrame()
			{
				this.Frame.CurrentRow = (this.Frame.CurrentRow + 1) % this.Frame.RowCount;
			}

			// Token: 0x06004863 RID: 18531
			public abstract Color GetColor(Color backgroundColor);

			// Token: 0x06004864 RID: 18532
			public abstract void Update(int frameCount);

			// Token: 0x06004865 RID: 18533 RVA: 0x006CD00C File Offset: 0x006CB20C
			protected void SetPositionInWorldBasedOnScreenSpace(Vector2 actualWorldSpace)
			{
				Vector2 vector = actualWorldSpace - Main.Camera.Center;
				Vector2 vector2 = Main.Camera.Center + vector * (this.Depth / 3f);
				this.Position = vector2;
			}

			// Token: 0x06004866 RID: 18534
			public abstract Vector2 GetDrawPosition();

			// Token: 0x06004867 RID: 18535 RVA: 0x006CD053 File Offset: 0x006CB253
			public virtual void Draw(SpriteBatch spriteBatch, float depthScale, float minDepth, float maxDepth)
			{
				this.CommonDraw(spriteBatch, depthScale, minDepth, maxDepth);
			}

			// Token: 0x06004868 RID: 18536 RVA: 0x006CD060 File Offset: 0x006CB260
			public void CommonDraw(SpriteBatch spriteBatch, float depthScale, float minDepth, float maxDepth)
			{
				if (this.Depth <= minDepth || this.Depth > maxDepth)
				{
					return;
				}
				Vector2 drawPositionByDepth = this.GetDrawPositionByDepth();
				Color color = this.GetColor(Main.ColorOfTheSkies) * Main.atmo;
				Vector2 vector = this.SourceRectangle.Size() / 2f;
				float num = depthScale / this.Depth;
				spriteBatch.Draw(this.Texture.Value, drawPositionByDepth - Main.Camera.UnscaledPosition, new Rectangle?(this.SourceRectangle), color, this.Rotation, vector, num, this.Effects, 0f);
			}

			// Token: 0x06004869 RID: 18537 RVA: 0x006CD100 File Offset: 0x006CB300
			internal Vector2 GetDrawPositionByDepth()
			{
				return (this.GetDrawPosition() - Main.Camera.Center) * new Vector2(1f / this.Depth, 0.9f / this.Depth) + Main.Camera.Center;
			}

			// Token: 0x0600486A RID: 18538 RVA: 0x006CD154 File Offset: 0x006CB354
			internal float Helper_GetOpacityWithAccountingForOceanWaterLine()
			{
				ref Vector2 ptr = this.GetDrawPositionByDepth() - Main.Camera.UnscaledPosition;
				int num = this.SourceRectangle.Height / 2;
				float num2 = ptr.Y + (float)num;
				float yscreenPosition = AmbientSkyDrawCache.Instance.OceanLineInfo.YScreenPosition;
				float num3 = Utils.GetLerpValue(yscreenPosition - 10f, yscreenPosition - 2f, num2, true);
				num3 *= AmbientSkyDrawCache.Instance.OceanLineInfo.OceanOpacity;
				return 1f - num3;
			}

			// Token: 0x0600486B RID: 18539 RVA: 0x006CD1CC File Offset: 0x006CB3CC
			protected SkyEntity()
			{
			}

			// Token: 0x0400756D RID: 30061
			public Vector2 Position;

			// Token: 0x0400756E RID: 30062
			public Asset<Texture2D> Texture;

			// Token: 0x0400756F RID: 30063
			public SpriteFrame Frame;

			// Token: 0x04007570 RID: 30064
			public float Depth;

			// Token: 0x04007571 RID: 30065
			public SpriteEffects Effects;

			// Token: 0x04007572 RID: 30066
			public bool IsActive = true;

			// Token: 0x04007573 RID: 30067
			public float Rotation;
		}

		// Token: 0x0200094B RID: 2379
		private class FadingSkyEntity : AmbientSky.SkyEntity
		{
			// Token: 0x0600486C RID: 18540 RVA: 0x006CD1DC File Offset: 0x006CB3DC
			public FadingSkyEntity()
			{
				this.Opacity = 0f;
				this.TimeEntitySpawnedIn = -1;
				this.BrightnessLerper = 0f;
				this.FinalOpacityMultiplier = 1f;
				this.OpacityNormalizedTimeToFadeIn = 0.1f;
				this.OpacityNormalizedTimeToFadeOut = 0.9f;
			}

			// Token: 0x0600486D RID: 18541 RVA: 0x006CD230 File Offset: 0x006CB430
			public override void Update(int frameCount)
			{
				if (this.IsMovementDone(frameCount))
				{
					return;
				}
				this.UpdateOpacity(frameCount);
				if ((frameCount + this.FrameOffset) % this.FramingSpeed == 0)
				{
					base.NextFrame();
				}
				this.UpdateVelocity(frameCount);
				this.Position += this.Velocity;
			}

			// Token: 0x0600486E RID: 18542 RVA: 0x00009E46 File Offset: 0x00008046
			public virtual void UpdateVelocity(int frameCount)
			{
			}

			// Token: 0x0600486F RID: 18543 RVA: 0x006CD284 File Offset: 0x006CB484
			private void UpdateOpacity(int frameCount)
			{
				int num = frameCount - this.TimeEntitySpawnedIn;
				if ((float)num >= (float)this.LifeTime * this.OpacityNormalizedTimeToFadeOut)
				{
					this.Opacity = Utils.GetLerpValue((float)this.LifeTime, (float)this.LifeTime * this.OpacityNormalizedTimeToFadeOut, (float)num, true);
					return;
				}
				this.Opacity = Utils.GetLerpValue(0f, (float)this.LifeTime * this.OpacityNormalizedTimeToFadeIn, (float)num, true);
			}

			// Token: 0x06004870 RID: 18544 RVA: 0x006CD2F1 File Offset: 0x006CB4F1
			private bool IsMovementDone(int frameCount)
			{
				if (this.TimeEntitySpawnedIn == -1)
				{
					this.TimeEntitySpawnedIn = frameCount;
				}
				if (frameCount - this.TimeEntitySpawnedIn >= this.LifeTime)
				{
					this.IsActive = false;
					return true;
				}
				return false;
			}

			// Token: 0x06004871 RID: 18545 RVA: 0x006CD31D File Offset: 0x006CB51D
			public override Color GetColor(Color backgroundColor)
			{
				return Color.Lerp(backgroundColor, Color.White, this.BrightnessLerper) * this.Opacity * this.FinalOpacityMultiplier * base.Helper_GetOpacityWithAccountingForOceanWaterLine();
			}

			// Token: 0x06004872 RID: 18546 RVA: 0x006CD354 File Offset: 0x006CB554
			public void StartFadingOut(int currentFrameCount)
			{
				int num = (int)((float)this.LifeTime * this.OpacityNormalizedTimeToFadeOut);
				int num2 = currentFrameCount - num;
				if (num2 < this.TimeEntitySpawnedIn)
				{
					this.TimeEntitySpawnedIn = num2;
				}
			}

			// Token: 0x06004873 RID: 18547 RVA: 0x006CD385 File Offset: 0x006CB585
			public override Vector2 GetDrawPosition()
			{
				return this.Position;
			}

			// Token: 0x04007574 RID: 30068
			protected int LifeTime;

			// Token: 0x04007575 RID: 30069
			protected Vector2 Velocity;

			// Token: 0x04007576 RID: 30070
			protected int FramingSpeed;

			// Token: 0x04007577 RID: 30071
			protected int TimeEntitySpawnedIn;

			// Token: 0x04007578 RID: 30072
			protected float Opacity;

			// Token: 0x04007579 RID: 30073
			protected float BrightnessLerper;

			// Token: 0x0400757A RID: 30074
			protected float FinalOpacityMultiplier;

			// Token: 0x0400757B RID: 30075
			protected float OpacityNormalizedTimeToFadeIn;

			// Token: 0x0400757C RID: 30076
			protected float OpacityNormalizedTimeToFadeOut;

			// Token: 0x0400757D RID: 30077
			protected int FrameOffset;
		}

		// Token: 0x0200094C RID: 2380
		private class ButterfliesSkyEntity : AmbientSky.FadingSkyEntity
		{
			// Token: 0x06004874 RID: 18548 RVA: 0x006CD390 File Offset: 0x006CB590
			public ButterfliesSkyEntity(Player player, FastRandom random)
			{
				VirtualCamera virtualCamera = new VirtualCamera(player);
				this.Effects = ((Main.WindForVisuals > 0f) ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
				int num = 100;
				if (this.Effects == SpriteEffects.FlipHorizontally)
				{
					this.Position.X = virtualCamera.Position.X + virtualCamera.Size.X + (float)num;
				}
				else
				{
					this.Position.X = virtualCamera.Position.X - (float)num;
				}
				this.Position.Y = random.NextFloat() * ((float)Main.worldSurface * 16f - 4000f) + 4000f;
				this.Depth = random.NextFloat() * 3f + 3f;
				base.SetPositionInWorldBasedOnScreenSpace(this.Position);
				int num2 = random.Next(2) + 1;
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/ButterflySwarm" + num2, 1);
				this.Frame = new SpriteFrame(1, (num2 == 2) ? 19 : 17);
				this.LifeTime = random.Next(60, 121) * 60;
				this.OpacityNormalizedTimeToFadeIn = 0.15f;
				this.OpacityNormalizedTimeToFadeOut = 0.85f;
				this.BrightnessLerper = 0.2f;
				this.FinalOpacityMultiplier = 1f;
				this.FramingSpeed = 5;
			}

			// Token: 0x06004875 RID: 18549 RVA: 0x006CD4EC File Offset: 0x006CB6EC
			public override void UpdateVelocity(int frameCount)
			{
				float num = 0.1f + Math.Abs(Main.WindForVisuals) * 0.05f;
				this.Velocity = new Vector2(num * (float)((this.Effects == SpriteEffects.FlipHorizontally) ? (-1) : 1), 0f);
			}

			// Token: 0x06004876 RID: 18550 RVA: 0x006CD530 File Offset: 0x006CB730
			public override void Update(int frameCount)
			{
				base.Update(frameCount);
				if (Main.IsItRaining || !Main.dayTime || Main.eclipse)
				{
					base.StartFadingOut(frameCount);
				}
			}
		}

		// Token: 0x0200094D RID: 2381
		private class LostKiteSkyEntity : AmbientSky.FadingSkyEntity
		{
			// Token: 0x06004877 RID: 18551 RVA: 0x006CD558 File Offset: 0x006CB758
			public LostKiteSkyEntity(Player player, FastRandom random)
			{
				VirtualCamera virtualCamera = new VirtualCamera(player);
				this.Effects = ((Main.WindForVisuals > 0f) ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
				int num = 100;
				if (this.Effects == SpriteEffects.FlipHorizontally)
				{
					this.Position.X = virtualCamera.Position.X + virtualCamera.Size.X + (float)num;
				}
				else
				{
					this.Position.X = virtualCamera.Position.X - (float)num;
				}
				this.Position.Y = random.NextFloat() * ((float)Main.worldSurface * 16f - 1600f - 2400f) + 2400f;
				this.Depth = random.NextFloat() * 3f + 3f;
				base.SetPositionInWorldBasedOnScreenSpace(this.Position);
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/LostKite", 1);
				this.Frame = new SpriteFrame(1, 42);
				this.LifeTime = random.Next(60, 121) * 60;
				this.OpacityNormalizedTimeToFadeIn = 0.15f;
				this.OpacityNormalizedTimeToFadeOut = 0.85f;
				this.BrightnessLerper = 0.2f;
				this.FinalOpacityMultiplier = 1f;
				this.FramingSpeed = 6;
				int num2 = random.Next((int)this.Frame.RowCount);
				for (int i = 0; i < num2; i++)
				{
					base.NextFrame();
				}
			}

			// Token: 0x06004878 RID: 18552 RVA: 0x006CD6C0 File Offset: 0x006CB8C0
			public override void UpdateVelocity(int frameCount)
			{
				float num = 1.2f + Math.Abs(Main.WindForVisuals) * 3f;
				if (Main.IsItStorming)
				{
					num *= 1.5f;
				}
				this.Velocity = new Vector2(num * (float)((this.Effects == SpriteEffects.FlipHorizontally) ? (-1) : 1), 0f);
			}

			// Token: 0x06004879 RID: 18553 RVA: 0x006CD714 File Offset: 0x006CB914
			public override void Update(int frameCount)
			{
				if (Main.IsItStorming)
				{
					this.FramingSpeed = 4;
				}
				this.Effects = ((Main.WindForVisuals > 0f) ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
				base.Update(frameCount);
				if (!Main.dayTime || Main.eclipse)
				{
					base.StartFadingOut(frameCount);
				}
			}
		}

		// Token: 0x0200094E RID: 2382
		private class PegasusSkyEntity : AmbientSky.FadingSkyEntity
		{
			// Token: 0x0600487A RID: 18554 RVA: 0x006CD764 File Offset: 0x006CB964
			public PegasusSkyEntity(Player player, FastRandom random)
			{
				VirtualCamera virtualCamera = new VirtualCamera(player);
				this.Effects = ((Main.WindForVisuals > 0f) ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
				int num = 100;
				if (this.Effects == SpriteEffects.FlipHorizontally)
				{
					this.Position.X = virtualCamera.Position.X + virtualCamera.Size.X + (float)num;
				}
				else
				{
					this.Position.X = virtualCamera.Position.X - (float)num;
				}
				this.Position.Y = random.NextFloat() * ((float)Main.worldSurface * 16f - 1600f - 2400f) + 2400f;
				this.Depth = random.NextFloat() * 3f + 3f;
				base.SetPositionInWorldBasedOnScreenSpace(this.Position);
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/Pegasus", 1);
				this.Frame = new SpriteFrame(1, 11);
				this.LifeTime = random.Next(60, 121) * 60;
				this.OpacityNormalizedTimeToFadeIn = 0.15f;
				this.OpacityNormalizedTimeToFadeOut = 0.85f;
				this.BrightnessLerper = 0.2f;
				this.FinalOpacityMultiplier = 1f;
				this.FramingSpeed = 5;
			}

			// Token: 0x0600487B RID: 18555 RVA: 0x006CD8A8 File Offset: 0x006CBAA8
			public override void UpdateVelocity(int frameCount)
			{
				float num = 1.5f + Math.Abs(Main.WindForVisuals) * 0.6f;
				this.Velocity = new Vector2(num * (float)((this.Effects == SpriteEffects.FlipHorizontally) ? (-1) : 1), 0f);
			}

			// Token: 0x0600487C RID: 18556 RVA: 0x006CD530 File Offset: 0x006CB730
			public override void Update(int frameCount)
			{
				base.Update(frameCount);
				if (Main.IsItRaining || !Main.dayTime || Main.eclipse)
				{
					base.StartFadingOut(frameCount);
				}
			}

			// Token: 0x0600487D RID: 18557 RVA: 0x006CD8EC File Offset: 0x006CBAEC
			public override Color GetColor(Color backgroundColor)
			{
				return base.GetColor(backgroundColor) * Main.bgAlphaFrontLayer[6];
			}
		}

		// Token: 0x0200094F RID: 2383
		private class VultureSkyEntity : AmbientSky.FadingSkyEntity
		{
			// Token: 0x0600487E RID: 18558 RVA: 0x006CD904 File Offset: 0x006CBB04
			public VultureSkyEntity(Player player, FastRandom random)
			{
				VirtualCamera virtualCamera = new VirtualCamera(player);
				this.Effects = ((Main.WindForVisuals > 0f) ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
				int num = 100;
				if (this.Effects == SpriteEffects.FlipHorizontally)
				{
					this.Position.X = virtualCamera.Position.X + virtualCamera.Size.X + (float)num;
				}
				else
				{
					this.Position.X = virtualCamera.Position.X - (float)num;
				}
				this.Position.Y = random.NextFloat() * ((float)Main.worldSurface * 16f - 1600f - 2400f) + 2400f;
				this.Depth = random.NextFloat() * 3f + 3f;
				base.SetPositionInWorldBasedOnScreenSpace(this.Position);
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/Vulture", 1);
				this.Frame = new SpriteFrame(1, 10);
				this.LifeTime = random.Next(60, 121) * 60;
				this.OpacityNormalizedTimeToFadeIn = 0.15f;
				this.OpacityNormalizedTimeToFadeOut = 0.85f;
				this.BrightnessLerper = 0.2f;
				this.FinalOpacityMultiplier = 1f;
				this.FramingSpeed = 5;
			}

			// Token: 0x0600487F RID: 18559 RVA: 0x006CDA48 File Offset: 0x006CBC48
			public override void UpdateVelocity(int frameCount)
			{
				float num = 3f + Math.Abs(Main.WindForVisuals) * 0.8f;
				this.Velocity = new Vector2(num * (float)((this.Effects == SpriteEffects.FlipHorizontally) ? (-1) : 1), 0f);
			}

			// Token: 0x06004880 RID: 18560 RVA: 0x006CD530 File Offset: 0x006CB730
			public override void Update(int frameCount)
			{
				base.Update(frameCount);
				if (Main.IsItRaining || !Main.dayTime || Main.eclipse)
				{
					base.StartFadingOut(frameCount);
				}
			}

			// Token: 0x06004881 RID: 18561 RVA: 0x006CDA8C File Offset: 0x006CBC8C
			public override Color GetColor(Color backgroundColor)
			{
				float num = Math.Max(Main.bgAlphaFrontLayer[5], Main.bgAlphaFrontLayer[14]);
				num = Math.Max(num, Main.bgAlphaFrontLayer[13]);
				return base.GetColor(backgroundColor) * Math.Max(Main.bgAlphaFrontLayer[2], num);
			}
		}

		// Token: 0x02000950 RID: 2384
		private class PixiePosseSkyEntity : AmbientSky.FadingSkyEntity
		{
			// Token: 0x06004882 RID: 18562 RVA: 0x006CDAD8 File Offset: 0x006CBCD8
			public PixiePosseSkyEntity(Player player, FastRandom random)
			{
				VirtualCamera virtualCamera = new VirtualCamera(player);
				this.Effects = ((Main.WindForVisuals > 0f) ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
				int num = 100;
				if (this.Effects == SpriteEffects.FlipHorizontally)
				{
					this.Position.X = virtualCamera.Position.X + virtualCamera.Size.X + (float)num;
				}
				else
				{
					this.Position.X = virtualCamera.Position.X - (float)num;
				}
				this.Position.Y = random.NextFloat() * ((float)Main.worldSurface * 16f - 4000f) + 4000f;
				this.Depth = random.NextFloat() * 3f + 2f;
				base.SetPositionInWorldBasedOnScreenSpace(this.Position);
				if (!Main.dayTime)
				{
					this.pixieType = 2;
				}
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/PixiePosse" + this.pixieType, 1);
				this.Frame = new SpriteFrame(1, 25);
				this.LifeTime = random.Next(60, 121) * 60;
				this.OpacityNormalizedTimeToFadeIn = 0.15f;
				this.OpacityNormalizedTimeToFadeOut = 0.85f;
				this.BrightnessLerper = 0.6f;
				this.FinalOpacityMultiplier = 1f;
				this.FramingSpeed = 5;
			}

			// Token: 0x06004883 RID: 18563 RVA: 0x006CDC38 File Offset: 0x006CBE38
			public override void UpdateVelocity(int frameCount)
			{
				float num = 0.12f + Math.Abs(Main.WindForVisuals) * 0.08f;
				this.Velocity = new Vector2(num * (float)((this.Effects == SpriteEffects.FlipHorizontally) ? (-1) : 1), 0f);
			}

			// Token: 0x06004884 RID: 18564 RVA: 0x006CDC7C File Offset: 0x006CBE7C
			public override void Update(int frameCount)
			{
				base.Update(frameCount);
				if ((this.pixieType == 1 && !Main.dayTime) || (this.pixieType == 2 && Main.dayTime) || Main.IsItRaining || Main.eclipse || Main.bloodMoon || Main.pumpkinMoon || Main.snowMoon)
				{
					base.StartFadingOut(frameCount);
				}
			}

			// Token: 0x06004885 RID: 18565 RVA: 0x006CDCDA File Offset: 0x006CBEDA
			public override void Draw(SpriteBatch spriteBatch, float depthScale, float minDepth, float maxDepth)
			{
				base.CommonDraw(spriteBatch, depthScale - 0.1f, minDepth, maxDepth);
			}

			// Token: 0x0400757E RID: 30078
			private int pixieType = 1;
		}

		// Token: 0x02000951 RID: 2385
		private class BirdsPackSkyEntity : AmbientSky.FadingSkyEntity
		{
			// Token: 0x06004886 RID: 18566 RVA: 0x006CDCF0 File Offset: 0x006CBEF0
			public BirdsPackSkyEntity(Player player, FastRandom random)
			{
				VirtualCamera virtualCamera = new VirtualCamera(player);
				this.Effects = ((Main.WindForVisuals > 0f) ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
				int num = 100;
				if (this.Effects == SpriteEffects.FlipHorizontally)
				{
					this.Position.X = virtualCamera.Position.X + virtualCamera.Size.X + (float)num;
				}
				else
				{
					this.Position.X = virtualCamera.Position.X - (float)num;
				}
				this.Position.Y = random.NextFloat() * ((float)Main.worldSurface * 16f - 1600f - 2400f) + 2400f;
				this.Depth = random.NextFloat() * 3f + 3f;
				base.SetPositionInWorldBasedOnScreenSpace(this.Position);
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/BirdsVShape", 1);
				this.Frame = new SpriteFrame(1, 4);
				this.LifeTime = random.Next(60, 121) * 60;
				this.OpacityNormalizedTimeToFadeIn = 0.15f;
				this.OpacityNormalizedTimeToFadeOut = 0.85f;
				this.BrightnessLerper = 0.2f;
				this.FinalOpacityMultiplier = 1f;
				this.FramingSpeed = 5;
			}

			// Token: 0x06004887 RID: 18567 RVA: 0x006CDE30 File Offset: 0x006CC030
			public override void UpdateVelocity(int frameCount)
			{
				float num = 3f + Math.Abs(Main.WindForVisuals) * 0.8f;
				this.Velocity = new Vector2(num * (float)((this.Effects == SpriteEffects.FlipHorizontally) ? (-1) : 1), 0f);
			}

			// Token: 0x06004888 RID: 18568 RVA: 0x006CD530 File Offset: 0x006CB730
			public override void Update(int frameCount)
			{
				base.Update(frameCount);
				if (Main.IsItRaining || !Main.dayTime || Main.eclipse)
				{
					base.StartFadingOut(frameCount);
				}
			}
		}

		// Token: 0x02000952 RID: 2386
		private class SeagullsGroupSkyEntity : AmbientSky.FadingSkyEntity
		{
			// Token: 0x06004889 RID: 18569 RVA: 0x006CDE74 File Offset: 0x006CC074
			public SeagullsGroupSkyEntity(Player player, FastRandom random)
			{
				VirtualCamera virtualCamera = new VirtualCamera(player);
				this.Effects = ((Main.WindForVisuals > 0f) ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
				int num = 100;
				if (this.Effects == SpriteEffects.FlipHorizontally)
				{
					this.Position.X = virtualCamera.Position.X + virtualCamera.Size.X + (float)num;
				}
				else
				{
					this.Position.X = virtualCamera.Position.X - (float)num;
				}
				this.Position.Y = random.NextFloat() * ((float)Main.worldSurface * 16f - 1600f - 2400f) + 2400f;
				this.Depth = random.NextFloat() * 3f + 3f;
				base.SetPositionInWorldBasedOnScreenSpace(this.Position);
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/Seagull", 1);
				this.Frame = new SpriteFrame(1, 9);
				this.LifeTime = random.Next(60, 121) * 60;
				this.OpacityNormalizedTimeToFadeIn = 0.1f;
				this.OpacityNormalizedTimeToFadeOut = 0.9f;
				this.BrightnessLerper = 0.2f;
				this.FinalOpacityMultiplier = 1f;
				this.FramingSpeed = 4;
				this.FrameOffset = random.Next(0, (int)this.Frame.RowCount);
				int num2 = random.Next((int)this.Frame.RowCount);
				for (int i = 0; i < num2; i++)
				{
					base.NextFrame();
				}
			}

			// Token: 0x0600488A RID: 18570 RVA: 0x006CDFF4 File Offset: 0x006CC1F4
			public override void UpdateVelocity(int frameCount)
			{
				Vector2 vector = this._magnetAccelerations * new Vector2((float)Math.Sign(this._magnetPointTarget.X - this._positionVsMagnet.X), (float)Math.Sign(this._magnetPointTarget.Y - this._positionVsMagnet.Y));
				this._velocityVsMagnet += vector;
				this._positionVsMagnet += this._velocityVsMagnet;
				float num = 4f * (float)((this.Effects == SpriteEffects.FlipHorizontally) ? (-1) : 1);
				this.Velocity = new Vector2(num, 0f) + this._velocityVsMagnet;
			}

			// Token: 0x0600488B RID: 18571 RVA: 0x006CD530 File Offset: 0x006CB730
			public override void Update(int frameCount)
			{
				base.Update(frameCount);
				if (Main.IsItRaining || !Main.dayTime || Main.eclipse)
				{
					base.StartFadingOut(frameCount);
				}
			}

			// Token: 0x0600488C RID: 18572 RVA: 0x006CE0A6 File Offset: 0x006CC2A6
			public void SetMagnetization(Vector2 accelerations, Vector2 targetOffset)
			{
				this._magnetAccelerations = accelerations;
				this._magnetPointTarget = targetOffset;
			}

			// Token: 0x0600488D RID: 18573 RVA: 0x006CE0B6 File Offset: 0x006CC2B6
			public override Color GetColor(Color backgroundColor)
			{
				return base.GetColor(backgroundColor) * Main.bgAlphaFrontLayer[4];
			}

			// Token: 0x0600488E RID: 18574 RVA: 0x006CE0CB File Offset: 0x006CC2CB
			public override void Draw(SpriteBatch spriteBatch, float depthScale, float minDepth, float maxDepth)
			{
				base.CommonDraw(spriteBatch, depthScale - 1.5f, minDepth, maxDepth);
			}

			// Token: 0x0600488F RID: 18575 RVA: 0x006CE0E0 File Offset: 0x006CC2E0
			public static List<AmbientSky.SeagullsGroupSkyEntity> CreateGroup(Player player, FastRandom random)
			{
				List<AmbientSky.SeagullsGroupSkyEntity> list = new List<AmbientSky.SeagullsGroupSkyEntity>();
				int num = 100;
				int num2 = random.Next(5, 9);
				float num3 = 100f;
				VirtualCamera virtualCamera = new VirtualCamera(player);
				SpriteEffects spriteEffects = ((Main.WindForVisuals > 0f) ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
				Vector2 vector = default(Vector2);
				if (spriteEffects == SpriteEffects.FlipHorizontally)
				{
					vector.X = virtualCamera.Position.X + virtualCamera.Size.X + (float)num;
				}
				else
				{
					vector.X = virtualCamera.Position.X - (float)num;
				}
				vector.Y = random.NextFloat() * ((float)Main.worldSurface * 16f - 1600f - 2400f) + 2400f;
				float num4 = random.NextFloat() * 2f + 1f;
				int num5 = random.Next(30, 61) * 60;
				Vector2 vector2 = new Vector2(random.NextFloat() * 0.5f + 0.5f, random.NextFloat() * 0.5f + 0.5f);
				Vector2 vector3 = new Vector2(random.NextFloat() * 2f - 1f, random.NextFloat() * 2f - 1f) * num3;
				for (int i = 0; i < num2; i++)
				{
					AmbientSky.SeagullsGroupSkyEntity seagullsGroupSkyEntity = new AmbientSky.SeagullsGroupSkyEntity(player, random);
					seagullsGroupSkyEntity.Depth = num4 + random.NextFloat() * 0.5f;
					seagullsGroupSkyEntity.Position = vector + new Vector2(random.NextFloat() * 20f - 10f, random.NextFloat() * 3f) * 50f;
					seagullsGroupSkyEntity.Effects = spriteEffects;
					seagullsGroupSkyEntity.SetPositionInWorldBasedOnScreenSpace(seagullsGroupSkyEntity.Position);
					seagullsGroupSkyEntity.LifeTime = num5 + random.Next(301);
					seagullsGroupSkyEntity.SetMagnetization(vector2 * (random.NextFloat() * 0.3f + 0.85f) * 0.05f, vector3);
					list.Add(seagullsGroupSkyEntity);
				}
				return list;
			}

			// Token: 0x0400757F RID: 30079
			private Vector2 _magnetAccelerations;

			// Token: 0x04007580 RID: 30080
			private Vector2 _magnetPointTarget;

			// Token: 0x04007581 RID: 30081
			private Vector2 _positionVsMagnet;

			// Token: 0x04007582 RID: 30082
			private Vector2 _velocityVsMagnet;
		}

		// Token: 0x02000953 RID: 2387
		private class GastropodGroupSkyEntity : AmbientSky.FadingSkyEntity
		{
			// Token: 0x06004890 RID: 18576 RVA: 0x006CE2F4 File Offset: 0x006CC4F4
			public GastropodGroupSkyEntity(Player player, FastRandom random)
			{
				VirtualCamera virtualCamera = new VirtualCamera(player);
				this.Effects = ((Main.WindForVisuals > 0f) ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
				int num = 100;
				if (this.Effects == SpriteEffects.FlipHorizontally)
				{
					this.Position.X = virtualCamera.Position.X + virtualCamera.Size.X + (float)num;
				}
				else
				{
					this.Position.X = virtualCamera.Position.X - (float)num;
				}
				this.Position.Y = random.NextFloat() * ((float)Main.worldSurface * 16f - 1600f - 3200f) + 3200f;
				this.Depth = random.NextFloat() * 3f + 2f;
				base.SetPositionInWorldBasedOnScreenSpace(this.Position);
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/Gastropod", 1);
				this.Frame = new SpriteFrame(1, 1);
				this.LifeTime = random.Next(60, 121) * 60;
				this.OpacityNormalizedTimeToFadeIn = 0.1f;
				this.OpacityNormalizedTimeToFadeOut = 0.9f;
				this.BrightnessLerper = 0.75f;
				this.FinalOpacityMultiplier = 1f;
				this.FramingSpeed = int.MaxValue;
			}

			// Token: 0x06004891 RID: 18577 RVA: 0x006CE438 File Offset: 0x006CC638
			public override void UpdateVelocity(int frameCount)
			{
				Vector2 vector = this._magnetAccelerations * new Vector2((float)Math.Sign(this._magnetPointTarget.X - this._positionVsMagnet.X), (float)Math.Sign(this._magnetPointTarget.Y - this._positionVsMagnet.Y));
				this._velocityVsMagnet += vector;
				this._positionVsMagnet += this._velocityVsMagnet;
				float num = (1.5f + Math.Abs(Main.WindForVisuals) * 0.2f) * (float)((this.Effects == SpriteEffects.FlipHorizontally) ? (-1) : 1);
				this.Velocity = new Vector2(num, 0f) + this._velocityVsMagnet;
				this.Rotation = this.Velocity.X * 0.1f;
			}

			// Token: 0x06004892 RID: 18578 RVA: 0x006CE512 File Offset: 0x006CC712
			public override void Update(int frameCount)
			{
				base.Update(frameCount);
				if (Main.IsItRaining || Main.dayTime || Main.bloodMoon || Main.pumpkinMoon || Main.snowMoon)
				{
					base.StartFadingOut(frameCount);
				}
			}

			// Token: 0x06004893 RID: 18579 RVA: 0x006CE545 File Offset: 0x006CC745
			public override Color GetColor(Color backgroundColor)
			{
				return Color.Lerp(backgroundColor, Colors.AmbientNPCGastropodLight, this.BrightnessLerper) * this.Opacity * this.FinalOpacityMultiplier;
			}

			// Token: 0x06004894 RID: 18580 RVA: 0x006CDCDA File Offset: 0x006CBEDA
			public override void Draw(SpriteBatch spriteBatch, float depthScale, float minDepth, float maxDepth)
			{
				base.CommonDraw(spriteBatch, depthScale - 0.1f, minDepth, maxDepth);
			}

			// Token: 0x06004895 RID: 18581 RVA: 0x006CE56E File Offset: 0x006CC76E
			public void SetMagnetization(Vector2 accelerations, Vector2 targetOffset)
			{
				this._magnetAccelerations = accelerations;
				this._magnetPointTarget = targetOffset;
			}

			// Token: 0x06004896 RID: 18582 RVA: 0x006CE580 File Offset: 0x006CC780
			public static List<AmbientSky.GastropodGroupSkyEntity> CreateGroup(Player player, FastRandom random)
			{
				List<AmbientSky.GastropodGroupSkyEntity> list = new List<AmbientSky.GastropodGroupSkyEntity>();
				int num = 100;
				int num2 = random.Next(3, 8);
				VirtualCamera virtualCamera = new VirtualCamera(player);
				SpriteEffects spriteEffects = ((Main.WindForVisuals > 0f) ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
				Vector2 vector = default(Vector2);
				if (spriteEffects == SpriteEffects.FlipHorizontally)
				{
					vector.X = virtualCamera.Position.X + virtualCamera.Size.X + (float)num;
				}
				else
				{
					vector.X = virtualCamera.Position.X - (float)num;
				}
				vector.Y = random.NextFloat() * ((float)Main.worldSurface * 16f - 1600f - 3200f) + 3200f;
				float num3 = random.NextFloat() * 3f + 2f;
				int num4 = random.Next(30, 61) * 60;
				Vector2 vector2 = new Vector2(random.NextFloat() * 0.1f + 0.1f, random.NextFloat() * 0.3f + 0.3f);
				Vector2 vector3 = new Vector2(random.NextFloat() * 2f - 1f, random.NextFloat() * 2f - 1f) * 120f;
				for (int i = 0; i < num2; i++)
				{
					AmbientSky.GastropodGroupSkyEntity gastropodGroupSkyEntity = new AmbientSky.GastropodGroupSkyEntity(player, random);
					gastropodGroupSkyEntity.Depth = num3 + random.NextFloat() * 0.5f;
					gastropodGroupSkyEntity.Position = vector + new Vector2(random.NextFloat() * 20f - 10f, random.NextFloat() * 3f) * 60f;
					gastropodGroupSkyEntity.Effects = spriteEffects;
					gastropodGroupSkyEntity.SetPositionInWorldBasedOnScreenSpace(gastropodGroupSkyEntity.Position);
					gastropodGroupSkyEntity.LifeTime = num4 + random.Next(301);
					gastropodGroupSkyEntity.SetMagnetization(vector2 * (random.NextFloat() * 0.5f) * 0.05f, vector3);
					list.Add(gastropodGroupSkyEntity);
				}
				return list;
			}

			// Token: 0x04007583 RID: 30083
			private Vector2 _magnetAccelerations;

			// Token: 0x04007584 RID: 30084
			private Vector2 _magnetPointTarget;

			// Token: 0x04007585 RID: 30085
			private Vector2 _positionVsMagnet;

			// Token: 0x04007586 RID: 30086
			private Vector2 _velocityVsMagnet;
		}

		// Token: 0x02000954 RID: 2388
		private class SlimeBalloonGroupSkyEntity : AmbientSky.FadingSkyEntity
		{
			// Token: 0x06004897 RID: 18583 RVA: 0x006CE788 File Offset: 0x006CC988
			public SlimeBalloonGroupSkyEntity(Player player, FastRandom random)
			{
				VirtualCamera virtualCamera = new VirtualCamera(player);
				this.Effects = ((Main.WindForVisuals > 0f) ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
				int num = 100;
				if (this.Effects == SpriteEffects.FlipHorizontally)
				{
					this.Position.X = virtualCamera.Position.X + virtualCamera.Size.X + (float)num;
				}
				else
				{
					this.Position.X = virtualCamera.Position.X - (float)num;
				}
				this.Position.Y = random.NextFloat() * ((float)Main.worldSurface * 16f - 1600f - 4000f) + 4000f;
				this.Depth = random.NextFloat() * 3f + 3f;
				base.SetPositionInWorldBasedOnScreenSpace(this.Position);
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/SlimeBalloons", 1);
				this.Frame = new SpriteFrame(1, 7);
				this.Frame.CurrentRow = (byte)random.Next(7);
				this.LifeTime = random.Next(60, 121) * 60;
				this.OpacityNormalizedTimeToFadeIn = 0.025f;
				this.OpacityNormalizedTimeToFadeOut = 0.975f;
				this.BrightnessLerper = 0.2f;
				this.FinalOpacityMultiplier = 1f;
				this.FramingSpeed = int.MaxValue;
			}

			// Token: 0x06004898 RID: 18584 RVA: 0x006CE8E0 File Offset: 0x006CCAE0
			public override void UpdateVelocity(int frameCount)
			{
				Vector2 vector = this._magnetAccelerations * new Vector2((float)Math.Sign(this._magnetPointTarget.X - this._positionVsMagnet.X), (float)Math.Sign(this._magnetPointTarget.Y - this._positionVsMagnet.Y));
				this._velocityVsMagnet += vector;
				this._positionVsMagnet += this._velocityVsMagnet;
				float num = (1f + Math.Abs(Main.WindForVisuals) * 1f) * (float)((this.Effects == SpriteEffects.FlipHorizontally) ? (-1) : 1);
				this.Velocity = new Vector2(num, -0.01f) + this._velocityVsMagnet;
				this.Rotation = this.Velocity.X * 0.1f;
			}

			// Token: 0x06004899 RID: 18585 RVA: 0x006CE9BC File Offset: 0x006CCBBC
			public override void Update(int frameCount)
			{
				base.Update(frameCount);
				this.Effects = ((Main.WindForVisuals > 0f) ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
				if (!Main.IsItAHappyWindyDay || Main.IsItRaining || !Main.dayTime || Main.eclipse)
				{
					base.StartFadingOut(frameCount);
				}
			}

			// Token: 0x0600489A RID: 18586 RVA: 0x006CEA09 File Offset: 0x006CCC09
			public void SetMagnetization(Vector2 accelerations, Vector2 targetOffset)
			{
				this._magnetAccelerations = accelerations;
				this._magnetPointTarget = targetOffset;
			}

			// Token: 0x0600489B RID: 18587 RVA: 0x006CEA1C File Offset: 0x006CCC1C
			public static List<AmbientSky.SlimeBalloonGroupSkyEntity> CreateGroup(Player player, FastRandom random)
			{
				List<AmbientSky.SlimeBalloonGroupSkyEntity> list = new List<AmbientSky.SlimeBalloonGroupSkyEntity>();
				int num = 100;
				int num2 = random.Next(5, 10);
				VirtualCamera virtualCamera = new VirtualCamera(player);
				SpriteEffects spriteEffects = ((Main.WindForVisuals > 0f) ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
				Vector2 vector = default(Vector2);
				if (spriteEffects == SpriteEffects.FlipHorizontally)
				{
					vector.X = virtualCamera.Position.X + virtualCamera.Size.X + (float)num;
				}
				else
				{
					vector.X = virtualCamera.Position.X - (float)num;
				}
				vector.Y = random.NextFloat() * ((float)Main.worldSurface * 16f - 1600f - 2400f) + 2400f;
				float num3 = random.NextFloat() * 3f + 3f;
				int num4 = random.Next(80, 121) * 60;
				Vector2 vector2 = new Vector2(random.NextFloat() * 0.1f + 0.1f, random.NextFloat() * 0.1f + 0.1f);
				Vector2 vector3 = new Vector2(random.NextFloat() * 2f - 1f, random.NextFloat() * 2f - 1f) * 150f;
				for (int i = 0; i < num2; i++)
				{
					AmbientSky.SlimeBalloonGroupSkyEntity slimeBalloonGroupSkyEntity = new AmbientSky.SlimeBalloonGroupSkyEntity(player, random);
					slimeBalloonGroupSkyEntity.Depth = num3 + random.NextFloat() * 0.5f;
					slimeBalloonGroupSkyEntity.Position = vector + new Vector2(random.NextFloat() * 20f - 10f, random.NextFloat() * 3f) * 80f;
					slimeBalloonGroupSkyEntity.Effects = spriteEffects;
					slimeBalloonGroupSkyEntity.SetPositionInWorldBasedOnScreenSpace(slimeBalloonGroupSkyEntity.Position);
					slimeBalloonGroupSkyEntity.LifeTime = num4 + random.Next(301);
					slimeBalloonGroupSkyEntity.SetMagnetization(vector2 * (random.NextFloat() * 0.2f) * 0.05f, vector3);
					list.Add(slimeBalloonGroupSkyEntity);
				}
				return list;
			}

			// Token: 0x04007587 RID: 30087
			private Vector2 _magnetAccelerations;

			// Token: 0x04007588 RID: 30088
			private Vector2 _magnetPointTarget;

			// Token: 0x04007589 RID: 30089
			private Vector2 _positionVsMagnet;

			// Token: 0x0400758A RID: 30090
			private Vector2 _velocityVsMagnet;
		}

		// Token: 0x02000955 RID: 2389
		private class HellBatsGoupSkyEntity : AmbientSky.FadingSkyEntity
		{
			// Token: 0x0600489C RID: 18588 RVA: 0x006CEC28 File Offset: 0x006CCE28
			public HellBatsGoupSkyEntity(Player player, FastRandom random)
			{
				VirtualCamera virtualCamera = new VirtualCamera(player);
				this.Effects = ((Main.WindForVisuals > 0f) ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
				int num = 100;
				if (this.Effects == SpriteEffects.FlipHorizontally)
				{
					this.Position.X = virtualCamera.Position.X + virtualCamera.Size.X + (float)num;
				}
				else
				{
					this.Position.X = virtualCamera.Position.X - (float)num;
				}
				this.Position.Y = random.NextFloat() * 400f + (float)(Main.UnderworldLayer * 16);
				this.Depth = random.NextFloat() * 5f + 3f;
				base.SetPositionInWorldBasedOnScreenSpace(this.Position);
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/HellBat" + random.Next(1, 3), 1);
				this.Frame = new SpriteFrame(1, 10);
				this.LifeTime = random.Next(60, 121) * 60;
				this.OpacityNormalizedTimeToFadeIn = 0.1f;
				this.OpacityNormalizedTimeToFadeOut = 0.9f;
				this.BrightnessLerper = 0.2f;
				this.FinalOpacityMultiplier = 1f;
				this.FramingSpeed = 4;
				this.FrameOffset = random.Next(0, (int)this.Frame.RowCount);
				int num2 = random.Next((int)this.Frame.RowCount);
				for (int i = 0; i < num2; i++)
				{
					base.NextFrame();
				}
			}

			// Token: 0x0600489D RID: 18589 RVA: 0x006CEDAC File Offset: 0x006CCFAC
			public override void UpdateVelocity(int frameCount)
			{
				Vector2 vector = this._magnetAccelerations * new Vector2((float)Math.Sign(this._magnetPointTarget.X - this._positionVsMagnet.X), (float)Math.Sign(this._magnetPointTarget.Y - this._positionVsMagnet.Y));
				this._velocityVsMagnet += vector;
				this._positionVsMagnet += this._velocityVsMagnet;
				float num = (3f + Math.Abs(Main.WindForVisuals) * 0.8f) * (float)((this.Effects == SpriteEffects.FlipHorizontally) ? (-1) : 1);
				this.Velocity = new Vector2(num, 0f) + this._velocityVsMagnet;
			}

			// Token: 0x0600489E RID: 18590 RVA: 0x006CEE6F File Offset: 0x006CD06F
			public override void Update(int frameCount)
			{
				base.Update(frameCount);
			}

			// Token: 0x0600489F RID: 18591 RVA: 0x006CEE78 File Offset: 0x006CD078
			public void SetMagnetization(Vector2 accelerations, Vector2 targetOffset)
			{
				this._magnetAccelerations = accelerations;
				this._magnetPointTarget = targetOffset;
			}

			// Token: 0x060048A0 RID: 18592 RVA: 0x006CEE88 File Offset: 0x006CD088
			public override Color GetColor(Color backgroundColor)
			{
				return Color.Lerp(Color.White, Color.Gray, this.Depth / 15f) * this.Opacity * this.FinalOpacityMultiplier * this.Helper_GetOpacityWithAccountingForBackgroundsOff();
			}

			// Token: 0x060048A1 RID: 18593 RVA: 0x006CEEC8 File Offset: 0x006CD0C8
			public static List<AmbientSky.HellBatsGoupSkyEntity> CreateGroup(Player player, FastRandom random)
			{
				List<AmbientSky.HellBatsGoupSkyEntity> list = new List<AmbientSky.HellBatsGoupSkyEntity>();
				int num = 100;
				int num2 = random.Next(20, 40);
				VirtualCamera virtualCamera = new VirtualCamera(player);
				SpriteEffects spriteEffects = ((Main.WindForVisuals > 0f) ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
				Vector2 vector = default(Vector2);
				if (spriteEffects == SpriteEffects.FlipHorizontally)
				{
					vector.X = virtualCamera.Position.X + virtualCamera.Size.X + (float)num;
				}
				else
				{
					vector.X = virtualCamera.Position.X - (float)num;
				}
				vector.Y = random.NextFloat() * 800f + (float)(Main.UnderworldLayer * 16);
				float num3 = random.NextFloat() * 5f + 3f;
				int num4 = random.Next(30, 61) * 60;
				Vector2 vector2 = new Vector2(random.NextFloat() * 0.5f + 0.5f, random.NextFloat() * 0.5f + 0.5f);
				Vector2 vector3 = new Vector2(random.NextFloat() * 2f - 1f, random.NextFloat() * 2f - 1f) * 100f;
				for (int i = 0; i < num2; i++)
				{
					AmbientSky.HellBatsGoupSkyEntity hellBatsGoupSkyEntity = new AmbientSky.HellBatsGoupSkyEntity(player, random);
					hellBatsGoupSkyEntity.Depth = num3 + random.NextFloat() * 0.5f;
					hellBatsGoupSkyEntity.Position = vector + new Vector2(random.NextFloat() * 20f - 10f, random.NextFloat() * 3f) * 50f;
					hellBatsGoupSkyEntity.Effects = spriteEffects;
					hellBatsGoupSkyEntity.SetPositionInWorldBasedOnScreenSpace(hellBatsGoupSkyEntity.Position);
					hellBatsGoupSkyEntity.LifeTime = num4 + random.Next(301);
					hellBatsGoupSkyEntity.SetMagnetization(vector2 * (random.NextFloat() * 0.3f + 0.85f) * 0.05f, vector3);
					list.Add(hellBatsGoupSkyEntity);
				}
				return list;
			}

			// Token: 0x060048A2 RID: 18594 RVA: 0x006CF0C9 File Offset: 0x006CD2C9
			internal float Helper_GetOpacityWithAccountingForBackgroundsOff()
			{
				if (Main.netMode == 2 || Main.BackgroundEnabled)
				{
					return 1f;
				}
				return 0f;
			}

			// Token: 0x0400758B RID: 30091
			private Vector2 _magnetAccelerations;

			// Token: 0x0400758C RID: 30092
			private Vector2 _magnetPointTarget;

			// Token: 0x0400758D RID: 30093
			private Vector2 _positionVsMagnet;

			// Token: 0x0400758E RID: 30094
			private Vector2 _velocityVsMagnet;
		}

		// Token: 0x02000956 RID: 2390
		private class BatsGroupSkyEntity : AmbientSky.FadingSkyEntity
		{
			// Token: 0x060048A3 RID: 18595 RVA: 0x006CF0E8 File Offset: 0x006CD2E8
			public BatsGroupSkyEntity(Player player, FastRandom random)
			{
				VirtualCamera virtualCamera = new VirtualCamera(player);
				this.Effects = ((Main.WindForVisuals > 0f) ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
				int num = 100;
				if (this.Effects == SpriteEffects.FlipHorizontally)
				{
					this.Position.X = virtualCamera.Position.X + virtualCamera.Size.X + (float)num;
				}
				else
				{
					this.Position.X = virtualCamera.Position.X - (float)num;
				}
				this.Position.Y = random.NextFloat() * ((float)Main.worldSurface * 16f - 1600f - 2400f) + 2400f;
				this.Depth = random.NextFloat() * 3f + 3f;
				base.SetPositionInWorldBasedOnScreenSpace(this.Position);
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/Bat" + random.Next(1, 4), 1);
				this.Frame = new SpriteFrame(1, 10);
				this.LifeTime = random.Next(60, 121) * 60;
				this.OpacityNormalizedTimeToFadeIn = 0.1f;
				this.OpacityNormalizedTimeToFadeOut = 0.9f;
				this.BrightnessLerper = 0.2f;
				this.FinalOpacityMultiplier = 1f;
				this.FramingSpeed = 4;
				this.FrameOffset = random.Next(0, (int)this.Frame.RowCount);
				int num2 = random.Next((int)this.Frame.RowCount);
				for (int i = 0; i < num2; i++)
				{
					base.NextFrame();
				}
			}

			// Token: 0x060048A4 RID: 18596 RVA: 0x006CF27C File Offset: 0x006CD47C
			public override void UpdateVelocity(int frameCount)
			{
				Vector2 vector = this._magnetAccelerations * new Vector2((float)Math.Sign(this._magnetPointTarget.X - this._positionVsMagnet.X), (float)Math.Sign(this._magnetPointTarget.Y - this._positionVsMagnet.Y));
				this._velocityVsMagnet += vector;
				this._positionVsMagnet += this._velocityVsMagnet;
				float num = (3f + Math.Abs(Main.WindForVisuals) * 0.8f) * (float)((this.Effects == SpriteEffects.FlipHorizontally) ? (-1) : 1);
				this.Velocity = new Vector2(num, 0f) + this._velocityVsMagnet;
			}

			// Token: 0x060048A5 RID: 18597 RVA: 0x006CD530 File Offset: 0x006CB730
			public override void Update(int frameCount)
			{
				base.Update(frameCount);
				if (Main.IsItRaining || !Main.dayTime || Main.eclipse)
				{
					base.StartFadingOut(frameCount);
				}
			}

			// Token: 0x060048A6 RID: 18598 RVA: 0x006CF33F File Offset: 0x006CD53F
			public void SetMagnetization(Vector2 accelerations, Vector2 targetOffset)
			{
				this._magnetAccelerations = accelerations;
				this._magnetPointTarget = targetOffset;
			}

			// Token: 0x060048A7 RID: 18599 RVA: 0x006CF350 File Offset: 0x006CD550
			public override Color GetColor(Color backgroundColor)
			{
				return base.GetColor(backgroundColor) * Utils.Max<float>(new float[]
				{
					Main.bgAlphaFrontLayer[3],
					Main.bgAlphaFrontLayer[0],
					Main.bgAlphaFrontLayer[10],
					Main.bgAlphaFrontLayer[11],
					Main.bgAlphaFrontLayer[12]
				});
			}

			// Token: 0x060048A8 RID: 18600 RVA: 0x006CF3AC File Offset: 0x006CD5AC
			public static List<AmbientSky.BatsGroupSkyEntity> CreateGroup(Player player, FastRandom random)
			{
				List<AmbientSky.BatsGroupSkyEntity> list = new List<AmbientSky.BatsGroupSkyEntity>();
				int num = 100;
				int num2 = random.Next(20, 40);
				VirtualCamera virtualCamera = new VirtualCamera(player);
				SpriteEffects spriteEffects = ((Main.WindForVisuals > 0f) ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
				Vector2 vector = default(Vector2);
				if (spriteEffects == SpriteEffects.FlipHorizontally)
				{
					vector.X = virtualCamera.Position.X + virtualCamera.Size.X + (float)num;
				}
				else
				{
					vector.X = virtualCamera.Position.X - (float)num;
				}
				vector.Y = random.NextFloat() * ((float)Main.worldSurface * 16f - 1600f - 2400f) + 2400f;
				float num3 = random.NextFloat() * 3f + 3f;
				int num4 = random.Next(30, 61) * 60;
				Vector2 vector2 = new Vector2(random.NextFloat() * 0.5f + 0.5f, random.NextFloat() * 0.5f + 0.5f);
				Vector2 vector3 = new Vector2(random.NextFloat() * 2f - 1f, random.NextFloat() * 2f - 1f) * 100f;
				for (int i = 0; i < num2; i++)
				{
					AmbientSky.BatsGroupSkyEntity batsGroupSkyEntity = new AmbientSky.BatsGroupSkyEntity(player, random);
					batsGroupSkyEntity.Depth = num3 + random.NextFloat() * 0.5f;
					batsGroupSkyEntity.Position = vector + new Vector2(random.NextFloat() * 20f - 10f, random.NextFloat() * 3f) * 50f;
					batsGroupSkyEntity.Effects = spriteEffects;
					batsGroupSkyEntity.SetPositionInWorldBasedOnScreenSpace(batsGroupSkyEntity.Position);
					batsGroupSkyEntity.LifeTime = num4 + random.Next(301);
					batsGroupSkyEntity.SetMagnetization(vector2 * (random.NextFloat() * 0.3f + 0.85f) * 0.05f, vector3);
					list.Add(batsGroupSkyEntity);
				}
				return list;
			}

			// Token: 0x0400758F RID: 30095
			private Vector2 _magnetAccelerations;

			// Token: 0x04007590 RID: 30096
			private Vector2 _magnetPointTarget;

			// Token: 0x04007591 RID: 30097
			private Vector2 _positionVsMagnet;

			// Token: 0x04007592 RID: 30098
			private Vector2 _velocityVsMagnet;
		}

		// Token: 0x02000957 RID: 2391
		private class WyvernSkyEntity : AmbientSky.FadingSkyEntity
		{
			// Token: 0x060048A9 RID: 18601 RVA: 0x006CF5BC File Offset: 0x006CD7BC
			public WyvernSkyEntity(Player player, FastRandom random)
			{
				VirtualCamera virtualCamera = new VirtualCamera(player);
				this.Effects = ((Main.WindForVisuals > 0f) ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
				int num = 100;
				if (this.Effects == SpriteEffects.FlipHorizontally)
				{
					this.Position.X = virtualCamera.Position.X + virtualCamera.Size.X + (float)num;
				}
				else
				{
					this.Position.X = virtualCamera.Position.X - (float)num;
				}
				this.Position.Y = random.NextFloat() * ((float)Main.worldSurface * 16f - 1600f - 2400f) + 2400f;
				this.Depth = random.NextFloat() * 3f + 3f;
				base.SetPositionInWorldBasedOnScreenSpace(this.Position);
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/Wyvern", 1);
				this.Frame = new SpriteFrame(1, 5);
				this.LifeTime = random.Next(40, 71) * 60;
				this.OpacityNormalizedTimeToFadeIn = 0.15f;
				this.OpacityNormalizedTimeToFadeOut = 0.85f;
				this.BrightnessLerper = 0.2f;
				this.FinalOpacityMultiplier = 1f;
				this.FramingSpeed = 4;
			}

			// Token: 0x060048AA RID: 18602 RVA: 0x006CF6FC File Offset: 0x006CD8FC
			public override void UpdateVelocity(int frameCount)
			{
				float num = 3f + Math.Abs(Main.WindForVisuals) * 0.8f;
				this.Velocity = new Vector2(num * (float)((this.Effects == SpriteEffects.FlipHorizontally) ? (-1) : 1), 0f);
			}
		}

		// Token: 0x02000958 RID: 2392
		private class NormalizedBackgroundLayerSpaceSkyEntity : AmbientSky.SkyEntity
		{
			// Token: 0x060048AB RID: 18603 RVA: 0x006CF740 File Offset: 0x006CD940
			public override Color GetColor(Color backgroundColor)
			{
				return Color.Lerp(backgroundColor, Color.White, 0.3f);
			}

			// Token: 0x060048AC RID: 18604 RVA: 0x006CD385 File Offset: 0x006CB585
			public override Vector2 GetDrawPosition()
			{
				return this.Position;
			}

			// Token: 0x060048AD RID: 18605 RVA: 0x00009E46 File Offset: 0x00008046
			public override void Update(int frameCount)
			{
			}

			// Token: 0x060048AE RID: 18606 RVA: 0x006CF752 File Offset: 0x006CD952
			public NormalizedBackgroundLayerSpaceSkyEntity()
			{
			}
		}

		// Token: 0x02000959 RID: 2393
		private class BoneSerpentSkyEntity : AmbientSky.NormalizedBackgroundLayerSpaceSkyEntity
		{
			// Token: 0x060048AF RID: 18607 RVA: 0x006CF75A File Offset: 0x006CD95A
			public BoneSerpentSkyEntity()
			{
			}
		}

		// Token: 0x0200095A RID: 2394
		private class AirshipSkyEntity : AmbientSky.FadingSkyEntity
		{
			// Token: 0x060048B0 RID: 18608 RVA: 0x006CF764 File Offset: 0x006CD964
			public AirshipSkyEntity(Player player, FastRandom random)
			{
				VirtualCamera virtualCamera = new VirtualCamera(player);
				this.Effects = ((random.Next(2) == 0) ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
				int num = 100;
				if (this.Effects == SpriteEffects.FlipHorizontally)
				{
					this.Position.X = virtualCamera.Position.X + virtualCamera.Size.X + (float)num;
				}
				else
				{
					this.Position.X = virtualCamera.Position.X - (float)num;
				}
				this.Position.Y = random.NextFloat() * ((float)Main.worldSurface * 16f - 1600f - 2400f) + 2400f;
				this.Depth = random.NextFloat() * 3f + 3f;
				base.SetPositionInWorldBasedOnScreenSpace(this.Position);
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/FlyingShip", 1);
				this.Frame = new SpriteFrame(1, 4);
				this.LifeTime = random.Next(40, 71) * 60;
				this.OpacityNormalizedTimeToFadeIn = 0.05f;
				this.OpacityNormalizedTimeToFadeOut = 0.95f;
				this.BrightnessLerper = 0.2f;
				this.FinalOpacityMultiplier = 1f;
				this.FramingSpeed = 4;
			}

			// Token: 0x060048B1 RID: 18609 RVA: 0x006CF8A4 File Offset: 0x006CDAA4
			public override void UpdateVelocity(int frameCount)
			{
				float num = 6f + Math.Abs(Main.WindForVisuals) * 1.6f;
				this.Velocity = new Vector2(num * (float)((this.Effects == SpriteEffects.FlipHorizontally) ? (-1) : 1), 0f);
			}

			// Token: 0x060048B2 RID: 18610 RVA: 0x006CD530 File Offset: 0x006CB730
			public override void Update(int frameCount)
			{
				base.Update(frameCount);
				if (Main.IsItRaining || !Main.dayTime || Main.eclipse)
				{
					base.StartFadingOut(frameCount);
				}
			}
		}

		// Token: 0x0200095B RID: 2395
		private class AirBalloonSkyEntity : AmbientSky.FadingSkyEntity
		{
			// Token: 0x060048B3 RID: 18611 RVA: 0x006CF8E8 File Offset: 0x006CDAE8
			public AirBalloonSkyEntity(Player player, FastRandom random)
			{
				new VirtualCamera(player);
				int x = player.Center.ToTileCoordinates().X;
				this.Effects = ((random.Next(2) == 0) ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
				this.Position.X = ((float)x + 100f * (random.NextFloat() * 2f - 1f)) * 16f;
				this.Position.Y = (float)Main.worldSurface * 16f - (float)random.Next(50, 81) * 16f;
				this.Depth = random.NextFloat() * 3f + 3f;
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/AirBalloons_" + ((random.Next(2) == 0) ? "Large" : "Small"), 1);
				this.Frame = new SpriteFrame(1, 5);
				this.Frame.CurrentRow = (byte)random.Next(5);
				this.LifeTime = random.Next(20, 51) * 60;
				this.OpacityNormalizedTimeToFadeIn = 0.05f;
				this.OpacityNormalizedTimeToFadeOut = 0.95f;
				this.BrightnessLerper = 0.2f;
				this.FinalOpacityMultiplier = 1f;
				this.FramingSpeed = int.MaxValue;
			}

			// Token: 0x060048B4 RID: 18612 RVA: 0x006CFA34 File Offset: 0x006CDC34
			public override void UpdateVelocity(int frameCount)
			{
				float num = Main.WindForVisuals * 4f;
				float num2 = 3f + Math.Abs(Main.WindForVisuals) * 1f;
				if ((double)this.Position.Y < Main.worldSurface * 12.0)
				{
					num2 *= 0.5f;
				}
				if ((double)this.Position.Y < Main.worldSurface * 8.0)
				{
					num2 *= 0.5f;
				}
				if ((double)this.Position.Y < Main.worldSurface * 4.0)
				{
					num2 *= 0.5f;
				}
				this.Velocity = new Vector2(num, -num2);
			}

			// Token: 0x060048B5 RID: 18613 RVA: 0x006CD530 File Offset: 0x006CB730
			public override void Update(int frameCount)
			{
				base.Update(frameCount);
				if (Main.IsItRaining || !Main.dayTime || Main.eclipse)
				{
					base.StartFadingOut(frameCount);
				}
			}

			// Token: 0x04007593 RID: 30099
			private const int RANDOM_TILE_SPAWN_RANGE = 100;
		}

		// Token: 0x0200095C RID: 2396
		private class CrimeraSkyEntity : AmbientSky.EOCSkyEntity
		{
			// Token: 0x060048B6 RID: 18614 RVA: 0x006CFAE4 File Offset: 0x006CDCE4
			public CrimeraSkyEntity(Player player, FastRandom random)
				: base(player, random)
			{
				int num = 3;
				if (this.Depth <= 6f)
				{
					num = 2;
				}
				if (this.Depth <= 5f)
				{
					num = 1;
				}
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/Crimera" + num, 1);
				this.Frame = new SpriteFrame(1, 3);
			}

			// Token: 0x060048B7 RID: 18615 RVA: 0x006CFB47 File Offset: 0x006CDD47
			public override Color GetColor(Color backgroundColor)
			{
				return base.GetColor(backgroundColor) * Main.bgAlphaFrontLayer[8];
			}
		}

		// Token: 0x0200095D RID: 2397
		private class EOSSkyEntity : AmbientSky.EOCSkyEntity
		{
			// Token: 0x060048B8 RID: 18616 RVA: 0x006CFB5C File Offset: 0x006CDD5C
			public EOSSkyEntity(Player player, FastRandom random)
				: base(player, random)
			{
				int num = 3;
				if (this.Depth <= 6f)
				{
					num = 2;
				}
				if (this.Depth <= 5f)
				{
					num = 1;
				}
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/EOS" + num, 1);
				this.Frame = new SpriteFrame(1, 4);
			}

			// Token: 0x060048B9 RID: 18617 RVA: 0x006CFBBF File Offset: 0x006CDDBF
			public override Color GetColor(Color backgroundColor)
			{
				return base.GetColor(backgroundColor) * Main.bgAlphaFrontLayer[1];
			}
		}

		// Token: 0x0200095E RID: 2398
		private class EOCSkyEntity : AmbientSky.FadingSkyEntity
		{
			// Token: 0x060048BA RID: 18618 RVA: 0x006CFBD4 File Offset: 0x006CDDD4
			public EOCSkyEntity(Player player, FastRandom random)
			{
				VirtualCamera virtualCamera = new VirtualCamera(player);
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/EOC", 1);
				this.Frame = new SpriteFrame(1, 3);
				this.Depth = random.NextFloat() * 3f + 4.5f;
				if (random.Next(4) != 0)
				{
					this.BeginZigZag(ref random, virtualCamera, (random.Next(2) == 1) ? 1 : (-1));
				}
				else
				{
					this.BeginChasingPlayer(ref random, virtualCamera);
				}
				base.SetPositionInWorldBasedOnScreenSpace(this.Position);
				this.OpacityNormalizedTimeToFadeIn = 0.1f;
				this.OpacityNormalizedTimeToFadeOut = 0.9f;
				this.BrightnessLerper = 0.2f;
				this.FinalOpacityMultiplier = 1f;
				this.FramingSpeed = 5;
			}

			// Token: 0x060048BB RID: 18619 RVA: 0x006CFC98 File Offset: 0x006CDE98
			private void BeginZigZag(ref FastRandom random, VirtualCamera camera, int direction)
			{
				this._state = 1;
				this.LifeTime = random.Next(18, 31) * 60;
				this._direction = direction;
				this._waviness = random.NextFloat() * 1f + 1f;
				this.Position.Y = camera.Position.Y;
				int num = 100;
				if (this._direction == 1)
				{
					this.Position.X = camera.Position.X - (float)num;
					return;
				}
				this.Position.X = camera.Position.X + camera.Size.X + (float)num;
			}

			// Token: 0x060048BC RID: 18620 RVA: 0x006CFD44 File Offset: 0x006CDF44
			private void BeginChasingPlayer(ref FastRandom random, VirtualCamera camera)
			{
				this._state = 2;
				this.LifeTime = random.Next(18, 31) * 60;
				this.Position = camera.Position + camera.Size * new Vector2(random.NextFloat(), random.NextFloat());
			}

			// Token: 0x060048BD RID: 18621 RVA: 0x006CFD9C File Offset: 0x006CDF9C
			public override void UpdateVelocity(int frameCount)
			{
				int state = this._state;
				if (state != 1)
				{
					if (state == 2)
					{
						this.ChasePlayerTop(frameCount);
					}
				}
				else
				{
					this.ZigzagMove(frameCount);
				}
				this.Rotation = this.Velocity.ToRotation();
			}

			// Token: 0x060048BE RID: 18622 RVA: 0x006CFDDB File Offset: 0x006CDFDB
			private void ZigzagMove(int frameCount)
			{
				this.Velocity = new Vector2((float)(this._direction * 3), (float)Math.Cos((double)((float)frameCount / 1200f * 6.2831855f)) * this._waviness);
			}

			// Token: 0x060048BF RID: 18623 RVA: 0x006CFE10 File Offset: 0x006CE010
			private void ChasePlayerTop(int frameCount)
			{
				Vector2 vector = Main.LocalPlayer.Center + new Vector2(0f, -500f) - this.Position;
				if (vector.Length() >= 100f)
				{
					this.Velocity.X = this.Velocity.X + 0.1f * (float)Math.Sign(vector.X);
					this.Velocity.Y = this.Velocity.Y + 0.1f * (float)Math.Sign(vector.Y);
					this.Velocity = Vector2.Clamp(this.Velocity, new Vector2(-18f), new Vector2(18f));
				}
			}

			// Token: 0x04007594 RID: 30100
			private const int STATE_ZIGZAG = 1;

			// Token: 0x04007595 RID: 30101
			private const int STATE_GOOVERPLAYER = 2;

			// Token: 0x04007596 RID: 30102
			private int _state;

			// Token: 0x04007597 RID: 30103
			private int _direction;

			// Token: 0x04007598 RID: 30104
			private float _waviness;
		}

		// Token: 0x0200095F RID: 2399
		private class MeteorSkyEntity : AmbientSky.FadingSkyEntity
		{
			// Token: 0x060048C0 RID: 18624 RVA: 0x006CFEBC File Offset: 0x006CE0BC
			public MeteorSkyEntity(Player player, FastRandom random)
			{
				new VirtualCamera(player);
				this.Effects = ((random.Next(2) == 0) ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
				this.Depth = random.NextFloat() * 3f + 3f;
				this.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/Meteor", 1);
				this.Frame = new SpriteFrame(1, 4);
				Vector2 vector = (0.7853982f + random.NextFloat() * 1.5707964f).ToRotationVector2();
				float num = (float)(Main.worldSurface * 16.0 - 0.0) / vector.Y;
				float num2 = 1200f;
				float num3 = num / num2;
				Vector2 vector2 = vector * num3;
				this.Velocity = vector2;
				int num4 = 100;
				Vector2 vector3 = player.Center + new Vector2((float)random.Next(-num4, num4 + 1), (float)random.Next(-num4, num4 + 1)) - this.Velocity * num2 * 0.5f;
				this.Position = vector3;
				this.LifeTime = (int)num2;
				this.OpacityNormalizedTimeToFadeIn = 0.05f;
				this.OpacityNormalizedTimeToFadeOut = 0.95f;
				this.BrightnessLerper = 0.5f;
				this.FinalOpacityMultiplier = 1f;
				this.FramingSpeed = 5;
				this.Rotation = this.Velocity.ToRotation() + 1.5707964f;
			}
		}

		// Token: 0x02000960 RID: 2400
		// (Invoke) Token: 0x060048C2 RID: 18626
		private delegate AmbientSky.SkyEntity EntityFactoryMethod(Player player, int seed);
	}
}
