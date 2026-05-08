using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;
using System.Threading;
using Mono;

namespace System.Reflection
{
	// Token: 0x020008B8 RID: 2232
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_Assembly))]
	[ClassInterface(ClassInterfaceType.None)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public abstract class Assembly : ICustomAttributeProvider, _Assembly, IEvidenceFactory, ISerializable
	{
		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06004B4B RID: 19275 RVA: 0x000174FB File Offset: 0x000156FB
		// (remove) Token: 0x06004B4C RID: 19276 RVA: 0x000174FB File Offset: 0x000156FB
		public virtual event ModuleResolveEventHandler ModuleResolve
		{
			[SecurityPermission(SecurityAction.LinkDemand, ControlAppDomain = true)]
			add
			{
				throw new NotImplementedException();
			}
			[SecurityPermission(SecurityAction.LinkDemand, ControlAppDomain = true)]
			remove
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x06004B4D RID: 19277 RVA: 0x000174FB File Offset: 0x000156FB
		public virtual string CodeBase
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x06004B4E RID: 19278 RVA: 0x000174FB File Offset: 0x000156FB
		public virtual string EscapedCodeBase
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x06004B4F RID: 19279 RVA: 0x000174FB File Offset: 0x000156FB
		public virtual string FullName
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x06004B50 RID: 19280 RVA: 0x000174FB File Offset: 0x000156FB
		public virtual MethodInfo EntryPoint
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x06004B51 RID: 19281 RVA: 0x000174FB File Offset: 0x000156FB
		public virtual Evidence Evidence
		{
			[SecurityPermission(SecurityAction.Demand, ControlEvidence = true)]
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06004B52 RID: 19282 RVA: 0x000174FB File Offset: 0x000156FB
		internal virtual Evidence UnprotectedGetEvidence()
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x06004B53 RID: 19283 RVA: 0x000174FB File Offset: 0x000156FB
		internal virtual IntPtr MonoAssembly
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000C2F RID: 3119
		// (set) Token: 0x06004B54 RID: 19284 RVA: 0x000174FB File Offset: 0x000156FB
		internal virtual bool FromByteArray
		{
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000C30 RID: 3120
		// (get) Token: 0x06004B55 RID: 19285 RVA: 0x000174FB File Offset: 0x000156FB
		public virtual string Location
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000C31 RID: 3121
		// (get) Token: 0x06004B56 RID: 19286 RVA: 0x000174FB File Offset: 0x000156FB
		[ComVisible(false)]
		public virtual string ImageRuntimeVersion
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06004B57 RID: 19287 RVA: 0x000174FB File Offset: 0x000156FB
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004B58 RID: 19288 RVA: 0x000174FB File Offset: 0x000156FB
		public virtual bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004B59 RID: 19289 RVA: 0x000174FB File Offset: 0x000156FB
		public virtual object[] GetCustomAttributes(bool inherit)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004B5A RID: 19290 RVA: 0x000174FB File Offset: 0x000156FB
		public virtual object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004B5B RID: 19291 RVA: 0x000F1419 File Offset: 0x000EF619
		public virtual FileStream[] GetFiles()
		{
			return this.GetFiles(false);
		}

		// Token: 0x06004B5C RID: 19292 RVA: 0x000174FB File Offset: 0x000156FB
		public virtual FileStream[] GetFiles(bool getResourceModules)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004B5D RID: 19293 RVA: 0x000174FB File Offset: 0x000156FB
		public virtual FileStream GetFile(string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004B5E RID: 19294 RVA: 0x000174FB File Offset: 0x000156FB
		public virtual Stream GetManifestResourceStream(string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004B5F RID: 19295 RVA: 0x000174FB File Offset: 0x000156FB
		public virtual Stream GetManifestResourceStream(Type type, string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004B60 RID: 19296 RVA: 0x000F1424 File Offset: 0x000EF624
		internal Stream GetManifestResourceStream(Type type, string name, bool skipSecurityCheck, ref StackCrawlMark stackMark)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (type == null)
			{
				if (name == null)
				{
					throw new ArgumentNullException("type");
				}
			}
			else
			{
				string @namespace = type.Namespace;
				if (@namespace != null)
				{
					stringBuilder.Append(@namespace);
					if (name != null)
					{
						stringBuilder.Append(Type.Delimiter);
					}
				}
			}
			if (name != null)
			{
				stringBuilder.Append(name);
			}
			return this.GetManifestResourceStream(stringBuilder.ToString());
		}

		// Token: 0x06004B61 RID: 19297 RVA: 0x000F1486 File Offset: 0x000EF686
		internal Stream GetManifestResourceStream(string name, ref StackCrawlMark stackMark, bool skipSecurityCheck)
		{
			return this.GetManifestResourceStream(null, name, skipSecurityCheck, ref stackMark);
		}

		// Token: 0x06004B62 RID: 19298 RVA: 0x000F1492 File Offset: 0x000EF692
		internal string GetSimpleName()
		{
			return this.GetName(true).Name;
		}

		// Token: 0x06004B63 RID: 19299 RVA: 0x000F14A0 File Offset: 0x000EF6A0
		internal byte[] GetPublicKey()
		{
			return this.GetName(true).GetPublicKey();
		}

		// Token: 0x06004B64 RID: 19300 RVA: 0x000F14AE File Offset: 0x000EF6AE
		internal Version GetVersion()
		{
			return this.GetName(true).Version;
		}

		// Token: 0x06004B65 RID: 19301 RVA: 0x000F14BC File Offset: 0x000EF6BC
		private AssemblyNameFlags GetFlags()
		{
			return this.GetName(true).Flags;
		}

		// Token: 0x06004B66 RID: 19302
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal virtual extern Type[] GetTypes(bool exportedOnly);

		// Token: 0x06004B67 RID: 19303 RVA: 0x000F14CA File Offset: 0x000EF6CA
		public virtual Type[] GetTypes()
		{
			return this.GetTypes(false);
		}

		// Token: 0x06004B68 RID: 19304 RVA: 0x000174FB File Offset: 0x000156FB
		public virtual Type[] GetExportedTypes()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004B69 RID: 19305 RVA: 0x000F14D3 File Offset: 0x000EF6D3
		public virtual Type GetType(string name, bool throwOnError)
		{
			return this.GetType(name, throwOnError, false);
		}

		// Token: 0x06004B6A RID: 19306 RVA: 0x000F14DE File Offset: 0x000EF6DE
		public virtual Type GetType(string name)
		{
			return this.GetType(name, false, false);
		}

		// Token: 0x06004B6B RID: 19307
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Type InternalGetType(Module module, string name, bool throwOnError, bool ignoreCase);

		// Token: 0x06004B6C RID: 19308
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InternalGetAssemblyName(string assemblyFile, out MonoAssemblyName aname, out string codebase);

		// Token: 0x06004B6D RID: 19309 RVA: 0x000174FB File Offset: 0x000156FB
		public virtual AssemblyName GetName(bool copiedName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004B6E RID: 19310 RVA: 0x000F14E9 File Offset: 0x000EF6E9
		public virtual AssemblyName GetName()
		{
			return this.GetName(false);
		}

		// Token: 0x06004B6F RID: 19311 RVA: 0x00097F83 File Offset: 0x00096183
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06004B70 RID: 19312 RVA: 0x000F14F2 File Offset: 0x000EF6F2
		public static string CreateQualifiedName(string assemblyName, string typeName)
		{
			return typeName + ", " + assemblyName;
		}

		// Token: 0x06004B71 RID: 19313 RVA: 0x000F1500 File Offset: 0x000EF700
		public static Assembly GetAssembly(Type type)
		{
			if (type != null)
			{
				return type.Assembly;
			}
			throw new ArgumentNullException("type");
		}

		// Token: 0x06004B72 RID: 19314
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Assembly GetEntryAssembly();

		// Token: 0x06004B73 RID: 19315 RVA: 0x000F151C File Offset: 0x000EF71C
		internal Assembly GetSatelliteAssembly(CultureInfo culture, Version version, bool throwOnError, ref StackCrawlMark stackMark)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			string text = this.GetSimpleName() + ".resources";
			return this.InternalGetSatelliteAssembly(text, culture, version, true, ref stackMark);
		}

		// Token: 0x06004B74 RID: 19316 RVA: 0x000F1554 File Offset: 0x000EF754
		internal RuntimeAssembly InternalGetSatelliteAssembly(string name, CultureInfo culture, Version version, bool throwOnFileNotFound, ref StackCrawlMark stackMark)
		{
			AssemblyName assemblyName = new AssemblyName();
			assemblyName.SetPublicKey(this.GetPublicKey());
			assemblyName.Flags = this.GetFlags() | AssemblyNameFlags.PublicKey;
			if (version == null)
			{
				assemblyName.Version = this.GetVersion();
			}
			else
			{
				assemblyName.Version = version;
			}
			assemblyName.CultureInfo = culture;
			assemblyName.Name = name;
			try
			{
				Assembly assembly = AppDomain.CurrentDomain.LoadSatellite(assemblyName, false, ref stackMark);
				if (assembly != null)
				{
					return (RuntimeAssembly)assembly;
				}
			}
			catch (FileNotFoundException)
			{
			}
			if (string.IsNullOrEmpty(this.Location))
			{
				return null;
			}
			string text = Path.Combine(Path.GetDirectoryName(this.Location), Path.Combine(culture.Name, assemblyName.Name + ".dll"));
			RuntimeAssembly runtimeAssembly;
			try
			{
				runtimeAssembly = (RuntimeAssembly)Assembly.LoadFrom(text, false, ref stackMark);
			}
			catch
			{
				if (throwOnFileNotFound || File.Exists(text))
				{
					throw;
				}
				runtimeAssembly = null;
			}
			return runtimeAssembly;
		}

		// Token: 0x06004B75 RID: 19317 RVA: 0x00047D48 File Offset: 0x00045F48
		Type _Assembly.GetType()
		{
			return base.GetType();
		}

		// Token: 0x06004B76 RID: 19318
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Assembly LoadFrom(string assemblyFile, bool refOnly, ref StackCrawlMark stackMark);

		// Token: 0x06004B77 RID: 19319
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Assembly LoadFile_internal(string assemblyFile, ref StackCrawlMark stackMark);

		// Token: 0x06004B78 RID: 19320 RVA: 0x000F1654 File Offset: 0x000EF854
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly LoadFrom(string assemblyFile)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Assembly.LoadFrom(assemblyFile, false, ref stackCrawlMark);
		}

		// Token: 0x06004B79 RID: 19321 RVA: 0x000F166C File Offset: 0x000EF86C
		[Obsolete]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly LoadFrom(string assemblyFile, Evidence securityEvidence)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Assembly assembly = Assembly.LoadFrom(assemblyFile, false, ref stackCrawlMark);
			if (assembly != null && securityEvidence != null)
			{
				assembly.Evidence.Merge(securityEvidence);
			}
			return assembly;
		}

		// Token: 0x06004B7A RID: 19322 RVA: 0x000174FB File Offset: 0x000156FB
		[Obsolete]
		[MonoTODO("This overload is not currently implemented")]
		public static Assembly LoadFrom(string assemblyFile, Evidence securityEvidence, byte[] hashValue, AssemblyHashAlgorithm hashAlgorithm)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004B7B RID: 19323 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO]
		public static Assembly LoadFrom(string assemblyFile, byte[] hashValue, AssemblyHashAlgorithm hashAlgorithm)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004B7C RID: 19324 RVA: 0x000F16A0 File Offset: 0x000EF8A0
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly UnsafeLoadFrom(string assemblyFile)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Assembly.LoadFrom(assemblyFile, false, ref stackCrawlMark);
		}

		// Token: 0x06004B7D RID: 19325 RVA: 0x000F16B8 File Offset: 0x000EF8B8
		[Obsolete]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly LoadFile(string path, Evidence securityEvidence)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path == string.Empty)
			{
				throw new ArgumentException("Path can't be empty", "path");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Assembly assembly = Assembly.LoadFile_internal(path, ref stackCrawlMark);
			if (assembly != null && securityEvidence != null)
			{
				throw new NotImplementedException();
			}
			return assembly;
		}

		// Token: 0x06004B7E RID: 19326 RVA: 0x000F170C File Offset: 0x000EF90C
		public static Assembly LoadFile(string path)
		{
			return Assembly.LoadFile(path, null);
		}

		// Token: 0x06004B7F RID: 19327 RVA: 0x000F1715 File Offset: 0x000EF915
		public static Assembly Load(string assemblyString)
		{
			return AppDomain.CurrentDomain.Load(assemblyString);
		}

		// Token: 0x06004B80 RID: 19328 RVA: 0x000F1722 File Offset: 0x000EF922
		[Obsolete]
		public static Assembly Load(string assemblyString, Evidence assemblySecurity)
		{
			return AppDomain.CurrentDomain.Load(assemblyString, assemblySecurity);
		}

		// Token: 0x06004B81 RID: 19329 RVA: 0x000F1730 File Offset: 0x000EF930
		public static Assembly Load(AssemblyName assemblyRef)
		{
			return AppDomain.CurrentDomain.Load(assemblyRef);
		}

		// Token: 0x06004B82 RID: 19330 RVA: 0x000F173D File Offset: 0x000EF93D
		[Obsolete]
		public static Assembly Load(AssemblyName assemblyRef, Evidence assemblySecurity)
		{
			return AppDomain.CurrentDomain.Load(assemblyRef, assemblySecurity);
		}

		// Token: 0x06004B83 RID: 19331 RVA: 0x000F174B File Offset: 0x000EF94B
		public static Assembly Load(byte[] rawAssembly)
		{
			return AppDomain.CurrentDomain.Load(rawAssembly);
		}

		// Token: 0x06004B84 RID: 19332 RVA: 0x000F1758 File Offset: 0x000EF958
		public static Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore)
		{
			return AppDomain.CurrentDomain.Load(rawAssembly, rawSymbolStore);
		}

		// Token: 0x06004B85 RID: 19333 RVA: 0x000F1766 File Offset: 0x000EF966
		[Obsolete]
		public static Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore, Evidence securityEvidence)
		{
			return AppDomain.CurrentDomain.Load(rawAssembly, rawSymbolStore, securityEvidence);
		}

		// Token: 0x06004B86 RID: 19334 RVA: 0x000F1758 File Offset: 0x000EF958
		[MonoLimitation("Argument securityContextSource is ignored")]
		public static Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore, SecurityContextSource securityContextSource)
		{
			return AppDomain.CurrentDomain.Load(rawAssembly, rawSymbolStore);
		}

		// Token: 0x06004B87 RID: 19335 RVA: 0x000F1775 File Offset: 0x000EF975
		public static Assembly ReflectionOnlyLoad(byte[] rawAssembly)
		{
			return AppDomain.CurrentDomain.Load(rawAssembly, null, null, true);
		}

		// Token: 0x06004B88 RID: 19336 RVA: 0x000F1788 File Offset: 0x000EF988
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly ReflectionOnlyLoad(string assemblyString)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return AppDomain.CurrentDomain.Load(assemblyString, null, true, ref stackCrawlMark);
		}

		// Token: 0x06004B89 RID: 19337 RVA: 0x000F17A8 File Offset: 0x000EF9A8
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly ReflectionOnlyLoadFrom(string assemblyFile)
		{
			if (assemblyFile == null)
			{
				throw new ArgumentNullException("assemblyFile");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Assembly.LoadFrom(assemblyFile, true, ref stackCrawlMark);
		}

		// Token: 0x06004B8A RID: 19338 RVA: 0x000F17CE File Offset: 0x000EF9CE
		[Obsolete("This method has been deprecated. Please use Assembly.Load() instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		public static Assembly LoadWithPartialName(string partialName)
		{
			return Assembly.LoadWithPartialName(partialName, null);
		}

		// Token: 0x06004B8B RID: 19339 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO("Not implemented")]
		public Module LoadModule(string moduleName, byte[] rawModule)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004B8C RID: 19340 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO("Not implemented")]
		public virtual Module LoadModule(string moduleName, byte[] rawModule, byte[] rawSymbolStore)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004B8D RID: 19341
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Assembly load_with_partial_name(string name, Evidence e);

		// Token: 0x06004B8E RID: 19342 RVA: 0x000F17D7 File Offset: 0x000EF9D7
		[Obsolete("This method has been deprecated. Please use Assembly.Load() instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		public static Assembly LoadWithPartialName(string partialName, Evidence securityEvidence)
		{
			return Assembly.LoadWithPartialName(partialName, securityEvidence, true);
		}

		// Token: 0x06004B8F RID: 19343 RVA: 0x000F17E1 File Offset: 0x000EF9E1
		internal static Assembly LoadWithPartialName(string partialName, Evidence securityEvidence, bool oldBehavior)
		{
			if (!oldBehavior)
			{
				throw new NotImplementedException();
			}
			if (partialName == null)
			{
				throw new NullReferenceException();
			}
			return Assembly.load_with_partial_name(partialName, securityEvidence);
		}

		// Token: 0x06004B90 RID: 19344 RVA: 0x000F17FC File Offset: 0x000EF9FC
		public object CreateInstance(string typeName)
		{
			return this.CreateInstance(typeName, false);
		}

		// Token: 0x06004B91 RID: 19345 RVA: 0x000F1808 File Offset: 0x000EFA08
		public object CreateInstance(string typeName, bool ignoreCase)
		{
			Type type = this.GetType(typeName, false, ignoreCase);
			if (type == null)
			{
				return null;
			}
			object obj;
			try
			{
				obj = Activator.CreateInstance(type);
			}
			catch (InvalidOperationException)
			{
				throw new ArgumentException("It is illegal to invoke a method on a Type loaded via ReflectionOnly methods.");
			}
			return obj;
		}

		// Token: 0x06004B92 RID: 19346 RVA: 0x000F1854 File Offset: 0x000EFA54
		public virtual object CreateInstance(string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
		{
			Type type = this.GetType(typeName, false, ignoreCase);
			if (type == null)
			{
				return null;
			}
			object obj;
			try
			{
				obj = Activator.CreateInstance(type, bindingAttr, binder, args, culture, activationAttributes);
			}
			catch (InvalidOperationException)
			{
				throw new ArgumentException("It is illegal to invoke a method on a Type loaded via ReflectionOnly methods.");
			}
			return obj;
		}

		// Token: 0x06004B93 RID: 19347 RVA: 0x000F18A8 File Offset: 0x000EFAA8
		public Module[] GetLoadedModules()
		{
			return this.GetLoadedModules(false);
		}

		// Token: 0x06004B94 RID: 19348 RVA: 0x000F18B1 File Offset: 0x000EFAB1
		public Module[] GetModules()
		{
			return this.GetModules(false);
		}

		// Token: 0x06004B95 RID: 19349 RVA: 0x000174FB File Offset: 0x000156FB
		internal virtual Module[] GetModulesInternal()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004B96 RID: 19350
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Assembly GetExecutingAssembly();

		// Token: 0x06004B97 RID: 19351
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Assembly GetCallingAssembly();

		// Token: 0x06004B98 RID: 19352
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr InternalGetReferencedAssemblies(Assembly module);

		// Token: 0x06004B99 RID: 19353 RVA: 0x000174FB File Offset: 0x000156FB
		public virtual string[] GetManifestResourceNames()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004B9A RID: 19354 RVA: 0x000F18BC File Offset: 0x000EFABC
		internal unsafe static AssemblyName[] GetReferencedAssemblies(Assembly module)
		{
			AssemblyName[] array2;
			using (SafeGPtrArrayHandle safeGPtrArrayHandle = new SafeGPtrArrayHandle(Assembly.InternalGetReferencedAssemblies(module)))
			{
				int length = safeGPtrArrayHandle.Length;
				try
				{
					AssemblyName[] array = new AssemblyName[length];
					for (int i = 0; i < length; i++)
					{
						AssemblyName assemblyName = new AssemblyName();
						MonoAssemblyName* ptr = (MonoAssemblyName*)(void*)safeGPtrArrayHandle[i];
						assemblyName.FillName(ptr, null, true, false, true, true);
						array[i] = assemblyName;
					}
					array2 = array;
				}
				finally
				{
					for (int j = 0; j < length; j++)
					{
						MonoAssemblyName* ptr2 = (MonoAssemblyName*)(void*)safeGPtrArrayHandle[j];
						RuntimeMarshal.FreeAssemblyName(ref *ptr2, true);
					}
				}
			}
			return array2;
		}

		// Token: 0x06004B9B RID: 19355 RVA: 0x000174FB File Offset: 0x000156FB
		public virtual ManifestResourceInfo GetManifestResourceInfo(string resourceName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000C32 RID: 3122
		// (get) Token: 0x06004B9C RID: 19356 RVA: 0x0000408D File Offset: 0x0000228D
		[MonoTODO("Currently it always returns zero")]
		[ComVisible(false)]
		public virtual long HostContext
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x06004B9D RID: 19357 RVA: 0x000174FB File Offset: 0x000156FB
		internal virtual Module GetManifestModule()
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x06004B9E RID: 19358 RVA: 0x000174FB File Offset: 0x000156FB
		[ComVisible(false)]
		public virtual bool ReflectionOnly
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06004B9F RID: 19359 RVA: 0x00093238 File Offset: 0x00091438
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06004BA0 RID: 19360 RVA: 0x00097F7A File Offset: 0x0009617A
		public override bool Equals(object o)
		{
			return base.Equals(o);
		}

		// Token: 0x17000C34 RID: 3124
		// (get) Token: 0x06004BA1 RID: 19361 RVA: 0x000174FB File Offset: 0x000156FB
		internal virtual PermissionSet GrantedPermissionSet
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x06004BA2 RID: 19362 RVA: 0x000174FB File Offset: 0x000156FB
		internal virtual PermissionSet DeniedPermissionSet
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x06004BA3 RID: 19363 RVA: 0x000174FB File Offset: 0x000156FB
		public virtual PermissionSet PermissionSet
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x06004BA4 RID: 19364 RVA: 0x000F1974 File Offset: 0x000EFB74
		public virtual SecurityRuleSet SecurityRuleSet
		{
			get
			{
				throw Assembly.CreateNIE();
			}
		}

		// Token: 0x06004BA5 RID: 19365 RVA: 0x000F197B File Offset: 0x000EFB7B
		private static Exception CreateNIE()
		{
			return new NotImplementedException("Derived classes must implement it");
		}

		// Token: 0x06004BA6 RID: 19366 RVA: 0x000174FB File Offset: 0x000156FB
		public virtual IList<CustomAttributeData> GetCustomAttributesData()
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x06004BA7 RID: 19367 RVA: 0x00003FB7 File Offset: 0x000021B7
		[MonoTODO]
		public bool IsFullyTrusted
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004BA8 RID: 19368 RVA: 0x000F1974 File Offset: 0x000EFB74
		public virtual Type GetType(string name, bool throwOnError, bool ignoreCase)
		{
			throw Assembly.CreateNIE();
		}

		// Token: 0x06004BA9 RID: 19369 RVA: 0x000F1974 File Offset: 0x000EFB74
		public virtual Module GetModule(string name)
		{
			throw Assembly.CreateNIE();
		}

		// Token: 0x06004BAA RID: 19370 RVA: 0x000F1974 File Offset: 0x000EFB74
		public virtual AssemblyName[] GetReferencedAssemblies()
		{
			throw Assembly.CreateNIE();
		}

		// Token: 0x06004BAB RID: 19371 RVA: 0x000F1974 File Offset: 0x000EFB74
		public virtual Module[] GetModules(bool getResourceModules)
		{
			throw Assembly.CreateNIE();
		}

		// Token: 0x06004BAC RID: 19372 RVA: 0x000F1974 File Offset: 0x000EFB74
		[MonoTODO("Always returns the same as GetModules")]
		public virtual Module[] GetLoadedModules(bool getResourceModules)
		{
			throw Assembly.CreateNIE();
		}

		// Token: 0x06004BAD RID: 19373 RVA: 0x000F1974 File Offset: 0x000EFB74
		public virtual Assembly GetSatelliteAssembly(CultureInfo culture)
		{
			throw Assembly.CreateNIE();
		}

		// Token: 0x06004BAE RID: 19374 RVA: 0x000F1974 File Offset: 0x000EFB74
		public virtual Assembly GetSatelliteAssembly(CultureInfo culture, Version version)
		{
			throw Assembly.CreateNIE();
		}

		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x06004BAF RID: 19375 RVA: 0x000F1974 File Offset: 0x000EFB74
		public virtual Module ManifestModule
		{
			get
			{
				throw Assembly.CreateNIE();
			}
		}

		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x06004BB0 RID: 19376 RVA: 0x000F1974 File Offset: 0x000EFB74
		public virtual bool GlobalAssemblyCache
		{
			get
			{
				throw Assembly.CreateNIE();
			}
		}

		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x06004BB1 RID: 19377 RVA: 0x0000408A File Offset: 0x0000228A
		public virtual bool IsDynamic
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004BB2 RID: 19378 RVA: 0x000F1987 File Offset: 0x000EFB87
		public static bool operator ==(Assembly left, Assembly right)
		{
			return left == right || (!((left == null) ^ (right == null)) && left.Equals(right));
		}

		// Token: 0x06004BB3 RID: 19379 RVA: 0x000F19A3 File Offset: 0x000EFBA3
		public static bool operator !=(Assembly left, Assembly right)
		{
			return left != right && (((left == null) ^ (right == null)) || !left.Equals(right));
		}

		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x06004BB4 RID: 19380 RVA: 0x000F19C2 File Offset: 0x000EFBC2
		public virtual IEnumerable<TypeInfo> DefinedTypes
		{
			get
			{
				foreach (Type type in this.GetTypes())
				{
					yield return type.GetTypeInfo();
				}
				Type[] array = null;
				yield break;
			}
		}

		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x06004BB5 RID: 19381 RVA: 0x000F19D2 File Offset: 0x000EFBD2
		public virtual IEnumerable<Type> ExportedTypes
		{
			get
			{
				return this.GetExportedTypes();
			}
		}

		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x06004BB6 RID: 19382 RVA: 0x000F19DA File Offset: 0x000EFBDA
		public virtual IEnumerable<Module> Modules
		{
			get
			{
				return this.GetModules();
			}
		}

		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x06004BB7 RID: 19383 RVA: 0x000F19E2 File Offset: 0x000EFBE2
		public virtual IEnumerable<CustomAttributeData> CustomAttributes
		{
			get
			{
				return this.GetCustomAttributesData();
			}
		}

		// Token: 0x06004BB8 RID: 19384 RVA: 0x0003CB93 File Offset: 0x0003AD93
		public virtual Type[] GetForwardedTypes()
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06004BB9 RID: 19385 RVA: 0x000025BE File Offset: 0x000007BE
		protected Assembly()
		{
		}

		// Token: 0x020008B9 RID: 2233
		internal class ResolveEventHolder
		{
			// Token: 0x1400001D RID: 29
			// (add) Token: 0x06004BBA RID: 19386 RVA: 0x000F19EC File Offset: 0x000EFBEC
			// (remove) Token: 0x06004BBB RID: 19387 RVA: 0x000F1A24 File Offset: 0x000EFC24
			public event ModuleResolveEventHandler ModuleResolve
			{
				[CompilerGenerated]
				add
				{
					ModuleResolveEventHandler moduleResolveEventHandler = this.ModuleResolve;
					ModuleResolveEventHandler moduleResolveEventHandler2;
					do
					{
						moduleResolveEventHandler2 = moduleResolveEventHandler;
						ModuleResolveEventHandler moduleResolveEventHandler3 = (ModuleResolveEventHandler)Delegate.Combine(moduleResolveEventHandler2, value);
						moduleResolveEventHandler = Interlocked.CompareExchange<ModuleResolveEventHandler>(ref this.ModuleResolve, moduleResolveEventHandler3, moduleResolveEventHandler2);
					}
					while (moduleResolveEventHandler != moduleResolveEventHandler2);
				}
				[CompilerGenerated]
				remove
				{
					ModuleResolveEventHandler moduleResolveEventHandler = this.ModuleResolve;
					ModuleResolveEventHandler moduleResolveEventHandler2;
					do
					{
						moduleResolveEventHandler2 = moduleResolveEventHandler;
						ModuleResolveEventHandler moduleResolveEventHandler3 = (ModuleResolveEventHandler)Delegate.Remove(moduleResolveEventHandler2, value);
						moduleResolveEventHandler = Interlocked.CompareExchange<ModuleResolveEventHandler>(ref this.ModuleResolve, moduleResolveEventHandler3, moduleResolveEventHandler2);
					}
					while (moduleResolveEventHandler != moduleResolveEventHandler2);
				}
			}

			// Token: 0x06004BBC RID: 19388 RVA: 0x000025BE File Offset: 0x000007BE
			public ResolveEventHolder()
			{
			}

			// Token: 0x04002F8C RID: 12172
			[CompilerGenerated]
			private ModuleResolveEventHandler ModuleResolve;
		}

		// Token: 0x020008BA RID: 2234
		[CompilerGenerated]
		private sealed class <get_DefinedTypes>d__127 : IEnumerable<TypeInfo>, IEnumerable, IEnumerator<TypeInfo>, IDisposable, IEnumerator
		{
			// Token: 0x06004BBD RID: 19389 RVA: 0x000F1A59 File Offset: 0x000EFC59
			[DebuggerHidden]
			public <get_DefinedTypes>d__127(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06004BBE RID: 19390 RVA: 0x00004088 File Offset: 0x00002288
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06004BBF RID: 19391 RVA: 0x000F1A74 File Offset: 0x000EFC74
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				Assembly assembly = this;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					i++;
				}
				else
				{
					this.<>1__state = -1;
					array = assembly.GetTypes();
					i = 0;
				}
				if (i >= array.Length)
				{
					array = null;
					return false;
				}
				Type type = array[i];
				this.<>2__current = type.GetTypeInfo();
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x17000C40 RID: 3136
			// (get) Token: 0x06004BC0 RID: 19392 RVA: 0x000F1B04 File Offset: 0x000EFD04
			TypeInfo IEnumerator<TypeInfo>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06004BC1 RID: 19393 RVA: 0x00047E00 File Offset: 0x00046000
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000C41 RID: 3137
			// (get) Token: 0x06004BC2 RID: 19394 RVA: 0x000F1B04 File Offset: 0x000EFD04
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06004BC3 RID: 19395 RVA: 0x000F1B0C File Offset: 0x000EFD0C
			[DebuggerHidden]
			IEnumerator<TypeInfo> IEnumerable<TypeInfo>.GetEnumerator()
			{
				Assembly.<get_DefinedTypes>d__127 <get_DefinedTypes>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<get_DefinedTypes>d__ = this;
				}
				else
				{
					<get_DefinedTypes>d__ = new Assembly.<get_DefinedTypes>d__127(0);
					<get_DefinedTypes>d__.<>4__this = this;
				}
				return <get_DefinedTypes>d__;
			}

			// Token: 0x06004BC4 RID: 19396 RVA: 0x000F1B4F File Offset: 0x000EFD4F
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Reflection.TypeInfo>.GetEnumerator();
			}

			// Token: 0x04002F8D RID: 12173
			private int <>1__state;

			// Token: 0x04002F8E RID: 12174
			private TypeInfo <>2__current;

			// Token: 0x04002F8F RID: 12175
			private int <>l__initialThreadId;

			// Token: 0x04002F90 RID: 12176
			public Assembly <>4__this;

			// Token: 0x04002F91 RID: 12177
			private Type[] <>7__wrap1;

			// Token: 0x04002F92 RID: 12178
			private int <>7__wrap2;
		}
	}
}
