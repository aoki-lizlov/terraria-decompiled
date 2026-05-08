using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Policy;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000733 RID: 1843
	[ComVisible(true)]
	[CLSCompliant(false)]
	[InterfaceType(ComInterfaceType.InterfaceIsDual)]
	[Guid("17156360-2F1A-384A-BC52-FDE93C215C5B")]
	[TypeLibImportClass(typeof(Assembly))]
	public interface _Assembly
	{
		// Token: 0x0600423B RID: 16955
		string ToString();

		// Token: 0x0600423C RID: 16956
		bool Equals(object other);

		// Token: 0x0600423D RID: 16957
		int GetHashCode();

		// Token: 0x0600423E RID: 16958
		Type GetType();

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x0600423F RID: 16959
		string CodeBase { get; }

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x06004240 RID: 16960
		string EscapedCodeBase { get; }

		// Token: 0x06004241 RID: 16961
		AssemblyName GetName();

		// Token: 0x06004242 RID: 16962
		AssemblyName GetName(bool copiedName);

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x06004243 RID: 16963
		string FullName { get; }

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x06004244 RID: 16964
		MethodInfo EntryPoint { get; }

		// Token: 0x06004245 RID: 16965
		Type GetType(string name);

		// Token: 0x06004246 RID: 16966
		Type GetType(string name, bool throwOnError);

		// Token: 0x06004247 RID: 16967
		Type[] GetExportedTypes();

		// Token: 0x06004248 RID: 16968
		Type[] GetTypes();

		// Token: 0x06004249 RID: 16969
		Stream GetManifestResourceStream(Type type, string name);

		// Token: 0x0600424A RID: 16970
		Stream GetManifestResourceStream(string name);

		// Token: 0x0600424B RID: 16971
		FileStream GetFile(string name);

		// Token: 0x0600424C RID: 16972
		FileStream[] GetFiles();

		// Token: 0x0600424D RID: 16973
		FileStream[] GetFiles(bool getResourceModules);

		// Token: 0x0600424E RID: 16974
		string[] GetManifestResourceNames();

		// Token: 0x0600424F RID: 16975
		ManifestResourceInfo GetManifestResourceInfo(string resourceName);

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x06004250 RID: 16976
		string Location { get; }

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x06004251 RID: 16977
		Evidence Evidence { get; }

		// Token: 0x06004252 RID: 16978
		object[] GetCustomAttributes(Type attributeType, bool inherit);

		// Token: 0x06004253 RID: 16979
		object[] GetCustomAttributes(bool inherit);

		// Token: 0x06004254 RID: 16980
		bool IsDefined(Type attributeType, bool inherit);

		// Token: 0x06004255 RID: 16981
		void GetObjectData(SerializationInfo info, StreamingContext context);

		// Token: 0x06004256 RID: 16982
		Type GetType(string name, bool throwOnError, bool ignoreCase);

		// Token: 0x06004257 RID: 16983
		Assembly GetSatelliteAssembly(CultureInfo culture);

		// Token: 0x06004258 RID: 16984
		Assembly GetSatelliteAssembly(CultureInfo culture, Version version);

		// Token: 0x06004259 RID: 16985
		Module LoadModule(string moduleName, byte[] rawModule);

		// Token: 0x0600425A RID: 16986
		Module LoadModule(string moduleName, byte[] rawModule, byte[] rawSymbolStore);

		// Token: 0x0600425B RID: 16987
		object CreateInstance(string typeName);

		// Token: 0x0600425C RID: 16988
		object CreateInstance(string typeName, bool ignoreCase);

		// Token: 0x0600425D RID: 16989
		object CreateInstance(string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes);

		// Token: 0x0600425E RID: 16990
		Module[] GetLoadedModules();

		// Token: 0x0600425F RID: 16991
		Module[] GetLoadedModules(bool getResourceModules);

		// Token: 0x06004260 RID: 16992
		Module[] GetModules();

		// Token: 0x06004261 RID: 16993
		Module[] GetModules(bool getResourceModules);

		// Token: 0x06004262 RID: 16994
		Module GetModule(string name);

		// Token: 0x06004263 RID: 16995
		AssemblyName[] GetReferencedAssemblies();

		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x06004264 RID: 16996
		bool GlobalAssemblyCache { get; }

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06004265 RID: 16997
		// (remove) Token: 0x06004266 RID: 16998
		event ModuleResolveEventHandler ModuleResolve;
	}
}
