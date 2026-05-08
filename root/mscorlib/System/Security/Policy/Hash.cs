using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;

namespace System.Security.Policy
{
	// Token: 0x020003E2 RID: 994
	[ComVisible(true)]
	[Serializable]
	public sealed class Hash : EvidenceBase, ISerializable, IBuiltInEvidence
	{
		// Token: 0x06002A44 RID: 10820 RVA: 0x0009A28D File Offset: 0x0009848D
		public Hash(Assembly assembly)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			this.assembly = assembly;
		}

		// Token: 0x06002A45 RID: 10821 RVA: 0x00097F72 File Offset: 0x00096172
		internal Hash()
		{
		}

		// Token: 0x06002A46 RID: 10822 RVA: 0x0009A2B0 File Offset: 0x000984B0
		internal Hash(SerializationInfo info, StreamingContext context)
		{
			this.data = (byte[])info.GetValue("RawData", typeof(byte[]));
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06002A47 RID: 10823 RVA: 0x0009A2D8 File Offset: 0x000984D8
		public byte[] MD5
		{
			get
			{
				if (this._md5 != null)
				{
					return this._md5;
				}
				if (this.assembly == null && this._sha1 != null)
				{
					throw new SecurityException(Locale.GetText("No assembly data. This instance was initialized with an MSHA1 digest value."));
				}
				HashAlgorithm hashAlgorithm = global::System.Security.Cryptography.MD5.Create();
				this._md5 = this.GenerateHash(hashAlgorithm);
				return this._md5;
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06002A48 RID: 10824 RVA: 0x0009A334 File Offset: 0x00098534
		public byte[] SHA1
		{
			get
			{
				if (this._sha1 != null)
				{
					return this._sha1;
				}
				if (this.assembly == null && this._md5 != null)
				{
					throw new SecurityException(Locale.GetText("No assembly data. This instance was initialized with an MD5 digest value."));
				}
				HashAlgorithm hashAlgorithm = global::System.Security.Cryptography.SHA1.Create();
				this._sha1 = this.GenerateHash(hashAlgorithm);
				return this._sha1;
			}
		}

		// Token: 0x06002A49 RID: 10825 RVA: 0x0009A38F File Offset: 0x0009858F
		public byte[] GenerateHash(HashAlgorithm hashAlg)
		{
			if (hashAlg == null)
			{
				throw new ArgumentNullException("hashAlg");
			}
			return hashAlg.ComputeHash(this.GetData());
		}

		// Token: 0x06002A4A RID: 10826 RVA: 0x0009A3AB File Offset: 0x000985AB
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("RawData", this.GetData());
		}

		// Token: 0x06002A4B RID: 10827 RVA: 0x0009A3CC File Offset: 0x000985CC
		public override string ToString()
		{
			SecurityElement securityElement = new SecurityElement(base.GetType().FullName);
			securityElement.AddAttribute("version", "1");
			StringBuilder stringBuilder = new StringBuilder();
			byte[] array = this.GetData();
			for (int i = 0; i < array.Length; i++)
			{
				stringBuilder.Append(array[i].ToString("X2"));
			}
			securityElement.AddChild(new SecurityElement("RawData", stringBuilder.ToString()));
			return securityElement.ToString();
		}

		// Token: 0x06002A4C RID: 10828 RVA: 0x0009A44C File Offset: 0x0009864C
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		private byte[] GetData()
		{
			if (this.assembly == null && this.data == null)
			{
				throw new SecurityException(Locale.GetText("No assembly data."));
			}
			if (this.data == null)
			{
				FileStream fileStream = new FileStream(this.assembly.Location, FileMode.Open, FileAccess.Read);
				this.data = new byte[fileStream.Length];
				fileStream.Read(this.data, 0, (int)fileStream.Length);
			}
			return this.data;
		}

		// Token: 0x06002A4D RID: 10829 RVA: 0x0009A4C7 File Offset: 0x000986C7
		int IBuiltInEvidence.GetRequiredSize(bool verbose)
		{
			if (!verbose)
			{
				return 0;
			}
			return 5;
		}

		// Token: 0x06002A4E RID: 10830 RVA: 0x0000408A File Offset: 0x0000228A
		[MonoTODO("IBuiltInEvidence")]
		int IBuiltInEvidence.InitFromBuffer(char[] buffer, int position)
		{
			return 0;
		}

		// Token: 0x06002A4F RID: 10831 RVA: 0x0000408A File Offset: 0x0000228A
		[MonoTODO("IBuiltInEvidence")]
		int IBuiltInEvidence.OutputToBuffer(char[] buffer, int position, bool verbose)
		{
			return 0;
		}

		// Token: 0x06002A50 RID: 10832 RVA: 0x0009A4CF File Offset: 0x000986CF
		public static Hash CreateMD5(byte[] md5)
		{
			if (md5 == null)
			{
				throw new ArgumentNullException("md5");
			}
			return new Hash
			{
				_md5 = md5
			};
		}

		// Token: 0x06002A51 RID: 10833 RVA: 0x0009A4EB File Offset: 0x000986EB
		public static Hash CreateSHA1(byte[] sha1)
		{
			if (sha1 == null)
			{
				throw new ArgumentNullException("sha1");
			}
			return new Hash
			{
				_sha1 = sha1
			};
		}

		// Token: 0x04001E63 RID: 7779
		private Assembly assembly;

		// Token: 0x04001E64 RID: 7780
		private byte[] data;

		// Token: 0x04001E65 RID: 7781
		internal byte[] _md5;

		// Token: 0x04001E66 RID: 7782
		internal byte[] _sha1;
	}
}
