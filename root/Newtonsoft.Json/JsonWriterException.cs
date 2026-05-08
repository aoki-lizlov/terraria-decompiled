using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Newtonsoft.Json
{
	// Token: 0x02000025 RID: 37
	[Serializable]
	public class JsonWriterException : JsonException
	{
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00007F26 File Offset: 0x00006126
		public string Path
		{
			[CompilerGenerated]
			get
			{
				return this.<Path>k__BackingField;
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00007F2E File Offset: 0x0000612E
		public JsonWriterException()
		{
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00007F36 File Offset: 0x00006136
		public JsonWriterException(string message)
			: base(message)
		{
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00007F3F File Offset: 0x0000613F
		public JsonWriterException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00007F49 File Offset: 0x00006149
		public JsonWriterException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00007F53 File Offset: 0x00006153
		public JsonWriterException(string message, string path, Exception innerException)
			: base(message, innerException)
		{
			this.Path = path;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00007F64 File Offset: 0x00006164
		internal static JsonWriterException Create(JsonWriter writer, string message, Exception ex)
		{
			return JsonWriterException.Create(writer.ContainerPath, message, ex);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00007F73 File Offset: 0x00006173
		internal static JsonWriterException Create(string path, string message, Exception ex)
		{
			message = JsonPosition.FormatMessage(null, path, message);
			return new JsonWriterException(message, path, ex);
		}

		// Token: 0x040000C6 RID: 198
		[CompilerGenerated]
		private readonly string <Path>k__BackingField;
	}
}
