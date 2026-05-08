using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.IO
{
	// Token: 0x02000925 RID: 2341
	[Serializable]
	public class FileLoadException : IOException
	{
		// Token: 0x06005377 RID: 21367 RVA: 0x001187AE File Offset: 0x001169AE
		public FileLoadException()
			: base("Could not load the specified file.")
		{
			base.HResult = -2146232799;
		}

		// Token: 0x06005378 RID: 21368 RVA: 0x001187C6 File Offset: 0x001169C6
		public FileLoadException(string message)
			: base(message)
		{
			base.HResult = -2146232799;
		}

		// Token: 0x06005379 RID: 21369 RVA: 0x001187DA File Offset: 0x001169DA
		public FileLoadException(string message, Exception inner)
			: base(message, inner)
		{
			base.HResult = -2146232799;
		}

		// Token: 0x0600537A RID: 21370 RVA: 0x001187EF File Offset: 0x001169EF
		public FileLoadException(string message, string fileName)
			: base(message)
		{
			base.HResult = -2146232799;
			this.FileName = fileName;
		}

		// Token: 0x0600537B RID: 21371 RVA: 0x0011880A File Offset: 0x00116A0A
		public FileLoadException(string message, string fileName, Exception inner)
			: base(message, inner)
		{
			base.HResult = -2146232799;
			this.FileName = fileName;
		}

		// Token: 0x17000DD5 RID: 3541
		// (get) Token: 0x0600537C RID: 21372 RVA: 0x00118826 File Offset: 0x00116A26
		public override string Message
		{
			get
			{
				if (this._message == null)
				{
					this._message = FileLoadException.FormatFileLoadExceptionMessage(this.FileName, base.HResult);
				}
				return this._message;
			}
		}

		// Token: 0x17000DD6 RID: 3542
		// (get) Token: 0x0600537D RID: 21373 RVA: 0x0011884D File Offset: 0x00116A4D
		public string FileName
		{
			[CompilerGenerated]
			get
			{
				return this.<FileName>k__BackingField;
			}
		}

		// Token: 0x17000DD7 RID: 3543
		// (get) Token: 0x0600537E RID: 21374 RVA: 0x00118855 File Offset: 0x00116A55
		public string FusionLog
		{
			[CompilerGenerated]
			get
			{
				return this.<FusionLog>k__BackingField;
			}
		}

		// Token: 0x0600537F RID: 21375 RVA: 0x00118860 File Offset: 0x00116A60
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

		// Token: 0x06005380 RID: 21376 RVA: 0x0011892A File Offset: 0x00116B2A
		protected FileLoadException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.FileName = info.GetString("FileLoad_FileName");
			this.FusionLog = info.GetString("FileLoad_FusionLog");
		}

		// Token: 0x06005381 RID: 21377 RVA: 0x00118956 File Offset: 0x00116B56
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("FileLoad_FileName", this.FileName, typeof(string));
			info.AddValue("FileLoad_FusionLog", this.FusionLog, typeof(string));
		}

		// Token: 0x06005382 RID: 21378 RVA: 0x00118996 File Offset: 0x00116B96
		internal static string FormatFileLoadExceptionMessage(string fileName, int hResult)
		{
			if (fileName != null)
			{
				return SR.Format("Could not load the file '{0}'.", fileName);
			}
			return "Could not load the specified file.";
		}

		// Token: 0x04003315 RID: 13077
		[CompilerGenerated]
		private readonly string <FileName>k__BackingField;

		// Token: 0x04003316 RID: 13078
		[CompilerGenerated]
		private readonly string <FusionLog>k__BackingField;
	}
}
