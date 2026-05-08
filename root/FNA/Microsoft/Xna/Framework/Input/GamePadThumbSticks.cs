using System;

namespace Microsoft.Xna.Framework.Input
{
	// Token: 0x02000063 RID: 99
	public struct GamePadThumbSticks
	{
		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06001055 RID: 4181 RVA: 0x00022466 File Offset: 0x00020666
		public Vector2 Left
		{
			get
			{
				return this.left;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06001056 RID: 4182 RVA: 0x0002246E File Offset: 0x0002066E
		public Vector2 Right
		{
			get
			{
				return this.right;
			}
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x00022476 File Offset: 0x00020676
		public GamePadThumbSticks(Vector2 leftPosition, Vector2 rightPosition)
		{
			this.left = leftPosition;
			this.right = rightPosition;
			this.ApplySquareClamp();
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x0002248C File Offset: 0x0002068C
		internal GamePadThumbSticks(Vector2 leftPosition, Vector2 rightPosition, GamePadDeadZone deadZoneMode)
		{
			this.left = leftPosition;
			this.right = rightPosition;
			this.ApplyDeadZone(deadZoneMode);
			if (deadZoneMode == GamePadDeadZone.Circular)
			{
				this.ApplyCircularClamp();
				return;
			}
			this.ApplySquareClamp();
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x000224B4 File Offset: 0x000206B4
		private void ApplyDeadZone(GamePadDeadZone dz)
		{
			switch (dz)
			{
			case GamePadDeadZone.None:
				break;
			case GamePadDeadZone.IndependentAxes:
				this.left.X = GamePad.ExcludeAxisDeadZone(this.left.X, 0.23953247f);
				this.left.Y = GamePad.ExcludeAxisDeadZone(this.left.Y, 0.23953247f);
				this.right.X = GamePad.ExcludeAxisDeadZone(this.right.X, 0.26516724f);
				this.right.Y = GamePad.ExcludeAxisDeadZone(this.right.Y, 0.26516724f);
				return;
			case GamePadDeadZone.Circular:
				this.left = GamePadThumbSticks.ExcludeCircularDeadZone(this.left, 0.23953247f);
				this.right = GamePadThumbSticks.ExcludeCircularDeadZone(this.right, 0.26516724f);
				break;
			default:
				return;
			}
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x00022584 File Offset: 0x00020784
		private void ApplySquareClamp()
		{
			this.left.X = MathHelper.Clamp(this.left.X, -1f, 1f);
			this.left.Y = MathHelper.Clamp(this.left.Y, -1f, 1f);
			this.right.X = MathHelper.Clamp(this.right.X, -1f, 1f);
			this.right.Y = MathHelper.Clamp(this.right.Y, -1f, 1f);
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x00022625 File Offset: 0x00020825
		private void ApplyCircularClamp()
		{
			if (this.left.LengthSquared() > 1f)
			{
				this.left.Normalize();
			}
			if (this.right.LengthSquared() > 1f)
			{
				this.right.Normalize();
			}
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x00022664 File Offset: 0x00020864
		private static Vector2 ExcludeCircularDeadZone(Vector2 value, float deadZone)
		{
			float num = value.Length();
			if (num <= deadZone)
			{
				return Vector2.Zero;
			}
			float num2 = (num - deadZone) / (1f - deadZone);
			return value * (num2 / num);
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x00022698 File Offset: 0x00020898
		public static bool operator ==(GamePadThumbSticks left, GamePadThumbSticks right)
		{
			return left.left == right.left && left.right == right.right;
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x000226C0 File Offset: 0x000208C0
		public static bool operator !=(GamePadThumbSticks left, GamePadThumbSticks right)
		{
			return !(left == right);
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x000226CC File Offset: 0x000208CC
		public override bool Equals(object obj)
		{
			return obj is GamePadThumbSticks && this == (GamePadThumbSticks)obj;
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x000226EC File Offset: 0x000208EC
		public override int GetHashCode()
		{
			return this.Left.GetHashCode() + 37 * this.Right.GetHashCode();
		}

		// Token: 0x040006A0 RID: 1696
		private Vector2 left;

		// Token: 0x040006A1 RID: 1697
		private Vector2 right;
	}
}
