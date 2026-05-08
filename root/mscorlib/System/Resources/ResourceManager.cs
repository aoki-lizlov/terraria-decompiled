using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Text;
using System.Threading;

namespace System.Resources
{
	// Token: 0x02000838 RID: 2104
	[ComVisible(true)]
	[Serializable]
	public class ResourceManager
	{
		// Token: 0x06004714 RID: 18196 RVA: 0x000E8EE4 File Offset: 0x000E70E4
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void Init()
		{
			try
			{
				this.m_callingAssembly = (RuntimeAssembly)Assembly.GetCallingAssembly();
			}
			catch
			{
			}
		}

		// Token: 0x06004715 RID: 18197 RVA: 0x000E8F18 File Offset: 0x000E7118
		protected ResourceManager()
		{
			this.Init();
			this._lastUsedResourceCache = new ResourceManager.CultureNameResourceSetPair();
			ResourceManager.ResourceManagerMediator resourceManagerMediator = new ResourceManager.ResourceManagerMediator(this);
			this.resourceGroveler = new ManifestBasedResourceGroveler(resourceManagerMediator);
		}

		// Token: 0x06004716 RID: 18198 RVA: 0x000E8F50 File Offset: 0x000E7150
		private ResourceManager(string baseName, string resourceDir, Type usingResourceSet)
		{
			if (baseName == null)
			{
				throw new ArgumentNullException("baseName");
			}
			if (resourceDir == null)
			{
				throw new ArgumentNullException("resourceDir");
			}
			this.BaseNameField = baseName;
			this.moduleDir = resourceDir;
			this._userResourceSet = usingResourceSet;
			this.ResourceSets = new Hashtable();
			this._resourceSets = new Dictionary<string, ResourceSet>();
			this._lastUsedResourceCache = new ResourceManager.CultureNameResourceSetPair();
			this.UseManifest = false;
			ResourceManager.ResourceManagerMediator resourceManagerMediator = new ResourceManager.ResourceManagerMediator(this);
			this.resourceGroveler = new FileBasedResourceGroveler(resourceManagerMediator);
		}

		// Token: 0x06004717 RID: 18199 RVA: 0x000E8FD0 File Offset: 0x000E71D0
		[MethodImpl(MethodImplOptions.NoInlining)]
		public ResourceManager(string baseName, Assembly assembly)
		{
			if (baseName == null)
			{
				throw new ArgumentNullException("baseName");
			}
			if (null == assembly)
			{
				throw new ArgumentNullException("assembly");
			}
			if (!(assembly is RuntimeAssembly))
			{
				throw new ArgumentException(Environment.GetResourceString("Assembly must be a runtime Assembly object."));
			}
			this.MainAssembly = assembly;
			this.BaseNameField = baseName;
			this.SetAppXConfiguration();
			this.CommonAssemblyInit();
			try
			{
				this.m_callingAssembly = (RuntimeAssembly)Assembly.GetCallingAssembly();
				if (assembly == typeof(object).Assembly && this.m_callingAssembly != assembly)
				{
					this.m_callingAssembly = null;
				}
			}
			catch
			{
			}
		}

		// Token: 0x06004718 RID: 18200 RVA: 0x000E9088 File Offset: 0x000E7288
		[MethodImpl(MethodImplOptions.NoInlining)]
		public ResourceManager(string baseName, Assembly assembly, Type usingResourceSet)
		{
			if (baseName == null)
			{
				throw new ArgumentNullException("baseName");
			}
			if (null == assembly)
			{
				throw new ArgumentNullException("assembly");
			}
			if (!(assembly is RuntimeAssembly))
			{
				throw new ArgumentException(Environment.GetResourceString("Assembly must be a runtime Assembly object."));
			}
			this.MainAssembly = assembly;
			this.BaseNameField = baseName;
			if (usingResourceSet != null && usingResourceSet != ResourceManager._minResourceSet && !usingResourceSet.IsSubclassOf(ResourceManager._minResourceSet))
			{
				throw new ArgumentException(Environment.GetResourceString("Type parameter must refer to a subclass of ResourceSet."), "usingResourceSet");
			}
			this._userResourceSet = usingResourceSet;
			this.CommonAssemblyInit();
			try
			{
				this.m_callingAssembly = (RuntimeAssembly)Assembly.GetCallingAssembly();
				if (assembly == typeof(object).Assembly && this.m_callingAssembly != assembly)
				{
					this.m_callingAssembly = null;
				}
			}
			catch
			{
			}
		}

