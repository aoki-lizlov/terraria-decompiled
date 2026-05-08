using System;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000209 RID: 521
	[Serializable]
	internal class DelegateSerializationHolder : ISerializable, IObjectReference
	{
		// Token: 0x0600199F RID: 6559 RVA: 0x000603D4 File Offset: 0x0005E5D4
		private DelegateSerializationHolder(SerializationInfo info, StreamingContext ctx)
		{
			DelegateSerializationHolder.DelegateEntry delegateEntry = (DelegateSerializationHolder.DelegateEntry)info.GetValue("Delegate", typeof(DelegateSerializationHolder.DelegateEntry));
			int num = 0;
			DelegateSerializationHolder.DelegateEntry delegateEntry2 = delegateEntry;
			while (delegateEntry2 != null)
			{
				delegateEntry2 = delegateEntry2.delegateEntry;
				num++;
			}
			if (num == 1)
			{
				this._delegate = delegateEntry.DeserializeDelegate(info, 0);
				return;
			}
			Delegate[] array = new Delegate[num];
			delegateEntry2 = delegateEntry;
			for (int i = 0; i < num; i++)
			{
				array[i] = delegateEntry2.DeserializeDelegate(info, i);
				delegateEntry2 = delegateEntry2.delegateEntry;
			}
			this._delegate = Delegate.Combine(array);
		}

		// Token: 0x060019A0 RID: 6560 RVA: 0x00060464 File Offset: 0x0005E664
		public static void GetDelegateData(Delegate instance, SerializationInfo info, StreamingContext ctx)
		{
			Delegate[] invocationList = instance.GetInvocationList();
			DelegateSerializationHolder.DelegateEntry delegateEntry = null;
			for (int i = 0; i < invocationList.Length; i++)
			{
				Delegate @delegate = invocationList[i];
				string text = ((@delegate.Target != null) ? ("target" + i.ToString()) : null);
				DelegateSerializationHolder.DelegateEntry delegateEntry2 = new DelegateSerializationHolder.DelegateEntry(@delegate, text);
				if (delegateEntry == null)
				{
					info.AddValue("Delegate", delegateEntry2);
				}
				else
				{
					delegateEntry.delegateEntry = delegateEntry2;
				}
				delegateEntry = delegateEntry2;
				if (@delegate.Target != null)
				{
					info.AddValue(text, @delegate.Target);
				}
				info.AddValue("method" + i.ToString(), @delegate.Method);
			}
			info.SetType(typeof(DelegateSerializationHolder));
		}

		// Token: 0x060019A1 RID: 6561 RVA: 0x00047E00 File Offset: 0x00046000
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060019A2 RID: 6562 RVA: 0x0006051A File Offset: 0x0005E71A
		public object GetRealObject(StreamingContext context)
		{
			return this._delegate;
		}

		// Token: 0x040015F1 RID: 5617
		private Delegate _delegate;

		// Token: 0x0200020A RID: 522
		[Serializable]
		private class DelegateEntry
		{
			// Token: 0x060019A3 RID: 6563 RVA: 0x00060524 File Offset: 0x0005E724
			public DelegateEntry(Delegate del, string targetLabel)
			{
				this.type = del.GetType().FullName;
				this.assembly = del.GetType().Assembly.FullName;
				this.target = targetLabel;
				this.targetTypeAssembly = del.Method.DeclaringType.Assembly.FullName;
				this.targetTypeName = del.Method.DeclaringType.FullName;
				this.methodName = del.Method.Name;
			}

			// Token: 0x060019A4 RID: 6564 RVA: 0x000605A8 File Offset: 0x0005E7A8
			public Delegate DeserializeDelegate(SerializationInfo info, int index)
			{
				object obj = null;
				if (this.target != null)
				{
					obj = info.GetValue(this.target.ToString(), typeof(object));
				}
				string text = "method" + index.ToString();
				MethodInfo methodInfo = (MethodInfo)info.GetValueNoThrow(text, typeof(MethodInfo));
				Type type = Assembly.Load(this.assembly).GetType(this.type);
				if (obj != null)
				{
					if (RemotingServices.IsTransparentProxy(obj) && !Assembly.Load(this.targetTypeAssembly).GetType(this.targetTypeName).IsInstanceOfType(obj))
					{
						throw new RemotingException("Unexpected proxy type.");
					}
					if (!(methodInfo == null))
					{
						return Delegate.CreateDelegate(type, obj, methodInfo);
					}
					return Delegate.CreateDelegate(type, obj, this.methodName);
				}
				else
				{
					if (methodInfo != null)
					{
						return Delegate.CreateDelegate(type, obj, methodInfo);
					}
					Type type2 = Assembly.Load(this.targetTypeAssembly).GetType(this.targetTypeName);
					return Delegate.CreateDelegate(type, type2, this.methodName);
				}
			}

			// Token: 0x040015F2 RID: 5618
			private string type;

			// Token: 0x040015F3 RID: 5619
			private string assembly;

			// Token: 0x040015F4 RID: 5620
			private object target;

			// Token: 0x040015F5 RID: 5621
			private string targetTypeAssembly;

			// Token: 0x040015F6 RID: 5622
			private string targetTypeName;

			// Token: 0x040015F7 RID: 5623
			private string methodName;

			// Token: 0x040015F8 RID: 5624
			public DelegateSerializationHolder.DelegateEntry delegateEntry;
		}
	}
}
