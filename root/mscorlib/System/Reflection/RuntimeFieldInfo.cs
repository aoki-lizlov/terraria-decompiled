using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x020008C6 RID: 2246
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal class RuntimeFieldInfo : RtFieldInfo, ISerializable
	{
		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x06004C80 RID: 19584 RVA: 0x0000408A File Offset: 0x0000228A
		internal BindingFlags BindingFlags
		{
			get
			{
				return BindingFlags.Default;
			}
		}

		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x06004C81 RID: 19585 RVA: 0x000F3699 File Offset: 0x000F1899
		public override Module Module
		{
			get
			{
				return this.GetRuntimeModule();
			}
		}

		// Token: 0x06004C82 RID: 19586 RVA: 0x000F340A File Offset: 0x000F160A
		internal RuntimeType GetDeclaringTypeInternal()
		{
			return (RuntimeType)this.DeclaringType;
		}

		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x06004C83 RID: 19587 RVA: 0x000F3417 File Offset: 0x000F1617
		private RuntimeType ReflectedTypeInternal
		{
			get
			{
				return (RuntimeType)this.ReflectedType;
			}
		}

		// Token: 0x06004C84 RID: 19588 RVA: 0x000F36A1 File Offset: 0x000F18A1
		internal RuntimeModule GetRuntimeModule()
		{
			return this.GetDeclaringTypeInternal().GetRuntimeModule();
		}

		// Token: 0x06004C85 RID: 19589 RVA: 0x000F36AE File Offset: 0x000F18AE
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			MemberInfoSerializationHolder.GetSerializationInfo(info, this.Name, this.ReflectedTypeInternal, this.ToString(), MemberTypes.Field);
		}

		// Token: 0x06004C86 RID: 19590
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal override extern object UnsafeGetValue(object obj);

		// Token: 0x06004C87 RID: 19591 RVA: 0x000F36D8 File Offset: 0x000F18D8
		internal override void CheckConsistency(object target)
		{
			if ((this.Attributes & FieldAttributes.Static) == FieldAttributes.Static || this.DeclaringType.IsInstanceOfType(target))
			{
				return;
			}
			if (target == null)
			{
				throw new TargetException(Environment.GetResourceString("Non-static field requires a target."));
			}
			throw new ArgumentException(string.Format(CultureInfo.CurrentUICulture, Environment.GetResourceString("Field '{0}' defined on type '{1}' is not a field on the target object which is of type '{2}'."), this.Name, this.DeclaringType, target.GetType()));
		}

		// Token: 0x06004C88 RID: 19592 RVA: 0x000F3740 File Offset: 0x000F1940
		[DebuggerStepThrough]
		[DebuggerHidden]
		internal override void UnsafeSetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			bool flag = false;
			RuntimeFieldHandle.SetValue(this, obj, value, null, this.Attributes, null, ref flag);
		}

		// Token: 0x06004C89 RID: 19593 RVA: 0x000F3761 File Offset: 0x000F1961
		[DebuggerStepThrough]
		[DebuggerHidden]
		public unsafe override void SetValueDirect(TypedReference obj, object value)
		{
			if (obj.IsNull)
			{
				throw new ArgumentException(Environment.GetResourceString("The TypedReference must be initialized."));
			}
			RuntimeFieldHandle.SetValueDirect(this, (RuntimeType)this.FieldType, (void*)(&obj), value, (RuntimeType)this.DeclaringType);
		}

		// Token: 0x06004C8A RID: 19594 RVA: 0x000F379C File Offset: 0x000F199C
		[DebuggerStepThrough]
		[DebuggerHidden]
		public unsafe override object GetValueDirect(TypedReference obj)
		{
			if (obj.IsNull)
			{
				throw new ArgumentException(Environment.GetResourceString("The TypedReference must be initialized."));
			}
			return RuntimeFieldHandle.GetValueDirect(this, (RuntimeType)this.FieldType, (void*)(&obj), (RuntimeType)this.DeclaringType);
		}

		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x06004C8B RID: 19595 RVA: 0x000F37D6 File Offset: 0x000F19D6
		public override FieldAttributes Attributes
		{
			get
			{
				return this.attrs;
			}
		}

		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x06004C8C RID: 19596 RVA: 0x000F37DE File Offset: 0x000F19DE
		public override RuntimeFieldHandle FieldHandle
		{
			get
			{
				return this.fhandle;
			}
		}

		// Token: 0x06004C8D RID: 19597
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Type ResolveType();

		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x06004C8E RID: 19598 RVA: 0x000F37E6 File Offset: 0x000F19E6
		public override Type FieldType
		{
			get
			{
				if (this.type == null)
				{
					this.type = this.ResolveType();
				}
				return this.type;
			}
		}

		// Token: 0x06004C8F RID: 19599
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Type GetParentType(bool declaring);

		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x06004C90 RID: 19600 RVA: 0x000F3808 File Offset: 0x000F1A08
		public override Type ReflectedType
		{
			get
			{
				return this.GetParentType(false);
			}
		}

		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x06004C91 RID: 19601 RVA: 0x000F3811 File Offset: 0x000F1A11
		public override Type DeclaringType
		{
			get
			{
				return this.GetParentType(true);
			}
		}

		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x06004C92 RID: 19602 RVA: 0x000F381A File Offset: 0x000F1A1A
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x06004C93 RID: 19603 RVA: 0x000534DE File Offset: 0x000516DE
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		// Token: 0x06004C94 RID: 19604 RVA: 0x000F133D File Offset: 0x000EF53D
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, inherit);
		}

		// Token: 0x06004C95 RID: 19605 RVA: 0x000F1346 File Offset: 0x000EF546
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
		}

		// Token: 0x06004C96 RID: 19606
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal override extern int GetFieldOffset();

		// Token: 0x06004C97 RID: 19607
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern object GetValueInternal(object obj);

		// Token: 0x06004C98 RID: 19608 RVA: 0x000F3824 File Offset: 0x000F1A24
		public override object GetValue(object obj)
		{
			if (!base.IsStatic)
			{
				if (obj == null)
				{
					throw new TargetException("Non-static field requires a target");
				}
				if (!this.DeclaringType.IsAssignableFrom(obj.GetType()))
				{
					throw new ArgumentException(string.Format("Field {0} defined on type {1} is not a field on the target object which is of type {2}.", this.Name, this.DeclaringType, obj.GetType()), "obj");
				}
			}
			if (!base.IsLiteral)
			{
				this.CheckGeneric();
			}
			return this.GetValueInternal(obj);
		}

		// Token: 0x06004C99 RID: 19609 RVA: 0x000F3896 File Offset: 0x000F1A96
		public override string ToString()
		{
			return string.Format("{0} {1}", this.FieldType, this.name);
		}

		// Token: 0x06004C9A RID: 19610
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetValueInternal(FieldInfo fi, object obj, object value);

		// Token: 0x06004C9B RID: 19611 RVA: 0x000F38B0 File Offset: 0x000F1AB0
		public override void SetValue(object obj, object val, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			if (!base.IsStatic)
			{
				if (obj == null)
				{
					throw new TargetException("Non-static field requires a target");
				}
				if (!this.DeclaringType.IsAssignableFrom(obj.GetType()))
				{
					throw new ArgumentException(string.Format("Field {0} defined on type {1} is not a field on the target object which is of type {2}.", this.Name, this.DeclaringType, obj.GetType()), "obj");
				}
			}
			if (base.IsLiteral)
			{
				throw new FieldAccessException("Cannot set a constant field");
			}
			if (binder == null)
			{
				binder = Type.DefaultBinder;
			}
			this.CheckGeneric();
			if (val != null)
			{
				val = ((RuntimeType)this.FieldType).CheckValue(val, binder, culture, invokeAttr);
			}
			RuntimeFieldInfo.SetValueInternal(this, obj, val);
		}

		// Token: 0x06004C9C RID: 19612 RVA: 0x000F3954 File Offset: 0x000F1B54
		internal RuntimeFieldInfo Clone(string newName)
		{
			return new RuntimeFieldInfo
			{
				name = newName,
				type = this.type,
				attrs = this.attrs,
				klass = this.klass,
				fhandle = this.fhandle
			};
		}

		// Token: 0x06004C9D RID: 19613
		[MethodImpl(MethodImplOptions.InternalCall)]
		public override extern object GetRawConstantValue();

		// Token: 0x06004C9E RID: 19614 RVA: 0x000F3670 File Offset: 0x000F1870
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributes(this);
		}

		// Token: 0x06004C9F RID: 19615 RVA: 0x000F3992 File Offset: 0x000F1B92
		private void CheckGeneric()
		{
			if (this.DeclaringType.ContainsGenericParameters)
			{
				throw new InvalidOperationException("Late bound operations cannot be performed on fields with types for which Type.ContainsGenericParameters is true.");
			}
		}

		// Token: 0x06004CA0 RID: 19616
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int get_core_clr_security_level();

		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x06004CA1 RID: 19617 RVA: 0x000F39AC File Offset: 0x000F1BAC
		public override bool IsSecurityTransparent
		{
			get
			{
				return this.get_core_clr_security_level() == 0;
			}
		}

		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x06004CA2 RID: 19618 RVA: 0x000F39B7 File Offset: 0x000F1BB7
		public override bool IsSecurityCritical
		{
			get
			{
				return this.get_core_clr_security_level() > 0;
			}
		}

		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x06004CA3 RID: 19619 RVA: 0x000F39C2 File Offset: 0x000F1BC2
		public override bool IsSecuritySafeCritical
		{
			get
			{
				return this.get_core_clr_security_level() == 1;
			}
		}

		// Token: 0x06004CA4 RID: 19620 RVA: 0x000F39CD File Offset: 0x000F1BCD
		public sealed override bool HasSameMetadataDefinitionAs(MemberInfo other)
		{
			return base.HasSameMetadataDefinitionAsCore<RuntimeFieldInfo>(other);
		}

		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x06004CA5 RID: 19621 RVA: 0x000F39D6 File Offset: 0x000F1BD6
		public override int MetadataToken
		{
			get
			{
				return RuntimeFieldInfo.get_metadata_token(this);
			}
		}

		// Token: 0x06004CA6 RID: 19622
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int get_metadata_token(RuntimeFieldInfo monoField);

		// Token: 0x06004CA7 RID: 19623
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Type[] GetTypeModifiers(bool optional);

		// Token: 0x06004CA8 RID: 19624 RVA: 0x000F39DE File Offset: 0x000F1BDE
		public override Type[] GetOptionalCustomModifiers()
		{
			return this.GetCustomModifiers(true);
		}

		// Token: 0x06004CA9 RID: 19625 RVA: 0x000F39E7 File Offset: 0x000F1BE7
		public override Type[] GetRequiredCustomModifiers()
		{
			return this.GetCustomModifiers(false);
		}

		// Token: 0x06004CAA RID: 19626 RVA: 0x000F39F0 File Offset: 0x000F1BF0
		private Type[] GetCustomModifiers(bool optional)
		{
			return this.GetTypeModifiers(optional) ?? Type.EmptyTypes;
		}

		// Token: 0x06004CAB RID: 19627 RVA: 0x000F3A02 File Offset: 0x000F1C02
		public RuntimeFieldInfo()
		{
		}

		// Token: 0x04002FCF RID: 12239
		internal IntPtr klass;

		// Token: 0x04002FD0 RID: 12240
		internal RuntimeFieldHandle fhandle;

		// Token: 0x04002FD1 RID: 12241
		private string name;

		// Token: 0x04002FD2 RID: 12242
		private Type type;

		// Token: 0x04002FD3 RID: 12243
		private FieldAttributes attrs;
	}
}
