using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020001FE RID: 510
	[ComVisible(false)]
	[Serializable]
	public sealed class ApplicationIdentity : ISerializable
	{
		// Token: 0x060018A2 RID: 6306 RVA: 0x0005EA35 File Offset: 0x0005CC35
		public ApplicationIdentity(string applicationIdentityFullName)
		{
			if (applicationIdentityFullName == null)
			{
				throw new ArgumentNullException("applicationIdentityFullName");
			}
			if (applicationIdentityFullName.IndexOf(", Culture=") == -1)
			{
				this._fullName = applicationIdentityFullName + ", Culture=neutral";
				return;
			}
			this._fullName = applicationIdentityFullName;
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x060018A3 RID: 6307 RVA: 0x0005EA72 File Offset: 0x0005CC72
		public string CodeBase
		{
			get
			{
				return this._codeBase;
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x060018A4 RID: 6308 RVA: 0x0005EA7A File Offset: 0x0005CC7A
		public string FullName
		{
			get
			{
				return this._fullName;
			}
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x0005EA7A File Offset: 0x0005CC7A
		public override string ToString()
		{
			return this._fullName;
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x0005E370 File Offset: 0x0005C570
		[MonoTODO("Missing serialization")]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
		}

		// Token: 0x040015BD RID: 5565
		private string _fullName;

		// Token: 0x040015BE RID: 5566
		private string _codeBase;
	}
}
