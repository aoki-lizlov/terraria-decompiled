using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Terraria.Localization
{
	// Token: 0x0200018A RID: 394
	public class LocalizedText
	{
		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06001ECF RID: 7887 RVA: 0x005116C4 File Offset: 0x0050F8C4
		public string Value
		{
			get
			{
				VariableText variableText = this._value as VariableText;
				string text;
				if (variableText != null && variableText.TryFormat(new Func<string, object>(Lang.GetGlobalSubstitution), out text))
				{
					return text;
				}
				return this._value.ToString();
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06001ED0 RID: 7888 RVA: 0x00511703 File Offset: 0x0050F903
		public string UnformattedValue
		{
			get
			{
				return this._value.ToString();
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06001ED1 RID: 7889 RVA: 0x00511710 File Offset: 0x0050F910
		// (set) Token: 0x06001ED2 RID: 7890 RVA: 0x00511718 File Offset: 0x0050F918
		public string EnglishValue
		{
			[CompilerGenerated]
			get
			{
				return this.<EnglishValue>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<EnglishValue>k__BackingField = value;
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06001ED3 RID: 7891 RVA: 0x00511721 File Offset: 0x0050F921
		public bool HasValue
		{
			get
			{
				return this.EnglishValue != this.Key;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06001ED4 RID: 7892 RVA: 0x00511734 File Offset: 0x0050F934
		public bool ConditionsMet
		{
			get
			{
				VariableText variableText = this._value as VariableText;
				return variableText == null || variableText.ConditionsMet(new Func<string, object>(Lang.GetGlobalSubstitution));
			}
		}

		// Token: 0x06001ED5 RID: 7893 RVA: 0x00511764 File Offset: 0x0050F964
		internal LocalizedText(string key, string text)
		{
			this.Key = key;
			this.SetValue(text);
		}

		// Token: 0x06001ED6 RID: 7894 RVA: 0x0051177C File Offset: 0x0050F97C
		internal void SetValue(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this._value = value;
			if (LanguageManager.Instance != null && LanguageManager.Instance.ActiveCulture == GameCulture.DefaultCulture)
			{
				this.EnglishValue = value;
			}
			VariableText variableText;
			if (VariableText.TryCreate(value, out variableText))
			{
				this._value = variableText;
			}
		}

		// Token: 0x06001ED7 RID: 7895 RVA: 0x005117D0 File Offset: 0x0050F9D0
		public bool GetValueIfConditionsMet(out string formatted)
		{
			VariableText variableText = this._value as VariableText;
			if (variableText != null)
			{
				return variableText.TryFormat(new Func<string, object>(Lang.GetGlobalSubstitution), out formatted);
			}
			formatted = this.Value;
			return true;
		}

		// Token: 0x06001ED8 RID: 7896 RVA: 0x0051180C File Offset: 0x0050FA0C
		public bool TryFormatWith(object obj, out string formatted)
		{
			VariableText variableText = this._value as VariableText;
			if (variableText != null)
			{
				return variableText.TryFormat(LocalizedText.GetPropertyLookupFunc(obj), out formatted);
			}
			formatted = this.Value;
			return true;
		}

		// Token: 0x06001ED9 RID: 7897 RVA: 0x00511840 File Offset: 0x0050FA40
		public bool TryFormatWith(Func<string, object> lookup, out string formatted)
		{
			VariableText variableText = this._value as VariableText;
			if (variableText != null)
			{
				return variableText.TryFormat(lookup, out formatted);
			}
			formatted = this.Value;
			return true;
		}

		// Token: 0x06001EDA RID: 7898 RVA: 0x00511870 File Offset: 0x0050FA70
		public string FormatWith(object obj)
		{
			string text;
			if (!this.TryFormatWith(obj, out text))
			{
				return this.Value;
			}
			return text;
		}

		// Token: 0x06001EDB RID: 7899 RVA: 0x00511890 File Offset: 0x0050FA90
		public string FormatWith(Func<string, object> lookup)
		{
			string text;
			if (!this.TryFormatWith(lookup, out text))
			{
				return this.Value;
			}
			return text;
		}

		// Token: 0x06001EDC RID: 7900 RVA: 0x005118B0 File Offset: 0x0050FAB0
		public bool ConditionsMetWith(object obj)
		{
			VariableText variableText = this._value as VariableText;
			return variableText == null || variableText.ConditionsMet(LocalizedText.GetPropertyLookupFunc(obj));
		}

		// Token: 0x06001EDD RID: 7901 RVA: 0x005118DC File Offset: 0x0050FADC
		public bool ConditionsMetWith(Func<string, object> lookup)
		{
			VariableText variableText = this._value as VariableText;
			return variableText == null || variableText.ConditionsMet(lookup);
		}

		// Token: 0x06001EDE RID: 7902 RVA: 0x00511901 File Offset: 0x0050FB01
		public NetworkText ToNetworkText()
		{
			return NetworkText.FromKey(this.Key, new object[0]);
		}

		// Token: 0x06001EDF RID: 7903 RVA: 0x00511914 File Offset: 0x0050FB14
		public NetworkText ToNetworkText(params object[] substitutions)
		{
			return NetworkText.FromKey(this.Key, substitutions);
		}

		// Token: 0x06001EE0 RID: 7904 RVA: 0x00511922 File Offset: 0x0050FB22
		public static explicit operator string(LocalizedText text)
		{
			return text.Value;
		}

		// Token: 0x06001EE1 RID: 7905 RVA: 0x0051192A File Offset: 0x0050FB2A
		public string Format(object arg0)
		{
			return string.Format(this.Value, arg0);
		}

		// Token: 0x06001EE2 RID: 7906 RVA: 0x00511938 File Offset: 0x0050FB38
		public string Format(object arg0, object arg1)
		{
			return string.Format(this.Value, arg0, arg1);
		}

		// Token: 0x06001EE3 RID: 7907 RVA: 0x00511947 File Offset: 0x0050FB47
		public string Format(object arg0, object arg1, object arg2)
		{
			return string.Format(this.Value, arg0, arg1, arg2);
		}

		// Token: 0x06001EE4 RID: 7908 RVA: 0x00511957 File Offset: 0x0050FB57
		public string Format(params object[] args)
		{
			return string.Format(this.Value, args);
		}

		// Token: 0x06001EE5 RID: 7909 RVA: 0x00511965 File Offset: 0x0050FB65
		public override string ToString()
		{
			return this.Value;
		}

		// Token: 0x06001EE6 RID: 7910 RVA: 0x0051196D File Offset: 0x0050FB6D
		public bool EqualsCommand(string text)
		{
			text = text.ToLower();
			return text == this.Value || text == this.EnglishValue;
		}

		// Token: 0x06001EE7 RID: 7911 RVA: 0x00511993 File Offset: 0x0050FB93
		public bool ParseCommandPrefix(string text, out string remainder)
		{
			return Utils.ParseCommandPrefix(text, this.Value, out remainder) || Utils.ParseCommandPrefix(text, this.EnglishValue, out remainder);
		}

		// Token: 0x06001EE8 RID: 7912 RVA: 0x005119B4 File Offset: 0x0050FBB4
		private static Func<string, object> GetPropertyLookupFunc(object inst)
		{
			Type type = inst.GetType();
			PropertyDescriptorCollection properties;
			if (!LocalizedText._propertyLookupCache.TryGetValue(type, out properties))
			{
				LocalizedText._propertyLookupCache[type] = (properties = TypeDescriptor.GetProperties(type));
			}
			return delegate(string name)
			{
				PropertyDescriptor propertyDescriptor = properties[name];
				if (propertyDescriptor != null)
				{
					return propertyDescriptor.GetValue(inst);
				}
				return Lang.GetGlobalSubstitution(name);
			};
		}

		// Token: 0x06001EE9 RID: 7913 RVA: 0x00511A13 File Offset: 0x0050FC13
		// Note: this type is marked as 'beforefieldinit'.
		static LocalizedText()
		{
		}

		// Token: 0x040016F4 RID: 5876
		public static readonly LocalizedText Empty = new LocalizedText("", "");

		// Token: 0x040016F5 RID: 5877
		private static Regex _substitutionRegex = new Regex("{(\\?(?:!)?)?([a-zA-Z][\\w\\.]*)}", RegexOptions.Compiled);

		// Token: 0x040016F6 RID: 5878
		public readonly string Key;

		// Token: 0x040016F7 RID: 5879
		private object _value;

		// Token: 0x040016F8 RID: 5880
		[CompilerGenerated]
		private string <EnglishValue>k__BackingField;

		// Token: 0x040016F9 RID: 5881
		private static Dictionary<Type, PropertyDescriptorCollection> _propertyLookupCache = new Dictionary<Type, PropertyDescriptorCollection>();

		// Token: 0x02000759 RID: 1881
		[CompilerGenerated]
		private sealed class <>c__DisplayClass36_0
		{
			// Token: 0x060040FE RID: 16638 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass36_0()
			{
			}

			// Token: 0x060040FF RID: 16639 RVA: 0x0069F058 File Offset: 0x0069D258
			internal object <GetPropertyLookupFunc>b__0(string name)
			{
				PropertyDescriptor propertyDescriptor = this.properties[name];
				if (propertyDescriptor != null)
				{
					return propertyDescriptor.GetValue(this.inst);
				}
				return Lang.GetGlobalSubstitution(name);
			}

			// Token: 0x040069F9 RID: 27129
			public PropertyDescriptorCollection properties;

			// Token: 0x040069FA RID: 27130
			public object inst;
		}
	}
}
