using System;
using System.Reflection;
using System.Security;

namespace System.Runtime.Serialization
{
	// Token: 0x02000633 RID: 1587
	internal sealed class ObjectHolder
	{
		// Token: 0x06003CA7 RID: 15527 RVA: 0x000D3786 File Offset: 0x000D1986
		internal ObjectHolder(long objID)
			: this(null, objID, null, null, 0L, null, null)
		{
		}

		// Token: 0x06003CA8 RID: 15528 RVA: 0x000D3798 File Offset: 0x000D1998
		internal ObjectHolder(object obj, long objID, SerializationInfo info, ISerializationSurrogate surrogate, long idOfContainingObj, FieldInfo field, int[] arrayIndex)
		{
			this.m_object = obj;
			this.m_id = objID;
			this.m_flags = 0;
			this.m_missingElementsRemaining = 0;
			this.m_missingDecendents = 0;
			this.m_dependentObjects = null;
			this.m_next = null;
			this.m_serInfo = info;
			this.m_surrogate = surrogate;
			this.m_markForFixupWhenAvailable = false;
			if (obj is TypeLoadExceptionHolder)
			{
				this.m_typeLoad = (TypeLoadExceptionHolder)obj;
			}
			if (idOfContainingObj != 0L && ((field != null && field.FieldType.IsValueType) || arrayIndex != null))
			{
				if (idOfContainingObj == objID)
				{
					throw new SerializationException(Environment.GetResourceString("The ID of the containing object cannot be the same as the object ID."));
				}
				this.m_valueFixup = new ValueTypeFixupInfo(idOfContainingObj, field, arrayIndex);
			}
			this.SetFlags();
		}

		// Token: 0x06003CA9 RID: 15529 RVA: 0x000D3854 File Offset: 0x000D1A54
		internal ObjectHolder(string obj, long objID, SerializationInfo info, ISerializationSurrogate surrogate, long idOfContainingObj, FieldInfo field, int[] arrayIndex)
		{
			this.m_object = obj;
			this.m_id = objID;
			this.m_flags = 0;
			this.m_missingElementsRemaining = 0;
			this.m_missingDecendents = 0;
			this.m_dependentObjects = null;
			this.m_next = null;
			this.m_serInfo = info;
			this.m_surrogate = surrogate;
			this.m_markForFixupWhenAvailable = false;
			if (idOfContainingObj != 0L && arrayIndex != null)
			{
				this.m_valueFixup = new ValueTypeFixupInfo(idOfContainingObj, field, arrayIndex);
			}
			if (this.m_valueFixup != null)
			{
				this.m_flags |= 8;
			}
		}

		// Token: 0x06003CAA RID: 15530 RVA: 0x000D38DD File Offset: 0x000D1ADD
		private void IncrementDescendentFixups(int amount)
		{
			this.m_missingDecendents += amount;
		}

		// Token: 0x06003CAB RID: 15531 RVA: 0x000D38ED File Offset: 0x000D1AED
		internal void DecrementFixupsRemaining(ObjectManager manager)
		{
			this.m_missingElementsRemaining--;
			if (this.RequiresValueTypeFixup)
			{
				this.UpdateDescendentDependencyChain(-1, manager);
			}
		}

		// Token: 0x06003CAC RID: 15532 RVA: 0x000D390D File Offset: 0x000D1B0D
		internal void RemoveDependency(long id)
		{
			this.m_dependentObjects.RemoveElement(id);
		}

		// Token: 0x06003CAD RID: 15533 RVA: 0x000D391C File Offset: 0x000D1B1C
		internal void AddFixup(FixupHolder fixup, ObjectManager manager)
		{
			if (this.m_missingElements == null)
			{
				this.m_missingElements = new FixupHolderList();
			}
			this.m_missingElements.Add(fixup);
			this.m_missingElementsRemaining++;
			if (this.RequiresValueTypeFixup)
			{
				this.UpdateDescendentDependencyChain(1, manager);
			}
		}

