using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x0200057C RID: 1404
	[ComVisible(true)]
	public class ClientChannelSinkStack : IClientChannelSinkStack, IClientResponseChannelSinkStack
	{
		// Token: 0x060037E1 RID: 14305 RVA: 0x000025BE File Offset: 0x000007BE
		public ClientChannelSinkStack()
		{
		}

		// Token: 0x060037E2 RID: 14306 RVA: 0x000C9BF4 File Offset: 0x000C7DF4
		public ClientChannelSinkStack(IMessageSink replySink)
		{
			this._replySink = replySink;
		}

		// Token: 0x060037E3 RID: 14307 RVA: 0x000C9C04 File Offset: 0x000C7E04
		public void AsyncProcessResponse(ITransportHeaders headers, Stream stream)
		{
			if (this._sinkStack == null)
			{
				throw new RemotingException("The current sink stack is empty");
			}
			ChanelSinkStackEntry sinkStack = this._sinkStack;
			this._sinkStack = this._sinkStack.Next;
			((IClientChannelSink)sinkStack.Sink).AsyncProcessResponse(this, sinkStack.State, headers, stream);
		}

		// Token: 0x060037E4 RID: 14308 RVA: 0x000C9C55 File Offset: 0x000C7E55
		public void DispatchException(Exception e)
		{
			this.DispatchReplyMessage(new ReturnMessage(e, null));
		}

		// Token: 0x060037E5 RID: 14309 RVA: 0x000C9C64 File Offset: 0x000C7E64
		public void DispatchReplyMessage(IMessage msg)
		{
			if (this._replySink != null)
			{
				this._replySink.SyncProcessMessage(msg);
			}
		}

		// Token: 0x060037E6 RID: 14310 RVA: 0x000C9C7C File Offset: 0x000C7E7C
		public object Pop(IClientChannelSink sink)
		{
			while (this._sinkStack != null)
			{
				ChanelSinkStackEntry sinkStack = this._sinkStack;
				this._sinkStack = this._sinkStack.Next;
				if (sinkStack.Sink == sink)
				{
					return sinkStack.State;
				}
			}
			throw new RemotingException("The current sink stack is empty, or the specified sink was never pushed onto the current stack");
		}

		// Token: 0x060037E7 RID: 14311 RVA: 0x000C9CC5 File Offset: 0x000C7EC5
		public void Push(IClientChannelSink sink, object state)
		{
			this._sinkStack = new ChanelSinkStackEntry(sink, state, this._sinkStack);
		}

		// Token: 0x0400255F RID: 9567
		private IMessageSink _replySink;

		// Token: 0x04002560 RID: 9568
		private ChanelSinkStackEntry _sinkStack;
	}
}
