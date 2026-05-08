using System;

namespace Microsoft.Xna.Framework.Input
{
	// Token: 0x02000064 RID: 100
	public struct GamePadTriggers
	{
		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06001061 RID: 4193 RVA: 0x00022725 File Offset: 0x00020925
		public float Left
		{
			get
			{
				return this.left;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06001062 RID: 4194 RVA: 0x0002272D File Offset: 0x0002092D
		public float Right
		{
			get
			{
				return this.right;
			}
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x00022735 File Offset: 0x00020935
		public GamePadTriggers(float leftTrigger, float rightTrigger)
		{
			this.left = MathHelper.Clamp(leftTrigger, 0f, 1f);
			this.right = MathHelper.Clamp(rightTrigger, 0f, 1f);
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x00022764 File Offset: 0x00020964
		internal GamePadTriggers(float leftTrigger, float rightTrigger, GamePadDeadZone deadZoneMode)
		{
			if (deadZoneMode == GamePadDeadZone.None)
			{
				this.left = MathHelper.Clamp(leftTrigger, 0f, 1f);
				this.right = MathHelper.Clamp(rightTrigger, 0f, 1f);
				return;
			}
			this.left = MathHelper.Clamp(GamePad.ExcludeAxisDeadZone(leftTrigger, 0.11764706f), 0f, 1f);
			this.right = MathHelper.Clamp(GamePad.ExcludeAxisDeadZone(rightTrigger, 0.11764706f), 0f, 1f);
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x000227E1 File Offset: 0x000209E1
		public static bool operator ==(GamePadTriggers left, GamePadTriggers right)
		{
			return MathHelper.WithinEpsilon(left.left, right.left) && MathHelper.WithinEpsilon(left.right, right.right);
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x00022809 File Offset: 0x00020A09
		public static bool operator !=(GamePadTriggers left, GamePadTriggers right)
		{
			return !(left == right);
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x00022815 File Offset: 0x00020A15
		public override bool Equals(object obj)
		{
			return obj is GamePadTriggers && this == (GamePadTriggers)obj;
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x00022834 File Offset: 0x00020A34
		public override int GetHashCode()
		{
			return this.Left.GetHashCode() + this.Right.GetHashCode();
		}

		// Token: 0x040006A2 RID: 1698
		private float left;

		// Token: 0x040006A3 RID: 1699
		private float right;
	}
}
