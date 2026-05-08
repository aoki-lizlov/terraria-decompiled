using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.GameContent.UI;
using Terraria.Graphics.Shaders;
using Terraria.Localization;
using Terraria.UI.Chat;

namespace Terraria.GameContent.Animations
{
	// Token: 0x0200052B RID: 1323
	public class Segments
	{
		// Token: 0x060036ED RID: 14061 RVA: 0x0000357B File Offset: 0x0000177B
		public Segments()
		{
		}

		// Token: 0x04005B5E RID: 23390
		private const float PixelsToRollUpPerFrame = 0.5f;

		// Token: 0x020009A2 RID: 2466
		public class LocalizedTextSegment : IAnimationSegment
		{
			// Token: 0x17000598 RID: 1432
			// (get) Token: 0x060049B2 RID: 18866 RVA: 0x006D2B3B File Offset: 0x006D0D3B
			public float DedicatedTimeNeeded
			{
				get
				{
					return 240f;
				}
			}

			// Token: 0x060049B3 RID: 18867 RVA: 0x006D2B42 File Offset: 0x006D0D42
			public LocalizedTextSegment(float timeInAnimation, string textKey)
			{
				this._text = Language.GetText(textKey);
				this._timeToShowPeak = timeInAnimation;
			}

			// Token: 0x060049B4 RID: 18868 RVA: 0x006D2B5D File Offset: 0x006D0D5D
			public LocalizedTextSegment(float timeInAnimation, LocalizedText textObject, Vector2 anchorOffset)
			{
				this._text = textObject;
				this._timeToShowPeak = timeInAnimation;
				this._anchorOffset = anchorOffset;
			}

			// Token: 0x060049B5 RID: 18869 RVA: 0x006D2B7C File Offset: 0x006D0D7C
			public void Draw(ref GameAnimationSegment info)
			{
				float num = 250f;
				float num2 = 250f;
				int timeInAnimation = info.TimeInAnimation;
				float num3 = Utils.GetLerpValue(this._timeToShowPeak - num, this._timeToShowPeak, (float)timeInAnimation, true) * Utils.GetLerpValue(this._timeToShowPeak + num2, this._timeToShowPeak, (float)timeInAnimation, true);
				if (num3 <= 0f)
				{
					return;
				}
				float num4 = this._timeToShowPeak - (float)timeInAnimation;
				Vector2 vector = info.AnchorPositionOnScreen + new Vector2(0f, num4 * 0.5f);
				vector += this._anchorOffset;
				Vector2 vector2 = new Vector2(0.7f);
				float num5 = Main.GlobalTimeWrappedHourly * 0.02f % 1f;
				if (num5 < 0f)
				{
					num5 += 1f;
				}
				Color color = Main.hslToRgb(num5, 1f, 0.5f, byte.MaxValue);
				string value = this._text.Value;
				Vector2 vector3 = FontAssets.DeathText.Value.MeasureString(value);
				vector3 *= 0.5f;
				float num6 = 1f - (1f - num3) * (1f - num3);
				ChatManager.DrawColorCodedStringShadow(info.SpriteBatch, FontAssets.DeathText.Value, value, vector, color * num6 * num6 * 0.25f * info.DisplayOpacity, 0f, vector3, vector2, -1f, 2f);
				ChatManager.DrawColorCodedString(info.SpriteBatch, FontAssets.DeathText.Value, value, vector, Color.White * num6 * info.DisplayOpacity, 0f, vector3, vector2, -1f, false);
			}

			// Token: 0x04007684 RID: 30340
			private const int PixelsForALine = 120;

			// Token: 0x04007685 RID: 30341
			private LocalizedText _text;

			// Token: 0x04007686 RID: 30342
			private float _timeToShowPeak;

			// Token: 0x04007687 RID: 30343
			private Vector2 _anchorOffset;
		}

