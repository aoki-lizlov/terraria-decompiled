using System;

namespace System
{
	// Token: 0x020001B1 RID: 433
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoDocumentationNoteAttribute : MonoTODOAttribute
	{
		// Token: 0x0600148F RID: 5263 RVA: 0x000521F8 File Offset: 0x000503F8
		public MonoDocumentationNoteAttribute(string comment)
			: base(comment)
		{
		}
	}
}
