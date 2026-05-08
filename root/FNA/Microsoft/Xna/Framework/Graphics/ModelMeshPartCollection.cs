using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x020000A7 RID: 167
	public sealed class ModelMeshPartCollection : ReadOnlyCollection<ModelMeshPart>
	{
		// Token: 0x060013EA RID: 5098 RVA: 0x0002E157 File Offset: 0x0002C357
		internal ModelMeshPartCollection(IList<ModelMeshPart> list)
			: base(list)
		{
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x0002E160 File Offset: 0x0002C360
		public new ModelMeshPartCollection.Enumerator GetEnumerator()
		{
			return new ModelMeshPartCollection.Enumerator(this);
		}

		// Token: 0x020003D3 RID: 979
		public struct Enumerator : IEnumerator<ModelMeshPart>, IDisposable, IEnumerator
		{
			// Token: 0x06001AE7 RID: 6887 RVA: 0x0003F8C2 File Offset: 0x0003DAC2
			internal Enumerator(ModelMeshPartCollection collection)
			{
				this.collection = collection;
				this.position = -1;
			}

			// Token: 0x170003AF RID: 943
			// (get) Token: 0x06001AE8 RID: 6888 RVA: 0x0003F8D2 File Offset: 0x0003DAD2
			public ModelMeshPart Current
			{
				get
				{
					return this.collection[this.position];
				}
			}

			// Token: 0x06001AE9 RID: 6889 RVA: 0x0003F8E5 File Offset: 0x0003DAE5
			public bool MoveNext()
			{
				this.position++;
				return this.position < this.collection.Count;
			}

			// Token: 0x06001AEA RID: 6890 RVA: 0x00009E6B File Offset: 0x0000806B
			public void Dispose()
			{
			}

			// Token: 0x170003B0 RID: 944
			// (get) Token: 0x06001AEB RID: 6891 RVA: 0x0003F8D2 File Offset: 0x0003DAD2
			object IEnumerator.Current
			{
				get
				{
					return this.collection[this.position];
				}
			}

			// Token: 0x06001AEC RID: 6892 RVA: 0x0003F908 File Offset: 0x0003DB08
			void IEnumerator.Reset()
			{
				this.position = -1;
			}

			// Token: 0x04001DC3 RID: 7619
			private readonly ModelMeshPartCollection collection;

			// Token: 0x04001DC4 RID: 7620
			private int position;
		}
	}
}