		// Token: 0x06004719 RID: 18201 RVA: 0x000E917C File Offset: 0x000E737C
		[MethodImpl(MethodImplOptions.NoInlining)]
		public ResourceManager(Type resourceSource)
		{
			if (null == resourceSource)
			{
				throw new ArgumentNullException("resourceSource");
			}
			if (!(resourceSource is RuntimeType))
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a runtime Type object."));
			}
			this._locationInfo = resourceSource;
			this.MainAssembly = this._locationInfo.Assembly;
			this.BaseNameField = resourceSource.Name;
			this.SetAppXConfiguration();
			this.CommonAssemblyInit();
			try
			{
				this.m_callingAssembly = (RuntimeAssembly)Assembly.GetCallingAssembly();
				if (this.MainAssembly == typeof(object).Assembly && this.m_callingAssembly != this.MainAssembly)
				{
					this.m_callingAssembly = null;
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600471A RID: 18202 RVA: 0x000E9248 File Offset: 0x000E7448
		[OnDeserializing]
		private void OnDeserializing(StreamingContext ctx)
		{
			this._resourceSets = null;
			this.resourceGroveler = null;
			this._lastUsedResourceCache = null;
		}

		// Token: 0x0600471B RID: 18203 RVA: 0x000E9260 File Offset: 0x000E7460
		[SecuritySafeCritical]
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			this._resourceSets = new Dictionary<string, ResourceSet>();
			this._lastUsedResourceCache = new ResourceManager.CultureNameResourceSetPair();
			ResourceManager.ResourceManagerMediator resourceManagerMediator = new ResourceManager.ResourceManagerMediator(this);
			if (this.UseManifest)
			{
				this.resourceGroveler = new ManifestBasedResourceGroveler(resourceManagerMediator);
			}
			else
			{
				this.resourceGroveler = new FileBasedResourceGroveler(resourceManagerMediator);
			}
			if (this.m_callingAssembly == null)
			{
				this.m_callingAssembly = (RuntimeAssembly)this._callingAssembly;
			}
			if (this.UseManifest && this._neutralResourcesCulture == null)
			{
				this._neutralResourcesCulture = ManifestBasedResourceGroveler.GetNeutralResourcesLanguage(this.MainAssembly, ref this._fallbackLoc);
			}
		}

		// Token: 0x0600471C RID: 18204 RVA: 0x000E92F2 File Offset: 0x000E74F2
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			this._callingAssembly = this.m_callingAssembly;
			this.UseSatelliteAssem = this.UseManifest;
			this.ResourceSets = new Hashtable();
		}

		// Token: 0x0600471D RID: 18205 RVA: 0x000E9318 File Offset: 0x000E7518
		[SecuritySafeCritical]
		private void CommonAssemblyInit()
		{
			this.UseManifest = true;
			this._resourceSets = new Dictionary<string, ResourceSet>();
			this._lastUsedResourceCache = new ResourceManager.CultureNameResourceSetPair();
			this._fallbackLoc = UltimateResourceFallbackLocation.MainAssembly;
			ResourceManager.ResourceManagerMediator resourceManagerMediator = new ResourceManager.ResourceManagerMediator(this);
			this.resourceGroveler = new ManifestBasedResourceGroveler(resourceManagerMediator);
			this._neutralResourcesCulture = ManifestBasedResourceGroveler.GetNeutralResourcesLanguage(this.MainAssembly, ref this._fallbackLoc);
			this.ResourceSets = new Hashtable();
		}

		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x0600471E RID: 18206 RVA: 0x000E937E File Offset: 0x000E757E
		public virtual string BaseName
		{
			get
			{
				return this.BaseNameField;
			}
		}

		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x0600471F RID: 18207 RVA: 0x000E9386 File Offset: 0x000E7586
		// (set) Token: 0x06004720 RID: 18208 RVA: 0x000E938E File Offset: 0x000E758E
		public virtual bool IgnoreCase
		{
			get
			{
				return this._ignoreCase;
			}
			set
			{
				this._ignoreCase = value;
			}
		}

		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x06004721 RID: 18209 RVA: 0x000E9397 File Offset: 0x000E7597
		public virtual Type ResourceSetType
		{
			get
			{
				if (!(this._userResourceSet == null))
				{
					return this._userResourceSet;
				}
				return typeof(RuntimeResourceSet);
			}
		}

		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x06004722 RID: 18210 RVA: 0x000E93B8 File Offset: 0x000E75B8
		// (set) Token: 0x06004723 RID: 18211 RVA: 0x000E93C0 File Offset: 0x000E75C0
		protected UltimateResourceFallbackLocation FallbackLocation
		{
			get
			{
				return this._fallbackLoc;
			}
			set
			{
				this._fallbackLoc = value;
			}
		}

