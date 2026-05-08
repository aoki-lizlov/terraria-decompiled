using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x020000A3 RID: 163
	public sealed class ModelEffectCollection : ReadOnlyCollection<Effect>
	{
		// Token: 0x060013C2 RID: 5058 RVA: 0x0002DD58 File Offset: 0x0002BF58
		internal ModelEffectCollection(IList<Effect> list)
			: base(list)
		{
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x0002DD61 File Offset: 0x0002BF61
		internal ModelEffectCollection()
			: base(new List<Effect>())
		{
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x0002DD6E File Offset: 0x0002BF6E
		public new ModelEffectCollection.Enumerator GetEnumerator()
		{
			return new ModelEffectCollection.Enumerator((List<Effect>)base.Items);
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x0002DD80 File Offset: 0x0002BF80
		internal void Add(Effect item)
		{
			base.Items.Add(item);
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x0002DD8E File Offset: 0x0002BF8E
		internal void Remove(Effect item)
		{
			base.Items.Remove(item);
		}

		// Token: 0x020003D1 RID: 977
		public struct Enumerator : IEnumerator<Effect>, IDisposable, IEnumerator
		{
			// Token: 0x170003AB RID: 939
			// (get) Token: 0x06001ADB RID: 6875 RVA: 0x0003F7F2 File Offset: 0x0003D9F2
			public Effect Current
			{
				get
				{
					return this.enumerator.Current;
				}
			}

			// Token: 0x06001ADC RID: 6876 RVA: 0x0003F7FF File Offset: 0x0003D9FF
			internal Enumerator(List<Effect> list)
			{
				this.enumerator = list.GetEnumerator();
				this.disposed = false;
			}

			// Token: 0x06001ADD RID: 6877 RVA: 0x0003F814 File Offset: 0x0003DA14
			public void Dispose()
			{
				if (!this.disposed)
				{
					this.enumerator.Dispose();
					this.disposed = true;
				}
			}

			// Token: 0x06001ADE RID: 6878 RVA: 0x0003F830 File Offset: 0x0003DA30
			public bool MoveNext()
			{
				return this.enumerator.MoveNext();
			}

			// Token: 0x170003AC RID: 940
			// (get) Token: 0x06001ADF RID: 6879 RVA: 0x0003F83D File Offset: 0x0003DA3D
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001AE0 RID: 6880 RVA: 0x0003F848 File Offset: 0x0003DA48
			void IEnumerator.Reset()
			{
				IEnumerator enumerator = this.enumerator;
				enumerator.Reset();
				this.enumerator = (List<Effect>.Enumerator)enumerator;
			}

			// Token: 0x04001DBF RID: 7615
			private List<Effect>.Enumerator enumerator;

			// Token: 0x04001DC0 RID: 7616
			private bool disposed;
		}
	}
}
