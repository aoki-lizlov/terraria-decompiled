using System;
using System.IO;
using System.IO.Pipes;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Terraria.Social.WeGame
{
	// Token: 0x02000133 RID: 307
	public class IPCServer : IPCBase
	{
		// Token: 0x14000033 RID: 51
		// (add) Token: 0x06001C3C RID: 7228 RVA: 0x004FDD98 File Offset: 0x004FBF98
		// (remove) Token: 0x06001C3D RID: 7229 RVA: 0x004FDDD0 File Offset: 0x004FBFD0
		public event Action OnClientAccess
		{
			[CompilerGenerated]
			add
			{
				Action action = this.OnClientAccess;
				Action action2;
				do
				{
					action2 = action;
					Action action3 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.OnClientAccess, action3, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.OnClientAccess;
				Action action2;
				do
				{
					action2 = action;
					Action action3 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.OnClientAccess, action3, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000034 RID: 52
		// (add) Token: 0x06001C3E RID: 7230 RVA: 0x004FD8FF File Offset: 0x004FBAFF
		// (remove) Token: 0x06001C3F RID: 7231 RVA: 0x004FD918 File Offset: 0x004FBB18
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

		// Token: 0x06001C40 RID: 7232 RVA: 0x004FDE05 File Offset: 0x004FC005
		private NamedPipeServerStream GetPipeStream()
		{
			return (NamedPipeServerStream)this._pipeStream;
		}

		// Token: 0x06001C41 RID: 7233 RVA: 0x004FDE12 File Offset: 0x004FC012
		public void Init(string serverName)
		{
			this._serverName = serverName;
		}

		// Token: 0x06001C42 RID: 7234 RVA: 0x004FDE1B File Offset: 0x004FC01B
		private void LazyCreatePipe()
		{
			if (this.GetPipeStream() == null)
			{
				this._pipeStream = new NamedPipeServerStream(this._serverName, PipeDirection.InOut, 1, PipeTransmissionMode.Message, PipeOptions.Asynchronous);
				this._cancelTokenSrc = new CancellationTokenSource();
			}
		}

		// Token: 0x06001C43 RID: 7235 RVA: 0x004FDE4C File Offset: 0x004FC04C
		public override void ReadCallback(IAsyncResult result)
		{
			IPCContent ipccontent = (IPCContent)result.AsyncState;
			base.ReadCallback(result);
			if (!ipccontent.CancelToken.IsCancellationRequested)
			{
				this.ContinueReadOrWait();
				return;
			}
			WeGameHelper.WriteDebugString("servcer.ReadCallback cancel", new object[0]);
		}

		// Token: 0x06001C44 RID: 7236 RVA: 0x004FDE91 File Offset: 0x004FC091
		public void StartListen()
		{
			this.LazyCreatePipe();
			WeGameHelper.WriteDebugString("begin listen", new object[0]);
			this.GetPipeStream().BeginWaitForConnection(new AsyncCallback(this.ConnectionCallback), this._cancelTokenSrc.Token);
		}

		// Token: 0x06001C45 RID: 7237 RVA: 0x004FDED1 File Offset: 0x004FC0D1
		private void RestartListen()
		{
			this.StartListen();
		}

		// Token: 0x06001C46 RID: 7238 RVA: 0x004FDEDC File Offset: 0x004FC0DC
		private void ConnectionCallback(IAsyncResult result)
		{
			try
			{
				this._haveClientAccessFlag = true;
				WeGameHelper.WriteDebugString("Connected in", new object[0]);
				this.GetPipeStream().EndWaitForConnection(result);
				if (!((CancellationToken)result.AsyncState).IsCancellationRequested)
				{
					this.BeginReadData();
				}
				else
				{
					WeGameHelper.WriteDebugString("ConnectionCallback but user cancel", new object[0]);
				}
			}
			catch (IOException ex)
			{
				this._pipeBrokenFlag = true;
				WeGameHelper.WriteDebugString("ConnectionCallback Exception, {0}", new object[] { ex.Message });
			}
		}

		// Token: 0x06001C47 RID: 7239 RVA: 0x004FDF74 File Offset: 0x004FC174
		public void ContinueReadOrWait()
		{
			if (this.GetPipeStream().IsConnected)
			{
				this.BeginReadData();
				return;
			}
			try
			{
				this.GetPipeStream().BeginWaitForConnection(new AsyncCallback(this.ConnectionCallback), null);
			}
			catch (IOException ex)
			{
				this._pipeBrokenFlag = true;
				WeGameHelper.WriteDebugString("ContinueReadOrWait Exception, {0}", new object[] { ex.Message });
			}
		}

		// Token: 0x06001C48 RID: 7240 RVA: 0x004FDFE8 File Offset: 0x004FC1E8
		private void ProcessClientAccessEvent()
		{
			if (this._haveClientAccessFlag)
			{
				if (this.OnClientAccess != null)
				{
					this.OnClientAccess();
				}
				this._haveClientAccessFlag = false;
			}
		}

		// Token: 0x06001C49 RID: 7241 RVA: 0x004FE00C File Offset: 0x004FC20C
		private void CheckFlagAndFireEvent()
		{
			this.ProcessClientAccessEvent();
			this.ProcessDataArriveEvent();
			this.ProcessPipeBrokenEvent();
		}

		// Token: 0x06001C4A RID: 7242 RVA: 0x004FE020 File Offset: 0x004FC220
		private void ProcessPipeBrokenEvent()
		{
			if (this._pipeBrokenFlag)
			{
				this.Reset();
				this._pipeBrokenFlag = false;
				this.RestartListen();
			}
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x004FE041 File Offset: 0x004FC241
		public void Tick()
		{
			this.CheckFlagAndFireEvent();
		}

		// Token: 0x06001C4C RID: 7244 RVA: 0x004FE049 File Offset: 0x004FC249
		public IPCServer()
		{
		}

		// Token: 0x040015C1 RID: 5569
		private string _serverName;

		// Token: 0x040015C2 RID: 5570
		private bool _haveClientAccessFlag;

		// Token: 0x040015C3 RID: 5571
		[CompilerGenerated]
		private Action OnClientAccess;
	}
}
