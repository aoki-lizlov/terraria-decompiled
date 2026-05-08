using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200071E RID: 1822
	[ComVisible(true)]
	[Guid("475e398f-8afa-43a7-a3be-f4ef8d6787c9")]
	[ClassInterface(ClassInterfaceType.None)]
	public class RegistrationServices : IRegistrationServices
	{
		// Token: 0x060041CD RID: 16845 RVA: 0x000025BE File Offset: 0x000007BE
		public RegistrationServices()
		{
		}

		// Token: 0x060041CE RID: 16846 RVA: 0x000E34BA File Offset: 0x000E16BA
		public virtual Guid GetManagedCategoryGuid()
		{
			return RegistrationServices.guidManagedCategory;
		}

		// Token: 0x060041CF RID: 16847 RVA: 0x000E34C1 File Offset: 0x000E16C1
		public virtual string GetProgIdForType(Type type)
		{
			return Marshal.GenerateProgIdForType(type);
		}

		// Token: 0x060041D0 RID: 16848 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO("implement")]
		public virtual Type[] GetRegistrableTypesInAssembly(Assembly assembly)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041D1 RID: 16849 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO("implement")]
		public virtual bool RegisterAssembly(Assembly assembly, AssemblyRegistrationFlags flags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041D2 RID: 16850 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO("implement")]
		public virtual void RegisterTypeForComClients(Type type, ref Guid g)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041D3 RID: 16851 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO("implement")]
		public virtual bool TypeRepresentsComType(Type type)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041D4 RID: 16852 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO("implement")]
		public virtual bool TypeRequiresRegistration(Type type)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041D5 RID: 16853 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO("implement")]
		public virtual bool UnregisterAssembly(Assembly assembly)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041D6 RID: 16854 RVA: 0x000174FB File Offset: 0x000156FB
		[ComVisible(false)]
		[MonoTODO("implement")]
		public virtual int RegisterTypeForComClients(Type type, RegistrationClassContext classContext, RegistrationConnectionType flags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041D7 RID: 16855 RVA: 0x000174FB File Offset: 0x000156FB
		[ComVisible(false)]
		[MonoTODO("implement")]
		public virtual void UnregisterTypeForComClients(int cookie)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041D8 RID: 16856 RVA: 0x000E34C9 File Offset: 0x000E16C9
		// Note: this type is marked as 'beforefieldinit'.
		static RegistrationServices()
		{
		}

		// Token: 0x04002B73 RID: 11123
		private static Guid guidManagedCategory = new Guid("{62C8FE65-4EBB-45e7-B440-6E39B2CDBF29}");
	}
}
