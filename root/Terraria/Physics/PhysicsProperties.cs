using System;

namespace Terraria.Physics
{
	// Token: 0x0200007E RID: 126
	public class PhysicsProperties
	{
		// Token: 0x0600156E RID: 5486 RVA: 0x004C4997 File Offset: 0x004C2B97
		public PhysicsProperties(float gravity, float drag)
		{
			this.Gravity = gravity;
			this.Drag = drag;
		}

		// Token: 0x04001104 RID: 4356
		public readonly float Gravity;

		// Token: 0x04001105 RID: 4357
		public readonly float Drag;
	}
}
