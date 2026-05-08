using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using FullSerializer.Internal;

namespace FullSerializer
{
	// Token: 0x02000004 RID: 4
	public abstract class fsBaseConverter
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000024B4 File Offset: 0x000006B4
		public virtual object CreateInstance(fsData data, Type storageType)
		{
			if (this.RequestCycleSupport(storageType))
			{
				throw new InvalidOperationException(string.Concat(new object[]
				{
					"Please override CreateInstance for ",
					base.GetType().FullName,
					"; the object graph for ",
					storageType,
					" can contain potentially contain cycles, so separated instance creation is needed"
				}));
			}
			return storageType;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002506 File Offset: 0x00000706
		public virtual bool RequestCycleSupport(Type storageType)
		{
			return !(storageType == typeof(string)) && (storageType.Resolve().IsClass || storageType.Resolve().IsInterface);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002536 File Offset: 0x00000736
		public virtual bool RequestInheritanceSupport(Type storageType)
		{
			return !storageType.Resolve().IsSealed;
		}

		// Token: 0x0600000C RID: 12
		public abstract fsResult TrySerialize(object instance, out fsData serialized, Type storageType);

		// Token: 0x0600000D RID: 13
		public abstract fsResult TryDeserialize(fsData data, ref object instance, Type storageType);

		// Token: 0x0600000E RID: 14 RVA: 0x00002548 File Offset: 0x00000748
		protected fsResult FailExpectedType(fsData data, params fsDataType[] types)
		{
			object[] array = new object[7];
			array[0] = base.GetType().Name;
			array[1] = " expected one of ";
			array[2] = string.Join(", ", Enumerable.ToArray<string>(Enumerable.Select<fsDataType, string>(types, (fsDataType t) => t.ToString())));
			array[3] = " but got ";
			array[4] = data.Type;
			array[5] = " in ";
			array[6] = data;
			return fsResult.Fail(string.Concat(array));
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000025D4 File Offset: 0x000007D4
		protected fsResult CheckType(fsData data, fsDataType type)
		{
			if (data.Type != type)
			{
				return fsResult.Fail(string.Concat(new object[]
				{
					base.GetType().Name,
					" expected ",
					type,
					" but got ",
					data.Type,
					" in ",
					data
				}));
			}
			return fsResult.Success;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002641 File Offset: 0x00000841
		protected fsResult CheckKey(fsData data, string key, out fsData subitem)
		{
			return this.CheckKey(data.AsDictionary, key, out subitem);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002654 File Offset: 0x00000854
		protected fsResult CheckKey(Dictionary<string, fsData> data, string key, out fsData subitem)
		{
			if (!data.TryGetValue(key, ref subitem))
			{
				return fsResult.Fail(string.Concat(new object[]
				{
					base.GetType().Name,
					" requires a <",
					key,
					"> key in the data ",
					data
				}));
			}
			return fsResult.Success;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000026A8 File Offset: 0x000008A8
		protected fsResult SerializeMember<T>(Dictionary<string, fsData> data, Type overrideConverterType, string name, T value)
		{
			fsData fsData;
			fsResult fsResult = this.Serializer.TrySerialize(typeof(T), overrideConverterType, value, out fsData);
			if (fsResult.Succeeded)
			{
				data[name] = fsData;
			}
			return fsResult;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000026E8 File Offset: 0x000008E8
		protected fsResult DeserializeMember<T>(Dictionary<string, fsData> data, Type overrideConverterType, string name, out T value)
		{
			fsData fsData;
			if (!data.TryGetValue(name, ref fsData))
			{
				value = default(T);
				return fsResult.Fail("Unable to find member \"" + name + "\"");
			}
			object obj = null;
			fsResult fsResult = this.Serializer.TryDeserialize(fsData, typeof(T), overrideConverterType, ref obj);
			value = (T)((object)obj);
			return fsResult;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002493 File Offset: 0x00000693
		protected fsBaseConverter()
		{
		}

		// Token: 0x04000004 RID: 4
		public fsSerializer Serializer;

		// Token: 0x020000B2 RID: 178
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600028C RID: 652 RVA: 0x00009B35 File Offset: 0x00007D35
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600028D RID: 653 RVA: 0x00002493 File Offset: 0x00000693
			public <>c()
			{
			}

			// Token: 0x0600028E RID: 654 RVA: 0x00009B41 File Offset: 0x00007D41
			internal string <FailExpectedType>b__6_0(fsDataType t)
			{
				return t.ToString();
			}

			// Token: 0x04000248 RID: 584
			public static readonly fsBaseConverter.<>c <>9 = new fsBaseConverter.<>c();

			// Token: 0x04000249 RID: 585
			public static Func<fsDataType, string> <>9__6_0;
		}
	}
}
