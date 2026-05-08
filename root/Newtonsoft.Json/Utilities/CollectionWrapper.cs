using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000062 RID: 98
	internal class CollectionWrapper<T> : ICollection<T>, IEnumerable<T>, IEnumerable, IWrappedCollection, IList, ICollection
	{
		// Token: 0x0600049E RID: 1182 RVA: 0x00013B78 File Offset: 0x00011D78
		public CollectionWrapper(IList list)
		{
			ValidationUtils.ArgumentNotNull(list, "list");
			ICollection<T> collection = list as ICollection<T>;
			if (collection != null)
			{
				this._genericCollection = collection;
				return;
			}
			this._list = list;
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00013BAF File Offset: 0x00011DAF
		public CollectionWrapper(ICollection<T> list)
		{
			ValidationUtils.ArgumentNotNull(list, "list");
			this._genericCollection = list;
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00013BC9 File Offset: 0x00011DC9
		public virtual void Add(T item)
		{
			if (this._genericCollection != null)
			{
				this._genericCollection.Add(item);
				return;
			}
			this._list.Add(item);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00013BF2 File Offset: 0x00011DF2
		public virtual void Clear()
		{
			if (this._genericCollection != null)
			{
				this._genericCollection.Clear();
				return;
			}
			this._list.Clear();
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00013C13 File Offset: 0x00011E13
		public virtual bool Contains(T item)
		{
			if (this._genericCollection != null)
			{
				return this._genericCollection.Contains(item);
			}
			return this._list.Contains(item);
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00013C3B File Offset: 0x00011E3B
		public virtual void CopyTo(T[] array, int arrayIndex)
		{
			if (this._genericCollection != null)
			{
				this._genericCollection.CopyTo(array, arrayIndex);
				return;
			}
			this._list.CopyTo(array, arrayIndex);
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x00013C60 File Offset: 0x00011E60
		public virtual int Count
		{
			get
			{
				if (this._genericCollection != null)
				{
					return this._genericCollection.Count;
				}
				return this._list.Count;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x00013C81 File Offset: 0x00011E81
		public virtual bool IsReadOnly
		{
			get
			{
				if (this._genericCollection != null)
				{
					return this._genericCollection.IsReadOnly;
				}
				return this._list.IsReadOnly;
			}
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00013CA2 File Offset: 0x00011EA2
		public virtual bool Remove(T item)
		{
			if (this._genericCollection != null)
			{
				return this._genericCollection.Remove(item);
			}
			bool flag = this._list.Contains(item);
			if (flag)
			{
				this._list.Remove(item);
			}
			return flag;
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00013CE0 File Offset: 0x00011EE0
		public virtual IEnumerator<T> GetEnumerator()
		{
			IEnumerable<T> genericCollection = this._genericCollection;
			return (genericCollection ?? Enumerable.Cast<T>(this._list)).GetEnumerator();
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00013D0C File Offset: 0x00011F0C
		IEnumerator IEnumerable.GetEnumerator()
		{
			IEnumerable genericCollection = this._genericCollection;
			return (genericCollection ?? this._list).GetEnumerator();
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x00013D30 File Offset: 0x00011F30
		int IList.Add(object value)
		{
			CollectionWrapper<T>.VerifyValueType(value);
			this.Add((T)((object)value));
			return this.Count - 1;
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00013D4C File Offset: 0x00011F4C
		bool IList.Contains(object value)
		{
			return CollectionWrapper<T>.IsCompatibleObject(value) && this.Contains((T)((object)value));
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00013D64 File Offset: 0x00011F64
		int IList.IndexOf(object value)
		{
			if (this._genericCollection != null)
			{
				throw new InvalidOperationException("Wrapped ICollection<T> does not support IndexOf.");
			}
			if (CollectionWrapper<T>.IsCompatibleObject(value))
			{
				return this._list.IndexOf((T)((object)value));
			}
			return -1;
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00013D99 File Offset: 0x00011F99
		void IList.RemoveAt(int index)
		{
			if (this._genericCollection != null)
			{
				throw new InvalidOperationException("Wrapped ICollection<T> does not support RemoveAt.");
			}
			this._list.RemoveAt(index);
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00013DBA File Offset: 0x00011FBA
		void IList.Insert(int index, object value)
		{
			if (this._genericCollection != null)
			{
				throw new InvalidOperationException("Wrapped ICollection<T> does not support Insert.");
			}
			CollectionWrapper<T>.VerifyValueType(value);
			this._list.Insert(index, (T)((object)value));
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x00013DEC File Offset: 0x00011FEC
		bool IList.IsFixedSize
		{
			get
			{
				if (this._genericCollection != null)
				{
					return this._genericCollection.IsReadOnly;
				}
				return this._list.IsFixedSize;
			}
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00013E0D File Offset: 0x0001200D
		void IList.Remove(object value)
		{
			if (CollectionWrapper<T>.IsCompatibleObject(value))
			{
				this.Remove((T)((object)value));
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x00013E24 File Offset: 0x00012024
		// (set) Token: 0x060004B1 RID: 1201 RVA: 0x00013E45 File Offset: 0x00012045
		object IList.Item
		{
			get
			{
				if (this._genericCollection != null)
				{
					throw new InvalidOperationException("Wrapped ICollection<T> does not support indexer.");
				}
				return this._list[index];
			}
			set
			{
				if (this._genericCollection != null)
				{
					throw new InvalidOperationException("Wrapped ICollection<T> does not support indexer.");
				}
				CollectionWrapper<T>.VerifyValueType(value);
				this._list[index] = (T)((object)value);
			}
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00013E77 File Offset: 0x00012077
		void ICollection.CopyTo(Array array, int arrayIndex)
		{
			this.CopyTo((T[])array, arrayIndex);
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x0000F9CA File Offset: 0x0000DBCA
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x00013E86 File Offset: 0x00012086
		object ICollection.SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00013EA8 File Offset: 0x000120A8
		private static void VerifyValueType(object value)
		{
			if (!CollectionWrapper<T>.IsCompatibleObject(value))
			{
				throw new ArgumentException("The value '{0}' is not of type '{1}' and cannot be used in this generic collection.".FormatWith(CultureInfo.InvariantCulture, value, typeof(T)), "value");
			}
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00013ED7 File Offset: 0x000120D7
		private static bool IsCompatibleObject(object value)
		{
			return value is T || (value == null && (!typeof(T).IsValueType() || ReflectionUtils.IsNullableType(typeof(T))));
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x00013F09 File Offset: 0x00012109
		public object UnderlyingCollection
		{
			get
			{
				return this._genericCollection ?? this._list;
			}
		}

		// Token: 0x04000248 RID: 584
		private readonly IList _list;

		// Token: 0x04000249 RID: 585
		private readonly ICollection<T> _genericCollection;

		// Token: 0x0400024A RID: 586
		private object _syncRoot;
	}
}
