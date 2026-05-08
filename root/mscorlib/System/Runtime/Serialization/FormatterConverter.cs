using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.Runtime.Serialization
{
	// Token: 0x02000622 RID: 1570
	public class FormatterConverter : IFormatterConverter
	{
		// Token: 0x06003C1E RID: 15390 RVA: 0x000D1351 File Offset: 0x000CF551
		public object Convert(object value, Type type)
		{
			if (value == null)
			{
				FormatterConverter.ThrowValueNullException();
			}
			return global::System.Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
		}

		// Token: 0x06003C1F RID: 15391 RVA: 0x000D1367 File Offset: 0x000CF567
		public object Convert(object value, TypeCode typeCode)
		{
			if (value == null)
			{
				FormatterConverter.ThrowValueNullException();
			}
			return global::System.Convert.ChangeType(value, typeCode, CultureInfo.InvariantCulture);
		}

		// Token: 0x06003C20 RID: 15392 RVA: 0x000D137D File Offset: 0x000CF57D
		public bool ToBoolean(object value)
		{
			if (value == null)
			{
				FormatterConverter.ThrowValueNullException();
			}
			return global::System.Convert.ToBoolean(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06003C21 RID: 15393 RVA: 0x000D1392 File Offset: 0x000CF592
		public char ToChar(object value)
		{
			if (value == null)
			{
				FormatterConverter.ThrowValueNullException();
			}
			return global::System.Convert.ToChar(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06003C22 RID: 15394 RVA: 0x000D13A7 File Offset: 0x000CF5A7
		[CLSCompliant(false)]
		public sbyte ToSByte(object value)
		{
			if (value == null)
			{
				FormatterConverter.ThrowValueNullException();
			}
			return global::System.Convert.ToSByte(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06003C23 RID: 15395 RVA: 0x000D13BC File Offset: 0x000CF5BC
		public byte ToByte(object value)
		{
			if (value == null)
			{
				FormatterConverter.ThrowValueNullException();
			}
			return global::System.Convert.ToByte(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06003C24 RID: 15396 RVA: 0x000D13D1 File Offset: 0x000CF5D1
		public short ToInt16(object value)
		{
			if (value == null)
			{
				FormatterConverter.ThrowValueNullException();
			}
			return global::System.Convert.ToInt16(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06003C25 RID: 15397 RVA: 0x000D13E6 File Offset: 0x000CF5E6
		[CLSCompliant(false)]
		public ushort ToUInt16(object value)
		{
			if (value == null)
			{
				FormatterConverter.ThrowValueNullException();
			}
			return global::System.Convert.ToUInt16(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06003C26 RID: 15398 RVA: 0x000D13FB File Offset: 0x000CF5FB
		public int ToInt32(object value)
		{
			if (value == null)
			{
				FormatterConverter.ThrowValueNullException();
			}
			return global::System.Convert.ToInt32(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06003C27 RID: 15399 RVA: 0x000D1410 File Offset: 0x000CF610
		[CLSCompliant(false)]
		public uint ToUInt32(object value)
		{
			if (value == null)
			{
				FormatterConverter.ThrowValueNullException();
			}
			return global::System.Convert.ToUInt32(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06003C28 RID: 15400 RVA: 0x000D1425 File Offset: 0x000CF625
		public long ToInt64(object value)
		{
			if (value == null)
			{
				FormatterConverter.ThrowValueNullException();
			}
			return global::System.Convert.ToInt64(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06003C29 RID: 15401 RVA: 0x000D143A File Offset: 0x000CF63A
		[CLSCompliant(false)]
		public ulong ToUInt64(object value)
		{
			if (value == null)
			{
				FormatterConverter.ThrowValueNullException();
			}
			return global::System.Convert.ToUInt64(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06003C2A RID: 15402 RVA: 0x000D144F File Offset: 0x000CF64F
		public float ToSingle(object value)
		{
			if (value == null)
			{
				FormatterConverter.ThrowValueNullException();
			}
			return global::System.Convert.ToSingle(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06003C2B RID: 15403 RVA: 0x000D1464 File Offset: 0x000CF664
		public double ToDouble(object value)
		{
			if (value == null)
			{
				FormatterConverter.ThrowValueNullException();
			}
			return global::System.Convert.ToDouble(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06003C2C RID: 15404 RVA: 0x000D1479 File Offset: 0x000CF679
		public decimal ToDecimal(object value)
		{
			if (value == null)
			{
				FormatterConverter.ThrowValueNullException();
			}
			return global::System.Convert.ToDecimal(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06003C2D RID: 15405 RVA: 0x000D148E File Offset: 0x000CF68E
		public DateTime ToDateTime(object value)
		{
			if (value == null)
			{
				FormatterConverter.ThrowValueNullException();
			}
			return global::System.Convert.ToDateTime(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06003C2E RID: 15406 RVA: 0x000D14A3 File Offset: 0x000CF6A3
		public string ToString(object value)
		{
			if (value == null)
			{
				FormatterConverter.ThrowValueNullException();
			}
			return global::System.Convert.ToString(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06003C2F RID: 15407 RVA: 0x000D14B8 File Offset: 0x000CF6B8
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void ThrowValueNullException()
		{
			throw new ArgumentNullException("value");
		}

		// Token: 0x06003C30 RID: 15408 RVA: 0x000025BE File Offset: 0x000007BE
		public FormatterConverter()
		{
		}
	}
}
