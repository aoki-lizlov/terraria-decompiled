using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x020000A2 RID: 162
	public class ModelBoneCollection : ReadOnlyCollection<ModelBone>
	{
		// Token: 0x170002BD RID: 701
		public ModelBone this[string boneName]
		{
			get
			{
				ModelBone modelBone;
				if (this.TryGetValue(boneName, out modelBone))
				{
					return modelBone;
				}
				throw new KeyNotFoundException();
			}
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x0002DCE3 File Offset: 0x0002BEE3
		internal ModelBoneCollection(IList<ModelBone> list)
			: base(list)
		{
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x0002DCEC File Offset: 0x0002BEEC
		public bool TryGetValue(string boneName, out ModelBone value)
		{
			foreach (ModelBone modelBone in base.Items)
			{
				if (modelBone.Name == boneName)
				{
					value = modelBone;
					return true;
				}
			}
			value = null;
			return false;
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x0002DD50 File Offset: 0x0002BF50
		public new ModelBoneCollection.Enumerator GetEnumerator()
		{
			return new ModelBoneCollection.Enumerator(this);
		}

		// Token: 0x020003D0 RID: 976
		public struct Enumerator : IEnumerator<ModelBone>, IDisposable, IEnumerator
		{
			// Token: 0x06001AD5 RID: 6869 RVA: 0x0003F7A3 File Offset: 0x0003D9A3
			internal Enumerator(ModelBoneCollection collection)
			{
				this.collection = collection;
				this.position = -1;
			}

			// Token: 0x170003A9 RID: 937
			// (get) Token: 0x06001AD6 RID: 6870 RVA: 0x0003F7B3 File Offset: 0x0003D9B3
			public ModelBone Current
			{
				get
				{
					return this.collection[this.position];
				}
			}

			// Token: 0x06001AD7 RID: 6871 RVA: 0x0003F7C6 File Offset: 0x0003D9C6
			public bool MoveNext()
			{
				this.position++;
				return this.position < this.collection.Count;
			}

			// Token: 0x06001AD8 RID: 6872 RVA: 0x00009E6B File Offset: 0x0000806B
			public void Dispose()
			{
			}

			// Token: 0x170003AA RID: 938
			// (get) Token: 0x06001AD9 RID: 6873 RVA: 0x0003F7B3 File Offset: 0x0003D9B3
			object IEnumerator.Current
			{
				get
				{
					return this.collection[this.position];
				}
			}

			// Token: 0x06001ADA RID: 6874 RVA: 0x0003F7E9 File Offset: 0x0003D9E9
			void IEnumerator.Reset()
			{
				this.position = -1;
			}

			// Token: 0x04001DBD RID: 7613
			private readonly ModelBoneCollection collection;

			// Token: 0x04001DBE RID: 7614
			private int position;
		}
	}
}
