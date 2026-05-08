using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Mono
{
	// Token: 0x02000024 RID: 36
	internal static class DependencyInjector
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00003E90 File Offset: 0x00002090
		internal static ISystemDependencyProvider SystemProvider
		{
			get
			{
				if (DependencyInjector.systemDependency != null)
				{
					return DependencyInjector.systemDependency;
				}
				object obj = DependencyInjector.locker;
				ISystemDependencyProvider systemDependencyProvider;
				lock (obj)
				{
					if (DependencyInjector.systemDependency != null)
					{
						systemDependencyProvider = DependencyInjector.systemDependency;
					}
					else
					{
						DependencyInjector.systemDependency = DependencyInjector.ReflectionLoad();
						if (DependencyInjector.systemDependency == null)
						{
							throw new PlatformNotSupportedException("Cannot find 'Mono.SystemDependencyProvider, System' dependency");
						}
						systemDependencyProvider = DependencyInjector.systemDependency;
					}
				}
				return systemDependencyProvider;
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003F08 File Offset: 0x00002108
		internal static void Register(ISystemDependencyProvider provider)
		{
			object obj = DependencyInjector.locker;
			lock (obj)
			{
				if (DependencyInjector.systemDependency != null && DependencyInjector.systemDependency != provider)
				{
					throw new InvalidOperationException();
				}
				DependencyInjector.systemDependency = provider;
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003F5C File Offset: 0x0000215C
		[PreserveDependency("get_Instance()", "Mono.SystemDependencyProvider", "System")]
		private static ISystemDependencyProvider ReflectionLoad()
		{
			Type type = Type.GetType("Mono.SystemDependencyProvider, System");
			if (type == null)
			{
				return null;
			}
			PropertyInfo property = type.GetProperty("Instance", BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public);
			if (property == null)
			{
				return null;
			}
			return (ISystemDependencyProvider)property.GetValue(null);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003FA4 File Offset: 0x000021A4
		// Note: this type is marked as 'beforefieldinit'.
		static DependencyInjector()
		{
		}

		// Token: 0x04000CCD RID: 3277
		private const string TypeName = "Mono.SystemDependencyProvider, System";

		// Token: 0x04000CCE RID: 3278
		private static object locker = new object();

		// Token: 0x04000CCF RID: 3279
		private static ISystemDependencyProvider systemDependency;
	}
}
