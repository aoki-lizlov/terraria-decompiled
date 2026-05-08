using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using SDL3;

namespace Terraria.Utilities
{
	// Token: 0x020000C6 RID: 198
	public static class MessageBox
	{
		// Token: 0x060017E8 RID: 6120 RVA: 0x004E0968 File Offset: 0x004DEB68
		public static DialogResult Show(string message, string title, MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.None)
		{
			switch (buttons)
			{
			case MessageBoxButtons.OK:
				return MessageBox.Show(message, title, new DialogResult[] { DialogResult.OK }, icon, 0, 0);
			case MessageBoxButtons.OKCancel:
				return MessageBox.Show(message, title, new DialogResult[]
				{
					DialogResult.Cancel,
					DialogResult.OK
				}, icon, 0, 1);
			case MessageBoxButtons.YesNoCancel:
				return MessageBox.Show(message, title, new DialogResult[]
				{
					DialogResult.Yes,
					DialogResult.No,
					DialogResult.Cancel
				}, icon, 0, 2);
			case MessageBoxButtons.YesNo:
				return MessageBox.Show(message, title, new DialogResult[]
				{
					DialogResult.No,
					DialogResult.Yes
				}, icon, 0, -1);
			case MessageBoxButtons.RetryCancel:
				return MessageBox.Show(message, title, new DialogResult[]
				{
					DialogResult.Cancel,
					DialogResult.Retry
				}, icon, 0, 1);
			default:
				throw new ArgumentOutOfRangeException("buttons");
			}
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x004E0A18 File Offset: 0x004DEC18
		private static DialogResult Show(string message, string title, DialogResult[] buttons, MessageBoxIcon icon, int returnButtonIndex = -1, int cancelButtonIndex = -1)
		{
			string[] array = buttons.Select((DialogResult r) => Enum.GetName(typeof(DialogResult), r)).ToArray<string>();
			int num = MessageBox.Show(message, title, array, -1, -1, icon);
			if (num >= 0)
			{
				return buttons[num];
			}
			return DialogResult.None;
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x004E0A68 File Offset: 0x004DEC68
		private unsafe static byte* AllocUTF8(string s)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(s + "\0");
			byte* ptr = (byte*)(void*)Marshal.AllocHGlobal(bytes.Length);
			Marshal.Copy(bytes, 0, (IntPtr)((void*)ptr), bytes.Length);
			return ptr;
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x004E0AAC File Offset: 0x004DECAC
		public unsafe static int Show(string message, string title, string[] buttonLabels, int returnButtonIndex = -1, int cancelButtonIndex = -1, MessageBoxIcon icon = MessageBoxIcon.None)
		{
			SDL.SDL_MessageBoxButtonData* ptr = (SDL.SDL_MessageBoxButtonData*)(void*)Marshal.AllocHGlobal(Marshal.SizeOf(typeof(SDL.SDL_MessageBoxButtonData)) * buttonLabels.Length);
			for (int i = 0; i < buttonLabels.Length; i++)
			{
				ptr[i] = new SDL.SDL_MessageBoxButtonData
				{
					flags = ((i == returnButtonIndex) ? SDL.SDL_MessageBoxButtonFlags.SDL_MESSAGEBOX_BUTTON_RETURNKEY_DEFAULT : ((i == cancelButtonIndex) ? SDL.SDL_MessageBoxButtonFlags.SDL_MESSAGEBOX_BUTTON_ESCAPEKEY_DEFAULT : ((SDL.SDL_MessageBoxButtonFlags)0U))),
					buttonID = i,
					text = MessageBox.AllocUTF8(buttonLabels[i])
				};
			}
			SDL.SDL_MessageBoxData sdl_MessageBoxData = new SDL.SDL_MessageBoxData
			{
				flags = (SDL.SDL_MessageBoxFlags)(icon | (MessageBoxIcon)128U),
				message = MessageBox.AllocUTF8(message),
				title = MessageBox.AllocUTF8(title),
				buttons = ptr,
				numbuttons = buttonLabels.Length
			};
			int num2;
			try
			{
				int num;
				if (!SDL.SDL_ShowMessageBox(ref sdl_MessageBoxData, out num))
				{
					num2 = -1;
				}
				else
				{
					num2 = num;
				}
			}
			finally
			{
				for (int j = 0; j < buttonLabels.Length; j++)
				{
					Marshal.FreeHGlobal((IntPtr)((void*)ptr[j].text));
				}
				Marshal.FreeHGlobal((IntPtr)((void*)sdl_MessageBoxData.message));
				Marshal.FreeHGlobal((IntPtr)((void*)sdl_MessageBoxData.title));
			}
			return num2;
		}

		// Token: 0x020006EF RID: 1775
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06003FA1 RID: 16289 RVA: 0x0069B0D6 File Offset: 0x006992D6
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06003FA2 RID: 16290 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x06003FA3 RID: 16291 RVA: 0x0069B0E2 File Offset: 0x006992E2
			internal string <Show>b__1_0(DialogResult r)
			{
				return Enum.GetName(typeof(DialogResult), r);
			}

			// Token: 0x04006806 RID: 26630
			public static readonly MessageBox.<>c <>9 = new MessageBox.<>c();

			// Token: 0x04006807 RID: 26631
			public static Func<DialogResult, string> <>9__1_0;
		}
	}
}
