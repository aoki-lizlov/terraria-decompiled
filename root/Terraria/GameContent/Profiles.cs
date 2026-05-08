using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Localization;

namespace Terraria.GameContent
{
	// Token: 0x0200026D RID: 621
	public class Profiles
	{
		// Token: 0x0600240B RID: 9227 RVA: 0x0000357B File Offset: 0x0000177B
		public Profiles()
		{
		}

		// Token: 0x020007ED RID: 2029
		public class StackedNPCProfile : ITownNPCProfile
		{
			// Token: 0x0600427B RID: 17019 RVA: 0x006BED80 File Offset: 0x006BCF80
			public StackedNPCProfile(params ITownNPCProfile[] profilesInOrderOfVariants)
			{
				this._profiles = profilesInOrderOfVariants;
			}

			// Token: 0x0600427C RID: 17020 RVA: 0x001DAC3B File Offset: 0x001D8E3B
			public int RollVariation()
			{
				return 0;
			}

			// Token: 0x0600427D RID: 17021 RVA: 0x006BED90 File Offset: 0x006BCF90
			public string GetNameForVariant(NPC npc)
			{
				int num = 0;
				if (this._profiles.IndexInRange(npc.townNpcVariationIndex))
				{
					num = npc.townNpcVariationIndex;
				}
				return this._profiles[num].GetNameForVariant(npc);
			}

			// Token: 0x0600427E RID: 17022 RVA: 0x006BEDC8 File Offset: 0x006BCFC8
			public Asset<Texture2D> GetTextureNPCShouldUse(NPC npc)
			{
				int num = 0;
				if (this._profiles.IndexInRange(npc.townNpcVariationIndex))
				{
					num = npc.townNpcVariationIndex;
				}
				return this._profiles[num].GetTextureNPCShouldUse(npc);
			}

			// Token: 0x0600427F RID: 17023 RVA: 0x006BEE00 File Offset: 0x006BD000
			public int GetHeadTextureIndex(NPC npc)
			{
				int num = 0;
				if (this._profiles.IndexInRange(npc.townNpcVariationIndex))
				{
					num = npc.townNpcVariationIndex;
				}
				return this._profiles[num].GetHeadTextureIndex(npc);
			}

			// Token: 0x04007158 RID: 29016
			internal ITownNPCProfile[] _profiles;
		}

		// Token: 0x020007EE RID: 2030
		public class LegacyNPCProfile : ITownNPCProfile
		{
			// Token: 0x06004280 RID: 17024 RVA: 0x006BEE38 File Offset: 0x006BD038
			public LegacyNPCProfile(string npcFileTitleFilePath, int defaultHeadIndex, bool includeDefault = true, bool uniquePartyTexture = true)
			{
				this._rootFilePath = npcFileTitleFilePath;
				this._defaultVariationHeadIndex = defaultHeadIndex;
				if (Main.dedServ)
				{
					return;
				}
				this._defaultNoAlt = Main.Assets.Request<Texture2D>(npcFileTitleFilePath + (includeDefault ? "_Default" : ""), 0);
				if (uniquePartyTexture)
				{
					this._defaultParty = Main.Assets.Request<Texture2D>(npcFileTitleFilePath + (includeDefault ? "_Default_Party" : "_Party"), 0);
					return;
				}
				this._defaultParty = this._defaultNoAlt;
			}

			// Token: 0x06004281 RID: 17025 RVA: 0x001DAC3B File Offset: 0x001D8E3B
			public int RollVariation()
			{
				return 0;
			}

			// Token: 0x06004282 RID: 17026 RVA: 0x006BEEBE File Offset: 0x006BD0BE
			public string GetNameForVariant(NPC npc)
			{
				return NPC.getNewNPCName(npc.type);
			}

			// Token: 0x06004283 RID: 17027 RVA: 0x006BEECB File Offset: 0x006BD0CB
			public Asset<Texture2D> GetTextureNPCShouldUse(NPC npc)
			{
				if (npc.IsABestiaryIconDummy && !npc.ForcePartyHatOn)
				{
					return this._defaultNoAlt;
				}
				if (npc.altTexture == 1)
				{
					return this._defaultParty;
				}
				return this._defaultNoAlt;
			}

			// Token: 0x06004284 RID: 17028 RVA: 0x006BEEFA File Offset: 0x006BD0FA
			public int GetHeadTextureIndex(NPC npc)
			{
				return this._defaultVariationHeadIndex;
			}

			// Token: 0x04007159 RID: 29017
			private string _rootFilePath;

			// Token: 0x0400715A RID: 29018
			private int _defaultVariationHeadIndex;

			// Token: 0x0400715B RID: 29019
			internal Asset<Texture2D> _defaultNoAlt;

			// Token: 0x0400715C RID: 29020
			internal Asset<Texture2D> _defaultParty;
		}

		// Token: 0x020007EF RID: 2031
		public class TransformableNPCProfile : ITownNPCProfile
		{
			// Token: 0x06004285 RID: 17029 RVA: 0x006BEF04 File Offset: 0x006BD104
			public TransformableNPCProfile(string npcFileTitleFilePath, int defaultHeadIndex, bool includeCredits = true)
			{
				this._rootFilePath = npcFileTitleFilePath;
				this._defaultVariationHeadIndex = defaultHeadIndex;
				if (Main.dedServ)
				{
					return;
				}
				this._defaultNoAlt = Main.Assets.Request<Texture2D>(npcFileTitleFilePath + "_Default", 0);
				this._defaultTransformed = Main.Assets.Request<Texture2D>(npcFileTitleFilePath + "_Default_Transformed", 0);
				if (includeCredits)
				{
					this._defaultCredits = Main.Assets.Request<Texture2D>(npcFileTitleFilePath + "_Default_Credits", 0);
				}
			}

