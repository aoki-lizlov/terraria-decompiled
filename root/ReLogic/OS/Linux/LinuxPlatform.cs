using System;
using ReLogic.Localization.IME;

namespace ReLogic.OS.Linux
{
	// Token: 0x02000076 RID: 118
	internal class LinuxPlatform : Platform
	{
		// Token: 0x06000289 RID: 649 RVA: 0x0000A8AE File Offset: 0x00008AAE
		public LinuxPlatform()
			: base(PlatformType.Linux)
		{
			base.RegisterService<IClipboard>(new Clipboard());
			base.RegisterService<IPathService>(new PathService());
			base.RegisterService<IWindowService>(new WindowService());
			base.RegisterService<IImeService>(new UnsupportedPlatformIme());
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000A704 File Offset: 0x00008904
		public override void InitializeClientServices(IntPtr windowHandle)
		{
			base.RegisterService<IImeService>(new FnaIme());
		}
	}
}
