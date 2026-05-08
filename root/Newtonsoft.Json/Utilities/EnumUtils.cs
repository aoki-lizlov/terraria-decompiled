using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000066 RID: 102
	internal static class EnumUtils
	{
		// Token: 0x060004FA RID: 1274 RVA: 0x00015408 File Offset: 0x00013608
		private static BidirectionalDictionary<string, string> InitializeEnumType(Type type)
		{
			BidirectionalDictionary<string, string> bidirectionalDictionary = new BidirectionalDictionary<string, string>(StringComparer.Ordinal, StringComparer.Ordinal);
			foreach (FieldInfo fieldInfo in type.GetFields(24))
			{
				string name = fieldInfo.Name;
				string text = Enumerable.SingleOrDefault<string>(Enumerable.Select<EnumMemberAttribute, string>(Enumerable.Cast<EnumMemberAttribute>(fieldInfo.GetCustomAttributes(typeof(EnumMemberAttribute), true)), (EnumMemberAttribute a) => a.Value)) ?? fieldInfo.Name;
				string text2;
				if (bidirectionalDictionary.TryGetBySecond(text, out text2))
				{
					throw new InvalidOperationException("Enum name '{0}' already exists on enum '{1}'.".FormatWith(CultureInfo.InvariantCulture, text, type.Name));
				}
				bidirectionalDictionary.Set(name, text);
			}
			return bidirectionalDictionary;
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x000154D0 File Offset: 0x000136D0
		public static IList<T> GetFlagsValues<T>(T value) where T : struct
		{
			Type typeFromHandle = typeof(T);
			if (!typeFromHandle.IsDefined(typeof(FlagsAttribute), false))
			{
				throw new ArgumentException("Enum type {0} is not a set of flags.".FormatWith(CultureInfo.InvariantCulture, typeFromHandle));
			}
			Type underlyingType = Enum.GetUnderlyingType(value.GetType());
			ulong num = Convert.ToUInt64(value, CultureInfo.InvariantCulture);
			IList<EnumValue<ulong>> namesAndValues = EnumUtils.GetNamesAndValues<T>();
			IList<T> list = new List<T>();
			foreach (EnumValue<ulong> enumValue in namesAndValues)
			{
				if ((num & enumValue.Value) == enumValue.Value && enumValue.Value != 0UL)
				{
					list.Add((T)((object)Convert.ChangeType(enumValue.Value, underlyingType, CultureInfo.CurrentCulture)));
				}
			}
			if (list.Count == 0)
			{
				if (Enumerable.SingleOrDefault<EnumValue<ulong>>(namesAndValues, (EnumValue<ulong> v) => v.Value == 0UL) != null)
				{
					list.Add(default(T));
				}
			}
			return list;
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00015600 File Offset: 0x00013800
		public static IList<EnumValue<ulong>> GetNamesAndValues<T>() where T : struct
		{
			return EnumUtils.GetNamesAndValues<ulong>(typeof(T));
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00015614 File Offset: 0x00013814
		public static IList<EnumValue<TUnderlyingType>> GetNamesAndValues<TUnderlyingType>(Type enumType) where TUnderlyingType : struct
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (!enumType.IsEnum())
			{
				throw new ArgumentException("Type {0} is not an enum.".FormatWith(CultureInfo.InvariantCulture, enumType.Name), "enumType");
			}
			IList<object> values = EnumUtils.GetValues(enumType);
			IList<string> names = EnumUtils.GetNames(enumType);
			IList<EnumValue<TUnderlyingType>> list = new List<EnumValue<TUnderlyingType>>();
			for (int i = 0; i < values.Count; i++)
			{
				try
				{
					list.Add(new EnumValue<TUnderlyingType>(names[i], (TUnderlyingType)((object)Convert.ChangeType(values[i], typeof(TUnderlyingType), CultureInfo.CurrentCulture))));
				}
				catch (OverflowException ex)
				{
					throw new InvalidOperationException("Value from enum with the underlying type of {0} cannot be added to dictionary with a value type of {1}. Value was too large: {2}".FormatWith(CultureInfo.InvariantCulture, Enum.GetUnderlyingType(enumType), typeof(TUnderlyingType), Convert.ToUInt64(values[i], CultureInfo.InvariantCulture)), ex);
				}
			}
			return list;
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0001570C File Offset: 0x0001390C
		public static IList<object> GetValues(Type enumType)
		{
			if (!enumType.IsEnum())
			{
				throw new ArgumentException("Type {0} is not an enum.".FormatWith(CultureInfo.InvariantCulture, enumType.Name), "enumType");
			}
			List<object> list = new List<object>();
			FieldInfo[] fields = enumType.GetFields(24);
			for (int i = 0; i < fields.Length; i++)
			{
				object value = fields[i].GetValue(enumType);
				list.Add(value);
			}
			return list;
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00015770 File Offset: 0x00013970
		public static IList<string> GetNames(Type enumType)
		{
			if (!enumType.IsEnum())
			{
				throw new ArgumentException("Type {0} is not an enum.".FormatWith(CultureInfo.InvariantCulture, enumType.Name), "enumType");
			}
			List<string> list = new List<string>();
			foreach (FieldInfo fieldInfo in enumType.GetFields(24))
			{
				list.Add(fieldInfo.Name);
			}
			return list;
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x000157D4 File Offset: 0x000139D4
		public static object ParseEnumName(string enumText, bool isNullable, bool disallowValue, Type t)
		{
			if (enumText == string.Empty && isNullable)
			{
				return null;
			}
			BidirectionalDictionary<string, string> bidirectionalDictionary = EnumUtils.EnumMemberNamesPerType.Get(t);
			string text;
			string text2;
			if (EnumUtils.TryResolvedEnumName(bidirectionalDictionary, enumText, out text))
			{
				text2 = text;
			}
			else if (enumText.IndexOf(',') != -1)
			{
				string[] array = enumText.Split(new char[] { ',' });
				for (int i = 0; i < array.Length; i++)
				{
					string text3 = array[i].Trim();
					array[i] = (EnumUtils.TryResolvedEnumName(bidirectionalDictionary, text3, out text) ? text : text3);
				}
				text2 = string.Join(", ", array);
			}
			else
			{
				text2 = enumText;
				if (disallowValue)
				{
					bool flag = true;
					for (int j = 0; j < text2.Length; j++)
					{
						if (!char.IsNumber(text2.get_Chars(j)))
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						throw new FormatException("Integer string '{0}' is not allowed.".FormatWith(CultureInfo.InvariantCulture, enumText));
					}
				}
			}
			return Enum.Parse(t, text2, true);
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x000158C0 File Offset: 0x00013AC0
		public static string ToEnumName(Type enumType, string enumText, bool camelCaseText)
		{
			BidirectionalDictionary<string, string> bidirectionalDictionary = EnumUtils.EnumMemberNamesPerType.Get(enumType);
			string[] array = enumText.Split(new char[] { ',' });
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i].Trim();
				string text2;
				bidirectionalDictionary.TryGetByFirst(text, out text2);
				text2 = text2 ?? text;
				if (camelCaseText)
				{
					text2 = StringUtils.ToCamelCase(text2);
				}
				array[i] = text2;
			}
			return string.Join(", ", array);
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00015931 File Offset: 0x00013B31
		private static bool TryResolvedEnumName(BidirectionalDictionary<string, string> map, string enumText, out string resolvedEnumName)
		{
			if (map.TryGetBySecond(enumText, out resolvedEnumName))
			{
				return true;
			}
			resolvedEnumName = null;
			return false;
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00015943 File Offset: 0x00013B43
		// Note: this type is marked as 'beforefieldinit'.
		static EnumUtils()
		{
		}

		// Token: 0x04000257 RID: 599
		private static readonly ThreadSafeStore<Type, BidirectionalDictionary<string, string>> EnumMemberNamesPerType = new ThreadSafeStore<Type, BidirectionalDictionary<string, string>>(new Func<Type, BidirectionalDictionary<string, string>>(EnumUtils.InitializeEnumType));

		// Token: 0x02000136 RID: 310
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000CCF RID: 3279 RVA: 0x000311E0 File Offset: 0x0002F3E0
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000CD0 RID: 3280 RVA: 0x00008020 File Offset: 0x00006220
			public <>c()
			{
			}

			// Token: 0x06000CD1 RID: 3281 RVA: 0x000311EC File Offset: 0x0002F3EC
			internal string <InitializeEnumType>b__1_0(EnumMemberAttribute a)
			{
				return a.Value;
			}

			// Token: 0x04000496 RID: 1174
			public static readonly EnumUtils.<>c <>9 = new EnumUtils.<>c();

			// Token: 0x04000497 RID: 1175
			public static Func<EnumMemberAttribute, string> <>9__1_0;
		}

		// Token: 0x02000137 RID: 311
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c__2<T> where T : struct
		{
			// Token: 0x06000CD2 RID: 3282 RVA: 0x000311F4 File Offset: 0x0002F3F4
			// Note: this type is marked as 'beforefieldinit'.
			static <>c__2()
			{
			}

			// Token: 0x06000CD3 RID: 3283 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__2()
			{
			}

			// Token: 0x06000CD4 RID: 3284 RVA: 0x00031200 File Offset: 0x0002F400
			internal bool <GetFlagsValues>b__2_0(EnumValue<ulong> v)
			{
				return v.Value == 0UL;
			}

			// Token: 0x04000498 RID: 1176
			public static readonly EnumUtils.<>c__2<T> <>9 = new EnumUtils.<>c__2<T>();

			// Token: 0x04000499 RID: 1177
			public static Func<EnumValue<ulong>, bool> <>9__2_0;
		}
	}
}
