using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x0200057F RID: 1407
	[MonoTODO("Handle domain unloading?")]
	internal class CrossAppDomainSink : IMessageSink
	{
		// Token: 0x060037F6 RID: 14326 RVA: 0x000C9DE3 File Offset: 0x000C7FE3
		internal CrossAppDomainSink(int domainID)
		{
			this._domainID = domainID;
		}

		// Token: 0x060037F7 RID: 14327 RVA: 0x000C9DF4 File Offset: 0x000C7FF4
		internal static CrossAppDomainSink GetSink(int domainID)
		{
			object syncRoot = CrossAppDomainSink.s_sinks.SyncRoot;
			CrossAppDomainSink crossAppDomainSink;
			lock (syncRoot)
			{
				if (CrossAppDomainSink.s_sinks.ContainsKey(domainID))
				{
					crossAppDomainSink = (CrossAppDomainSink)CrossAppDomainSink.s_sinks[domainID];
				}
				else
				{
					CrossAppDomainSink crossAppDomainSink2 = new CrossAppDomainSink(domainID);
					CrossAppDomainSink.s_sinks[domainID] = crossAppDomainSink2;
					crossAppDomainSink = crossAppDomainSink2;
				}
			}
			return crossAppDomainSink;
		}

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x060037F8 RID: 14328 RVA: 0x000C9E78 File Offset: 0x000C8078
		internal int TargetDomainId
		{
			get
			{
				return this._domainID;
			}
		}

		// Token: 0x060037F9 RID: 14329 RVA: 0x000C9E80 File Offset: 0x000C8080
		private static CrossAppDomainSink.ProcessMessageRes ProcessMessageInDomain(byte[] arrRequest, CADMethodCallMessage cadMsg)
		{
			CrossAppDomainSink.ProcessMessageRes processMessageRes = default(CrossAppDomainSink.ProcessMessageRes);
			try
			{
				AppDomain.CurrentDomain.ProcessMessageInDomain(arrRequest, cadMsg, out processMessageRes.arrResponse, out processMessageRes.cadMrm);
			}
			catch (Exception ex)
			{
				IMessage message = new MethodResponse(ex, new ErrorMessage());
				processMessageRes.arrResponse = CADSerializer.SerializeMessage(message).GetBuffer();
			}
			return processMessageRes;
		}

		// Token: 0x060037FA RID: 14330 RVA: 0x000C9EE4 File Offset: 0x000C80E4
		public virtual IMessage SyncProcessMessage(IMessage msgRequest)
		{
			IMessage message = null;
			try
			{
				byte[] array = null;
				byte[] array2 = null;
				CADMethodReturnMessage cadmethodReturnMessage = null;
				CADMethodCallMessage cadmethodCallMessage = CADMethodCallMessage.Create(msgRequest);
				if (cadmethodCallMessage == null)
				{
					array2 = CADSerializer.SerializeMessage(msgRequest).GetBuffer();
				}
				Context currentContext = Thread.CurrentContext;
				try
				{
					CrossAppDomainSink.ProcessMessageRes processMessageRes = (CrossAppDomainSink.ProcessMessageRes)AppDomain.InvokeInDomainByID(this._domainID, CrossAppDomainSink.processMessageMethod, null, new object[] { array2, cadmethodCallMessage });
					array = processMessageRes.arrResponse;
					cadmethodReturnMessage = processMessageRes.cadMrm;
				}
				finally
				{
					AppDomain.InternalSetContext(currentContext);
				}
				if (array != null)
				{
					message = CADSerializer.DeserializeMessage(new MemoryStream(array), msgRequest as IMethodCallMessage);
				}
				else
				{
					message = new MethodResponse(msgRequest as IMethodCallMessage, cadmethodReturnMessage);
				}
			}
			catch (Exception ex)
			{
				try
				{
					message = new ReturnMessage(ex, msgRequest as IMethodCallMessage);
				}
				catch (Exception)
				{
				}
			}
			return message;
		}

		// Token: 0x060037FB RID: 14331 RVA: 0x000C9FBC File Offset: 0x000C81BC
		public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
		{
			AsyncRequest asyncRequest = new AsyncRequest(reqMsg, replySink);
			ThreadPool.QueueUserWorkItem(delegate(object data)
			{
				try
				{
					this.SendAsyncMessage(data);
				}
				catch
				{
				}
			}, asyncRequest);
			return null;
		}

		// Token: 0x060037FC RID: 14332 RVA: 0x000C9FE8 File Offset: 0x000C81E8
		public void SendAsyncMessage(object data)
		{
			AsyncRequest asyncRequest = (AsyncRequest)data;
			IMessage message = this.SyncProcessMessage(asyncRequest.MsgRequest);
			asyncRequest.ReplySink.SyncProcessMessage(message);
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x060037FD RID: 14333 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public IMessageSink NextSink
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060037FE RID: 14334 RVA: 0x000CA016 File Offset: 0x000C8216
		// Note: this type is marked as 'beforefieldinit'.
		static CrossAppDomainSink()
		{
		}

		// Token: 0x060037FF RID: 14335 RVA: 0x000CA040 File Offset: 0x000C8240
		[CompilerGenerated]
		private void <AsyncProcessMessage>b__10_0(object data)
		{
			try
			{
				this.SendAsyncMessage(data);
			}
			catch
			{
			}
		}

		// Token: 0x04002566 RID: 9574
		private static Hashtable s_sinks = new Hashtable();

		// Token: 0x04002567 RID: 9575
		private static MethodInfo processMessageMethod = typeof(CrossAppDomainSink).GetMethod("ProcessMessageInDomain", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x04002568 RID: 9576
		private int _domainID;

		// Token: 0x02000580 RID: 1408
		private struct ProcessMessageRes
		{
			// Token: 0x04002569 RID: 9577
			public byte[] arrResponse;

			// Token: 0x0400256A RID: 9578
			public CADMethodReturnMessage cadMrm;
		}
	}
}
