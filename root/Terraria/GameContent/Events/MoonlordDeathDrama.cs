using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Utilities;

namespace Terraria.GameContent.Events
{
	// Token: 0x020004FD RID: 1277
	public class MoonlordDeathDrama
	{
		// Token: 0x060035C8 RID: 13768 RVA: 0x0061D2EC File Offset: 0x0061B4EC
		public static void Update(SceneState sceneState, SceneMetrics metrics)
		{
			for (int i = 0; i < MoonlordDeathDrama._pieces.Count; i++)
			{
				MoonlordDeathDrama.MoonlordPiece moonlordPiece = MoonlordDeathDrama._pieces[i];
				moonlordPiece.Update();
				if (moonlordPiece.Dead)
				{
					MoonlordDeathDrama._pieces.Remove(moonlordPiece);
					i--;
				}
			}
			for (int j = 0; j < MoonlordDeathDrama._explosions.Count; j++)
			{
				MoonlordDeathDrama.MoonlordExplosion moonlordExplosion = MoonlordDeathDrama._explosions[j];
				moonlordExplosion.Update();
				if (moonlordExplosion.Dead)
				{
					MoonlordDeathDrama._explosions.Remove(moonlordExplosion);
					j--;
				}
			}
			bool flag = false;
			for (int k = 0; k < MoonlordDeathDrama._lightSources.Count; k++)
			{
				if (metrics.Center.Distance(MoonlordDeathDrama._lightSources[k]) < 2000f)
				{
					flag = true;
					break;
				}
			}
			MoonlordDeathDrama._lightSources.Clear();
			if (!flag)
			{
				MoonlordDeathDrama.requestedLight = 0f;
			}
			sceneState.MoveTowards(ref MoonlordDeathDrama.whitening, MoonlordDeathDrama.requestedLight, 0.02f);
			MoonlordDeathDrama.requestedLight = 0f;
		}

		// Token: 0x060035C9 RID: 13769 RVA: 0x0061D3F0 File Offset: 0x0061B5F0
		public static void DrawPieces(SpriteBatch spriteBatch)
		{
			Rectangle rectangle = Utils.CenteredRectangle(Main.screenPosition + new Vector2((float)Main.screenWidth, (float)Main.screenHeight) * 0.5f, new Vector2((float)(Main.screenWidth + 1000), (float)(Main.screenHeight + 1000)));
			for (int i = 0; i < MoonlordDeathDrama._pieces.Count; i++)
			{
				if (MoonlordDeathDrama._pieces[i].InDrawRange(rectangle))
				{
					MoonlordDeathDrama._pieces[i].Draw(spriteBatch);
				}
			}
		}

		// Token: 0x060035CA RID: 13770 RVA: 0x0061D480 File Offset: 0x0061B680
		public static void DrawExplosions(SpriteBatch spriteBatch)
		{
			Rectangle rectangle = Utils.CenteredRectangle(Main.screenPosition + new Vector2((float)Main.screenWidth, (float)Main.screenHeight) * 0.5f, new Vector2((float)(Main.screenWidth + 1000), (float)(Main.screenHeight + 1000)));
			for (int i = 0; i < MoonlordDeathDrama._explosions.Count; i++)
			{
				if (MoonlordDeathDrama._explosions[i].InDrawRange(rectangle))
				{
					MoonlordDeathDrama._explosions[i].Draw(spriteBatch);
				}
			}
		}

		// Token: 0x060035CB RID: 13771 RVA: 0x0061D510 File Offset: 0x0061B710
		public static void DrawWhite(SpriteBatch spriteBatch)
		{
			if (MoonlordDeathDrama.whitening == 0f)
			{
				return;
			}
			Color color = Color.White * MoonlordDeathDrama.whitening;
			spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(-2, -2, Main.screenWidth + 4, Main.screenHeight + 4), new Rectangle?(new Rectangle(0, 0, 1, 1)), color);
		}

