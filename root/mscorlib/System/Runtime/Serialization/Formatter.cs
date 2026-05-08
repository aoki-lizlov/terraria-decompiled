using System;
using System.Collections;
using System.Globalization;
using System.IO;

namespace System.Runtime.Serialization
{
	// Token: 0x02000621 RID: 1569
	[CLSCompliant(false)]
	[Serializable]
	public abstract class Formatter : IFormatter
	{
		// Token: 0x06003C00 RID: 15360 RVA: 0x000D105A File Offset: 0x000CF25A
		protected Formatter()
		{
			this.m_objectQueue = new Queue();
			this.m_idGenerator = new ObjectIDGenerator();
		}

		// Token: 0x06003C01 RID: 15361
		public abstract object Deserialize(Stream serializationStream);

		// Token: 0x06003C02 RID: 15362 RVA: 0x000D1078 File Offset: 0x000CF278
		protected virtual object GetNext(out long objID)
		{
			if (this.m_objectQueue.Count == 0)
			{
				objID = 0L;
				return null;
			}
			object obj = this.m_objectQueue.Dequeue();
			bool flag;
			objID = this.m_idGenerator.HasId(obj, out flag);
			if (flag)
			{
				throw new SerializationException("Object has never been assigned an objectID");
			}
			return obj;
		}

		// Token: 0x06003C03 RID: 15363 RVA: 0x000D10C4 File Offset: 0x000CF2C4
		protected virtual long Schedule(object obj)
		{
			if (obj == null)
			{
				return 0L;
			}
			bool flag;
			long id = this.m_idGenerator.GetId(obj, out flag);
			if (flag)
			{
				this.m_objectQueue.Enqueue(obj);
			}
			return id;
		}

		// Token: 0x06003C04 RID: 15364
		public abstract void Serialize(Stream serializationStream, object graph);

		// Token: 0x06003C05 RID: 15365
		protected abstract void WriteArray(object obj, string name, Type memberType);

		// Token: 0x06003C06 RID: 15366
		protected abstract void WriteBoolean(bool val, string name);

		// Token: 0x06003C07 RID: 15367
		protected abstract void WriteByte(byte val, string name);

		// Token: 0x06003C08 RID: 15368
		protected abstract void WriteChar(char val, string name);

		// Token: 0x06003C09 RID: 15369
		protected abstract void WriteDateTime(DateTime val, string name);

		// Token: 0x06003C0A RID: 15370
		protected abstract void WriteDecimal(decimal val, string name);

		// Token: 0x06003C0B RID: 15371
		protected abstract void WriteDouble(double val, string name);

		// Token: 0x06003C0C RID: 15372
		protected abstract void WriteInt16(short val, string name);

		// Token: 0x06003C0D RID: 15373
		protected abstract void WriteInt32(int val, string name);

		// Token: 0x06003C0E RID: 15374
		protected abstract void WriteInt64(long val, string name);

		// Token: 0x06003C0F RID: 15375
		protected abstract void WriteObjectRef(object obj, string name, Type memberType);

		// Token: 0x06003C10 RID: 15376 RVA: 0x000D10F4 File Offset: 0x000CF2F4
		protected virtual void WriteMember(string memberName, object data)
		{
			if (data == null)
			{
				this.WriteObjectRef(data, memberName, typeof(object));
				return;
			}
			Type type = data.GetType();
			if (type == typeof(bool))
			{
				this.WriteBoolean(Convert.ToBoolean(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(char))
			{
				this.WriteChar(Convert.ToChar(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(sbyte))
			{
				this.WriteSByte(Convert.ToSByte(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(byte))
			{
				this.WriteByte(Convert.ToByte(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(short))
			{
				this.WriteInt16(Convert.ToInt16(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(int))
			{
				this.WriteInt32(Convert.ToInt32(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(long))
			{
				this.WriteInt64(Convert.ToInt64(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(float))
			{
				this.WriteSingle(Convert.ToSingle(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(double))
			{
				this.WriteDouble(Convert.ToDouble(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(DateTime))
			{
				this.WriteDateTime(Convert.ToDateTime(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(decimal))
			{
				this.WriteDecimal(Convert.ToDecimal(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(ushort))
			{
				this.WriteUInt16(Convert.ToUInt16(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(uint))
			{
				this.WriteUInt32(Convert.ToUInt32(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(ulong))
			{
				this.WriteUInt64(Convert.ToUInt64(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type.IsArray)
			{
				this.WriteArray(data, memberName, type);
				return;
			}
			if (type.IsValueType)
			{
				this.WriteValueType(data, memberName, type);
				return;
			}
			this.WriteObjectRef(data, memberName, type);
		}

		// Token: 0x06003C11 RID: 15377
		[CLSCompliant(false)]
		protected abstract void WriteSByte(sbyte val, string name);

		// Token: 0x06003C12 RID: 15378
		protected abstract void WriteSingle(float val, string name);

		// Token: 0x06003C13 RID: 15379
		protected abstract void WriteTimeSpan(TimeSpan val, string name);

		// Token: 0x06003C14 RID: 15380
		[CLSCompliant(false)]
		protected abstract void WriteUInt16(ushort val, string name);

		// Token: 0x06003C15 RID: 15381
		[CLSCompliant(false)]
		protected abstract void WriteUInt32(uint val, string name);

		// Token: 0x06003C16 RID: 15382
		[CLSCompliant(false)]
		protected abstract void WriteUInt64(ulong val, string name);

		// Token: 0x06003C17 RID: 15383
		protected abstract void WriteValueType(object obj, string name, Type memberType);

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x06003C18 RID: 15384
		// (set) Token: 0x06003C19 RID: 15385
		public abstract ISurrogateSelector SurrogateSelector { get; set; }

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x06003C1A RID: 15386
		// (set) Token: 0x06003C1B RID: 15387
		public abstract SerializationBinder Binder { get; set; }

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x06003C1C RID: 15388
		// (set) Token: 0x06003C1D RID: 15389
		public abstract StreamingContext Context { get; set; }

		// Token: 0x040026A3 RID: 9891
		protected ObjectIDGenerator m_idGenerator;

		// Token: 0x040026A4 RID: 9892
		protected Queue m_objectQueue;
	}
}
