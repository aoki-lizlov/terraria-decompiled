using System;

namespace Terraria.Utilities.FileBrowser
{
	// Token: 0x020000DD RID: 221
	public class FileBrowser
	{
		// Token: 0x06001890 RID: 6288 RVA: 0x004E2E7D File Offset: 0x004E107D
		static FileBrowser()
		{
		}

		// Token: 0x06001891 RID: 6289 RVA: 0x004E2E8C File Offset: 0x004E108C
		public static string OpenFilePanel(string title, string extension)
		{
			ExtensionFilter[] array;
			if (!string.IsNullOrEmpty(extension))
			{
				(array = new ExtensionFilter[1])[0] = new ExtensionFilter("", new string[] { extension });
			}
			else
			{
				array = null;
			}
			ExtensionFilter[] array2 = array;
			return FileBrowser.OpenFilePanel(title, array2);
		}

		// Token: 0x06001892 RID: 6290 RVA: 0x004E2ECD File Offset: 0x004E10CD
		public static string OpenFilePanel(string title, ExtensionFilter[] extensions)
		{
			return FileBrowser._platformWrapper.OpenFilePanel(title, extensions);
		}

		// Token: 0x06001893 RID: 6291 RVA: 0x0000357B File Offset: 0x0000177B
		public FileBrowser()
		{
		}

		// Token: 0x040012E1 RID: 4833
		private static IFileBrowser _platformWrapper = new NativeFileDialog();
	}
}
