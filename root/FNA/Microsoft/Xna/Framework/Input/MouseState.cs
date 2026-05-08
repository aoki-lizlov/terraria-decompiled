using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Xna.Framework.Input
{
	// Token: 0x0200006B RID: 107
	public struct MouseState
	{
		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06001085 RID: 4229 RVA: 0x00022F7C File Offset: 0x0002117C
		// (set) Token: 0x06001086 RID: 4230 RVA: 0x00022F84 File Offset: 0x00021184
		public int X
		{
			[CompilerGenerated]
			get
			{
				return this.<X>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<X>k__BackingField = value;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06001087 RID: 4231 RVA: 0x00022F8D File Offset: 0x0002118D
		// (set) Token: 0x06001088 RID: 4232 RVA: 0x00022F95 File Offset: 0x00021195
		public int Y
		{
			[CompilerGenerated]
			get
			{
				return this.<Y>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<Y>k__BackingField = value;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06001089 RID: 4233 RVA: 0x00022F9E File Offset: 0x0002119E
		// (set) Token: 0x0600108A RID: 4234 RVA: 0x00022FA6 File Offset: 0x000211A6
		public ButtonState LeftButton
		{
			[CompilerGenerated]
			get
			{
				return this.<LeftButton>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<LeftButton>k__BackingField = value;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x0600108B RID: 4235 RVA: 0x00022FAF File Offset: 0x000211AF
		// (set) Token: 0x0600108C RID: 4236 RVA: 0x00022FB7 File Offset: 0x000211B7
		public ButtonState RightButton
		{
			[CompilerGenerated]
			get
			{
				return this.<RightButton>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<RightButton>k__BackingField = value;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600108D RID: 4237 RVA: 0x00022FC0 File Offset: 0x000211C0
		// (set) Token: 0x0600108E RID: 4238 RVA: 0x00022FC8 File Offset: 0x000211C8
		public ButtonState MiddleButton
		{
			[CompilerGenerated]
			get
			{
				return this.<MiddleButton>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<MiddleButton>k__BackingField = value;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600108F RID: 4239 RVA: 0x00022FD1 File Offset: 0x000211D1
		// (set) Token: 0x06001090 RID: 4240 RVA: 0x00022FD9 File Offset: 0x000211D9
		public ButtonState XButton1
		{
			[CompilerGenerated]
			get
			{
				return this.<XButton1>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<XButton1>k__BackingField = value;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06001091 RID: 4241 RVA: 0x00022FE2 File Offset: 0x000211E2
		// (set) Token: 0x06001092 RID: 4242 RVA: 0x00022FEA File Offset: 0x000211EA
		public ButtonState XButton2
		{
			[CompilerGenerated]
			get
			{
				return this.<XButton2>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<XButton2>k__BackingField = value;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06001093 RID: 4243 RVA: 0x00022FF3 File Offset: 0x000211F3
		// (set) Token: 0x06001094 RID: 4244 RVA: 0x00022FFB File Offset: 0x000211FB
		public int ScrollWheelValue
		{
			[CompilerGenerated]
			get
			{
				return this.<ScrollWheelValue>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<ScrollWheelValue>k__BackingField = value;
			}
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x00023004 File Offset: 0x00021204
		public MouseState(int x, int y, int scrollWheel, ButtonState leftButton, ButtonState middleButton, ButtonState rightButton, ButtonState xButton1, ButtonState xButton2)
		{
			this = default(MouseState);
			this.X = x;
			this.Y = y;
			this.ScrollWheelValue = scrollWheel;
			this.LeftButton = leftButton;
			this.MiddleButton = middleButton;
			this.RightButton = rightButton;
			this.XButton1 = xButton1;
			this.XButton2 = xButton2;
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x00023058 File Offset: 0x00021258
		public static bool operator ==(MouseState left, MouseState right)
		{
			return left.X == right.X && left.Y == right.Y && left.LeftButton == right.LeftButton && left.MiddleButton == right.MiddleButton && left.RightButton == right.RightButton && left.ScrollWheelValue == right.ScrollWheelValue && left.XButton1 == right.XButton1 && left.XButton2 == right.XButton2;
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x000230E7 File Offset: 0x000212E7
		public static bool operator !=(MouseState left, MouseState right)
		{
			return !(left == right);
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x000230F3 File Offset: 0x000212F3
		public override bool Equals(object obj)
		{
			return obj is MouseState && this == (MouseState)obj;
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x00023110 File Offset: 0x00021310
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x00023124 File Offset: 0x00021324
		public override string ToString()
		{
			string text = string.Empty;
			if (this.LeftButton == ButtonState.Pressed)
			{
				text = "Left";
			}
			if (this.RightButton == ButtonState.Pressed)
			{
				if (text.Length > 0)
				{
					text += " ";
				}
				text += "Right";
			}
			if (this.MiddleButton == ButtonState.Pressed)
			{
				if (text.Length > 0)
				{
					text += " ";
				}
				text += "Middle";
			}
			if (this.XButton1 == ButtonState.Pressed)
			{
				if (text.Length > 0)
				{
					text += " ";
				}
				text += "XButton1";
			}
			if (this.XButton2 == ButtonState.Pressed)
			{
				if (text.Length > 0)
				{
					text += " ";
				}
				text += "XButton2";
			}
			if (string.IsNullOrEmpty(text))
			{
				text = "None";
			}
			return string.Format("[MouseState X={0}, Y={1}, Buttons={2}, Wheel={3}]", new object[] { this.X, this.Y, text, this.ScrollWheelValue });
		}

		// Token: 0x04000764 RID: 1892
		[CompilerGenerated]
		private int <X>k__BackingField;

		// Token: 0x04000765 RID: 1893
		[CompilerGenerated]
		private int <Y>k__BackingField;

		// Token: 0x04000766 RID: 1894
		[CompilerGenerated]
		private ButtonState <LeftButton>k__BackingField;

		// Token: 0x04000767 RID: 1895
		[CompilerGenerated]
		private ButtonState <RightButton>k__BackingField;

		// Token: 0x04000768 RID: 1896
		[CompilerGenerated]
		private ButtonState <MiddleButton>k__BackingField;

		// Token: 0x04000769 RID: 1897
		[CompilerGenerated]
		private ButtonState <XButton1>k__BackingField;

		// Token: 0x0400076A RID: 1898
		[CompilerGenerated]
		private ButtonState <XButton2>k__BackingField;

		// Token: 0x0400076B RID: 1899
		[CompilerGenerated]
		private int <ScrollWheelValue>k__BackingField;
	}
}
