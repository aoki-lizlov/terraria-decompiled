using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000717 RID: 1815
	internal class ManagedErrorInfo : IErrorInfo
	{
		// Token: 0x060040DC RID: 16604 RVA: 0x000E19C7 File Offset: 0x000DFBC7
		public ManagedErrorInfo(Exception e)
		{
			this.m_Exception = e;
		}

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x060040DD RID: 16605 RVA: 0x000E19D6 File Offset: 0x000DFBD6
		public Exception Exception
		{
			get
			{
				return this.m_Exception;
			}
		}

		// Token: 0x060040DE RID: 16606 RVA: 0x000E19DE File Offset: 0x000DFBDE
		public int GetGUID(out Guid guid)
		{
			guid = Guid.Empty;
			return 0;
		}

		// Token: 0x060040DF RID: 16607 RVA: 0x000E19EC File Offset: 0x000DFBEC
		public int GetSource(out string source)
		{
			source = this.m_Exception.Source;
			return 0;
		}

		// Token: 0x060040E0 RID: 16608 RVA: 0x000E19FC File Offset: 0x000DFBFC
		public int GetDescription(out string description)
		{
			description = this.m_Exception.Message;
			return 0;
		}

		// Token: 0x060040E1 RID: 16609 RVA: 0x000E1A0C File Offset: 0x000DFC0C
		public int GetHelpFile(out string helpFile)
		{
			helpFile = this.m_Exception.HelpLink;
			return 0;
		}

		// Token: 0x060040E2 RID: 16610 RVA: 0x000E1A1C File Offset: 0x000DFC1C
		public int GetHelpContext(out uint helpContext)
		{
			helpContext = 0U;
			return 0;
		}

		// Token: 0x04002B4D RID: 11085
		private Exception m_Exception;
	}
}