		// Token: 0x020009A3 RID: 2467
		public abstract class AnimationSegmentWithActions<T> : IAnimationSegment
		{
			// Token: 0x17000599 RID: 1433
			// (get) Token: 0x060049B6 RID: 18870 RVA: 0x006D2D2C File Offset: 0x006D0F2C
			public float DedicatedTimeNeeded
			{
				get
				{
					return (float)this._dedicatedTimeNeeded;
				}
			}

			// Token: 0x060049B7 RID: 18871 RVA: 0x006D2D35 File Offset: 0x006D0F35
			public AnimationSegmentWithActions(int targetTime)
			{
				this._targetTime = targetTime;
				this._dedicatedTimeNeeded = 0;
			}

			// Token: 0x060049B8 RID: 18872 RVA: 0x006D2D58 File Offset: 0x006D0F58
			protected void ProcessActions(T obj, float localTimeForObject)
			{
				for (int i = 0; i < this._actions.Count; i++)
				{
					this._actions[i].ApplyTo(obj, localTimeForObject);
				}
			}

			// Token: 0x060049B9 RID: 18873 RVA: 0x006D2D90 File Offset: 0x006D0F90
			public Segments.AnimationSegmentWithActions<T> Then(IAnimationSegmentAction<T> act)
			{
				this.Bind(act);
				act.SetDelay((float)this._dedicatedTimeNeeded);
				this._actions.Add(act);
				this._lastDedicatedTimeNeeded = this._dedicatedTimeNeeded;
				this._dedicatedTimeNeeded += act.ExpectedLengthOfActionInFrames;
				return this;
			}

			// Token: 0x060049BA RID: 18874 RVA: 0x006D2DDD File Offset: 0x006D0FDD
			public Segments.AnimationSegmentWithActions<T> With(IAnimationSegmentAction<T> act)
			{
				this.Bind(act);
				act.SetDelay((float)this._lastDedicatedTimeNeeded);
				this._actions.Add(act);
				return this;
			}

			// Token: 0x060049BB RID: 18875
			protected abstract void Bind(IAnimationSegmentAction<T> act);

			// Token: 0x060049BC RID: 18876
			public abstract void Draw(ref GameAnimationSegment info);

			// Token: 0x04007688 RID: 30344
			private int _dedicatedTimeNeeded;

			// Token: 0x04007689 RID: 30345
			private int _lastDedicatedTimeNeeded;

			// Token: 0x0400768A RID: 30346
			protected int _targetTime;

			// Token: 0x0400768B RID: 30347
			private List<IAnimationSegmentAction<T>> _actions = new List<IAnimationSegmentAction<T>>();
		}

		// Token: 0x020009A4 RID: 2468
		public class PlayerSegment : Segments.AnimationSegmentWithActions<Player>
		{
			// Token: 0x060049BD RID: 18877 RVA: 0x006D2E00 File Offset: 0x006D1000
			public PlayerSegment(int targetTime, Vector2 anchorOffset, Vector2 normalizedHitboxOrigin)
				: base(targetTime)
			{
				this._player = new Player();
				this._anchorOffset = anchorOffset;
				this._normalizedOriginForHitbox = normalizedHitboxOrigin;
			}

			// Token: 0x060049BE RID: 18878 RVA: 0x006D2E22 File Offset: 0x006D1022
			public Segments.PlayerSegment UseShaderEffect(Segments.PlayerSegment.IShaderEffect shaderEffect)
			{
				this._shaderEffect = shaderEffect;
				return this;
			}

			// Token: 0x060049BF RID: 18879 RVA: 0x006D2E2C File Offset: 0x006D102C
			protected override void Bind(IAnimationSegmentAction<Player> act)
			{
				act.BindTo(this._player);
			}

