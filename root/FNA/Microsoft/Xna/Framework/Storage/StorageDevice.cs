using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.Xna.Framework.Storage
{
	// Token: 0x0200003D RID: 61
	public sealed class StorageDevice
	{
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000E63 RID: 3683 RVA: 0x0001F37C File Offset: 0x0001D57C
		public long FreeSpace
		{
			get
			{
				long num;
				try
				{
					if (StorageDevice.drive == null)
					{
						num = long.MaxValue;
					}
					else
					{
						num = StorageDevice.drive.AvailableFreeSpace;
					}
				}
				catch (Exception ex)
				{
					throw new StorageDeviceNotConnectedException("The storage device bound to the container is not connected.", ex);
				}
				return num;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000E64 RID: 3684 RVA: 0x0001F3C8 File Offset: 0x0001D5C8
		public bool IsConnected
		{
			get
			{
				bool flag;
				try
				{
					if (StorageDevice.drive == null)
					{
						flag = true;
					}
					else
					{
						flag = StorageDevice.drive.IsReady;
					}
				}
				catch
				{
					flag = false;
				}
				return flag;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000E65 RID: 3685 RVA: 0x0001F404 File Offset: 0x0001D604
		public long TotalSpace
		{
			get
			{
				long num;
				try
				{
					if (StorageDevice.drive == null)
					{
						num = long.MaxValue;
					}
					else
					{
						num = StorageDevice.drive.TotalSize;
					}
				}
				catch (Exception ex)
				{
					throw new StorageDeviceNotConnectedException("The storage device bound to the container is not connected.", ex);
				}
				return num;
			}
		}

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x06000E66 RID: 3686 RVA: 0x0001F450 File Offset: 0x0001D650
		// (remove) Token: 0x06000E67 RID: 3687 RVA: 0x0001F484 File Offset: 0x0001D684
		public static event EventHandler<EventArgs> DeviceChanged
		{
			[CompilerGenerated]
			add
			{
				EventHandler<EventArgs> eventHandler = StorageDevice.DeviceChanged;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref StorageDevice.DeviceChanged, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<EventArgs> eventHandler = StorageDevice.DeviceChanged;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref StorageDevice.DeviceChanged, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x0001F4B7 File Offset: 0x0001D6B7
		private void OnDeviceChanged()
		{
			if (StorageDevice.DeviceChanged != null)
			{
				StorageDevice.DeviceChanged(this, null);
			}
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x0001F4CC File Offset: 0x0001D6CC
		internal StorageDevice(PlayerIndex? player)
		{
			this.devicePlayer = player;
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x0001F4DC File Offset: 0x0001D6DC
		public IAsyncResult BeginOpenContainer(string displayName, AsyncCallback callback, object state)
		{
			IAsyncResult asyncResult = new StorageDevice.OpenContainerLie(state, displayName);
			if (callback != null)
			{
				callback(asyncResult);
			}
			return asyncResult;
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x0001F4FC File Offset: 0x0001D6FC
		public StorageContainer EndOpenContainer(IAsyncResult result)
		{
			return new StorageContainer(this, (result as StorageDevice.OpenContainerLie).DisplayName, StorageDevice.storageRoot, this.devicePlayer);
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x0001F51A File Offset: 0x0001D71A
		public static IAsyncResult BeginShowSelector(AsyncCallback callback, object state)
		{
			return StorageDevice.BeginShowSelector(0, 0, callback, state);
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x0001F525 File Offset: 0x0001D725
		public static IAsyncResult BeginShowSelector(PlayerIndex player, AsyncCallback callback, object state)
		{
			return StorageDevice.BeginShowSelector(player, 0, 0, callback, state);
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x0001F534 File Offset: 0x0001D734
		public static IAsyncResult BeginShowSelector(int sizeInBytes, int directoryCount, AsyncCallback callback, object state)
		{
			IAsyncResult asyncResult = new StorageDevice.ShowSelectorLie(state, null);
			if (callback != null)
			{
				callback(asyncResult);
			}
			return asyncResult;
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x0001F55C File Offset: 0x0001D75C
		public static IAsyncResult BeginShowSelector(PlayerIndex player, int sizeInBytes, int directoryCount, AsyncCallback callback, object state)
		{
			IAsyncResult asyncResult = new StorageDevice.ShowSelectorLie(state, new PlayerIndex?(player));
			if (callback != null)
			{
				callback(asyncResult);
			}
			return asyncResult;
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x0001F582 File Offset: 0x0001D782
		public static StorageDevice EndShowSelector(IAsyncResult result)
		{
			return new StorageDevice((result as StorageDevice.ShowSelectorLie).PlayerIndex);
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x000136EE File Offset: 0x000118EE
		public void DeleteContainer(string titleName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x0001F594 File Offset: 0x0001D794
		// Note: this type is marked as 'beforefieldinit'.
		static StorageDevice()
		{
		}

		// Token: 0x040005E0 RID: 1504
		private PlayerIndex? devicePlayer;

		// Token: 0x040005E1 RID: 1505
		private static readonly string storageRoot = FNAPlatform.GetStorageRoot();

		// Token: 0x040005E2 RID: 1506
		private static readonly DriveInfo drive = FNAPlatform.GetDriveInfo(StorageDevice.storageRoot);

		// Token: 0x040005E3 RID: 1507
		[CompilerGenerated]
		private static EventHandler<EventArgs> DeviceChanged;

		// Token: 0x0200039C RID: 924
		private class NotAsyncLie : IAsyncResult
		{
			// Token: 0x170003A3 RID: 931
			// (get) Token: 0x06001AAB RID: 6827 RVA: 0x0003F6B3 File Offset: 0x0003D8B3
			// (set) Token: 0x06001AAC RID: 6828 RVA: 0x0003F6BB File Offset: 0x0003D8BB
			public object AsyncState
			{
				[CompilerGenerated]
				get
				{
					return this.<AsyncState>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<AsyncState>k__BackingField = value;
				}
			}

			// Token: 0x170003A4 RID: 932
			// (get) Token: 0x06001AAD RID: 6829 RVA: 0x0001F5E1 File Offset: 0x0001D7E1
			public bool CompletedSynchronously
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170003A5 RID: 933
			// (get) Token: 0x06001AAE RID: 6830 RVA: 0x0001F5E1 File Offset: 0x0001D7E1
			public bool IsCompleted
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170003A6 RID: 934
			// (get) Token: 0x06001AAF RID: 6831 RVA: 0x0003F6C4 File Offset: 0x0003D8C4
			// (set) Token: 0x06001AB0 RID: 6832 RVA: 0x0003F6CC File Offset: 0x0003D8CC
			public WaitHandle AsyncWaitHandle
			{
				[CompilerGenerated]
				get
				{
					return this.<AsyncWaitHandle>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<AsyncWaitHandle>k__BackingField = value;
				}
			}

			// Token: 0x06001AB1 RID: 6833 RVA: 0x0003F6D5 File Offset: 0x0003D8D5
			public NotAsyncLie(object state)
			{
				this.AsyncState = state;
				this.AsyncWaitHandle = new ManualResetEvent(true);
			}

			// Token: 0x04001C39 RID: 7225
			[CompilerGenerated]
			private object <AsyncState>k__BackingField;

			// Token: 0x04001C3A RID: 7226
			[CompilerGenerated]
			private WaitHandle <AsyncWaitHandle>k__BackingField;
		}

		// Token: 0x0200039D RID: 925
		private class ShowSelectorLie : StorageDevice.NotAsyncLie
		{
			// Token: 0x06001AB2 RID: 6834 RVA: 0x0003F6F0 File Offset: 0x0003D8F0
			public ShowSelectorLie(object state, PlayerIndex? playerIndex)
				: base(state)
			{
				this.PlayerIndex = playerIndex;
			}

			// Token: 0x04001C3B RID: 7227
			public readonly PlayerIndex? PlayerIndex;
		}

		// Token: 0x0200039E RID: 926
		private class OpenContainerLie : StorageDevice.NotAsyncLie
		{
			// Token: 0x06001AB3 RID: 6835 RVA: 0x0003F700 File Offset: 0x0003D900
			public OpenContainerLie(object state, string displayName)
				: base(state)
			{
				this.DisplayName = displayName;
			}

			// Token: 0x04001C3C RID: 7228
			public readonly string DisplayName;
		}
	}
}
