using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020001D9 RID: 473
	[Guid("27FFF232-A7A8-40dd-8D4A-734AD59FCD41")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true)]
	public interface IAppDomainSetup
	{
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x0600163C RID: 5692
		// (set) Token: 0x0600163D RID: 5693
		string ApplicationBase { get; set; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x0600163E RID: 5694
		// (set) Token: 0x0600163F RID: 5695
		string ApplicationName { get; set; }

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06001640 RID: 5696
		// (set) Token: 0x06001641 RID: 5697
		string CachePath { get; set; }

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06001642 RID: 5698
		// (set) Token: 0x06001643 RID: 5699
		string ConfigurationFile { get; set; }

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06001644 RID: 5700
		// (set) Token: 0x06001645 RID: 5701
		string DynamicBase { get; set; }

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06001646 RID: 5702
		// (set) Token: 0x06001647 RID: 5703
		string LicenseFile { get; set; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06001648 RID: 5704
		// (set) Token: 0x06001649 RID: 5705
		string PrivateBinPath { get; set; }

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x0600164A RID: 5706
		// (set) Token: 0x0600164B RID: 5707
		string PrivateBinPathProbe { get; set; }

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x0600164C RID: 5708
		// (set) Token: 0x0600164D RID: 5709
		string ShadowCopyDirectories { get; set; }

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x0600164E RID: 5710
		// (set) Token: 0x0600164F RID: 5711
		string ShadowCopyFiles { get; set; }
	}
}
