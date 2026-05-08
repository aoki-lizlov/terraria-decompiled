using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x02000035 RID: 53
	[Obsolete("JSON Schema validation has been moved to its own package. See http://www.newtonsoft.com/jsonschema for more details.")]
	[Serializable]
	public class JsonSchemaException : JsonException
	{
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x0000C340 File Offset: 0x0000A540
		public int LineNumber
		{
			[CompilerGenerated]
			get
			{
				return this.<LineNumber>k__BackingField;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x0000C348 File Offset: 0x0000A548
		public int LinePosition
		{
			[CompilerGenerated]
			get
			{
				return this.<LinePosition>k__BackingField;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x0000C350 File Offset: 0x0000A550
		public string Path
		{
			[CompilerGenerated]
			get
			{
				return this.<Path>k__BackingField;
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00007F2E File Offset: 0x0000612E
		public JsonSchemaException()
		{
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00007F36 File Offset: 0x00006136
		public JsonSchemaException(string message)
			: base(message)
		{
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00007F3F File Offset: 0x0000613F
		public JsonSchemaException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00007F49 File Offset: 0x00006149
		public JsonSchemaException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000C358 File Offset: 0x0000A558
		internal JsonSchemaException(string message, Exception innerException, string path, int lineNumber, int linePosition)
			: base(message, innerException)
		{
			this.Path = path;
			this.LineNumber = lineNumber;
			this.LinePosition = linePosition;
		}

		// Token: 0x04000139 RID: 313
		[CompilerGenerated]
		private readonly int <LineNumber>k__BackingField;

		// Token: 0x0400013A RID: 314
		[CompilerGenerated]
		private readonly int <LinePosition>k__BackingField;

		// Token: 0x0400013B RID: 315
		[CompilerGenerated]
		private readonly string <Path>k__BackingField;
	}
}
