using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000219 RID: 537
	[ComVisible(true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public abstract class MulticastDelegate : Delegate
	{
		// Token: 0x06001A29 RID: 6697 RVA: 0x00061A48 File Offset: 0x0005FC48
		protected MulticastDelegate(object target, string method)
			: base(target, method)
		{
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x00061A52 File Offset: 0x0005FC52
		protected MulticastDelegate(Type target, string method)
			: base(target, method)
		{
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x00061A5C File Offset: 0x0005FC5C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x00061A68 File Offset: 0x0005FC68
		protected sealed override object DynamicInvokeImpl(object[] args)
		{
			if (this.delegates == null)
			{
				return base.DynamicInvokeImpl(args);
			}
			int num = 0;
			int num2 = this.delegates.Length;
			object obj;
			do
			{
				obj = this.delegates[num].DynamicInvoke(args);
			}
			while (++num < num2);
			return obj;
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06001A2D RID: 6701 RVA: 0x00061AA8 File Offset: 0x0005FCA8
		internal bool HasSingleTarget
		{
			get
			{
				return this.delegates == null;
			}
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x00061AB4 File Offset: 0x0005FCB4
		public sealed override bool Equals(object obj)
		{
			if (!base.Equals(obj))
			{
				return false;
			}
			MulticastDelegate multicastDelegate = obj as MulticastDelegate;
			if (multicastDelegate == null)
			{
				return false;
			}
			if (this.delegates == null && multicastDelegate.delegates == null)
			{
				return true;
			}
			if ((this.delegates == null) ^ (multicastDelegate.delegates == null))
			{
				return false;
			}
			if (this.delegates.Length != multicastDelegate.delegates.Length)
			{
				return false;
			}
			for (int i = 0; i < this.delegates.Length; i++)
			{
				if (!this.delegates[i].Equals(multicastDelegate.delegates[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x00061B42 File Offset: 0x0005FD42
		public sealed override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x00061B4A File Offset: 0x0005FD4A
		protected override MethodInfo GetMethodImpl()
		{
			if (this.delegates != null)
			{
				return this.delegates[this.delegates.Length - 1].Method;
			}
			return base.GetMethodImpl();
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x00061B71 File Offset: 0x0005FD71
		public sealed override Delegate[] GetInvocationList()
		{
			if (this.delegates != null)
			{
				return (Delegate[])this.delegates.Clone();
			}
			return new Delegate[] { this };
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x00061B98 File Offset: 0x0005FD98
		protected sealed override Delegate CombineImpl(Delegate follow)
		{
			if (follow == null)
			{
				return this;
			}
			MulticastDelegate multicastDelegate = (MulticastDelegate)follow;
			MulticastDelegate multicastDelegate2 = Delegate.AllocDelegateLike_internal(this);
			if (this.delegates == null && multicastDelegate.delegates == null)
			{
				multicastDelegate2.delegates = new Delegate[] { this, multicastDelegate };
			}
			else if (this.delegates == null)
			{
				multicastDelegate2.delegates = new Delegate[1 + multicastDelegate.delegates.Length];
				multicastDelegate2.delegates[0] = this;
				Array.Copy(multicastDelegate.delegates, 0, multicastDelegate2.delegates, 1, multicastDelegate.delegates.Length);
			}
			else if (multicastDelegate.delegates == null)
			{
				multicastDelegate2.delegates = new Delegate[this.delegates.Length + 1];
				Array.Copy(this.delegates, 0, multicastDelegate2.delegates, 0, this.delegates.Length);
				multicastDelegate2.delegates[multicastDelegate2.delegates.Length - 1] = multicastDelegate;
			}
			else
			{
				multicastDelegate2.delegates = new Delegate[this.delegates.Length + multicastDelegate.delegates.Length];
				Array.Copy(this.delegates, 0, multicastDelegate2.delegates, 0, this.delegates.Length);
				Array.Copy(multicastDelegate.delegates, 0, multicastDelegate2.delegates, this.delegates.Length, multicastDelegate.delegates.Length);
			}
			return multicastDelegate2;
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x00061CD0 File Offset: 0x0005FED0
		private int LastIndexOf(Delegate[] haystack, Delegate[] needle)
		{
			if (haystack.Length < needle.Length)
			{
				return -1;
			}
			if (haystack.Length == needle.Length)
			{
				for (int i = 0; i < haystack.Length; i++)
				{
					if (!haystack[i].Equals(needle[i]))
					{
						return -1;
					}
				}
				return 0;
			}
			int num;
			for (int j = haystack.Length - needle.Length; j >= 0; j -= num + 1)
			{
				num = 0;
				while (needle[num].Equals(haystack[j]))
				{
					if (num == needle.Length - 1)
					{
						return j - num;
					}
					j++;
					num++;
				}
			}
			return -1;
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x00061D48 File Offset: 0x0005FF48
		protected sealed override Delegate RemoveImpl(Delegate value)
		{
			if (value == null)
			{
				return this;
			}
			MulticastDelegate multicastDelegate = (MulticastDelegate)value;
			if (this.delegates == null && multicastDelegate.delegates == null)
			{
				if (!this.Equals(multicastDelegate))
				{
					return this;
				}
				return null;
			}
			else
			{
				if (this.delegates == null)
				{
					foreach (Delegate @delegate in multicastDelegate.delegates)
					{
						if (this.Equals(@delegate))
						{
							return null;
						}
					}
					return this;
				}
				if (multicastDelegate.delegates == null)
				{
					int num = Array.LastIndexOf<Delegate>(this.delegates, multicastDelegate);
					if (num == -1)
					{
						return this;
					}
					if (this.delegates.Length <= 1)
					{
						throw new InvalidOperationException();
					}
					if (this.delegates.Length == 2)
					{
						return this.delegates[(num == 0) ? 1 : 0];
					}
					MulticastDelegate multicastDelegate2 = Delegate.AllocDelegateLike_internal(this);
					multicastDelegate2.delegates = new Delegate[this.delegates.Length - 1];
					Array.Copy(this.delegates, multicastDelegate2.delegates, num);
					Array.Copy(this.delegates, num + 1, multicastDelegate2.delegates, num, this.delegates.Length - num - 1);
					return multicastDelegate2;
				}
				else
				{
					if (this.delegates.Equals(multicastDelegate.delegates))
					{
						return null;
					}
					int num2 = this.LastIndexOf(this.delegates, multicastDelegate.delegates);
					if (num2 == -1)
					{
						return this;
					}
					MulticastDelegate multicastDelegate3 = Delegate.AllocDelegateLike_internal(this);
					multicastDelegate3.delegates = new Delegate[this.delegates.Length - multicastDelegate.delegates.Length];
					Array.Copy(this.delegates, multicastDelegate3.delegates, num2);
					Array.Copy(this.delegates, num2 + multicastDelegate.delegates.Length, multicastDelegate3.delegates, num2, this.delegates.Length - num2 - multicastDelegate.delegates.Length);
					return multicastDelegate3;
				}
			}
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x00061EF0 File Offset: 0x000600F0
		public static bool operator ==(MulticastDelegate d1, MulticastDelegate d2)
		{
			if (d1 == null)
			{
				return d2 == null;
			}
			return d1.Equals(d2);
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x00061F01 File Offset: 0x00060101
		public static bool operator !=(MulticastDelegate d1, MulticastDelegate d2)
		{
			if (d1 == null)
			{
				return d2 != null;
			}
			return !d1.Equals(d2);
		}

		// Token: 0x04001617 RID: 5655
		private Delegate[] delegates;
	}
}
