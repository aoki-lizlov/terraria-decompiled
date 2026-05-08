using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Threading
{
	// Token: 0x020002BB RID: 699
	[Serializable]
	public sealed class CompressedStack : ISerializable
	{
		// Token: 0x0600204E RID: 8270 RVA: 0x00076A34 File Offset: 0x00074C34
		internal CompressedStack(int length)
		{
			if (length > 0)
			{
				this._list = new ArrayList(length);
			}
		}

		// Token: 0x0600204F RID: 8271 RVA: 0x00076A4C File Offset: 0x00074C4C
		internal CompressedStack(CompressedStack cs)
		{
			if (cs != null && cs._list != null)
			{
				this._list = (ArrayList)cs._list.Clone();
			}
		}

		// Token: 0x06002050 RID: 8272 RVA: 0x00076A75 File Offset: 0x00074C75
		[ComVisible(false)]
		public CompressedStack CreateCopy()
		{
			return new CompressedStack(this);
		}

		// Token: 0x06002051 RID: 8273 RVA: 0x00047E00 File Offset: 0x00046000
		public static CompressedStack Capture()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002052 RID: 8274 RVA: 0x00047E00 File Offset: 0x00046000
		[SecurityCritical]
		public static CompressedStack GetCompressedStack()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002053 RID: 8275 RVA: 0x0005E370 File Offset: 0x0005C570
		[MonoTODO("incomplete")]
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
		}

		// Token: 0x06002054 RID: 8276 RVA: 0x00047E00 File Offset: 0x00046000
		[SecurityCritical]
		public static void Run(CompressedStack compressedStack, ContextCallback callback, object state)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002055 RID: 8277 RVA: 0x00076A7D File Offset: 0x00074C7D
		internal bool Equals(CompressedStack cs)
		{
			if (this.IsEmpty())
			{
				return cs.IsEmpty();
			}
			return !cs.IsEmpty() && this._list.Count == cs._list.Count;
		}

		// Token: 0x06002056 RID: 8278 RVA: 0x00076AB3 File Offset: 0x00074CB3
		internal bool IsEmpty()
		{
			return this._list == null || this._list.Count == 0;
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06002057 RID: 8279 RVA: 0x00076ACD File Offset: 0x00074CCD
		internal IList List
		{
			get
			{
				return this._list;
			}
		}

		// Token: 0x04001A36 RID: 6710
		private ArrayList _list;
	}
}
