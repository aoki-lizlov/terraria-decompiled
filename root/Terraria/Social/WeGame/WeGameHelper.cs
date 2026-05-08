using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Terraria.Social.WeGame
{
	// Token: 0x0200012E RID: 302
	public class WeGameHelper
	{
		// Token: 0x06001C20 RID: 7200
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		private static extern void OutputDebugString(string message);

		// Token: 0x06001C21 RID: 7201 RVA: 0x004FD769 File Offset: 0x004FB969
		public static void WriteDebugString(string format, params object[] args)
		{
			"[WeGame] - " + format;
		}

		// Token: 0x06001C22 RID: 7202 RVA: 0x004FD778 File Offset: 0x004FB978
		public static string Serialize<T>(T data)
		{
			DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(T));
			string text;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				dataContractJsonSerializer.WriteObject(memoryStream, data);
				memoryStream.Position = 0L;
				using (StreamReader streamReader = new StreamReader(memoryStream, Encoding.UTF8))
				{
					text = streamReader.ReadToEnd();
				}
			}
			return text;
		}

		// Token: 0x06001C23 RID: 7203 RVA: 0x004FD7F8 File Offset: 0x004FB9F8
		public static void UnSerialize<T>(string str, out T data)
		{
			using (MemoryStream memoryStream = new MemoryStream(Encoding.Unicode.GetBytes(str)))
			{
				DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(T));
				data = (T)((object)dataContractJsonSerializer.ReadObject(memoryStream));
			}
		}

		// Token: 0x06001C24 RID: 7204 RVA: 0x0000357B File Offset: 0x0000177B
		public WeGameHelper()
		{
		}
	}
}
