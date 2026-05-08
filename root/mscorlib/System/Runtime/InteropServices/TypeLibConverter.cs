using System;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000722 RID: 1826
	[ComVisible(true)]
	[Guid("f1c3bf79-c3e4-11d3-88e7-00902754c43a")]
	[ClassInterface(ClassInterfaceType.None)]
	public sealed class TypeLibConverter : ITypeLibConverter
	{
		// Token: 0x060041D9 RID: 16857 RVA: 0x000025BE File Offset: 0x000007BE
		public TypeLibConverter()
		{
		}

		// Token: 0x060041DA RID: 16858 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO("implement")]
		[return: MarshalAs(UnmanagedType.Interface)]
		public object ConvertAssemblyToTypeLib(Assembly assembly, string strTypeLibName, TypeLibExporterFlags flags, ITypeLibExporterNotifySink notifySink)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041DB RID: 16859 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO("implement")]
		public AssemblyBuilder ConvertTypeLibToAssembly([MarshalAs(UnmanagedType.Interface)] object typeLib, string asmFileName, int flags, ITypeLibImporterNotifySink notifySink, byte[] publicKey, StrongNameKeyPair keyPair, bool unsafeInterfaces)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041DC RID: 16860 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO("implement")]
		public AssemblyBuilder ConvertTypeLibToAssembly([MarshalAs(UnmanagedType.Interface)] object typeLib, string asmFileName, TypeLibImporterFlags flags, ITypeLibImporterNotifySink notifySink, byte[] publicKey, StrongNameKeyPair keyPair, string asmNamespace, Version asmVersion)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041DD RID: 16861 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO("implement")]
		public bool GetPrimaryInteropAssembly(Guid g, int major, int minor, int lcid, out string asmName, out string asmCodeBase)
		{
			throw new NotImplementedException();
		}
	}
}
