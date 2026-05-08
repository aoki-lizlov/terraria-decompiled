using System;

namespace Terraria.UI
{
	// Token: 0x020000EF RID: 239
	public class LegacyGameInterfaceLayer : GameInterfaceLayer
	{
		// Token: 0x06001919 RID: 6425 RVA: 0x004E785A File Offset: 0x004E5A5A
		public LegacyGameInterfaceLayer(string name, GameInterfaceDrawMethod drawMethod, InterfaceScaleType scaleType = InterfaceScaleType.Game)
			: base(name, scaleType)
		{
			this._drawMethod = drawMethod;
		}

		// Token: 0x0600191A RID: 6426 RVA: 0x004E786B File Offset: 0x004E5A6B
		protected override bool DrawSelf()
		{
			return this._drawMethod();
		}

		// Token: 0x04001332 RID: 4914
		private GameInterfaceDrawMethod _drawMethod;
	}
}
