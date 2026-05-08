using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020008BF RID: 2239
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public class LocalVariableInfo
	{
		// Token: 0x06004C13 RID: 19475 RVA: 0x000025BE File Offset: 0x000007BE
		protected LocalVariableInfo()
		{
		}

		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x06004C14 RID: 19476 RVA: 0x000F2B25 File Offset: 0x000F0D25
		public virtual bool IsPinned
		{
			get
			{
				return this.is_pinned;
			}
		}

		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x06004C15 RID: 19477 RVA: 0x000F2B2D File Offset: 0x000F0D2D
		public virtual int LocalIndex
		{
			get
			{
				return (int)this.position;
			}
		}

		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x06004C16 RID: 19478 RVA: 0x000F2B35 File Offset: 0x000F0D35
		public virtual Type LocalType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x06004C17 RID: 19479 RVA: 0x000F2B40 File Offset: 0x000F0D40
		public override string ToString()
		{
			if (this.is_pinned)
			{
				return string.Format("{0} ({1}) (pinned)", this.type, this.position);
			}
			return string.Format("{0} ({1})", this.type, this.position);
		}

		// Token: 0x04002FB1 RID: 12209
		internal Type type;

		// Token: 0x04002FB2 RID: 12210
		internal bool is_pinned;

		// Token: 0x04002FB3 RID: 12211
		internal ushort position;
	}
}
