using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.Graphics.Renderers;
using Terraria.ID;
using Terraria.ObjectData;

namespace Terraria.DataStructures
{
	// Token: 0x02000533 RID: 1331
	public class RichRoomCheckFeedback : IRoomCheckFeedback, IRoomCheckFeedback_Spread, IRoomCheckFeedback_Scoring
	{
		// Token: 0x0600371E RID: 14110 RVA: 0x00009AD9 File Offset: 0x00007CD9
		private static RoomCheckParticle GetNewParticle()
		{
			return new RoomCheckParticle();
		}

		// Token: 0x0600371F RID: 14111 RVA: 0x0062C7E0 File Offset: 0x0062A9E0
		private void Add(int x, int y, int iteration, RichRoomCheckFeedback.Reason type)
		{
			if (this._spaceCount >= this._space.Length)
			{
				Array.Resize<RichRoomCheckFeedback.ParticlePreparation>(ref this._space, this._space.Length * 2);
			}
			if (this._highestIteration < iteration)
			{
				this._highestIteration = iteration;
			}
			RichRoomCheckFeedback.ParticlePreparation[] space = this._space;
			int spaceCount = this._spaceCount;
			this._spaceCount = spaceCount + 1;
			space[spaceCount] = new RichRoomCheckFeedback.ParticlePreparation
			{
				type = type,
				x = x,
				y = y,
				iteration = iteration
			};
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06003720 RID: 14112 RVA: 0x001DAC3B File Offset: 0x001D8E3B
		public bool StopOnFail
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06003721 RID: 14113 RVA: 0x001DAC3B File Offset: 0x001D8E3B
		public bool DisplayText
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003722 RID: 14114 RVA: 0x0062C868 File Offset: 0x0062AA68
		public void BeginSpread(int x, int y)
		{
			this._spaceCount = 0;
			this._highestIteration = 0;
			this._originX = x;
			this._originY = y;
		}

		// Token: 0x06003723 RID: 14115 RVA: 0x0062C886 File Offset: 0x0062AA86
		public void StartedInASolidTile(int x, int y)
		{
			this.Add(x, y, 0, RichRoomCheckFeedback.Reason.BlockedWall);
		}

		// Token: 0x06003724 RID: 14116 RVA: 0x0062C892 File Offset: 0x0062AA92
		public void TooCloseToWorldEdge(int x, int y, int iteration)
		{
			this.Add(x, y, iteration, RichRoomCheckFeedback.Reason.OpenAir);
		}

		// Token: 0x06003725 RID: 14117 RVA: 0x0062C89E File Offset: 0x0062AA9E
		public void AnyBlockScannedHere(int x, int y, int iteration)
		{
			this.Add(x, y, iteration, RichRoomCheckFeedback.Reason.Good);
		}

		// Token: 0x06003726 RID: 14118 RVA: 0x0062C892 File Offset: 0x0062AA92
		public void RoomTooBig(int x, int y, int iteration)
		{
			this.Add(x, y, iteration, RichRoomCheckFeedback.Reason.OpenAir);
		}

		// Token: 0x06003727 RID: 14119 RVA: 0x0062C8AA File Offset: 0x0062AAAA
		public void BlockingWall(int x, int y, int iteration)
		{
			this.Add(x, y, iteration, RichRoomCheckFeedback.Reason.BlockedWall);
		}

		// Token: 0x06003728 RID: 14120 RVA: 0x0062C8AA File Offset: 0x0062AAAA
		public void BlockingOpenGate(int x, int y, int iteration)
		{
			this.Add(x, y, iteration, RichRoomCheckFeedback.Reason.BlockedWall);
		}

		// Token: 0x06003729 RID: 14121 RVA: 0x0062C8B6 File Offset: 0x0062AAB6
		public void Stinkbug(int x, int y, int iteration)
		{
			this.Add(x, y, iteration, RichRoomCheckFeedback.Reason.Hazard);
		}

		// Token: 0x0600372A RID: 14122 RVA: 0x0062C8B6 File Offset: 0x0062AAB6
		public void EchoStinkbug(int x, int y, int iteration)
		{
			this.Add(x, y, iteration, RichRoomCheckFeedback.Reason.Hazard);
		}

		// Token: 0x0600372B RID: 14123 RVA: 0x0062C892 File Offset: 0x0062AA92
		public void MissingAWall(int x, int y, int iteration)
		{
			this.Add(x, y, iteration, RichRoomCheckFeedback.Reason.OpenAir);
		}

		// Token: 0x0600372C RID: 14124 RVA: 0x0062C8C2 File Offset: 0x0062AAC2
		public void UnsafeWall(int x, int y, int iteration)
		{
			this.Add(x, y, iteration, RichRoomCheckFeedback.Reason.UnsafeWall);
		}

		// Token: 0x0600372D RID: 14125 RVA: 0x0062C8D0 File Offset: 0x0062AAD0
		public void EndSpread()
		{
			Vector2 vector = new Vector2((float)this._originX, (float)this._originY);
			for (int i = 0; i < this._spaceCount; i++)
			{
				RichRoomCheckFeedback.ParticlePreparation particlePreparation = this._space[i];
				for (int j = 0; j < this._spaceCount; j++)
				{
					RichRoomCheckFeedback.ParticlePreparation particlePreparation2 = this._space[j];
					if (particlePreparation.x == particlePreparation2.x && particlePreparation.y == particlePreparation2.y && particlePreparation.type == RichRoomCheckFeedback.Reason.Good && particlePreparation2.type != RichRoomCheckFeedback.Reason.Good)
					{
						particlePreparation.consumed = true;
					}
				}
				this._space[i] = particlePreparation;
			}
			float highestDistanceFromOrigin = this.GetHighestDistanceFromOrigin(ref vector);
			float num = 3f * highestDistanceFromOrigin + 60f;
			for (int k = 0; k < this._spaceCount; k++)
			{
				RichRoomCheckFeedback.ParticlePreparation particlePreparation3 = this._space[k];
				if (!particlePreparation3.consumed)
				{
					ushort type = Main.tile[particlePreparation3.x, particlePreparation3.y].type;
					bool flag = TileID.Sets.RoomNeeds.CountsAsTable[(int)type] || TileID.Sets.RoomNeeds.CountsAsChair[(int)type] || TileID.Sets.RoomNeeds.CountsAsTorch[(int)type] || TileID.Sets.RoomNeeds.CountsAsDoor[(int)type];
					Asset<Texture2D> asset = TextureAssets.Extra[293];
					Color color = Color.Cyan * 0.7f;
					color.A /= 2;
					Vector2 vector2 = new Vector2((float)particlePreparation3.x, (float)particlePreparation3.y);
					float num2 = 1f;
					switch (particlePreparation3.type)
					{
					case RichRoomCheckFeedback.Reason.BlockedWall:
						asset = TextureAssets.Extra[292];
						color = new Color(80, 255, 255) * 0.7f;
						color.A /= 2;
						goto IL_031D;
					case RichRoomCheckFeedback.Reason.UnsafeWall:
						asset = TextureAssets.Extra[298];
						color = new Color(255, 40, 40, 255);
						goto IL_023C;
					case RichRoomCheckFeedback.Reason.OpenAir:
					case RichRoomCheckFeedback.Reason.Hazard:
						asset = TextureAssets.Extra[292];
						color = new Color(255, 40, 40, 255);
						goto IL_023C;
					}
					num2 = 1.5f;
					if (flag)
					{
						goto IL_031D;
					}
					IL_023C:
					RoomCheckParticle roomCheckParticle = RichRoomCheckFeedback._particlePool.RequestParticle();
					roomCheckParticle.SetBasicInfo(asset, null, Vector2.Zero, new Vector2((float)(particlePreparation3.x * 16 + 8), (float)(particlePreparation3.y * 16 + 8)));
					roomCheckParticle.Delay = (int)(3f * Vector2.Distance(vector, vector2));
					float num3 = num - (float)roomCheckParticle.Delay;
					roomCheckParticle.SetTypeInfo(num3 * num2, true);
					roomCheckParticle.FadeInNormalizedTime = Utils.Remap(num - 24f, (float)roomCheckParticle.Delay, num, 0f, 1f, true);
					roomCheckParticle.FadeOutNormalizedTime = Utils.Remap(num - 6f, (float)roomCheckParticle.Delay, num, 0f, 1f, true);
					roomCheckParticle.ColorTint = color;
					roomCheckParticle.Scale = Vector2.One;
					Main.ParticleSystem_World_OverPlayers.Add(roomCheckParticle);
				}
				IL_031D:;
			}
			for (int l = 0; l < this._spaceCount; l++)
			{
				RichRoomCheckFeedback.ParticlePreparation particlePreparation4 = this._space[l];
				if (!particlePreparation4.consumed)
				{
					ushort type2 = Main.tile[particlePreparation4.x, particlePreparation4.y].type;
					if (TileID.Sets.RoomNeeds.CountsAsTable[(int)type2] || TileID.Sets.RoomNeeds.CountsAsChair[(int)type2] || TileID.Sets.RoomNeeds.CountsAsTorch[(int)type2] || TileID.Sets.RoomNeeds.CountsAsDoor[(int)type2])
					{
						Rectangle rectangle;
						TileObjectData.TryGetTileBounds(particlePreparation4.x, particlePreparation4.y, out rectangle);
						for (int m = 0; m < this._spaceCount; m++)
						{
							if (m != l)
							{
								RichRoomCheckFeedback.ParticlePreparation particlePreparation5 = this._space[m];
								if (particlePreparation5.x >= rectangle.Left && particlePreparation5.x < rectangle.Right && particlePreparation5.y >= rectangle.Top && particlePreparation5.y < rectangle.Bottom)
								{
									this._space[m].consumed = true;
								}
							}
						}
					}
				}
			}
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			for (int n = 0; n < this._spaceCount; n++)
			{
				RichRoomCheckFeedback.ParticlePreparation particlePreparation6 = this._space[n];
				if (!particlePreparation6.consumed)
				{
					ushort type3 = Main.tile[particlePreparation6.x, particlePreparation6.y].type;
					bool flag6 = TileID.Sets.RoomNeeds.CountsAsTable[(int)type3];
					bool flag7 = TileID.Sets.RoomNeeds.CountsAsChair[(int)type3];
					bool flag8 = TileID.Sets.RoomNeeds.CountsAsTorch[(int)type3];
					bool flag9 = TileID.Sets.RoomNeeds.CountsAsDoor[(int)type3];
					if (flag6 || flag7 || flag9 || flag8)
					{
						Asset<Texture2D> asset2 = TextureAssets.Extra[293];
						if (flag6)
						{
							if (flag3)
							{
								goto IL_0711;
							}
							flag3 = true;
							asset2 = TextureAssets.Extra[297];
						}
						if (flag7)
						{
							if (flag2)
							{
								goto IL_0711;
							}
							flag2 = true;
							asset2 = TextureAssets.Extra[295];
						}
						if (flag9)
						{
							if (flag5)
							{
								goto IL_0711;
							}
							flag5 = true;
							asset2 = TextureAssets.Extra[296];
						}
						if (flag8)
						{
							if (flag4)
							{
								goto IL_0711;
							}
							flag4 = true;
							asset2 = TextureAssets.Extra[294];
						}
						Rectangle rectangle2;
						TileObjectData.TryGetTileBounds(particlePreparation6.x, particlePreparation6.y, out rectangle2);
						Color color2;
						(Color.LimeGreen * 0.8f).A = color2.A / 2;
						Vector2 vector3 = new Vector2((float)particlePreparation6.x, (float)particlePreparation6.y);
						RoomCheckParticle roomCheckParticle2 = RichRoomCheckFeedback._particlePool.RequestParticle();
						Vector2 vector4 = new Vector2((float)(rectangle2.Left + rectangle2.Right) / 2f, MathHelper.Min((float)rectangle2.Top / 2f + (float)rectangle2.Bottom / 2f, (float)(rectangle2.Top + 1)));
						roomCheckParticle2.SetBasicInfo(asset2, null, Vector2.Zero, vector4 * 16f + new Vector2(0f, (float)(-(float)asset2.Height() / 2)));
						roomCheckParticle2.Delay = (int)(3f * Vector2.Distance(vector, vector3));
						float num4 = num - (float)roomCheckParticle2.Delay;
						roomCheckParticle2.SetTypeInfo(num4, true);
						roomCheckParticle2.FadeInNormalizedTime = Utils.Remap(num - 24f, (float)roomCheckParticle2.Delay, num, 0f, 1f, true);
						roomCheckParticle2.FadeOutNormalizedTime = Utils.Remap(num - 6f, (float)roomCheckParticle2.Delay, num, 0f, 1f, true);
						roomCheckParticle2.Scale = Vector2.One;
						int num5 = 32;
						RoomCheckParticle roomCheckParticle3 = roomCheckParticle2;
						roomCheckParticle3.LocalPosition.Y = roomCheckParticle3.LocalPosition.Y - (float)num5;
						roomCheckParticle2.Velocity = new Vector2(0f, (float)num5 * 2.5f) / num4;
						roomCheckParticle2.AccelerationPerFrame = -roomCheckParticle2.Velocity * 1.25f / num4;
						Main.ParticleSystem_World_OverPlayers.Add(roomCheckParticle2);
					}
				}
				IL_0711:;
			}
		}

		// Token: 0x0600372E RID: 14126 RVA: 0x0062D004 File Offset: 0x0062B204
		private float GetHighestDistanceFromOrigin(ref Vector2 origin)
		{
			float num = 0f;
			for (int i = 0; i < this._spaceCount; i++)
			{
				RichRoomCheckFeedback.ParticlePreparation particlePreparation = this._space[i];
				Vector2 vector = new Vector2((float)particlePreparation.x, (float)particlePreparation.y);
				float num2 = Vector2.Distance(origin, vector);
				if (num < num2)
				{
					num = num2;
				}
			}
			return num;
		}

		// Token: 0x0600372F RID: 14127 RVA: 0x0062D061 File Offset: 0x0062B261
		public void BeginScoring()
		{
			this._bestScore = default(RichRoomCheckFeedback.ScorePreparation);
			this._scoreCount = 0;
		}

		// Token: 0x06003730 RID: 14128 RVA: 0x0062D078 File Offset: 0x0062B278
		public void ReportScore(int x, int y, int score)
		{
			if (this._scoreCount >= this._score.Length)
			{
				Array.Resize<RichRoomCheckFeedback.ScorePreparation>(ref this._score, this._score.Length * 2);
			}
			RichRoomCheckFeedback.ScorePreparation[] score2 = this._score;
			int scoreCount = this._scoreCount;
			this._scoreCount = scoreCount + 1;
			score2[scoreCount] = new RichRoomCheckFeedback.ScorePreparation
			{
				x = x,
				y = y,
				score = score
			};
		}

		// Token: 0x06003731 RID: 14129 RVA: 0x0062D0E8 File Offset: 0x0062B2E8
		public void SetAsHighScore(int x, int y, int score)
		{
			this._bestScore = new RichRoomCheckFeedback.ScorePreparation
			{
				x = x,
				y = y,
				score = score
			};
		}

		// Token: 0x06003732 RID: 14130 RVA: 0x0062D11C File Offset: 0x0062B31C
		public void EndScoring()
		{
			Vector2 vector = new Vector2((float)this._originX, (float)this._originY);
			float num = 0f;
			for (int i = 0; i < this._spaceCount; i++)
			{
				RichRoomCheckFeedback.ParticlePreparation particlePreparation = this._space[i];
				Vector2 vector2 = new Vector2((float)particlePreparation.x, (float)particlePreparation.y);
				float num2 = Vector2.Distance(vector, vector2);
				if (num < num2)
				{
					num = num2;
				}
			}
			float num3 = 3f * num + 90f;
			int score = this._bestScore.score;
			if (score == 0)
			{
				return;
			}
			for (int j = 0; j < this._scoreCount; j++)
			{
				RichRoomCheckFeedback.ScorePreparation scorePreparation = this._score[j];
				if (scorePreparation.score != 0 && (scorePreparation.x != this._bestScore.x || scorePreparation.y != this._bestScore.y))
				{
					Asset<Texture2D> asset = TextureAssets.Extra[293];
					RoomCheckParticle roomCheckParticle = RichRoomCheckFeedback._particlePool.RequestParticle();
					roomCheckParticle.SetBasicInfo(asset, null, Vector2.Zero, new Vector2((float)(scorePreparation.x * 16 + 8), (float)(scorePreparation.y * 16 + 8 - 16)));
					Vector2 vector3 = new Vector2((float)scorePreparation.x, (float)scorePreparation.y);
					roomCheckParticle.Delay = (int)(3f * Vector2.Distance(vector, vector3));
					float num4 = num3 - (float)roomCheckParticle.Delay;
					roomCheckParticle.SetTypeInfo(num4, true);
					roomCheckParticle.FadeInNormalizedTime = Utils.Remap(num3 - 24f, (float)roomCheckParticle.Delay, num3, 0f, 1f, true);
					roomCheckParticle.FadeOutNormalizedTime = Utils.Remap(num3 - 6f, (float)roomCheckParticle.Delay, num3, 0f, 1f, true);
					if (scorePreparation.score > 0)
					{
						roomCheckParticle.ColorTint = Color.LimeGreen * (0.5f + 0.5f * (float)scorePreparation.score / (float)score);
					}
					else
					{
						roomCheckParticle.ColorTint = Color.Red * ((float)scorePreparation.score / (float)(-(float)score));
					}
					roomCheckParticle.Scale = Vector2.One * 2f;
					Main.ParticleSystem_World_OverPlayers.Add(roomCheckParticle);
				}
			}
			for (int k = 0; k < 1; k++)
			{
				RichRoomCheckFeedback.ScorePreparation bestScore = this._bestScore;
				if (bestScore.score != 0)
				{
					Asset<Texture2D> asset2 = TextureAssets.Extra[293];
					RoomCheckParticle roomCheckParticle2 = RichRoomCheckFeedback._particlePool.RequestParticle();
					roomCheckParticle2.SetBasicInfo(asset2, null, Vector2.Zero, new Vector2((float)(bestScore.x * 16 + 8), (float)(bestScore.y * 16 + 8 - 16)));
					Vector2 vector4 = new Vector2((float)bestScore.x, (float)bestScore.y);
					roomCheckParticle2.Delay = (int)(3f * Vector2.Distance(vector, vector4));
					float num5 = num3 - (float)roomCheckParticle2.Delay;
					roomCheckParticle2.SetTypeInfo(num5, true);
					roomCheckParticle2.FadeInNormalizedTime = Utils.Remap(num3 - 24f, (float)roomCheckParticle2.Delay, num3, 0f, 1f, true);
					roomCheckParticle2.FadeOutNormalizedTime = Utils.Remap(num3 - 6f, (float)roomCheckParticle2.Delay, num3, 0f, 1f, true);
					roomCheckParticle2.ColorTint = Main.OurFavoriteColor;
					roomCheckParticle2.Scale = Vector2.One * 3f;
					Main.ParticleSystem_World_OverPlayers.Add(roomCheckParticle2);
				}
			}
		}

		// Token: 0x06003733 RID: 14131 RVA: 0x0062D4A3 File Offset: 0x0062B6A3
		public RichRoomCheckFeedback()
		{
		}

		// Token: 0x06003734 RID: 14132 RVA: 0x0062D4CB File Offset: 0x0062B6CB
		// Note: this type is marked as 'beforefieldinit'.
		static RichRoomCheckFeedback()
		{
		}

		// Token: 0x04005B63 RID: 23395
		public static RichRoomCheckFeedback Instance = new RichRoomCheckFeedback();

		// Token: 0x04005B64 RID: 23396
		private static ParticlePool<RoomCheckParticle> _particlePool = new ParticlePool<RoomCheckParticle>(100, new ParticlePool<RoomCheckParticle>.ParticleInstantiator(RichRoomCheckFeedback.GetNewParticle));

		// Token: 0x04005B65 RID: 23397
		private RichRoomCheckFeedback.ParticlePreparation[] _space = new RichRoomCheckFeedback.ParticlePreparation[128];

		// Token: 0x04005B66 RID: 23398
		private int _spaceCount;

		// Token: 0x04005B67 RID: 23399
		private int _highestIteration;

		// Token: 0x04005B68 RID: 23400
		private RichRoomCheckFeedback.ScorePreparation[] _score = new RichRoomCheckFeedback.ScorePreparation[128];

		// Token: 0x04005B69 RID: 23401
		private int _scoreCount;

		// Token: 0x04005B6A RID: 23402
		private RichRoomCheckFeedback.ScorePreparation _bestScore;

		// Token: 0x04005B6B RID: 23403
		private int _originX;

		// Token: 0x04005B6C RID: 23404
		private int _originY;

		// Token: 0x020009AD RID: 2477
		private enum Reason
		{
			// Token: 0x040076A6 RID: 30374
			BlockedWall,
			// Token: 0x040076A7 RID: 30375
			UnsafeWall,
			// Token: 0x040076A8 RID: 30376
			OpenAir,
			// Token: 0x040076A9 RID: 30377
			Good,
			// Token: 0x040076AA RID: 30378
			Hazard
		}

		// Token: 0x020009AE RID: 2478
		private struct ParticlePreparation
		{
			// Token: 0x040076AB RID: 30379
			public RichRoomCheckFeedback.Reason type;

			// Token: 0x040076AC RID: 30380
			public int x;

			// Token: 0x040076AD RID: 30381
			public int y;

			// Token: 0x040076AE RID: 30382
			public int iteration;

			// Token: 0x040076AF RID: 30383
			public bool consumed;
		}

		// Token: 0x020009AF RID: 2479
		private struct ScorePreparation
		{
			// Token: 0x040076B0 RID: 30384
			public int x;

			// Token: 0x040076B1 RID: 30385
			public int y;

			// Token: 0x040076B2 RID: 30386
			public int score;
		}
	}
}
