using System;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200009C RID: 156
	public interface IReferenceResolver
	{
		// Token: 0x06000730 RID: 1840
		object ResolveReference(object context, string reference);

		// Token: 0x06000731 RID: 1841
		string GetReference(object context, object value);

		// Token: 0x06000732 RID: 1842
		bool IsReferenced(object context, object value);

		// Token: 0x06000733 RID: 1843
		void AddReference(object context, string reference, object value);
	}
}
