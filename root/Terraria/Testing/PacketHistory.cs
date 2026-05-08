using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;

namespace Terraria.Testing
{
	// Token: 0x02000117 RID: 279
	public class PacketHistory
	{
		// Token: 0x06001B0C RID: 6924 RVA: 0x0000357B File Offset: 0x0000177B
		public PacketHistory(int historySize = 100, int bufferSize = 65535)
		{
		}

		// Token: 0x06001B0D RID: 6925 RVA: 0x004F98C0 File Offset: 0x004F7AC0
		[Conditional("DEBUG")]
		public void Record(byte[] buffer, int offset, int length)
		{
			length = Math.Max(0, length);
			PacketHistory.PacketView packetView = this.AppendPacket(length);
			Buffer.BlockCopy(buffer, offset, this._buffer, packetView.Offset, length);
		}

		// Token: 0x06001B0E RID: 6926 RVA: 0x004F98F4 File Offset: 0x004F7AF4
		private PacketHistory.PacketView AppendPacket(int size)
		{
			int num = this._bufferPosition;
			if (num + size > this._buffer.Length)
			{
				num = 0;
			}
			PacketHistory.PacketView packetView = new PacketHistory.PacketView(num, size, DateTime.Now);
			this._packets[this._historyPosition] = packetView;
			this._historyPosition = (this._historyPosition + 1) % this._packets.Length;
			this._bufferPosition = num + size;
			return packetView;
		}

		// Token: 0x06001B0F RID: 6927 RVA: 0x004F9958 File Offset: 0x004F7B58
		[Conditional("DEBUG")]
		public void Dump(string reason)
		{
			byte[] array = new byte[this._buffer.Length];
			Buffer.BlockCopy(this._buffer, this._bufferPosition, array, 0, this._buffer.Length - this._bufferPosition);
			Buffer.BlockCopy(this._buffer, 0, array, this._buffer.Length - this._bufferPosition, this._bufferPosition);
			StringBuilder stringBuilder = new StringBuilder();
			int num = 1;
			for (int i = 0; i < this._packets.Length; i++)
			{
				PacketHistory.PacketView packetView = this._packets[(i + this._historyPosition) % this._packets.Length];
				if (packetView.Offset != 0 || packetView.Length != 0)
				{
					stringBuilder.Append(string.Format("Packet {0} [Assumed MessageID: {4}, Size: {2}, Buffer Position: {1}, Timestamp: {3:G}]\r\n", new object[]
					{
						num++,
						packetView.Offset,
						packetView.Length,
						packetView.Time,
						this._buffer[packetView.Offset]
					}));
					for (int j = 0; j < packetView.Length; j++)
					{
						stringBuilder.Append(this._buffer[packetView.Offset + j].ToString("X2") + " ");
						if (j % 16 == 15 && j != this._packets.Length - 1)
						{
							stringBuilder.Append("\r\n");
						}
					}
					stringBuilder.Append("\r\n\r\n");
				}
			}
			stringBuilder.Append(reason);
			Directory.CreateDirectory(Path.Combine(Main.SavePath, "NetDump"));
			File.WriteAllText(Path.Combine(Main.SavePath, "NetDump", this.CreateDumpFileName()), stringBuilder.ToString());
		}

		// Token: 0x06001B10 RID: 6928 RVA: 0x004F9B24 File Offset: 0x004F7D24
		private string CreateDumpFileName()
		{
			DateTime dateTime = DateTime.Now.ToLocalTime();
			return string.Format("Net_{0}_{1}_{2}_{3}.txt", new object[]
			{
				Main.dedServ ? "TerrariaServer" : "Terraria",
				Main.versionNumber,
				dateTime.ToString("MM-dd-yy_HH-mm-ss-ffff", CultureInfo.InvariantCulture),
				Thread.CurrentThread.ManagedThreadId
			});
		}

		// Token: 0x06001B11 RID: 6929 RVA: 0x004F9B93 File Offset: 0x004F7D93
		[Conditional("DEBUG")]
		private void InitializeBuffer(int historySize, int bufferSize)
		{
			this._packets = new PacketHistory.PacketView[historySize];
			this._buffer = new byte[bufferSize];
		}

		// Token: 0x04001554 RID: 5460
		private byte[] _buffer;

		// Token: 0x04001555 RID: 5461
		private PacketHistory.PacketView[] _packets;

		// Token: 0x04001556 RID: 5462
		private int _bufferPosition;

		// Token: 0x04001557 RID: 5463
		private int _historyPosition;

		// Token: 0x02000728 RID: 1832
		private struct PacketView
		{
			// Token: 0x0600407E RID: 16510 RVA: 0x0069E6BE File Offset: 0x0069C8BE
			public PacketView(int offset, int length, DateTime time)
			{
				this.Offset = offset;
				this.Length = length;
				this.Time = time;
			}

			// Token: 0x0600407F RID: 16511 RVA: 0x0069E6D5 File Offset: 0x0069C8D5
			// Note: this type is marked as 'beforefieldinit'.
			static PacketView()
			{
			}

			// Token: 0x04006976 RID: 26998
			public static readonly PacketHistory.PacketView Empty = new PacketHistory.PacketView(0, 0, DateTime.Now);

			// Token: 0x04006977 RID: 26999
			public readonly int Offset;

			// Token: 0x04006978 RID: 27000
			public readonly int Length;

			// Token: 0x04006979 RID: 27001
			public readonly DateTime Time;
		}
	}
}
