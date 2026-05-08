using System;
using ReLogic.OS.Base;
using SDL3;

namespace ReLogic.OS.Linux
{
	// Token: 0x02000074 RID: 116
	internal class Clipboard : Clipboard
	{
		// Token: 0x06000285 RID: 645 RVA: 0x0000A89E File Offset: 0x00008A9E
		protected override string GetClipboard()
		{
			return SDL.SDL_GetClipboardText();
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000A8A5 File Offset: 0x00008AA5
		protected override void SetClipboard(string text)
		{
			SDL.SDL_SetClipboardText(text);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00009B4C File Offset: 0x00007D4C
		public Clipboard()
		{
		}
	}
}
