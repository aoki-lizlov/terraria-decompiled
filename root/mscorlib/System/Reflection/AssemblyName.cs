using System;
using System.Configuration.Assemblies;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Text;
using Mono;
using Mono.Security;
using Mono.Security.Cryptography;

namespace System.Reflection
{
	// Token: 0x020008BB RID: 2235
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_AssemblyName))]
	[ClassInterface(ClassInterfaceType.None)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class AssemblyName : ICloneable, ISerializable, IDeserializationCallback, _AssemblyName
	{
		// Token: 0x06004BC5 RID: 19397 RVA: 0x000F1B57 File Offset: 0x000EFD57
		public AssemblyName()
		{
			this.versioncompat = AssemblyVersionCompatibility.SameMachine;
		}

		// Token: 0x06004BC6 RID: 19398
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ParseAssemblyName(IntPtr name, out MonoAssemblyName aname, out bool is_version_definited, out bool is_token_defined);

		// Token: 0x06004BC7 RID: 19399 RVA: 0x000F1B68 File Offset: 0x000EFD68
		public unsafe AssemblyName(string assemblyName)
		{
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			if (assemblyName.Length < 1)
			{
				throw new ArgumentException("assemblyName cannot have zero length.");
			}
			using (SafeStringMarshal safeStringMarshal = RuntimeMarshal.MarshalString(assemblyName))
			{
				MonoAssemblyName monoAssemblyName;
				bool flag;
				bool flag2;
				if (!AssemblyName.ParseAssemblyName(safeStringMarshal.Value, out monoAssemblyName, out flag, out flag2))
				{
					throw new FileLoadException("The assembly name is invalid.");
				}
				try
				{
					this.FillName(&monoAssemblyName, null, flag, false, flag2, false);
				}
				finally
				{
					RuntimeMarshal.FreeAssemblyName(ref monoAssemblyName, false);
				}
			}
		}

		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x06004BC8 RID: 19400 RVA: 0x000F1C08 File Offset: 0x000EFE08
		// (set) Token: 0x06004BC9 RID: 19401 RVA: 0x000F1C10 File Offset: 0x000EFE10
		public ProcessorArchitecture ProcessorArchitecture
		{
			get
			{
				return this.processor_architecture;
			}
			set
			{
				this.processor_architecture = value;
			}
		}

		// Token: 0x06004BCA RID: 19402 RVA: 0x000F1C1C File Offset: 0x000EFE1C
		internal AssemblyName(SerializationInfo si, StreamingContext sc)
		{
			this.name = si.GetString("_Name");
			this.codebase = si.GetString("_CodeBase");
			this.version = (Version)si.GetValue("_Version", typeof(Version));
			this.publicKey = (byte[])si.GetValue("_PublicKey", typeof(byte[]));
			this.keyToken = (byte[])si.GetValue("_PublicKeyToken", typeof(byte[]));
			this.hashalg = (AssemblyHashAlgorithm)si.GetValue("_HashAlgorithm", typeof(AssemblyHashAlgorithm));
			this.keypair = (StrongNameKeyPair)si.GetValue("_StrongNameKeyPair", typeof(StrongNameKeyPair));
			this.versioncompat = (AssemblyVersionCompatibility)si.GetValue("_VersionCompatibility", typeof(AssemblyVersionCompatibility));
			this.flags = (AssemblyNameFlags)si.GetValue("_Flags", typeof(AssemblyNameFlags));
			int @int = si.GetInt32("_CultureInfo");
			if (@int != -1)
			{
				this.cultureinfo = new CultureInfo(@int);
			}
		}

		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x06004BCB RID: 19403 RVA: 0x000F1D4D File Offset: 0x000EFF4D
		// (set) Token: 0x06004BCC RID: 19404 RVA: 0x000F1D55 File Offset: 0x000EFF55
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x06004BCD RID: 19405 RVA: 0x000F1D5E File Offset: 0x000EFF5E
		// (set) Token: 0x06004BCE RID: 19406 RVA: 0x000F1D66 File Offset: 0x000EFF66
		public string CodeBase
		{
			get
			{
				return this.codebase;
			}
			set
			{
				this.codebase = value;
			}
		}

		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x06004BCF RID: 19407 RVA: 0x000F1D6F File Offset: 0x000EFF6F
		public string EscapedCodeBase
		{
			get
			{
				if (this.codebase == null)
				{
					return null;
				}
				return Uri.EscapeString(this.codebase, false, true, true);
			}
		}

		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x06004BD0 RID: 19408 RVA: 0x000F1D89 File Offset: 0x000EFF89
		// (set) Token: 0x06004BD1 RID: 19409 RVA: 0x000F1D91 File Offset: 0x000EFF91
		public CultureInfo CultureInfo
		{
			get
			{
				return this.cultureinfo;
			}
			set
			{
				this.cultureinfo = value;
			}
		}

		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x06004BD2 RID: 19410 RVA: 0x000F1D9A File Offset: 0x000EFF9A
		// (set) Token: 0x06004BD3 RID: 19411 RVA: 0x000F1DA2 File Offset: 0x000EFFA2
		public AssemblyNameFlags Flags
		{
			get
			{
				return this.flags;
			}
			set
			{
				this.flags = value;
			}
		}

		// Token: 0x17000C48 RID: 3144
		// (get) Token: 0x06004BD4 RID: 19412 RVA: 0x000F1DAC File Offset: 0x000EFFAC
		public string FullName
		{
			get
			{
				if (this.name == null)
				{
					return string.Empty;
				}
				StringBuilder stringBuilder = new StringBuilder();
				if (char.IsWhiteSpace(this.name[0]))
				{
					stringBuilder.Append("\"" + this.name + "\"");
				}
				else
				{
					stringBuilder.Append(this.name);
				}
				if (this.Version != null)
				{
					stringBuilder.Append(", Version=");
					stringBuilder.Append(this.Version.ToString());
				}
				if (this.cultureinfo != null)
				{
					stringBuilder.Append(", Culture=");
					if (this.cultureinfo.LCID == CultureInfo.InvariantCulture.LCID)
					{
						stringBuilder.Append("neutral");
					}
					else
					{
						stringBuilder.Append(this.cultureinfo.Name);
					}
				}
				byte[] array = this.InternalGetPublicKeyToken();
				if (array != null)
				{
					if (array.Length == 0)
					{
						stringBuilder.Append(", PublicKeyToken=null");
					}
					else
					{
						stringBuilder.Append(", PublicKeyToken=");
						for (int i = 0; i < array.Length; i++)
						{
							stringBuilder.Append(array[i].ToString("x2"));
						}
					}
				}
				if ((this.Flags & AssemblyNameFlags.Retargetable) != AssemblyNameFlags.None)
				{
					stringBuilder.Append(", Retargetable=Yes");
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x06004BD5 RID: 19413 RVA: 0x000F1EF0 File Offset: 0x000F00F0
		// (set) Token: 0x06004BD6 RID: 19414 RVA: 0x000F1EF8 File Offset: 0x000F00F8
		public AssemblyHashAlgorithm HashAlgorithm
		{
			get
			{
				return this.hashalg;
			}
			set
			{
				this.hashalg = value;
			}
		}

		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x06004BD7 RID: 19415 RVA: 0x000F1F01 File Offset: 0x000F0101
		// (set) Token: 0x06004BD8 RID: 19416 RVA: 0x000F1F09 File Offset: 0x000F0109
		public StrongNameKeyPair KeyPair
		{
			get
			{
				return this.keypair;
			}
			set
			{
				this.keypair = value;
			}
		}

		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x06004BD9 RID: 19417 RVA: 0x000F1F12 File Offset: 0x000F0112
		// (set) Token: 0x06004BDA RID: 19418 RVA: 0x000F1F1C File Offset: 0x000F011C
		public Version Version
		{
			get
			{
				return this.version;
			}
			set
			{
				this.version = value;
				if (value == null)
				{
					this.major = (this.minor = (this.build = (this.revision = 0)));
					return;
				}
				this.major = value.Major;
				this.minor = value.Minor;
				this.build = value.Build;
				this.revision = value.Revision;
			}
		}

		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x06004BDB RID: 19419 RVA: 0x000F1F8C File Offset: 0x000F018C
		// (set) Token: 0x06004BDC RID: 19420 RVA: 0x000F1F94 File Offset: 0x000F0194
		public AssemblyVersionCompatibility VersionCompatibility
		{
			get
			{
				return this.versioncompat;
			}
			set
			{
				this.versioncompat = value;
			}
		}

		// Token: 0x06004BDD RID: 19421 RVA: 0x000F1FA0 File Offset: 0x000F01A0
		public override string ToString()
		{
			string fullName = this.FullName;
			if (fullName == null)
			{
				return base.ToString();
			}
			return fullName;
		}

		// Token: 0x06004BDE RID: 19422 RVA: 0x000F1FBF File Offset: 0x000F01BF
		public byte[] GetPublicKey()
		{
			return this.publicKey;
		}

		// Token: 0x06004BDF RID: 19423 RVA: 0x000F1FC8 File Offset: 0x000F01C8
		public byte[] GetPublicKeyToken()
		{
			if (this.keyToken != null)
			{
				return this.keyToken;
			}
			if (this.publicKey == null)
			{
				return null;
			}
			if (this.publicKey.Length == 0)
			{
				return EmptyArray<byte>.Value;
			}
			if (!this.IsPublicKeyValid)
			{
				throw new SecurityException("The public key is not valid.");
			}
			this.keyToken = this.ComputePublicKeyToken();
			return this.keyToken;
		}

		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x06004BE0 RID: 19424 RVA: 0x000F2024 File Offset: 0x000F0224
		private bool IsPublicKeyValid
		{
			get
			{
				if (this.publicKey.Length == 16)
				{
					int i = 0;
					int num = 0;
					while (i < this.publicKey.Length)
					{
						num += (int)this.publicKey[i++];
					}
					if (num == 4)
					{
						return true;
					}
				}
				byte b = this.publicKey[0];
				if (b != 0)
				{
					if (b == 6)
					{
						return CryptoConvert.TryImportCapiPublicKeyBlob(this.publicKey, 0);
					}
					if (b != 7)
					{
					}
				}
				else if (this.publicKey.Length > 12 && this.publicKey[12] == 6)
				{
					return CryptoConvert.TryImportCapiPublicKeyBlob(this.publicKey, 12);
				}
				return false;
			}
		}

		// Token: 0x06004BE1 RID: 19425 RVA: 0x000F20B0 File Offset: 0x000F02B0
		private byte[] InternalGetPublicKeyToken()
		{
			if (this.keyToken != null)
			{
				return this.keyToken;
			}
			if (this.publicKey == null)
			{
				return null;
			}
			if (this.publicKey.Length == 0)
			{
				return EmptyArray<byte>.Value;
			}
			if (!this.IsPublicKeyValid)
			{
				throw new SecurityException("The public key is not valid.");
			}
			return this.ComputePublicKeyToken();
		}

		// Token: 0x06004BE2 RID: 19426
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void get_public_token(byte* token, byte* pubkey, int len);

		// Token: 0x06004BE3 RID: 19427 RVA: 0x000F2100 File Offset: 0x000F0300
		private unsafe byte[] ComputePublicKeyToken()
		{
			byte[] array2;
			byte[] array = (array2 = new byte[8]);
			byte* ptr;
			if (array == null || array2.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array2[0];
			}
			byte[] array3;
			byte* ptr2;
			if ((array3 = this.publicKey) == null || array3.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &array3[0];
			}
			AssemblyName.get_public_token(ptr, ptr2, this.publicKey.Length);
			array3 = null;
			array2 = null;
			return array;
		}

		// Token: 0x06004BE4 RID: 19428 RVA: 0x000F215B File Offset: 0x000F035B
		public static bool ReferenceMatchesDefinition(AssemblyName reference, AssemblyName definition)
		{
			if (reference == null)
			{
				throw new ArgumentNullException("reference");
			}
			if (definition == null)
			{
				throw new ArgumentNullException("definition");
			}
			return string.Equals(reference.Name, definition.Name, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06004BE5 RID: 19429 RVA: 0x000F218B File Offset: 0x000F038B
		public void SetPublicKey(byte[] publicKey)
		{
			if (publicKey == null)
			{
				this.flags ^= AssemblyNameFlags.PublicKey;
			}
			else
			{
				this.flags |= AssemblyNameFlags.PublicKey;
			}
			this.publicKey = publicKey;
		}

		// Token: 0x06004BE6 RID: 19430 RVA: 0x000F21B5 File Offset: 0x000F03B5
		public void SetPublicKeyToken(byte[] publicKeyToken)
		{
			this.keyToken = publicKeyToken;
		}

		// Token: 0x06004BE7 RID: 19431 RVA: 0x000F21C0 File Offset: 0x000F03C0
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("_Name", this.name);
			info.AddValue("_PublicKey", this.publicKey);
			info.AddValue("_PublicKeyToken", this.keyToken);
			info.AddValue("_CultureInfo", (this.cultureinfo != null) ? this.cultureinfo.LCID : (-1));
			info.AddValue("_CodeBase", this.codebase);
			info.AddValue("_Version", this.Version);
			info.AddValue("_HashAlgorithm", this.hashalg);
			info.AddValue("_HashAlgorithmForControl", AssemblyHashAlgorithm.None);
			info.AddValue("_StrongNameKeyPair", this.keypair);
			info.AddValue("_VersionCompatibility", this.versioncompat);
			info.AddValue("_Flags", this.flags);
			info.AddValue("_HashForControl", null);
		}

		// Token: 0x06004BE8 RID: 19432 RVA: 0x000F22C4 File Offset: 0x000F04C4
		public object Clone()
		{
			return new AssemblyName
			{
				name = this.name,
				codebase = this.codebase,
				major = this.major,
				minor = this.minor,
				build = this.build,
				revision = this.revision,
				version = this.version,
				cultureinfo = this.cultureinfo,
				flags = this.flags,
				hashalg = this.hashalg,
				keypair = this.keypair,
				publicKey = this.publicKey,
				keyToken = this.keyToken,
				versioncompat = this.versioncompat,
				processor_architecture = this.processor_architecture
			};
		}

		// Token: 0x06004BE9 RID: 19433 RVA: 0x000F238A File Offset: 0x000F058A
		public void OnDeserialization(object sender)
		{
			this.Version = this.version;
		}

		// Token: 0x06004BEA RID: 19434 RVA: 0x000F2398 File Offset: 0x000F0598
		public unsafe static AssemblyName GetAssemblyName(string assemblyFile)
		{
			if (assemblyFile == null)
			{
				throw new ArgumentNullException("assemblyFile");
			}
			AssemblyName assemblyName = new AssemblyName();
			MonoAssemblyName monoAssemblyName;
			string text;
			Assembly.InternalGetAssemblyName(Path.GetFullPath(assemblyFile), out monoAssemblyName, out text);
			try
			{
				assemblyName.FillName(&monoAssemblyName, text, true, false, true, false);
			}
			finally
			{
				RuntimeMarshal.FreeAssemblyName(ref monoAssemblyName, false);
			}
			return assemblyName;
		}

		// Token: 0x06004BEB RID: 19435 RVA: 0x000174FB File Offset: 0x000156FB
		void _AssemblyName.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004BEC RID: 19436 RVA: 0x000174FB File Offset: 0x000156FB
		void _AssemblyName.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004BED RID: 19437 RVA: 0x000174FB File Offset: 0x000156FB
		void _AssemblyName.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004BEE RID: 19438 RVA: 0x000174FB File Offset: 0x000156FB
		void _AssemblyName.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x06004BEF RID: 19439 RVA: 0x000F23F4 File Offset: 0x000F05F4
		// (set) Token: 0x06004BF0 RID: 19440 RVA: 0x000F240B File Offset: 0x000F060B
		public string CultureName
		{
			get
			{
				if (this.cultureinfo != null)
				{
					return this.cultureinfo.Name;
				}
				return null;
			}
			set
			{
				if (value == null)
				{
					this.cultureinfo = null;
					return;
				}
				this.cultureinfo = new CultureInfo(value);
			}
		}

		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x06004BF1 RID: 19441 RVA: 0x000F2424 File Offset: 0x000F0624
		// (set) Token: 0x06004BF2 RID: 19442 RVA: 0x000F242C File Offset: 0x000F062C
		[ComVisible(false)]
		public AssemblyContentType ContentType
		{
			get
			{
				return this.contentType;
			}
			set
			{
				this.contentType = value;
			}
		}

		// Token: 0x06004BF3 RID: 19443
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern MonoAssemblyName* GetNativeName(IntPtr assembly_ptr);

		// Token: 0x06004BF4 RID: 19444 RVA: 0x000F2438 File Offset: 0x000F0638
		internal unsafe void FillName(MonoAssemblyName* native, string codeBase, bool addVersion, bool addPublickey, bool defaultToken, bool assemblyRef)
		{
			this.name = RuntimeMarshal.PtrToUtf8String(native->name);
			this.major = (int)native->major;
			this.minor = (int)native->minor;
			this.build = (int)native->build;
			this.revision = (int)native->revision;
			this.flags = (AssemblyNameFlags)native->flags;
			this.hashalg = (AssemblyHashAlgorithm)native->hash_alg;
			this.versioncompat = AssemblyVersionCompatibility.SameMachine;
			this.processor_architecture = (ProcessorArchitecture)native->arch;
			if (addVersion)
			{
				this.version = new Version(this.major, this.minor, this.build, this.revision);
			}
			this.codebase = codeBase;
			if (native->culture != IntPtr.Zero)
			{
				this.cultureinfo = CultureInfo.CreateCulture(RuntimeMarshal.PtrToUtf8String(native->culture), assemblyRef);
			}
			if (native->public_key != IntPtr.Zero)
			{
				this.publicKey = RuntimeMarshal.DecodeBlobArray(native->public_key);
				this.flags |= AssemblyNameFlags.PublicKey;
			}
			else if (addPublickey)
			{
				this.publicKey = EmptyArray<byte>.Value;
				this.flags |= AssemblyNameFlags.PublicKey;
			}
			if (native->public_key_token.FixedElementField != 0)
			{
				byte[] array = new byte[8];
				int i = 0;
				int num = 0;
				while (i < 8)
				{
					array[i] = (byte)(RuntimeMarshal.AsciHexDigitValue((int)(*((ref native->public_key_token.FixedElementField) + num++))) << 4);
					byte[] array2 = array;
					int num2 = i;
					array2[num2] |= (byte)RuntimeMarshal.AsciHexDigitValue((int)(*((ref native->public_key_token.FixedElementField) + num++)));
					i++;
				}
				this.keyToken = array;
				return;
			}
			if (defaultToken)
			{
				this.keyToken = EmptyArray<byte>.Value;
			}
		}

		// Token: 0x06004BF5 RID: 19445 RVA: 0x000F25D4 File Offset: 0x000F07D4
		internal unsafe static AssemblyName Create(Assembly assembly, bool fillCodebase)
		{
			AssemblyName assemblyName = new AssemblyName();
			MonoAssemblyName* nativeName = AssemblyName.GetNativeName(assembly.MonoAssembly);
			assemblyName.FillName(nativeName, fillCodebase ? assembly.CodeBase : null, true, true, true, false);
			return assemblyName;
		}

		// Token: 0x04002F93 RID: 12179
		private string name;

		// Token: 0x04002F94 RID: 12180
		private string codebase;

		// Token: 0x04002F95 RID: 12181
		private int major;

		// Token: 0x04002F96 RID: 12182
		private int minor;

		// Token: 0x04002F97 RID: 12183
		private int build;

		// Token: 0x04002F98 RID: 12184
		private int revision;

		// Token: 0x04002F99 RID: 12185
		private CultureInfo cultureinfo;

		// Token: 0x04002F9A RID: 12186
		private AssemblyNameFlags flags;

		// Token: 0x04002F9B RID: 12187
		private AssemblyHashAlgorithm hashalg;

		// Token: 0x04002F9C RID: 12188
		private StrongNameKeyPair keypair;

		// Token: 0x04002F9D RID: 12189
		private byte[] publicKey;

		// Token: 0x04002F9E RID: 12190
		private byte[] keyToken;

		// Token: 0x04002F9F RID: 12191
		private AssemblyVersionCompatibility versioncompat;

		// Token: 0x04002FA0 RID: 12192
		private Version version;

		// Token: 0x04002FA1 RID: 12193
		private ProcessorArchitecture processor_architecture;

		// Token: 0x04002FA2 RID: 12194
		private AssemblyContentType contentType;
	}
}
