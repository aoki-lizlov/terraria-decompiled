using System;
using ReLogic.Localization.IME;

namespace ReLogic.OS.OSX
{
	// Token: 0x02000072 RID: 114
	internal class OsxPlatform : Platform
	{
		// Token: 0x0600027F RID: 639 RVA: 0x0000A6CF File Offset: 0x000088CF
		public OsxPlatform()
			: base(PlatformType.OSX)
		{
			base.RegisterService<IClipboard>(new Clipboard());
			base.RegisterService<IPathService>(new PathService());
			base.RegisterService<IWindowService>(new WindowService());
			base.RegisterService<IImeService>(new UnsupportedPlatformIme());
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000A704 File Offset: 0x00008904
		public override void InitializeClientServices(IntPtr windowHandle)
		{
			base.RegisterService<IImeService>(new FnaIme());
		}
	}
}
