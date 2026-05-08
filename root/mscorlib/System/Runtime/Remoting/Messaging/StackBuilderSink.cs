using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Proxies;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200060A RID: 1546
	internal class StackBuilderSink : IMessageSink
	{
		// Token: 0x06003BB2 RID: 15282 RVA: 0x000D08F0 File Offset: 0x000CEAF0
		public StackBuilderSink(MarshalByRefObject obj, bool forceInternalExecute)
		{
			this._target = obj;
			if (!forceInternalExecute && RemotingServices.IsTransparentProxy(obj))
			{
				this._rp = RemotingServices.GetRealProxy(obj);
			}
		}

		// Token: 0x06003BB3 RID: 15283 RVA: 0x000D0916 File Offset: 0x000CEB16
		public IMessage SyncProcessMessage(IMessage msg)
		{
			this.CheckParameters(msg);
			if (this._rp != null)
			{
				return this._rp.Invoke(msg);
			}
			return RemotingServices.InternalExecuteMessage(this._target, (IMethodCallMessage)msg);
		}

		// Token: 0x06003BB4 RID: 15284 RVA: 0x000D0948 File Offset: 0x000CEB48
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			object[] array = new object[] { msg, replySink };
			ThreadPool.QueueUserWorkItem(delegate(object data)
			{
				try
				{
					this.ExecuteAsyncMessage(data);
				}
				catch
				{
				}
			}, array);
			return null;
		}

		// Token: 0x06003BB5 RID: 15285 RVA: 0x000D0978 File Offset: 0x000CEB78
		private void ExecuteAsyncMessage(object ob)
		{
			object[] array = (object[])ob;
			IMethodCallMessage methodCallMessage = (IMethodCallMessage)array[0];
			IMessageSink messageSink = (IMessageSink)array[1];
			this.CheckParameters(methodCallMessage);
			IMessage message;
			if (this._rp != null)
			{
				message = this._rp.Invoke(methodCallMessage);
			}
			else
			{
				message = RemotingServices.InternalExecuteMessage(this._target, methodCallMessage);
			}
			messageSink.SyncProcessMessage(message);
		}

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x06003BB6 RID: 15286 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public IMessageSink NextSink
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06003BB7 RID: 15287 RVA: 0x000D09D0 File Offset: 0x000CEBD0
		private void CheckParameters(IMessage msg)
		{
			IMethodCallMessage methodCallMessage = (IMethodCallMessage)msg;
			ParameterInfo[] parameters = methodCallMessage.MethodBase.GetParameters();
			int num = 0;
			foreach (ParameterInfo parameterInfo in parameters)
			{
				object arg = methodCallMessage.GetArg(num++);
				Type type = parameterInfo.ParameterType;
				if (type.IsByRef)
				{
					type = type.GetElementType();
				}
				if (arg != null && !type.IsInstanceOfType(arg))
				{
					throw new RemotingException(string.Concat(new string[]
					{
						"Cannot cast argument ",
						parameterInfo.Position.ToString(),
						" of type '",
						arg.GetType().AssemblyQualifiedName,
						"' to type '",
						type.AssemblyQualifiedName,
						"'"
					}));
				}
			}
		}

		// Token: 0x06003BB8 RID: 15288 RVA: 0x000D0AA0 File Offset: 0x000CECA0
		[CompilerGenerated]
		private void <AsyncProcessMessage>b__4_0(object data)
		{
			try
			{
				this.ExecuteAsyncMessage(data);
			}
			catch
			{
			}
		}

		// Token: 0x04002670 RID: 9840
		private MarshalByRefObject _target;

		// Token: 0x04002671 RID: 9841
		private RealProxy _rp;
	}
}
