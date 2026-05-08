using System;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace Terraria
{
	// Token: 0x0200005C RID: 92
	public class Animation
	{
		// Token: 0x060013BE RID: 5054 RVA: 0x004AD700 File Offset: 0x004AB900
		public static void Initialize()
		{
			Animation._animations = new List<Animation>();
			Animation._temporaryAnimations = new Dictionary<Point16, Animation>();
			Animation._awaitingRemoval = new List<Point16>();
			Animation._awaitingAddition = new List<Animation>();
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x004AD72C File Offset: 0x004AB92C
		private void SetDefaults(int type)
		{
			this._tileType = 0;
			this._frame = 0;
			this._frameMax = 0;
			this._frameCounter = 0;
			this._frameCounterMax = 0;
			this._temporary = false;
			switch (type)
			{
			case 0:
			{
				this._frameMax = 5;
				this._frameCounterMax = 12;
				this._frameData = new int[this._frameMax];
				for (int i = 0; i < this._frameMax; i++)
				{
					this._frameData[i] = i + 1;
				}
				return;
			}
			case 1:
			{
				this._frameMax = 5;
				this._frameCounterMax = 12;
				this._frameData = new int[this._frameMax];
				for (int j = 0; j < this._frameMax; j++)
				{
					this._frameData[j] = 5 - j;
				}
				return;
			}
			case 2:
				this._frameCounterMax = 6;
				this._frameData = new int[] { 1, 2, 2, 2, 1 };
				this._frameMax = this._frameData.Length;
				return;
			case 3:
			{
				this._frameMax = 5;
				this._frameCounterMax = 5;
				this._frameData = new int[this._frameMax];
				for (int k = 0; k < this._frameMax; k++)
				{
					this._frameData[k] = k;
				}
				return;
			}
			case 4:
			{
				this._frameMax = 3;
				this._frameCounterMax = 5;
				this._frameData = new int[this._frameMax];
				for (int l = 0; l < this._frameMax; l++)
				{
					this._frameData[l] = 9 + l;
				}
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x004AD89C File Offset: 0x004ABA9C
		public static void NewTemporaryAnimation(int type, ushort tileType, int x, int y)
		{
			Point16 point = new Point16(x, y);
			if (x < 0 || x >= Main.maxTilesX || y < 0 || y >= Main.maxTilesY)
			{
				return;
			}
			Animation animation = new Animation();
			animation.SetDefaults(type);
			animation._tileType = tileType;
			animation._coordinates = point;
			animation._temporary = true;
			Animation._awaitingAddition.Add(animation);
			if (Main.netMode == 2)
			{
				NetMessage.SendTemporaryAnimation(-1, type, (int)tileType, x, y);
			}
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x004AD90C File Offset: 0x004ABB0C
		private static void RemoveTemporaryAnimation(short x, short y)
		{
			Point16 point = new Point16(x, y);
			if (Animation._temporaryAnimations.ContainsKey(point))
			{
				Animation._awaitingRemoval.Add(point);
			}
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x004AD93C File Offset: 0x004ABB3C
		public static void UpdateAll()
		{
			for (int i = 0; i < Animation._animations.Count; i++)
			{
				Animation._animations[i].Update();
			}
			if (Animation._awaitingAddition.Count > 0)
			{
				for (int j = 0; j < Animation._awaitingAddition.Count; j++)
				{
					Animation animation = Animation._awaitingAddition[j];
					Animation._temporaryAnimations[animation._coordinates] = animation;
				}
				Animation._awaitingAddition.Clear();
			}
			foreach (KeyValuePair<Point16, Animation> keyValuePair in Animation._temporaryAnimations)
			{
				keyValuePair.Value.Update();
			}
			if (Animation._awaitingRemoval.Count > 0)
			{
				for (int k = 0; k < Animation._awaitingRemoval.Count; k++)
				{
					Animation._temporaryAnimations.Remove(Animation._awaitingRemoval[k]);
				}
				Animation._awaitingRemoval.Clear();
			}
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x004ADA4C File Offset: 0x004ABC4C
		public void Update()
		{
			if (this._temporary)
			{
				Tile tile = Main.tile[(int)this._coordinates.X, (int)this._coordinates.Y];
				if (tile != null && tile.type != this._tileType)
				{
					Animation.RemoveTemporaryAnimation(this._coordinates.X, this._coordinates.Y);
					return;
				}
			}
			this._frameCounter++;
			if (this._frameCounter >= this._frameCounterMax)
			{
				this._frameCounter = 0;
				this._frame++;
				if (this._frame >= this._frameMax)
				{
					this._frame = 0;
					if (this._temporary)
					{
						Animation.RemoveTemporaryAnimation(this._coordinates.X, this._coordinates.Y);
					}
				}
			}
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x004ADB18 File Offset: 0x004ABD18
		public static bool GetTemporaryFrame(int x, int y, out int frameData)
		{
			Point16 point = new Point16(x, y);
			Animation animation;
			if (!Animation._temporaryAnimations.TryGetValue(point, out animation))
			{
				frameData = 0;
				return false;
			}
			frameData = animation._frameData[animation._frame];
			return true;
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x0000357B File Offset: 0x0000177B
		public Animation()
		{
		}

		// Token: 0x04001002 RID: 4098
		private static List<Animation> _animations;

		// Token: 0x04001003 RID: 4099
		private static Dictionary<Point16, Animation> _temporaryAnimations;

		// Token: 0x04001004 RID: 4100
		private static List<Point16> _awaitingRemoval;

		// Token: 0x04001005 RID: 4101
		private static List<Animation> _awaitingAddition;

		// Token: 0x04001006 RID: 4102
		private bool _temporary;

		// Token: 0x04001007 RID: 4103
		private Point16 _coordinates;

		// Token: 0x04001008 RID: 4104
		private ushort _tileType;

		// Token: 0x04001009 RID: 4105
		private int _frame;

		// Token: 0x0400100A RID: 4106
		private int _frameMax;

		// Token: 0x0400100B RID: 4107
		private int _frameCounter;

		// Token: 0x0400100C RID: 4108
		private int _frameCounterMax;

		// Token: 0x0400100D RID: 4109
		private int[] _frameData;
	}
}
