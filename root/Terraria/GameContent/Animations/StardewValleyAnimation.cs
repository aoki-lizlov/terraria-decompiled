using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;

namespace Terraria.GameContent.Animations
{
	// Token: 0x0200052C RID: 1324
	public class StardewValleyAnimation
	{
		// Token: 0x060036EE RID: 14062 RVA: 0x0062C47F File Offset: 0x0062A67F
		public StardewValleyAnimation()
		{
			this.ComposeAnimation();
		}

		// Token: 0x060036EF RID: 14063 RVA: 0x0062C498 File Offset: 0x0062A698
		private void ComposeAnimation()
		{
			Asset<Texture2D> asset = TextureAssets.Extra[247];
			Rectangle rectangle = asset.Frame(1, 1, 0, 0, 0, 0);
			DrawData drawData = new DrawData(asset.Value, Vector2.Zero, new Rectangle?(rectangle), Color.White, 0f, rectangle.Size() * new Vector2(0.5f, 0.5f), 1f, SpriteEffects.None, 0f);
			int num = 128;
			int num2 = 60;
			int num3 = 360;
			int num4 = 60;
			int num5 = 4;
			Segments.AnimationSegmentWithActions<Segments.LooseSprite> animationSegmentWithActions = new Segments.SpriteSegment(asset, num, drawData, Vector2.Zero).UseShaderEffect(new Segments.SpriteSegment.MaskedFadeEffect(new Segments.SpriteSegment.MaskedFadeEffect.GetMatrixAction(this.GetMatrixForAnimation), "StardewValleyFade", 8, num5).WithPanX(new Segments.Panning
			{
				Delay = 128f,
				Duration = (float)(num3 - 120 + num2 - 60),
				AmountOverTime = 0.55f,
				StartAmount = -0.4f
			}).WithPanY(new Segments.Panning
			{
				StartAmount = 0f
			})).Then(new Actions.Sprites.OutCircleScale(Vector2.Zero)).With(new Actions.Sprites.OutCircleScale(Vector2.One, num2))
				.Then(new Actions.Sprites.Wait(num3))
				.Then(new Actions.Sprites.OutCircleScale(Vector2.Zero, num4));
			this._segments.Add(animationSegmentWithActions);
			Asset<Texture2D> asset2 = TextureAssets.Extra[249];
			Rectangle rectangle2 = asset2.Frame(1, 8, 0, 0, 0, 0);
			DrawData drawData2 = new DrawData(asset2.Value, Vector2.Zero, new Rectangle?(rectangle2), Color.White, 0f, rectangle2.Size() * new Vector2(0.5f, 0.5f), 1f, SpriteEffects.None, 0f);
			Segments.AnimationSegmentWithActions<Segments.LooseSprite> animationSegmentWithActions2 = new Segments.SpriteSegment(asset2, num, drawData2, Vector2.Zero).Then(new Actions.Sprites.OutCircleScale(Vector2.Zero)).With(new Actions.Sprites.OutCircleScale(Vector2.One, num2)).With(new Actions.Sprites.SetFrameSequence(100, new Point[]
			{
				new Point(0, 0),
				new Point(0, 1),
				new Point(0, 2),
				new Point(0, 3),
				new Point(0, 4),
				new Point(0, 5),
				new Point(0, 6),
				new Point(0, 7)
			}, num5, 0, 0))
				.Then(new Actions.Sprites.Wait(num3))
				.Then(new Actions.Sprites.OutCircleScale(Vector2.Zero, num4));
			this._segments.Add(animationSegmentWithActions2);
		}

		// Token: 0x060036F0 RID: 14064 RVA: 0x0062C73C File Offset: 0x0062A93C
		private Matrix GetMatrixForAnimation()
		{
			return Main.Transform;
		}

		// Token: 0x060036F1 RID: 14065 RVA: 0x0062C744 File Offset: 0x0062A944
		public void Draw(SpriteBatch spriteBatch, int timeInAnimation, Vector2 positionInScreen)
		{
			GameAnimationSegment gameAnimationSegment = new GameAnimationSegment
			{
				SpriteBatch = spriteBatch,
				AnchorPositionOnScreen = positionInScreen,
				TimeInAnimation = timeInAnimation,
				DisplayOpacity = 1f
			};
			for (int i = 0; i < this._segments.Count; i++)
			{
				this._segments[i].Draw(ref gameAnimationSegment);
			}
		}

		// Token: 0x04005B5F RID: 23391
		private List<IAnimationSegment> _segments = new List<IAnimationSegment>();
	}
}
