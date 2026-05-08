using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Text;
using Mono;

namespace System.Reflection
{
	// Token: 0x020008D1 RID: 2257
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal class RuntimePropertyInfo : PropertyInfo, ISerializable
	{
		// Token: 0x06004D73 RID: 19827
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void get_property_info(RuntimePropertyInfo prop, ref MonoPropertyInfo info, PInfo req_info);

		// Token: 0x06004D74 RID: 19828
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Type[] GetTypeModifiers(RuntimePropertyInfo prop, bool optional);

		// Token: 0x06004D75 RID: 19829
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object get_default_value(RuntimePropertyInfo prop);

		// Token: 0x17000CB4 RID: 3252
		// (get) Token: 0x06004D76 RID: 19830 RVA: 0x0000408A File Offset: 0x0000228A
		internal BindingFlags BindingFlags
		{
			get
			{
				return BindingFlags.Default;
			}
		}

		// Token: 0x17000CB5 RID: 3253
		// (get) Token: 0x06004D77 RID: 19831 RVA: 0x000F516F File Offset: 0x000F336F
		public override Module Module
		{
			get
			{
				return this.GetRuntimeModule();
			}
		}

		// Token: 0x06004D78 RID: 19832 RVA: 0x000F340A File Offset: 0x000F160A
		internal RuntimeType GetDeclaringTypeInternal()
		{
			return (RuntimeType)this.DeclaringType;
		}

		// Token: 0x17000CB6 RID: 3254
		// (get) Token: 0x06004D79 RID: 19833 RVA: 0x000F3417 File Offset: 0x000F1617
		private RuntimeType ReflectedTypeInternal
		{
			get
			{
				return (RuntimeType)this.ReflectedType;
			}
		}

		// Token: 0x06004D7A RID: 19834 RVA: 0x000F5177 File Offset: 0x000F3377
		internal RuntimeModule GetRuntimeModule()
		{
			return this.GetDeclaringTypeInternal().GetRuntimeModule();
		}

		// Token: 0x06004D7B RID: 19835 RVA: 0x000F5184 File Offset: 0x000F3384
		public override string ToString()
		{
			return this.FormatNameAndSig(false);
		}

		// Token: 0x06004D7C RID: 19836 RVA: 0x000F5190 File Offset: 0x000F3390
		private string FormatNameAndSig(bool serialization)
		{
			StringBuilder stringBuilder = new StringBuilder(this.PropertyType.FormatTypeName(serialization));
			stringBuilder.Append(" ");
			stringBuilder.Append(this.Name);
			ParameterInfo[] indexParameters = this.GetIndexParameters();
			if (indexParameters.Length != 0)
			{
				stringBuilder.Append(" [");
				RuntimeParameterInfo.FormatParameters(stringBuilder, indexParameters, (CallingConventions)0, serialization);
				stringBuilder.Append("]");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06004D7D RID: 19837 RVA: 0x000F51FA File Offset: 0x000F33FA
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			MemberInfoSerializationHolder.GetSerializationInfo(info, this.Name, this.ReflectedTypeInternal, this.ToString(), this.SerializationToString(), MemberTypes.Property, null);
		}

		// Token: 0x06004D7E RID: 19838 RVA: 0x000F522B File Offset: 0x000F342B
		internal string SerializationToString()
		{
			return this.FormatNameAndSig(true);
		}

		// Token: 0x06004D7F RID: 19839 RVA: 0x000F5234 File Offset: 0x000F3434
		private void CachePropertyInfo(PInfo flags)
		{
			if ((this.cached & flags) != flags)
			{
				RuntimePropertyInfo.get_property_info(this, ref this.info, flags);
				this.cached |= flags;
			}
		}

		// Token: 0x17000CB7 RID: 3255
		// (get) Token: 0x06004D80 RID: 19840 RVA: 0x000F525C File Offset: 0x000F345C
		public override PropertyAttributes Attributes
		{
			get
			{
				this.CachePropertyInfo(PInfo.Attributes);
				return this.info.attrs;
			}
		}

		// Token: 0x17000CB8 RID: 3256
		// (get) Token: 0x06004D81 RID: 19841 RVA: 0x000F5270 File Offset: 0x000F3470
		public override bool CanRead
		{
			get
			{
				this.CachePropertyInfo(PInfo.GetMethod);
				return this.info.get_method != null;
			}
		}

		// Token: 0x17000CB9 RID: 3257
		// (get) Token: 0x06004D82 RID: 19842 RVA: 0x000F528A File Offset: 0x000F348A
		public override bool CanWrite
		{
			get
			{
				this.CachePropertyInfo(PInfo.SetMethod);
				return this.info.set_method != null;
			}
		}

		// Token: 0x17000CBA RID: 3258
		// (get) Token: 0x06004D83 RID: 19843 RVA: 0x000F52A4 File Offset: 0x000F34A4
		public override Type PropertyType
		{
			get
			{
				this.CachePropertyInfo(PInfo.GetMethod | PInfo.SetMethod);
				if (this.info.get_method != null)
				{
					return this.info.get_method.ReturnType;
				}
				ParameterInfo[] parametersInternal = this.info.set_method.GetParametersInternal();
				return parametersInternal[parametersInternal.Length - 1].ParameterType;
			}
		}

		// Token: 0x17000CBB RID: 3259
		// (get) Token: 0x06004D84 RID: 19844 RVA: 0x000F52F7 File Offset: 0x000F34F7
		public override Type ReflectedType
		{
			get
			{
				this.CachePropertyInfo(PInfo.ReflectedType);
				return this.info.parent;
			}
		}

		// Token: 0x17000CBC RID: 3260
		// (get) Token: 0x06004D85 RID: 19845 RVA: 0x000F530B File Offset: 0x000F350B
		public override Type DeclaringType
		{
			get
			{
				this.CachePropertyInfo(PInfo.DeclaringType);
				return this.info.declaring_type;
			}
		}

		// Token: 0x17000CBD RID: 3261
		// (get) Token: 0x06004D86 RID: 19846 RVA: 0x000F5320 File Offset: 0x000F3520
		public override string Name
		{
			get
			{
				this.CachePropertyInfo(PInfo.Name);
				return this.info.name;
			}
		}

		// Token: 0x06004D87 RID: 19847 RVA: 0x000F5338 File Offset: 0x000F3538
		public override MethodInfo[] GetAccessors(bool nonPublic)
		{
			int num = 0;
			int num2 = 0;
			this.CachePropertyInfo(PInfo.GetMethod | PInfo.SetMethod);
			if (this.info.set_method != null && (nonPublic || this.info.set_method.IsPublic))
			{
				num2 = 1;
			}
			if (this.info.get_method != null && (nonPublic || this.info.get_method.IsPublic))
			{
				num = 1;
			}
			MethodInfo[] array = new MethodInfo[num + num2];
			int num3 = 0;
			if (num2 != 0)
			{
				array[num3++] = this.info.set_method;
			}
			if (num != 0)
			{
				array[num3++] = this.info.get_method;
			}
			return array;
		}

		// Token: 0x06004D88 RID: 19848 RVA: 0x000F53DA File Offset: 0x000F35DA
		public override MethodInfo GetGetMethod(bool nonPublic)
		{
			this.CachePropertyInfo(PInfo.GetMethod);
			if (this.info.get_method != null && (nonPublic || this.info.get_method.IsPublic))
			{
				return this.info.get_method;
			}
			return null;
		}

		// Token: 0x06004D89 RID: 19849 RVA: 0x000F5418 File Offset: 0x000F3618
		public override ParameterInfo[] GetIndexParameters()
		{
			this.CachePropertyInfo(PInfo.GetMethod | PInfo.SetMethod);
			ParameterInfo[] array;
			int num;
			if (this.info.get_method != null)
			{
				array = this.info.get_method.GetParametersInternal();
				num = array.Length;
			}
			else
			{
				if (!(this.info.set_method != null))
				{
					return EmptyArray<ParameterInfo>.Value;
				}
				array = this.info.set_method.GetParametersInternal();
				num = array.Length - 1;
			}
			ParameterInfo[] array2 = new ParameterInfo[num];
			for (int i = 0; i < num; i++)
			{
				array2[i] = RuntimeParameterInfo.New(array[i], this);
			}
			return array2;
		}

		// Token: 0x06004D8A RID: 19850 RVA: 0x000F54A8 File Offset: 0x000F36A8
		public override MethodInfo GetSetMethod(bool nonPublic)
		{
			this.CachePropertyInfo(PInfo.SetMethod);
			if (this.info.set_method != null && (nonPublic || this.info.set_method.IsPublic))
			{
				return this.info.set_method;
			}
			return null;
		}

		// Token: 0x06004D8B RID: 19851 RVA: 0x000F54E6 File Offset: 0x000F36E6
		public override object GetConstantValue()
		{
			return RuntimePropertyInfo.get_default_value(this);
		}

		// Token: 0x06004D8C RID: 19852 RVA: 0x000F54E6 File Offset: 0x000F36E6
		public override object GetRawConstantValue()
		{
			return RuntimePropertyInfo.get_default_value(this);
		}

		// Token: 0x06004D8D RID: 19853 RVA: 0x000F54EE File Offset: 0x000F36EE
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, false);
		}

