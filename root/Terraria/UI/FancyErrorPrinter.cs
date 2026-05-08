using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ReLogic.Content;
using Terraria.Utilities;

namespace Terraria.UI
{
	// Token: 0x020000E8 RID: 232
	public class FancyErrorPrinter
	{
		// Token: 0x060018EA RID: 6378 RVA: 0x004E5FF0 File Offset: 0x004E41F0
		public static void ShowFailedToLoadAssetError(Exception exception, string filePath)
		{
			bool flag = false;
			if (exception is UnauthorizedAccessException)
			{
				flag = true;
			}
			if (exception is FileNotFoundException)
			{
				flag = true;
			}
			if (exception is DirectoryNotFoundException)
			{
				flag = true;
			}
			if (exception is AssetLoadException)
			{
				flag = true;
			}
			if (!flag)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("Failed to load asset: \"" + filePath.Replace("/", "\\") + "\"!");
			List<string> list = new List<string>();
			list.Add("Try to verify/repair the game installation, the asset may be missing.");
			list.Add("If you are using an Anti-virus, please make sure it does not block Terraria in any way.");
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("Suggestions:");
			FancyErrorPrinter.AppendSuggestions(stringBuilder, list);
			stringBuilder.AppendLine();
			FancyErrorPrinter.IncludeOriginalMessage(stringBuilder, exception);
			FancyErrorPrinter.ShowTheBox(stringBuilder.ToString());
			Console.WriteLine(stringBuilder.ToString());
		}

		// Token: 0x060018EB RID: 6379 RVA: 0x004E60B0 File Offset: 0x004E42B0
		public static void ShowFileSavingFailError(Exception exception, string filePath)
		{
			bool flag = false;
			if (exception is UnauthorizedAccessException)
			{
				flag = true;
			}
			if (exception is FileNotFoundException)
			{
				flag = true;
			}
			if (exception is DirectoryNotFoundException)
			{
				flag = true;
			}
			if (!flag)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("Failed to create the file: \"" + filePath.Replace("/", "\\") + "\"!");
			List<string> list = new List<string>();
			list.Add("If you are using an Anti-virus, please make sure it does not block Terraria in any way.");
			list.Add("Try making sure your `Documents/My Games/Terraria` folder is not set to 'read-only'.");
			list.Add("Try to verify/repair the game installation.");
			if (filePath.ToLower().Contains("onedrive"))
			{
				list.Add("Try updating OneDrive.");
			}
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("Suggestions:");
			FancyErrorPrinter.AppendSuggestions(stringBuilder, list);
			stringBuilder.AppendLine();
			FancyErrorPrinter.IncludeOriginalMessage(stringBuilder, exception);
			FancyErrorPrinter.ShowTheBox(stringBuilder.ToString());
			Console.WriteLine(stringBuilder.ToString());
		}

		// Token: 0x060018EC RID: 6380 RVA: 0x004E6190 File Offset: 0x004E4390
		public static void ShowDirectoryCreationFailError(Exception exception, string folderPath)
		{
			bool flag = false;
			if (exception is UnauthorizedAccessException)
			{
				flag = true;
			}
			if (exception is FileNotFoundException)
			{
				flag = true;
			}
			if (exception is DirectoryNotFoundException)
			{
				flag = true;
			}
			if (!flag)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("Failed to create the folder: \"" + folderPath.Replace("/", "\\") + "\"!");
			List<string> list = new List<string>();
			list.Add("If you are using an Anti-virus, please make sure it does not block Terraria in any way.");
			list.Add("Try making sure your `Documents/My Games/Terraria` folder is not set to 'read-only'.");
			list.Add("Try to verify/repair the game installation.");
			if (folderPath.ToLower().Contains("onedrive"))
			{
				list.Add("Try updating OneDrive.");
			}
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("Suggestions:");
			FancyErrorPrinter.AppendSuggestions(stringBuilder, list);
			stringBuilder.AppendLine();
			FancyErrorPrinter.IncludeOriginalMessage(stringBuilder, exception);
			FancyErrorPrinter.ShowTheBox(stringBuilder.ToString());
			Console.WriteLine(exception);
		}

		// Token: 0x060018ED RID: 6381 RVA: 0x004E6268 File Offset: 0x004E4468
		private static void IncludeOriginalMessage(StringBuilder text, Exception exception)
		{
			text.AppendLine("The original Error below");
			text.Append(exception);
		}

		// Token: 0x060018EE RID: 6382 RVA: 0x004E6280 File Offset: 0x004E4480
		private static void AppendSuggestions(StringBuilder text, List<string> suggestions)
		{
			for (int i = 0; i < suggestions.Count; i++)
			{
				string text2 = suggestions[i];
				text.AppendLine((i + 1).ToString() + ". " + text2);
			}
		}

		// Token: 0x060018EF RID: 6383 RVA: 0x004E62C3 File Offset: 0x004E44C3
		private static void ShowTheBox(string preparedMessage)
		{
			if (!Main.dedServ)
			{
				MessageBox.Show(preparedMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		// Token: 0x060018F0 RID: 6384 RVA: 0x0000357B File Offset: 0x0000177B
		public FancyErrorPrinter()
		{
		}
	}
}
