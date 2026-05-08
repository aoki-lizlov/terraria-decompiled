using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Terraria.Social.WeGame
{
	// Token: 0x02000138 RID: 312
	public class MessageDispatcherClient
	{
		// Token: 0x14000039 RID: 57
		// (add) Token: 0x06001C6D RID: 7277 RVA: 0x004FE474 File Offset: 0x004FC674
		// (remove) Token: 0x06001C6E RID: 7278 RVA: 0x004FE4AC File Offset: 0x004FC6AC
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

		// Token: 0x1400003A RID: 58
		// (add) Token: 0x06001C6F RID: 7279 RVA: 0x004FE4E4 File Offset: 0x004FC6E4
		// (remove) Token: 0x06001C70 RID: 7280 RVA: 0x004FE51C File Offset: 0x004FC71C
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

		// Token: 0x06001C71 RID: 7281 RVA: 0x004FE554 File Offset: 0x004FC754
		public void Init(string clientName, string serverName)
		{
			this._clientName = clientName;
			this._severName = serverName;
			this._ipcClient.Init(clientName);
			this._ipcClient.OnDataArrive += this.OnDataArrive;
			this._ipcClient.OnConnected += this.OnServerConnected;
		}

		// Token: 0x06001C72 RID: 7282 RVA: 0x004FE5A9 File Offset: 0x004FC7A9
		public void Start()
		{
			this._ipcClient.ConnectTo(this._severName);
		}

		// Token: 0x06001C73 RID: 7283 RVA: 0x004FE5BC File Offset: 0x004FC7BC
		private void OnDataArrive(byte[] data)
		{
			IPCMessage ipcmessage = new IPCMessage();
			ipcmessage.BuildFrom(data);
			if (this.OnMessage != null)
			{
				this.OnMessage(ipcmessage);
			}
		}

		// Token: 0x06001C74 RID: 7284 RVA: 0x004FE5EA File Offset: 0x004FC7EA
		private void OnServerConnected()
		{
			if (this.OnConnected != null)
			{
				this.OnConnected();
			}
		}

		// Token: 0x06001C75 RID: 7285 RVA: 0x004FE5FF File Offset: 0x004FC7FF
		public void Tick()
		{
			this._ipcClient.Tick();
		}

		// Token: 0x06001C76 RID: 7286 RVA: 0x004FE60C File Offset: 0x004FC80C
		public bool SendMessage(IPCMessage msg)
		{
			return this._ipcClient.Send(msg.GetBytes());
		}

		// Token: 0x06001C77 RID: 7287 RVA: 0x004FE61F File Offset: 0x004FC81F
		public MessageDispatcherClient()
		{
		}

		// Token: 0x040015CB RID: 5579
		private IPCClient _ipcClient = new IPCClient();

		// Token: 0x040015CC RID: 5580
		private string _severName;

		// Token: 0x040015CD RID: 5581
		private string _clientName;

		// Token: 0x040015CE RID: 5582
		[CompilerGenerated]
		private Action<IPCMessage> OnMessage;

		// Token: 0x040015CF RID: 5583
		[CompilerGenerated]
		private Action OnConnected;
	}
}
