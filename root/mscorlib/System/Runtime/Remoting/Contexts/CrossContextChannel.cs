using System;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x02000562 RID: 1378
	internal class CrossContextChannel : IMessageSink
	{
		// Token: 0x0600375B RID: 14171 RVA: 0x000C8530 File Offset: 0x000C6730
		public IMessage SyncProcessMessage(IMessage msg)
		{
			ServerIdentity serverIdentity = (ServerIdentity)RemotingServices.GetMessageTargetIdentity(msg);
			Context context = null;
			if (Thread.CurrentContext != serverIdentity.Context)
			{
				context = Context.SwitchToContext(serverIdentity.Context);
			}
			IMessage message;
			try
			{
				Context.NotifyGlobalDynamicSinks(true, msg, false, false);
				Thread.CurrentContext.NotifyDynamicSinks(true, msg, false, false);
				message = serverIdentity.Context.GetServerContextSinkChain().SyncProcessMessage(msg);
				Context.NotifyGlobalDynamicSinks(false, msg, false, false);
				Thread.CurrentContext.NotifyDynamicSinks(false, msg, false, false);
			}
			catch (Exception ex)
			{
				message = new ReturnMessage(ex, (IMethodCallMessage)msg);
			}
			finally
			{
				if (context != null)
				{
					Context.SwitchToContext(context);
				}
			}
			return message;
		}

		// Token: 0x0600375C RID: 14172 RVA: 0x000C85E0 File Offset: 0x000C67E0
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			ServerIdentity serverIdentity = (ServerIdentity)RemotingServices.GetMessageTargetIdentity(msg);
			Context context = null;
			if (Thread.CurrentContext != serverIdentity.Context)
			{
				context = Context.SwitchToContext(serverIdentity.Context);
			}
			IMessageCtrl messageCtrl2;
			try
			{
				Context.NotifyGlobalDynamicSinks(true, msg, false, true);
				Thread.CurrentContext.NotifyDynamicSinks(true, msg, false, false);
				if (replySink != null)
				{
					replySink = new CrossContextChannel.ContextRestoreSink(replySink, context, msg);
				}
				IMessageCtrl messageCtrl = serverIdentity.AsyncObjectProcessMessage(msg, replySink);
				if (replySink == null)
				{
					Context.NotifyGlobalDynamicSinks(false, msg, false, false);
					Thread.CurrentContext.NotifyDynamicSinks(false, msg, false, false);
				}
				messageCtrl2 = messageCtrl;
			}
			catch (Exception ex)
			{
				if (replySink != null)
				{
					replySink.SyncProcessMessage(new ReturnMessage(ex, (IMethodCallMessage)msg));
				}
				messageCtrl2 = null;
			}
			finally
			{
				if (context != null)
				{
					Context.SwitchToContext(context);
				}
			}
			return messageCtrl2;
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x0600375D RID: 14173 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public IMessageSink NextSink
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600375E RID: 14174 RVA: 0x000025BE File Offset: 0x000007BE
		public CrossContextChannel()
		{
		}

		// Token: 0x02000563 RID: 1379
		private class ContextRestoreSink : IMessageSink
		{
			// Token: 0x0600375F RID: 14175 RVA: 0x000C86A4 File Offset: 0x000C68A4
			public ContextRestoreSink(IMessageSink next, Context context, IMessage call)
			{
				this._next = next;
				this._context = context;
				this._call = call;
			}

			// Token: 0x06003760 RID: 14176 RVA: 0x000C86C4 File Offset: 0x000C68C4
			public IMessage SyncProcessMessage(IMessage msg)
			{
				IMessage message;
				try
				{
					Context.NotifyGlobalDynamicSinks(false, msg, false, false);
					Thread.CurrentContext.NotifyDynamicSinks(false, msg, false, false);
					message = this._next.SyncProcessMessage(msg);
				}
				catch (Exception ex)
				{
					message = new ReturnMessage(ex, (IMethodCallMessage)this._call);
				}
				finally
				{
					if (this._context != null)
					{
						Context.SwitchToContext(this._context);
					}
				}
				return message;
			}

			// Token: 0x06003761 RID: 14177 RVA: 0x00047E00 File Offset: 0x00046000
			public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
			{
				throw new NotSupportedException();
			}

			// Token: 0x170007C9 RID: 1993
			// (get) Token: 0x06003762 RID: 14178 RVA: 0x000C873C File Offset: 0x000C693C
			public IMessageSink NextSink
			{
				get
				{
					return this._next;
				}
			}

			// Token: 0x04002539 RID: 9529
			private IMessageSink _next;

			// Token: 0x0400253A RID: 9530
			private Context _context;

			// Token: 0x0400253B RID: 9531
			private IMessage _call;
		}
	}
}
