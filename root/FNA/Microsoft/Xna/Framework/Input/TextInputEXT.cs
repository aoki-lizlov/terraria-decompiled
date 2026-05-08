using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.Xna.Framework.Input
{
	// Token: 0x0200006C RID: 108
	public static class TextInputEXT
	{
		// Token: 0x1400001D RID: 29
		// (add) Token: 0x0600109B RID: 4251 RVA: 0x0002323C File Offset: 0x0002143C
		// (remove) Token: 0x0600109C RID: 4252 RVA: 0x00023270 File Offset: 0x00021470
		public static event Action<char> TextInput
		{
			[CompilerGenerated]
			add
			{
				Action<char> action = TextInputEXT.TextInput;
				Action<char> action2;
				do
				{
					action2 = action;
					Action<char> action3 = (Action<char>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<char>>(ref TextInputEXT.TextInput, action3, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<char> action = TextInputEXT.TextInput;
				Action<char> action2;
				do
				{
					action2 = action;
					Action<char> action3 = (Action<char>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<char>>(ref TextInputEXT.TextInput, action3, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x0600109D RID: 4253 RVA: 0x000232A4 File Offset: 0x000214A4
		// (remove) Token: 0x0600109E RID: 4254 RVA: 0x000232D8 File Offset: 0x000214D8
		public static event Action<string, int, int> TextEditing
		{
			[CompilerGenerated]
			add
			{
				Action<string, int, int> action = TextInputEXT.TextEditing;
				Action<string, int, int> action2;
				do
				{
					action2 = action;
					Action<string, int, int> action3 = (Action<string, int, int>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<string, int, int>>(ref TextInputEXT.TextEditing, action3, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<string, int, int> action = TextInputEXT.TextEditing;
				Action<string, int, int> action2;
				do
				{
					action2 = action;
					Action<string, int, int> action3 = (Action<string, int, int>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<string, int, int>>(ref TextInputEXT.TextEditing, action3, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x0600109F RID: 4255 RVA: 0x0002330B File Offset: 0x0002150B
		// (set) Token: 0x060010A0 RID: 4256 RVA: 0x00023312 File Offset: 0x00021512
		public static IntPtr WindowHandle
		{
			[CompilerGenerated]
			get
			{
				return TextInputEXT.<WindowHandle>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				TextInputEXT.<WindowHandle>k__BackingField = value;
			}
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x0002331A File Offset: 0x0002151A
		public static bool IsTextInputActive()
		{
			return FNAPlatform.IsTextInputActive(TextInputEXT.WindowHandle);
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x0002332B File Offset: 0x0002152B
		public static bool IsScreenKeyboardShown()
		{
			return FNAPlatform.IsScreenKeyboardShown(TextInputEXT.WindowHandle);
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x0002333C File Offset: 0x0002153C
		public static bool IsScreenKeyboardShown(IntPtr window)
		{
			return FNAPlatform.IsScreenKeyboardShown(window);
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x00023349 File Offset: 0x00021549
		public static void StartTextInput()
		{
			FNAPlatform.StartTextInput(TextInputEXT.WindowHandle);
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x0002335A File Offset: 0x0002155A
		public static void StopTextInput()
		{
			FNAPlatform.StopTextInput(TextInputEXT.WindowHandle);
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x0002336B File Offset: 0x0002156B
		public static void SetInputRectangle(Rectangle rectangle)
		{
			FNAPlatform.SetTextInputRectangle(TextInputEXT.WindowHandle, rectangle);
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x0002337D File Offset: 0x0002157D
		internal static void OnTextInput(char c)
		{
			if (TextInputEXT.TextInput != null)
			{
				TextInputEXT.TextInput(c);
			}
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x00023391 File Offset: 0x00021591
		internal static void OnTextEditing(string text, int start, int length)
		{
			if (TextInputEXT.TextEditing != null)
			{
				TextInputEXT.TextEditing(text, start, length);
			}
		}

		// Token: 0x0400076C RID: 1900
		[CompilerGenerated]
		private static Action<char> TextInput;

		// Token: 0x0400076D RID: 1901
		[CompilerGenerated]
		private static Action<string, int, int> TextEditing;

		// Token: 0x0400076E RID: 1902
		[CompilerGenerated]
		private static IntPtr <WindowHandle>k__BackingField;
	}
}
