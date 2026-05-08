using System;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Security;
using System.Security.Policy;

namespace System
{
	// Token: 0x020001D8 RID: 472
	[Guid("05F696DC-2B29-3663-AD8B-C4389CF2A713")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[CLSCompliant(false)]
	[ComVisible(true)]
	public interface _AppDomain
	{
		// Token: 0x060015FD RID: 5629
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x060015FE RID: 5630
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x060015FF RID: 5631
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x06001600 RID: 5632
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);

		// Token: 0x06001601 RID: 5633
		string ToString();

		// Token: 0x06001602 RID: 5634
		bool Equals(object other);

		// Token: 0x06001603 RID: 5635
		int GetHashCode();

		// Token: 0x06001604 RID: 5636
		Type GetType();

		// Token: 0x06001605 RID: 5637
		[SecurityCritical]
		object InitializeLifetimeService();

		// Token: 0x06001606 RID: 5638
		[SecurityCritical]
		object GetLifetimeService();

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06001607 RID: 5639
		// (remove) Token: 0x06001608 RID: 5640
		event EventHandler DomainUnload;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06001609 RID: 5641
		// (remove) Token: 0x0600160A RID: 5642
		event AssemblyLoadEventHandler AssemblyLoad;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600160B RID: 5643
		// (remove) Token: 0x0600160C RID: 5644
		event EventHandler ProcessExit;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x0600160D RID: 5645
		// (remove) Token: 0x0600160E RID: 5646
		event ResolveEventHandler TypeResolve;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x0600160F RID: 5647
		// (remove) Token: 0x06001610 RID: 5648
		event ResolveEventHandler ResourceResolve;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06001611 RID: 5649
		// (remove) Token: 0x06001612 RID: 5650
		event ResolveEventHandler AssemblyResolve;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06001613 RID: 5651
		// (remove) Token: 0x06001614 RID: 5652
		event UnhandledExceptionEventHandler UnhandledException;

		// Token: 0x06001615 RID: 5653
		AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access);

		// Token: 0x06001616 RID: 5654
		AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir);

		// Token: 0x06001617 RID: 5655
		AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, Evidence evidence);

		// Token: 0x06001618 RID: 5656
		AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions);

		// Token: 0x06001619 RID: 5657
		AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, Evidence evidence);

		// Token: 0x0600161A RID: 5658
		AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions);

		// Token: 0x0600161B RID: 5659
		AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, Evidence evidence, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions);

		// Token: 0x0600161C RID: 5660
		AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, Evidence evidence, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions);

		// Token: 0x0600161D RID: 5661
		AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, Evidence evidence, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions, bool isSynchronized);

		// Token: 0x0600161E RID: 5662
		ObjectHandle CreateInstance(string assemblyName, string typeName);

		// Token: 0x0600161F RID: 5663
		ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName);

		// Token: 0x06001620 RID: 5664
		ObjectHandle CreateInstance(string assemblyName, string typeName, object[] activationAttributes);

		// Token: 0x06001621 RID: 5665
		ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, object[] activationAttributes);

		// Token: 0x06001622 RID: 5666
		ObjectHandle CreateInstance(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes);

		// Token: 0x06001623 RID: 5667
		ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes);

		// Token: 0x06001624 RID: 5668
		Assembly Load(AssemblyName assemblyRef);

		// Token: 0x06001625 RID: 5669
		Assembly Load(string assemblyString);

		// Token: 0x06001626 RID: 5670
		Assembly Load(byte[] rawAssembly);

		// Token: 0x06001627 RID: 5671
		Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore);

		// Token: 0x06001628 RID: 5672
		Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore, Evidence securityEvidence);

		// Token: 0x06001629 RID: 5673
		Assembly Load(AssemblyName assemblyRef, Evidence assemblySecurity);

		// Token: 0x0600162A RID: 5674
		Assembly Load(string assemblyString, Evidence assemblySecurity);

		// Token: 0x0600162B RID: 5675
		int ExecuteAssembly(string assemblyFile, Evidence assemblySecurity);

		// Token: 0x0600162C RID: 5676
		int ExecuteAssembly(string assemblyFile);

		// Token: 0x0600162D RID: 5677
		int ExecuteAssembly(string assemblyFile, Evidence assemblySecurity, string[] args);

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x0600162E RID: 5678
		string FriendlyName { get; }

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x0600162F RID: 5679
		string BaseDirectory { get; }

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06001630 RID: 5680
		string RelativeSearchPath { get; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06001631 RID: 5681
		bool ShadowCopyFiles { get; }

		// Token: 0x06001632 RID: 5682
		Assembly[] GetAssemblies();

		// Token: 0x06001633 RID: 5683
		[SecurityCritical]
		void AppendPrivatePath(string path);

		// Token: 0x06001634 RID: 5684
		[SecurityCritical]
		void ClearPrivatePath();

		// Token: 0x06001635 RID: 5685
		[SecurityCritical]
		void SetShadowCopyPath(string s);

		// Token: 0x06001636 RID: 5686
		[SecurityCritical]
		void ClearShadowCopyPath();

		// Token: 0x06001637 RID: 5687
		[SecurityCritical]
		void SetCachePath(string s);

		// Token: 0x06001638 RID: 5688
		[SecurityCritical]
		void SetData(string name, object data);

		// Token: 0x06001639 RID: 5689
		object GetData(string name);

		// Token: 0x0600163A RID: 5690
		void DoCallBack(CrossAppDomainDelegate theDelegate);

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x0600163B RID: 5691
		string DynamicDirectory { get; }
	}
}
