using System;
using System.Collections.Generic;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x02000041 RID: 65
	[Obsolete("JSON Schema validation has been moved to its own package. See http://www.newtonsoft.com/jsonschema for more details.")]
	internal static class JsonSchemaConstants
	{
		// Token: 0x060003A8 RID: 936 RVA: 0x0000EA48 File Offset: 0x0000CC48
		// Note: this type is marked as 'beforefieldinit'.
		static JsonSchemaConstants()
		{
			Dictionary<string, JsonSchemaType> dictionary = new Dictionary<string, JsonSchemaType>();
			dictionary.Add("string", JsonSchemaType.String);
			dictionary.Add("object", JsonSchemaType.Object);
			dictionary.Add("integer", JsonSchemaType.Integer);
			dictionary.Add("number", JsonSchemaType.Float);
			dictionary.Add("null", JsonSchemaType.Null);
			dictionary.Add("boolean", JsonSchemaType.Boolean);
			dictionary.Add("array", JsonSchemaType.Array);
			dictionary.Add("any", JsonSchemaType.Any);
			JsonSchemaConstants.JsonSchemaTypeMapping = dictionary;
		}

		// Token: 0x0400018F RID: 399
		public const string TypePropertyName = "type";

		// Token: 0x04000190 RID: 400
		public const string PropertiesPropertyName = "properties";

		// Token: 0x04000191 RID: 401
		public const string ItemsPropertyName = "items";

		// Token: 0x04000192 RID: 402
		public const string AdditionalItemsPropertyName = "additionalItems";

		// Token: 0x04000193 RID: 403
		public const string RequiredPropertyName = "required";

		// Token: 0x04000194 RID: 404
		public const string PatternPropertiesPropertyName = "patternProperties";

		// Token: 0x04000195 RID: 405
		public const string AdditionalPropertiesPropertyName = "additionalProperties";

		// Token: 0x04000196 RID: 406
		public const string RequiresPropertyName = "requires";

		// Token: 0x04000197 RID: 407
		public const string MinimumPropertyName = "minimum";

		// Token: 0x04000198 RID: 408
		public const string MaximumPropertyName = "maximum";

		// Token: 0x04000199 RID: 409
		public const string ExclusiveMinimumPropertyName = "exclusiveMinimum";

		// Token: 0x0400019A RID: 410
		public const string ExclusiveMaximumPropertyName = "exclusiveMaximum";

		// Token: 0x0400019B RID: 411
		public const string MinimumItemsPropertyName = "minItems";

		// Token: 0x0400019C RID: 412
		public const string MaximumItemsPropertyName = "maxItems";

		// Token: 0x0400019D RID: 413
		public const string PatternPropertyName = "pattern";

		// Token: 0x0400019E RID: 414
		public const string MaximumLengthPropertyName = "maxLength";

		// Token: 0x0400019F RID: 415
		public const string MinimumLengthPropertyName = "minLength";

		// Token: 0x040001A0 RID: 416
		public const string EnumPropertyName = "enum";

		// Token: 0x040001A1 RID: 417
		public const string ReadOnlyPropertyName = "readonly";

		// Token: 0x040001A2 RID: 418
		public const string TitlePropertyName = "title";

		// Token: 0x040001A3 RID: 419
		public const string DescriptionPropertyName = "description";

		// Token: 0x040001A4 RID: 420
		public const string FormatPropertyName = "format";

		// Token: 0x040001A5 RID: 421
		public const string DefaultPropertyName = "default";

		// Token: 0x040001A6 RID: 422
		public const string TransientPropertyName = "transient";

		// Token: 0x040001A7 RID: 423
		public const string DivisibleByPropertyName = "divisibleBy";

		// Token: 0x040001A8 RID: 424
		public const string HiddenPropertyName = "hidden";

		// Token: 0x040001A9 RID: 425
		public const string DisallowPropertyName = "disallow";

		// Token: 0x040001AA RID: 426
		public const string ExtendsPropertyName = "extends";

		// Token: 0x040001AB RID: 427
		public const string IdPropertyName = "id";

		// Token: 0x040001AC RID: 428
		public const string UniqueItemsPropertyName = "uniqueItems";

		// Token: 0x040001AD RID: 429
		public const string OptionValuePropertyName = "value";

		// Token: 0x040001AE RID: 430
		public const string OptionLabelPropertyName = "label";

		// Token: 0x040001AF RID: 431
		public static readonly IDictionary<string, JsonSchemaType> JsonSchemaTypeMapping;
	}
}
