using System;
using System.Collections.Generic;

namespace Microsoft.Xna.Framework.Input
{
	// Token: 0x02000066 RID: 102
	public static class Keyboard
	{
		// Token: 0x06001069 RID: 4201 RVA: 0x0002285E File Offset: 0x00020A5E
		public static KeyboardState GetState()
		{
			return new KeyboardState(Keyboard.keys);
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x0002285E File Offset: 0x00020A5E
		public static KeyboardState GetState(PlayerIndex playerIndex)
		{
			return new KeyboardState(Keyboard.keys);
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x0002286A File Offset: 0x00020A6A
		public static Keys GetKeyFromScancodeEXT(Keys scancode)
		{
			return FNAPlatform.GetKeyFromScancode(scancode);
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x00022877 File Offset: 0x00020A77
		// Note: this type is marked as 'beforefieldinit'.
		static Keyboard()
		{
		}

		// Token: 0x040006AF RID: 1711
		internal static List<Keys> keys = new List<Keys>();
	}
}
