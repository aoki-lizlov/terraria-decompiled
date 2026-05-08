using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NVorbis.Contracts;

namespace NVorbis
{
	// Token: 0x02000008 RID: 8
	internal class Huffman : IHuffman, IComparer<HuffmanListNode>
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00003947 File Offset: 0x00001B47
		// (set) Token: 0x0600004A RID: 74 RVA: 0x0000394F File Offset: 0x00001B4F
		public int TableBits
		{
			[CompilerGenerated]
			get
			{
				return this.<TableBits>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<TableBits>k__BackingField = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00003958 File Offset: 0x00001B58
		// (set) Token: 0x0600004C RID: 76 RVA: 0x00003960 File Offset: 0x00001B60
		public IList<HuffmanListNode> PrefixTree
		{
			[CompilerGenerated]
			get
			{
				return this.<PrefixTree>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<PrefixTree>k__BackingField = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00003969 File Offset: 0x00001B69
		// (set) Token: 0x0600004E RID: 78 RVA: 0x00003971 File Offset: 0x00001B71
		public IList<HuffmanListNode> OverflowList
		{
			[CompilerGenerated]
			get
			{
				return this.<OverflowList>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<OverflowList>k__BackingField = value;
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x0000397C File Offset: 0x00001B7C
		public void GenerateTable(IList<int> values, int[] lengthList, int[] codeList)
		{
			HuffmanListNode[] array = new HuffmanListNode[lengthList.Length];
			int num = 0;
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new HuffmanListNode
				{
					Value = values[i],
					Length = ((lengthList[i] <= 0) ? 99999 : lengthList[i]),
					Bits = codeList[i],
					Mask = (1 << lengthList[i]) - 1
				};
				if (lengthList[i] > 0 && num < lengthList[i])
				{
					num = lengthList[i];
				}
			}
			Array.Sort<HuffmanListNode>(array, 0, array.Length, this);
			int num2 = ((num > 10) ? 10 : num);
			List<HuffmanListNode> list = new List<HuffmanListNode>(1 << num2);
			List<HuffmanListNode> list2 = null;
			for (int j = 0; j < array.Length; j++)
			{
				if (array[j].Length >= 99999)
				{
					break;
				}
				int length = array[j].Length;
				if (length > num2)
				{
					list2 = new List<HuffmanListNode>(array.Length - j);
					while (j < array.Length)
					{
						if (array[j].Length >= 99999)
						{
							break;
						}
						list2.Add(array[j]);
						j++;
					}
				}
				else
				{
					int num3 = 1 << num2 - length;
					HuffmanListNode huffmanListNode = array[j];
					for (int k = 0; k < num3; k++)
					{
						int num4 = (k << length) | huffmanListNode.Bits;
						while (list.Count <= num4)
						{
							list.Add(null);
						}
						list[num4] = huffmanListNode;
					}
				}
			}
			while (list.Count < 1 << num2)
			{
				list.Add(null);
			}
			this.TableBits = num2;
			this.PrefixTree = list;
			this.OverflowList = list2;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003B18 File Offset: 0x00001D18
		int IComparer<HuffmanListNode>.Compare(HuffmanListNode x, HuffmanListNode y)
		{
			int num = x.Length - y.Length;
			if (num == 0)
			{
				return x.Bits - y.Bits;
			}
			return num;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003B45 File Offset: 0x00001D45
		public Huffman()
		{
		}

		// Token: 0x0400002C RID: 44
		private const int MAX_TABLE_BITS = 10;

		// Token: 0x0400002D RID: 45
		[CompilerGenerated]
		private int <TableBits>k__BackingField;

		// Token: 0x0400002E RID: 46
		[CompilerGenerated]
		private IList<HuffmanListNode> <PrefixTree>k__BackingField;

		// Token: 0x0400002F RID: 47
		[CompilerGenerated]
		private IList<HuffmanListNode> <OverflowList>k__BackingField;
	}
}
