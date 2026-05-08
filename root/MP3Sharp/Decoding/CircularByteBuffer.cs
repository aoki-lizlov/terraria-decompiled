using System;

namespace XPT.Core.Audio.MP3Sharp.Decoding
{
	// Token: 0x02000010 RID: 16
	[Serializable]
	internal class CircularByteBuffer
	{
		// Token: 0x06000086 RID: 134 RVA: 0x00004101 File Offset: 0x00002301
		internal CircularByteBuffer(int size)
		{
			this._DataArray = new byte[size];
			this._Length = size;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000411C File Offset: 0x0000231C
		internal CircularByteBuffer(CircularByteBuffer cdb)
		{
			lock (cdb)
			{
				this._Length = cdb._Length;
				this._NumValid = cdb._NumValid;
				this._Index = cdb._Index;
				this._DataArray = new byte[this._Length];
				for (int i = 0; i < this._Length; i++)
				{
					this._DataArray[i] = cdb._DataArray[i];
				}
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000088 RID: 136 RVA: 0x000041B0 File Offset: 0x000023B0
		// (set) Token: 0x06000089 RID: 137 RVA: 0x000041B8 File Offset: 0x000023B8
		internal int BufferSize
		{
			get
			{
				return this._Length;
			}
			set
			{
				byte[] array = new byte[value];
				int num = ((this._Length > value) ? value : this._Length);
				for (int i = 0; i < num; i++)
				{
					array[i] = this.InternalGet(i - this._Length + 1);
				}
				this._DataArray = array;
				this._Index = num - 1;
				this._Length = value;
			}
		}

		// Token: 0x1700000E RID: 14
		internal byte this[int index]
		{
			get
			{
				return this.InternalGet(-1 - index);
			}
			set
			{
				this.InternalSet(-1 - index, value);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600008C RID: 140 RVA: 0x0000422C File Offset: 0x0000242C
		// (set) Token: 0x0600008D RID: 141 RVA: 0x00004234 File Offset: 0x00002434
		internal int NumValid
		{
			get
			{
				return this._NumValid;
			}
			set
			{
				if (value > this._NumValid)
				{
					throw new Exception(string.Concat(new object[] { "Can't set NumValid to ", value, " which is greater than the current numValid value of ", this._NumValid }));
				}
				this._NumValid = value;
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004289 File Offset: 0x00002489
		internal CircularByteBuffer Copy()
		{
			return new CircularByteBuffer(this);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004291 File Offset: 0x00002491
		internal void Reset()
		{
			this._Index = 0;
			this._NumValid = 0;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000042A4 File Offset: 0x000024A4
		internal byte Push(byte newValue)
		{
			byte b;
			lock (this)
			{
				b = this.InternalGet(this._Length);
				this._DataArray[this._Index] = newValue;
				this._NumValid++;
				if (this._NumValid > this._Length)
				{
					this._NumValid = this._Length;
				}
				this._Index++;
				this._Index %= this._Length;
			}
			return b;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004340 File Offset: 0x00002540
		internal byte Pop()
		{
			byte b;
			lock (this)
			{
				if (this._NumValid == 0)
				{
					throw new Exception("Can't pop off an empty CircularByteBuffer");
				}
				this._NumValid--;
				b = this[this._NumValid];
			}
			return b;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000043A4 File Offset: 0x000025A4
		internal byte Peek()
		{
			byte b;
			lock (this)
			{
				b = this.InternalGet(this._Length);
			}
			return b;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000043E8 File Offset: 0x000025E8
		private byte InternalGet(int offset)
		{
			int i;
			for (i = this._Index + offset; i >= this._Length; i -= this._Length)
			{
			}
			while (i < 0)
			{
				i += this._Length;
			}
			return this._DataArray[i];
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000442C File Offset: 0x0000262C
		private void InternalSet(int offset, byte valueToSet)
		{
			int i;
			for (i = this._Index + offset; i > this._Length; i -= this._Length)
			{
			}
			while (i < 0)
			{
				i += this._Length;
			}
			this._DataArray[i] = valueToSet;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004470 File Offset: 0x00002670
		internal byte[] GetRange(int str, int stp)
		{
			byte[] array = new byte[str - stp + 1];
			int i = str;
			int num = 0;
			while (i >= stp)
			{
				array[num] = this[i];
				i--;
				num++;
			}
			return array;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000044A8 File Offset: 0x000026A8
		public override string ToString()
		{
			string text = "";
			for (int i = 0; i < this._DataArray.Length; i++)
			{
				text = text + this._DataArray[i] + " ";
			}
			return string.Concat(new object[] { text, "\n index = ", this._Index, " numValid = ", this.NumValid });
		}

		// Token: 0x0400004A RID: 74
		private byte[] _DataArray;

		// Token: 0x0400004B RID: 75
		private int _Index;

		// Token: 0x0400004C RID: 76
		private int _Length;

		// Token: 0x0400004D RID: 77
		private int _NumValid;
	}
}
