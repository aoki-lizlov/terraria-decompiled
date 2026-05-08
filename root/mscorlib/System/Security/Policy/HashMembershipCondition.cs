using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using Mono.Security.Cryptography;

namespace System.Security.Policy
{
	// Token: 0x020003E3 RID: 995
	[ComVisible(true)]
	[Serializable]
	public sealed class HashMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IDeserializationCallback, ISerializable
	{
		// Token: 0x06002A52 RID: 10834 RVA: 0x0009A507 File Offset: 0x00098707
		internal HashMembershipCondition()
		{
		}

		// Token: 0x06002A53 RID: 10835 RVA: 0x0009A518 File Offset: 0x00098718
		public HashMembershipCondition(HashAlgorithm hashAlg, byte[] value)
		{
			if (hashAlg == null)
			{
				throw new ArgumentNullException("hashAlg");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.hash_algorithm = hashAlg;
			this.hash_value = (byte[])value.Clone();
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06002A54 RID: 10836 RVA: 0x0009A566 File Offset: 0x00098766
		// (set) Token: 0x06002A55 RID: 10837 RVA: 0x0009A581 File Offset: 0x00098781
		public HashAlgorithm HashAlgorithm
		{
			get
			{
				if (this.hash_algorithm == null)
				{
					this.hash_algorithm = new SHA1Managed();
				}
				return this.hash_algorithm;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("HashAlgorithm");
				}
				this.hash_algorithm = value;
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06002A56 RID: 10838 RVA: 0x0009A598 File Offset: 0x00098798
		// (set) Token: 0x06002A57 RID: 10839 RVA: 0x0009A5C2 File Offset: 0x000987C2
		public byte[] HashValue
		{
			get
			{
				if (this.hash_value == null)
				{
					throw new ArgumentException(Locale.GetText("No HashValue available."));
				}
				return (byte[])this.hash_value.Clone();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("HashValue");
				}
				this.hash_value = (byte[])value.Clone();
			}
		}

		// Token: 0x06002A58 RID: 10840 RVA: 0x0009A5E4 File Offset: 0x000987E4
		public bool Check(Evidence evidence)
		{
			if (evidence == null)
			{
				return false;
			}
			IEnumerator hostEnumerator = evidence.GetHostEnumerator();
			while (hostEnumerator.MoveNext())
			{
				object obj = hostEnumerator.Current;
				Hash hash = obj as Hash;
				if (hash != null)
				{
					if (this.Compare(this.hash_value, hash.GenerateHash(this.hash_algorithm)))
					{
						return true;
					}
					break;
				}
			}
			return false;
		}

		// Token: 0x06002A59 RID: 10841 RVA: 0x0009A633 File Offset: 0x00098833
		public IMembershipCondition Copy()
		{
			return new HashMembershipCondition(this.hash_algorithm, this.hash_value);
		}

		// Token: 0x06002A5A RID: 10842 RVA: 0x0009A648 File Offset: 0x00098848
		public override bool Equals(object o)
		{
			HashMembershipCondition hashMembershipCondition = o as HashMembershipCondition;
			return hashMembershipCondition != null && hashMembershipCondition.HashAlgorithm == this.hash_algorithm && this.Compare(this.hash_value, hashMembershipCondition.hash_value);
		}

		// Token: 0x06002A5B RID: 10843 RVA: 0x0009A683 File Offset: 0x00098883
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		// Token: 0x06002A5C RID: 10844 RVA: 0x0009A68C File Offset: 0x0009888C
		public SecurityElement ToXml(PolicyLevel level)
		{
			SecurityElement securityElement = MembershipConditionHelper.Element(typeof(HashMembershipCondition), this.version);
			securityElement.AddAttribute("HashValue", CryptoConvert.ToHex(this.HashValue));
			securityElement.AddAttribute("HashAlgorithm", this.hash_algorithm.GetType().FullName);
			return securityElement;
		}

		// Token: 0x06002A5D RID: 10845 RVA: 0x0009A6DF File Offset: 0x000988DF
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		// Token: 0x06002A5E RID: 10846 RVA: 0x0009A6EC File Offset: 0x000988EC
		public void FromXml(SecurityElement e, PolicyLevel level)
		{
			MembershipConditionHelper.CheckSecurityElement(e, "e", this.version, this.version);
			this.hash_value = CryptoConvert.FromHex(e.Attribute("HashValue"));
			string text = e.Attribute("HashAlgorithm");
			this.hash_algorithm = ((text == null) ? null : HashAlgorithm.Create(text));
		}

		// Token: 0x06002A5F RID: 10847 RVA: 0x0009A748 File Offset: 0x00098948
		public override int GetHashCode()
		{
			int num = this.hash_algorithm.GetType().GetHashCode();
			if (this.hash_value != null)
			{
				foreach (byte b in this.hash_value)
				{
					num ^= (int)b;
				}
			}
			return num;
		}

		// Token: 0x06002A60 RID: 10848 RVA: 0x0009A78C File Offset: 0x0009898C
		public override string ToString()
		{
			Type type = this.HashAlgorithm.GetType();
			return string.Format("Hash - {0} {1} = {2}", type.FullName, type.Assembly, CryptoConvert.ToHex(this.HashValue));
		}

		// Token: 0x06002A61 RID: 10849 RVA: 0x0009A7C8 File Offset: 0x000989C8
		private bool Compare(byte[] expected, byte[] actual)
		{
			if (expected.Length != actual.Length)
			{
				return false;
			}
			int num = expected.Length;
			for (int i = 0; i < num; i++)
			{
				if (expected[i] != actual[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002A62 RID: 10850 RVA: 0x00004088 File Offset: 0x00002288
		[MonoTODO("fx 2.0")]
		void IDeserializationCallback.OnDeserialization(object sender)
		{
		}

		// Token: 0x06002A63 RID: 10851 RVA: 0x00004088 File Offset: 0x00002288
		[MonoTODO("fx 2.0")]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
		}

		// Token: 0x04001E67 RID: 7783
		private readonly int version = 1;

		// Token: 0x04001E68 RID: 7784
		private HashAlgorithm hash_algorithm;

		// Token: 0x04001E69 RID: 7785
		private byte[] hash_value;
	}
}