			// Token: 0x060049C0 RID: 18880 RVA: 0x006D2E3C File Offset: 0x006D103C
			public override void Draw(ref GameAnimationSegment info)
			{
				if ((float)info.TimeInAnimation > (float)this._targetTime + base.DedicatedTimeNeeded)
				{
					return;
				}
				if (info.TimeInAnimation < this._targetTime)
				{
					return;
				}
				this.ResetPlayerAnimation(ref info);
				float num = (float)(info.TimeInAnimation - this._targetTime);
				base.ProcessActions(this._player, num);
				if (info.DisplayOpacity == 0f)
				{
					return;
				}
				this._player.ResetEffects();
				this._player.ResetVisibleAccessories();
				this._player.UpdateMiscCounter();
				this._player.UpdateDyes();
				this._player.PlayerFrame();
				this._player.socialIgnoreLight = true;
				this._player.position += Main.screenPosition;
				this._player.position -= new Vector2((float)(this._player.width / 2), (float)this._player.height);
				this._player.opacityForAnimation *= info.DisplayOpacity;
				Item item = this._player.inventory[this._player.selectedItem];
				this._player.inventory[this._player.selectedItem] = Segments.PlayerSegment._blankItem;
				float num2 = 1f - this._player.opacityForAnimation;
				num2 = 0f;
				if (this._shaderEffect != null)
				{
					this._shaderEffect.BeforeDrawing(ref info);
				}
				Main.PlayerRenderer.DrawPlayer(Main.Camera, this._player, this._player.position, 0f, this._player.fullRotationOrigin, num2, 1f);
				if (this._shaderEffect != null)
				{
					this._shaderEffect.AfterDrawing(ref info);
				}
				this._player.inventory[this._player.selectedItem] = item;
			}

			// Token: 0x060049C1 RID: 18881 RVA: 0x006D300E File Offset: 0x006D120E
			private void ResetPlayerAnimation(ref GameAnimationSegment info)
			{
				this._player.CopyVisuals(Main.LocalPlayer);
				this._player.position = info.AnchorPositionOnScreen + this._anchorOffset;
				this._player.opacityForAnimation = 1f;
			}

			// Token: 0x060049C2 RID: 18882 RVA: 0x006D304C File Offset: 0x006D124C
			// Note: this type is marked as 'beforefieldinit'.
			static PlayerSegment()
			{
			}

			// Token: 0x0400768C RID: 30348
			private Player _player;

			// Token: 0x0400768D RID: 30349
			private Vector2 _anchorOffset;

			// Token: 0x0400768E RID: 30350
			private Vector2 _normalizedOriginForHitbox;

			// Token: 0x0400768F RID: 30351
			private Segments.PlayerSegment.IShaderEffect _shaderEffect;

			// Token: 0x04007690 RID: 30352
			private static Item _blankItem = new Item();

			// Token: 0x02000AF2 RID: 2802
			public interface IShaderEffect
			{
				// Token: 0x06004D25 RID: 19749
				void BeforeDrawing(ref GameAnimationSegment info);

				// Token: 0x06004D26 RID: 19750
				void AfterDrawing(ref GameAnimationSegment info);
			}

			// Token: 0x02000AF3 RID: 2803
			public class ImmediateSpritebatchForPlayerDyesEffect : Segments.PlayerSegment.IShaderEffect
			{
				// Token: 0x06004D27 RID: 19751 RVA: 0x006DB999 File Offset: 0x006D9B99
				public void BeforeDrawing(ref GameAnimationSegment info)
				{
					info.SpriteBatch.End();
					info.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, null, Main.CurrentFrameFlags.Hacks.CurrentBackgroundMatrixForCreditsRoll);
				}

				// Token: 0x06004D28 RID: 19752 RVA: 0x006DB9CC File Offset: 0x006D9BCC
				public void AfterDrawing(ref GameAnimationSegment info)
				{
					info.SpriteBatch.End();
					info.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, null, Main.CurrentFrameFlags.Hacks.CurrentBackgroundMatrixForCreditsRoll);
				}

