using System;
using System.Runtime.Serialization;
using System.Threading;

namespace System
{
	// Token: 0x02000131 RID: 305
	[Serializable]
	public class OperationCanceledException : SystemException
	{
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000C97 RID: 3223 RVA: 0x00032893 File Offset: 0x00030A93
		// (set) Token: 0x06000C98 RID: 3224 RVA: 0x0003289B File Offset: 0x00030A9B
		public CancellationToken CancellationToken
		{
			get
			{
				return this._cancellationToken;
			}
			private set
			{
				this._cancellationToken = value;
			}
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x000328A4 File Offset: 0x00030AA4
		public OperationCanceledException()
			: base("The operation was canceled.")
		{
			base.HResult = -2146233029;
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x000328BC File Offset: 0x00030ABC
		public OperationCanceledException(string message)
			: base(message)
		{
			base.HResult = -2146233029;
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x000328D0 File Offset: 0x00030AD0
		public OperationCanceledException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146233029;
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x000328E5 File Offset: 0x00030AE5
		public OperationCanceledException(CancellationToken token)
			: this()
		{
			this.CancellationToken = token;
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x000328F4 File Offset: 0x00030AF4
		public OperationCanceledException(string message, CancellationToken token)
			: this(message)
		{
			this.CancellationToken = token;
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x00032904 File Offset: 0x00030B04
		public OperationCanceledException(string message, Exception innerException, CancellationToken token)
			: this(message, innerException)
		{
			this.CancellationToken = token;
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x000183F5 File Offset: 0x000165F5
		protected OperationCanceledException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x0400112B RID: 4395
		[NonSerialized]
		private CancellationToken _cancellationToken;
	}
}
