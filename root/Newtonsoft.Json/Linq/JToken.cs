using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading;
using Newtonsoft.Json.Linq.JsonPath;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000BB RID: 187
	public abstract class JToken : IJEnumerable<JToken>, IEnumerable<JToken>, IEnumerable, IJsonLineInfo, ICloneable, IDynamicMetaObjectProvider
	{
		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600095E RID: 2398 RVA: 0x00025AEC File Offset: 0x00023CEC
		public static JTokenEqualityComparer EqualityComparer
		{
			get
			{
				if (JToken._equalityComparer == null)
				{
					JToken._equalityComparer = new JTokenEqualityComparer();
				}
				return JToken._equalityComparer;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x0600095F RID: 2399 RVA: 0x00025B04 File Offset: 0x00023D04
		// (set) Token: 0x06000960 RID: 2400 RVA: 0x00025B0C File Offset: 0x00023D0C
		public JContainer Parent
		{
			[DebuggerStepThrough]
			get
			{
				return this._parent;
			}
			internal set
			{
				this._parent = value;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000961 RID: 2401 RVA: 0x00025B18 File Offset: 0x00023D18
		public JToken Root
		{
			get
			{
				JContainer jcontainer = this.Parent;
				if (jcontainer == null)
				{
					return this;
				}
				while (jcontainer.Parent != null)
				{
					jcontainer = jcontainer.Parent;
				}
				return jcontainer;
			}
		}

		// Token: 0x06000962 RID: 2402
		internal abstract JToken CloneToken();

		// Token: 0x06000963 RID: 2403
		internal abstract bool DeepEquals(JToken node);

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000964 RID: 2404
		public abstract JTokenType Type { get; }

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000965 RID: 2405
		public abstract bool HasValues { get; }

		// Token: 0x06000966 RID: 2406 RVA: 0x00025B41 File Offset: 0x00023D41
		public static bool DeepEquals(JToken t1, JToken t2)
		{
			return t1 == t2 || (t1 != null && t2 != null && t1.DeepEquals(t2));
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x00025B58 File Offset: 0x00023D58
		// (set) Token: 0x06000968 RID: 2408 RVA: 0x00025B60 File Offset: 0x00023D60
		public JToken Next
		{
			get
			{
				return this._next;
			}
			internal set
			{
				this._next = value;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000969 RID: 2409 RVA: 0x00025B69 File Offset: 0x00023D69
		// (set) Token: 0x0600096A RID: 2410 RVA: 0x00025B71 File Offset: 0x00023D71
		public JToken Previous
		{
			get
			{
				return this._previous;
			}
			internal set
			{
				this._previous = value;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x00025B7C File Offset: 0x00023D7C
		public string Path
		{
			get
			{
				if (this.Parent == null)
				{
					return string.Empty;
				}
				List<JsonPosition> list = new List<JsonPosition>();
				JToken jtoken = null;
				for (JToken jtoken2 = this; jtoken2 != null; jtoken2 = jtoken2.Parent)
				{
					switch (jtoken2.Type)
					{
					case JTokenType.Array:
					case JTokenType.Constructor:
						if (jtoken != null)
						{
							int num = ((IList<JToken>)jtoken2).IndexOf(jtoken);
							List<JsonPosition> list2 = list;
							JsonPosition jsonPosition = new JsonPosition(JsonContainerType.Array)
							{
								Position = num
							};
							list2.Add(jsonPosition);
						}
						break;
					case JTokenType.Property:
					{
						JProperty jproperty = (JProperty)jtoken2;
						List<JsonPosition> list3 = list;
						JsonPosition jsonPosition = new JsonPosition(JsonContainerType.Object)
						{
							PropertyName = jproperty.Name
						};
						list3.Add(jsonPosition);
						break;
					}
					}
					jtoken = jtoken2;
				}
				list.Reverse();
				return JsonPosition.BuildPath(list, default(JsonPosition?));
			}
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x00008020 File Offset: 0x00006220
		internal JToken()
		{
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x00025C34 File Offset: 0x00023E34
		public void AddAfterSelf(object content)
		{
			if (this._parent == null)
			{
				throw new InvalidOperationException("The parent is missing.");
			}
			int num = this._parent.IndexOfItem(this);
			this._parent.AddInternal(num + 1, content, false);
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x00025C74 File Offset: 0x00023E74
		public void AddBeforeSelf(object content)
		{
			if (this._parent == null)
			{
				throw new InvalidOperationException("The parent is missing.");
			}
			int num = this._parent.IndexOfItem(this);
			this._parent.AddInternal(num, content, false);
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x00025CAF File Offset: 0x00023EAF
		public IEnumerable<JToken> Ancestors()
		{
			return this.GetAncestors(false);
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x00025CB8 File Offset: 0x00023EB8
		public IEnumerable<JToken> AncestorsAndSelf()
		{
			return this.GetAncestors(true);
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x00025CC1 File Offset: 0x00023EC1
		internal IEnumerable<JToken> GetAncestors(bool self)
		{
			JToken current;
			for (current = (self ? this : this.Parent); current != null; current = current.Parent)
			{
				yield return current;
			}
			current = null;
			yield break;
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x00025CD8 File Offset: 0x00023ED8
		public IEnumerable<JToken> AfterSelf()
		{
			if (this.Parent == null)
			{
				yield break;
			}
			JToken o;
			for (o = this.Next; o != null; o = o.Next)
			{
				yield return o;
			}
			o = null;
			yield break;
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x00025CE8 File Offset: 0x00023EE8
		public IEnumerable<JToken> BeforeSelf()
		{
			JToken o;
			for (o = this.Parent.First; o != this; o = o.Next)
			{
				yield return o;
			}
			o = null;
			yield break;
		}

		// Token: 0x170001EE RID: 494
		public virtual JToken this[object key]
		{
			get
			{
				throw new InvalidOperationException("Cannot access child value on {0}.".FormatWith(CultureInfo.InvariantCulture, base.GetType()));
			}
			set
			{
				throw new InvalidOperationException("Cannot set child value on {0}.".FormatWith(CultureInfo.InvariantCulture, base.GetType()));
			}
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x00025D30 File Offset: 0x00023F30
		public virtual T Value<T>(object key)
		{
			JToken jtoken = this[key];
			if (jtoken != null)
			{
				return jtoken.Convert<JToken, T>();
			}
			return default(T);
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000977 RID: 2423 RVA: 0x00025CF8 File Offset: 0x00023EF8
		public virtual JToken First
		{
			get
			{
				throw new InvalidOperationException("Cannot access child value on {0}.".FormatWith(CultureInfo.InvariantCulture, base.GetType()));
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000978 RID: 2424 RVA: 0x00025CF8 File Offset: 0x00023EF8
		public virtual JToken Last
		{
			get
			{
				throw new InvalidOperationException("Cannot access child value on {0}.".FormatWith(CultureInfo.InvariantCulture, base.GetType()));
			}
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x00025D58 File Offset: 0x00023F58
		public virtual JEnumerable<JToken> Children()
		{
			return JEnumerable<JToken>.Empty;
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x00025D5F File Offset: 0x00023F5F
		public JEnumerable<T> Children<T>() where T : JToken
		{
			return new JEnumerable<T>(Enumerable.OfType<T>(this.Children()));
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x00025CF8 File Offset: 0x00023EF8
		public virtual IEnumerable<T> Values<T>()
		{
			throw new InvalidOperationException("Cannot access child value on {0}.".FormatWith(CultureInfo.InvariantCulture, base.GetType()));
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x00025D76 File Offset: 0x00023F76
		public void Remove()
		{
			if (this._parent == null)
			{
				throw new InvalidOperationException("The parent is missing.");
			}
			this._parent.RemoveItem(this);
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x00025D98 File Offset: 0x00023F98
		public void Replace(JToken value)
		{
			if (this._parent == null)
			{
				throw new InvalidOperationException("The parent is missing.");
			}
			this._parent.ReplaceItem(this, value);
		}

		// Token: 0x0600097E RID: 2430
		public abstract void WriteTo(JsonWriter writer, params JsonConverter[] converters);

		// Token: 0x0600097F RID: 2431 RVA: 0x00025DBA File Offset: 0x00023FBA
		public override string ToString()
		{
			return this.ToString(Formatting.Indented, new JsonConverter[0]);
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x00025DCC File Offset: 0x00023FCC
		public string ToString(Formatting formatting, params JsonConverter[] converters)
		{
			string text;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				this.WriteTo(new JsonTextWriter(stringWriter)
				{
					Formatting = formatting
				}, converters);
				text = stringWriter.ToString();
			}
			return text;
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x00025E20 File Offset: 0x00024020
		private static JValue EnsureValue(JToken value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value is JProperty)
			{
				value = ((JProperty)value).Value;
			}
			return value as JValue;
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x00025E4C File Offset: 0x0002404C
		private static string GetType(JToken token)
		{
			ValidationUtils.ArgumentNotNull(token, "token");
			if (token is JProperty)
			{
				token = ((JProperty)token).Value;
			}
			return token.Type.ToString();
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x00025E8D File Offset: 0x0002408D
		private static bool ValidateToken(JToken o, JTokenType[] validTypes, bool nullable)
		{
			return Array.IndexOf<JTokenType>(validTypes, o.Type) != -1 || (nullable && (o.Type == JTokenType.Null || o.Type == JTokenType.Undefined));
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x00025EBC File Offset: 0x000240BC
		public static explicit operator bool(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.BooleanTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to Boolean.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value is BigInteger)
			{
				return Convert.ToBoolean((int)((BigInteger)jvalue.Value));
			}
			return Convert.ToBoolean(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x00025F30 File Offset: 0x00024130
		public static explicit operator DateTimeOffset(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.DateTimeTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to DateTimeOffset.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value is DateTimeOffset)
			{
				return (DateTimeOffset)jvalue.Value;
			}
			string text = jvalue.Value as string;
			if (text != null)
			{
				return DateTimeOffset.Parse(text, CultureInfo.InvariantCulture);
			}
			return new DateTimeOffset(Convert.ToDateTime(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x00025FBC File Offset: 0x000241BC
		public static explicit operator bool?(JToken value)
		{
			if (value == null)
			{
				return default(bool?);
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.BooleanTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to Boolean.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value is BigInteger)
			{
				return new bool?(Convert.ToBoolean((int)((BigInteger)jvalue.Value)));
			}
			if (jvalue.Value == null)
			{
				return default(bool?);
			}
			return new bool?(Convert.ToBoolean(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x00026058 File Offset: 0x00024258
		public static explicit operator long(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to Int64.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value is BigInteger)
			{
				return (long)((BigInteger)jvalue.Value);
			}
			return Convert.ToInt64(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x000260C8 File Offset: 0x000242C8
		public static explicit operator DateTime?(JToken value)
		{
			if (value == null)
			{
				return default(DateTime?);
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.DateTimeTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to DateTime.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value is DateTimeOffset)
			{
				return new DateTime?(((DateTimeOffset)jvalue.Value).DateTime);
			}
			if (jvalue.Value == null)
			{
				return default(DateTime?);
			}
			return new DateTime?(Convert.ToDateTime(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x00026164 File Offset: 0x00024364
		public static explicit operator DateTimeOffset?(JToken value)
		{
			if (value == null)
			{
				return default(DateTimeOffset?);
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.DateTimeTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to DateTimeOffset.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value == null)
			{
				return default(DateTimeOffset?);
			}
			if (jvalue.Value is DateTimeOffset)
			{
				return (DateTimeOffset?)jvalue.Value;
			}
			string text = jvalue.Value as string;
			if (text != null)
			{
				return new DateTimeOffset?(DateTimeOffset.Parse(text, CultureInfo.InvariantCulture));
			}
			return new DateTimeOffset?(new DateTimeOffset(Convert.ToDateTime(jvalue.Value, CultureInfo.InvariantCulture)));
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x00026218 File Offset: 0x00024418
		public static explicit operator decimal?(JToken value)
		{
			if (value == null)
			{
				return default(decimal?);
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to Decimal.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value is BigInteger)
			{
				return new decimal?((decimal)((BigInteger)jvalue.Value));
			}
			if (jvalue.Value == null)
			{
				return default(decimal?);
			}
			return new decimal?(Convert.ToDecimal(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x000262B0 File Offset: 0x000244B0
		public static explicit operator double?(JToken value)
		{
			if (value == null)
			{
				return default(double?);
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to Double.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value is BigInteger)
			{
				return new double?((double)((BigInteger)jvalue.Value));
			}
			if (jvalue.Value == null)
			{
				return default(double?);
			}
			return new double?(Convert.ToDouble(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x00026348 File Offset: 0x00024548
		public static explicit operator char?(JToken value)
		{
			if (value == null)
			{
				return default(char?);
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.CharTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to Char.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value is BigInteger)
			{
				return new char?((char)(ushort)((BigInteger)jvalue.Value));
			}
			if (jvalue.Value == null)
			{
				return default(char?);
			}
			return new char?(Convert.ToChar(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x000263E0 File Offset: 0x000245E0
		public static explicit operator int(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to Int32.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value is BigInteger)
			{
				return (int)((BigInteger)jvalue.Value);
			}
			return Convert.ToInt32(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x00026450 File Offset: 0x00024650
		public static explicit operator short(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to Int16.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value is BigInteger)
			{
				return (short)((BigInteger)jvalue.Value);
			}
			return Convert.ToInt16(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x000264C0 File Offset: 0x000246C0
		[CLSCompliant(false)]
		public static explicit operator ushort(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to UInt16.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value is BigInteger)
			{
				return (ushort)((BigInteger)jvalue.Value);
			}
			return Convert.ToUInt16(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x00026530 File Offset: 0x00024730
		[CLSCompliant(false)]
		public static explicit operator char(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.CharTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to Char.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value is BigInteger)
			{
				return (char)(ushort)((BigInteger)jvalue.Value);
			}
			return Convert.ToChar(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x000265A0 File Offset: 0x000247A0
		public static explicit operator byte(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to Byte.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value is BigInteger)
			{
				return (byte)((BigInteger)jvalue.Value);
			}
			return Convert.ToByte(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x00026610 File Offset: 0x00024810
		[CLSCompliant(false)]
		public static explicit operator sbyte(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to SByte.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value is BigInteger)
			{
				return (sbyte)((BigInteger)jvalue.Value);
			}
			return Convert.ToSByte(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x00026680 File Offset: 0x00024880
		public static explicit operator int?(JToken value)
		{
			if (value == null)
			{
				return default(int?);
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to Int32.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value is BigInteger)
			{
				return new int?((int)((BigInteger)jvalue.Value));
			}
			if (jvalue.Value == null)
			{
				return default(int?);
			}
			return new int?(Convert.ToInt32(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x00026718 File Offset: 0x00024918
		public static explicit operator short?(JToken value)
		{
			if (value == null)
			{
				return default(short?);
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to Int16.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value is BigInteger)
			{
				return new short?((short)((BigInteger)jvalue.Value));
			}
			if (jvalue.Value == null)
			{
				return default(short?);
			}
			return new short?(Convert.ToInt16(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x000267B0 File Offset: 0x000249B0
		[CLSCompliant(false)]
		public static explicit operator ushort?(JToken value)
		{
			if (value == null)
			{
				return default(ushort?);
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to UInt16.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value is BigInteger)
			{
				return new ushort?((ushort)((BigInteger)jvalue.Value));
			}
			if (jvalue.Value == null)
			{
				return default(ushort?);
			}
			return new ushort?(Convert.ToUInt16(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x00026848 File Offset: 0x00024A48
		public static explicit operator byte?(JToken value)
		{
			if (value == null)
			{
				return default(byte?);
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to Byte.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value is BigInteger)
			{
				return new byte?((byte)((BigInteger)jvalue.Value));
			}
			if (jvalue.Value == null)
			{
				return default(byte?);
			}
			return new byte?(Convert.ToByte(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x000268E0 File Offset: 0x00024AE0
		[CLSCompliant(false)]
		public static explicit operator sbyte?(JToken value)
		{
			if (value == null)
			{
				return default(sbyte?);
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to SByte.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value is BigInteger)
			{
				return new sbyte?((sbyte)((BigInteger)jvalue.Value));
			}
			if (jvalue.Value == null)
			{
				return default(sbyte?);
			}
			return new sbyte?(Convert.ToSByte(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x00026978 File Offset: 0x00024B78
		public static explicit operator DateTime(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.DateTimeTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to DateTime.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value is DateTimeOffset)
			{
				return ((DateTimeOffset)jvalue.Value).DateTime;
			}
			return Convert.ToDateTime(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x000269EC File Offset: 0x00024BEC
		public static explicit operator long?(JToken value)
		{
			if (value == null)
			{
				return default(long?);
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to Int64.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value is BigInteger)
			{
				return new long?((long)((BigInteger)jvalue.Value));
			}
			if (jvalue.Value == null)
			{
				return default(long?);
			}
			return new long?(Convert.ToInt64(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x00026A84 File Offset: 0x00024C84
		public static explicit operator float?(JToken value)
		{
			if (value == null)
			{
				return default(float?);
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to Single.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value is BigInteger)
			{
				return new float?((float)((BigInteger)jvalue.Value));
			}
			if (jvalue.Value == null)
			{
				return default(float?);
			}
			return new float?(Convert.ToSingle(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x00026B1C File Offset: 0x00024D1C
		public static explicit operator decimal(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to Decimal.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value is BigInteger)
			{
				return (decimal)((BigInteger)jvalue.Value);
			}
			return Convert.ToDecimal(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x00026B8C File Offset: 0x00024D8C
		[CLSCompliant(false)]
		public static explicit operator uint?(JToken value)
		{
			if (value == null)
			{
				return default(uint?);
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to UInt32.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value is BigInteger)
			{
				return new uint?((uint)((BigInteger)jvalue.Value));
			}
			if (jvalue.Value == null)
			{
				return default(uint?);
			}
			return new uint?(Convert.ToUInt32(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x00026C24 File Offset: 0x00024E24
		[CLSCompliant(false)]
		public static explicit operator ulong?(JToken value)
		{
			if (value == null)
			{
				return default(ulong?);
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to UInt64.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value is BigInteger)
			{
				return new ulong?((ulong)((BigInteger)jvalue.Value));
			}
			if (jvalue.Value == null)
			{
				return default(ulong?);
			}
			return new ulong?(Convert.ToUInt64(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x00026CBC File Offset: 0x00024EBC
		public static explicit operator double(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to Double.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value is BigInteger)
			{
				return (double)((BigInteger)jvalue.Value);
			}
			return Convert.ToDouble(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x00026D2C File Offset: 0x00024F2C
		public static explicit operator float(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to Single.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value is BigInteger)
			{
				return (float)((BigInteger)jvalue.Value);
			}
			return Convert.ToSingle(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x00026D9C File Offset: 0x00024F9C
		public static explicit operator string(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.StringTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to String.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			byte[] array = jvalue.Value as byte[];
			if (array != null)
			{
				return Convert.ToBase64String(array);
			}
			if (jvalue.Value is BigInteger)
			{
				return ((BigInteger)jvalue.Value).ToString(CultureInfo.InvariantCulture);
			}
			return Convert.ToString(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x00026E38 File Offset: 0x00025038
		[CLSCompliant(false)]
		public static explicit operator uint(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to UInt32.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value is BigInteger)
			{
				return (uint)((BigInteger)jvalue.Value);
			}
			return Convert.ToUInt32(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x00026EA8 File Offset: 0x000250A8
		[CLSCompliant(false)]
		public static explicit operator ulong(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to UInt64.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value is BigInteger)
			{
				return (ulong)((BigInteger)jvalue.Value);
			}
			return Convert.ToUInt64(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x00026F18 File Offset: 0x00025118
		public static explicit operator byte[](JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.BytesTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to byte array.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value is string)
			{
				return Convert.FromBase64String(Convert.ToString(jvalue.Value, CultureInfo.InvariantCulture));
			}
			if (jvalue.Value is BigInteger)
			{
				return ((BigInteger)jvalue.Value).ToByteArray();
			}
			byte[] array = jvalue.Value as byte[];
			if (array != null)
			{
				return array;
			}
			throw new ArgumentException("Can not convert {0} to byte array.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x00026FCC File Offset: 0x000251CC
		public static explicit operator Guid(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.GuidTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to Guid.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			byte[] array = jvalue.Value as byte[];
			if (array != null)
			{
				return new Guid(array);
			}
			if (!(jvalue.Value is Guid))
			{
				return new Guid(Convert.ToString(jvalue.Value, CultureInfo.InvariantCulture));
			}
			return (Guid)jvalue.Value;
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x00027050 File Offset: 0x00025250
		public static explicit operator Guid?(JToken value)
		{
			if (value == null)
			{
				return default(Guid?);
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.GuidTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to Guid.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value == null)
			{
				return default(Guid?);
			}
			byte[] array = jvalue.Value as byte[];
			if (array != null)
			{
				return new Guid?(new Guid(array));
			}
			return new Guid?((jvalue.Value is Guid) ? ((Guid)jvalue.Value) : new Guid(Convert.ToString(jvalue.Value, CultureInfo.InvariantCulture)));
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x00027100 File Offset: 0x00025300
		public static explicit operator TimeSpan(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.TimeSpanTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to TimeSpan.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (!(jvalue.Value is TimeSpan))
			{
				return ConvertUtils.ParseTimeSpan(Convert.ToString(jvalue.Value, CultureInfo.InvariantCulture));
			}
			return (TimeSpan)jvalue.Value;
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x00027170 File Offset: 0x00025370
		public static explicit operator TimeSpan?(JToken value)
		{
			if (value == null)
			{
				return default(TimeSpan?);
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.TimeSpanTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to TimeSpan.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value == null)
			{
				return default(TimeSpan?);
			}
			return new TimeSpan?((jvalue.Value is TimeSpan) ? ((TimeSpan)jvalue.Value) : ConvertUtils.ParseTimeSpan(Convert.ToString(jvalue.Value, CultureInfo.InvariantCulture)));
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x00027204 File Offset: 0x00025404
		public static explicit operator Uri(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.UriTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to Uri.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			if (!(jvalue.Value is Uri))
			{
				return new Uri(Convert.ToString(jvalue.Value, CultureInfo.InvariantCulture));
			}
			return (Uri)jvalue.Value;
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x00027284 File Offset: 0x00025484
		private static BigInteger ToBigInteger(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.BigIntegerTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to BigInteger.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			return ConvertUtils.ToBigInteger(jvalue.Value);
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x000272D0 File Offset: 0x000254D0
		private static BigInteger? ToBigIntegerNullable(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.BigIntegerTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to BigInteger.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value == null)
			{
				return default(BigInteger?);
			}
			return new BigInteger?(ConvertUtils.ToBigInteger(jvalue.Value));
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x00027332 File Offset: 0x00025532
		public static implicit operator JToken(bool value)
		{
			return new JValue(value);
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x0002733A File Offset: 0x0002553A
		public static implicit operator JToken(DateTimeOffset value)
		{
			return new JValue(value);
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x00027342 File Offset: 0x00025542
		public static implicit operator JToken(byte value)
		{
			return new JValue((long)((ulong)value));
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0002734B File Offset: 0x0002554B
		public static implicit operator JToken(byte? value)
		{
			return new JValue(value);
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x00027358 File Offset: 0x00025558
		[CLSCompliant(false)]
		public static implicit operator JToken(sbyte value)
		{
			return new JValue((long)value);
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x00027361 File Offset: 0x00025561
		[CLSCompliant(false)]
		public static implicit operator JToken(sbyte? value)
		{
			return new JValue(value);
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x0002736E File Offset: 0x0002556E
		public static implicit operator JToken(bool? value)
		{
			return new JValue(value);
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x0002737B File Offset: 0x0002557B
		public static implicit operator JToken(long value)
		{
			return new JValue(value);
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x00027383 File Offset: 0x00025583
		public static implicit operator JToken(DateTime? value)
		{
			return new JValue(value);
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x00027390 File Offset: 0x00025590
		public static implicit operator JToken(DateTimeOffset? value)
		{
			return new JValue(value);
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0002739D File Offset: 0x0002559D
		public static implicit operator JToken(decimal? value)
		{
			return new JValue(value);
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x000273AA File Offset: 0x000255AA
		public static implicit operator JToken(double? value)
		{
			return new JValue(value);
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x00027358 File Offset: 0x00025558
		[CLSCompliant(false)]
		public static implicit operator JToken(short value)
		{
			return new JValue((long)value);
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x00027342 File Offset: 0x00025542
		[CLSCompliant(false)]
		public static implicit operator JToken(ushort value)
		{
			return new JValue((long)((ulong)value));
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x00027358 File Offset: 0x00025558
		public static implicit operator JToken(int value)
		{
			return new JValue((long)value);
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x000273B7 File Offset: 0x000255B7
		public static implicit operator JToken(int? value)
		{
			return new JValue(value);
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x000273C4 File Offset: 0x000255C4
		public static implicit operator JToken(DateTime value)
		{
			return new JValue(value);
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x000273CC File Offset: 0x000255CC
		public static implicit operator JToken(long? value)
		{
			return new JValue(value);
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x000273D9 File Offset: 0x000255D9
		public static implicit operator JToken(float? value)
		{
			return new JValue(value);
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x000273E6 File Offset: 0x000255E6
		public static implicit operator JToken(decimal value)
		{
			return new JValue(value);
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x000273EE File Offset: 0x000255EE
		[CLSCompliant(false)]
		public static implicit operator JToken(short? value)
		{
			return new JValue(value);
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x000273FB File Offset: 0x000255FB
		[CLSCompliant(false)]
		public static implicit operator JToken(ushort? value)
		{
			return new JValue(value);
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x00027408 File Offset: 0x00025608
		[CLSCompliant(false)]
		public static implicit operator JToken(uint? value)
		{
			return new JValue(value);
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x00027415 File Offset: 0x00025615
		[CLSCompliant(false)]
		public static implicit operator JToken(ulong? value)
		{
			return new JValue(value);
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x00027422 File Offset: 0x00025622
		public static implicit operator JToken(double value)
		{
			return new JValue(value);
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x0002742A File Offset: 0x0002562A
		public static implicit operator JToken(float value)
		{
			return new JValue(value);
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x00027432 File Offset: 0x00025632
		public static implicit operator JToken(string value)
		{
			return new JValue(value);
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x00027342 File Offset: 0x00025542
		[CLSCompliant(false)]
		public static implicit operator JToken(uint value)
		{
			return new JValue((long)((ulong)value));
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x0002743A File Offset: 0x0002563A
		[CLSCompliant(false)]
		public static implicit operator JToken(ulong value)
		{
			return new JValue(value);
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x00027442 File Offset: 0x00025642
		public static implicit operator JToken(byte[] value)
		{
			return new JValue(value);
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x0002744A File Offset: 0x0002564A
		public static implicit operator JToken(Uri value)
		{
			return new JValue(value);
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x00027452 File Offset: 0x00025652
		public static implicit operator JToken(TimeSpan value)
		{
			return new JValue(value);
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x0002745A File Offset: 0x0002565A
		public static implicit operator JToken(TimeSpan? value)
		{
			return new JValue(value);
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x00027467 File Offset: 0x00025667
		public static implicit operator JToken(Guid value)
		{
			return new JValue(value);
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x0002746F File Offset: 0x0002566F
		public static implicit operator JToken(Guid? value)
		{
			return new JValue(value);
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x0002747C File Offset: 0x0002567C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x00027484 File Offset: 0x00025684
		IEnumerator<JToken> IEnumerable<JToken>.GetEnumerator()
		{
			return this.Children().GetEnumerator();
		}

		// Token: 0x060009D0 RID: 2512
		internal abstract int GetDeepHashCode();

		// Token: 0x170001F1 RID: 497
		IJEnumerable<JToken> IJEnumerable<JToken>.this[object key]
		{
			get
			{
				return this[key];
			}
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x000274A8 File Offset: 0x000256A8
		public JsonReader CreateReader()
		{
			return new JTokenReader(this);
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x000274B0 File Offset: 0x000256B0
		internal static JToken FromObjectInternal(object o, JsonSerializer jsonSerializer)
		{
			ValidationUtils.ArgumentNotNull(o, "o");
			ValidationUtils.ArgumentNotNull(jsonSerializer, "jsonSerializer");
			JToken token;
			using (JTokenWriter jtokenWriter = new JTokenWriter())
			{
				jsonSerializer.Serialize(jtokenWriter, o);
				token = jtokenWriter.Token;
			}
			return token;
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x00027508 File Offset: 0x00025708
		public static JToken FromObject(object o)
		{
			return JToken.FromObjectInternal(o, JsonSerializer.CreateDefault());
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x00027515 File Offset: 0x00025715
		public static JToken FromObject(object o, JsonSerializer jsonSerializer)
		{
			return JToken.FromObjectInternal(o, jsonSerializer);
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x0002751E File Offset: 0x0002571E
		public T ToObject<T>()
		{
			return (T)((object)this.ToObject(typeof(T)));
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x00027538 File Offset: 0x00025738
		public object ToObject(Type objectType)
		{
			if (JsonConvert.DefaultSettings == null)
			{
				bool flag;
				PrimitiveTypeCode typeCode = ConvertUtils.GetTypeCode(objectType, out flag);
				if (flag)
				{
					if (this.Type == JTokenType.String)
					{
						try
						{
							return this.ToObject(objectType, JsonSerializer.CreateDefault());
						}
						catch (Exception ex)
						{
							Type type = (objectType.IsEnum() ? objectType : Nullable.GetUnderlyingType(objectType));
							throw new ArgumentException("Could not convert '{0}' to {1}.".FormatWith(CultureInfo.InvariantCulture, (string)this, type.Name), ex);
						}
					}
					if (this.Type == JTokenType.Integer)
					{
						return Enum.ToObject(objectType.IsEnum() ? objectType : Nullable.GetUnderlyingType(objectType), ((JValue)this).Value);
					}
				}
				switch (typeCode)
				{
				case PrimitiveTypeCode.Char:
					return (char)this;
				case PrimitiveTypeCode.CharNullable:
					return (char?)this;
				case PrimitiveTypeCode.Boolean:
					return (bool)this;
				case PrimitiveTypeCode.BooleanNullable:
					return (bool?)this;
				case PrimitiveTypeCode.SByte:
					return (sbyte)this;
				case PrimitiveTypeCode.SByteNullable:
					return (sbyte?)this;
				case PrimitiveTypeCode.Int16:
					return (short)this;
				case PrimitiveTypeCode.Int16Nullable:
					return (short?)this;
				case PrimitiveTypeCode.UInt16:
					return (ushort)this;
				case PrimitiveTypeCode.UInt16Nullable:
					return (ushort?)this;
				case PrimitiveTypeCode.Int32:
					return (int)this;
				case PrimitiveTypeCode.Int32Nullable:
					return (int?)this;
				case PrimitiveTypeCode.Byte:
					return (byte)this;
				case PrimitiveTypeCode.ByteNullable:
					return (byte?)this;
				case PrimitiveTypeCode.UInt32:
					return (uint)this;
				case PrimitiveTypeCode.UInt32Nullable:
					return (uint?)this;
				case PrimitiveTypeCode.Int64:
					return (long)this;
				case PrimitiveTypeCode.Int64Nullable:
					return (long?)this;
				case PrimitiveTypeCode.UInt64:
					return (ulong)this;
				case PrimitiveTypeCode.UInt64Nullable:
					return (ulong?)this;
				case PrimitiveTypeCode.Single:
					return (float)this;
				case PrimitiveTypeCode.SingleNullable:
					return (float?)this;
				case PrimitiveTypeCode.Double:
					return (double)this;
				case PrimitiveTypeCode.DoubleNullable:
					return (double?)this;
				case PrimitiveTypeCode.DateTime:
					return (DateTime)this;
				case PrimitiveTypeCode.DateTimeNullable:
					return (DateTime?)this;
				case PrimitiveTypeCode.DateTimeOffset:
					return (DateTimeOffset)this;
				case PrimitiveTypeCode.DateTimeOffsetNullable:
					return (DateTimeOffset?)this;
				case PrimitiveTypeCode.Decimal:
					return (decimal)this;
				case PrimitiveTypeCode.DecimalNullable:
					return (decimal?)this;
				case PrimitiveTypeCode.Guid:
					return (Guid)this;
				case PrimitiveTypeCode.GuidNullable:
					return (Guid?)this;
				case PrimitiveTypeCode.TimeSpan:
					return (TimeSpan)this;
				case PrimitiveTypeCode.TimeSpanNullable:
					return (TimeSpan?)this;
				case PrimitiveTypeCode.BigInteger:
					return JToken.ToBigInteger(this);
				case PrimitiveTypeCode.BigIntegerNullable:
					return JToken.ToBigIntegerNullable(this);
				case PrimitiveTypeCode.Uri:
					return (Uri)this;
				case PrimitiveTypeCode.String:
					return (string)this;
				}
			}
			return this.ToObject(objectType, JsonSerializer.CreateDefault());
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x0002785C File Offset: 0x00025A5C
		public T ToObject<T>(JsonSerializer jsonSerializer)
		{
			return (T)((object)this.ToObject(typeof(T), jsonSerializer));
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x00027874 File Offset: 0x00025A74
		public object ToObject(Type objectType, JsonSerializer jsonSerializer)
		{
			ValidationUtils.ArgumentNotNull(jsonSerializer, "jsonSerializer");
			object obj;
			using (JTokenReader jtokenReader = new JTokenReader(this))
			{
				obj = jsonSerializer.Deserialize(jtokenReader, objectType);
			}
			return obj;
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x000278BC File Offset: 0x00025ABC
		public static JToken ReadFrom(JsonReader reader)
		{
			return JToken.ReadFrom(reader, null);
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x000278C8 File Offset: 0x00025AC8
		public static JToken ReadFrom(JsonReader reader, JsonLoadSettings settings)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			if (reader.TokenType == JsonToken.None && !((settings != null && settings.CommentHandling == CommentHandling.Ignore) ? reader.ReadAndMoveToContent() : reader.Read()))
			{
				throw JsonReaderException.Create(reader, "Error reading JToken from JsonReader.");
			}
			IJsonLineInfo jsonLineInfo = reader as IJsonLineInfo;
			switch (reader.TokenType)
			{
			case JsonToken.StartObject:
				return JObject.Load(reader, settings);
			case JsonToken.StartArray:
				return JArray.Load(reader, settings);
			case JsonToken.StartConstructor:
				return JConstructor.Load(reader, settings);
			case JsonToken.PropertyName:
				return JProperty.Load(reader, settings);
			case JsonToken.Comment:
			{
				JValue jvalue = JValue.CreateComment(reader.Value.ToString());
				jvalue.SetLineInfo(jsonLineInfo, settings);
				return jvalue;
			}
			case JsonToken.Integer:
			case JsonToken.Float:
			case JsonToken.String:
			case JsonToken.Boolean:
			case JsonToken.Date:
			case JsonToken.Bytes:
			{
				JValue jvalue2 = new JValue(reader.Value);
				jvalue2.SetLineInfo(jsonLineInfo, settings);
				return jvalue2;
			}
			case JsonToken.Null:
			{
				JValue jvalue3 = JValue.CreateNull();
				jvalue3.SetLineInfo(jsonLineInfo, settings);
				return jvalue3;
			}
			case JsonToken.Undefined:
			{
				JValue jvalue4 = JValue.CreateUndefined();
				jvalue4.SetLineInfo(jsonLineInfo, settings);
				return jvalue4;
			}
			}
			throw JsonReaderException.Create(reader, "Error reading JToken from JsonReader. Unexpected token: {0}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x000279F4 File Offset: 0x00025BF4
		public static JToken Parse(string json)
		{
			return JToken.Parse(json, null);
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x00027A00 File Offset: 0x00025C00
		public static JToken Parse(string json, JsonLoadSettings settings)
		{
			JToken jtoken2;
			using (JsonReader jsonReader = new JsonTextReader(new StringReader(json)))
			{
				JToken jtoken = JToken.Load(jsonReader, settings);
				while (jsonReader.Read())
				{
				}
				jtoken2 = jtoken;
			}
			return jtoken2;
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x00027A48 File Offset: 0x00025C48
		public static JToken Load(JsonReader reader, JsonLoadSettings settings)
		{
			return JToken.ReadFrom(reader, settings);
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x00027A51 File Offset: 0x00025C51
		public static JToken Load(JsonReader reader)
		{
			return JToken.Load(reader, null);
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x00027A5A File Offset: 0x00025C5A
		internal void SetLineInfo(IJsonLineInfo lineInfo, JsonLoadSettings settings)
		{
			if (settings != null && settings.LineInfoHandling != LineInfoHandling.Load)
			{
				return;
			}
			if (lineInfo == null || !lineInfo.HasLineInfo())
			{
				return;
			}
			this.SetLineInfo(lineInfo.LineNumber, lineInfo.LinePosition);
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x00027A87 File Offset: 0x00025C87
		internal void SetLineInfo(int lineNumber, int linePosition)
		{
			this.AddAnnotation(new JToken.LineInfoAnnotation(lineNumber, linePosition));
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x00027A96 File Offset: 0x00025C96
		bool IJsonLineInfo.HasLineInfo()
		{
			return this.Annotation<JToken.LineInfoAnnotation>() != null;
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060009E3 RID: 2531 RVA: 0x00027AA4 File Offset: 0x00025CA4
		int IJsonLineInfo.LineNumber
		{
			get
			{
				JToken.LineInfoAnnotation lineInfoAnnotation = this.Annotation<JToken.LineInfoAnnotation>();
				if (lineInfoAnnotation != null)
				{
					return lineInfoAnnotation.LineNumber;
				}
				return 0;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060009E4 RID: 2532 RVA: 0x00027AC4 File Offset: 0x00025CC4
		int IJsonLineInfo.LinePosition
		{
			get
			{
				JToken.LineInfoAnnotation lineInfoAnnotation = this.Annotation<JToken.LineInfoAnnotation>();
				if (lineInfoAnnotation != null)
				{
					return lineInfoAnnotation.LinePosition;
				}
				return 0;
			}
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x00027AE3 File Offset: 0x00025CE3
		public JToken SelectToken(string path)
		{
			return this.SelectToken(path, false);
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x00027AF0 File Offset: 0x00025CF0
		public JToken SelectToken(string path, bool errorWhenNoMatch)
		{
			JPath jpath = new JPath(path);
			JToken jtoken = null;
			foreach (JToken jtoken2 in jpath.Evaluate(this, this, errorWhenNoMatch))
			{
				if (jtoken != null)
				{
					throw new JsonException("Path returned multiple tokens.");
				}
				jtoken = jtoken2;
			}
			return jtoken;
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x00027B50 File Offset: 0x00025D50
		public IEnumerable<JToken> SelectTokens(string path)
		{
			return this.SelectTokens(path, false);
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x00027B5A File Offset: 0x00025D5A
		public IEnumerable<JToken> SelectTokens(string path, bool errorWhenNoMatch)
		{
			return new JPath(path).Evaluate(this, this, errorWhenNoMatch);
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x00027B6A File Offset: 0x00025D6A
		protected virtual DynamicMetaObject GetMetaObject(Expression parameter)
		{
			return new DynamicProxyMetaObject<JToken>(parameter, this, new DynamicProxy<JToken>());
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x00027B78 File Offset: 0x00025D78
		DynamicMetaObject IDynamicMetaObjectProvider.GetMetaObject(Expression parameter)
		{
			return this.GetMetaObject(parameter);
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x00027B81 File Offset: 0x00025D81
		object ICloneable.Clone()
		{
			return this.DeepClone();
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x00027B89 File Offset: 0x00025D89
		public JToken DeepClone()
		{
			return this.CloneToken();
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x00027B94 File Offset: 0x00025D94
		public void AddAnnotation(object annotation)
		{
			if (annotation == null)
			{
				throw new ArgumentNullException("annotation");
			}
			if (this._annotations == null)
			{
				object obj;
				if (!(annotation is object[]))
				{
					obj = annotation;
				}
				else
				{
					(obj = new object[1])[0] = annotation;
				}
				this._annotations = obj;
				return;
			}
			object[] array = this._annotations as object[];
			if (array == null)
			{
				this._annotations = new object[] { this._annotations, annotation };
				return;
			}
			int num = 0;
			while (num < array.Length && array[num] != null)
			{
				num++;
			}
			if (num == array.Length)
			{
				Array.Resize<object>(ref array, num * 2);
				this._annotations = array;
			}
			array[num] = annotation;
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x00027C2C File Offset: 0x00025E2C
		public T Annotation<T>() where T : class
		{
			if (this._annotations != null)
			{
				object[] array = this._annotations as object[];
				if (array == null)
				{
					return this._annotations as T;
				}
				foreach (object obj in array)
				{
					if (obj == null)
					{
						break;
					}
					T t = obj as T;
					if (t != null)
					{
						return t;
					}
				}
			}
			return default(T);
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x00027C98 File Offset: 0x00025E98
		public object Annotation(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (this._annotations != null)
			{
				object[] array = this._annotations as object[];
				if (array == null)
				{
					if (type.IsInstanceOfType(this._annotations))
					{
						return this._annotations;
					}
				}
				else
				{
					foreach (object obj in array)
					{
						if (obj == null)
						{
							break;
						}
						if (type.IsInstanceOfType(obj))
						{
							return obj;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x00027D06 File Offset: 0x00025F06
		public IEnumerable<T> Annotations<T>() where T : class
		{
			if (this._annotations == null)
			{
				yield break;
			}
			object[] annotations = this._annotations as object[];
			if (annotations != null)
			{
				int num;
				for (int i = 0; i < annotations.Length; i = num + 1)
				{
					object obj = annotations[i];
					if (obj == null)
					{
						break;
					}
					T t = obj as T;
					if (t != null)
					{
						yield return t;
					}
					num = i;
				}
				yield break;
			}
			T t2 = this._annotations as T;
			if (t2 == null)
			{
				yield break;
			}
			yield return t2;
			yield break;
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x00027D16 File Offset: 0x00025F16
		public IEnumerable<object> Annotations(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (this._annotations == null)
			{
				yield break;
			}
			object[] annotations = this._annotations as object[];
			if (annotations != null)
			{
				int num;
				for (int i = 0; i < annotations.Length; i = num + 1)
				{
					object obj = annotations[i];
					if (obj == null)
					{
						break;
					}
					if (type.IsInstanceOfType(obj))
					{
						yield return obj;
					}
					num = i;
				}
				yield break;
			}
			if (!type.IsInstanceOfType(this._annotations))
			{
				yield break;
			}
			yield return this._annotations;
			yield break;
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x00027D30 File Offset: 0x00025F30
		public void RemoveAnnotations<T>() where T : class
		{
			if (this._annotations != null)
			{
				object[] array = this._annotations as object[];
				if (array == null)
				{
					if (this._annotations is T)
					{
						this._annotations = null;
						return;
					}
				}
				else
				{
					int i = 0;
					int j = 0;
					while (i < array.Length)
					{
						object obj = array[i];
						if (obj == null)
						{
							break;
						}
						if (!(obj is T))
						{
							array[j++] = obj;
						}
						i++;
					}
					if (j != 0)
					{
						while (j < i)
						{
							array[j++] = null;
						}
						return;
					}
					this._annotations = null;
				}
			}
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x00027DAC File Offset: 0x00025FAC
		public void RemoveAnnotations(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (this._annotations != null)
			{
				object[] array = this._annotations as object[];
				if (array == null)
				{
					if (type.IsInstanceOfType(this._annotations))
					{
						this._annotations = null;
						return;
					}
				}
				else
				{
					int i = 0;
					int j = 0;
					while (i < array.Length)
					{
						object obj = array[i];
						if (obj == null)
						{
							break;
						}
						if (!type.IsInstanceOfType(obj))
						{
							array[j++] = obj;
						}
						i++;
					}
					if (j != 0)
					{
						while (j < i)
						{
							array[j++] = null;
						}
						return;
					}
					this._annotations = null;
				}
			}
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x00027E3C File Offset: 0x0002603C
		// Note: this type is marked as 'beforefieldinit'.
		static JToken()
		{
		}

		// Token: 0x04000351 RID: 849
		private static JTokenEqualityComparer _equalityComparer;

		// Token: 0x04000352 RID: 850
		private JContainer _parent;

		// Token: 0x04000353 RID: 851
		private JToken _previous;

		// Token: 0x04000354 RID: 852
		private JToken _next;

		// Token: 0x04000355 RID: 853
		private object _annotations;

		// Token: 0x04000356 RID: 854
		private static readonly JTokenType[] BooleanTypes = new JTokenType[]
		{
			JTokenType.Integer,
			JTokenType.Float,
			JTokenType.String,
			JTokenType.Comment,
			JTokenType.Raw,
			JTokenType.Boolean
		};

		// Token: 0x04000357 RID: 855
		private static readonly JTokenType[] NumberTypes = new JTokenType[]
		{
			JTokenType.Integer,
			JTokenType.Float,
			JTokenType.String,
			JTokenType.Comment,
			JTokenType.Raw,
			JTokenType.Boolean
		};

		// Token: 0x04000358 RID: 856
		private static readonly JTokenType[] BigIntegerTypes = new JTokenType[]
		{
			JTokenType.Integer,
			JTokenType.Float,
			JTokenType.String,
			JTokenType.Comment,
			JTokenType.Raw,
			JTokenType.Boolean,
			JTokenType.Bytes
		};

		// Token: 0x04000359 RID: 857
		private static readonly JTokenType[] StringTypes = new JTokenType[]
		{
			JTokenType.Date,
			JTokenType.Integer,
			JTokenType.Float,
			JTokenType.String,
			JTokenType.Comment,
			JTokenType.Raw,
			JTokenType.Boolean,
			JTokenType.Bytes,
			JTokenType.Guid,
			JTokenType.TimeSpan,
			JTokenType.Uri
		};

		// Token: 0x0400035A RID: 858
		private static readonly JTokenType[] GuidTypes = new JTokenType[]
		{
			JTokenType.String,
			JTokenType.Comment,
			JTokenType.Raw,
			JTokenType.Guid,
			JTokenType.Bytes
		};

		// Token: 0x0400035B RID: 859
		private static readonly JTokenType[] TimeSpanTypes = new JTokenType[]
		{
			JTokenType.String,
			JTokenType.Comment,
			JTokenType.Raw,
			JTokenType.TimeSpan
		};

		// Token: 0x0400035C RID: 860
		private static readonly JTokenType[] UriTypes = new JTokenType[]
		{
			JTokenType.String,
			JTokenType.Comment,
			JTokenType.Raw,
			JTokenType.Uri
		};

		// Token: 0x0400035D RID: 861
		private static readonly JTokenType[] CharTypes = new JTokenType[]
		{
			JTokenType.Integer,
			JTokenType.Float,
			JTokenType.String,
			JTokenType.Comment,
			JTokenType.Raw
		};

		// Token: 0x0400035E RID: 862
		private static readonly JTokenType[] DateTimeTypes = new JTokenType[]
		{
			JTokenType.Date,
			JTokenType.String,
			JTokenType.Comment,
			JTokenType.Raw
		};

		// Token: 0x0400035F RID: 863
		private static readonly JTokenType[] BytesTypes = new JTokenType[]
		{
			JTokenType.Bytes,
			JTokenType.String,
			JTokenType.Comment,
			JTokenType.Raw,
			JTokenType.Integer
		};

		// Token: 0x0200015D RID: 349
		private class LineInfoAnnotation
		{
			// Token: 0x06000D52 RID: 3410 RVA: 0x00032131 File Offset: 0x00030331
			public LineInfoAnnotation(int lineNumber, int linePosition)
			{
				this.LineNumber = lineNumber;
				this.LinePosition = linePosition;
			}

			// Token: 0x04000501 RID: 1281
			internal readonly int LineNumber;

			// Token: 0x04000502 RID: 1282
			internal readonly int LinePosition;
		}

		// Token: 0x0200015E RID: 350
		[CompilerGenerated]
		private sealed class <GetAncestors>d__42 : IEnumerable<JToken>, IEnumerable, IEnumerator<JToken>, IDisposable, IEnumerator
		{
			// Token: 0x06000D53 RID: 3411 RVA: 0x00032147 File Offset: 0x00030347
			[DebuggerHidden]
			public <GetAncestors>d__42(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
			}

			// Token: 0x06000D54 RID: 3412 RVA: 0x00002C0D File Offset: 0x00000E0D
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000D55 RID: 3413 RVA: 0x00032168 File Offset: 0x00030368
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					current = current.Parent;
				}
				else
				{
					this.<>1__state = -1;
					current = (self ? this : this.Parent);
				}
				if (current == null)
				{
					current = null;
					return false;
				}
				this.<>2__current = current;
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x17000295 RID: 661
			// (get) Token: 0x06000D56 RID: 3414 RVA: 0x000321EC File Offset: 0x000303EC
			JToken IEnumerator<JToken>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000D57 RID: 3415 RVA: 0x00024289 File Offset: 0x00022489
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000296 RID: 662
			// (get) Token: 0x06000D58 RID: 3416 RVA: 0x000321EC File Offset: 0x000303EC
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000D59 RID: 3417 RVA: 0x000321F4 File Offset: 0x000303F4
			[DebuggerHidden]
			IEnumerator<JToken> IEnumerable<JToken>.GetEnumerator()
			{
				JToken.<GetAncestors>d__42 <GetAncestors>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.<>1__state = 0;
					<GetAncestors>d__ = this;
				}
				else
				{
					<GetAncestors>d__ = new JToken.<GetAncestors>d__42(0);
					<GetAncestors>d__.<>4__this = this;
				}
				<GetAncestors>d__.self = self;
				return <GetAncestors>d__;
			}

			// Token: 0x06000D5A RID: 3418 RVA: 0x00032248 File Offset: 0x00030448
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<Newtonsoft.Json.Linq.JToken>.GetEnumerator();
			}

			// Token: 0x04000503 RID: 1283
			private int <>1__state;

			// Token: 0x04000504 RID: 1284
			private JToken <>2__current;

			// Token: 0x04000505 RID: 1285
			private int <>l__initialThreadId;

			// Token: 0x04000506 RID: 1286
			private bool self;

			// Token: 0x04000507 RID: 1287
			public bool <>3__self;

			// Token: 0x04000508 RID: 1288
			public JToken <>4__this;

			// Token: 0x04000509 RID: 1289
			private JToken <current>5__1;
		}

		// Token: 0x0200015F RID: 351
		[CompilerGenerated]
		private sealed class <AfterSelf>d__43 : IEnumerable<JToken>, IEnumerable, IEnumerator<JToken>, IDisposable, IEnumerator
		{
			// Token: 0x06000D5B RID: 3419 RVA: 0x00032250 File Offset: 0x00030450
			[DebuggerHidden]
			public <AfterSelf>d__43(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
			}

			// Token: 0x06000D5C RID: 3420 RVA: 0x00002C0D File Offset: 0x00000E0D
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000D5D RID: 3421 RVA: 0x00032270 File Offset: 0x00030470
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					o = o.Next;
				}
				else
				{
					this.<>1__state = -1;
					if (this.Parent == null)
					{
						return false;
					}
					o = this.Next;
				}
				if (o == null)
				{
					o = null;
					return false;
				}
				this.<>2__current = o;
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x17000297 RID: 663
			// (get) Token: 0x06000D5E RID: 3422 RVA: 0x000322F3 File Offset: 0x000304F3
			JToken IEnumerator<JToken>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000D5F RID: 3423 RVA: 0x00024289 File Offset: 0x00022489
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000298 RID: 664
			// (get) Token: 0x06000D60 RID: 3424 RVA: 0x000322F3 File Offset: 0x000304F3
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000D61 RID: 3425 RVA: 0x000322FC File Offset: 0x000304FC
			[DebuggerHidden]
			IEnumerator<JToken> IEnumerable<JToken>.GetEnumerator()
			{
				JToken.<AfterSelf>d__43 <AfterSelf>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.<>1__state = 0;
					<AfterSelf>d__ = this;
				}
				else
				{
					<AfterSelf>d__ = new JToken.<AfterSelf>d__43(0);
					<AfterSelf>d__.<>4__this = this;
				}
				return <AfterSelf>d__;
			}

			// Token: 0x06000D62 RID: 3426 RVA: 0x00032344 File Offset: 0x00030544
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<Newtonsoft.Json.Linq.JToken>.GetEnumerator();
			}

			// Token: 0x0400050A RID: 1290
			private int <>1__state;

			// Token: 0x0400050B RID: 1291
			private JToken <>2__current;

			// Token: 0x0400050C RID: 1292
			private int <>l__initialThreadId;

			// Token: 0x0400050D RID: 1293
			public JToken <>4__this;

			// Token: 0x0400050E RID: 1294
			private JToken <o>5__1;
		}

		// Token: 0x02000160 RID: 352
		[CompilerGenerated]
		private sealed class <BeforeSelf>d__44 : IEnumerable<JToken>, IEnumerable, IEnumerator<JToken>, IDisposable, IEnumerator
		{
			// Token: 0x06000D63 RID: 3427 RVA: 0x0003234C File Offset: 0x0003054C
			[DebuggerHidden]
			public <BeforeSelf>d__44(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
			}

			// Token: 0x06000D64 RID: 3428 RVA: 0x00002C0D File Offset: 0x00000E0D
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000D65 RID: 3429 RVA: 0x0003236C File Offset: 0x0003056C
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					o = o.Next;
				}
				else
				{
					this.<>1__state = -1;
					o = this.Parent.First;
				}
				if (o == this)
				{
					o = null;
					return false;
				}
				this.<>2__current = o;
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x17000299 RID: 665
			// (get) Token: 0x06000D66 RID: 3430 RVA: 0x000323EB File Offset: 0x000305EB
			JToken IEnumerator<JToken>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000D67 RID: 3431 RVA: 0x00024289 File Offset: 0x00022489
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700029A RID: 666
			// (get) Token: 0x06000D68 RID: 3432 RVA: 0x000323EB File Offset: 0x000305EB
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000D69 RID: 3433 RVA: 0x000323F4 File Offset: 0x000305F4
			[DebuggerHidden]
			IEnumerator<JToken> IEnumerable<JToken>.GetEnumerator()
			{
				JToken.<BeforeSelf>d__44 <BeforeSelf>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.<>1__state = 0;
					<BeforeSelf>d__ = this;
				}
				else
				{
					<BeforeSelf>d__ = new JToken.<BeforeSelf>d__44(0);
					<BeforeSelf>d__.<>4__this = this;
				}
				return <BeforeSelf>d__;
			}

			// Token: 0x06000D6A RID: 3434 RVA: 0x0003243C File Offset: 0x0003063C
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<Newtonsoft.Json.Linq.JToken>.GetEnumerator();
			}

			// Token: 0x0400050F RID: 1295
			private int <>1__state;

			// Token: 0x04000510 RID: 1296
			private JToken <>2__current;

			// Token: 0x04000511 RID: 1297
			private int <>l__initialThreadId;

			// Token: 0x04000512 RID: 1298
			public JToken <>4__this;

			// Token: 0x04000513 RID: 1299
			private JToken <o>5__1;
		}

		// Token: 0x02000161 RID: 353
		[CompilerGenerated]
		private sealed class <Annotations>d__176<T> : IEnumerable<T>, IEnumerable, IEnumerator<T>, IDisposable, IEnumerator where T : class
		{
			// Token: 0x06000D6B RID: 3435 RVA: 0x00032444 File Offset: 0x00030644
			[DebuggerHidden]
			public <Annotations>d__176(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
			}

			// Token: 0x06000D6C RID: 3436 RVA: 0x00002C0D File Offset: 0x00000E0D
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000D6D RID: 3437 RVA: 0x00032464 File Offset: 0x00030664
			bool IEnumerator.MoveNext()
			{
				switch (this.<>1__state)
				{
				case 0:
				{
					this.<>1__state = -1;
					if (this._annotations == null)
					{
						return false;
					}
					annotations = this._annotations as object[];
					if (annotations != null)
					{
						i = 0;
						goto IL_00A6;
					}
					T t = this._annotations as T;
					if (t == null)
					{
						return false;
					}
					this.<>2__current = t;
					this.<>1__state = 2;
					return true;
				}
				case 1:
					this.<>1__state = -1;
					break;
				case 2:
					this.<>1__state = -1;
					return false;
				default:
					return false;
				}
				IL_0094:
				int num = i;
				i = num + 1;
				IL_00A6:
				if (i < annotations.Length)
				{
					object obj = annotations[i];
					if (obj != null)
					{
						T t2 = obj as T;
						if (t2 != null)
						{
							this.<>2__current = t2;
							this.<>1__state = 1;
							return true;
						}
						goto IL_0094;
					}
				}
				return false;
			}

			// Token: 0x1700029B RID: 667
			// (get) Token: 0x06000D6E RID: 3438 RVA: 0x00032561 File Offset: 0x00030761
			T IEnumerator<T>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000D6F RID: 3439 RVA: 0x00024289 File Offset: 0x00022489
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700029C RID: 668
			// (get) Token: 0x06000D70 RID: 3440 RVA: 0x00032569 File Offset: 0x00030769
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000D71 RID: 3441 RVA: 0x00032578 File Offset: 0x00030778
			[DebuggerHidden]
			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				JToken.<Annotations>d__176<T> <Annotations>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.<>1__state = 0;
					<Annotations>d__ = this;
				}
				else
				{
					<Annotations>d__ = new JToken.<Annotations>d__176<T>(0);
					<Annotations>d__.<>4__this = this;
				}
				return <Annotations>d__;
			}

			// Token: 0x06000D72 RID: 3442 RVA: 0x000325C0 File Offset: 0x000307C0
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<T>.GetEnumerator();
			}

			// Token: 0x04000514 RID: 1300
			private int <>1__state;

			// Token: 0x04000515 RID: 1301
			private T <>2__current;

			// Token: 0x04000516 RID: 1302
			private int <>l__initialThreadId;

			// Token: 0x04000517 RID: 1303
			public JToken <>4__this;

			// Token: 0x04000518 RID: 1304
			private object[] <annotations>5__1;

			// Token: 0x04000519 RID: 1305
			private int <i>5__2;
		}

		// Token: 0x02000162 RID: 354
		[CompilerGenerated]
		private sealed class <Annotations>d__177 : IEnumerable<object>, IEnumerable, IEnumerator<object>, IDisposable, IEnumerator
		{
			// Token: 0x06000D73 RID: 3443 RVA: 0x000325C8 File Offset: 0x000307C8
			[DebuggerHidden]
			public <Annotations>d__177(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
			}

			// Token: 0x06000D74 RID: 3444 RVA: 0x00002C0D File Offset: 0x00000E0D
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000D75 RID: 3445 RVA: 0x000325E8 File Offset: 0x000307E8
			bool IEnumerator.MoveNext()
			{
				switch (this.<>1__state)
				{
				case 0:
					this.<>1__state = -1;
					if (type == null)
					{
						throw new ArgumentNullException("type");
					}
					if (this._annotations == null)
					{
						return false;
					}
					annotations = this._annotations as object[];
					if (annotations != null)
					{
						i = 0;
						goto IL_00B7;
					}
					if (!type.IsInstanceOfType(this._annotations))
					{
						return false;
					}
					this.<>2__current = this._annotations;
					this.<>1__state = 2;
					return true;
				case 1:
					this.<>1__state = -1;
					break;
				case 2:
					this.<>1__state = -1;
					return false;
				default:
					return false;
				}
				IL_00A7:
				int num = i;
				i = num + 1;
				IL_00B7:
				if (i < annotations.Length)
				{
					object obj = annotations[i];
					if (obj != null)
					{
						if (type.IsInstanceOfType(obj))
						{
							this.<>2__current = obj;
							this.<>1__state = 1;
							return true;
						}
						goto IL_00A7;
					}
				}
				return false;
			}

			// Token: 0x1700029D RID: 669
			// (get) Token: 0x06000D76 RID: 3446 RVA: 0x000326FA File Offset: 0x000308FA
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000D77 RID: 3447 RVA: 0x00024289 File Offset: 0x00022489
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700029E RID: 670
			// (get) Token: 0x06000D78 RID: 3448 RVA: 0x000326FA File Offset: 0x000308FA
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000D79 RID: 3449 RVA: 0x00032704 File Offset: 0x00030904
			[DebuggerHidden]
			IEnumerator<object> IEnumerable<object>.GetEnumerator()
			{
				JToken.<Annotations>d__177 <Annotations>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.<>1__state = 0;
					<Annotations>d__ = this;
				}
				else
				{
					<Annotations>d__ = new JToken.<Annotations>d__177(0);
					<Annotations>d__.<>4__this = this;
				}
				<Annotations>d__.type = type;
				return <Annotations>d__;
			}

			// Token: 0x06000D7A RID: 3450 RVA: 0x00032758 File Offset: 0x00030958
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Object>.GetEnumerator();
			}

			// Token: 0x0400051A RID: 1306
			private int <>1__state;

			// Token: 0x0400051B RID: 1307
			private object <>2__current;

			// Token: 0x0400051C RID: 1308
			private int <>l__initialThreadId;

			// Token: 0x0400051D RID: 1309
			private Type type;

			// Token: 0x0400051E RID: 1310
			public Type <>3__type;

			// Token: 0x0400051F RID: 1311
			public JToken <>4__this;

			// Token: 0x04000520 RID: 1312
			private object[] <annotations>5__1;

			// Token: 0x04000521 RID: 1313
			private int <i>5__2;
		}
	}
}
