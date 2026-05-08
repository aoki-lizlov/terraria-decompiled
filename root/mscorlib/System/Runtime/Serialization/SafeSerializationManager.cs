using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;

namespace System.Runtime.Serialization
{
	// Token: 0x0200063C RID: 1596
	[Serializable]
	internal sealed class SafeSerializationManager : IObjectReference, ISerializable
	{
		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06003CF0 RID: 15600 RVA: 0x000D41D0 File Offset: 0x000D23D0
		// (remove) Token: 0x06003CF1 RID: 15601 RVA: 0x000D4208 File Offset: 0x000D2408
		internal event EventHandler<SafeSerializationEventArgs> SerializeObjectState
		{
			[CompilerGenerated]
			add
			{
				EventHandler<SafeSerializationEventArgs> eventHandler = this.SerializeObjectState;
				EventHandler<SafeSerializationEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<SafeSerializationEventArgs> eventHandler3 = (EventHandler<SafeSerializationEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<SafeSerializationEventArgs>>(ref this.SerializeObjectState, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<SafeSerializationEventArgs> eventHandler = this.SerializeObjectState;
				EventHandler<SafeSerializationEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<SafeSerializationEventArgs> eventHandler3 = (EventHandler<SafeSerializationEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<SafeSerializationEventArgs>>(ref this.SerializeObjectState, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x06003CF2 RID: 15602 RVA: 0x000025BE File Offset: 0x000007BE
		internal SafeSerializationManager()
		{
		}

		// Token: 0x06003CF3 RID: 15603 RVA: 0x000D4240 File Offset: 0x000D2440
		[SecurityCritical]
		private SafeSerializationManager(SerializationInfo info, StreamingContext context)
		{
			RuntimeType runtimeType = info.GetValueNoThrow("CLR_SafeSerializationManager_RealType", typeof(RuntimeType)) as RuntimeType;
			if (runtimeType == null)
			{
				this.m_serializedStates = info.GetValue("m_serializedStates", typeof(List<object>)) as List<object>;
				return;
			}
			this.m_realType = runtimeType;
			this.m_savedSerializationInfo = info;
		}

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x06003CF4 RID: 15604 RVA: 0x000D42A6 File Offset: 0x000D24A6
		internal bool IsActive
		{
			get
			{
				return this.SerializeObjectState != null;
			}
		}

		// Token: 0x06003CF5 RID: 15605 RVA: 0x000D42B4 File Offset: 0x000D24B4
		[SecurityCritical]
		internal void CompleteSerialization(object serializedObject, SerializationInfo info, StreamingContext context)
		{
			this.m_serializedStates = null;
			EventHandler<SafeSerializationEventArgs> serializeObjectState = this.SerializeObjectState;
			if (serializeObjectState != null)
			{
				SafeSerializationEventArgs safeSerializationEventArgs = new SafeSerializationEventArgs(context);
				serializeObjectState(serializedObject, safeSerializationEventArgs);
				this.m_serializedStates = safeSerializationEventArgs.SerializedStates;
				info.AddValue("CLR_SafeSerializationManager_RealType", serializedObject.GetType(), typeof(RuntimeType));
				info.SetType(typeof(SafeSerializationManager));
			}
		}

		// Token: 0x06003CF6 RID: 15606 RVA: 0x000D4318 File Offset: 0x000D2518
		internal void CompleteDeserialization(object deserializedObject)
		{
			if (this.m_serializedStates != null)
			{
				foreach (object obj in this.m_serializedStates)
				{
					((ISafeSerializationData)obj).CompleteDeserialization(deserializedObject);
				}
			}
		}

		// Token: 0x06003CF7 RID: 15607 RVA: 0x000D4370 File Offset: 0x000D2570
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("m_serializedStates", this.m_serializedStates, typeof(List<IDeserializationCallback>));
		}

		// Token: 0x06003CF8 RID: 15608 RVA: 0x000D4390 File Offset: 0x000D2590
		[SecurityCritical]
		object IObjectReference.GetRealObject(StreamingContext context)
		{
			if (this.m_realObject != null)
			{
				return this.m_realObject;
			}
			if (this.m_realType == null)
			{
				return this;
			}
			Stack stack = new Stack();
			RuntimeType runtimeType = this.m_realType;
			do
			{
				stack.Push(runtimeType);
				runtimeType = runtimeType.BaseType as RuntimeType;
			}
			while (runtimeType != typeof(object));
			RuntimeType runtimeType2;
			RuntimeConstructorInfo runtimeConstructorInfo;
			do
			{
				runtimeType2 = runtimeType;
				runtimeType = stack.Pop() as RuntimeType;
				runtimeConstructorInfo = runtimeType.GetSerializationCtor();
			}
			while (runtimeConstructorInfo != null && runtimeConstructorInfo.IsSecurityCritical);
			runtimeConstructorInfo = ObjectManager.GetConstructor(runtimeType2);
			object uninitializedObject = FormatterServices.GetUninitializedObject(this.m_realType);
			runtimeConstructorInfo.SerializationInvoke(uninitializedObject, this.m_savedSerializationInfo, context);
			this.m_savedSerializationInfo = null;
			this.m_realType = null;
			this.m_realObject = uninitializedObject;
			return uninitializedObject;
		}

		// Token: 0x06003CF9 RID: 15609 RVA: 0x000D4453 File Offset: 0x000D2653
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			if (this.m_realObject != null)
			{
				SerializationEventsCache.GetSerializationEventsForType(this.m_realObject.GetType()).InvokeOnDeserialized(this.m_realObject, context);
				this.m_realObject = null;
			}
		}

		// Token: 0x040026FF RID: 9983
		private IList<object> m_serializedStates;

		// Token: 0x04002700 RID: 9984
		private SerializationInfo m_savedSerializationInfo;

		// Token: 0x04002701 RID: 9985
		private object m_realObject;

		// Token: 0x04002702 RID: 9986
		private RuntimeType m_realType;

		// Token: 0x04002703 RID: 9987
		[CompilerGenerated]
		private EventHandler<SafeSerializationEventArgs> SerializeObjectState;

		// Token: 0x04002704 RID: 9988
		private const string RealTypeSerializationName = "CLR_SafeSerializationManager_RealType";
	}
}
