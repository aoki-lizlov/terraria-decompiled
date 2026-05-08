using System;
using System.IO;

namespace System.Resources
{
	// Token: 0x02000845 RID: 2117
	internal class Win32EncodedResource : Win32Resource
	{
		// Token: 0x060047AE RID: 18350 RVA: 0x000ECB5C File Offset: 0x000EAD5C
		internal Win32EncodedResource(NameOrId type, NameOrId name, int language, byte[] data)
			: base(type, name, language)
		{
			this.data = data;
		}

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x060047AF RID: 18351 RVA: 0x000ECB6F File Offset: 0x000EAD6F
		public byte[] Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x060047B0 RID: 18352 RVA: 0x000ECB77 File Offset: 0x000EAD77
		public override void WriteTo(Stream s)
		{
			s.Write(this.data, 0, this.data.Length);
		}

		// Token: 0x04002DAD RID: 11693
		private byte[] data;
	}
}
