using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x0200012F RID: 303
	[Serializable]
	public class ObjectDisposedException : InvalidOperationException
	{
		// Token: 0x06000C8A RID: 3210 RVA: 0x00032759 File Offset: 0x00030959
		private ObjectDisposedException()
			: this(null, "Cannot access a disposed object.")
		{
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x00032767 File Offset: 0x00030967
		public ObjectDisposedException(string objectName)
			: this(objectName, "Cannot access a disposed object.")
		{
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x00032775 File Offset: 0x00030975
		public ObjectDisposedException(string objectName, string message)
			: base(message)
		{
			base.HResult = -2146232798;
			this._objectName = objectName;
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x00032790 File Offset: 0x00030990
		public ObjectDisposedException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146232798;
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x000327A5 File Offset: 0x000309A5
		protected ObjectDisposedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._objectName = info.GetString("ObjectName");
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x000327C0 File Offset: 0x000309C0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ObjectName", this.ObjectName, typeof(string));
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000C90 RID: 3216 RVA: 0x000327E8 File Offset: 0x000309E8
		public override string Message
		{
			get
			{
				string objectName = this.ObjectName;
				if (objectName == null || objectName.Length == 0)
				{
					return base.Message;
				}
				string text = SR.Format("Object name: '{0}'.", objectName);
				return base.Message + Environment.NewLine + text;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000C91 RID: 3217 RVA: 0x0003282B File Offset: 0x00030A2B
		public string ObjectName
		{
			get
			{
				if (this._objectName == null)
				{
					return string.Empty;
				}
				return this._objectName;
			}
		}

		// Token: 0x04001128 RID: 4392
		private string _objectName;
	}
}
