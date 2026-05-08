using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020008C0 RID: 2240
	[ComVisible(true)]
	public class MethodBody
	{
		// Token: 0x06004C18 RID: 19480 RVA: 0x000025BE File Offset: 0x000007BE
		protected MethodBody()
		{
		}

		// Token: 0x06004C19 RID: 19481 RVA: 0x000F2B8C File Offset: 0x000F0D8C
		internal MethodBody(ExceptionHandlingClause[] clauses, LocalVariableInfo[] locals, byte[] il, bool init_locals, int sig_token, int max_stack)
		{
			this.clauses = clauses;
			this.locals = locals;
			this.il = il;
			this.init_locals = init_locals;
			this.sig_token = sig_token;
			this.max_stack = max_stack;
		}

		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x06004C1A RID: 19482 RVA: 0x000F2BC1 File Offset: 0x000F0DC1
		public virtual IList<ExceptionHandlingClause> ExceptionHandlingClauses
		{
			get
			{
				return Array.AsReadOnly<ExceptionHandlingClause>(this.clauses);
			}
		}

		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x06004C1B RID: 19483 RVA: 0x000F2BCE File Offset: 0x000F0DCE
		public virtual IList<LocalVariableInfo> LocalVariables
		{
			get
			{
				return Array.AsReadOnly<LocalVariableInfo>(this.locals);
			}
		}

		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x06004C1C RID: 19484 RVA: 0x000F2BDB File Offset: 0x000F0DDB
		public virtual bool InitLocals
		{
			get
			{
				return this.init_locals;
			}
		}

		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x06004C1D RID: 19485 RVA: 0x000F2BE3 File Offset: 0x000F0DE3
		public virtual int LocalSignatureMetadataToken
		{
			get
			{
				return this.sig_token;
			}
		}

		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x06004C1E RID: 19486 RVA: 0x000F2BEB File Offset: 0x000F0DEB
		public virtual int MaxStackSize
		{
			get
			{
				return this.max_stack;
			}
		}

		// Token: 0x06004C1F RID: 19487 RVA: 0x000F2BF3 File Offset: 0x000F0DF3
		public virtual byte[] GetILAsByteArray()
		{
			return this.il;
		}

		// Token: 0x04002FB4 RID: 12212
		private ExceptionHandlingClause[] clauses;

		// Token: 0x04002FB5 RID: 12213
		private LocalVariableInfo[] locals;

		// Token: 0x04002FB6 RID: 12214
		private byte[] il;

		// Token: 0x04002FB7 RID: 12215
		private bool init_locals;

		// Token: 0x04002FB8 RID: 12216
		private int sig_token;

		// Token: 0x04002FB9 RID: 12217
		private int max_stack;
	}
}