		// Token: 0x06004724 RID: 18212 RVA: 0x000E93CC File Offset: 0x000E75CC
		public virtual void ReleaseAllResources()
		{
			Dictionary<string, ResourceSet> resourceSets = this._resourceSets;
			this._resourceSets = new Dictionary<string, ResourceSet>();
			this._lastUsedResourceCache = new ResourceManager.CultureNameResourceSetPair();
			Dictionary<string, ResourceSet> dictionary = resourceSets;
			lock (dictionary)
			{
				IDictionaryEnumerator dictionaryEnumerator = resourceSets.GetEnumerator();
				IDictionaryEnumerator dictionaryEnumerator2 = null;
				if (this.ResourceSets != null)
				{
					dictionaryEnumerator2 = this.ResourceSets.GetEnumerator();
				}
				this.ResourceSets = new Hashtable();
				while (dictionaryEnumerator.MoveNext())
				{
					((ResourceSet)dictionaryEnumerator.Value).Close();
				}
				if (dictionaryEnumerator2 != null)
				{
					while (dictionaryEnumerator2.MoveNext())
					{
						((ResourceSet)dictionaryEnumerator2.Value).Close();
					}
				}
			}
		}

		// Token: 0x06004725 RID: 18213 RVA: 0x000E9488 File Offset: 0x000E7688
		public static ResourceManager CreateFileBasedResourceManager(string baseName, string resourceDir, Type usingResourceSet)
		{
			return new ResourceManager(baseName, resourceDir, usingResourceSet);
		}

		// Token: 0x06004726 RID: 18214 RVA: 0x000E9494 File Offset: 0x000E7694
		protected virtual string GetResourceFileName(CultureInfo culture)
		{
			StringBuilder stringBuilder = new StringBuilder(255);
			stringBuilder.Append(this.BaseNameField);
			if (!culture.HasInvariantCultureName)
			{
				CultureInfo.VerifyCultureName(culture.Name, true);
				stringBuilder.Append('.');
				stringBuilder.Append(culture.Name);
			}
			stringBuilder.Append(".resources");
			return stringBuilder.ToString();
		}

		// Token: 0x06004727 RID: 18215 RVA: 0x000E94F8 File Offset: 0x000E76F8
		internal ResourceSet GetFirstResourceSet(CultureInfo culture)
		{
			if (this._neutralResourcesCulture != null && culture.Name == this._neutralResourcesCulture.Name)
			{
				culture = CultureInfo.InvariantCulture;
			}
			if (this._lastUsedResourceCache != null)
			{
				ResourceManager.CultureNameResourceSetPair cultureNameResourceSetPair = this._lastUsedResourceCache;
				lock (cultureNameResourceSetPair)
				{
					if (culture.Name == this._lastUsedResourceCache.lastCultureName)
					{
						return this._lastUsedResourceCache.lastResourceSet;
					}
				}
			}
			Dictionary<string, ResourceSet> resourceSets = this._resourceSets;
			ResourceSet resourceSet = null;
			if (resourceSets != null)
			{
				Dictionary<string, ResourceSet> dictionary = resourceSets;
				lock (dictionary)
				{
					resourceSets.TryGetValue(culture.Name, out resourceSet);
				}
			}
			if (resourceSet != null)
			{
				if (this._lastUsedResourceCache != null)
				{
					ResourceManager.CultureNameResourceSetPair cultureNameResourceSetPair = this._lastUsedResourceCache;
					lock (cultureNameResourceSetPair)
					{
						this._lastUsedResourceCache.lastCultureName = culture.Name;
						this._lastUsedResourceCache.lastResourceSet = resourceSet;
					}
				}
				return resourceSet;
			}
			return null;
		}

