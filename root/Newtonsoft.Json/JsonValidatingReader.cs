using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
	// Token: 0x0200001D RID: 29
	[Obsolete("JSON Schema validation has been moved to its own package. See http://www.newtonsoft.com/jsonschema for more details.")]
	public class JsonValidatingReader : JsonReader, IJsonLineInfo
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000087 RID: 135 RVA: 0x00002B6C File Offset: 0x00000D6C
		// (remove) Token: 0x06000088 RID: 136 RVA: 0x00002BA4 File Offset: 0x00000DA4
		public event ValidationEventHandler ValidationEventHandler
		{
			[CompilerGenerated]
			add
			{
				ValidationEventHandler validationEventHandler = this.ValidationEventHandler;
				ValidationEventHandler validationEventHandler2;
				do
				{
					validationEventHandler2 = validationEventHandler;
					ValidationEventHandler validationEventHandler3 = (ValidationEventHandler)Delegate.Combine(validationEventHandler2, value);
					validationEventHandler = Interlocked.CompareExchange<ValidationEventHandler>(ref this.ValidationEventHandler, validationEventHandler3, validationEventHandler2);
				}
				while (validationEventHandler != validationEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				ValidationEventHandler validationEventHandler = this.ValidationEventHandler;
				ValidationEventHandler validationEventHandler2;
				do
				{
					validationEventHandler2 = validationEventHandler;
					ValidationEventHandler validationEventHandler3 = (ValidationEventHandler)Delegate.Remove(validationEventHandler2, value);
					validationEventHandler = Interlocked.CompareExchange<ValidationEventHandler>(ref this.ValidationEventHandler, validationEventHandler3, validationEventHandler2);
				}
				while (validationEventHandler != validationEventHandler2);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00002BD9 File Offset: 0x00000DD9
		public override object Value
		{
			get
			{
				return this._reader.Value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00002BE6 File Offset: 0x00000DE6
		public override int Depth
		{
			get
			{
				return this._reader.Depth;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00002BF3 File Offset: 0x00000DF3
		public override string Path
		{
			get
			{
				return this._reader.Path;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00002C00 File Offset: 0x00000E00
		// (set) Token: 0x0600008D RID: 141 RVA: 0x00002C0D File Offset: 0x00000E0D
		public override char QuoteChar
		{
			get
			{
				return this._reader.QuoteChar;
			}
			protected internal set
			{
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00002C0F File Offset: 0x00000E0F
		public override JsonToken TokenType
		{
			get
			{
				return this._reader.TokenType;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00002C1C File Offset: 0x00000E1C
		public override Type ValueType
		{
			get
			{
				return this._reader.ValueType;
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00002C29 File Offset: 0x00000E29
		private void Push(JsonValidatingReader.SchemaScope scope)
		{
			this._stack.Push(scope);
			this._currentScope = scope;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00002C3E File Offset: 0x00000E3E
		private JsonValidatingReader.SchemaScope Pop()
		{
			JsonValidatingReader.SchemaScope schemaScope = this._stack.Pop();
			this._currentScope = ((this._stack.Count != 0) ? this._stack.Peek() : null);
			return schemaScope;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00002C6C File Offset: 0x00000E6C
		private IList<JsonSchemaModel> CurrentSchemas
		{
			get
			{
				return this._currentScope.Schemas;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00002C7C File Offset: 0x00000E7C
		private IList<JsonSchemaModel> CurrentMemberSchemas
		{
			get
			{
				if (this._currentScope == null)
				{
					return new List<JsonSchemaModel>(new JsonSchemaModel[] { this._model });
				}
				if (this._currentScope.Schemas == null || this._currentScope.Schemas.Count == 0)
				{
					return JsonValidatingReader.EmptySchemaList;
				}
				switch (this._currentScope.TokenType)
				{
				case JTokenType.None:
					return this._currentScope.Schemas;
				case JTokenType.Object:
				{
					if (this._currentScope.CurrentPropertyName == null)
					{
						throw new JsonReaderException("CurrentPropertyName has not been set on scope.");
					}
					IList<JsonSchemaModel> list = new List<JsonSchemaModel>();
					foreach (JsonSchemaModel jsonSchemaModel in this.CurrentSchemas)
					{
						JsonSchemaModel jsonSchemaModel2;
						if (jsonSchemaModel.Properties != null && jsonSchemaModel.Properties.TryGetValue(this._currentScope.CurrentPropertyName, ref jsonSchemaModel2))
						{
							list.Add(jsonSchemaModel2);
						}
						if (jsonSchemaModel.PatternProperties != null)
						{
							foreach (KeyValuePair<string, JsonSchemaModel> keyValuePair in jsonSchemaModel.PatternProperties)
							{
								if (Regex.IsMatch(this._currentScope.CurrentPropertyName, keyValuePair.Key))
								{
									list.Add(keyValuePair.Value);
								}
							}
						}
						if (list.Count == 0 && jsonSchemaModel.AllowAdditionalProperties && jsonSchemaModel.AdditionalProperties != null)
						{
							list.Add(jsonSchemaModel.AdditionalProperties);
						}
					}
					return list;
				}
				case JTokenType.Array:
				{
					IList<JsonSchemaModel> list2 = new List<JsonSchemaModel>();
					foreach (JsonSchemaModel jsonSchemaModel3 in this.CurrentSchemas)
					{
						if (!jsonSchemaModel3.PositionalItemsValidation)
						{
							if (jsonSchemaModel3.Items != null && jsonSchemaModel3.Items.Count > 0)
							{
								list2.Add(jsonSchemaModel3.Items[0]);
							}
						}
						else
						{
							if (jsonSchemaModel3.Items != null && jsonSchemaModel3.Items.Count > 0 && jsonSchemaModel3.Items.Count > this._currentScope.ArrayItemCount - 1)
							{
								list2.Add(jsonSchemaModel3.Items[this._currentScope.ArrayItemCount - 1]);
							}
							if (jsonSchemaModel3.AllowAdditionalItems && jsonSchemaModel3.AdditionalItems != null)
							{
								list2.Add(jsonSchemaModel3.AdditionalItems);
							}
						}
					}
					return list2;
				}
				case JTokenType.Constructor:
					return JsonValidatingReader.EmptySchemaList;
				default:
					throw new ArgumentOutOfRangeException("TokenType", "Unexpected token type: {0}".FormatWith(CultureInfo.InvariantCulture, this._currentScope.TokenType));
				}
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00002F3C File Offset: 0x0000113C
		private void RaiseError(string message, JsonSchemaModel schema)
		{
			string text = (((IJsonLineInfo)this).HasLineInfo() ? (message + " Line {0}, position {1}.".FormatWith(CultureInfo.InvariantCulture, ((IJsonLineInfo)this).LineNumber, ((IJsonLineInfo)this).LinePosition)) : message);
			this.OnValidationEvent(new JsonSchemaException(text, null, this.Path, ((IJsonLineInfo)this).LineNumber, ((IJsonLineInfo)this).LinePosition));
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00002FA4 File Offset: 0x000011A4
		private void OnValidationEvent(JsonSchemaException exception)
		{
			ValidationEventHandler validationEventHandler = this.ValidationEventHandler;
			if (validationEventHandler != null)
			{
				validationEventHandler(this, new ValidationEventArgs(exception));
				return;
			}
			throw exception;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00002FCA File Offset: 0x000011CA
		public JsonValidatingReader(JsonReader reader)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			this._reader = reader;
			this._stack = new Stack<JsonValidatingReader.SchemaScope>();
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00002FEF File Offset: 0x000011EF
		// (set) Token: 0x06000098 RID: 152 RVA: 0x00002FF7 File Offset: 0x000011F7
		public JsonSchema Schema
		{
			get
			{
				return this._schema;
			}
			set
			{
				if (this.TokenType != JsonToken.None)
				{
					throw new InvalidOperationException("Cannot change schema while validating JSON.");
				}
				this._schema = value;
				this._model = null;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000099 RID: 153 RVA: 0x0000301A File Offset: 0x0000121A
		public JsonReader Reader
		{
			get
			{
				return this._reader;
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003022 File Offset: 0x00001222
		public override void Close()
		{
			base.Close();
			if (base.CloseInput)
			{
				JsonReader reader = this._reader;
				if (reader == null)
				{
					return;
				}
				reader.Close();
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003044 File Offset: 0x00001244
		private void ValidateNotDisallowed(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			JsonSchemaType? currentNodeSchemaType = this.GetCurrentNodeSchemaType();
			if (currentNodeSchemaType != null && JsonSchemaGenerator.HasFlag(new JsonSchemaType?(schema.Disallow), currentNodeSchemaType.GetValueOrDefault()))
			{
				this.RaiseError("Type {0} is disallowed.".FormatWith(CultureInfo.InvariantCulture, currentNodeSchemaType), schema);
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000309C File Offset: 0x0000129C
		private JsonSchemaType? GetCurrentNodeSchemaType()
		{
			switch (this._reader.TokenType)
			{
			case JsonToken.StartObject:
				return new JsonSchemaType?(JsonSchemaType.Object);
			case JsonToken.StartArray:
				return new JsonSchemaType?(JsonSchemaType.Array);
			case JsonToken.Integer:
				return new JsonSchemaType?(JsonSchemaType.Integer);
			case JsonToken.Float:
				return new JsonSchemaType?(JsonSchemaType.Float);
			case JsonToken.String:
				return new JsonSchemaType?(JsonSchemaType.String);
			case JsonToken.Boolean:
				return new JsonSchemaType?(JsonSchemaType.Boolean);
			case JsonToken.Null:
				return new JsonSchemaType?(JsonSchemaType.Null);
			}
			return default(JsonSchemaType?);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003128 File Offset: 0x00001328
		public override int? ReadAsInt32()
		{
			int? num = this._reader.ReadAsInt32();
			this.ValidateCurrentToken();
			return num;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000313B File Offset: 0x0000133B
		public override byte[] ReadAsBytes()
		{
			byte[] array = this._reader.ReadAsBytes();
			this.ValidateCurrentToken();
			return array;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000314E File Offset: 0x0000134E
		public override decimal? ReadAsDecimal()
		{
			decimal? num = this._reader.ReadAsDecimal();
			this.ValidateCurrentToken();
			return num;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003161 File Offset: 0x00001361
		public override double? ReadAsDouble()
		{
			double? num = this._reader.ReadAsDouble();
			this.ValidateCurrentToken();
			return num;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003174 File Offset: 0x00001374
		public override bool? ReadAsBoolean()
		{
			bool? flag = this._reader.ReadAsBoolean();
			this.ValidateCurrentToken();
			return flag;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003187 File Offset: 0x00001387
		public override string ReadAsString()
		{
			string text = this._reader.ReadAsString();
			this.ValidateCurrentToken();
			return text;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000319A File Offset: 0x0000139A
		public override DateTime? ReadAsDateTime()
		{
			DateTime? dateTime = this._reader.ReadAsDateTime();
			this.ValidateCurrentToken();
			return dateTime;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000031AD File Offset: 0x000013AD
		public override DateTimeOffset? ReadAsDateTimeOffset()
		{
			DateTimeOffset? dateTimeOffset = this._reader.ReadAsDateTimeOffset();
			this.ValidateCurrentToken();
			return dateTimeOffset;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000031C0 File Offset: 0x000013C0
		public override bool Read()
		{
			if (!this._reader.Read())
			{
				return false;
			}
			if (this._reader.TokenType == JsonToken.Comment)
			{
				return true;
			}
			this.ValidateCurrentToken();
			return true;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000031E8 File Offset: 0x000013E8
		private void ValidateCurrentToken()
		{
			if (this._model == null)
			{
				JsonSchemaModelBuilder jsonSchemaModelBuilder = new JsonSchemaModelBuilder();
				this._model = jsonSchemaModelBuilder.Build(this._schema);
				if (!JsonTokenUtils.IsStartToken(this._reader.TokenType))
				{
					this.Push(new JsonValidatingReader.SchemaScope(JTokenType.None, this.CurrentMemberSchemas));
				}
			}
			switch (this._reader.TokenType)
			{
			case JsonToken.None:
				return;
			case JsonToken.StartObject:
			{
				this.ProcessValue();
				IList<JsonSchemaModel> list = Enumerable.ToList<JsonSchemaModel>(Enumerable.Where<JsonSchemaModel>(this.CurrentMemberSchemas, new Func<JsonSchemaModel, bool>(this.ValidateObject)));
				this.Push(new JsonValidatingReader.SchemaScope(JTokenType.Object, list));
				this.WriteToken(this.CurrentSchemas);
				return;
			}
			case JsonToken.StartArray:
			{
				this.ProcessValue();
				IList<JsonSchemaModel> list2 = Enumerable.ToList<JsonSchemaModel>(Enumerable.Where<JsonSchemaModel>(this.CurrentMemberSchemas, new Func<JsonSchemaModel, bool>(this.ValidateArray)));
				this.Push(new JsonValidatingReader.SchemaScope(JTokenType.Array, list2));
				this.WriteToken(this.CurrentSchemas);
				return;
			}
			case JsonToken.StartConstructor:
				this.ProcessValue();
				this.Push(new JsonValidatingReader.SchemaScope(JTokenType.Constructor, null));
				this.WriteToken(this.CurrentSchemas);
				return;
			case JsonToken.PropertyName:
			{
				this.WriteToken(this.CurrentSchemas);
				using (IEnumerator<JsonSchemaModel> enumerator = this.CurrentSchemas.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						JsonSchemaModel jsonSchemaModel = enumerator.Current;
						this.ValidatePropertyName(jsonSchemaModel);
					}
					return;
				}
				break;
			}
			case JsonToken.Comment:
				goto IL_03BD;
			case JsonToken.Raw:
				break;
			case JsonToken.Integer:
			{
				this.ProcessValue();
				this.WriteToken(this.CurrentMemberSchemas);
				using (IEnumerator<JsonSchemaModel> enumerator = this.CurrentMemberSchemas.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						JsonSchemaModel jsonSchemaModel2 = enumerator.Current;
						this.ValidateInteger(jsonSchemaModel2);
					}
					return;
				}
				goto IL_01D6;
			}
			case JsonToken.Float:
				goto IL_01D6;
			case JsonToken.String:
				goto IL_0222;
			case JsonToken.Boolean:
				goto IL_026E;
			case JsonToken.Null:
				goto IL_02BA;
			case JsonToken.Undefined:
			case JsonToken.Date:
			case JsonToken.Bytes:
				this.WriteToken(this.CurrentMemberSchemas);
				return;
			case JsonToken.EndObject:
				goto IL_0306;
			case JsonToken.EndArray:
				this.WriteToken(this.CurrentSchemas);
				foreach (JsonSchemaModel jsonSchemaModel3 in this.CurrentSchemas)
				{
					this.ValidateEndArray(jsonSchemaModel3);
				}
				this.Pop();
				return;
			case JsonToken.EndConstructor:
				this.WriteToken(this.CurrentSchemas);
				this.Pop();
				return;
			default:
				goto IL_03BD;
			}
			this.ProcessValue();
			return;
			IL_01D6:
			this.ProcessValue();
			this.WriteToken(this.CurrentMemberSchemas);
			using (IEnumerator<JsonSchemaModel> enumerator = this.CurrentMemberSchemas.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					JsonSchemaModel jsonSchemaModel4 = enumerator.Current;
					this.ValidateFloat(jsonSchemaModel4);
				}
				return;
			}
			IL_0222:
			this.ProcessValue();
			this.WriteToken(this.CurrentMemberSchemas);
			using (IEnumerator<JsonSchemaModel> enumerator = this.CurrentMemberSchemas.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					JsonSchemaModel jsonSchemaModel5 = enumerator.Current;
					this.ValidateString(jsonSchemaModel5);
				}
				return;
			}
			IL_026E:
			this.ProcessValue();
			this.WriteToken(this.CurrentMemberSchemas);
			using (IEnumerator<JsonSchemaModel> enumerator = this.CurrentMemberSchemas.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					JsonSchemaModel jsonSchemaModel6 = enumerator.Current;
					this.ValidateBoolean(jsonSchemaModel6);
				}
				return;
			}
			IL_02BA:
			this.ProcessValue();
			this.WriteToken(this.CurrentMemberSchemas);
			using (IEnumerator<JsonSchemaModel> enumerator = this.CurrentMemberSchemas.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					JsonSchemaModel jsonSchemaModel7 = enumerator.Current;
					this.ValidateNull(jsonSchemaModel7);
				}
				return;
			}
			IL_0306:
			this.WriteToken(this.CurrentSchemas);
			foreach (JsonSchemaModel jsonSchemaModel8 in this.CurrentSchemas)
			{
				this.ValidateEndObject(jsonSchemaModel8);
			}
			this.Pop();
			return;
			IL_03BD:
			throw new ArgumentOutOfRangeException();
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000361C File Offset: 0x0000181C
		private void WriteToken(IList<JsonSchemaModel> schemas)
		{
			foreach (JsonValidatingReader.SchemaScope schemaScope in this._stack)
			{
				bool flag = schemaScope.TokenType == JTokenType.Array && schemaScope.IsUniqueArray && schemaScope.ArrayItemCount > 0;
				if (!flag)
				{
					if (!Enumerable.Any<JsonSchemaModel>(schemas, (JsonSchemaModel s) => s.Enum != null))
					{
						continue;
					}
				}
				if (schemaScope.CurrentItemWriter == null)
				{
					if (JsonTokenUtils.IsEndToken(this._reader.TokenType))
					{
						continue;
					}
					schemaScope.CurrentItemWriter = new JTokenWriter();
				}
				schemaScope.CurrentItemWriter.WriteToken(this._reader, false);
				if (schemaScope.CurrentItemWriter.Top == 0 && this._reader.TokenType != JsonToken.PropertyName)
				{
					JToken token = schemaScope.CurrentItemWriter.Token;
					schemaScope.CurrentItemWriter = null;
					if (flag)
					{
						if (Enumerable.Contains<JToken>(schemaScope.UniqueArrayItems, token, JToken.EqualityComparer))
						{
							this.RaiseError("Non-unique array item at index {0}.".FormatWith(CultureInfo.InvariantCulture, schemaScope.ArrayItemCount - 1), Enumerable.First<JsonSchemaModel>(schemaScope.Schemas, (JsonSchemaModel s) => s.UniqueItems));
						}
						schemaScope.UniqueArrayItems.Add(token);
					}
					else if (Enumerable.Any<JsonSchemaModel>(schemas, (JsonSchemaModel s) => s.Enum != null))
					{
						foreach (JsonSchemaModel jsonSchemaModel in schemas)
						{
							if (jsonSchemaModel.Enum != null && !jsonSchemaModel.Enum.ContainsValue(token, JToken.EqualityComparer))
							{
								StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
								token.WriteTo(new JsonTextWriter(stringWriter), new JsonConverter[0]);
								this.RaiseError("Value {0} is not defined in enum.".FormatWith(CultureInfo.InvariantCulture, stringWriter.ToString()), jsonSchemaModel);
							}
						}
					}
				}
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000386C File Offset: 0x00001A6C
		private void ValidateEndObject(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			Dictionary<string, bool> requiredProperties = this._currentScope.RequiredProperties;
			if (requiredProperties != null)
			{
				if (Enumerable.Any<bool>(requiredProperties.Values, (bool v) => !v))
				{
					IEnumerable<string> enumerable = Enumerable.Select<KeyValuePair<string, bool>, string>(Enumerable.Where<KeyValuePair<string, bool>>(requiredProperties, (KeyValuePair<string, bool> kv) => !kv.Value), (KeyValuePair<string, bool> kv) => kv.Key);
					this.RaiseError("Required properties are missing from object: {0}.".FormatWith(CultureInfo.InvariantCulture, string.Join(", ", enumerable)), schema);
				}
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003928 File Offset: 0x00001B28
		private void ValidateEndArray(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			int arrayItemCount = this._currentScope.ArrayItemCount;
			if (schema.MaximumItems != null && arrayItemCount > schema.MaximumItems)
			{
				this.RaiseError("Array item count {0} exceeds maximum count of {1}.".FormatWith(CultureInfo.InvariantCulture, arrayItemCount, schema.MaximumItems), schema);
			}
			if (schema.MinimumItems != null && arrayItemCount < schema.MinimumItems)
			{
				this.RaiseError("Array item count {0} is less than minimum count of {1}.".FormatWith(CultureInfo.InvariantCulture, arrayItemCount, schema.MinimumItems), schema);
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000039ED File Offset: 0x00001BED
		private void ValidateNull(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			if (!this.TestType(schema, JsonSchemaType.Null))
			{
				return;
			}
			this.ValidateNotDisallowed(schema);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003A06 File Offset: 0x00001C06
		private void ValidateBoolean(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			if (!this.TestType(schema, JsonSchemaType.Boolean))
			{
				return;
			}
			this.ValidateNotDisallowed(schema);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003A20 File Offset: 0x00001C20
		private void ValidateString(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			if (!this.TestType(schema, JsonSchemaType.String))
			{
				return;
			}
			this.ValidateNotDisallowed(schema);
			string text = this._reader.Value.ToString();
			if (schema.MaximumLength != null && text.Length > schema.MaximumLength)
			{
				this.RaiseError("String '{0}' exceeds maximum length of {1}.".FormatWith(CultureInfo.InvariantCulture, text, schema.MaximumLength), schema);
			}
			if (schema.MinimumLength != null && text.Length < schema.MinimumLength)
			{
				this.RaiseError("String '{0}' is less than minimum length of {1}.".FormatWith(CultureInfo.InvariantCulture, text, schema.MinimumLength), schema);
			}
			if (schema.Patterns != null)
			{
				foreach (string text2 in schema.Patterns)
				{
					if (!Regex.IsMatch(text, text2))
					{
						this.RaiseError("String '{0}' does not match regex pattern '{1}'.".FormatWith(CultureInfo.InvariantCulture, text, text2), schema);
					}
				}
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003B60 File Offset: 0x00001D60
		private void ValidateInteger(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			if (!this.TestType(schema, JsonSchemaType.Integer))
			{
				return;
			}
			this.ValidateNotDisallowed(schema);
			object value = this._reader.Value;
			if (schema.Maximum != null)
			{
				if (JValue.Compare(JTokenType.Integer, value, schema.Maximum) > 0)
				{
					this.RaiseError("Integer {0} exceeds maximum value of {1}.".FormatWith(CultureInfo.InvariantCulture, value, schema.Maximum), schema);
				}
				if (schema.ExclusiveMaximum && JValue.Compare(JTokenType.Integer, value, schema.Maximum) == 0)
				{
					this.RaiseError("Integer {0} equals maximum value of {1} and exclusive maximum is true.".FormatWith(CultureInfo.InvariantCulture, value, schema.Maximum), schema);
				}
			}
			if (schema.Minimum != null)
			{
				if (JValue.Compare(JTokenType.Integer, value, schema.Minimum) < 0)
				{
					this.RaiseError("Integer {0} is less than minimum value of {1}.".FormatWith(CultureInfo.InvariantCulture, value, schema.Minimum), schema);
				}
				if (schema.ExclusiveMinimum && JValue.Compare(JTokenType.Integer, value, schema.Minimum) == 0)
				{
					this.RaiseError("Integer {0} equals minimum value of {1} and exclusive minimum is true.".FormatWith(CultureInfo.InvariantCulture, value, schema.Minimum), schema);
				}
			}
			if (schema.DivisibleBy != null)
			{
				bool flag;
				if (value is BigInteger)
				{
					BigInteger bigInteger = (BigInteger)value;
					if (!Math.Abs(schema.DivisibleBy.Value - Math.Truncate(schema.DivisibleBy.Value)).Equals(0.0))
					{
						flag = bigInteger != 0L;
					}
					else
					{
						flag = bigInteger % new BigInteger(schema.DivisibleBy.Value) != 0L;
					}
				}
				else
				{
					flag = !JsonValidatingReader.IsZero((double)Convert.ToInt64(value, CultureInfo.InvariantCulture) % schema.DivisibleBy.GetValueOrDefault());
				}
				if (flag)
				{
					this.RaiseError("Integer {0} is not evenly divisible by {1}.".FormatWith(CultureInfo.InvariantCulture, JsonConvert.ToString(value), schema.DivisibleBy), schema);
				}
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003D78 File Offset: 0x00001F78
		private void ProcessValue()
		{
			if (this._currentScope != null && this._currentScope.TokenType == JTokenType.Array)
			{
				JsonValidatingReader.SchemaScope currentScope = this._currentScope;
				int arrayItemCount = currentScope.ArrayItemCount;
				currentScope.ArrayItemCount = arrayItemCount + 1;
				foreach (JsonSchemaModel jsonSchemaModel in this.CurrentSchemas)
				{
					if (jsonSchemaModel != null && jsonSchemaModel.PositionalItemsValidation && !jsonSchemaModel.AllowAdditionalItems && (jsonSchemaModel.Items == null || this._currentScope.ArrayItemCount - 1 >= jsonSchemaModel.Items.Count))
					{
						this.RaiseError("Index {0} has not been defined and the schema does not allow additional items.".FormatWith(CultureInfo.InvariantCulture, this._currentScope.ArrayItemCount), jsonSchemaModel);
					}
				}
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003E4C File Offset: 0x0000204C
		private void ValidateFloat(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			if (!this.TestType(schema, JsonSchemaType.Float))
			{
				return;
			}
			this.ValidateNotDisallowed(schema);
			double num = Convert.ToDouble(this._reader.Value, CultureInfo.InvariantCulture);
			if (schema.Maximum != null)
			{
				if (num > schema.Maximum)
				{
					this.RaiseError("Float {0} exceeds maximum value of {1}.".FormatWith(CultureInfo.InvariantCulture, JsonConvert.ToString(num), schema.Maximum), schema);
				}
				if (schema.ExclusiveMaximum && num == schema.Maximum)
				{
					this.RaiseError("Float {0} equals maximum value of {1} and exclusive maximum is true.".FormatWith(CultureInfo.InvariantCulture, JsonConvert.ToString(num), schema.Maximum), schema);
				}
			}
			if (schema.Minimum != null)
			{
				if (num < schema.Minimum)
				{
					this.RaiseError("Float {0} is less than minimum value of {1}.".FormatWith(CultureInfo.InvariantCulture, JsonConvert.ToString(num), schema.Minimum), schema);
				}
				if (schema.ExclusiveMinimum && num == schema.Minimum)
				{
					this.RaiseError("Float {0} equals minimum value of {1} and exclusive minimum is true.".FormatWith(CultureInfo.InvariantCulture, JsonConvert.ToString(num), schema.Minimum), schema);
				}
			}
			if (schema.DivisibleBy != null && !JsonValidatingReader.IsZero(JsonValidatingReader.FloatingPointRemainder(num, schema.DivisibleBy.GetValueOrDefault())))
			{
				this.RaiseError("Float {0} is not evenly divisible by {1}.".FormatWith(CultureInfo.InvariantCulture, JsonConvert.ToString(num), schema.DivisibleBy), schema);
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x0000401D File Offset: 0x0000221D
		private static double FloatingPointRemainder(double dividend, double divisor)
		{
			return dividend - Math.Floor(dividend / divisor) * divisor;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000402B File Offset: 0x0000222B
		private static bool IsZero(double value)
		{
			return Math.Abs(value) < 4.440892098500626E-15;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004040 File Offset: 0x00002240
		private void ValidatePropertyName(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			string text = Convert.ToString(this._reader.Value, CultureInfo.InvariantCulture);
			if (this._currentScope.RequiredProperties.ContainsKey(text))
			{
				this._currentScope.RequiredProperties[text] = true;
			}
			if (!schema.AllowAdditionalProperties && !this.IsPropertyDefinied(schema, text))
			{
				this.RaiseError("Property '{0}' has not been defined and the schema does not allow additional properties.".FormatWith(CultureInfo.InvariantCulture, text), schema);
			}
			this._currentScope.CurrentPropertyName = text;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000040C4 File Offset: 0x000022C4
		private bool IsPropertyDefinied(JsonSchemaModel schema, string propertyName)
		{
			if (schema.Properties != null && schema.Properties.ContainsKey(propertyName))
			{
				return true;
			}
			if (schema.PatternProperties != null)
			{
				foreach (string text in schema.PatternProperties.Keys)
				{
					if (Regex.IsMatch(propertyName, text))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00004140 File Offset: 0x00002340
		private bool ValidateArray(JsonSchemaModel schema)
		{
			return schema == null || this.TestType(schema, JsonSchemaType.Array);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00004150 File Offset: 0x00002350
		private bool ValidateObject(JsonSchemaModel schema)
		{
			return schema == null || this.TestType(schema, JsonSchemaType.Object);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004160 File Offset: 0x00002360
		private bool TestType(JsonSchemaModel currentSchema, JsonSchemaType currentType)
		{
			if (!JsonSchemaGenerator.HasFlag(new JsonSchemaType?(currentSchema.Type), currentType))
			{
				this.RaiseError("Invalid type. Expected {0} but got {1}.".FormatWith(CultureInfo.InvariantCulture, currentSchema.Type, currentType), currentSchema);
				return false;
			}
			return true;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000041A0 File Offset: 0x000023A0
		bool IJsonLineInfo.HasLineInfo()
		{
			IJsonLineInfo jsonLineInfo = this._reader as IJsonLineInfo;
			return jsonLineInfo != null && jsonLineInfo.HasLineInfo();
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x000041C4 File Offset: 0x000023C4
		int IJsonLineInfo.LineNumber
		{
			get
			{
				IJsonLineInfo jsonLineInfo = this._reader as IJsonLineInfo;
				if (jsonLineInfo == null)
				{
					return 0;
				}
				return jsonLineInfo.LineNumber;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x000041E8 File Offset: 0x000023E8
		int IJsonLineInfo.LinePosition
		{
			get
			{
				IJsonLineInfo jsonLineInfo = this._reader as IJsonLineInfo;
				if (jsonLineInfo == null)
				{
					return 0;
				}
				return jsonLineInfo.LinePosition;
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000420C File Offset: 0x0000240C
		// Note: this type is marked as 'beforefieldinit'.
		static JsonValidatingReader()
		{
		}

		// Token: 0x04000085 RID: 133
		private readonly JsonReader _reader;

		// Token: 0x04000086 RID: 134
		private readonly Stack<JsonValidatingReader.SchemaScope> _stack;

		// Token: 0x04000087 RID: 135
		private JsonSchema _schema;

		// Token: 0x04000088 RID: 136
		private JsonSchemaModel _model;

		// Token: 0x04000089 RID: 137
		private JsonValidatingReader.SchemaScope _currentScope;

		// Token: 0x0400008A RID: 138
		[CompilerGenerated]
		private ValidationEventHandler ValidationEventHandler;

		// Token: 0x0400008B RID: 139
		private static readonly IList<JsonSchemaModel> EmptySchemaList = new List<JsonSchemaModel>();

		// Token: 0x02000104 RID: 260
		private class SchemaScope
		{
			// Token: 0x1700027D RID: 637
			// (get) Token: 0x06000C47 RID: 3143 RVA: 0x00030854 File Offset: 0x0002EA54
			// (set) Token: 0x06000C48 RID: 3144 RVA: 0x0003085C File Offset: 0x0002EA5C
			public string CurrentPropertyName
			{
				[CompilerGenerated]
				get
				{
					return this.<CurrentPropertyName>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<CurrentPropertyName>k__BackingField = value;
				}
			}

			// Token: 0x1700027E RID: 638
			// (get) Token: 0x06000C49 RID: 3145 RVA: 0x00030865 File Offset: 0x0002EA65
			// (set) Token: 0x06000C4A RID: 3146 RVA: 0x0003086D File Offset: 0x0002EA6D
			public int ArrayItemCount
			{
				[CompilerGenerated]
				get
				{
					return this.<ArrayItemCount>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<ArrayItemCount>k__BackingField = value;
				}
			}

			// Token: 0x1700027F RID: 639
			// (get) Token: 0x06000C4B RID: 3147 RVA: 0x00030876 File Offset: 0x0002EA76
			public bool IsUniqueArray
			{
				[CompilerGenerated]
				get
				{
					return this.<IsUniqueArray>k__BackingField;
				}
			}

			// Token: 0x17000280 RID: 640
			// (get) Token: 0x06000C4C RID: 3148 RVA: 0x0003087E File Offset: 0x0002EA7E
			public IList<JToken> UniqueArrayItems
			{
				[CompilerGenerated]
				get
				{
					return this.<UniqueArrayItems>k__BackingField;
				}
			}

			// Token: 0x17000281 RID: 641
			// (get) Token: 0x06000C4D RID: 3149 RVA: 0x00030886 File Offset: 0x0002EA86
			// (set) Token: 0x06000C4E RID: 3150 RVA: 0x0003088E File Offset: 0x0002EA8E
			public JTokenWriter CurrentItemWriter
			{
				[CompilerGenerated]
				get
				{
					return this.<CurrentItemWriter>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<CurrentItemWriter>k__BackingField = value;
				}
			}

			// Token: 0x17000282 RID: 642
			// (get) Token: 0x06000C4F RID: 3151 RVA: 0x00030897 File Offset: 0x0002EA97
			public IList<JsonSchemaModel> Schemas
			{
				get
				{
					return this._schemas;
				}
			}

			// Token: 0x17000283 RID: 643
			// (get) Token: 0x06000C50 RID: 3152 RVA: 0x0003089F File Offset: 0x0002EA9F
			public Dictionary<string, bool> RequiredProperties
			{
				get
				{
					return this._requiredProperties;
				}
			}

			// Token: 0x17000284 RID: 644
			// (get) Token: 0x06000C51 RID: 3153 RVA: 0x000308A7 File Offset: 0x0002EAA7
			public JTokenType TokenType
			{
				get
				{
					return this._tokenType;
				}
			}

			// Token: 0x06000C52 RID: 3154 RVA: 0x000308B0 File Offset: 0x0002EAB0
			public SchemaScope(JTokenType tokenType, IList<JsonSchemaModel> schemas)
			{
				this._tokenType = tokenType;
				this._schemas = schemas;
				this._requiredProperties = Enumerable.ToDictionary<string, string, bool>(Enumerable.Distinct<string>(Enumerable.SelectMany<JsonSchemaModel, string>(schemas, new Func<JsonSchemaModel, IEnumerable<string>>(this.GetRequiredProperties))), (string p) => p, (string p) => false);
				if (tokenType == JTokenType.Array)
				{
					if (Enumerable.Any<JsonSchemaModel>(schemas, (JsonSchemaModel s) => s.UniqueItems))
					{
						this.IsUniqueArray = true;
						this.UniqueArrayItems = new List<JToken>();
					}
				}
			}

			// Token: 0x06000C53 RID: 3155 RVA: 0x00030970 File Offset: 0x0002EB70
			private IEnumerable<string> GetRequiredProperties(JsonSchemaModel schema)
			{
				if (((schema != null) ? schema.Properties : null) == null)
				{
					return Enumerable.Empty<string>();
				}
				return Enumerable.Select<KeyValuePair<string, JsonSchemaModel>, string>(Enumerable.Where<KeyValuePair<string, JsonSchemaModel>>(schema.Properties, (KeyValuePair<string, JsonSchemaModel> p) => p.Value.Required), (KeyValuePair<string, JsonSchemaModel> p) => p.Key);
			}

			// Token: 0x04000410 RID: 1040
			private readonly JTokenType _tokenType;

			// Token: 0x04000411 RID: 1041
			private readonly IList<JsonSchemaModel> _schemas;

			// Token: 0x04000412 RID: 1042
			private readonly Dictionary<string, bool> _requiredProperties;

			// Token: 0x04000413 RID: 1043
			[CompilerGenerated]
			private string <CurrentPropertyName>k__BackingField;

			// Token: 0x04000414 RID: 1044
			[CompilerGenerated]
			private int <ArrayItemCount>k__BackingField;

			// Token: 0x04000415 RID: 1045
			[CompilerGenerated]
			private readonly bool <IsUniqueArray>k__BackingField;

			// Token: 0x04000416 RID: 1046
			[CompilerGenerated]
			private readonly IList<JToken> <UniqueArrayItems>k__BackingField;

			// Token: 0x04000417 RID: 1047
			[CompilerGenerated]
			private JTokenWriter <CurrentItemWriter>k__BackingField;

			// Token: 0x02000179 RID: 377
			[CompilerGenerated]
			[Serializable]
			private sealed class <>c
			{
				// Token: 0x06000DE7 RID: 3559 RVA: 0x000342CF File Offset: 0x000324CF
				// Note: this type is marked as 'beforefieldinit'.
				static <>c()
				{
				}

				// Token: 0x06000DE8 RID: 3560 RVA: 0x00008020 File Offset: 0x00006220
				public <>c()
				{
				}

				// Token: 0x06000DE9 RID: 3561 RVA: 0x00017F2F File Offset: 0x0001612F
				internal string <.ctor>b__27_0(string p)
				{
					return p;
				}

				// Token: 0x06000DEA RID: 3562 RVA: 0x0000F9CA File Offset: 0x0000DBCA
				internal bool <.ctor>b__27_1(string p)
				{
					return false;
				}

				// Token: 0x06000DEB RID: 3563 RVA: 0x000309F6 File Offset: 0x0002EBF6
				internal bool <.ctor>b__27_2(JsonSchemaModel s)
				{
					return s.UniqueItems;
				}

				// Token: 0x06000DEC RID: 3564 RVA: 0x000342DB File Offset: 0x000324DB
				internal bool <GetRequiredProperties>b__28_0(KeyValuePair<string, JsonSchemaModel> p)
				{
					return p.Value.Required;
				}

				// Token: 0x06000DED RID: 3565 RVA: 0x000342E9 File Offset: 0x000324E9
				internal string <GetRequiredProperties>b__28_1(KeyValuePair<string, JsonSchemaModel> p)
				{
					return p.Key;
				}

				// Token: 0x04000598 RID: 1432
				public static readonly JsonValidatingReader.SchemaScope.<>c <>9 = new JsonValidatingReader.SchemaScope.<>c();

				// Token: 0x04000599 RID: 1433
				public static Func<string, string> <>9__27_0;

				// Token: 0x0400059A RID: 1434
				public static Func<string, bool> <>9__27_1;

				// Token: 0x0400059B RID: 1435
				public static Func<JsonSchemaModel, bool> <>9__27_2;

				// Token: 0x0400059C RID: 1436
				public static Func<KeyValuePair<string, JsonSchemaModel>, bool> <>9__28_0;

				// Token: 0x0400059D RID: 1437
				public static Func<KeyValuePair<string, JsonSchemaModel>, string> <>9__28_1;
			}
		}

		// Token: 0x02000105 RID: 261
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000C54 RID: 3156 RVA: 0x000309DF File Offset: 0x0002EBDF
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000C55 RID: 3157 RVA: 0x00008020 File Offset: 0x00006220
			public <>c()
			{
			}

			// Token: 0x06000C56 RID: 3158 RVA: 0x000309EB File Offset: 0x0002EBEB
			internal bool <WriteToken>b__50_0(JsonSchemaModel s)
			{
				return s.Enum != null;
			}

			// Token: 0x06000C57 RID: 3159 RVA: 0x000309F6 File Offset: 0x0002EBF6
			internal bool <WriteToken>b__50_1(JsonSchemaModel s)
			{
				return s.UniqueItems;
			}

			// Token: 0x06000C58 RID: 3160 RVA: 0x000309EB File Offset: 0x0002EBEB
			internal bool <WriteToken>b__50_2(JsonSchemaModel s)
			{
				return s.Enum != null;
			}

			// Token: 0x06000C59 RID: 3161 RVA: 0x000309FE File Offset: 0x0002EBFE
			internal bool <ValidateEndObject>b__51_0(bool v)
			{
				return !v;
			}

			// Token: 0x06000C5A RID: 3162 RVA: 0x00030A04 File Offset: 0x0002EC04
			internal bool <ValidateEndObject>b__51_1(KeyValuePair<string, bool> kv)
			{
				return !kv.Value;
			}

			// Token: 0x06000C5B RID: 3163 RVA: 0x00030A10 File Offset: 0x0002EC10
			internal string <ValidateEndObject>b__51_2(KeyValuePair<string, bool> kv)
			{
				return kv.Key;
			}

			// Token: 0x04000418 RID: 1048
			public static readonly JsonValidatingReader.<>c <>9 = new JsonValidatingReader.<>c();

			// Token: 0x04000419 RID: 1049
			public static Func<JsonSchemaModel, bool> <>9__50_0;

			// Token: 0x0400041A RID: 1050
			public static Func<JsonSchemaModel, bool> <>9__50_1;

			// Token: 0x0400041B RID: 1051
			public static Func<JsonSchemaModel, bool> <>9__50_2;

			// Token: 0x0400041C RID: 1052
			public static Func<bool, bool> <>9__51_0;

			// Token: 0x0400041D RID: 1053
			public static Func<KeyValuePair<string, bool>, bool> <>9__51_1;

			// Token: 0x0400041E RID: 1054
			public static Func<KeyValuePair<string, bool>, string> <>9__51_2;
		}
	}
}
