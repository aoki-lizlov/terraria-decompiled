using System;
using System.Collections.Generic;

namespace Microsoft.Xna.Framework.Input
{
	// Token: 0x02000067 RID: 103
	public struct KeyboardState
	{
		// Token: 0x170001C9 RID: 457
		public KeyState this[Keys key]
		{
			get
			{
				if (!this.InternalGetKey(key))
				{
					return KeyState.Up;
				}
				return KeyState.Down;
			}
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x00022894 File Offset: 0x00020A94
		public KeyboardState(params Keys[] keys)
		{
			this.keys0 = 0U;
			this.keys1 = 0U;
			this.keys2 = 0U;
			this.keys3 = 0U;
			this.keys4 = 0U;
			this.keys5 = 0U;
			this.keys6 = 0U;
			this.keys7 = 0U;
			if (keys != null)
			{
				foreach (Keys keys2 in keys)
				{
					this.AddPressedKey((int)keys2);
				}
			}
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x000228F8 File Offset: 0x00020AF8
		internal KeyboardState(List<Keys> keys)
		{
			this.keys0 = 0U;
			this.keys1 = 0U;
			this.keys2 = 0U;
			this.keys3 = 0U;
			this.keys4 = 0U;
			this.keys5 = 0U;
			this.keys6 = 0U;
			this.keys7 = 0U;
			if (keys != null)
			{
				foreach (Keys keys2 in keys)
				{
					this.AddPressedKey((int)keys2);
				}
			}
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x00022984 File Offset: 0x00020B84
		public bool IsKeyDown(Keys key)
		{
			return this.InternalGetKey(key);
		}

		// Token: 0x06001071 RID: 4209 RVA: 0x0002298D File Offset: 0x00020B8D
		public bool IsKeyUp(Keys key)
		{
			return !this.InternalGetKey(key);
		}

		// Token: 0x06001072 RID: 4210 RVA: 0x0002299C File Offset: 0x00020B9C
		public Keys[] GetPressedKeys()
		{
			uint num = KeyboardState.CountBits(this.keys0) + KeyboardState.CountBits(this.keys1) + KeyboardState.CountBits(this.keys2) + KeyboardState.CountBits(this.keys3) + KeyboardState.CountBits(this.keys4) + KeyboardState.CountBits(this.keys5) + KeyboardState.CountBits(this.keys6) + KeyboardState.CountBits(this.keys7);
			if (num == 0U)
			{
				return KeyboardState.empty;
			}
			Keys[] array = new Keys[num];
			int num2 = 0;
			if (this.keys0 != 0U)
			{
				num2 = KeyboardState.AddKeysToArray(this.keys0, 0, array, num2);
			}
			if (this.keys1 != 0U)
			{
				num2 = KeyboardState.AddKeysToArray(this.keys1, 32, array, num2);
			}
			if (this.keys2 != 0U)
			{
				num2 = KeyboardState.AddKeysToArray(this.keys2, 64, array, num2);
			}
			if (this.keys3 != 0U)
			{
				num2 = KeyboardState.AddKeysToArray(this.keys3, 96, array, num2);
			}
			if (this.keys4 != 0U)
			{
				num2 = KeyboardState.AddKeysToArray(this.keys4, 128, array, num2);
			}
			if (this.keys5 != 0U)
			{
				num2 = KeyboardState.AddKeysToArray(this.keys5, 160, array, num2);
			}
			if (this.keys6 != 0U)
			{
				num2 = KeyboardState.AddKeysToArray(this.keys6, 192, array, num2);
			}
			if (this.keys7 != 0U)
			{
				num2 = KeyboardState.AddKeysToArray(this.keys7, 224, array, num2);
			}
			return array;
		}

		// Token: 0x06001073 RID: 4211 RVA: 0x00022AE8 File Offset: 0x00020CE8
		private bool InternalGetKey(Keys key)
		{
			uint num = 1U << (int)key;
			uint num2;
			switch (key >> 5)
			{
			case Keys.None:
				num2 = this.keys0;
				break;
			case (Keys)1:
				num2 = this.keys1;
				break;
			case (Keys)2:
				num2 = this.keys2;
				break;
			case (Keys)3:
				num2 = this.keys3;
				break;
			case (Keys)4:
				num2 = this.keys4;
				break;
			case (Keys)5:
				num2 = this.keys5;
				break;
			case (Keys)6:
				num2 = this.keys6;
				break;
			case (Keys)7:
				num2 = this.keys7;
				break;
			default:
				num2 = 0U;
				break;
			}
			return (num2 & num) > 0U;
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x00022B7C File Offset: 0x00020D7C
		private void AddPressedKey(int key)
		{
			uint num = 1U << key;
			switch (key >> 5)
			{
			case 0:
				this.keys0 |= num;
				return;
			case 1:
				this.keys1 |= num;
				return;
			case 2:
				this.keys2 |= num;
				return;
			case 3:
				this.keys3 |= num;
				return;
			case 4:
				this.keys4 |= num;
				return;
			case 5:
				this.keys5 |= num;
				return;
			case 6:
				this.keys6 |= num;
				return;
			case 7:
				this.keys7 |= num;
				return;
			default:
				return;
			}
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x00022C38 File Offset: 0x00020E38
		private void RemovePressedKey(int key)
		{
			uint num = 1U << key;
			switch (key >> 5)
			{
			case 0:
				this.keys0 &= ~num;
				return;
			case 1:
				this.keys1 &= ~num;
				return;
			case 2:
				this.keys2 &= ~num;
				return;
			case 3:
				this.keys3 &= ~num;
				return;
			case 4:
				this.keys4 &= ~num;
				return;
			case 5:
				this.keys5 &= ~num;
				return;
			case 6:
				this.keys6 &= ~num;
				return;
			case 7:
				this.keys7 &= ~num;
				return;
			default:
				return;
			}
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x00022CF9 File Offset: 0x00020EF9
		public override int GetHashCode()
		{
			return (int)(this.keys0 ^ this.keys1 ^ this.keys2 ^ this.keys3 ^ this.keys4 ^ this.keys5 ^ this.keys6 ^ this.keys7);
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x00022D34 File Offset: 0x00020F34
		public static bool operator ==(KeyboardState a, KeyboardState b)
		{
			return a.keys0 == b.keys0 && a.keys1 == b.keys1 && a.keys2 == b.keys2 && a.keys3 == b.keys3 && a.keys4 == b.keys4 && a.keys5 == b.keys5 && a.keys6 == b.keys6 && a.keys7 == b.keys7;
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x00022DB3 File Offset: 0x00020FB3
		public static bool operator !=(KeyboardState a, KeyboardState b)
		{
			return !(a == b);
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x00022DBF File Offset: 0x00020FBF
		public override bool Equals(object obj)
		{
			return obj is KeyboardState && this == (KeyboardState)obj;
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x00022DDC File Offset: 0x00020FDC
		private static uint CountBits(uint v)
		{
			v -= (v >> 1) & 1431655765U;
			v = (v & 858993459U) + ((v >> 2) & 858993459U);
			return ((v + (v >> 4)) & 252645135U) * 16843009U >> 24;
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x00022E14 File Offset: 0x00021014
		private static int AddKeysToArray(uint keys, int offset, Keys[] pressedKeys, int index)
		{
			for (int i = 0; i < 32; i++)
			{
				if (((ulong)keys & (ulong)(1L << (i & 31))) != 0UL)
				{
					pressedKeys[index++] = (Keys)(offset + i);
				}
			}
			return index;
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x00022E46 File Offset: 0x00021046
		// Note: this type is marked as 'beforefieldinit'.
		static KeyboardState()
		{
		}

		// Token: 0x040006B0 RID: 1712
		private uint keys0;

		// Token: 0x040006B1 RID: 1713
		private uint keys1;

		// Token: 0x040006B2 RID: 1714
		private uint keys2;

		// Token: 0x040006B3 RID: 1715
		private uint keys3;

		// Token: 0x040006B4 RID: 1716
		private uint keys4;

		// Token: 0x040006B5 RID: 1717
		private uint keys5;

		// Token: 0x040006B6 RID: 1718
		private uint keys6;

		// Token: 0x040006B7 RID: 1719
		private uint keys7;

		// Token: 0x040006B8 RID: 1720
		private static Keys[] empty = new Keys[0];
	}
}
