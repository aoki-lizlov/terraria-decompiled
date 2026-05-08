using System;

namespace Terraria.GameContent.Animations
{
	// Token: 0x0200052D RID: 1325
	public interface IAnimationSegmentAction<T>
	{
		// Token: 0x060036F2 RID: 14066
		void BindTo(T obj);

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x060036F3 RID: 14067
		int ExpectedLengthOfActionInFrames { get; }

		// Token: 0x060036F4 RID: 14068
		void ApplyTo(T obj, float localTimeForObj);

		// Token: 0x060036F5 RID: 14069
		void SetDelay(float delay);
	}
}
