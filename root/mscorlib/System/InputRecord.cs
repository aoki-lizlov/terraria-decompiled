using System;

namespace System
{
	// Token: 0x02000243 RID: 579
	internal struct InputRecord
	{
		// Token: 0x040018A7 RID: 6311
		public short EventType;

		// Token: 0x040018A8 RID: 6312
		public bool KeyDown;

		// Token: 0x040018A9 RID: 6313
		public short RepeatCount;

		// Token: 0x040018AA RID: 6314
		public short VirtualKeyCode;

		// Token: 0x040018AB RID: 6315
		public short VirtualScanCode;

		// Token: 0x040018AC RID: 6316
		public char Character;

		// Token: 0x040018AD RID: 6317
		public int ControlKeyState;

		// Token: 0x040018AE RID: 6318
		private int pad1;

		// Token: 0x040018AF RID: 6319
		private bool pad2;
	}
}
