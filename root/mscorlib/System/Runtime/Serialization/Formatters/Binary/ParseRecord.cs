using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000689 RID: 1673
	internal sealed class ParseRecord
	{
		// Token: 0x06003F3C RID: 16188 RVA: 0x000025BE File Offset: 0x000007BE
		internal ParseRecord()
		{
		}

		// Token: 0x06003F3D RID: 16189 RVA: 0x000DECA4 File Offset: 0x000DCEA4
		internal void Init()
		{
			this.PRparseTypeEnum = InternalParseTypeE.Empty;
			this.PRobjectTypeEnum = InternalObjectTypeE.Empty;
			this.PRarrayTypeEnum = InternalArrayTypeE.Empty;
			this.PRmemberTypeEnum = InternalMemberTypeE.Empty;
			this.PRmemberValueEnum = InternalMemberValueE.Empty;
			this.PRobjectPositionEnum = InternalObjectPositionE.Empty;
			this.PRname = null;
			this.PRvalue = null;
			this.PRkeyDt = null;
			this.PRdtType = null;
			this.PRdtTypeCode = InternalPrimitiveTypeE.Invalid;
			this.PRisEnum = false;
			this.PRobjectId = 0L;
			this.PRidRef = 0L;
			this.PRarrayElementTypeString = null;
			this.PRarrayElementType = null;
			this.PRisArrayVariant = false;
			this.PRarrayElementTypeCode = InternalPrimitiveTypeE.Invalid;
			this.PRrank = 0;
			this.PRlengthA = null;
			this.PRpositionA = null;
			this.PRlowerBoundA = null;
			this.PRupperBoundA = null;
			this.PRindexMap = null;
			this.PRmemberIndex = 0;
			this.PRlinearlength = 0;
			this.PRrectangularMap = null;
			this.PRisLowerBound = false;
			this.PRtopId = 0L;
			this.PRheaderId = 0L;
			this.PRisValueTypeFixup = false;
			this.PRnewObj = null;
			this.PRobjectA = null;
			this.PRprimitiveArray = null;
			this.PRobjectInfo = null;
			this.PRisRegistered = false;
			this.PRmemberData = null;
			this.PRsi = null;
			this.PRnullCount = 0;
		}

		// Token: 0x06003F3E RID: 16190 RVA: 0x000DEDC6 File Offset: 0x000DCFC6
		// Note: this type is marked as 'beforefieldinit'.
		static ParseRecord()
		{
		}

		// Token: 0x04002902 RID: 10498
		internal static int parseRecordIdCount = 1;

		// Token: 0x04002903 RID: 10499
		internal int PRparseRecordId;

		// Token: 0x04002904 RID: 10500
		internal InternalParseTypeE PRparseTypeEnum;

		// Token: 0x04002905 RID: 10501
		internal InternalObjectTypeE PRobjectTypeEnum;

		// Token: 0x04002906 RID: 10502
		internal InternalArrayTypeE PRarrayTypeEnum;

		// Token: 0x04002907 RID: 10503
		internal InternalMemberTypeE PRmemberTypeEnum;

		// Token: 0x04002908 RID: 10504
		internal InternalMemberValueE PRmemberValueEnum;

		// Token: 0x04002909 RID: 10505
		internal InternalObjectPositionE PRobjectPositionEnum;

		// Token: 0x0400290A RID: 10506
		internal string PRname;

		// Token: 0x0400290B RID: 10507
		internal string PRvalue;

		// Token: 0x0400290C RID: 10508
		internal object PRvarValue;

		// Token: 0x0400290D RID: 10509
		internal string PRkeyDt;

		// Token: 0x0400290E RID: 10510
		internal Type PRdtType;

		// Token: 0x0400290F RID: 10511
		internal InternalPrimitiveTypeE PRdtTypeCode;

		// Token: 0x04002910 RID: 10512
		internal bool PRisVariant;

		// Token: 0x04002911 RID: 10513
		internal bool PRisEnum;

		// Token: 0x04002912 RID: 10514
		internal long PRobjectId;

		// Token: 0x04002913 RID: 10515
		internal long PRidRef;

		// Token: 0x04002914 RID: 10516
		internal string PRarrayElementTypeString;

		// Token: 0x04002915 RID: 10517
		internal Type PRarrayElementType;

		// Token: 0x04002916 RID: 10518
		internal bool PRisArrayVariant;

		// Token: 0x04002917 RID: 10519
		internal InternalPrimitiveTypeE PRarrayElementTypeCode;

		// Token: 0x04002918 RID: 10520
		internal int PRrank;

		// Token: 0x04002919 RID: 10521
		internal int[] PRlengthA;

		// Token: 0x0400291A RID: 10522
		internal int[] PRpositionA;

		// Token: 0x0400291B RID: 10523
		internal int[] PRlowerBoundA;

		// Token: 0x0400291C RID: 10524
		internal int[] PRupperBoundA;

		// Token: 0x0400291D RID: 10525
		internal int[] PRindexMap;

		// Token: 0x0400291E RID: 10526
		internal int PRmemberIndex;

		// Token: 0x0400291F RID: 10527
		internal int PRlinearlength;

		// Token: 0x04002920 RID: 10528
		internal int[] PRrectangularMap;

		// Token: 0x04002921 RID: 10529
		internal bool PRisLowerBound;

		// Token: 0x04002922 RID: 10530
		internal long PRtopId;

		// Token: 0x04002923 RID: 10531
		internal long PRheaderId;

		// Token: 0x04002924 RID: 10532
		internal ReadObjectInfo PRobjectInfo;

		// Token: 0x04002925 RID: 10533
		internal bool PRisValueTypeFixup;

		// Token: 0x04002926 RID: 10534
		internal object PRnewObj;

		// Token: 0x04002927 RID: 10535
		internal object[] PRobjectA;

		// Token: 0x04002928 RID: 10536
		internal PrimitiveArray PRprimitiveArray;

		// Token: 0x04002929 RID: 10537
		internal bool PRisRegistered;

		// Token: 0x0400292A RID: 10538
		internal object[] PRmemberData;

		// Token: 0x0400292B RID: 10539
		internal SerializationInfo PRsi;

		// Token: 0x0400292C RID: 10540
		internal int PRnullCount;
	}
}
