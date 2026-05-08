using System;

namespace System
{
	// Token: 0x02000196 RID: 406
	[Serializable]
	public readonly struct ConsoleKeyInfo
	{
		// Token: 0x06001322 RID: 4898 RVA: 0x0004E308 File Offset: 0x0004C508
		public ConsoleKeyInfo(char keyChar, ConsoleKey key, bool shift, bool alt, bool control)
		{
			if (key < (ConsoleKey)0 || key > (ConsoleKey)255)
			{
				throw new ArgumentOutOfRangeException("key", "Console key values must be between 0 and 255 inclusive.");
			}
			this._keyChar = keyChar;
			this._key = key;
			this._mods = (ConsoleModifiers)0;
			if (shift)
			{
				this._mods |= ConsoleModifiers.Shift;
			}
			if (alt)
			{
				this._mods |= ConsoleModifiers.Alt;
			}
			if (control)
			{
				this._mods |= ConsoleModifiers.Control;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06001323 RID: 4899 RVA: 0x0004E37B File Offset: 0x0004C57B
		public char KeyChar
		{
			get
			{
				return this._keyChar;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06001324 RID: 4900 RVA: 0x0004E383 File Offset: 0x0004C583
		public ConsoleKey Key
		{
			get
			{
				return this._key;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06001325 RID: 4901 RVA: 0x0004E38B File Offset: 0x0004C58B
		public ConsoleModifiers Modifiers
		{
			get
			{
				return this._mods;
			}
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x0004E393 File Offset: 0x0004C593
		public override bool Equals(object value)
		{
			return value is ConsoleKeyInfo && this.Equals((ConsoleKeyInfo)value);
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x0004E3AB File Offset: 0x0004C5AB
		public bool Equals(ConsoleKeyInfo obj)
		{
			return obj._keyChar == this._keyChar && obj._key == this._key && obj._mods == this._mods;
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x0004E3D9 File Offset: 0x0004C5D9
		public static bool operator ==(ConsoleKeyInfo a, ConsoleKeyInfo b)
		{
			return a.Equals(b);
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x0004E3E3 File Offset: 0x0004C5E3
		public static bool operator !=(ConsoleKeyInfo a, ConsoleKeyInfo b)
		{
			return !(a == b);
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x0004E3EF File Offset: 0x0004C5EF
		public override int GetHashCode()
		{
			return (int)((ConsoleKey)this._keyChar | ((int)this._key << 16) | (ConsoleKey)((int)this._mods << 24));
		}

		// Token: 0x04001321 RID: 4897
		private readonly char _keyChar;

		// Token: 0x04001322 RID: 4898
		private readonly ConsoleKey _key;

		// Token: 0x04001323 RID: 4899
		private readonly ConsoleModifiers _mods;
	}
}