		// Token: 0x06003CAE RID: 15534 RVA: 0x000D395C File Offset: 0x000D1B5C
		private void UpdateDescendentDependencyChain(int amount, ObjectManager manager)
		{
			ObjectHolder objectHolder = this;
			do
			{
				objectHolder = manager.FindOrCreateObjectHolder(objectHolder.ContainerID);
				objectHolder.IncrementDescendentFixups(amount);
			}
			while (objectHolder.RequiresValueTypeFixup);
		}

		// Token: 0x06003CAF RID: 15535 RVA: 0x000D3987 File Offset: 0x000D1B87
		internal void AddDependency(long dependentObject)
		{
			if (this.m_dependentObjects == null)
			{
				this.m_dependentObjects = new LongList();
			}
			this.m_dependentObjects.Add(dependentObject);
		}

		// Token: 0x06003CB0 RID: 15536 RVA: 0x000D39A8 File Offset: 0x000D1BA8
		[SecurityCritical]
		internal void UpdateData(object obj, SerializationInfo info, ISerializationSurrogate surrogate, long idOfContainer, FieldInfo field, int[] arrayIndex, ObjectManager manager)
		{
			this.SetObjectValue(obj, manager);
			this.m_serInfo = info;
			this.m_surrogate = surrogate;
			if (idOfContainer != 0L && ((field != null && field.FieldType.IsValueType) || arrayIndex != null))
			{
				if (idOfContainer == this.m_id)
				{
					throw new SerializationException(Environment.GetResourceString("The ID of the containing object cannot be the same as the object ID."));
				}
				this.m_valueFixup = new ValueTypeFixupInfo(idOfContainer, field, arrayIndex);
			}
			this.SetFlags();
			if (this.RequiresValueTypeFixup)
			{
				this.UpdateDescendentDependencyChain(this.m_missingElementsRemaining, manager);
			}
		}

		// Token: 0x06003CB1 RID: 15537 RVA: 0x000D3A33 File Offset: 0x000D1C33
		internal void MarkForCompletionWhenAvailable()
		{
			this.m_markForFixupWhenAvailable = true;
		}

