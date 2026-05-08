using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Mono;

namespace System.Reflection
{
	// Token: 0x02000869 RID: 2153
	[Serializable]
	public abstract class EventInfo : MemberInfo
	{
		// Token: 0x06004824 RID: 18468 RVA: 0x00047D3C File Offset: 0x00045F3C
		protected EventInfo()
		{
		}

		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x06004825 RID: 18469 RVA: 0x00015289 File Offset: 0x00013489
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Event;
			}
		}

		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x06004826 RID: 18470
		public abstract EventAttributes Attributes { get; }

		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x06004827 RID: 18471 RVA: 0x000EDC86 File Offset: 0x000EBE86
		public bool IsSpecialName
		{
			get
			{
				return (this.Attributes & EventAttributes.SpecialName) > EventAttributes.None;
			}
		}

		// Token: 0x06004828 RID: 18472 RVA: 0x000EDC97 File Offset: 0x000EBE97
		public MethodInfo[] GetOtherMethods()
		{
			return this.GetOtherMethods(false);
		}

		// Token: 0x06004829 RID: 18473 RVA: 0x00047D5E File Offset: 0x00045F5E
		public virtual MethodInfo[] GetOtherMethods(bool nonPublic)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x0600482A RID: 18474 RVA: 0x000EDCA0 File Offset: 0x000EBEA0
		public virtual MethodInfo AddMethod
		{
			get
			{
				return this.GetAddMethod(true);
			}
		}

		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x0600482B RID: 18475 RVA: 0x000EDCA9 File Offset: 0x000EBEA9
		public virtual MethodInfo RemoveMethod
		{
			get
			{
				return this.GetRemoveMethod(true);
			}
		}

		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x0600482C RID: 18476 RVA: 0x000EDCB2 File Offset: 0x000EBEB2
		public virtual MethodInfo RaiseMethod
		{
			get
			{
				return this.GetRaiseMethod(true);
			}
		}

		// Token: 0x0600482D RID: 18477 RVA: 0x000EDCBB File Offset: 0x000EBEBB
		public MethodInfo GetAddMethod()
		{
			return this.GetAddMethod(false);
		}

		// Token: 0x0600482E RID: 18478 RVA: 0x000EDCC4 File Offset: 0x000EBEC4
		public MethodInfo GetRemoveMethod()
		{
			return this.GetRemoveMethod(false);
		}

		// Token: 0x0600482F RID: 18479 RVA: 0x000EDCCD File Offset: 0x000EBECD
		public MethodInfo GetRaiseMethod()
		{
			return this.GetRaiseMethod(false);
		}

		// Token: 0x06004830 RID: 18480
		public abstract MethodInfo GetAddMethod(bool nonPublic);

		// Token: 0x06004831 RID: 18481
		public abstract MethodInfo GetRemoveMethod(bool nonPublic);

		// Token: 0x06004832 RID: 18482
		public abstract MethodInfo GetRaiseMethod(bool nonPublic);

		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x06004833 RID: 18483 RVA: 0x000EDCD8 File Offset: 0x000EBED8
		public virtual bool IsMulticast
		{
			get
			{
				Type eventHandlerType = this.EventHandlerType;
				return typeof(MulticastDelegate).IsAssignableFrom(eventHandlerType);
			}
		}

		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x06004834 RID: 18484 RVA: 0x000EDCFC File Offset: 0x000EBEFC
		public virtual Type EventHandlerType
		{
			get
			{
				ParameterInfo[] parametersInternal = this.GetAddMethod(true).GetParametersInternal();
				Type typeFromHandle = typeof(Delegate);
				for (int i = 0; i < parametersInternal.Length; i++)
				{
					Type parameterType = parametersInternal[i].ParameterType;
					if (parameterType.IsSubclassOf(typeFromHandle))
					{
						return parameterType;
					}
				}
				return null;
			}
		}

		// Token: 0x06004835 RID: 18485 RVA: 0x000EDD44 File Offset: 0x000EBF44
		[DebuggerHidden]
		[DebuggerStepThrough]
		public virtual void RemoveEventHandler(object target, Delegate handler)
		{
			MethodInfo removeMethod = this.GetRemoveMethod(false);
			if (removeMethod == null)
			{
				throw new InvalidOperationException("Cannot remove the event handler since no public remove method exists for the event.");
			}
			if (removeMethod.GetParametersNoCopy()[0].ParameterType == typeof(EventRegistrationToken))
			{
				throw new InvalidOperationException("Adding or removing event handlers dynamically is not supported on WinRT events.");
			}
			removeMethod.Invoke(target, new object[] { handler });
		}

		// Token: 0x06004836 RID: 18486 RVA: 0x000EDDA6 File Offset: 0x000EBFA6
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06004837 RID: 18487 RVA: 0x000EDDAF File Offset: 0x000EBFAF
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06004838 RID: 18488 RVA: 0x00064F2C File Offset: 0x0006312C
		public static bool operator ==(EventInfo left, EventInfo right)
		{
			return left == right || (left != null && right != null && left.Equals(right));
		}

		// Token: 0x06004839 RID: 18489 RVA: 0x000EDDB7 File Offset: 0x000EBFB7
		public static bool operator !=(EventInfo left, EventInfo right)
		{
			return !(left == right);
		}

		// Token: 0x0600483A RID: 18490 RVA: 0x000EDDC4 File Offset: 0x000EBFC4
		[DebuggerHidden]
		[DebuggerStepThrough]
		public virtual void AddEventHandler(object target, Delegate handler)
		{
			if (this.cached_add_event == null)
			{
				MethodInfo addMethod = this.GetAddMethod();
				if (addMethod == null)
				{
					throw new InvalidOperationException("Cannot add the event handler since no public add method exists for the event.");
				}
				if (addMethod.DeclaringType.IsValueType)
				{
					if (target == null && !addMethod.IsStatic)
					{
						throw new TargetException("Cannot add a handler to a non static event with a null target");
					}
					addMethod.Invoke(target, new object[] { handler });
					return;
				}
				else
				{
					this.cached_add_event = EventInfo.CreateAddEventDelegate(addMethod);
				}
			}
			this.cached_add_event(target, handler);
		}

		// Token: 0x0600483B RID: 18491 RVA: 0x000EDE44 File Offset: 0x000EC044
		private static void AddEventFrame<T, D>(EventInfo.AddEvent<T, D> addEvent, object obj, object dele)
		{
			if (obj == null)
			{
				throw new TargetException("Cannot add a handler to a non static event with a null target");
			}
			if (!(obj is T))
			{
				throw new TargetException("Object doesn't match target");
			}
			if (!(dele is D))
			{
				throw new ArgumentException(string.Format("Object of type {0} cannot be converted to type {1}.", dele.GetType(), typeof(D)));
			}
			addEvent((T)((object)obj), (D)((object)dele));
		}

		// Token: 0x0600483C RID: 18492 RVA: 0x000EDEAC File Offset: 0x000EC0AC
		private static void StaticAddEventAdapterFrame<D>(EventInfo.StaticAddEvent<D> addEvent, object obj, object dele)
		{
			addEvent((D)((object)dele));
		}

		// Token: 0x0600483D RID: 18493 RVA: 0x000EDEBC File Offset: 0x000EC0BC
		private static EventInfo.AddEventAdapter CreateAddEventDelegate(MethodInfo method)
		{
			Type[] array;
			Type type;
			string text;
			if (method.IsStatic)
			{
				array = new Type[] { method.GetParametersInternal()[0].ParameterType };
				type = typeof(EventInfo.StaticAddEvent<>);
				text = "StaticAddEventAdapterFrame";
			}
			else
			{
				array = new Type[]
				{
					method.DeclaringType,
					method.GetParametersInternal()[0].ParameterType
				};
				type = typeof(EventInfo.AddEvent<, >);
				text = "AddEventFrame";
			}
			object obj = Delegate.CreateDelegate(type.MakeGenericType(array), method);
			MethodInfo methodInfo = typeof(EventInfo).GetMethod(text, BindingFlags.Static | BindingFlags.NonPublic);
			methodInfo = methodInfo.MakeGenericMethod(array);
			return (EventInfo.AddEventAdapter)Delegate.CreateDelegate(typeof(EventInfo.AddEventAdapter), obj, methodInfo, true);
		}

		// Token: 0x0600483E RID: 18494
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern EventInfo internal_from_handle_type(IntPtr event_handle, IntPtr type_handle);

		// Token: 0x0600483F RID: 18495 RVA: 0x000EDF70 File Offset: 0x000EC170
		internal static EventInfo GetEventFromHandle(RuntimeEventHandle handle, RuntimeTypeHandle reflectedType)
		{
			if (handle.Value == IntPtr.Zero)
			{
				throw new ArgumentException("The handle is invalid.");
			}
			EventInfo eventInfo = EventInfo.internal_from_handle_type(handle.Value, reflectedType.Value);
			if (eventInfo == null)
			{
				throw new ArgumentException("The event handle and the type handle are incompatible.");
			}
			return eventInfo;
		}

		// Token: 0x04002E0B RID: 11787
		private EventInfo.AddEventAdapter cached_add_event;

		// Token: 0x0200086A RID: 2154
		// (Invoke) Token: 0x06004841 RID: 18497
		private delegate void AddEventAdapter(object _this, Delegate dele);

		// Token: 0x0200086B RID: 2155
		// (Invoke) Token: 0x06004845 RID: 18501
		private delegate void AddEvent<T, D>(T _this, D dele);

		// Token: 0x0200086C RID: 2156
		// (Invoke) Token: 0x06004849 RID: 18505
		private delegate void StaticAddEvent<D>(D dele);
	}
}
