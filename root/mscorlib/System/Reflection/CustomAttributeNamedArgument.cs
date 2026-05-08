using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Internal.Runtime.Augments;

namespace System.Reflection
{
	// Token: 0x020008AD RID: 2221
	public struct CustomAttributeNamedArgument
	{
		// Token: 0x06004AF7 RID: 19191 RVA: 0x000F045D File Offset: 0x000EE65D
		internal CustomAttributeNamedArgument(Type attributeType, string memberName, bool isField, CustomAttributeTypedArgument typedValue)
		{
			this.IsField = isField;
			this.MemberName = memberName;
			this.TypedValue = typedValue;
			this._attributeType = attributeType;
			this._lazyMemberInfo = null;
		}

		// Token: 0x06004AF8 RID: 19192 RVA: 0x000F0488 File Offset: 0x000EE688
		public CustomAttributeNamedArgument(MemberInfo memberInfo, object value)
		{
			if (memberInfo == null)
			{
				throw new ArgumentNullException("memberInfo");
			}
			FieldInfo fieldInfo = memberInfo as FieldInfo;
			PropertyInfo propertyInfo = memberInfo as PropertyInfo;
			Type type;
			if (fieldInfo != null)
			{
				type = fieldInfo.FieldType;
			}
			else
			{
				if (!(propertyInfo != null))
				{
					throw new ArgumentException("The member must be either a field or a property.");
				}
				type = propertyInfo.PropertyType;
			}
			this._lazyMemberInfo = memberInfo;
			this._attributeType = memberInfo.DeclaringType;
			if (value is CustomAttributeTypedArgument)
			{
				CustomAttributeTypedArgument customAttributeTypedArgument = (CustomAttributeTypedArgument)value;
				this.TypedValue = customAttributeTypedArgument;
			}
			else
			{
				this.TypedValue = new CustomAttributeTypedArgument(type, value);
			}
			this.IsField = fieldInfo != null;
			this.MemberName = memberInfo.Name;
		}

		// Token: 0x06004AF9 RID: 19193 RVA: 0x000F053C File Offset: 0x000EE73C
		public CustomAttributeNamedArgument(MemberInfo memberInfo, CustomAttributeTypedArgument typedArgument)
		{
			if (memberInfo == null)
			{
				throw new ArgumentNullException("memberInfo");
			}
			this._lazyMemberInfo = memberInfo;
			this._attributeType = memberInfo.DeclaringType;
			this.TypedValue = typedArgument;
			this.IsField = memberInfo is FieldInfo;
			this.MemberName = memberInfo.Name;
		}

		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x06004AFA RID: 19194 RVA: 0x000F0594 File Offset: 0x000EE794
		public readonly CustomAttributeTypedArgument TypedValue
		{
			[CompilerGenerated]
			get
			{
				return this.<TypedValue>k__BackingField;
			}
		}

		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x06004AFB RID: 19195 RVA: 0x000F059C File Offset: 0x000EE79C
		public readonly bool IsField
		{
			[CompilerGenerated]
			get
			{
				return this.<IsField>k__BackingField;
			}
		}

		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x06004AFC RID: 19196 RVA: 0x000F05A4 File Offset: 0x000EE7A4
		public readonly string MemberName
		{
			[CompilerGenerated]
			get
			{
				return this.<MemberName>k__BackingField;
			}
		}

		// Token: 0x17000C1F RID: 3103
		// (get) Token: 0x06004AFD RID: 19197 RVA: 0x000F05AC File Offset: 0x000EE7AC
		public MemberInfo MemberInfo
		{
			get
			{
				MemberInfo memberInfo = this._lazyMemberInfo;
				if (memberInfo == null)
				{
					if (this.IsField)
					{
						memberInfo = this._attributeType.GetField(this.MemberName, BindingFlags.Instance | BindingFlags.Public);
					}
					else
					{
						memberInfo = this._attributeType.GetProperty(this.MemberName, BindingFlags.Instance | BindingFlags.Public);
					}
					if (memberInfo == null)
					{
						throw RuntimeAugments.Callbacks.CreateMissingMetadataException(this._attributeType);
					}
					this._lazyMemberInfo = memberInfo;
				}
				return memberInfo;
			}
		}

		// Token: 0x06004AFE RID: 19198 RVA: 0x000F0621 File Offset: 0x000EE821
		public override bool Equals(object obj)
		{
			return obj == this;
		}

		// Token: 0x06004AFF RID: 19199 RVA: 0x000F0631 File Offset: 0x000EE831
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06004B00 RID: 19200 RVA: 0x000F0643 File Offset: 0x000EE843
		public static bool operator ==(CustomAttributeNamedArgument left, CustomAttributeNamedArgument right)
		{
			return left.Equals(right);
		}

		// Token: 0x06004B01 RID: 19201 RVA: 0x000F0658 File Offset: 0x000EE858
		public static bool operator !=(CustomAttributeNamedArgument left, CustomAttributeNamedArgument right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06004B02 RID: 19202 RVA: 0x000F0670 File Offset: 0x000EE870
		public override string ToString()
		{
			if (this._attributeType == null)
			{
				return base.ToString();
			}
			string text;
			try
			{
				bool flag = this._lazyMemberInfo == null || (this.IsField ? ((FieldInfo)this._lazyMemberInfo).FieldType : ((PropertyInfo)this._lazyMemberInfo).PropertyType) != typeof(object);
				text = string.Format(CultureInfo.CurrentCulture, "{0} = {1}", this.MemberName, this.TypedValue.ToString(flag));
			}
			catch (MissingMetadataException)
			{
				text = base.ToString();
			}
			return text;
		}

		// Token: 0x04002EE6 RID: 12006
		[CompilerGenerated]
		private readonly CustomAttributeTypedArgument <TypedValue>k__BackingField;

		// Token: 0x04002EE7 RID: 12007
		[CompilerGenerated]
		private readonly bool <IsField>k__BackingField;

		// Token: 0x04002EE8 RID: 12008
		[CompilerGenerated]
		private readonly string <MemberName>k__BackingField;

		// Token: 0x04002EE9 RID: 12009
		private readonly Type _attributeType;

		// Token: 0x04002EEA RID: 12010
		private volatile MemberInfo _lazyMemberInfo;
	}
}
