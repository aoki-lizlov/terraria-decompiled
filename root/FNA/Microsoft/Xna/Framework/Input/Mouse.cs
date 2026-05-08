using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Xna.Framework.Input
{
	// Token: 0x0200006A RID: 106
	public static class Mouse
	{
		// Token: 0x170001CA RID: 458
		// (get) Token: 0x0600107D RID: 4221 RVA: 0x00022E53 File Offset: 0x00021053
		// (set) Token: 0x0600107E RID: 4222 RVA: 0x00022E5A File Offset: 0x0002105A
		public static IntPtr WindowHandle
		{
			[CompilerGenerated]
			get
			{
				return Mouse.<WindowHandle>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				Mouse.<WindowHandle>k__BackingField = value;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x0600107F RID: 4223 RVA: 0x00022E62 File Offset: 0x00021062
		// (set) Token: 0x06001080 RID: 4224 RVA: 0x00022E73 File Offset: 0x00021073
		public static bool IsRelativeMouseModeEXT
		{
			get
			{
				return FNAPlatform.GetRelativeMouseMode(Mouse.WindowHandle);
			}
			set
			{
				FNAPlatform.SetRelativeMouseMode(Mouse.WindowHandle, value);
			}
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x00022E88 File Offset: 0x00021088
		public static MouseState GetState()
		{
			int num;
			int num2;
			ButtonState buttonState;
			ButtonState buttonState2;
			ButtonState buttonState3;
			ButtonState buttonState4;
			ButtonState buttonState5;
			FNAPlatform.GetMouseState(Mouse.WindowHandle, out num, out num2, out buttonState, out buttonState2, out buttonState3, out buttonState4, out buttonState5);
			num = (int)((double)num * (double)Mouse.INTERNAL_BackBufferWidth / (double)Mouse.INTERNAL_WindowWidth);
			num2 = (int)((double)num2 * (double)Mouse.INTERNAL_BackBufferHeight / (double)Mouse.INTERNAL_WindowHeight);
			return new MouseState(num, num2, Mouse.INTERNAL_MouseWheel, buttonState, buttonState2, buttonState3, buttonState4, buttonState5);
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x00022EEC File Offset: 0x000210EC
		public static void SetPosition(int x, int y)
		{
			if (Mouse.IsRelativeMouseModeEXT)
			{
				return;
			}
			x = (int)((double)x * (double)Mouse.INTERNAL_WindowWidth / (double)Mouse.INTERNAL_BackBufferWidth);
			y = (int)((double)y * (double)Mouse.INTERNAL_WindowHeight / (double)Mouse.INTERNAL_BackBufferHeight);
			FNAPlatform.SetMousePosition(Mouse.WindowHandle, x, y);
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x00022F38 File Offset: 0x00021138
		internal static void INTERNAL_onClicked(int button)
		{
			if (Mouse.ClickedEXT != null)
			{
				Mouse.ClickedEXT(button);
			}
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x00022F4C File Offset: 0x0002114C
		// Note: this type is marked as 'beforefieldinit'.
		static Mouse()
		{
		}

		// Token: 0x0400075D RID: 1885
		[CompilerGenerated]
		private static IntPtr <WindowHandle>k__BackingField;

		// Token: 0x0400075E RID: 1886
		internal static int INTERNAL_WindowWidth = GraphicsDeviceManager.DefaultBackBufferWidth;

		// Token: 0x0400075F RID: 1887
		internal static int INTERNAL_WindowHeight = GraphicsDeviceManager.DefaultBackBufferHeight;

		// Token: 0x04000760 RID: 1888
		internal static int INTERNAL_BackBufferWidth = GraphicsDeviceManager.DefaultBackBufferWidth;

		// Token: 0x04000761 RID: 1889
		internal static int INTERNAL_BackBufferHeight = GraphicsDeviceManager.DefaultBackBufferHeight;

		// Token: 0x04000762 RID: 1890
		internal static int INTERNAL_MouseWheel = 0;

		// Token: 0x04000763 RID: 1891
		public static Action<int> ClickedEXT;
	}
}
