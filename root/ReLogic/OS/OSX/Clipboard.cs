using System;
using System.Diagnostics;
using ReLogic.OS.Base;

namespace ReLogic.OS.OSX
{
	// Token: 0x0200006F RID: 111
	internal class Clipboard : Clipboard
	{
		// Token: 0x06000276 RID: 630 RVA: 0x0000A484 File Offset: 0x00008684
		protected override string GetClipboard()
		{
			string text2;
			try
			{
				string text;
				using (Process process = new Process())
				{
					process.StartInfo = new ProcessStartInfo("pbpaste", "-pboard general")
					{
						UseShellExecute = false,
						RedirectStandardOutput = true
					};
					process.Start();
					text = process.StandardOutput.ReadToEnd();
					process.WaitForExit();
				}
				text2 = text;
			}
			catch (Exception)
			{
				text2 = "";
			}
			return text2;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000A50C File Offset: 0x0000870C
		protected override void SetClipboard(string text)
		{
			try
			{
				using (Process process = new Process())
				{
					process.StartInfo = new ProcessStartInfo("pbcopy", "-pboard general -Prefer txt")
					{
						UseShellExecute = false,
						RedirectStandardOutput = false,
						RedirectStandardInput = true
					};
					process.Start();
					process.StandardInput.Write(text);
					process.StandardInput.Close();
					process.WaitForExit();
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00009B4C File Offset: 0x00007D4C
		public Clipboard()
		{
		}
	}
}
