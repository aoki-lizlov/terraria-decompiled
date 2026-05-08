using System;

namespace ReLogic.Peripherals.RGB
{
	// Token: 0x02000019 RID: 25
	public abstract class ARgbGameValueTracker<TValueType> : ARgbGameValueTracker where TValueType : IComparable
	{
		// Token: 0x060000C8 RID: 200 RVA: 0x00004494 File Offset: 0x00002694
		public void Update(TValueType value, bool isVisible)
		{
			this.IsVisible = isVisible;
			if (this._currentValue.Equals(value) && this._timesDeniedRepeat < 30)
			{
				this._timesDeniedRepeat++;
				return;
			}
			this._timesDeniedRepeat = 0;
			this._currentValue = value;
			this._needsToSendMessage = true;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000044EF File Offset: 0x000026EF
		protected ARgbGameValueTracker()
		{
		}

		// Token: 0x04000038 RID: 56
		private const int TimesToDenyIdenticalValues = 30;

		// Token: 0x04000039 RID: 57
		protected TValueType _currentValue;

		// Token: 0x0400003A RID: 58
		private int _timesDeniedRepeat;
	}
}
