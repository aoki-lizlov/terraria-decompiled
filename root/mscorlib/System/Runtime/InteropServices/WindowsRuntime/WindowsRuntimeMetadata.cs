using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x0200076A RID: 1898
	[MonoTODO]
	public static class WindowsRuntimeMetadata
	{
		// Token: 0x0600447F RID: 17535 RVA: 0x000174FB File Offset: 0x000156FB
		public static IEnumerable<string> ResolveNamespace(string namespaceName, IEnumerable<string> packageGraphFilePaths)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004480 RID: 17536 RVA: 0x000174FB File Offset: 0x000156FB
		public static IEnumerable<string> ResolveNamespace(string namespaceName, string windowsSdkFilePath, IEnumerable<string> packageGraphFilePaths)
		{
			throw new NotImplementedException();
		}

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06004481 RID: 17537 RVA: 0x000E49A4 File Offset: 0x000E2BA4
		// (remove) Token: 0x06004482 RID: 17538 RVA: 0x000E49D8 File Offset: 0x000E2BD8
		public static event EventHandler<DesignerNamespaceResolveEventArgs> DesignerNamespaceResolve
		{
			[CompilerGenerated]
			add
			{
				EventHandler<DesignerNamespaceResolveEventArgs> eventHandler = WindowsRuntimeMetadata.DesignerNamespaceResolve;
				EventHandler<DesignerNamespaceResolveEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<DesignerNamespaceResolveEventArgs> eventHandler3 = (EventHandler<DesignerNamespaceResolveEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<DesignerNamespaceResolveEventArgs>>(ref WindowsRuntimeMetadata.DesignerNamespaceResolve, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<DesignerNamespaceResolveEventArgs> eventHandler = WindowsRuntimeMetadata.DesignerNamespaceResolve;
				EventHandler<DesignerNamespaceResolveEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<DesignerNamespaceResolveEventArgs> eventHandler3 = (EventHandler<DesignerNamespaceResolveEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<DesignerNamespaceResolveEventArgs>>(ref WindowsRuntimeMetadata.DesignerNamespaceResolve, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x06004483 RID: 17539 RVA: 0x000E4A0C File Offset: 0x000E2C0C
		// (remove) Token: 0x06004484 RID: 17540 RVA: 0x000E4A40 File Offset: 0x000E2C40
		public static event EventHandler<NamespaceResolveEventArgs> ReflectionOnlyNamespaceResolve
		{
			[CompilerGenerated]
			add
			{
				EventHandler<NamespaceResolveEventArgs> eventHandler = WindowsRuntimeMetadata.ReflectionOnlyNamespaceResolve;
				EventHandler<NamespaceResolveEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<NamespaceResolveEventArgs> eventHandler3 = (EventHandler<NamespaceResolveEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<NamespaceResolveEventArgs>>(ref WindowsRuntimeMetadata.ReflectionOnlyNamespaceResolve, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<NamespaceResolveEventArgs> eventHandler = WindowsRuntimeMetadata.ReflectionOnlyNamespaceResolve;
				EventHandler<NamespaceResolveEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<NamespaceResolveEventArgs> eventHandler3 = (EventHandler<NamespaceResolveEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<NamespaceResolveEventArgs>>(ref WindowsRuntimeMetadata.ReflectionOnlyNamespaceResolve, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x04002BC2 RID: 11202
		[CompilerGenerated]
		private static EventHandler<DesignerNamespaceResolveEventArgs> DesignerNamespaceResolve;

		// Token: 0x04002BC3 RID: 11203
		[CompilerGenerated]
		private static EventHandler<NamespaceResolveEventArgs> ReflectionOnlyNamespaceResolve;
	}
}
