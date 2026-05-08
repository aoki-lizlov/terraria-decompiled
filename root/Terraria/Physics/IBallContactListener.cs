using System;
using Microsoft.Xna.Framework;

namespace Terraria.Physics
{
	// Token: 0x0200007A RID: 122
	public interface IBallContactListener
	{
		// Token: 0x06001567 RID: 5479
		void OnCollision(PhysicsProperties properties, ref Vector2 position, ref Vector2 velocity, ref BallCollisionEvent collision);

		// Token: 0x06001568 RID: 5480
		void OnPassThrough(PhysicsProperties properties, ref Vector2 position, ref Vector2 velocity, ref float angularVelocity, ref BallPassThroughEvent passThrough);
	}
}
