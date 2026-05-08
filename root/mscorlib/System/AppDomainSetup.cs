using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Hosting;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Policy;
using Mono.Security;

namespace System
{
	// Token: 0x020001FD RID: 509
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class AppDomainSetup : IAppDomainSetup
	{
		// Token: 0x0600186A RID: 6250 RVA: 0x000025BE File Offset: 0x000007BE
		public AppDomainSetup()
		{
		}

		// Token: 0x0600186B RID: 6251 RVA: 0x0005E414 File Offset: 0x0005C614
		internal AppDomainSetup(AppDomainSetup setup)
		{
			this.application_base = setup.application_base;
			this.application_name = setup.application_name;
			this.cache_path = setup.cache_path;
			this.configuration_file = setup.configuration_file;
			this.dynamic_base = setup.dynamic_base;
			this.license_file = setup.license_file;
			this.private_bin_path = setup.private_bin_path;
			this.private_bin_path_probe = setup.private_bin_path_probe;
			this.shadow_copy_directories = setup.shadow_copy_directories;
			this.shadow_copy_files = setup.shadow_copy_files;
			this.publisher_policy = setup.publisher_policy;
			this.path_changed = setup.path_changed;
			this.loader_optimization = setup.loader_optimization;
			this.disallow_binding_redirects = setup.disallow_binding_redirects;
			this.disallow_code_downloads = setup.disallow_code_downloads;
			this._activationArguments = setup._activationArguments;
			this.domain_initializer = setup.domain_initializer;
			this.application_trust = setup.application_trust;
			this.domain_initializer_args = setup.domain_initializer_args;
			this.disallow_appbase_probe = setup.disallow_appbase_probe;
			this.configuration_bytes = setup.configuration_bytes;
			this.manager_assembly = setup.manager_assembly;
			this.manager_type = setup.manager_type;
			this.partial_visible_assemblies = setup.partial_visible_assemblies;
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x0005E547 File Offset: 0x0005C747
		public AppDomainSetup(ActivationArguments activationArguments)
		{
			this._activationArguments = activationArguments;
		}

		// Token: 0x0600186D RID: 6253 RVA: 0x0005E556 File Offset: 0x0005C756
		public AppDomainSetup(ActivationContext activationContext)
		{
			this._activationArguments = new ActivationArguments(activationContext);
		}

		// Token: 0x0600186E RID: 6254 RVA: 0x0005E56C File Offset: 0x0005C76C
		private static string GetAppBase(string appBase)
		{
			if (appBase == null)
			{
				return null;
			}
			if (appBase.StartsWith("file://", StringComparison.OrdinalIgnoreCase))
			{
				appBase = new Uri(appBase).LocalPath;
				if (Path.DirectorySeparatorChar != '/')
				{
					appBase = appBase.Replace('/', Path.DirectorySeparatorChar);
				}
			}
			appBase = Path.GetFullPath(appBase);
			if (Path.DirectorySeparatorChar != '/')
			{
				bool flag = appBase.StartsWith("\\\\?\\", StringComparison.Ordinal);
				if (appBase.IndexOf(':', flag ? 6 : 2) != -1)
				{
					throw new NotSupportedException("The given path's format is not supported.");
				}
			}
			string directoryName = Path.GetDirectoryName(appBase);
			if (directoryName != null && directoryName.LastIndexOfAny(Path.GetInvalidPathChars()) >= 0)
			{
				throw new ArgumentException(string.Format(Locale.GetText("Invalid path characters in path: '{0}'"), appBase), "appBase");
			}
			string fileName = Path.GetFileName(appBase);
			if (fileName != null && fileName.LastIndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
			{
				throw new ArgumentException(string.Format(Locale.GetText("Invalid filename characters in path: '{0}'"), appBase), "appBase");
			}
			return appBase;
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x0600186F RID: 6255 RVA: 0x0005E653 File Offset: 0x0005C853
		// (set) Token: 0x06001870 RID: 6256 RVA: 0x0005E660 File Offset: 0x0005C860
		public string ApplicationBase
		{
			get
			{
				return AppDomainSetup.GetAppBase(this.application_base);
			}
			set
			{
				this.application_base = value;
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06001871 RID: 6257 RVA: 0x0005E669 File Offset: 0x0005C869
		// (set) Token: 0x06001872 RID: 6258 RVA: 0x0005E671 File Offset: 0x0005C871
		public string ApplicationName
		{
			get
			{
				return this.application_name;
			}
			set
			{
				this.application_name = value;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06001873 RID: 6259 RVA: 0x0005E67A File Offset: 0x0005C87A
		// (set) Token: 0x06001874 RID: 6260 RVA: 0x0005E682 File Offset: 0x0005C882
		public string CachePath
		{
			get
			{
				return this.cache_path;
			}
			set
			{
				this.cache_path = value;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06001875 RID: 6261 RVA: 0x0005E68C File Offset: 0x0005C88C
		// (set) Token: 0x06001876 RID: 6262 RVA: 0x0005E6DB File Offset: 0x0005C8DB
		public string ConfigurationFile
		{
			get
			{
				if (this.configuration_file == null)
				{
					return null;
				}
				if (Path.IsPathRooted(this.configuration_file))
				{
					return this.configuration_file;
				}
				if (this.ApplicationBase == null)
				{
					throw new MemberAccessException("The ApplicationBase must be set before retrieving this property.");
				}
				return Path.Combine(this.ApplicationBase, this.configuration_file);
			}
			set
			{
				this.configuration_file = value;
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06001877 RID: 6263 RVA: 0x0005E6E4 File Offset: 0x0005C8E4
		// (set) Token: 0x06001878 RID: 6264 RVA: 0x0005E6EC File Offset: 0x0005C8EC
		public bool DisallowPublisherPolicy
		{
			get
			{
				return this.publisher_policy;
			}
			set
			{
				this.publisher_policy = value;
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06001879 RID: 6265 RVA: 0x0005E6F8 File Offset: 0x0005C8F8
		// (set) Token: 0x0600187A RID: 6266 RVA: 0x0005E748 File Offset: 0x0005C948
		public string DynamicBase
		{
			get
			{
				if (this.dynamic_base == null)
				{
					return null;
				}
				if (Path.IsPathRooted(this.dynamic_base))
				{
					return this.dynamic_base;
				}
				if (this.ApplicationBase == null)
				{
					throw new MemberAccessException("The ApplicationBase must be set before retrieving this property.");
				}
				return Path.Combine(this.ApplicationBase, this.dynamic_base);
			}
			set
			{
				if (this.application_name == null)
				{
					throw new MemberAccessException("ApplicationName must be set before the DynamicBase can be set.");
				}
				this.dynamic_base = Path.Combine(value, ((uint)this.application_name.GetHashCode()).ToString("x"));
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x0600187B RID: 6267 RVA: 0x0005E78C File Offset: 0x0005C98C
		// (set) Token: 0x0600187C RID: 6268 RVA: 0x0005E794 File Offset: 0x0005C994
		public string LicenseFile
		{
			get
			{
				return this.license_file;
			}
			set
			{
				this.license_file = value;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x0600187D RID: 6269 RVA: 0x0005E79D File Offset: 0x0005C99D
		// (set) Token: 0x0600187E RID: 6270 RVA: 0x0005E7A5 File Offset: 0x0005C9A5
		[MonoLimitation("In Mono this is controlled by the --share-code flag")]
		public LoaderOptimization LoaderOptimization
		{
			get
			{
				return this.loader_optimization;
			}
			set
			{
				this.loader_optimization = value;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x0600187F RID: 6271 RVA: 0x0005E7AE File Offset: 0x0005C9AE
		// (set) Token: 0x06001880 RID: 6272 RVA: 0x0005E7B6 File Offset: 0x0005C9B6
		public string AppDomainManagerAssembly
		{
			get
			{
				return this.manager_assembly;
			}
			set
			{
				this.manager_assembly = value;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06001881 RID: 6273 RVA: 0x0005E7BF File Offset: 0x0005C9BF
		// (set) Token: 0x06001882 RID: 6274 RVA: 0x0005E7C7 File Offset: 0x0005C9C7
		public string AppDomainManagerType
		{
			get
			{
				return this.manager_type;
			}
			set
			{
				this.manager_type = value;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06001883 RID: 6275 RVA: 0x0005E7D0 File Offset: 0x0005C9D0
		// (set) Token: 0x06001884 RID: 6276 RVA: 0x0005E7D8 File Offset: 0x0005C9D8
		public string[] PartialTrustVisibleAssemblies
		{
			get
			{
				return this.partial_visible_assemblies;
			}
			set
			{
				if (value != null)
				{
					this.partial_visible_assemblies = (string[])value.Clone();
					Array.Sort<string>(this.partial_visible_assemblies, StringComparer.OrdinalIgnoreCase);
					return;
				}
				this.partial_visible_assemblies = null;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06001885 RID: 6277 RVA: 0x0005E806 File Offset: 0x0005CA06
		// (set) Token: 0x06001886 RID: 6278 RVA: 0x0005E80E File Offset: 0x0005CA0E
		public string PrivateBinPath
		{
			get
			{
				return this.private_bin_path;
			}
			set
			{
				this.private_bin_path = value;
				this.path_changed = true;
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06001887 RID: 6279 RVA: 0x0005E81E File Offset: 0x0005CA1E
		// (set) Token: 0x06001888 RID: 6280 RVA: 0x0005E826 File Offset: 0x0005CA26
		public string PrivateBinPathProbe
		{
			get
			{
				return this.private_bin_path_probe;
			}
			set
			{
				this.private_bin_path_probe = value;
				this.path_changed = true;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06001889 RID: 6281 RVA: 0x0005E836 File Offset: 0x0005CA36
		// (set) Token: 0x0600188A RID: 6282 RVA: 0x0005E83E File Offset: 0x0005CA3E
		public string ShadowCopyDirectories
		{
			get
			{
				return this.shadow_copy_directories;
			}
			set
			{
				this.shadow_copy_directories = value;
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x0600188B RID: 6283 RVA: 0x0005E847 File Offset: 0x0005CA47
		// (set) Token: 0x0600188C RID: 6284 RVA: 0x0005E84F File Offset: 0x0005CA4F
		public string ShadowCopyFiles
		{
			get
			{
				return this.shadow_copy_files;
			}
			set
			{
				this.shadow_copy_files = value;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x0600188D RID: 6285 RVA: 0x0005E858 File Offset: 0x0005CA58
		// (set) Token: 0x0600188E RID: 6286 RVA: 0x0005E860 File Offset: 0x0005CA60
		public bool DisallowBindingRedirects
		{
			get
			{
				return this.disallow_binding_redirects;
			}
			set
			{
				this.disallow_binding_redirects = value;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x0600188F RID: 6287 RVA: 0x0005E869 File Offset: 0x0005CA69
		// (set) Token: 0x06001890 RID: 6288 RVA: 0x0005E871 File Offset: 0x0005CA71
		public bool DisallowCodeDownload
		{
			get
			{
				return this.disallow_code_downloads;
			}
			set
			{
				this.disallow_code_downloads = value;
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06001891 RID: 6289 RVA: 0x0005E87A File Offset: 0x0005CA7A
		// (set) Token: 0x06001892 RID: 6290 RVA: 0x0005E882 File Offset: 0x0005CA82
		public string TargetFrameworkName
		{
			[CompilerGenerated]
			get
			{
				return this.<TargetFrameworkName>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<TargetFrameworkName>k__BackingField = value;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06001893 RID: 6291 RVA: 0x0005E88B File Offset: 0x0005CA8B
		// (set) Token: 0x06001894 RID: 6292 RVA: 0x0005E8A8 File Offset: 0x0005CAA8
		public ActivationArguments ActivationArguments
		{
			get
			{
				if (this._activationArguments != null)
				{
					return this._activationArguments;
				}
				this.DeserializeNonPrimitives();
				return this._activationArguments;
			}
			set
			{
				this._activationArguments = value;
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06001895 RID: 6293 RVA: 0x0005E8B1 File Offset: 0x0005CAB1
		// (set) Token: 0x06001896 RID: 6294 RVA: 0x0005E8CE File Offset: 0x0005CACE
		[MonoLimitation("it needs to be invoked within the created domain")]
		public AppDomainInitializer AppDomainInitializer
		{
			get
			{
				if (this.domain_initializer != null)
				{
					return this.domain_initializer;
				}
				this.DeserializeNonPrimitives();
				return this.domain_initializer;
			}
			set
			{
				this.domain_initializer = value;
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06001897 RID: 6295 RVA: 0x0005E8D7 File Offset: 0x0005CAD7
		// (set) Token: 0x06001898 RID: 6296 RVA: 0x0005E8DF File Offset: 0x0005CADF
		[MonoLimitation("it needs to be used to invoke the initializer within the created domain")]
		public string[] AppDomainInitializerArguments
		{
			get
			{
				return this.domain_initializer_args;
			}
			set
			{
				this.domain_initializer_args = value;
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06001899 RID: 6297 RVA: 0x0005E8E8 File Offset: 0x0005CAE8
		// (set) Token: 0x0600189A RID: 6298 RVA: 0x0005E918 File Offset: 0x0005CB18
		[MonoNotSupported("This property exists but not considered.")]
		public ApplicationTrust ApplicationTrust
		{
			get
			{
				if (this.application_trust != null)
				{
					return this.application_trust;
				}
				this.DeserializeNonPrimitives();
				if (this.application_trust == null)
				{
					this.application_trust = new ApplicationTrust();
				}
				return this.application_trust;
			}
			set
			{
				this.application_trust = value;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x0600189B RID: 6299 RVA: 0x0005E921 File Offset: 0x0005CB21
		// (set) Token: 0x0600189C RID: 6300 RVA: 0x0005E929 File Offset: 0x0005CB29
		[MonoNotSupported("This property exists but not considered.")]
		public bool DisallowApplicationBaseProbing
		{
			get
			{
				return this.disallow_appbase_probe;
			}
			set
			{
				this.disallow_appbase_probe = value;
			}
		}

		// Token: 0x0600189D RID: 6301 RVA: 0x0005E932 File Offset: 0x0005CB32
		[MonoNotSupported("This method exists but not considered.")]
		public byte[] GetConfigurationBytes()
		{
			if (this.configuration_bytes == null)
			{
				return null;
			}
			return this.configuration_bytes.Clone() as byte[];
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x0005E94E File Offset: 0x0005CB4E
		[MonoNotSupported("This method exists but not considered.")]
		public void SetConfigurationBytes(byte[] value)
		{
			this.configuration_bytes = value;
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x0005E958 File Offset: 0x0005CB58
		private void DeserializeNonPrimitives()
		{
			lock (this)
			{
				if (this.serialized_non_primitives != null)
				{
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					MemoryStream memoryStream = new MemoryStream(this.serialized_non_primitives);
					object[] array = (object[])binaryFormatter.Deserialize(memoryStream);
					this._activationArguments = (ActivationArguments)array[0];
					this.domain_initializer = (AppDomainInitializer)array[1];
					this.application_trust = (ApplicationTrust)array[2];
					this.serialized_non_primitives = null;
				}
			}
		}

		// Token: 0x060018A0 RID: 6304 RVA: 0x0005E9E8 File Offset: 0x0005CBE8
		internal void SerializeNonPrimitives()
		{
			object[] array = new object[] { this._activationArguments, this.domain_initializer, this.application_trust };
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			MemoryStream memoryStream = new MemoryStream();
			binaryFormatter.Serialize(memoryStream, array);
			this.serialized_non_primitives = memoryStream.ToArray();
		}

		// Token: 0x060018A1 RID: 6305 RVA: 0x00004088 File Offset: 0x00002288
		[MonoTODO("not implemented, does not throw because it's used in testing moonlight")]
		public void SetCompatibilitySwitches(IEnumerable<string> switches)
		{
		}

		// Token: 0x040015A3 RID: 5539
		private string application_base;

		// Token: 0x040015A4 RID: 5540
		private string application_name;

		// Token: 0x040015A5 RID: 5541
		private string cache_path;

		// Token: 0x040015A6 RID: 5542
		private string configuration_file;

		// Token: 0x040015A7 RID: 5543
		private string dynamic_base;

		// Token: 0x040015A8 RID: 5544
		private string license_file;

		// Token: 0x040015A9 RID: 5545
		private string private_bin_path;

		// Token: 0x040015AA RID: 5546
		private string private_bin_path_probe;

		// Token: 0x040015AB RID: 5547
		private string shadow_copy_directories;

		// Token: 0x040015AC RID: 5548
		private string shadow_copy_files;

		// Token: 0x040015AD RID: 5549
		private bool publisher_policy;

		// Token: 0x040015AE RID: 5550
		private bool path_changed;

		// Token: 0x040015AF RID: 5551
		private LoaderOptimization loader_optimization;

		// Token: 0x040015B0 RID: 5552
		private bool disallow_binding_redirects;

		// Token: 0x040015B1 RID: 5553
		private bool disallow_code_downloads;

		// Token: 0x040015B2 RID: 5554
		private ActivationArguments _activationArguments;

		// Token: 0x040015B3 RID: 5555
		private AppDomainInitializer domain_initializer;

		// Token: 0x040015B4 RID: 5556
		[NonSerialized]
		private ApplicationTrust application_trust;

		// Token: 0x040015B5 RID: 5557
		private string[] domain_initializer_args;

		// Token: 0x040015B6 RID: 5558
		private bool disallow_appbase_probe;

		// Token: 0x040015B7 RID: 5559
		private byte[] configuration_bytes;

		// Token: 0x040015B8 RID: 5560
		private byte[] serialized_non_primitives;

		// Token: 0x040015B9 RID: 5561
		private string manager_assembly;

		// Token: 0x040015BA RID: 5562
		private string manager_type;

		// Token: 0x040015BB RID: 5563
		private string[] partial_visible_assemblies;

		// Token: 0x040015BC RID: 5564
		[CompilerGenerated]
		private string <TargetFrameworkName>k__BackingField;
	}
}
