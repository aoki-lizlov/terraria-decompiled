using System;

namespace System
{
	// Token: 0x020001B5 RID: 437
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoNotSupportedAttribute : MonoTODOAttribute
	{
		// Token: 0x06001493 RID: 5267 RVA: 0x000521F8 File Offset: 0x000503F8
		public MonoNotSupportedAttribute(string comment)
			: base(comment)
		{
		}
	}
}
