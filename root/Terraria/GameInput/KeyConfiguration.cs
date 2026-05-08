using System;
using System.Collections.Generic;
using System.Linq;

namespace Terraria.GameInput
{
	// Token: 0x0200008C RID: 140
	public class KeyConfiguration
	{
		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060015D3 RID: 5587 RVA: 0x004D36C4 File Offset: 0x004D18C4
		public bool DoGrappleAndInteractShareTheSameKey
		{
			get
			{
				return this.KeyStatus["Grapple"].Count > 0 && this.KeyStatus["MouseRight"].Count > 0 && this.KeyStatus["MouseRight"].Contains(this.KeyStatus["Grapple"][0]);
			}
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x004D3730 File Offset: 0x004D1930
		public void SetupKeys()
		{
			this.KeyStatus.Clear();
			foreach (string text in PlayerInput.KnownTriggers)
			{
				this.KeyStatus.Add(text, new List<string>());
			}
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x004D3798 File Offset: 0x004D1998
		public void Processkey(TriggersSet set, string newKey, InputMode mode)
		{
			foreach (KeyValuePair<string, List<string>> keyValuePair in this.KeyStatus)
			{
				if (keyValuePair.Value.Contains(newKey))
				{
					set.KeyStatus[keyValuePair.Key] = true;
					set.LatestInputMode[keyValuePair.Key] = mode;
				}
			}
			if (set.Up || set.Down || set.Left || set.Right || set.HotbarPlus || set.HotbarMinus || ((Main.gameMenu || Main.ingameOptionsWindow) && (set.MenuUp || set.MenuDown || set.MenuLeft || set.MenuRight)))
			{
				set.UsedMovementKey = true;
			}
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x004D3880 File Offset: 0x004D1A80
		public void CopyKeyState(TriggersSet oldSet, TriggersSet newSet, string newKey)
		{
			foreach (KeyValuePair<string, List<string>> keyValuePair in this.KeyStatus)
			{
				if (keyValuePair.Value.Contains(newKey))
				{
					newSet.KeyStatus[keyValuePair.Key] = oldSet.KeyStatus[keyValuePair.Key];
				}
			}
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x004D3900 File Offset: 0x004D1B00
		public void ReadPreferences(Dictionary<string, List<string>> dict)
		{
			foreach (KeyValuePair<string, List<string>> keyValuePair in dict)
			{
				if (this.KeyStatus.ContainsKey(keyValuePair.Key))
				{
					this.KeyStatus[keyValuePair.Key].Clear();
					foreach (string text in keyValuePair.Value)
					{
						this.KeyStatus[keyValuePair.Key].Add(text);
					}
				}
			}
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x004D39CC File Offset: 0x004D1BCC
		public Dictionary<string, List<string>> WritePreferences()
		{
			Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
			foreach (KeyValuePair<string, List<string>> keyValuePair in this.KeyStatus)
			{
				if (keyValuePair.Value.Count > 0)
				{
					dictionary.Add(keyValuePair.Key, keyValuePair.Value.ToList<string>());
				}
			}
			if (!dictionary.ContainsKey("MouseLeft") || dictionary["MouseLeft"].Count == 0)
			{
				dictionary["MouseLeft"] = new List<string> { "Mouse1" };
			}
			if (!dictionary.ContainsKey("Inventory") || dictionary["Inventory"].Count == 0)
			{
				dictionary["Inventory"] = new List<string> { "Escape" };
			}
			return dictionary;
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x004D3ABC File Offset: 0x004D1CBC
		public KeyConfiguration()
		{
		}

		// Token: 0x04001119 RID: 4377
		public Dictionary<string, List<string>> KeyStatus = new Dictionary<string, List<string>>();
	}
}
