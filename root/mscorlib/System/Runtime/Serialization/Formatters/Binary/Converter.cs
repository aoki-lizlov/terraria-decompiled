using System;
using System.Globalization;
using System.Reflection;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000679 RID: 1657
	internal sealed class Converter
	{
		// Token: 0x06003E1E RID: 15902 RVA: 0x000025BE File Offset: 0x000007BE
		private Converter()
		{
		}

		// Token: 0x06003E1F RID: 15903 RVA: 0x000D7874 File Offset: 0x000D5A74
		internal static InternalPrimitiveTypeE ToCode(Type type)
		{
			InternalPrimitiveTypeE internalPrimitiveTypeE;
			if (type != null && !type.IsPrimitive)
			{
				if (type == Converter.typeofDateTime)
				{
					internalPrimitiveTypeE = InternalPrimitiveTypeE.DateTime;
				}
				else if (type == Converter.typeofTimeSpan)
				{
					internalPrimitiveTypeE = InternalPrimitiveTypeE.TimeSpan;
				}
				else if (type == Converter.typeofDecimal)
				{
					internalPrimitiveTypeE = InternalPrimitiveTypeE.Decimal;
				}
				else
				{
					internalPrimitiveTypeE = InternalPrimitiveTypeE.Invalid;
				}
			}
			else
			{
				internalPrimitiveTypeE = Converter.ToPrimitiveTypeEnum(Type.GetTypeCode(type));
			}
			return internalPrimitiveTypeE;
		}

		// Token: 0x06003E20 RID: 15904 RVA: 0x000D78C4 File Offset: 0x000D5AC4
		internal static bool IsWriteAsByteArray(InternalPrimitiveTypeE code)
		{
			bool flag = false;
			switch (code)
			{
			case InternalPrimitiveTypeE.Boolean:
			case InternalPrimitiveTypeE.Byte:
			case InternalPrimitiveTypeE.Char:
			case InternalPrimitiveTypeE.Double:
			case InternalPrimitiveTypeE.Int16:
			case InternalPrimitiveTypeE.Int32:
			case InternalPrimitiveTypeE.Int64:
			case InternalPrimitiveTypeE.SByte:
			case InternalPrimitiveTypeE.Single:
			case InternalPrimitiveTypeE.UInt16:
			case InternalPrimitiveTypeE.UInt32:
			case InternalPrimitiveTypeE.UInt64:
				flag = true;
				break;
			}
			return flag;
		}

		// Token: 0x06003E21 RID: 15905 RVA: 0x000D7920 File Offset: 0x000D5B20
		internal static int TypeLength(InternalPrimitiveTypeE code)
		{
			int num = 0;
			switch (code)
			{
			case InternalPrimitiveTypeE.Boolean:
				num = 1;
				break;
			case InternalPrimitiveTypeE.Byte:
				num = 1;
				break;
			case InternalPrimitiveTypeE.Char:
				num = 2;
				break;
			case InternalPrimitiveTypeE.Double:
				num = 8;
				break;
			case InternalPrimitiveTypeE.Int16:
				num = 2;
				break;
			case InternalPrimitiveTypeE.Int32:
				num = 4;
				break;
			case InternalPrimitiveTypeE.Int64:
				num = 8;
				break;
			case InternalPrimitiveTypeE.SByte:
				num = 1;
				break;
			case InternalPrimitiveTypeE.Single:
				num = 4;
				break;
			case InternalPrimitiveTypeE.UInt16:
				num = 2;
				break;
			case InternalPrimitiveTypeE.UInt32:
				num = 4;
				break;
			case InternalPrimitiveTypeE.UInt64:
				num = 8;
				break;
			}
			return num;
		}

		// Token: 0x06003E22 RID: 15906 RVA: 0x000D79A8 File Offset: 0x000D5BA8
		internal static InternalNameSpaceE GetNameSpaceEnum(InternalPrimitiveTypeE code, Type type, WriteObjectInfo objectInfo, out string typeName)
		{
			InternalNameSpaceE internalNameSpaceE = InternalNameSpaceE.None;
			typeName = null;
			if (code != InternalPrimitiveTypeE.Invalid)
			{
				switch (code)
				{
				case InternalPrimitiveTypeE.Boolean:
				case InternalPrimitiveTypeE.Byte:
				case InternalPrimitiveTypeE.Char:
				case InternalPrimitiveTypeE.Double:
				case InternalPrimitiveTypeE.Int16:
				case InternalPrimitiveTypeE.Int32:
				case InternalPrimitiveTypeE.Int64:
				case InternalPrimitiveTypeE.SByte:
				case InternalPrimitiveTypeE.Single:
				case InternalPrimitiveTypeE.TimeSpan:
				case InternalPrimitiveTypeE.DateTime:
				case InternalPrimitiveTypeE.UInt16:
				case InternalPrimitiveTypeE.UInt32:
				case InternalPrimitiveTypeE.UInt64:
					internalNameSpaceE = InternalNameSpaceE.XdrPrimitive;
					typeName = "System." + Converter.ToComType(code);
					break;
				case InternalPrimitiveTypeE.Decimal:
					internalNameSpaceE = InternalNameSpaceE.UrtSystem;
					typeName = "System." + Converter.ToComType(code);
					break;
				}
			}
			if (internalNameSpaceE == InternalNameSpaceE.None && type != null)
			{
				if (type == Converter.typeofString)
				{
					internalNameSpaceE = InternalNameSpaceE.XdrString;
				}
				else if (objectInfo == null)
				{
					typeName = type.FullName;
					if (type.Assembly == Converter.urtAssembly)
					{
						internalNameSpaceE = InternalNameSpaceE.UrtSystem;
					}
					else
					{
						internalNameSpaceE = InternalNameSpaceE.UrtUser;
					}
				}
				else
				{
					typeName = objectInfo.GetTypeFullName();
					if (objectInfo.GetAssemblyString().Equals(Converter.urtAssemblyString))
					{
						internalNameSpaceE = InternalNameSpaceE.UrtSystem;
					}
					else
					{
						internalNameSpaceE = InternalNameSpaceE.UrtUser;
					}
				}
			}
			return internalNameSpaceE;
		}

		// Token: 0x06003E23 RID: 15907 RVA: 0x000D7A89 File Offset: 0x000D5C89
		internal static Type ToArrayType(InternalPrimitiveTypeE code)
		{
			if (Converter.arrayTypeA == null)
			{
				Converter.InitArrayTypeA();
			}
			return Converter.arrayTypeA[(int)code];
		}

		// Token: 0x06003E24 RID: 15908 RVA: 0x000D7AA4 File Offset: 0x000D5CA4
		private static void InitTypeA()
		{
			Type[] array = new Type[Converter.primitiveTypeEnumLength];
			array[0] = null;
			array[1] = Converter.typeofBoolean;
			array[2] = Converter.typeofByte;
			array[3] = Converter.typeofChar;
			array[5] = Converter.typeofDecimal;
			array[6] = Converter.typeofDouble;
			array[7] = Converter.typeofInt16;
			array[8] = Converter.typeofInt32;
			array[9] = Converter.typeofInt64;
			array[10] = Converter.typeofSByte;
			array[11] = Converter.typeofSingle;
			array[12] = Converter.typeofTimeSpan;
			array[13] = Converter.typeofDateTime;
			array[14] = Converter.typeofUInt16;
			array[15] = Converter.typeofUInt32;
			array[16] = Converter.typeofUInt64;
			Converter.typeA = array;
		}

		// Token: 0x06003E25 RID: 15909 RVA: 0x000D7B48 File Offset: 0x000D5D48
		private static void InitArrayTypeA()
		{
			Type[] array = new Type[Converter.primitiveTypeEnumLength];
			array[0] = null;
			array[1] = Converter.typeofBooleanArray;
			array[2] = Converter.typeofByteArray;
			array[3] = Converter.typeofCharArray;
			array[5] = Converter.typeofDecimalArray;
			array[6] = Converter.typeofDoubleArray;
			array[7] = Converter.typeofInt16Array;
			array[8] = Converter.typeofInt32Array;
			array[9] = Converter.typeofInt64Array;
			array[10] = Converter.typeofSByteArray;
			array[11] = Converter.typeofSingleArray;
			array[12] = Converter.typeofTimeSpanArray;
			array[13] = Converter.typeofDateTimeArray;
			array[14] = Converter.typeofUInt16Array;
			array[15] = Converter.typeofUInt32Array;
			array[16] = Converter.typeofUInt64Array;
			Converter.arrayTypeA = array;
		}

		// Token: 0x06003E26 RID: 15910 RVA: 0x000D7BEA File Offset: 0x000D5DEA
		internal static Type ToType(InternalPrimitiveTypeE code)
		{
			if (Converter.typeA == null)
			{
				Converter.InitTypeA();
			}
			return Converter.typeA[(int)code];
		}

		// Token: 0x06003E27 RID: 15911 RVA: 0x000D7C04 File Offset: 0x000D5E04
		internal static Array CreatePrimitiveArray(InternalPrimitiveTypeE code, int length)
		{
			Array array = null;
			switch (code)
			{
			case InternalPrimitiveTypeE.Boolean:
				array = new bool[length];
				break;
			case InternalPrimitiveTypeE.Byte:
				array = new byte[length];
				break;
			case InternalPrimitiveTypeE.Char:
				array = new char[length];
				break;
			case InternalPrimitiveTypeE.Decimal:
				array = new decimal[length];
				break;
			case InternalPrimitiveTypeE.Double:
				array = new double[length];
				break;
			case InternalPrimitiveTypeE.Int16:
				array = new short[length];
				break;
			case InternalPrimitiveTypeE.Int32:
				array = new int[length];
				break;
			case InternalPrimitiveTypeE.Int64:
				array = new long[length];
				break;
			case InternalPrimitiveTypeE.SByte:
				array = new sbyte[length];
				break;
			case InternalPrimitiveTypeE.Single:
				array = new float[length];
				break;
			case InternalPrimitiveTypeE.TimeSpan:
				array = new TimeSpan[length];
				break;
			case InternalPrimitiveTypeE.DateTime:
				array = new DateTime[length];
				break;
			case InternalPrimitiveTypeE.UInt16:
				array = new ushort[length];
				break;
			case InternalPrimitiveTypeE.UInt32:
				array = new uint[length];
				break;
			case InternalPrimitiveTypeE.UInt64:
				array = new ulong[length];
				break;
			}
			return array;
		}

		// Token: 0x06003E28 RID: 15912 RVA: 0x000D7CE8 File Offset: 0x000D5EE8
		internal static bool IsPrimitiveArray(Type type, out object typeInformation)
		{
			typeInformation = null;
			bool flag = true;
			if (type == Converter.typeofBooleanArray)
			{
				typeInformation = InternalPrimitiveTypeE.Boolean;
			}
			else if (type == Converter.typeofByteArray)
			{
				typeInformation = InternalPrimitiveTypeE.Byte;
			}
			else if (type == Converter.typeofCharArray)
			{
				typeInformation = InternalPrimitiveTypeE.Char;
			}
			else if (type == Converter.typeofDoubleArray)
			{
				typeInformation = InternalPrimitiveTypeE.Double;
			}
			else if (type == Converter.typeofInt16Array)
			{
				typeInformation = InternalPrimitiveTypeE.Int16;
			}
			else if (type == Converter.typeofInt32Array)
			{
				typeInformation = InternalPrimitiveTypeE.Int32;
			}
			else if (type == Converter.typeofInt64Array)
			{
				typeInformation = InternalPrimitiveTypeE.Int64;
			}
			else if (type == Converter.typeofSByteArray)
			{
				typeInformation = InternalPrimitiveTypeE.SByte;
			}
			else if (type == Converter.typeofSingleArray)
			{
				typeInformation = InternalPrimitiveTypeE.Single;
			}
			else if (type == Converter.typeofUInt16Array)
			{
				typeInformation = InternalPrimitiveTypeE.UInt16;
			}
			else if (type == Converter.typeofUInt32Array)
			{
				typeInformation = InternalPrimitiveTypeE.UInt32;
			}
			else if (type == Converter.typeofUInt64Array)
			{
				typeInformation = InternalPrimitiveTypeE.UInt64;
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06003E29 RID: 15913 RVA: 0x000D7DEC File Offset: 0x000D5FEC
		private static void InitValueA()
		{
			string[] array = new string[Converter.primitiveTypeEnumLength];
			array[0] = null;
			array[1] = "Boolean";
			array[2] = "Byte";
			array[3] = "Char";
			array[5] = "Decimal";
			array[6] = "Double";
			array[7] = "Int16";
			array[8] = "Int32";
			array[9] = "Int64";
			array[10] = "SByte";
			array[11] = "Single";
			array[12] = "TimeSpan";
			array[13] = "DateTime";
			array[14] = "UInt16";
			array[15] = "UInt32";
			array[16] = "UInt64";
			Converter.valueA = array;
		}

		// Token: 0x06003E2A RID: 15914 RVA: 0x000D7E8E File Offset: 0x000D608E
		internal static string ToComType(InternalPrimitiveTypeE code)
		{
			if (Converter.valueA == null)
			{
				Converter.InitValueA();
			}
			return Converter.valueA[(int)code];
		}

		// Token: 0x06003E2B RID: 15915 RVA: 0x000D7EA8 File Offset: 0x000D60A8
		private static void InitTypeCodeA()
		{
			TypeCode[] array = new TypeCode[Converter.primitiveTypeEnumLength];
			array[0] = TypeCode.Object;
			array[1] = TypeCode.Boolean;
			array[2] = TypeCode.Byte;
			array[3] = TypeCode.Char;
			array[5] = TypeCode.Decimal;
			array[6] = TypeCode.Double;
			array[7] = TypeCode.Int16;
			array[8] = TypeCode.Int32;
			array[9] = TypeCode.Int64;
			array[10] = TypeCode.SByte;
			array[11] = TypeCode.Single;
			array[12] = TypeCode.Object;
			array[13] = TypeCode.DateTime;
			array[14] = TypeCode.UInt16;
			array[15] = TypeCode.UInt32;
			array[16] = TypeCode.UInt64;
			Converter.typeCodeA = array;
		}

		// Token: 0x06003E2C RID: 15916 RVA: 0x000D7F16 File Offset: 0x000D6116
		internal static TypeCode ToTypeCode(InternalPrimitiveTypeE code)
		{
			if (Converter.typeCodeA == null)
			{
				Converter.InitTypeCodeA();
			}
			return Converter.typeCodeA[(int)code];
		}

		// Token: 0x06003E2D RID: 15917 RVA: 0x000D7F30 File Offset: 0x000D6130
		private static void InitCodeA()
		{
			Converter.codeA = new InternalPrimitiveTypeE[]
			{
				InternalPrimitiveTypeE.Invalid,
				InternalPrimitiveTypeE.Invalid,
				InternalPrimitiveTypeE.Invalid,
				InternalPrimitiveTypeE.Boolean,
				InternalPrimitiveTypeE.Char,
				InternalPrimitiveTypeE.SByte,
				InternalPrimitiveTypeE.Byte,
				InternalPrimitiveTypeE.Int16,
				InternalPrimitiveTypeE.UInt16,
				InternalPrimitiveTypeE.Int32,
				InternalPrimitiveTypeE.UInt32,
				InternalPrimitiveTypeE.Int64,
				InternalPrimitiveTypeE.UInt64,
				InternalPrimitiveTypeE.Single,
				InternalPrimitiveTypeE.Double,
				InternalPrimitiveTypeE.Decimal,
				InternalPrimitiveTypeE.DateTime,
				InternalPrimitiveTypeE.Invalid,
				InternalPrimitiveTypeE.Invalid
			};
		}

		// Token: 0x06003E2E RID: 15918 RVA: 0x000D7FA8 File Offset: 0x000D61A8
		internal static InternalPrimitiveTypeE ToPrimitiveTypeEnum(TypeCode typeCode)
		{
			if (Converter.codeA == null)
			{
				Converter.InitCodeA();
			}
			return Converter.codeA[(int)typeCode];
		}

		// Token: 0x06003E2F RID: 15919 RVA: 0x000D7FC4 File Offset: 0x000D61C4
		internal static object FromString(string value, InternalPrimitiveTypeE code)
		{
			object obj;
			if (code != InternalPrimitiveTypeE.Invalid)
			{
				obj = Convert.ChangeType(value, Converter.ToTypeCode(code), CultureInfo.InvariantCulture);
			}
			else
			{
				obj = value;
			}
			return obj;
		}

		// Token: 0x06003E30 RID: 15920 RVA: 0x000D7FEC File Offset: 0x000D61EC
		// Note: this type is marked as 'beforefieldinit'.
		static Converter()
		{
		}

		// Token: 0x04002823 RID: 10275
		private static int primitiveTypeEnumLength = 17;

		// Token: 0x04002824 RID: 10276
		private static volatile Type[] typeA;

		// Token: 0x04002825 RID: 10277
		private static volatile Type[] arrayTypeA;

		// Token: 0x04002826 RID: 10278
		private static volatile string[] valueA;

		// Token: 0x04002827 RID: 10279
		private static volatile TypeCode[] typeCodeA;

		// Token: 0x04002828 RID: 10280
		private static volatile InternalPrimitiveTypeE[] codeA;

		// Token: 0x04002829 RID: 10281
		internal static Type typeofISerializable = typeof(ISerializable);

		// Token: 0x0400282A RID: 10282
		internal static Type typeofString = typeof(string);

		// Token: 0x0400282B RID: 10283
		internal static Type typeofConverter = typeof(Converter);

		// Token: 0x0400282C RID: 10284
		internal static Type typeofBoolean = typeof(bool);

		// Token: 0x0400282D RID: 10285
		internal static Type typeofByte = typeof(byte);

		// Token: 0x0400282E RID: 10286
		internal static Type typeofChar = typeof(char);

		// Token: 0x0400282F RID: 10287
		internal static Type typeofDecimal = typeof(decimal);

		// Token: 0x04002830 RID: 10288
		internal static Type typeofDouble = typeof(double);

		// Token: 0x04002831 RID: 10289
		internal static Type typeofInt16 = typeof(short);

		// Token: 0x04002832 RID: 10290
		internal static Type typeofInt32 = typeof(int);

		// Token: 0x04002833 RID: 10291
		internal static Type typeofInt64 = typeof(long);

		// Token: 0x04002834 RID: 10292
		internal static Type typeofSByte = typeof(sbyte);

		// Token: 0x04002835 RID: 10293
		internal static Type typeofSingle = typeof(float);

		// Token: 0x04002836 RID: 10294
		internal static Type typeofTimeSpan = typeof(TimeSpan);

		// Token: 0x04002837 RID: 10295
		internal static Type typeofDateTime = typeof(DateTime);

		// Token: 0x04002838 RID: 10296
		internal static Type typeofUInt16 = typeof(ushort);

		// Token: 0x04002839 RID: 10297
		internal static Type typeofUInt32 = typeof(uint);

		// Token: 0x0400283A RID: 10298
		internal static Type typeofUInt64 = typeof(ulong);

		// Token: 0x0400283B RID: 10299
		internal static Type typeofObject = typeof(object);

		// Token: 0x0400283C RID: 10300
		internal static Type typeofSystemVoid = typeof(void);

		// Token: 0x0400283D RID: 10301
		internal static Assembly urtAssembly = Assembly.GetAssembly(Converter.typeofString);

		// Token: 0x0400283E RID: 10302
		internal static string urtAssemblyString = Converter.urtAssembly.FullName;

		// Token: 0x0400283F RID: 10303
		internal static Type typeofTypeArray = typeof(Type[]);

		// Token: 0x04002840 RID: 10304
		internal static Type typeofObjectArray = typeof(object[]);

		// Token: 0x04002841 RID: 10305
		internal static Type typeofStringArray = typeof(string[]);

		// Token: 0x04002842 RID: 10306
		internal static Type typeofBooleanArray = typeof(bool[]);

		// Token: 0x04002843 RID: 10307
		internal static Type typeofByteArray = typeof(byte[]);

		// Token: 0x04002844 RID: 10308
		internal static Type typeofCharArray = typeof(char[]);

		// Token: 0x04002845 RID: 10309
		internal static Type typeofDecimalArray = typeof(decimal[]);

		// Token: 0x04002846 RID: 10310
		internal static Type typeofDoubleArray = typeof(double[]);

		// Token: 0x04002847 RID: 10311
		internal static Type typeofInt16Array = typeof(short[]);

		// Token: 0x04002848 RID: 10312
		internal static Type typeofInt32Array = typeof(int[]);

		// Token: 0x04002849 RID: 10313
		internal static Type typeofInt64Array = typeof(long[]);

		// Token: 0x0400284A RID: 10314
		internal static Type typeofSByteArray = typeof(sbyte[]);

		// Token: 0x0400284B RID: 10315
		internal static Type typeofSingleArray = typeof(float[]);

		// Token: 0x0400284C RID: 10316
		internal static Type typeofTimeSpanArray = typeof(TimeSpan[]);

		// Token: 0x0400284D RID: 10317
		internal static Type typeofDateTimeArray = typeof(DateTime[]);

		// Token: 0x0400284E RID: 10318
		internal static Type typeofUInt16Array = typeof(ushort[]);

		// Token: 0x0400284F RID: 10319
		internal static Type typeofUInt32Array = typeof(uint[]);

		// Token: 0x04002850 RID: 10320
		internal static Type typeofUInt64Array = typeof(ulong[]);

		// Token: 0x04002851 RID: 10321
		internal static Type typeofMarshalByRefObject = typeof(MarshalByRefObject);
	}
}
