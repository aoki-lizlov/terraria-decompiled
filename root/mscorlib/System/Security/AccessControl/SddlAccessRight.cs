using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Security.AccessControl
{
	// Token: 0x0200051A RID: 1306
	internal class SddlAccessRight
	{
		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06003524 RID: 13604 RVA: 0x000C126C File Offset: 0x000BF46C
		// (set) Token: 0x06003525 RID: 13605 RVA: 0x000C1274 File Offset: 0x000BF474
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Name>k__BackingField = value;
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06003526 RID: 13606 RVA: 0x000C127D File Offset: 0x000BF47D
		// (set) Token: 0x06003527 RID: 13607 RVA: 0x000C1285 File Offset: 0x000BF485
		public int Value
		{
			[CompilerGenerated]
			get
			{
				return this.<Value>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Value>k__BackingField = value;
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06003528 RID: 13608 RVA: 0x000C128E File Offset: 0x000BF48E
		// (set) Token: 0x06003529 RID: 13609 RVA: 0x000C1296 File Offset: 0x000BF496
		public int ObjectType
		{
			[CompilerGenerated]
			get
			{
				return this.<ObjectType>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ObjectType>k__BackingField = value;
			}
		}

		// Token: 0x0600352A RID: 13610 RVA: 0x000C12A0 File Offset: 0x000BF4A0
		public static SddlAccessRight LookupByName(string s)
		{
			foreach (SddlAccessRight sddlAccessRight in SddlAccessRight.rights)
			{
				if (sddlAccessRight.Name == s)
				{
					return sddlAccessRight;
				}
			}
			return null;
		}

		// Token: 0x0600352B RID: 13611 RVA: 0x000C12D8 File Offset: 0x000BF4D8
		public static SddlAccessRight[] Decompose(int mask)
		{
			foreach (SddlAccessRight sddlAccessRight in SddlAccessRight.rights)
			{
				if (mask == sddlAccessRight.Value)
				{
					return new SddlAccessRight[] { sddlAccessRight };
				}
			}
			int num = 0;
			List<SddlAccessRight> list = new List<SddlAccessRight>();
			int num2 = 0;
			foreach (SddlAccessRight sddlAccessRight2 in SddlAccessRight.rights)
			{
				if ((mask & sddlAccessRight2.Value) == sddlAccessRight2.Value && (num2 | sddlAccessRight2.Value) != num2)
				{
					if (num == 0)
					{
						num = sddlAccessRight2.ObjectType;
					}
					if (sddlAccessRight2.ObjectType != 0 && num != sddlAccessRight2.ObjectType)
					{
						return null;
					}
					list.Add(sddlAccessRight2);
					num2 |= sddlAccessRight2.Value;
				}
				if (num2 == mask)
				{
					return list.ToArray();
				}
			}
			return null;
		}

		// Token: 0x0600352C RID: 13612 RVA: 0x000025BE File Offset: 0x000007BE
		public SddlAccessRight()
		{
		}

		// Token: 0x0600352D RID: 13613 RVA: 0x000C13A0 File Offset: 0x000BF5A0
		// Note: this type is marked as 'beforefieldinit'.
		static SddlAccessRight()
		{
		}

		// Token: 0x0400247F RID: 9343
		[CompilerGenerated]
		private string <Name>k__BackingField;

		// Token: 0x04002480 RID: 9344
		[CompilerGenerated]
		private int <Value>k__BackingField;

		// Token: 0x04002481 RID: 9345
		[CompilerGenerated]
		private int <ObjectType>k__BackingField;

		// Token: 0x04002482 RID: 9346
		private static readonly SddlAccessRight[] rights = new SddlAccessRight[]
		{
			new SddlAccessRight
			{
				Name = "CC",
				Value = 1,
				ObjectType = 1
			},
			new SddlAccessRight
			{
				Name = "DC",
				Value = 2,
				ObjectType = 1
			},
			new SddlAccessRight
			{
				Name = "LC",
				Value = 4,
				ObjectType = 1
			},
			new SddlAccessRight
			{
				Name = "SW",
				Value = 8,
				ObjectType = 1
			},
			new SddlAccessRight
			{
				Name = "RP",
				Value = 16,
				ObjectType = 1
			},
			new SddlAccessRight
			{
				Name = "WP",
				Value = 32,
				ObjectType = 1
			},
			new SddlAccessRight
			{
				Name = "DT",
				Value = 64,
				ObjectType = 1
			},
			new SddlAccessRight
			{
				Name = "LO",
				Value = 128,
				ObjectType = 1
			},
			new SddlAccessRight
			{
				Name = "CR",
				Value = 256,
				ObjectType = 1
			},
			new SddlAccessRight
			{
				Name = "SD",
				Value = 65536
			},
			new SddlAccessRight
			{
				Name = "RC",
				Value = 131072
			},
			new SddlAccessRight
			{
				Name = "WD",
				Value = 262144
			},
			new SddlAccessRight
			{
				Name = "WO",
				Value = 524288
			},
			new SddlAccessRight
			{
				Name = "GA",
				Value = 268435456
			},
			new SddlAccessRight
			{
				Name = "GX",
				Value = 536870912
			},
			new SddlAccessRight
			{
				Name = "GW",
				Value = 1073741824
			},
			new SddlAccessRight
			{
				Name = "GR",
				Value = int.MinValue
			},
			new SddlAccessRight
			{
				Name = "FA",
				Value = 2032127,
				ObjectType = 2
			},
			new SddlAccessRight
			{
				Name = "FR",
				Value = 1179785,
				ObjectType = 2
			},
			new SddlAccessRight
			{
				Name = "FW",
				Value = 1179926,
				ObjectType = 2
			},
			new SddlAccessRight
			{
				Name = "FX",
				Value = 1179808,
				ObjectType = 2
			},
			new SddlAccessRight
			{
				Name = "KA",
				Value = 983103,
				ObjectType = 3
			},
			new SddlAccessRight
			{
				Name = "KR",
				Value = 131097,
				ObjectType = 3
			},
			new SddlAccessRight
			{
				Name = "KW",
				Value = 131078,
				ObjectType = 3
			},
			new SddlAccessRight
			{
				Name = "KX",
				Value = 131097,
				ObjectType = 3
			},
			new SddlAccessRight
			{
				Name = "NW",
				Value = 1
			},
			new SddlAccessRight
			{
				Name = "NR",
				Value = 2
			},
			new SddlAccessRight
			{
				Name = "NX",
				Value = 4
			}
		};
	}
}
