using System;
using System.Linq;

namespace Terraria.GameInput
{
	// Token: 0x02000090 RID: 144
	public class TriggersPack
	{
		// Token: 0x06001603 RID: 5635 RVA: 0x004D595C File Offset: 0x004D3B5C
		public void Initialize()
		{
			this.Current.SetupKeys();
			this.Old.SetupKeys();
			this.JustPressed.SetupKeys();
			this.JustReleased.SetupKeys();
		}

		// Token: 0x06001604 RID: 5636 RVA: 0x004D598A File Offset: 0x004D3B8A
		public void Reset()
		{
			this.Old.CloneFrom(this.Current);
			this.Current.Reset();
		}

		// Token: 0x06001605 RID: 5637 RVA: 0x004D59A8 File Offset: 0x004D3BA8
		public void Update()
		{
			this.CompareDiffs(this.JustPressed, this.Old, this.Current);
			this.CompareDiffs(this.JustReleased, this.Current, this.Old);
		}

		// Token: 0x06001606 RID: 5638 RVA: 0x004D59DC File Offset: 0x004D3BDC
		public void CompareDiffs(TriggersSet Bearer, TriggersSet oldset, TriggersSet newset)
		{
			Bearer.Reset();
			foreach (string text in Bearer.KeyStatus.Keys.ToList<string>())
			{
				Bearer.KeyStatus[text] = newset.KeyStatus[text] && !oldset.KeyStatus[text];
			}
		}

		// Token: 0x06001607 RID: 5639 RVA: 0x004D5A64 File Offset: 0x004D3C64
		public TriggersPack()
		{
		}

		// Token: 0x04001172 RID: 4466
		public TriggersSet Current = new TriggersSet();

		// Token: 0x04001173 RID: 4467
		public TriggersSet Old = new TriggersSet();

		// Token: 0x04001174 RID: 4468
		public TriggersSet JustPressed = new TriggersSet();

		// Token: 0x04001175 RID: 4469
		public TriggersSet JustReleased = new TriggersSet();
	}
}
