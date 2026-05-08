using System;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200068E RID: 1678
	internal sealed class ValueFixup
	{
		// Token: 0x06003F5A RID: 16218 RVA: 0x000DF32D File Offset: 0x000DD52D
		internal ValueFixup(Array arrayObj, int[] indexMap)
		{
			this.valueFixupEnum = ValueFixupEnum.Array;
			this.arrayObj = arrayObj;
			this.indexMap = indexMap;
		}

		// Token: 0x06003F5B RID: 16219 RVA: 0x000DF34A File Offset: 0x000DD54A
		internal ValueFixup(object memberObject, string memberName, ReadObjectInfo objectInfo)
		{
			this.valueFixupEnum = ValueFixupEnum.Member;
			this.memberObject = memberObject;
			this.memberName = memberName;
			this.objectInfo = objectInfo;
		}

		// Token: 0x06003F5C RID: 16220 RVA: 0x000DF370 File Offset: 0x000DD570
		[SecurityCritical]
		internal void Fixup(ParseRecord record, ParseRecord parent)
		{
			object prnewObj = record.PRnewObj;
			switch (this.valueFixupEnum)
			{
			case ValueFixupEnum.Array:
				this.arrayObj.SetValue(prnewObj, this.indexMap);
				return;
			case ValueFixupEnum.Header:
			{
				Type typeFromHandle = typeof(Header);
				if (ValueFixup.valueInfo == null)
				{
					MemberInfo[] member = typeFromHandle.GetMember("Value");
					if (member.Length != 1)
					{
						throw new SerializationException(Environment.GetResourceString("Header reflection error: number of value members: {0}.", new object[] { member.Length }));
					}
					ValueFixup.valueInfo = member[0];
				}
				FormatterServices.SerializationSetValue(ValueFixup.valueInfo, this.header, prnewObj);
				return;
			}
			case ValueFixupEnum.Member:
			{
				if (this.objectInfo.isSi)
				{
					this.objectInfo.objectManager.RecordDelayedFixup(parent.PRobjectId, this.memberName, record.PRobjectId);
					return;
				}
				MemberInfo memberInfo = this.objectInfo.GetMemberInfo(this.memberName);
				if (memberInfo != null)
				{
					this.objectInfo.objectManager.RecordFixup(parent.PRobjectId, memberInfo, record.PRobjectId);
				}
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x04002937 RID: 10551
		internal ValueFixupEnum valueFixupEnum;

		// Token: 0x04002938 RID: 10552
		internal Array arrayObj;

		// Token: 0x04002939 RID: 10553
		internal int[] indexMap;

		// Token: 0x0400293A RID: 10554
		internal object header;

		// Token: 0x0400293B RID: 10555
		internal object memberObject;

		// Token: 0x0400293C RID: 10556
		internal static volatile MemberInfo valueInfo;

		// Token: 0x0400293D RID: 10557
		internal ReadObjectInfo objectInfo;

		// Token: 0x0400293E RID: 10558
		internal string memberName;
	}
}
