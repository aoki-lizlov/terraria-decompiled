using System;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x0200003D RID: 61
	[Obsolete("JSON Schema validation has been moved to its own package. See http://www.newtonsoft.com/jsonschema for more details.")]
	public class ValidationEventArgs : EventArgs
	{
		// Token: 0x0600033D RID: 829 RVA: 0x0000D593 File Offset: 0x0000B793
		internal ValidationEventArgs(JsonSchemaException ex)
		{
			ValidationUtils.ArgumentNotNull(ex, "ex");
			this._ex = ex;
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600033E RID: 830 RVA: 0x0000D5AD File Offset: 0x0000B7AD
		public JsonSchemaException Exception
		{
			get
			{
				return this._ex;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0000D5B5 File Offset: 0x0000B7B5
		public string Path
		{
			get
			{
				return this._ex.Path;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000340 RID: 832 RVA: 0x0000D5C2 File Offset: 0x0000B7C2
		public string Message
		{
			get
			{
				return this._ex.Message;
			}
		}

		// Token: 0x04000164 RID: 356
		private readonly JsonSchemaException _ex;
	}
}