		// Token: 0x060035CC RID: 13772 RVA: 0x0061D570 File Offset: 0x0061B770
		public static void ThrowPieces(Vector2 MoonlordCoreCenter, int DramaSeed)
		{
			UnifiedRandom unifiedRandom = new UnifiedRandom(DramaSeed);
			Vector2 vector = Vector2.UnitY.RotatedBy((double)(unifiedRandom.NextFloat() * 1.5707964f - 0.7853982f + 3.1415927f), default(Vector2));
			MoonlordDeathDrama._pieces.Add(new MoonlordDeathDrama.MoonlordPiece(Main.Assets.Request<Texture2D>("Images/Misc/MoonExplosion/Spine", 1).Value, new Vector2(64f, 150f), MoonlordCoreCenter + new Vector2(0f, 50f), vector * 6f, 0f, unifiedRandom.NextFloat() * 0.1f - 0.05f));
			vector = Vector2.UnitY.RotatedBy((double)(unifiedRandom.NextFloat() * 1.5707964f - 0.7853982f + 3.1415927f), default(Vector2));
			MoonlordDeathDrama._pieces.Add(new MoonlordDeathDrama.MoonlordPiece(Main.Assets.Request<Texture2D>("Images/Misc/MoonExplosion/Shoulder", 1).Value, new Vector2(40f, 120f), MoonlordCoreCenter + new Vector2(50f, -120f), vector * 10f, 0f, unifiedRandom.NextFloat() * 0.1f - 0.05f));
			vector = Vector2.UnitY.RotatedBy((double)(unifiedRandom.NextFloat() * 1.5707964f - 0.7853982f + 3.1415927f), default(Vector2));
			MoonlordDeathDrama._pieces.Add(new MoonlordDeathDrama.MoonlordPiece(Main.Assets.Request<Texture2D>("Images/Misc/MoonExplosion/Torso", 1).Value, new Vector2(192f, 252f), MoonlordCoreCenter, vector * 8f, 0f, unifiedRandom.NextFloat() * 0.1f - 0.05f));
			vector = Vector2.UnitY.RotatedBy((double)(unifiedRandom.NextFloat() * 1.5707964f - 0.7853982f + 3.1415927f), default(Vector2));
			MoonlordDeathDrama._pieces.Add(new MoonlordDeathDrama.MoonlordPiece(Main.Assets.Request<Texture2D>("Images/Misc/MoonExplosion/Head", 1).Value, new Vector2(138f, 185f), MoonlordCoreCenter - new Vector2(0f, 200f), vector * 12f, 0f, unifiedRandom.NextFloat() * 0.1f - 0.05f));
		}

		// Token: 0x060035CD RID: 13773 RVA: 0x0061D7CC File Offset: 0x0061B9CC
		public static void AddExplosion(Vector2 spot)
		{
			MoonlordDeathDrama._explosions.Add(new MoonlordDeathDrama.MoonlordExplosion(Main.Assets.Request<Texture2D>("Images/Misc/MoonExplosion/Explosion", 1).Value, spot, Main.rand.Next(2, 4)));
		}

		// Token: 0x060035CE RID: 13774 RVA: 0x0061D7FF File Offset: 0x0061B9FF
		public static void RequestLight(float light, Vector2 spot)
		{
			MoonlordDeathDrama._lightSources.Add(spot);
			if (light > 1f)
			{
				light = 1f;
			}
			if (MoonlordDeathDrama.requestedLight < light)
			{
				MoonlordDeathDrama.requestedLight = light;
			}
		}

		// Token: 0x060035CF RID: 13775 RVA: 0x0000357B File Offset: 0x0000177B
		public MoonlordDeathDrama()
		{
		}

		// Token: 0x060035D0 RID: 13776 RVA: 0x0061D829 File Offset: 0x0061BA29
		// Note: this type is marked as 'beforefieldinit'.
		static MoonlordDeathDrama()
		{
		}

		// Token: 0x04005AE6 RID: 23270
		private static List<MoonlordDeathDrama.MoonlordPiece> _pieces = new List<MoonlordDeathDrama.MoonlordPiece>();

		// Token: 0x04005AE7 RID: 23271
		private static List<MoonlordDeathDrama.MoonlordExplosion> _explosions = new List<MoonlordDeathDrama.MoonlordExplosion>();

		// Token: 0x04005AE8 RID: 23272
		private static List<Vector2> _lightSources = new List<Vector2>();

		// Token: 0x04005AE9 RID: 23273
		private static float whitening;

		// Token: 0x04005AEA RID: 23274
		private static float requestedLight;

		// Token: 0x0200098C RID: 2444
		public class MoonlordPiece
		{
			// Token: 0x0600496E RID: 18798 RVA: 0x006D1E1F File Offset: 0x006D001F
			public MoonlordPiece(Texture2D pieceTexture, Vector2 textureOrigin, Vector2 centerPos, Vector2 velocity, float rot, float angularVelocity)
			{
				this._texture = pieceTexture;
				this._origin = textureOrigin;
				this._position = centerPos;
				this._velocity = velocity;
				this._rotation = rot;
				this._rotationVelocity = angularVelocity;
			}

			// Token: 0x0600496F RID: 18799 RVA: 0x006D1E54 File Offset: 0x006D0054
			public void Update()
			{
				this._velocity.Y = this._velocity.Y + 0.3f;
				this._rotation += this._rotationVelocity;
				this._rotationVelocity *= 0.99f;
				this._position += this._velocity;
			}

