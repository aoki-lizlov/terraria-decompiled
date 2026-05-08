using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Threading;

namespace System.Reflection
{
	// Token: 0x020008C1 RID: 2241
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_Assembly))]
	[ClassInterface(ClassInterfaceType.None)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal class RuntimeAssembly : Assembly
	{
		// Token: 0x06004C20 RID: 19488 RVA: 0x000F2BFB File Offset: 0x000F0DFB
		protected RuntimeAssembly()
		{
			this.resolve_event_holder = new Assembly.ResolveEventHolder();
		}

		// Token: 0x06004C21 RID: 19489 RVA: 0x000F2C0E File Offset: 0x000F0E0E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			UnitySerializationHolder.GetUnitySerializationInfo(info, 6, this.FullName, this);
		}

		// Token: 0x06004C22 RID: 19490 RVA: 0x00047E00 File Offset: 0x00046000
		internal static RuntimeAssembly GetExecutingAssembly(ref StackCrawlMark stackMark)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004C23 RID: 19491 RVA: 0x000F2C2C File Offset: 0x000F0E2C
		[SecurityCritical]
		internal static AssemblyName CreateAssemblyName(string assemblyString, bool forIntrospection, out RuntimeAssembly assemblyFromResolveEvent)
		{
			if (assemblyString == null)
			{
				throw new ArgumentNullException("assemblyString");
			}
			if (assemblyString.Length == 0 || assemblyString[0] == '\0')
			{
				throw new ArgumentException(Environment.GetResourceString("String cannot have zero length."));
			}
			if (forIntrospection)
			{
				AppDomain.CheckReflectionOnlyLoadSupported();
			}
			AssemblyName assemblyName = new AssemblyName();
			assemblyName.Name = assemblyString;
			assemblyFromResolveEvent = null;
			return assemblyName;
		}

		// Token: 0x06004C24 RID: 19492 RVA: 0x000F2C7F File Offset: 0x000F0E7F
		internal static RuntimeAssembly InternalLoadAssemblyName(AssemblyName assemblyRef, Evidence assemblySecurity, RuntimeAssembly reqAssembly, ref StackCrawlMark stackMark, bool throwOnFileNotFound, bool forIntrospection, bool suppressSecurityChecks)
		{
			if (assemblyRef == null)
			{
				throw new ArgumentNullException("assemblyRef");
			}
			if (assemblyRef.CodeBase != null)
			{
				AppDomain.CheckLoadFromSupported();
			}
			assemblyRef = (AssemblyName)assemblyRef.Clone();
			if (assemblySecurity != null)
			{
			}
			return (RuntimeAssembly)Assembly.Load(assemblyRef);
		}

		// Token: 0x06004C25 RID: 19493 RVA: 0x000F2CBA File Offset: 0x000F0EBA
		internal static RuntimeAssembly LoadWithPartialNameInternal(string partialName, Evidence securityEvidence, ref StackCrawlMark stackMark)
		{
			return (RuntimeAssembly)Assembly.LoadWithPartialName(partialName, securityEvidence);
		}

		// Token: 0x06004C26 RID: 19494 RVA: 0x000F2CC8 File Offset: 0x000F0EC8
		internal static RuntimeAssembly LoadWithPartialNameInternal(AssemblyName an, Evidence securityEvidence, ref StackCrawlMark stackMark)
		{
			return RuntimeAssembly.LoadWithPartialNameInternal(an.ToString(), securityEvidence, ref stackMark);
		}

		// Token: 0x06004C27 RID: 19495 RVA: 0x000F2CD7 File Offset: 0x000F0ED7
		public override AssemblyName GetName(bool copiedName)
		{
			if (SecurityManager.SecurityEnabled)
			{
				string codeBase = this.CodeBase;
			}
			return AssemblyName.Create(this, true);
		}

		// Token: 0x06004C28 RID: 19496 RVA: 0x000F2CEE File Offset: 0x000F0EEE
		public override Type GetType(string name, bool throwOnError, bool ignoreCase)
		{
			if (name == null)
			{
				throw new ArgumentNullException(name);
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("name", "Name cannot be empty");
			}
			return base.InternalGetType(null, name, throwOnError, ignoreCase);
		}

		// Token: 0x06004C29 RID: 19497 RVA: 0x000F2D1C File Offset: 0x000F0F1C
		public override Module GetModule(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("Name can't be empty");
			}
			foreach (Module module in this.GetModules(true))
			{
				if (module.ScopeName == name)
				{
					return module;
				}
			}
			return null;
		}

		// Token: 0x06004C2A RID: 19498 RVA: 0x000F2D75 File Offset: 0x000F0F75
		public override AssemblyName[] GetReferencedAssemblies()
		{
			return Assembly.GetReferencedAssemblies(this);
		}

		// Token: 0x06004C2B RID: 19499 RVA: 0x000F2D80 File Offset: 0x000F0F80
		public override Module[] GetModules(bool getResourceModules)
		{
			Module[] modulesInternal = this.GetModulesInternal();
			if (!getResourceModules)
			{
				List<Module> list = new List<Module>(modulesInternal.Length);
				foreach (Module module in modulesInternal)
				{
					if (!module.IsResource())
					{
						list.Add(module);
					}
				}
				return list.ToArray();
			}
			return modulesInternal;
		}

		// Token: 0x06004C2C RID: 19500 RVA: 0x000F2DCE File Offset: 0x000F0FCE
		[MonoTODO("Always returns the same as GetModules")]
		public override Module[] GetLoadedModules(bool getResourceModules)
		{
			return this.GetModules(getResourceModules);
		}

		// Token: 0x06004C2D RID: 19501 RVA: 0x000F2DD8 File Offset: 0x000F0FD8
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override Assembly GetSatelliteAssembly(CultureInfo culture)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return base.GetSatelliteAssembly(culture, null, true, ref stackCrawlMark);
		}

		// Token: 0x06004C2E RID: 19502 RVA: 0x000F2DF4 File Offset: 0x000F0FF4
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override Assembly GetSatelliteAssembly(CultureInfo culture, Version version)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return base.GetSatelliteAssembly(culture, version, true, ref stackCrawlMark);
		}

		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x06004C2F RID: 19503 RVA: 0x000F2E0E File Offset: 0x000F100E
		[ComVisible(false)]
		public override Module ManifestModule
		{
			get
			{
				return this.GetManifestModule();
			}
		}

		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x06004C30 RID: 19504 RVA: 0x000F2E16 File Offset: 0x000F1016
		public override bool GlobalAssemblyCache
		{
			get
			{
				return this.get_global_assembly_cache();
			}
		}

		// Token: 0x06004C31 RID: 19505 RVA: 0x000F2E1E File Offset: 0x000F101E
		public override Type[] GetExportedTypes()
		{
			return this.GetTypes(true);
		}

		// Token: 0x06004C32 RID: 19506 RVA: 0x000F2E28 File Offset: 0x000F1028
		internal static byte[] GetAotId()
		{
			byte[] array = new byte[16];
			if (RuntimeAssembly.GetAotIdInternal(array))
			{
				return array;
			}
			return null;
		}

		// Token: 0x06004C33 RID: 19507
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string get_code_base(Assembly a, bool escaped);

		// Token: 0x06004C34 RID: 19508
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string get_location();

		// Token: 0x06004C35 RID: 19509
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string get_fullname(Assembly a);

		// Token: 0x06004C36 RID: 19510
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool GetAotIdInternal(byte[] aotid);

		// Token: 0x06004C37 RID: 19511
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string InternalImageRuntimeVersion(Assembly a);

		// Token: 0x06004C38 RID: 19512
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool get_global_assembly_cache();

		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x06004C39 RID: 19513
		public override extern MethodInfo EntryPoint
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x06004C3A RID: 19514
		[ComVisible(false)]
		public override extern bool ReflectionOnly
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06004C3B RID: 19515 RVA: 0x000F2E48 File Offset: 0x000F1048
		internal static string GetCodeBase(Assembly a, bool escaped)
		{
			string text = RuntimeAssembly.get_code_base(a, escaped);
			if (SecurityManager.SecurityEnabled && string.Compare("FILE://", 0, text, 0, 7, true, CultureInfo.InvariantCulture) == 0)
			{
				string text2 = text.Substring(7);
				new FileIOPermission(FileIOPermissionAccess.PathDiscovery, text2).Demand();
			}
			return text;
		}

		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x06004C3C RID: 19516 RVA: 0x000F2E8F File Offset: 0x000F108F
		public override string CodeBase
		{
			get
			{
				return RuntimeAssembly.GetCodeBase(this, false);
			}
		}

		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x06004C3D RID: 19517 RVA: 0x000F2E98 File Offset: 0x000F1098
		public override string EscapedCodeBase
		{
			get
			{
				return RuntimeAssembly.GetCodeBase(this, true);
			}
		}

		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x06004C3E RID: 19518 RVA: 0x000F2EA1 File Offset: 0x000F10A1
		public override string FullName
		{
			get
			{
				return RuntimeAssembly.get_fullname(this);
			}
		}

		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x06004C3F RID: 19519 RVA: 0x000F2EA9 File Offset: 0x000F10A9
		[ComVisible(false)]
		public override string ImageRuntimeVersion
		{
			get
			{
				return RuntimeAssembly.InternalImageRuntimeVersion(this);
			}
		}

		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x06004C40 RID: 19520 RVA: 0x000F2EB1 File Offset: 0x000F10B1
		internal override IntPtr MonoAssembly
		{
			get
			{
				return this._mono_assembly;
			}
		}

		// Token: 0x17000C6C RID: 3180
		// (set) Token: 0x06004C41 RID: 19521 RVA: 0x000F2EB9 File Offset: 0x000F10B9
		internal override bool FromByteArray
		{
			set
			{
				this.fromByteArray = value;
			}
		}

		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x06004C42 RID: 19522 RVA: 0x000F2EC4 File Offset: 0x000F10C4
		public override string Location
		{
			get
			{
				if (this.fromByteArray)
				{
					return string.Empty;
				}
				string location = this.get_location();
				if (location != string.Empty && SecurityManager.SecurityEnabled)
				{
					new FileIOPermission(FileIOPermissionAccess.PathDiscovery, location).Demand();
				}
				return location;
			}
		}

		// Token: 0x06004C43 RID: 19523
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool GetManifestResourceInfoInternal(string name, ManifestResourceInfo info);

		// Token: 0x06004C44 RID: 19524 RVA: 0x000F2F08 File Offset: 0x000F1108
		public override ManifestResourceInfo GetManifestResourceInfo(string resourceName)
		{
			if (resourceName == null)
			{
				throw new ArgumentNullException("resourceName");
			}
			if (resourceName.Length == 0)
			{
				throw new ArgumentException("String cannot have zero length.");
			}
			ManifestResourceInfo manifestResourceInfo = new ManifestResourceInfo(null, null, (ResourceLocation)0);
			if (this.GetManifestResourceInfoInternal(resourceName, manifestResourceInfo))
			{
				return manifestResourceInfo;
			}
			return null;
		}

		// Token: 0x06004C45 RID: 19525
		[MethodImpl(MethodImplOptions.InternalCall)]
		public override extern string[] GetManifestResourceNames();

		// Token: 0x06004C46 RID: 19526
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern IntPtr GetManifestResourceInternal(string name, out int size, out Module module);

		// Token: 0x06004C47 RID: 19527 RVA: 0x000F2F4C File Offset: 0x000F114C
		public unsafe override Stream GetManifestResourceStream(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("String cannot have zero length.", "name");
			}
			ManifestResourceInfo manifestResourceInfo = this.GetManifestResourceInfo(name);
			if (manifestResourceInfo == null)
			{
				Assembly assembly = AppDomain.CurrentDomain.DoResourceResolve(name, this);
				if (assembly != null && assembly != this)
				{
					return assembly.GetManifestResourceStream(name);
				}
				return null;
			}
			else
			{
				if (manifestResourceInfo.ReferencedAssembly != null)
				{
					return manifestResourceInfo.ReferencedAssembly.GetManifestResourceStream(name);
				}
				if (manifestResourceInfo.FileName != null && manifestResourceInfo.ResourceLocation == (ResourceLocation)0)
				{
					if (this.fromByteArray)
					{
						throw new FileNotFoundException(manifestResourceInfo.FileName);
					}
					return new FileStream(Path.Combine(Path.GetDirectoryName(this.Location), manifestResourceInfo.FileName), FileMode.Open, FileAccess.Read);
				}
				else
				{
					int num;
					Module module;
					IntPtr manifestResourceInternal = this.GetManifestResourceInternal(name, out num, out module);
					if (manifestResourceInternal == (IntPtr)0)
					{
						return null;
					}
					return new RuntimeAssembly.UnmanagedMemoryStreamForModule((byte*)(void*)manifestResourceInternal, (long)num, module);
				}
			}
		}

		// Token: 0x06004C48 RID: 19528 RVA: 0x000F3040 File Offset: 0x000F1240
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override Stream GetManifestResourceStream(Type type, string name)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return base.GetManifestResourceStream(type, name, false, ref stackCrawlMark);
		}

		// Token: 0x06004C49 RID: 19529 RVA: 0x000534DE File Offset: 0x000516DE
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		// Token: 0x06004C4A RID: 19530 RVA: 0x000F133D File Offset: 0x000EF53D
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, inherit);
		}

		// Token: 0x06004C4B RID: 19531 RVA: 0x000F1346 File Offset: 0x000EF546
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
		}

		// Token: 0x06004C4C RID: 19532 RVA: 0x000F305A File Offset: 0x000F125A
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributes(this);
		}

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06004C4D RID: 19533 RVA: 0x000F3062 File Offset: 0x000F1262
		// (remove) Token: 0x06004C4E RID: 19534 RVA: 0x000F3070 File Offset: 0x000F1270
		public override event ModuleResolveEventHandler ModuleResolve
		{
			[SecurityPermission(SecurityAction.LinkDemand, ControlAppDomain = true)]
			add
			{
				this.resolve_event_holder.ModuleResolve += value;
			}
			[SecurityPermission(SecurityAction.LinkDemand, ControlAppDomain = true)]
			remove
			{
				this.resolve_event_holder.ModuleResolve -= value;
			}
		}

		// Token: 0x06004C4F RID: 19535 RVA: 0x000F307E File Offset: 0x000F127E
		internal override Module GetManifestModule()
		{
			return this.GetManifestModuleInternal();
		}

		// Token: 0x06004C50 RID: 19536
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Module GetManifestModuleInternal();

		// Token: 0x06004C51 RID: 19537
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal override extern Module[] GetModulesInternal();

		// Token: 0x06004C52 RID: 19538
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern object GetFilesInternal(string name, bool getResourceModules);

		// Token: 0x06004C53 RID: 19539 RVA: 0x000F3088 File Offset: 0x000F1288
		public override FileStream[] GetFiles(bool getResourceModules)
		{
			string[] array = (string[])this.GetFilesInternal(null, getResourceModules);
			if (array == null)
			{
				return EmptyArray<FileStream>.Value;
			}
			string location = this.Location;
			FileStream[] array2;
			if (location != string.Empty)
			{
				array2 = new FileStream[array.Length + 1];
				array2[0] = new FileStream(location, FileMode.Open, FileAccess.Read);
				for (int i = 0; i < array.Length; i++)
				{
					array2[i + 1] = new FileStream(array[i], FileMode.Open, FileAccess.Read);
				}
			}
			else
			{
				array2 = new FileStream[array.Length];
				for (int j = 0; j < array.Length; j++)
				{
					array2[j] = new FileStream(array[j], FileMode.Open, FileAccess.Read);
				}
			}
			return array2;
		}

		// Token: 0x06004C54 RID: 19540 RVA: 0x000F3120 File Offset: 0x000F1320
		public override FileStream GetFile(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException(null, "Name cannot be null.");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("Empty name is not valid");
			}
			string text = (string)this.GetFilesInternal(name, true);
			if (text != null)
			{
				return new FileStream(text, FileMode.Open, FileAccess.Read);
			}
			return null;
		}

		// Token: 0x06004C55 RID: 19541 RVA: 0x000F316A File Offset: 0x000F136A
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06004C56 RID: 19542 RVA: 0x000F3172 File Offset: 0x000F1372
		public override bool Equals(object o)
		{
			return this == o || (o != null && o is RuntimeAssembly && ((RuntimeAssembly)o)._mono_assembly == this._mono_assembly);
		}

		// Token: 0x06004C57 RID: 19543 RVA: 0x000F319F File Offset: 0x000F139F
		public override string ToString()
		{
			if (this.assemblyName != null)
			{
				return this.assemblyName;
			}
			this.assemblyName = this.FullName;
			return this.assemblyName;
		}

		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x06004C58 RID: 19544 RVA: 0x000F31C2 File Offset: 0x000F13C2
		public override Evidence Evidence
		{
			[SecurityPermission(SecurityAction.Demand, ControlEvidence = true)]
			get
			{
				return this.UnprotectedGetEvidence();
			}
		}

		// Token: 0x06004C59 RID: 19545 RVA: 0x000F31CC File Offset: 0x000F13CC
		internal override Evidence UnprotectedGetEvidence()
		{
			if (this._evidence == null)
			{
				lock (this)
				{
					this._evidence = Evidence.GetDefaultHostEvidence(this);
				}
			}
			return this._evidence;
		}

		// Token: 0x06004C5A RID: 19546 RVA: 0x000F321C File Offset: 0x000F141C
		internal void Resolve()
		{
			lock (this)
			{
				this.LoadAssemblyPermissions();
				Evidence evidence = new Evidence(this.UnprotectedGetEvidence());
				evidence.AddHost(new PermissionRequestEvidence(this._minimum, this._optional, this._refuse));
				this._granted = SecurityManager.ResolvePolicy(evidence, this._minimum, this._optional, this._refuse, out this._denied);
			}
		}

		// Token: 0x17000C6F RID: 3183
		// (get) Token: 0x06004C5B RID: 19547 RVA: 0x000F32A4 File Offset: 0x000F14A4
		internal override PermissionSet GrantedPermissionSet
		{
			get
			{
				if (this._granted == null)
				{
					if (SecurityManager.ResolvingPolicyLevel != null)
					{
						if (SecurityManager.ResolvingPolicyLevel.IsFullTrustAssembly(this))
						{
							return DefaultPolicies.FullTrust;
						}
						return null;
					}
					else
					{
						this.Resolve();
					}
				}
				return this._granted;
			}
		}

		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x06004C5C RID: 19548 RVA: 0x000F32D6 File Offset: 0x000F14D6
		internal override PermissionSet DeniedPermissionSet
		{
			get
			{
				if (this._granted == null)
				{
					if (SecurityManager.ResolvingPolicyLevel != null)
					{
						if (SecurityManager.ResolvingPolicyLevel.IsFullTrustAssembly(this))
						{
							return null;
						}
						return DefaultPolicies.FullTrust;
					}
					else
					{
						this.Resolve();
					}
				}
				return this._denied;
			}
		}

		// Token: 0x06004C5D RID: 19549
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool LoadPermissions(Assembly a, ref IntPtr minimum, ref int minLength, ref IntPtr optional, ref int optLength, ref IntPtr refused, ref int refLength);

		// Token: 0x06004C5E RID: 19550 RVA: 0x000F3308 File Offset: 0x000F1508
		private void LoadAssemblyPermissions()
		{
			IntPtr zero = IntPtr.Zero;
			IntPtr zero2 = IntPtr.Zero;
			IntPtr zero3 = IntPtr.Zero;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			if (RuntimeAssembly.LoadPermissions(this, ref zero, ref num, ref zero2, ref num2, ref zero3, ref num3))
			{
				if (num > 0)
				{
					byte[] array = new byte[num];
					Marshal.Copy(zero, array, 0, num);
					this._minimum = SecurityManager.Decode(array);
				}
				if (num2 > 0)
				{
					byte[] array2 = new byte[num2];
					Marshal.Copy(zero2, array2, 0, num2);
					this._optional = SecurityManager.Decode(array2);
				}
				if (num3 > 0)
				{
					byte[] array3 = new byte[num3];
					Marshal.Copy(zero3, array3, 0, num3);
					this._refuse = SecurityManager.Decode(array3);
				}
			}
		}

		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x06004C5F RID: 19551 RVA: 0x000F33B2 File Offset: 0x000F15B2
		public override PermissionSet PermissionSet
		{
			get
			{
				return this.GrantedPermissionSet;
			}
		}

		// Token: 0x04002FBA RID: 12218
		internal IntPtr _mono_assembly;

		// Token: 0x04002FBB RID: 12219
		internal Evidence _evidence;

		// Token: 0x04002FBC RID: 12220
		internal Assembly.ResolveEventHolder resolve_event_holder;

		// Token: 0x04002FBD RID: 12221
		internal PermissionSet _minimum;

		// Token: 0x04002FBE RID: 12222
		internal PermissionSet _optional;

		// Token: 0x04002FBF RID: 12223
		internal PermissionSet _refuse;

		// Token: 0x04002FC0 RID: 12224
		internal PermissionSet _granted;

		// Token: 0x04002FC1 RID: 12225
		internal PermissionSet _denied;

		// Token: 0x04002FC2 RID: 12226
		internal bool fromByteArray;

		// Token: 0x04002FC3 RID: 12227
		internal string assemblyName;

		// Token: 0x020008C2 RID: 2242
		internal class UnmanagedMemoryStreamForModule : UnmanagedMemoryStream
		{
			// Token: 0x06004C60 RID: 19552 RVA: 0x000F33BA File Offset: 0x000F15BA
			public unsafe UnmanagedMemoryStreamForModule(byte* pointer, long length, Module module)
				: base(pointer, length)
			{
				this.module = module;
			}

			// Token: 0x06004C61 RID: 19553 RVA: 0x000F33CB File Offset: 0x000F15CB
			protected override void Dispose(bool disposing)
			{
				if (this._isOpen)
				{
					this.module = null;
				}
				base.Dispose(disposing);
			}

			// Token: 0x04002FC4 RID: 12228
			private Module module;
		}
	}
}
