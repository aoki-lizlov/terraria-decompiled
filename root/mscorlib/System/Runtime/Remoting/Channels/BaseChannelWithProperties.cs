using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000577 RID: 1399
	[ComVisible(true)]
	public abstract class BaseChannelWithProperties : BaseChannelObjectWithProperties
	{
		// Token: 0x060037BF RID: 14271 RVA: 0x000C8EF6 File Offset: 0x000C70F6
		protected BaseChannelWithProperties()
		{
		}

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x060037C0 RID: 14272 RVA: 0x000C8EFE File Offset: 0x000C70FE
		public override IDictionary Properties
		{
			get
			{
				if (this.SinksWithProperties == null || this.SinksWithProperties.Properties == null)
				{
					return base.Properties;
				}
				return new AggregateDictionary(new IDictionary[]
				{
					base.Properties,
					this.SinksWithProperties.Properties
				});
			}
		}

		// Token: 0x04002552 RID: 9554
		protected IChannelSinkBase SinksWithProperties;
	}
}
