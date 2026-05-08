using System;
using System.Runtime.Serialization;

namespace Newtonsoft.Json
{
	// Token: 0x02000007 RID: 7
	[Serializable]
	public class JsonException : Exception
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002061 File Offset: 0x00000261
		public JsonException()
		{
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002069 File Offset: 0x00000269
		public JsonException(string message)
			: base(message)
		{
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002072 File Offset: 0x00000272
		public JsonException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000207C File Offset: 0x0000027C
		public JsonException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002086 File Offset: 0x00000286
		internal static JsonException Create(IJsonLineInfo lineInfo, string path, string message)
		{
			message = JsonPosition.FormatMessage(lineInfo, path, message);
			return new JsonException(message);
		}
	}
}
