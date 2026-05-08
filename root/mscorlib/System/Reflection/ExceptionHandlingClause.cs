using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020008BE RID: 2238
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public class ExceptionHandlingClause
	{
		// Token: 0x06004C0A RID: 19466 RVA: 0x000025BE File Offset: 0x000007BE
		protected ExceptionHandlingClause()
		{
		}

		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x06004C0B RID: 19467 RVA: 0x000F2A47 File Offset: 0x000F0C47
		public virtual Type CatchType
		{
			get
			{
				return this.catch_type;
			}
		}

		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x06004C0C RID: 19468 RVA: 0x000F2A4F File Offset: 0x000F0C4F
		public virtual int FilterOffset
		{
			get
			{
				return this.filter_offset;
			}
		}

		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x06004C0D RID: 19469 RVA: 0x000F2A57 File Offset: 0x000F0C57
		public virtual ExceptionHandlingClauseOptions Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x06004C0E RID: 19470 RVA: 0x000F2A5F File Offset: 0x000F0C5F
		public virtual int HandlerLength
		{
			get
			{
				return this.handler_length;
			}
		}

		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x06004C0F RID: 19471 RVA: 0x000F2A67 File Offset: 0x000F0C67
		public virtual int HandlerOffset
		{
			get
			{
				return this.handler_offset;
			}
		}

		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x06004C10 RID: 19472 RVA: 0x000F2A6F File Offset: 0x000F0C6F
		public virtual int TryLength
		{
			get
			{
				return this.try_length;
			}
		}

		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x06004C11 RID: 19473 RVA: 0x000F2A77 File Offset: 0x000F0C77
		public virtual int TryOffset
		{
			get
			{
				return this.try_offset;
			}
		}

		// Token: 0x06004C12 RID: 19474 RVA: 0x000F2A80 File Offset: 0x000F0C80
		public override string ToString()
		{
			string text = string.Format("Flags={0}, TryOffset={1}, TryLength={2}, HandlerOffset={3}, HandlerLength={4}", new object[] { this.flags, this.try_offset, this.try_length, this.handler_offset, this.handler_length });
			if (this.catch_type != null)
			{
				text = string.Format("{0}, CatchType={1}", text, this.catch_type);
			}
			if (this.flags == ExceptionHandlingClauseOptions.Filter)
			{
				text = string.Format("{0}, FilterOffset={1}", text, this.filter_offset);
			}
			return text;
		}

		// Token: 0x04002FAA RID: 12202
		internal Type catch_type;

		// Token: 0x04002FAB RID: 12203
		internal int filter_offset;

		// Token: 0x04002FAC RID: 12204
		internal ExceptionHandlingClauseOptions flags;

		// Token: 0x04002FAD RID: 12205
		internal int try_offset;

		// Token: 0x04002FAE RID: 12206
		internal int try_length;

		// Token: 0x04002FAF RID: 12207
		internal int handler_offset;

		// Token: 0x04002FB0 RID: 12208
		internal int handler_length;
	}
}
