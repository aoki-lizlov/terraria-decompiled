using System;

namespace Microsoft.Xna.Framework.Input.Touch
{
	// Token: 0x02000070 RID: 112
	[Flags]
	public enum GestureType
	{
		// Token: 0x04000787 RID: 1927
		None = 0,
		// Token: 0x04000788 RID: 1928
		Tap = 1,
		// Token: 0x04000789 RID: 1929
		DoubleTap = 2,
		// Token: 0x0400078A RID: 1930
		Hold = 4,
		// Token: 0x0400078B RID: 1931
		HorizontalDrag = 8,
		// Token: 0x0400078C RID: 1932
		VerticalDrag = 16,
		// Token: 0x0400078D RID: 1933
		FreeDrag = 32,
		// Token: 0x0400078E RID: 1934
		Pinch = 64,
		// Token: 0x0400078F RID: 1935
		Flick = 128,
		// Token: 0x04000790 RID: 1936
		DragComplete = 256,
		// Token: 0x04000791 RID: 1937
		PinchComplete = 512
	}
}
