using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata;

namespace System.Runtime.Serialization.Formatters
{
	// Token: 0x02000652 RID: 1618
	[SoapType(Embedded = true)]
	[ComVisible(true)]
	[Serializable]
	public sealed class ServerFault
	{
		// Token: 0x06003D84 RID: 15748 RVA: 0x000D5685 File Offset: 0x000D3885
		internal ServerFault(Exception exception)
		{
			this.exception = exception;
		}

		// Token: 0x06003D85 RID: 15749 RVA: 0x000D5694 File Offset: 0x000D3894
		public ServerFault(string exceptionType, string message, string stackTrace)
		{
			this.exceptionType = exceptionType;
			this.message = message;
			this.stackTrace = stackTrace;
		}

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x06003D86 RID: 15750 RVA: 0x000D56B1 File Offset: 0x000D38B1
		// (set) Token: 0x06003D87 RID: 15751 RVA: 0x000D56B9 File Offset: 0x000D38B9
		public string ExceptionType
		{
			get
			{
				return this.exceptionType;
			}
			set
			{
				this.exceptionType = value;
			}
		}

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x06003D88 RID: 15752 RVA: 0x000D56C2 File Offset: 0x000D38C2
		// (set) Token: 0x06003D89 RID: 15753 RVA: 0x000D56CA File Offset: 0x000D38CA
		public string ExceptionMessage
		{
			get
			{
				return this.message;
			}
			set
			{
				this.message = value;
			}
		}

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x06003D8A RID: 15754 RVA: 0x000D56D3 File Offset: 0x000D38D3
		// (set) Token: 0x06003D8B RID: 15755 RVA: 0x000D56DB File Offset: 0x000D38DB
		public string StackTrace
		{
			get
			{
				return this.stackTrace;
			}
			set
			{
				this.stackTrace = value;
			}
		}

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x06003D8C RID: 15756 RVA: 0x000D56E4 File Offset: 0x000D38E4
		internal Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x04002737 RID: 10039
		private string exceptionType;

		// Token: 0x04002738 RID: 10040
		private string message;

		// Token: 0x04002739 RID: 10041
		private string stackTrace;

		// Token: 0x0400273A RID: 10042
		private Exception exception;
	}
}
