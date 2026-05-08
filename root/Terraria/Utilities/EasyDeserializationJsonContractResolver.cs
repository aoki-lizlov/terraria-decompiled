using System;
using System.ComponentModel;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Terraria.Utilities
{
	// Token: 0x020000C8 RID: 200
	public class EasyDeserializationJsonContractResolver : DefaultContractResolver
	{
		// Token: 0x060017F0 RID: 6128 RVA: 0x004E0CA4 File Offset: 0x004DEEA4
		protected override JsonContract CreateContract(Type objectType)
		{
			JsonContract jsonContract = base.CreateContract(objectType);
			if (jsonContract is JsonStringContract && objectType != typeof(string))
			{
				TypeConverter converter = TypeDescriptor.GetConverter(objectType);
				if (converter != null && converter.CanConvertTo(typeof(string)) && !converter.CanConvertFrom(typeof(string)))
				{
					jsonContract = base.CreateObjectContract(objectType);
				}
			}
			if (objectType.IsArray || objectType.IsValueType)
			{
				jsonContract.IsReference = new bool?(false);
			}
			return jsonContract;
		}

		// Token: 0x060017F1 RID: 6129 RVA: 0x004E0D28 File Offset: 0x004DEF28
		protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
		{
			JsonProperty jsonProperty = base.CreateProperty(member, memberSerialization);
			if (!jsonProperty.Writable)
			{
				jsonProperty.Ignored = true;
			}
			return jsonProperty;
		}

		// Token: 0x060017F2 RID: 6130 RVA: 0x004E0D4E File Offset: 0x004DEF4E
		public EasyDeserializationJsonContractResolver()
		{
		}
	}
}