				// Token: 0x06004D29 RID: 19753 RVA: 0x0000357B File Offset: 0x0000177B
				public ImmediateSpritebatchForPlayerDyesEffect()
				{
				}
			}
		}

		// Token: 0x020009A5 RID: 2469
		public class NPCSegment : Segments.AnimationSegmentWithActions<NPC>
		{
			// Token: 0x060049C3 RID: 18883 RVA: 0x006D3058 File Offset: 0x006D1258
			public NPCSegment(int targetTime, int npcId, Vector2 anchorOffset, Vector2 normalizedNPCHitboxOrigin)
				: base(targetTime)
			{
				this._npc = new NPC();
				this._npc.IsABestiaryIconDummy = true;
				this._npc.SetDefaults(npcId, new NPCSpawnParams
				{
					playerCountForMultiplayerDifficultyOverride = new int?(1),
					difficultyOverride = new float?(GameDifficultyLevel.Classic)
				});
				this._anchorOffset = anchorOffset;
				this._normalizedOriginForHitbox = normalizedNPCHitboxOrigin;
			}

			// Token: 0x060049C4 RID: 18884 RVA: 0x006D30C5 File Offset: 0x006D12C5
			protected override void Bind(IAnimationSegmentAction<NPC> act)
			{
				act.BindTo(this._npc);
			}

			// Token: 0x060049C5 RID: 18885 RVA: 0x006D30D4 File Offset: 0x006D12D4
			public override void Draw(ref GameAnimationSegment info)
			{
				if ((float)info.TimeInAnimation > (float)this._targetTime + base.DedicatedTimeNeeded)
				{
					return;
				}
				if (info.TimeInAnimation < this._targetTime)
				{
					return;
				}
				this.ResetNPCAnimation(ref info);
				float num = (float)(info.TimeInAnimation - this._targetTime);
				base.ProcessActions(this._npc, num);
				if (this._npc.alpha >= 255)
				{
					return;
				}
				this._npc.FindFrame();
				ITownNPCProfile townNPCProfile;
				if (TownNPCProfiles.Instance.GetProfile(this._npc.type, out townNPCProfile))
				{
					TextureAssets.Npc[this._npc.type] = townNPCProfile.GetTextureNPCShouldUse(this._npc);
				}
				this._npc.Opacity *= info.DisplayOpacity;
				Main.instance.DrawNPCDirect(info.SpriteBatch, this._npc, this._npc.behindTiles, Vector2.Zero);
			}

			// Token: 0x060049C6 RID: 18886 RVA: 0x006D31C0 File Offset: 0x006D13C0
			private void ResetNPCAnimation(ref GameAnimationSegment info)
			{
				this._npc.position = info.AnchorPositionOnScreen + this._anchorOffset - this._npc.Size * this._normalizedOriginForHitbox;
				this._npc.alpha = 0;
				this._npc.velocity = Vector2.Zero;
			}

			// Token: 0x04007691 RID: 30353
			private NPC _npc;

			// Token: 0x04007692 RID: 30354
			private Vector2 _anchorOffset;

			// Token: 0x04007693 RID: 30355
			private Vector2 _normalizedOriginForHitbox;
		}

		// Token: 0x020009A6 RID: 2470
		public class LooseSprite
		{
			// Token: 0x060049C7 RID: 18887 RVA: 0x006D3220 File Offset: 0x006D1420
			public LooseSprite(DrawData data, Asset<Texture2D> asset)
			{
				this._originalDrawData = data;
				this._asset = asset;
				this.Reset();
			}

			// Token: 0x060049C8 RID: 18888 RVA: 0x006D323C File Offset: 0x006D143C
			public void Reset()
			{
				this._originalDrawData.texture = this._asset.Value;
				this.CurrentDrawData = this._originalDrawData;
				this.CurrentOpacity = 1f;
			}

			// Token: 0x04007694 RID: 30356
			private DrawData _originalDrawData;

			// Token: 0x04007695 RID: 30357
			private Asset<Texture2D> _asset;

			// Token: 0x04007696 RID: 30358
			public DrawData CurrentDrawData;

			// Token: 0x04007697 RID: 30359
			public float CurrentOpacity;
		}

		// Token: 0x020009A7 RID: 2471
		public class SpriteSegment : Segments.AnimationSegmentWithActions<Segments.LooseSprite>
		{
			// Token: 0x060049C9 RID: 18889 RVA: 0x006D326B File Offset: 0x006D146B
			public SpriteSegment(Asset<Texture2D> asset, int targetTime, DrawData data, Vector2 anchorOffset)
				: base(targetTime)
			{
				this._sprite = new Segments.LooseSprite(data, asset);
				this._anchorOffset = anchorOffset;
			}

			// Token: 0x060049CA RID: 18890 RVA: 0x006D3289 File Offset: 0x006D1489
			protected override void Bind(IAnimationSegmentAction<Segments.LooseSprite> act)
			{
				act.BindTo(this._sprite);
			}

			// Token: 0x060049CB RID: 18891 RVA: 0x006D3297 File Offset: 0x006D1497
			public Segments.SpriteSegment UseShaderEffect(Segments.SpriteSegment.IShaderEffect shaderEffect)
			{
				this._shaderEffect = shaderEffect;
				return this;
			}

			// Token: 0x060049CC RID: 18892 RVA: 0x006D32A4 File Offset: 0x006D14A4
			public override void Draw(ref GameAnimationSegment info)
			{
				if ((float)info.TimeInAnimation > (float)this._targetTime + base.DedicatedTimeNeeded)
				{
					return;
				}
				if (info.TimeInAnimation < this._targetTime)
				{
					return;
				}
				this.ResetSpriteAnimation(ref info);
				float num = (float)(info.TimeInAnimation - this._targetTime);
				base.ProcessActions(this._sprite, num);
				DrawData currentDrawData = this._sprite.CurrentDrawData;
				currentDrawData.position += info.AnchorPositionOnScreen + this._anchorOffset;
				currentDrawData.color *= this._sprite.CurrentOpacity * info.DisplayOpacity;
				if (this._shaderEffect != null)
				{
					this._shaderEffect.BeforeDrawing(ref info, ref currentDrawData);
				}
				currentDrawData.Draw(info.SpriteBatch);
				if (this._shaderEffect != null)
				{
					this._shaderEffect.AfterDrawing(ref info, ref currentDrawData);
				}
			}

			// Token: 0x060049CD RID: 18893 RVA: 0x006D3391 File Offset: 0x006D1591
			private void ResetSpriteAnimation(ref GameAnimationSegment info)
			{
				this._sprite.Reset();
			}

			// Token: 0x04007698 RID: 30360
			private Segments.LooseSprite _sprite;

			// Token: 0x04007699 RID: 30361
			private Vector2 _anchorOffset;

			// Token: 0x0400769A RID: 30362
			private Segments.SpriteSegment.IShaderEffect _shaderEffect;

			// Token: 0x02000AF4 RID: 2804
			public interface IShaderEffect
			{
				// Token: 0x06004D2A RID: 19754
				void BeforeDrawing(ref GameAnimationSegment info, ref DrawData drawData);

				// Token: 0x06004D2B RID: 19755
				void AfterDrawing(ref GameAnimationSegment info, ref DrawData drawData);
			}

			// Token: 0x02000AF5 RID: 2805
			public class MaskedFadeEffect : Segments.SpriteSegment.IShaderEffect
			{
				// Token: 0x06004D2C RID: 19756 RVA: 0x006DBA00 File Offset: 0x006D9C00
				public MaskedFadeEffect(Segments.SpriteSegment.MaskedFadeEffect.GetMatrixAction fetchMatrixMethod = null, string shaderKey = "MaskedFade", int verticalFrameCount = 1, int verticalFrameWait = 1)
				{
					this._fetchMatrix = fetchMatrixMethod;
					this._shaderKey = shaderKey;
					this._verticalFrameCount = verticalFrameCount;
					if (verticalFrameWait < 1)
					{
						verticalFrameWait = 1;
					}
					this._verticalFrameWait = verticalFrameWait;
					if (this._fetchMatrix == null)
					{
						this._fetchMatrix = new Segments.SpriteSegment.MaskedFadeEffect.GetMatrixAction(this.DefaultFetchMatrix);
					}
				}

				// Token: 0x06004D2D RID: 19757 RVA: 0x006DBA52 File Offset: 0x006D9C52
				private Matrix DefaultFetchMatrix()
				{
					return Main.CurrentFrameFlags.Hacks.CurrentBackgroundMatrixForCreditsRoll;
				}

				// Token: 0x06004D2E RID: 19758 RVA: 0x006DBA5C File Offset: 0x006D9C5C
				public void BeforeDrawing(ref GameAnimationSegment info, ref DrawData drawData)
				{
					info.SpriteBatch.End();
					info.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, null, this._fetchMatrix());
					MiscShaderData miscShaderData = GameShaders.Misc[this._shaderKey];
					int num = info.TimeInAnimation / this._verticalFrameWait % this._verticalFrameCount;
					miscShaderData.UseShaderSpecificData(new Vector4(1f / (float)this._verticalFrameCount, (float)num / (float)this._verticalFrameCount, this._panX.GetPanAmount((float)info.TimeInAnimation), this._panY.GetPanAmount((float)info.TimeInAnimation)));
					miscShaderData.Apply(new DrawData?(drawData));
				}

				// Token: 0x06004D2F RID: 19759 RVA: 0x006DBB1C File Offset: 0x006D9D1C
				public Segments.SpriteSegment.MaskedFadeEffect WithPanX(Segments.Panning panning)
				{
					this._panX = panning;
					return this;
				}

				// Token: 0x06004D30 RID: 19760 RVA: 0x006DBB26 File Offset: 0x006D9D26
				public Segments.SpriteSegment.MaskedFadeEffect WithPanY(Segments.Panning panning)
				{
					this._panY = panning;
					return this;
				}

				// Token: 0x06004D31 RID: 19761 RVA: 0x006DBB30 File Offset: 0x006D9D30
				public void AfterDrawing(ref GameAnimationSegment info, ref DrawData drawData)
				{
					Main.pixelShader.CurrentTechnique.Passes[0].Apply();
					info.SpriteBatch.End();
					info.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, this._fetchMatrix());
				}

				// Token: 0x040078CC RID: 30924
				private readonly string _shaderKey;

				// Token: 0x040078CD RID: 30925
				private readonly int _verticalFrameCount;

				// Token: 0x040078CE RID: 30926
				private readonly int _verticalFrameWait;

				// Token: 0x040078CF RID: 30927
				private Segments.Panning _panX;

				// Token: 0x040078D0 RID: 30928
				private Segments.Panning _panY;

				// Token: 0x040078D1 RID: 30929
				private Segments.SpriteSegment.MaskedFadeEffect.GetMatrixAction _fetchMatrix;

				// Token: 0x02000B20 RID: 2848
				// (Invoke) Token: 0x06004DCA RID: 19914
				public delegate Matrix GetMatrixAction();
			}
		}

		// Token: 0x020009A8 RID: 2472
		public struct Panning
		{
			// Token: 0x060049CE RID: 18894 RVA: 0x006D33A0 File Offset: 0x006D15A0
			public float GetPanAmount(float time)
			{
				float num = MathHelper.Clamp((time - this.Delay) / this.Duration, 0f, 1f);
				return this.StartAmount + num * this.AmountOverTime;
			}

			// Token: 0x0400769B RID: 30363
			public float AmountOverTime;

			// Token: 0x0400769C RID: 30364
			public float StartAmount;

			// Token: 0x0400769D RID: 30365
			public float Delay;

			// Token: 0x0400769E RID: 30366
			public float Duration;
		}

		// Token: 0x020009A9 RID: 2473
		public class EmoteSegment : IAnimationSegment
		{
			// Token: 0x1700059A RID: 1434
			// (get) Token: 0x060049CF RID: 18895 RVA: 0x006D33DB File Offset: 0x006D15DB
			// (set) Token: 0x060049D0 RID: 18896 RVA: 0x006D33E3 File Offset: 0x006D15E3
			public float DedicatedTimeNeeded
			{
				[CompilerGenerated]
				get
				{
					return this.<DedicatedTimeNeeded>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<DedicatedTimeNeeded>k__BackingField = value;
				}
			}

			// Token: 0x060049D1 RID: 18897 RVA: 0x006D33EC File Offset: 0x006D15EC
			public EmoteSegment(int emoteId, int targetTime, int timeToPlay, Vector2 position, SpriteEffects drawEffect, Vector2 velocity = default(Vector2))
			{
				this._emoteId = emoteId;
				this._targetTime = targetTime;
				this._effect = drawEffect;
				this._offset = position;
				this._velocity = velocity;
				this.DedicatedTimeNeeded = (float)timeToPlay;
			}

			// Token: 0x060049D2 RID: 18898 RVA: 0x006D3424 File Offset: 0x006D1624
			public void Draw(ref GameAnimationSegment info)
			{
				int num = info.TimeInAnimation - this._targetTime;
				if (num < 0)
				{
					return;
				}
				if ((float)num >= this.DedicatedTimeNeeded)
				{
					return;
				}
				Vector2 vector = info.AnchorPositionOnScreen + this._offset + this._velocity * (float)num;
				vector = vector.Floor();
				bool flag = num < 6 || (float)num >= this.DedicatedTimeNeeded - 6f;
				Texture2D value = TextureAssets.Extra[48].Value;
				Rectangle rectangle = value.Frame(8, EmoteBubble.EMOTE_SHEET_VERTICAL_FRAMES, flag ? 0 : 1, 0, 0, 0);
				Vector2 vector2 = new Vector2((float)(rectangle.Width / 2), (float)rectangle.Height);
				SpriteEffects spriteEffects = this._effect;
				info.SpriteBatch.Draw(value, vector, new Rectangle?(rectangle), Color.White * info.DisplayOpacity, 0f, vector2, 1f, spriteEffects, 0f);
				if (!flag)
				{
					int emoteId = this._emoteId;
					if ((emoteId == 87 || emoteId == 89) && (spriteEffects & SpriteEffects.FlipHorizontally) != SpriteEffects.None)
					{
						spriteEffects &= ~SpriteEffects.FlipHorizontally;
						vector.X += 4f;
					}
					info.SpriteBatch.Draw(value, vector, new Rectangle?(this.GetFrame(num % 20)), Color.White, 0f, vector2, 1f, spriteEffects, 0f);
				}
			}

			// Token: 0x060049D3 RID: 18899 RVA: 0x006D357C File Offset: 0x006D177C
			private Rectangle GetFrame(int wrappedTime)
			{
				int num = ((wrappedTime >= 10) ? 1 : 0);
				return TextureAssets.Extra[48].Value.Frame(8, EmoteBubble.EMOTE_SHEET_VERTICAL_FRAMES, this._emoteId % 4 * 2 + num, this._emoteId / 4 + 1, 0, 0);
			}

			// Token: 0x0400769F RID: 30367
			[CompilerGenerated]
			private float <DedicatedTimeNeeded>k__BackingField;

			// Token: 0x040076A0 RID: 30368
			private int _targetTime;

			// Token: 0x040076A1 RID: 30369
			private Vector2 _offset;

			// Token: 0x040076A2 RID: 30370
			private SpriteEffects _effect;

			// Token: 0x040076A3 RID: 30371
			private int _emoteId;

			// Token: 0x040076A4 RID: 30372
			private Vector2 _velocity;
		}
	}
}
