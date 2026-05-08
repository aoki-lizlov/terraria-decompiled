using System;
using System.IO;

namespace System.Resources
{
	// Token: 0x02000846 RID: 2118
	internal class Win32IconResource : Win32Resource
	{
		// Token: 0x060047B1 RID: 18353 RVA: 0x000ECB8E File Offset: 0x000EAD8E
		public Win32IconResource(int id, int language, ICONDIRENTRY icon)
			: base(Win32ResourceType.RT_ICON, id, language)
		{
			this.icon = icon;
		}

		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x060047B2 RID: 18354 RVA: 0x000ECBA0 File Offset: 0x000EADA0
		public ICONDIRENTRY Icon
		{
			get
			{
				return this.icon;
			}
		}

		// Token: 0x060047B3 RID: 18355 RVA: 0x000ECBA8 File Offset: 0x000EADA8
		public override void WriteTo(Stream s)
		{
			s.Write(this.icon.image, 0, this.icon.image.Length);
		}

		// Token: 0x04002DAE RID: 11694
		private ICONDIRENTRY icon;
	}
}
