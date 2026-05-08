using System;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000052 RID: 82
	internal static class JsonTokenUtils
	{
		// Token: 0x0600043E RID: 1086 RVA: 0x00011B28 File Offset: 0x0000FD28
		internal static bool IsEndToken(JsonToken token)
		{
			switch (token)
			{
			case JsonToken.EndObject:
			case JsonToken.EndArray:
			case JsonToken.EndConstructor:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00011B44 File Offset: 0x0000FD44
		internal static bool IsStartToken(JsonToken token)
		{
			switch (token)
			{
			case JsonToken.StartObject:
			case JsonToken.StartArray:
			case JsonToken.StartConstructor:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00011B5F File Offset: 0x0000FD5F
		internal static bool IsPrimitiveToken(JsonToken token)
		{
			switch (token)
			{
			case JsonToken.Integer:
			case JsonToken.Float:
			case JsonToken.String:
			case JsonToken.Boolean:
			case JsonToken.Null:
			case JsonToken.Undefined:
			case JsonToken.Date:
			case JsonToken.Bytes:
				return true;
			}
			return false;
		}
	}
}
