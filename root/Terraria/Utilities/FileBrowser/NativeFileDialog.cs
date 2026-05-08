using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Terraria.Utilities.FileBrowser
{
	// Token: 0x020000DA RID: 218
	public class NativeFileDialog : IFileBrowser
	{
		// Token: 0x0600188C RID: 6284 RVA: 0x004E2E1C File Offset: 0x004E101C
		public string OpenFilePanel(string title, ExtensionFilter[] extensions)
		{
			string[] array = extensions.SelectMany((ExtensionFilter x) => x.Extensions).ToArray<string>();
			string text;
			if (nativefiledialog.NFD_OpenDialog(string.Join(",", array), null, out text) == nativefiledialog.nfdresult_t.NFD_OKAY)
			{
				return text;
			}
			return null;
		}

		// Token: 0x0600188D RID: 6285 RVA: 0x0000357B File Offset: 0x0000177B
		public NativeFileDialog()
		{
		}

		// Token: 0x020006FD RID: 1789
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06003FDA RID: 16346 RVA: 0x0069BDC8 File Offset: 0x00699FC8
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06003FDB RID: 16347 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x06003FDC RID: 16348 RVA: 0x0069BDD4 File Offset: 0x00699FD4
			internal IEnumerable<string> <OpenFilePanel>b__0_0(ExtensionFilter x)
			{
				return x.Extensions;
			}

			// Token: 0x0400684C RID: 26700
			public static readonly NativeFileDialog.<>c <>9 = new NativeFileDialog.<>c();

			// Token: 0x0400684D RID: 26701
			public static Func<ExtensionFilter, IEnumerable<string>> <>9__0_0;
		}
	}
}
