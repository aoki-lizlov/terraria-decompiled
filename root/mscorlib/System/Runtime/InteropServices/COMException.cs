using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006A1 RID: 1697
	[Serializable]
	public class COMException : ExternalException
	{
		// Token: 0x06003FB6 RID: 16310 RVA: 0x000E055D File Offset: 0x000DE75D
		internal COMException(int hr)
		{
			base.HResult = hr;
		}

		// Token: 0x06003FB7 RID: 16311 RVA: 0x000E056C File Offset: 0x000DE76C
		public COMException()
		{
		}

		// Token: 0x06003FB8 RID: 16312 RVA: 0x000E0574 File Offset: 0x000DE774
		public COMException(string message)
			: base(message)
		{
		}

		// Token: 0x06003FB9 RID: 16313 RVA: 0x000E057D File Offset: 0x000DE77D
		public COMException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x06003FBA RID: 16314 RVA: 0x000E0587 File Offset: 0x000DE787
		public COMException(string message, int errorCode)
			: base(message)
		{
			base.HResult = errorCode;
		}

		// Token: 0x06003FBB RID: 16315 RVA: 0x000E0597 File Offset: 0x000DE797
		protected COMException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06003FBC RID: 16316 RVA: 0x000E05A4 File Offset: 0x000DE7A4
		public override string ToString()
		{
			string message = this.Message;
			string text = base.GetType().ToString() + " (0x" + base.HResult.ToString("X8", CultureInfo.InvariantCulture) + ")";
			if (message != null && message.Length > 0)
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
