using System;
using System.Collections;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x0200055D RID: 1373
	internal class DynamicPropertyCollection
	{
		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06003747 RID: 14151 RVA: 0x000C8208 File Offset: 0x000C6408
		public bool HasProperties
		{
			get
			{
				return this._properties.Count > 0;
			}
		}

		// Token: 0x06003748 RID: 14152 RVA: 0x000C8218 File Offset: 0x000C6418
		public bool RegisterDynamicProperty(IDynamicProperty prop)
		{
			bool flag2;
			lock (this)
			{
				if (this.FindProperty(prop.Name) != -1)
				{
					throw new InvalidOperationException("Another property by this name already exists");
				}
				ArrayList arrayList = new ArrayList(this._properties);
				DynamicPropertyCollection.DynamicPropertyReg dynamicPropertyReg = new DynamicPropertyCollection.DynamicPropertyReg();
				dynamicPropertyReg.Property = prop;
				IContributeDynamicSink contributeDynamicSink = prop as IContributeDynamicSink;
				if (contributeDynamicSink != null)
				{
					dynamicPropertyReg.Sink = contributeDynamicSink.GetDynamicSink();
				}
				arrayList.Add(dynamicPropertyReg);
				this._properties = arrayList;
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x06003749 RID: 14153 RVA: 0x000C82B0 File Offset: 0x000C64B0
		public bool UnregisterDynamicProperty(string name)
		{
			bool flag2;
			lock (this)
			{
				int num = this.FindProperty(name);
				if (num == -1)
				{
					throw new RemotingException("A property with the name " + name + " was not found");
				}
				this._properties.RemoveAt(num);
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x0600374A RID: 14154 RVA: 0x000C8318 File Offset: 0x000C6518
		public void NotifyMessage(bool start, IMessage msg, bool client_site, bool async)
		{
			ArrayList properties = this._properties;
			if (start)
			{
				using (IEnumerator enumerator = properties.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						DynamicPropertyCollection.DynamicPropertyReg dynamicPropertyReg = (DynamicPropertyCollection.DynamicPropertyReg)obj;
						if (dynamicPropertyReg.Sink != null)
						{
							dynamicPropertyReg.Sink.ProcessMessageStart(msg, client_site, async);
						}
					}
					return;
				}
			}
			foreach (object obj2 in properties)
			{
				DynamicPropertyCollection.DynamicPropertyReg dynamicPropertyReg2 = (DynamicPropertyCollection.DynamicPropertyReg)obj2;
				if (dynamicPropertyReg2.Sink != null)
				{
					dynamicPropertyReg2.Sink.ProcessMessageFinish(msg, client_site, async);
				}
			}
		}

		// Token: 0x0600374B RID: 14155 RVA: 0x000C83DC File Offset: 0x000C65DC
		private int FindProperty(string name)
		{
			for (int i = 0; i < this._properties.Count; i++)
			{
				if (((DynamicPropertyCollection.DynamicPropertyReg)this._properties[i]).Property.Name == name)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600374C RID: 14156 RVA: 0x000C8425 File Offset: 0x000C6625
		public DynamicPropertyCollection()
		{
		}

		// Token: 0x04002533 RID: 9523
		private ArrayList _properties = new ArrayList();

		// Token: 0x0200055E RID: 1374
		private class DynamicPropertyReg
		{
			// Token: 0x0600374D RID: 14157 RVA: 0x000025BE File Offset: 0x000007BE
			public DynamicPropertyReg()
			{
			}

			// Token: 0x04002534 RID: 9524
			public IDynamicProperty Property;

			// Token: 0x04002535 RID: 9525
			public IDynamicMessageSink Sink;
		}
	}
}
