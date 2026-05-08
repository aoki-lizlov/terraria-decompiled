using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Terraria.ID;

namespace Terraria.GameContent.Creative
{
	// Token: 0x0200031B RID: 795
	public class CreativeItemSacrificesCatalog
	{
		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06002783 RID: 10115 RVA: 0x00567F2E File Offset: 0x0056612E
		public Dictionary<int, int> SacrificeCountNeededByItemId
		{
			get
			{
				return this._sacrificeCountNeededByItemId;
			}
		}

		// Token: 0x06002784 RID: 10116 RVA: 0x00567F38 File Offset: 0x00566138
		public void Initialize()
		{
			this._sacrificeCountNeededByItemId.Clear();
			foreach (string text in Regex.Split(Utils.ReadEmbeddedResource("Terraria.GameContent.Creative.Content.Sacrifices.tsv"), "\r\n|\r|\n"))
			{
				if (!text.StartsWith("//"))
				{
					string[] array2 = text.Split(new char[] { '\t' });
					int num;
					if (array2.Length >= 3 && ItemID.Search.TryGetId(array2[0], ref num))
					{
						int num2 = 0;
						bool flag = false;
						string text2 = array2[1].ToLower();
						uint num3 = <PrivateImplementationDetails>.ComputeStringHash(text2);
						if (num3 <= 3876335077U)
						{
							if (num3 <= 3792446982U)
							{
								if (num3 <= 3758891744U)
								{
									if (num3 != 2166136261U)
									{
										if (num3 != 3758891744U)
										{
											goto IL_0352;
										}
										if (!(text2 == "e"))
										{
											goto IL_0352;
										}
										flag = true;
										goto IL_036C;
									}
									else
									{
										if (text2 == null)
										{
											goto IL_0352;
										}
										if (text2.Length != 0)
										{
											goto IL_0352;
										}
									}
								}
								else if (num3 != 3775669363U)
								{
									if (num3 != 3792446982U)
									{
										goto IL_0352;
									}
									if (!(text2 == "g"))
									{
										goto IL_0352;
									}
									num2 = 3;
									goto IL_036C;
								}
								else
								{
									if (!(text2 == "d"))
									{
										goto IL_0352;
									}
									num2 = 1;
									goto IL_036C;
								}
							}
							else if (num3 <= 3826002220U)
							{
								if (num3 != 3809224601U)
								{
									if (num3 != 3826002220U)
									{
										goto IL_0352;
									}
									if (!(text2 == "a"))
									{
										goto IL_0352;
									}
								}
								else
								{
									if (!(text2 == "f"))
									{
										goto IL_0352;
									}
									num2 = 2;
									goto IL_036C;
								}
							}
							else if (num3 != 3859557458U)
							{
								if (num3 != 3876335077U)
								{
									goto IL_0352;
								}
								if (!(text2 == "b"))
								{
									goto IL_0352;
								}
								num2 = 25;
								goto IL_036C;
							}
							else
							{
								if (!(text2 == "c"))
								{
									goto IL_0352;
								}
								num2 = 5;
								goto IL_036C;
							}
							num2 = 50;
						}
						else if (num3 <= 3943445553U)
						{
							if (num3 <= 3909890315U)
							{
								if (num3 != 3893112696U)
								{
									if (num3 != 3909890315U)
									{
										goto IL_0352;
									}
									if (!(text2 == "l"))
									{
										goto IL_0352;
									}
									num2 = 100;
								}
								else
								{
									if (!(text2 == "m"))
									{
										goto IL_0352;
									}
									num2 = 200;
								}
							}
							else if (num3 != 3926667934U)
							{
								if (num3 != 3943445553U)
								{
									goto IL_0352;
								}
								if (!(text2 == "n"))
								{
									goto IL_0352;
								}
								num2 = 20;
							}
							else
							{
								if (!(text2 == "o"))
								{
									goto IL_0352;
								}
								num2 = 400;
							}
						}
						else if (num3 <= 3977000791U)
						{
							if (num3 != 3960223172U)
							{
								if (num3 != 3977000791U)
								{
									goto IL_0352;
								}
								if (!(text2 == "h"))
								{
									goto IL_0352;
								}
								num2 = 10;
							}
							else
							{
								if (!(text2 == "i"))
								{
									goto IL_0352;
								}
								num2 = 15;
							}
						}
						else if (num3 != 3993778410U)
						{
							if (num3 != 4010556029U)
							{
								goto IL_0352;
							}
							if (!(text2 == "j"))
							{
								goto IL_0352;
							}
							num2 = 30;
						}
						else
						{
							if (!(text2 == "k"))
							{
								goto IL_0352;
							}
							num2 = 99;
						}
						IL_036C:
						if (!flag)
						{
							this._sacrificeCountNeededByItemId[num] = num2;
							goto IL_037F;
						}
						goto IL_037F;
						IL_0352:
						throw new Exception("There is no category for this item: " + array2[0] + ", category: " + text2);
					}
				}
				IL_037F:;
			}
		}

		// Token: 0x06002785 RID: 10117 RVA: 0x005682D4 File Offset: 0x005664D4
		public bool TryGetSacrificeCountCapToUnlockInfiniteItems(int itemId, out int amountNeeded)
		{
			int num;
			if (ContentSamples.CreativeResearchItemPersistentIdOverride.TryGetValue(itemId, out num))
			{
				itemId = num;
			}
			return this._sacrificeCountNeededByItemId.TryGetValue(itemId, out amountNeeded);
		}

		// Token: 0x06002786 RID: 10118 RVA: 0x00568300 File Offset: 0x00566500
		public CreativeItemSacrificesCatalog()
		{
		}

		// Token: 0x06002787 RID: 10119 RVA: 0x00568313 File Offset: 0x00566513
		// Note: this type is marked as 'beforefieldinit'.
		static CreativeItemSacrificesCatalog()
		{
		}

		// Token: 0x040050EB RID: 20715
		public static CreativeItemSacrificesCatalog Instance = new CreativeItemSacrificesCatalog();

		// Token: 0x040050EC RID: 20716
		private Dictionary<int, int> _sacrificeCountNeededByItemId = new Dictionary<int, int>();
	}
}
