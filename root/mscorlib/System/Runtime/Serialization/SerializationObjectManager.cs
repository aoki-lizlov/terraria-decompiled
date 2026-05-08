using System;
using System.Collections.Generic;

namespace System.Runtime.Serialization
{
	// Token: 0x0200062A RID: 1578
	public sealed class SerializationObjectManager
	{
		// Token: 0x06003C4B RID: 15435 RVA: 0x000D1748 File Offset: 0x000CF948
		public SerializationObjectManager(StreamingContext context)
		{
			this._context = context;
			this._objectSeenTable = new Dictionary<object, object>();
		}

		// Token: 0x06003C4C RID: 15436 RVA: 0x000D1764 File Offset: 0x000CF964
		public void RegisterObject(object obj)
		{
			SerializationEvents serializationEventsForType = SerializationEventsCache.GetSerializationEventsForType(obj.GetType());
			if (serializationEventsForType.HasOnSerializingEvents && this._objectSeenTable.TryAdd(obj, true))
			{
				serializationEventsForType.InvokeOnSerializing(obj, this._context);
				this.AddOnSerialized(obj);
			}
		}

		// Token: 0x06003C4D RID: 15437 RVA: 0x000D17AD File Offset: 0x000CF9AD
		public void RaiseOnSerializedEvent()
		{
			SerializationEventHandler onSerializedHandler = this._onSerializedHandler;
			if (onSerializedHandler == null)
			{
				return;
			}
			onSerializedHandler(this._context);
		}

		// Token: 0x06003C4E RID: 15438 RVA: 0x000D17C8 File Offset: 0x000CF9C8
		private void AddOnSerialized(object obj)
		{
			SerializationEvents serializationEventsForType = SerializationEventsCache.GetSerializationEventsForType(obj.GetType());
			this._onSerializedHandler = serializationEventsForType.AddOnSerialized(obj, this._onSerializedHandler);
		}

		// Token: 0x040026AF RID: 9903
		private readonly Dictionary<object, object> _objectSeenTable;

		// Token: 0x040026B0 RID: 9904
		private readonly StreamingContext _context;

		// Token: 0x040026B1 RID: 9905
		private SerializationEventHandler _onSerializedHandler;
	}
}
