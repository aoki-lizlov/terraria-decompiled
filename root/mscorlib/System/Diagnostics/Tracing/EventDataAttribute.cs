using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000A48 RID: 2632
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
	public class EventDataAttribute : Attribute
	{
		// Token: 0x17001055 RID: 4181
		// (get) Token: 0x060060C6 RID: 24774 RVA: 0x000174FB File Offset: 0x000156FB
		// (set) Token: 0x060060C7 RID: 24775 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO]
		public string Name
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060060C8 RID: 24776 RVA: 0x00002050 File Offset: 0x00000250
		public EventDataAttribute()
		{
		}
	}
}
