using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Newtonsoft.Json
{
	// Token: 0x02000026 RID: 38
	[Serializable]
	public class JsonReaderException : JsonException
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00007F87 File Offset: 0x00006187
		public int LineNumber
		{
			[CompilerGenerated]
			get
			{
				return this.<LineNumber>k__BackingField;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00007F8F File Offset: 0x0000618F
		public int LinePosition
		{
			[CompilerGenerated]
			get
			{
				return this.<LinePosition>k__BackingField;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00007F97 File Offset: 0x00006197
		public string Path
		{
			[CompilerGenerated]
			get
			{
				return this.<Path>k__BackingField;
			}
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00007F2E File Offset: 0x0000612E
		public JsonReaderException()
		{
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00007F36 File Offset: 0x00006136
		public JsonReaderException(string message)
			: base(message)
		{
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00007F3F File Offset: 0x0000613F
		public JsonReaderException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00007F49 File Offset: 0x00006149
		public JsonReaderException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00007F9F File Offset: 0x0000619F
		public JsonReaderException(string message, string path, int lineNumber, int linePosition, Exception innerException)
			: base(message, innerException)
		{
			this.Path = path;
			this.LineNumber = lineNumber;
			this.LinePosition = linePosition;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00007FC0 File Offset: 0x000061C0
		internal static JsonReaderException Create(JsonReader reader, string message)
		{
			return JsonReaderException.Create(reader, message, null);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00007FCA File Offset: 0x000061CA
		internal static JsonReaderException Create(JsonReader reader, string message, Exception ex)
		{
			return JsonReaderException.Create(reader as IJsonLineInfo, reader.Path, message, ex);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00007FE0 File Offset: 0x000061E0
		internal static JsonReaderException Create(IJsonLineInfo lineInfo, string path, string message, Exception ex)
		{
			message = JsonPosition.FormatMessage(lineInfo, path, message);
			int num;
			int num2;
			if (lineInfo != null && lineInfo.HasLineInfo())
			{
				num = lineInfo.LineNumber;
				num2 = lineInfo.LinePosition;
			}
			else
			{
				num = 0;
				num2 = 0;
			}
			return new JsonReaderException(message, path, num, num2, ex);
		}

		// Token: 0x040000C7 RID: 199
		[CompilerGenerated]
		private readonly int <LineNumber>k__BackingField;

		// Token: 0x040000C8 RID: 200
		[CompilerGenerated]
		private readonly int <LinePosition>k__BackingField;

		// Token: 0x040000C9 RID: 201
		[CompilerGenerated]
		private readonly string <Path>k__BackingField;
	}
}
