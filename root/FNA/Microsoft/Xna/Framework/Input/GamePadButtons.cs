using System;

namespace Microsoft.Xna.Framework.Input
{
	// Token: 0x0200005E RID: 94
	public struct GamePadButtons
	{
		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000FD8 RID: 4056 RVA: 0x00021C7B File Offset: 0x0001FE7B
		public ButtonState A
		{
			get
			{
				if ((this.buttons & Buttons.A) != Buttons.A)
				{
					return ButtonState.Released;
				}
				return ButtonState.Pressed;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000FD9 RID: 4057 RVA: 0x00021C93 File Offset: 0x0001FE93
		public ButtonState B
		{
			get
			{
				if ((this.buttons & Buttons.B) != Buttons.B)
				{
					return ButtonState.Released;
				}
				return ButtonState.Pressed;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000FDA RID: 4058 RVA: 0x00021CAB File Offset: 0x0001FEAB
		public ButtonState Back
		{
			get
			{
				if ((this.buttons & Buttons.Back) != Buttons.Back)
				{
					return ButtonState.Released;
				}
				return ButtonState.Pressed;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000FDB RID: 4059 RVA: 0x00021CBD File Offset: 0x0001FEBD
		public ButtonState X
		{
			get
			{
				if ((this.buttons & Buttons.X) != Buttons.X)
				{
					return ButtonState.Released;
				}
				return ButtonState.Pressed;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000FDC RID: 4060 RVA: 0x00021CD5 File Offset: 0x0001FED5
		public ButtonState Y
		{
			get
			{
				if ((this.buttons & Buttons.Y) != Buttons.Y)
				{
					return ButtonState.Released;
				}
				return ButtonState.Pressed;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000FDD RID: 4061 RVA: 0x00021CED File Offset: 0x0001FEED
		public ButtonState Start
		{
			get
			{
				if ((this.buttons & Buttons.Start) != Buttons.Start)
				{
					return ButtonState.Released;
				}
				return ButtonState.Pressed;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000FDE RID: 4062 RVA: 0x00021CFF File Offset: 0x0001FEFF
		public ButtonState LeftShoulder
		{
			get
			{
				if ((this.buttons & Buttons.LeftShoulder) != Buttons.LeftShoulder)
				{
					return ButtonState.Released;
				}
				return ButtonState.Pressed;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000FDF RID: 4063 RVA: 0x00021D17 File Offset: 0x0001FF17
		public ButtonState LeftStick
		{
			get
			{
				if ((this.buttons & Buttons.LeftStick) != Buttons.LeftStick)
				{
					return ButtonState.Released;
				}
				return ButtonState.Pressed;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000FE0 RID: 4064 RVA: 0x00021D29 File Offset: 0x0001FF29
		public ButtonState RightShoulder
		{
			get
			{
				if ((this.buttons & Buttons.RightShoulder) != Buttons.RightShoulder)
				{
					return ButtonState.Released;
				}
				return ButtonState.Pressed;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000FE1 RID: 4065 RVA: 0x00021D41 File Offset: 0x0001FF41
		public ButtonState RightStick
		{
			get
			{
				if ((this.buttons & Buttons.RightStick) != Buttons.RightStick)
				{
					return ButtonState.Released;
				}
				return ButtonState.Pressed;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000FE2 RID: 4066 RVA: 0x00021D59 File Offset: 0x0001FF59
		public ButtonState BigButton
		{
			get
			{
				if ((this.buttons & Buttons.BigButton) != Buttons.BigButton)
				{
					return ButtonState.Released;
				}
				return ButtonState.Pressed;
			}
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x00021D71 File Offset: 0x0001FF71
		public GamePadButtons(Buttons buttons)
		{
			this.buttons = buttons;
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x00021D7C File Offset: 0x0001FF7C
		internal static GamePadButtons FromButtonArray(params Buttons[] buttons)
		{
			Buttons buttons2 = (Buttons)0;
			foreach (Buttons buttons3 in buttons)
			{
				buttons2 |= buttons3;
			}
			return new GamePadButtons(buttons2);
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x00021DA9 File Offset: 0x0001FFA9
		public static bool operator ==(GamePadButtons left, GamePadButtons right)
		{
			return left.buttons == right.buttons;
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x00021DB9 File Offset: 0x0001FFB9
		public static bool operator !=(GamePadButtons left, GamePadButtons right)
		{
			return !(left == right);
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x00021DC5 File Offset: 0x0001FFC5
		public override bool Equals(object obj)
		{
			return obj is GamePadButtons && this == (GamePadButtons)obj;
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x00021DE2 File Offset: 0x0001FFE2
		public override int GetHashCode()
		{
			return (int)this.buttons;
		}

		// Token: 0x0400066D RID: 1645
		internal Buttons buttons;
	}
}
