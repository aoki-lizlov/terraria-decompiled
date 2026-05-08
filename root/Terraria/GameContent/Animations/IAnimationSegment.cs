using System;

namespace Terraria.GameContent.Animations
{
	// Token: 0x0200052A RID: 1322
	public interface IAnimationSegment
	{
		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x060036EB RID: 14059
		float DedicatedTimeNeeded { get; }

		// Token: 0x060036EC RID: 14060
		void Draw(ref GameAnimationSegment info);
	}
}
