using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000208 RID: 520
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public abstract class Delegate : ICloneable, ISerializable
	{
		// Token: 0x06001975 RID: 6517 RVA: 0x0005F8B4 File Offset: 0x0005DAB4
		protected Delegate(object target, string method)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			this.m_target = target;
			this.data = new DelegateData();
			this.data.method_name = method;
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x0005F904 File Offset: 0x0005DB04
		protected Delegate(Type target, string method)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			this.data = new DelegateData();
			this.data.method_name = method;
			this.data.target_type = target;
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06001977 RID: 6519 RVA: 0x0005F95C File Offset: 0x0005DB5C
		public MethodInfo Method
		{
			get
			{
				return this.GetMethodImpl();
			}
		}

		// Token: 0x06001978 RID: 6520
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern MethodInfo GetVirtualMethod_internal();

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06001979 RID: 6521 RVA: 0x0005F964 File Offset: 0x0005DB64
		public object Target
		{
			get
			{
				return this.m_target;
			}
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x0005F96C File Offset: 0x0005DB6C
		internal IntPtr GetNativeFunctionPointer()
		{
			return this.method_ptr;
		}

		// Token: 0x0600197B RID: 6523
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Delegate CreateDelegate_internal(Type type, object target, MethodInfo info, bool throwOnBindFailure);

		// Token: 0x0600197C RID: 6524 RVA: 0x0005F974 File Offset: 0x0005DB74
		private static bool arg_type_match(Type delArgType, Type argType)
		{
			bool flag = delArgType == argType;
			if (!flag && !argType.IsValueType && argType.IsAssignableFrom(delArgType))
			{
				flag = true;
			}
			if (!flag)
			{
				if (delArgType.IsEnum && Enum.GetUnderlyingType(delArgType) == argType)
				{
					flag = true;
				}
				else if (argType.IsEnum && Enum.GetUnderlyingType(argType) == delArgType)
				{
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x0005F9D8 File Offset: 0x0005DBD8
		private static bool arg_type_match_this(Type delArgType, Type argType, bool boxedThis)
		{
			bool flag;
			if (argType.IsValueType)
			{
				flag = (delArgType.IsByRef && delArgType.GetElementType() == argType) || (boxedThis && delArgType == argType);
			}
			else
			{
				flag = delArgType == argType || argType.IsAssignableFrom(delArgType);
			}
			return flag;
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x0005FA2C File Offset: 0x0005DC2C
		private static bool return_type_match(Type delReturnType, Type returnType)
		{
			bool flag = returnType == delReturnType;
			if (!flag && !returnType.IsValueType && delReturnType.IsAssignableFrom(returnType))
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x0005FA58 File Offset: 0x0005DC58
		public static Delegate CreateDelegate(Type type, object firstArgument, MethodInfo method, bool throwOnBindFailure)
		{
			return Delegate.CreateDelegate(type, firstArgument, method, throwOnBindFailure, true);
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x0005FA64 File Offset: 0x0005DC64
		private static Delegate CreateDelegate(Type type, object firstArgument, MethodInfo method, bool throwOnBindFailure, bool allowClosed)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			if (!type.IsSubclassOf(typeof(MulticastDelegate)))
			{
				throw new ArgumentException("type is not a subclass of Multicastdelegate");
			}
			MethodInfo methodInfo = type.GetMethod("Invoke");
			if (!Delegate.return_type_match(methodInfo.ReturnType, method.ReturnType))
			{
				if (throwOnBindFailure)
				{
					throw new ArgumentException("method return type is incompatible");
				}
				return null;
			}
			else
			{
				ParameterInfo[] parametersInternal = methodInfo.GetParametersInternal();
				ParameterInfo[] parametersInternal2 = method.GetParametersInternal();
				bool flag;
				if (firstArgument != null)
				{
					if (!method.IsStatic)
					{
						flag = parametersInternal2.Length == parametersInternal.Length;
					}
					else
					{
						flag = parametersInternal2.Length == parametersInternal.Length + 1;
					}
				}
				else if (!method.IsStatic)
				{
					flag = parametersInternal2.Length + 1 == parametersInternal.Length;
					if (!flag)
					{
						flag = parametersInternal2.Length == parametersInternal.Length;
					}
				}
				else
				{
					flag = parametersInternal2.Length == parametersInternal.Length;
					if (!flag)
					{
						flag = parametersInternal2.Length == parametersInternal.Length + 1;
					}
				}
				if (!flag)
				{
					if (throwOnBindFailure)
					{
						throw new TargetParameterCountException("Parameter count mismatch.");
					}
					return null;
				}
				else
				{
					DelegateData delegateData = new DelegateData();
					bool flag2;
					if (firstArgument != null)
					{
						if (!method.IsStatic)
						{
							flag2 = Delegate.arg_type_match_this(firstArgument.GetType(), method.DeclaringType, true);
							for (int i = 0; i < parametersInternal2.Length; i++)
							{
								flag2 &= Delegate.arg_type_match(parametersInternal[i].ParameterType, parametersInternal2[i].ParameterType);
							}
						}
						else
						{
							flag2 = Delegate.arg_type_match(firstArgument.GetType(), parametersInternal2[0].ParameterType);
							for (int j = 1; j < parametersInternal2.Length; j++)
							{
								flag2 &= Delegate.arg_type_match(parametersInternal[j - 1].ParameterType, parametersInternal2[j].ParameterType);
							}
							delegateData.curried_first_arg = true;
						}
					}
					else if (!method.IsStatic)
					{
						if (parametersInternal2.Length + 1 == parametersInternal.Length)
						{
							flag2 = Delegate.arg_type_match_this(parametersInternal[0].ParameterType, method.DeclaringType, false);
							for (int k = 0; k < parametersInternal2.Length; k++)
							{
								flag2 &= Delegate.arg_type_match(parametersInternal[k + 1].ParameterType, parametersInternal2[k].ParameterType);
							}
						}
						else
						{
							flag2 = allowClosed;
							for (int l = 0; l < parametersInternal2.Length; l++)
							{
								flag2 &= Delegate.arg_type_match(parametersInternal[l].ParameterType, parametersInternal2[l].ParameterType);
							}
						}
					}
					else if (parametersInternal.Length + 1 == parametersInternal2.Length)
					{
						flag2 = !parametersInternal2[0].ParameterType.IsValueType && !parametersInternal2[0].ParameterType.IsByRef && allowClosed;
						for (int m = 0; m < parametersInternal.Length; m++)
						{
							flag2 &= Delegate.arg_type_match(parametersInternal[m].ParameterType, parametersInternal2[m + 1].ParameterType);
						}
						delegateData.curried_first_arg = true;
					}
					else
					{
						flag2 = true;
						for (int n = 0; n < parametersInternal2.Length; n++)
						{
							flag2 &= Delegate.arg_type_match(parametersInternal[n].ParameterType, parametersInternal2[n].ParameterType);
						}
					}
					if (flag2)
					{
						Delegate @delegate = Delegate.CreateDelegate_internal(type, firstArgument, method, throwOnBindFailure);
						if (@delegate != null)
						{
							@delegate.original_method_info = method;
						}
						if (delegateData != null)
						{
							@delegate.data = delegateData;
						}
						return @delegate;
					}
					if (throwOnBindFailure)
					{
						throw new ArgumentException("method arguments are incompatible");
					}
					return null;
				}
			}
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x0005FD87 File Offset: 0x0005DF87
		public static Delegate CreateDelegate(Type type, object firstArgument, MethodInfo method)
		{
			return Delegate.CreateDelegate(type, firstArgument, method, true, true);
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x0005FD93 File Offset: 0x0005DF93
		public static Delegate CreateDelegate(Type type, MethodInfo method, bool throwOnBindFailure)
		{
			return Delegate.CreateDelegate(type, null, method, throwOnBindFailure, false);
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x0005FD9F File Offset: 0x0005DF9F
		public static Delegate CreateDelegate(Type type, MethodInfo method)
		{
			return Delegate.CreateDelegate(type, method, true);
		}

		// Token: 0x06001984 RID: 6532 RVA: 0x0005FDA9 File Offset: 0x0005DFA9
		public static Delegate CreateDelegate(Type type, object target, string method)
		{
			return Delegate.CreateDelegate(type, target, method, false);
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x0005FDB4 File Offset: 0x0005DFB4
		private static MethodInfo GetCandidateMethod(Type type, Type target, string method, BindingFlags bflags, bool ignoreCase, bool throwOnBindFailure)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			if (!type.IsSubclassOf(typeof(MulticastDelegate)))
			{
				throw new ArgumentException("type is not subclass of MulticastDelegate.");
			}
			MethodInfo methodInfo = type.GetMethod("Invoke");
			ParameterInfo[] parametersInternal = methodInfo.GetParametersInternal();
			Type[] array = new Type[parametersInternal.Length];
			for (int i = 0; i < parametersInternal.Length; i++)
			{
				array[i] = parametersInternal[i].ParameterType;
			}
			BindingFlags bindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.ExactBinding | bflags;
			if (ignoreCase)
			{
				bindingFlags |= BindingFlags.IgnoreCase;
			}
			MethodInfo methodInfo2 = null;
			Type type2 = target;
			while (type2 != null)
			{
				MethodInfo methodInfo3 = type2.GetMethod(method, bindingFlags, null, array, Array.Empty<ParameterModifier>());
				if (methodInfo3 != null && Delegate.return_type_match(methodInfo.ReturnType, methodInfo3.ReturnType))
				{
					methodInfo2 = methodInfo3;
					break;
				}
				type2 = type2.BaseType;
			}
			if (!(methodInfo2 == null))
			{
				return methodInfo2;
			}
			if (throwOnBindFailure)
			{
				throw new ArgumentException("Couldn't bind to method '" + method + "'.");
			}
			return null;
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x0005FEC4 File Offset: 0x0005E0C4
		public static Delegate CreateDelegate(Type type, Type target, string method, bool ignoreCase, bool throwOnBindFailure)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			MethodInfo candidateMethod = Delegate.GetCandidateMethod(type, target, method, BindingFlags.Static, ignoreCase, throwOnBindFailure);
			if (candidateMethod == null)
			{
				return null;
			}
			return Delegate.CreateDelegate_internal(type, null, candidateMethod, throwOnBindFailure);
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x0005FF07 File Offset: 0x0005E107
		public static Delegate CreateDelegate(Type type, Type target, string method)
		{
			return Delegate.CreateDelegate(type, target, method, false, true);
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x0005FF13 File Offset: 0x0005E113
		public static Delegate CreateDelegate(Type type, Type target, string method, bool ignoreCase)
		{
			return Delegate.CreateDelegate(type, target, method, ignoreCase, true);
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x0005FF20 File Offset: 0x0005E120
		public static Delegate CreateDelegate(Type type, object target, string method, bool ignoreCase, bool throwOnBindFailure)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			MethodInfo candidateMethod = Delegate.GetCandidateMethod(type, target.GetType(), method, BindingFlags.Instance, ignoreCase, throwOnBindFailure);
			if (candidateMethod == null)
			{
				return null;
			}
			return Delegate.CreateDelegate_internal(type, target, candidateMethod, throwOnBindFailure);
		}

		// Token: 0x0600198A RID: 6538 RVA: 0x0005FF62 File Offset: 0x0005E162
		public static Delegate CreateDelegate(Type type, object target, string method, bool ignoreCase)
		{
			return Delegate.CreateDelegate(type, target, method, ignoreCase, true);
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x0005FF6E File Offset: 0x0005E16E
		public object DynamicInvoke(params object[] args)
		{
			return this.DynamicInvokeImpl(args);
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x0005FF78 File Offset: 0x0005E178
		private void InitializeDelegateData()
		{
			DelegateData delegateData = new DelegateData();
			if (this.method_info.IsStatic)
			{
				if (this.m_target != null)
				{
					delegateData.curried_first_arg = true;
				}
				else if (base.GetType().GetMethod("Invoke").GetParametersCount() + 1 == this.method_info.GetParametersCount())
				{
					delegateData.curried_first_arg = true;
				}
			}
			this.data = delegateData;
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x0005FFDC File Offset: 0x0005E1DC
		protected virtual object DynamicInvokeImpl(object[] args)
		{
			if (this.Method == null)
			{
				Type[] array = new Type[args.Length];
				for (int i = 0; i < args.Length; i++)
				{
					array[i] = args[i].GetType();
				}
				this.method_info = this.m_target.GetType().GetMethod(this.data.method_name, array);
			}
			object obj = this.m_target;
			if (this.data == null)
			{
				this.InitializeDelegateData();
			}
			if (this.Method.IsStatic)
			{
				if (this.data.curried_first_arg)
				{
					if (args == null)
					{
						args = new object[] { obj };
					}
					else
					{
						Array.Resize<object>(ref args, args.Length + 1);
						Array.Copy(args, 0, args, 1, args.Length - 1);
						args[0] = obj;
					}
					obj = null;
				}
			}
			else if (this.m_target == null && args != null && args.Length != 0)
			{
				obj = args[0];
				Array.Copy(args, 1, args, 0, args.Length - 1);
				Array.Resize<object>(ref args, args.Length - 1);
			}
			return this.Method.Invoke(obj, args);
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x0001AB5D File Offset: 0x00018D5D
		public virtual object Clone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x000600D8 File Offset: 0x0005E2D8
		public override bool Equals(object obj)
		{
			Delegate @delegate = obj as Delegate;
			if (@delegate == null)
			{
				return false;
			}
			if (@delegate.m_target != this.m_target || !(@delegate.Method == this.Method))
			{
				return false;
			}
			if (@delegate.data == null && this.data == null)
			{
				return true;
			}
			if (@delegate.data != null && this.data != null)
			{
				return @delegate.data.target_type == this.data.target_type && @delegate.data.method_name == this.data.method_name;
			}
			if (@delegate.data != null)
			{
				return @delegate.data.target_type == null;
			}
			return this.data != null && this.data.target_type == null;
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x000601B0 File Offset: 0x0005E3B0
		public override int GetHashCode()
		{
			MethodInfo methodInfo = this.Method;
			return ((methodInfo != null) ? methodInfo.GetHashCode() : base.GetType().GetHashCode()) ^ RuntimeHelpers.GetHashCode(this.m_target);
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x000601EC File Offset: 0x0005E3EC
		protected virtual MethodInfo GetMethodImpl()
		{
			if (this.method_info != null)
			{
				return this.method_info;
			}
			if (this.method != IntPtr.Zero)
			{
				if (!this.method_is_virtual)
				{
					this.method_info = (MethodInfo)RuntimeMethodInfo.GetMethodFromHandleNoGenericCheck(new RuntimeMethodHandle(this.method));
				}
				else
				{
					this.method_info = this.GetVirtualMethod_internal();
				}
			}
			return this.method_info;
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x00060257 File Offset: 0x0005E457
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			DelegateSerializationHolder.GetDelegateData(this, info, context);
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x00060261 File Offset: 0x0005E461
		public virtual Delegate[] GetInvocationList()
		{
			return new Delegate[] { this };
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x00060270 File Offset: 0x0005E470
		public static Delegate Combine(Delegate a, Delegate b)
		{
			if (a == null)
			{
				return b;
			}
			if (b == null)
			{
				return a;
			}
			if (a.GetType() != b.GetType())
			{
				throw new ArgumentException(string.Format("Incompatible Delegate Types. First is {0} second is {1}.", a.GetType().FullName, b.GetType().FullName));
			}
			return a.CombineImpl(b);
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x000602C8 File Offset: 0x0005E4C8
		[ComVisible(true)]
		public static Delegate Combine(params Delegate[] delegates)
		{
			if (delegates == null)
			{
				return null;
			}
			Delegate @delegate = null;
			foreach (Delegate delegate2 in delegates)
			{
				@delegate = Delegate.Combine(@delegate, delegate2);
			}
			return @delegate;
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x000602F9 File Offset: 0x0005E4F9
		protected virtual Delegate CombineImpl(Delegate d)
		{
			throw new MulticastNotSupportedException(string.Empty);
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x00060308 File Offset: 0x0005E508
		public static Delegate Remove(Delegate source, Delegate value)
		{
			if (source == null)
			{
				return null;
			}
			if (value == null)
			{
				return source;
			}
			if (source.GetType() != value.GetType())
			{
				throw new ArgumentException(string.Format("Incompatible Delegate Types. First is {0} second is {1}.", source.GetType().FullName, value.GetType().FullName));
			}
			return source.RemoveImpl(value);
		}

		// Token: 0x06001998 RID: 6552 RVA: 0x0006035F File Offset: 0x0005E55F
		protected virtual Delegate RemoveImpl(Delegate d)
		{
			if (this.Equals(d))
			{
				return null;
			}
			return this;
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x00060370 File Offset: 0x0005E570
		public static Delegate RemoveAll(Delegate source, Delegate value)
		{
			Delegate @delegate = source;
			while ((source = Delegate.Remove(source, value)) != @delegate)
			{
				@delegate = source;
			}
			return @delegate;
		}

		// Token: 0x0600199A RID: 6554 RVA: 0x00060396 File Offset: 0x0005E596
		public static bool operator ==(Delegate d1, Delegate d2)
		{
			if (d1 == null)
			{
				return d2 == null;
			}
			return d2 != null && d1.Equals(d2);
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x000603AE File Offset: 0x0005E5AE
		public static bool operator !=(Delegate d1, Delegate d2)
		{
			return !(d1 == d2);
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x000603BA File Offset: 0x0005E5BA
		internal bool IsTransparentProxy()
		{
			return RemotingServices.IsTransparentProxy(this.m_target);
		}

		// Token: 0x0600199D RID: 6557 RVA: 0x000603C7 File Offset: 0x0005E5C7
		internal static Delegate CreateDelegateNoSecurityCheck(RuntimeType type, object firstArgument, MethodInfo method)
		{
			return Delegate.CreateDelegate_internal(type, firstArgument, method, true);
		}

		// Token: 0x0600199E RID: 6558
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern MulticastDelegate AllocDelegateLike_internal(Delegate d);

		// Token: 0x040015E4 RID: 5604
		private IntPtr method_ptr;

		// Token: 0x040015E5 RID: 5605
		private IntPtr invoke_impl;

		// Token: 0x040015E6 RID: 5606
		private object m_target;

		// Token: 0x040015E7 RID: 5607
		private IntPtr method;

		// Token: 0x040015E8 RID: 5608
		private IntPtr delegate_trampoline;

		// Token: 0x040015E9 RID: 5609
		private IntPtr extra_arg;

		// Token: 0x040015EA RID: 5610
		private IntPtr method_code;

		// Token: 0x040015EB RID: 5611
		private IntPtr interp_method;

		// Token: 0x040015EC RID: 5612
		private IntPtr interp_invoke_impl;

		// Token: 0x040015ED RID: 5613
		private MethodInfo method_info;

		// Token: 0x040015EE RID: 5614
		private MethodInfo original_method_info;

		// Token: 0x040015EF RID: 5615
		private DelegateData data;

		// Token: 0x040015F0 RID: 5616
		private bool method_is_virtual;
	}
}
