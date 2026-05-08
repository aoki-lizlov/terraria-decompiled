using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.IO
{
	// Token: 0x02000927 RID: 2343
	[Serializable]
	public class FileNotFoundException : IOException
	{
		// Token: 0x06005383 RID: 21379 RVA: 0x001189AC File Offset: 0x00116BAC
		public FileNotFoundException()
			: base("Unable to find the specified file.")
		{
			base.HResult = -2147024894;
		}

		// Token: 0x06005384 RID: 21380 RVA: 0x001189C4 File Offset: 0x00116BC4
		public FileNotFoundException(string message)
			: base(message)
		{
			base.HResult = -2147024894;
		}

		// Token: 0x06005385 RID: 21381 RVA: 0x001189D8 File Offset: 0x00116BD8
		public FileNotFoundException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2147024894;
		}

		// Token: 0x06005386 RID: 21382 RVA: 0x001189ED File Offset: 0x00116BED
		public FileNotFoundException(string message, string fileName)
			: base(message)
		{
			base.HResult = -2147024894;
			this.FileName = fileName;
		}

		// Token: 0x06005387 RID: 21383 RVA: 0x00118A08 File Offset: 0x00116C08
		public FileNotFoundException(string message, string fileName, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2147024894;
			this.FileName = fileName;
		}

		// Token: 0x17000DD8 RID: 3544
		// (get) Token: 0x06005388 RID: 21384 RVA: 0x00118A24 File Offset: 0x00116C24
		public override string Message
		{
			get
			{
				this.SetMessageField();
				return this._message;
			}
		}

		// Token: 0x06005389 RID: 21385 RVA: 0x00118A34 File Offset: 0x00116C34
		private void SetMessageField()
		{
			if (this._message == null)
			{
				if (this.FileName == null && base.HResult == -2146233088)
				{
					this._message = "Unable to find the specified file.";
					return;
				}
				if (this.FileName != null)
				{
					this._message = FileLoadException.FormatFileLoadExceptionMessage(this.FileName, base.HResult);
				}
			}
		}

		// Token: 0x17000DD9 RID: 3545
		// (get) Token: 0x0600538A RID: 21386 RVA: 0x00118A89 File Offset: 0x00116C89
		public string FileName
		{
			[CompilerGenerated]
			get
			{
				return this.<FileName>k__BackingField;
			}
		}

		// Token: 0x17000DDA RID: 3546
		// (get) Token: 0x0600538B RID: 21387 RVA: 0x00118A91 File Offset: 0x00116C91
		public string FusionLog
		{
			[CompilerGenerated]
			get
			{
				return this.<FusionLog>k__BackingField;
			}
		}

		// Token: 0x0600538C RID: 21388 RVA: 0x00118A9C File Offset: 0x00116C9C
		public override string ToString()
		{
			string text = base.GetType().ToString() + ": " + this.Message;
			if (this.FileName != null && this.FileName.Length != 0)
			{
				text = text + Environment.NewLine + SR.Format("File name: '{0}'", this.FileName);
			}
			if (base.InnerException != null)
			{
				text = text + " ---> " + base.InnerException.ToString();
			}
			if (this.StackTrace != null)
			{
				text = text + Environment.NewLine + this.StackTrace;
			}
			if (this.FusionLog != null)
			{
				if (text == null)
				{
					text = " ";
				}
				text += Environment.NewLine;
				text += Environment.NewLine;
				text += this.FusionLog;
			}
			return text;
		}

		// Token: 0x0600538D RID: 21389 RVA: 0x00118B66 File Offset: 0x00116D66
		protected FileNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.FileName = info.GetString("FileNotFound_FileName");
			this.FusionLog = info.GetString("FileNotFound_FusionLog");
		}

		// Token: 0x0600538E RID: 21390 RVA: 0x00118B92 File Offset: 0x00116D92
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("FileNotFound_FileName", this.FileName, typeof(string));
			info.AddValue("FileNotFound_FusionLog", this.FusionLog, typeof(string));
		}

		// Token: 0x0400331E RID: 13086
		[CompilerGenerated]
		private readonly string <FileName>k__BackingField;

		// Token: 0x0400331F RID: 13087
		[CompilerGenerated]
		private readonly string <FusionLog>k__BackingField;
	}
}
