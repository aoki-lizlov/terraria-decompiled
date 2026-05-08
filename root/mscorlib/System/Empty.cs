using System;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	// Token: 0x020001CA RID: 458
	[Serializable]
	internal sealed class Empty : ISerializable
	{
		// Token: 0x06001557 RID: 5463 RVA: 0x000025BE File Offset: 0x000007BE
		private Empty()
		{
		}

		// Token: 0x06001558 RID: 5464 RVA: 0x00004091 File Offset: 0x00002291
		public override string ToString()
		{
			return string.Empty;
		}

		// Token: 0x06001559 RID: 5465 RVA: 0x00055A5B File Offset: 0x00053C5B
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			UnitySerializationHolder.GetUnitySerializationInfo(info, 1, null, null);
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x00055A74 File Offset: 0x00053C74
		// Note: this type is marked as 'beforefieldinit'.
		static Empty()
		{
		}

		// Token: 0x040013F5 RID: 5109
		public static readonly Empty Value = new Empty();
	}
}
