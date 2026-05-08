using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x0200086F RID: 2159
	[Serializable]
	public abstract class FieldInfo : MemberInfo
	{
		// Token: 0x0600484C RID: 18508 RVA: 0x00047D3C File Offset: 0x00045F3C
		protected FieldInfo()
		{
		}

		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x0600484D RID: 18509 RVA: 0x0001A197 File Offset: 0x00018397
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Field;
			}
		}

		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x0600484E RID: 18510
		public abstract FieldAttributes Attributes { get; }

		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x0600484F RID: 18511
		public abstract Type FieldType { get; }

		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x06004850 RID: 18512 RVA: 0x000EDFC2 File Offset: 0x000EC1C2
		public bool IsInitOnly
		{
			get
			{
				return (this.Attributes & FieldAttributes.InitOnly) > FieldAttributes.PrivateScope;
			}
		}

		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x06004851 RID: 18513 RVA: 0x000EDFD0 File Offset: 0x000EC1D0
		public bool IsLiteral
		{
			get
			{
				return (this.Attributes & FieldAttributes.Literal) > FieldAttributes.PrivateScope;
			}
		}

		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x06004852 RID: 18514 RVA: 0x000EDFDE File Offset: 0x000EC1DE
		public bool IsNotSerialized
		{
			get
			{
				return (this.Attributes & FieldAttributes.NotSerialized) > FieldAttributes.PrivateScope;
			}
		}

		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x06004853 RID: 18515 RVA: 0x000EDFEF File Offset: 0x000EC1EF
		public bool IsPinvokeImpl
		{
			get
			{
				return (this.Attributes & FieldAttributes.PinvokeImpl) > FieldAttributes.PrivateScope;
			}
		}

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x06004854 RID: 18516 RVA: 0x000EE000 File Offset: 0x000EC200
		public bool IsSpecialName
		{
			get
			{
				return (this.Attributes & FieldAttributes.SpecialName) > FieldAttributes.PrivateScope;
			}
		}

		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x06004855 RID: 18517 RVA: 0x000EE011 File Offset: 0x000EC211
		public bool IsStatic
		{
			get
			{
				return (this.Attributes & FieldAttributes.Static) > FieldAttributes.PrivateScope;
			}
		}

		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x06004856 RID: 18518 RVA: 0x000EE01F File Offset: 0x000EC21F
		public bool IsAssembly
		{
			get
			{
				return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Assembly;
			}
		}

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x06004857 RID: 18519 RVA: 0x000EE02C File Offset: 0x000EC22C
		public bool IsFamily
		{
			get
			{
				return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Family;
			}
		}

		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x06004858 RID: 18520 RVA: 0x000EE039 File Offset: 0x000EC239
		public bool IsFamilyAndAssembly
		{
			get
			{
				return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.FamANDAssem;
			}
		}

		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x06004859 RID: 18521 RVA: 0x000EE046 File Offset: 0x000EC246
		public bool IsFamilyOrAssembly
		{
			get
			{
				return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.FamORAssem;
			}
		}

		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x0600485A RID: 18522 RVA: 0x000EE053 File Offset: 0x000EC253
		public bool IsPrivate
		{
			get
			{
				return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Private;
			}
		}

		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x0600485B RID: 18523 RVA: 0x000EE060 File Offset: 0x000EC260
		public bool IsPublic
		{
			get
			{
				return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Public;
			}
		}

		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x0600485C RID: 18524 RVA: 0x00003FB7 File Offset: 0x000021B7
		public virtual bool IsSecurityCritical
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x0600485D RID: 18525 RVA: 0x0000408A File Offset: 0x0000228A
		public virtual bool IsSecuritySafeCritical
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x0600485E RID: 18526 RVA: 0x0000408A File Offset: 0x0000228A
		public virtual bool IsSecurityTransparent
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x0600485F RID: 18527
		public abstract RuntimeFieldHandle FieldHandle { get; }

		// Token: 0x06004860 RID: 18528 RVA: 0x000EDDA6 File Offset: 0x000EBFA6
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06004861 RID: 18529 RVA: 0x000EDDAF File Offset: 0x000EBFAF
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06004862 RID: 18530 RVA: 0x00064F2C File Offset: 0x0006312C
		public static bool operator ==(FieldInfo left, FieldInfo right)
		{
			return left == right || (left != null && right != null && left.Equals(right));
		}

		// Token: 0x06004863 RID: 18531 RVA: 0x000EE06D File Offset: 0x000EC26D
		public static bool operator !=(FieldInfo left, FieldInfo right)
		{
			return !(left == right);
		}

		// Token: 0x06004864 RID: 18532
		public abstract object GetValue(object obj);

		// Token: 0x06004865 RID: 18533 RVA: 0x000EE079 File Offset: 0x000EC279
		[DebuggerHidden]
		[DebuggerStepThrough]
		public void SetValue(object obj, object value)
		{
			this.SetValue(obj, value, BindingFlags.Default, Type.DefaultBinder, null);
		}

		// Token: 0x06004866 RID: 18534
		public abstract void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture);

		// Token: 0x06004867 RID: 18535 RVA: 0x000EE08A File Offset: 0x000EC28A
		[CLSCompliant(false)]
		public virtual void SetValueDirect(TypedReference obj, object value)
		{
			throw new NotSupportedException("This non-CLS method is not implemented.");
		}

		// Token: 0x06004868 RID: 18536 RVA: 0x000EE08A File Offset: 0x000EC28A
		[CLSCompliant(false)]
		public virtual object GetValueDirect(TypedReference obj)
		{
			throw new NotSupportedException("This non-CLS method is not implemented.");
		}

		// Token: 0x06004869 RID: 18537 RVA: 0x000EE08A File Offset: 0x000EC28A
		public virtual object GetRawConstantValue()
		{
			throw new NotSupportedException("This non-CLS method is not implemented.");
		}

		// Token: 0x0600486A RID: 18538 RVA: 0x00047D5E File Offset: 0x00045F5E
		public virtual Type[] GetOptionalCustomModifiers()
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x0600486B RID: 18539 RVA: 0x00047D5E File Offset: 0x00045F5E
		public virtual Type[] GetRequiredCustomModifiers()
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x0600486C RID: 18540
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern FieldInfo internal_from_handle_type(IntPtr field_handle, IntPtr type_handle);

		// Token: 0x0600486D RID: 18541 RVA: 0x000EE096 File Offset: 0x000EC296
		public static FieldInfo GetFieldFromHandle(RuntimeFieldHandle handle)
		{
			if (handle.Value == IntPtr.Zero)
			{
				throw new ArgumentException("The handle is invalid.");
			}
			return FieldInfo.internal_from_handle_type(handle.Value, IntPtr.Zero);
		}

		// Token: 0x0600486E RID: 18542 RVA: 0x000EE0C8 File Offset: 0x000EC2C8
		[ComVisible(false)]
		public static FieldInfo GetFieldFromHandle(RuntimeFieldHandle handle, RuntimeTypeHandle declaringType)
		{
			if (handle.Value == IntPtr.Zero)
			{
				throw new ArgumentException("The handle is invalid.");
			}
			FieldInfo fieldInfo = FieldInfo.internal_from_handle_type(handle.Value, declaringType.Value);
			if (fieldInfo == null)
			{
				throw new ArgumentException("The field handle and the type handle are incompatible.");
			}
			return fieldInfo;
		}

		// Token: 0x0600486F RID: 18543 RVA: 0x000EE11A File Offset: 0x000EC31A
		internal virtual int GetFieldOffset()
		{
			throw new SystemException("This method should not be called");
		}

		// Token: 0x06004870 RID: 18544
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern MarshalAsAttribute get_marshal_info();

		// Token: 0x06004871 RID: 18545 RVA: 0x000EE128 File Offset: 0x000EC328
		internal object[] GetPseudoCustomAttributes()
		{
			int num = 0;
			if (this.IsNotSerialized)
			{
				num++;
			}
			if (this.DeclaringType.IsExplicitLayout)
			{
				num++;
			}
			MarshalAsAttribute marshal_info = this.get_marshal_info();
			if (marshal_info != null)
			{
				num++;
			}
			if (num == 0)
			{
				return null;
			}
			object[] array = new object[num];
			num = 0;
			if (this.IsNotSerialized)
			{
				array[num++] = new NonSerializedAttribute();
			}
			if (this.DeclaringType.IsExplicitLayout)
			{
				array[num++] = new FieldOffsetAttribute(this.GetFieldOffset());
			}
			if (marshal_info != null)
			{
				array[num++] = marshal_info;
			}
			return array;
		}

		// Token: 0x06004872 RID: 18546 RVA: 0x000EE1B0 File Offset: 0x000EC3B0
		internal CustomAttributeData[] GetPseudoCustomAttributesData()
		{
			int num = 0;
			if (this.IsNotSerialized)
			{
				num++;
			}
			if (this.DeclaringType.IsExplicitLayout)
			{
				num++;
			}
			MarshalAsAttribute marshal_info = this.get_marshal_info();
			if (marshal_info != null)
			{
				num++;
			}
			if (num == 0)
			{
				return null;
			}
			CustomAttributeData[] array = new CustomAttributeData[num];
			num = 0;
			if (this.IsNotSerialized)
			{
				array[num++] = new CustomAttributeData(typeof(NonSerializedAttribute).GetConstructor(Type.EmptyTypes));
			}
			if (this.DeclaringType.IsExplicitLayout)
			{
				CustomAttributeTypedArgument[] array2 = new CustomAttributeTypedArgument[]
				{
					new CustomAttributeTypedArgument(typeof(int), this.GetFieldOffset())
				};
				array[num++] = new CustomAttributeData(typeof(FieldOffsetAttribute).GetConstructor(new Type[] { typeof(int) }), array2, EmptyArray<CustomAttributeNamedArgument>.Value);
			}
			if (marshal_info != null)
			{
				CustomAttributeTypedArgument[] array3 = new CustomAttributeTypedArgument[]
				{
					new CustomAttributeTypedArgument(typeof(UnmanagedType), marshal_info.Value)
				};
				array[num++] = new CustomAttributeData(typeof(MarshalAsAttribute).GetConstructor(new Type[] { typeof(UnmanagedType) }), array3, EmptyArray<CustomAttributeNamedArgument>.Value);
			}
			return array;
		}
	}
}
