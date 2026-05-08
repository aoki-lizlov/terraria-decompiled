using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x020000C4 RID: 196
	internal class JPath
	{
		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000A63 RID: 2659 RVA: 0x00029354 File Offset: 0x00027554
		public List<PathFilter> Filters
		{
			[CompilerGenerated]
			get
			{
				return this.<Filters>k__BackingField;
			}
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x0002935C File Offset: 0x0002755C
		public JPath(string expression)
		{
			ValidationUtils.ArgumentNotNull(expression, "expression");
			this._expression = expression;
			this.Filters = new List<PathFilter>();
			this.ParseMain();
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x00029388 File Offset: 0x00027588
		private void ParseMain()
		{
			int num = this._currentIndex;
			this.EatWhitespace();
			if (this._expression.Length == this._currentIndex)
			{
				return;
			}
			if (this._expression.get_Chars(this._currentIndex) == '$')
			{
				if (this._expression.Length == 1)
				{
					return;
				}
				char c = this._expression.get_Chars(this._currentIndex + 1);
				if (c == '.' || c == '[')
				{
					this._currentIndex++;
					num = this._currentIndex;
				}
			}
			if (!this.ParsePath(this.Filters, num, false))
			{
				int currentIndex = this._currentIndex;
				this.EatWhitespace();
				if (this._currentIndex < this._expression.Length)
				{
					throw new JsonException("Unexpected character while parsing path: " + this._expression.get_Chars(currentIndex).ToString());
				}
			}
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x00029464 File Offset: 0x00027664
		private bool ParsePath(List<PathFilter> filters, int currentPartStartIndex, bool query)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			while (this._currentIndex < this._expression.Length && !flag4)
			{
				char c = this._expression.get_Chars(this._currentIndex);
				if (c <= ')')
				{
					if (c != ' ')
					{
						if (c != '(')
						{
							if (c != ')')
							{
								goto IL_0187;
							}
							goto IL_00CB;
						}
					}
					else
					{
						if (this._currentIndex < this._expression.Length)
						{
							flag4 = true;
							continue;
						}
						continue;
					}
				}
				else
				{
					if (c == '.')
					{
						if (this._currentIndex > currentPartStartIndex)
						{
							string text = this._expression.Substring(currentPartStartIndex, this._currentIndex - currentPartStartIndex);
							if (text == "*")
							{
								text = null;
							}
							filters.Add(JPath.CreatePathFilter(text, flag));
							flag = false;
						}
						if (this._currentIndex + 1 < this._expression.Length && this._expression.get_Chars(this._currentIndex + 1) == '.')
						{
							flag = true;
							this._currentIndex++;
						}
						this._currentIndex++;
						currentPartStartIndex = this._currentIndex;
						flag2 = false;
						flag3 = true;
						continue;
					}
					if (c != '[')
					{
						if (c != ']')
						{
							goto IL_0187;
						}
						goto IL_00CB;
					}
				}
				if (this._currentIndex > currentPartStartIndex)
				{
					string text2 = this._expression.Substring(currentPartStartIndex, this._currentIndex - currentPartStartIndex);
					if (text2 == "*")
					{
						text2 = null;
					}
					filters.Add(JPath.CreatePathFilter(text2, flag));
					flag = false;
				}
				filters.Add(this.ParseIndexer(c, flag));
				this._currentIndex++;
				currentPartStartIndex = this._currentIndex;
				flag2 = true;
				flag3 = false;
				continue;
				IL_00CB:
				flag4 = true;
				continue;
				IL_0187:
				if (query && (c == '=' || c == '<' || c == '!' || c == '>' || c == '|' || c == '&'))
				{
					flag4 = true;
				}
				else
				{
					if (flag2)
					{
						throw new JsonException("Unexpected character following indexer: " + c.ToString());
					}
					this._currentIndex++;
				}
			}
			bool flag5 = this._currentIndex == this._expression.Length;
			if (this._currentIndex > currentPartStartIndex)
			{
				string text3 = this._expression.Substring(currentPartStartIndex, this._currentIndex - currentPartStartIndex).TrimEnd(new char[0]);
				if (text3 == "*")
				{
					text3 = null;
				}
				filters.Add(JPath.CreatePathFilter(text3, flag));
			}
			else if (flag3 && (flag5 || query))
			{
				throw new JsonException("Unexpected end while parsing path.");
			}
			return flag5;
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x000296DA File Offset: 0x000278DA
		private static PathFilter CreatePathFilter(string member, bool scan)
		{
			if (!scan)
			{
				return new FieldFilter
				{
					Name = member
				};
			}
			return new ScanFilter
			{
				Name = member
			};
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x000296F8 File Offset: 0x000278F8
		private PathFilter ParseIndexer(char indexerOpenChar, bool scan)
		{
			this._currentIndex++;
			char c = ((indexerOpenChar == '[') ? ']' : ')');
			this.EnsureLength("Path ended with open indexer.");
			this.EatWhitespace();
			if (this._expression.get_Chars(this._currentIndex) == '\'')
			{
				return this.ParseQuotedField(c, scan);
			}
			if (this._expression.get_Chars(this._currentIndex) == '?')
			{
				return this.ParseQuery(c);
			}
			return this.ParseArrayIndexer(c);
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x00029774 File Offset: 0x00027974
		private PathFilter ParseArrayIndexer(char indexerCloseChar)
		{
			int num = this._currentIndex;
			int? num2 = default(int?);
			List<int> list = null;
			int num3 = 0;
			int? num4 = default(int?);
			int? num5 = default(int?);
			int? num6 = default(int?);
			while (this._currentIndex < this._expression.Length)
			{
				char c = this._expression.get_Chars(this._currentIndex);
				if (c == ' ')
				{
					num2 = new int?(this._currentIndex);
					this.EatWhitespace();
				}
				else if (c == indexerCloseChar)
				{
					int num7 = (num2 ?? this._currentIndex) - num;
					if (list != null)
					{
						if (num7 == 0)
						{
							throw new JsonException("Array index expected.");
						}
						int num8 = Convert.ToInt32(this._expression.Substring(num, num7), CultureInfo.InvariantCulture);
						list.Add(num8);
						return new ArrayMultipleIndexFilter
						{
							Indexes = list
						};
					}
					else
					{
						if (num3 > 0)
						{
							if (num7 > 0)
							{
								int num9 = Convert.ToInt32(this._expression.Substring(num, num7), CultureInfo.InvariantCulture);
								if (num3 == 1)
								{
									num5 = new int?(num9);
								}
								else
								{
									num6 = new int?(num9);
								}
							}
							return new ArraySliceFilter
							{
								Start = num4,
								End = num5,
								Step = num6
							};
						}
						if (num7 == 0)
						{
							throw new JsonException("Array index expected.");
						}
						int num10 = Convert.ToInt32(this._expression.Substring(num, num7), CultureInfo.InvariantCulture);
						return new ArrayIndexFilter
						{
							Index = new int?(num10)
						};
					}
				}
				else if (c == ',')
				{
					int num11 = (num2 ?? this._currentIndex) - num;
					if (num11 == 0)
					{
						throw new JsonException("Array index expected.");
					}
					if (list == null)
					{
						list = new List<int>();
					}
					string text = this._expression.Substring(num, num11);
					list.Add(Convert.ToInt32(text, CultureInfo.InvariantCulture));
					this._currentIndex++;
					this.EatWhitespace();
					num = this._currentIndex;
					num2 = default(int?);
				}
				else if (c == '*')
				{
					this._currentIndex++;
					this.EnsureLength("Path ended with open indexer.");
					this.EatWhitespace();
					if (this._expression.get_Chars(this._currentIndex) != indexerCloseChar)
					{
						throw new JsonException("Unexpected character while parsing path indexer: " + c.ToString());
					}
					return new ArrayIndexFilter();
				}
				else if (c == ':')
				{
					int num12 = (num2 ?? this._currentIndex) - num;
					if (num12 > 0)
					{
						int num13 = Convert.ToInt32(this._expression.Substring(num, num12), CultureInfo.InvariantCulture);
						if (num3 == 0)
						{
							num4 = new int?(num13);
						}
						else if (num3 == 1)
						{
							num5 = new int?(num13);
						}
						else
						{
							num6 = new int?(num13);
						}
					}
					num3++;
					this._currentIndex++;
					this.EatWhitespace();
					num = this._currentIndex;
					num2 = default(int?);
				}
				else
				{
					if (!char.IsDigit(c) && c != '-')
					{
						throw new JsonException("Unexpected character while parsing path indexer: " + c.ToString());
					}
					if (num2 != null)
					{
						throw new JsonException("Unexpected character while parsing path indexer: " + c.ToString());
					}
					this._currentIndex++;
				}
			}
			throw new JsonException("Path ended with open indexer.");
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x00029AC9 File Offset: 0x00027CC9
		private void EatWhitespace()
		{
			while (this._currentIndex < this._expression.Length && this._expression.get_Chars(this._currentIndex) == ' ')
			{
				this._currentIndex++;
			}
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x00029B04 File Offset: 0x00027D04
		private PathFilter ParseQuery(char indexerCloseChar)
		{
			this._currentIndex++;
			this.EnsureLength("Path ended with open indexer.");
			if (this._expression.get_Chars(this._currentIndex) != '(')
			{
				throw new JsonException("Unexpected character while parsing path indexer: " + this._expression.get_Chars(this._currentIndex).ToString());
			}
			this._currentIndex++;
			QueryExpression queryExpression = this.ParseExpression();
			this._currentIndex++;
			this.EnsureLength("Path ended with open indexer.");
			this.EatWhitespace();
			if (this._expression.get_Chars(this._currentIndex) != indexerCloseChar)
			{
				throw new JsonException("Unexpected character while parsing path indexer: " + this._expression.get_Chars(this._currentIndex).ToString());
			}
			return new QueryFilter
			{
				Expression = queryExpression
			};
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x00029BE8 File Offset: 0x00027DE8
		private bool TryParseExpression(out List<PathFilter> expressionPath)
		{
			if (this._expression.get_Chars(this._currentIndex) == '$')
			{
				expressionPath = new List<PathFilter>();
				expressionPath.Add(RootFilter.Instance);
			}
			else
			{
				if (this._expression.get_Chars(this._currentIndex) != '@')
				{
					expressionPath = null;
					return false;
				}
				expressionPath = new List<PathFilter>();
			}
			this._currentIndex++;
			if (this.ParsePath(expressionPath, this._currentIndex, true))
			{
				throw new JsonException("Path ended with open query.");
			}
			return true;
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x00029C70 File Offset: 0x00027E70
		private JsonException CreateUnexpectedCharacterException()
		{
			return new JsonException("Unexpected character while parsing path query: " + this._expression.get_Chars(this._currentIndex).ToString());
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x00029CA8 File Offset: 0x00027EA8
		private object ParseSide()
		{
			this.EatWhitespace();
			List<PathFilter> list;
			if (this.TryParseExpression(out list))
			{
				this.EatWhitespace();
				this.EnsureLength("Path ended with open query.");
				return list;
			}
			object obj;
			if (this.TryParseValue(out obj))
			{
				this.EatWhitespace();
				this.EnsureLength("Path ended with open query.");
				return new JValue(obj);
			}
			throw this.CreateUnexpectedCharacterException();
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x00029D00 File Offset: 0x00027F00
		private QueryExpression ParseExpression()
		{
			QueryExpression queryExpression = null;
			CompositeExpression compositeExpression = null;
			while (this._currentIndex < this._expression.Length)
			{
				object obj = this.ParseSide();
				object obj2 = null;
				QueryOperator queryOperator;
				if (this._expression.get_Chars(this._currentIndex) == ')' || this._expression.get_Chars(this._currentIndex) == '|' || this._expression.get_Chars(this._currentIndex) == '&')
				{
					queryOperator = QueryOperator.Exists;
				}
				else
				{
					queryOperator = this.ParseOperator();
					obj2 = this.ParseSide();
				}
				BooleanQueryExpression booleanQueryExpression = new BooleanQueryExpression
				{
					Left = obj,
					Operator = queryOperator,
					Right = obj2
				};
				if (this._expression.get_Chars(this._currentIndex) == ')')
				{
					if (compositeExpression != null)
					{
						compositeExpression.Expressions.Add(booleanQueryExpression);
						return queryExpression;
					}
					return booleanQueryExpression;
				}
				else
				{
					if (this._expression.get_Chars(this._currentIndex) == '&')
					{
						if (!this.Match("&&"))
						{
							throw this.CreateUnexpectedCharacterException();
						}
						if (compositeExpression == null || compositeExpression.Operator != QueryOperator.And)
						{
							CompositeExpression compositeExpression2 = new CompositeExpression
							{
								Operator = QueryOperator.And
							};
							if (compositeExpression != null)
							{
								compositeExpression.Expressions.Add(compositeExpression2);
							}
							compositeExpression = compositeExpression2;
							if (queryExpression == null)
							{
								queryExpression = compositeExpression;
							}
						}
						compositeExpression.Expressions.Add(booleanQueryExpression);
					}
					if (this._expression.get_Chars(this._currentIndex) == '|')
					{
						if (!this.Match("||"))
						{
							throw this.CreateUnexpectedCharacterException();
						}
						if (compositeExpression == null || compositeExpression.Operator != QueryOperator.Or)
						{
							CompositeExpression compositeExpression3 = new CompositeExpression
							{
								Operator = QueryOperator.Or
							};
							if (compositeExpression != null)
							{
								compositeExpression.Expressions.Add(compositeExpression3);
							}
							compositeExpression = compositeExpression3;
							if (queryExpression == null)
							{
								queryExpression = compositeExpression;
							}
						}
						compositeExpression.Expressions.Add(booleanQueryExpression);
					}
				}
			}
			throw new JsonException("Path ended with open query.");
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x00029EAC File Offset: 0x000280AC
		private bool TryParseValue(out object value)
		{
			char c = this._expression.get_Chars(this._currentIndex);
			if (c == '\'')
			{
				value = this.ReadQuotedString();
				return true;
			}
			if (char.IsDigit(c) || c == '-')
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(c);
				this._currentIndex++;
				while (this._currentIndex < this._expression.Length)
				{
					c = this._expression.get_Chars(this._currentIndex);
					if (c == ' ' || c == ')')
					{
						string text = stringBuilder.ToString();
						if (text.IndexOfAny(new char[] { '.', 'E', 'e' }) != -1)
						{
							double num;
							bool flag = double.TryParse(text, 231, CultureInfo.InvariantCulture, ref num);
							value = num;
							return flag;
						}
						long num2;
						bool flag2 = long.TryParse(text, 7, CultureInfo.InvariantCulture, ref num2);
						value = num2;
						return flag2;
					}
					else
					{
						stringBuilder.Append(c);
						this._currentIndex++;
					}
				}
			}
			else if (c == 't')
			{
				if (this.Match("true"))
				{
					value = true;
					return true;
				}
			}
			else if (c == 'f')
			{
				if (this.Match("false"))
				{
					value = false;
					return true;
				}
			}
			else if (c == 'n' && this.Match("null"))
			{
				value = null;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x00029FFC File Offset: 0x000281FC
		private string ReadQuotedString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this._currentIndex++;
			while (this._currentIndex < this._expression.Length)
			{
				char c = this._expression.get_Chars(this._currentIndex);
				if (c == '\\' && this._currentIndex + 1 < this._expression.Length)
				{
					this._currentIndex++;
					if (this._expression.get_Chars(this._currentIndex) == '\'')
					{
						stringBuilder.Append('\'');
					}
					else
					{
						if (this._expression.get_Chars(this._currentIndex) != '\\')
						{
							throw new JsonException("Unknown escape character: \\" + this._expression.get_Chars(this._currentIndex).ToString());
						}
						stringBuilder.Append('\\');
					}
					this._currentIndex++;
				}
				else
				{
					if (c == '\'')
					{
						this._currentIndex++;
						return stringBuilder.ToString();
					}
					this._currentIndex++;
					stringBuilder.Append(c);
				}
			}
			throw new JsonException("Path ended with an open string.");
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x0002A12C File Offset: 0x0002832C
		private bool Match(string s)
		{
			int num = this._currentIndex;
			for (int i = 0; i < s.Length; i++)
			{
				char c = s.get_Chars(i);
				if (num >= this._expression.Length || this._expression.get_Chars(num) != c)
				{
					return false;
				}
				num++;
			}
			this._currentIndex = num;
			return true;
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x0002A188 File Offset: 0x00028388
		private QueryOperator ParseOperator()
		{
			if (this._currentIndex + 1 >= this._expression.Length)
			{
				throw new JsonException("Path ended with open query.");
			}
			if (this.Match("=="))
			{
				return QueryOperator.Equals;
			}
			if (this.Match("!=") || this.Match("<>"))
			{
				return QueryOperator.NotEquals;
			}
			if (this.Match("<="))
			{
				return QueryOperator.LessThanOrEquals;
			}
			if (this.Match("<"))
			{
				return QueryOperator.LessThan;
			}
			if (this.Match(">="))
			{
				return QueryOperator.GreaterThanOrEquals;
			}
			if (this.Match(">"))
			{
				return QueryOperator.GreaterThan;
			}
			throw new JsonException("Could not read query operator.");
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x0002A228 File Offset: 0x00028428
		private PathFilter ParseQuotedField(char indexerCloseChar, bool scan)
		{
			List<string> list = null;
			while (this._currentIndex < this._expression.Length)
			{
				string text = this.ReadQuotedString();
				this.EatWhitespace();
				this.EnsureLength("Path ended with open indexer.");
				if (this._expression.get_Chars(this._currentIndex) == indexerCloseChar)
				{
					if (list == null)
					{
						return JPath.CreatePathFilter(text, scan);
					}
					list.Add(text);
					if (!scan)
					{
						return new FieldMultipleFilter
						{
							Names = list
						};
					}
					return new ScanMultipleFilter
					{
						Names = list
					};
				}
				else
				{
					if (this._expression.get_Chars(this._currentIndex) != ',')
					{
						throw new JsonException("Unexpected character while parsing path indexer: " + this._expression.get_Chars(this._currentIndex).ToString());
					}
					this._currentIndex++;
					this.EatWhitespace();
					if (list == null)
					{
						list = new List<string>();
					}
					list.Add(text);
				}
			}
			throw new JsonException("Path ended with open indexer.");
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x0002A31B File Offset: 0x0002851B
		private void EnsureLength(string message)
		{
			if (this._currentIndex >= this._expression.Length)
			{
				throw new JsonException(message);
			}
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x0002A337 File Offset: 0x00028537
		internal IEnumerable<JToken> Evaluate(JToken root, JToken t, bool errorWhenNoMatch)
		{
			return JPath.Evaluate(this.Filters, root, t, errorWhenNoMatch);
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x0002A348 File Offset: 0x00028548
		internal static IEnumerable<JToken> Evaluate(List<PathFilter> filters, JToken root, JToken t, bool errorWhenNoMatch)
		{
			IEnumerable<JToken> enumerable = new JToken[] { t };
			foreach (PathFilter pathFilter in filters)
			{
				enumerable = pathFilter.ExecuteFilter(root, enumerable, errorWhenNoMatch);
			}
			return enumerable;
		}

		// Token: 0x0400037E RID: 894
		private readonly string _expression;

		// Token: 0x0400037F RID: 895
		[CompilerGenerated]
		private readonly List<PathFilter> <Filters>k__BackingField;

		// Token: 0x04000380 RID: 896
		private int _currentIndex;
	}
}
