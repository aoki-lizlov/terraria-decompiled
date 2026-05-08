using System;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Security.Principal;
using System.Threading;
using Mono.Security;

namespace System
{
	// Token: 0x020001EA RID: 490
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_AppDomain))]
	[ClassInterface(ClassInterfaceType.None)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class AppDomain : MarshalByRefObject, _AppDomain, IEvidenceFactory
	{
		// Token: 0x0600173B RID: 5947 RVA: 0x0000408A File Offset: 0x0000228A
		internal static bool IsAppXModel()
		{
			return false;
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x0000408A File Offset: 0x0000228A
		internal static bool IsAppXDesignMode()
		{
			return false;
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x00004088 File Offset: 0x00002288
		internal static void CheckReflectionOnlyLoadSupported()
		{
		}

		// Token: 0x0600173E RID: 5950 RVA: 0x00004088 File Offset: 0x00002288
		internal static void CheckLoadFromSupported()
		{
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x000543BD File Offset: 0x000525BD
		private AppDomain()
		{
		}

		// Token: 0x06001740 RID: 5952
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern AppDomainSetup getSetup();

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06001741 RID: 5953 RVA: 0x0005BCAE File Offset: 0x00059EAE
		private AppDomainSetup SetupInformationNoCopy
		{
			get
			{
				return this.getSetup();
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06001742 RID: 5954 RVA: 0x0005BCB6 File Offset: 0x00059EB6
		public AppDomainSetup SetupInformation
		{
			get
			{
				return new AppDomainSetup(this.getSetup());
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06001743 RID: 5955 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO]
		public ApplicationTrust ApplicationTrust
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06001744 RID: 5956 RVA: 0x0005BCC4 File Offset: 0x00059EC4
		public string BaseDirectory
		{
			get
			{
				string applicationBase = this.SetupInformationNoCopy.ApplicationBase;
				if (SecurityManager.SecurityEnabled && applicationBase != null && applicationBase.Length > 0)
				{
					new FileIOPermission(FileIOPermissionAccess.PathDiscovery, applicationBase).Demand();
				}
				return applicationBase;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06001745 RID: 5957 RVA: 0x0005BD00 File Offset: 0x00059F00
		public string RelativeSearchPath
		{
			get
			{
				string privateBinPath = this.SetupInformationNoCopy.PrivateBinPath;
				if (SecurityManager.SecurityEnabled && privateBinPath != null && privateBinPath.Length > 0)
				{
					new FileIOPermission(FileIOPermissionAccess.PathDiscovery, privateBinPath).Demand();
				}
				return privateBinPath;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06001746 RID: 5958 RVA: 0x0005BD3C File Offset: 0x00059F3C
		public string DynamicDirectory
		{
			get
			{
				AppDomainSetup setupInformationNoCopy = this.SetupInformationNoCopy;
				if (setupInformationNoCopy.DynamicBase == null)
				{
					return null;
				}
				string text = Path.Combine(setupInformationNoCopy.DynamicBase, setupInformationNoCopy.ApplicationName);
				if (SecurityManager.SecurityEnabled && text != null && text.Length > 0)
				{
					new FileIOPermission(FileIOPermissionAccess.PathDiscovery, text).Demand();
				}
				return text;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06001747 RID: 5959 RVA: 0x0005BD8C File Offset: 0x00059F8C
		public bool ShadowCopyFiles
		{
			get
			{
				return this.SetupInformationNoCopy.ShadowCopyFiles == "true";
			}
		}

		// Token: 0x06001748 RID: 5960
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string getFriendlyName();

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06001749 RID: 5961 RVA: 0x0005BDA3 File Offset: 0x00059FA3
		public string FriendlyName
		{
			get
			{
				return this.getFriendlyName();
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x0600174A RID: 5962 RVA: 0x0005BDAC File Offset: 0x00059FAC
		public Evidence Evidence
		{
			get
			{
				if (this._evidence == null)
				{
					lock (this)
					{
						Assembly entryAssembly = Assembly.GetEntryAssembly();
						if (entryAssembly == null)
						{
							if (this == AppDomain.DefaultDomain)
							{
								return new Evidence();
							}
							this._evidence = AppDomain.DefaultDomain.Evidence;
						}
						else
						{
							this._evidence = Evidence.GetDefaultHostEvidence(entryAssembly);
						}
					}
				}
				return new Evidence(this._evidence);
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x0600174B RID: 5963 RVA: 0x0005BE34 File Offset: 0x0005A034
		internal IPrincipal DefaultPrincipal
		{
			get
			{
				if (AppDomain._principal == null)
				{
					PrincipalPolicy principalPolicy = this._principalPolicy;
					if (principalPolicy != PrincipalPolicy.UnauthenticatedPrincipal)
					{
						if (principalPolicy == PrincipalPolicy.WindowsPrincipal)
						{
							AppDomain._principal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
						}
					}
					else
					{
						AppDomain._principal = new GenericPrincipal(new GenericIdentity(string.Empty, string.Empty), null);
					}
				}
				return AppDomain._principal;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x0600174C RID: 5964 RVA: 0x0005BE88 File Offset: 0x0005A088
		internal PermissionSet GrantedPermissionSet
		{
			get
			{
				return this._granted;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x0600174D RID: 5965 RVA: 0x0005BE90 File Offset: 0x0005A090
		public PermissionSet PermissionSet
		{
			get
			{
				PermissionSet permissionSet;
				if ((permissionSet = this._granted) == null)
				{
					permissionSet = (this._granted = new PermissionSet(PermissionState.Unrestricted));
				}
				return permissionSet;
			}
		}

		// Token: 0x0600174E RID: 5966
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AppDomain getCurDomain();

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x0600174F RID: 5967 RVA: 0x0005BEB6 File Offset: 0x0005A0B6
		public static AppDomain CurrentDomain
		{
			get
			{
				return AppDomain.getCurDomain();
			}
		}

		// Token: 0x06001750 RID: 5968
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AppDomain getRootDomain();

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06001751 RID: 5969 RVA: 0x0005BEC0 File Offset: 0x0005A0C0
		internal static AppDomain DefaultDomain
		{
			get
			{
				if (AppDomain.default_domain == null)
				{
					AppDomain rootDomain = AppDomain.getRootDomain();
					if (rootDomain == AppDomain.CurrentDomain)
					{
						AppDomain.default_domain = rootDomain;
					}
					else
					{
						AppDomain.default_domain = (AppDomain)RemotingServices.GetDomainProxy(rootDomain);
					}
				}
				return AppDomain.default_domain;
			}
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x0005BF00 File Offset: 0x0005A100
		[Obsolete("AppDomain.AppendPrivatePath has been deprecated. Please investigate the use of AppDomainSetup.PrivateBinPath instead.")]
		[SecurityPermission(SecurityAction.LinkDemand, ControlAppDomain = true)]
		public void AppendPrivatePath(string path)
		{
			if (path == null || path.Length == 0)
			{
				return;
			}
			AppDomainSetup setupInformationNoCopy = this.SetupInformationNoCopy;
			string text = setupInformationNoCopy.PrivateBinPath;
			if (text == null || text.Length == 0)
			{
				setupInformationNoCopy.PrivateBinPath = path;
				return;
			}
			text = text.Trim();
			if (text[text.Length - 1] != Path.PathSeparator)
			{
				text += Path.PathSeparator.ToString();
			}
			setupInformationNoCopy.PrivateBinPath = text + path;
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x0005BF74 File Offset: 0x0005A174
		[Obsolete("AppDomain.ClearPrivatePath has been deprecated. Please investigate the use of AppDomainSetup.PrivateBinPath instead.")]
		[SecurityPermission(SecurityAction.LinkDemand, ControlAppDomain = true)]
		public void ClearPrivatePath()
		{
			this.SetupInformationNoCopy.PrivateBinPath = string.Empty;
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x0005BF86 File Offset: 0x0005A186
		[Obsolete("Use AppDomainSetup.ShadowCopyDirectories")]
		[SecurityPermission(SecurityAction.LinkDemand, ControlAppDomain = true)]
		public void ClearShadowCopyPath()
		{
			this.SetupInformationNoCopy.ShadowCopyDirectories = string.Empty;
		}

		// Token: 0x06001755 RID: 5973 RVA: 0x0005BF98 File Offset: 0x0005A198
		public ObjectHandle CreateComInstanceFrom(string assemblyName, string typeName)
		{
			return Activator.CreateComInstanceFrom(assemblyName, typeName);
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x0005BFA1 File Offset: 0x0005A1A1
		public ObjectHandle CreateComInstanceFrom(string assemblyFile, string typeName, byte[] hashValue, AssemblyHashAlgorithm hashAlgorithm)
		{
			return Activator.CreateComInstanceFrom(assemblyFile, typeName, hashValue, hashAlgorithm);
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x0005BFAD File Offset: 0x0005A1AD
		internal ObjectHandle InternalCreateInstanceWithNoSecurity(string assemblyName, string typeName)
		{
			return this.CreateInstance(assemblyName, typeName);
		}

		// Token: 0x06001758 RID: 5976 RVA: 0x0005BFB8 File Offset: 0x0005A1B8
		internal ObjectHandle InternalCreateInstanceWithNoSecurity(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes)
		{
			return this.CreateInstance(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityAttributes);
		}

		// Token: 0x06001759 RID: 5977 RVA: 0x0005BFDA File Offset: 0x0005A1DA
		internal ObjectHandle InternalCreateInstanceFromWithNoSecurity(string assemblyName, string typeName)
		{
			return this.CreateInstanceFrom(assemblyName, typeName);
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x0005BFE4 File Offset: 0x0005A1E4
		internal ObjectHandle InternalCreateInstanceFromWithNoSecurity(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes)
		{
			return this.CreateInstanceFrom(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityAttributes);
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x0005C006 File Offset: 0x0005A206
		public ObjectHandle CreateInstance(string assemblyName, string typeName)
		{
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			return Activator.CreateInstance(assemblyName, typeName);
		}

		// Token: 0x0600175C RID: 5980 RVA: 0x0005C01D File Offset: 0x0005A21D
		public ObjectHandle CreateInstance(string assemblyName, string typeName, object[] activationAttributes)
		{
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			return Activator.CreateInstance(assemblyName, typeName, activationAttributes);
		}

		// Token: 0x0600175D RID: 5981 RVA: 0x0005C038 File Offset: 0x0005A238
		[Obsolete("Use an overload that does not take an Evidence parameter")]
		public ObjectHandle CreateInstance(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes)
		{
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			return Activator.CreateInstance(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityAttributes);
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x0005C068 File Offset: 0x0005A268
		public object CreateInstanceAndUnwrap(string assemblyName, string typeName)
		{
			ObjectHandle objectHandle = this.CreateInstance(assemblyName, typeName);
			if (objectHandle == null)
			{
				return null;
			}
			return objectHandle.Unwrap();
		}

		// Token: 0x0600175F RID: 5983 RVA: 0x0005C08C File Offset: 0x0005A28C
		public object CreateInstanceAndUnwrap(string assemblyName, string typeName, object[] activationAttributes)
		{
			ObjectHandle objectHandle = this.CreateInstance(assemblyName, typeName, activationAttributes);
			if (objectHandle == null)
			{
				return null;
			}
			return objectHandle.Unwrap();
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x0005C0B0 File Offset: 0x0005A2B0
		[Obsolete("Use an overload that does not take an Evidence parameter")]
		public object CreateInstanceAndUnwrap(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes)
		{
			ObjectHandle objectHandle = this.CreateInstance(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityAttributes);
			if (objectHandle == null)
			{
				return null;
			}
			return objectHandle.Unwrap();
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x0005C0E0 File Offset: 0x0005A2E0
		public ObjectHandle CreateInstance(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
		{
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			return Activator.CreateInstance(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, null);
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x0005C110 File Offset: 0x0005A310
		public object CreateInstanceAndUnwrap(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
		{
			ObjectHandle objectHandle = this.CreateInstance(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes);
			if (objectHandle == null)
			{
				return null;
			}
			return objectHandle.Unwrap();
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x0005C13C File Offset: 0x0005A33C
		public ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
		{
			if (assemblyFile == null)
			{
				throw new ArgumentNullException("assemblyFile");
			}
			return Activator.CreateInstanceFrom(assemblyFile, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, null);
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x0005C16C File Offset: 0x0005A36C
		public object CreateInstanceFromAndUnwrap(string assemblyFile, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
		{
			ObjectHandle objectHandle = this.CreateInstanceFrom(assemblyFile, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes);
			if (objectHandle == null)
			{
				return null;
			}
			return objectHandle.Unwrap();
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x0005C198 File Offset: 0x0005A398
		public ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName)
		{
			if (assemblyFile == null)
			{
				throw new ArgumentNullException("assemblyFile");
			}
			return Activator.CreateInstanceFrom(assemblyFile, typeName);
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x0005C1AF File Offset: 0x0005A3AF
		public ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, object[] activationAttributes)
		{
			if (assemblyFile == null)
			{
				throw new ArgumentNullException("assemblyFile");
			}
			return Activator.CreateInstanceFrom(assemblyFile, typeName, activationAttributes);
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x0005C1C8 File Offset: 0x0005A3C8
		[Obsolete("Use an overload that does not take an Evidence parameter")]
		public ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes)
		{
			if (assemblyFile == null)
			{
				throw new ArgumentNullException("assemblyFile");
			}
			return Activator.CreateInstanceFrom(assemblyFile, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityAttributes);
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x0005C1F8 File Offset: 0x0005A3F8
		public object CreateInstanceFromAndUnwrap(string assemblyName, string typeName)
		{
			ObjectHandle objectHandle = this.CreateInstanceFrom(assemblyName, typeName);
			if (objectHandle == null)
			{
				return null;
			}
			return objectHandle.Unwrap();
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x0005C21C File Offset: 0x0005A41C
		public object CreateInstanceFromAndUnwrap(string assemblyName, string typeName, object[] activationAttributes)
		{
			ObjectHandle objectHandle = this.CreateInstanceFrom(assemblyName, typeName, activationAttributes);
			if (objectHandle == null)
			{
				return null;
			}
			return objectHandle.Unwrap();
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x0005C240 File Offset: 0x0005A440
		[Obsolete("Use an overload that does not take an Evidence parameter")]
		public object CreateInstanceFromAndUnwrap(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes)
		{
			ObjectHandle objectHandle = this.CreateInstanceFrom(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityAttributes);
			if (objectHandle == null)
			{
				return null;
			}
			return objectHandle.Unwrap();
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x0005C270 File Offset: 0x0005A470
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access)
		{
			return this.DefineDynamicAssembly(name, access, null, null, null, null, null, false);
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x0005C28C File Offset: 0x0005A48C
		[Obsolete("Declarative security for assembly level is no longer enforced")]
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, Evidence evidence)
		{
			return this.DefineDynamicAssembly(name, access, null, evidence, null, null, null, false);
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x0005C2A8 File Offset: 0x0005A4A8
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir)
		{
			return this.DefineDynamicAssembly(name, access, dir, null, null, null, null, false);
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x0005C2C4 File Offset: 0x0005A4C4
		[Obsolete("Declarative security for assembly level is no longer enforced")]
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, Evidence evidence)
		{
			return this.DefineDynamicAssembly(name, access, dir, evidence, null, null, null, false);
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x0005C2E0 File Offset: 0x0005A4E0
		[Obsolete("Declarative security for assembly level is no longer enforced")]
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions)
		{
			return this.DefineDynamicAssembly(name, access, null, null, requiredPermissions, optionalPermissions, refusedPermissions, false);
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x0005C300 File Offset: 0x0005A500
		[Obsolete("Declarative security for assembly level is no longer enforced")]
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, Evidence evidence, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions)
		{
			return this.DefineDynamicAssembly(name, access, null, evidence, requiredPermissions, optionalPermissions, refusedPermissions, false);
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x0005C320 File Offset: 0x0005A520
		[Obsolete("Declarative security for assembly level is no longer enforced")]
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions)
		{
			return this.DefineDynamicAssembly(name, access, dir, null, requiredPermissions, optionalPermissions, refusedPermissions, false);
		}

		// Token: 0x06001772 RID: 6002 RVA: 0x0005C340 File Offset: 0x0005A540
		[Obsolete("Declarative security for assembly level is no longer enforced")]
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, Evidence evidence, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions)
		{
			return this.DefineDynamicAssembly(name, access, dir, evidence, requiredPermissions, optionalPermissions, refusedPermissions, false);
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x0005C35F File Offset: 0x0005A55F
		[Obsolete("Declarative security for assembly level is no longer enforced")]
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, Evidence evidence, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions, bool isSynchronized)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			AppDomain.ValidateAssemblyName(name.Name);
			AssemblyBuilder assemblyBuilder = new AssemblyBuilder(name, dir, access, false);
			assemblyBuilder.AddPermissionRequests(requiredPermissions, optionalPermissions, refusedPermissions);
			return assemblyBuilder;
		}

		// Token: 0x06001774 RID: 6004 RVA: 0x0005C390 File Offset: 0x0005A590
		[Obsolete("Declarative security for assembly level is no longer enforced")]
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, Evidence evidence, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions, bool isSynchronized, IEnumerable<CustomAttributeBuilder> assemblyAttributes)
		{
			AssemblyBuilder assemblyBuilder = this.DefineDynamicAssembly(name, access, dir, evidence, requiredPermissions, optionalPermissions, refusedPermissions, isSynchronized);
			if (assemblyAttributes != null)
			{
				foreach (CustomAttributeBuilder customAttributeBuilder in assemblyAttributes)
				{
					assemblyBuilder.SetCustomAttribute(customAttributeBuilder);
				}
			}
			return assemblyBuilder;
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x0005C3F4 File Offset: 0x0005A5F4
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, IEnumerable<CustomAttributeBuilder> assemblyAttributes)
		{
			return this.DefineDynamicAssembly(name, access, null, null, null, null, null, false, assemblyAttributes);
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x0005C410 File Offset: 0x0005A610
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, bool isSynchronized, IEnumerable<CustomAttributeBuilder> assemblyAttributes)
		{
			return this.DefineDynamicAssembly(name, access, dir, null, null, null, null, isSynchronized, assemblyAttributes);
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x0005C42E File Offset: 0x0005A62E
		[MonoLimitation("The argument securityContextSource is ignored")]
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, IEnumerable<CustomAttributeBuilder> assemblyAttributes, SecurityContextSource securityContextSource)
		{
			return this.DefineDynamicAssembly(name, access, assemblyAttributes);
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x0005C439 File Offset: 0x0005A639
		internal AssemblyBuilder DefineInternalDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access)
		{
			return new AssemblyBuilder(name, null, access, true);
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x0005C444 File Offset: 0x0005A644
		public void DoCallBack(CrossAppDomainDelegate callBackDelegate)
		{
			if (callBackDelegate != null)
			{
				callBackDelegate();
			}
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x0005C44F File Offset: 0x0005A64F
		public int ExecuteAssembly(string assemblyFile)
		{
			return this.ExecuteAssembly(assemblyFile, null, null);
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x0005C45A File Offset: 0x0005A65A
		[Obsolete("Use an overload that does not take an Evidence parameter")]
		public int ExecuteAssembly(string assemblyFile, Evidence assemblySecurity)
		{
			return this.ExecuteAssembly(assemblyFile, assemblySecurity, null);
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x0005C468 File Offset: 0x0005A668
		[Obsolete("Use an overload that does not take an Evidence parameter")]
		public int ExecuteAssembly(string assemblyFile, Evidence assemblySecurity, string[] args)
		{
			Assembly assembly = Assembly.LoadFrom(assemblyFile, assemblySecurity);
			return this.ExecuteAssemblyInternal(assembly, args);
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x0005C488 File Offset: 0x0005A688
		[Obsolete("Use an overload that does not take an Evidence parameter")]
		public int ExecuteAssembly(string assemblyFile, Evidence assemblySecurity, string[] args, byte[] hashValue, AssemblyHashAlgorithm hashAlgorithm)
		{
			Assembly assembly = Assembly.LoadFrom(assemblyFile, assemblySecurity, hashValue, hashAlgorithm);
			return this.ExecuteAssemblyInternal(assembly, args);
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x0005C4AC File Offset: 0x0005A6AC
		public int ExecuteAssembly(string assemblyFile, string[] args)
		{
			Assembly assembly = Assembly.LoadFrom(assemblyFile, null);
			return this.ExecuteAssemblyInternal(assembly, args);
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x0005C4CC File Offset: 0x0005A6CC
		public int ExecuteAssembly(string assemblyFile, string[] args, byte[] hashValue, AssemblyHashAlgorithm hashAlgorithm)
		{
			Assembly assembly = Assembly.LoadFrom(assemblyFile, null, hashValue, hashAlgorithm);
			return this.ExecuteAssemblyInternal(assembly, args);
		}

		// Token: 0x06001780 RID: 6016 RVA: 0x0005C4EC File Offset: 0x0005A6EC
		private int ExecuteAssemblyInternal(Assembly a, string[] args)
		{
			if (a.EntryPoint == null)
			{
				throw new MissingMethodException("Entry point not found in assembly '" + a.FullName + "'.");
			}
			return this.ExecuteAssembly(a, args);
		}

		// Token: 0x06001781 RID: 6017
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int ExecuteAssembly(Assembly a, string[] args);

		// Token: 0x06001782 RID: 6018
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Assembly[] GetAssemblies(bool refOnly);

		// Token: 0x06001783 RID: 6019 RVA: 0x0005C51F File Offset: 0x0005A71F
		public Assembly[] GetAssemblies()
		{
			return this.GetAssemblies(false);
		}

		// Token: 0x06001784 RID: 6020
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern object GetData(string name);

		// Token: 0x06001785 RID: 6021 RVA: 0x00047D48 File Offset: 0x00045F48
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x06001786 RID: 6022 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x06001787 RID: 6023
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Assembly LoadAssembly(string assemblyRef, Evidence securityEvidence, bool refOnly, ref StackCrawlMark stackMark);

		// Token: 0x06001788 RID: 6024 RVA: 0x0005C528 File Offset: 0x0005A728
		public Assembly Load(AssemblyName assemblyRef)
		{
			return this.Load(assemblyRef, null);
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x0005C532 File Offset: 0x0005A732
		internal Assembly LoadSatellite(AssemblyName assemblyRef, bool throwOnError, ref StackCrawlMark stackMark)
		{
			if (assemblyRef == null)
			{
				throw new ArgumentNullException("assemblyRef");
			}
			Assembly assembly = this.LoadAssembly(assemblyRef.FullName, null, false, ref stackMark);
			if (assembly == null && throwOnError)
			{
				throw new FileNotFoundException(null, assemblyRef.Name);
			}
			return assembly;
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x0005C56C File Offset: 0x0005A76C
		[Obsolete("Use an overload that does not take an Evidence parameter")]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Assembly Load(AssemblyName assemblyRef, Evidence assemblySecurity)
		{
			if (assemblyRef == null)
			{
				throw new ArgumentNullException("assemblyRef");
			}
			if (assemblyRef.Name == null || assemblyRef.Name.Length == 0)
			{
				if (assemblyRef.CodeBase != null)
				{
					return Assembly.LoadFrom(assemblyRef.CodeBase, assemblySecurity);
				}
				throw new ArgumentException(Locale.GetText("assemblyRef.Name cannot be empty."), "assemblyRef");
			}
			else
			{
				StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
				Assembly assembly = this.LoadAssembly(assemblyRef.FullName, assemblySecurity, false, ref stackCrawlMark);
				if (assembly != null)
				{
					return assembly;
				}
				if (assemblyRef.CodeBase == null)
				{
					throw new FileNotFoundException(null, assemblyRef.Name);
				}
				string text = assemblyRef.CodeBase;
				if (text.StartsWith("file://", StringComparison.OrdinalIgnoreCase))
				{
					text = new Uri(text).LocalPath;
				}
				try
				{
					assembly = Assembly.LoadFrom(text, assemblySecurity);
				}
				catch
				{
					throw new FileNotFoundException(null, assemblyRef.Name);
				}
				AssemblyName name = assembly.GetName();
				if (assemblyRef.Name != name.Name)
				{
					throw new FileNotFoundException(null, assemblyRef.Name);
				}
				if (assemblyRef.Version != null && assemblyRef.Version != new Version(0, 0, 0, 0) && assemblyRef.Version != name.Version)
				{
					throw new FileNotFoundException(null, assemblyRef.Name);
				}
				if (assemblyRef.CultureInfo != null && assemblyRef.CultureInfo.Equals(name))
				{
					throw new FileNotFoundException(null, assemblyRef.Name);
				}
				byte[] publicKeyToken = assemblyRef.GetPublicKeyToken();
				if (publicKeyToken != null && publicKeyToken.Length != 0)
				{
					byte[] publicKeyToken2 = name.GetPublicKeyToken();
					if (publicKeyToken2 == null || publicKeyToken.Length != publicKeyToken2.Length)
					{
						throw new FileNotFoundException(null, assemblyRef.Name);
					}
					for (int i = publicKeyToken.Length - 1; i >= 0; i--)
					{
						if (publicKeyToken2[i] != publicKeyToken[i])
						{
							throw new FileNotFoundException(null, assemblyRef.Name);
						}
					}
				}
				return assembly;
			}
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x0005C734 File Offset: 0x0005A934
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Assembly Load(string assemblyString)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.Load(assemblyString, null, false, ref stackCrawlMark);
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x0005C750 File Offset: 0x0005A950
		[Obsolete("Use an overload that does not take an Evidence parameter")]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Assembly Load(string assemblyString, Evidence assemblySecurity)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.Load(assemblyString, assemblySecurity, false, ref stackCrawlMark);
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x0005C76A File Offset: 0x0005A96A
		internal Assembly Load(string assemblyString, Evidence assemblySecurity, bool refonly, ref StackCrawlMark stackMark)
		{
			if (assemblyString == null)
			{
				throw new ArgumentNullException("assemblyString");
			}
			if (assemblyString.Length == 0)
			{
				throw new ArgumentException("assemblyString cannot have zero length");
			}
			Assembly assembly = this.LoadAssembly(assemblyString, assemblySecurity, refonly, ref stackMark);
			if (assembly == null)
			{
				throw new FileNotFoundException(null, assemblyString);
			}
			return assembly;
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x0005C7A9 File Offset: 0x0005A9A9
		public Assembly Load(byte[] rawAssembly)
		{
			return this.Load(rawAssembly, null, null);
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x0005C7B4 File Offset: 0x0005A9B4
		public Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore)
		{
			return this.Load(rawAssembly, rawSymbolStore, null);
		}

		// Token: 0x06001790 RID: 6032
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Assembly LoadAssemblyRaw(byte[] rawAssembly, byte[] rawSymbolStore, Evidence securityEvidence, bool refonly);

		// Token: 0x06001791 RID: 6033 RVA: 0x0005C7BF File Offset: 0x0005A9BF
		[Obsolete("Use an overload that does not take an Evidence parameter")]
		public Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore, Evidence securityEvidence)
		{
			return this.Load(rawAssembly, rawSymbolStore, securityEvidence, false);
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x0005C7CB File Offset: 0x0005A9CB
		internal Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore, Evidence securityEvidence, bool refonly)
		{
			if (rawAssembly == null)
			{
				throw new ArgumentNullException("rawAssembly");
			}
			Assembly assembly = this.LoadAssemblyRaw(rawAssembly, rawSymbolStore, securityEvidence, refonly);
			assembly.FromByteArray = true;
			return assembly;
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x0005C7F0 File Offset: 0x0005A9F0
		[Obsolete("AppDomain policy levels are obsolete")]
		[SecurityPermission(SecurityAction.Demand, ControlPolicy = true)]
		public void SetAppDomainPolicy(PolicyLevel domainPolicy)
		{
			if (domainPolicy == null)
			{
				throw new ArgumentNullException("domainPolicy");
			}
			if (this._granted != null)
			{
				throw new PolicyException(Locale.GetText("An AppDomain policy is already specified."));
			}
			if (this.IsFinalizingForUnload())
			{
				throw new AppDomainUnloadedException();
			}
			PolicyStatement policyStatement = domainPolicy.Resolve(this._evidence);
			this._granted = policyStatement.PermissionSet;
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x0005C84A File Offset: 0x0005AA4A
		[Obsolete("Use AppDomainSetup.SetCachePath")]
		[SecurityPermission(SecurityAction.LinkDemand, ControlAppDomain = true)]
		public void SetCachePath(string path)
		{
			this.SetupInformationNoCopy.CachePath = path;
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x0005C858 File Offset: 0x0005AA58
		[SecurityPermission(SecurityAction.Demand, ControlPrincipal = true)]
		public void SetPrincipalPolicy(PrincipalPolicy policy)
		{
			if (this.IsFinalizingForUnload())
			{
				throw new AppDomainUnloadedException();
			}
			this._principalPolicy = policy;
			AppDomain._principal = null;
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x0005C875 File Offset: 0x0005AA75
		[Obsolete("Use AppDomainSetup.ShadowCopyFiles")]
		[SecurityPermission(SecurityAction.LinkDemand, ControlAppDomain = true)]
		public void SetShadowCopyFiles()
		{
			this.SetupInformationNoCopy.ShadowCopyFiles = "true";
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x0005C887 File Offset: 0x0005AA87
		[Obsolete("Use AppDomainSetup.ShadowCopyDirectories")]
		[SecurityPermission(SecurityAction.LinkDemand, ControlAppDomain = true)]
		public void SetShadowCopyPath(string path)
		{
			this.SetupInformationNoCopy.ShadowCopyDirectories = path;
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x0005C895 File Offset: 0x0005AA95
		[SecurityPermission(SecurityAction.Demand, ControlPrincipal = true)]
		public void SetThreadPrincipal(IPrincipal principal)
		{
			if (principal == null)
			{
				throw new ArgumentNullException("principal");
			}
			if (AppDomain._principal != null)
			{
				throw new PolicyException(Locale.GetText("principal already present."));
			}
			if (this.IsFinalizingForUnload())
			{
				throw new AppDomainUnloadedException();
			}
			AppDomain._principal = principal;
		}

		// Token: 0x06001799 RID: 6041
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AppDomain InternalSetDomainByID(int domain_id);

		// Token: 0x0600179A RID: 6042
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AppDomain InternalSetDomain(AppDomain context);

		// Token: 0x0600179B RID: 6043
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InternalPushDomainRef(AppDomain domain);

		// Token: 0x0600179C RID: 6044
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InternalPushDomainRefByID(int domain_id);

		// Token: 0x0600179D RID: 6045
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InternalPopDomainRef();

		// Token: 0x0600179E RID: 6046
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Context InternalSetContext(Context context);

		// Token: 0x0600179F RID: 6047
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Context InternalGetContext();

		// Token: 0x060017A0 RID: 6048
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Context InternalGetDefaultContext();

		// Token: 0x060017A1 RID: 6049
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string InternalGetProcessGuid(string newguid);

		// Token: 0x060017A2 RID: 6050 RVA: 0x0005C8D0 File Offset: 0x0005AAD0
		internal static object InvokeInDomain(AppDomain domain, MethodInfo method, object obj, object[] args)
		{
			AppDomain currentDomain = AppDomain.CurrentDomain;
			bool flag = false;
			object obj3;
			try
			{
				AppDomain.InternalPushDomainRef(domain);
				flag = true;
				AppDomain.InternalSetDomain(domain);
				Exception ex;
				object obj2 = ((RuntimeMethodInfo)method).InternalInvoke(obj, args, out ex);
				if (ex != null)
				{
					throw ex;
				}
				obj3 = obj2;
			}
			finally
			{
				AppDomain.InternalSetDomain(currentDomain);
				if (flag)
				{
					AppDomain.InternalPopDomainRef();
				}
			}
			return obj3;
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x0005C92C File Offset: 0x0005AB2C
		internal static object InvokeInDomainByID(int domain_id, MethodInfo method, object obj, object[] args)
		{
			AppDomain currentDomain = AppDomain.CurrentDomain;
			bool flag = false;
			object obj3;
			try
			{
				AppDomain.InternalPushDomainRefByID(domain_id);
				flag = true;
				AppDomain.InternalSetDomainByID(domain_id);
				Exception ex;
				object obj2 = ((RuntimeMethodInfo)method).InternalInvoke(obj, args, out ex);
				if (ex != null)
				{
					throw ex;
				}
				obj3 = obj2;
			}
			finally
			{
				AppDomain.InternalSetDomain(currentDomain);
				if (flag)
				{
					AppDomain.InternalPopDomainRef();
				}
			}
			return obj3;
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x0005C988 File Offset: 0x0005AB88
		internal static string GetProcessGuid()
		{
			if (AppDomain._process_guid == null)
			{
				AppDomain._process_guid = AppDomain.InternalGetProcessGuid(Guid.NewGuid().ToString());
			}
			return AppDomain._process_guid;
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x0005C9BE File Offset: 0x0005ABBE
		public static AppDomain CreateDomain(string friendlyName)
		{
			return AppDomain.CreateDomain(friendlyName, null, null);
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x0005C9C8 File Offset: 0x0005ABC8
		public static AppDomain CreateDomain(string friendlyName, Evidence securityInfo)
		{
			return AppDomain.CreateDomain(friendlyName, securityInfo, null);
		}

		// Token: 0x060017A7 RID: 6055
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AppDomain createDomain(string friendlyName, AppDomainSetup info);

		// Token: 0x060017A8 RID: 6056 RVA: 0x0005C9D4 File Offset: 0x0005ABD4
		[MonoLimitation("Currently it does not allow the setup in the other domain")]
		[SecurityPermission(SecurityAction.Demand, ControlAppDomain = true)]
		public static AppDomain CreateDomain(string friendlyName, Evidence securityInfo, AppDomainSetup info)
		{
			if (friendlyName == null)
			{
				throw new ArgumentNullException("friendlyName");
			}
			AppDomain defaultDomain = AppDomain.DefaultDomain;
			if (info == null)
			{
				if (defaultDomain == null)
				{
					info = new AppDomainSetup();
				}
				else
				{
					info = defaultDomain.SetupInformation;
				}
			}
			else
			{
				info = new AppDomainSetup(info);
			}
			if (defaultDomain != null)
			{
				if (!info.Equals(defaultDomain.SetupInformation))
				{
					if (info.ApplicationBase == null)
					{
						info.ApplicationBase = defaultDomain.SetupInformation.ApplicationBase;
					}
					if (info.ConfigurationFile == null)
					{
						info.ConfigurationFile = Path.GetFileName(defaultDomain.SetupInformation.ConfigurationFile);
					}
				}
			}
			else if (info.ConfigurationFile == null)
			{
				info.ConfigurationFile = "[I don't have a config file]";
			}
			if (info.AppDomainInitializer != null && !info.AppDomainInitializer.Method.IsStatic)
			{
				throw new ArgumentException("Non-static methods cannot be invoked as an appdomain initializer");
			}
			info.SerializeNonPrimitives();
			AppDomain appDomain = (AppDomain)RemotingServices.GetDomainProxy(AppDomain.createDomain(friendlyName, info));
			if (securityInfo == null)
			{
				if (defaultDomain == null)
				{
					appDomain._evidence = null;
				}
				else
				{
					appDomain._evidence = defaultDomain.Evidence;
				}
			}
			else
			{
				appDomain._evidence = new Evidence(securityInfo);
			}
			if (info.AppDomainInitializer != null)
			{
				AppDomain.Loader loader = new AppDomain.Loader(info.AppDomainInitializer.Method.DeclaringType.Assembly.Location);
				appDomain.DoCallBack(new CrossAppDomainDelegate(loader.Load));
				AppDomain.Initializer initializer = new AppDomain.Initializer(info.AppDomainInitializer, info.AppDomainInitializerArguments);
				appDomain.DoCallBack(new CrossAppDomainDelegate(initializer.Initialize));
			}
			return appDomain;
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x0005CB38 File Offset: 0x0005AD38
		public static AppDomain CreateDomain(string friendlyName, Evidence securityInfo, string appBasePath, string appRelativeSearchPath, bool shadowCopyFiles)
		{
			return AppDomain.CreateDomain(friendlyName, securityInfo, AppDomain.CreateDomainSetup(appBasePath, appRelativeSearchPath, shadowCopyFiles));
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x0005CB4A File Offset: 0x0005AD4A
		public static AppDomain CreateDomain(string friendlyName, Evidence securityInfo, AppDomainSetup info, PermissionSet grantSet, params global::System.Security.Policy.StrongName[] fullTrustAssemblies)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.ApplicationTrust = new ApplicationTrust(grantSet, fullTrustAssemblies ?? EmptyArray<global::System.Security.Policy.StrongName>.Value);
			return AppDomain.CreateDomain(friendlyName, securityInfo, info);
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x0005CB7C File Offset: 0x0005AD7C
		private static AppDomainSetup CreateDomainSetup(string appBasePath, string appRelativeSearchPath, bool shadowCopyFiles)
		{
			AppDomainSetup appDomainSetup = new AppDomainSetup();
			appDomainSetup.ApplicationBase = appBasePath;
			appDomainSetup.PrivateBinPath = appRelativeSearchPath;
			if (shadowCopyFiles)
			{
				appDomainSetup.ShadowCopyFiles = "true";
			}
			else
			{
				appDomainSetup.ShadowCopyFiles = "false";
			}
			return appDomainSetup;
		}

		// Token: 0x060017AC RID: 6060
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool InternalIsFinalizingForUnload(int domain_id);

		// Token: 0x060017AD RID: 6061 RVA: 0x0005CBB9 File Offset: 0x0005ADB9
		public bool IsFinalizingForUnload()
		{
			return AppDomain.InternalIsFinalizingForUnload(this.getDomainID());
		}

		// Token: 0x060017AE RID: 6062
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalUnload(int domain_id);

		// Token: 0x060017AF RID: 6063 RVA: 0x0005CBC6 File Offset: 0x0005ADC6
		private int getDomainID()
		{
			return Thread.GetDomainID();
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x0005CBCD File Offset: 0x0005ADCD
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.MayFail)]
		[SecurityPermission(SecurityAction.Demand, ControlAppDomain = true)]
		public static void Unload(AppDomain domain)
		{
			if (domain == null)
			{
				throw new ArgumentNullException("domain");
			}
			AppDomain.InternalUnload(domain.getDomainID());
		}

		// Token: 0x060017B1 RID: 6065
		[SecurityPermission(SecurityAction.LinkDemand, ControlAppDomain = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetData(string name, object data);

		// Token: 0x060017B2 RID: 6066 RVA: 0x0005CBE8 File Offset: 0x0005ADE8
		[MonoLimitation("The permission field is ignored")]
		public void SetData(string name, object data, IPermission permission)
		{
			this.SetData(name, data);
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x0005CBF2 File Offset: 0x0005ADF2
		[Obsolete("Use AppDomainSetup.DynamicBase")]
		[SecurityPermission(SecurityAction.LinkDemand, ControlAppDomain = true)]
		public void SetDynamicBase(string path)
		{
			this.SetupInformationNoCopy.DynamicBase = path;
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x0005CC00 File Offset: 0x0005AE00
		[Obsolete("AppDomain.GetCurrentThreadId has been deprecated because it does not provide a stable Id when managed threads are running on fibers (aka lightweight threads). To get a stable identifier for a managed thread, use the ManagedThreadId property on Thread.'")]
		public static int GetCurrentThreadId()
		{
			return Thread.CurrentThreadId;
		}

		// Token: 0x060017B5 RID: 6069 RVA: 0x0005BDA3 File Offset: 0x00059FA3
		public override string ToString()
		{
			return this.getFriendlyName();
		}

		// Token: 0x060017B6 RID: 6070 RVA: 0x0005CC08 File Offset: 0x0005AE08
		private static void ValidateAssemblyName(string name)
		{
			if (name == null || name.Length == 0)
			{
				throw new ArgumentException("The Name of AssemblyName cannot be null or a zero-length string.");
			}
			bool flag = true;
			for (int i = 0; i < name.Length; i++)
			{
				char c = name[i];
				if (i == 0 && char.IsWhiteSpace(c))
				{
					flag = false;
					break;
				}
				if (c == '/' || c == '\\' || c == ':')
				{
					flag = false;
					break;
				}
			}
			if (!flag)
			{
				throw new ArgumentException("The Name of AssemblyName cannot start with whitespace, or contain '/', '\\'  or ':'.");
			}
		}

		// Token: 0x060017B7 RID: 6071 RVA: 0x0005CC76 File Offset: 0x0005AE76
		private void DoAssemblyLoad(Assembly assembly)
		{
			if (this.AssemblyLoad == null)
			{
				return;
			}
			this.AssemblyLoad(this, new AssemblyLoadEventArgs(assembly));
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x0005CC94 File Offset: 0x0005AE94
		private Assembly DoAssemblyResolve(string name, Assembly requestingAssembly, bool refonly)
		{
			ResolveEventHandler resolveEventHandler;
			if (refonly)
			{
				resolveEventHandler = this.ReflectionOnlyAssemblyResolve;
			}
			else
			{
				resolveEventHandler = this.AssemblyResolve;
			}
			if (resolveEventHandler == null)
			{
				return null;
			}
			Dictionary<string, object> dictionary;
			if (refonly)
			{
				dictionary = AppDomain.assembly_resolve_in_progress_refonly;
				if (dictionary == null)
				{
					dictionary = new Dictionary<string, object>();
					AppDomain.assembly_resolve_in_progress_refonly = dictionary;
				}
			}
			else
			{
				dictionary = AppDomain.assembly_resolve_in_progress;
				if (dictionary == null)
				{
					dictionary = new Dictionary<string, object>();
					AppDomain.assembly_resolve_in_progress = dictionary;
				}
			}
			if (dictionary.ContainsKey(name))
			{
				return null;
			}
			dictionary[name] = null;
			Assembly assembly2;
			try
			{
				Delegate[] invocationList = resolveEventHandler.GetInvocationList();
				for (int i = 0; i < invocationList.Length; i++)
				{
					Assembly assembly = ((ResolveEventHandler)invocationList[i])(this, new ResolveEventArgs(name, requestingAssembly));
					if (assembly != null)
					{
						return assembly;
					}
				}
				assembly2 = null;
			}
			finally
			{
				dictionary.Remove(name);
			}
			return assembly2;
		}

		// Token: 0x060017B9 RID: 6073 RVA: 0x0005CD58 File Offset: 0x0005AF58
		internal Assembly DoTypeBuilderResolve(TypeBuilder tb)
		{
			if (this.TypeResolve == null)
			{
				return null;
			}
			return this.DoTypeResolve(tb.FullName);
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x0005CD70 File Offset: 0x0005AF70
		internal Assembly DoTypeResolve(string name)
		{
			if (this.TypeResolve == null)
			{
				return null;
			}
			Dictionary<string, object> dictionary = AppDomain.type_resolve_in_progress;
			if (dictionary == null)
			{
				dictionary = (AppDomain.type_resolve_in_progress = new Dictionary<string, object>());
			}
			if (dictionary.ContainsKey(name))
			{
				return null;
			}
			dictionary[name] = null;
			Assembly assembly2;
			try
			{
				Delegate[] invocationList = this.TypeResolve.GetInvocationList();
				for (int i = 0; i < invocationList.Length; i++)
				{
					Assembly assembly = ((ResolveEventHandler)invocationList[i])(this, new ResolveEventArgs(name));
					if (assembly != null)
					{
						return assembly;
					}
				}
				assembly2 = null;
			}
			finally
			{
				dictionary.Remove(name);
			}
			return assembly2;
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x0005CE0C File Offset: 0x0005B00C
		internal Assembly DoResourceResolve(string name, Assembly requesting)
		{
			if (this.ResourceResolve == null)
			{
				return null;
			}
			Delegate[] invocationList = this.ResourceResolve.GetInvocationList();
			for (int i = 0; i < invocationList.Length; i++)
			{
				Assembly assembly = ((ResolveEventHandler)invocationList[i])(this, new ResolveEventArgs(name, requesting));
				if (assembly != null)
				{
					return assembly;
				}
			}
			return null;
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x0005CE5F File Offset: 0x0005B05F
		private void DoDomainUnload()
		{
			if (this.DomainUnload != null)
			{
				this.DomainUnload(this, null);
			}
		}

		// Token: 0x060017BD RID: 6077
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void DoUnhandledException(Exception e);

		// Token: 0x060017BE RID: 6078 RVA: 0x0005CE76 File Offset: 0x0005B076
		internal void DoUnhandledException(UnhandledExceptionEventArgs args)
		{
			if (this.UnhandledException != null)
			{
				this.UnhandledException(this, args);
			}
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x0005CE8D File Offset: 0x0005B08D
		internal byte[] GetMarshalledDomainObjRef()
		{
			return CADSerializer.SerializeObject(RemotingServices.Marshal(AppDomain.CurrentDomain, null, typeof(AppDomain))).GetBuffer();
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x0005CEB0 File Offset: 0x0005B0B0
		internal void ProcessMessageInDomain(byte[] arrRequest, CADMethodCallMessage cadMsg, out byte[] arrResponse, out CADMethodReturnMessage cadMrm)
		{
			IMessage message;
			if (arrRequest != null)
			{
				message = CADSerializer.DeserializeMessage(new MemoryStream(arrRequest), null);
			}
			else
			{
				message = new MethodCall(cadMsg);
			}
			IMessage message2 = ChannelServices.SyncDispatchMessage(message);
			cadMrm = CADMethodReturnMessage.Create(message2);
			if (cadMrm == null)
			{
				arrResponse = CADSerializer.SerializeMessage(message2).GetBuffer();
				return;
			}
			arrResponse = null;
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x060017C1 RID: 6081 RVA: 0x0005CEFC File Offset: 0x0005B0FC
		// (remove) Token: 0x060017C2 RID: 6082 RVA: 0x0005CF34 File Offset: 0x0005B134
		public event AssemblyLoadEventHandler AssemblyLoad
		{
			[CompilerGenerated]
			[SecurityPermission(SecurityAction.LinkDemand, ControlAppDomain = true)]
			add
			{
				AssemblyLoadEventHandler assemblyLoadEventHandler = this.AssemblyLoad;
				AssemblyLoadEventHandler assemblyLoadEventHandler2;
				do
				{
					assemblyLoadEventHandler2 = assemblyLoadEventHandler;
					AssemblyLoadEventHandler assemblyLoadEventHandler3 = (AssemblyLoadEventHandler)Delegate.Combine(assemblyLoadEventHandler2, value);
					assemblyLoadEventHandler = Interlocked.CompareExchange<AssemblyLoadEventHandler>(ref this.AssemblyLoad, assemblyLoadEventHandler3, assemblyLoadEventHandler2);
				}
				while (assemblyLoadEventHandler != assemblyLoadEventHandler2);
			}
			[CompilerGenerated]
			[SecurityPermission(SecurityAction.LinkDemand, ControlAppDomain = true)]
			remove
			{
				AssemblyLoadEventHandler assemblyLoadEventHandler = this.AssemblyLoad;
				AssemblyLoadEventHandler assemblyLoadEventHandler2;
				do
				{
					assemblyLoadEventHandler2 = assemblyLoadEventHandler;
					AssemblyLoadEventHandler assemblyLoadEventHandler3 = (AssemblyLoadEventHandler)Delegate.Remove(assemblyLoadEventHandler2, value);
					assemblyLoadEventHandler = Interlocked.CompareExchange<AssemblyLoadEventHandler>(ref this.AssemblyLoad, assemblyLoadEventHandler3, assemblyLoadEventHandler2);
				}
				while (assemblyLoadEventHandler != assemblyLoadEventHandler2);
			}
		}

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x060017C3 RID: 6083 RVA: 0x0005CF6C File Offset: 0x0005B16C
		// (remove) Token: 0x060017C4 RID: 6084 RVA: 0x0005CFA4 File Offset: 0x0005B1A4
		public event ResolveEventHandler AssemblyResolve
		{
			[CompilerGenerated]
			[SecurityPermission(SecurityAction.LinkDemand, ControlAppDomain = true)]
			add
			{
				ResolveEventHandler resolveEventHandler = this.AssemblyResolve;
				ResolveEventHandler resolveEventHandler2;
				do
				{
					resolveEventHandler2 = resolveEventHandler;
					ResolveEventHandler resolveEventHandler3 = (ResolveEventHandler)Delegate.Combine(resolveEventHandler2, value);
					resolveEventHandler = Interlocked.CompareExchange<ResolveEventHandler>(ref this.AssemblyResolve, resolveEventHandler3, resolveEventHandler2);
				}
				while (resolveEventHandler != resolveEventHandler2);
			}
			[CompilerGenerated]
			[SecurityPermission(SecurityAction.LinkDemand, ControlAppDomain = true)]
			remove
			{
				ResolveEventHandler resolveEventHandler = this.AssemblyResolve;
				ResolveEventHandler resolveEventHandler2;
				do
				{
					resolveEventHandler2 = resolveEventHandler;
					ResolveEventHandler resolveEventHandler3 = (ResolveEventHandler)Delegate.Remove(resolveEventHandler2, value);
					resolveEventHandler = Interlocked.CompareExchange<ResolveEventHandler>(ref this.AssemblyResolve, resolveEventHandler3, resolveEventHandler2);
				}
				while (resolveEventHandler != resolveEventHandler2);
			}
		}

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060017C5 RID: 6085 RVA: 0x0005CFDC File Offset: 0x0005B1DC
		// (remove) Token: 0x060017C6 RID: 6086 RVA: 0x0005D014 File Offset: 0x0005B214
		public event EventHandler DomainUnload
		{
			[CompilerGenerated]
			[SecurityPermission(SecurityAction.LinkDemand, ControlAppDomain = true)]
			add
			{
				EventHandler eventHandler = this.DomainUnload;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler eventHandler3 = (EventHandler)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.DomainUnload, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			[SecurityPermission(SecurityAction.LinkDemand, ControlAppDomain = true)]
			remove
			{
				EventHandler eventHandler = this.DomainUnload;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler eventHandler3 = (EventHandler)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.DomainUnload, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x060017C7 RID: 6087 RVA: 0x0005D04C File Offset: 0x0005B24C
		// (remove) Token: 0x060017C8 RID: 6088 RVA: 0x0005D084 File Offset: 0x0005B284
		public event EventHandler ProcessExit
		{
			[CompilerGenerated]
			[SecurityPermission(SecurityAction.LinkDemand, ControlAppDomain = true)]
			add
			{
				EventHandler eventHandler = this.ProcessExit;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler eventHandler3 = (EventHandler)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.ProcessExit, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			[SecurityPermission(SecurityAction.LinkDemand, ControlAppDomain = true)]
			remove
			{
				EventHandler eventHandler = this.ProcessExit;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler eventHandler3 = (EventHandler)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.ProcessExit, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x060017C9 RID: 6089 RVA: 0x0005D0BC File Offset: 0x0005B2BC
		// (remove) Token: 0x060017CA RID: 6090 RVA: 0x0005D0F4 File Offset: 0x0005B2F4
		public event ResolveEventHandler ResourceResolve
		{
			[CompilerGenerated]
			[SecurityPermission(SecurityAction.LinkDemand, ControlAppDomain = true)]
			add
			{
				ResolveEventHandler resolveEventHandler = this.ResourceResolve;
				ResolveEventHandler resolveEventHandler2;
				do
				{
					resolveEventHandler2 = resolveEventHandler;
					ResolveEventHandler resolveEventHandler3 = (ResolveEventHandler)Delegate.Combine(resolveEventHandler2, value);
					resolveEventHandler = Interlocked.CompareExchange<ResolveEventHandler>(ref this.ResourceResolve, resolveEventHandler3, resolveEventHandler2);
				}
				while (resolveEventHandler != resolveEventHandler2);
			}
			[CompilerGenerated]
			[SecurityPermission(SecurityAction.LinkDemand, ControlAppDomain = true)]
			remove
			{
				ResolveEventHandler resolveEventHandler = this.ResourceResolve;
				ResolveEventHandler resolveEventHandler2;
				do
				{
					resolveEventHandler2 = resolveEventHandler;
					ResolveEventHandler resolveEventHandler3 = (ResolveEventHandler)Delegate.Remove(resolveEventHandler2, value);
					resolveEventHandler = Interlocked.CompareExchange<ResolveEventHandler>(ref this.ResourceResolve, resolveEventHandler3, resolveEventHandler2);
				}
				while (resolveEventHandler != resolveEventHandler2);
			}
		}

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x060017CB RID: 6091 RVA: 0x0005D12C File Offset: 0x0005B32C
		// (remove) Token: 0x060017CC RID: 6092 RVA: 0x0005D164 File Offset: 0x0005B364
		public event ResolveEventHandler TypeResolve
		{
			[CompilerGenerated]
			[SecurityPermission(SecurityAction.LinkDemand, ControlAppDomain = true)]
			add
			{
				ResolveEventHandler resolveEventHandler = this.TypeResolve;
				ResolveEventHandler resolveEventHandler2;
				do
				{
					resolveEventHandler2 = resolveEventHandler;
					ResolveEventHandler resolveEventHandler3 = (ResolveEventHandler)Delegate.Combine(resolveEventHandler2, value);
					resolveEventHandler = Interlocked.CompareExchange<ResolveEventHandler>(ref this.TypeResolve, resolveEventHandler3, resolveEventHandler2);
				}
				while (resolveEventHandler != resolveEventHandler2);
			}
			[CompilerGenerated]
			[SecurityPermission(SecurityAction.LinkDemand, ControlAppDomain = true)]
			remove
			{
				ResolveEventHandler resolveEventHandler = this.TypeResolve;
				ResolveEventHandler resolveEventHandler2;
				do
				{
					resolveEventHandler2 = resolveEventHandler;
					ResolveEventHandler resolveEventHandler3 = (ResolveEventHandler)Delegate.Remove(resolveEventHandler2, value);
					resolveEventHandler = Interlocked.CompareExchange<ResolveEventHandler>(ref this.TypeResolve, resolveEventHandler3, resolveEventHandler2);
				}
				while (resolveEventHandler != resolveEventHandler2);
			}
		}

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x060017CD RID: 6093 RVA: 0x0005D19C File Offset: 0x0005B39C
		// (remove) Token: 0x060017CE RID: 6094 RVA: 0x0005D1D4 File Offset: 0x0005B3D4
		public event UnhandledExceptionEventHandler UnhandledException
		{
			[CompilerGenerated]
			[SecurityPermission(SecurityAction.LinkDemand, ControlAppDomain = true)]
			add
			{
				UnhandledExceptionEventHandler unhandledExceptionEventHandler = this.UnhandledException;
				UnhandledExceptionEventHandler unhandledExceptionEventHandler2;
				do
				{
					unhandledExceptionEventHandler2 = unhandledExceptionEventHandler;
					UnhandledExceptionEventHandler unhandledExceptionEventHandler3 = (UnhandledExceptionEventHandler)Delegate.Combine(unhandledExceptionEventHandler2, value);
					unhandledExceptionEventHandler = Interlocked.CompareExchange<UnhandledExceptionEventHandler>(ref this.UnhandledException, unhandledExceptionEventHandler3, unhandledExceptionEventHandler2);
				}
				while (unhandledExceptionEventHandler != unhandledExceptionEventHandler2);
			}
			[CompilerGenerated]
			[SecurityPermission(SecurityAction.LinkDemand, ControlAppDomain = true)]
			remove
			{
				UnhandledExceptionEventHandler unhandledExceptionEventHandler = this.UnhandledException;
				UnhandledExceptionEventHandler unhandledExceptionEventHandler2;
				do
				{
					unhandledExceptionEventHandler2 = unhandledExceptionEventHandler;
					UnhandledExceptionEventHandler unhandledExceptionEventHandler3 = (UnhandledExceptionEventHandler)Delegate.Remove(unhandledExceptionEventHandler2, value);
					unhandledExceptionEventHandler = Interlocked.CompareExchange<UnhandledExceptionEventHandler>(ref this.UnhandledException, unhandledExceptionEventHandler3, unhandledExceptionEventHandler2);
				}
				while (unhandledExceptionEventHandler != unhandledExceptionEventHandler2);
			}
		}

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x060017CF RID: 6095 RVA: 0x0005D20C File Offset: 0x0005B40C
		// (remove) Token: 0x060017D0 RID: 6096 RVA: 0x0005D244 File Offset: 0x0005B444
		public event EventHandler<FirstChanceExceptionEventArgs> FirstChanceException
		{
			[CompilerGenerated]
			add
			{
				EventHandler<FirstChanceExceptionEventArgs> eventHandler = this.FirstChanceException;
				EventHandler<FirstChanceExceptionEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<FirstChanceExceptionEventArgs> eventHandler3 = (EventHandler<FirstChanceExceptionEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<FirstChanceExceptionEventArgs>>(ref this.FirstChanceException, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<FirstChanceExceptionEventArgs> eventHandler = this.FirstChanceException;
				EventHandler<FirstChanceExceptionEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<FirstChanceExceptionEventArgs> eventHandler3 = (EventHandler<FirstChanceExceptionEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<FirstChanceExceptionEventArgs>>(ref this.FirstChanceException, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060017D1 RID: 6097 RVA: 0x00003FB7 File Offset: 0x000021B7
		[MonoTODO]
		public bool IsHomogenous
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060017D2 RID: 6098 RVA: 0x00003FB7 File Offset: 0x000021B7
		[MonoTODO]
		public bool IsFullyTrusted
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060017D3 RID: 6099 RVA: 0x0005D279 File Offset: 0x0005B479
		public AppDomainManager DomainManager
		{
			get
			{
				return this._domain_manager;
			}
		}

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x060017D4 RID: 6100 RVA: 0x0005D284 File Offset: 0x0005B484
		// (remove) Token: 0x060017D5 RID: 6101 RVA: 0x0005D2BC File Offset: 0x0005B4BC
		public event ResolveEventHandler ReflectionOnlyAssemblyResolve
		{
			[CompilerGenerated]
			add
			{
				ResolveEventHandler resolveEventHandler = this.ReflectionOnlyAssemblyResolve;
				ResolveEventHandler resolveEventHandler2;
				do
				{
					resolveEventHandler2 = resolveEventHandler;
					ResolveEventHandler resolveEventHandler3 = (ResolveEventHandler)Delegate.Combine(resolveEventHandler2, value);
					resolveEventHandler = Interlocked.CompareExchange<ResolveEventHandler>(ref this.ReflectionOnlyAssemblyResolve, resolveEventHandler3, resolveEventHandler2);
				}
				while (resolveEventHandler != resolveEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				ResolveEventHandler resolveEventHandler = this.ReflectionOnlyAssemblyResolve;
				ResolveEventHandler resolveEventHandler2;
				do
				{
					resolveEventHandler2 = resolveEventHandler;
					ResolveEventHandler resolveEventHandler3 = (ResolveEventHandler)Delegate.Remove(resolveEventHandler2, value);
					resolveEventHandler = Interlocked.CompareExchange<ResolveEventHandler>(ref this.ReflectionOnlyAssemblyResolve, resolveEventHandler3, resolveEventHandler2);
				}
				while (resolveEventHandler != resolveEventHandler2);
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x060017D6 RID: 6102 RVA: 0x0005D2F1 File Offset: 0x0005B4F1
		public ActivationContext ActivationContext
		{
			get
			{
				return this._activation;
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060017D7 RID: 6103 RVA: 0x0005D2F9 File Offset: 0x0005B4F9
		public ApplicationIdentity ApplicationIdentity
		{
			get
			{
				return this._applicationIdentity;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060017D8 RID: 6104 RVA: 0x0005D301 File Offset: 0x0005B501
		public int Id
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this.getDomainID();
			}
		}

		// Token: 0x060017D9 RID: 6105 RVA: 0x0005D309 File Offset: 0x0005B509
		[MonoTODO("This routine only returns the parameter currently")]
		[ComVisible(false)]
		public string ApplyPolicy(string assemblyName)
		{
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			if (assemblyName.Length == 0)
			{
				throw new ArgumentException("assemblyName");
			}
			return assemblyName;
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x0005D330 File Offset: 0x0005B530
		public static AppDomain CreateDomain(string friendlyName, Evidence securityInfo, string appBasePath, string appRelativeSearchPath, bool shadowCopyFiles, AppDomainInitializer adInit, string[] adInitArgs)
		{
			AppDomainSetup appDomainSetup = AppDomain.CreateDomainSetup(appBasePath, appRelativeSearchPath, shadowCopyFiles);
			appDomainSetup.AppDomainInitializerArguments = adInitArgs;
			appDomainSetup.AppDomainInitializer = adInit;
			return AppDomain.CreateDomain(friendlyName, securityInfo, appDomainSetup);
		}

		// Token: 0x060017DB RID: 6107 RVA: 0x0005D35F File Offset: 0x0005B55F
		public int ExecuteAssemblyByName(string assemblyName)
		{
			return this.ExecuteAssemblyByName(assemblyName, null, null);
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x0005D36A File Offset: 0x0005B56A
		[Obsolete("Use an overload that does not take an Evidence parameter")]
		public int ExecuteAssemblyByName(string assemblyName, Evidence assemblySecurity)
		{
			return this.ExecuteAssemblyByName(assemblyName, assemblySecurity, null);
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x0005D378 File Offset: 0x0005B578
		[Obsolete("Use an overload that does not take an Evidence parameter")]
		public int ExecuteAssemblyByName(string assemblyName, Evidence assemblySecurity, params string[] args)
		{
			Assembly assembly = Assembly.Load(assemblyName, assemblySecurity);
			return this.ExecuteAssemblyInternal(assembly, args);
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x0005D398 File Offset: 0x0005B598
		[Obsolete("Use an overload that does not take an Evidence parameter")]
		public int ExecuteAssemblyByName(AssemblyName assemblyName, Evidence assemblySecurity, params string[] args)
		{
			Assembly assembly = Assembly.Load(assemblyName, assemblySecurity);
			return this.ExecuteAssemblyInternal(assembly, args);
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x0005D3B8 File Offset: 0x0005B5B8
		public int ExecuteAssemblyByName(string assemblyName, params string[] args)
		{
			Assembly assembly = Assembly.Load(assemblyName, null);
			return this.ExecuteAssemblyInternal(assembly, args);
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x0005D3D8 File Offset: 0x0005B5D8
		public int ExecuteAssemblyByName(AssemblyName assemblyName, params string[] args)
		{
			Assembly assembly = Assembly.Load(assemblyName, null);
			return this.ExecuteAssemblyInternal(assembly, args);
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x0005D3F5 File Offset: 0x0005B5F5
		public bool IsDefaultAppDomain()
		{
			return this == AppDomain.DefaultDomain;
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x0005D3FF File Offset: 0x0005B5FF
		public Assembly[] ReflectionOnlyGetAssemblies()
		{
			return this.GetAssemblies(true);
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x000174FB File Offset: 0x000156FB
		void _AppDomain.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x000174FB File Offset: 0x000156FB
		void _AppDomain.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x000174FB File Offset: 0x000156FB
		void _AppDomain.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x000174FB File Offset: 0x000156FB
		void _AppDomain.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x0005D408 File Offset: 0x0005B608
		public bool? IsCompatibilitySwitchSet(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return new bool?(this.compatibility_switch != null && this.compatibility_switch.Contains(value));
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x0005D434 File Offset: 0x0005B634
		internal void SetCompatibilitySwitch(string value)
		{
			if (this.compatibility_switch == null)
			{
				this.compatibility_switch = new List<string>();
			}
			this.compatibility_switch.Add(value);
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060017E9 RID: 6121 RVA: 0x0000408A File Offset: 0x0000228A
		// (set) Token: 0x060017EA RID: 6122 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO("Currently always returns false")]
		public static bool MonitoringIsEnabled
		{
			get
			{
				return false;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060017EB RID: 6123 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO]
		public long MonitoringSurvivedMemorySize
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060017EC RID: 6124 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO]
		public static long MonitoringSurvivedProcessMemorySize
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060017ED RID: 6125 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO]
		public long MonitoringTotalAllocatedMemorySize
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x060017EE RID: 6126 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO]
		public TimeSpan MonitoringTotalProcessorTime
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x0400153C RID: 5436
		private IntPtr _mono_app_domain;

		// Token: 0x0400153D RID: 5437
		private static string _process_guid;

		// Token: 0x0400153E RID: 5438
		[ThreadStatic]
		private static Dictionary<string, object> type_resolve_in_progress;

		// Token: 0x0400153F RID: 5439
		[ThreadStatic]
		private static Dictionary<string, object> assembly_resolve_in_progress;

		// Token: 0x04001540 RID: 5440
		[ThreadStatic]
		private static Dictionary<string, object> assembly_resolve_in_progress_refonly;

		// Token: 0x04001541 RID: 5441
		private Evidence _evidence;

		// Token: 0x04001542 RID: 5442
		private PermissionSet _granted;

		// Token: 0x04001543 RID: 5443
		private PrincipalPolicy _principalPolicy;

		// Token: 0x04001544 RID: 5444
		[ThreadStatic]
		private static IPrincipal _principal;

		// Token: 0x04001545 RID: 5445
		private static AppDomain default_domain;

		// Token: 0x04001546 RID: 5446
		[CompilerGenerated]
		private AssemblyLoadEventHandler AssemblyLoad;

		// Token: 0x04001547 RID: 5447
		[CompilerGenerated]
		private ResolveEventHandler AssemblyResolve;

		// Token: 0x04001548 RID: 5448
		[CompilerGenerated]
		private EventHandler DomainUnload;

		// Token: 0x04001549 RID: 5449
		[CompilerGenerated]
		private EventHandler ProcessExit;

		// Token: 0x0400154A RID: 5450
		[CompilerGenerated]
		private ResolveEventHandler ResourceResolve;

		// Token: 0x0400154B RID: 5451
		[CompilerGenerated]
		private ResolveEventHandler TypeResolve;

		// Token: 0x0400154C RID: 5452
		[CompilerGenerated]
		private UnhandledExceptionEventHandler UnhandledException;

		// Token: 0x0400154D RID: 5453
		[CompilerGenerated]
		private EventHandler<FirstChanceExceptionEventArgs> FirstChanceException;

		// Token: 0x0400154E RID: 5454
		private AppDomainManager _domain_manager;

		// Token: 0x0400154F RID: 5455
		[CompilerGenerated]
		private ResolveEventHandler ReflectionOnlyAssemblyResolve;

		// Token: 0x04001550 RID: 5456
		private ActivationContext _activation;

		// Token: 0x04001551 RID: 5457
		private ApplicationIdentity _applicationIdentity;

		// Token: 0x04001552 RID: 5458
		private List<string> compatibility_switch;

		// Token: 0x020001EB RID: 491
		[Serializable]
		private class Loader
		{
			// Token: 0x060017EF RID: 6127 RVA: 0x0005D455 File Offset: 0x0005B655
			public Loader(string assembly)
			{
				this.assembly = assembly;
			}

			// Token: 0x060017F0 RID: 6128 RVA: 0x0005D464 File Offset: 0x0005B664
			public void Load()
			{
				Assembly.LoadFrom(this.assembly);
			}

			// Token: 0x04001553 RID: 5459
			private string assembly;
		}

		// Token: 0x020001EC RID: 492
		[Serializable]
		private class Initializer
		{
			// Token: 0x060017F1 RID: 6129 RVA: 0x0005D472 File Offset: 0x0005B672
			public Initializer(AppDomainInitializer initializer, string[] arguments)
			{
				this.initializer = initializer;
				this.arguments = arguments;
			}

			// Token: 0x060017F2 RID: 6130 RVA: 0x0005D488 File Offset: 0x0005B688
			public void Initialize()
			{
				this.initializer(this.arguments);
			}

			// Token: 0x04001554 RID: 5460
			private AppDomainInitializer initializer;

			// Token: 0x04001555 RID: 5461
			private string[] arguments;
		}
	}
}
