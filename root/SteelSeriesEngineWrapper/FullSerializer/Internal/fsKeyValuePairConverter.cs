using System;
using System.Collections.Generic;
using System.Reflection;

namespace FullSerializer.Internal
{
	// Token: 0x02000023 RID: 35
	public class fsKeyValuePairConverter : fsConverter
	{
		// Token: 0x060000F3 RID: 243 RVA: 0x000069D7 File Offset: 0x00004BD7
		public override bool CanProcess(Type type)
		{
			return type.Resolve().IsGenericType && type.GetGenericTypeDefinition() == typeof(KeyValuePair);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000059C8 File Offset: 0x00003BC8
		public override bool RequestCycleSupport(Type storageType)
		{
			return false;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000059C8 File Offset: 0x00003BC8
		public override bool RequestInheritanceSupport(Type storageType)
		{
			return false;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00006A00 File Offset: 0x00004C00
		public override fsResult TryDeserialize(fsData data, ref object instance, Type storageType)
		{
			fsResult fsResult = fsResult.Success;
			fsData fsData;
			fsResult fsResult2;
			fsResult = (fsResult2 = fsResult + base.CheckKey(data, "Key", out fsData));
			if (fsResult2.Failed)
			{
				return fsResult;
			}
			fsData fsData2;
			fsResult = (fsResult2 = fsResult + base.CheckKey(data, "Value", out fsData2));
			if (fsResult2.Failed)
			{
				return fsResult;
			}
			Type[] genericArguments = storageType.GetGenericArguments();
			Type type = genericArguments[0];
			Type type2 = genericArguments[1];
			object obj = null;
			object obj2 = null;
			fsResult.AddMessages(this.Serializer.TryDeserialize(fsData, type, ref obj));
			fsResult.AddMessages(this.Serializer.TryDeserialize(fsData2, type2, ref obj2));
			instance = Activator.CreateInstance(storageType, new object[] { obj, obj2 });
			return fsResult;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00006AB4 File Offset: 0x00004CB4
		public override fsResult TrySerialize(object instance, out fsData serialized, Type storageType)
		{
			PropertyInfo declaredProperty = storageType.GetDeclaredProperty("Key");
			PropertyInfo declaredProperty2 = storageType.GetDeclaredProperty("Value");
			object value = declaredProperty.GetValue(instance, null);
			object value2 = declaredProperty2.GetValue(instance, null);
			Type[] genericArguments = storageType.GetGenericArguments();
			Type type = genericArguments[0];
			Type type2 = genericArguments[1];
			fsResult success = fsResult.Success;
			fsData fsData;
			success.AddMessages(this.Serializer.TrySerialize(type, value, out fsData));
			fsData fsData2;
			success.AddMessages(this.Serializer.TrySerialize(type2, value2, out fsData2));
			serialized = fsData.CreateDictionary();
			if (fsData != null)
			{
				serialized.AsDictionary["Key"] = fsData;
			}
			if (fsData2 != null)
			{
				serialized.AsDictionary["Value"] = fsData2;
			}
			return success;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00005B35 File Offset: 0x00003D35
		public fsKeyValuePairConverter()
		{
		}
	}
}