		// Token: 0x06003CB2 RID: 15538 RVA: 0x000D3A3C File Offset: 0x000D1C3C
		internal void SetFlags()
		{
			if (this.m_object is IObjectReference)
			{
				this.m_flags |= 1;
			}
			this.m_flags &= -7;
			if (this.m_surrogate != null)
			{
				this.m_flags |= 4;
			}
			else if (this.m_object is ISerializable)
			{
				this.m_flags |= 2;
			}
			if (this.m_valueFixup != null)
			{
				this.m_flags |= 8;
			}
		}

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06003CB3 RID: 15539 RVA: 0x000D3ABC File Offset: 0x000D1CBC
		// (set) Token: 0x06003CB4 RID: 15540 RVA: 0x000D3AC9 File Offset: 0x000D1CC9
		internal bool IsIncompleteObjectReference
		{
			get
			{
				return (this.m_flags & 1) != 0;
			}
			set
			{
				if (value)
				{
					this.m_flags |= 1;
					return;
				}
				this.m_flags &= -2;
			}
		}

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06003CB5 RID: 15541 RVA: 0x000D3AEC File Offset: 0x000D1CEC
		internal bool RequiresDelayedFixup
		{
			get
			{
				return (this.m_flags & 7) != 0;
			}
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x06003CB6 RID: 15542 RVA: 0x000D3AF9 File Offset: 0x000D1CF9
		internal bool RequiresValueTypeFixup
		{
			get
			{
				return (this.m_flags & 8) != 0;
			}
		}

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x06003CB7 RID: 15543 RVA: 0x000D3B06 File Offset: 0x000D1D06
		// (set) Token: 0x06003CB8 RID: 15544 RVA: 0x000D3B3A File Offset: 0x000D1D3A
		internal bool ValueTypeFixupPerformed
		{
			get
			{
				return (this.m_flags & 32768) != 0 || (this.m_object != null && (this.m_dependentObjects == null || this.m_dependentObjects.Count == 0));
			}
			set
			{
				if (value)
				{
					this.m_flags |= 32768;
				}
			}
		}

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x06003CB9 RID: 15545 RVA: 0x000D3B51 File Offset: 0x000D1D51
		internal bool HasISerializable
		{
			get
			{
				return (this.m_flags & 2) != 0;
			}
		}

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x06003CBA RID: 15546 RVA: 0x000D3B5E File Offset: 0x000D1D5E
		internal bool HasSurrogate
		{
			get
			{
				return (this.m_flags & 4) != 0;
			}
		}

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x06003CBB RID: 15547 RVA: 0x000D3B6B File Offset: 0x000D1D6B
		internal bool CanSurrogatedObjectValueChange
		{
			get
			{
				return this.m_surrogate == null || this.m_surrogate.GetType() != typeof(SurrogateForCyclicalReference);
			}
		}

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x06003CBC RID: 15548 RVA: 0x000D3B91 File Offset: 0x000D1D91
		internal bool CanObjectValueChange
		{
			get
			{
				return this.IsIncompleteObjectReference || (this.HasSurrogate && this.CanSurrogatedObjectValueChange);
			}
		}

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06003CBD RID: 15549 RVA: 0x000D3BAD File Offset: 0x000D1DAD
		internal int DirectlyDependentObjects
		{
			get
			{
				return this.m_missingElementsRemaining;
			}
		}

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x06003CBE RID: 15550 RVA: 0x000D3BB5 File Offset: 0x000D1DB5
		internal int TotalDependentObjects
		{
			get
			{
				return this.m_missingElementsRemaining + this.m_missingDecendents;
			}
		}

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06003CBF RID: 15551 RVA: 0x000D3BC4 File Offset: 0x000D1DC4
		// (set) Token: 0x06003CC0 RID: 15552 RVA: 0x000D3BCC File Offset: 0x000D1DCC
		internal bool Reachable
		{
			get
			{
				return this.m_reachable;
			}
			set
			{
				this.m_reachable = value;
			}
		}

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x06003CC1 RID: 15553 RVA: 0x000D3BD5 File Offset: 0x000D1DD5
		internal bool TypeLoadExceptionReachable
		{
			get
			{
				return this.m_typeLoad != null;
			}
		}

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x06003CC2 RID: 15554 RVA: 0x000D3BE0 File Offset: 0x000D1DE0
		// (set) Token: 0x06003CC3 RID: 15555 RVA: 0x000D3BE8 File Offset: 0x000D1DE8
		internal TypeLoadExceptionHolder TypeLoadException
		{
			get
			{
				return this.m_typeLoad;
			}
			set
			{
				this.m_typeLoad = value;
			}
		}

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x06003CC4 RID: 15556 RVA: 0x000D3BF1 File Offset: 0x000D1DF1
		internal object ObjectValue
		{
			get
			{
				return this.m_object;
			}
		}

		// Token: 0x06003CC5 RID: 15557 RVA: 0x000D3BF9 File Offset: 0x000D1DF9
		[SecurityCritical]
		internal void SetObjectValue(object obj, ObjectManager manager)
		{
			this.m_object = obj;
			if (obj == manager.TopObject)
			{
				this.m_reachable = true;
			}
			if (obj is TypeLoadExceptionHolder)
			{
				this.m_typeLoad = (TypeLoadExceptionHolder)obj;
			}
			if (this.m_markForFixupWhenAvailable)
			{
				manager.CompleteObject(this, true);
			}
		}

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06003CC6 RID: 15558 RVA: 0x000D3C36 File Offset: 0x000D1E36
		// (set) Token: 0x06003CC7 RID: 15559 RVA: 0x000D3C3E File Offset: 0x000D1E3E
		internal SerializationInfo SerializationInfo
		{
			get
			{
				return this.m_serInfo;
			}
			set
			{
				this.m_serInfo = value;
			}
		}

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06003CC8 RID: 15560 RVA: 0x000D3C47 File Offset: 0x000D1E47
		internal ISerializationSurrogate Surrogate
		{
			get
			{
				return this.m_surrogate;
			}
		}

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x06003CC9 RID: 15561 RVA: 0x000D3C4F File Offset: 0x000D1E4F
		// (set) Token: 0x06003CCA RID: 15562 RVA: 0x000D3C57 File Offset: 0x000D1E57
		internal LongList DependentObjects
		{
			get
			{
				return this.m_dependentObjects;
			}
			set
			{
				this.m_dependentObjects = value;
			}
		}

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x06003CCB RID: 15563 RVA: 0x000D3C60 File Offset: 0x000D1E60
		// (set) Token: 0x06003CCC RID: 15564 RVA: 0x000D3C87 File Offset: 0x000D1E87
		internal bool RequiresSerInfoFixup
		{
			get
			{
				return ((this.m_flags & 4) != 0 || (this.m_flags & 2) != 0) && (this.m_flags & 16384) == 0;
			}
			set
			{
				if (!value)
				{
					this.m_flags |= 16384;
					return;
				}
				this.m_flags &= -16385;
			}
		}

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x06003CCD RID: 15565 RVA: 0x000D3CB1 File Offset: 0x000D1EB1
		internal ValueTypeFixupInfo ValueFixup
		{
			get
			{
				return this.m_valueFixup;
			}
		}

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x06003CCE RID: 15566 RVA: 0x000D3CB9 File Offset: 0x000D1EB9
		internal bool CompletelyFixed
		{
			get
			{
				return !this.RequiresSerInfoFixup && !this.IsIncompleteObjectReference;
			}
		}

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06003CCF RID: 15567 RVA: 0x000D3CCE File Offset: 0x000D1ECE
		internal long ContainerID
		{
			get
			{
				if (this.m_valueFixup != null)
				{
					return this.m_valueFixup.ContainerID;
				}
				return 0L;
			}
		}

		// Token: 0x040026D2 RID: 9938
		internal const int INCOMPLETE_OBJECT_REFERENCE = 1;

		// Token: 0x040026D3 RID: 9939
		internal const int HAS_ISERIALIZABLE = 2;

		// Token: 0x040026D4 RID: 9940
		internal const int HAS_SURROGATE = 4;

		// Token: 0x040026D5 RID: 9941
		internal const int REQUIRES_VALUETYPE_FIXUP = 8;

		// Token: 0x040026D6 RID: 9942
		internal const int REQUIRES_DELAYED_FIXUP = 7;

		// Token: 0x040026D7 RID: 9943
		internal const int SER_INFO_FIXED = 16384;

		// Token: 0x040026D8 RID: 9944
		internal const int VALUETYPE_FIXUP_PERFORMED = 32768;

		// Token: 0x040026D9 RID: 9945
		private object m_object;

		// Token: 0x040026DA RID: 9946
		internal long m_id;

		// Token: 0x040026DB RID: 9947
		private int m_missingElementsRemaining;

		// Token: 0x040026DC RID: 9948
		private int m_missingDecendents;

		// Token: 0x040026DD RID: 9949
		internal SerializationInfo m_serInfo;

		// Token: 0x040026DE RID: 9950
		internal ISerializationSurrogate m_surrogate;

		// Token: 0x040026DF RID: 9951
		internal FixupHolderList m_missingElements;

		// Token: 0x040026E0 RID: 9952
		internal LongList m_dependentObjects;

		// Token: 0x040026E1 RID: 9953
		internal ObjectHolder m_next;

		// Token: 0x040026E2 RID: 9954
		internal int m_flags;

		// Token: 0x040026E3 RID: 9955
		private bool m_markForFixupWhenAvailable;

		// Token: 0x040026E4 RID: 9956
		private ValueTypeFixupInfo m_valueFixup;

		// Token: 0x040026E5 RID: 9957
		private TypeLoadExceptionHolder m_typeLoad;

		// Token: 0x040026E6 RID: 9958
		private bool m_reachable;
	}
}
