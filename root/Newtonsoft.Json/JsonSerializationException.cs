using System;
using System.Runtime.Serialization;

namespace Newtonsoft.Json
{
	// Token: 0x0200002B RID: 43
	[Serializable]
	public class JsonSerializationException : JsonException
	{
		// Token: 0x0600020C RID: 524 RVA: 0x00007F2E File Offset: 0x0000612E
		public JsonSerializationException()
		{
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00007F36 File Offset: 0x00006136
		public JsonSerializationException(string message)
			: base(message)
		{
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00007F3F File Offset: 0x0000613F
		public JsonSerializationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00007F49 File Offset: 0x00006149
		public JsonSerializationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00009BB3 File Offset: 0x00007DB3
		internal static JsonSerializationException Create(JsonReader reader, string message)
		{
			return JsonSerializationException.Create(reader, message, null);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00009BBD File Offset: 0x00007DBD
		internal static JsonSerializationException Create(JsonReader reader, string message, Exception ex)
		{
			return JsonSerializationException.Create(reader as IJsonLineInfo, reader.Path, message, ex);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00009BD2 File Offset: 0x00007DD2
		internal static JsonSerializationException Create(IJsonLineInfo lineInfo, string path, string message, Exception ex)
		{
			message = JsonPosition.FormatMessage(lineInfo, path, message);
			return new JsonSerializationException(message, ex);
		}
	}
}
