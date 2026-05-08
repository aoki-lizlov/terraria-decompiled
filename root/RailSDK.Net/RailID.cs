using System;

namespace rail
{
	// Token: 0x02000078 RID: 120
	public class RailID : RailComparableID
	{
		// Token: 0x06001641 RID: 5697 RVA: 0x00010859 File Offset: 0x0000EA59
		public RailID()
		{
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x00010861 File Offset: 0x0000EA61
		public RailID(ulong id)
			: base(id)
		{
		}

		// Token: 0x06001643 RID: 5699 RVA: 0x0001086A File Offset: 0x0000EA6A
		public EnumRailIDDomain GetDomain()
		{
			if ((int)(this.id_ >> 56) == 1)
			{
				return EnumRailIDDomain.kRailIDDomainPublic;
			}
			return EnumRailIDDomain.kRailIDDomainInvalid;
		}
	}
}
