using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000596 RID: 1430
	[ComVisible(true)]
	public class ServerChannelSinkStack : IServerChannelSinkStack, IServerResponseChannelSinkStack
	{
		// Token: 0x06003838 RID: 14392 RVA: 0x000025BE File Offset: 0x000007BE
		public ServerChannelSinkStack()
		{
		}

		// Token: 0x06003839 RID: 14393 RVA: 0x000CA175 File Offset: 0x000C8375
		public Stream GetResponseStream(IMessage msg, ITransportHeaders headers)
		{
			if (this._sinkStack == null)
			{
				throw new RemotingException("The sink stack is empty");
			}
			return ((IServerChannelSink)this._sinkStack.Sink).GetResponseStream(this, this._sinkStack.State, msg, headers);
		}

		// Token: 0x0600383A RID: 14394 RVA: 0x000CA1B0 File Offset: 0x000C83B0
		public object Pop(IServerChannelSink sink)
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

		// Token: 0x0600383B RID: 14395 RVA: 0x000CA1F9 File Offset: 0x000C83F9
		public void Push(IServerChannelSink sink, object state)
		{
			this._sinkStack = new ChanelSinkStackEntry(sink, state, this._sinkStack);
		}

		// Token: 0x0600383C RID: 14396 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO]
		public void ServerCallback(IAsyncResult ar)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600383D RID: 14397 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO]
		public void Store(IServerChannelSink sink, object state)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600383E RID: 14398 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO]
		public void StoreAndDispatch(IServerChannelSink sink, object state)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600383F RID: 14399 RVA: 0x000CA210 File Offset: 0x000C8410
		public void AsyncProcessResponse(IMessage msg, ITransportHeaders headers, Stream stream)
		{
			if (this._sinkStack == null)
			{
				throw new RemotingException("The current sink stack is empty");
			}
			ChanelSinkStackEntry sinkStack = this._sinkStack;
			this._sinkStack = this._sinkStack.Next;
			((IServerChannelSink)sinkStack.Sink).AsyncProcessResponse(this, sinkStack.State, msg, headers, stream);
		}

		// Token: 0x0400256D RID: 9581
		private ChanelSinkStackEntry _sinkStack;
	}
}
