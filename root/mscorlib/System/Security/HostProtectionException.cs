using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Security
{
	// Token: 0x020003AE RID: 942
	[ComVisible(true)]
	[MonoTODO("Not supported in the runtime")]
	[Serializable]
	public class HostProtectionException : SystemException
	{
		// Token: 0x06002858 RID: 10328 RVA: 0x00092B99 File Offset: 0x00090D99
		public HostProtectionException()
		{
		}

		// Token: 0x06002859 RID: 10329 RVA: 0x0006F1CD File Offset: 0x0006D3CD
		public HostProtectionException(string message)
			: base(message)
		{
		}

		// Token: 0x0600285A RID: 10330 RVA: 0x0006F1D6 File Offset: 0x0006D3D6
		public HostProtectionException(string message, Exception e)
			: base(message, e)
		{
		}

		// Token: 0x0600285B RID: 10331 RVA: 0x00093414 File Offset: 0x00091614
		public HostProtectionException(string message, HostProtectionResource protectedResources, HostProtectionResource demandedResources)
			: base(message)
		{
			this._protected = protectedResources;
			this._demanded = demandedResources;
		}

		// Token: 0x0600285C RID: 10332 RVA: 0x0009342B File Offset: 0x0009162B
		protected HostProtectionException(SerializationInfo info, StreamingContext context)
		{
			this.GetObjectData(info, context);
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x0600285D RID: 10333 RVA: 0x0009343B File Offset: 0x0009163B
		public HostProtectionResource DemandedResources
		{
			get
			{
				return this._demanded;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x0600285E RID: 10334 RVA: 0x00093443 File Offset: 0x00091643
		public HostProtectionResource ProtectedResources
		{
			get
			{
				return this._protected;
			}
		}

		// Token: 0x0600285F RID: 10335 RVA: 0x0005E370 File Offset: 0x0005C570
		[MonoTODO]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
		}

		// Token: 0x06002860 RID: 10336 RVA: 0x0009344B File Offset: 0x0009164B
		[MonoTODO]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x04001D80 RID: 7552
		private HostProtectionResource _protected;

		// Token: 0x04001D81 RID: 7553
		private HostProtectionResource _demanded;
	}
}
