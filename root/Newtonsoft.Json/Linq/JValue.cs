using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq.Expressions;
using System.Numerics;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000BE RID: 190
	public class JValue : JToken, IEquatable<JValue>, IFormattable, IComparable, IComparable<JValue>, IConvertible
	{
		// Token: 0x06000A0D RID: 2573 RVA: 0x000282EF File Offset: 0x000264EF
		internal JValue(object value, JTokenType type)
		{
			this._value = value;
			this._valueType = type;
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x00028305 File Offset: 0x00026505
		public JValue(JValue other)
			: this(other.Value, other.Type)
		{
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x00028319 File Offset: 0x00026519
		public JValue(long value)
			: this(value, JTokenType.Integer)
		{
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x00028328 File Offset: 0x00026528
		public JValue(decimal value)
			: this(value, JTokenType.Float)
		{
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x00028337 File Offset: 0x00026537
		public JValue(char value)
			: this(value, JTokenType.String)
		{
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x00028346 File Offset: 0x00026546
		[CLSCompliant(false)]
		public JValue(ulong value)
			: this(value, JTokenType.Integer)
		{
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x00028355 File Offset: 0x00026555
		public JValue(double value)
			: this(value, JTokenType.Float)
		{
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x00028364 File Offset: 0x00026564
		public JValue(float value)
			: this(value, JTokenType.Float)
		{
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x00028373 File Offset: 0x00026573
		public JValue(DateTime value)
			: this(value, JTokenType.Date)
		{
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x00028383 File Offset: 0x00026583
		public JValue(DateTimeOffset value)
			: this(value, JTokenType.Date)
		{
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x00028393 File Offset: 0x00026593
		public JValue(bool value)
			: this(value, JTokenType.Boolean)
		{
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x000283A3 File Offset: 0x000265A3
		public JValue(string value)
			: this(value, JTokenType.String)
		{
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x000283AD File Offset: 0x000265AD
		public JValue(Guid value)
			: this(value, JTokenType.Guid)
		{
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x000283BD File Offset: 0x000265BD
		public JValue(Uri value)
			: this(value, (value != null) ? JTokenType.Uri : JTokenType.Null)
		{
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x000283D5 File Offset: 0x000265D5
		public JValue(TimeSpan value)
			: this(value, JTokenType.TimeSpan)
		{
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x000283E8 File Offset: 0x000265E8
		public JValue(object value)
			: this(value, JValue.GetValueType(default(JTokenType?), value))
		{
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x0002840C File Offset: 0x0002660C
		internal override bool DeepEquals(JToken node)
		{
			JValue jvalue = node as JValue;
			return jvalue != null && (jvalue == this || JValue.ValuesEquals(this, jvalue));
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x0000F9CA File Offset: 0x0000DBCA
		public override bool HasValues
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x00028434 File Offset: 0x00026634
		private static int CompareBigInteger(BigInteger i1, object i2)
		{
			int num = i1.CompareTo(ConvertUtils.ToBigInteger(i2));
			if (num != 0)
			{
				return num;
			}
			if (i2 is decimal)
			{
				decimal num2 = (decimal)i2;
				return 0m.CompareTo(Math.Abs(num2 - Math.Truncate(num2)));
			}
			if (i2 is double || i2 is float)
			{
				double num3 = Convert.ToDouble(i2, CultureInfo.InvariantCulture);
				return 0.0.CompareTo(Math.Abs(num3 - Math.Truncate(num3)));
			}
			return num;
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x000284C0 File Offset: 0x000266C0
		internal static int Compare(JTokenType valueType, object objA, object objB)
		{
			if (objA == objB)
			{
				return 0;
			}
			if (objB == null)
			{
				return 1;
			}
			if (objA == null)
			{
				return -1;
			}
			switch (valueType)
			{
			case JTokenType.Comment:
			case JTokenType.String:
			case JTokenType.Raw:
			{
				string text = Convert.ToString(objA, CultureInfo.InvariantCulture);
				string text2 = Convert.ToString(objB, CultureInfo.InvariantCulture);
				return string.CompareOrdinal(text, text2);
			}
			case JTokenType.Integer:
				if (objA is BigInteger)
				{
					return JValue.CompareBigInteger((BigInteger)objA, objB);
				}
				if (objB is BigInteger)
				{
					return -JValue.CompareBigInteger((BigInteger)objB, objA);
				}
				if (objA is ulong || objB is ulong || objA is decimal || objB is decimal)
				{
					return Convert.ToDecimal(objA, CultureInfo.InvariantCulture).CompareTo(Convert.ToDecimal(objB, CultureInfo.InvariantCulture));
				}
				if (objA is float || objB is float || objA is double || objB is double)
				{
					return JValue.CompareFloat(objA, objB);
				}
				return Convert.ToInt64(objA, CultureInfo.InvariantCulture).CompareTo(Convert.ToInt64(objB, CultureInfo.InvariantCulture));
			case JTokenType.Float:
				if (objA is BigInteger)
				{
					return JValue.CompareBigInteger((BigInteger)objA, objB);
				}
				if (objB is BigInteger)
				{
					return -JValue.CompareBigInteger((BigInteger)objB, objA);
				}
				return JValue.CompareFloat(objA, objB);
			case JTokenType.Boolean:
			{
				bool flag = Convert.ToBoolean(objA, CultureInfo.InvariantCulture);
				bool flag2 = Convert.ToBoolean(objB, CultureInfo.InvariantCulture);
				return flag.CompareTo(flag2);
			}
			case JTokenType.Date:
			{
				if (objA is DateTime)
				{
					DateTime dateTime = (DateTime)objA;
					DateTime dateTime2;
					if (objB is DateTimeOffset)
					{
						dateTime2 = ((DateTimeOffset)objB).DateTime;
					}
					else
					{
						dateTime2 = Convert.ToDateTime(objB, CultureInfo.InvariantCulture);
					}
					return dateTime.CompareTo(dateTime2);
				}
				DateTimeOffset dateTimeOffset = (DateTimeOffset)objA;
				DateTimeOffset dateTimeOffset2;
				if (objB is DateTimeOffset)
				{
					dateTimeOffset2 = (DateTimeOffset)objB;
				}
				else
				{
					dateTimeOffset2..ctor(Convert.ToDateTime(objB, CultureInfo.InvariantCulture));
				}
				return dateTimeOffset.CompareTo(dateTimeOffset2);
			}
			case JTokenType.Bytes:
			{
				byte[] array = objB as byte[];
				if (array == null)
				{
					throw new ArgumentException("Object must be of type byte[].");
				}
				return MiscellaneousUtils.ByteArrayCompare(objA as byte[], array);
			}
			case JTokenType.Guid:
			{
				if (!(objB is Guid))
				{
					throw new ArgumentException("Object must be of type Guid.");
				}
				Guid guid = (Guid)objA;
				Guid guid2 = (Guid)objB;
				return guid.CompareTo(guid2);
			}
			case JTokenType.Uri:
			{
				Uri uri = objB as Uri;
				if (uri == null)
				{
					throw new ArgumentException("Object must be of type Uri.");
				}
				Uri uri2 = (Uri)objA;
				return Comparer<string>.Default.Compare(uri2.ToString(), uri.ToString());
			}
			case JTokenType.TimeSpan:
			{
				if (!(objB is TimeSpan))
				{
					throw new ArgumentException("Object must be of type TimeSpan.");
				}
				TimeSpan timeSpan = (TimeSpan)objA;
				TimeSpan timeSpan2 = (TimeSpan)objB;
				return timeSpan.CompareTo(timeSpan2);
			}
			}
			throw MiscellaneousUtils.CreateArgumentOutOfRangeException("valueType", valueType, "Unexpected value type: {0}".FormatWith(CultureInfo.InvariantCulture, valueType));
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0002879C File Offset: 0x0002699C
		private static int CompareFloat(object objA, object objB)
		{
			double num = Convert.ToDouble(objA, CultureInfo.InvariantCulture);
			double num2 = Convert.ToDouble(objB, CultureInfo.InvariantCulture);
			if (MathUtils.ApproxEquals(num, num2))
			{
				return 0;
			}
			return num.CompareTo(num2);
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x000287D4 File Offset: 0x000269D4
		private static bool Operation(ExpressionType operation, object objA, object objB, out object result)
		{
			if ((objA is string || objB is string) && (operation == null || operation == 63))
			{
				result = ((objA != null) ? objA.ToString() : null) + ((objB != null) ? objB.ToString() : null);
				return true;
			}
			if (objA is BigInteger || objB is BigInteger)
			{
				if (objA == null || objB == null)
				{
					result = null;
					return true;
				}
				BigInteger bigInteger = ConvertUtils.ToBigInteger(objA);
				BigInteger bigInteger2 = ConvertUtils.ToBigInteger(objB);
				if (operation <= 42)
				{
					if (operation <= 12)
					{
						if (operation != null)
						{
							if (operation != 12)
							{
								goto IL_0393;
							}
							goto IL_00DE;
						}
					}
					else
					{
						if (operation == 26)
						{
							goto IL_00CE;
						}
						if (operation != 42)
						{
							goto IL_0393;
						}
						goto IL_00BE;
					}
				}
				else if (operation <= 65)
				{
					if (operation != 63)
					{
						if (operation != 65)
						{
							goto IL_0393;
						}
						goto IL_00DE;
					}
				}
				else
				{
					if (operation == 69)
					{
						goto IL_00CE;
					}
					if (operation != 73)
					{
						goto IL_0393;
					}
					goto IL_00BE;
				}
				result = bigInteger + bigInteger2;
				return true;
				IL_00BE:
				result = bigInteger - bigInteger2;
				return true;
				IL_00CE:
				result = bigInteger * bigInteger2;
				return true;
				IL_00DE:
				result = bigInteger / bigInteger2;
				return true;
			}
			else if (objA is ulong || objB is ulong || objA is decimal || objB is decimal)
			{
				if (objA == null || objB == null)
				{
					result = null;
					return true;
				}
				decimal num = Convert.ToDecimal(objA, CultureInfo.InvariantCulture);
				decimal num2 = Convert.ToDecimal(objB, CultureInfo.InvariantCulture);
				if (operation <= 42)
				{
					if (operation <= 12)
					{
						if (operation != null)
						{
							if (operation != 12)
							{
								goto IL_0393;
							}
							goto IL_01AD;
						}
					}
					else
					{
						if (operation == 26)
						{
							goto IL_019D;
						}
						if (operation != 42)
						{
							goto IL_0393;
						}
						goto IL_018D;
					}
				}
				else if (operation <= 65)
				{
					if (operation != 63)
					{
						if (operation != 65)
						{
							goto IL_0393;
						}
						goto IL_01AD;
					}
				}
				else
				{
					if (operation == 69)
					{
						goto IL_019D;
					}
					if (operation != 73)
					{
						goto IL_0393;
					}
					goto IL_018D;
				}
				result = num + num2;
				return true;
				IL_018D:
				result = num - num2;
				return true;
				IL_019D:
				result = num * num2;
				return true;
				IL_01AD:
				result = num / num2;
				return true;
			}
			else if (objA is float || objB is float || objA is double || objB is double)
			{
				if (objA == null || objB == null)
				{
					result = null;
					return true;
				}
				double num3 = Convert.ToDouble(objA, CultureInfo.InvariantCulture);
				double num4 = Convert.ToDouble(objB, CultureInfo.InvariantCulture);
				if (operation <= 42)
				{
					if (operation <= 12)
					{
						if (operation != null)
						{
							if (operation != 12)
							{
								goto IL_0393;
							}
							goto IL_0278;
						}
					}
					else
					{
						if (operation == 26)
						{
							goto IL_026A;
						}
						if (operation != 42)
						{
							goto IL_0393;
						}
						goto IL_025C;
					}
				}
				else if (operation <= 65)
				{
					if (operation != 63)
					{
						if (operation != 65)
						{
							goto IL_0393;
						}
						goto IL_0278;
					}
				}
				else
				{
					if (operation == 69)
					{
						goto IL_026A;
					}
					if (operation != 73)
					{
						goto IL_0393;
					}
					goto IL_025C;
				}
				result = num3 + num4;
				return true;
				IL_025C:
				result = num3 - num4;
				return true;
				IL_026A:
				result = num3 * num4;
				return true;
				IL_0278:
				result = num3 / num4;
				return true;
			}
			else if (objA is int || objA is uint || objA is long || objA is short || objA is ushort || objA is sbyte || objA is byte || objB is int || objB is uint || objB is long || objB is short || objB is ushort || objB is sbyte || objB is byte)
			{
				if (objA == null || objB == null)
				{
					result = null;
					return true;
				}
				long num5 = Convert.ToInt64(objA, CultureInfo.InvariantCulture);
				long num6 = Convert.ToInt64(objB, CultureInfo.InvariantCulture);
				if (operation <= 42)
				{
					if (operation <= 12)
					{
						if (operation != null)
						{
							if (operation != 12)
							{
								goto IL_0393;
							}
							goto IL_0385;
						}
					}
					else
					{
						if (operation == 26)
						{
							goto IL_0377;
						}
						if (operation != 42)
						{
							goto IL_0393;
						}
						goto IL_0369;
					}
				}
				else if (operation <= 65)
				{
					if (operation != 63)
					{
						if (operation != 65)
						{
							goto IL_0393;
						}
						goto IL_0385;
					}
				}
				else
				{
					if (operation == 69)
					{
						goto IL_0377;
					}
					if (operation != 73)
					{
						goto IL_0393;
					}
					goto IL_0369;
				}
				result = num5 + num6;
				return true;
				IL_0369:
				result = num5 - num6;
				return true;
				IL_0377:
				result = num5 * num6;
				return true;
				IL_0385:
				result = num5 / num6;
				return true;
			}
			IL_0393:
			result = null;
			return false;
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x00028B78 File Offset: 0x00026D78
		internal override JToken CloneToken()
		{
			return new JValue(this);
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x00028B80 File Offset: 0x00026D80
		public static JValue CreateComment(string value)
		{
			return new JValue(value, JTokenType.Comment);
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x00028B89 File Offset: 0x00026D89
		public static JValue CreateString(string value)
		{
			return new JValue(value, JTokenType.String);
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x00028B92 File Offset: 0x00026D92
		public static JValue CreateNull()
		{
			return new JValue(null, JTokenType.Null);
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x00028B9C File Offset: 0x00026D9C
		public static JValue CreateUndefined()
		{
			return new JValue(null, JTokenType.Undefined);
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x00028BA8 File Offset: 0x00026DA8
		private static JTokenType GetValueType(JTokenType? current, object value)
		{
			if (value == null)
			{
				return JTokenType.Null;
			}
			if (value == DBNull.Value)
			{
				return JTokenType.Null;
			}
			if (value is string)
			{
				return JValue.GetStringValueType(current);
			}
			if (value is long || value is int || value is short || value is sbyte || value is ulong || value is uint || value is ushort || value is byte)
			{
				return JTokenType.Integer;
			}
			if (value is Enum)
			{
				return JTokenType.Integer;
			}
			if (value is BigInteger)
			{
				return JTokenType.Integer;
			}
			if (value is double || value is float || value is decimal)
			{
				return JTokenType.Float;
			}
			if (value is DateTime)
			{
				return JTokenType.Date;
			}
			if (value is DateTimeOffset)
			{
				return JTokenType.Date;
			}
			if (value is byte[])
			{
				return JTokenType.Bytes;
			}
			if (value is bool)
			{
				return JTokenType.Boolean;
			}
			if (value is Guid)
			{
				return JTokenType.Guid;
			}
			if (value is Uri)
			{
				return JTokenType.Uri;
			}
			if (value is TimeSpan)
			{
				return JTokenType.TimeSpan;
			}
			throw new ArgumentException("Could not determine JSON object type for type {0}.".FormatWith(CultureInfo.InvariantCulture, value.GetType()));
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x00028CAC File Offset: 0x00026EAC
		private static JTokenType GetStringValueType(JTokenType? current)
		{
			if (current == null)
			{
				return JTokenType.String;
			}
			JTokenType valueOrDefault = current.GetValueOrDefault();
			if (valueOrDefault == JTokenType.Comment || valueOrDefault == JTokenType.String || valueOrDefault == JTokenType.Raw)
			{
				return current.GetValueOrDefault();
			}
			return JTokenType.String;
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000A2A RID: 2602 RVA: 0x00028CE2 File Offset: 0x00026EE2
		public override JTokenType Type
		{
			get
			{
				return this._valueType;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000A2B RID: 2603 RVA: 0x00028CEA File Offset: 0x00026EEA
		// (set) Token: 0x06000A2C RID: 2604 RVA: 0x00028CF4 File Offset: 0x00026EF4
		public new object Value
		{
			get
			{
				return this._value;
			}
			set
			{
				object value2 = this._value;
				Type type = ((value2 != null) ? value2.GetType() : null);
				Type type2 = ((value != null) ? value.GetType() : null);
				if (type != type2)
				{
					this._valueType = JValue.GetValueType(new JTokenType?(this._valueType), value);
				}
				this._value = value;
			}
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x00028D48 File Offset: 0x00026F48
		public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
		{
			if (converters != null && converters.Length != 0 && this._value != null)
			{
				JsonConverter matchingConverter = JsonSerializer.GetMatchingConverter(converters, this._value.GetType());
				if (matchingConverter != null && matchingConverter.CanWrite)
				{
					matchingConverter.WriteJson(writer, this._value, JsonSerializer.CreateDefault());
					return;
				}
			}
			switch (this._valueType)
			{
			case JTokenType.Comment:
			{
				object value = this._value;
				writer.WriteComment((value != null) ? value.ToString() : null);
				return;
			}
			case JTokenType.Integer:
				if (this._value is int)
				{
					writer.WriteValue((int)this._value);
					return;
				}
				if (this._value is long)
				{
					writer.WriteValue((long)this._value);
					return;
				}
				if (this._value is ulong)
				{
					writer.WriteValue((ulong)this._value);
					return;
				}
				if (this._value is BigInteger)
				{
					writer.WriteValue((BigInteger)this._value);
					return;
				}
				writer.WriteValue(Convert.ToInt64(this._value, CultureInfo.InvariantCulture));
				return;
			case JTokenType.Float:
				if (this._value is decimal)
				{
					writer.WriteValue((decimal)this._value);
					return;
				}
				if (this._value is double)
				{
					writer.WriteValue((double)this._value);
					return;
				}
				if (this._value is float)
				{
					writer.WriteValue((float)this._value);
					return;
				}
				writer.WriteValue(Convert.ToDouble(this._value, CultureInfo.InvariantCulture));
				return;
			case JTokenType.String:
			{
				object value2 = this._value;
				writer.WriteValue((value2 != null) ? value2.ToString() : null);
				return;
			}
			case JTokenType.Boolean:
				writer.WriteValue(Convert.ToBoolean(this._value, CultureInfo.InvariantCulture));
				return;
			case JTokenType.Null:
				writer.WriteNull();
				return;
			case JTokenType.Undefined:
				writer.WriteUndefined();
				return;
			case JTokenType.Date:
				if (this._value is DateTimeOffset)
				{
					writer.WriteValue((DateTimeOffset)this._value);
					return;
				}
				writer.WriteValue(Convert.ToDateTime(this._value, CultureInfo.InvariantCulture));
				return;
			case JTokenType.Raw:
			{
				object value3 = this._value;
				writer.WriteRawValue((value3 != null) ? value3.ToString() : null);
				return;
			}
			case JTokenType.Bytes:
				writer.WriteValue((byte[])this._value);
				return;
			case JTokenType.Guid:
				writer.WriteValue((this._value != null) ? ((Guid?)this._value) : default(Guid?));
				return;
			case JTokenType.Uri:
				writer.WriteValue((Uri)this._value);
				return;
			case JTokenType.TimeSpan:
				writer.WriteValue((this._value != null) ? ((TimeSpan?)this._value) : default(TimeSpan?));
				return;
			default:
				throw MiscellaneousUtils.CreateArgumentOutOfRangeException("Type", this._valueType, "Unexpected token type.");
			}
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x00029018 File Offset: 0x00027218
		internal override int GetDeepHashCode()
		{
			int num = ((this._value != null) ? this._value.GetHashCode() : 0);
			int valueType = (int)this._valueType;
			return valueType.GetHashCode() ^ num;
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0002904C File Offset: 0x0002724C
		private static bool ValuesEquals(JValue v1, JValue v2)
		{
			return v1 == v2 || (v1._valueType == v2._valueType && JValue.Compare(v1._valueType, v1._value, v2._value) == 0);
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0002907E File Offset: 0x0002727E
		public bool Equals(JValue other)
		{
			return other != null && JValue.ValuesEquals(this, other);
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0002908C File Offset: 0x0002728C
		public override bool Equals(object obj)
		{
			return this.Equals(obj as JValue);
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x0002909A File Offset: 0x0002729A
		public override int GetHashCode()
		{
			if (this._value == null)
			{
				return 0;
			}
			return this._value.GetHashCode();
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x000290B1 File Offset: 0x000272B1
		public override string ToString()
		{
			if (this._value == null)
			{
				return string.Empty;
			}
			return this._value.ToString();
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x000290CC File Offset: 0x000272CC
		public string ToString(string format)
		{
			return this.ToString(format, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x000290DA File Offset: 0x000272DA
		public string ToString(IFormatProvider formatProvider)
		{
			return this.ToString(null, formatProvider);
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x000290E4 File Offset: 0x000272E4
		public string ToString(string format, IFormatProvider formatProvider)
		{
			if (this._value == null)
			{
				return string.Empty;
			}
			IFormattable formattable = this._value as IFormattable;
			if (formattable != null)
			{
				return formattable.ToString(format, formatProvider);
			}
			return this._value.ToString();
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x00029122 File Offset: 0x00027322
		protected override DynamicMetaObject GetMetaObject(Expression parameter)
		{
			return new DynamicProxyMetaObject<JValue>(parameter, this, new JValue.JValueDynamicProxy());
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x00029130 File Offset: 0x00027330
		int IComparable.CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			JValue jvalue = obj as JValue;
			object obj2 = ((jvalue != null) ? jvalue.Value : obj);
			return JValue.Compare(this._valueType, this._value, obj2);
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x00029168 File Offset: 0x00027368
		public int CompareTo(JValue obj)
		{
			if (obj == null)
			{
				return 1;
			}
			return JValue.Compare(this._valueType, this._value, obj._value);
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x00029188 File Offset: 0x00027388
		TypeCode IConvertible.GetTypeCode()
		{
			if (this._value == null)
			{
				return 0;
			}
			IConvertible convertible = this._value as IConvertible;
			if (convertible == null)
			{
				return 1;
			}
			return convertible.GetTypeCode();
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x000291B6 File Offset: 0x000273B6
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return (bool)this;
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x000291BE File Offset: 0x000273BE
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return (char)this;
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x000291C6 File Offset: 0x000273C6
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return (sbyte)this;
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x000291CE File Offset: 0x000273CE
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return (byte)this;
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x000291D6 File Offset: 0x000273D6
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return (short)this;
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x000291DE File Offset: 0x000273DE
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return (ushort)this;
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x000291E6 File Offset: 0x000273E6
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return (int)this;
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x000291EE File Offset: 0x000273EE
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return (uint)this;
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x000291F6 File Offset: 0x000273F6
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return (long)this;
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x000291FE File Offset: 0x000273FE
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return (ulong)this;
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x00029206 File Offset: 0x00027406
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return (float)this;
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x0002920F File Offset: 0x0002740F
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return (double)this;
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x00029218 File Offset: 0x00027418
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return (decimal)this;
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x00029220 File Offset: 0x00027420
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			return (DateTime)this;
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x00029228 File Offset: 0x00027428
		object IConvertible.ToType(Type conversionType, IFormatProvider provider)
		{
			return base.ToObject(conversionType);
		}

		// Token: 0x04000375 RID: 885
		private JTokenType _valueType;

		// Token: 0x04000376 RID: 886
		private object _value;

		// Token: 0x02000164 RID: 356
		private class JValueDynamicProxy : DynamicProxy<JValue>
		{
			// Token: 0x06000D8A RID: 3466 RVA: 0x000327FC File Offset: 0x000309FC
			public override bool TryConvert(JValue instance, ConvertBinder binder, out object result)
			{
				if (binder.Type == typeof(JValue) || binder.Type == typeof(JToken))
				{
					result = instance;
					return true;
				}
				object value = instance.Value;
				if (value == null)
				{
					result = null;
					return ReflectionUtils.IsNullable(binder.Type);
				}
				result = ConvertUtils.Convert(value, CultureInfo.InvariantCulture, binder.Type);
				return true;
			}

			// Token: 0x06000D8B RID: 3467 RVA: 0x0003286C File Offset: 0x00030A6C
			public override bool TryBinaryOperation(JValue instance, BinaryOperationBinder binder, object arg, out object result)
			{
				JValue jvalue = arg as JValue;
				object obj = ((jvalue != null) ? jvalue.Value : arg);
				ExpressionType operation = binder.Operation;
				if (operation <= 35)
				{
					if (operation <= 21)
					{
						if (operation != null)
						{
							switch (operation)
							{
							case 12:
								break;
							case 13:
								result = JValue.Compare(instance.Type, instance.Value, obj) == 0;
								return true;
							case 14:
							case 17:
							case 18:
							case 19:
								goto IL_018D;
							case 15:
								result = JValue.Compare(instance.Type, instance.Value, obj) > 0;
								return true;
							case 16:
								result = JValue.Compare(instance.Type, instance.Value, obj) >= 0;
								return true;
							case 20:
								result = JValue.Compare(instance.Type, instance.Value, obj) < 0;
								return true;
							case 21:
								result = JValue.Compare(instance.Type, instance.Value, obj) <= 0;
								return true;
							default:
								goto IL_018D;
							}
						}
					}
					else if (operation != 26)
					{
						if (operation != 35)
						{
							goto IL_018D;
						}
						result = JValue.Compare(instance.Type, instance.Value, obj) != 0;
						return true;
					}
				}
				else if (operation <= 63)
				{
					if (operation != 42 && operation != 63)
					{
						goto IL_018D;
					}
				}
				else if (operation != 65 && operation != 69 && operation != 73)
				{
					goto IL_018D;
				}
				if (JValue.Operation(binder.Operation, instance.Value, obj, out result))
				{
					result = new JValue(result);
					return true;
				}
				IL_018D:
				result = null;
				return false;
			}

			// Token: 0x06000D8C RID: 3468 RVA: 0x00032A0B File Offset: 0x00030C0B
			public JValueDynamicProxy()
			{
			}
		}
	}
}
