using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace ReLogic.Peripherals.RGB
{
	// Token: 0x02000020 RID: 32
	internal class HotkeyCollection : IEnumerable<RgbKey>, IEnumerable
	{
		// Token: 0x060000ED RID: 237 RVA: 0x0000470F File Offset: 0x0000290F
		public RgbKey BindKey(Keys key, string keyTriggerName)
		{
			if (!this._keys.ContainsKey(key))
			{
				this._keys.Add(key, new RgbKey(key, keyTriggerName));
			}
			return this._keys[key];
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000473E File Offset: 0x0000293E
		public void UnbindKey(Keys key)
		{
			this._keys.Remove(key);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000474D File Offset: 0x0000294D
		public IEnumerator<RgbKey> GetEnumerator()
		{
			return this._keys.Values.GetEnumerator();
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000474D File Offset: 0x0000294D
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._keys.Values.GetEnumerator();
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00004764 File Offset: 0x00002964
		public void UpdateAll(float timeElapsed)
		{
			foreach (RgbKey rgbKey in this._keys.Values)
			{
				rgbKey.Update(timeElapsed);
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000047BC File Offset: 0x000029BC
		public HotkeyCollection()
		{
		}

		// Token: 0x04000049 RID: 73
		private Dictionary<Keys, RgbKey> _keys = new Dictionary<Keys, RgbKey>();
	}
}
