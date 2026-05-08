using System;
using Microsoft.Xna.Framework.Input;

namespace ReLogic.Localization.IME
{
	// Token: 0x0200007A RID: 122
	internal class FnaIme : PlatformIme
	{
		// Token: 0x060002A2 RID: 674 RVA: 0x0000AA29 File Offset: 0x00008C29
		public FnaIme()
		{
			TextInputEXT.TextInput += new Action<char>(this.OnCharCallback);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000AA42 File Offset: 0x00008C42
		private void OnCharCallback(char key)
		{
			if (base.IsEnabled)
			{
				base.OnKeyPress(key);
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x0000AA53 File Offset: 0x00008C53
		public override uint CandidateCount
		{
			get
			{
				return 0U;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000AA56 File Offset: 0x00008C56
		public override string CompositionString
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0000AA53 File Offset: 0x00008C53
		public override bool IsCandidateListVisible
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000AA5D File Offset: 0x00008C5D
		public override uint? SelectedCandidate
		{
			get
			{
				return new uint?(0U);
			}
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000AA56 File Offset: 0x00008C56
		public override string GetCandidate(uint index)
		{
			return string.Empty;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000AA65 File Offset: 0x00008C65
		protected override void OnEnable()
		{
			TextInputEXT.StartTextInput();
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000AA6C File Offset: 0x00008C6C
		protected override void OnDisable()
		{
			TextInputEXT.StopTextInput();
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000AA73 File Offset: 0x00008C73
		protected override void Dispose(bool disposing)
		{
			if (this._disposedValue)
			{
				return;
			}
			if (disposing)
			{
				TextInputEXT.TextInput -= new Action<char>(this.OnCharCallback);
				TextInputEXT.StopTextInput();
			}
			if (base.IsEnabled)
			{
				base.Disable();
			}
			this._disposedValue = true;
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000AAAC File Offset: 0x00008CAC
		~FnaIme()
		{
			this.Dispose(false);
		}

		// Token: 0x0400032B RID: 811
		private bool _disposedValue;
	}
}
