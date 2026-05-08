using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ReLogic.Localization.IME
{
	// Token: 0x0200007B RID: 123
	public abstract class PlatformIme : IImeService, IDisposable
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060002AD RID: 685
		public abstract string CompositionString { get; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060002AE RID: 686
		public abstract bool IsCandidateListVisible { get; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060002AF RID: 687
		public abstract uint? SelectedCandidate { get; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060002B0 RID: 688
		public abstract uint CandidateCount { get; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000AADC File Offset: 0x00008CDC
		// (set) Token: 0x060002B2 RID: 690 RVA: 0x0000AAE4 File Offset: 0x00008CE4
		public bool IsEnabled
		{
			[CompilerGenerated]
			get
			{
				return this.<IsEnabled>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<IsEnabled>k__BackingField = value;
			}
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000AAED File Offset: 0x00008CED
		protected PlatformIme()
		{
			this.IsEnabled = false;
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000AB07 File Offset: 0x00008D07
		public void AddKeyListener(Action<char> listener)
		{
			this._keyPressCallbacks.Add(listener);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000AB15 File Offset: 0x00008D15
		public void RemoveKeyListener(Action<char> listener)
		{
			this._keyPressCallbacks.Remove(listener);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000AB24 File Offset: 0x00008D24
		protected void OnKeyPress(char character)
		{
			foreach (Action<char> action in this._keyPressCallbacks)
			{
				action.Invoke(character);
			}
		}

		// Token: 0x060002B7 RID: 695
		public abstract string GetCandidate(uint index);

		// Token: 0x060002B8 RID: 696 RVA: 0x0000AB78 File Offset: 0x00008D78
		public void Enable()
		{
			if (this.IsEnabled)
			{
				return;
			}
			this.OnEnable();
			this.IsEnabled = true;
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000AB90 File Offset: 0x00008D90
		public void Disable()
		{
			if (!this.IsEnabled)
			{
				return;
			}
			this.OnDisable();
			this.IsEnabled = false;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x000046AD File Offset: 0x000028AD
		protected virtual void OnEnable()
		{
		}

		// Token: 0x060002BB RID: 699 RVA: 0x000046AD File Offset: 0x000028AD
		protected virtual void OnDisable()
		{
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000ABA8 File Offset: 0x00008DA8
		protected virtual void Dispose(bool disposing)
		{
			if (!this._disposedValue)
			{
				this._disposedValue = true;
			}
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000ABBC File Offset: 0x00008DBC
		~PlatformIme()
		{
			this.Dispose(false);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000ABEC File Offset: 0x00008DEC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0400032C RID: 812
		[CompilerGenerated]
		private bool <IsEnabled>k__BackingField;

		// Token: 0x0400032D RID: 813
		private readonly List<Action<char>> _keyPressCallbacks = new List<Action<char>>();

		// Token: 0x0400032E RID: 814
		private bool _disposedValue;
	}
}
