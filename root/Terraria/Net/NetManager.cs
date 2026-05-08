using System;
using System.Collections.Generic;
using System.IO;
using Terraria.Net.Sockets;

namespace Terraria.Net
{
	// Token: 0x0200016F RID: 367
	public class NetManager
	{
		// Token: 0x06001DD6 RID: 7638 RVA: 0x005025D7 File Offset: 0x005007D7
		private NetManager()
		{
		}

		// Token: 0x06001DD7 RID: 7639 RVA: 0x005025EC File Offset: 0x005007EC
		public void Register<T>() where T : NetModule, new()
		{
			T t = new T();
			NetManager.PacketTypeStorage<T>.Id = this._moduleCount;
			NetManager.PacketTypeStorage<T>.Module = t;
			this._modules[this._moduleCount] = t;
			this._moduleCount += 1;
		}

		// Token: 0x06001DD8 RID: 7640 RVA: 0x00502636 File Offset: 0x00500836
		public NetModule GetModule<T>() where T : NetModule
		{
			return NetManager.PacketTypeStorage<T>.Module;
		}

		// Token: 0x06001DD9 RID: 7641 RVA: 0x00502642 File Offset: 0x00500842
		public ushort GetId<T>() where T : NetModule
		{
			return NetManager.PacketTypeStorage<T>.Id;
		}

		// Token: 0x06001DDA RID: 7642 RVA: 0x00502649 File Offset: 0x00500849
		public void Read(BinaryReader reader, int userId, int readLength)
		{
			this.Read(reader, userId, readLength, true);
		}

		// Token: 0x06001DDB RID: 7643 RVA: 0x00502658 File Offset: 0x00500858
		private void Read(BinaryReader reader, int userId, int readLength, bool addToDiagnostics)
		{
			ushort num = reader.ReadUInt16();
			if (this._modules.ContainsKey(num))
			{
				this._modules[num].Deserialize(reader, userId);
			}
			if (addToDiagnostics)
			{
				Main.ActiveNetDiagnosticsUI.CountReadModuleMessage((int)num, readLength);
			}
		}

		// Token: 0x06001DDC RID: 7644 RVA: 0x005026A0 File Offset: 0x005008A0
		public void Broadcast(NetPacket packet, int ignoreClient = -1)
		{
			for (int i = 0; i < 256; i++)
			{
				if (i != ignoreClient && Netplay.Clients[i].IsConnected())
				{
					this.SendData(Netplay.Clients[i].Socket, packet);
				}
			}
			packet.Recycle();
		}

		// Token: 0x06001DDD RID: 7645 RVA: 0x005026EC File Offset: 0x005008EC
		public void Broadcast(NetPacket packet, NetManager.BroadcastCondition conditionToBroadcast, int ignoreClient = -1)
		{
			for (int i = 0; i < 256; i++)
			{
				if (i != ignoreClient && Netplay.Clients[i].IsConnected() && conditionToBroadcast(i))
				{
					this.SendData(Netplay.Clients[i].Socket, packet);
				}
			}
			packet.Recycle();
		}

		// Token: 0x06001DDE RID: 7646 RVA: 0x0050273E File Offset: 0x0050093E
		private void SendToSelf(NetPacket packet)
		{
			packet.Reader.BaseStream.Position = 3L;
			this.Read(packet.Reader, Main.myPlayer, packet.Length, false);
			packet.Recycle();
		}

		// Token: 0x06001DDF RID: 7647 RVA: 0x00502774 File Offset: 0x00500974
		public void BroadcastOrLoopback(NetPacket packet)
		{
			if (Main.netMode == 2)
			{
				this.Broadcast(packet, -1);
				return;
			}
			if (Main.netMode == 0)
			{
				this.SendToSelf(packet);
				return;
			}
			packet.Recycle();
		}

		// Token: 0x06001DE0 RID: 7648 RVA: 0x0050279D File Offset: 0x0050099D
		public void SendToServerOrLoopback(NetPacket packet)
		{
			if (Main.netMode == 1)
			{
				this.SendToServer(packet);
				return;
			}
			if (Main.netMode == 0)
			{
				this.SendToSelf(packet);
				return;
			}
			packet.Recycle();
		}

		// Token: 0x06001DE1 RID: 7649 RVA: 0x005027C5 File Offset: 0x005009C5
		public void SendToServerOrBroadcast(NetPacket packet)
		{
			if (Main.netMode == 1)
			{
				this.SendToServer(packet);
				return;
			}
			if (Main.netMode == 2)
			{
				this.Broadcast(packet, -1);
				return;
			}
			packet.Recycle();
		}

		// Token: 0x06001DE2 RID: 7650 RVA: 0x005027EF File Offset: 0x005009EF
		public void SendToServer(NetPacket packet)
		{
			this.SendData(Netplay.Connection.Socket, packet);
			packet.Recycle();
		}

		// Token: 0x06001DE3 RID: 7651 RVA: 0x00502809 File Offset: 0x00500A09
		public void SendToClient(NetPacket packet, int playerId)
		{
			this.SendData(Netplay.Clients[playerId].Socket, packet);
			packet.Recycle();
		}

		// Token: 0x06001DE4 RID: 7652 RVA: 0x00502825 File Offset: 0x00500A25
		public void SendToClientOrLoopback(NetPacket packet, int playerId)
		{
			if (Main.netMode == 0 && playerId == Main.myPlayer)
			{
				this.SendToSelf(packet);
				return;
			}
			this.SendToClient(packet, playerId);
		}

		// Token: 0x06001DE5 RID: 7653 RVA: 0x00502848 File Offset: 0x00500A48
		private void SendData(ISocket socket, NetPacket packet)
		{
			if (Main.netMode == 0)
			{
				return;
			}
			packet.ShrinkToFit();
			try
			{
				Main.ActiveNetDiagnosticsUI.CountSentModuleMessage((int)packet.Id, packet.Length);
				socket.AsyncSend(packet.Buffer.Data, 0, packet.Length, new SocketSendCallback(NetManager.EmptyCallback), null);
			}
			catch
			{
			}
		}

		// Token: 0x06001DE6 RID: 7654 RVA: 0x00009E46 File Offset: 0x00008046
		private static void EmptyCallback(object state)
		{
		}

		// Token: 0x06001DE7 RID: 7655 RVA: 0x005028B8 File Offset: 0x00500AB8
		// Note: this type is marked as 'beforefieldinit'.
		static NetManager()
		{
		}

		// Token: 0x0400167B RID: 5755
		public static readonly NetManager Instance = new NetManager();

		// Token: 0x0400167C RID: 5756
		private Dictionary<ushort, NetModule> _modules = new Dictionary<ushort, NetModule>();

		// Token: 0x0400167D RID: 5757
		private ushort _moduleCount;

		// Token: 0x0200074A RID: 1866
		private class PacketTypeStorage<T> where T : NetModule
		{
			// Token: 0x060040D2 RID: 16594 RVA: 0x0000357B File Offset: 0x0000177B
			public PacketTypeStorage()
			{
			}

			// Token: 0x040069CF RID: 27087
			public static ushort Id;

			// Token: 0x040069D0 RID: 27088
			public static T Module;
		}

		// Token: 0x0200074B RID: 1867
		// (Invoke) Token: 0x060040D4 RID: 16596
		public delegate bool BroadcastCondition(int clientIndex);
	}
}
