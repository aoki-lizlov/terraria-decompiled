using System;
using System.Collections;
using System.Collections.Generic;

namespace FullSerializer.Internal
{
	// Token: 0x0200001E RID: 30
	public class fsDictionaryConverter : fsConverter
	{
		// Token: 0x060000CC RID: 204 RVA: 0x00005DB8 File Offset: 0x00003FB8
		public override bool CanProcess(Type type)
		{
			return typeof(IDictionary).IsAssignableFrom(type);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00005B1D File Offset: 0x00003D1D
		public override object CreateInstance(fsData data, Type storageType)
		{
			return fsMetaType.Get(this.Serializer.Config, storageType).CreateInstance();
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00005DCC File Offset: 0x00003FCC
		public override fsResult TryDeserialize(fsData data, ref object instance_, Type storageType)
		{
			IDictionary dictionary = (IDictionary)instance_;
			fsResult fsResult = fsResult.Success;
			Type type;
			Type type2;
			fsDictionaryConverter.GetKeyValueTypes(dictionary.GetType(), out type, out type2);
			if (!data.IsList)
			{
				if (data.IsDictionary)
				{
					using (Dictionary<string, fsData>.Enumerator enumerator = data.AsDictionary.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							KeyValuePair<string, fsData> keyValuePair = enumerator.Current;
							if (!fsSerializer.IsReservedKeyword(keyValuePair.Key))
							{
								fsData fsData = new fsData(keyValuePair.Key);
								fsData value = keyValuePair.Value;
								object obj = null;
								object obj2 = null;
								fsResult fsResult2;
								fsResult = (fsResult2 = fsResult + this.Serializer.TryDeserialize(fsData, type, ref obj));
								if (fsResult2.Failed)
								{
									return fsResult;
								}
								fsResult fsResult3;
								fsResult = (fsResult3 = fsResult + this.Serializer.TryDeserialize(value, type2, ref obj2));
								if (fsResult3.Failed)
								{
									return fsResult;
								}
								this.AddItemToDictionary(dictionary, obj, obj2);
							}
						}
						return fsResult;
					}
				}
				return base.FailExpectedType(data, new fsDataType[]
				{
					fsDataType.Array,
					fsDataType.Object
				});
			}
			List<fsData> asList = data.AsList;
			for (int i = 0; i < asList.Count; i++)
			{
				fsData fsData2 = asList[i];
				fsResult fsResult2;
				fsResult = (fsResult2 = fsResult + base.CheckType(fsData2, fsDataType.Object));
				if (fsResult2.Failed)
				{
					return fsResult;
				}
				fsData fsData3;
				fsResult = (fsResult2 = fsResult + base.CheckKey(fsData2, "Key", out fsData3));
				if (fsResult2.Failed)
				{
					return fsResult;
				}
				fsData fsData4;
				fsResult = (fsResult2 = fsResult + base.CheckKey(fsData2, "Value", out fsData4));
				if (fsResult2.Failed)
				{
					return fsResult;
				}
				object obj3 = null;
				object obj4 = null;
				fsResult = (fsResult2 = fsResult + this.Serializer.TryDeserialize(fsData3, type, ref obj3));
				if (fsResult2.Failed)
				{
					return fsResult;
				}
				fsResult = (fsResult2 = fsResult + this.Serializer.TryDeserialize(fsData4, type2, ref obj4));
				if (fsResult2.Failed)
				{
					return fsResult;
				}
				this.AddItemToDictionary(dictionary, obj3, obj4);
			}
			return fsResult;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00005FE8 File Offset: 0x000041E8
		public override fsResult TrySerialize(object instance_, out fsData serialized, Type storageType)
		{
			serialized = fsData.Null;
			fsResult fsResult = fsResult.Success;
			IDictionary dictionary = (IDictionary)instance_;
			Type type;
			Type type2;
			fsDictionaryConverter.GetKeyValueTypes(dictionary.GetType(), out type, out type2);
			IDictionaryEnumerator enumerator = dictionary.GetEnumerator();
			bool flag = true;
			List<fsData> list = new List<fsData>(dictionary.Count);
			List<fsData> list2 = new List<fsData>(dictionary.Count);
			while (enumerator.MoveNext())
			{
				fsData fsData;
				fsResult fsResult2;
				fsResult = (fsResult2 = fsResult + this.Serializer.TrySerialize(type, enumerator.Key, out fsData));
				if (fsResult2.Failed)
				{
					return fsResult;
				}
				fsData fsData2;
				fsResult = (fsResult2 = fsResult + this.Serializer.TrySerialize(type2, enumerator.Value, out fsData2));
				if (fsResult2.Failed)
				{
					return fsResult;
				}
				list.Add(fsData);
				list2.Add(fsData2);
				flag &= fsData.IsString;
			}
			if (flag)
			{
				serialized = fsData.CreateDictionary();
				Dictionary<string, fsData> asDictionary = serialized.AsDictionary;
				for (int i = 0; i < list.Count; i++)
				{
					fsData fsData3 = list[i];
					fsData fsData4 = list2[i];
					asDictionary[fsData3.AsString] = fsData4;
				}
			}
			else
			{
				serialized = fsData.CreateList(list.Count);
				List<fsData> asList = serialized.AsList;
				for (int j = 0; j < list.Count; j++)
				{
					fsData fsData5 = list[j];
					fsData fsData6 = list2[j];
					Dictionary<string, fsData> dictionary2 = new Dictionary<string, fsData>();
					dictionary2["Key"] = fsData5;
					dictionary2["Value"] = fsData6;
					asList.Add(new fsData(dictionary2));
				}
			}
			return fsResult;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x0000617C File Offset: 0x0000437C
		private fsResult AddItemToDictionary(IDictionary dictionary, object key, object value)
		{
			if (key != null && value != null)
			{
				dictionary[key] = value;
				return fsResult.Success;
			}
			Type @interface = fsReflectionUtility.GetInterface(dictionary.GetType(), typeof(ICollection));
			if (@interface == null)
			{
				return fsResult.Warn(dictionary.GetType() + " does not extend ICollection");
			}
			object obj = Activator.CreateInstance(@interface.GetGenericArguments()[0], new object[] { key, value });
			@interface.GetFlattenedMethod("Add").Invoke(dictionary, new object[] { obj });
			return fsResult.Success;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00006210 File Offset: 0x00004410
		private static void GetKeyValueTypes(Type dictionaryType, out Type keyStorageType, out Type valueStorageType)
		{
			Type @interface = fsReflectionUtility.GetInterface(dictionaryType, typeof(IDictionary));
			if (@interface != null)
			{
				Type[] genericArguments = @interface.GetGenericArguments();
				keyStorageType = genericArguments[0];
				valueStorageType = genericArguments[1];
				return;
			}
			keyStorageType = typeof(object);
			valueStorageType = typeof(object);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00005B35 File Offset: 0x00003D35
		public fsDictionaryConverter()
		{
		}
	}
}
