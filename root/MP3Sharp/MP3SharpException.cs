using System;
using System.IO;
using System.Runtime.Serialization;
using XPT.Core.Audio.MP3Sharp.Support;

namespace XPT.Core.Audio.MP3Sharp
{
	// Token: 0x02000003 RID: 3
	[Serializable]
	public class MP3SharpException : Exception
	{
		// Token: 0x0600000A RID: 10 RVA: 0x0000225D File Offset: 0x0000045D
		internal MP3SharpException()
		{
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002265 File Offset: 0x00000465
		internal MP3SharpException(string message)
			: base(message)
		{
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000226E File Offset: 0x0000046E
		internal MP3SharpException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002278 File Offset: 0x00000478
		protected MP3SharpException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002282 File Offset: 0x00000482
		internal void PrintStackTrace()
		{
			SupportClass.WriteStackTrace(this, Console.Error);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000228F File Offset: 0x0000048F
		internal void PrintStackTrace(StreamWriter ps)
		{
			if (base.InnerException == null)
			{
				SupportClass.WriteStackTrace(this, ps);
				return;
			}
			SupportClass.WriteStackTrace(base.InnerException, Console.Error);
		}
	}
}
