using System;
using ReLogic.OS.Linux;
using ReLogic.Utilities;

namespace ReLogic.OS
{
	// Token: 0x02000065 RID: 101
	public abstract class Platform : IDisposable
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000225 RID: 549 RVA: 0x000098B3 File Offset: 0x00007AB3
		public static bool IsWindows
		{
			get
			{
				return Platform.Current.Type == PlatformType.Windows;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000226 RID: 550 RVA: 0x000098C2 File Offset: 0x00007AC2
		public static bool IsOSX
		{
			get
			{
				return Platform.Current.Type == PlatformType.OSX;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000227 RID: 551 RVA: 0x000098D1 File Offset: 0x00007AD1
		public static bool IsLinux
		{
			get
			{
				return Platform.Current.Type == PlatformType.Linux;
			}
		}

		// Token: 0x06000228 RID: 552 RVA: 0x000098E0 File Offset: 0x00007AE0
		protected Platform(PlatformType type)
		{
			this.Type = type;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x000098FA File Offset: 0x00007AFA
		protected void RegisterService<T>(T service) where T : class
		{
			if (this._services.Has<T>())
			{
				this._services.Remove<T>();
			}
			this._services.Register<T>(service);
		}

		// Token: 0x0600022A RID: 554
		public abstract void InitializeClientServices(IntPtr windowHandle);

		// Token: 0x0600022B RID: 555 RVA: 0x00009920 File Offset: 0x00007B20
		public static T Get<T>() where T : class
		{
			return Platform.Current._services.Get<T>();
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00009931 File Offset: 0x00007B31
		public static bool Has<T>() where T : class
		{
			return Platform.Current._services.Has<T>();
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00009942 File Offset: 0x00007B42
		public static void IfHas<T>(Action<T> callback) where T : class
		{
			Platform.Current._services.IfHas<T>(callback);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00009954 File Offset: 0x00007B54
		public static U IfHas<T, U>(Func<T, U> callback) where T : class
		{
			return Platform.Current._services.IfHas<T, U>(callback);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00009966 File Offset: 0x00007B66
		protected virtual void Dispose(bool disposing)
		{
			if (!this._disposedValue)
			{
				if (disposing && this._services != null)
				{
					this._services.Dispose();
					this._services = null;
				}
				this._disposedValue = true;
			}
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00009994 File Offset: 0x00007B94
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000999D File Offset: 0x00007B9D
		// Note: this type is marked as 'beforefieldinit'.
		static Platform()
		{
		}

		// Token: 0x04000302 RID: 770
		public static readonly Platform Current = new LinuxPlatform();

		// Token: 0x04000303 RID: 771
		public readonly PlatformType Type;

		// Token: 0x04000304 RID: 772
		private TypeInstanceCollection<object> _services = new TypeInstanceCollection<object>();

		// Token: 0x04000305 RID: 773
		private bool _disposedValue;
	}
}
