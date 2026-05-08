using System;
using System.Diagnostics;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200068A RID: 1674
	internal sealed class SerStack
	{
		// Token: 0x06003F3F RID: 16191 RVA: 0x000DEDCE File Offset: 0x000DCFCE
		internal SerStack()
		{
			this.stackId = "System";
		}

		// Token: 0x06003F40 RID: 16192 RVA: 0x000DEDF4 File Offset: 0x000DCFF4
		internal SerStack(string stackId)
		{
			this.stackId = stackId;
		}

		// Token: 0x06003F41 RID: 16193 RVA: 0x000DEE18 File Offset: 0x000DD018
		internal void Push(object obj)
		{
			if (this.top == this.objects.Length - 1)
			{
				this.IncreaseCapacity();
			}
			object[] array = this.objects;
			int num = this.top + 1;
			this.top = num;
			array[num] = obj;
		}

		// Token: 0x06003F42 RID: 16194 RVA: 0x000DEE58 File Offset: 0x000DD058
		internal object Pop()
		{
			if (this.top < 0)
			{
				return null;
			}
			object obj = this.objects[this.top];
			object[] array = this.objects;
			int num = this.top;
			this.top = num - 1;
			array[num] = null;
			return obj;
		}

		// Token: 0x06003F43 RID: 16195 RVA: 0x000DEE98 File Offset: 0x000DD098
		internal void IncreaseCapacity()
		{
			object[] array = new object[this.objects.Length * 2];
			Array.Copy(this.objects, 0, array, 0, this.objects.Length);
			this.objects = array;
		}

		// Token: 0x06003F44 RID: 16196 RVA: 0x000DEED2 File Offset: 0x000DD0D2
		internal object Peek()
		{
			if (this.top < 0)
			{
				return null;
			}
			return this.objects[this.top];
		}

		// Token: 0x06003F45 RID: 16197 RVA: 0x000DEEEC File Offset: 0x000DD0EC
		internal object PeekPeek()
		{
			if (this.top < 1)
			{
				return null;
			}
			return this.objects[this.top - 1];
		}

		// Token: 0x06003F46 RID: 16198 RVA: 0x000DEF08 File Offset: 0x000DD108
		internal int Count()
		{
			return this.top + 1;
		}

		// Token: 0x06003F47 RID: 16199 RVA: 0x000DEF12 File Offset: 0x000DD112
		internal bool IsEmpty()
		{
			return this.top <= 0;
		}

		// Token: 0x06003F48 RID: 16200 RVA: 0x000DEF20 File Offset: 0x000DD120
		[Conditional("SER_LOGGING")]
		internal void Dump()
		{
			for (int i = 0; i < this.Count(); i++)
			{
			}
		}

		// Token: 0x0400292D RID: 10541
		internal object[] objects = new object[5];

		// Token: 0x0400292E RID: 10542
		internal string stackId;

		// Token: 0x0400292F RID: 10543
		internal int top = -1;

		// Token: 0x04002930 RID: 10544
		internal int next;
	}
}
