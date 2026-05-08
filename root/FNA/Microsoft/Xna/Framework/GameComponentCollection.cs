using System;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.Xna.Framework
{
	// Token: 0x0200001F RID: 31
	public sealed class GameComponentCollection : Collection<IGameComponent>
	{
		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000BBB RID: 3003 RVA: 0x000133A4 File Offset: 0x000115A4
		// (remove) Token: 0x06000BBC RID: 3004 RVA: 0x000133DC File Offset: 0x000115DC
		public event EventHandler<GameComponentCollectionEventArgs> ComponentAdded
		{
			[CompilerGenerated]
			add
			{
				EventHandler<GameComponentCollectionEventArgs> eventHandler = this.ComponentAdded;
				EventHandler<GameComponentCollectionEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<GameComponentCollectionEventArgs> eventHandler3 = (EventHandler<GameComponentCollectionEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<GameComponentCollectionEventArgs>>(ref this.ComponentAdded, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<GameComponentCollectionEventArgs> eventHandler = this.ComponentAdded;
				EventHandler<GameComponentCollectionEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<GameComponentCollectionEventArgs> eventHandler3 = (EventHandler<GameComponentCollectionEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<GameComponentCollectionEventArgs>>(ref this.ComponentAdded, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000BBD RID: 3005 RVA: 0x00013414 File Offset: 0x00011614
		// (remove) Token: 0x06000BBE RID: 3006 RVA: 0x0001344C File Offset: 0x0001164C
		public event EventHandler<GameComponentCollectionEventArgs> ComponentRemoved
		{
			[CompilerGenerated]
			add
			{
				EventHandler<GameComponentCollectionEventArgs> eventHandler = this.ComponentRemoved;
				EventHandler<GameComponentCollectionEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<GameComponentCollectionEventArgs> eventHandler3 = (EventHandler<GameComponentCollectionEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<GameComponentCollectionEventArgs>>(ref this.ComponentRemoved, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<GameComponentCollectionEventArgs> eventHandler = this.ComponentRemoved;
				EventHandler<GameComponentCollectionEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<GameComponentCollectionEventArgs> eventHandler3 = (EventHandler<GameComponentCollectionEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<GameComponentCollectionEventArgs>>(ref this.ComponentRemoved, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x00013484 File Offset: 0x00011684
		protected override void ClearItems()
		{
			for (int i = 0; i < base.Count; i++)
			{
				this.OnComponentRemoved(new GameComponentCollectionEventArgs(base[i]));
			}
			base.ClearItems();
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x000134BA File Offset: 0x000116BA
		protected override void InsertItem(int index, IGameComponent item)
		{
			if (base.IndexOf(item) != -1)
			{
				throw new ArgumentException("Cannot Add Same Component Multiple Times");
			}
			base.InsertItem(index, item);
			if (item != null)
			{
				this.OnComponentAdded(new GameComponentCollectionEventArgs(item));
			}
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x000134E8 File Offset: 0x000116E8
		protected override void RemoveItem(int index)
		{
			IGameComponent gameComponent = base[index];
			base.RemoveItem(index);
			if (gameComponent != null)
			{
				this.OnComponentRemoved(new GameComponentCollectionEventArgs(gameComponent));
			}
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x00013513 File Offset: 0x00011713
		protected override void SetItem(int index, IGameComponent item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x0001351A File Offset: 0x0001171A
		private void OnComponentAdded(GameComponentCollectionEventArgs eventArgs)
		{
			if (this.ComponentAdded != null)
			{
				this.ComponentAdded(this, eventArgs);
			}
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x00013531 File Offset: 0x00011731
		private void OnComponentRemoved(GameComponentCollectionEventArgs eventArgs)
		{
			if (this.ComponentRemoved != null)
			{
				this.ComponentRemoved(this, eventArgs);
			}
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x00013548 File Offset: 0x00011748
		public GameComponentCollection()
		{
		}

		// Token: 0x0400055F RID: 1375
		[CompilerGenerated]
		private EventHandler<GameComponentCollectionEventArgs> ComponentAdded;

		// Token: 0x04000560 RID: 1376
		[CompilerGenerated]
		private EventHandler<GameComponentCollectionEventArgs> ComponentRemoved;
	}
}
