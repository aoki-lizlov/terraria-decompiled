using System;

namespace System
{
	// Token: 0x020001B2 RID: 434
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoExtensionAttribute : MonoTODOAttribute
	{
		// Token: 0x06001490 RID: 5264 RVA: 0x000521F8 File Offset: 0x000503F8
		public MonoExtensionAttribute(string comment)
			: base(comment)
		{
		}
	}
}
