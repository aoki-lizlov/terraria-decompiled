using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Animations;
using Terraria.GameContent.Skies.CreditsRoll;
using Terraria.Graphics.Effects;

namespace Terraria.GameContent.Skies
{
	// Token: 0x0200044A RID: 1098
	public class CreditsRollSky : CustomSky
	{
		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x060031E5 RID: 12773 RVA: 0x005E485D File Offset: 0x005E2A5D
		public int AmountOfTimeNeededForFullPlay
		{
			get
			{
				return this._endTime;
			}
		}

		// Token: 0x060031E6 RID: 12774 RVA: 0x005E4865 File Offset: 0x005E2A65
		public CreditsRollSky()
		{
			this.EnsureSegmentsAreMade();
		}

		// Token: 0x060031E7 RID: 12775 RVA: 0x005E4894 File Offset: 0x005E2A94
		public override void Update(GameTime gameTime)
		{
			if (FocusHelper.PauseSkies)
			{
				return;
			}
			this._currentTime++;
			float num = 0.008333334f;
			if (Main.gameMenu)
			{
				num = 0.06666667f;
			}
			this._opacity = MathHelper.Clamp(this._opacity + num * (float)this._wantsToBeSeen.ToDirectionInt(), 0f, 1f);
			if (this._opacity == 0f && !this._wantsToBeSeen)
			{
				this._isActive = false;
				return;
			}
			bool flag = true;
			if (!Main.CanPlayCreditsRoll())
			{
				flag = false;
			}
			if (this._currentTime >= this._endTime)
			{
				flag = false;
			}
			if (!flag)
			{
				SkyManager.Instance.Deactivate("CreditsRoll", new object[0]);
			}
		}

		// Token: 0x060031E8 RID: 12776 RVA: 0x005E494C File Offset: 0x005E2B4C
		public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
		{
			float num = 1f;
			if (num < minDepth || num > maxDepth)
			{
				return;
			}
			Vector2 vector = Main.ScreenSize.ToVector2() / 2f;
			if (Main.gameMenu)
			{
				vector.Y = 300f * Main.UIScaleMatrix.M22 / Main.BackgroundViewMatrix.Zoom.Y;
			}
			GameAnimationSegment gameAnimationSegment = new GameAnimationSegment
			{
				SpriteBatch = spriteBatch,
				AnchorPositionOnScreen = vector,
				TimeInAnimation = this._currentTime,
				DisplayOpacity = this._opacity
			};
			List<IAnimationSegment> list = this._segmentsInGame;
			if (Main.gameMenu)
			{
				list = this._segmentsInMainMenu;
			}
			for (int i = 0; i < list.Count; i++)
			{
				list[i].Draw(ref gameAnimationSegment);
			}
		}

		// Token: 0x060031E9 RID: 12777 RVA: 0x005E4A19 File Offset: 0x005E2C19
		public override bool IsActive()
		{
			return this._isActive;
		}

		// Token: 0x060031EA RID: 12778 RVA: 0x005E4A21 File Offset: 0x005E2C21
		public override void Reset()
		{
			this._currentTime = 0;
			this.EnsureSegmentsAreMade();
			this._isActive = false;
			this._wantsToBeSeen = false;
		}

		// Token: 0x060031EB RID: 12779 RVA: 0x005E4A3E File Offset: 0x005E2C3E
		public override void Activate(Vector2 position, params object[] args)
		{
			this._isActive = true;
			this._wantsToBeSeen = true;
			if (this._opacity == 0f)
			{
				this.EnsureSegmentsAreMade();
				this._currentTime = 0;
			}
		}

		// Token: 0x060031EC RID: 12780 RVA: 0x005E4A68 File Offset: 0x005E2C68
		private void EnsureSegmentsAreMade()
		{
			if (this._segmentsInMainMenu.Count > 0 && this._segmentsInGame.Count > 0)
			{
				return;
			}
			this._segmentsInGame.Clear();
			this._composer.FillSegments(this._segmentsInGame, out this._endTime, true);
			this._segmentsInMainMenu.Clear();
			this._composer.FillSegments(this._segmentsInMainMenu, out this._endTime, false);
		}

		// Token: 0x060031ED RID: 12781 RVA: 0x005E4AD8 File Offset: 0x005E2CD8
		public override void Deactivate(params object[] args)
		{
			this._wantsToBeSeen = false;
		}

		// Token: 0x040057CA RID: 22474
		private int _endTime;

		// Token: 0x040057CB RID: 22475
		private int _currentTime;

		// Token: 0x040057CC RID: 22476
		private CreditsRollComposer _composer = new CreditsRollComposer();

		// Token: 0x040057CD RID: 22477
		private List<IAnimationSegment> _segmentsInGame = new List<IAnimationSegment>();

		// Token: 0x040057CE RID: 22478
		private List<IAnimationSegment> _segmentsInMainMenu = new List<IAnimationSegment>();

		// Token: 0x040057CF RID: 22479
		private bool _isActive;

		// Token: 0x040057D0 RID: 22480
		private bool _wantsToBeSeen;

		// Token: 0x040057D1 RID: 22481
		private float _opacity;
	}
}
