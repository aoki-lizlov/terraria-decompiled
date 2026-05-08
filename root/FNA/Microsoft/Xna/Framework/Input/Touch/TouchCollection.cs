using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Xna.Framework.Input.Touch
{
	// Token: 0x02000071 RID: 113
	public struct TouchCollection : IList<TouchLocation>, ICollection<TouchLocation>, IEnumerable<TouchLocation>, IEnumerable
	{
		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060010C5 RID: 4293 RVA: 0x00023C0D File Offset: 0x00021E0D
		public int Count
		{
			get
			{
				if (this.touches == null)
				{
					return 0;
				}
				return this.touches.Count;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060010C6 RID: 4294 RVA: 0x00023C24 File Offset: 0x00021E24
		public bool IsConnected
		{
			get
			{
				return TouchPanel.TouchDeviceExists;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060010C7 RID: 4295 RVA: 0x0001F5E1 File Offset: 0x0001D7E1
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001E0 RID: 480
		public TouchLocation this[int index]
		{
			get
			{
				if (this.touches == null)
				{
					throw new ArgumentOutOfRangeException();
				}
				return this.touches[index];
			}
			set
			{
				this.touches[index] = value;
			}
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x00023C56 File Offset: 0x00021E56
		public TouchCollection(TouchLocation[] touches)
		{
			this.touches = new List<TouchLocation>(touches);
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x00023C64 File Offset: 0x00021E64
		public void Add(TouchLocation item)
		{
			this.touches.Add(item);
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x00023C72 File Offset: 0x00021E72
		public void Clear()
		{
			this.touches.Clear();
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x00023C7F File Offset: 0x00021E7F
		public bool Contains(TouchLocation item)
		{
			return this.touches != null && this.touches.Contains(item);
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x00023C97 File Offset: 0x00021E97
		public void CopyTo(TouchLocation[] array, int arrayIndex)
		{
			if (this.touches == null)
			{
				return;
			}
			this.touches.CopyTo(array, arrayIndex);
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x00023CB0 File Offset: 0x00021EB0
		public bool FindById(int id, out TouchLocation touchLocation)
		{
			if (this.touches != null)
			{
				foreach (TouchLocation touchLocation2 in this.touches)
				{
					if (touchLocation2.Id == id)
					{
						touchLocation = touchLocation2;
						return true;
					}
				}
			}
			touchLocation = new TouchLocation(-1, TouchLocationState.Invalid, Vector2.Zero);
			return false;
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x00023D30 File Offset: 0x00021F30
		public TouchCollection.Enumerator GetEnumerator()
		{
			return new TouchCollection.Enumerator(this);
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x00023D3D File Offset: 0x00021F3D
		public int IndexOf(TouchLocation item)
		{
			if (this.touches == null)
			{
				return -1;
			}
			return this.touches.IndexOf(item);
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x00023D55 File Offset: 0x00021F55
		public void Insert(int index, TouchLocation item)
		{
			this.touches.Insert(index, item);
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x00023D64 File Offset: 0x00021F64
		public bool Remove(TouchLocation item)
		{
			return this.touches.Remove(item);
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x00023D72 File Offset: 0x00021F72
		public void RemoveAt(int index)
		{
			this.touches.RemoveAt(index);
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x00023D80 File Offset: 0x00021F80
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new TouchCollection.Enumerator(this);
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x00023D80 File Offset: 0x00021F80
		IEnumerator<TouchLocation> IEnumerable<TouchLocation>.GetEnumerator()
		{
			return new TouchCollection.Enumerator(this);
		}

		// Token: 0x04000792 RID: 1938
		private readonly List<TouchLocation> touches;

		// Token: 0x020003A2 RID: 930
		public struct Enumerator : IEnumerator<TouchLocation>, IDisposable, IEnumerator
		{
			// Token: 0x06001AB8 RID: 6840 RVA: 0x0003F72A File Offset: 0x0003D92A
			internal Enumerator(TouchCollection collection)
			{
				this.collection = collection;
				this.position = -1;
			}

			// Token: 0x170003A7 RID: 935
			// (get) Token: 0x06001AB9 RID: 6841 RVA: 0x0003F73A File Offset: 0x0003D93A
			public TouchLocation Current
			{
				get
				{
					return this.collection[this.position];
				}
			}

			// Token: 0x06001ABA RID: 6842 RVA: 0x0003F74D File Offset: 0x0003D94D
			public bool MoveNext()
			{
				this.position++;
				return this.position < this.collection.Count;
			}

			// Token: 0x06001ABB RID: 6843 RVA: 0x00009E6B File Offset: 0x0000806B
			public void Dispose()
			{
			}

			// Token: 0x170003A8 RID: 936
			// (get) Token: 0x06001ABC RID: 6844 RVA: 0x0003F770 File Offset: 0x0003D970
			object IEnumerator.Current
			{
				get
				{
					return this.collection[this.position];
				}
			}

			// Token: 0x06001ABD RID: 6845 RVA: 0x0003F788 File Offset: 0x0003D988
			void IEnumerator.Reset()
			{
				this.position = -1;
			}

			// Token: 0x04001C4A RID: 7242
			private TouchCollection collection;

			// Token: 0x04001C4B RID: 7243
			private int position;
		}
	}
}
