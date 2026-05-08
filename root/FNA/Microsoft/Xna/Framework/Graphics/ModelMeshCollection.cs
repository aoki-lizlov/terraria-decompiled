using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x020000A5 RID: 165
	public sealed class ModelMeshCollection : ReadOnlyCollection<ModelMesh>
	{
		// Token: 0x170002C4 RID: 708
		public ModelMesh this[string meshName]
		{
			get
			{
				ModelMesh modelMesh;
				if (!this.TryGetValue(meshName, out modelMesh))
				{
					throw new KeyNotFoundException();
				}
				return modelMesh;
			}
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x0002DF83 File Offset: 0x0002C183
		internal ModelMeshCollection(IList<ModelMesh> list)
			: base(list)
		{
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x0002DF8C File Offset: 0x0002C18C
		public bool TryGetValue(string meshName, out ModelMesh value)
		{
			if (string.IsNullOrEmpty(meshName))
			{
				throw new ArgumentNullException("meshName");
			}
			foreach (ModelMesh modelMesh in this)
			{
				if (string.Compare(modelMesh.Name, meshName, StringComparison.Ordinal) == 0)
				{
					value = modelMesh;
					return true;
				}
			}
			value = null;
			return false;
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x0002E004 File Offset: 0x0002C204
		public new ModelMeshCollection.Enumerator GetEnumerator()
		{
			return new ModelMeshCollection.Enumerator(this);
		}

		// Token: 0x020003D2 RID: 978
		public struct Enumerator : IEnumerator<ModelMesh>, IDisposable, IEnumerator
		{
			// Token: 0x06001AE1 RID: 6881 RVA: 0x0003F873 File Offset: 0x0003DA73
			internal Enumerator(ModelMeshCollection collection)
			{
				this.collection = collection;
				this.position = -1;
			}

			// Token: 0x170003AD RID: 941
			// (get) Token: 0x06001AE2 RID: 6882 RVA: 0x0003F883 File Offset: 0x0003DA83
			public ModelMesh Current
			{
				get
				{
					return this.collection[this.position];
				}
			}

			// Token: 0x06001AE3 RID: 6883 RVA: 0x0003F896 File Offset: 0x0003DA96
			public bool MoveNext()
			{
				this.position++;
				return this.position < this.collection.Count;
			}

			// Token: 0x06001AE4 RID: 6884 RVA: 0x00009E6B File Offset: 0x0000806B
			public void Dispose()
			{
			}

			// Token: 0x170003AE RID: 942
			// (get) Token: 0x06001AE5 RID: 6885 RVA: 0x0003F883 File Offset: 0x0003DA83
			object IEnumerator.Current
			{
				get
				{
					return this.collection[this.position];
				}
			}

			// Token: 0x06001AE6 RID: 6886 RVA: 0x0003F8B9 File Offset: 0x0003DAB9
			void IEnumerator.Reset()
			{
				this.position = -1;
			}

			// Token: 0x04001DC1 RID: 7617
			private readonly ModelMeshCollection collection;

			// Token: 0x04001DC2 RID: 7618
			private int position;
		}
	}
}
