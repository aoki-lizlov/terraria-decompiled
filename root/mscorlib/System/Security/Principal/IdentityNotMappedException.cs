using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security.Principal
{
	// Token: 0x020004B4 RID: 1204
	[ComVisible(false)]
	[Serializable]
	public sealed class IdentityNotMappedException : SystemException
	{
		// Token: 0x06003197 RID: 12695 RVA: 0x000B76AB File Offset: 0x000B58AB
		public IdentityNotMappedException()
			: base(Locale.GetText("Couldn't translate some identities."))
		{
		}

		// Token: 0x06003198 RID: 12696 RVA: 0x0006F1CD File Offset: 0x0006D3CD
		public IdentityNotMappedException(string message)
			: base(message)
		{
		}

		// Token: 0x06003199 RID: 12697 RVA: 0x0006F1D6 File Offset: 0x0006D3D6
		public IdentityNotMappedException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x0600319A RID: 12698 RVA: 0x000B76BD File Offset: 0x000B58BD
		public IdentityReferenceCollection UnmappedIdentities
		{
			get
			{
				if (this._coll == null)
				{
					this._coll = new IdentityReferenceCollection();
				}
				return this._coll;
			}
		}

		// Token: 0x0600319B RID: 12699 RVA: 0x00004088 File Offset: 0x00002288
		[MonoTODO("not implemented")]
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
		}

		// Token: 0x04002244 RID: 8772
		private IdentityReferenceCollection _coll;
	}
}
