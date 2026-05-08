using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using NVorbis.Contracts;

namespace NVorbis
{
	// Token: 0x02000006 RID: 6
	internal class Floor0 : IFloor
	{
		// Token: 0x0600003A RID: 58 RVA: 0x00002B94 File Offset: 0x00000D94
		public void Init(IPacket packet, int channels, int block0Size, int block1Size, ICodebook[] codebooks)
		{
			this._order = (int)packet.ReadBits(8);
			this._rate = (int)packet.ReadBits(16);
			this._bark_map_size = (int)packet.ReadBits(16);
			this._ampBits = (int)packet.ReadBits(6);
			this._ampOfs = (int)packet.ReadBits(8);
			this._books = new ICodebook[(int)packet.ReadBits(4) + 1];
			if (this._order < 1 || this._rate < 1 || this._bark_map_size < 1 || this._books.Length == 0)
			{
				throw new InvalidDataException();
			}
			this._ampDiv = (1 << this._ampBits) - 1;
			for (int i = 0; i < this._books.Length; i++)
			{
				int num = (int)packet.ReadBits(8);
				if (num < 0 || num >= codebooks.Length)
				{
					throw new InvalidDataException();
				}
				ICodebook codebook = codebooks[num];
				if (codebook.MapType == 0 || codebook.Dimensions < 1)
				{
					throw new InvalidDataException();
				}
				this._books[i] = codebook;
			}
			this._bookBits = Utils.ilog(this._books.Length);
			Dictionary<int, int[]> dictionary = new Dictionary<int, int[]>();
			dictionary[block0Size] = this.SynthesizeBarkCurve(block0Size / 2);
			dictionary[block1Size] = this.SynthesizeBarkCurve(block1Size / 2);
			this._barkMaps = dictionary;
			Dictionary<int, float[]> dictionary2 = new Dictionary<int, float[]>();
			dictionary2[block0Size] = this.SynthesizeWDelMap(block0Size / 2);
			dictionary2[block1Size] = this.SynthesizeWDelMap(block1Size / 2);
			this._wMap = dictionary2;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002D08 File Offset: 0x00000F08
		private int[] SynthesizeBarkCurve(int n)
		{
			float num = (float)this._bark_map_size / Floor0.toBARK((double)(this._rate / 2));
			int[] array = new int[n + 1];
			for (int i = 0; i < n - 1; i++)
			{
				array[i] = Math.Min(this._bark_map_size - 1, (int)Math.Floor((double)(Floor0.toBARK((double)((float)this._rate / 2f / (float)n * (float)i)) * num)));
			}
			array[n] = -1;
			return array;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002D7C File Offset: 0x00000F7C
		private static float toBARK(double lsp)
		{
			return (float)(13.1 * Math.Atan(0.00074 * lsp) + 2.24 * Math.Atan(1.85E-08 * lsp * lsp) + 0.0001 * lsp);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002DD0 File Offset: 0x00000FD0
		private float[] SynthesizeWDelMap(int n)
		{
			float num = (float)(3.141592653589793 / (double)this._bark_map_size);
			float[] array = new float[n];
			for (int i = 0; i < n; i++)
			{
				array[i] = 2f * (float)Math.Cos((double)(num * (float)i));
			}
			return array;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002E18 File Offset: 0x00001018
		public IFloorData Unpack(IPacket packet, int blockSize, int channel)
		{
			Floor0.Data data = new Floor0.Data
			{
				Coeff = new float[this._order + 1]
			};
			data.Amp = packet.ReadBits(this._ampBits);
			if (data.Amp > 0f)
			{
				Array.Clear(data.Coeff, 0, data.Coeff.Length);
				data.Amp = data.Amp / (float)this._ampDiv * (float)this._ampOfs;
				uint num = (uint)packet.ReadBits(this._bookBits);
				if ((ulong)num >= (ulong)((long)this._books.Length))
				{
					data.Amp = 0f;
					return data;
				}
				ICodebook codebook = this._books[(int)num];
				int i = 0;
				while (i < this._order)
				{
					int num2 = codebook.DecodeScalar(packet);
					if (num2 == -1)
					{
						data.Amp = 0f;
						return data;
					}
					int num3 = 0;
					while (i < this._order && num3 < codebook.Dimensions)
					{
						data.Coeff[i] = codebook[num2, num3];
						num3++;
						i++;
					}
				}
				float num4 = 0f;
				int j = 0;
				while (j < this._order)
				{
					int num5 = 0;
					while (j < this._order && num5 < codebook.Dimensions)
					{
						data.Coeff[j] += num4;
						j++;
						num5++;
					}
					num4 = data.Coeff[j - 1];
				}
			}
			return data;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002F80 File Offset: 0x00001180
		public void Apply(IFloorData floorData, int blockSize, float[] residue)
		{
			Floor0.Data data = floorData as Floor0.Data;
			if (data == null)
			{
				throw new ArgumentException("Incorrect packet data!");
			}
			int num = blockSize / 2;
			if (data.Amp > 0f)
			{
				int[] array = this._barkMaps[blockSize];
				float[] array2 = this._wMap[blockSize];
				int i;
				for (i = 0; i < this._order; i++)
				{
					data.Coeff[i] = 2f * (float)Math.Cos((double)data.Coeff[i]);
				}
				i = 0;
				while (i < num)
				{
					int num2 = array[i];
					float num3 = 0.5f;
					float num4 = 0.5f;
					float num5 = array2[num2];
					int j;
					for (j = 1; j < this._order; j += 2)
					{
						num4 *= num5 - data.Coeff[j - 1];
						num3 *= num5 - data.Coeff[j];
					}
					if (j == this._order)
					{
						num4 *= num5 - data.Coeff[j - 1];
						num3 *= num3 * (4f - num5 * num5);
						num4 *= num4;
					}
					else
					{
						num3 *= num3 * (2f - num5);
						num4 *= num4 * (2f + num5);
					}
					num4 = data.Amp / (float)Math.Sqrt((double)(num3 + num4)) - (float)this._ampOfs;
					num4 = (float)Math.Exp((double)(num4 * 0.11512925f));
					residue[i] *= num4;
					while (array[++i] == num2)
					{
						residue[i] *= num4;
					}
				}
				return;
			}
			Array.Clear(residue, 0, num);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003125 File Offset: 0x00001325
		public Floor0()
		{
		}

		// Token: 0x04000011 RID: 17
		private int _order;

		// Token: 0x04000012 RID: 18
		private int _rate;

		// Token: 0x04000013 RID: 19
		private int _bark_map_size;

		// Token: 0x04000014 RID: 20
		private int _ampBits;

		// Token: 0x04000015 RID: 21
		private int _ampOfs;

		// Token: 0x04000016 RID: 22
		private int _ampDiv;

		// Token: 0x04000017 RID: 23
		private ICodebook[] _books;

		// Token: 0x04000018 RID: 24
		private int _bookBits;

		// Token: 0x04000019 RID: 25
		private Dictionary<int, float[]> _wMap;

		// Token: 0x0400001A RID: 26
		private Dictionary<int, int[]> _barkMaps;

		// Token: 0x02000041 RID: 65
		private class Data : IFloorData
		{
			// Token: 0x170000EC RID: 236
			// (get) Token: 0x06000269 RID: 617 RVA: 0x000087D3 File Offset: 0x000069D3
			public bool ExecuteChannel
			{
				get
				{
					return (this.ForceEnergy || this.Amp > 0f) && !this.ForceNoEnergy;
				}
			}

			// Token: 0x170000ED RID: 237
			// (get) Token: 0x0600026A RID: 618 RVA: 0x000087F5 File Offset: 0x000069F5
			// (set) Token: 0x0600026B RID: 619 RVA: 0x000087FD File Offset: 0x000069FD
			public bool ForceEnergy
			{
				[CompilerGenerated]
				get
				{
					return this.<ForceEnergy>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<ForceEnergy>k__BackingField = value;
				}
			}

			// Token: 0x170000EE RID: 238
			// (get) Token: 0x0600026C RID: 620 RVA: 0x00008806 File Offset: 0x00006A06
			// (set) Token: 0x0600026D RID: 621 RVA: 0x0000880E File Offset: 0x00006A0E
			public bool ForceNoEnergy
			{
				[CompilerGenerated]
				get
				{
					return this.<ForceNoEnergy>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<ForceNoEnergy>k__BackingField = value;
				}
			}

			// Token: 0x0600026E RID: 622 RVA: 0x00008817 File Offset: 0x00006A17
			public Data()
			{
			}

			// Token: 0x040000F4 RID: 244
			internal float[] Coeff;

			// Token: 0x040000F5 RID: 245
			internal float Amp;

			// Token: 0x040000F6 RID: 246
			[CompilerGenerated]
			private bool <ForceEnergy>k__BackingField;

			// Token: 0x040000F7 RID: 247
			[CompilerGenerated]
			private bool <ForceNoEnergy>k__BackingField;
		}
	}
}
