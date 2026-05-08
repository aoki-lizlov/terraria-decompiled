using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000BC RID: 188
	[Serializable]
	public class ArgumentOutOfRangeException : ArgumentException
	{
		// Token: 0x0600053F RID: 1343 RVA: 0x00018AA9 File Offset: 0x00016CA9
		public ArgumentOutOfRangeException()
			: base("Specified argument was out of the range of valid values.")
		{
			base.HResult = -2146233086;
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00018AC1 File Offset: 0x00016CC1
		public ArgumentOutOfRangeException(string paramName)
			: base("Specified argument was out of the range of valid values.", paramName)
		{
			base.HResult = -2146233086;
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00018ADA File Offset: 0x00016CDA
		public ArgumentOutOfRangeException(string paramName, string message)
			: base(message, paramName)
		{
			base.HResult = -2146233086;
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00018AEF File Offset: 0x00016CEF
		public ArgumentOutOfRangeException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146233086;
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00018B04 File Offset: 0x00016D04
		public ArgumentOutOfRangeException(string paramName, object actualValue, string message)
			: base(message, paramName)
		{
			this._actualValue = actualValue;
			base.HResult = -2146233086;
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00018B20 File Offset: 0x00016D20
		protected ArgumentOutOfRangeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._actualValue = info.GetValue("ActualValue", typeof(object));
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00018B45 File Offset: 0x00016D45
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ActualValue", this._actualValue, typeof(object));
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x00018B6C File Offset: 0x00016D6C
		public override string Message
		{
			get
			{
				string message = base.Message;
				if (this._actualValue == null)
				{
					return message;
				}
				string text = SR.Format("Actual value was {0}.", this._actualValue.ToString());
				if (message == null)
				{
					return text;
				}
				return message + Environment.NewLine + text;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x00018BB1 File Offset: 0x00016DB1
		public virtual object ActualValue
		{
			get
			{
				return this._actualValue;
			}
		}

		// Token: 0x04000ECC RID: 3788
		private object _actualValue;
	}
}
