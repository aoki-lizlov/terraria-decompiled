using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x02000034 RID: 52
	[Obsolete("JSON Schema validation has been moved to its own package. See http://www.newtonsoft.com/jsonschema for more details.")]
	public static class Extensions
	{
		// Token: 0x060002DD RID: 733 RVA: 0x0000C258 File Offset: 0x0000A458
		[Obsolete("JSON Schema validation has been moved to its own package. See http://www.newtonsoft.com/jsonschema for more details.")]
		public static bool IsValid(this JToken source, JsonSchema schema)
		{
			bool valid = true;
			source.Validate(schema, delegate(object sender, ValidationEventArgs args)
			{
				valid = false;
			});
			return valid;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000C28C File Offset: 0x0000A48C
		[Obsolete("JSON Schema validation has been moved to its own package. See http://www.newtonsoft.com/jsonschema for more details.")]
		public static bool IsValid(this JToken source, JsonSchema schema, out IList<string> errorMessages)
		{
			IList<string> errors = new List<string>();
			source.Validate(schema, delegate(object sender, ValidationEventArgs args)
			{
				errors.Add(args.Message);
			});
			errorMessages = errors;
			return errorMessages.Count == 0;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000C2CF File Offset: 0x0000A4CF
		[Obsolete("JSON Schema validation has been moved to its own package. See http://www.newtonsoft.com/jsonschema for more details.")]
		public static void Validate(this JToken source, JsonSchema schema)
		{
			source.Validate(schema, null);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000C2DC File Offset: 0x0000A4DC
		[Obsolete("JSON Schema validation has been moved to its own package. See http://www.newtonsoft.com/jsonschema for more details.")]
		public static void Validate(this JToken source, JsonSchema schema, ValidationEventHandler validationEventHandler)
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			ValidationUtils.ArgumentNotNull(schema, "schema");
			using (JsonValidatingReader jsonValidatingReader = new JsonValidatingReader(source.CreateReader()))
			{
				jsonValidatingReader.Schema = schema;
				if (validationEventHandler != null)
				{
					jsonValidatingReader.ValidationEventHandler += validationEventHandler;
				}
				while (jsonValidatingReader.Read())
				{
				}
			}
		}

		// Token: 0x02000108 RID: 264
		[CompilerGenerated]
		private sealed class <>c__DisplayClass0_0
		{
			// Token: 0x06000C5C RID: 3164 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__DisplayClass0_0()
			{
			}

			// Token: 0x06000C5D RID: 3165 RVA: 0x00030A19 File Offset: 0x0002EC19
			internal void <IsValid>b__0(object sender, ValidationEventArgs args)
			{
				this.valid = false;
			}

			// Token: 0x04000438 RID: 1080
			public bool valid;
		}

		// Token: 0x02000109 RID: 265
		[CompilerGenerated]
		private sealed class <>c__DisplayClass1_0
		{
			// Token: 0x06000C5E RID: 3166 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__DisplayClass1_0()
			{
			}

			// Token: 0x06000C5F RID: 3167 RVA: 0x00030A22 File Offset: 0x0002EC22
			internal void <IsValid>b__0(object sender, ValidationEventArgs args)
			{
				this.errors.Add(args.Message);
			}

			// Token: 0x04000439 RID: 1081
			public IList<string> errors;
		}
	}
}
