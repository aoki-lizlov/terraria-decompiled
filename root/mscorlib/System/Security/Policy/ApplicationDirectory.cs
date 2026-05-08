using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	// Token: 0x020003CF RID: 975
	[ComVisible(true)]
	[Serializable]
	public sealed class ApplicationDirectory : EvidenceBase, IBuiltInEvidence
	{
		// Token: 0x0600297D RID: 10621 RVA: 0x00098019 File Offset: 0x00096219
		public ApplicationDirectory(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length < 1)
			{
				throw new FormatException(Locale.GetText("Empty"));
			}
			this.directory = name;
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x0600297E RID: 10622 RVA: 0x0009804F File Offset: 0x0009624F
		public string Directory
		{
			get
			{
				return this.directory;
			}
		}

		// Token: 0x0600297F RID: 10623 RVA: 0x00098057 File Offset: 0x00096257
		public object Copy()
		{
			return new ApplicationDirectory(this.Directory);
		}

		// Token: 0x06002980 RID: 10624 RVA: 0x00098064 File Offset: 0x00096264
		public override bool Equals(object o)
		{
			ApplicationDirectory applicationDirectory = o as ApplicationDirectory;
			if (applicationDirectory != null)
			{
				this.ThrowOnInvalid(applicationDirectory.directory);
				return this.directory == applicationDirectory.directory;
			}
			return false;
		}

		// Token: 0x06002981 RID: 10625 RVA: 0x0009809A File Offset: 0x0009629A
		public override int GetHashCode()
		{
			return this.Directory.GetHashCode();
		}

		// Token: 0x06002982 RID: 10626 RVA: 0x000980A8 File Offset: 0x000962A8
		public override string ToString()
		{
			this.ThrowOnInvalid(this.Directory);
			SecurityElement securityElement = new SecurityElement("System.Security.Policy.ApplicationDirectory");
			securityElement.AddAttribute("version", "1");
			securityElement.AddChild(new SecurityElement("Directory", this.directory));
			return securityElement.ToString();
		}

		// Token: 0x06002983 RID: 10627 RVA: 0x000980F6 File Offset: 0x000962F6
		int IBuiltInEvidence.GetRequiredSize(bool verbose)
		{
			return (verbose ? 3 : 1) + this.directory.Length;
		}

		// Token: 0x06002984 RID: 10628 RVA: 0x0000408A File Offset: 0x0000228A
		[MonoTODO("IBuiltInEvidence")]
		int IBuiltInEvidence.InitFromBuffer(char[] buffer, int position)
		{
			return 0;
		}

		// Token: 0x06002985 RID: 10629 RVA: 0x0000408A File Offset: 0x0000228A
		[MonoTODO("IBuiltInEvidence")]
		int IBuiltInEvidence.OutputToBuffer(char[] buffer, int position, bool verbose)
		{
			return 0;
		}

		// Token: 0x06002986 RID: 10630 RVA: 0x0009810B File Offset: 0x0009630B
		private void ThrowOnInvalid(string appdir)
		{
			if (appdir.IndexOfAny(Path.InvalidPathChars) != -1)
			{
				throw new ArgumentException(string.Format(Locale.GetText("Invalid character(s) in directory {0}"), appdir), "other");
			}
		}

		// Token: 0x04001E1E RID: 7710
		private string directory;
	}
}