			// Token: 0x06004970 RID: 18800 RVA: 0x006D1EB4 File Offset: 0x006D00B4
			public void Draw(SpriteBatch sp)
			{
				Color light = this.GetLight();
				sp.Draw(this._texture, this._position - Main.screenPosition, null, light, this._rotation, this._origin, 1f, SpriteEffects.None, 0f);
			}

			// Token: 0x1700058F RID: 1423
			// (get) Token: 0x06004971 RID: 18801 RVA: 0x006D1F08 File Offset: 0x006D0108
			public bool Dead
			{
				get
				{
					return this._position.Y > (float)(Main.maxTilesY * 16) - 480f || this._position.X < 480f || this._position.X >= (float)(Main.maxTilesX * 16) - 480f;
				}
			}

			// Token: 0x06004972 RID: 18802 RVA: 0x006D1F64 File Offset: 0x006D0164
			public bool InDrawRange(Rectangle playerScreen)
			{
				return playerScreen.Contains(this._position.ToPoint());
			}

			// Token: 0x06004973 RID: 18803 RVA: 0x006D1F78 File Offset: 0x006D0178
			public Color GetLight()
			{
				Vector3 vector = Vector3.Zero;
				float num = 0f;
				int num2 = 5;
				Point point = this._position.ToTileCoordinates();
				for (int i = point.X - num2; i <= point.X + num2; i++)
				{
					for (int j = point.Y - num2; j <= point.Y + num2; j++)
					{
						vector += Lighting.GetColor(i, j).ToVector3();
						num += 1f;
					}
				}
				if (num == 0f)
				{
					return Color.White;
				}
				return new Color(vector / num);
			}

			// Token: 0x04007646 RID: 30278
			private Texture2D _texture;

			// Token: 0x04007647 RID: 30279
			private Vector2 _position;

			// Token: 0x04007648 RID: 30280
			private Vector2 _velocity;

			// Token: 0x04007649 RID: 30281
			private Vector2 _origin;

			// Token: 0x0400764A RID: 30282
			private float _rotation;

			// Token: 0x0400764B RID: 30283
			private float _rotationVelocity;
		}

		// Token: 0x0200098D RID: 2445
		public class MoonlordExplosion
		{
			// Token: 0x06004974 RID: 18804 RVA: 0x006D2018 File Offset: 0x006D0218
			public MoonlordExplosion(Texture2D pieceTexture, Vector2 centerPos, int frameSpeed)
			{
				this._texture = pieceTexture;
				this._position = centerPos;
				this._frameSpeed = frameSpeed;
				this._frameCounter = 0;
				this._frame = this._texture.Frame(1, 7, 0, 0, 0, 0);
				this._origin = this._frame.Size() / 2f;
			}

			// Token: 0x06004975 RID: 18805 RVA: 0x006D2079 File Offset: 0x006D0279
			public void Update()
			{
				this._frameCounter++;
				this._frame = this._texture.Frame(1, 7, 0, this._frameCounter / this._frameSpeed, 0, 0);
			}

			// Token: 0x06004976 RID: 18806 RVA: 0x006D20AC File Offset: 0x006D02AC
			public void Draw(SpriteBatch sp)
			{
				Color light = this.GetLight();
				sp.Draw(this._texture, this._position - Main.screenPosition, new Rectangle?(this._frame), light, 0f, this._origin, 1f, SpriteEffects.None, 0f);
			}

			// Token: 0x17000590 RID: 1424
			// (get) Token: 0x06004977 RID: 18807 RVA: 0x006D2100 File Offset: 0x006D0300
			public bool Dead
			{
				get
				{
					return this._position.Y > (float)(Main.maxTilesY * 16) - 480f || this._position.X < 480f || this._position.X >= (float)(Main.maxTilesX * 16) - 480f || this._frameCounter >= this._frameSpeed * 7;
				}
			}

			// Token: 0x06004978 RID: 18808 RVA: 0x006D216C File Offset: 0x006D036C
			public bool InDrawRange(Rectangle playerScreen)
			{
				return playerScreen.Contains(this._position.ToPoint());
			}

			// Token: 0x06004979 RID: 18809 RVA: 0x006D2180 File Offset: 0x006D0380
			public Color GetLight()
			{
				return new Color(255, 255, 255, 127);
			}

			// Token: 0x0400764C RID: 30284
			private Texture2D _texture;

			// Token: 0x0400764D RID: 30285
			private Vector2 _position;

			// Token: 0x0400764E RID: 30286
			private Vector2 _origin;

			// Token: 0x0400764F RID: 30287
			private Rectangle _frame;

			// Token: 0x04007650 RID: 30288
			private int _frameCounter;

			// Token: 0x04007651 RID: 30289
			private int _frameSpeed;
		}
	}
}
