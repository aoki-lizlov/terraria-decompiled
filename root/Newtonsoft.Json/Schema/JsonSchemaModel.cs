using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x02000036 RID: 54
	[Obsolete("JSON Schema validation has been moved to its own package. See http://www.newtonsoft.com/jsonschema for more details.")]
	internal class JsonSchemaModel
	{
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x0000C379 File Offset: 0x0000A579
		// (set) Token: 0x060002EA RID: 746 RVA: 0x0000C381 File Offset: 0x0000A581
		public bool Required
		{
			[CompilerGenerated]
			get
			{
				return this.<Required>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Required>k__BackingField = value;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0000C38A File Offset: 0x0000A58A
		// (set) Token: 0x060002EC RID: 748 RVA: 0x0000C392 File Offset: 0x0000A592
		public JsonSchemaType Type
		{
			[CompilerGenerated]
			get
			{
				return this.<Type>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Type>k__BackingField = value;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0000C39B File Offset: 0x0000A59B
		// (set) Token: 0x060002EE RID: 750 RVA: 0x0000C3A3 File Offset: 0x0000A5A3
		public int? MinimumLength
		{
			[CompilerGenerated]
			get
			{
				return this.<MinimumLength>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<MinimumLength>k__BackingField = value;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0000C3AC File Offset: 0x0000A5AC
		// (set) Token: 0x060002F0 RID: 752 RVA: 0x0000C3B4 File Offset: 0x0000A5B4
		public int? MaximumLength
		{
			[CompilerGenerated]
			get
			{
				return this.<MaximumLength>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<MaximumLength>k__BackingField = value;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000C3BD File Offset: 0x0000A5BD
		// (set) Token: 0x060002F2 RID: 754 RVA: 0x0000C3C5 File Offset: 0x0000A5C5
		public double? DivisibleBy
		{
			[CompilerGenerated]
			get
			{
				return this.<DivisibleBy>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<DivisibleBy>k__BackingField = value;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000C3CE File Offset: 0x0000A5CE
		// (set) Token: 0x060002F4 RID: 756 RVA: 0x0000C3D6 File Offset: 0x0000A5D6
		public double? Minimum
		{
			[CompilerGenerated]
			get
			{
				return this.<Minimum>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Minimum>k__BackingField = value;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x0000C3DF File Offset: 0x0000A5DF
		// (set) Token: 0x060002F6 RID: 758 RVA: 0x0000C3E7 File Offset: 0x0000A5E7
		public double? Maximum
		{
			[CompilerGenerated]
			get
			{
				return this.<Maximum>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Maximum>k__BackingField = value;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x0000C3F0 File Offset: 0x0000A5F0
		// (set) Token: 0x060002F8 RID: 760 RVA: 0x0000C3F8 File Offset: 0x0000A5F8
		public bool ExclusiveMinimum
		{
			[CompilerGenerated]
			get
			{
				return this.<ExclusiveMinimum>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ExclusiveMinimum>k__BackingField = value;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x0000C401 File Offset: 0x0000A601
		// (set) Token: 0x060002FA RID: 762 RVA: 0x0000C409 File Offset: 0x0000A609
		public bool ExclusiveMaximum
		{
			[CompilerGenerated]
			get
			{
				return this.<ExclusiveMaximum>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ExclusiveMaximum>k__BackingField = value;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002FB RID: 763 RVA: 0x0000C412 File Offset: 0x0000A612
		// (set) Token: 0x060002FC RID: 764 RVA: 0x0000C41A File Offset: 0x0000A61A
		public int? MinimumItems
		{
			[CompilerGenerated]
			get
			{
				return this.<MinimumItems>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<MinimumItems>k__BackingField = value;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002FD RID: 765 RVA: 0x0000C423 File Offset: 0x0000A623
		// (set) Token: 0x060002FE RID: 766 RVA: 0x0000C42B File Offset: 0x0000A62B
		public int? MaximumItems
		{
			[CompilerGenerated]
			get
			{
				return this.<MaximumItems>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<MaximumItems>k__BackingField = value;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002FF RID: 767 RVA: 0x0000C434 File Offset: 0x0000A634
		// (set) Token: 0x06000300 RID: 768 RVA: 0x0000C43C File Offset: 0x0000A63C
		public IList<string> Patterns
		{
			[CompilerGenerated]
			get
			{
				return this.<Patterns>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Patterns>k__BackingField = value;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000301 RID: 769 RVA: 0x0000C445 File Offset: 0x0000A645
		// (set) Token: 0x06000302 RID: 770 RVA: 0x0000C44D File Offset: 0x0000A64D
		public IList<JsonSchemaModel> Items
		{
			[CompilerGenerated]
			get
			{
				return this.<Items>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Items>k__BackingField = value;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000303 RID: 771 RVA: 0x0000C456 File Offset: 0x0000A656
		// (set) Token: 0x06000304 RID: 772 RVA: 0x0000C45E File Offset: 0x0000A65E
		public IDictionary<string, JsonSchemaModel> Properties
		{
			[CompilerGenerated]
			get
			{
				return this.<Properties>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Properties>k__BackingField = value;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000305 RID: 773 RVA: 0x0000C467 File Offset: 0x0000A667
		// (set) Token: 0x06000306 RID: 774 RVA: 0x0000C46F File Offset: 0x0000A66F
		public IDictionary<string, JsonSchemaModel> PatternProperties
		{
			[CompilerGenerated]
			get
			{
				return this.<PatternProperties>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<PatternProperties>k__BackingField = value;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000307 RID: 775 RVA: 0x0000C478 File Offset: 0x0000A678
		// (set) Token: 0x06000308 RID: 776 RVA: 0x0000C480 File Offset: 0x0000A680
		public JsonSchemaModel AdditionalProperties
		{
			[CompilerGenerated]
			get
			{
				return this.<AdditionalProperties>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<AdditionalProperties>k__BackingField = value;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000309 RID: 777 RVA: 0x0000C489 File Offset: 0x0000A689
		// (set) Token: 0x0600030A RID: 778 RVA: 0x0000C491 File Offset: 0x0000A691
		public JsonSchemaModel AdditionalItems
		{
			[CompilerGenerated]
			get
			{
				return this.<AdditionalItems>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<AdditionalItems>k__BackingField = value;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0000C49A File Offset: 0x0000A69A
		// (set) Token: 0x0600030C RID: 780 RVA: 0x0000C4A2 File Offset: 0x0000A6A2
		public bool PositionalItemsValidation
		{
			[CompilerGenerated]
			get
			{
				return this.<PositionalItemsValidation>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<PositionalItemsValidation>k__BackingField = value;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600030D RID: 781 RVA: 0x0000C4AB File Offset: 0x0000A6AB
		// (set) Token: 0x0600030E RID: 782 RVA: 0x0000C4B3 File Offset: 0x0000A6B3
		public bool AllowAdditionalProperties
		{
			[CompilerGenerated]
			get
			{
				return this.<AllowAdditionalProperties>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<AllowAdditionalProperties>k__BackingField = value;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600030F RID: 783 RVA: 0x0000C4BC File Offset: 0x0000A6BC
		// (set) Token: 0x06000310 RID: 784 RVA: 0x0000C4C4 File Offset: 0x0000A6C4
		public bool AllowAdditionalItems
		{
			[CompilerGenerated]
			get
			{
				return this.<AllowAdditionalItems>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<AllowAdditionalItems>k__BackingField = value;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000311 RID: 785 RVA: 0x0000C4CD File Offset: 0x0000A6CD
		// (set) Token: 0x06000312 RID: 786 RVA: 0x0000C4D5 File Offset: 0x0000A6D5
		public bool UniqueItems
		{
			[CompilerGenerated]
			get
			{
				return this.<UniqueItems>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<UniqueItems>k__BackingField = value;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000313 RID: 787 RVA: 0x0000C4DE File Offset: 0x0000A6DE
		// (set) Token: 0x06000314 RID: 788 RVA: 0x0000C4E6 File Offset: 0x0000A6E6
		public IList<JToken> Enum
		{
			[CompilerGenerated]
			get
			{
				return this.<Enum>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Enum>k__BackingField = value;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000315 RID: 789 RVA: 0x0000C4EF File Offset: 0x0000A6EF
		// (set) Token: 0x06000316 RID: 790 RVA: 0x0000C4F7 File Offset: 0x0000A6F7
		public JsonSchemaType Disallow
		{
			[CompilerGenerated]
			get
			{
				return this.<Disallow>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Disallow>k__BackingField = value;
			}
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000C500 File Offset: 0x0000A700
		public JsonSchemaModel()
		{
			this.Type = JsonSchemaType.Any;
			this.AllowAdditionalProperties = true;
			this.AllowAdditionalItems = true;
			this.Required = false;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000C528 File Offset: 0x0000A728
		public static JsonSchemaModel Create(IList<JsonSchema> schemata)
		{
			JsonSchemaModel jsonSchemaModel = new JsonSchemaModel();
			foreach (JsonSchema jsonSchema in schemata)
			{
				JsonSchemaModel.Combine(jsonSchemaModel, jsonSchema);
			}
			return jsonSchemaModel;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000C578 File Offset: 0x0000A778
		private static void Combine(JsonSchemaModel model, JsonSchema schema)
		{
			model.Required = model.Required || (schema.Required ?? false);
			model.Type &= schema.Type ?? JsonSchemaType.Any;
			model.MinimumLength = MathUtils.Max(model.MinimumLength, schema.MinimumLength);
			model.MaximumLength = MathUtils.Min(model.MaximumLength, schema.MaximumLength);
			model.DivisibleBy = MathUtils.Max(model.DivisibleBy, schema.DivisibleBy);
			model.Minimum = MathUtils.Max(model.Minimum, schema.Minimum);
			model.Maximum = MathUtils.Max(model.Maximum, schema.Maximum);
			model.ExclusiveMinimum = model.ExclusiveMinimum || (schema.ExclusiveMinimum ?? false);
			model.ExclusiveMaximum = model.ExclusiveMaximum || (schema.ExclusiveMaximum ?? false);
			model.MinimumItems = MathUtils.Max(model.MinimumItems, schema.MinimumItems);
			model.MaximumItems = MathUtils.Min(model.MaximumItems, schema.MaximumItems);
			model.PositionalItemsValidation = model.PositionalItemsValidation || schema.PositionalItemsValidation;
			model.AllowAdditionalProperties = model.AllowAdditionalProperties && schema.AllowAdditionalProperties;
			model.AllowAdditionalItems = model.AllowAdditionalItems && schema.AllowAdditionalItems;
			model.UniqueItems = model.UniqueItems || schema.UniqueItems;
			if (schema.Enum != null)
			{
				if (model.Enum == null)
				{
					model.Enum = new List<JToken>();
				}
				model.Enum.AddRangeDistinct(schema.Enum, JToken.EqualityComparer);
			}
			model.Disallow |= schema.Disallow ?? JsonSchemaType.None;
			if (schema.Pattern != null)
			{
				if (model.Patterns == null)
				{
					model.Patterns = new List<string>();
				}
				model.Patterns.AddDistinct(schema.Pattern);
			}
		}

		// Token: 0x0400013C RID: 316
		[CompilerGenerated]
		private bool <Required>k__BackingField;

		// Token: 0x0400013D RID: 317
		[CompilerGenerated]
		private JsonSchemaType <Type>k__BackingField;

		// Token: 0x0400013E RID: 318
		[CompilerGenerated]
		private int? <MinimumLength>k__BackingField;

		// Token: 0x0400013F RID: 319
		[CompilerGenerated]
		private int? <MaximumLength>k__BackingField;

		// Token: 0x04000140 RID: 320
		[CompilerGenerated]
		private double? <DivisibleBy>k__BackingField;

		// Token: 0x04000141 RID: 321
		[CompilerGenerated]
		private double? <Minimum>k__BackingField;

		// Token: 0x04000142 RID: 322
		[CompilerGenerated]
		private double? <Maximum>k__BackingField;

		// Token: 0x04000143 RID: 323
		[CompilerGenerated]
		private bool <ExclusiveMinimum>k__BackingField;

		// Token: 0x04000144 RID: 324
		[CompilerGenerated]
		private bool <ExclusiveMaximum>k__BackingField;

		// Token: 0x04000145 RID: 325
		[CompilerGenerated]
		private int? <MinimumItems>k__BackingField;

		// Token: 0x04000146 RID: 326
		[CompilerGenerated]
		private int? <MaximumItems>k__BackingField;

		// Token: 0x04000147 RID: 327
		[CompilerGenerated]
		private IList<string> <Patterns>k__BackingField;

		// Token: 0x04000148 RID: 328
		[CompilerGenerated]
		private IList<JsonSchemaModel> <Items>k__BackingField;

		// Token: 0x04000149 RID: 329
		[CompilerGenerated]
		private IDictionary<string, JsonSchemaModel> <Properties>k__BackingField;

		// Token: 0x0400014A RID: 330
		[CompilerGenerated]
		private IDictionary<string, JsonSchemaModel> <PatternProperties>k__BackingField;

		// Token: 0x0400014B RID: 331
		[CompilerGenerated]
		private JsonSchemaModel <AdditionalProperties>k__BackingField;

		// Token: 0x0400014C RID: 332
		[CompilerGenerated]
		private JsonSchemaModel <AdditionalItems>k__BackingField;

		// Token: 0x0400014D RID: 333
		[CompilerGenerated]
		private bool <PositionalItemsValidation>k__BackingField;

		// Token: 0x0400014E RID: 334
		[CompilerGenerated]
		private bool <AllowAdditionalProperties>k__BackingField;

		// Token: 0x0400014F RID: 335
		[CompilerGenerated]
		private bool <AllowAdditionalItems>k__BackingField;

		// Token: 0x04000150 RID: 336
		[CompilerGenerated]
		private bool <UniqueItems>k__BackingField;

		// Token: 0x04000151 RID: 337
		[CompilerGenerated]
		private IList<JToken> <Enum>k__BackingField;

		// Token: 0x04000152 RID: 338
		[CompilerGenerated]
		private JsonSchemaType <Disallow>k__BackingField;
	}
}
