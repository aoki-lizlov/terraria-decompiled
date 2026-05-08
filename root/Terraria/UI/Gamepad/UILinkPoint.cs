using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Xna.Framework;

namespace Terraria.UI.Gamepad
{
	// Token: 0x02000107 RID: 263
	public class UILinkPoint
	{
		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06001A67 RID: 6759 RVA: 0x004F6008 File Offset: 0x004F4208
		// (set) Token: 0x06001A68 RID: 6760 RVA: 0x004F6010 File Offset: 0x004F4210
		public int Page
		{
			[CompilerGenerated]
			get
			{
				return this.<Page>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Page>k__BackingField = value;
			}
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x004F6019 File Offset: 0x004F4219
		public UILinkPoint(int id, bool enabled, int left, int right, int up, int down)
		{
			this.ID = id;
			this.Enabled = enabled;
			this.Left = left;
			this.Right = right;
			this.Up = up;
			this.Down = down;
		}

		// Token: 0x06001A6A RID: 6762 RVA: 0x004F604E File Offset: 0x004F424E
		public void SetPage(int page)
		{
			this.Page = page;
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x004F6057 File Offset: 0x004F4257
		public void Unlink()
		{
			this.Left = -3;
			this.Right = -4;
			this.Up = -1;
			this.Down = -2;
		}

		// Token: 0x14000030 RID: 48
		// (add) Token: 0x06001A6C RID: 6764 RVA: 0x004F6078 File Offset: 0x004F4278
		// (remove) Token: 0x06001A6D RID: 6765 RVA: 0x004F60B0 File Offset: 0x004F42B0
		public event Func<string> OnSpecialInteracts
		{
			[CompilerGenerated]
			add
			{
				Func<string> func = this.OnSpecialInteracts;
				Func<string> func2;
				do
				{
					func2 = func;
					Func<string> func3 = (Func<string>)Delegate.Combine(func2, value);
					func = Interlocked.CompareExchange<Func<string>>(ref this.OnSpecialInteracts, func3, func2);
				}
				while (func != func2);
			}
			[CompilerGenerated]
			remove
			{
				Func<string> func = this.OnSpecialInteracts;
				Func<string> func2;
				do
				{
					func2 = func;
					Func<string> func3 = (Func<string>)Delegate.Remove(func2, value);
					func = Interlocked.CompareExchange<Func<string>>(ref this.OnSpecialInteracts, func3, func2);
				}
				while (func != func2);
			}
		}

		// Token: 0x06001A6E RID: 6766 RVA: 0x004F60E5 File Offset: 0x004F42E5
		public string SpecialInteractions()
		{
			if (this.OnSpecialInteracts != null)
			{
				return this.OnSpecialInteracts();
			}
			return string.Empty;
		}

		// Token: 0x040014DE RID: 5342
		public int ID;

		// Token: 0x040014DF RID: 5343
		[CompilerGenerated]
		private int <Page>k__BackingField;

		// Token: 0x040014E0 RID: 5344
		public bool Enabled;

		// Token: 0x040014E1 RID: 5345
		public Vector2 Position;

		// Token: 0x040014E2 RID: 5346
		public int Left;

		// Token: 0x040014E3 RID: 5347
		public int Right;

		// Token: 0x040014E4 RID: 5348
		public int Up;

		// Token: 0x040014E5 RID: 5349
		public int Down;

		// Token: 0x040014E6 RID: 5350
		[CompilerGenerated]
		private Func<string> OnSpecialInteracts;
	}
}
