using System;
using System.IO;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000C6 RID: 198
	[Serializable]
	public class BadImageFormatException : SystemException
	{
		// Token: 0x0600058C RID: 1420 RVA: 0x00019146 File Offset: 0x00017346
		public BadImageFormatException()
			: base("Format of the executable (.exe) or library (.dll) is invalid.")
		{
			base.HResult = -2147024885;
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0001915E File Offset: 0x0001735E
		public BadImageFormatException(string message)
			: base(message)
		{
			base.HResult = -2147024885;
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00019172 File Offset: 0x00017372
		public BadImageFormatException(string message, Exception inner)
			: base(message, inner)
		{
			base.HResult = -2147024885;
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00019187 File Offset: 0x00017387
		public BadImageFormatException(string message, string fileName)
			: base(message)
		{
			base.HResult = -2147024885;
			this._fileName = fileName;
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x000191A2 File Offset: 0x000173A2
		public BadImageFormatException(string message, string fileName, Exception inner)
			: base(message, inner)
		{
			base.HResult = -2147024885;
			this._fileName = fileName;
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x000191BE File Offset: 0x000173BE
		protected BadImageFormatException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._fileName = info.GetString("BadImageFormat_FileName");
			this._fusionLog = info.GetString("BadImageFormat_FusionLog");
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x000191EA File Offset: 0x000173EA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("BadImageFormat_FileName", this._fileName, typeof(string));
			info.AddValue("BadImageFormat_FusionLog", this._fusionLog, typeof(string));
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x0001922A File Offset: 0x0001742A
		public override string Message
		{
			get
			{
				this.SetMessageField();
				return this._message;
			}
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00019238 File Offset: 0x00017438
		private void SetMessageField()
		{
			if (this._message == null)
			{
				if (this._fileName == null && base.HResult == -2146233088)
				{
					this._message = "Format of the executable (.exe) or library (.dll) is invalid.";
					return;
				}
				this._message = FileLoadException.FormatFileLoadExceptionMessage(this._fileName, base.HResult);
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x00019285 File Offset: 0x00017485
		public string FileName
		{
			get
			{
				return this._fileName;
			}
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00019290 File Offset: 0x00017490
		public override string ToString()
		{
			string text = base.GetType().ToString() + ": " + this.Message;
			if (this._fileName != null && this._fileName.Length != 0)
			{
				text = text + Environment.NewLine + SR.Format("File name: '{0}'", this._fileName);
			}
			if (base.InnerException != null)
			{
				text = text + " ---> " + base.InnerException.ToString();
			}
			if (this.StackTrace != null)
			{
				text = text + Environment.NewLine + this.StackTrace;
			}
			if (this._fusionLog != null)
			{
				if (text == null)
				{
					text = " ";
				}
				text += Environment.NewLine;
				text += Environment.NewLine;
				text += this._fusionLog;
			}
			return text;
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x0001935A File Offset: 0x0001755A
		public string FusionLog
		{
			get
			{
				return this._fusionLog;
			}
		}

		// Token: 0x04000EEB RID: 3819
		private string _fileName;

		// Token: 0x04000EEC RID: 3820
		private string _fusionLog;
	}
}
