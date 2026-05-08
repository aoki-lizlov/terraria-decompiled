using System;

namespace ReLogic.OS
{
	// Token: 0x02000060 RID: 96
	public interface IMouseNotifier
	{
		// Token: 0x06000211 RID: 529
		void ForceCursorHidden();

		// Token: 0x06000212 RID: 530
		void AddMouseHandler(Action<bool> action);

		// Token: 0x06000213 RID: 531
		void RemoveMouseHandler(Action<bool> action);
	}
}