		// Token: 0x06004728 RID: 18216 RVA: 0x000E9624 File Offset: 0x000E7824
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public virtual ResourceSet GetResourceSet(CultureInfo culture, bool createIfNotExists, bool tryParents)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			Dictionary<string, ResourceSet> resourceSets = this._resourceSets;
			if (resourceSets != null)
			{
				Dictionary<string, ResourceSet> dictionary = resourceSets;
				lock (dictionary)
				{
					ResourceSet resourceSet;
					if (resourceSets.TryGetValue(culture.Name, out resourceSet))
					{
						return resourceSet;
					}
				}
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			if (this.UseManifest && culture.HasInvariantCultureName)
			{
				string resourceFileName = this.GetResourceFileName(culture);
				Stream manifestResourceStream = ((RuntimeAssembly)this.MainAssembly).GetManifestResourceStream(this._locationInfo, resourceFileName, this.m_callingAssembly == this.MainAssembly, ref stackCrawlMark);
				if (createIfNotExists && manifestResourceStream != null)
				{
					ResourceSet resourceSet = ((ManifestBasedResourceGroveler)this.resourceGroveler).CreateResourceSet(manifestResourceStream, this.MainAssembly);
					ResourceManager.AddResourceSet(resourceSets, culture.Name, ref resourceSet);
					return resourceSet;
				}
			}
			return this.InternalGetResourceSet(culture, createIfNotExists, tryParents);
		}

		// Token: 0x06004729 RID: 18217 RVA: 0x000E9710 File Offset: 0x000E7910
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected virtual ResourceSet InternalGetResourceSet(CultureInfo culture, bool createIfNotExists, bool tryParents)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.InternalGetResourceSet(culture, createIfNotExists, tryParents, ref stackCrawlMark);
		}

		// Token: 0x0600472A RID: 18218 RVA: 0x000E972C File Offset: 0x000E792C
		[SecurityCritical]
		private ResourceSet InternalGetResourceSet(CultureInfo requestedCulture, bool createIfNotExists, bool tryParents, ref StackCrawlMark stackMark)
		{
			Dictionary<string, ResourceSet> resourceSets = this._resourceSets;
			ResourceSet resourceSet = null;
			CultureInfo cultureInfo = null;
			Dictionary<string, ResourceSet> dictionary = resourceSets;
			lock (dictionary)
			{
				if (resourceSets.TryGetValue(requestedCulture.Name, out resourceSet))
				{
					return resourceSet;
				}
			}
			ResourceFallbackManager resourceFallbackManager = new ResourceFallbackManager(requestedCulture, this._neutralResourcesCulture, tryParents);
			foreach (CultureInfo cultureInfo2 in resourceFallbackManager)
			{
				dictionary = resourceSets;
				lock (dictionary)
				{
					if (resourceSets.TryGetValue(cultureInfo2.Name, out resourceSet))
					{
						if (requestedCulture != cultureInfo2)
						{
							cultureInfo = cultureInfo2;
						}
						break;
					}
				}
				resourceSet = this.resourceGroveler.GrovelForResourceSet(cultureInfo2, resourceSets, tryParents, createIfNotExists, ref stackMark);
				if (resourceSet != null)
				{
					cultureInfo = cultureInfo2;
					break;
				}
			}
			if (resourceSet != null && cultureInfo != null)
			{
				foreach (CultureInfo cultureInfo3 in resourceFallbackManager)
				{
					ResourceManager.AddResourceSet(resourceSets, cultureInfo3.Name, ref resourceSet);
					if (cultureInfo3 == cultureInfo)
					{
						break;
					}
				}
			}
			return resourceSet;
		}

		// Token: 0x0600472B RID: 18219 RVA: 0x000E9884 File Offset: 0x000E7A84
		private static void AddResourceSet(Dictionary<string, ResourceSet> localResourceSets, string cultureName, ref ResourceSet rs)
		{
			lock (localResourceSets)
			{
				ResourceSet resourceSet;
				if (localResourceSets.TryGetValue(cultureName, out resourceSet))
				{
					if (resourceSet != rs)
					{
						if (!localResourceSets.ContainsValue(rs))
						{
							rs.Dispose();
						}
						rs = resourceSet;
					}
				}
				else
				{
					localResourceSets.Add(cultureName, rs);
				}
			}
		}

