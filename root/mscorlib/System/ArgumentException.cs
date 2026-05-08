using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000BA RID: 186
	[Serializable]
	public class ArgumentException : SystemException
	{
		// Token: 0x06000531 RID: 1329 RVA: 0x00018941 File Offset: 0x00016B41
		public ArgumentException()
			: base("Value does not fall within the expected range.")
		{
			base.HResult = -2147024809;
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00018959 File Offset: 0x00016B59
		public ArgumentException(string message)
			: base(message)
		{
			base.HResult = -2147024809;
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0001896D File Offset: 0x00016B6D
		public ArgumentException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2147024809;
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x00018982 File Offset: 0x00016B82
		public ArgumentException(string message, string paramName, Exception innerException)
			: base(message, innerException)
		{
			this._paramName = paramName;
			base.HResult = -2147024809;
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0001899E File Offset: 0x00016B9E
		public ArgumentException(string message, string paramName)
			: base(message)
		{
			this._paramName = paramName;
			base.HResult = -2147024809;
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x000189B9 File Offset: 0x00016BB9
		protected ArgumentException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._paramName = info.GetString("ParamName");
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x000189D4 File Offset: 0x00016BD4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ParamName", this._paramName, typeof(string));
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x000189FC File Offset: 0x00016BFC
		public override string Message
		{
			get
			{
				string message = base.Message;
				if (!string.IsNullOrEmpty(this._paramName))
				{
					string text = SR.Format("Parameter name: {0}", this._paramName);
					return message + Environment.NewLine + text;
				}
				return message;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x00018A3C File Offset: 0x00016C3C
		public virtual string ParamName
		{
			get
			{
				return this._paramName;
			}
		}

		// Token: 0x04000ECB RID: 3787
		private string _paramName;
	}
}
