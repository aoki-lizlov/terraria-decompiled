using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200068B RID: 1675
	[Serializable]
	internal sealed class SizedArray : ICloneable
	{
		// Token: 0x06003F49 RID: 16201 RVA: 0x000DEF3E File Offset: 0x000DD13E
		internal SizedArray()
		{
			this.objects = new object[16];
			this.negObjects = new object[4];
		}

		// Token: 0x06003F4A RID: 16202 RVA: 0x000DEF5F File Offset: 0x000DD15F
		internal SizedArray(int length)
		{
			this.objects = new object[length];
			this.negObjects = new object[length];
		}

		// Token: 0x06003F4B RID: 16203 RVA: 0x000DEF80 File Offset: 0x000DD180
		private SizedArray(SizedArray sizedArray)
		{
			this.objects = new object[sizedArray.objects.Length];
			sizedArray.objects.CopyTo(this.objects, 0);
			this.negObjects = new object[sizedArray.negObjects.Length];
			sizedArray.negObjects.CopyTo(this.negObjects, 0);
		}

		// Token: 0x06003F4C RID: 16204 RVA: 0x000DEFDD File Offset: 0x000DD1DD
		public object Clone()
		{
			return new SizedArray(this);
		}

		// Token: 0x170009AE RID: 2478
		internal object this[int index]
		{
			get
			{
				if (index < 0)
				{
					if (-index > this.negObjects.Length - 1)
					{
						return null;
					}
					return this.negObjects[-index];
				}
				else
				{
					if (index > this.objects.Length - 1)
					{
						return null;
					}
					return this.objects[index];
				}
			}
			set
			{
				if (index < 0)
				{
					if (-index > this.negObjects.Length - 1)
					{
						this.IncreaseCapacity(index);
					}
					this.negObjects[-index] = value;
					return;
				}
				if (index > this.objects.Length - 1)
				{
					this.IncreaseCapacity(index);
				}
				object obj = this.objects[index];
				this.objects[index] = value;
			}
		}

		// Token: 0x06003F4F RID: 16207 RVA: 0x000DF074 File Offset: 0x000DD274
		internal void IncreaseCapacity(int index)
		{
			try
			{
				if (index < 0)
				{
					object[] array = new object[Math.Max(this.negObjects.Length * 2, -index + 1)];
					Array.Copy(this.negObjects, 0, array, 0, this.negObjects.Length);
					this.negObjects = array;
				}
				else
				{
					object[] array2 = new object[Math.Max(this.objects.Length * 2, index + 1)];
					Array.Copy(this.objects, 0, array2, 0, this.objects.Length);
					this.objects = array2;
				}
			}
			catch (Exception)
			{
				throw new SerializationException(Environment.GetResourceString("Invalid BinaryFormatter stream."));
			}
		}

		// Token: 0x04002931 RID: 10545
		internal object[] objects;

		// Token: 0x04002932 RID: 10546
		internal object[] negObjects;
	}
}