		// Token: 0x06004D8E RID: 19854 RVA: 0x000F4EC4 File Offset: 0x000F30C4
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, false);
		}

		// Token: 0x06004D8F RID: 19855 RVA: 0x000F4ECD File Offset: 0x000F30CD
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, false);
		}

		// Token: 0x06004D90 RID: 19856 RVA: 0x000F54F8 File Offset: 0x000F36F8
		private static object GetterAdapterFrame<T, R>(RuntimePropertyInfo.Getter<T, R> getter, object obj)
		{
			return getter((T)((object)obj));
		}

		// Token: 0x06004D91 RID: 19857 RVA: 0x000F550B File Offset: 0x000F370B
		private static object StaticGetterAdapterFrame<R>(RuntimePropertyInfo.StaticGetter<R> getter, object obj)
		{
			return getter();
		}

		// Token: 0x06004D92 RID: 19858 RVA: 0x000F5518 File Offset: 0x000F3718
		private static RuntimePropertyInfo.GetterAdapter CreateGetterDelegate(MethodInfo method)
		{
			Type[] array;
			Type type;
			string text;
			if (method.IsStatic)
			{
				array = new Type[] { method.ReturnType };
				type = typeof(RuntimePropertyInfo.StaticGetter<>);
				text = "StaticGetterAdapterFrame";
			}
			else
			{
				array = new Type[] { method.DeclaringType, method.ReturnType };
				type = typeof(RuntimePropertyInfo.Getter<, >);
				text = "GetterAdapterFrame";
			}
			object obj = Delegate.CreateDelegate(type.MakeGenericType(array), method);
			MethodInfo methodInfo = typeof(RuntimePropertyInfo).GetMethod(text, BindingFlags.Static | BindingFlags.NonPublic);
			methodInfo = methodInfo.MakeGenericMethod(array);
			return (RuntimePropertyInfo.GetterAdapter)Delegate.CreateDelegate(typeof(RuntimePropertyInfo.GetterAdapter), obj, methodInfo, true);
		}

		// Token: 0x06004D93 RID: 19859 RVA: 0x000F55C0 File Offset: 0x000F37C0
		public override object GetValue(object obj, object[] index)
		{
			if (index == null || index.Length == 0)
			{
				if (this.cached_getter == null)
				{
					MethodInfo getMethod = this.GetGetMethod(true);
					if (getMethod == null)
					{
						throw new ArgumentException("Get Method not found for '" + this.Name + "'");
					}
					if (this.DeclaringType.IsValueType || this.PropertyType.IsByRef || getMethod.ContainsGenericParameters)
					{
						goto IL_0097;
					}
					this.cached_getter = RuntimePropertyInfo.CreateGetterDelegate(getMethod);
					try
					{
						return this.cached_getter(obj);
					}
					catch (Exception ex)
					{
						throw new TargetInvocationException(ex);
					}
				}
				try
				{
					return this.cached_getter(obj);
				}
				catch (Exception ex2)
				{
					throw new TargetInvocationException(ex2);
				}
			}
			IL_0097:
			return this.GetValue(obj, BindingFlags.Default, null, index, null);
		}

		// Token: 0x06004D94 RID: 19860 RVA: 0x000F5690 File Offset: 0x000F3890
		public override object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
			object obj2 = null;
			MethodInfo getMethod = this.GetGetMethod(true);
			if (getMethod == null)
			{
				throw new ArgumentException("Get Method not found for '" + this.Name + "'");
			}
			try
			{
				if (index == null || index.Length == 0)
				{
					obj2 = getMethod.Invoke(obj, invokeAttr, binder, null, culture);
				}
				else
				{
					obj2 = getMethod.Invoke(obj, invokeAttr, binder, index, culture);
				}
			}
			catch (SecurityException ex)
			{
				throw new TargetInvocationException(ex);
			}
			return obj2;
		}

		// Token: 0x06004D95 RID: 19861 RVA: 0x000F570C File Offset: 0x000F390C
		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
			MethodInfo setMethod = this.GetSetMethod(true);
			if (setMethod == null)
			{
				throw new ArgumentException("Set Method not found for '" + this.Name + "'");
			}
			object[] array;
			if (index == null || index.Length == 0)
			{
				array = new object[] { value };
			}
			else
			{
				int num = index.Length;
				array = new object[num + 1];
				index.CopyTo(array, 0);
				array[num] = value;
			}
			setMethod.Invoke(obj, invokeAttr, binder, array, culture);
		}

		// Token: 0x06004D96 RID: 19862 RVA: 0x000F5784 File Offset: 0x000F3984
		public override Type[] GetOptionalCustomModifiers()
		{
			return this.GetCustomModifiers(true);
		}

		// Token: 0x06004D97 RID: 19863 RVA: 0x000F578D File Offset: 0x000F398D
		public override Type[] GetRequiredCustomModifiers()
		{
			return this.GetCustomModifiers(false);
		}

		// Token: 0x06004D98 RID: 19864 RVA: 0x000F5796 File Offset: 0x000F3996
		private Type[] GetCustomModifiers(bool optional)
		{
			return RuntimePropertyInfo.GetTypeModifiers(this, optional) ?? Type.EmptyTypes;
		}

		// Token: 0x06004D99 RID: 19865 RVA: 0x000F3670 File Offset: 0x000F1870
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributes(this);
		}

		// Token: 0x06004D9A RID: 19866 RVA: 0x000F57A8 File Offset: 0x000F39A8
		public sealed override bool HasSameMetadataDefinitionAs(MemberInfo other)
		{
			return base.HasSameMetadataDefinitionAsCore<RuntimePropertyInfo>(other);
		}

		// Token: 0x17000CBE RID: 3262
		// (get) Token: 0x06004D9B RID: 19867 RVA: 0x000F57B1 File Offset: 0x000F39B1
		public override int MetadataToken
		{
			get
			{
				return RuntimePropertyInfo.get_metadata_token(this);
			}
		}

		// Token: 0x06004D9C RID: 19868
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int get_metadata_token(RuntimePropertyInfo monoProperty);

		// Token: 0x06004D9D RID: 19869
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern PropertyInfo internal_from_handle_type(IntPtr event_handle, IntPtr type_handle);

		// Token: 0x06004D9E RID: 19870 RVA: 0x000F57BC File Offset: 0x000F39BC
		internal static PropertyInfo GetPropertyFromHandle(RuntimePropertyHandle handle, RuntimeTypeHandle reflectedType)
		{
			if (handle.Value == IntPtr.Zero)
			{
				throw new ArgumentException("The handle is invalid.");
			}
			PropertyInfo propertyInfo = RuntimePropertyInfo.internal_from_handle_type(handle.Value, reflectedType.Value);
			if (propertyInfo == null)
			{
				throw new ArgumentException("The property handle and the type handle are incompatible.");
			}
			return propertyInfo;
		}

		// Token: 0x06004D9F RID: 19871 RVA: 0x000F580E File Offset: 0x000F3A0E
		public RuntimePropertyInfo()
		{
		}

		// Token: 0x04002FF8 RID: 12280
		internal IntPtr klass;

		// Token: 0x04002FF9 RID: 12281
		internal IntPtr prop;

		// Token: 0x04002FFA RID: 12282
		private MonoPropertyInfo info;

		// Token: 0x04002FFB RID: 12283
		private PInfo cached;

		// Token: 0x04002FFC RID: 12284
		private RuntimePropertyInfo.GetterAdapter cached_getter;

		// Token: 0x020008D2 RID: 2258
		// (Invoke) Token: 0x06004DA1 RID: 19873
		private delegate object GetterAdapter(object _this);

		// Token: 0x020008D3 RID: 2259
		// (Invoke) Token: 0x06004DA5 RID: 19877
		private delegate R Getter<T, R>(T _this);

		// Token: 0x020008D4 RID: 2260
		// (Invoke) Token: 0x06004DA9 RID: 19881
		private delegate R StaticGetter<R>();
	}
}
