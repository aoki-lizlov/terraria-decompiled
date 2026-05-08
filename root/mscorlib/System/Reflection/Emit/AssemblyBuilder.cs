using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Threading;
using Mono.Security;

namespace System.Reflection.Emit
{
	// Token: 0x020008E3 RID: 2275
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_AssemblyBuilder))]
	[ClassInterface(ClassInterfaceType.None)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class AssemblyBuilder : Assembly, _AssemblyBuilder
	{
		// Token: 0x06004E01 RID: 19969 RVA: 0x000174FB File Offset: 0x000156FB
		void _AssemblyBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004E02 RID: 19970 RVA: 0x000174FB File Offset: 0x000156FB
		void _AssemblyBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004E03 RID: 19971 RVA: 0x000174FB File Offset: 0x000156FB
		void _AssemblyBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004E04 RID: 19972 RVA: 0x000174FB File Offset: 0x000156FB
		void _AssemblyBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004E05 RID: 19973
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void basic_init(AssemblyBuilder ab);

		// Token: 0x06004E06 RID: 19974
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UpdateNativeCustomAttributes(AssemblyBuilder ab);

		// Token: 0x06004E07 RID: 19975 RVA: 0x000F6270 File Offset: 0x000F4470
		[PreserveDependency("RuntimeResolve", "System.Reflection.Emit.ModuleBuilder")]
		internal AssemblyBuilder(AssemblyName n, string directory, AssemblyBuilderAccess access, bool corlib_internal)
		{
			if ((access & (AssemblyBuilderAccess)2048) != (AssemblyBuilderAccess)0)
			{
				throw new NotImplementedException("COMPILER_ACCESS is no longer supperted, use a newer mcs.");
			}
			if (!Enum.IsDefined(typeof(AssemblyBuilderAccess), access))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Argument value {0} is not valid.", (int)access), "access");
			}
			this.name = n.Name;
			this.access = (uint)access;
			this.flags = (uint)n.Flags;
			if (this.IsSave && (directory == null || directory.Length == 0))
			{
				this.dir = Directory.GetCurrentDirectory();
			}
			else
			{
				this.dir = directory;
			}
			if (n.CultureInfo != null)
			{
				this.culture = n.CultureInfo.Name;
				this.versioninfo_culture = n.CultureInfo.Name;
			}
			Version version = n.Version;
			if (version != null)
			{
				this.version = version.ToString();
			}
			if (n.KeyPair != null)
			{
				this.sn = n.KeyPair.StrongName();
			}
			else
			{
				byte[] publicKey = n.GetPublicKey();
				if (publicKey != null && publicKey.Length != 0)
				{
					this.sn = new Mono.Security.StrongName(publicKey);
				}
			}
			if (this.sn != null)
			{
				this.flags |= 1U;
			}
			this.corlib_internal = corlib_internal;
			if (this.sn != null)
			{
				this.pktoken = new byte[this.sn.PublicKeyToken.Length * 2];
				int num = 0;
				foreach (byte b in this.sn.PublicKeyToken)
				{
					string text = b.ToString("x2");
					this.pktoken[num++] = (byte)text[0];
					this.pktoken[num++] = (byte)text[1];
				}
			}
			AssemblyBuilder.basic_init(this);
		}

		// Token: 0x17000CD3 RID: 3283
		// (get) Token: 0x06004E08 RID: 19976 RVA: 0x000F647A File Offset: 0x000F467A
		public override string CodeBase
		{
			get
			{
				throw this.not_supported();
			}
		}

		// Token: 0x17000CD4 RID: 3284
		// (get) Token: 0x06004E09 RID: 19977 RVA: 0x000F2E98 File Offset: 0x000F1098
		public override string EscapedCodeBase
		{
			get
			{
				return RuntimeAssembly.GetCodeBase(this, true);
			}
		}

		// Token: 0x17000CD5 RID: 3285
		// (get) Token: 0x06004E0A RID: 19978 RVA: 0x000F6482 File Offset: 0x000F4682
		public override MethodInfo EntryPoint
		{
			get
			{
				return this.entry_point;
			}
		}

		// Token: 0x17000CD6 RID: 3286
		// (get) Token: 0x06004E0B RID: 19979 RVA: 0x000F647A File Offset: 0x000F467A
		public override string Location
		{
			get
			{
				throw this.not_supported();
			}
		}

		// Token: 0x17000CD7 RID: 3287
		// (get) Token: 0x06004E0C RID: 19980 RVA: 0x000F2EA9 File Offset: 0x000F10A9
		public override string ImageRuntimeVersion
		{
			get
			{
				return RuntimeAssembly.InternalImageRuntimeVersion(this);
			}
		}

		// Token: 0x17000CD8 RID: 3288
		// (get) Token: 0x06004E0D RID: 19981 RVA: 0x000F648A File Offset: 0x000F468A
		public override bool ReflectionOnly
		{
			get
			{
				return this.access == 6U;
			}
		}

		// Token: 0x06004E0E RID: 19982 RVA: 0x000F6495 File Offset: 0x000F4695
		public void AddResourceFile(string name, string fileName)
		{
			this.AddResourceFile(name, fileName, ResourceAttributes.Public);
		}

		// Token: 0x06004E0F RID: 19983 RVA: 0x000F64A0 File Offset: 0x000F46A0
		public void AddResourceFile(string name, string fileName, ResourceAttributes attribute)
		{
			this.AddResourceFile(name, fileName, attribute, true);
		}

		// Token: 0x06004E10 RID: 19984 RVA: 0x000F64AC File Offset: 0x000F46AC
		private void AddResourceFile(string name, string fileName, ResourceAttributes attribute, bool fileNeedsToExists)
		{
			this.check_name_and_filename(name, fileName, fileNeedsToExists);
			if (this.dir != null)
			{
				fileName = Path.Combine(this.dir, fileName);
			}
			if (this.resources != null)
			{
				MonoResource[] array = new MonoResource[this.resources.Length + 1];
				Array.Copy(this.resources, array, this.resources.Length);
				this.resources = array;
			}
			else
			{
				this.resources = new MonoResource[1];
			}
			int num = this.resources.Length - 1;
			this.resources[num].name = name;
			this.resources[num].filename = fileName;
			this.resources[num].attrs = attribute;
		}

		// Token: 0x06004E11 RID: 19985 RVA: 0x000F655C File Offset: 0x000F475C
		internal void AddPermissionRequests(PermissionSet required, PermissionSet optional, PermissionSet refused)
		{
			if (this.created)
			{
				throw new InvalidOperationException("Assembly was already saved.");
			}
			this._minimum = required;
			this._optional = optional;
			this._refuse = refused;
			if (required != null)
			{
				this.permissions_minimum = new RefEmitPermissionSet[1];
				this.permissions_minimum[0] = new RefEmitPermissionSet(SecurityAction.RequestMinimum, required.ToXml().ToString());
			}
			if (optional != null)
			{
				this.permissions_optional = new RefEmitPermissionSet[1];
				this.permissions_optional[0] = new RefEmitPermissionSet(SecurityAction.RequestOptional, optional.ToXml().ToString());
			}
			if (refused != null)
			{
				this.permissions_refused = new RefEmitPermissionSet[1];
				this.permissions_refused[0] = new RefEmitPermissionSet(SecurityAction.RequestRefuse, refused.ToXml().ToString());
			}
		}

		// Token: 0x06004E12 RID: 19986 RVA: 0x000F6617 File Offset: 0x000F4817
		internal void EmbedResourceFile(string name, string fileName)
		{
			this.EmbedResourceFile(name, fileName, ResourceAttributes.Public);
		}

		// Token: 0x06004E13 RID: 19987 RVA: 0x000F6624 File Offset: 0x000F4824
		private void EmbedResourceFile(string name, string fileName, ResourceAttributes attribute)
		{
			if (this.resources != null)
			{
				MonoResource[] array = new MonoResource[this.resources.Length + 1];
				Array.Copy(this.resources, array, this.resources.Length);
				this.resources = array;
			}
			else
			{
				this.resources = new MonoResource[1];
			}
			int num = this.resources.Length - 1;
			this.resources[num].name = name;
			this.resources[num].attrs = attribute;
			try
			{
				FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
				long length = fileStream.Length;
				this.resources[num].data = new byte[length];
				fileStream.Read(this.resources[num].data, 0, (int)length);
				fileStream.Close();
			}
			catch
			{
			}
		}

		// Token: 0x06004E14 RID: 19988 RVA: 0x000F6700 File Offset: 0x000F4900
		public static AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return new AssemblyBuilder(name, null, access, false);
		}

		// Token: 0x06004E15 RID: 19989 RVA: 0x000F671C File Offset: 0x000F491C
		public static AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, IEnumerable<CustomAttributeBuilder> assemblyAttributes)
		{
			AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(name, access);
			foreach (CustomAttributeBuilder customAttributeBuilder in assemblyAttributes)
			{
				assemblyBuilder.SetCustomAttribute(customAttributeBuilder);
			}
			return assemblyBuilder;
		}

		// Token: 0x06004E16 RID: 19990 RVA: 0x000F6770 File Offset: 0x000F4970
		public ModuleBuilder DefineDynamicModule(string name)
		{
			return this.DefineDynamicModule(name, name, false, true);
		}

		// Token: 0x06004E17 RID: 19991 RVA: 0x000F677C File Offset: 0x000F497C
		public ModuleBuilder DefineDynamicModule(string name, bool emitSymbolInfo)
		{
			return this.DefineDynamicModule(name, name, emitSymbolInfo, true);
		}

		// Token: 0x06004E18 RID: 19992 RVA: 0x000F6788 File Offset: 0x000F4988
		public ModuleBuilder DefineDynamicModule(string name, string fileName)
		{
			return this.DefineDynamicModule(name, fileName, false, false);
		}

		// Token: 0x06004E19 RID: 19993 RVA: 0x000F6794 File Offset: 0x000F4994
		public ModuleBuilder DefineDynamicModule(string name, string fileName, bool emitSymbolInfo)
		{
			return this.DefineDynamicModule(name, fileName, emitSymbolInfo, false);
		}

		// Token: 0x06004E1A RID: 19994 RVA: 0x000F67A0 File Offset: 0x000F49A0
		private ModuleBuilder DefineDynamicModule(string name, string fileName, bool emitSymbolInfo, bool transient)
		{
			this.check_name_and_filename(name, fileName, false);
			if (!transient)
			{
				if (Path.GetExtension(fileName) == string.Empty)
				{
					throw new ArgumentException("Module file name '" + fileName + "' must have file extension.");
				}
				if (!this.IsSave)
				{
					throw new NotSupportedException("Persistable modules are not supported in a dynamic assembly created with AssemblyBuilderAccess.Run");
				}
				if (this.created)
				{
					throw new InvalidOperationException("Assembly was already saved.");
				}
			}
			ModuleBuilder moduleBuilder = new ModuleBuilder(this, name, fileName, emitSymbolInfo, transient);
			if (this.modules != null && this.is_module_only)
			{
				throw new InvalidOperationException("A module-only assembly can only contain one module.");
			}
			if (this.modules != null)
			{
				ModuleBuilder[] array = new ModuleBuilder[this.modules.Length + 1];
				Array.Copy(this.modules, array, this.modules.Length);
				this.modules = array;
			}
			else
			{
				this.modules = new ModuleBuilder[1];
			}
			this.modules[this.modules.Length - 1] = moduleBuilder;
			return moduleBuilder;
		}

		// Token: 0x06004E1B RID: 19995 RVA: 0x000F6883 File Offset: 0x000F4A83
		public IResourceWriter DefineResource(string name, string description, string fileName)
		{
			return this.DefineResource(name, description, fileName, ResourceAttributes.Public);
		}

		// Token: 0x06004E1C RID: 19996 RVA: 0x000F6890 File Offset: 0x000F4A90
		public IResourceWriter DefineResource(string name, string description, string fileName, ResourceAttributes attribute)
		{
			this.AddResourceFile(name, fileName, attribute, false);
			IResourceWriter resourceWriter = new ResourceWriter(fileName);
			if (this.resource_writers == null)
			{
				this.resource_writers = new ArrayList();
			}
			this.resource_writers.Add(resourceWriter);
			return resourceWriter;
		}

		// Token: 0x06004E1D RID: 19997 RVA: 0x000F68D0 File Offset: 0x000F4AD0
		private void AddUnmanagedResource(Win32Resource res)
		{
			MemoryStream memoryStream = new MemoryStream();
			res.WriteTo(memoryStream);
			if (this.win32_resources != null)
			{
				MonoWin32Resource[] array = new MonoWin32Resource[this.win32_resources.Length + 1];
				Array.Copy(this.win32_resources, array, this.win32_resources.Length);
				this.win32_resources = array;
			}
			else
			{
				this.win32_resources = new MonoWin32Resource[1];
			}
			this.win32_resources[this.win32_resources.Length - 1] = new MonoWin32Resource(res.Type.Id, res.Name.Id, res.Language, memoryStream.ToArray());
		}

		// Token: 0x06004E1E RID: 19998 RVA: 0x000F6967 File Offset: 0x000F4B67
		[MonoTODO("Not currently implemenented")]
		public void DefineUnmanagedResource(byte[] resource)
		{
			if (resource == null)
			{
				throw new ArgumentNullException("resource");
			}
			if (this.native_resource != NativeResourceType.None)
			{
				throw new ArgumentException("Native resource has already been defined.");
			}
			this.native_resource = NativeResourceType.Unmanaged;
			throw new NotImplementedException();
		}

		// Token: 0x06004E1F RID: 19999 RVA: 0x000F6998 File Offset: 0x000F4B98
		public void DefineUnmanagedResource(string resourceFileName)
		{
			if (resourceFileName == null)
			{
				throw new ArgumentNullException("resourceFileName");
			}
			if (resourceFileName.Length == 0)
			{
				throw new ArgumentException("resourceFileName");
			}
			if (!File.Exists(resourceFileName) || Directory.Exists(resourceFileName))
			{
				throw new FileNotFoundException("File '" + resourceFileName + "' does not exist or is a directory.");
			}
			if (this.native_resource != NativeResourceType.None)
			{
				throw new ArgumentException("Native resource has already been defined.");
			}
			this.native_resource = NativeResourceType.Unmanaged;
			using (FileStream fileStream = new FileStream(resourceFileName, FileMode.Open, FileAccess.Read))
			{
				foreach (object obj in new Win32ResFileReader(fileStream).ReadResources())
				{
					Win32EncodedResource win32EncodedResource = (Win32EncodedResource)obj;
					if (win32EncodedResource.Name.IsName || win32EncodedResource.Type.IsName)
					{
						throw new InvalidOperationException("resource files with named resources or non-default resource types are not supported.");
					}
					this.AddUnmanagedResource(win32EncodedResource);
				}
			}
		}

		// Token: 0x06004E20 RID: 20000 RVA: 0x000F6A9C File Offset: 0x000F4C9C
		public void DefineVersionInfoResource()
		{
			if (this.native_resource != NativeResourceType.None)
			{
				throw new ArgumentException("Native resource has already been defined.");
			}
			this.native_resource = NativeResourceType.Assembly;
			this.version_res = new Win32VersionResource(1, 0, false);
		}

		// Token: 0x06004E21 RID: 20001 RVA: 0x000F6AC8 File Offset: 0x000F4CC8
		public void DefineVersionInfoResource(string product, string productVersion, string company, string copyright, string trademark)
		{
			if (this.native_resource != NativeResourceType.None)
			{
				throw new ArgumentException("Native resource has already been defined.");
			}
			this.native_resource = NativeResourceType.Explicit;
			this.version_res = new Win32VersionResource(1, 0, false);
			this.version_res.ProductName = ((product != null) ? product : " ");
			this.version_res.ProductVersion = ((productVersion != null) ? productVersion : " ");
			this.version_res.CompanyName = ((company != null) ? company : " ");
			this.version_res.LegalCopyright = ((copyright != null) ? copyright : " ");
			this.version_res.LegalTrademarks = ((trademark != null) ? trademark : " ");
		}

		// Token: 0x06004E22 RID: 20002 RVA: 0x000F6B70 File Offset: 0x000F4D70
		private void DefineVersionInfoResourceImpl(string fileName)
		{
			if (this.versioninfo_culture != null)
			{
				this.version_res.FileLanguage = new CultureInfo(this.versioninfo_culture).LCID;
			}
			this.version_res.Version = ((this.version == null) ? "0.0.0.0" : this.version);
			if (this.cattrs != null)
			{
				NativeResourceType nativeResourceType = this.native_resource;
				if (nativeResourceType != NativeResourceType.Assembly)
				{
					if (nativeResourceType == NativeResourceType.Explicit)
					{
						foreach (CustomAttributeBuilder customAttributeBuilder in this.cattrs)
						{
							string fullName = customAttributeBuilder.Ctor.ReflectedType.FullName;
							if (fullName == "System.Reflection.AssemblyCultureAttribute")
							{
								this.version_res.FileLanguage = new CultureInfo(customAttributeBuilder.string_arg()).LCID;
							}
							else if (fullName == "System.Reflection.AssemblyDescriptionAttribute")
							{
								this.version_res.Comments = customAttributeBuilder.string_arg();
							}
						}
					}
				}
				else
				{
					foreach (CustomAttributeBuilder customAttributeBuilder2 in this.cattrs)
					{
						string fullName2 = customAttributeBuilder2.Ctor.ReflectedType.FullName;
						if (fullName2 == "System.Reflection.AssemblyProductAttribute")
						{
							this.version_res.ProductName = customAttributeBuilder2.string_arg();
						}
						else if (fullName2 == "System.Reflection.AssemblyCompanyAttribute")
						{
							this.version_res.CompanyName = customAttributeBuilder2.string_arg();
						}
						else if (fullName2 == "System.Reflection.AssemblyCopyrightAttribute")
						{
							this.version_res.LegalCopyright = customAttributeBuilder2.string_arg();
						}
						else if (fullName2 == "System.Reflection.AssemblyTrademarkAttribute")
						{
							this.version_res.LegalTrademarks = customAttributeBuilder2.string_arg();
						}
						else if (fullName2 == "System.Reflection.AssemblyCultureAttribute")
						{
							this.version_res.FileLanguage = new CultureInfo(customAttributeBuilder2.string_arg()).LCID;
						}
						else if (fullName2 == "System.Reflection.AssemblyFileVersionAttribute")
						{
							this.version_res.FileVersion = customAttributeBuilder2.string_arg();
						}
						else if (fullName2 == "System.Reflection.AssemblyInformationalVersionAttribute")
						{
							this.version_res.ProductVersion = customAttributeBuilder2.string_arg();
						}
						else if (fullName2 == "System.Reflection.AssemblyTitleAttribute")
						{
							this.version_res.FileDescription = customAttributeBuilder2.string_arg();
						}
						else if (fullName2 == "System.Reflection.AssemblyDescriptionAttribute")
						{
							this.version_res.Comments = customAttributeBuilder2.string_arg();
						}
					}
				}
			}
			this.version_res.OriginalFilename = fileName;
			this.version_res.InternalName = Path.GetFileNameWithoutExtension(fileName);
			this.AddUnmanagedResource(this.version_res);
		}

		// Token: 0x06004E23 RID: 20003 RVA: 0x000F6DF8 File Offset: 0x000F4FF8
		public ModuleBuilder GetDynamicModule(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("Empty name is not legal.", "name");
			}
			if (this.modules != null)
			{
				for (int i = 0; i < this.modules.Length; i++)
				{
					if (this.modules[i].name == name)
					{
						return this.modules[i];
					}
				}
			}
			return null;
		}

		// Token: 0x06004E24 RID: 20004 RVA: 0x000F647A File Offset: 0x000F467A
		public override Type[] GetExportedTypes()
		{
			throw this.not_supported();
		}

		// Token: 0x06004E25 RID: 20005 RVA: 0x000F647A File Offset: 0x000F467A
		public override FileStream GetFile(string name)
		{
			throw this.not_supported();
		}

		// Token: 0x06004E26 RID: 20006 RVA: 0x000F647A File Offset: 0x000F467A
		public override FileStream[] GetFiles(bool getResourceModules)
		{
			throw this.not_supported();
		}

		// Token: 0x06004E27 RID: 20007 RVA: 0x000F6E65 File Offset: 0x000F5065
		internal override Module[] GetModulesInternal()
		{
			if (this.modules == null)
			{
				return new Module[0];
			}
			return (Module[])this.modules.Clone();
		}

		// Token: 0x06004E28 RID: 20008 RVA: 0x000F6E88 File Offset: 0x000F5088
		internal override Type[] GetTypes(bool exportedOnly)
		{
			Type[] array = null;
			if (this.modules != null)
			{
				for (int i = 0; i < this.modules.Length; i++)
				{
					Type[] types = this.modules[i].GetTypes();
					if (array == null)
					{
						array = types;
					}
					else
					{
						Type[] array2 = new Type[array.Length + types.Length];
						Array.Copy(array, 0, array2, 0, array.Length);
						Array.Copy(types, 0, array2, array.Length, types.Length);
					}
				}
			}
			if (this.loaded_modules != null)
			{
				for (int j = 0; j < this.loaded_modules.Length; j++)
				{
					Type[] types2 = this.loaded_modules[j].GetTypes();
					if (array == null)
					{
						array = types2;
					}
					else
					{
						Type[] array3 = new Type[array.Length + types2.Length];
						Array.Copy(array, 0, array3, 0, array.Length);
						Array.Copy(types2, 0, array3, array.Length, types2.Length);
					}
				}
			}
			if (array != null)
			{
				List<Exception> list = null;
				foreach (Type type in array)
				{
					if (type is TypeBuilder)
					{
						if (list == null)
						{
							list = new List<Exception>();
						}
						list.Add(new TypeLoadException(string.Format("Type '{0}' is not finished", type.FullName)));
					}
				}
				if (list != null)
				{
					throw new ReflectionTypeLoadException(new Type[list.Count], list.ToArray());
				}
			}
			if (array != null)
			{
				return array;
			}
			return Type.EmptyTypes;
		}

		// Token: 0x06004E29 RID: 20009 RVA: 0x000F647A File Offset: 0x000F467A
		public override ManifestResourceInfo GetManifestResourceInfo(string resourceName)
		{
			throw this.not_supported();
		}

		// Token: 0x06004E2A RID: 20010 RVA: 0x000F647A File Offset: 0x000F467A
		public override string[] GetManifestResourceNames()
		{
			throw this.not_supported();
		}

		// Token: 0x06004E2B RID: 20011 RVA: 0x000F647A File Offset: 0x000F467A
		public override Stream GetManifestResourceStream(string name)
		{
			throw this.not_supported();
		}

		// Token: 0x06004E2C RID: 20012 RVA: 0x000F647A File Offset: 0x000F467A
		public override Stream GetManifestResourceStream(Type type, string name)
		{
			throw this.not_supported();
		}

		// Token: 0x17000CD9 RID: 3289
		// (get) Token: 0x06004E2D RID: 20013 RVA: 0x000F6FCD File Offset: 0x000F51CD
		internal bool IsSave
		{
			get
			{
				return this.access != 1U;
			}
		}

		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x06004E2E RID: 20014 RVA: 0x000F6FDB File Offset: 0x000F51DB
		internal bool IsRun
		{
			get
			{
				return this.access == 1U || this.access == 3U || this.access == 9U;
			}
		}

		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x06004E2F RID: 20015 RVA: 0x000F6FFB File Offset: 0x000F51FB
		internal string AssemblyDir
		{
			get
			{
				return this.dir;
			}
		}

		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x06004E30 RID: 20016 RVA: 0x000F7003 File Offset: 0x000F5203
		// (set) Token: 0x06004E31 RID: 20017 RVA: 0x000F700B File Offset: 0x000F520B
		internal bool IsModuleOnly
		{
			get
			{
				return this.is_module_only;
			}
			set
			{
				this.is_module_only = value;
			}
		}

		// Token: 0x06004E32 RID: 20018 RVA: 0x000F7014 File Offset: 0x000F5214
		internal override Module GetManifestModule()
		{
			if (this.manifest_module == null)
			{
				this.manifest_module = this.DefineDynamicModule("Default Dynamic Module");
			}
			return this.manifest_module;
		}

		// Token: 0x06004E33 RID: 20019 RVA: 0x000F703C File Offset: 0x000F523C
		[MonoLimitation("No support for PE32+ assemblies for AMD64 and IA64")]
		public void Save(string assemblyFileName, PortableExecutableKinds portableExecutableKind, ImageFileMachine imageFileMachine)
		{
			this.peKind = portableExecutableKind;
			this.machine = imageFileMachine;
			if ((this.peKind & PortableExecutableKinds.PE32Plus) != PortableExecutableKinds.NotAPortableExecutableImage || (this.peKind & PortableExecutableKinds.Unmanaged32Bit) != PortableExecutableKinds.NotAPortableExecutableImage)
			{
				throw new NotImplementedException(this.peKind.ToString());
			}
			if (this.machine == ImageFileMachine.IA64 || this.machine == ImageFileMachine.AMD64)
			{
				throw new NotImplementedException(this.machine.ToString());
			}
			if (this.resource_writers != null)
			{
				foreach (object obj in this.resource_writers)
				{
					IResourceWriter resourceWriter = (IResourceWriter)obj;
					resourceWriter.Generate();
					resourceWriter.Close();
				}
			}
			ModuleBuilder moduleBuilder = null;
			if (this.modules != null)
			{
				foreach (ModuleBuilder moduleBuilder2 in this.modules)
				{
					if (moduleBuilder2.FileName == assemblyFileName)
					{
						moduleBuilder = moduleBuilder2;
					}
				}
			}
			if (moduleBuilder == null)
			{
				moduleBuilder = this.DefineDynamicModule("RefEmit_OnDiskManifestModule", assemblyFileName);
			}
			if (!this.is_module_only)
			{
				moduleBuilder.IsMain = true;
			}
			if (this.entry_point != null && this.entry_point.DeclaringType.Module != moduleBuilder)
			{
				Type[] array2;
				if (this.entry_point.GetParametersCount() == 1)
				{
					array2 = new Type[] { typeof(string) };
				}
				else
				{
					array2 = Type.EmptyTypes;
				}
				MethodBuilder methodBuilder = moduleBuilder.DefineGlobalMethod("__EntryPoint$", MethodAttributes.Static, this.entry_point.ReturnType, array2);
				ILGenerator ilgenerator = methodBuilder.GetILGenerator();
				if (array2.Length == 1)
				{
					ilgenerator.Emit(OpCodes.Ldarg_0);
				}
				ilgenerator.Emit(OpCodes.Tailcall);
				ilgenerator.Emit(OpCodes.Call, this.entry_point);
				ilgenerator.Emit(OpCodes.Ret);
				this.entry_point = methodBuilder;
			}
			if (this.version_res != null)
			{
				this.DefineVersionInfoResourceImpl(assemblyFileName);
			}
			if (this.sn != null)
			{
				this.public_key = this.sn.PublicKey;
			}
			foreach (ModuleBuilder moduleBuilder3 in this.modules)
			{
				if (moduleBuilder3 != moduleBuilder)
				{
					moduleBuilder3.Save();
				}
			}
			moduleBuilder.Save();
			if (this.sn != null && this.sn.CanSign)
			{
				this.sn.Sign(Path.Combine(this.AssemblyDir, assemblyFileName));
			}
			this.created = true;
		}

		// Token: 0x06004E34 RID: 20020 RVA: 0x000F72C0 File Offset: 0x000F54C0
		public void Save(string assemblyFileName)
		{
			this.Save(assemblyFileName, PortableExecutableKinds.ILOnly, ImageFileMachine.I386);
		}

		// Token: 0x06004E35 RID: 20021 RVA: 0x000F72CF File Offset: 0x000F54CF
		public void SetEntryPoint(MethodInfo entryMethod)
		{
			this.SetEntryPoint(entryMethod, PEFileKinds.ConsoleApplication);
		}

		// Token: 0x06004E36 RID: 20022 RVA: 0x000F72DC File Offset: 0x000F54DC
		public void SetEntryPoint(MethodInfo entryMethod, PEFileKinds fileKind)
		{
			if (entryMethod == null)
			{
				throw new ArgumentNullException("entryMethod");
			}
			if (entryMethod.DeclaringType.Assembly != this)
			{
				throw new InvalidOperationException("Entry method is not defined in the same assembly.");
			}
			this.entry_point = entryMethod;
			this.pekind = fileKind;
		}

		// Token: 0x06004E37 RID: 20023 RVA: 0x000F732C File Offset: 0x000F552C
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
			}
			if (this.cattrs != null)
			{
				CustomAttributeBuilder[] array = new CustomAttributeBuilder[this.cattrs.Length + 1];
				this.cattrs.CopyTo(array, 0);
				array[this.cattrs.Length] = customBuilder;
				this.cattrs = array;
			}
			else
			{
				this.cattrs = new CustomAttributeBuilder[1];
				this.cattrs[0] = customBuilder;
			}
			if (customBuilder.Ctor != null && customBuilder.Ctor.DeclaringType == typeof(RuntimeCompatibilityAttribute))
			{
				AssemblyBuilder.UpdateNativeCustomAttributes(this);
			}
		}

		// Token: 0x06004E38 RID: 20024 RVA: 0x000F73C5 File Offset: 0x000F55C5
		[ComVisible(true)]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			if (binaryAttribute == null)
			{
				throw new ArgumentNullException("binaryAttribute");
			}
			this.SetCustomAttribute(new CustomAttributeBuilder(con, binaryAttribute));
		}

		// Token: 0x06004E39 RID: 20025 RVA: 0x000F73F6 File Offset: 0x000F55F6
		private Exception not_supported()
		{
			return new NotSupportedException("The invoked member is not supported in a dynamic module.");
		}

		// Token: 0x06004E3A RID: 20026 RVA: 0x000F7404 File Offset: 0x000F5604
		private void check_name_and_filename(string name, string fileName, bool fileNeedsToExists)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("Empty name is not legal.", "name");
			}
			if (fileName.Length == 0)
			{
				throw new ArgumentException("Empty file name is not legal.", "fileName");
			}
			if (Path.GetFileName(fileName) != fileName)
			{
				throw new ArgumentException("fileName '" + fileName + "' must not include a path.", "fileName");
			}
			string text = fileName;
			if (this.dir != null)
			{
				text = Path.Combine(this.dir, fileName);
			}
			if (fileNeedsToExists && !File.Exists(text))
			{
				throw new FileNotFoundException("Could not find file '" + fileName + "'");
			}
			if (this.resources != null)
			{
				for (int i = 0; i < this.resources.Length; i++)
				{
					if (this.resources[i].filename == text)
					{
						throw new ArgumentException("Duplicate file name '" + fileName + "'");
					}
					if (this.resources[i].name == name)
					{
						throw new ArgumentException("Duplicate name '" + name + "'");
					}
				}
			}
			if (this.modules != null)
			{
				for (int j = 0; j < this.modules.Length; j++)
				{
					if (!this.modules[j].IsTransient() && this.modules[j].FileName == fileName)
					{
						throw new ArgumentException("Duplicate file name '" + fileName + "'");
					}
					if (this.modules[j].Name == name)
					{
						throw new ArgumentException("Duplicate name '" + name + "'");
					}
				}
			}
		}

		// Token: 0x06004E3B RID: 20027 RVA: 0x000F75B8 File Offset: 0x000F57B8
		private string create_assembly_version(string version)
		{
			string[] array = version.Split('.', StringSplitOptions.None);
			int[] array2 = new int[4];
			if (array.Length < 0 || array.Length > 4)
			{
				throw new ArgumentException("The version specified '" + version + "' is invalid");
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == "*")
				{
					DateTime now = DateTime.Now;
					if (i == 2)
					{
						array2[2] = (now - new DateTime(2000, 1, 1)).Days;
						if (array.Length == 3)
						{
							array2[3] = (now.Second + now.Minute * 60 + now.Hour * 3600) / 2;
						}
					}
					else
					{
						if (i != 3)
						{
							throw new ArgumentException("The version specified '" + version + "' is invalid");
						}
						array2[3] = (now.Second + now.Minute * 60 + now.Hour * 3600) / 2;
					}
				}
				else
				{
					try
					{
						array2[i] = int.Parse(array[i]);
					}
					catch (FormatException)
					{
						throw new ArgumentException("The version specified '" + version + "' is invalid");
					}
				}
			}
			return string.Concat(new string[]
			{
				array2[0].ToString(),
				".",
				array2[1].ToString(),
				".",
				array2[2].ToString(),
				".",
				array2[3].ToString()
			});
		}

		// Token: 0x06004E3C RID: 20028 RVA: 0x000F774C File Offset: 0x000F594C
		private string GetCultureString(string str)
		{
			if (!(str == "neutral"))
			{
				return str;
			}
			return string.Empty;
		}

		// Token: 0x06004E3D RID: 20029 RVA: 0x000F7762 File Offset: 0x000F5962
		internal Type MakeGenericType(Type gtd, Type[] typeArguments)
		{
			return new TypeBuilderInstantiation(gtd, typeArguments);
		}

		// Token: 0x06004E3E RID: 20030 RVA: 0x000F776C File Offset: 0x000F596C
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
			Type type = base.InternalGetType(null, name, throwOnError, ignoreCase);
			if (!(type is TypeBuilder))
			{
				return type;
			}
			if (throwOnError)
			{
				throw new TypeLoadException(string.Format("Could not load type '{0}' from assembly '{1}'", name, this.name));
			}
			return null;
		}

		// Token: 0x06004E3F RID: 20031 RVA: 0x000F77CC File Offset: 0x000F59CC
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
			if (this.modules == null)
			{
				return null;
			}
			foreach (ModuleBuilder module in this.modules)
			{
				if (module.ScopeName == name)
				{
					return module;
				}
			}
			return null;
		}

		// Token: 0x06004E40 RID: 20032 RVA: 0x000F7830 File Offset: 0x000F5A30
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

		// Token: 0x06004E41 RID: 20033 RVA: 0x000F7880 File Offset: 0x000F5A80
		public override AssemblyName GetName(bool copiedName)
		{
			AssemblyName assemblyName = AssemblyName.Create(this, false);
			if (this.sn != null)
			{
				assemblyName.SetPublicKey(this.sn.PublicKey);
				assemblyName.SetPublicKeyToken(this.sn.PublicKeyToken);
			}
			return assemblyName;
		}

		// Token: 0x06004E42 RID: 20034 RVA: 0x000F2D75 File Offset: 0x000F0F75
		[MonoTODO("This always returns an empty array")]
		public override AssemblyName[] GetReferencedAssemblies()
		{
			return Assembly.GetReferencedAssemblies(this);
		}

		// Token: 0x06004E43 RID: 20035 RVA: 0x000F2DCE File Offset: 0x000F0FCE
		public override Module[] GetLoadedModules(bool getResourceModules)
		{
			return this.GetModules(getResourceModules);
		}

		// Token: 0x06004E44 RID: 20036 RVA: 0x000F78C0 File Offset: 0x000F5AC0
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override Assembly GetSatelliteAssembly(CultureInfo culture)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return base.GetSatelliteAssembly(culture, null, true, ref stackCrawlMark);
		}

		// Token: 0x06004E45 RID: 20037 RVA: 0x000F78DC File Offset: 0x000F5ADC
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override Assembly GetSatelliteAssembly(CultureInfo culture, Version version)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return base.GetSatelliteAssembly(culture, version, true, ref stackCrawlMark);
		}

		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x06004E46 RID: 20038 RVA: 0x000F2E0E File Offset: 0x000F100E
		public override Module ManifestModule
		{
			get
			{
				return this.GetManifestModule();
			}
		}

		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x06004E47 RID: 20039 RVA: 0x0000408A File Offset: 0x0000228A
		public override bool GlobalAssemblyCache
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x06004E48 RID: 20040 RVA: 0x00003FB7 File Offset: 0x000021B7
		public override bool IsDynamic
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004E49 RID: 20041 RVA: 0x000F78F6 File Offset: 0x000F5AF6
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06004E4A RID: 20042 RVA: 0x000F316A File Offset: 0x000F136A
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06004E4B RID: 20043 RVA: 0x000F78FF File Offset: 0x000F5AFF
		public override string ToString()
		{
			if (this.assemblyName != null)
			{
				return this.assemblyName;
			}
			this.assemblyName = this.FullName;
			return this.assemblyName;
		}

		// Token: 0x06004E4C RID: 20044 RVA: 0x000534DE File Offset: 0x000516DE
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		// Token: 0x06004E4D RID: 20045 RVA: 0x000F133D File Offset: 0x000EF53D
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, inherit);
		}

		// Token: 0x06004E4E RID: 20046 RVA: 0x000F1346 File Offset: 0x000EF546
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
		}

		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x06004E4F RID: 20047 RVA: 0x000F2EA1 File Offset: 0x000F10A1
		public override string FullName
		{
			get
			{
				return RuntimeAssembly.get_fullname(this);
			}
		}

		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x06004E50 RID: 20048 RVA: 0x000F7922 File Offset: 0x000F5B22
		internal override IntPtr MonoAssembly
		{
			get
			{
				return this._mono_assembly;
			}
		}

		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x06004E51 RID: 20049 RVA: 0x000F31C2 File Offset: 0x000F13C2
		public override Evidence Evidence
		{
			[SecurityPermission(SecurityAction.Demand, ControlEvidence = true)]
			get
			{
				return this.UnprotectedGetEvidence();
			}
		}

		// Token: 0x06004E52 RID: 20050 RVA: 0x000F792C File Offset: 0x000F5B2C
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

		// Token: 0x04003064 RID: 12388
		internal IntPtr _mono_assembly;

		// Token: 0x04003065 RID: 12389
		internal Evidence _evidence;

		// Token: 0x04003066 RID: 12390
		private UIntPtr dynamic_assembly;

		// Token: 0x04003067 RID: 12391
		private MethodInfo entry_point;

		// Token: 0x04003068 RID: 12392
		private ModuleBuilder[] modules;

		// Token: 0x04003069 RID: 12393
		private string name;

		// Token: 0x0400306A RID: 12394
		private string dir;

		// Token: 0x0400306B RID: 12395
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x0400306C RID: 12396
		private MonoResource[] resources;

		// Token: 0x0400306D RID: 12397
		private byte[] public_key;

		// Token: 0x0400306E RID: 12398
		private string version;

		// Token: 0x0400306F RID: 12399
		private string culture;

		// Token: 0x04003070 RID: 12400
		private uint algid;

		// Token: 0x04003071 RID: 12401
		private uint flags;

		// Token: 0x04003072 RID: 12402
		private PEFileKinds pekind = PEFileKinds.Dll;

		// Token: 0x04003073 RID: 12403
		private bool delay_sign;

		// Token: 0x04003074 RID: 12404
		private uint access;

		// Token: 0x04003075 RID: 12405
		private Module[] loaded_modules;

		// Token: 0x04003076 RID: 12406
		private MonoWin32Resource[] win32_resources;

		// Token: 0x04003077 RID: 12407
		private RefEmitPermissionSet[] permissions_minimum;

		// Token: 0x04003078 RID: 12408
		private RefEmitPermissionSet[] permissions_optional;

		// Token: 0x04003079 RID: 12409
		private RefEmitPermissionSet[] permissions_refused;

		// Token: 0x0400307A RID: 12410
		private PortableExecutableKinds peKind;

		// Token: 0x0400307B RID: 12411
		private ImageFileMachine machine;

		// Token: 0x0400307C RID: 12412
		private bool corlib_internal;

		// Token: 0x0400307D RID: 12413
		private Type[] type_forwarders;

		// Token: 0x0400307E RID: 12414
		private byte[] pktoken;

		// Token: 0x0400307F RID: 12415
		internal PermissionSet _minimum;

		// Token: 0x04003080 RID: 12416
		internal PermissionSet _optional;

		// Token: 0x04003081 RID: 12417
		internal PermissionSet _refuse;

		// Token: 0x04003082 RID: 12418
		internal PermissionSet _granted;

		// Token: 0x04003083 RID: 12419
		internal PermissionSet _denied;

		// Token: 0x04003084 RID: 12420
		private string assemblyName;

		// Token: 0x04003085 RID: 12421
		internal Type corlib_object_type = typeof(object);

		// Token: 0x04003086 RID: 12422
		internal Type corlib_value_type = typeof(ValueType);

		// Token: 0x04003087 RID: 12423
		internal Type corlib_enum_type = typeof(Enum);

		// Token: 0x04003088 RID: 12424
		internal Type corlib_void_type = typeof(void);

		// Token: 0x04003089 RID: 12425
		private ArrayList resource_writers;

		// Token: 0x0400308A RID: 12426
		private Win32VersionResource version_res;

		// Token: 0x0400308B RID: 12427
		private bool created;

		// Token: 0x0400308C RID: 12428
		private bool is_module_only;

		// Token: 0x0400308D RID: 12429
		private Mono.Security.StrongName sn;

		// Token: 0x0400308E RID: 12430
		private NativeResourceType native_resource;

		// Token: 0x0400308F RID: 12431
		private string versioninfo_culture;

		// Token: 0x04003090 RID: 12432
		private const AssemblyBuilderAccess COMPILER_ACCESS = (AssemblyBuilderAccess)2048;

		// Token: 0x04003091 RID: 12433
		private ModuleBuilder manifest_module;
	}
}
