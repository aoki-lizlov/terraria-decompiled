using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Xna.Framework.Input
{
	// Token: 0x02000062 RID: 98
	public struct GamePadState
	{
		// Token: 0x170001BF RID: 447
		// (get) Token: 0x0600103F RID: 4159 RVA: 0x000221BD File Offset: 0x000203BD
		// (set) Token: 0x06001040 RID: 4160 RVA: 0x000221C5 File Offset: 0x000203C5
		public bool IsConnected
		{
			[CompilerGenerated]
			get
			{
				return this.<IsConnected>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<IsConnected>k__BackingField = value;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06001041 RID: 4161 RVA: 0x000221CE File Offset: 0x000203CE
		// (set) Token: 0x06001042 RID: 4162 RVA: 0x000221D6 File Offset: 0x000203D6
		public int PacketNumber
		{
			[CompilerGenerated]
			get
			{
				return this.<PacketNumber>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<PacketNumber>k__BackingField = value;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06001043 RID: 4163 RVA: 0x000221DF File Offset: 0x000203DF
		// (set) Token: 0x06001044 RID: 4164 RVA: 0x000221E7 File Offset: 0x000203E7
		public GamePadButtons Buttons
		{
			[CompilerGenerated]
			get
			{
				return this.<Buttons>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<Buttons>k__BackingField = value;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06001045 RID: 4165 RVA: 0x000221F0 File Offset: 0x000203F0
		// (set) Token: 0x06001046 RID: 4166 RVA: 0x000221F8 File Offset: 0x000203F8
		public GamePadDPad DPad
		{
			[CompilerGenerated]
			get
			{
				return this.<DPad>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<DPad>k__BackingField = value;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06001047 RID: 4167 RVA: 0x00022201 File Offset: 0x00020401
		// (set) Token: 0x06001048 RID: 4168 RVA: 0x00022209 File Offset: 0x00020409
		public GamePadThumbSticks ThumbSticks
		{
			[CompilerGenerated]
			get
			{
				return this.<ThumbSticks>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<ThumbSticks>k__BackingField = value;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06001049 RID: 4169 RVA: 0x00022212 File Offset: 0x00020412
		// (set) Token: 0x0600104A RID: 4170 RVA: 0x0002221A File Offset: 0x0002041A
		public GamePadTriggers Triggers
		{
			[CompilerGenerated]
			get
			{
				return this.<Triggers>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<Triggers>k__BackingField = value;
			}
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x00022224 File Offset: 0x00020424
		public GamePadState(GamePadThumbSticks thumbSticks, GamePadTriggers triggers, GamePadButtons buttons, GamePadDPad dPad)
		{
			this = default(GamePadState);
			if (triggers.Left > 0.11764706f)
			{
				buttons.buttons |= Microsoft.Xna.Framework.Input.Buttons.LeftTrigger;
			}
			if (triggers.Right > 0.11764706f)
			{
				buttons.buttons |= Microsoft.Xna.Framework.Input.Buttons.RightTrigger;
			}
			buttons.buttons |= GamePadState.StickToButtons(thumbSticks.Left, Microsoft.Xna.Framework.Input.Buttons.LeftThumbstickLeft, Microsoft.Xna.Framework.Input.Buttons.LeftThumbstickRight, Microsoft.Xna.Framework.Input.Buttons.LeftThumbstickUp, Microsoft.Xna.Framework.Input.Buttons.LeftThumbstickDown, 0.23953247f);
			buttons.buttons |= GamePadState.StickToButtons(thumbSticks.Right, Microsoft.Xna.Framework.Input.Buttons.RightThumbstickLeft, Microsoft.Xna.Framework.Input.Buttons.RightThumbstickRight, Microsoft.Xna.Framework.Input.Buttons.RightThumbstickUp, Microsoft.Xna.Framework.Input.Buttons.RightThumbstickDown, 0.26516724f);
			this.ThumbSticks = thumbSticks;
			this.Triggers = triggers;
			this.Buttons = buttons;
			this.DPad = dPad;
			this.IsConnected = true;
			this.PacketNumber = 0;
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x000222FF File Offset: 0x000204FF
		public GamePadState(Vector2 leftThumbStick, Vector2 rightThumbStick, float leftTrigger, float rightTrigger, params Buttons[] buttons)
		{
			this = new GamePadState(new GamePadThumbSticks(leftThumbStick, rightThumbStick), new GamePadTriggers(leftTrigger, rightTrigger), GamePadButtons.FromButtonArray(buttons), GamePadDPad.FromButtonArray(buttons));
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x00022324 File Offset: 0x00020524
		public bool IsButtonDown(Buttons button)
		{
			return (this.Buttons.buttons & button) == button;
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x00022336 File Offset: 0x00020536
		public bool IsButtonUp(Buttons button)
		{
			return (this.Buttons.buttons & button) != button;
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x0002234C File Offset: 0x0002054C
		private static Buttons StickToButtons(Vector2 stick, Buttons left, Buttons right, Buttons up, Buttons down, float DeadZoneSize)
		{
			Buttons buttons = (Buttons)0;
			if (stick.X > DeadZoneSize)
			{
				buttons |= right;
			}
			if (stick.X < -DeadZoneSize)
			{
				buttons |= left;
			}
			if (stick.Y > DeadZoneSize)
			{
				buttons |= up;
			}
			if (stick.Y < -DeadZoneSize)
			{
				buttons |= down;
			}
			return buttons;
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x00022398 File Offset: 0x00020598
		public static bool operator ==(GamePadState left, GamePadState right)
		{
			return left.IsConnected == right.IsConnected && left.PacketNumber == right.PacketNumber && left.Buttons == right.Buttons && left.DPad == right.DPad && left.ThumbSticks == right.ThumbSticks && left.Triggers == right.Triggers;
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x00022419 File Offset: 0x00020619
		public static bool operator !=(GamePadState left, GamePadState right)
		{
			return !(left == right);
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x00022425 File Offset: 0x00020625
		public override bool Equals(object obj)
		{
			return obj is GamePadState && this == (GamePadState)obj;
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x00022442 File Offset: 0x00020642
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x00022454 File Offset: 0x00020654
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x0400069A RID: 1690
		[CompilerGenerated]
		private bool <IsConnected>k__BackingField;

		// Token: 0x0400069B RID: 1691
		[CompilerGenerated]
		private int <PacketNumber>k__BackingField;

		// Token: 0x0400069C RID: 1692
		[CompilerGenerated]
		private GamePadButtons <Buttons>k__BackingField;

		// Token: 0x0400069D RID: 1693
		[CompilerGenerated]
		private GamePadDPad <DPad>k__BackingField;

		// Token: 0x0400069E RID: 1694
		[CompilerGenerated]
		private GamePadThumbSticks <ThumbSticks>k__BackingField;

		// Token: 0x0400069F RID: 1695
		[CompilerGenerated]
		private GamePadTriggers <Triggers>k__BackingField;
	}
}