			// Token: 0x06004286 RID: 17030 RVA: 0x001DAC3B File Offset: 0x001D8E3B
			public int RollVariation()
			{
				return 0;
			}

			// Token: 0x06004287 RID: 17031 RVA: 0x006BEEBE File Offset: 0x006BD0BE
			public string GetNameForVariant(NPC npc)
			{
				return NPC.getNewNPCName(npc.type);
			}

			// Token: 0x06004288 RID: 17032 RVA: 0x006BEF84 File Offset: 0x006BD184
			public Asset<Texture2D> GetTextureNPCShouldUse(NPC npc)
			{
				if (npc.altTexture == 3 && this._defaultCredits != null)
				{
					return this._defaultCredits;
				}
				if (npc.IsABestiaryIconDummy)
				{
					return this._defaultNoAlt;
				}
				if (npc.altTexture == 2)
				{
					return this._defaultTransformed;
				}
				return this._defaultNoAlt;
			}

			// Token: 0x06004289 RID: 17033 RVA: 0x006BEFC3 File Offset: 0x006BD1C3
			public int GetHeadTextureIndex(NPC npc)
			{
				return this._defaultVariationHeadIndex;
			}

			// Token: 0x0400715D RID: 29021
			private string _rootFilePath;

			// Token: 0x0400715E RID: 29022
			private int _defaultVariationHeadIndex;

			// Token: 0x0400715F RID: 29023
			internal Asset<Texture2D> _defaultNoAlt;

			// Token: 0x04007160 RID: 29024
			internal Asset<Texture2D> _defaultTransformed;

			// Token: 0x04007161 RID: 29025
			internal Asset<Texture2D> _defaultCredits;
		}

		// Token: 0x020007F0 RID: 2032
		public class VariantNPCProfile : ITownNPCProfile
		{
			// Token: 0x0600428A RID: 17034 RVA: 0x006BEFCC File Offset: 0x006BD1CC
			public VariantNPCProfile(string npcFileTitleFilePath, string npcBaseName, int[] variantHeadIds, params string[] variantTextureNames)
			{
				this._rootFilePath = npcFileTitleFilePath;
				this._npcBaseName = npcBaseName;
				this._variantHeadIDs = variantHeadIds;
				this._variants = variantTextureNames;
				foreach (string text in this._variants)
				{
					string text2 = this._rootFilePath + "_" + text;
					if (!Main.dedServ)
					{
						this._variantTextures[text2] = Main.Assets.Request<Texture2D>(text2, 0);
					}
				}
			}

			// Token: 0x0600428B RID: 17035 RVA: 0x006BF054 File Offset: 0x006BD254
			public Profiles.VariantNPCProfile SetPartyTextures(params string[] variantTextureNames)
			{
				foreach (string text in variantTextureNames)
				{
					string text2 = this._rootFilePath + "_" + text + "_Party";
					if (!Main.dedServ)
					{
						this._variantTextures[text2] = Main.Assets.Request<Texture2D>(text2, 0);
					}
				}
				return this;
			}

			// Token: 0x0600428C RID: 17036 RVA: 0x006BF0AC File Offset: 0x006BD2AC
			public int RollVariation()
			{
				return Main.rand.Next(this._variants.Length);
			}

			// Token: 0x0600428D RID: 17037 RVA: 0x006BF0C0 File Offset: 0x006BD2C0
			public string GetNameForVariant(NPC npc)
			{
				return Language.RandomFromCategory(this._npcBaseName + "Names_" + this._variants[npc.townNpcVariationIndex], WorldGen.genRand).Value;
			}

			// Token: 0x0600428E RID: 17038 RVA: 0x006BF0F0 File Offset: 0x006BD2F0
			public Asset<Texture2D> GetTextureNPCShouldUse(NPC npc)
			{
				string text = this._rootFilePath + "_" + this._variants[npc.townNpcVariationIndex];
				if (npc.IsABestiaryIconDummy)
				{
					return this._variantTextures[text];
				}
				if (npc.altTexture == 1 && this._variantTextures.ContainsKey(text + "_Party"))
				{
					return this._variantTextures[text + "_Party"];
				}
				return this._variantTextures[text];
			}

			// Token: 0x0600428F RID: 17039 RVA: 0x006BF174 File Offset: 0x006BD374
			public int GetHeadTextureIndex(NPC npc)
			{
				return this._variantHeadIDs[npc.townNpcVariationIndex];
			}

			// Token: 0x04007162 RID: 29026
			private string _rootFilePath;

			// Token: 0x04007163 RID: 29027
			private string _npcBaseName;

			// Token: 0x04007164 RID: 29028
			private int[] _variantHeadIDs;

			// Token: 0x04007165 RID: 29029
			private string[] _variants;

			// Token: 0x04007166 RID: 29030
			internal Dictionary<string, Asset<Texture2D>> _variantTextures = new Dictionary<string, Asset<Texture2D>>();
		}
	}
}
