using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000D3 RID: 211
	public class DiscriminatedUnionConverter : JsonConverter
	{
		// Token: 0x06000AB4 RID: 2740 RVA: 0x0002B1B0 File Offset: 0x000293B0
		private static Type CreateUnionTypeLookup(Type t)
		{
			MethodCall<object, object> getUnionCases = FSharpUtils.GetUnionCases;
			object obj = null;
			object[] array = new object[2];
			array[0] = t;
			object obj2 = Enumerable.First<object>((object[])getUnionCases(obj, array));
			return (Type)FSharpUtils.GetUnionCaseInfoDeclaringType.Invoke(obj2);
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x0002B1F0 File Offset: 0x000293F0
		private static DiscriminatedUnionConverter.Union CreateUnion(Type t)
		{
			DiscriminatedUnionConverter.Union union = new DiscriminatedUnionConverter.Union();
			DiscriminatedUnionConverter.Union union2 = union;
			MethodCall<object, object> preComputeUnionTagReader = FSharpUtils.PreComputeUnionTagReader;
			object obj = null;
			object[] array = new object[2];
			array[0] = t;
			union2.TagReader = (FSharpFunction)preComputeUnionTagReader(obj, array);
			union.Cases = new List<DiscriminatedUnionConverter.UnionCase>();
			MethodCall<object, object> getUnionCases = FSharpUtils.GetUnionCases;
			object obj2 = null;
			object[] array2 = new object[2];
			array2[0] = t;
			foreach (object obj3 in (object[])getUnionCases(obj2, array2))
			{
				DiscriminatedUnionConverter.UnionCase unionCase = new DiscriminatedUnionConverter.UnionCase();
				unionCase.Tag = (int)FSharpUtils.GetUnionCaseInfoTag.Invoke(obj3);
				unionCase.Name = (string)FSharpUtils.GetUnionCaseInfoName.Invoke(obj3);
				unionCase.Fields = (PropertyInfo[])FSharpUtils.GetUnionCaseInfoFields(obj3, new object[0]);
				DiscriminatedUnionConverter.UnionCase unionCase2 = unionCase;
				MethodCall<object, object> preComputeUnionReader = FSharpUtils.PreComputeUnionReader;
				object obj4 = null;
				object[] array4 = new object[2];
				array4[0] = obj3;
				unionCase2.FieldReader = (FSharpFunction)preComputeUnionReader(obj4, array4);
				DiscriminatedUnionConverter.UnionCase unionCase3 = unionCase;
				MethodCall<object, object> preComputeUnionConstructor = FSharpUtils.PreComputeUnionConstructor;
				object obj5 = null;
				object[] array5 = new object[2];
				array5[0] = obj3;
				unionCase3.Constructor = (FSharpFunction)preComputeUnionConstructor(obj5, array5);
				union.Cases.Add(unionCase);
			}
			return union;
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x0002B304 File Offset: 0x00029504
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			DefaultContractResolver defaultContractResolver = serializer.ContractResolver as DefaultContractResolver;
			Type type = DiscriminatedUnionConverter.UnionTypeLookupCache.Get(value.GetType());
			DiscriminatedUnionConverter.Union union = DiscriminatedUnionConverter.UnionCache.Get(type);
			int tag = (int)union.TagReader.Invoke(new object[] { value });
			DiscriminatedUnionConverter.UnionCase unionCase = Enumerable.Single<DiscriminatedUnionConverter.UnionCase>(union.Cases, (DiscriminatedUnionConverter.UnionCase c) => c.Tag == tag);
			writer.WriteStartObject();
			writer.WritePropertyName((defaultContractResolver != null) ? defaultContractResolver.GetResolvedPropertyName("Case") : "Case");
			writer.WriteValue(unionCase.Name);
			if (unionCase.Fields != null && unionCase.Fields.Length != 0)
			{
				object[] array = (object[])unionCase.FieldReader.Invoke(new object[] { value });
				writer.WritePropertyName((defaultContractResolver != null) ? defaultContractResolver.GetResolvedPropertyName("Fields") : "Fields");
				writer.WriteStartArray();
				foreach (object obj in array)
				{
					serializer.Serialize(writer, obj);
				}
				writer.WriteEndArray();
			}
			writer.WriteEndObject();
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x0002B428 File Offset: 0x00029628
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
			{
				return null;
			}
			DiscriminatedUnionConverter.UnionCase unionCase = null;
			string caseName = null;
			JArray jarray = null;
			reader.ReadAndAssert();
			Func<DiscriminatedUnionConverter.UnionCase, bool> <>9__0;
			while (reader.TokenType == JsonToken.PropertyName)
			{
				string text = reader.Value.ToString();
				if (string.Equals(text, "Case", 5))
				{
					reader.ReadAndAssert();
					DiscriminatedUnionConverter.Union union = DiscriminatedUnionConverter.UnionCache.Get(objectType);
					caseName = reader.Value.ToString();
					IEnumerable<DiscriminatedUnionConverter.UnionCase> cases = union.Cases;
					Func<DiscriminatedUnionConverter.UnionCase, bool> func;
					if ((func = <>9__0) == null)
					{
						func = (<>9__0 = (DiscriminatedUnionConverter.UnionCase c) => c.Name == caseName);
					}
					unionCase = Enumerable.SingleOrDefault<DiscriminatedUnionConverter.UnionCase>(cases, func);
					if (unionCase == null)
					{
						throw JsonSerializationException.Create(reader, "No union type found with the name '{0}'.".FormatWith(CultureInfo.InvariantCulture, caseName));
					}
				}
				else
				{
					if (!string.Equals(text, "Fields", 5))
					{
						throw JsonSerializationException.Create(reader, "Unexpected property '{0}' found when reading union.".FormatWith(CultureInfo.InvariantCulture, text));
					}
					reader.ReadAndAssert();
					if (reader.TokenType != JsonToken.StartArray)
					{
						throw JsonSerializationException.Create(reader, "Union fields must been an array.");
					}
					jarray = (JArray)JToken.ReadFrom(reader);
				}
				reader.ReadAndAssert();
			}
			if (unionCase == null)
			{
				throw JsonSerializationException.Create(reader, "No '{0}' property with union name found.".FormatWith(CultureInfo.InvariantCulture, "Case"));
			}
			object[] array = new object[unionCase.Fields.Length];
			if (unionCase.Fields.Length != 0 && jarray == null)
			{
				throw JsonSerializationException.Create(reader, "No '{0}' property with union fields found.".FormatWith(CultureInfo.InvariantCulture, "Fields"));
			}
			if (jarray != null)
			{
				if (unionCase.Fields.Length != jarray.Count)
				{
					throw JsonSerializationException.Create(reader, "The number of field values does not match the number of properties defined by union '{0}'.".FormatWith(CultureInfo.InvariantCulture, caseName));
				}
				for (int i = 0; i < jarray.Count; i++)
				{
					JToken jtoken = jarray[i];
					PropertyInfo propertyInfo = unionCase.Fields[i];
					array[i] = jtoken.ToObject(propertyInfo.PropertyType, serializer);
				}
			}
			object[] array2 = new object[] { array };
			return unionCase.Constructor.Invoke(array2);
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x0002B624 File Offset: 0x00029824
		public override bool CanConvert(Type objectType)
		{
			if (typeof(IEnumerable).IsAssignableFrom(objectType))
			{
				return false;
			}
			object[] customAttributes = objectType.GetCustomAttributes(true);
			bool flag = false;
			object[] array = customAttributes;
			for (int i = 0; i < array.Length; i++)
			{
				Type type = array[i].GetType();
				if (type.FullName == "Microsoft.FSharp.Core.CompilationMappingAttribute")
				{
					FSharpUtils.EnsureInitialized(type.Assembly());
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return false;
			}
			MethodCall<object, object> isUnion = FSharpUtils.IsUnion;
			object obj = null;
			object[] array2 = new object[2];
			array2[0] = objectType;
			return (bool)isUnion(obj, array2);
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x0002AB63 File Offset: 0x00028D63
		public DiscriminatedUnionConverter()
		{
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x0002B6A6 File Offset: 0x000298A6
		// Note: this type is marked as 'beforefieldinit'.
		static DiscriminatedUnionConverter()
		{
		}

		// Token: 0x04000397 RID: 919
		private const string CasePropertyName = "Case";

		// Token: 0x04000398 RID: 920
		private const string FieldsPropertyName = "Fields";

		// Token: 0x04000399 RID: 921
		private static readonly ThreadSafeStore<Type, DiscriminatedUnionConverter.Union> UnionCache = new ThreadSafeStore<Type, DiscriminatedUnionConverter.Union>(new Func<Type, DiscriminatedUnionConverter.Union>(DiscriminatedUnionConverter.CreateUnion));

		// Token: 0x0400039A RID: 922
		private static readonly ThreadSafeStore<Type, Type> UnionTypeLookupCache = new ThreadSafeStore<Type, Type>(new Func<Type, Type>(DiscriminatedUnionConverter.CreateUnionTypeLookup));

		// Token: 0x0200016E RID: 366
		internal class Union
		{
			// Token: 0x170002B2 RID: 690
			// (get) Token: 0x06000DDE RID: 3550 RVA: 0x0003428C File Offset: 0x0003248C
			// (set) Token: 0x06000DDF RID: 3551 RVA: 0x00034294 File Offset: 0x00032494
			public FSharpFunction TagReader
			{
				[CompilerGenerated]
				get
				{
					return this.<TagReader>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<TagReader>k__BackingField = value;
				}
			}

			// Token: 0x06000DE0 RID: 3552 RVA: 0x00008020 File Offset: 0x00006220
			public Union()
			{
			}

			// Token: 0x04000581 RID: 1409
			public List<DiscriminatedUnionConverter.UnionCase> Cases;

			// Token: 0x04000582 RID: 1410
			[CompilerGenerated]
			private FSharpFunction <TagReader>k__BackingField;
		}

		// Token: 0x0200016F RID: 367
		internal class UnionCase
		{
			// Token: 0x06000DE1 RID: 3553 RVA: 0x00008020 File Offset: 0x00006220
			public UnionCase()
			{
			}

			// Token: 0x04000583 RID: 1411
			public int Tag;

			// Token: 0x04000584 RID: 1412
			public string Name;

			// Token: 0x04000585 RID: 1413
			public PropertyInfo[] Fields;

			// Token: 0x04000586 RID: 1414
			public FSharpFunction FieldReader;

			// Token: 0x04000587 RID: 1415
			public FSharpFunction Constructor;
		}

		// Token: 0x02000170 RID: 368
		[CompilerGenerated]
		private sealed class <>c__DisplayClass8_0
		{
			// Token: 0x06000DE2 RID: 3554 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__DisplayClass8_0()
			{
			}

			// Token: 0x06000DE3 RID: 3555 RVA: 0x0003429D File Offset: 0x0003249D
			internal bool <WriteJson>b__0(DiscriminatedUnionConverter.UnionCase c)
			{
				return c.Tag == this.tag;
			}

			// Token: 0x04000588 RID: 1416
			public int tag;
		}

		// Token: 0x02000171 RID: 369
		[CompilerGenerated]
		private sealed class <>c__DisplayClass9_0
		{
			// Token: 0x06000DE4 RID: 3556 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__DisplayClass9_0()
			{
			}

			// Token: 0x06000DE5 RID: 3557 RVA: 0x000342AD File Offset: 0x000324AD
			internal bool <ReadJson>b__0(DiscriminatedUnionConverter.UnionCase c)
			{
				return c.Name == this.caseName;
			}

			// Token: 0x04000589 RID: 1417
			public string caseName;

			// Token: 0x0400058A RID: 1418
			public Func<DiscriminatedUnionConverter.UnionCase, bool> <>9__0;
		}
	}
}
