using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Terraria.Localization
{
	// Token: 0x02000184 RID: 388
	internal class VariableText
	{
		// Token: 0x06001E7F RID: 7807 RVA: 0x005102F5 File Offset: 0x0050E4F5
		private VariableText(string original, string format, VariableText.Condition[] conditions, string[] variables)
		{
			this._original = original;
			this._format = format;
			this._conditions = conditions;
			this._variables = variables;
			this._formatArgBuffer = new object[variables.Length];
		}

		// Token: 0x06001E80 RID: 7808 RVA: 0x00510334 File Offset: 0x0050E534
		public static bool TryCreate(string s, out VariableText text)
		{
			if (!VariableText._substitutionRegex.IsMatch(s))
			{
				text = null;
				return false;
			}
			List<string> variables = new List<string>();
			List<VariableText.Condition> conditions = new List<VariableText.Condition>();
			string text2 = VariableText._substitutionRegex.Replace(s, delegate(Match match)
			{
				string text3 = match.Groups[2].ToString();
				string text4 = match.Groups[1].ToString();
				if (text4 != "")
				{
					conditions.Add(new VariableText.Condition
					{
						Name = text3,
						RequiredValue = (text4 == "?")
					});
					return "";
				}
				int num = variables.IndexOf(text3);
				if (num < 0)
				{
					num = variables.Count;
					variables.Add(text3);
				}
				return "{" + num + "}";
			});
			text = new VariableText(s, text2, conditions.ToArray(), variables.ToArray());
			return true;
		}

		// Token: 0x06001E81 RID: 7809 RVA: 0x005103A8 File Offset: 0x0050E5A8
		private bool CheckConditionsAndLoadArgs(Func<string, object> lookup)
		{
			foreach (VariableText.Condition condition in this._conditions)
			{
				if (((lookup(condition.Name) as bool?) ?? false) != condition.RequiredValue)
				{
					return false;
				}
			}
			for (int j = 0; j < this._variables.Length; j++)
			{
				object obj = lookup(this._variables[j]);
				if (obj == null)
				{
					return false;
				}
				this._formatArgBuffer[j] = obj;
			}
			return true;
		}

		// Token: 0x06001E82 RID: 7810 RVA: 0x00510440 File Offset: 0x0050E640
		public bool ConditionsMet(Func<string, object> lookup)
		{
			return this.CheckConditionsAndLoadArgs(lookup);
		}

		// Token: 0x06001E83 RID: 7811 RVA: 0x0051044C File Offset: 0x0050E64C
		public bool TryFormat(Func<string, object> lookup, out string formatted)
		{
			if (!this.CheckConditionsAndLoadArgs(lookup))
			{
				formatted = null;
				return false;
			}
			this._formatBuffer.AppendFormat(this._format, this._formatArgBuffer);
			formatted = this._formatBuffer.ToString();
			this._formatBuffer.Clear();
			return true;
		}

		// Token: 0x06001E84 RID: 7812 RVA: 0x00510499 File Offset: 0x0050E699
		public override string ToString()
		{
			return this._original;
		}

		// Token: 0x06001E85 RID: 7813 RVA: 0x005104A1 File Offset: 0x0050E6A1
		// Note: this type is marked as 'beforefieldinit'.
		static VariableText()
		{
		}

		// Token: 0x040016DF RID: 5855
		private readonly string _original;

		// Token: 0x040016E0 RID: 5856
		private readonly string _format;

		// Token: 0x040016E1 RID: 5857
		private readonly VariableText.Condition[] _conditions;

		// Token: 0x040016E2 RID: 5858
		private readonly string[] _variables;

		// Token: 0x040016E3 RID: 5859
		private readonly object[] _formatArgBuffer;

		// Token: 0x040016E4 RID: 5860
		private readonly StringBuilder _formatBuffer = new StringBuilder();

		// Token: 0x040016E5 RID: 5861
		private static readonly Regex _substitutionRegex = new Regex("{(\\?!?)?([a-zA-Z][\\w\\.]*)}", RegexOptions.Compiled);

		// Token: 0x02000753 RID: 1875
		private struct Condition
		{
			// Token: 0x040069DF RID: 27103
			public bool RequiredValue;

			// Token: 0x040069E0 RID: 27104
			public string Name;
		}

		// Token: 0x02000754 RID: 1876
		[CompilerGenerated]
		private sealed class <>c__DisplayClass9_0
		{
			// Token: 0x060040F1 RID: 16625 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass9_0()
			{
			}

			// Token: 0x060040F2 RID: 16626 RVA: 0x0069EEFC File Offset: 0x0069D0FC
			internal string <TryCreate>b__0(Match match)
			{
				string text = match.Groups[2].ToString();
				string text2 = match.Groups[1].ToString();
				if (text2 != "")
				{
					this.conditions.Add(new VariableText.Condition
					{
						Name = text,
						RequiredValue = (text2 == "?")
					});
					return "";
				}
				int num = this.variables.IndexOf(text);
				if (num < 0)
				{
					num = this.variables.Count;
					this.variables.Add(text);
				}
				return "{" + num + "}";
			}

			// Token: 0x040069E1 RID: 27105
			public List<VariableText.Condition> conditions;

			// Token: 0x040069E2 RID: 27106
			public List<string> variables;
		}
	}
}
