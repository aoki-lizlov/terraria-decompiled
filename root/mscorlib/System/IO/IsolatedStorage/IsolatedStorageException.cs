using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.IO.IsolatedStorage
{
	// Token: 0x02000993 RID: 2451
	[ComVisible(true)]
	[Serializable]
	public class IsolatedStorageException : Exception
	{
		// Token: 0x06005958 RID: 22872 RVA: 0x0012F16C File Offset: 0x0012D36C
		public IsolatedStorageException()
			: base(Locale.GetText("An Isolated storage operation failed."))
		{
		}

		// Token: 0x06005959 RID: 22873 RVA: 0x0002A236 File Offset: 0x00028436
		public IsolatedStorageException(string message)
			: base(message)
		{
		}

		// Token: 0x0600595A RID: 22874 RVA: 0x0002A23F File Offset: 0x0002843F
		public IsolatedStorageException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x0600595B RID: 22875 RVA: 0x00018937 File Offset: 0x00016B37
		protected IsolatedStorageException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
