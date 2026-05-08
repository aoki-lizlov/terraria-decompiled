using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Terraria.Social.WeGame
{
	// Token: 0x02000137 RID: 311
	public class MessageDispatcherServer
	{
		// Token: 0x14000037 RID: 55
		// (add) Token: 0x06001C62 RID: 7266 RVA: 0x004FE2D4 File Offset: 0x004FC4D4
		// (remove) Token: 0x06001C63 RID: 7267 RVA: 0x004FE30C File Offset: 0x004FC50C
		public event Action OnIPCClientAccess
		{
			[CompilerGenerated]
			add
			{
				Action action = this.OnIPCClientAccess;
				Action action2;
				do
				{
					action2 = action;
					Action action3 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.OnIPCClientAccess, action3, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.OnIPCClientAccess;
				Action action2;
				do
				{
					action2 = action;
					Action action3 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.OnIPCClientAccess, action3, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000038 RID: 56
		// (add) Token: 0x06001C64 RID: 7268 RVA: 0x004FE344 File Offset: 0x004FC544
		// (remove) Token: 0x06001C65 RID: 7269 RVA: 0x004FE37C File Offset: 0x004FC57C
		public event Action<IPCMessage> OnMessage
		{
			[CompilerGenerated]
			add
			{
				Action<IPCMessage> action = this.OnMessage;
				Action<IPCMessage> action2;
				do
				{
					action2 = action;
					Action<IPCMessage> action3 = (Action<IPCMessage>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<IPCMessage>>(ref this.OnMessage, action3, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<IPCMessage> action = this.OnMessage;
				Action<IPCMessage> action2;
				do
				{
					action2 = action;
					Action<IPCMessage> action3 = (Action<IPCMessage>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<IPCMessage>>(ref this.OnMessage, action3, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x06001C66 RID: 7270 RVA: 0x004FE3B1 File Offset: 0x004FC5B1
		public void Init(string serverName)
		{
			this._ipcSever.Init(serverName);
			this._ipcSever.OnDataArrive += this.OnDataArrive;
			this._ipcSever.OnClientAccess += this.OnClientAccess;
		}

		// Token: 0x06001C67 RID: 7271 RVA: 0x004FE3ED File Offset: 0x004FC5ED
		public void OnClientAccess()
		{
			if (this.OnIPCClientAccess != null)
			{
				this.OnIPCClientAccess();
			}
		}

		// Token: 0x06001C68 RID: 7272 RVA: 0x004FE402 File Offset: 0x004FC602
		public void Start()
		{
			this._ipcSever.StartListen();
		}

		// Token: 0x06001C69 RID: 7273 RVA: 0x004FE410 File Offset: 0x004FC610
		private void OnDataArrive(byte[] data)
		{
			IPCMessage ipcmessage = new IPCMessage();
			ipcmessage.BuildFrom(data);
			if (this.OnMessage != null)
			{
				this.OnMessage(ipcmessage);
			}
		}

		// Token: 0x06001C6A RID: 7274 RVA: 0x004FE43E File Offset: 0x004FC63E
		public void Tick()
		{
			this._ipcSever.Tick();
		}

		// Token: 0x06001C6B RID: 7275 RVA: 0x004FE44B File Offset: 0x004FC64B
		public bool SendMessage(IPCMessage msg)
		{
			return this._ipcSever.Send(msg.GetBytes());
		}

		// Token: 0x06001C6C RID: 7276 RVA: 0x004FE45E File Offset: 0x004FC65E
		public MessageDispatcherServer()
		{
		}

		// Token: 0x040015C8 RID: 5576
		private IPCServer _ipcSever = new IPCServer();

		// Token: 0x040015C9 RID: 5577
		[CompilerGenerated]
		private Action OnIPCClientAccess;

		// Token: 0x040015CA RID: 5578
		[CompilerGenerated]
		private Action<IPCMessage> OnMessage;
	}
}
