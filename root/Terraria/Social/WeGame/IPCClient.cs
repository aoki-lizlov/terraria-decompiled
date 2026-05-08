using System;
using System.IO.Pipes;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Terraria.Social.WeGame
{
	// Token: 0x02000134 RID: 308
	public class IPCClient : IPCBase
	{
		// Token: 0x14000035 RID: 53
		// (add) Token: 0x06001C4D RID: 7245 RVA: 0x004FE054 File Offset: 0x004FC254
		// (remove) Token: 0x06001C4E RID: 7246 RVA: 0x004FE08C File Offset: 0x004FC28C
		public event Action OnConnected
		{
			[CompilerGenerated]
			add
			{
				Action action = this.OnConnected;
				Action action2;
				do
				{
					action2 = action;
					Action action3 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.OnConnected, action3, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.OnConnected;
				Action action2;
				do
				{
					action2 = action;
					Action action3 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.OnConnected, action3, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000036 RID: 54
		// (add) Token: 0x06001C4F RID: 7247 RVA: 0x004FD8FF File Offset: 0x004FBAFF
		// (remove) Token: 0x06001C50 RID: 7248 RVA: 0x004FD918 File Offset: 0x004FBB18
		public override event Action<byte[]> OnDataArrive
		{
			add
			{
				this._onDataArrive = (Action<byte[]>)Delegate.Combine(this._onDataArrive, value);
			}
			remove
			{
				this._onDataArrive = (Action<byte[]>)Delegate.Remove(this._onDataArrive, value);
			}
		}

		// Token: 0x06001C51 RID: 7249 RVA: 0x004FE0C1 File Offset: 0x004FC2C1
		private NamedPipeClientStream GetPipeStream()
		{
			return (NamedPipeClientStream)this._pipeStream;
		}

		// Token: 0x06001C52 RID: 7250 RVA: 0x004FE0CE File Offset: 0x004FC2CE
		private void ProcessConnectedEvent()
		{
			if (this._connectedFlag)
			{
				if (this.OnConnected != null)
				{
					this.OnConnected();
				}
				this._connectedFlag = false;
			}
		}

		// Token: 0x06001C53 RID: 7251 RVA: 0x004FE0F2 File Offset: 0x004FC2F2
		private void ProcessPipeBrokenEvent()
		{
			if (this._pipeBrokenFlag)
			{
				this.Reset();
				this._pipeBrokenFlag = false;
			}
		}

		// Token: 0x06001C54 RID: 7252 RVA: 0x004FE10D File Offset: 0x004FC30D
		private void CheckFlagAndFireEvent()
		{
			this.ProcessConnectedEvent();
			this.ProcessDataArriveEvent();
			this.ProcessPipeBrokenEvent();
		}

		// Token: 0x06001C55 RID: 7253 RVA: 0x00009E46 File Offset: 0x00008046
		public void Init(string clientName)
		{
		}

		// Token: 0x06001C56 RID: 7254 RVA: 0x004FE124 File Offset: 0x004FC324
		public void ConnectTo(string serverName)
		{
			if (this.GetPipeStream() == null)
			{
				this._pipeStream = new NamedPipeClientStream(".", serverName, PipeDirection.InOut, PipeOptions.Asynchronous);
				this._cancelTokenSrc = new CancellationTokenSource();
				Task.Factory.StartNew(delegate(object content)
				{
					this.GetPipeStream().Connect();
					if (!((CancellationToken)content).IsCancellationRequested)
					{
						this.GetPipeStream().ReadMode = PipeTransmissionMode.Message;
						this.BeginReadData();
						this._connectedFlag = true;
					}
				}, this._cancelTokenSrc.Token);
			}
		}

		// Token: 0x06001C57 RID: 7255 RVA: 0x004FE182 File Offset: 0x004FC382
		public void Tick()
		{
			this.CheckFlagAndFireEvent();
		}

		// Token: 0x06001C58 RID: 7256 RVA: 0x004FE18C File Offset: 0x004FC38C
		public override void ReadCallback(IAsyncResult result)
		{
			IPCContent ipccontent = (IPCContent)result.AsyncState;
			base.ReadCallback(result);
			if (!ipccontent.CancelToken.IsCancellationRequested)
			{
				if (this.GetPipeStream().IsConnected)
				{
					this.BeginReadData();
					return;
				}
			}
			else
			{
				WeGameHelper.WriteDebugString("ReadCallback cancel", new object[0]);
			}
		}

		// Token: 0x06001C59 RID: 7257 RVA: 0x004FE049 File Offset: 0x004FC249
		public IPCClient()
		{
		}

		// Token: 0x06001C5A RID: 7258 RVA: 0x004FE1E0 File Offset: 0x004FC3E0
		[CompilerGenerated]
		private void <ConnectTo>b__12_0(object content)
		{
			this.GetPipeStream().Connect();
			if (!((CancellationToken)content).IsCancellationRequested)
			{
				this.GetPipeStream().ReadMode = PipeTransmissionMode.Message;
				this.BeginReadData();
				this._connectedFlag = true;
			}
		}

		// Token: 0x040015C4 RID: 5572
		private bool _connectedFlag;

		// Token: 0x040015C5 RID: 5573
		[CompilerGenerated]
		private Action OnConnected;
	}
}
