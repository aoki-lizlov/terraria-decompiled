using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace Terraria.DataStructures
{
	// Token: 0x0200059F RID: 1439
	public struct PlayerDrawHeadSet
	{
		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x060038E8 RID: 14568 RVA: 0x0064E284 File Offset: 0x0064C484
		public Rectangle HairFrame
		{
			get
			{
				Rectangle rectangle = this.bodyFrameMemory;
				rectangle.Height--;
				return rectangle;
			}
		}

		// Token: 0x060038E9 RID: 14569 RVA: 0x0064E2A8 File Offset: 0x0064C4A8
		public void BoringSetup(Player drawPlayer2, List<DrawData> drawData, List<int> dust, List<int> gore, float X, float Y, float Alpha, float Scale)
		{
			this.DrawData = drawData;
			this.Dust = dust;
			this.Gore = gore;
			this.drawPlayer = drawPlayer2;
			this.Position = this.drawPlayer.position;
			this.cHead = 0;
			this.cFace = 0;
			this.cUnicornHorn = 0;
			this.cAngelHalo = 0;
			this.cBeard = 0;
			this.drawUnicornHorn = false;
			this.drawAngelHalo = false;
			this.skinVar = this.drawPlayer.skinVariant;
			this.hairShaderPacked = PlayerDrawHelper.PackShader((int)this.drawPlayer.hairDye, PlayerDrawHelper.ShaderConfiguration.HairShader);
			if (this.drawPlayer.head == 0 && this.drawPlayer.hairDye == 0)
			{
				this.hairShaderPacked = PlayerDrawHelper.PackShader(1, PlayerDrawHelper.ShaderConfiguration.HairShader);
			}
			this.skinDyePacked = this.drawPlayer.skinDyePacked;
			if (this.drawPlayer.face > 0 && this.drawPlayer.face < ArmorIDs.Face.Count)
			{
				Main.instance.LoadAccFace((int)this.drawPlayer.face);
			}
			this.cHead = this.drawPlayer.cHead;
			this.cFace = this.drawPlayer.cFace;
			this.cFaceHead = this.drawPlayer.cFaceHead;
			this.cFaceFlower = this.drawPlayer.cFaceFlower;
			this.cFaceMask = this.drawPlayer.cFaceMask;
			this.cUnicornHorn = this.drawPlayer.cUnicornHorn;
			this.cAngelHalo = this.drawPlayer.cAngelHalo;
			this.cBeard = this.drawPlayer.cBeard;
			this.drawUnicornHorn = this.drawPlayer.hasUnicornHorn;
			this.drawAngelHalo = this.drawPlayer.hasAngelHalo;
			Main.instance.LoadHair(this.drawPlayer.hair);
			this.scale = Scale;
			this.colorEyeWhites = Main.quickAlpha(Color.White, Alpha);
			this.colorEyes = Main.quickAlpha(this.drawPlayer.eyeColor, Alpha);
			this.colorHair = Main.quickAlpha(this.drawPlayer.GetHairColor(false), Alpha);
			this.colorHead = Main.quickAlpha(this.drawPlayer.skinColor, Alpha);
			this.colorArmorHead = Main.quickAlpha(Color.White, Alpha);
			if (this.drawPlayer.isDisplayDollOrInanimate)
			{
				this.colorDisplayDollSkin = Main.quickAlpha(PlayerDrawHelper.DISPLAY_DOLL_DEFAULT_SKIN_COLOR, Alpha);
			}
			else
			{
				this.colorDisplayDollSkin = this.colorHead;
			}
			this.playerEffect = SpriteEffects.None;
			if (this.drawPlayer.direction < 0)
			{
				this.playerEffect = SpriteEffects.FlipHorizontally;
			}
			this.headVect = new Vector2((float)this.drawPlayer.legFrame.Width * 0.5f, (float)this.drawPlayer.legFrame.Height * 0.4f);
			this.bodyFrameMemory = this.drawPlayer.bodyFrame;
			this.bodyFrameMemory.Y = 0;
			this.Position = Main.screenPosition;
			this.Position.X = this.Position.X + X;
			this.Position.Y = this.Position.Y + Y;
			this.Position.X = this.Position.X - 6f;
			this.Position.Y = this.Position.Y - 4f;
			this.Position.Y = this.Position.Y - (float)this.drawPlayer.HeightMapOffset;
			if (this.drawPlayer.head > 0 && this.drawPlayer.head < ArmorIDs.Head.Count)
			{
				Main.instance.LoadArmorHead(this.drawPlayer.head);
				int num = ArmorIDs.Head.Sets.FrontToBackID[this.drawPlayer.head];
				if (num >= 0)
				{
					Main.instance.LoadArmorHead(num);
				}
			}
			if (this.drawPlayer.face > 0 && this.drawPlayer.face < ArmorIDs.Face.Count)
			{
				Main.instance.LoadAccFace((int)this.drawPlayer.face);
			}
			if (this.drawPlayer.faceHead > 0 && this.drawPlayer.faceHead < ArmorIDs.Face.Count)
			{
				Main.instance.LoadAccFace((int)this.drawPlayer.faceHead);
			}
			if (this.drawPlayer.faceFlower > 0 && this.drawPlayer.faceFlower < ArmorIDs.Face.Count)
			{
				Main.instance.LoadAccFace((int)this.drawPlayer.faceFlower);
			}
			if (this.drawPlayer.faceMask > 0 && this.drawPlayer.faceMask < ArmorIDs.Face.Count)
			{
				Main.instance.LoadAccFace((int)this.drawPlayer.faceMask);
			}
			if (this.drawPlayer.beard > 0 && this.drawPlayer.beard < ArmorIDs.Beard.Count)
			{
				Main.instance.LoadAccBeard((int)this.drawPlayer.beard);
			}
			bool flag;
			this.drawPlayer.GetHairSettings(out this.fullHair, out this.hatHair, out this.hideHair, out flag, out this.helmetIsOverFullHair);
			this.hairOffset = this.drawPlayer.GetHairDrawOffset(this.drawPlayer.hair, this.hatHair);
			this.hairOffset.Y = this.hairOffset.Y * this.drawPlayer.Directions.Y;
			this.helmetOffset = this.drawPlayer.GetHelmetDrawOffset(true);
			this.helmetOffset.Y = this.helmetOffset.Y * this.drawPlayer.Directions.Y;
			this.helmetIsTall = this.drawPlayer.head == 14 || this.drawPlayer.head == 56 || this.drawPlayer.head == 158;
			this.helmetIsNormal = !this.helmetIsTall && !this.helmetIsOverFullHair && this.drawPlayer.head > 0 && this.drawPlayer.head < ArmorIDs.Head.Count && this.drawPlayer.head != 28;
		}

		// Token: 0x04005D2C RID: 23852
		public List<DrawData> DrawData;

		// Token: 0x04005D2D RID: 23853
		public List<int> Dust;

		// Token: 0x04005D2E RID: 23854
		public List<int> Gore;

		// Token: 0x04005D2F RID: 23855
		public Player drawPlayer;

		// Token: 0x04005D30 RID: 23856
		public int cHead;

		// Token: 0x04005D31 RID: 23857
		public int cFace;

		// Token: 0x04005D32 RID: 23858
		public int cFaceHead;

		// Token: 0x04005D33 RID: 23859
		public int cFaceFlower;

		// Token: 0x04005D34 RID: 23860
		public int cFaceMask;

		// Token: 0x04005D35 RID: 23861
		public int cUnicornHorn;

		// Token: 0x04005D36 RID: 23862
		public int cAngelHalo;

		// Token: 0x04005D37 RID: 23863
		public int cBeard;

		// Token: 0x04005D38 RID: 23864
		public int skinVar;

		// Token: 0x04005D39 RID: 23865
		public int hairShaderPacked;

		// Token: 0x04005D3A RID: 23866
		public int skinDyePacked;

		// Token: 0x04005D3B RID: 23867
		public float scale;

		// Token: 0x04005D3C RID: 23868
		public Color colorEyeWhites;

		// Token: 0x04005D3D RID: 23869
		public Color colorEyes;

		// Token: 0x04005D3E RID: 23870
		public Color colorHair;

		// Token: 0x04005D3F RID: 23871
		public Color colorHead;

		// Token: 0x04005D40 RID: 23872
		public Color colorArmorHead;

		// Token: 0x04005D41 RID: 23873
		public Color colorDisplayDollSkin;

		// Token: 0x04005D42 RID: 23874
		public SpriteEffects playerEffect;

		// Token: 0x04005D43 RID: 23875
		public Vector2 headVect;

		// Token: 0x04005D44 RID: 23876
		public Rectangle bodyFrameMemory;

		// Token: 0x04005D45 RID: 23877
		public bool fullHair;

		// Token: 0x04005D46 RID: 23878
		public bool hatHair;

		// Token: 0x04005D47 RID: 23879
		public bool hideHair;

		// Token: 0x04005D48 RID: 23880
		public bool helmetIsTall;

		// Token: 0x04005D49 RID: 23881
		public bool helmetIsOverFullHair;

		// Token: 0x04005D4A RID: 23882
		public bool helmetIsNormal;

		// Token: 0x04005D4B RID: 23883
		public bool drawUnicornHorn;

		// Token: 0x04005D4C RID: 23884
		public bool drawAngelHalo;

		// Token: 0x04005D4D RID: 23885
		public Vector2 Position;

		// Token: 0x04005D4E RID: 23886
		public Vector2 hairOffset;

		// Token: 0x04005D4F RID: 23887
		public Vector2 helmetOffset;
	}
}
