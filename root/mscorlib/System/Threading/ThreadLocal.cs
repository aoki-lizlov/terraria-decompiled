using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Permissions;

namespace System.Threading
{
	// Token: 0x0200028E RID: 654
	[DebuggerTypeProxy(typeof(SystemThreading_ThreadLocalDebugView<>))]
	[DebuggerDisplay("IsValueCreated={IsValueCreated}, Value={ValueForDebugDisplay}, Count={ValuesCountForDebugDisplay}")]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class ThreadLocal<T> : IDisposable
	{
		// Token: 0x06001E60 RID: 7776 RVA: 0x0007209B File Offset: 0x0007029B
		public ThreadLocal()
		{
			this.Initialize(null, false);
		}

		// Token: 0x06001E61 RID: 7777 RVA: 0x000720B7 File Offset: 0x000702B7
		public ThreadLocal(bool trackAllValues)
		{
			this.Initialize(null, trackAllValues);
		}

		// Token: 0x06001E62 RID: 7778 RVA: 0x000720D3 File Offset: 0x000702D3
		public ThreadLocal(Func<T> valueFactory)
		{
			if (valueFactory == null)
			{
				throw new ArgumentNullException("valueFactory");
			}
			this.Initialize(valueFactory, false);
		}

		// Token: 0x06001E63 RID: 7779 RVA: 0x000720FD File Offset: 0x000702FD
		public ThreadLocal(Func<T> valueFactory, bool trackAllValues)
		{
			if (valueFactory == null)
			{
				throw new ArgumentNullException("valueFactory");
			}
			this.Initialize(valueFactory, trackAllValues);
		}

		// Token: 0x06001E64 RID: 7780 RVA: 0x00072128 File Offset: 0x00070328
		private void Initialize(Func<T> valueFactory, bool trackAllValues)
		{
			this.m_valueFactory = valueFactory;
			this.m_trackAllValues = trackAllValues;
			try
			{
			}
			finally
			{
				this.m_idComplement = ~ThreadLocal<T>.s_idManager.GetId();
				this.m_initialized = true;
			}
		}

		// Token: 0x06001E65 RID: 7781 RVA: 0x00072170 File Offset: 0x00070370
		~ThreadLocal()
		{
			this.Dispose(false);
		}

		// Token: 0x06001E66 RID: 7782 RVA: 0x000721A0 File Offset: 0x000703A0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001E67 RID: 7783 RVA: 0x000721B0 File Offset: 0x000703B0
		protected virtual void Dispose(bool disposing)
		{
			ThreadLocal<T>.IdManager idManager = ThreadLocal<T>.s_idManager;
			int num;
			lock (idManager)
			{
				num = ~this.m_idComplement;
				this.m_idComplement = 0;
				if (num < 0 || !this.m_initialized)
				{
					return;
				}
				this.m_initialized = false;
				for (ThreadLocal<T>.LinkedSlot linkedSlot = this.m_linkedSlot.Next; linkedSlot != null; linkedSlot = linkedSlot.Next)
				{
					ThreadLocal<T>.LinkedSlotVolatile[] slotArray = linkedSlot.SlotArray;
					if (slotArray != null)
					{
						linkedSlot.SlotArray = null;
						slotArray[num].Value.Value = default(T);
						slotArray[num].Value = null;
					}
				}
			}
			this.m_linkedSlot = null;
			ThreadLocal<T>.s_idManager.ReturnId(num);
		}

		// Token: 0x06001E68 RID: 7784 RVA: 0x00072284 File Offset: 0x00070484
		public override string ToString()
		{
			T value = this.Value;
			return value.ToString();
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06001E69 RID: 7785 RVA: 0x000722A8 File Offset: 0x000704A8
		// (set) Token: 0x06001E6A RID: 7786 RVA: 0x000722FC File Offset: 0x000704FC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public T Value
		{
			get
			{
				ThreadLocal<T>.LinkedSlotVolatile[] array = ThreadLocal<T>.ts_slotArray;
				int num = ~this.m_idComplement;
				ThreadLocal<T>.LinkedSlot value;
				if (array != null && num >= 0 && num < array.Length && (value = array[num].Value) != null && this.m_initialized)
				{
					return value.Value;
				}
				return this.GetValueSlow();
			}
			set
			{
				ThreadLocal<T>.LinkedSlotVolatile[] array = ThreadLocal<T>.ts_slotArray;
				int num = ~this.m_idComplement;
				ThreadLocal<T>.LinkedSlot value2;
				if (array != null && num >= 0 && num < array.Length && (value2 = array[num].Value) != null && this.m_initialized)
				{
					value2.Value = value;
					return;
				}
				this.SetValueSlow(value, array);
			}
		}

		// Token: 0x06001E6B RID: 7787 RVA: 0x00072350 File Offset: 0x00070550
		private T GetValueSlow()
		{
			if (~this.m_idComplement < 0)
			{
				throw new ObjectDisposedException(Environment.GetResourceString("The ThreadLocal object has been disposed."));
			}
			Debugger.NotifyOfCrossThreadDependency();
			T t;
			if (this.m_valueFactory == null)
			{
				t = default(T);
			}
			else
			{
				t = this.m_valueFactory();
				if (this.IsValueCreated)
				{
					throw new InvalidOperationException(Environment.GetResourceString("ValueFactory attempted to access the Value property of this instance."));
				}
			}
			this.Value = t;
			return t;
		}

		// Token: 0x06001E6C RID: 7788 RVA: 0x000723BC File Offset: 0x000705BC
		private void SetValueSlow(T value, ThreadLocal<T>.LinkedSlotVolatile[] slotArray)
		{
			int num = ~this.m_idComplement;
			if (num < 0)
			{
				throw new ObjectDisposedException(Environment.GetResourceString("The ThreadLocal object has been disposed."));
			}
			if (slotArray == null)
			{
				slotArray = new ThreadLocal<T>.LinkedSlotVolatile[ThreadLocal<T>.GetNewTableSize(num + 1)];
				ThreadLocal<T>.ts_finalizationHelper = new ThreadLocal<T>.FinalizationHelper(slotArray, this.m_trackAllValues);
				ThreadLocal<T>.ts_slotArray = slotArray;
			}
			if (num >= slotArray.Length)
			{
				this.GrowTable(ref slotArray, num + 1);
				ThreadLocal<T>.ts_finalizationHelper.SlotArray = slotArray;
				ThreadLocal<T>.ts_slotArray = slotArray;
			}
			if (slotArray[num].Value == null)
			{
				this.CreateLinkedSlot(slotArray, num, value);
				return;
			}
			ThreadLocal<T>.LinkedSlot value2 = slotArray[num].Value;
			if (!this.m_initialized)
			{
				throw new ObjectDisposedException(Environment.GetResourceString("The ThreadLocal object has been disposed."));
			}
			value2.Value = value;
		}

		// Token: 0x06001E6D RID: 7789 RVA: 0x00072478 File Offset: 0x00070678
		private void CreateLinkedSlot(ThreadLocal<T>.LinkedSlotVolatile[] slotArray, int id, T value)
		{
			ThreadLocal<T>.LinkedSlot linkedSlot = new ThreadLocal<T>.LinkedSlot(slotArray);
			ThreadLocal<T>.IdManager idManager = ThreadLocal<T>.s_idManager;
			lock (idManager)
			{
				if (!this.m_initialized)
				{
					throw new ObjectDisposedException(Environment.GetResourceString("The ThreadLocal object has been disposed."));
				}
				ThreadLocal<T>.LinkedSlot next = this.m_linkedSlot.Next;
				linkedSlot.Next = next;
				linkedSlot.Previous = this.m_linkedSlot;
				linkedSlot.Value = value;
				if (next != null)
				{
					next.Previous = linkedSlot;
				}
				this.m_linkedSlot.Next = linkedSlot;
				slotArray[id].Value = linkedSlot;
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06001E6E RID: 7790 RVA: 0x00072528 File Offset: 0x00070728
		public IList<T> Values
		{
			get
			{
				if (!this.m_trackAllValues)
				{
					throw new InvalidOperationException(Environment.GetResourceString("The ThreadLocal object is not tracking values. To use the Values property, use a ThreadLocal constructor that accepts the trackAllValues parameter and set the parameter to true."));
				}
				List<T> valuesAsList = this.GetValuesAsList();
				if (valuesAsList == null)
				{
					throw new ObjectDisposedException(Environment.GetResourceString("The ThreadLocal object has been disposed."));
				}
				return valuesAsList;
			}
		}

		// Token: 0x06001E6F RID: 7791 RVA: 0x0007255C File Offset: 0x0007075C
		private List<T> GetValuesAsList()
		{
			List<T> list = new List<T>();
			if (~this.m_idComplement == -1)
			{
				return null;
			}
			for (ThreadLocal<T>.LinkedSlot linkedSlot = this.m_linkedSlot.Next; linkedSlot != null; linkedSlot = linkedSlot.Next)
			{
				list.Add(linkedSlot.Value);
			}
			return list;
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06001E70 RID: 7792 RVA: 0x000725A4 File Offset: 0x000707A4
		private int ValuesCountForDebugDisplay
		{
			get
			{
				int num = 0;
				for (ThreadLocal<T>.LinkedSlot linkedSlot = this.m_linkedSlot.Next; linkedSlot != null; linkedSlot = linkedSlot.Next)
				{
					num++;
				}
				return num;
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06001E71 RID: 7793 RVA: 0x000725D4 File Offset: 0x000707D4
		public bool IsValueCreated
		{
			get
			{
				int num = ~this.m_idComplement;
				if (num < 0)
				{
					throw new ObjectDisposedException(Environment.GetResourceString("The ThreadLocal object has been disposed."));
				}
				ThreadLocal<T>.LinkedSlotVolatile[] array = ThreadLocal<T>.ts_slotArray;
				return array != null && num < array.Length && array[num].Value != null;
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06001E72 RID: 7794 RVA: 0x00072620 File Offset: 0x00070820
		internal T ValueForDebugDisplay
		{
			get
			{
				ThreadLocal<T>.LinkedSlotVolatile[] array = ThreadLocal<T>.ts_slotArray;
				int num = ~this.m_idComplement;
				ThreadLocal<T>.LinkedSlot value;
				if (array == null || num >= array.Length || (value = array[num].Value) == null || !this.m_initialized)
				{
					return default(T);
				}
				return value.Value;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06001E73 RID: 7795 RVA: 0x00072670 File Offset: 0x00070870
		internal List<T> ValuesForDebugDisplay
		{
			get
			{
				return this.GetValuesAsList();
			}
		}

		// Token: 0x06001E74 RID: 7796 RVA: 0x00072678 File Offset: 0x00070878
		private void GrowTable(ref ThreadLocal<T>.LinkedSlotVolatile[] table, int minLength)
		{
			ThreadLocal<T>.LinkedSlotVolatile[] array = new ThreadLocal<T>.LinkedSlotVolatile[ThreadLocal<T>.GetNewTableSize(minLength)];
			ThreadLocal<T>.IdManager idManager = ThreadLocal<T>.s_idManager;
			lock (idManager)
			{
				for (int i = 0; i < table.Length; i++)
				{
					ThreadLocal<T>.LinkedSlot value = table[i].Value;
					if (value != null && value.SlotArray != null)
					{
						value.SlotArray = array;
						array[i] = table[i];
					}
				}
			}
			table = array;
		}

		// Token: 0x06001E75 RID: 7797 RVA: 0x00072708 File Offset: 0x00070908
		private static int GetNewTableSize(int minSize)
		{
			if (minSize > 2146435071)
			{
				return int.MaxValue;
			}
			int num = minSize - 1;
			num |= num >> 1;
			num |= num >> 2;
			num |= num >> 4;
			num |= num >> 8;
			num |= num >> 16;
			num++;
			if (num > 2146435071)
			{
				num = 2146435071;
			}
			return num;
		}

		// Token: 0x06001E76 RID: 7798 RVA: 0x0007275B File Offset: 0x0007095B
		// Note: this type is marked as 'beforefieldinit'.
		static ThreadLocal()
		{
		}

		// Token: 0x040019A7 RID: 6567
		private Func<T> m_valueFactory;

		// Token: 0x040019A8 RID: 6568
		[ThreadStatic]
		private static ThreadLocal<T>.LinkedSlotVolatile[] ts_slotArray;

		// Token: 0x040019A9 RID: 6569
		[ThreadStatic]
		private static ThreadLocal<T>.FinalizationHelper ts_finalizationHelper;

		// Token: 0x040019AA RID: 6570
		private int m_idComplement;

		// Token: 0x040019AB RID: 6571
		private volatile bool m_initialized;

		// Token: 0x040019AC RID: 6572
		private static ThreadLocal<T>.IdManager s_idManager = new ThreadLocal<T>.IdManager();

		// Token: 0x040019AD RID: 6573
		private ThreadLocal<T>.LinkedSlot m_linkedSlot = new ThreadLocal<T>.LinkedSlot(null);

		// Token: 0x040019AE RID: 6574
		private bool m_trackAllValues;

		// Token: 0x0200028F RID: 655
		private struct LinkedSlotVolatile
		{
			// Token: 0x040019AF RID: 6575
			internal volatile ThreadLocal<T>.LinkedSlot Value;
		}

		// Token: 0x02000290 RID: 656
		private sealed class LinkedSlot
		{
			// Token: 0x06001E77 RID: 7799 RVA: 0x00072767 File Offset: 0x00070967
			internal LinkedSlot(ThreadLocal<T>.LinkedSlotVolatile[] slotArray)
			{
				this.SlotArray = slotArray;
			}

			// Token: 0x040019B0 RID: 6576
			internal volatile ThreadLocal<T>.LinkedSlot Next;

			// Token: 0x040019B1 RID: 6577
			internal volatile ThreadLocal<T>.LinkedSlot Previous;

			// Token: 0x040019B2 RID: 6578
			internal volatile ThreadLocal<T>.LinkedSlotVolatile[] SlotArray;

			// Token: 0x040019B3 RID: 6579
			internal T Value;
		}

		// Token: 0x02000291 RID: 657
		private class IdManager
		{
			// Token: 0x06001E78 RID: 7800 RVA: 0x00072778 File Offset: 0x00070978
			internal int GetId()
			{
				List<bool> freeIds = this.m_freeIds;
				int num2;
				lock (freeIds)
				{
					int num = this.m_nextIdToTry;
					while (num < this.m_freeIds.Count && !this.m_freeIds[num])
					{
						num++;
					}
					if (num == this.m_freeIds.Count)
					{
						this.m_freeIds.Add(false);
					}
					else
					{
						this.m_freeIds[num] = false;
					}
					this.m_nextIdToTry = num + 1;
					num2 = num;
				}
				return num2;
			}

			// Token: 0x06001E79 RID: 7801 RVA: 0x00072810 File Offset: 0x00070A10
			internal void ReturnId(int id)
			{
				List<bool> freeIds = this.m_freeIds;
				lock (freeIds)
				{
					this.m_freeIds[id] = true;
					if (id < this.m_nextIdToTry)
					{
						this.m_nextIdToTry = id;
					}
				}
			}

			// Token: 0x06001E7A RID: 7802 RVA: 0x00072868 File Offset: 0x00070A68
			public IdManager()
			{
			}

			// Token: 0x040019B4 RID: 6580
			private int m_nextIdToTry;

			// Token: 0x040019B5 RID: 6581
			private List<bool> m_freeIds = new List<bool>();
		}

		// Token: 0x02000292 RID: 658
		private class FinalizationHelper
		{
			// Token: 0x06001E7B RID: 7803 RVA: 0x0007287B File Offset: 0x00070A7B
			internal FinalizationHelper(ThreadLocal<T>.LinkedSlotVolatile[] slotArray, bool trackAllValues)
			{
				this.SlotArray = slotArray;
				this.m_trackAllValues = trackAllValues;
			}

			// Token: 0x06001E7C RID: 7804 RVA: 0x00072894 File Offset: 0x00070A94
			protected override void Finalize()
			{
				try
				{
					ThreadLocal<T>.LinkedSlotVolatile[] slotArray = this.SlotArray;
					for (int i = 0; i < slotArray.Length; i++)
					{
						ThreadLocal<T>.LinkedSlot value = slotArray[i].Value;
						if (value != null)
						{
							if (this.m_trackAllValues)
							{
								value.SlotArray = null;
							}
							else
							{
								ThreadLocal<T>.IdManager s_idManager = ThreadLocal<T>.s_idManager;
								lock (s_idManager)
								{
									if (value.Next != null)
									{
										value.Next.Previous = value.Previous;
									}
									value.Previous.Next = value.Next;
								}
							}
						}
					}
				}
				finally
				{
					base.Finalize();
				}
			}

			// Token: 0x040019B6 RID: 6582
			internal ThreadLocal<T>.LinkedSlotVolatile[] SlotArray;

			// Token: 0x040019B7 RID: 6583
			private bool m_trackAllValues;
		}
	}
}
