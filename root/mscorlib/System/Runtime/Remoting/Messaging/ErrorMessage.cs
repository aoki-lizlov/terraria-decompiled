using System;
using System.Collections;
using System.Reflection;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020005E8 RID: 1512
	[Serializable]
	internal class ErrorMessage : IMethodCallMessage, IMethodMessage, IMessage
	{
		// Token: 0x06003A72 RID: 14962 RVA: 0x000CDD22 File Offset: 0x000CBF22
		public ErrorMessage()
		{
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06003A73 RID: 14963 RVA: 0x0000408A File Offset: 0x0000228A
		public int ArgCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x06003A74 RID: 14964 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public object[] Args
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x06003A75 RID: 14965 RVA: 0x0000408A File Offset: 0x0000228A
		public bool HasVarArgs
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06003A76 RID: 14966 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public MethodBase MethodBase
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06003A77 RID: 14967 RVA: 0x000CDD35 File Offset: 0x000CBF35
		public string MethodName
		{
			get
			{
				return "unknown";
			}
		}

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06003A78 RID: 14968 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public object MethodSignature
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x06003A79 RID: 14969 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public virtual IDictionary Properties
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x06003A7A RID: 14970 RVA: 0x000CDD35 File Offset: 0x000CBF35
		public string TypeName
		{
			get
			{
				return "unknown";
			}
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x06003A7B RID: 14971 RVA: 0x000CDD3C File Offset: 0x000CBF3C
		// (set) Token: 0x06003A7C RID: 14972 RVA: 0x000CDD44 File Offset: 0x000CBF44
		public string Uri
		{
			get
			{
				return this._uri;
			}
			set
			{
				this._uri = value;
			}
		}

		// Token: 0x06003A7D RID: 14973 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public object GetArg(int arg_num)
		{
			return null;
		}

		// Token: 0x06003A7E RID: 14974 RVA: 0x000CDD35 File Offset: 0x000CBF35
		public string GetArgName(int arg_num)
		{
			return "unknown";
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x06003A7F RID: 14975 RVA: 0x0000408A File Offset: 0x0000228A
		public int InArgCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06003A80 RID: 14976 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public string GetInArgName(int index)
		{
			return null;
		}

		// Token: 0x06003A81 RID: 14977 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public object GetInArg(int argNum)
		{
			return null;
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06003A82 RID: 14978 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public object[] InArgs
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06003A83 RID: 14979 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public LogicalCallContext LogicalCallContext
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04002611 RID: 9745
		private string _uri = "Exception";
	}
}
