using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terraria.Social.WeGame
{
	// Token: 0x02000135 RID: 309
	public class IPCMessage
	{
		// Token: 0x06001C5B RID: 7259 RVA: 0x004FE222 File Offset: 0x004FC422
		public void Build<T>(IPCMessageType cmd, T t)
		{
			this._jsonData = WeGameHelper.Serialize<T>(t);
			this._cmd = cmd;
		}

		// Token: 0x06001C5C RID: 7260 RVA: 0x004FE238 File Offset: 0x004FC438
		public void BuildFrom(byte[] data)
		{
			byte[] array = data.Take(4).ToArray<byte>();
			byte[] array2 = data.Skip(4).ToArray<byte>();
			this._cmd = (IPCMessageType)BitConverter.ToInt32(array, 0);
			this._jsonData = Encoding.UTF8.GetString(array2);
		}

		// Token: 0x06001C5D RID: 7261 RVA: 0x004FE27D File Offset: 0x004FC47D
		public void Parse<T>(out T value)
		{
			WeGameHelper.UnSerialize<T>(this._jsonData, out value);
		}

		// Token: 0x06001C5E RID: 7262 RVA: 0x004FE28C File Offset: 0x004FC48C
		public byte[] GetBytes()
		{
			List<byte> list = new List<byte>();
			byte[] bytes = BitConverter.GetBytes((int)this._cmd);
			list.AddRange(bytes);
			list.AddRange(Encoding.UTF8.GetBytes(this._jsonData));
			return list.ToArray();
		}

		// Token: 0x06001C5F RID: 7263 RVA: 0x004FE2CC File Offset: 0x004FC4CC
		public IPCMessageType GetCmd()
		{
			return this._cmd;
		}

		// Token: 0x06001C60 RID: 7264 RVA: 0x0000357B File Offset: 0x0000177B
		public IPCMessage()
		{
		}

		// Token: 0x040015C6 RID: 5574
		private IPCMessageType _cmd;

		// Token: 0x040015C7 RID: 5575
		private string _jsonData;
	}
}
