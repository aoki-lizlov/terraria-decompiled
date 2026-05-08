using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Xna.Framework.Input
{
	// Token: 0x02000061 RID: 97
	public struct GamePadDPad
	{
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06001031 RID: 4145 RVA: 0x0002204E File Offset: 0x0002024E
		// (set) Token: 0x06001032 RID: 4146 RVA: 0x00022056 File Offset: 0x00020256
		public ButtonState Down
		{
			[CompilerGenerated]
			get
			{
				return this.<Down>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<Down>k__BackingField = value;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06001033 RID: 4147 RVA: 0x0002205F File Offset: 0x0002025F
		// (set) Token: 0x06001034 RID: 4148 RVA: 0x00022067 File Offset: 0x00020267
		public ButtonState Left
		{
			[CompilerGenerated]
			get
			{
				return this.<Left>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<Left>k__BackingField = value;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06001035 RID: 4149 RVA: 0x00022070 File Offset: 0x00020270
		// (set) Token: 0x06001036 RID: 4150 RVA: 0x00022078 File Offset: 0x00020278
		public ButtonState Right
		{
			[CompilerGenerated]
			get
			{
				return this.<Right>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<Right>k__BackingField = value;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06001037 RID: 4151 RVA: 0x00022081 File Offset: 0x00020281
		// (set) Token: 0x06001038 RID: 4152 RVA: 0x00022089 File Offset: 0x00020289
		public ButtonState Up
		{
			[CompilerGenerated]
			get
			{
				return this.<Up>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<Up>k__BackingField = value;
			}
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x00022092 File Offset: 0x00020292
		public GamePadDPad(ButtonState upValue, ButtonState downValue, ButtonState leftValue, ButtonState rightValue)
		{
			this = default(GamePadDPad);
			this.Up = upValue;
			this.Down = downValue;
			this.Left = leftValue;
			this.Right = rightValue;
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x000220B8 File Offset: 0x000202B8
		internal static GamePadDPad FromButtonArray(params Buttons[] buttons)
		{
			ButtonState buttonState = ButtonState.Released;
			ButtonState buttonState2 = ButtonState.Released;
			ButtonState buttonState3 = ButtonState.Released;
			ButtonState buttonState4 = ButtonState.Released;
			foreach (Buttons buttons2 in buttons)
			{
				if ((buttons2 & Buttons.DPadUp) == Buttons.DPadUp)
				{
					buttonState = ButtonState.Pressed;
				}
				if ((buttons2 & Buttons.DPadDown) == Buttons.DPadDown)
				{
					buttonState2 = ButtonState.Pressed;
				}
				if ((buttons2 & Buttons.DPadLeft) == Buttons.DPadLeft)
				{
					buttonState3 = ButtonState.Pressed;
				}
				if ((buttons2 & Buttons.DPadRight) == Buttons.DPadRight)
				{
					buttonState4 = ButtonState.Pressed;
				}
			}
			return new GamePadDPad(buttonState, buttonState2, buttonState3, buttonState4);
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x00022110 File Offset: 0x00020310
		public static bool operator ==(GamePadDPad left, GamePadDPad right)
		{
			return left.Down == right.Down && left.Left == right.Left && left.Right == right.Right && left.Up == right.Up;
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x0002215F File Offset: 0x0002035F
		public static bool operator !=(GamePadDPad left, GamePadDPad right)
		{
			return !(left == right);
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x0002216B File Offset: 0x0002036B
		public override bool Equals(object obj)
		{
			return obj is GamePadDPad && this == (GamePadDPad)obj;
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x00022188 File Offset: 0x00020388
		public override int GetHashCode()
		{
			return ((this.Down == ButtonState.Pressed) ? 1 : 0) + ((this.Left == ButtonState.Pressed) ? 2 : 0) + ((this.Right == ButtonState.Pressed) ? 4 : 0) + ((this.Up == ButtonState.Pressed) ? 8 : 0);
		}

		// Token: 0x04000696 RID: 1686
		[CompilerGenerated]
		private ButtonState <Down>k__BackingField;

		// Token: 0x04000697 RID: 1687
		[CompilerGenerated]
		private ButtonState <Left>k__BackingField;

		// Token: 0x04000698 RID: 1688
		[CompilerGenerated]
		private ButtonState <Right>k__BackingField;

		// Token: 0x04000699 RID: 1689
		[CompilerGenerated]
		private ButtonState <Up>k__BackingField;
	}
}
