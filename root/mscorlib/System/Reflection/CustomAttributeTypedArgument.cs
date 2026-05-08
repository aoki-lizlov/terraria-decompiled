using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x020008AE RID: 2222
	public struct CustomAttributeTypedArgument
	{
		// Token: 0x06004B03 RID: 19203 RVA: 0x000F073C File Offset: 0x000EE93C
		public CustomAttributeTypedArgument(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.Value = CustomAttributeTypedArgument.CanonicalizeValue(value);
			this.ArgumentType = value.GetType();
		}

		// Token: 0x06004B04 RID: 19204 RVA: 0x000F0764 File Offset: 0x000EE964
		public CustomAttributeTypedArgument(Type argumentType, object value)
		{
			if (argumentType == null)
			{
				throw new ArgumentNullException("argumentType");
			}
			this.Value = ((value == null) ? null : CustomAttributeTypedArgument.CanonicalizeValue(value));
			this.ArgumentType = argumentType;
			Array array = value as Array;
			if (array != null)
			{
				Type elementType = array.GetType().GetElementType();
				CustomAttributeTypedArgument[] array2 = new CustomAttributeTypedArgument[array.GetLength(0)];
				for (int i = 0; i < array2.Length; i++)
				{
					object value2 = array.GetValue(i);
					Type type = ((elementType == typeof(object) && value2 != null) ? value2.GetType() : elementType);
					array2[i] = new CustomAttributeTypedArgument(type, value2);
				}
				this.Value = new ReadOnlyCollection<CustomAttributeTypedArgument>(array2);
			}
		}

		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x06004B05 RID: 19205 RVA: 0x000F0816 File Offset: 0x000EEA16
		public readonly Type ArgumentType
		{
			[CompilerGenerated]
			get
			{
				return this.<ArgumentType>k__BackingField;
			}
		}

		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x06004B06 RID: 19206 RVA: 0x000F081E File Offset: 0x000EEA1E
		public readonly object Value
		{
			[CompilerGenerated]
			get
			{
				return this.<Value>k__BackingField;
			}
		}

		// Token: 0x06004B07 RID: 19207 RVA: 0x000F0826 File Offset: 0x000EEA26
		public override bool Equals(object obj)
		{
			return obj == this;
		}

		// Token: 0x06004B08 RID: 19208 RVA: 0x000F0836 File Offset: 0x000EEA36
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06004B09 RID: 19209 RVA: 0x000F0848 File Offset: 0x000EEA48
		public static bool operator ==(CustomAttributeTypedArgument left, CustomAttributeTypedArgument right)
		{
			return left.Equals(right);
		}

		// Token: 0x06004B0A RID: 19210 RVA: 0x000F085D File Offset: 0x000EEA5D
		public static bool operator !=(CustomAttributeTypedArgument left, CustomAttributeTypedArgument right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06004B0B RID: 19211 RVA: 0x000F0875 File Offset: 0x000EEA75
		public override string ToString()
		{
			return this.ToString(false);
		}

		// Token: 0x06004B0C RID: 19212 RVA: 0x000F0880 File Offset: 0x000EEA80
		internal string ToString(bool typed)
		{
			if (this.ArgumentType == null)
			{
				return base.ToString();
			}
			string text;
			try
			{
				if (this.ArgumentType.IsEnum)
				{
					text = string.Format(CultureInfo.CurrentCulture, typed ? "{0}" : "({1}){0}", this.Value, this.ArgumentType.FullNameOrDefault);
				}
				else if (this.Value == null)
				{
					text = string.Format(CultureInfo.CurrentCulture, typed ? "null" : "({0})null", this.ArgumentType.NameOrDefault);
				}
				else if (this.ArgumentType == typeof(string))
				{
					text = string.Format(CultureInfo.CurrentCulture, "\"{0}\"", this.Value);
				}
				else if (this.ArgumentType == typeof(char))
				{
					text = string.Format(CultureInfo.CurrentCulture, "'{0}'", this.Value);
				}
				else if (this.ArgumentType == typeof(Type))
				{
					text = string.Format(CultureInfo.CurrentCulture, "typeof({0})", ((Type)this.Value).FullNameOrDefault);
				}
				else if (this.ArgumentType.IsArray)
				{
					IList<CustomAttributeTypedArgument> list = this.Value as IList<CustomAttributeTypedArgument>;
					Type elementType = this.ArgumentType.GetElementType();
					string text2 = string.Format(CultureInfo.CurrentCulture, "new {0}[{1}] {{ ", elementType.IsEnum ? elementType.FullNameOrDefault : elementType.NameOrDefault, list.Count);
					for (int i = 0; i < list.Count; i++)
					{
						text2 += string.Format(CultureInfo.CurrentCulture, (i == 0) ? "{0}" : ", {0}", list[i].ToString(elementType != typeof(object)));
					}
					text = text2 + " }";
				}
				else
				{
					text = string.Format(CultureInfo.CurrentCulture, typed ? "{0}" : "({1}){0}", this.Value, this.ArgumentType.NameOrDefault);
				}
			}
			catch (MissingMetadataException)
			{
				text = base.ToString();
			}
			return text;
		}

		// Token: 0x06004B0D RID: 19213 RVA: 0x000F0ADC File Offset: 0x000EECDC
		private static object CanonicalizeValue(object value)
		{
			if (value.GetType().IsEnum)
			{
				return ((Enum)value).GetValue();
			}
			return value;
		}

		// Token: 0x04002EEB RID: 12011
		[CompilerGenerated]
		private readonly Type <ArgumentType>k__BackingField;

		// Token: 0x04002EEC RID: 12012
		[CompilerGenerated]
		private readonly object <Value>k__BackingField;
	}
}
