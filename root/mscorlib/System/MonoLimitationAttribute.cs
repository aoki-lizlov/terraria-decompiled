using System;

namespace System
{
	// Token: 0x020001B4 RID: 436
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoLimitationAttribute : MonoTODOAttribute
	{
		// Token: 0x06001492 RID: 5266 RVA: 0x000521F8 File Offset: 0x000503F8
		public MonoLimitationAttribute(string comment)
			: base(comment)
		{
		}
	}
}
