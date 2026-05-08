using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Threading;

namespace System.Runtime.Remoting.Proxies
{
	// Token: 0x02000551 RID: 1361
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public abstract class RealProxy
	{
		// Token: 0x060036BB RID: 14011 RVA: 0x000C6977 File Offset: 0x000C4B77
		protected RealProxy()
		{
		}

		// Token: 0x060036BC RID: 14012 RVA: 0x000C6986 File Offset: 0x000C4B86
		protected RealProxy(Type classToProxy)
			: this(classToProxy, IntPtr.Zero, null)
		{
		}

		// Token: 0x060036BD RID: 14013 RVA: 0x000C6995 File Offset: 0x000C4B95
		internal RealProxy(Type classToProxy, ClientIdentity identity)
			: this(classToProxy, IntPtr.Zero, null)
		{
			this._objectIdentity = identity;
		}

		// Token: 0x060036BE RID: 14014 RVA: 0x000C69AC File Offset: 0x000C4BAC
		protected RealProxy(Type classToProxy, IntPtr stub, object stubData)
		{
			if (!classToProxy.IsMarshalByRef && !classToProxy.IsInterface)
			{
				throw new ArgumentException("object must be MarshalByRef");
			}
			this.class_to_proxy = classToProxy;
			if (stub != IntPtr.Zero)
			{
				throw new NotSupportedException("stub is not used in Mono");
			}
		}

		// Token: 0x060036BF RID: 14015
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Type InternalGetProxyType(object transparentProxy);

		// Token: 0x060036C0 RID: 14016 RVA: 0x000C6A00 File Offset: 0x000C4C00
		public Type GetProxiedType()
		{
			if (this._objTP != null)
			{
				return RealProxy.InternalGetProxyType(this._objTP);
			}
			if (this.class_to_proxy.IsInterface)
			{
				return typeof(MarshalByRefObject);
			}
			return this.class_to_proxy;
		}

		// Token: 0x060036C1 RID: 14017 RVA: 0x000C6A34 File Offset: 0x000C4C34
		public virtual ObjRef CreateObjRef(Type requestedType)
		{
			return RemotingServices.Marshal((MarshalByRefObject)this.GetTransparentProxy(), null, requestedType);
		}

		// Token: 0x060036C2 RID: 14018 RVA: 0x000C6A48 File Offset: 0x000C4C48
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RemotingServices.GetObjectData(this.GetTransparentProxy(), info, context);
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x060036C3 RID: 14019 RVA: 0x000C6A57 File Offset: 0x000C4C57
		// (set) Token: 0x060036C4 RID: 14020 RVA: 0x000C6A5F File Offset: 0x000C4C5F
		internal Identity ObjectIdentity
		{
			get
			{
				return this._objectIdentity;
			}
			set
			{
				this._objectIdentity = value;
			}
		}

		// Token: 0x060036C5 RID: 14021 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO]
		public virtual IntPtr GetCOMIUnknown(bool fIsMarshalled)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060036C6 RID: 14022 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO]
		public virtual void SetCOMIUnknown(IntPtr i)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060036C7 RID: 14023 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO]
		public virtual IntPtr SupportsInterface(ref Guid iid)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060036C8 RID: 14024 RVA: 0x000C6A68 File Offset: 0x000C4C68
		public static object GetStubData(RealProxy rp)
		{
			return rp._stubData;
		}

		// Token: 0x060036C9 RID: 14025 RVA: 0x000C6A70 File Offset: 0x000C4C70
		public static void SetStubData(RealProxy rp, object stubData)
		{
			rp._stubData = stubData;
		}

		// Token: 0x060036CA RID: 14026
		public abstract IMessage Invoke(IMessage msg);

		// Token: 0x060036CB RID: 14027 RVA: 0x000C6A7C File Offset: 0x000C4C7C
		internal static object PrivateInvoke(RealProxy rp, IMessage msg, out Exception exc, out object[] out_args)
		{
			MonoMethodMessage monoMethodMessage = (MonoMethodMessage)msg;
			monoMethodMessage.LogicalCallContext = Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext;
			CallType callType = monoMethodMessage.CallType;
			bool flag = rp is RemotingProxy;
			out_args = null;
			IMethodReturnMessage methodReturnMessage = null;
			if (callType == CallType.BeginInvoke)
			{
				monoMethodMessage.AsyncResult.CallMessage = monoMethodMessage;
			}
			if (callType == CallType.EndInvoke)
			{
				methodReturnMessage = (IMethodReturnMessage)monoMethodMessage.AsyncResult.EndInvoke();
			}
			if (monoMethodMessage.MethodBase.IsConstructor)
			{
				if (flag)
				{
					methodReturnMessage = (IMethodReturnMessage)(rp as RemotingProxy).ActivateRemoteObject((IMethodMessage)msg);
				}
				else
				{
					msg = new ConstructionCall(rp.GetProxiedType());
				}
			}
			if (methodReturnMessage == null)
			{
				bool flag2 = false;
				try
				{
					methodReturnMessage = (IMethodReturnMessage)rp.Invoke(msg);
				}
				catch (Exception ex)
				{
					flag2 = true;
					if (callType != CallType.BeginInvoke)
					{
						throw;
					}
					monoMethodMessage.AsyncResult.SyncProcessMessage(new ReturnMessage(ex, msg as IMethodCallMessage));
					methodReturnMessage = new ReturnMessage(null, null, 0, null, msg as IMethodCallMessage);
				}
				if (!flag && callType == CallType.BeginInvoke && !flag2)
				{
					object obj = monoMethodMessage.AsyncResult.SyncProcessMessage(methodReturnMessage);
					out_args = methodReturnMessage.OutArgs;
					methodReturnMessage = new ReturnMessage(obj, null, 0, null, methodReturnMessage as IMethodCallMessage);
				}
			}
			if (methodReturnMessage.LogicalCallContext != null && methodReturnMessage.LogicalCallContext.HasInfo)
			{
				Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext.Merge(methodReturnMessage.LogicalCallContext);
			}
			exc = methodReturnMessage.Exception;
			if (exc != null)
			{
				out_args = null;
				throw exc.FixRemotingException();
			}
			if (methodReturnMessage is IConstructionReturnMessage)
			{
				if (out_args == null)
				{
					out_args = methodReturnMessage.OutArgs;
				}
			}
			else if (monoMethodMessage.CallType != CallType.BeginInvoke)
			{
				if (monoMethodMessage.CallType == CallType.Sync)
				{
					out_args = RealProxy.ProcessResponse(methodReturnMessage, monoMethodMessage);
				}
				else if (monoMethodMessage.CallType == CallType.EndInvoke)
				{
					out_args = RealProxy.ProcessResponse(methodReturnMessage, monoMethodMessage.AsyncResult.CallMessage);
				}
				else if (out_args == null)
				{
					out_args = methodReturnMessage.OutArgs;
				}
			}
			return methodReturnMessage.ReturnValue;
		}

		// Token: 0x060036CC RID: 14028
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal virtual extern object InternalGetTransparentProxy(string className);

		// Token: 0x060036CD RID: 14029 RVA: 0x000C6C4C File Offset: 0x000C4E4C
		public virtual object GetTransparentProxy()
		{
			if (this._objTP == null)
			{
				IRemotingTypeInfo remotingTypeInfo = this as IRemotingTypeInfo;
				string text;
				if (remotingTypeInfo != null)
				{
					text = remotingTypeInfo.TypeName;
					if (text == null || text == typeof(MarshalByRefObject).AssemblyQualifiedName)
					{
						text = this.class_to_proxy.AssemblyQualifiedName;
					}
				}
				else
				{
					text = this.class_to_proxy.AssemblyQualifiedName;
				}
				this._objTP = this.InternalGetTransparentProxy(text);
			}
			return this._objTP;
		}

		// Token: 0x060036CE RID: 14030 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO]
		[ComVisible(true)]
		public IConstructionReturnMessage InitializeServerObject(IConstructionCallMessage ctorMsg)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060036CF RID: 14031 RVA: 0x000C6CB9 File Offset: 0x000C4EB9
		protected void AttachServer(MarshalByRefObject s)
		{
			this._server = s;
		}

		// Token: 0x060036D0 RID: 14032 RVA: 0x000C6CC2 File Offset: 0x000C4EC2
		protected MarshalByRefObject DetachServer()
		{
			MarshalByRefObject server = this._server;
			this._server = null;
			return server;
		}

		// Token: 0x060036D1 RID: 14033 RVA: 0x000C6CD1 File Offset: 0x000C4ED1
		protected MarshalByRefObject GetUnwrappedServer()
		{
			return this._server;
		}

		// Token: 0x060036D2 RID: 14034 RVA: 0x000C6CD9 File Offset: 0x000C4ED9
		internal void SetTargetDomain(int domainId)
		{
			this._targetDomainId = domainId;
		}

		// Token: 0x060036D3 RID: 14035 RVA: 0x000C6CE4 File Offset: 0x000C4EE4
		internal object GetAppDomainTarget()
		{
			if (this._server == null)
			{
				ClientActivatedIdentity clientActivatedIdentity = RemotingServices.GetIdentityForUri(this._targetUri) as ClientActivatedIdentity;
				if (clientActivatedIdentity == null)
				{
					throw new RemotingException("Server for uri '" + this._targetUri + "' not found");
				}
				this._server = clientActivatedIdentity.GetServerObject();
			}
			return this._server;
		}

		// Token: 0x060036D4 RID: 14036 RVA: 0x000C6D3C File Offset: 0x000C4F3C
		private static object[] ProcessResponse(IMethodReturnMessage mrm, MonoMethodMessage call)
		{
			MethodInfo methodInfo = (MethodInfo)call.MethodBase;
			if (mrm.ReturnValue != null && !methodInfo.ReturnType.IsInstanceOfType(mrm.ReturnValue))
			{
				throw new InvalidCastException("Return value has an invalid type");
			}
			int num;
			if (call.NeedsOutProcessing(out num))
			{
				ParameterInfo[] parameters = methodInfo.GetParameters();
				object[] array = new object[num];
				int num2 = 0;
				foreach (ParameterInfo parameterInfo in parameters)
				{
					if (parameterInfo.IsOut && !parameterInfo.ParameterType.IsByRef)
					{
						object obj = ((parameterInfo.Position < mrm.ArgCount) ? mrm.GetArg(parameterInfo.Position) : null);
						if (obj != null)
						{
							object arg = call.GetArg(parameterInfo.Position);
							if (arg == null)
							{
								throw new RemotingException("Unexpected null value in local out parameter '" + parameterInfo.Name + "'");
							}
							RemotingServices.UpdateOutArgObject(parameterInfo, arg, obj);
						}
					}
					else if (parameterInfo.ParameterType.IsByRef)
					{
						object obj2 = ((parameterInfo.Position < mrm.ArgCount) ? mrm.GetArg(parameterInfo.Position) : null);
						if (obj2 != null && !parameterInfo.ParameterType.GetElementType().IsInstanceOfType(obj2))
						{
							throw new InvalidCastException("Return argument '" + parameterInfo.Name + "' has an invalid type");
						}
						array[num2++] = obj2;
					}
				}
				return array;
			}
			return new object[0];
		}

		// Token: 0x040024FF RID: 9471
		private Type class_to_proxy;

		// Token: 0x04002500 RID: 9472
		internal Context _targetContext;

		// Token: 0x04002501 RID: 9473
		internal MarshalByRefObject _server;

		// Token: 0x04002502 RID: 9474
		private int _targetDomainId = -1;

		// Token: 0x04002503 RID: 9475
		internal string _targetUri;

		// Token: 0x04002504 RID: 9476
		internal Identity _objectIdentity;

		// Token: 0x04002505 RID: 9477
		private object _objTP;

		// Token: 0x04002506 RID: 9478
		private object _stubData;
	}
}
