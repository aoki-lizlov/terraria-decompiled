using System;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x020002F1 RID: 753
	public class ItemDropRule
	{
		// Token: 0x06002674 RID: 9844 RVA: 0x0055F265 File Offset: 0x0055D465
		public static IItemDropRule Common(int itemId, int chanceDenominator = 1, int minimumDropped = 1, int maximumDropped = 1)
		{
			return new CommonDrop(itemId, chanceDenominator, minimumDropped, maximumDropped, 1);
		}

		// Token: 0x06002675 RID: 9845 RVA: 0x0055F271 File Offset: 0x0055D471
		public static IItemDropRule BossBag(int itemId)
		{
			return new DropBasedOnExpertMode(ItemDropRule.DropNothing(), new DropLocalPerClientAndResetsNPCMoneyTo0(itemId, 1, 1, 1, null));
		}

		// Token: 0x06002676 RID: 9846 RVA: 0x0055F287 File Offset: 0x0055D487
		public static IItemDropRule BossBagByCondition(IItemDropRuleCondition condition, int itemId)
		{
			return new DropBasedOnExpertMode(ItemDropRule.DropNothing(), new DropLocalPerClientAndResetsNPCMoneyTo0(itemId, 1, 1, 1, condition));
		}

		// Token: 0x06002677 RID: 9847 RVA: 0x0055F29D File Offset: 0x0055D49D
		public static IItemDropRule ExpertGetsRerolls(int itemId, int chanceDenominator, int expertRerolls)
		{
			return new DropBasedOnExpertMode(ItemDropRule.WithRerolls(itemId, 0, chanceDenominator, 1, 1), ItemDropRule.WithRerolls(itemId, expertRerolls, chanceDenominator, 1, 1));
		}

		// Token: 0x06002678 RID: 9848 RVA: 0x0055F2B8 File Offset: 0x0055D4B8
		public static IItemDropRule MasterModeCommonDrop(int itemId)
		{
			return ItemDropRule.ByCondition(new Conditions.IsMasterMode(), itemId, 1, 1, 1, 1);
		}

		// Token: 0x06002679 RID: 9849 RVA: 0x0055F2C9 File Offset: 0x0055D4C9
		public static IItemDropRule MasterModeDropOnAllPlayers(int itemId, int chanceDenominator = 1)
		{
			return new DropBasedOnMasterMode(ItemDropRule.DropNothing(), new DropPerPlayerOnThePlayer(itemId, chanceDenominator, 1, 1, new Conditions.IsMasterMode()));
		}

		// Token: 0x0600267A RID: 9850 RVA: 0x0055F2E3 File Offset: 0x0055D4E3
		public static IItemDropRule WithRerolls(int itemId, int rerolls, int chanceDenominator = 1, int minimumDropped = 1, int maximumDropped = 1)
		{
			return new CommonDropWithRerolls(itemId, chanceDenominator, minimumDropped, maximumDropped, rerolls);
		}

		// Token: 0x0600267B RID: 9851 RVA: 0x0055F2F0 File Offset: 0x0055D4F0
		public static IItemDropRule ByCondition(IItemDropRuleCondition condition, int itemId, int chanceDenominator = 1, int minimumDropped = 1, int maximumDropped = 1, int chanceNumerator = 1)
		{
			return new ItemDropWithConditionRule(itemId, chanceDenominator, minimumDropped, maximumDropped, condition, chanceNumerator);
		}

		// Token: 0x0600267C RID: 9852 RVA: 0x0055F2FF File Offset: 0x0055D4FF
		public static IItemDropRule ScalingWithOnlyBadLuck(int itemId, int chanceDenominator = 1, int minimumDropped = 1, int maximumDropped = 1)
		{
			return new CommonDropScalingWithOnlyBadLuck(itemId, chanceDenominator, minimumDropped, maximumDropped);
		}

		// Token: 0x0600267D RID: 9853 RVA: 0x0055F30A File Offset: 0x0055D50A
		public static IItemDropRule NotScalingWithLuck(int itemId, int chanceDenominator = 1, int minimumDropped = 1, int maximumDropped = 1)
		{
			return new CommonDropNotScalingWithLuck(itemId, chanceDenominator, minimumDropped, maximumDropped);
		}

		// Token: 0x0600267E RID: 9854 RVA: 0x0055F315 File Offset: 0x0055D515
		public static IItemDropRule OneFromOptionsNotScalingWithLuck(int chanceDenominator, params int[] options)
		{
			return new OneFromOptionsNotScaledWithLuckDropRule(chanceDenominator, 1, options);
		}

		// Token: 0x0600267F RID: 9855 RVA: 0x0055F31F File Offset: 0x0055D51F
		public static IItemDropRule OneFromOptionsNotScalingWithLuckWithX(int chanceDenominator, int chanceNumerator, params int[] options)
		{
			return new OneFromOptionsNotScaledWithLuckDropRule(chanceDenominator, chanceNumerator, options);
		}

		// Token: 0x06002680 RID: 9856 RVA: 0x0055F329 File Offset: 0x0055D529
		public static IItemDropRule OneFromOptions(int chanceDenominator, params int[] options)
		{
			return new OneFromOptionsDropRule(chanceDenominator, 1, options);
		}

		// Token: 0x06002681 RID: 9857 RVA: 0x0055F333 File Offset: 0x0055D533
		public static IItemDropRule OneFromOptionsWithNumerator(int chanceDenominator, int chanceNumerator, params int[] options)
		{
			return new OneFromOptionsDropRule(chanceDenominator, chanceNumerator, options);
		}

		// Token: 0x06002682 RID: 9858 RVA: 0x0055F33D File Offset: 0x0055D53D
		public static IItemDropRule DropNothing()
		{
			return new DropNothing();
		}

		// Token: 0x06002683 RID: 9859 RVA: 0x0055F344 File Offset: 0x0055D544
		public static IItemDropRule Gel(int chanceDenominator = 1, int minimumDropped = 1, int maximumDropped = 1)
		{
			short num = 23;
			int num2 = 2;
			return new DropBasedOnExtraGel(ItemDropRule.Common((int)num, chanceDenominator, minimumDropped, maximumDropped), ItemDropRule.Common((int)num, chanceDenominator, minimumDropped * num2, maximumDropped * num2));
		}

		// Token: 0x06002684 RID: 9860 RVA: 0x0055F371 File Offset: 0x0055D571
		public static IItemDropRule NormalvsExpert(int itemId, int chanceDenominatorInNormal, int chanceDenominatorInExpert)
		{
			return new DropBasedOnExpertMode(ItemDropRule.Common(itemId, chanceDenominatorInNormal, 1, 1), ItemDropRule.Common(itemId, chanceDenominatorInExpert, 1, 1));
		}

		// Token: 0x06002685 RID: 9861 RVA: 0x0055F38A File Offset: 0x0055D58A
		public static IItemDropRule NormalvsExpertNotScalingWithLuck(int itemId, int chanceDenominatorInNormal, int chanceDenominatorInExpert)
		{
			return new DropBasedOnExpertMode(ItemDropRule.NotScalingWithLuck(itemId, chanceDenominatorInNormal, 1, 1), ItemDropRule.NotScalingWithLuck(itemId, chanceDenominatorInExpert, 1, 1));
		}

		// Token: 0x06002686 RID: 9862 RVA: 0x0055F3A3 File Offset: 0x0055D5A3
		public static IItemDropRule NormalvsExpertOneFromOptionsNotScalingWithLuck(int chanceDenominatorInNormal, int chanceDenominatorInExpert, params int[] options)
		{
			return new DropBasedOnExpertMode(ItemDropRule.OneFromOptionsNotScalingWithLuck(chanceDenominatorInNormal, options), ItemDropRule.OneFromOptionsNotScalingWithLuck(chanceDenominatorInExpert, options));
		}

		// Token: 0x06002687 RID: 9863 RVA: 0x0055F3B8 File Offset: 0x0055D5B8
		public static IItemDropRule NormalvsExpertOneFromOptions(int chanceDenominatorInNormal, int chanceDenominatorInExpert, params int[] options)
		{
			return new DropBasedOnExpertMode(ItemDropRule.OneFromOptions(chanceDenominatorInNormal, options), ItemDropRule.OneFromOptions(chanceDenominatorInExpert, options));
		}

		// Token: 0x06002688 RID: 9864 RVA: 0x0055F3CD File Offset: 0x0055D5CD
		public static IItemDropRule Food(int itemId, int chanceDenominator, int minimumDropped = 1, int maximumDropped = 1)
		{
			return new ItemDropWithConditionRule(itemId, chanceDenominator, minimumDropped, maximumDropped, new Conditions.NotFromStatue(), 1);
		}

		// Token: 0x06002689 RID: 9865 RVA: 0x0055F3DE File Offset: 0x0055D5DE
		public static IItemDropRule StatusImmunityItem(int itemId, int dropsOutOfX)
		{
			return ItemDropRule.ExpertGetsRerolls(itemId, dropsOutOfX, 1);
		}

		// Token: 0x0600268A RID: 9866 RVA: 0x0000357B File Offset: 0x0000177B
		public ItemDropRule()
		{
		}
	}
}