		// Token: 0x0600472C RID: 18220 RVA: 0x000E98E8 File Offset: 0x000E7AE8
		protected static Version GetSatelliteContractVersion(Assembly a)
		{
			if (a == null)
			{
				throw new ArgumentNullException("a", Environment.GetResourceString("Assembly cannot be null."));
			}
			string text = null;
			if (a.ReflectionOnly)
			{
				foreach (CustomAttributeData customAttributeData in CustomAttributeData.GetCustomAttributes(a))
				{
					if (customAttributeData.Constructor.DeclaringType == typeof(SatelliteContractVersionAttribute))
					{
						text = (string)customAttributeData.ConstructorArguments[0].Value;
						break;
					}
				}
				if (text == null)
				{
					return null;
				}
			}
			else
			{
				object[] customAttributes = a.GetCustomAttributes(typeof(SatelliteContractVersionAttribute), false);
				if (customAttributes.Length == 0)
				{
					return null;
				}
				text = ((SatelliteContractVersionAttribute)customAttributes[0]).Version;
			}
			Version version;
			try
			{
				version = new Version(text);
			}
			catch (ArgumentOutOfRangeException ex)
			{
				if (a == typeof(object).Assembly)
				{
					return null;
				}
				throw new ArgumentException(Environment.GetResourceString("Satellite contract version attribute on the assembly '{0}' specifies an invalid version: {1}.", new object[]
				{
					a.ToString(),
					text
				}), ex);
			}
			return version;
		}

		// Token: 0x0600472D RID: 18221 RVA: 0x000E9A1C File Offset: 0x000E7C1C
		[SecuritySafeCritical]
		protected static CultureInfo GetNeutralResourcesLanguage(Assembly a)
		{
			UltimateResourceFallbackLocation ultimateResourceFallbackLocation = UltimateResourceFallbackLocation.MainAssembly;
			return ManifestBasedResourceGroveler.GetNeutralResourcesLanguage(a, ref ultimateResourceFallbackLocation);
		}

