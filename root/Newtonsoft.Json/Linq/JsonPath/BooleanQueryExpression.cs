using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x020000C9 RID: 201
	internal class BooleanQueryExpression : QueryExpression
	{
		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x0002A548 File Offset: 0x00028748
		// (set) Token: 0x06000A84 RID: 2692 RVA: 0x0002A550 File Offset: 0x00028750
		public object Left
		{
			[CompilerGenerated]
			get
			{
				return this.<Left>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Left>k__BackingField = value;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000A85 RID: 2693 RVA: 0x0002A559 File Offset: 0x00028759
		// (set) Token: 0x06000A86 RID: 2694 RVA: 0x0002A561 File Offset: 0x00028761
		public object Right
		{
			[CompilerGenerated]
			get
			{
				return this.<Right>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Right>k__BackingField = value;
			}
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x0002A56C File Offset: 0x0002876C
		private IEnumerable<JToken> GetResult(JToken root, JToken t, object o)
		{
			JToken jtoken = o as JToken;
			if (jtoken != null)
			{
				return new JToken[] { jtoken };
			}
			List<PathFilter> list = o as List<PathFilter>;
			if (list != null)
			{
				return JPath.Evaluate(list, root, t, false);
			}
			return CollectionUtils.ArrayEmpty<JToken>();
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x0002A5A8 File Offset: 0x000287A8
		public override bool IsMatch(JToken root, JToken t)
		{
			if (base.Operator == QueryOperator.Exists)
			{
				return Enumerable.Any<JToken>(this.GetResult(root, t, this.Left));
			}
			using (IEnumerator<JToken> enumerator = this.GetResult(root, t, this.Left).GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					IEnumerable<JToken> result = this.GetResult(root, t, this.Right);
					ICollection<JToken> collection = (result as ICollection<JToken>) ?? Enumerable.ToList<JToken>(result);
					do
					{
						JToken jtoken = enumerator.Current;
						foreach (JToken jtoken2 in collection)
						{
							if (this.MatchTokens(jtoken, jtoken2))
							{
								return true;
							}
						}
					}
					while (enumerator.MoveNext());
				}
			}
			return false;
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x0002A684 File Offset: 0x00028884
		private bool MatchTokens(JToken leftResult, JToken rightResult)
		{
			JValue jvalue = leftResult as JValue;
			JValue jvalue2 = rightResult as JValue;
			if (jvalue != null && jvalue2 != null)
			{
				switch (base.Operator)
				{
				case QueryOperator.Equals:
					if (this.EqualsWithStringCoercion(jvalue, jvalue2))
					{
						return true;
					}
					break;
				case QueryOperator.NotEquals:
					if (!this.EqualsWithStringCoercion(jvalue, jvalue2))
					{
						return true;
					}
					break;
				case QueryOperator.Exists:
					return true;
				case QueryOperator.LessThan:
					if (jvalue.CompareTo(jvalue2) < 0)
					{
						return true;
					}
					break;
				case QueryOperator.LessThanOrEquals:
					if (jvalue.CompareTo(jvalue2) <= 0)
					{
						return true;
					}
					break;
				case QueryOperator.GreaterThan:
					if (jvalue.CompareTo(jvalue2) > 0)
					{
						return true;
					}
					break;
				case QueryOperator.GreaterThanOrEquals:
					if (jvalue.CompareTo(jvalue2) >= 0)
					{
						return true;
					}
					break;
				}
			}
			else
			{
				QueryOperator @operator = base.Operator;
				if (@operator == QueryOperator.NotEquals || @operator == QueryOperator.Exists)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x0002A730 File Offset: 0x00028930
		private bool EqualsWithStringCoercion(JValue value, JValue queryValue)
		{
			if (value.Equals(queryValue))
			{
				return true;
			}
			if (queryValue.Type != JTokenType.String)
			{
				return false;
			}
			string text = (string)queryValue.Value;
			string text2;
			switch (value.Type)
			{
			case JTokenType.Date:
			{
				using (StringWriter stringWriter = StringUtils.CreateStringWriter(64))
				{
					if (value.Value is DateTimeOffset)
					{
						DateTimeUtils.WriteDateTimeOffsetString(stringWriter, (DateTimeOffset)value.Value, DateFormatHandling.IsoDateFormat, null, CultureInfo.InvariantCulture);
					}
					else
					{
						DateTimeUtils.WriteDateTimeString(stringWriter, (DateTime)value.Value, DateFormatHandling.IsoDateFormat, null, CultureInfo.InvariantCulture);
					}
					text2 = stringWriter.ToString();
					goto IL_00DF;
				}
				break;
			}
			case JTokenType.Raw:
				return false;
			case JTokenType.Bytes:
				break;
			case JTokenType.Guid:
			case JTokenType.TimeSpan:
				text2 = value.Value.ToString();
				goto IL_00DF;
			case JTokenType.Uri:
				text2 = ((Uri)value.Value).OriginalString;
				goto IL_00DF;
			default:
				return false;
			}
			text2 = Convert.ToBase64String((byte[])value.Value);
			IL_00DF:
			return string.Equals(text2, text, 4);
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0002A834 File Offset: 0x00028A34
		public BooleanQueryExpression()
		{
		}

		// Token: 0x0400038E RID: 910
		[CompilerGenerated]
		private object <Left>k__BackingField;

		// Token: 0x0400038F RID: 911
		[CompilerGenerated]
		private object <Right>k__BackingField;
	}
}
