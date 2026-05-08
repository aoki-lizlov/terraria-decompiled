using System;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;

namespace System
{
	// Token: 0x020001BD RID: 445
	internal sealed class LocalDataStore
	{
		// Token: 0x060014B8 RID: 5304 RVA: 0x000528C8 File Offset: 0x00050AC8
		public LocalDataStore(LocalDataStoreMgr mgr, int InitialCapacity)
		{
			this.m_Manager = mgr;
			this.m_DataTable = new LocalDataStoreElement[InitialCapacity];
		}

		// Token: 0x060014B9 RID: 5305 RVA: 0x000528E3 File Offset: 0x00050AE3
		internal void Dispose()
		{
			this.m_Manager.DeleteLocalDataStore(this);
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x000528F4 File Offset: 0x00050AF4
		public object GetData(LocalDataStoreSlot slot)
		{
			this.m_Manager.ValidateSlot(slot);
			int slot2 = slot.Slot;
			if (slot2 >= 0)
			{
				if (slot2 >= this.m_DataTable.Length)
				{
					return null;
				}
				LocalDataStoreElement localDataStoreElement = this.m_DataTable[slot2];
				if (localDataStoreElement == null)
				{
					return null;
				}
				if (localDataStoreElement.Cookie == slot.Cookie)
				{
					return localDataStoreElement.Value;
				}
			}
			throw new InvalidOperationException(Environment.GetResourceString("LocalDataStoreSlot storage has been freed."));
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x00052958 File Offset: 0x00050B58
		public void SetData(LocalDataStoreSlot slot, object data)
		{
			this.m_Manager.ValidateSlot(slot);
			int slot2 = slot.Slot;
			if (slot2 >= 0)
			{
				LocalDataStoreElement localDataStoreElement = ((slot2 < this.m_DataTable.Length) ? this.m_DataTable[slot2] : null);
				if (localDataStoreElement == null)
				{
					localDataStoreElement = this.PopulateElement(slot);
				}
				if (localDataStoreElement.Cookie == slot.Cookie)
				{
					localDataStoreElement.Value = data;
					return;
				}
			}
			throw new InvalidOperationException(Environment.GetResourceString("LocalDataStoreSlot storage has been freed."));
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x000529C4 File Offset: 0x00050BC4
		internal void FreeData(int slot, long cookie)
		{
			if (slot >= this.m_DataTable.Length)
			{
				return;
			}
			LocalDataStoreElement localDataStoreElement = this.m_DataTable[slot];
			if (localDataStoreElement != null && localDataStoreElement.Cookie == cookie)
			{
				this.m_DataTable[slot] = null;
			}
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x000529FC File Offset: 0x00050BFC
		[SecuritySafeCritical]
		private LocalDataStoreElement PopulateElement(LocalDataStoreSlot slot)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			LocalDataStoreElement localDataStoreElement;
			try
			{
				Monitor.Enter(this.m_Manager, ref flag);
				int slot2 = slot.Slot;
				if (slot2 < 0)
				{
					throw new InvalidOperationException(Environment.GetResourceString("LocalDataStoreSlot storage has been freed."));
				}
				if (slot2 >= this.m_DataTable.Length)
				{
					LocalDataStoreElement[] array = new LocalDataStoreElement[this.m_Manager.GetSlotTableLength()];
					Array.Copy(this.m_DataTable, array, this.m_DataTable.Length);
					this.m_DataTable = array;
				}
				if (this.m_DataTable[slot2] == null)
				{
					this.m_DataTable[slot2] = new LocalDataStoreElement(slot.Cookie);
				}
				localDataStoreElement = this.m_DataTable[slot2];
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this.m_Manager);
				}
			}
			return localDataStoreElement;
		}

		// Token: 0x040013CF RID: 5071
		private LocalDataStoreElement[] m_DataTable;

		// Token: 0x040013D0 RID: 5072
		private LocalDataStoreMgr m_Manager;
	}
}
