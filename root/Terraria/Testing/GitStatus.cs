using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using Terraria.Utilities;

namespace Terraria.Testing
{
	// Token: 0x02000111 RID: 273
	public static class GitStatus
	{
		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06001AC8 RID: 6856 RVA: 0x004F848C File Offset: 0x004F668C
		public static string GitSHA
		{
			get
			{
				GitStatus.Init();
				return GitStatus._gitSHA;
			}
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x004F8498 File Offset: 0x004F6698
		private static void Init()
		{
			if (GitStatus._init)
			{
				return;
			}
			GitStatus._init = true;
			if (!GitStatus.HasGitFolder())
			{
				return;
			}
			try
			{
				GitStatus._gitSHA = GitStatus.GitRevParse();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "git command failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		// Token: 0x06001ACA RID: 6858 RVA: 0x004F84F0 File Offset: 0x004F66F0
		private static string GitRevParse()
		{
			string text2;
			using (Process process = new Process())
			{
				process.StartInfo = new ProcessStartInfo("git", "rev-parse HEAD")
				{
					UseShellExecute = false,
					RedirectStandardOutput = true,
					CreateNoWindow = true
				};
				process.Start();
				string text = process.StandardOutput.ReadToEnd().Trim();
				if (!Regex.IsMatch(text, "^[0-9a-f]+$"))
				{
					throw new Exception(text);
				}
				text2 = text;
			}
			return text2;
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x004F8578 File Offset: 0x004F6778
		private static bool HasGitFolder()
		{
			try
			{
				string text = Path.GetDirectoryName(Path.GetFullPath("."));
				while (!Directory.Exists(Path.Combine(text, ".git")))
				{
					if ((text = Path.GetDirectoryName(text)) == null)
					{
						return false;
					}
				}
				return true;
			}
			catch (Exception)
			{
			}
			return false;
		}

		// Token: 0x06001ACC RID: 6860 RVA: 0x004F85D0 File Offset: 0x004F67D0
		// Note: this type is marked as 'beforefieldinit'.
		static GitStatus()
		{
		}

		// Token: 0x0400151C RID: 5404
		private static string _gitSHA = "";

		// Token: 0x0400151D RID: 5405
		private static bool _init;
	}
}
