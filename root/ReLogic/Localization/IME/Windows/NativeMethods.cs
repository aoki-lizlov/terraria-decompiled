using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ReLogic.Localization.IME.Windows
{
	// Token: 0x02000080 RID: 128
	internal static class NativeMethods
	{
		// Token: 0x060002DA RID: 730
		[DllImport("ReLogic.Native.dll", CharSet = 3)]
		[return: MarshalAs(3)]
		public static extern bool ImeUi_Initialize(IntPtr hWnd, [MarshalAs(3)] bool bDisabled = false);

		// Token: 0x060002DB RID: 731
		[DllImport("ReLogic.Native.dll", CharSet = 3)]
		[return: MarshalAs(3)]
		public static extern bool ImeUi_Uninitialize();

		// Token: 0x060002DC RID: 732
		[DllImport("ReLogic.Native.dll", CharSet = 3, EntryPoint = "ImeUi_EnableIme")]
		public static extern void ImeUi_Enable([MarshalAs(3)] bool bEnable);

		// Token: 0x060002DD RID: 733
		[DllImport("ReLogic.Native.dll", CharSet = 3)]
		[return: MarshalAs(3)]
		public static extern bool ImeUi_IsEnabled();

		// Token: 0x060002DE RID: 734
		[DllImport("ReLogic.Native.dll", CharSet = 3)]
		public static extern IntPtr ImeUi_ProcessMessage(IntPtr hWnd, int msg, IntPtr wParam, ref IntPtr lParam, [MarshalAs(3)] ref bool trapped);

		// Token: 0x060002DF RID: 735
		[DllImport("ReLogic.Native.dll", CharSet = 3)]
		public static extern IntPtr ImeUi_GetCompositionString();

		// Token: 0x060002E0 RID: 736
		[DllImport("ReLogic.Native.dll", CharSet = 3)]
		public static extern IntPtr ImeUi_GetCandidate(uint index);

		// Token: 0x060002E1 RID: 737
		[DllImport("ReLogic.Native.dll", CharSet = 3)]
		public static extern uint ImeUi_GetCandidateSelection();

		// Token: 0x060002E2 RID: 738
		[DllImport("ReLogic.Native.dll", CharSet = 3)]
		public static extern void ImeUi_SetCandidateSelection(uint index);

		// Token: 0x060002E3 RID: 739
		[DllImport("ReLogic.Native.dll", CharSet = 3)]
		public static extern uint ImeUi_GetCandidateCount();

		// Token: 0x060002E4 RID: 740
		[DllImport("ReLogic.Native.dll", CharSet = 3)]
		public static extern void ImeUi_FinalizeString([MarshalAs(3)] bool bSend = false);

		// Token: 0x060002E5 RID: 741
		[DllImport("ReLogic.Native.dll", CharSet = 3)]
		public static extern uint ImeUi_GetCandidatePageSize();

		// Token: 0x060002E6 RID: 742
		[DllImport("ReLogic.Native.dll", CharSet = 3, EntryPoint = "ImeUi_IsShowCandListWindow")]
		[return: MarshalAs(3)]
		public static extern bool ImeUi_IsCandidateListVisible();

		// Token: 0x060002E7 RID: 743
		[DllImport("ReLogic.Native.dll", CharSet = 3, EntryPoint = "ImeUi_IgnoreHotKey")]
		[return: MarshalAs(3)]
		public static extern bool ImeUi_ShouldIgnoreHotKey(ref Message message);

		// Token: 0x060002E8 RID: 744
		[DllImport("ReLogic.Native.dll")]
		public static extern ushort ImeUi_GetPrimaryLanguage();

		// Token: 0x060002E9 RID: 745
		[DllImport("Imm32.dll")]
		public static extern uint ImmGetVirtualKey(IntPtr hWnd);

		// Token: 0x060002EA RID: 746
		[DllImport("user32.dll")]
		public static extern int SendInput(int nInputs, NativeMethods.INPUT[] pInputs, int cbSize);

		// Token: 0x040004E0 RID: 1248
		private const string DLL_NAME = "ReLogic.Native.dll";

		// Token: 0x020000E0 RID: 224
		public struct INPUT
		{
			// Token: 0x040005FF RID: 1535
			public uint Type;

			// Token: 0x04000600 RID: 1536
			public NativeMethods.INPUTUNION Data;
		}

		// Token: 0x020000E1 RID: 225
		[StructLayout(2)]
		public struct INPUTUNION
		{
			// Token: 0x04000601 RID: 1537
			[FieldOffset(0)]
			public NativeMethods.MOUSEINPUT MouseInput;

			// Token: 0x04000602 RID: 1538
			[FieldOffset(0)]
			public NativeMethods.KEYBDINPUT KeyboardInput;

			// Token: 0x04000603 RID: 1539
			[FieldOffset(0)]
			public NativeMethods.HARDWAREINPUT HardwareInput;
		}

		// Token: 0x020000E2 RID: 226
		public struct KEYBDINPUT
		{
			// Token: 0x04000604 RID: 1540
			public ushort VirtualKey;

			// Token: 0x04000605 RID: 1541
			public ushort Scan;

			// Token: 0x04000606 RID: 1542
			public uint Flags;

			// Token: 0x04000607 RID: 1543
			public uint Time;

			// Token: 0x04000608 RID: 1544
			public IntPtr ExtraInfo;
		}

		// Token: 0x020000E3 RID: 227
		public struct HARDWAREINPUT
		{
			// Token: 0x04000609 RID: 1545
			public uint Msg;

			// Token: 0x0400060A RID: 1546
			public ushort ParamL;

			// Token: 0x0400060B RID: 1547
			public ushort ParamH;
		}

		// Token: 0x020000E4 RID: 228
		public struct MOUSEINPUT
		{
			// Token: 0x0400060C RID: 1548
			public int X;

			// Token: 0x0400060D RID: 1549
			public int Y;

			// Token: 0x0400060E RID: 1550
			public uint MouseData;

			// Token: 0x0400060F RID: 1551
			public uint Flags;

			// Token: 0x04000610 RID: 1552
			public uint Time;

			// Token: 0x04000611 RID: 1553
			public IntPtr ExtraInfo;
		}
	}
}
