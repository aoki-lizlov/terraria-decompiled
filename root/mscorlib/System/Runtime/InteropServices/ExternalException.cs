using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000695 RID: 1685
	[Serializable]
	public class ExternalException : SystemException
	{
		// Token: 0x06003F66 RID: 16230 RVA: 0x000DF794 File Offset: 0x000DD994
		public ExternalException()
			: base("External component has thrown an exception.")
		{
			base.HResult = -2147467259;
		}

		// Token: 0x06003F67 RID: 16231 RVA: 0x000DF7AC File Offset: 0x000DD9AC
		public ExternalException(string message)
			: base(message)
		{
			base.HResult = -2147467259;
		}

		// Token: 0x06003F68 RID: 16232 RVA: 0x000DF7C0 File Offset: 0x000DD9C0
		public ExternalException(string message, Exception inner)
			: base(message, inner)
		{
			base.HResult = -2147467259;
		}

		// Token: 0x06003F69 RID: 16233 RVA: 0x0002A1A4 File Offset: 0x000283A4
		public ExternalException(string message, int errorCode)
			: base(message)
		{
			base.HResult = errorCode;
		}

		// Token: 0x06003F6A RID: 16234 RVA: 0x000183F5 File Offset: 0x000165F5
		protected ExternalException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x06003F6B RID: 16235 RVA: 0x000DF7D5 File Offset: 0x000DD9D5
		public virtual int ErrorCode
		{
			get
			{
				return base.HResult;
			}
		}

		// Token: 0x06003F6C RID: 16236 RVA: 0x000DF7E0 File Offset: 0x000DD9E0
		public override string ToString()
		{
			string message = this.Message;
			string text = base.GetType().ToString() + " (0x" + base.HResult.ToString("X8", CultureInfo.InvariantCulture) + ")";
			if (!string.IsNullOrEmpty(message))
			{
				text = text + ": " + message;
			}
			Exception innerException = base.InnerException;
			if (innerException != null)
			{
				text = text + " ---> " + innerException.ToString();
			}
			if (this.StackTrace != null)
			{
				text = text + Environment.NewLine + this.StackTrace;
			}
			return text;
		}
	}
}