		// Token: 0x0600472E RID: 18222 RVA: 0x000E9A34 File Offset: 0x000E7C34
		internal static bool CompareNames(string asmTypeName1, string typeName2, AssemblyName asmName2)
		{
			int num = asmTypeName1.IndexOf(',');
			if (((num == -1) ? asmTypeName1.Length : num) != typeName2.Length)
			{
				return false;
			}
			if (string.Compare(asmTypeName1, 0, typeName2, 0, typeName2.Length, StringComparison.Ordinal) != 0)
			{
				return false;
			}
			if (num == -1)
			{
				return true;
			}
			while (char.IsWhiteSpace(asmTypeName1[++num]))
			{
			}
			AssemblyName assemblyName = new AssemblyName(asmTypeName1.Substring(num));
			if (string.Compare(assemblyName.Name, asmName2.Name, StringComparison.OrdinalIgnoreCase) != 0)
			{
				return false;
			}
			if (string.Compare(assemblyName.Name, "mscorlib", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return true;
			}
			if (assemblyName.CultureInfo != null && asmName2.CultureInfo != null && assemblyName.CultureInfo.LCID != asmName2.CultureInfo.LCID)
			{
				return false;
			}
			byte[] publicKeyToken = assemblyName.GetPublicKeyToken();
			byte[] publicKeyToken2 = asmName2.GetPublicKeyToken();
			if (publicKeyToken != null && publicKeyToken2 != null)
			{
				if (publicKeyToken.Length != publicKeyToken2.Length)
				{
					return false;
				}
				for (int i = 0; i < publicKeyToken.Length; i++)
				{
					if (publicKeyToken[i] != publicKeyToken2[i])
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600472F RID: 18223 RVA: 0x00004088 File Offset: 0x00002288
		private void SetAppXConfiguration()
		{
		}

		// Token: 0x06004730 RID: 18224 RVA: 0x000E9B2C File Offset: 0x000E7D2C
		public virtual string GetString(string name)
		{
			return this.GetString(name, null);
		}

		// Token: 0x06004731 RID: 18225 RVA: 0x000E9B38 File Offset: 0x000E7D38
		public virtual string GetString(string name, CultureInfo culture)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (culture == null)
			{
				culture = Thread.CurrentThread.GetCurrentUICultureNoAppX();
			}
			ResourceSet resourceSet = this.GetFirstResourceSet(culture);
			if (resourceSet != null)
			{
				string @string = resourceSet.GetString(name, this._ignoreCase);
				if (@string != null)
				{
					return @string;
				}
			}
			foreach (CultureInfo cultureInfo in new ResourceFallbackManager(culture, this._neutralResourcesCulture, true))
			{
				ResourceSet resourceSet2 = this.InternalGetResourceSet(cultureInfo, true, true);
				if (resourceSet2 == null)
				{
					break;
				}
				if (resourceSet2 != resourceSet)
				{
					string string2 = resourceSet2.GetString(name, this._ignoreCase);
					if (string2 != null)
					{
						if (this._lastUsedResourceCache != null)
						{
							ResourceManager.CultureNameResourceSetPair lastUsedResourceCache = this._lastUsedResourceCache;
							lock (lastUsedResourceCache)
							{
								this._lastUsedResourceCache.lastCultureName = cultureInfo.Name;
								this._lastUsedResourceCache.lastResourceSet = resourceSet2;
							}
						}
						return string2;
					}
					resourceSet = resourceSet2;
				}
			}
			return null;
		}

		// Token: 0x06004732 RID: 18226 RVA: 0x000E9C54 File Offset: 0x000E7E54
		public virtual object GetObject(string name)
		{
			return this.GetObject(name, null, true);
		}

		// Token: 0x06004733 RID: 18227 RVA: 0x000E9C5F File Offset: 0x000E7E5F
		public virtual object GetObject(string name, CultureInfo culture)
		{
			return this.GetObject(name, culture, true);
		}

		// Token: 0x06004734 RID: 18228 RVA: 0x000E9C6C File Offset: 0x000E7E6C
		private object GetObject(string name, CultureInfo culture, bool wrapUnmanagedMemStream)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (culture == null)
			{
				culture = Thread.CurrentThread.GetCurrentUICultureNoAppX();
			}
			ResourceSet resourceSet = this.GetFirstResourceSet(culture);
			if (resourceSet != null)
			{
				object @object = resourceSet.GetObject(name, this._ignoreCase);
				if (@object != null)
				{
					UnmanagedMemoryStream unmanagedMemoryStream = @object as UnmanagedMemoryStream;
					if (unmanagedMemoryStream != null && wrapUnmanagedMemStream)
					{
						return new UnmanagedMemoryStreamWrapper(unmanagedMemoryStream);
					}
					return @object;
				}
			}
			foreach (CultureInfo cultureInfo in new ResourceFallbackManager(culture, this._neutralResourcesCulture, true))
			{
				ResourceSet resourceSet2 = this.InternalGetResourceSet(cultureInfo, true, true);
				if (resourceSet2 == null)
				{
					break;
				}
				if (resourceSet2 != resourceSet)
				{
					object object2 = resourceSet2.GetObject(name, this._ignoreCase);
					if (object2 != null)
					{
						if (this._lastUsedResourceCache != null)
						{
							ResourceManager.CultureNameResourceSetPair lastUsedResourceCache = this._lastUsedResourceCache;
							lock (lastUsedResourceCache)
							{
								this._lastUsedResourceCache.lastCultureName = cultureInfo.Name;
								this._lastUsedResourceCache.lastResourceSet = resourceSet2;
							}
						}
						UnmanagedMemoryStream unmanagedMemoryStream2 = object2 as UnmanagedMemoryStream;
						if (unmanagedMemoryStream2 != null && wrapUnmanagedMemStream)
						{
							return new UnmanagedMemoryStreamWrapper(unmanagedMemoryStream2);
						}
						return object2;
					}
					else
					{
						resourceSet = resourceSet2;
					}
				}
			}
			return null;
		}

		// Token: 0x06004735 RID: 18229 RVA: 0x000E9DC4 File Offset: 0x000E7FC4
		[ComVisible(false)]
		public UnmanagedMemoryStream GetStream(string name)
		{
			return this.GetStream(name, null);
		}

		// Token: 0x06004736 RID: 18230 RVA: 0x000E9DD0 File Offset: 0x000E7FD0
		[ComVisible(false)]
		public UnmanagedMemoryStream GetStream(string name, CultureInfo culture)
		{
			object @object = this.GetObject(name, culture, false);
			UnmanagedMemoryStream unmanagedMemoryStream = @object as UnmanagedMemoryStream;
			if (unmanagedMemoryStream == null && @object != null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Resource '{0}' was not a Stream - call GetObject instead.", new object[] { name }));
			}
			return unmanagedMemoryStream;
		}

		// Token: 0x06004737 RID: 18231 RVA: 0x000E9E10 File Offset: 0x000E8010
		// Note: this type is marked as 'beforefieldinit'.
		static ResourceManager()
		{
		}

		// Token: 0x04002D4E RID: 11598
		protected string BaseNameField;

		// Token: 0x04002D4F RID: 11599
		[Obsolete("call InternalGetResourceSet instead")]
		protected Hashtable ResourceSets;

		// Token: 0x04002D50 RID: 11600
		[NonSerialized]
		private Dictionary<string, ResourceSet> _resourceSets;

		// Token: 0x04002D51 RID: 11601
		private string moduleDir;

		// Token: 0x04002D52 RID: 11602
		protected Assembly MainAssembly;

		// Token: 0x04002D53 RID: 11603
		private Type _locationInfo;

		// Token: 0x04002D54 RID: 11604
		private Type _userResourceSet;

		// Token: 0x04002D55 RID: 11605
		private CultureInfo _neutralResourcesCulture;

		// Token: 0x04002D56 RID: 11606
		[NonSerialized]
		private ResourceManager.CultureNameResourceSetPair _lastUsedResourceCache;

		// Token: 0x04002D57 RID: 11607
		private bool _ignoreCase;

		// Token: 0x04002D58 RID: 11608
		private bool UseManifest;

		// Token: 0x04002D59 RID: 11609
		[OptionalField(VersionAdded = 1)]
		private bool UseSatelliteAssem;

		// Token: 0x04002D5A RID: 11610
		[OptionalField]
		private UltimateResourceFallbackLocation _fallbackLoc;

		// Token: 0x04002D5B RID: 11611
		[OptionalField]
		private Version _satelliteContractVersion;

		// Token: 0x04002D5C RID: 11612
		[OptionalField]
		private bool _lookedForSatelliteContractVersion;

		// Token: 0x04002D5D RID: 11613
		[OptionalField(VersionAdded = 1)]
		private Assembly _callingAssembly;

		// Token: 0x04002D5E RID: 11614
		[OptionalField(VersionAdded = 4)]
		private RuntimeAssembly m_callingAssembly;

		// Token: 0x04002D5F RID: 11615
		[NonSerialized]
		private IResourceGroveler resourceGroveler;

		// Token: 0x04002D60 RID: 11616
		public static readonly int MagicNumber = -1091581234;

		// Token: 0x04002D61 RID: 11617
		public static readonly int HeaderVersionNumber = 1;

		// Token: 0x04002D62 RID: 11618
		private static readonly Type _minResourceSet = typeof(ResourceSet);

		// Token: 0x04002D63 RID: 11619
		internal static readonly string ResReaderTypeName = typeof(ResourceReader).FullName;

		// Token: 0x04002D64 RID: 11620
		internal static readonly string ResSetTypeName = typeof(RuntimeResourceSet).FullName;

		// Token: 0x04002D65 RID: 11621
		internal static readonly string MscorlibName = typeof(ResourceReader).Assembly.FullName;

		// Token: 0x04002D66 RID: 11622
		internal const string ResFileExtension = ".resources";

		// Token: 0x04002D67 RID: 11623
		internal const int ResFileExtensionLength = 10;

		// Token: 0x04002D68 RID: 11624
		internal static readonly int DEBUG = 0;

		// Token: 0x02000839 RID: 2105
		internal class CultureNameResourceSetPair
		{
			// Token: 0x06004738 RID: 18232 RVA: 0x000025BE File Offset: 0x000007BE
			public CultureNameResourceSetPair()
			{
			}

			// Token: 0x04002D69 RID: 11625
			public string lastCultureName;

			// Token: 0x04002D6A RID: 11626
			public ResourceSet lastResourceSet;
		}

		// Token: 0x0200083A RID: 2106
		internal class ResourceManagerMediator
		{
			// Token: 0x06004739 RID: 18233 RVA: 0x000E9E83 File Offset: 0x000E8083
			internal ResourceManagerMediator(ResourceManager rm)
			{
				if (rm == null)
				{
					throw new ArgumentNullException("rm");
				}
				this._rm = rm;
			}

			// Token: 0x17000AEF RID: 2799
			// (get) Token: 0x0600473A RID: 18234 RVA: 0x000E9EA0 File Offset: 0x000E80A0
			internal string ModuleDir
			{
				get
				{
					return this._rm.moduleDir;
				}
			}

			// Token: 0x17000AF0 RID: 2800
			// (get) Token: 0x0600473B RID: 18235 RVA: 0x000E9EAD File Offset: 0x000E80AD
			internal Type LocationInfo
			{
				get
				{
					return this._rm._locationInfo;
				}
			}

			// Token: 0x17000AF1 RID: 2801
			// (get) Token: 0x0600473C RID: 18236 RVA: 0x000E9EBA File Offset: 0x000E80BA
			internal Type UserResourceSet
			{
				get
				{
					return this._rm._userResourceSet;
				}
			}

			// Token: 0x17000AF2 RID: 2802
			// (get) Token: 0x0600473D RID: 18237 RVA: 0x000E9EC7 File Offset: 0x000E80C7
			internal string BaseNameField
			{
				get
				{
					return this._rm.BaseNameField;
				}
			}

			// Token: 0x17000AF3 RID: 2803
			// (get) Token: 0x0600473E RID: 18238 RVA: 0x000E9ED4 File Offset: 0x000E80D4
			// (set) Token: 0x0600473F RID: 18239 RVA: 0x000E9EE1 File Offset: 0x000E80E1
			internal CultureInfo NeutralResourcesCulture
			{
				get
				{
					return this._rm._neutralResourcesCulture;
				}
				set
				{
					this._rm._neutralResourcesCulture = value;
				}
			}

			// Token: 0x06004740 RID: 18240 RVA: 0x000E9EEF File Offset: 0x000E80EF
			internal string GetResourceFileName(CultureInfo culture)
			{
				return this._rm.GetResourceFileName(culture);
			}

			// Token: 0x17000AF4 RID: 2804
			// (get) Token: 0x06004741 RID: 18241 RVA: 0x000E9EFD File Offset: 0x000E80FD
			// (set) Token: 0x06004742 RID: 18242 RVA: 0x000E9F0A File Offset: 0x000E810A
			internal bool LookedForSatelliteContractVersion
			{
				get
				{
					return this._rm._lookedForSatelliteContractVersion;
				}
				set
				{
					this._rm._lookedForSatelliteContractVersion = value;
				}
			}

			// Token: 0x17000AF5 RID: 2805
			// (get) Token: 0x06004743 RID: 18243 RVA: 0x000E9F18 File Offset: 0x000E8118
			// (set) Token: 0x06004744 RID: 18244 RVA: 0x000E9F25 File Offset: 0x000E8125
			internal Version SatelliteContractVersion
			{
				get
				{
					return this._rm._satelliteContractVersion;
				}
				set
				{
					this._rm._satelliteContractVersion = value;
				}
			}

			// Token: 0x06004745 RID: 18245 RVA: 0x000E9F33 File Offset: 0x000E8133
			internal Version ObtainSatelliteContractVersion(Assembly a)
			{
				return ResourceManager.GetSatelliteContractVersion(a);
			}

			// Token: 0x17000AF6 RID: 2806
			// (get) Token: 0x06004746 RID: 18246 RVA: 0x000E9F3B File Offset: 0x000E813B
			// (set) Token: 0x06004747 RID: 18247 RVA: 0x000E9F48 File Offset: 0x000E8148
			internal UltimateResourceFallbackLocation FallbackLoc
			{
				get
				{
					return this._rm.FallbackLocation;
				}
				set
				{
					this._rm._fallbackLoc = value;
				}
			}

			// Token: 0x17000AF7 RID: 2807
			// (get) Token: 0x06004748 RID: 18248 RVA: 0x000E9F56 File Offset: 0x000E8156
			internal RuntimeAssembly CallingAssembly
			{
				get
				{
					return this._rm.m_callingAssembly;
				}
			}

			// Token: 0x17000AF8 RID: 2808
			// (get) Token: 0x06004749 RID: 18249 RVA: 0x000E9F63 File Offset: 0x000E8163
			internal RuntimeAssembly MainAssembly
			{
				get
				{
					return (RuntimeAssembly)this._rm.MainAssembly;
				}
			}

			// Token: 0x17000AF9 RID: 2809
			// (get) Token: 0x0600474A RID: 18250 RVA: 0x000E9F75 File Offset: 0x000E8175
			internal string BaseName
			{
				get
				{
					return this._rm.BaseName;
				}
			}

			// Token: 0x04002D6B RID: 11627
			private ResourceManager _rm;
		}
	}
}
