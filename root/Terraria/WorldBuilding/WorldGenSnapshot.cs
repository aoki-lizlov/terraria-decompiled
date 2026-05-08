using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Terraria.Testing;
using Terraria.Utilities;

namespace Terraria.WorldBuilding
{
	// Token: 0x020000C1 RID: 193
	public class WorldGenSnapshot
	{
		// Token: 0x1700029F RID: 671
		// (get) Token: 0x060017C1 RID: 6081 RVA: 0x004DFCE7 File Offset: 0x004DDEE7
		// (set) Token: 0x060017C2 RID: 6082 RVA: 0x004DFCEF File Offset: 0x004DDEEF
		public WorldManifest Manifest
		{
			[CompilerGenerated]
			get
			{
				return this.<Manifest>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Manifest>k__BackingField = value;
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x060017C3 RID: 6083 RVA: 0x004DFCF8 File Offset: 0x004DDEF8
		// (set) Token: 0x060017C4 RID: 6084 RVA: 0x004DFD00 File Offset: 0x004DDF00
		private string Path
		{
			[CompilerGenerated]
			get
			{
				return this.<Path>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Path>k__BackingField = value;
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x060017C5 RID: 6085 RVA: 0x004DFD09 File Offset: 0x004DDF09
		// (set) Token: 0x060017C6 RID: 6086 RVA: 0x004DFD11 File Offset: 0x004DDF11
		private string GenVarsJson
		{
			[CompilerGenerated]
			get
			{
				return this.<GenVarsJson>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<GenVarsJson>k__BackingField = value;
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x060017C7 RID: 6087 RVA: 0x004DFD1A File Offset: 0x004DDF1A
		public List<GenPassResult> GenPassResults
		{
			get
			{
				return this.Manifest.GenPassResults;
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x060017C8 RID: 6088 RVA: 0x004DFD28 File Offset: 0x004DDF28
		public bool Outdated
		{
			get
			{
				if (!(this.Manifest.GitSHA != GitStatus.GitSHA) && !(this.Manifest.Version != Main.versionNumber))
				{
					return !this._matchingPasses.Zip(this.GenPassResults, (GenPass p, GenPassResult r) => p.Enabled == !r.Skipped).All((bool x) => x);
				}
				return true;
			}
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x004DFDBC File Offset: 0x004DDFBC
		public override string ToString()
		{
			GenPassResult genPassResult = this.GenPassResults.Last<GenPassResult>();
			return string.Format("Pass - {0}, rand - {1:X8}, hash - {2:X8}", genPassResult.Name, genPassResult.RandNext, genPassResult.Hash);
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x060017CA RID: 6090 RVA: 0x004DFDFB File Offset: 0x004DDFFB
		private static string PathForActiveWorld
		{
			get
			{
				return global::System.IO.Path.ChangeExtension(Main.ActiveWorldFileData.Path, null) + WorldGenSnapshot.SnapshotFolderSuffix;
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x060017CB RID: 6091 RVA: 0x004DFE17 File Offset: 0x004DE017
		public static long EstimatedDiskUsage
		{
			get
			{
				return WorldGenSnapshot._snapshotSizeCache.Values.Sum();
			}
		}

		// Token: 0x060017CC RID: 6092 RVA: 0x004DFE28 File Offset: 0x004DE028
		public static void DeleteAllForCurrentWorld()
		{
			if (Directory.Exists(WorldGenSnapshot.PathForActiveWorld))
			{
				try
				{
					Directory.Delete(WorldGenSnapshot.PathForActiveWorld, true);
				}
				catch (Exception)
				{
				}
			}
			WorldGenSnapshot._snapshotSizeCache.Clear();
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x004DFE6C File Offset: 0x004DE06C
		public static WorldGenSnapshot Create()
		{
			WorldGenSnapshot worldGenSnapshot = new WorldGenSnapshot
			{
				Manifest = WorldGen.Manifest.Clone(),
				GenVarsJson = WorldGenSnapshot.SnapshotGenVars.Serialize()
			};
			worldGenSnapshot._matchingPasses = WorldGenerator.CurrentController.Passes.GetRange(0, worldGenSnapshot.GenPassResults.Count);
			worldGenSnapshot.Path = global::System.IO.Path.Combine(WorldGenSnapshot.PathForActiveWorld, worldGenSnapshot + WorldGenSnapshot.Extension);
			if (!Directory.Exists(WorldGenSnapshot.PathForActiveWorld))
			{
				Directory.CreateDirectory(WorldGenSnapshot.PathForActiveWorld);
			}
			TileSnapshot.Create(worldGenSnapshot);
			using (BinaryWriter binaryWriter = new BinaryWriter(File.Create(worldGenSnapshot.Path)))
			{
				binaryWriter.Write(worldGenSnapshot.Manifest.Serialize());
				binaryWriter.Write(worldGenSnapshot.GenVarsJson);
				worldGenSnapshot._dataOffset = (int)binaryWriter.BaseStream.Position;
				TileSnapshot.Save(binaryWriter);
				WorldGenSnapshot._snapshotSizeCache[worldGenSnapshot.Path] = binaryWriter.BaseStream.Length;
			}
			return worldGenSnapshot;
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x004DFF74 File Offset: 0x004DE174
		public static void Delete(WorldGenSnapshot snap)
		{
			try
			{
				File.Delete(snap.Path);
			}
			catch (Exception)
			{
			}
			WorldGenSnapshot._snapshotSizeCache.Remove(snap.Path);
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x004DFFB4 File Offset: 0x004DE1B4
		public void ResaveForCurrentVersion()
		{
			this.Manifest = WorldGen.Manifest.Clone();
			this.GenVarsJson = WorldGenSnapshot.SnapshotGenVars.Serialize();
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (FileStream fileStream = File.OpenRead(this.Path))
				{
					fileStream.Seek((long)this._dataOffset, SeekOrigin.Current);
					fileStream.CopyTo(memoryStream);
				}
				memoryStream.Position = 0L;
				using (BinaryWriter binaryWriter = new BinaryWriter(File.Create(this.Path)))
				{
					binaryWriter.Write(this.Manifest.Serialize());
					binaryWriter.Write(this.GenVarsJson);
					this._dataOffset = (int)binaryWriter.BaseStream.Position;
					memoryStream.CopyTo(binaryWriter.BaseStream);
					WorldGenSnapshot._snapshotSizeCache[this.Path] = binaryWriter.BaseStream.Length;
				}
			}
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x004E00C0 File Offset: 0x004DE2C0
		public static Dictionary<GenPass, WorldGenSnapshot> LoadSnapshots(WorldManifest worldManifest, List<GenPass> passes)
		{
			WorldGenSnapshot._snapshotSizeCache.Clear();
			Dictionary<GenPass, WorldGenSnapshot> dictionary = new Dictionary<GenPass, WorldGenSnapshot>();
			Task.Factory.StartNew(delegate
			{
				WorldGenSnapshot.DeleteSnapshotsForOtherWorlds(WorldGenSnapshot.PathForActiveWorld);
			});
			if (!Directory.Exists(WorldGenSnapshot.PathForActiveWorld))
			{
				return dictionary;
			}
			if (worldManifest == null)
			{
				Trace.WriteLine("Deleting old snapshots because a new world is being created (/regen was not used)");
				WorldGenSnapshot.DeleteAllForCurrentWorld();
				return dictionary;
			}
			using (IEnumerator<string> enumerator = Directory.EnumerateFiles(WorldGenSnapshot.PathForActiveWorld, "*" + WorldGenSnapshot.Extension).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					WorldGenSnapshot worldGenSnapshot;
					GenPass genPass;
					if (WorldGenSnapshot.ReadSnapshot(enumerator.Current, out worldGenSnapshot) && worldGenSnapshot.IsValidHistoryOf(worldManifest) && WorldGenSnapshot.FindMatchingGenPass(worldGenSnapshot.Manifest, passes, out genPass))
					{
						worldGenSnapshot._matchingPasses = passes.GetRange(0, worldGenSnapshot.GenPassResults.Count);
						dictionary[genPass] = worldGenSnapshot;
					}
					else
					{
						Trace.WriteLine(string.Format("Deleting snapshot ({0}) due to manifest mismatch. A change to the codebase has probably invalidated it.", worldGenSnapshot));
						WorldGenSnapshot.Delete(worldGenSnapshot);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x004E01D0 File Offset: 0x004DE3D0
		private static void DeleteSnapshotsForOtherWorlds(string snapshotPathForActiveWorld)
		{
			string directoryName = global::System.IO.Path.GetDirectoryName(snapshotPathForActiveWorld);
			string fileName = global::System.IO.Path.GetFileName(snapshotPathForActiveWorld);
			foreach (string text in Directory.EnumerateDirectories(directoryName))
			{
				if (text.EndsWith(WorldGenSnapshot.SnapshotFolderSuffix) && !(global::System.IO.Path.GetFileName(text) == fileName))
				{
					Trace.WriteLine("Deleting snapshot directory: " + text);
					try
					{
						Directory.Delete(text, true);
					}
					catch (Exception)
					{
					}
				}
			}
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x004E0268 File Offset: 0x004DE468
		private static bool FindMatchingGenPass(WorldManifest manifest, List<GenPass> passes, out GenPass pass)
		{
			pass = null;
			List<GenPassResult> genPassResults = manifest.GenPassResults;
			if (genPassResults.Count <= passes.Count)
			{
				if (genPassResults.Zip(passes, (GenPassResult r, GenPass p) => r.Name == p.Name).All((bool x) => x))
				{
					pass = passes[genPassResults.Count - 1];
					return true;
				}
			}
			return false;
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x004E02EC File Offset: 0x004DE4EC
		public bool IsValidHistoryOf(WorldManifest target)
		{
			return WorldGenSnapshot.StartsWith(target.GenPassResults, this.Manifest.GenPassResults);
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x004E0304 File Offset: 0x004DE504
		private static bool StartsWith(List<GenPassResult> list, List<GenPassResult> prefix)
		{
			if (prefix.Count <= list.Count)
			{
				return list.Zip(prefix, (GenPassResult a, GenPassResult b) => a.Matches(b)).All((bool x) => x);
			}
			return false;
		}

		// Token: 0x060017D5 RID: 6101 RVA: 0x004E036C File Offset: 0x004DE56C
		private static bool ReadSnapshot(string path, out WorldGenSnapshot snap)
		{
			bool flag;
			try
			{
				using (BinaryReader binaryReader = new BinaryReader(File.OpenRead(path)))
				{
					snap = new WorldGenSnapshot
					{
						Path = path,
						Manifest = WorldManifest.Deserialize(binaryReader.ReadString()),
						GenVarsJson = binaryReader.ReadString(),
						_dataOffset = (int)binaryReader.BaseStream.Position
					};
					WorldGenSnapshot._snapshotSizeCache[path] = binaryReader.BaseStream.Length;
					flag = true;
				}
			}
			catch (Exception ex)
			{
				Trace.WriteLine(string.Concat(new object[] { "Failed to read snapshot: ", path, ", ", ex }));
				snap = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x060017D6 RID: 6102 RVA: 0x004E0434 File Offset: 0x004DE634
		public void Load()
		{
			if (TileSnapshot.Context == this)
			{
				return;
			}
			using (BinaryReader binaryReader = new BinaryReader(File.OpenRead(this.Path)))
			{
				binaryReader.BaseStream.Seek((long)this._dataOffset, SeekOrigin.Current);
				TileSnapshot.Load(binaryReader, this);
			}
		}

		// Token: 0x060017D7 RID: 6103 RVA: 0x004E0494 File Offset: 0x004DE694
		public void Restore()
		{
			this.Load();
			WorldGen.RestoreTemporaryStateChanges();
			WorldGen.Reset();
			WorldGen.Manifest = this.Manifest.Clone();
			WorldGenSnapshot.SnapshotGenVars.DeserializeAndApply(this.GenVarsJson);
			TileSnapshot.Restore();
			NPC[] npc = Main.npc;
			for (int i = 0; i < npc.Length; i++)
			{
				npc[i].active = false;
			}
			Main.NewText("Restored " + this, byte.MaxValue, byte.MaxValue, 0);
		}

		// Token: 0x060017D8 RID: 6104 RVA: 0x0000357B File Offset: 0x0000177B
		public WorldGenSnapshot()
		{
		}

		// Token: 0x060017D9 RID: 6105 RVA: 0x004E0509 File Offset: 0x004DE709
		// Note: this type is marked as 'beforefieldinit'.
		static WorldGenSnapshot()
		{
		}

		// Token: 0x0400129D RID: 4765
		[CompilerGenerated]
		private WorldManifest <Manifest>k__BackingField;

		// Token: 0x0400129E RID: 4766
		[CompilerGenerated]
		private string <Path>k__BackingField;

		// Token: 0x0400129F RID: 4767
		[CompilerGenerated]
		private string <GenVarsJson>k__BackingField;

		// Token: 0x040012A0 RID: 4768
		private int _dataOffset;

		// Token: 0x040012A1 RID: 4769
		private List<GenPass> _matchingPasses;

		// Token: 0x040012A2 RID: 4770
		private static string SnapshotFolderSuffix = "_gensnapshots";

		// Token: 0x040012A3 RID: 4771
		private static string Extension = ".gensnapshot";

		// Token: 0x040012A4 RID: 4772
		private static IDictionary<string, long> _snapshotSizeCache = new Dictionary<string, long>();

		// Token: 0x020006ED RID: 1773
		[JsonConverter(typeof(WorldGenSnapshot.SnapshotGenVars))]
		private class SnapshotGenVars : JsonConverter
		{
			// Token: 0x06003F8E RID: 16270 RVA: 0x0069ADAC File Offset: 0x00698FAC
			public static string Serialize()
			{
				return JsonConvert.SerializeObject(new WorldGenSnapshot.SnapshotGenVars(), WorldGenSnapshot.SnapshotGenVars.SerializerSettings);
			}

			// Token: 0x06003F8F RID: 16271 RVA: 0x0069ADBD File Offset: 0x00698FBD
			public static void DeserializeAndApply(string json)
			{
				JsonConvert.DeserializeObject<WorldGenSnapshot.SnapshotGenVars>(json, WorldGenSnapshot.SnapshotGenVars.SerializerSettings);
			}

			// Token: 0x06003F90 RID: 16272 RVA: 0x0069ADCB File Offset: 0x00698FCB
			public override bool CanConvert(Type objectType)
			{
				return objectType == typeof(WorldGenSnapshot.SnapshotGenVars);
			}

			// Token: 0x06003F91 RID: 16273 RVA: 0x0069ADE0 File Offset: 0x00698FE0
			public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
			{
				if (reader.TokenType != 1)
				{
					throw new JsonReaderException();
				}
				while (reader.Read() && reader.TokenType != 13)
				{
					if (reader.TokenType != 4)
					{
						throw new JsonReaderException("Expected PropertyName");
					}
					string text = (string)reader.Value;
					if (!reader.Read())
					{
						throw new JsonReaderException();
					}
					MemberInfo memberInfo;
					if (WorldGenSnapshot.SnapshotGenVars.fieldsAndProperties.TryGetValue(text, out memberInfo))
					{
						this.SetValue(memberInfo, serializer.Deserialize(reader, this.GetType(memberInfo)));
					}
					else
					{
						reader.Skip();
					}
				}
				return null;
			}

			// Token: 0x06003F92 RID: 16274 RVA: 0x0069AE6C File Offset: 0x0069906C
			public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
			{
				writer.WriteStartObject();
				foreach (MemberInfo memberInfo in WorldGenSnapshot.SnapshotGenVars.fieldsAndProperties.Values)
				{
					writer.WritePropertyName(memberInfo.Name);
					serializer.Serialize(writer, this.GetValue(memberInfo), this.GetType(memberInfo));
				}
				writer.WriteEndObject();
			}

			// Token: 0x06003F93 RID: 16275 RVA: 0x0069AEEC File Offset: 0x006990EC
			private Type GetType(MemberInfo member)
			{
				if (member is PropertyInfo)
				{
					return ((PropertyInfo)member).PropertyType;
				}
				if (member is FieldInfo)
				{
					return ((FieldInfo)member).FieldType;
				}
				throw new ArgumentException(member.GetType().ToString());
			}

			// Token: 0x06003F94 RID: 16276 RVA: 0x0069AF28 File Offset: 0x00699128
			private object GetValue(MemberInfo member)
			{
				if (member is PropertyInfo)
				{
					return ((PropertyInfo)member).GetGetMethod().Invoke(null, null);
				}
				if (member is FieldInfo)
				{
					return ((FieldInfo)member).GetValue(null);
				}
				throw new ArgumentException(member.GetType().ToString());
			}

			// Token: 0x06003F95 RID: 16277 RVA: 0x0069AF78 File Offset: 0x00699178
			private void SetValue(MemberInfo member, object v)
			{
				if (member is PropertyInfo)
				{
					((PropertyInfo)member).GetSetMethod().Invoke(null, new object[] { v });
					return;
				}
				if (member is FieldInfo)
				{
					((FieldInfo)member).SetValue(null, v);
					return;
				}
				throw new ArgumentException(member.GetType().ToString());
			}

			// Token: 0x06003F96 RID: 16278 RVA: 0x0069AFD0 File Offset: 0x006991D0
			public SnapshotGenVars()
			{
			}

			// Token: 0x06003F97 RID: 16279 RVA: 0x0069AFD8 File Offset: 0x006991D8
			// Note: this type is marked as 'beforefieldinit'.
			static SnapshotGenVars()
			{
			}

			// Token: 0x040067FC RID: 26620
			public static JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
			{
				ContractResolver = new EasyDeserializationJsonContractResolver(),
				PreserveReferencesHandling = 1,
				ReferenceLoopHandling = 2,
				TypeNameHandling = 4
			};

			// Token: 0x040067FD RID: 26621
			private static Dictionary<string, MemberInfo> fieldsAndProperties = (from m in typeof(GenVars).GetFields(BindingFlags.Static | BindingFlags.Public).Concat(typeof(GenVars).GetProperties(BindingFlags.Static | BindingFlags.Public))
				where !(m is PropertyInfo) || ((PropertyInfo)m).CanWrite
				where !(m is FieldInfo) || !((FieldInfo)m).IsInitOnly
				where !m.GetCustomAttributes(typeof(JsonIgnoreAttribute), true).Any<object>()
				select m).ToDictionary((MemberInfo m) => m.Name);

			// Token: 0x02000A4E RID: 2638
			[CompilerGenerated]
			[Serializable]
			private sealed class <>c
			{
				// Token: 0x06004AC8 RID: 19144 RVA: 0x006D4C76 File Offset: 0x006D2E76
				// Note: this type is marked as 'beforefieldinit'.
				static <>c()
				{
				}

				// Token: 0x06004AC9 RID: 19145 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c()
				{
				}

				// Token: 0x06004ACA RID: 19146 RVA: 0x006D4C82 File Offset: 0x006D2E82
				internal bool <.cctor>b__11_0(MemberInfo m)
				{
					return !(m is PropertyInfo) || ((PropertyInfo)m).CanWrite;
				}

				// Token: 0x06004ACB RID: 19147 RVA: 0x006D4C99 File Offset: 0x006D2E99
				internal bool <.cctor>b__11_1(MemberInfo m)
				{
					return !(m is FieldInfo) || !((FieldInfo)m).IsInitOnly;
				}

				// Token: 0x06004ACC RID: 19148 RVA: 0x006D4CB3 File Offset: 0x006D2EB3
				internal bool <.cctor>b__11_2(MemberInfo m)
				{
					return !m.GetCustomAttributes(typeof(JsonIgnoreAttribute), true).Any<object>();
				}

				// Token: 0x06004ACD RID: 19149 RVA: 0x006D4CCE File Offset: 0x006D2ECE
				internal string <.cctor>b__11_3(MemberInfo m)
				{
					return m.Name;
				}

				// Token: 0x0400772B RID: 30507
				public static readonly WorldGenSnapshot.SnapshotGenVars.<>c <>9 = new WorldGenSnapshot.SnapshotGenVars.<>c();
			}
		}

		// Token: 0x020006EE RID: 1774
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06003F98 RID: 16280 RVA: 0x0069B08F File Offset: 0x0069928F
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06003F99 RID: 16281 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x06003F9A RID: 16282 RVA: 0x0069B09B File Offset: 0x0069929B
			internal bool <get_Outdated>b__17_0(GenPass p, GenPassResult r)
			{
				return p.Enabled == !r.Skipped;
			}

			// Token: 0x06003F9B RID: 16283 RVA: 0x001FC6F1 File Offset: 0x001FA8F1
			internal bool <get_Outdated>b__17_1(bool x)
			{
				return x;
			}

			// Token: 0x06003F9C RID: 16284 RVA: 0x0069B0AE File Offset: 0x006992AE
			internal void <LoadSnapshots>b__30_0()
			{
				WorldGenSnapshot.DeleteSnapshotsForOtherWorlds(WorldGenSnapshot.PathForActiveWorld);
			}

			// Token: 0x06003F9D RID: 16285 RVA: 0x0069B0BA File Offset: 0x006992BA
			internal bool <FindMatchingGenPass>b__32_0(GenPassResult r, GenPass p)
			{
				return r.Name == p.Name;
			}

			// Token: 0x06003F9E RID: 16286 RVA: 0x001FC6F1 File Offset: 0x001FA8F1
			internal bool <FindMatchingGenPass>b__32_1(bool x)
			{
				return x;
			}

			// Token: 0x06003F9F RID: 16287 RVA: 0x0069B0CD File Offset: 0x006992CD
			internal bool <StartsWith>b__34_0(GenPassResult a, GenPassResult b)
			{
				return a.Matches(b);
			}

			// Token: 0x06003FA0 RID: 16288 RVA: 0x001FC6F1 File Offset: 0x001FA8F1
			internal bool <StartsWith>b__34_1(bool x)
			{
				return x;
			}

			// Token: 0x040067FE RID: 26622
			public static readonly WorldGenSnapshot.<>c <>9 = new WorldGenSnapshot.<>c();

			// Token: 0x040067FF RID: 26623
			public static Func<GenPass, GenPassResult, bool> <>9__17_0;

			// Token: 0x04006800 RID: 26624
			public static Func<bool, bool> <>9__17_1;

			// Token: 0x04006801 RID: 26625
			public static Action <>9__30_0;

			// Token: 0x04006802 RID: 26626
			public static Func<GenPassResult, GenPass, bool> <>9__32_0;

			// Token: 0x04006803 RID: 26627
			public static Func<bool, bool> <>9__32_1;

			// Token: 0x04006804 RID: 26628
			public static Func<GenPassResult, GenPassResult, bool> <>9__34_0;

			// Token: 0x04006805 RID: 26629
			public static Func<bool, bool> <>9__34_1;
		}
	}
}
