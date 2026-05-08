using System;
using System.Buffers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;
using System.Text;

namespace System
{
	// Token: 0x0200015E RID: 350
	[TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public sealed class TimeZoneInfo : IEquatable<TimeZoneInfo>, ISerializable, IDeserializationCallback
	{
		// Token: 0x06000F5A RID: 3930 RVA: 0x0003E144 File Offset: 0x0003C344
		private TimeZoneInfo(byte[] data, string id, bool dstDisabled)
		{
			TimeZoneInfo.TZifHead tzifHead;
			DateTime[] array;
			byte[] array2;
			TimeZoneInfo.TZifType[] array3;
			string text;
			bool[] array4;
			bool[] array5;
			string text2;
			TimeZoneInfo.TZif_ParseRaw(data, out tzifHead, out array, out array2, out array3, out text, out array4, out array5, out text2);
			this._id = id;
			this._displayName = "Local";
			this._baseUtcOffset = TimeSpan.Zero;
			DateTime utcNow = DateTime.UtcNow;
			int num = 0;
			while (num < array.Length && array[num] <= utcNow)
			{
				int num2 = (int)array2[num];
				if (!array3[num2].IsDst)
				{
					this._baseUtcOffset = array3[num2].UtcOffset;
					this._standardDisplayName = TimeZoneInfo.TZif_GetZoneAbbreviation(text, (int)array3[num2].AbbreviationIndex);
				}
				else
				{
					this._daylightDisplayName = TimeZoneInfo.TZif_GetZoneAbbreviation(text, (int)array3[num2].AbbreviationIndex);
				}
				num++;
			}
			if (array.Length == 0)
			{
				for (int i = 0; i < array3.Length; i++)
				{
					if (!array3[i].IsDst)
					{
						this._baseUtcOffset = array3[i].UtcOffset;
						this._standardDisplayName = TimeZoneInfo.TZif_GetZoneAbbreviation(text, (int)array3[i].AbbreviationIndex);
					}
					else
					{
						this._daylightDisplayName = TimeZoneInfo.TZif_GetZoneAbbreviation(text, (int)array3[i].AbbreviationIndex);
					}
				}
			}
			this._displayName = this._standardDisplayName;
			if (this._baseUtcOffset.Ticks % 600000000L != 0L)
			{
				this._baseUtcOffset = new TimeSpan(this._baseUtcOffset.Hours, this._baseUtcOffset.Minutes, 0);
			}
			if (!dstDisabled)
			{
				TimeZoneInfo.TZif_GenerateAdjustmentRules(out this._adjustmentRules, this._baseUtcOffset, array, array2, array3, array4, array5, text2);
			}
			TimeZoneInfo.ValidateTimeZoneInfo(this._id, this._baseUtcOffset, this._adjustmentRules, out this._supportsDaylightSavingTime);
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x0003E2FC File Offset: 0x0003C4FC
		public TimeZoneInfo.AdjustmentRule[] GetAdjustmentRules()
		{
			if (this._adjustmentRules == null)
			{
				return Array.Empty<TimeZoneInfo.AdjustmentRule>();
			}
			TimeZoneInfo.AdjustmentRule[] array = new TimeZoneInfo.AdjustmentRule[this._adjustmentRules.Length];
			for (int i = 0; i < this._adjustmentRules.Length; i++)
			{
				TimeZoneInfo.AdjustmentRule adjustmentRule = this._adjustmentRules[i];
				DateTime dateTime = ((adjustmentRule.DateStart.Kind == DateTimeKind.Utc) ? new DateTime(adjustmentRule.DateStart.Ticks + this._baseUtcOffset.Ticks, DateTimeKind.Unspecified) : adjustmentRule.DateStart);
				DateTime dateTime2 = ((adjustmentRule.DateEnd.Kind == DateTimeKind.Utc) ? new DateTime(adjustmentRule.DateEnd.Ticks + this._baseUtcOffset.Ticks + adjustmentRule.DaylightDelta.Ticks, DateTimeKind.Unspecified) : adjustmentRule.DateEnd);
				TimeZoneInfo.TransitionTime transitionTime = TimeZoneInfo.TransitionTime.CreateFixedDateRule(new DateTime(1, 1, 1, dateTime.Hour, dateTime.Minute, dateTime.Second), dateTime.Month, dateTime.Day);
				TimeZoneInfo.TransitionTime transitionTime2 = TimeZoneInfo.TransitionTime.CreateFixedDateRule(new DateTime(1, 1, 1, dateTime2.Hour, dateTime2.Minute, dateTime2.Second), dateTime2.Month, dateTime2.Day);
				array[i] = TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(dateTime.Date, dateTime2.Date, adjustmentRule.DaylightDelta, transitionTime, transitionTime2);
			}
			return array;
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x0003E458 File Offset: 0x0003C658
		private static void PopulateAllSystemTimeZones(TimeZoneInfo.CachedData cachedData)
		{
			foreach (string text in TimeZoneInfo.GetTimeZoneIds(TimeZoneInfo.GetTimeZoneDirectory()))
			{
				TimeZoneInfo timeZoneInfo;
				Exception ex;
				TimeZoneInfo.TryGetTimeZone(text, false, out timeZoneInfo, out ex, cachedData, true);
			}
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x0003E4B4 File Offset: 0x0003C6B4
		private static TimeZoneInfo GetLocalTimeZone(TimeZoneInfo.CachedData cachedData)
		{
			return TimeZoneInfo.GetLocalTimeZoneFromTzFile();
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x0003E4BC File Offset: 0x0003C6BC
		private static TimeZoneInfo.TimeZoneInfoResult TryGetTimeZoneFromLocalMachine(string id, out TimeZoneInfo value, out Exception e)
		{
			value = null;
			e = null;
			string text = Path.Combine(TimeZoneInfo.GetTimeZoneDirectory(), id);
			byte[] array;
			try
			{
				array = File.ReadAllBytes(text);
			}
			catch (UnauthorizedAccessException ex)
			{
				e = ex;
				return TimeZoneInfo.TimeZoneInfoResult.SecurityException;
			}
			catch (FileNotFoundException ex2)
			{
				e = ex2;
				return TimeZoneInfo.TimeZoneInfoResult.TimeZoneNotFoundException;
			}
			catch (DirectoryNotFoundException ex3)
			{
				e = ex3;
				return TimeZoneInfo.TimeZoneInfoResult.TimeZoneNotFoundException;
			}
			catch (IOException ex4)
			{
				e = new InvalidTimeZoneException(SR.Format("The time zone ID '{0}' was found on the local computer, but the file at '{1}' was corrupt.", id, text), ex4);
				return TimeZoneInfo.TimeZoneInfoResult.InvalidTimeZoneException;
			}
			value = TimeZoneInfo.GetTimeZoneFromTzData(array, id);
			if (value == null)
			{
				e = new InvalidTimeZoneException(SR.Format("The time zone ID '{0}' was found on the local computer, but the file at '{1}' was corrupt.", id, text));
				return TimeZoneInfo.TimeZoneInfoResult.InvalidTimeZoneException;
			}
			return TimeZoneInfo.TimeZoneInfoResult.Success;
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x0003E574 File Offset: 0x0003C774
		private static List<string> GetTimeZoneIds(string timeZoneDirectory)
		{
			List<string> list = new List<string>();
			try
			{
				using (StreamReader streamReader = new StreamReader(Path.Combine(timeZoneDirectory, "zone.tab"), Encoding.UTF8))
				{
					string text;
					while ((text = streamReader.ReadLine()) != null)
					{
						if (!string.IsNullOrEmpty(text) && text[0] != '#')
						{
							int num = text.IndexOf('\t');
							if (num != -1)
							{
								int num2 = text.IndexOf('\t', num + 1);
								if (num2 != -1)
								{
									int num3 = num2 + 1;
									int num4 = text.IndexOf('\t', num3);
									string text2;
									if (num4 != -1)
									{
										int num5 = num4 - num3;
										text2 = text.Substring(num3, num5);
									}
									else
									{
										text2 = text.Substring(num3);
									}
									if (!string.IsNullOrEmpty(text2))
									{
										list.Add(text2);
									}
								}
							}
						}
					}
				}
			}
			catch (IOException)
			{
			}
			catch (UnauthorizedAccessException)
			{
			}
			return list;
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x0003E664 File Offset: 0x0003C864
		private static bool TryGetLocalTzFile(out byte[] rawData, out string id)
		{
			rawData = null;
			id = null;
			string tzEnvironmentVariable = TimeZoneInfo.GetTzEnvironmentVariable();
			if (tzEnvironmentVariable == null)
			{
				return TimeZoneInfo.TryLoadTzFile("/etc/localtime", ref rawData, ref id) || TimeZoneInfo.TryLoadTzFile(Path.Combine(TimeZoneInfo.GetTimeZoneDirectory(), "localtime"), ref rawData, ref id);
			}
			if (tzEnvironmentVariable.Length == 0)
			{
				return false;
			}
			string text;
			if (tzEnvironmentVariable[0] != '/')
			{
				id = tzEnvironmentVariable;
				text = Path.Combine(TimeZoneInfo.GetTimeZoneDirectory(), tzEnvironmentVariable);
			}
			else
			{
				text = tzEnvironmentVariable;
			}
			return TimeZoneInfo.TryLoadTzFile(text, ref rawData, ref id);
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x0003E6D8 File Offset: 0x0003C8D8
		private static string GetTzEnvironmentVariable()
		{
			string text = Environment.GetEnvironmentVariable("TZ");
			if (!string.IsNullOrEmpty(text) && text[0] == ':')
			{
				text = text.Substring(1);
			}
			return text;
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x0003E70C File Offset: 0x0003C90C
		private static bool TryLoadTzFile(string tzFilePath, ref byte[] rawData, ref string id)
		{
			if (File.Exists(tzFilePath))
			{
				try
				{
					rawData = File.ReadAllBytes(tzFilePath);
					if (string.IsNullOrEmpty(id))
					{
						id = TimeZoneInfo.FindTimeZoneIdUsingReadLink(tzFilePath);
						if (string.IsNullOrEmpty(id))
						{
							id = TimeZoneInfo.FindTimeZoneId(rawData);
						}
					}
					return true;
				}
				catch (IOException)
				{
				}
				catch (SecurityException)
				{
				}
				catch (UnauthorizedAccessException)
				{
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x0003E784 File Offset: 0x0003C984
		private static string FindTimeZoneIdUsingReadLink(string tzFilePath)
		{
			string text = null;
			string text2 = Interop.Sys.ReadLink(tzFilePath);
			if (text2 != null)
			{
				text2 = Path.Combine(tzFilePath, text2);
				string timeZoneDirectory = TimeZoneInfo.GetTimeZoneDirectory();
				if (text2.StartsWith(timeZoneDirectory, StringComparison.Ordinal))
				{
					text = text2.Substring(timeZoneDirectory.Length);
				}
			}
			return text;
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x0003E7C4 File Offset: 0x0003C9C4
		private unsafe static string GetDirectoryEntryFullPath(ref Interop.Sys.DirectoryEntry dirent, string currentPath)
		{
			Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)512], 256);
			ReadOnlySpan<char> name = dirent.GetName(span);
			if ((name.Length == 1 && *name[0] == 46) || (name.Length == 2 && *name[0] == 46 && *name[1] == 46))
			{
				return null;
			}
			return Path.Join(currentPath.AsSpan(), name);
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x0003E838 File Offset: 0x0003CA38
		private unsafe static void EnumerateFilesRecursively(string path, Predicate<string> condition)
		{
			List<string> list = null;
			int readDirRBufferSize = Interop.Sys.GetReadDirRBufferSize();
			byte[] array = null;
			try
			{
				array = ArrayPool<byte>.Shared.Rent(readDirRBufferSize);
				string text = path;
				try
				{
					byte[] array2;
					byte* ptr;
					if ((array2 = array) == null || array2.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array2[0];
					}
					for (;;)
					{
						IntPtr intPtr = Interop.Sys.OpenDir(text);
						if (intPtr == IntPtr.Zero)
						{
							break;
						}
						try
						{
							Interop.Sys.DirectoryEntry directoryEntry;
							while (Interop.Sys.ReadDirR(intPtr, ptr, readDirRBufferSize, out directoryEntry) == 0)
							{
								string directoryEntryFullPath = TimeZoneInfo.GetDirectoryEntryFullPath(ref directoryEntry, text);
								if (directoryEntryFullPath != null)
								{
									Interop.Sys.FileStatus fileStatus;
									bool flag = directoryEntry.InodeType == Interop.Sys.NodeType.DT_DIR || ((directoryEntry.InodeType == Interop.Sys.NodeType.DT_LNK || directoryEntry.InodeType == Interop.Sys.NodeType.DT_UNKNOWN) && Interop.Sys.Stat(directoryEntryFullPath, out fileStatus) >= 0 && (fileStatus.Mode & 61440) == 16384);
									if (flag)
									{
										if (list == null)
										{
											list = new List<string>();
										}
										list.Add(directoryEntryFullPath);
									}
									else if (condition(directoryEntryFullPath))
									{
										return;
									}
								}
							}
						}
						finally
						{
							if (intPtr != IntPtr.Zero)
							{
								Interop.Sys.CloseDir(intPtr);
							}
						}
						if (list == null)
						{
							goto IL_0137;
						}
						if (list.Count == 0)
						{
							goto Block_9;
						}
						text = list[list.Count - 1];
						list.RemoveAt(list.Count - 1);
					}
					throw Interop.GetExceptionForIoErrno(Interop.Sys.GetLastErrorInfo(), text, true);
					Block_9:
					IL_0137:;
				}
				finally
				{
					byte[] array2 = null;
				}
			}
			finally
			{
				if (array != null)
				{
					ArrayPool<byte>.Shared.Return(array, false);
				}
			}
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x0003E9E0 File Offset: 0x0003CBE0
		private static string FindTimeZoneId(byte[] rawData)
		{
			string id = "Local";
			string timeZoneDirectory = TimeZoneInfo.GetTimeZoneDirectory();
			string localtimeFilePath = Path.Combine(timeZoneDirectory, "localtime");
			string posixrulesFilePath = Path.Combine(timeZoneDirectory, "posixrules");
			byte[] buffer = new byte[rawData.Length];
			try
			{
				TimeZoneInfo.EnumerateFilesRecursively(timeZoneDirectory, delegate(string filePath)
				{
					if (!string.Equals(filePath, localtimeFilePath, StringComparison.OrdinalIgnoreCase) && !string.Equals(filePath, posixrulesFilePath, StringComparison.OrdinalIgnoreCase) && TimeZoneInfo.CompareTimeZoneFile(filePath, buffer, rawData))
					{
						id = filePath;
						if (id.StartsWith(timeZoneDirectory, StringComparison.Ordinal))
						{
							id = id.Substring(timeZoneDirectory.Length);
						}
						return true;
					}
					return false;
				});
			}
			catch (IOException)
			{
			}
			catch (SecurityException)
			{
			}
			catch (UnauthorizedAccessException)
			{
			}
			return id;
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x0003EAA0 File Offset: 0x0003CCA0
		private static bool CompareTimeZoneFile(string filePath, byte[] buffer, byte[] rawData)
		{
			try
			{
				using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 1))
				{
					if (fileStream.Length == (long)rawData.Length)
					{
						int i = 0;
						int num;
						for (int j = rawData.Length; j > 0; j -= num)
						{
							num = fileStream.Read(buffer, i, j);
							if (num == 0)
							{
								throw Error.GetEndOfFile();
							}
							int num2 = i + num;
							while (i < num2)
							{
								if (buffer[i] != rawData[i])
								{
									return false;
								}
								i++;
							}
						}
						return true;
					}
				}
			}
			catch (IOException)
			{
			}
			catch (SecurityException)
			{
			}
			catch (UnauthorizedAccessException)
			{
			}
			return false;
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x0003EB58 File Offset: 0x0003CD58
		private static TimeZoneInfo GetLocalTimeZoneFromTzFile()
		{
			byte[] array;
			string text;
			if (TimeZoneInfo.TryGetLocalTzFile(out array, out text))
			{
				TimeZoneInfo timeZoneFromTzData = TimeZoneInfo.GetTimeZoneFromTzData(array, text);
				if (timeZoneFromTzData != null)
				{
					return timeZoneFromTzData;
				}
			}
			return TimeZoneInfo.Utc;
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x0003EB84 File Offset: 0x0003CD84
		private static TimeZoneInfo GetTimeZoneFromTzData(byte[] rawData, string id)
		{
			if (rawData != null)
			{
				try
				{
					return new TimeZoneInfo(rawData, id, false);
				}
				catch (ArgumentException)
				{
				}
				catch (InvalidTimeZoneException)
				{
				}
				try
				{
					return new TimeZoneInfo(rawData, id, true);
				}
				catch (ArgumentException)
				{
				}
				catch (InvalidTimeZoneException)
				{
				}
			}
			return null;
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x0003EBF0 File Offset: 0x0003CDF0
		private static string GetTimeZoneDirectory()
		{
			string text = Environment.GetEnvironmentVariable("TZDIR");
			if (text == null)
			{
				text = "/usr/share/zoneinfo/";
			}
			else if (!text.EndsWith(Path.DirectorySeparatorChar))
			{
				text += Path.DirectorySeparatorChar.ToString();
			}
			return text;
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x0003EC34 File Offset: 0x0003CE34
		public static TimeZoneInfo FindSystemTimeZoneById(string id)
		{
			if (string.Equals(id, "UTC", StringComparison.OrdinalIgnoreCase))
			{
				return TimeZoneInfo.Utc;
			}
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			if (id.Length == 0 || id.Contains('\0'))
			{
				throw new TimeZoneNotFoundException(SR.Format("The time zone ID '{0}' was not found on the local computer.", id));
			}
			TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
			TimeZoneInfo.CachedData cachedData2 = cachedData;
			TimeZoneInfo timeZoneInfo;
			Exception ex;
			TimeZoneInfo.TimeZoneInfoResult timeZoneInfoResult;
			lock (cachedData2)
			{
				timeZoneInfoResult = TimeZoneInfo.TryGetTimeZone(id, false, out timeZoneInfo, out ex, cachedData, true);
			}
			if (timeZoneInfoResult == TimeZoneInfo.TimeZoneInfoResult.Success)
			{
				return timeZoneInfo;
			}
			if (timeZoneInfoResult == TimeZoneInfo.TimeZoneInfoResult.InvalidTimeZoneException)
			{
				throw ex;
			}
			if (timeZoneInfoResult == TimeZoneInfo.TimeZoneInfoResult.SecurityException)
			{
				throw new SecurityException(SR.Format("The time zone ID '{0}' was found on the local computer, but the application does not have permission to read the file.", id), ex);
			}
			throw new TimeZoneNotFoundException(SR.Format("The time zone ID '{0}' was not found on the local computer.", id), ex);
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x0003ECF8 File Offset: 0x0003CEF8
		internal static TimeSpan GetDateTimeNowUtcOffsetFromUtc(DateTime time, out bool isAmbiguousLocalDst)
		{
			bool flag;
			return TimeZoneInfo.GetUtcOffsetFromUtc(time, TimeZoneInfo.Local, out flag, out isAmbiguousLocalDst);
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x0003ED14 File Offset: 0x0003CF14
		private static void TZif_GenerateAdjustmentRules(out TimeZoneInfo.AdjustmentRule[] rules, TimeSpan baseUtcOffset, DateTime[] dts, byte[] typeOfLocalTime, TimeZoneInfo.TZifType[] transitionType, bool[] StandardTime, bool[] GmtTime, string futureTransitionsPosixFormat)
		{
			rules = null;
			if (dts.Length != 0)
			{
				int i = 0;
				List<TimeZoneInfo.AdjustmentRule> list = new List<TimeZoneInfo.AdjustmentRule>();
				while (i <= dts.Length)
				{
					TimeZoneInfo.TZif_GenerateAdjustmentRule(ref i, baseUtcOffset, list, dts, typeOfLocalTime, transitionType, StandardTime, GmtTime, futureTransitionsPosixFormat);
				}
				rules = list.ToArray();
				if (rules != null && rules.Length == 0)
				{
					rules = null;
				}
			}
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x0003ED60 File Offset: 0x0003CF60
		private static void TZif_GenerateAdjustmentRule(ref int index, TimeSpan timeZoneBaseUtcOffset, List<TimeZoneInfo.AdjustmentRule> rulesList, DateTime[] dts, byte[] typeOfLocalTime, TimeZoneInfo.TZifType[] transitionTypes, bool[] StandardTime, bool[] GmtTime, string futureTransitionsPosixFormat)
		{
			while (index < dts.Length && dts[index] == DateTime.MinValue)
			{
				index++;
			}
			if (rulesList.Count == 0 && index < dts.Length)
			{
				TimeZoneInfo.TZifType tzifType = TimeZoneInfo.TZif_GetEarlyDateTransitionType(transitionTypes);
				DateTime dateTime = dts[index];
				TimeSpan timeSpan = TimeZoneInfo.TZif_CalculateTransitionOffsetFromBase(tzifType.UtcOffset, timeZoneBaseUtcOffset);
				TimeSpan timeSpan2 = (tzifType.IsDst ? timeSpan : TimeSpan.Zero);
				TimeSpan timeSpan3 = (tzifType.IsDst ? TimeSpan.Zero : timeSpan);
				TimeZoneInfo.AdjustmentRule adjustmentRule = TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(DateTime.MinValue, dateTime.AddTicks(-1L), timeSpan2, default(TimeZoneInfo.TransitionTime), default(TimeZoneInfo.TransitionTime), timeSpan3, true);
				if (!TimeZoneInfo.IsValidAdjustmentRuleOffest(timeZoneBaseUtcOffset, adjustmentRule))
				{
					TimeZoneInfo.NormalizeAdjustmentRuleOffset(timeZoneBaseUtcOffset, ref adjustmentRule);
				}
				rulesList.Add(adjustmentRule);
			}
			else if (index < dts.Length)
			{
				DateTime dateTime2 = dts[index - 1];
				TimeZoneInfo.TZifType tzifType2 = transitionTypes[(int)typeOfLocalTime[index - 1]];
				DateTime dateTime3 = dts[index];
				TimeSpan timeSpan4 = TimeZoneInfo.TZif_CalculateTransitionOffsetFromBase(tzifType2.UtcOffset, timeZoneBaseUtcOffset);
				TimeSpan timeSpan5 = (tzifType2.IsDst ? timeSpan4 : TimeSpan.Zero);
				TimeSpan timeSpan6 = (tzifType2.IsDst ? TimeSpan.Zero : timeSpan4);
				TimeZoneInfo.TransitionTime transitionTime;
				if (tzifType2.IsDst)
				{
					transitionTime = TimeZoneInfo.TransitionTime.CreateFixedDateRule(DateTime.MinValue.AddMilliseconds(2.0), 1, 1);
				}
				else
				{
					transitionTime = default(TimeZoneInfo.TransitionTime);
				}
				TimeZoneInfo.AdjustmentRule adjustmentRule2 = TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(dateTime2, dateTime3.AddTicks(-1L), timeSpan5, transitionTime, default(TimeZoneInfo.TransitionTime), timeSpan6, true);
				if (!TimeZoneInfo.IsValidAdjustmentRuleOffest(timeZoneBaseUtcOffset, adjustmentRule2))
				{
					TimeZoneInfo.NormalizeAdjustmentRuleOffset(timeZoneBaseUtcOffset, ref adjustmentRule2);
				}
				rulesList.Add(adjustmentRule2);
			}
			else
			{
				DateTime dateTime4 = dts[index - 1];
				if (!string.IsNullOrEmpty(futureTransitionsPosixFormat))
				{
					TimeZoneInfo.AdjustmentRule adjustmentRule3 = TimeZoneInfo.TZif_CreateAdjustmentRuleForPosixFormat(futureTransitionsPosixFormat, dateTime4, timeZoneBaseUtcOffset);
					if (adjustmentRule3 != null)
					{
						if (!TimeZoneInfo.IsValidAdjustmentRuleOffest(timeZoneBaseUtcOffset, adjustmentRule3))
						{
							TimeZoneInfo.NormalizeAdjustmentRuleOffset(timeZoneBaseUtcOffset, ref adjustmentRule3);
						}
						rulesList.Add(adjustmentRule3);
					}
				}
				else
				{
					TimeZoneInfo.TZifType tzifType3 = transitionTypes[(int)typeOfLocalTime[index - 1]];
					TimeSpan timeSpan7 = TimeZoneInfo.TZif_CalculateTransitionOffsetFromBase(tzifType3.UtcOffset, timeZoneBaseUtcOffset);
					TimeSpan timeSpan8 = (tzifType3.IsDst ? timeSpan7 : TimeSpan.Zero);
					TimeSpan timeSpan9 = (tzifType3.IsDst ? TimeSpan.Zero : timeSpan7);
					TimeZoneInfo.AdjustmentRule adjustmentRule4 = TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(dateTime4, DateTime.MaxValue, timeSpan8, default(TimeZoneInfo.TransitionTime), default(TimeZoneInfo.TransitionTime), timeSpan9, true);
					if (!TimeZoneInfo.IsValidAdjustmentRuleOffest(timeZoneBaseUtcOffset, adjustmentRule4))
					{
						TimeZoneInfo.NormalizeAdjustmentRuleOffset(timeZoneBaseUtcOffset, ref adjustmentRule4);
					}
					rulesList.Add(adjustmentRule4);
				}
			}
			index++;
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x0003EFCC File Offset: 0x0003D1CC
		private static TimeSpan TZif_CalculateTransitionOffsetFromBase(TimeSpan transitionOffset, TimeSpan timeZoneBaseUtcOffset)
		{
			TimeSpan timeSpan = transitionOffset - timeZoneBaseUtcOffset;
			if (timeSpan.Ticks % 600000000L != 0L)
			{
				timeSpan = new TimeSpan(timeSpan.Hours, timeSpan.Minutes, 0);
			}
			return timeSpan;
		}

		// Token: 0x06000F70 RID: 3952 RVA: 0x0003F008 File Offset: 0x0003D208
		private static TimeZoneInfo.TZifType TZif_GetEarlyDateTransitionType(TimeZoneInfo.TZifType[] transitionTypes)
		{
			foreach (TimeZoneInfo.TZifType tzifType in transitionTypes)
			{
				if (!tzifType.IsDst)
				{
					return tzifType;
				}
			}
			if (transitionTypes.Length != 0)
			{
				return transitionTypes[0];
			}
			throw new InvalidTimeZoneException("There are no ttinfo structures in the tzfile.  At least one ttinfo structure is required in order to construct a TimeZoneInfo object.");
		}

		// Token: 0x06000F71 RID: 3953 RVA: 0x0003F050 File Offset: 0x0003D250
		private static TimeZoneInfo.AdjustmentRule TZif_CreateAdjustmentRuleForPosixFormat(string posixFormat, DateTime startTransitionDate, TimeSpan timeZoneBaseUtcOffset)
		{
			string text;
			string text2;
			string text3;
			string text4;
			string text5;
			string text6;
			string text7;
			string text8;
			if (TimeZoneInfo.TZif_ParsePosixFormat(posixFormat, out text, out text2, out text3, out text4, out text5, out text6, out text7, out text8))
			{
				TimeSpan? timeSpan = TimeZoneInfo.TZif_ParseOffsetString(text2);
				if (timeSpan != null)
				{
					TimeSpan timeSpan2 = timeSpan.Value.Negate();
					timeSpan2 = TimeZoneInfo.TZif_CalculateTransitionOffsetFromBase(timeSpan2, timeZoneBaseUtcOffset);
					if (!string.IsNullOrEmpty(text3))
					{
						TimeSpan? timeSpan3 = TimeZoneInfo.TZif_ParseOffsetString(text4);
						TimeSpan timeSpan4;
						if (timeSpan3 == null)
						{
							timeSpan4 = new TimeSpan(1, 0, 0);
						}
						else
						{
							timeSpan4 = timeSpan3.Value.Negate();
							timeSpan4 = TimeZoneInfo.TZif_CalculateTransitionOffsetFromBase(timeSpan4, timeZoneBaseUtcOffset);
							timeSpan4 = TimeZoneInfo.TZif_CalculateTransitionOffsetFromBase(timeSpan4, timeSpan2);
						}
						TimeZoneInfo.TransitionTime transitionTime = TimeZoneInfo.TZif_CreateTransitionTimeFromPosixRule(text5, text6);
						TimeZoneInfo.TransitionTime transitionTime2 = TimeZoneInfo.TZif_CreateTransitionTimeFromPosixRule(text7, text8);
						return TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(startTransitionDate, DateTime.MaxValue, timeSpan4, transitionTime, transitionTime2, timeSpan2, false);
					}
					return TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(startTransitionDate, DateTime.MaxValue, TimeSpan.Zero, default(TimeZoneInfo.TransitionTime), default(TimeZoneInfo.TransitionTime), timeSpan2, true);
				}
			}
			return null;
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x0003F148 File Offset: 0x0003D348
		private static TimeSpan? TZif_ParseOffsetString(string offset)
		{
			TimeSpan? timeSpan = null;
			if (!string.IsNullOrEmpty(offset))
			{
				bool flag = offset[0] == '-';
				if (flag || offset[0] == '+')
				{
					offset = offset.Substring(1);
				}
				int num;
				TimeSpan timeSpan2;
				if (int.TryParse(offset, out num))
				{
					timeSpan = new TimeSpan?(new TimeSpan(num, 0, 0));
				}
				else if (TimeSpan.TryParseExact(offset, "g", CultureInfo.InvariantCulture, out timeSpan2))
				{
					timeSpan = new TimeSpan?(timeSpan2);
				}
				if (timeSpan != null && flag)
				{
					timeSpan = new TimeSpan?(timeSpan.Value.Negate());
				}
			}
			return timeSpan;
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x0003F1E4 File Offset: 0x0003D3E4
		private static DateTime ParseTimeOfDay(string time)
		{
			TimeSpan? timeSpan = TimeZoneInfo.TZif_ParseOffsetString(time);
			DateTime dateTime;
			if (timeSpan != null)
			{
				timeSpan = new TimeSpan?(new TimeSpan(timeSpan.Value.Hours, timeSpan.Value.Minutes, timeSpan.Value.Seconds));
				if (timeSpan.Value < TimeSpan.Zero)
				{
					dateTime = new DateTime(1, 1, 2, 0, 0, 0);
				}
				else
				{
					dateTime = new DateTime(1, 1, 1, 0, 0, 0);
				}
				dateTime += timeSpan.Value;
			}
			else
			{
				dateTime = new DateTime(1, 1, 1, 2, 0, 0);
			}
			return dateTime;
		}

		// Token: 0x06000F74 RID: 3956 RVA: 0x0003F288 File Offset: 0x0003D488
		private static TimeZoneInfo.TransitionTime TZif_CreateTransitionTimeFromPosixRule(string date, string time)
		{
			if (string.IsNullOrEmpty(date))
			{
				return default(TimeZoneInfo.TransitionTime);
			}
			if (date[0] == 'M')
			{
				int num;
				int num2;
				DayOfWeek dayOfWeek;
				if (!TimeZoneInfo.TZif_ParseMDateRule(date, out num, out num2, out dayOfWeek))
				{
					throw new InvalidTimeZoneException(SR.Format("'{0}' is not a valid POSIX-TZ-environment-variable MDate rule.  A valid rule has the format 'Mm.w.d'.", date));
				}
				return TimeZoneInfo.TransitionTime.CreateFloatingDateRule(TimeZoneInfo.ParseTimeOfDay(time), num, num2, dayOfWeek);
			}
			else
			{
				if (date[0] != 'J')
				{
					throw new InvalidTimeZoneException("Julian n day in POSIX strings is not supported.");
				}
				int num3;
				int num4;
				TimeZoneInfo.TZif_ParseJulianDay(date, out num3, out num4);
				return TimeZoneInfo.TransitionTime.CreateFixedDateRule(TimeZoneInfo.ParseTimeOfDay(time), num3, num4);
			}
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x0003F310 File Offset: 0x0003D510
		private static void TZif_ParseJulianDay(string date, out int month, out int day)
		{
			month = (day = 0);
			int num = 1;
			if (num >= date.Length || date[num] - '0' > '\t')
			{
				throw new InvalidTimeZoneException("Invalid Julian day in POSIX strings.");
			}
			int num2 = 0;
			do
			{
				num2 = num2 * 10 + (int)(date[num] - '0');
				num++;
			}
			while (num < date.Length && date[num] - '0' <= '\t');
			int[] daysToMonth = GregorianCalendarHelper.DaysToMonth365;
			if (num2 == 0 || num2 > daysToMonth[daysToMonth.Length - 1])
			{
				throw new InvalidTimeZoneException("Invalid Julian day in POSIX strings.");
			}
			int num3 = 1;
			while (num3 < daysToMonth.Length && num2 > daysToMonth[num3])
			{
				num3++;
			}
			month = num3;
			day = num2 - daysToMonth[num3 - 1];
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x0003F3B8 File Offset: 0x0003D5B8
		private static bool TZif_ParseMDateRule(string dateRule, out int month, out int week, out DayOfWeek dayOfWeek)
		{
			if (dateRule[0] == 'M')
			{
				int num = dateRule.IndexOf('.');
				if (num > 0)
				{
					int num2 = dateRule.IndexOf('.', num + 1);
					int num3;
					if (num2 > 0 && int.TryParse(dateRule.AsSpan(1, num - 1), out month) && int.TryParse(dateRule.AsSpan(num + 1, num2 - num - 1), out week) && int.TryParse(dateRule.AsSpan(num2 + 1), out num3))
					{
						dayOfWeek = (DayOfWeek)num3;
						return true;
					}
				}
			}
			month = 0;
			week = 0;
			dayOfWeek = DayOfWeek.Sunday;
			return false;
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x0003F438 File Offset: 0x0003D638
		private static bool TZif_ParsePosixFormat(string posixFormat, out string standardName, out string standardOffset, out string daylightSavingsName, out string daylightSavingsOffset, out string start, out string startTime, out string end, out string endTime)
		{
			standardName = null;
			standardOffset = null;
			daylightSavingsName = null;
			daylightSavingsOffset = null;
			start = null;
			startTime = null;
			end = null;
			endTime = null;
			int num = 0;
			standardName = TimeZoneInfo.TZif_ParsePosixName(posixFormat, ref num);
			standardOffset = TimeZoneInfo.TZif_ParsePosixOffset(posixFormat, ref num);
			daylightSavingsName = TimeZoneInfo.TZif_ParsePosixName(posixFormat, ref num);
			if (!string.IsNullOrEmpty(daylightSavingsName))
			{
				daylightSavingsOffset = TimeZoneInfo.TZif_ParsePosixOffset(posixFormat, ref num);
				if (num < posixFormat.Length && posixFormat[num] == ',')
				{
					num++;
					TimeZoneInfo.TZif_ParsePosixDateTime(posixFormat, ref num, out start, out startTime);
					if (num < posixFormat.Length && posixFormat[num] == ',')
					{
						num++;
						TimeZoneInfo.TZif_ParsePosixDateTime(posixFormat, ref num, out end, out endTime);
					}
				}
			}
			return !string.IsNullOrEmpty(standardName) && !string.IsNullOrEmpty(standardOffset);
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x0003F4F4 File Offset: 0x0003D6F4
		private static string TZif_ParsePosixName(string posixFormat, ref int index)
		{
			if (index < posixFormat.Length && posixFormat[index] == '<')
			{
				index++;
				string text = TimeZoneInfo.TZif_ParsePosixString(posixFormat, ref index, (char c) => c == '>');
				if (index < posixFormat.Length && posixFormat[index] == '>')
				{
					index++;
				}
				return text;
			}
			return TimeZoneInfo.TZif_ParsePosixString(posixFormat, ref index, (char c) => char.IsDigit(c) || c == '+' || c == '-' || c == ',');
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x0003F58B File Offset: 0x0003D78B
		private static string TZif_ParsePosixOffset(string posixFormat, ref int index)
		{
			return TimeZoneInfo.TZif_ParsePosixString(posixFormat, ref index, (char c) => !char.IsDigit(c) && c != '+' && c != '-' && c != ':');
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x0003F5B3 File Offset: 0x0003D7B3
		private static void TZif_ParsePosixDateTime(string posixFormat, ref int index, out string date, out string time)
		{
			time = null;
			date = TimeZoneInfo.TZif_ParsePosixDate(posixFormat, ref index);
			if (index < posixFormat.Length && posixFormat[index] == '/')
			{
				index++;
				time = TimeZoneInfo.TZif_ParsePosixTime(posixFormat, ref index);
			}
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x0003F5E6 File Offset: 0x0003D7E6
		private static string TZif_ParsePosixDate(string posixFormat, ref int index)
		{
			return TimeZoneInfo.TZif_ParsePosixString(posixFormat, ref index, (char c) => c == '/' || c == ',');
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x0003F60E File Offset: 0x0003D80E
		private static string TZif_ParsePosixTime(string posixFormat, ref int index)
		{
			return TimeZoneInfo.TZif_ParsePosixString(posixFormat, ref index, (char c) => c == ',');
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x0003F638 File Offset: 0x0003D838
		private static string TZif_ParsePosixString(string posixFormat, ref int index, Func<char, bool> breakCondition)
		{
			int num = index;
			while (index < posixFormat.Length)
			{
				char c = posixFormat[index];
				if (breakCondition(c))
				{
					break;
				}
				index++;
			}
			return posixFormat.Substring(num, index - num);
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x0003F678 File Offset: 0x0003D878
		private static string TZif_GetZoneAbbreviation(string zoneAbbreviations, int index)
		{
			int num = zoneAbbreviations.IndexOf('\0', index);
			if (num <= 0)
			{
				return zoneAbbreviations.Substring(index);
			}
			return zoneAbbreviations.Substring(index, num - index);
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x0003F6A4 File Offset: 0x0003D8A4
		private unsafe static int TZif_ToInt32(byte[] value, int startIndex)
		{
			fixed (byte* ptr = &value[startIndex])
			{
				byte* ptr2 = ptr;
				return ((int)(*ptr2) << 24) | ((int)ptr2[1] << 16) | ((int)ptr2[2] << 8) | (int)ptr2[3];
			}
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x0003F6D8 File Offset: 0x0003D8D8
		private unsafe static long TZif_ToInt64(byte[] value, int startIndex)
		{
			fixed (byte* ptr = &value[startIndex])
			{
				byte* ptr2 = ptr;
				int num = ((int)(*ptr2) << 24) | ((int)ptr2[1] << 16) | ((int)ptr2[2] << 8) | (int)ptr2[3];
				return (long)((ulong)(((int)ptr2[4] << 24) | ((int)ptr2[5] << 16) | ((int)ptr2[6] << 8) | (int)ptr2[7]) | (ulong)((ulong)((long)num) << 32));
			}
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x0003F72C File Offset: 0x0003D92C
		private static long TZif_ToUnixTime(byte[] value, int startIndex, TimeZoneInfo.TZVersion version)
		{
			if (version == TimeZoneInfo.TZVersion.V1)
			{
				return (long)TimeZoneInfo.TZif_ToInt32(value, startIndex);
			}
			return TimeZoneInfo.TZif_ToInt64(value, startIndex);
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x0003F744 File Offset: 0x0003D944
		private static DateTime TZif_UnixTimeToDateTime(long unixTime)
		{
			if (unixTime < -62135596800L)
			{
				return DateTime.MinValue;
			}
			if (unixTime <= 253402300799L)
			{
				return DateTimeOffset.FromUnixTimeSeconds(unixTime).UtcDateTime;
			}
			return DateTime.MaxValue;
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x0003F784 File Offset: 0x0003D984
		private static void TZif_ParseRaw(byte[] data, out TimeZoneInfo.TZifHead t, out DateTime[] dts, out byte[] typeOfLocalTime, out TimeZoneInfo.TZifType[] transitionType, out string zoneAbbreviations, out bool[] StandardTime, out bool[] GmtTime, out string futureTransitionsPosixFormat)
		{
			dts = null;
			typeOfLocalTime = null;
			transitionType = null;
			zoneAbbreviations = string.Empty;
			StandardTime = null;
			GmtTime = null;
			futureTransitionsPosixFormat = null;
			int num = 0;
			t = new TimeZoneInfo.TZifHead(data, num);
			num += 44;
			int num2 = 4;
			if (t.Version != TimeZoneInfo.TZVersion.V1)
			{
				num += (int)((long)num2 * (long)((ulong)t.TimeCount) + (long)((ulong)t.TimeCount) + (long)((ulong)(6U * t.TypeCount)) + (long)(num2 + 4) * (long)((ulong)t.LeapCount) + (long)((ulong)t.IsStdCount) + (long)((ulong)t.IsGmtCount) + (long)((ulong)t.CharCount));
				t = new TimeZoneInfo.TZifHead(data, num);
				num += 44;
				num2 = 8;
			}
			dts = new DateTime[t.TimeCount];
			typeOfLocalTime = new byte[t.TimeCount];
			transitionType = new TimeZoneInfo.TZifType[t.TypeCount];
			zoneAbbreviations = string.Empty;
			StandardTime = new bool[t.TypeCount];
			GmtTime = new bool[t.TypeCount];
			int num3 = 0;
			while ((long)num3 < (long)((ulong)t.TimeCount))
			{
				long num4 = TimeZoneInfo.TZif_ToUnixTime(data, num, t.Version);
				dts[num3] = TimeZoneInfo.TZif_UnixTimeToDateTime(num4);
				num += num2;
				num3++;
			}
			int num5 = 0;
			while ((long)num5 < (long)((ulong)t.TimeCount))
			{
				typeOfLocalTime[num5] = data[num];
				num++;
				num5++;
			}
			int num6 = 0;
			while ((long)num6 < (long)((ulong)t.TypeCount))
			{
				transitionType[num6] = new TimeZoneInfo.TZifType(data, num);
				num += 6;
				num6++;
			}
			Encoding utf = Encoding.UTF8;
			zoneAbbreviations = utf.GetString(data, num, (int)t.CharCount);
			num += (int)t.CharCount;
			num += (int)((ulong)t.LeapCount * (ulong)((long)(num2 + 4)));
			int num7 = 0;
			while ((long)num7 < (long)((ulong)t.IsStdCount) && (long)num7 < (long)((ulong)t.TypeCount) && num < data.Length)
			{
				StandardTime[num7] = data[num++] > 0;
				num7++;
			}
			int num8 = 0;
			while ((long)num8 < (long)((ulong)t.IsGmtCount) && (long)num8 < (long)((ulong)t.TypeCount) && num < data.Length)
			{
				GmtTime[num8] = data[num++] > 0;
				num8++;
			}
			if (t.Version != TimeZoneInfo.TZVersion.V1 && data[num++] == 10 && data[data.Length - 1] == 10)
			{
				futureTransitionsPosixFormat = utf.GetString(data, num, data.Length - num - 1);
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000F84 RID: 3972 RVA: 0x0003F9C8 File Offset: 0x0003DBC8
		public string Id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000F85 RID: 3973 RVA: 0x0003F9D0 File Offset: 0x0003DBD0
		public string DisplayName
		{
			get
			{
				return this._displayName ?? string.Empty;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000F86 RID: 3974 RVA: 0x0003F9E1 File Offset: 0x0003DBE1
		public string StandardName
		{
			get
			{
				return this._standardDisplayName ?? string.Empty;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000F87 RID: 3975 RVA: 0x0003F9F2 File Offset: 0x0003DBF2
		public string DaylightName
		{
			get
			{
				return this._daylightDisplayName ?? string.Empty;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000F88 RID: 3976 RVA: 0x0003FA03 File Offset: 0x0003DC03
		public TimeSpan BaseUtcOffset
		{
			get
			{
				return this._baseUtcOffset;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000F89 RID: 3977 RVA: 0x0003FA0B File Offset: 0x0003DC0B
		public bool SupportsDaylightSavingTime
		{
			get
			{
				return this._supportsDaylightSavingTime;
			}
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x0003FA14 File Offset: 0x0003DC14
		public TimeSpan[] GetAmbiguousTimeOffsets(DateTimeOffset dateTimeOffset)
		{
			if (!this.SupportsDaylightSavingTime)
			{
				throw new ArgumentException("The supplied DateTimeOffset is not in an ambiguous time range.", "dateTimeOffset");
			}
			DateTime dateTime = TimeZoneInfo.ConvertTime(dateTimeOffset, this).DateTime;
			bool flag = false;
			int? num;
			TimeZoneInfo.AdjustmentRule adjustmentRuleForAmbiguousOffsets = this.GetAdjustmentRuleForAmbiguousOffsets(dateTime, out num);
			if (adjustmentRuleForAmbiguousOffsets != null && adjustmentRuleForAmbiguousOffsets.HasDaylightSaving)
			{
				DaylightTimeStruct daylightTime = this.GetDaylightTime(dateTime.Year, adjustmentRuleForAmbiguousOffsets, num);
				flag = TimeZoneInfo.GetIsAmbiguousTime(dateTime, adjustmentRuleForAmbiguousOffsets, daylightTime);
			}
			if (!flag)
			{
				throw new ArgumentException("The supplied DateTimeOffset is not in an ambiguous time range.", "dateTimeOffset");
			}
			TimeSpan[] array = new TimeSpan[2];
			TimeSpan timeSpan = this._baseUtcOffset + adjustmentRuleForAmbiguousOffsets.BaseUtcOffsetDelta;
			if (adjustmentRuleForAmbiguousOffsets.DaylightDelta > TimeSpan.Zero)
			{
				array[0] = timeSpan;
				array[1] = timeSpan + adjustmentRuleForAmbiguousOffsets.DaylightDelta;
			}
			else
			{
				array[0] = timeSpan + adjustmentRuleForAmbiguousOffsets.DaylightDelta;
				array[1] = timeSpan;
			}
			return array;
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x0003FB00 File Offset: 0x0003DD00
		public TimeSpan[] GetAmbiguousTimeOffsets(DateTime dateTime)
		{
			if (!this.SupportsDaylightSavingTime)
			{
				throw new ArgumentException("The supplied DateTime is not in an ambiguous time range.", "dateTime");
			}
			DateTime dateTime2;
			if (dateTime.Kind == DateTimeKind.Local)
			{
				TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
				dateTime2 = TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, this, TimeZoneInfoOptions.None, cachedData);
			}
			else if (dateTime.Kind == DateTimeKind.Utc)
			{
				TimeZoneInfo.CachedData cachedData2 = TimeZoneInfo.s_cachedData;
				dateTime2 = TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.s_utcTimeZone, this, TimeZoneInfoOptions.None, cachedData2);
			}
			else
			{
				dateTime2 = dateTime;
			}
			bool flag = false;
			int? num;
			TimeZoneInfo.AdjustmentRule adjustmentRuleForAmbiguousOffsets = this.GetAdjustmentRuleForAmbiguousOffsets(dateTime2, out num);
			if (adjustmentRuleForAmbiguousOffsets != null && adjustmentRuleForAmbiguousOffsets.HasDaylightSaving)
			{
				DaylightTimeStruct daylightTime = this.GetDaylightTime(dateTime2.Year, adjustmentRuleForAmbiguousOffsets, num);
				flag = TimeZoneInfo.GetIsAmbiguousTime(dateTime2, adjustmentRuleForAmbiguousOffsets, daylightTime);
			}
			if (!flag)
			{
				throw new ArgumentException("The supplied DateTime is not in an ambiguous time range.", "dateTime");
			}
			TimeSpan[] array = new TimeSpan[2];
			TimeSpan timeSpan = this._baseUtcOffset + adjustmentRuleForAmbiguousOffsets.BaseUtcOffsetDelta;
			if (adjustmentRuleForAmbiguousOffsets.DaylightDelta > TimeSpan.Zero)
			{
				array[0] = timeSpan;
				array[1] = timeSpan + adjustmentRuleForAmbiguousOffsets.DaylightDelta;
			}
			else
			{
				array[0] = timeSpan + adjustmentRuleForAmbiguousOffsets.DaylightDelta;
				array[1] = timeSpan;
			}
			return array;
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x0003FC24 File Offset: 0x0003DE24
		private TimeZoneInfo.AdjustmentRule GetAdjustmentRuleForAmbiguousOffsets(DateTime adjustedTime, out int? ruleIndex)
		{
			TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = this.GetAdjustmentRuleForTime(adjustedTime, out ruleIndex);
			if (adjustmentRuleForTime != null && adjustmentRuleForTime.NoDaylightTransitions && !adjustmentRuleForTime.HasDaylightSaving)
			{
				return this.GetPreviousAdjustmentRule(adjustmentRuleForTime, ruleIndex);
			}
			return adjustmentRuleForTime;
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x0003FC5C File Offset: 0x0003DE5C
		private TimeZoneInfo.AdjustmentRule GetPreviousAdjustmentRule(TimeZoneInfo.AdjustmentRule rule, int? ruleIndex)
		{
			if (ruleIndex != null && 0 < ruleIndex.Value && ruleIndex.Value < this._adjustmentRules.Length)
			{
				return this._adjustmentRules[ruleIndex.Value - 1];
			}
			TimeZoneInfo.AdjustmentRule adjustmentRule = rule;
			for (int i = 1; i < this._adjustmentRules.Length; i++)
			{
				if (rule == this._adjustmentRules[i])
				{
					adjustmentRule = this._adjustmentRules[i - 1];
					break;
				}
			}
			return adjustmentRule;
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x0003FCCC File Offset: 0x0003DECC
		public TimeSpan GetUtcOffset(DateTimeOffset dateTimeOffset)
		{
			return TimeZoneInfo.GetUtcOffsetFromUtc(dateTimeOffset.UtcDateTime, this);
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x0003FCDB File Offset: 0x0003DEDB
		public TimeSpan GetUtcOffset(DateTime dateTime)
		{
			return this.GetUtcOffset(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime, TimeZoneInfo.s_cachedData);
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x0003FCEC File Offset: 0x0003DEEC
		internal static TimeSpan GetLocalUtcOffset(DateTime dateTime, TimeZoneInfoOptions flags)
		{
			TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
			return cachedData.Local.GetUtcOffset(dateTime, flags, cachedData);
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x0003FD0D File Offset: 0x0003DF0D
		internal TimeSpan GetUtcOffset(DateTime dateTime, TimeZoneInfoOptions flags)
		{
			return this.GetUtcOffset(dateTime, flags, TimeZoneInfo.s_cachedData);
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x0003FD1C File Offset: 0x0003DF1C
		private TimeSpan GetUtcOffset(DateTime dateTime, TimeZoneInfoOptions flags, TimeZoneInfo.CachedData cachedData)
		{
			if (dateTime.Kind == DateTimeKind.Local)
			{
				if (cachedData.GetCorrespondingKind(this) != DateTimeKind.Local)
				{
					return TimeZoneInfo.GetUtcOffsetFromUtc(TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, TimeZoneInfo.s_utcTimeZone, flags), this);
				}
			}
			else if (dateTime.Kind == DateTimeKind.Utc)
			{
				if (cachedData.GetCorrespondingKind(this) == DateTimeKind.Utc)
				{
					return this._baseUtcOffset;
				}
				return TimeZoneInfo.GetUtcOffsetFromUtc(dateTime, this);
			}
			return TimeZoneInfo.GetUtcOffset(dateTime, this, flags);
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x0003FD84 File Offset: 0x0003DF84
		public bool IsAmbiguousTime(DateTimeOffset dateTimeOffset)
		{
			return this._supportsDaylightSavingTime && this.IsAmbiguousTime(TimeZoneInfo.ConvertTime(dateTimeOffset, this).DateTime);
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x0003FDB0 File Offset: 0x0003DFB0
		public bool IsAmbiguousTime(DateTime dateTime)
		{
			return this.IsAmbiguousTime(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime);
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x0003FDBC File Offset: 0x0003DFBC
		internal bool IsAmbiguousTime(DateTime dateTime, TimeZoneInfoOptions flags)
		{
			if (!this._supportsDaylightSavingTime)
			{
				return false;
			}
			TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
			DateTime dateTime2 = ((dateTime.Kind == DateTimeKind.Local) ? TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, this, flags, cachedData) : ((dateTime.Kind == DateTimeKind.Utc) ? TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.s_utcTimeZone, this, flags, cachedData) : dateTime));
			int? num;
			TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = this.GetAdjustmentRuleForTime(dateTime2, out num);
			if (adjustmentRuleForTime != null && adjustmentRuleForTime.HasDaylightSaving)
			{
				DaylightTimeStruct daylightTime = this.GetDaylightTime(dateTime2.Year, adjustmentRuleForTime, num);
				return TimeZoneInfo.GetIsAmbiguousTime(dateTime2, adjustmentRuleForTime, daylightTime);
			}
			return false;
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x0003FE44 File Offset: 0x0003E044
		public bool IsDaylightSavingTime(DateTimeOffset dateTimeOffset)
		{
			bool flag;
			TimeZoneInfo.GetUtcOffsetFromUtc(dateTimeOffset.UtcDateTime, this, out flag);
			return flag;
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x0003FE62 File Offset: 0x0003E062
		public bool IsDaylightSavingTime(DateTime dateTime)
		{
			return this.IsDaylightSavingTime(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime, TimeZoneInfo.s_cachedData);
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x0003FE71 File Offset: 0x0003E071
		internal bool IsDaylightSavingTime(DateTime dateTime, TimeZoneInfoOptions flags)
		{
			return this.IsDaylightSavingTime(dateTime, flags, TimeZoneInfo.s_cachedData);
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x0003FE80 File Offset: 0x0003E080
		private bool IsDaylightSavingTime(DateTime dateTime, TimeZoneInfoOptions flags, TimeZoneInfo.CachedData cachedData)
		{
			if (!this._supportsDaylightSavingTime || this._adjustmentRules == null)
			{
				return false;
			}
			DateTime dateTime2;
			if (dateTime.Kind == DateTimeKind.Local)
			{
				dateTime2 = TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, this, flags, cachedData);
			}
			else if (dateTime.Kind == DateTimeKind.Utc)
			{
				if (cachedData.GetCorrespondingKind(this) == DateTimeKind.Utc)
				{
					return false;
				}
				bool flag;
				TimeZoneInfo.GetUtcOffsetFromUtc(dateTime, this, out flag);
				return flag;
			}
			else
			{
				dateTime2 = dateTime;
			}
			int? num;
			TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = this.GetAdjustmentRuleForTime(dateTime2, out num);
			if (adjustmentRuleForTime != null && adjustmentRuleForTime.HasDaylightSaving)
			{
				DaylightTimeStruct daylightTime = this.GetDaylightTime(dateTime2.Year, adjustmentRuleForTime, num);
				return TimeZoneInfo.GetIsDaylightSavings(dateTime2, adjustmentRuleForTime, daylightTime, flags);
			}
			return false;
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x0003FF14 File Offset: 0x0003E114
		public bool IsInvalidTime(DateTime dateTime)
		{
			bool flag = false;
			if (dateTime.Kind == DateTimeKind.Unspecified || (dateTime.Kind == DateTimeKind.Local && TimeZoneInfo.s_cachedData.GetCorrespondingKind(this) == DateTimeKind.Local))
			{
				int? num;
				TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = this.GetAdjustmentRuleForTime(dateTime, out num);
				if (adjustmentRuleForTime != null && adjustmentRuleForTime.HasDaylightSaving)
				{
					DaylightTimeStruct daylightTime = this.GetDaylightTime(dateTime.Year, adjustmentRuleForTime, num);
					flag = TimeZoneInfo.GetIsInvalidTime(dateTime, adjustmentRuleForTime, daylightTime);
				}
				else
				{
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x0003FF77 File Offset: 0x0003E177
		public static void ClearCachedData()
		{
			TimeZoneInfo.s_cachedData = new TimeZoneInfo.CachedData();
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x0003FF83 File Offset: 0x0003E183
		public static DateTimeOffset ConvertTimeBySystemTimeZoneId(DateTimeOffset dateTimeOffset, string destinationTimeZoneId)
		{
			return TimeZoneInfo.ConvertTime(dateTimeOffset, TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId));
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x0003FF91 File Offset: 0x0003E191
		public static DateTime ConvertTimeBySystemTimeZoneId(DateTime dateTime, string destinationTimeZoneId)
		{
			return TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId));
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x0003FFA0 File Offset: 0x0003E1A0
		public static DateTime ConvertTimeBySystemTimeZoneId(DateTime dateTime, string sourceTimeZoneId, string destinationTimeZoneId)
		{
			if (dateTime.Kind == DateTimeKind.Local && string.Equals(sourceTimeZoneId, TimeZoneInfo.Local.Id, StringComparison.OrdinalIgnoreCase))
			{
				TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
				return TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId), TimeZoneInfoOptions.None, cachedData);
			}
			if (dateTime.Kind == DateTimeKind.Utc && string.Equals(sourceTimeZoneId, TimeZoneInfo.Utc.Id, StringComparison.OrdinalIgnoreCase))
			{
				return TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.s_utcTimeZone, TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId), TimeZoneInfoOptions.None, TimeZoneInfo.s_cachedData);
			}
			return TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.FindSystemTimeZoneById(sourceTimeZoneId), TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId));
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x0004002C File Offset: 0x0003E22C
		public static DateTimeOffset ConvertTime(DateTimeOffset dateTimeOffset, TimeZoneInfo destinationTimeZone)
		{
			if (destinationTimeZone == null)
			{
				throw new ArgumentNullException("destinationTimeZone");
			}
			DateTime utcDateTime = dateTimeOffset.UtcDateTime;
			TimeSpan utcOffsetFromUtc = TimeZoneInfo.GetUtcOffsetFromUtc(utcDateTime, destinationTimeZone);
			long num = utcDateTime.Ticks + utcOffsetFromUtc.Ticks;
			if (num > DateTimeOffset.MaxValue.Ticks)
			{
				return DateTimeOffset.MaxValue;
			}
			if (num >= DateTimeOffset.MinValue.Ticks)
			{
				return new DateTimeOffset(num, utcOffsetFromUtc);
			}
			return DateTimeOffset.MinValue;
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x00040094 File Offset: 0x0003E294
		public static DateTime ConvertTime(DateTime dateTime, TimeZoneInfo destinationTimeZone)
		{
			if (destinationTimeZone == null)
			{
				throw new ArgumentNullException("destinationTimeZone");
			}
			if (dateTime.Ticks == 0L)
			{
				TimeZoneInfo.ClearCachedData();
			}
			TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
			TimeZoneInfo timeZoneInfo = ((dateTime.Kind == DateTimeKind.Utc) ? TimeZoneInfo.s_utcTimeZone : cachedData.Local);
			return TimeZoneInfo.ConvertTime(dateTime, timeZoneInfo, destinationTimeZone, TimeZoneInfoOptions.None, cachedData);
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x000400E5 File Offset: 0x0003E2E5
		public static DateTime ConvertTime(DateTime dateTime, TimeZoneInfo sourceTimeZone, TimeZoneInfo destinationTimeZone)
		{
			return TimeZoneInfo.ConvertTime(dateTime, sourceTimeZone, destinationTimeZone, TimeZoneInfoOptions.None, TimeZoneInfo.s_cachedData);
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x000400F5 File Offset: 0x0003E2F5
		internal static DateTime ConvertTime(DateTime dateTime, TimeZoneInfo sourceTimeZone, TimeZoneInfo destinationTimeZone, TimeZoneInfoOptions flags)
		{
			return TimeZoneInfo.ConvertTime(dateTime, sourceTimeZone, destinationTimeZone, flags, TimeZoneInfo.s_cachedData);
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x00040108 File Offset: 0x0003E308
		private static DateTime ConvertTime(DateTime dateTime, TimeZoneInfo sourceTimeZone, TimeZoneInfo destinationTimeZone, TimeZoneInfoOptions flags, TimeZoneInfo.CachedData cachedData)
		{
			if (sourceTimeZone == null)
			{
				throw new ArgumentNullException("sourceTimeZone");
			}
			if (destinationTimeZone == null)
			{
				throw new ArgumentNullException("destinationTimeZone");
			}
			DateTimeKind correspondingKind = cachedData.GetCorrespondingKind(sourceTimeZone);
			if ((flags & TimeZoneInfoOptions.NoThrowOnInvalidTime) == (TimeZoneInfoOptions)0 && dateTime.Kind != DateTimeKind.Unspecified && dateTime.Kind != correspondingKind)
			{
				throw new ArgumentException("The conversion could not be completed because the supplied DateTime did not have the Kind property set correctly.  For example, when the Kind property is DateTimeKind.Local, the source time zone must be TimeZoneInfo.Local.", "sourceTimeZone");
			}
			int? num;
			TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = sourceTimeZone.GetAdjustmentRuleForTime(dateTime, out num);
			TimeSpan timeSpan = sourceTimeZone.BaseUtcOffset;
			if (adjustmentRuleForTime != null)
			{
				timeSpan += adjustmentRuleForTime.BaseUtcOffsetDelta;
				if (adjustmentRuleForTime.HasDaylightSaving)
				{
					DaylightTimeStruct daylightTime = sourceTimeZone.GetDaylightTime(dateTime.Year, adjustmentRuleForTime, num);
					if ((flags & TimeZoneInfoOptions.NoThrowOnInvalidTime) == (TimeZoneInfoOptions)0 && TimeZoneInfo.GetIsInvalidTime(dateTime, adjustmentRuleForTime, daylightTime))
					{
						throw new ArgumentException("The supplied DateTime represents an invalid time.  For example, when the clock is adjusted forward, any time in the period that is skipped is invalid.", "dateTime");
					}
					bool isDaylightSavings = TimeZoneInfo.GetIsDaylightSavings(dateTime, adjustmentRuleForTime, daylightTime, flags);
					timeSpan += (isDaylightSavings ? adjustmentRuleForTime.DaylightDelta : TimeSpan.Zero);
				}
			}
			DateTimeKind correspondingKind2 = cachedData.GetCorrespondingKind(destinationTimeZone);
			if (dateTime.Kind != DateTimeKind.Unspecified && correspondingKind != DateTimeKind.Unspecified && correspondingKind == correspondingKind2)
			{
				return dateTime;
			}
			bool flag;
			DateTime dateTime2 = TimeZoneInfo.ConvertUtcToTimeZone(dateTime.Ticks - timeSpan.Ticks, destinationTimeZone, out flag);
			if (correspondingKind2 == DateTimeKind.Local)
			{
				return new DateTime(dateTime2.Ticks, DateTimeKind.Local, flag);
			}
			return new DateTime(dateTime2.Ticks, correspondingKind2);
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x0004023C File Offset: 0x0003E43C
		public static DateTime ConvertTimeFromUtc(DateTime dateTime, TimeZoneInfo destinationTimeZone)
		{
			return TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.s_utcTimeZone, destinationTimeZone, TimeZoneInfoOptions.None, TimeZoneInfo.s_cachedData);
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x00040250 File Offset: 0x0003E450
		public static DateTime ConvertTimeToUtc(DateTime dateTime)
		{
			if (dateTime.Kind == DateTimeKind.Utc)
			{
				return dateTime;
			}
			TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
			return TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, TimeZoneInfo.s_utcTimeZone, TimeZoneInfoOptions.None, cachedData);
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x00040284 File Offset: 0x0003E484
		internal static DateTime ConvertTimeToUtc(DateTime dateTime, TimeZoneInfoOptions flags)
		{
			if (dateTime.Kind == DateTimeKind.Utc)
			{
				return dateTime;
			}
			TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
			return TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, TimeZoneInfo.s_utcTimeZone, flags, cachedData);
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x000402B6 File Offset: 0x0003E4B6
		public static DateTime ConvertTimeToUtc(DateTime dateTime, TimeZoneInfo sourceTimeZone)
		{
			return TimeZoneInfo.ConvertTime(dateTime, sourceTimeZone, TimeZoneInfo.s_utcTimeZone, TimeZoneInfoOptions.None, TimeZoneInfo.s_cachedData);
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x000402CA File Offset: 0x0003E4CA
		public bool Equals(TimeZoneInfo other)
		{
			return other != null && string.Equals(this._id, other._id, StringComparison.OrdinalIgnoreCase) && this.HasSameRules(other);
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x000402EC File Offset: 0x0003E4EC
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TimeZoneInfo);
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x000402FA File Offset: 0x0003E4FA
		public static TimeZoneInfo FromSerializedString(string source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (source.Length == 0)
			{
				throw new ArgumentException(SR.Format("The specified serialized string '{0}' is not supported.", source), "source");
			}
			return TimeZoneInfo.StringSerializer.GetDeserializedTimeZoneInfo(source);
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x0004032E File Offset: 0x0003E52E
		public override int GetHashCode()
		{
			return StringComparer.OrdinalIgnoreCase.GetHashCode(this._id);
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x00040340 File Offset: 0x0003E540
		public static ReadOnlyCollection<TimeZoneInfo> GetSystemTimeZones()
		{
			TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
			TimeZoneInfo.CachedData cachedData2 = cachedData;
			lock (cachedData2)
			{
				if (cachedData._readOnlySystemTimeZones == null)
				{
					TimeZoneInfo.PopulateAllSystemTimeZones(cachedData);
					cachedData._allSystemTimeZonesRead = true;
					List<TimeZoneInfo> list;
					if (cachedData._systemTimeZones != null)
					{
						list = new List<TimeZoneInfo>(cachedData._systemTimeZones.Values);
					}
					else
					{
						list = new List<TimeZoneInfo>();
					}
					list.Sort(delegate(TimeZoneInfo x, TimeZoneInfo y)
					{
						int num = x.BaseUtcOffset.CompareTo(y.BaseUtcOffset);
						if (num != 0)
						{
							return num;
						}
						return string.CompareOrdinal(x.DisplayName, y.DisplayName);
					});
					cachedData._readOnlySystemTimeZones = new ReadOnlyCollection<TimeZoneInfo>(list);
				}
			}
			return cachedData._readOnlySystemTimeZones;
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x000403E8 File Offset: 0x0003E5E8
		public bool HasSameRules(TimeZoneInfo other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this._baseUtcOffset != other._baseUtcOffset || this._supportsDaylightSavingTime != other._supportsDaylightSavingTime)
			{
				return false;
			}
			TimeZoneInfo.AdjustmentRule[] adjustmentRules = this._adjustmentRules;
			TimeZoneInfo.AdjustmentRule[] adjustmentRules2 = other._adjustmentRules;
			bool flag = (adjustmentRules == null && adjustmentRules2 == null) || (adjustmentRules != null && adjustmentRules2 != null);
			if (!flag)
			{
				return false;
			}
			if (adjustmentRules != null)
			{
				if (adjustmentRules.Length != adjustmentRules2.Length)
				{
					return false;
				}
				for (int i = 0; i < adjustmentRules.Length; i++)
				{
					if (!adjustmentRules[i].Equals(adjustmentRules2[i]))
					{
						return false;
					}
				}
			}
			return flag;
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000FAE RID: 4014 RVA: 0x00040478 File Offset: 0x0003E678
		public static TimeZoneInfo Local
		{
			get
			{
				return TimeZoneInfo.s_cachedData.Local;
			}
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x00040484 File Offset: 0x0003E684
		public string ToSerializedString()
		{
			return TimeZoneInfo.StringSerializer.GetSerializedString(this);
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x0004048C File Offset: 0x0003E68C
		public override string ToString()
		{
			return this.DisplayName;
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000FB1 RID: 4017 RVA: 0x00040494 File Offset: 0x0003E694
		public static TimeZoneInfo Utc
		{
			get
			{
				return TimeZoneInfo.s_utcTimeZone;
			}
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x0004049C File Offset: 0x0003E69C
		private TimeZoneInfo(string id, TimeSpan baseUtcOffset, string displayName, string standardDisplayName, string daylightDisplayName, TimeZoneInfo.AdjustmentRule[] adjustmentRules, bool disableDaylightSavingTime)
		{
			bool flag;
			TimeZoneInfo.ValidateTimeZoneInfo(id, baseUtcOffset, adjustmentRules, out flag);
			this._id = id;
			this._baseUtcOffset = baseUtcOffset;
			this._displayName = displayName;
			this._standardDisplayName = standardDisplayName;
			this._daylightDisplayName = (disableDaylightSavingTime ? null : daylightDisplayName);
			this._supportsDaylightSavingTime = flag && !disableDaylightSavingTime;
			this._adjustmentRules = adjustmentRules;
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x000404FF File Offset: 0x0003E6FF
		public static TimeZoneInfo CreateCustomTimeZone(string id, TimeSpan baseUtcOffset, string displayName, string standardDisplayName)
		{
			return new TimeZoneInfo(id, baseUtcOffset, displayName, standardDisplayName, standardDisplayName, null, false);
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x0004050D File Offset: 0x0003E70D
		public static TimeZoneInfo CreateCustomTimeZone(string id, TimeSpan baseUtcOffset, string displayName, string standardDisplayName, string daylightDisplayName, TimeZoneInfo.AdjustmentRule[] adjustmentRules)
		{
			return TimeZoneInfo.CreateCustomTimeZone(id, baseUtcOffset, displayName, standardDisplayName, daylightDisplayName, adjustmentRules, false);
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x0004051D File Offset: 0x0003E71D
		public static TimeZoneInfo CreateCustomTimeZone(string id, TimeSpan baseUtcOffset, string displayName, string standardDisplayName, string daylightDisplayName, TimeZoneInfo.AdjustmentRule[] adjustmentRules, bool disableDaylightSavingTime)
		{
			if (!disableDaylightSavingTime && adjustmentRules != null && adjustmentRules.Length != 0)
			{
				adjustmentRules = (TimeZoneInfo.AdjustmentRule[])adjustmentRules.Clone();
			}
			return new TimeZoneInfo(id, baseUtcOffset, displayName, standardDisplayName, daylightDisplayName, adjustmentRules, disableDaylightSavingTime);
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x0004054C File Offset: 0x0003E74C
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			try
			{
				bool flag;
				TimeZoneInfo.ValidateTimeZoneInfo(this._id, this._baseUtcOffset, this._adjustmentRules, out flag);
				if (flag != this._supportsDaylightSavingTime)
				{
					throw new SerializationException(SR.Format("The value of the field '{0}' is invalid.  The serialized data is corrupt.", "SupportsDaylightSavingTime"));
				}
			}
			catch (ArgumentException ex)
			{
				throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.", ex);
			}
			catch (InvalidTimeZoneException ex2)
			{
				throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.", ex2);
			}
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x000405C8 File Offset: 0x0003E7C8
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("Id", this._id);
			info.AddValue("DisplayName", this._displayName);
			info.AddValue("StandardName", this._standardDisplayName);
			info.AddValue("DaylightName", this._daylightDisplayName);
			info.AddValue("BaseUtcOffset", this._baseUtcOffset);
			info.AddValue("AdjustmentRules", this._adjustmentRules);
			info.AddValue("SupportsDaylightSavingTime", this._supportsDaylightSavingTime);
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x00040660 File Offset: 0x0003E860
		private TimeZoneInfo(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this._id = (string)info.GetValue("Id", typeof(string));
			this._displayName = (string)info.GetValue("DisplayName", typeof(string));
			this._standardDisplayName = (string)info.GetValue("StandardName", typeof(string));
			this._daylightDisplayName = (string)info.GetValue("DaylightName", typeof(string));
			this._baseUtcOffset = (TimeSpan)info.GetValue("BaseUtcOffset", typeof(TimeSpan));
			this._adjustmentRules = (TimeZoneInfo.AdjustmentRule[])info.GetValue("AdjustmentRules", typeof(TimeZoneInfo.AdjustmentRule[]));
			this._supportsDaylightSavingTime = (bool)info.GetValue("SupportsDaylightSavingTime", typeof(bool));
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x00040761 File Offset: 0x0003E961
		private TimeZoneInfo.AdjustmentRule GetAdjustmentRuleForTime(DateTime dateTime, out int? ruleIndex)
		{
			return this.GetAdjustmentRuleForTime(dateTime, false, out ruleIndex);
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x0004076C File Offset: 0x0003E96C
		private TimeZoneInfo.AdjustmentRule GetAdjustmentRuleForTime(DateTime dateTime, bool dateTimeisUtc, out int? ruleIndex)
		{
			if (this._adjustmentRules == null || this._adjustmentRules.Length == 0)
			{
				ruleIndex = null;
				return null;
			}
			DateTime dateTime2 = (dateTimeisUtc ? (dateTime + this.BaseUtcOffset).Date : dateTime.Date);
			int i = 0;
			int num = this._adjustmentRules.Length - 1;
			while (i <= num)
			{
				int num2 = i + (num - i >> 1);
				TimeZoneInfo.AdjustmentRule adjustmentRule = this._adjustmentRules[num2];
				TimeZoneInfo.AdjustmentRule adjustmentRule2 = ((num2 > 0) ? this._adjustmentRules[num2 - 1] : adjustmentRule);
				int num3 = this.CompareAdjustmentRuleToDateTime(adjustmentRule, adjustmentRule2, dateTime, dateTime2, dateTimeisUtc);
				if (num3 == 0)
				{
					ruleIndex = new int?(num2);
					return adjustmentRule;
				}
				if (num3 < 0)
				{
					i = num2 + 1;
				}
				else
				{
					num = num2 - 1;
				}
			}
			ruleIndex = null;
			return null;
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x00040830 File Offset: 0x0003EA30
		private int CompareAdjustmentRuleToDateTime(TimeZoneInfo.AdjustmentRule rule, TimeZoneInfo.AdjustmentRule previousRule, DateTime dateTime, DateTime dateOnly, bool dateTimeisUtc)
		{
			bool flag;
			if (rule.DateStart.Kind == DateTimeKind.Utc)
			{
				flag = (dateTimeisUtc ? dateTime : this.ConvertToUtc(dateTime, previousRule.DaylightDelta, previousRule.BaseUtcOffsetDelta)) >= rule.DateStart;
			}
			else
			{
				flag = dateOnly >= rule.DateStart;
			}
			if (!flag)
			{
				return 1;
			}
			bool flag2;
			if (rule.DateEnd.Kind == DateTimeKind.Utc)
			{
				flag2 = (dateTimeisUtc ? dateTime : this.ConvertToUtc(dateTime, rule.DaylightDelta, rule.BaseUtcOffsetDelta)) <= rule.DateEnd;
			}
			else
			{
				flag2 = dateOnly <= rule.DateEnd;
			}
			if (!flag2)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x000408D6 File Offset: 0x0003EAD6
		private DateTime ConvertToUtc(DateTime dateTime, TimeSpan daylightDelta, TimeSpan baseUtcOffsetDelta)
		{
			return this.ConvertToFromUtc(dateTime, daylightDelta, baseUtcOffsetDelta, true);
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x000408E2 File Offset: 0x0003EAE2
		private DateTime ConvertFromUtc(DateTime dateTime, TimeSpan daylightDelta, TimeSpan baseUtcOffsetDelta)
		{
			return this.ConvertToFromUtc(dateTime, daylightDelta, baseUtcOffsetDelta, false);
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x000408F0 File Offset: 0x0003EAF0
		private DateTime ConvertToFromUtc(DateTime dateTime, TimeSpan daylightDelta, TimeSpan baseUtcOffsetDelta, bool convertToUtc)
		{
			TimeSpan timeSpan = this.BaseUtcOffset + daylightDelta + baseUtcOffsetDelta;
			if (convertToUtc)
			{
				timeSpan = timeSpan.Negate();
			}
			long num = dateTime.Ticks + timeSpan.Ticks;
			if (num > DateTime.MaxValue.Ticks)
			{
				return DateTime.MaxValue;
			}
			if (num >= DateTime.MinValue.Ticks)
			{
				return new DateTime(num);
			}
			return DateTime.MinValue;
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x00040958 File Offset: 0x0003EB58
		private static DateTime ConvertUtcToTimeZone(long ticks, TimeZoneInfo destinationTimeZone, out bool isAmbiguousLocalDst)
		{
			ticks += TimeZoneInfo.GetUtcOffsetFromUtc((ticks > DateTime.MaxValue.Ticks) ? DateTime.MaxValue : ((ticks < DateTime.MinValue.Ticks) ? DateTime.MinValue : new DateTime(ticks)), destinationTimeZone, out isAmbiguousLocalDst).Ticks;
			if (ticks > DateTime.MaxValue.Ticks)
			{
				return DateTime.MaxValue;
			}
			if (ticks >= DateTime.MinValue.Ticks)
			{
				return new DateTime(ticks);
			}
			return DateTime.MinValue;
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x000409D4 File Offset: 0x0003EBD4
		private DaylightTimeStruct GetDaylightTime(int year, TimeZoneInfo.AdjustmentRule rule, int? ruleIndex)
		{
			TimeSpan daylightDelta = rule.DaylightDelta;
			DateTime dateTime;
			DateTime dateTime2;
			if (rule.NoDaylightTransitions)
			{
				TimeZoneInfo.AdjustmentRule previousAdjustmentRule = this.GetPreviousAdjustmentRule(rule, ruleIndex);
				dateTime = this.ConvertFromUtc(rule.DateStart, previousAdjustmentRule.DaylightDelta, previousAdjustmentRule.BaseUtcOffsetDelta);
				dateTime2 = this.ConvertFromUtc(rule.DateEnd, rule.DaylightDelta, rule.BaseUtcOffsetDelta);
			}
			else
			{
				dateTime = TimeZoneInfo.TransitionTimeToDateTime(year, rule.DaylightTransitionStart);
				dateTime2 = TimeZoneInfo.TransitionTimeToDateTime(year, rule.DaylightTransitionEnd);
			}
			return new DaylightTimeStruct(dateTime, dateTime2, daylightDelta);
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x00040A50 File Offset: 0x0003EC50
		private static bool GetIsDaylightSavings(DateTime time, TimeZoneInfo.AdjustmentRule rule, DaylightTimeStruct daylightTime, TimeZoneInfoOptions flags)
		{
			if (rule == null)
			{
				return false;
			}
			DateTime dateTime;
			DateTime dateTime2;
			if (time.Kind == DateTimeKind.Local)
			{
				dateTime = (rule.IsStartDateMarkerForBeginningOfYear() ? new DateTime(daylightTime.Start.Year, 1, 1, 0, 0, 0) : (daylightTime.Start + daylightTime.Delta));
				dateTime2 = (rule.IsEndDateMarkerForEndOfYear() ? new DateTime(daylightTime.End.Year + 1, 1, 1, 0, 0, 0).AddTicks(-1L) : daylightTime.End);
			}
			else
			{
				bool flag = rule.DaylightDelta > TimeSpan.Zero;
				dateTime = (rule.IsStartDateMarkerForBeginningOfYear() ? new DateTime(daylightTime.Start.Year, 1, 1, 0, 0, 0) : (daylightTime.Start + (flag ? rule.DaylightDelta : TimeSpan.Zero)));
				dateTime2 = (rule.IsEndDateMarkerForEndOfYear() ? new DateTime(daylightTime.End.Year + 1, 1, 1, 0, 0, 0).AddTicks(-1L) : (daylightTime.End + (flag ? (-rule.DaylightDelta) : TimeSpan.Zero)));
			}
			bool flag2 = TimeZoneInfo.CheckIsDst(dateTime, time, dateTime2, false, rule);
			if (flag2 && time.Kind == DateTimeKind.Local && TimeZoneInfo.GetIsAmbiguousTime(time, rule, daylightTime))
			{
				flag2 = time.IsAmbiguousDaylightSavingTime();
			}
			return flag2;
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x00040BA0 File Offset: 0x0003EDA0
		private TimeSpan GetDaylightSavingsStartOffsetFromUtc(TimeSpan baseUtcOffset, TimeZoneInfo.AdjustmentRule rule, int? ruleIndex)
		{
			if (rule.NoDaylightTransitions)
			{
				TimeZoneInfo.AdjustmentRule previousAdjustmentRule = this.GetPreviousAdjustmentRule(rule, ruleIndex);
				return baseUtcOffset + previousAdjustmentRule.BaseUtcOffsetDelta + previousAdjustmentRule.DaylightDelta;
			}
			return baseUtcOffset + rule.BaseUtcOffsetDelta;
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x00040BE2 File Offset: 0x0003EDE2
		private TimeSpan GetDaylightSavingsEndOffsetFromUtc(TimeSpan baseUtcOffset, TimeZoneInfo.AdjustmentRule rule)
		{
			return baseUtcOffset + rule.BaseUtcOffsetDelta + rule.DaylightDelta;
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x00040BFC File Offset: 0x0003EDFC
		private static bool GetIsDaylightSavingsFromUtc(DateTime time, int year, TimeSpan utc, TimeZoneInfo.AdjustmentRule rule, int? ruleIndex, out bool isAmbiguousLocalDst, TimeZoneInfo zone)
		{
			isAmbiguousLocalDst = false;
			if (rule == null)
			{
				return false;
			}
			DaylightTimeStruct daylightTime = zone.GetDaylightTime(year, rule, ruleIndex);
			bool flag = false;
			TimeSpan daylightSavingsStartOffsetFromUtc = zone.GetDaylightSavingsStartOffsetFromUtc(utc, rule, ruleIndex);
			DateTime dateTime;
			if (rule.IsStartDateMarkerForBeginningOfYear() && daylightTime.Start.Year > DateTime.MinValue.Year)
			{
				int? num;
				TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = zone.GetAdjustmentRuleForTime(new DateTime(daylightTime.Start.Year - 1, 12, 31), out num);
				if (adjustmentRuleForTime != null && adjustmentRuleForTime.IsEndDateMarkerForEndOfYear())
				{
					dateTime = zone.GetDaylightTime(daylightTime.Start.Year - 1, adjustmentRuleForTime, num).Start - utc - adjustmentRuleForTime.BaseUtcOffsetDelta;
					flag = true;
				}
				else
				{
					dateTime = new DateTime(daylightTime.Start.Year, 1, 1, 0, 0, 0) - daylightSavingsStartOffsetFromUtc;
				}
			}
			else
			{
				dateTime = daylightTime.Start - daylightSavingsStartOffsetFromUtc;
			}
			TimeSpan daylightSavingsEndOffsetFromUtc = zone.GetDaylightSavingsEndOffsetFromUtc(utc, rule);
			DateTime dateTime2;
			if (rule.IsEndDateMarkerForEndOfYear() && daylightTime.End.Year < DateTime.MaxValue.Year)
			{
				int? num2;
				TimeZoneInfo.AdjustmentRule adjustmentRuleForTime2 = zone.GetAdjustmentRuleForTime(new DateTime(daylightTime.End.Year + 1, 1, 1), out num2);
				if (adjustmentRuleForTime2 != null && adjustmentRuleForTime2.IsStartDateMarkerForBeginningOfYear())
				{
					if (adjustmentRuleForTime2.IsEndDateMarkerForEndOfYear())
					{
						dateTime2 = new DateTime(daylightTime.End.Year + 1, 12, 31) - utc - adjustmentRuleForTime2.BaseUtcOffsetDelta - adjustmentRuleForTime2.DaylightDelta;
					}
					else
					{
						dateTime2 = zone.GetDaylightTime(daylightTime.End.Year + 1, adjustmentRuleForTime2, num2).End - utc - adjustmentRuleForTime2.BaseUtcOffsetDelta - adjustmentRuleForTime2.DaylightDelta;
					}
					flag = true;
				}
				else
				{
					dateTime2 = new DateTime(daylightTime.End.Year + 1, 1, 1, 0, 0, 0).AddTicks(-1L) - daylightSavingsEndOffsetFromUtc;
				}
			}
			else
			{
				dateTime2 = daylightTime.End - daylightSavingsEndOffsetFromUtc;
			}
			DateTime dateTime3;
			DateTime dateTime4;
			if (daylightTime.Delta.Ticks > 0L)
			{
				dateTime3 = dateTime2 - daylightTime.Delta;
				dateTime4 = dateTime2;
			}
			else
			{
				dateTime3 = dateTime;
				dateTime4 = dateTime - daylightTime.Delta;
			}
			bool flag2 = TimeZoneInfo.CheckIsDst(dateTime, time, dateTime2, flag, rule);
			if (flag2)
			{
				isAmbiguousLocalDst = time >= dateTime3 && time < dateTime4;
				if (!isAmbiguousLocalDst && dateTime3.Year != dateTime4.Year)
				{
					try
					{
						dateTime3.AddYears(1);
						dateTime4.AddYears(1);
						isAmbiguousLocalDst = time >= dateTime3 && time < dateTime4;
					}
					catch (ArgumentOutOfRangeException)
					{
					}
					if (!isAmbiguousLocalDst)
					{
						try
						{
							dateTime3.AddYears(-1);
							dateTime4.AddYears(-1);
							isAmbiguousLocalDst = time >= dateTime3 && time < dateTime4;
						}
						catch (ArgumentOutOfRangeException)
						{
						}
					}
				}
			}
			return flag2;
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x00040F00 File Offset: 0x0003F100
		private static bool CheckIsDst(DateTime startTime, DateTime time, DateTime endTime, bool ignoreYearAdjustment, TimeZoneInfo.AdjustmentRule rule)
		{
			if (!ignoreYearAdjustment && !rule.NoDaylightTransitions)
			{
				int year = startTime.Year;
				int year2 = endTime.Year;
				if (year != year2)
				{
					endTime = endTime.AddYears(year - year2);
				}
				int year3 = time.Year;
				if (year != year3)
				{
					time = time.AddYears(year - year3);
				}
			}
			if (startTime > endTime)
			{
				return time < endTime || time >= startTime;
			}
			if (rule.NoDaylightTransitions)
			{
				return time >= startTime && time <= endTime;
			}
			return time >= startTime && time < endTime;
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x00040F9C File Offset: 0x0003F19C
		private static bool GetIsAmbiguousTime(DateTime time, TimeZoneInfo.AdjustmentRule rule, DaylightTimeStruct daylightTime)
		{
			bool flag = false;
			if (rule == null || rule.DaylightDelta == TimeSpan.Zero)
			{
				return flag;
			}
			DateTime dateTime;
			DateTime dateTime2;
			if (rule.DaylightDelta > TimeSpan.Zero)
			{
				if (rule.IsEndDateMarkerForEndOfYear())
				{
					return false;
				}
				dateTime = daylightTime.End;
				dateTime2 = daylightTime.End - rule.DaylightDelta;
			}
			else
			{
				if (rule.IsStartDateMarkerForBeginningOfYear())
				{
					return false;
				}
				dateTime = daylightTime.Start;
				dateTime2 = daylightTime.Start + rule.DaylightDelta;
			}
			flag = time >= dateTime2 && time < dateTime;
			if (!flag && dateTime.Year != dateTime2.Year)
			{
				try
				{
					DateTime dateTime3 = dateTime.AddYears(1);
					DateTime dateTime4 = dateTime2.AddYears(1);
					flag = time >= dateTime4 && time < dateTime3;
				}
				catch (ArgumentOutOfRangeException)
				{
				}
				if (!flag)
				{
					try
					{
						DateTime dateTime3 = dateTime.AddYears(-1);
						DateTime dateTime4 = dateTime2.AddYears(-1);
						flag = time >= dateTime4 && time < dateTime3;
					}
					catch (ArgumentOutOfRangeException)
					{
					}
				}
			}
			return flag;
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x000410C0 File Offset: 0x0003F2C0
		private static bool GetIsInvalidTime(DateTime time, TimeZoneInfo.AdjustmentRule rule, DaylightTimeStruct daylightTime)
		{
			bool flag = false;
			if (rule == null || rule.DaylightDelta == TimeSpan.Zero)
			{
				return flag;
			}
			DateTime dateTime;
			DateTime dateTime2;
			if (rule.DaylightDelta < TimeSpan.Zero)
			{
				if (rule.IsEndDateMarkerForEndOfYear())
				{
					return false;
				}
				dateTime = daylightTime.End;
				dateTime2 = daylightTime.End - rule.DaylightDelta;
			}
			else
			{
				if (rule.IsStartDateMarkerForBeginningOfYear())
				{
					return false;
				}
				dateTime = daylightTime.Start;
				dateTime2 = daylightTime.Start + rule.DaylightDelta;
			}
			flag = time >= dateTime && time < dateTime2;
			if (!flag && dateTime.Year != dateTime2.Year)
			{
				try
				{
					DateTime dateTime3 = dateTime.AddYears(1);
					DateTime dateTime4 = dateTime2.AddYears(1);
					flag = time >= dateTime3 && time < dateTime4;
				}
				catch (ArgumentOutOfRangeException)
				{
				}
				if (!flag)
				{
					try
					{
						DateTime dateTime3 = dateTime.AddYears(-1);
						DateTime dateTime4 = dateTime2.AddYears(-1);
						flag = time >= dateTime3 && time < dateTime4;
					}
					catch (ArgumentOutOfRangeException)
					{
					}
				}
			}
			return flag;
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x000411E4 File Offset: 0x0003F3E4
		private static TimeSpan GetUtcOffset(DateTime time, TimeZoneInfo zone, TimeZoneInfoOptions flags)
		{
			TimeSpan timeSpan = zone.BaseUtcOffset;
			int? num;
			TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = zone.GetAdjustmentRuleForTime(time, out num);
			if (adjustmentRuleForTime != null)
			{
				timeSpan += adjustmentRuleForTime.BaseUtcOffsetDelta;
				if (adjustmentRuleForTime.HasDaylightSaving)
				{
					DaylightTimeStruct daylightTime = zone.GetDaylightTime(time.Year, adjustmentRuleForTime, num);
					bool isDaylightSavings = TimeZoneInfo.GetIsDaylightSavings(time, adjustmentRuleForTime, daylightTime, flags);
					timeSpan += (isDaylightSavings ? adjustmentRuleForTime.DaylightDelta : TimeSpan.Zero);
				}
			}
			return timeSpan;
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x00041250 File Offset: 0x0003F450
		private static TimeSpan GetUtcOffsetFromUtc(DateTime time, TimeZoneInfo zone)
		{
			bool flag;
			return TimeZoneInfo.GetUtcOffsetFromUtc(time, zone, out flag);
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x00041268 File Offset: 0x0003F468
		private static TimeSpan GetUtcOffsetFromUtc(DateTime time, TimeZoneInfo zone, out bool isDaylightSavings)
		{
			bool flag;
			return TimeZoneInfo.GetUtcOffsetFromUtc(time, zone, out isDaylightSavings, out flag);
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x00041280 File Offset: 0x0003F480
		internal static TimeSpan GetUtcOffsetFromUtc(DateTime time, TimeZoneInfo zone, out bool isDaylightSavings, out bool isAmbiguousLocalDst)
		{
			isDaylightSavings = false;
			isAmbiguousLocalDst = false;
			TimeSpan timeSpan = zone.BaseUtcOffset;
			int? num;
			TimeZoneInfo.AdjustmentRule adjustmentRule;
			int num2;
			if (time > TimeZoneInfo.s_maxDateOnly)
			{
				adjustmentRule = zone.GetAdjustmentRuleForTime(DateTime.MaxValue, out num);
				num2 = 9999;
			}
			else if (time < TimeZoneInfo.s_minDateOnly)
			{
				adjustmentRule = zone.GetAdjustmentRuleForTime(DateTime.MinValue, out num);
				num2 = 1;
			}
			else
			{
				adjustmentRule = zone.GetAdjustmentRuleForTime(time, true, out num);
				num2 = (time + timeSpan).Year;
			}
			if (adjustmentRule != null)
			{
				timeSpan += adjustmentRule.BaseUtcOffsetDelta;
				if (adjustmentRule.HasDaylightSaving)
				{
					isDaylightSavings = TimeZoneInfo.GetIsDaylightSavingsFromUtc(time, num2, zone._baseUtcOffset, adjustmentRule, num, out isAmbiguousLocalDst, zone);
					timeSpan += (isDaylightSavings ? adjustmentRule.DaylightDelta : TimeSpan.Zero);
				}
			}
			return timeSpan;
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x0004133C File Offset: 0x0003F53C
		internal static DateTime TransitionTimeToDateTime(int year, TimeZoneInfo.TransitionTime transitionTime)
		{
			DateTime timeOfDay = transitionTime.TimeOfDay;
			DateTime dateTime;
			if (transitionTime.IsFixedDateRule)
			{
				int num = DateTime.DaysInMonth(year, transitionTime.Month);
				dateTime = new DateTime(year, transitionTime.Month, (num < transitionTime.Day) ? num : transitionTime.Day, timeOfDay.Hour, timeOfDay.Minute, timeOfDay.Second, timeOfDay.Millisecond);
			}
			else if (transitionTime.Week <= 4)
			{
				dateTime = new DateTime(year, transitionTime.Month, 1, timeOfDay.Hour, timeOfDay.Minute, timeOfDay.Second, timeOfDay.Millisecond);
				int dayOfWeek = (int)dateTime.DayOfWeek;
				int num2 = transitionTime.DayOfWeek - (DayOfWeek)dayOfWeek;
				if (num2 < 0)
				{
					num2 += 7;
				}
				num2 += 7 * (transitionTime.Week - 1);
				if (num2 > 0)
				{
					dateTime = dateTime.AddDays((double)num2);
				}
			}
			else
			{
				int num3 = DateTime.DaysInMonth(year, transitionTime.Month);
				dateTime = new DateTime(year, transitionTime.Month, num3, timeOfDay.Hour, timeOfDay.Minute, timeOfDay.Second, timeOfDay.Millisecond);
				int num4 = dateTime.DayOfWeek - transitionTime.DayOfWeek;
				if (num4 < 0)
				{
					num4 += 7;
				}
				if (num4 > 0)
				{
					dateTime = dateTime.AddDays((double)(-(double)num4));
				}
			}
			return dateTime;
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x0004148C File Offset: 0x0003F68C
		private static TimeZoneInfo.TimeZoneInfoResult TryGetTimeZone(string id, bool dstDisabled, out TimeZoneInfo value, out Exception e, TimeZoneInfo.CachedData cachedData, bool alwaysFallbackToLocalMachine = false)
		{
			TimeZoneInfo.TimeZoneInfoResult timeZoneInfoResult = TimeZoneInfo.TimeZoneInfoResult.Success;
			e = null;
			TimeZoneInfo timeZoneInfo = null;
			if (cachedData._systemTimeZones != null && cachedData._systemTimeZones.TryGetValue(id, out timeZoneInfo))
			{
				if (dstDisabled && timeZoneInfo._supportsDaylightSavingTime)
				{
					value = TimeZoneInfo.CreateCustomTimeZone(timeZoneInfo._id, timeZoneInfo._baseUtcOffset, timeZoneInfo._displayName, timeZoneInfo._standardDisplayName);
				}
				else
				{
					value = new TimeZoneInfo(timeZoneInfo._id, timeZoneInfo._baseUtcOffset, timeZoneInfo._displayName, timeZoneInfo._standardDisplayName, timeZoneInfo._daylightDisplayName, timeZoneInfo._adjustmentRules, false);
				}
				return timeZoneInfoResult;
			}
			if (!cachedData._allSystemTimeZonesRead || alwaysFallbackToLocalMachine)
			{
				timeZoneInfoResult = TimeZoneInfo.TryGetTimeZoneFromLocalMachine(id, dstDisabled, out value, out e, cachedData);
			}
			else
			{
				timeZoneInfoResult = TimeZoneInfo.TimeZoneInfoResult.TimeZoneNotFoundException;
				value = null;
			}
			return timeZoneInfoResult;
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x00041538 File Offset: 0x0003F738
		private static TimeZoneInfo.TimeZoneInfoResult TryGetTimeZoneFromLocalMachine(string id, bool dstDisabled, out TimeZoneInfo value, out Exception e, TimeZoneInfo.CachedData cachedData)
		{
			TimeZoneInfo timeZoneInfo;
			TimeZoneInfo.TimeZoneInfoResult timeZoneInfoResult = TimeZoneInfo.TryGetTimeZoneFromLocalMachine(id, out timeZoneInfo, out e);
			if (timeZoneInfoResult != TimeZoneInfo.TimeZoneInfoResult.Success)
			{
				value = null;
				return timeZoneInfoResult;
			}
			if (cachedData._systemTimeZones == null)
			{
				cachedData._systemTimeZones = new Dictionary<string, TimeZoneInfo>(StringComparer.OrdinalIgnoreCase);
			}
			cachedData._systemTimeZones.Add(id, timeZoneInfo);
			if (dstDisabled && timeZoneInfo._supportsDaylightSavingTime)
			{
				value = TimeZoneInfo.CreateCustomTimeZone(timeZoneInfo._id, timeZoneInfo._baseUtcOffset, timeZoneInfo._displayName, timeZoneInfo._standardDisplayName);
				return timeZoneInfoResult;
			}
			value = new TimeZoneInfo(timeZoneInfo._id, timeZoneInfo._baseUtcOffset, timeZoneInfo._displayName, timeZoneInfo._standardDisplayName, timeZoneInfo._daylightDisplayName, timeZoneInfo._adjustmentRules, false);
			return timeZoneInfoResult;
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x000415D8 File Offset: 0x0003F7D8
		private static void ValidateTimeZoneInfo(string id, TimeSpan baseUtcOffset, TimeZoneInfo.AdjustmentRule[] adjustmentRules, out bool adjustmentRulesSupportDst)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			if (id.Length == 0)
			{
				throw new ArgumentException(SR.Format("The specified ID parameter '{0}' is not supported.", id), "id");
			}
			if (TimeZoneInfo.UtcOffsetOutOfRange(baseUtcOffset))
			{
				throw new ArgumentOutOfRangeException("baseUtcOffset", "The TimeSpan parameter must be within plus or minus 14.0 hours.");
			}
			if (baseUtcOffset.Ticks % 600000000L != 0L)
			{
				throw new ArgumentException("The TimeSpan parameter cannot be specified more precisely than whole minutes.", "baseUtcOffset");
			}
			adjustmentRulesSupportDst = false;
			if (adjustmentRules != null && adjustmentRules.Length != 0)
			{
				adjustmentRulesSupportDst = true;
				TimeZoneInfo.AdjustmentRule adjustmentRule = null;
				for (int i = 0; i < adjustmentRules.Length; i++)
				{
					TimeZoneInfo.AdjustmentRule adjustmentRule2 = adjustmentRule;
					adjustmentRule = adjustmentRules[i];
					if (adjustmentRule == null)
					{
						throw new InvalidTimeZoneException("The AdjustmentRule array cannot contain null elements.");
					}
					if (!TimeZoneInfo.IsValidAdjustmentRuleOffest(baseUtcOffset, adjustmentRule))
					{
						throw new InvalidTimeZoneException("The sum of the BaseUtcOffset and DaylightDelta properties must within plus or minus 14.0 hours.");
					}
					if (adjustmentRule2 != null && adjustmentRule.DateStart <= adjustmentRule2.DateEnd)
					{
						throw new InvalidTimeZoneException("The elements of the AdjustmentRule array must be in chronological order and must not overlap.");
					}
				}
			}
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x000416B1 File Offset: 0x0003F8B1
		internal static bool UtcOffsetOutOfRange(TimeSpan offset)
		{
			return offset < TimeZoneInfo.MinOffset || offset > TimeZoneInfo.MaxOffset;
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x000416CD File Offset: 0x0003F8CD
		private static TimeSpan GetUtcOffset(TimeSpan baseUtcOffset, TimeZoneInfo.AdjustmentRule adjustmentRule)
		{
			return baseUtcOffset + adjustmentRule.BaseUtcOffsetDelta + (adjustmentRule.HasDaylightSaving ? adjustmentRule.DaylightDelta : TimeSpan.Zero);
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x000416F5 File Offset: 0x0003F8F5
		private static bool IsValidAdjustmentRuleOffest(TimeSpan baseUtcOffset, TimeZoneInfo.AdjustmentRule adjustmentRule)
		{
			return !TimeZoneInfo.UtcOffsetOutOfRange(TimeZoneInfo.GetUtcOffset(baseUtcOffset, adjustmentRule));
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x00041708 File Offset: 0x0003F908
		private static void NormalizeAdjustmentRuleOffset(TimeSpan baseUtcOffset, ref TimeZoneInfo.AdjustmentRule adjustmentRule)
		{
			TimeSpan utcOffset = TimeZoneInfo.GetUtcOffset(baseUtcOffset, adjustmentRule);
			TimeSpan timeSpan = TimeSpan.Zero;
			if (utcOffset > TimeZoneInfo.MaxOffset)
			{
				timeSpan = TimeZoneInfo.MaxOffset - utcOffset;
			}
			else if (utcOffset < TimeZoneInfo.MinOffset)
			{
				timeSpan = TimeZoneInfo.MinOffset - utcOffset;
			}
			if (timeSpan != TimeSpan.Zero)
			{
				adjustmentRule = TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(adjustmentRule.DateStart, adjustmentRule.DateEnd, adjustmentRule.DaylightDelta, adjustmentRule.DaylightTransitionStart, adjustmentRule.DaylightTransitionEnd, adjustmentRule.BaseUtcOffsetDelta + timeSpan, adjustmentRule.NoDaylightTransitions);
			}
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x000417A4 File Offset: 0x0003F9A4
		// Note: this type is marked as 'beforefieldinit'.
		static TimeZoneInfo()
		{
		}

		// Token: 0x040011A8 RID: 4520
		private const string DefaultTimeZoneDirectory = "/usr/share/zoneinfo/";

		// Token: 0x040011A9 RID: 4521
		private const string ZoneTabFileName = "zone.tab";

		// Token: 0x040011AA RID: 4522
		private const string TimeZoneEnvironmentVariable = "TZ";

		// Token: 0x040011AB RID: 4523
		private const string TimeZoneDirectoryEnvironmentVariable = "TZDIR";

		// Token: 0x040011AC RID: 4524
		private readonly string _id;

		// Token: 0x040011AD RID: 4525
		private readonly string _displayName;

		// Token: 0x040011AE RID: 4526
		private readonly string _standardDisplayName;

		// Token: 0x040011AF RID: 4527
		private readonly string _daylightDisplayName;

		// Token: 0x040011B0 RID: 4528
		private readonly TimeSpan _baseUtcOffset;

		// Token: 0x040011B1 RID: 4529
		private readonly bool _supportsDaylightSavingTime;

		// Token: 0x040011B2 RID: 4530
		private readonly TimeZoneInfo.AdjustmentRule[] _adjustmentRules;

		// Token: 0x040011B3 RID: 4531
		private const string UtcId = "UTC";

		// Token: 0x040011B4 RID: 4532
		private const string LocalId = "Local";

		// Token: 0x040011B5 RID: 4533
		private static readonly TimeZoneInfo s_utcTimeZone = TimeZoneInfo.CreateCustomTimeZone("UTC", TimeSpan.Zero, "UTC", "UTC");

		// Token: 0x040011B6 RID: 4534
		private static TimeZoneInfo.CachedData s_cachedData = new TimeZoneInfo.CachedData();

		// Token: 0x040011B7 RID: 4535
		private static readonly DateTime s_maxDateOnly = new DateTime(9999, 12, 31);

		// Token: 0x040011B8 RID: 4536
		private static readonly DateTime s_minDateOnly = new DateTime(1, 1, 2);

		// Token: 0x040011B9 RID: 4537
		private static readonly TimeSpan MaxOffset = TimeSpan.FromHours(14.0);

		// Token: 0x040011BA RID: 4538
		private static readonly TimeSpan MinOffset = -TimeZoneInfo.MaxOffset;

		// Token: 0x0200015F RID: 351
		[Serializable]
		public sealed class AdjustmentRule : IEquatable<TimeZoneInfo.AdjustmentRule>, ISerializable, IDeserializationCallback
		{
			// Token: 0x17000122 RID: 290
			// (get) Token: 0x06000FD5 RID: 4053 RVA: 0x0004181B File Offset: 0x0003FA1B
			public DateTime DateStart
			{
				get
				{
					return this._dateStart;
				}
			}

			// Token: 0x17000123 RID: 291
			// (get) Token: 0x06000FD6 RID: 4054 RVA: 0x00041823 File Offset: 0x0003FA23
			public DateTime DateEnd
			{
				get
				{
					return this._dateEnd;
				}
			}

			// Token: 0x17000124 RID: 292
			// (get) Token: 0x06000FD7 RID: 4055 RVA: 0x0004182B File Offset: 0x0003FA2B
			public TimeSpan DaylightDelta
			{
				get
				{
					return this._daylightDelta;
				}
			}

			// Token: 0x17000125 RID: 293
			// (get) Token: 0x06000FD8 RID: 4056 RVA: 0x00041833 File Offset: 0x0003FA33
			public TimeZoneInfo.TransitionTime DaylightTransitionStart
			{
				get
				{
					return this._daylightTransitionStart;
				}
			}

			// Token: 0x17000126 RID: 294
			// (get) Token: 0x06000FD9 RID: 4057 RVA: 0x0004183B File Offset: 0x0003FA3B
			public TimeZoneInfo.TransitionTime DaylightTransitionEnd
			{
				get
				{
					return this._daylightTransitionEnd;
				}
			}

			// Token: 0x17000127 RID: 295
			// (get) Token: 0x06000FDA RID: 4058 RVA: 0x00041843 File Offset: 0x0003FA43
			internal TimeSpan BaseUtcOffsetDelta
			{
				get
				{
					return this._baseUtcOffsetDelta;
				}
			}

			// Token: 0x17000128 RID: 296
			// (get) Token: 0x06000FDB RID: 4059 RVA: 0x0004184B File Offset: 0x0003FA4B
			internal bool NoDaylightTransitions
			{
				get
				{
					return this._noDaylightTransitions;
				}
			}

			// Token: 0x17000129 RID: 297
			// (get) Token: 0x06000FDC RID: 4060 RVA: 0x00041854 File Offset: 0x0003FA54
			internal bool HasDaylightSaving
			{
				get
				{
					return this.DaylightDelta != TimeSpan.Zero || (this.DaylightTransitionStart != default(TimeZoneInfo.TransitionTime) && this.DaylightTransitionStart.TimeOfDay != DateTime.MinValue) || (this.DaylightTransitionEnd != default(TimeZoneInfo.TransitionTime) && this.DaylightTransitionEnd.TimeOfDay != DateTime.MinValue.AddMilliseconds(1.0));
				}
			}

			// Token: 0x06000FDD RID: 4061 RVA: 0x000418E4 File Offset: 0x0003FAE4
			public bool Equals(TimeZoneInfo.AdjustmentRule other)
			{
				return other != null && this._dateStart == other._dateStart && this._dateEnd == other._dateEnd && this._daylightDelta == other._daylightDelta && this._baseUtcOffsetDelta == other._baseUtcOffsetDelta && this._daylightTransitionEnd.Equals(other._daylightTransitionEnd) && this._daylightTransitionStart.Equals(other._daylightTransitionStart);
			}

			// Token: 0x06000FDE RID: 4062 RVA: 0x00041966 File Offset: 0x0003FB66
			public override int GetHashCode()
			{
				return this._dateStart.GetHashCode();
			}

			// Token: 0x06000FDF RID: 4063 RVA: 0x00041974 File Offset: 0x0003FB74
			private AdjustmentRule(DateTime dateStart, DateTime dateEnd, TimeSpan daylightDelta, TimeZoneInfo.TransitionTime daylightTransitionStart, TimeZoneInfo.TransitionTime daylightTransitionEnd, TimeSpan baseUtcOffsetDelta, bool noDaylightTransitions)
			{
				TimeZoneInfo.AdjustmentRule.ValidateAdjustmentRule(dateStart, dateEnd, daylightDelta, daylightTransitionStart, daylightTransitionEnd, noDaylightTransitions);
				this._dateStart = dateStart;
				this._dateEnd = dateEnd;
				this._daylightDelta = daylightDelta;
				this._daylightTransitionStart = daylightTransitionStart;
				this._daylightTransitionEnd = daylightTransitionEnd;
				this._baseUtcOffsetDelta = baseUtcOffsetDelta;
				this._noDaylightTransitions = noDaylightTransitions;
			}

			// Token: 0x06000FE0 RID: 4064 RVA: 0x000419CA File Offset: 0x0003FBCA
			public static TimeZoneInfo.AdjustmentRule CreateAdjustmentRule(DateTime dateStart, DateTime dateEnd, TimeSpan daylightDelta, TimeZoneInfo.TransitionTime daylightTransitionStart, TimeZoneInfo.TransitionTime daylightTransitionEnd)
			{
				return new TimeZoneInfo.AdjustmentRule(dateStart, dateEnd, daylightDelta, daylightTransitionStart, daylightTransitionEnd, TimeSpan.Zero, false);
			}

			// Token: 0x06000FE1 RID: 4065 RVA: 0x000419DD File Offset: 0x0003FBDD
			internal static TimeZoneInfo.AdjustmentRule CreateAdjustmentRule(DateTime dateStart, DateTime dateEnd, TimeSpan daylightDelta, TimeZoneInfo.TransitionTime daylightTransitionStart, TimeZoneInfo.TransitionTime daylightTransitionEnd, TimeSpan baseUtcOffsetDelta, bool noDaylightTransitions)
			{
				return new TimeZoneInfo.AdjustmentRule(dateStart, dateEnd, daylightDelta, daylightTransitionStart, daylightTransitionEnd, baseUtcOffsetDelta, noDaylightTransitions);
			}

			// Token: 0x06000FE2 RID: 4066 RVA: 0x000419F0 File Offset: 0x0003FBF0
			internal bool IsStartDateMarkerForBeginningOfYear()
			{
				return !this.NoDaylightTransitions && this.DaylightTransitionStart.Month == 1 && this.DaylightTransitionStart.Day == 1 && this.DaylightTransitionStart.TimeOfDay.Hour == 0 && this.DaylightTransitionStart.TimeOfDay.Minute == 0 && this.DaylightTransitionStart.TimeOfDay.Second == 0 && this._dateStart.Year == this._dateEnd.Year;
			}

			// Token: 0x06000FE3 RID: 4067 RVA: 0x00041A8C File Offset: 0x0003FC8C
			internal bool IsEndDateMarkerForEndOfYear()
			{
				return !this.NoDaylightTransitions && this.DaylightTransitionEnd.Month == 1 && this.DaylightTransitionEnd.Day == 1 && this.DaylightTransitionEnd.TimeOfDay.Hour == 0 && this.DaylightTransitionEnd.TimeOfDay.Minute == 0 && this.DaylightTransitionEnd.TimeOfDay.Second == 0 && this._dateStart.Year == this._dateEnd.Year;
			}

			// Token: 0x06000FE4 RID: 4068 RVA: 0x00041B28 File Offset: 0x0003FD28
			private static void ValidateAdjustmentRule(DateTime dateStart, DateTime dateEnd, TimeSpan daylightDelta, TimeZoneInfo.TransitionTime daylightTransitionStart, TimeZoneInfo.TransitionTime daylightTransitionEnd, bool noDaylightTransitions)
			{
				if (dateStart.Kind != DateTimeKind.Unspecified && dateStart.Kind != DateTimeKind.Utc)
				{
					throw new ArgumentException("The supplied DateTime must have the Kind property set to DateTimeKind.Unspecified or DateTimeKind.Utc.", "dateStart");
				}
				if (dateEnd.Kind != DateTimeKind.Unspecified && dateEnd.Kind != DateTimeKind.Utc)
				{
					throw new ArgumentException("The supplied DateTime must have the Kind property set to DateTimeKind.Unspecified or DateTimeKind.Utc.", "dateEnd");
				}
				if (daylightTransitionStart.Equals(daylightTransitionEnd) && !noDaylightTransitions)
				{
					throw new ArgumentException("The DaylightTransitionStart property must not equal the DaylightTransitionEnd property.", "daylightTransitionEnd");
				}
				if (dateStart > dateEnd)
				{
					throw new ArgumentException("The DateStart property must come before the DateEnd property.", "dateStart");
				}
				if (daylightDelta.TotalHours < -23.0 || daylightDelta.TotalHours > 14.0)
				{
					throw new ArgumentOutOfRangeException("daylightDelta", daylightDelta, "The TimeSpan parameter must be within plus or minus 14.0 hours.");
				}
				if (daylightDelta.Ticks % 600000000L != 0L)
				{
					throw new ArgumentException("The TimeSpan parameter cannot be specified more precisely than whole minutes.", "daylightDelta");
				}
				if (dateStart != DateTime.MinValue && dateStart.Kind == DateTimeKind.Unspecified && dateStart.TimeOfDay != TimeSpan.Zero)
				{
					throw new ArgumentException("The supplied DateTime includes a TimeOfDay setting.   This is not supported.", "dateStart");
				}
				if (dateEnd != DateTime.MaxValue && dateEnd.Kind == DateTimeKind.Unspecified && dateEnd.TimeOfDay != TimeSpan.Zero)
				{
					throw new ArgumentException("The supplied DateTime includes a TimeOfDay setting.   This is not supported.", "dateEnd");
				}
			}

			// Token: 0x06000FE5 RID: 4069 RVA: 0x00041C80 File Offset: 0x0003FE80
			void IDeserializationCallback.OnDeserialization(object sender)
			{
				try
				{
					TimeZoneInfo.AdjustmentRule.ValidateAdjustmentRule(this._dateStart, this._dateEnd, this._daylightDelta, this._daylightTransitionStart, this._daylightTransitionEnd, this._noDaylightTransitions);
				}
				catch (ArgumentException ex)
				{
					throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.", ex);
				}
			}

			// Token: 0x06000FE6 RID: 4070 RVA: 0x00041CD8 File Offset: 0x0003FED8
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				info.AddValue("DateStart", this._dateStart);
				info.AddValue("DateEnd", this._dateEnd);
				info.AddValue("DaylightDelta", this._daylightDelta);
				info.AddValue("DaylightTransitionStart", this._daylightTransitionStart);
				info.AddValue("DaylightTransitionEnd", this._daylightTransitionEnd);
				info.AddValue("BaseUtcOffsetDelta", this._baseUtcOffsetDelta);
				info.AddValue("NoDaylightTransitions", this._noDaylightTransitions);
			}

			// Token: 0x06000FE7 RID: 4071 RVA: 0x00041D80 File Offset: 0x0003FF80
			private AdjustmentRule(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				this._dateStart = (DateTime)info.GetValue("DateStart", typeof(DateTime));
				this._dateEnd = (DateTime)info.GetValue("DateEnd", typeof(DateTime));
				this._daylightDelta = (TimeSpan)info.GetValue("DaylightDelta", typeof(TimeSpan));
				this._daylightTransitionStart = (TimeZoneInfo.TransitionTime)info.GetValue("DaylightTransitionStart", typeof(TimeZoneInfo.TransitionTime));
				this._daylightTransitionEnd = (TimeZoneInfo.TransitionTime)info.GetValue("DaylightTransitionEnd", typeof(TimeZoneInfo.TransitionTime));
				object obj = info.GetValueNoThrow("BaseUtcOffsetDelta", typeof(TimeSpan));
				if (obj != null)
				{
					this._baseUtcOffsetDelta = (TimeSpan)obj;
				}
				obj = info.GetValueNoThrow("NoDaylightTransitions", typeof(bool));
				if (obj != null)
				{
					this._noDaylightTransitions = (bool)obj;
				}
			}

			// Token: 0x040011BB RID: 4539
			private readonly DateTime _dateStart;

			// Token: 0x040011BC RID: 4540
			private readonly DateTime _dateEnd;

			// Token: 0x040011BD RID: 4541
			private readonly TimeSpan _daylightDelta;

			// Token: 0x040011BE RID: 4542
			private readonly TimeZoneInfo.TransitionTime _daylightTransitionStart;

			// Token: 0x040011BF RID: 4543
			private readonly TimeZoneInfo.TransitionTime _daylightTransitionEnd;

			// Token: 0x040011C0 RID: 4544
			private readonly TimeSpan _baseUtcOffsetDelta;

			// Token: 0x040011C1 RID: 4545
			private readonly bool _noDaylightTransitions;
		}

		// Token: 0x02000160 RID: 352
		private struct StringSerializer
		{
			// Token: 0x06000FE8 RID: 4072 RVA: 0x00041E8C File Offset: 0x0004008C
			public static string GetSerializedString(TimeZoneInfo zone)
			{
				StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
				TimeZoneInfo.StringSerializer.SerializeSubstitute(zone.Id, stringBuilder);
				stringBuilder.Append(';');
				stringBuilder.Append(zone.BaseUtcOffset.TotalMinutes.ToString(CultureInfo.InvariantCulture));
				stringBuilder.Append(';');
				TimeZoneInfo.StringSerializer.SerializeSubstitute(zone.DisplayName, stringBuilder);
				stringBuilder.Append(';');
				TimeZoneInfo.StringSerializer.SerializeSubstitute(zone.StandardName, stringBuilder);
				stringBuilder.Append(';');
				TimeZoneInfo.StringSerializer.SerializeSubstitute(zone.DaylightName, stringBuilder);
				stringBuilder.Append(';');
				foreach (TimeZoneInfo.AdjustmentRule adjustmentRule in zone.GetAdjustmentRules())
				{
					stringBuilder.Append('[');
					stringBuilder.Append(adjustmentRule.DateStart.ToString("MM:dd:yyyy", DateTimeFormatInfo.InvariantInfo));
					stringBuilder.Append(';');
					stringBuilder.Append(adjustmentRule.DateEnd.ToString("MM:dd:yyyy", DateTimeFormatInfo.InvariantInfo));
					stringBuilder.Append(';');
					stringBuilder.Append(adjustmentRule.DaylightDelta.TotalMinutes.ToString(CultureInfo.InvariantCulture));
					stringBuilder.Append(';');
					TimeZoneInfo.StringSerializer.SerializeTransitionTime(adjustmentRule.DaylightTransitionStart, stringBuilder);
					stringBuilder.Append(';');
					TimeZoneInfo.StringSerializer.SerializeTransitionTime(adjustmentRule.DaylightTransitionEnd, stringBuilder);
					stringBuilder.Append(';');
					if (adjustmentRule.BaseUtcOffsetDelta != TimeSpan.Zero)
					{
						stringBuilder.Append(adjustmentRule.BaseUtcOffsetDelta.TotalMinutes.ToString(CultureInfo.InvariantCulture));
						stringBuilder.Append(';');
					}
					if (adjustmentRule.NoDaylightTransitions)
					{
						stringBuilder.Append('1');
						stringBuilder.Append(';');
					}
					stringBuilder.Append(']');
				}
				stringBuilder.Append(';');
				return StringBuilderCache.GetStringAndRelease(stringBuilder);
			}

			// Token: 0x06000FE9 RID: 4073 RVA: 0x0004206C File Offset: 0x0004026C
			public static TimeZoneInfo GetDeserializedTimeZoneInfo(string source)
			{
				TimeZoneInfo.StringSerializer stringSerializer = new TimeZoneInfo.StringSerializer(source);
				string nextStringValue = stringSerializer.GetNextStringValue();
				TimeSpan nextTimeSpanValue = stringSerializer.GetNextTimeSpanValue();
				string nextStringValue2 = stringSerializer.GetNextStringValue();
				string nextStringValue3 = stringSerializer.GetNextStringValue();
				string nextStringValue4 = stringSerializer.GetNextStringValue();
				TimeZoneInfo.AdjustmentRule[] nextAdjustmentRuleArrayValue = stringSerializer.GetNextAdjustmentRuleArrayValue();
				TimeZoneInfo timeZoneInfo;
				try
				{
					timeZoneInfo = new TimeZoneInfo(nextStringValue, nextTimeSpanValue, nextStringValue2, nextStringValue3, nextStringValue4, nextAdjustmentRuleArrayValue, false);
				}
				catch (ArgumentException ex)
				{
					throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.", ex);
				}
				catch (InvalidTimeZoneException ex2)
				{
					throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.", ex2);
				}
				return timeZoneInfo;
			}

			// Token: 0x06000FEA RID: 4074 RVA: 0x00042104 File Offset: 0x00040304
			private StringSerializer(string str)
			{
				this._serializedText = str;
				this._currentTokenStartIndex = 0;
				this._state = TimeZoneInfo.StringSerializer.State.StartOfToken;
			}

			// Token: 0x06000FEB RID: 4075 RVA: 0x0004211C File Offset: 0x0004031C
			private static void SerializeSubstitute(string text, StringBuilder serializedText)
			{
				foreach (char c in text)
				{
					if (c == '\\' || c == '[' || c == ']' || c == ';')
					{
						serializedText.Append('\\');
					}
					serializedText.Append(c);
				}
			}

			// Token: 0x06000FEC RID: 4076 RVA: 0x0004216C File Offset: 0x0004036C
			private static void SerializeTransitionTime(TimeZoneInfo.TransitionTime time, StringBuilder serializedText)
			{
				serializedText.Append('[');
				serializedText.Append(time.IsFixedDateRule ? '1' : '0');
				serializedText.Append(';');
				serializedText.Append(time.TimeOfDay.ToString("HH:mm:ss.FFF", DateTimeFormatInfo.InvariantInfo));
				serializedText.Append(';');
				serializedText.Append(time.Month.ToString(CultureInfo.InvariantCulture));
				serializedText.Append(';');
				if (time.IsFixedDateRule)
				{
					serializedText.Append(time.Day.ToString(CultureInfo.InvariantCulture));
					serializedText.Append(';');
				}
				else
				{
					serializedText.Append(time.Week.ToString(CultureInfo.InvariantCulture));
					serializedText.Append(';');
					serializedText.Append(((int)time.DayOfWeek).ToString(CultureInfo.InvariantCulture));
					serializedText.Append(';');
				}
				serializedText.Append(']');
			}

			// Token: 0x06000FED RID: 4077 RVA: 0x0004226E File Offset: 0x0004046E
			private static void VerifyIsEscapableCharacter(char c)
			{
				if (c != '\\' && c != ';' && c != '[' && c != ']')
				{
					throw new SerializationException(SR.Format("The serialized data contained an invalid escape sequence '\\\\{0}'.", c));
				}
			}

			// Token: 0x06000FEE RID: 4078 RVA: 0x0004229C File Offset: 0x0004049C
			private void SkipVersionNextDataFields(int depth)
			{
				if (this._currentTokenStartIndex < 0 || this._currentTokenStartIndex >= this._serializedText.Length)
				{
					throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.");
				}
				TimeZoneInfo.StringSerializer.State state = TimeZoneInfo.StringSerializer.State.NotEscaped;
				for (int i = this._currentTokenStartIndex; i < this._serializedText.Length; i++)
				{
					if (state == TimeZoneInfo.StringSerializer.State.Escaped)
					{
						TimeZoneInfo.StringSerializer.VerifyIsEscapableCharacter(this._serializedText[i]);
						state = TimeZoneInfo.StringSerializer.State.NotEscaped;
					}
					else if (state == TimeZoneInfo.StringSerializer.State.NotEscaped)
					{
						char c = this._serializedText[i];
						if (c == '\0')
						{
							throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.");
						}
						switch (c)
						{
						case '[':
							depth++;
							break;
						case '\\':
							state = TimeZoneInfo.StringSerializer.State.Escaped;
							break;
						case ']':
							depth--;
							if (depth == 0)
							{
								this._currentTokenStartIndex = i + 1;
								if (this._currentTokenStartIndex >= this._serializedText.Length)
								{
									this._state = TimeZoneInfo.StringSerializer.State.EndOfLine;
									return;
								}
								this._state = TimeZoneInfo.StringSerializer.State.StartOfToken;
								return;
							}
							break;
						}
					}
				}
				throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.");
			}

			// Token: 0x06000FEF RID: 4079 RVA: 0x0004238C File Offset: 0x0004058C
			private string GetNextStringValue()
			{
				if (this._state == TimeZoneInfo.StringSerializer.State.EndOfLine)
				{
					throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.");
				}
				if (this._currentTokenStartIndex < 0 || this._currentTokenStartIndex >= this._serializedText.Length)
				{
					throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.");
				}
				TimeZoneInfo.StringSerializer.State state = TimeZoneInfo.StringSerializer.State.NotEscaped;
				StringBuilder stringBuilder = StringBuilderCache.Acquire(64);
				for (int i = this._currentTokenStartIndex; i < this._serializedText.Length; i++)
				{
					if (state == TimeZoneInfo.StringSerializer.State.Escaped)
					{
						TimeZoneInfo.StringSerializer.VerifyIsEscapableCharacter(this._serializedText[i]);
						stringBuilder.Append(this._serializedText[i]);
						state = TimeZoneInfo.StringSerializer.State.NotEscaped;
					}
					else if (state == TimeZoneInfo.StringSerializer.State.NotEscaped)
					{
						char c = this._serializedText[i];
						if (c == '\0')
						{
							throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.");
						}
						if (c == ';')
						{
							this._currentTokenStartIndex = i + 1;
							if (this._currentTokenStartIndex >= this._serializedText.Length)
							{
								this._state = TimeZoneInfo.StringSerializer.State.EndOfLine;
							}
							else
							{
								this._state = TimeZoneInfo.StringSerializer.State.StartOfToken;
							}
							return StringBuilderCache.GetStringAndRelease(stringBuilder);
						}
						switch (c)
						{
						case '[':
							throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.");
						case '\\':
							state = TimeZoneInfo.StringSerializer.State.Escaped;
							break;
						case ']':
							throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.");
						default:
							stringBuilder.Append(this._serializedText[i]);
							break;
						}
					}
				}
				if (state == TimeZoneInfo.StringSerializer.State.Escaped)
				{
					throw new SerializationException(SR.Format("The serialized data contained an invalid escape sequence '\\\\{0}'.", string.Empty));
				}
				throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.");
			}

			// Token: 0x06000FF0 RID: 4080 RVA: 0x000424F0 File Offset: 0x000406F0
			private DateTime GetNextDateTimeValue(string format)
			{
				DateTime dateTime;
				if (!DateTime.TryParseExact(this.GetNextStringValue(), format, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out dateTime))
				{
					throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.");
				}
				return dateTime;
			}

			// Token: 0x06000FF1 RID: 4081 RVA: 0x00042520 File Offset: 0x00040720
			private TimeSpan GetNextTimeSpanValue()
			{
				int nextInt32Value = this.GetNextInt32Value();
				TimeSpan timeSpan;
				try
				{
					timeSpan = new TimeSpan(0, nextInt32Value, 0);
				}
				catch (ArgumentOutOfRangeException ex)
				{
					throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.", ex);
				}
				return timeSpan;
			}

			// Token: 0x06000FF2 RID: 4082 RVA: 0x00042560 File Offset: 0x00040760
			private int GetNextInt32Value()
			{
				int num;
				if (!int.TryParse(this.GetNextStringValue(), NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out num))
				{
					throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.");
				}
				return num;
			}

			// Token: 0x06000FF3 RID: 4083 RVA: 0x00042590 File Offset: 0x00040790
			private TimeZoneInfo.AdjustmentRule[] GetNextAdjustmentRuleArrayValue()
			{
				List<TimeZoneInfo.AdjustmentRule> list = new List<TimeZoneInfo.AdjustmentRule>(1);
				int num = 0;
				for (TimeZoneInfo.AdjustmentRule adjustmentRule = this.GetNextAdjustmentRuleValue(); adjustmentRule != null; adjustmentRule = this.GetNextAdjustmentRuleValue())
				{
					list.Add(adjustmentRule);
					num++;
				}
				if (this._state == TimeZoneInfo.StringSerializer.State.EndOfLine)
				{
					throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.");
				}
				if (this._currentTokenStartIndex < 0 || this._currentTokenStartIndex >= this._serializedText.Length)
				{
					throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.");
				}
				if (num == 0)
				{
					return null;
				}
				return list.ToArray();
			}

			// Token: 0x06000FF4 RID: 4084 RVA: 0x0004260C File Offset: 0x0004080C
			private TimeZoneInfo.AdjustmentRule GetNextAdjustmentRuleValue()
			{
				if (this._state == TimeZoneInfo.StringSerializer.State.EndOfLine)
				{
					return null;
				}
				if (this._currentTokenStartIndex < 0 || this._currentTokenStartIndex >= this._serializedText.Length)
				{
					throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.");
				}
				if (this._serializedText[this._currentTokenStartIndex] == ';')
				{
					return null;
				}
				if (this._serializedText[this._currentTokenStartIndex] != '[')
				{
					throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.");
				}
				this._currentTokenStartIndex++;
				DateTime nextDateTimeValue = this.GetNextDateTimeValue("MM:dd:yyyy");
				DateTime nextDateTimeValue2 = this.GetNextDateTimeValue("MM:dd:yyyy");
				TimeSpan nextTimeSpanValue = this.GetNextTimeSpanValue();
				TimeZoneInfo.TransitionTime nextTransitionTimeValue = this.GetNextTransitionTimeValue();
				TimeZoneInfo.TransitionTime nextTransitionTimeValue2 = this.GetNextTransitionTimeValue();
				TimeSpan timeSpan = TimeSpan.Zero;
				int num = 0;
				if (this._state == TimeZoneInfo.StringSerializer.State.EndOfLine || this._currentTokenStartIndex >= this._serializedText.Length)
				{
					throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.");
				}
				if ((this._serializedText[this._currentTokenStartIndex] >= '0' && this._serializedText[this._currentTokenStartIndex] <= '9') || this._serializedText[this._currentTokenStartIndex] == '-' || this._serializedText[this._currentTokenStartIndex] == '+')
				{
					timeSpan = this.GetNextTimeSpanValue();
				}
				if (this._serializedText[this._currentTokenStartIndex] >= '0' && this._serializedText[this._currentTokenStartIndex] <= '1')
				{
					num = this.GetNextInt32Value();
				}
				if (this._state == TimeZoneInfo.StringSerializer.State.EndOfLine || this._currentTokenStartIndex >= this._serializedText.Length)
				{
					throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.");
				}
				if (this._serializedText[this._currentTokenStartIndex] != ']')
				{
					this.SkipVersionNextDataFields(1);
				}
				else
				{
					this._currentTokenStartIndex++;
				}
				TimeZoneInfo.AdjustmentRule adjustmentRule;
				try
				{
					adjustmentRule = TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(nextDateTimeValue, nextDateTimeValue2, nextTimeSpanValue, nextTransitionTimeValue, nextTransitionTimeValue2, timeSpan, num > 0);
				}
				catch (ArgumentException ex)
				{
					throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.", ex);
				}
				if (this._currentTokenStartIndex >= this._serializedText.Length)
				{
					this._state = TimeZoneInfo.StringSerializer.State.EndOfLine;
				}
				else
				{
					this._state = TimeZoneInfo.StringSerializer.State.StartOfToken;
				}
				return adjustmentRule;
			}

			// Token: 0x06000FF5 RID: 4085 RVA: 0x0004282C File Offset: 0x00040A2C
			private TimeZoneInfo.TransitionTime GetNextTransitionTimeValue()
			{
				if (this._state == TimeZoneInfo.StringSerializer.State.EndOfLine || (this._currentTokenStartIndex < this._serializedText.Length && this._serializedText[this._currentTokenStartIndex] == ']'))
				{
					throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.");
				}
				if (this._currentTokenStartIndex < 0 || this._currentTokenStartIndex >= this._serializedText.Length)
				{
					throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.");
				}
				if (this._serializedText[this._currentTokenStartIndex] != '[')
				{
					throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.");
				}
				this._currentTokenStartIndex++;
				int nextInt32Value = this.GetNextInt32Value();
				if (nextInt32Value != 0 && nextInt32Value != 1)
				{
					throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.");
				}
				DateTime nextDateTimeValue = this.GetNextDateTimeValue("HH:mm:ss.FFF");
				nextDateTimeValue = new DateTime(1, 1, 1, nextDateTimeValue.Hour, nextDateTimeValue.Minute, nextDateTimeValue.Second, nextDateTimeValue.Millisecond);
				int nextInt32Value2 = this.GetNextInt32Value();
				TimeZoneInfo.TransitionTime transitionTime;
				if (nextInt32Value == 1)
				{
					int nextInt32Value3 = this.GetNextInt32Value();
					try
					{
						transitionTime = TimeZoneInfo.TransitionTime.CreateFixedDateRule(nextDateTimeValue, nextInt32Value2, nextInt32Value3);
						goto IL_0137;
					}
					catch (ArgumentException ex)
					{
						throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.", ex);
					}
				}
				int nextInt32Value4 = this.GetNextInt32Value();
				int nextInt32Value5 = this.GetNextInt32Value();
				try
				{
					transitionTime = TimeZoneInfo.TransitionTime.CreateFloatingDateRule(nextDateTimeValue, nextInt32Value2, nextInt32Value4, (DayOfWeek)nextInt32Value5);
				}
				catch (ArgumentException ex2)
				{
					throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.", ex2);
				}
				IL_0137:
				if (this._state == TimeZoneInfo.StringSerializer.State.EndOfLine || this._currentTokenStartIndex >= this._serializedText.Length)
				{
					throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.");
				}
				if (this._serializedText[this._currentTokenStartIndex] != ']')
				{
					this.SkipVersionNextDataFields(1);
				}
				else
				{
					this._currentTokenStartIndex++;
				}
				bool flag = false;
				if (this._currentTokenStartIndex < this._serializedText.Length && this._serializedText[this._currentTokenStartIndex] == ';')
				{
					this._currentTokenStartIndex++;
					flag = true;
				}
				if (!flag)
				{
					throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.");
				}
				if (this._currentTokenStartIndex >= this._serializedText.Length)
				{
					this._state = TimeZoneInfo.StringSerializer.State.EndOfLine;
				}
				else
				{
					this._state = TimeZoneInfo.StringSerializer.State.StartOfToken;
				}
				return transitionTime;
			}

			// Token: 0x040011C2 RID: 4546
			private readonly string _serializedText;

			// Token: 0x040011C3 RID: 4547
			private int _currentTokenStartIndex;

			// Token: 0x040011C4 RID: 4548
			private TimeZoneInfo.StringSerializer.State _state;

			// Token: 0x040011C5 RID: 4549
			private const int InitialCapacityForString = 64;

			// Token: 0x040011C6 RID: 4550
			private const char Esc = '\\';

			// Token: 0x040011C7 RID: 4551
			private const char Sep = ';';

			// Token: 0x040011C8 RID: 4552
			private const char Lhs = '[';

			// Token: 0x040011C9 RID: 4553
			private const char Rhs = ']';

			// Token: 0x040011CA RID: 4554
			private const string DateTimeFormat = "MM:dd:yyyy";

			// Token: 0x040011CB RID: 4555
			private const string TimeOfDayFormat = "HH:mm:ss.FFF";

			// Token: 0x02000161 RID: 353
			private enum State
			{
				// Token: 0x040011CD RID: 4557
				Escaped,
				// Token: 0x040011CE RID: 4558
				NotEscaped,
				// Token: 0x040011CF RID: 4559
				StartOfToken,
				// Token: 0x040011D0 RID: 4560
				EndOfLine
			}
		}

		// Token: 0x02000162 RID: 354
		[Serializable]
		public readonly struct TransitionTime : IEquatable<TimeZoneInfo.TransitionTime>, ISerializable, IDeserializationCallback
		{
			// Token: 0x1700012A RID: 298
			// (get) Token: 0x06000FF6 RID: 4086 RVA: 0x00042A50 File Offset: 0x00040C50
			public DateTime TimeOfDay
			{
				get
				{
					return this._timeOfDay;
				}
			}

			// Token: 0x1700012B RID: 299
			// (get) Token: 0x06000FF7 RID: 4087 RVA: 0x00042A58 File Offset: 0x00040C58
			public int Month
			{
				get
				{
					return (int)this._month;
				}
			}

			// Token: 0x1700012C RID: 300
			// (get) Token: 0x06000FF8 RID: 4088 RVA: 0x00042A60 File Offset: 0x00040C60
			public int Week
			{
				get
				{
					return (int)this._week;
				}
			}

			// Token: 0x1700012D RID: 301
			// (get) Token: 0x06000FF9 RID: 4089 RVA: 0x00042A68 File Offset: 0x00040C68
			public int Day
			{
				get
				{
					return (int)this._day;
				}
			}

			// Token: 0x1700012E RID: 302
			// (get) Token: 0x06000FFA RID: 4090 RVA: 0x00042A70 File Offset: 0x00040C70
			public DayOfWeek DayOfWeek
			{
				get
				{
					return this._dayOfWeek;
				}
			}

			// Token: 0x1700012F RID: 303
			// (get) Token: 0x06000FFB RID: 4091 RVA: 0x00042A78 File Offset: 0x00040C78
			public bool IsFixedDateRule
			{
				get
				{
					return this._isFixedDateRule;
				}
			}

			// Token: 0x06000FFC RID: 4092 RVA: 0x00042A80 File Offset: 0x00040C80
			public override bool Equals(object obj)
			{
				return obj is TimeZoneInfo.TransitionTime && this.Equals((TimeZoneInfo.TransitionTime)obj);
			}

			// Token: 0x06000FFD RID: 4093 RVA: 0x00042A98 File Offset: 0x00040C98
			public static bool operator ==(TimeZoneInfo.TransitionTime t1, TimeZoneInfo.TransitionTime t2)
			{
				return t1.Equals(t2);
			}

			// Token: 0x06000FFE RID: 4094 RVA: 0x00042AA2 File Offset: 0x00040CA2
			public static bool operator !=(TimeZoneInfo.TransitionTime t1, TimeZoneInfo.TransitionTime t2)
			{
				return !t1.Equals(t2);
			}

			// Token: 0x06000FFF RID: 4095 RVA: 0x00042AB0 File Offset: 0x00040CB0
			public bool Equals(TimeZoneInfo.TransitionTime other)
			{
				if (this._isFixedDateRule != other._isFixedDateRule || !(this._timeOfDay == other._timeOfDay) || this._month != other._month)
				{
					return false;
				}
				if (!other._isFixedDateRule)
				{
					return this._week == other._week && this._dayOfWeek == other._dayOfWeek;
				}
				return this._day == other._day;
			}

			// Token: 0x06001000 RID: 4096 RVA: 0x00042B23 File Offset: 0x00040D23
			public override int GetHashCode()
			{
				return (int)this._month ^ ((int)this._week << 8);
			}

			// Token: 0x06001001 RID: 4097 RVA: 0x00042B34 File Offset: 0x00040D34
			private TransitionTime(DateTime timeOfDay, int month, int week, int day, DayOfWeek dayOfWeek, bool isFixedDateRule)
			{
				TimeZoneInfo.TransitionTime.ValidateTransitionTime(timeOfDay, month, week, day, dayOfWeek);
				this._timeOfDay = timeOfDay;
				this._month = (byte)month;
				this._week = (byte)week;
				this._day = (byte)day;
				this._dayOfWeek = dayOfWeek;
				this._isFixedDateRule = isFixedDateRule;
			}

			// Token: 0x06001002 RID: 4098 RVA: 0x00042B72 File Offset: 0x00040D72
			public static TimeZoneInfo.TransitionTime CreateFixedDateRule(DateTime timeOfDay, int month, int day)
			{
				return new TimeZoneInfo.TransitionTime(timeOfDay, month, 1, day, DayOfWeek.Sunday, true);
			}

			// Token: 0x06001003 RID: 4099 RVA: 0x00042B7F File Offset: 0x00040D7F
			public static TimeZoneInfo.TransitionTime CreateFloatingDateRule(DateTime timeOfDay, int month, int week, DayOfWeek dayOfWeek)
			{
				return new TimeZoneInfo.TransitionTime(timeOfDay, month, week, 1, dayOfWeek, false);
			}

			// Token: 0x06001004 RID: 4100 RVA: 0x00042B8C File Offset: 0x00040D8C
			private static void ValidateTransitionTime(DateTime timeOfDay, int month, int week, int day, DayOfWeek dayOfWeek)
			{
				if (timeOfDay.Kind != DateTimeKind.Unspecified)
				{
					throw new ArgumentException("The supplied DateTime must have the Kind property set to DateTimeKind.Unspecified.", "timeOfDay");
				}
				if (month < 1 || month > 12)
				{
					throw new ArgumentOutOfRangeException("month", "The Month parameter must be in the range 1 through 12.");
				}
				if (day < 1 || day > 31)
				{
					throw new ArgumentOutOfRangeException("day", "The Day parameter must be in the range 1 through 31.");
				}
				if (week < 1 || week > 5)
				{
					throw new ArgumentOutOfRangeException("week", "The Week parameter must be in the range 1 through 5.");
				}
				if (dayOfWeek < DayOfWeek.Sunday || dayOfWeek > DayOfWeek.Saturday)
				{
					throw new ArgumentOutOfRangeException("dayOfWeek", "The DayOfWeek enumeration must be in the range 0 through 6.");
				}
				int num;
				int num2;
				int num3;
				timeOfDay.GetDatePart(out num, out num2, out num3);
				if (num != 1 || num2 != 1 || num3 != 1 || timeOfDay.Ticks % 10000L != 0L)
				{
					throw new ArgumentException("The supplied DateTime must have the Year, Month, and Day properties set to 1.  The time cannot be specified more precisely than whole milliseconds.", "timeOfDay");
				}
			}

			// Token: 0x06001005 RID: 4101 RVA: 0x00042C50 File Offset: 0x00040E50
			void IDeserializationCallback.OnDeserialization(object sender)
			{
				try
				{
					TimeZoneInfo.TransitionTime.ValidateTransitionTime(this._timeOfDay, (int)this._month, (int)this._week, (int)this._day, this._dayOfWeek);
				}
				catch (ArgumentException ex)
				{
					throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.", ex);
				}
			}

			// Token: 0x06001006 RID: 4102 RVA: 0x00042CA0 File Offset: 0x00040EA0
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				info.AddValue("TimeOfDay", this._timeOfDay);
				info.AddValue("Month", this._month);
				info.AddValue("Week", this._week);
				info.AddValue("Day", this._day);
				info.AddValue("DayOfWeek", this._dayOfWeek);
				info.AddValue("IsFixedDateRule", this._isFixedDateRule);
			}

			// Token: 0x06001007 RID: 4103 RVA: 0x00042D28 File Offset: 0x00040F28
			private TransitionTime(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				this._timeOfDay = (DateTime)info.GetValue("TimeOfDay", typeof(DateTime));
				this._month = (byte)info.GetValue("Month", typeof(byte));
				this._week = (byte)info.GetValue("Week", typeof(byte));
				this._day = (byte)info.GetValue("Day", typeof(byte));
				this._dayOfWeek = (DayOfWeek)info.GetValue("DayOfWeek", typeof(DayOfWeek));
				this._isFixedDateRule = (bool)info.GetValue("IsFixedDateRule", typeof(bool));
			}

			// Token: 0x040011D1 RID: 4561
			private readonly DateTime _timeOfDay;

			// Token: 0x040011D2 RID: 4562
			private readonly byte _month;

			// Token: 0x040011D3 RID: 4563
			private readonly byte _week;

			// Token: 0x040011D4 RID: 4564
			private readonly byte _day;

			// Token: 0x040011D5 RID: 4565
			private readonly DayOfWeek _dayOfWeek;

			// Token: 0x040011D6 RID: 4566
			private readonly bool _isFixedDateRule;
		}

		// Token: 0x02000163 RID: 355
		private struct TZifType
		{
			// Token: 0x06001008 RID: 4104 RVA: 0x00042E04 File Offset: 0x00041004
			public TZifType(byte[] data, int index)
			{
				if (data == null || data.Length < index + 6)
				{
					throw new ArgumentException("The TZif data structure is corrupt.", "data");
				}
				this.UtcOffset = new TimeSpan(0, 0, TimeZoneInfo.TZif_ToInt32(data, index));
				this.IsDst = data[index + 4] > 0;
				this.AbbreviationIndex = data[index + 5];
			}

			// Token: 0x040011D7 RID: 4567
			public const int Length = 6;

			// Token: 0x040011D8 RID: 4568
			public readonly TimeSpan UtcOffset;

			// Token: 0x040011D9 RID: 4569
			public readonly bool IsDst;

			// Token: 0x040011DA RID: 4570
			public readonly byte AbbreviationIndex;
		}

		// Token: 0x02000164 RID: 356
		private struct TZifHead
		{
			// Token: 0x06001009 RID: 4105 RVA: 0x00042E5C File Offset: 0x0004105C
			public TZifHead(byte[] data, int index)
			{
				if (data == null || data.Length < 44)
				{
					throw new ArgumentException("bad data", "data");
				}
				this.Magic = (uint)TimeZoneInfo.TZif_ToInt32(data, index);
				if (this.Magic != 1415211366U)
				{
					throw new ArgumentException("The tzfile does not begin with the magic characters 'TZif'.  Please verify that the file is not corrupt.", "data");
				}
				byte b = data[index + 4];
				this.Version = ((b == 50) ? TimeZoneInfo.TZVersion.V2 : ((b == 51) ? TimeZoneInfo.TZVersion.V3 : TimeZoneInfo.TZVersion.V1));
				this.IsGmtCount = (uint)TimeZoneInfo.TZif_ToInt32(data, index + 20);
				this.IsStdCount = (uint)TimeZoneInfo.TZif_ToInt32(data, index + 24);
				this.LeapCount = (uint)TimeZoneInfo.TZif_ToInt32(data, index + 28);
				this.TimeCount = (uint)TimeZoneInfo.TZif_ToInt32(data, index + 32);
				this.TypeCount = (uint)TimeZoneInfo.TZif_ToInt32(data, index + 36);
				this.CharCount = (uint)TimeZoneInfo.TZif_ToInt32(data, index + 40);
			}

			// Token: 0x040011DB RID: 4571
			public const int Length = 44;

			// Token: 0x040011DC RID: 4572
			public readonly uint Magic;

			// Token: 0x040011DD RID: 4573
			public readonly TimeZoneInfo.TZVersion Version;

			// Token: 0x040011DE RID: 4574
			public readonly uint IsGmtCount;

			// Token: 0x040011DF RID: 4575
			public readonly uint IsStdCount;

			// Token: 0x040011E0 RID: 4576
			public readonly uint LeapCount;

			// Token: 0x040011E1 RID: 4577
			public readonly uint TimeCount;

			// Token: 0x040011E2 RID: 4578
			public readonly uint TypeCount;

			// Token: 0x040011E3 RID: 4579
			public readonly uint CharCount;
		}

		// Token: 0x02000165 RID: 357
		private enum TZVersion : byte
		{
			// Token: 0x040011E5 RID: 4581
			V1,
			// Token: 0x040011E6 RID: 4582
			V2,
			// Token: 0x040011E7 RID: 4583
			V3
		}

		// Token: 0x02000166 RID: 358
		private enum TimeZoneInfoResult
		{
			// Token: 0x040011E9 RID: 4585
			Success,
			// Token: 0x040011EA RID: 4586
			TimeZoneNotFoundException,
			// Token: 0x040011EB RID: 4587
			InvalidTimeZoneException,
			// Token: 0x040011EC RID: 4588
			SecurityException
		}

		// Token: 0x02000167 RID: 359
		private sealed class CachedData
		{
			// Token: 0x0600100A RID: 4106 RVA: 0x00042F2C File Offset: 0x0004112C
			private TimeZoneInfo CreateLocal()
			{
				TimeZoneInfo timeZoneInfo2;
				lock (this)
				{
					TimeZoneInfo timeZoneInfo = this._localTimeZone;
					if (timeZoneInfo == null)
					{
						timeZoneInfo = TimeZoneInfo.GetLocalTimeZone(this);
						timeZoneInfo = new TimeZoneInfo(timeZoneInfo._id, timeZoneInfo._baseUtcOffset, timeZoneInfo._displayName, timeZoneInfo._standardDisplayName, timeZoneInfo._daylightDisplayName, timeZoneInfo._adjustmentRules, false);
						this._localTimeZone = timeZoneInfo;
					}
					timeZoneInfo2 = timeZoneInfo;
				}
				return timeZoneInfo2;
			}

			// Token: 0x17000130 RID: 304
			// (get) Token: 0x0600100B RID: 4107 RVA: 0x00042FAC File Offset: 0x000411AC
			public TimeZoneInfo Local
			{
				get
				{
					TimeZoneInfo timeZoneInfo = this._localTimeZone;
					if (timeZoneInfo == null)
					{
						timeZoneInfo = this.CreateLocal();
					}
					return timeZoneInfo;
				}
			}

			// Token: 0x0600100C RID: 4108 RVA: 0x00042FCD File Offset: 0x000411CD
			public DateTimeKind GetCorrespondingKind(TimeZoneInfo timeZone)
			{
				if (timeZone == TimeZoneInfo.s_utcTimeZone)
				{
					return DateTimeKind.Utc;
				}
				if (timeZone != this._localTimeZone)
				{
					return DateTimeKind.Unspecified;
				}
				return DateTimeKind.Local;
			}

			// Token: 0x0600100D RID: 4109 RVA: 0x000025BE File Offset: 0x000007BE
			public CachedData()
			{
			}

			// Token: 0x040011ED RID: 4589
			private volatile TimeZoneInfo _localTimeZone;

			// Token: 0x040011EE RID: 4590
			public Dictionary<string, TimeZoneInfo> _systemTimeZones;

			// Token: 0x040011EF RID: 4591
			public ReadOnlyCollection<TimeZoneInfo> _readOnlySystemTimeZones;

			// Token: 0x040011F0 RID: 4592
			public bool _allSystemTimeZonesRead;
		}

		// Token: 0x02000168 RID: 360
		[CompilerGenerated]
		private sealed class <>c__DisplayClass19_0
		{
			// Token: 0x0600100E RID: 4110 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c__DisplayClass19_0()
			{
			}

			// Token: 0x0600100F RID: 4111 RVA: 0x00042FE8 File Offset: 0x000411E8
			internal bool <FindTimeZoneId>b__0(string filePath)
			{
				if (!string.Equals(filePath, this.localtimeFilePath, StringComparison.OrdinalIgnoreCase) && !string.Equals(filePath, this.posixrulesFilePath, StringComparison.OrdinalIgnoreCase) && TimeZoneInfo.CompareTimeZoneFile(filePath, this.buffer, this.rawData))
				{
					this.id = filePath;
					if (this.id.StartsWith(this.timeZoneDirectory, StringComparison.Ordinal))
					{
						this.id = this.id.Substring(this.timeZoneDirectory.Length);
					}
					return true;
				}
				return false;
			}

			// Token: 0x040011F1 RID: 4593
			public string localtimeFilePath;

			// Token: 0x040011F2 RID: 4594
			public string posixrulesFilePath;

			// Token: 0x040011F3 RID: 4595
			public byte[] buffer;

			// Token: 0x040011F4 RID: 4596
			public byte[] rawData;

			// Token: 0x040011F5 RID: 4597
			public string id;

			// Token: 0x040011F6 RID: 4598
			public string timeZoneDirectory;
		}

		// Token: 0x02000169 RID: 361
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06001010 RID: 4112 RVA: 0x00043061 File Offset: 0x00041261
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06001011 RID: 4113 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c()
			{
			}

			// Token: 0x06001012 RID: 4114 RVA: 0x0004306D File Offset: 0x0004126D
			internal bool <TZif_ParsePosixName>b__37_1(char c)
			{
				return c == '>';
			}

			// Token: 0x06001013 RID: 4115 RVA: 0x00043074 File Offset: 0x00041274
			internal bool <TZif_ParsePosixName>b__37_0(char c)
			{
				return char.IsDigit(c) || c == '+' || c == '-' || c == ',';
			}

			// Token: 0x06001014 RID: 4116 RVA: 0x0004308F File Offset: 0x0004128F
			internal bool <TZif_ParsePosixOffset>b__38_0(char c)
			{
				return !char.IsDigit(c) && c != '+' && c != '-' && c != ':';
			}

			// Token: 0x06001015 RID: 4117 RVA: 0x000430AD File Offset: 0x000412AD
			internal bool <TZif_ParsePosixDate>b__40_0(char c)
			{
				return c == '/' || c == ',';
			}

			// Token: 0x06001016 RID: 4118 RVA: 0x000430BB File Offset: 0x000412BB
			internal bool <TZif_ParsePosixTime>b__41_0(char c)
			{
				return c == ',';
			}

			// Token: 0x06001017 RID: 4119 RVA: 0x000430C4 File Offset: 0x000412C4
			internal int <GetSystemTimeZones>b__113_0(TimeZoneInfo x, TimeZoneInfo y)
			{
				int num = x.BaseUtcOffset.CompareTo(y.BaseUtcOffset);
				if (num != 0)
				{
					return num;
				}
				return string.CompareOrdinal(x.DisplayName, y.DisplayName);
			}

			// Token: 0x040011F7 RID: 4599
			public static readonly TimeZoneInfo.<>c <>9 = new TimeZoneInfo.<>c();

			// Token: 0x040011F8 RID: 4600
			public static Func<char, bool> <>9__37_1;

			// Token: 0x040011F9 RID: 4601
			public static Func<char, bool> <>9__37_0;

			// Token: 0x040011FA RID: 4602
			public static Func<char, bool> <>9__38_0;

			// Token: 0x040011FB RID: 4603
			public static Func<char, bool> <>9__40_0;

			// Token: 0x040011FC RID: 4604
			public static Func<char, bool> <>9__41_0;

			// Token: 0x040011FD RID: 4605
			public static Comparison<TimeZoneInfo> <>9__113_0;
		}
	}
}
