using System;
using System.Reflection;
using System.Security;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006EA RID: 1770
	[Guid("CCBD682C-73A5-4568-B8B0-C7007E11ABA2")]
	[ComVisible(true)]
	public interface IRegistrationServices
	{
		// Token: 0x0600406D RID: 16493
		[SecurityCritical]
		bool RegisterAssembly(Assembly assembly, AssemblyRegistrationFlags flags);

		// Token: 0x0600406E RID: 16494
		[SecurityCritical]
		bool UnregisterAssembly(Assembly assembly);

		// Token: 0x0600406F RID: 16495
		[SecurityCritical]
		Type[] GetRegistrableTypesInAssembly(Assembly assembly);

		// Token: 0x06004070 RID: 16496
		[SecurityCritical]
		string GetProgIdForType(Type type);

		// Token: 0x06004071 RID: 16497
		[SecurityCritical]
		void RegisterTypeForComClients(Type type, ref Guid g);

		// Token: 0x06004072 RID: 16498
		Guid GetManagedCategoryGuid();

		// Token: 0x06004073 RID: 16499
		[SecurityCritical]
		bool TypeRequiresRegistration(Type type);

		// Token: 0x06004074 RID: 16500
		bool TypeRepresentsComType(Type type);
	}
}
