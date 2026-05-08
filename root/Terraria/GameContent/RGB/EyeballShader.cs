using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;
using Terraria.Utilities;

namespace Terraria.GameContent.RGB
{
	// Token: 0x020002B9 RID: 697
	public class EyeballShader : ChromaShader
	{
		// Token: 0x060025AD RID: 9645 RVA: 0x00558B0C File Offset: 0x00556D0C
		public EyeballShader(bool isSpawning)
		{
			this._isSpawning = isSpawning;
		}

		// Token: 0x060025AE RID: 9646 RVA: 0x00558B60 File Offset: 0x00556D60
		public override void Update(float elapsedTime)
		{
			this.UpdateEyelid(elapsedTime);
			bool flag = this._timeUntilPupilMove <= 0f;
			this._pupilOffset = (this._targetOffset + this._pupilOffset) * 0.5f;
			this._timeUntilPupilMove -= elapsedTime;
			if (flag)
			{
				float num = (float)this._random.NextDouble() * 6.2831855f;
				float num2;
				if (this._isSpawning)
				{
					this._timeUntilPupilMove = (float)this._random.NextDouble() * 0.4f + 0.3f;
					num2 = (float)this._random.NextDouble() * 0.7f;
				}
				else
				{
					this._timeUntilPupilMove = (float)this._random.NextDouble() * 0.4f + 0.6f;
					num2 = (float)this._random.NextDouble() * 0.3f;
				}
				this._targetOffset = new Vector2((float)Math.Cos((double)num), (float)Math.Sin((double)num)) * num2;
			}
		}

		// Token: 0x060025AF RID: 9647 RVA: 0x00558C58 File Offset: 0x00556E58
		private void UpdateEyelid(float elapsedTime)
		{
			float num = 0.5f;
			float num2 = 6f;
			if (this._isSpawning)
			{
				if (NPC.MoonLordCountdown >= NPC.MaxMoonLordCountdown - 10)
				{
					this._eyelidStateTime = 0f;
					this._eyelidState = EyeballShader.EyelidState.Closed;
				}
				num = (float)NPC.MoonLordCountdown / (float)NPC.MaxMoonLordCountdown * 10f + 0.5f;
				num2 = 2f;
			}
			this._eyelidStateTime += elapsedTime;
			switch (this._eyelidState)
			{
			case EyeballShader.EyelidState.Closed:
				this._eyelidProgress = 0f;
				if (this._eyelidStateTime > num)
				{
					this._eyelidStateTime = 0f;
					this._eyelidState = EyeballShader.EyelidState.Opening;
					return;
				}
				break;
			case EyeballShader.EyelidState.Opening:
				this._eyelidProgress = this._eyelidStateTime / 0.4f;
				if (this._eyelidStateTime > 0.4f)
				{
					this._eyelidStateTime = 0f;
					this._eyelidState = EyeballShader.EyelidState.Open;
					return;
				}
				break;
			case EyeballShader.EyelidState.Open:
				this._eyelidProgress = 1f;
				if (this._eyelidStateTime > num2)
				{
					this._eyelidStateTime = 0f;
					this._eyelidState = EyeballShader.EyelidState.Closing;
					return;
				}
				break;
			case EyeballShader.EyelidState.Closing:
				this._eyelidProgress = 1f - this._eyelidStateTime / 0.4f;
				if (this._eyelidStateTime > 0.4f)
				{
					this._eyelidStateTime = 0f;
					this._eyelidState = EyeballShader.EyelidState.Closed;
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x060025B0 RID: 9648 RVA: 0x00558DA0 File Offset: 0x00556FA0
		[RgbProcessor(new EffectDetailLevel[] { 1 })]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			Vector2 vector = new Vector2(1.5f, 0.5f);
			Vector2 vector2 = vector + this._pupilOffset;
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector2 vector3 = canvasPositionOfIndex - vector;
				Vector4 vector4 = Vector4.One;
				float num = (vector2 - canvasPositionOfIndex).Length();
				for (int j = 1; j < EyeballShader.Rings.Length; j++)
				{
					EyeballShader.Ring ring = EyeballShader.Rings[j];
					EyeballShader.Ring ring2 = EyeballShader.Rings[j - 1];
					if (num < ring.Distance)
					{
						vector4 = Vector4.Lerp(ring2.Color, ring.Color, (num - ring2.Distance) / (ring.Distance - ring2.Distance));
						break;
					}
				}
				float num2 = (float)Math.Sqrt((double)(1f - 0.4f * vector3.Y * vector3.Y)) * 5f;
				float num3 = Math.Abs(vector3.X) - num2 * (1.1f * this._eyelidProgress - 0.1f);
				if (num3 > 0f)
				{
					vector4 = Vector4.Lerp(vector4, this._eyelidColor, Math.Min(1f, num3 * 10f));
				}
				fragment.SetColor(i, vector4);
			}
		}

		// Token: 0x060025B1 RID: 9649 RVA: 0x00558F00 File Offset: 0x00557100
		// Note: this type is marked as 'beforefieldinit'.
		static EyeballShader()
		{
		}

		// Token: 0x04005011 RID: 20497
		private static readonly EyeballShader.Ring[] Rings = new EyeballShader.Ring[]
		{
			new EyeballShader.Ring(Color.Black.ToVector4(), 0f),
			new EyeballShader.Ring(Color.Black.ToVector4(), 0.4f),
			new EyeballShader.Ring(new Color(17, 220, 237).ToVector4(), 0.5f),
			new EyeballShader.Ring(new Color(17, 120, 237).ToVector4(), 0.6f),
			new EyeballShader.Ring(Vector4.One, 0.65f)
		};

		// Token: 0x04005012 RID: 20498
		private readonly Vector4 _eyelidColor = new Color(108, 110, 75).ToVector4();

		// Token: 0x04005013 RID: 20499
		private float _eyelidProgress;

		// Token: 0x04005014 RID: 20500
		private Vector2 _pupilOffset = Vector2.Zero;

		// Token: 0x04005015 RID: 20501
		private Vector2 _targetOffset = Vector2.Zero;

		// Token: 0x04005016 RID: 20502
		private readonly UnifiedRandom _random = new UnifiedRandom();

		// Token: 0x04005017 RID: 20503
		private float _timeUntilPupilMove;

		// Token: 0x04005018 RID: 20504
		private float _eyelidStateTime;

		// Token: 0x04005019 RID: 20505
		private readonly bool _isSpawning;

		// Token: 0x0400501A RID: 20506
		private EyeballShader.EyelidState _eyelidState;

		// Token: 0x02000811 RID: 2065
		private struct Ring
		{
			// Token: 0x060042F0 RID: 17136 RVA: 0x006BFFF9 File Offset: 0x006BE1F9
			public Ring(Vector4 color, float distance)
			{
				this.Color = color;
				this.Distance = distance;
			}

			// Token: 0x040071EE RID: 29166
			public readonly Vector4 Color;

			// Token: 0x040071EF RID: 29167
			public readonly float Distance;
		}

		// Token: 0x02000812 RID: 2066
		private enum EyelidState
		{
			// Token: 0x040071F1 RID: 29169
			Closed,
			// Token: 0x040071F2 RID: 29170
			Opening,
			// Token: 0x040071F3 RID: 29171
			Open,
			// Token: 0x040071F4 RID: 29172
			Closing
		}
	}
}
