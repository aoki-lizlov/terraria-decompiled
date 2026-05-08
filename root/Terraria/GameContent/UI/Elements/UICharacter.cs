using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ID;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003FC RID: 1020
	public class UICharacter : UIElement
	{
		// Token: 0x06002EE0 RID: 12000 RVA: 0x005AF5CC File Offset: 0x005AD7CC
		public UICharacter(Player player, bool animated = false, bool hasBackPanel = true, float characterScale = 1f, bool useAClone = false)
		{
			this._player = player;
			if (useAClone)
			{
				this._player = player.SerializedClone();
				this._player.dead = false;
				this._player.PlayerFrame();
			}
			this.Width.Set(59f, 0f);
			this.Height.Set(58f, 0f);
			this._texture = Main.Assets.Request<Texture2D>("Images/UI/PlayerBackground", 1);
			this.UseImmediateMode = true;
			this._animated = animated;
			this._drawsBackPanel = hasBackPanel;
			this._characterScale = characterScale;
			this.OverrideSamplerState = SamplerState.PointClamp;
			this._petProjectiles = UICharacter.NoPets;
			this.PreparePetProjectiles();
		}

		// Token: 0x06002EE1 RID: 12001 RVA: 0x005AF694 File Offset: 0x005AD894
		private void PreparePetProjectiles()
		{
			if (this._player.hideMisc[0])
			{
				return;
			}
			Item item = this._player.miscEquips[0];
			if (item.IsAir)
			{
				return;
			}
			int shoot = item.shoot;
			this._petProjectiles = new Projectile[] { this.PreparePetProjectiles_CreatePetProjectileDummy(shoot) };
		}

		// Token: 0x06002EE2 RID: 12002 RVA: 0x005AF6E9 File Offset: 0x005AD8E9
		private Projectile PreparePetProjectiles_CreatePetProjectileDummy(int projectileId)
		{
			Projectile projectile = new Projectile();
			projectile.SetDefaults(projectileId);
			projectile.isAPreviewDummy = true;
			return projectile;
		}

		// Token: 0x06002EE3 RID: 12003 RVA: 0x005AF6FE File Offset: 0x005AD8FE
		public override void Update(GameTime gameTime)
		{
			if (this._animated)
			{
				this._animationCounter++;
			}
			base.Update(gameTime);
		}

		// Token: 0x06002EE4 RID: 12004 RVA: 0x005AF720 File Offset: 0x005AD920
		private void UpdateAnim()
		{
			if (!this._animated)
			{
				this._player.bodyFrame.Y = (this._player.legFrame.Y = (this._player.headFrame.Y = 0));
				return;
			}
			int num = (int)(Main.GlobalTimeWrappedHourly / 0.07f) % 14 + 6;
			this._player.bodyFrame.Y = (this._player.legFrame.Y = (this._player.headFrame.Y = num * 56));
			this._player.WingFrame(false);
		}

		// Token: 0x06002EE5 RID: 12005 RVA: 0x005AF7C8 File Offset: 0x005AD9C8
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			if (this._drawsBackPanel)
			{
				spriteBatch.Draw(this._texture.Value, dimensions.Position(), Color.White);
			}
			this._player.ResetEffects();
			this._player.ResetVisibleAccessories();
			this._player.UpdateMiscCounter();
			this._player.UpdateDyes();
			if (this.PrepareAction != null)
			{
				this.PrepareAction();
			}
			this._player.PlayerFrame();
			this.UpdateAnim();
			this.DrawPets(spriteBatch);
			Vector2 playerPosition = this.GetPlayerPosition(ref dimensions);
			Item item = this._player.inventory[this._player.selectedItem];
			this._player.inventory[this._player.selectedItem] = UICharacter._blankItem;
			Main.PlayerRenderer.DrawPlayer(Main.Camera, this._player, playerPosition + Main.screenPosition, 0f, Vector2.Zero, 0f, this._characterScale);
			this._player.inventory[this._player.selectedItem] = item;
		}

		// Token: 0x06002EE6 RID: 12006 RVA: 0x005AF8E4 File Offset: 0x005ADAE4
		private Vector2 GetPlayerPosition(ref CalculatedStyle dimensions)
		{
			Vector2 vector = dimensions.Position() + new Vector2(dimensions.Width * 0.5f - (float)(this._player.width >> 1), dimensions.Height * 0.5f - (float)(this._player.height >> 1));
			if (this._petProjectiles.Length != 0)
			{
				vector.X -= 10f;
			}
			return vector;
		}

		// Token: 0x06002EE7 RID: 12007 RVA: 0x005AF954 File Offset: 0x005ADB54
		public void DrawPets(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			Vector2 playerPosition = this.GetPlayerPosition(ref dimensions);
			for (int i = 0; i < this._petProjectiles.Length; i++)
			{
				Projectile projectile = this._petProjectiles[i];
				Vector2 vector = playerPosition + new Vector2(0f, (float)this._player.height) + new Vector2(20f, 0f) + new Vector2(0f, (float)(-(float)projectile.height));
				projectile.position = vector + Main.screenPosition;
				projectile.velocity = new Vector2(0.1f, 0f);
				projectile.direction = 1;
				projectile.owner = Main.myPlayer;
				ProjectileID.Sets.CharacterPreviewAnimations[projectile.type].ApplyTo(projectile, this._animated);
				Player player = Main.player[Main.myPlayer];
				Main.player[Main.myPlayer] = this._player;
				Main.instance.DrawProjDirect(projectile, null);
				Main.player[Main.myPlayer] = player;
			}
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Immediate, spriteBatch.GraphicsDevice.BlendState, spriteBatch.GraphicsDevice.SamplerStates[0], spriteBatch.GraphicsDevice.DepthStencilState, spriteBatch.GraphicsDevice.RasterizerState, null, Main.UIScaleMatrix);
		}

		// Token: 0x06002EE8 RID: 12008 RVA: 0x005AFAA9 File Offset: 0x005ADCA9
		public void SetAnimated(bool animated)
		{
			this._animated = animated;
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06002EE9 RID: 12009 RVA: 0x005AFAB2 File Offset: 0x005ADCB2
		public bool IsAnimated
		{
			get
			{
				return this._animated;
			}
		}

		// Token: 0x06002EEA RID: 12010 RVA: 0x005AFABA File Offset: 0x005ADCBA
		// Note: this type is marked as 'beforefieldinit'.
		static UICharacter()
		{
		}

		// Token: 0x04005609 RID: 22025
		private Player _player;

		// Token: 0x0400560A RID: 22026
		private Projectile[] _petProjectiles;

		// Token: 0x0400560B RID: 22027
		private Asset<Texture2D> _texture;

		// Token: 0x0400560C RID: 22028
		private static Item _blankItem = new Item();

		// Token: 0x0400560D RID: 22029
		private bool _animated;

		// Token: 0x0400560E RID: 22030
		private bool _drawsBackPanel;

		// Token: 0x0400560F RID: 22031
		private float _characterScale = 1f;

		// Token: 0x04005610 RID: 22032
		private int _animationCounter;

		// Token: 0x04005611 RID: 22033
		public Action PrepareAction;

		// Token: 0x04005612 RID: 22034
		private static readonly Projectile[] NoPets = new Projectile[0];
	}
}
