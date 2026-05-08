using System;

namespace System
{
	// Token: 0x020001B3 RID: 435
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoInternalNoteAttribute : MonoTODOAttribute
	{
		// Token: 0x06001491 RID: 5265 RVA: 0x000521F8 File Offset: 0x000503F8
		public MonoInternalNoteAttribute(string comment)
			: base(comment)
		{
		}
	}
}
