using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.Animations
{
	// Token: 0x0200052E RID: 1326
	public class Actions
	{
		// Token: 0x060036F6 RID: 14070 RVA: 0x0000357B File Offset: 0x0000177B
		public Actions()
		{
		}

		// Token: 0x020009AA RID: 2474
		public class Players
		{
			// Token: 0x060049D4 RID: 18900 RVA: 0x0000357B File Offset: 0x0000177B
			public Players()
			{
			}

			// Token: 0x02000AF6 RID: 2806
			public interface IPlayerAction : IAnimationSegmentAction<Player>
			{
			}

			// Token: 0x02000AF7 RID: 2807
			public class Fade : Actions.Players.IPlayerAction, IAnimationSegmentAction<Player>
			{
				// Token: 0x06004D32 RID: 19762 RVA: 0x006DBB8E File Offset: 0x006D9D8E
				public Fade(float opacityTarget)
				{
					this._duration = 0;
					this._opacityTarget = opacityTarget;
				}

				// Token: 0x06004D33 RID: 19763 RVA: 0x006DBBA4 File Offset: 0x006D9DA4
				public Fade(float opacityTarget, int duration)
				{
					this._duration = duration;
					this._opacityTarget = opacityTarget;
				}

				// Token: 0x06004D34 RID: 19764 RVA: 0x00009E46 File Offset: 0x00008046
				public void BindTo(Player obj)
				{
				}

				// Token: 0x170005C3 RID: 1475
				// (get) Token: 0x06004D35 RID: 19765 RVA: 0x006DBBBA File Offset: 0x006D9DBA
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return this._duration;
					}
				}

				// Token: 0x06004D36 RID: 19766 RVA: 0x006DBBC2 File Offset: 0x006D9DC2
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x06004D37 RID: 19767 RVA: 0x006DBBCC File Offset: 0x006D9DCC
				public void ApplyTo(Player obj, float localTimeForObj)
				{
					if (localTimeForObj < this._delay)
					{
						return;
					}
					if (this._duration == 0)
					{
						obj.opacityForAnimation = this._opacityTarget;
						return;
					}
					float num = localTimeForObj - this._delay;
					if (num > (float)this._duration)
					{
						num = (float)this._duration;
					}
					obj.opacityForAnimation = MathHelper.Lerp(obj.opacityForAnimation, this._opacityTarget, Utils.GetLerpValue(0f, (float)this._duration, num, true));
				}

				// Token: 0x040078D2 RID: 30930
				private int _duration;

				// Token: 0x040078D3 RID: 30931
				private float _opacityTarget;

				// Token: 0x040078D4 RID: 30932
				private float _delay;
			}

			// Token: 0x02000AF8 RID: 2808
			public class Wait : Actions.Players.IPlayerAction, IAnimationSegmentAction<Player>
			{
				// Token: 0x06004D38 RID: 19768 RVA: 0x006DBC3D File Offset: 0x006D9E3D
				public Wait(int durationInFrames)
				{
					this._duration = durationInFrames;
				}

				// Token: 0x06004D39 RID: 19769 RVA: 0x00009E46 File Offset: 0x00008046
				public void BindTo(Player obj)
				{
				}

				// Token: 0x170005C4 RID: 1476
				// (get) Token: 0x06004D3A RID: 19770 RVA: 0x006DBC4C File Offset: 0x006D9E4C
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return this._duration;
					}
				}

				// Token: 0x06004D3B RID: 19771 RVA: 0x006DBC54 File Offset: 0x006D9E54
				public void ApplyTo(Player obj, float localTimeForObj)
				{
					if (localTimeForObj < this._delay)
					{
						return;
					}
					obj.velocity = Vector2.Zero;
				}

				// Token: 0x06004D3C RID: 19772 RVA: 0x006DBC6B File Offset: 0x006D9E6B
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x040078D5 RID: 30933
				private int _duration;

				// Token: 0x040078D6 RID: 30934
				private float _delay;
			}

			// Token: 0x02000AF9 RID: 2809
			public class LookAt : Actions.Players.IPlayerAction, IAnimationSegmentAction<Player>
			{
				// Token: 0x06004D3D RID: 19773 RVA: 0x006DBC74 File Offset: 0x006D9E74
				public LookAt(int direction)
				{
					this._direction = direction;
				}

				// Token: 0x06004D3E RID: 19774 RVA: 0x00009E46 File Offset: 0x00008046
				public void BindTo(Player obj)
				{
				}

				// Token: 0x170005C5 RID: 1477
				// (get) Token: 0x06004D3F RID: 19775 RVA: 0x001DAC3B File Offset: 0x001D8E3B
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return 0;
					}
				}

				// Token: 0x06004D40 RID: 19776 RVA: 0x006DBC83 File Offset: 0x006D9E83
				public void ApplyTo(Player obj, float localTimeForObj)
				{
					if (localTimeForObj < this._delay)
					{
						return;
					}
					obj.direction = this._direction;
				}

				// Token: 0x06004D41 RID: 19777 RVA: 0x006DBC9B File Offset: 0x006D9E9B
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x040078D7 RID: 30935
				private int _direction;

				// Token: 0x040078D8 RID: 30936
				private float _delay;
			}

			// Token: 0x02000AFA RID: 2810
			public class MoveWithAcceleration : Actions.Players.IPlayerAction, IAnimationSegmentAction<Player>
			{
				// Token: 0x06004D42 RID: 19778 RVA: 0x006DBCA4 File Offset: 0x006D9EA4
				public MoveWithAcceleration(Vector2 offsetPerFrame, Vector2 accelerationPerFrame, int durationInFrames)
				{
					this._accelerationPerFrame = accelerationPerFrame;
					this._offsetPerFrame = offsetPerFrame;
					this._duration = durationInFrames;
				}

				// Token: 0x06004D43 RID: 19779 RVA: 0x00009E46 File Offset: 0x00008046
				public void BindTo(Player obj)
				{
				}

				// Token: 0x170005C6 RID: 1478
				// (get) Token: 0x06004D44 RID: 19780 RVA: 0x006DBCC1 File Offset: 0x006D9EC1
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return this._duration;
					}
				}

				// Token: 0x06004D45 RID: 19781 RVA: 0x006DBCC9 File Offset: 0x006D9EC9
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x06004D46 RID: 19782 RVA: 0x006DBCD4 File Offset: 0x006D9ED4
				public void ApplyTo(Player obj, float localTimeForObj)
				{
					if (localTimeForObj < this._delay)
					{
						return;
					}
					float num = localTimeForObj - this._delay;
					if (num > (float)this._duration)
					{
						num = (float)this._duration;
					}
					Vector2 vector = this._offsetPerFrame * num + this._accelerationPerFrame * (num * num * 0.5f);
					obj.position += vector;
					obj.velocity = this._offsetPerFrame + this._accelerationPerFrame * num;
					if (this._offsetPerFrame.X != 0f)
					{
						obj.direction = ((this._offsetPerFrame.X > 0f) ? 1 : (-1));
					}
				}

				// Token: 0x040078D9 RID: 30937
				private Vector2 _offsetPerFrame;

				// Token: 0x040078DA RID: 30938
				private Vector2 _accelerationPerFrame;

				// Token: 0x040078DB RID: 30939
				private int _duration;

				// Token: 0x040078DC RID: 30940
				private float _delay;
			}
		}

		// Token: 0x020009AB RID: 2475
		public class NPCs
		{
			// Token: 0x060049D5 RID: 18901 RVA: 0x0000357B File Offset: 0x0000177B
			public NPCs()
			{
			}

			// Token: 0x02000AFB RID: 2811
			public interface INPCAction : IAnimationSegmentAction<NPC>
			{
			}

			// Token: 0x02000AFC RID: 2812
			public class Fade : Actions.NPCs.INPCAction, IAnimationSegmentAction<NPC>
			{
				// Token: 0x06004D47 RID: 19783 RVA: 0x006DBD89 File Offset: 0x006D9F89
				public Fade(int alphaPerFrame)
				{
					this._duration = 0;
					this._alphaPerFrame = alphaPerFrame;
				}

				// Token: 0x06004D48 RID: 19784 RVA: 0x006DBD9F File Offset: 0x006D9F9F
				public Fade(int alphaPerFrame, int duration)
				{
					this._duration = duration;
					this._alphaPerFrame = alphaPerFrame;
				}

				// Token: 0x06004D49 RID: 19785 RVA: 0x00009E46 File Offset: 0x00008046
				public void BindTo(NPC obj)
				{
				}

				// Token: 0x170005C7 RID: 1479
				// (get) Token: 0x06004D4A RID: 19786 RVA: 0x006DBDB5 File Offset: 0x006D9FB5
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return this._duration;
					}
				}

				// Token: 0x06004D4B RID: 19787 RVA: 0x006DBDBD File Offset: 0x006D9FBD
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x06004D4C RID: 19788 RVA: 0x006DBDC8 File Offset: 0x006D9FC8
				public void ApplyTo(NPC obj, float localTimeForObj)
				{
					if (localTimeForObj < this._delay)
					{
						return;
					}
					if (this._duration == 0)
					{
						obj.alpha = Utils.Clamp<int>(obj.alpha + this._alphaPerFrame, 0, 255);
						return;
					}
					float num = localTimeForObj - this._delay;
					if (num > (float)this._duration)
					{
						num = (float)this._duration;
					}
					obj.alpha = Utils.Clamp<int>(obj.alpha + (int)num * this._alphaPerFrame, 0, 255);
				}

				// Token: 0x040078DD RID: 30941
				private int _duration;

				// Token: 0x040078DE RID: 30942
				private int _alphaPerFrame;

				// Token: 0x040078DF RID: 30943
				private float _delay;
			}

			// Token: 0x02000AFD RID: 2813
			public class ShowItem : Actions.NPCs.INPCAction, IAnimationSegmentAction<NPC>
			{
				// Token: 0x06004D4D RID: 19789 RVA: 0x006DBE42 File Offset: 0x006DA042
				public ShowItem(int durationInFrames, int itemIdToShow)
				{
					this._duration = durationInFrames;
					this._itemIdToShow = itemIdToShow;
				}

				// Token: 0x06004D4E RID: 19790 RVA: 0x00009E46 File Offset: 0x00008046
				public void BindTo(NPC obj)
				{
				}

				// Token: 0x170005C8 RID: 1480
				// (get) Token: 0x06004D4F RID: 19791 RVA: 0x006DBE58 File Offset: 0x006DA058
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return this._duration;
					}
				}

				// Token: 0x06004D50 RID: 19792 RVA: 0x006DBE60 File Offset: 0x006DA060
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x06004D51 RID: 19793 RVA: 0x006DBE6C File Offset: 0x006DA06C
				public void ApplyTo(NPC obj, float localTimeForObj)
				{
					if (localTimeForObj < this._delay)
					{
						return;
					}
					float num = localTimeForObj - this._delay;
					if (num > (float)this._duration)
					{
						this.FixNPCIfWasHoldingItem(obj);
						return;
					}
					obj.velocity = Vector2.Zero;
					obj.frameCounter = (double)num;
					obj.ai[0] = 23f;
					obj.ai[1] = (float)this._duration - num;
					obj.ai[2] = (float)this._itemIdToShow;
				}

				// Token: 0x06004D52 RID: 19794 RVA: 0x006DBEE0 File Offset: 0x006DA0E0
				private void FixNPCIfWasHoldingItem(NPC obj)
				{
					if (obj.ai[0] == 23f)
					{
						obj.frameCounter = 0.0;
						obj.ai[0] = 0f;
						obj.ai[1] = 0f;
						obj.ai[2] = 0f;
					}
				}

				// Token: 0x040078E0 RID: 30944
				private int _itemIdToShow;

				// Token: 0x040078E1 RID: 30945
				private int _duration;

				// Token: 0x040078E2 RID: 30946
				private float _delay;
			}

			// Token: 0x02000AFE RID: 2814
			public class Move : Actions.NPCs.INPCAction, IAnimationSegmentAction<NPC>
			{
				// Token: 0x06004D53 RID: 19795 RVA: 0x006DBF32 File Offset: 0x006DA132
				public Move(Vector2 offsetPerFrame, int durationInFrames)
				{
					this._offsetPerFrame = offsetPerFrame;
					this._duration = durationInFrames;
				}

				// Token: 0x06004D54 RID: 19796 RVA: 0x00009E46 File Offset: 0x00008046
				public void BindTo(NPC obj)
				{
				}

				// Token: 0x170005C9 RID: 1481
				// (get) Token: 0x06004D55 RID: 19797 RVA: 0x006DBF48 File Offset: 0x006DA148
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return this._duration;
					}
				}

				// Token: 0x06004D56 RID: 19798 RVA: 0x006DBF50 File Offset: 0x006DA150
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x06004D57 RID: 19799 RVA: 0x006DBF5C File Offset: 0x006DA15C
				public void ApplyTo(NPC obj, float localTimeForObj)
				{
					if (localTimeForObj < this._delay)
					{
						return;
					}
					float num = localTimeForObj - this._delay;
					if (num > (float)this._duration)
					{
						num = (float)this._duration;
					}
					obj.position += this._offsetPerFrame * num;
					obj.velocity = this._offsetPerFrame;
					if (this._offsetPerFrame.X != 0f)
					{
						obj.direction = (obj.spriteDirection = ((this._offsetPerFrame.X > 0f) ? 1 : (-1)));
					}
				}

				// Token: 0x040078E3 RID: 30947
				private Vector2 _offsetPerFrame;

				// Token: 0x040078E4 RID: 30948
				private int _duration;

				// Token: 0x040078E5 RID: 30949
				private float _delay;
			}

			// Token: 0x02000AFF RID: 2815
			public class MoveWithAcceleration : Actions.NPCs.INPCAction, IAnimationSegmentAction<NPC>
			{
				// Token: 0x06004D58 RID: 19800 RVA: 0x006DBFEE File Offset: 0x006DA1EE
				public MoveWithAcceleration(Vector2 offsetPerFrame, Vector2 accelerationPerFrame, int durationInFrames)
				{
					this._accelerationPerFrame = accelerationPerFrame;
					this._offsetPerFrame = offsetPerFrame;
					this._duration = durationInFrames;
				}

				// Token: 0x06004D59 RID: 19801 RVA: 0x00009E46 File Offset: 0x00008046
				public void BindTo(NPC obj)
				{
				}

				// Token: 0x170005CA RID: 1482
				// (get) Token: 0x06004D5A RID: 19802 RVA: 0x006DC00B File Offset: 0x006DA20B
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return this._duration;
					}
				}

				// Token: 0x06004D5B RID: 19803 RVA: 0x006DC013 File Offset: 0x006DA213
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x06004D5C RID: 19804 RVA: 0x006DC01C File Offset: 0x006DA21C
				public void ApplyTo(NPC obj, float localTimeForObj)
				{
					if (localTimeForObj < this._delay)
					{
						return;
					}
					float num = localTimeForObj - this._delay;
					if (num > (float)this._duration)
					{
						num = (float)this._duration;
					}
					Vector2 vector = this._offsetPerFrame * num + this._accelerationPerFrame * (num * num * 0.5f);
					obj.position += vector;
					obj.velocity = this._offsetPerFrame + this._accelerationPerFrame * num;
					if (this._offsetPerFrame.X != 0f)
					{
						obj.direction = (obj.spriteDirection = ((this._offsetPerFrame.X > 0f) ? 1 : (-1)));
					}
				}

				// Token: 0x040078E6 RID: 30950
				private Vector2 _offsetPerFrame;

				// Token: 0x040078E7 RID: 30951
				private Vector2 _accelerationPerFrame;

				// Token: 0x040078E8 RID: 30952
				private int _duration;

				// Token: 0x040078E9 RID: 30953
				private float _delay;
			}

			// Token: 0x02000B00 RID: 2816
			public class MoveWithRotor : Actions.NPCs.INPCAction, IAnimationSegmentAction<NPC>
			{
				// Token: 0x06004D5D RID: 19805 RVA: 0x006DC0DA File Offset: 0x006DA2DA
				public MoveWithRotor(Vector2 radialOffset, float rotationPerFrame, Vector2 resultMultiplier, int durationInFrames)
				{
					this._radialOffset = rotationPerFrame;
					this._offsetPerFrame = radialOffset;
					this._resultMultiplier = resultMultiplier;
					this._duration = durationInFrames;
				}

				// Token: 0x06004D5E RID: 19806 RVA: 0x00009E46 File Offset: 0x00008046
				public void BindTo(NPC obj)
				{
				}

				// Token: 0x170005CB RID: 1483
				// (get) Token: 0x06004D5F RID: 19807 RVA: 0x006DC0FF File Offset: 0x006DA2FF
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return this._duration;
					}
				}

				// Token: 0x06004D60 RID: 19808 RVA: 0x006DC107 File Offset: 0x006DA307
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x06004D61 RID: 19809 RVA: 0x006DC110 File Offset: 0x006DA310
				public void ApplyTo(NPC obj, float localTimeForObj)
				{
					if (localTimeForObj < this._delay)
					{
						return;
					}
					float num = localTimeForObj - this._delay;
					if (num > (float)this._duration)
					{
						num = (float)this._duration;
					}
					Vector2 vector = this._offsetPerFrame.RotatedBy((double)(this._radialOffset * num), default(Vector2)) * this._resultMultiplier;
					obj.position += vector;
				}

				// Token: 0x040078EA RID: 30954
				private Vector2 _offsetPerFrame;

				// Token: 0x040078EB RID: 30955
				private Vector2 _resultMultiplier;

				// Token: 0x040078EC RID: 30956
				private float _radialOffset;

				// Token: 0x040078ED RID: 30957
				private int _duration;

				// Token: 0x040078EE RID: 30958
				private float _delay;
			}

			// Token: 0x02000B01 RID: 2817
			public class DoBunnyRestAnimation : Actions.NPCs.INPCAction, IAnimationSegmentAction<NPC>
			{
				// Token: 0x06004D62 RID: 19810 RVA: 0x006DC17D File Offset: 0x006DA37D
				public DoBunnyRestAnimation(int durationInFrames)
				{
					this._duration = durationInFrames;
				}

				// Token: 0x06004D63 RID: 19811 RVA: 0x00009E46 File Offset: 0x00008046
				public void BindTo(NPC obj)
				{
				}

				// Token: 0x170005CC RID: 1484
				// (get) Token: 0x06004D64 RID: 19812 RVA: 0x006DC18C File Offset: 0x006DA38C
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return this._duration;
					}
				}

				// Token: 0x06004D65 RID: 19813 RVA: 0x006DC194 File Offset: 0x006DA394
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x06004D66 RID: 19814 RVA: 0x006DC1A0 File Offset: 0x006DA3A0
				public void ApplyTo(NPC obj, float localTimeForObj)
				{
					if (localTimeForObj < this._delay)
					{
						return;
					}
					float num = localTimeForObj - this._delay;
					if (num > (float)this._duration)
					{
						num = (float)this._duration;
					}
					Rectangle frame = obj.frame;
					int num2 = 10;
					int i = (int)num;
					while (i > 4)
					{
						i -= 4;
						num2++;
						if (num2 > 13)
						{
							num2 = 13;
						}
					}
					obj.ai[0] = 21f;
					obj.ai[1] = 31f;
					obj.frameCounter = (double)i;
					obj.frame.Y = num2 * frame.Height;
				}

				// Token: 0x040078EF RID: 30959
				private int _duration;

				// Token: 0x040078F0 RID: 30960
				private float _delay;
			}

			// Token: 0x02000B02 RID: 2818
			public class Wait : Actions.NPCs.INPCAction, IAnimationSegmentAction<NPC>
			{
				// Token: 0x06004D67 RID: 19815 RVA: 0x006DC22A File Offset: 0x006DA42A
				public Wait(int durationInFrames)
				{
					this._duration = durationInFrames;
				}

				// Token: 0x06004D68 RID: 19816 RVA: 0x00009E46 File Offset: 0x00008046
				public void BindTo(NPC obj)
				{
				}

				// Token: 0x170005CD RID: 1485
				// (get) Token: 0x06004D69 RID: 19817 RVA: 0x006DC239 File Offset: 0x006DA439
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return this._duration;
					}
				}

				// Token: 0x06004D6A RID: 19818 RVA: 0x006DC241 File Offset: 0x006DA441
				public void ApplyTo(NPC obj, float localTimeForObj)
				{
					if (localTimeForObj < this._delay)
					{
						return;
					}
					obj.velocity = Vector2.Zero;
				}

				// Token: 0x06004D6B RID: 19819 RVA: 0x006DC258 File Offset: 0x006DA458
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x040078F1 RID: 30961
				private int _duration;

				// Token: 0x040078F2 RID: 30962
				private float _delay;
			}

			// Token: 0x02000B03 RID: 2819
			public class Blink : Actions.NPCs.INPCAction, IAnimationSegmentAction<NPC>
			{
				// Token: 0x06004D6C RID: 19820 RVA: 0x006DC261 File Offset: 0x006DA461
				public Blink(int durationInFrames)
				{
					this._duration = durationInFrames;
				}

				// Token: 0x06004D6D RID: 19821 RVA: 0x00009E46 File Offset: 0x00008046
				public void BindTo(NPC obj)
				{
				}

				// Token: 0x170005CE RID: 1486
				// (get) Token: 0x06004D6E RID: 19822 RVA: 0x006DC270 File Offset: 0x006DA470
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return this._duration;
					}
				}

				// Token: 0x06004D6F RID: 19823 RVA: 0x006DC278 File Offset: 0x006DA478
				public void ApplyTo(NPC obj, float localTimeForObj)
				{
					if (localTimeForObj < this._delay)
					{
						return;
					}
					obj.velocity = Vector2.Zero;
					obj.ai[0] = 0f;
					if (localTimeForObj > this._delay + (float)this._duration)
					{
						return;
					}
					obj.ai[0] = 1001f;
				}

				// Token: 0x06004D70 RID: 19824 RVA: 0x006DC2C6 File Offset: 0x006DA4C6
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x040078F3 RID: 30963
				private int _duration;

				// Token: 0x040078F4 RID: 30964
				private float _delay;
			}

			// Token: 0x02000B04 RID: 2820
			public class LookAt : Actions.NPCs.INPCAction, IAnimationSegmentAction<NPC>
			{
				// Token: 0x06004D71 RID: 19825 RVA: 0x006DC2CF File Offset: 0x006DA4CF
				public LookAt(int direction)
				{
					this._direction = direction;
				}

				// Token: 0x06004D72 RID: 19826 RVA: 0x00009E46 File Offset: 0x00008046
				public void BindTo(NPC obj)
				{
				}

				// Token: 0x170005CF RID: 1487
				// (get) Token: 0x06004D73 RID: 19827 RVA: 0x001DAC3B File Offset: 0x001D8E3B
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return 0;
					}
				}

				// Token: 0x06004D74 RID: 19828 RVA: 0x006DC2E0 File Offset: 0x006DA4E0
				public void ApplyTo(NPC obj, float localTimeForObj)
				{
					if (localTimeForObj < this._delay)
					{
						return;
					}
					obj.direction = (obj.spriteDirection = this._direction);
				}

				// Token: 0x06004D75 RID: 19829 RVA: 0x006DC30C File Offset: 0x006DA50C
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x040078F5 RID: 30965
				private int _direction;

				// Token: 0x040078F6 RID: 30966
				private float _delay;
			}

			// Token: 0x02000B05 RID: 2821
			public class PartyHard : Actions.NPCs.INPCAction, IAnimationSegmentAction<NPC>
			{
				// Token: 0x06004D76 RID: 19830 RVA: 0x0000357B File Offset: 0x0000177B
				public PartyHard()
				{
				}

				// Token: 0x06004D77 RID: 19831 RVA: 0x006DC315 File Offset: 0x006DA515
				public void BindTo(NPC obj)
				{
					obj.ForcePartyHatOn = true;
					obj.UpdateAltTexture();
				}

				// Token: 0x170005D0 RID: 1488
				// (get) Token: 0x06004D78 RID: 19832 RVA: 0x001DAC3B File Offset: 0x001D8E3B
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return 0;
					}
				}

				// Token: 0x06004D79 RID: 19833 RVA: 0x00009E46 File Offset: 0x00008046
				public void ApplyTo(NPC obj, float localTimeForObj)
				{
				}

				// Token: 0x06004D7A RID: 19834 RVA: 0x00009E46 File Offset: 0x00008046
				public void SetDelay(float delay)
				{
				}
			}

			// Token: 0x02000B06 RID: 2822
			public class ForceAltTexture : Actions.NPCs.INPCAction, IAnimationSegmentAction<NPC>
			{
				// Token: 0x06004D7B RID: 19835 RVA: 0x006DC324 File Offset: 0x006DA524
				public ForceAltTexture(int altTexture)
				{
					this._altTexture = altTexture;
				}

				// Token: 0x06004D7C RID: 19836 RVA: 0x006DC333 File Offset: 0x006DA533
				public void BindTo(NPC obj)
				{
					obj.altTexture = this._altTexture;
				}

				// Token: 0x170005D1 RID: 1489
				// (get) Token: 0x06004D7D RID: 19837 RVA: 0x001DAC3B File Offset: 0x001D8E3B
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return 0;
					}
				}

				// Token: 0x06004D7E RID: 19838 RVA: 0x00009E46 File Offset: 0x00008046
				public void ApplyTo(NPC obj, float localTimeForObj)
				{
				}

				// Token: 0x06004D7F RID: 19839 RVA: 0x00009E46 File Offset: 0x00008046
				public void SetDelay(float delay)
				{
				}

				// Token: 0x040078F7 RID: 30967
				private int _altTexture;
			}

			// Token: 0x02000B07 RID: 2823
			public class Variant : Actions.NPCs.INPCAction, IAnimationSegmentAction<NPC>
			{
				// Token: 0x06004D80 RID: 19840 RVA: 0x006DC341 File Offset: 0x006DA541
				public Variant(int variant)
				{
					this._variant = variant;
				}

				// Token: 0x06004D81 RID: 19841 RVA: 0x006DC350 File Offset: 0x006DA550
				public void BindTo(NPC obj)
				{
					obj.townNpcVariationIndex = this._variant;
				}

				// Token: 0x170005D2 RID: 1490
				// (get) Token: 0x06004D82 RID: 19842 RVA: 0x001DAC3B File Offset: 0x001D8E3B
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return 0;
					}
				}

				// Token: 0x06004D83 RID: 19843 RVA: 0x00009E46 File Offset: 0x00008046
				public void ApplyTo(NPC obj, float localTimeForObj)
				{
				}

				// Token: 0x06004D84 RID: 19844 RVA: 0x00009E46 File Offset: 0x00008046
				public void SetDelay(float delay)
				{
				}

				// Token: 0x040078F8 RID: 30968
				private int _variant;
			}

			// Token: 0x02000B08 RID: 2824
			public class ZombieKnockOnDoor : Actions.NPCs.INPCAction, IAnimationSegmentAction<NPC>
			{
				// Token: 0x06004D85 RID: 19845 RVA: 0x006DC35E File Offset: 0x006DA55E
				public ZombieKnockOnDoor(int durationInFrames)
				{
					this._duration = durationInFrames;
				}

				// Token: 0x06004D86 RID: 19846 RVA: 0x00009E46 File Offset: 0x00008046
				public void BindTo(NPC obj)
				{
				}

				// Token: 0x170005D3 RID: 1491
				// (get) Token: 0x06004D87 RID: 19847 RVA: 0x006DC397 File Offset: 0x006DA597
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return this._duration;
					}
				}

				// Token: 0x06004D88 RID: 19848 RVA: 0x006DC39F File Offset: 0x006DA59F
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x06004D89 RID: 19849 RVA: 0x006DC3A8 File Offset: 0x006DA5A8
				public void ApplyTo(NPC obj, float localTimeForObj)
				{
					if (localTimeForObj < this._delay)
					{
						return;
					}
					float num = localTimeForObj - this._delay;
					if (num > (float)this._duration)
					{
						num = (float)this._duration;
					}
					if ((int)num % 60 / 4 <= 3)
					{
						obj.position += this.bumpOffset;
						obj.velocity = this.bumpVelocity;
						return;
					}
					obj.position -= this.bumpOffset;
					obj.velocity = Vector2.Zero;
				}

				// Token: 0x040078F9 RID: 30969
				private int _duration;

				// Token: 0x040078FA RID: 30970
				private float _delay;

				// Token: 0x040078FB RID: 30971
				private Vector2 bumpOffset = new Vector2(-1f, 0f);

				// Token: 0x040078FC RID: 30972
				private Vector2 bumpVelocity = new Vector2(0.75f, 0f);
			}
		}

		// Token: 0x020009AC RID: 2476
		public class Sprites
		{
			// Token: 0x060049D6 RID: 18902 RVA: 0x0000357B File Offset: 0x0000177B
			public Sprites()
			{
			}

			// Token: 0x02000B09 RID: 2825
			public interface ISpriteAction : IAnimationSegmentAction<Segments.LooseSprite>
			{
			}

			// Token: 0x02000B0A RID: 2826
			public class Fade : Actions.Sprites.ISpriteAction, IAnimationSegmentAction<Segments.LooseSprite>
			{
				// Token: 0x06004D8A RID: 19850 RVA: 0x006DC42A File Offset: 0x006DA62A
				public Fade(float opacityTarget)
				{
					this._duration = 0;
					this._opacityTarget = opacityTarget;
				}

				// Token: 0x06004D8B RID: 19851 RVA: 0x006DC440 File Offset: 0x006DA640
				public Fade(float opacityTarget, int duration)
				{
					this._duration = duration;
					this._opacityTarget = opacityTarget;
				}

				// Token: 0x06004D8C RID: 19852 RVA: 0x00009E46 File Offset: 0x00008046
				public void BindTo(Segments.LooseSprite obj)
				{
				}

				// Token: 0x170005D4 RID: 1492
				// (get) Token: 0x06004D8D RID: 19853 RVA: 0x006DC456 File Offset: 0x006DA656
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return this._duration;
					}
				}

				// Token: 0x06004D8E RID: 19854 RVA: 0x006DC45E File Offset: 0x006DA65E
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x06004D8F RID: 19855 RVA: 0x006DC468 File Offset: 0x006DA668
				public void ApplyTo(Segments.LooseSprite obj, float localTimeForObj)
				{
					if (localTimeForObj < this._delay)
					{
						return;
					}
					if (this._duration == 0)
					{
						obj.CurrentOpacity = this._opacityTarget;
						return;
					}
					float num = localTimeForObj - this._delay;
					if (num > (float)this._duration)
					{
						num = (float)this._duration;
					}
					obj.CurrentOpacity = MathHelper.Lerp(obj.CurrentOpacity, this._opacityTarget, Utils.GetLerpValue(0f, (float)this._duration, num, true));
				}

				// Token: 0x040078FD RID: 30973
				private int _duration;

				// Token: 0x040078FE RID: 30974
				private float _opacityTarget;

				// Token: 0x040078FF RID: 30975
				private float _delay;
			}

			// Token: 0x02000B0B RID: 2827
			public abstract class AScale : Actions.Sprites.ISpriteAction, IAnimationSegmentAction<Segments.LooseSprite>
			{
				// Token: 0x06004D90 RID: 19856 RVA: 0x006DC4D9 File Offset: 0x006DA6D9
				public AScale(Vector2 scaleTarget)
				{
					this.Duration = 0;
					this._scaleTarget = scaleTarget;
				}

				// Token: 0x06004D91 RID: 19857 RVA: 0x006DC4EF File Offset: 0x006DA6EF
				public AScale(Vector2 scaleTarget, int duration)
				{
					this.Duration = duration;
					this._scaleTarget = scaleTarget;
				}

				// Token: 0x06004D92 RID: 19858 RVA: 0x00009E46 File Offset: 0x00008046
				public void BindTo(Segments.LooseSprite obj)
				{
				}

				// Token: 0x170005D5 RID: 1493
				// (get) Token: 0x06004D93 RID: 19859 RVA: 0x006DC505 File Offset: 0x006DA705
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return this.Duration;
					}
				}

				// Token: 0x06004D94 RID: 19860 RVA: 0x006DC50D File Offset: 0x006DA70D
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x06004D95 RID: 19861 RVA: 0x006DC518 File Offset: 0x006DA718
				public void ApplyTo(Segments.LooseSprite obj, float localTimeForObj)
				{
					if (localTimeForObj < this._delay)
					{
						return;
					}
					if (this.Duration == 0)
					{
						obj.CurrentDrawData.scale = this._scaleTarget;
						return;
					}
					float num = localTimeForObj - this._delay;
					if (num > (float)this.Duration)
					{
						num = (float)this.Duration;
					}
					float progress = this.GetProgress(num);
					obj.CurrentDrawData.scale = Vector2.Lerp(obj.CurrentDrawData.scale, this._scaleTarget, progress);
				}

				// Token: 0x06004D96 RID: 19862
				protected abstract float GetProgress(float durationInFramesToApply);

				// Token: 0x04007900 RID: 30976
				protected int Duration;

				// Token: 0x04007901 RID: 30977
				private Vector2 _scaleTarget;

				// Token: 0x04007902 RID: 30978
				private float _delay;
			}

			// Token: 0x02000B0C RID: 2828
			public class LinearScale : Actions.Sprites.AScale
			{
				// Token: 0x06004D97 RID: 19863 RVA: 0x006DC58E File Offset: 0x006DA78E
				public LinearScale(Vector2 scaleTarget)
					: base(scaleTarget)
				{
				}

				// Token: 0x06004D98 RID: 19864 RVA: 0x006DC597 File Offset: 0x006DA797
				public LinearScale(Vector2 scaleTarget, int duration)
					: base(scaleTarget, duration)
				{
				}

				// Token: 0x06004D99 RID: 19865 RVA: 0x006DC5A1 File Offset: 0x006DA7A1
				protected override float GetProgress(float durationInFramesToApply)
				{
					return Utils.GetLerpValue(0f, (float)this.Duration, durationInFramesToApply, true);
				}
			}

			// Token: 0x02000B0D RID: 2829
			public class OutCircleScale : Actions.Sprites.AScale
			{
				// Token: 0x06004D9A RID: 19866 RVA: 0x006DC58E File Offset: 0x006DA78E
				public OutCircleScale(Vector2 scaleTarget)
					: base(scaleTarget)
				{
				}

				// Token: 0x06004D9B RID: 19867 RVA: 0x006DC597 File Offset: 0x006DA797
				public OutCircleScale(Vector2 scaleTarget, int duration)
					: base(scaleTarget, duration)
				{
				}

				// Token: 0x06004D9C RID: 19868 RVA: 0x006DC5B8 File Offset: 0x006DA7B8
				protected override float GetProgress(float durationInFramesToApply)
				{
					float num = Utils.GetLerpValue(0f, (float)this.Duration, durationInFramesToApply, true);
					num -= 1f;
					return (float)Math.Sqrt((double)(1f - num * num));
				}
			}

			// Token: 0x02000B0E RID: 2830
			public class Wait : Actions.Sprites.ISpriteAction, IAnimationSegmentAction<Segments.LooseSprite>
			{
				// Token: 0x06004D9D RID: 19869 RVA: 0x006DC5F1 File Offset: 0x006DA7F1
				public Wait(int durationInFrames)
				{
					this._duration = durationInFrames;
				}

				// Token: 0x06004D9E RID: 19870 RVA: 0x00009E46 File Offset: 0x00008046
				public void BindTo(Segments.LooseSprite obj)
				{
				}

				// Token: 0x170005D6 RID: 1494
				// (get) Token: 0x06004D9F RID: 19871 RVA: 0x006DC600 File Offset: 0x006DA800
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return this._duration;
					}
				}

				// Token: 0x06004DA0 RID: 19872 RVA: 0x006DC608 File Offset: 0x006DA808
				public void ApplyTo(Segments.LooseSprite obj, float localTimeForObj)
				{
					float delay = this._delay;
				}

				// Token: 0x06004DA1 RID: 19873 RVA: 0x006DC613 File Offset: 0x006DA813
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x04007903 RID: 30979
				private int _duration;

				// Token: 0x04007904 RID: 30980
				private float _delay;
			}

			// Token: 0x02000B0F RID: 2831
			public class SimulateGravity : Actions.Sprites.ISpriteAction, IAnimationSegmentAction<Segments.LooseSprite>
			{
				// Token: 0x06004DA2 RID: 19874 RVA: 0x006DC61C File Offset: 0x006DA81C
				public SimulateGravity(Vector2 initialVelocity, Vector2 gravityPerFrame, float rotationPerFrame, int duration)
				{
					this._duration = duration;
					this._initialVelocity = initialVelocity;
					this._gravityPerFrame = gravityPerFrame;
					this._rotationPerFrame = rotationPerFrame;
				}

				// Token: 0x06004DA3 RID: 19875 RVA: 0x00009E46 File Offset: 0x00008046
				public void BindTo(Segments.LooseSprite obj)
				{
				}

				// Token: 0x170005D7 RID: 1495
				// (get) Token: 0x06004DA4 RID: 19876 RVA: 0x006DC641 File Offset: 0x006DA841
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return this._duration;
					}
				}

				// Token: 0x06004DA5 RID: 19877 RVA: 0x006DC649 File Offset: 0x006DA849
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x06004DA6 RID: 19878 RVA: 0x006DC654 File Offset: 0x006DA854
				public void ApplyTo(Segments.LooseSprite obj, float localTimeForObj)
				{
					if (localTimeForObj < this._delay)
					{
						return;
					}
					float num = localTimeForObj - this._delay;
					if (num > (float)this._duration)
					{
						num = (float)this._duration;
					}
					Vector2 vector = this._initialVelocity * num + this._gravityPerFrame * (num * num);
					obj.CurrentDrawData.position = obj.CurrentDrawData.position + vector;
					obj.CurrentDrawData.rotation = obj.CurrentDrawData.rotation + this._rotationPerFrame * num;
				}

				// Token: 0x04007905 RID: 30981
				private int _duration;

				// Token: 0x04007906 RID: 30982
				private float _delay;

				// Token: 0x04007907 RID: 30983
				private Vector2 _initialVelocity;

				// Token: 0x04007908 RID: 30984
				private Vector2 _gravityPerFrame;

				// Token: 0x04007909 RID: 30985
				private float _rotationPerFrame;
			}

			// Token: 0x02000B10 RID: 2832
			public class SetFrame : Actions.Sprites.ISpriteAction, IAnimationSegmentAction<Segments.LooseSprite>
			{
				// Token: 0x06004DA7 RID: 19879 RVA: 0x006DC6D9 File Offset: 0x006DA8D9
				public SetFrame(int frameX, int frameY, int paddingX = 2, int paddingY = 2)
				{
					this._targetFrameX = frameX;
					this._targetFrameY = frameY;
					this._paddingX = paddingX;
					this._paddingY = paddingY;
				}

				// Token: 0x06004DA8 RID: 19880 RVA: 0x00009E46 File Offset: 0x00008046
				public void BindTo(Segments.LooseSprite obj)
				{
				}

				// Token: 0x170005D8 RID: 1496
				// (get) Token: 0x06004DA9 RID: 19881 RVA: 0x001DAC3B File Offset: 0x001D8E3B
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return 0;
					}
				}

				// Token: 0x06004DAA RID: 19882 RVA: 0x006DC6FE File Offset: 0x006DA8FE
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x06004DAB RID: 19883 RVA: 0x006DC708 File Offset: 0x006DA908
				public void ApplyTo(Segments.LooseSprite obj, float localTimeForObj)
				{
					if (localTimeForObj < this._delay)
					{
						return;
					}
					Rectangle value = obj.CurrentDrawData.sourceRect.Value;
					value.X = (value.Width + this._paddingX) * this._targetFrameX;
					value.Y = (value.Height + this._paddingY) * this._targetFrameY;
					obj.CurrentDrawData.sourceRect = new Rectangle?(value);
				}

				// Token: 0x0400790A RID: 30986
				private int _targetFrameX;

				// Token: 0x0400790B RID: 30987
				private int _targetFrameY;

				// Token: 0x0400790C RID: 30988
				private int _paddingX;

				// Token: 0x0400790D RID: 30989
				private int _paddingY;

				// Token: 0x0400790E RID: 30990
				private float _delay;
			}

			// Token: 0x02000B11 RID: 2833
			public class SetFrameSequence : Actions.Sprites.ISpriteAction, IAnimationSegmentAction<Segments.LooseSprite>
			{
				// Token: 0x06004DAC RID: 19884 RVA: 0x006DC777 File Offset: 0x006DA977
				public SetFrameSequence(int duration, Point[] frameIndices, int timePerFrame, int paddingX = 2, int paddingY = 2)
					: this(frameIndices, timePerFrame, paddingX, paddingY)
				{
					this._duration = duration;
					this._loop = true;
				}

				// Token: 0x06004DAD RID: 19885 RVA: 0x006DC793 File Offset: 0x006DA993
				public SetFrameSequence(Point[] frameIndices, int timePerFrame, int paddingX = 2, int paddingY = 2)
				{
					this._frameIndices = frameIndices;
					this._timePerFrame = timePerFrame;
					this._paddingX = paddingX;
					this._paddingY = paddingY;
					this._duration = this._timePerFrame * this._frameIndices.Length;
				}

				// Token: 0x06004DAE RID: 19886 RVA: 0x00009E46 File Offset: 0x00008046
				public void BindTo(Segments.LooseSprite obj)
				{
				}

				// Token: 0x170005D9 RID: 1497
				// (get) Token: 0x06004DAF RID: 19887 RVA: 0x006DC7CD File Offset: 0x006DA9CD
				public int ExpectedLengthOfActionInFrames
				{
					get
					{
						return this._duration;
					}
				}

				// Token: 0x06004DB0 RID: 19888 RVA: 0x006DC7D5 File Offset: 0x006DA9D5
				public void SetDelay(float delay)
				{
					this._delay = delay;
				}

				// Token: 0x06004DB1 RID: 19889 RVA: 0x006DC7E0 File Offset: 0x006DA9E0
				public void ApplyTo(Segments.LooseSprite obj, float localTimeForObj)
				{
					if (localTimeForObj < this._delay)
					{
						return;
					}
					Rectangle value = obj.CurrentDrawData.sourceRect.Value;
					int num2;
					if (this._loop)
					{
						int num = this._frameIndices.Length;
						num2 = (int)(localTimeForObj % (float)(this._timePerFrame * num)) / this._timePerFrame;
						if (num2 >= num)
						{
							num2 = num - 1;
						}
					}
					else
					{
						float num3 = localTimeForObj - this._delay;
						if (num3 > (float)this._duration)
						{
							num3 = (float)this._duration;
						}
						num2 = (int)(num3 / (float)this._timePerFrame);
						if (num2 >= this._frameIndices.Length)
						{
							num2 = this._frameIndices.Length - 1;
						}
					}
					Point point = this._frameIndices[num2];
					value.X = (value.Width + this._paddingX) * point.X;
					value.Y = (value.Height + this._paddingY) * point.Y;
					obj.CurrentDrawData.sourceRect = new Rectangle?(value);
				}

				// Token: 0x0400790F RID: 30991
				private Point[] _frameIndices;

				// Token: 0x04007910 RID: 30992
				private int _timePerFrame;

				// Token: 0x04007911 RID: 30993
				private int _paddingX;

				// Token: 0x04007912 RID: 30994
				private int _paddingY;

				// Token: 0x04007913 RID: 30995
				private float _delay;

				// Token: 0x04007914 RID: 30996
				private int _duration;

				// Token: 0x04007915 RID: 30997
				private bool _loop;
			}
		}
	}
}
