using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Text
{
	// Token: 0x0200037B RID: 891
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class StringBuilder : ISerializable
	{
		// Token: 0x0600262C RID: 9772 RVA: 0x0008891B File Offset: 0x00086B1B
		public StringBuilder()
		{
			this.m_MaxCapacity = int.MaxValue;
			this.m_ChunkChars = new char[16];
		}

		// Token: 0x0600262D RID: 9773 RVA: 0x0008893B File Offset: 0x00086B3B
		public StringBuilder(int capacity)
			: this(capacity, int.MaxValue)
		{
		}

		// Token: 0x0600262E RID: 9774 RVA: 0x00088949 File Offset: 0x00086B49
		public StringBuilder(string value)
			: this(value, 16)
		{
		}

		// Token: 0x0600262F RID: 9775 RVA: 0x00088954 File Offset: 0x00086B54
		public StringBuilder(string value, int capacity)
			: this(value, 0, (value != null) ? value.Length : 0, capacity)
		{
		}

		// Token: 0x06002630 RID: 9776 RVA: 0x0008896C File Offset: 0x00086B6C
		public unsafe StringBuilder(string value, int startIndex, int length, int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", SR.Format("'{0}' must be greater than zero.", "capacity"));
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", SR.Format("'{0}' must be non-negative.", "length"));
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", "StartIndex cannot be less than zero.");
			}
			if (value == null)
			{
				value = string.Empty;
			}
			if (startIndex > value.Length - length)
			{
				throw new ArgumentOutOfRangeException("length", "Index and length must refer to a location within the string.");
			}
			this.m_MaxCapacity = int.MaxValue;
			if (capacity == 0)
			{
				capacity = 16;
			}
			capacity = Math.Max(capacity, length);
			this.m_ChunkChars = new char[capacity];
			this.m_ChunkLength = length;
			fixed (string text = value)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				StringBuilder.ThreadSafeCopy(ptr + startIndex, this.m_ChunkChars, 0, length);
			}
		}

		// Token: 0x06002631 RID: 9777 RVA: 0x00088A4C File Offset: 0x00086C4C
		public StringBuilder(int capacity, int maxCapacity)
		{
			if (capacity > maxCapacity)
			{
				throw new ArgumentOutOfRangeException("capacity", "Capacity exceeds maximum capacity.");
			}
			if (maxCapacity < 1)
			{
				throw new ArgumentOutOfRangeException("maxCapacity", "MaxCapacity must be one or greater.");
			}
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", SR.Format("'{0}' must be greater than zero.", "capacity"));
			}
			if (capacity == 0)
			{
				capacity = Math.Min(16, maxCapacity);
			}
			this.m_MaxCapacity = maxCapacity;
			this.m_ChunkChars = new char[capacity];
		}

		// Token: 0x06002632 RID: 9778 RVA: 0x00088AC8 File Offset: 0x00086CC8
		private StringBuilder(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			int num = 0;
			string text = null;
			int num2 = int.MaxValue;
			bool flag = false;
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				if (!(name == "m_MaxCapacity"))
				{
					if (!(name == "m_StringValue"))
					{
						if (name == "Capacity")
						{
							num = info.GetInt32("Capacity");
							flag = true;
						}
					}
					else
					{
						text = info.GetString("m_StringValue");
					}
				}
				else
				{
					num2 = info.GetInt32("m_MaxCapacity");
				}
			}
			if (text == null)
			{
				text = string.Empty;
			}
			if (num2 < 1 || text.Length > num2)
			{
				throw new SerializationException("The serialized MaxCapacity property of StringBuilder must be positive and greater than or equal to the String length.");
			}
			if (!flag)
			{
				num = Math.Min(Math.Max(16, text.Length), num2);
			}
			if (num < 0 || num < text.Length || num > num2)
			{
				throw new SerializationException("The serialized Capacity property of StringBuilder must be positive, less than or equal to MaxCapacity and greater than or equal to the String length.");
			}
			this.m_MaxCapacity = num2;
			this.m_ChunkChars = new char[num];
			text.CopyTo(0, this.m_ChunkChars, 0, text.Length);
			this.m_ChunkLength = text.Length;
			this.m_ChunkPrevious = null;
		}

		// Token: 0x06002633 RID: 9779 RVA: 0x00088BF8 File Offset: 0x00086DF8
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("m_MaxCapacity", this.m_MaxCapacity);
			info.AddValue("Capacity", this.Capacity);
			info.AddValue("m_StringValue", this.ToString());
			info.AddValue("m_currentThread", 0);
		}

		// Token: 0x06002634 RID: 9780 RVA: 0x00088C54 File Offset: 0x00086E54
		[Conditional("DEBUG")]
		private void AssertInvariants()
		{
			StringBuilder stringBuilder = this;
			int maxCapacity = this.m_MaxCapacity;
			for (;;)
			{
				StringBuilder chunkPrevious = stringBuilder.m_ChunkPrevious;
				if (chunkPrevious == null)
				{
					break;
				}
				stringBuilder = chunkPrevious;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06002635 RID: 9781 RVA: 0x00088C78 File Offset: 0x00086E78
		// (set) Token: 0x06002636 RID: 9782 RVA: 0x00088C8C File Offset: 0x00086E8C
		public int Capacity
		{
			get
			{
				return this.m_ChunkChars.Length + this.m_ChunkOffset;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value", "Capacity must be positive.");
				}
				if (value > this.MaxCapacity)
				{
					throw new ArgumentOutOfRangeException("value", "Capacity exceeds maximum capacity.");
				}
				if (value < this.Length)
				{
					throw new ArgumentOutOfRangeException("value", "capacity was less than the current size.");
				}
				if (this.Capacity != value)
				{
					char[] array = new char[value - this.m_ChunkOffset];
					Array.Copy(this.m_ChunkChars, 0, array, 0, this.m_ChunkLength);
					this.m_ChunkChars = array;
				}
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06002637 RID: 9783 RVA: 0x00088D11 File Offset: 0x00086F11
		public int MaxCapacity
		{
			get
			{
				return this.m_MaxCapacity;
			}
		}

		// Token: 0x06002638 RID: 9784 RVA: 0x00088D19 File Offset: 0x00086F19
		public int EnsureCapacity(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", "Capacity must be positive.");
			}
			if (this.Capacity < capacity)
			{
				this.Capacity = capacity;
			}
			return this.Capacity;
		}

		// Token: 0x06002639 RID: 9785 RVA: 0x00088D48 File Offset: 0x00086F48
		public unsafe override string ToString()
		{
			if (this.Length == 0)
			{
				return string.Empty;
			}
			string text = string.FastAllocateString(this.Length);
			StringBuilder stringBuilder = this;
			fixed (string text2 = text)
			{
				char* ptr = text2;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				for (;;)
				{
					if (stringBuilder.m_ChunkLength > 0)
					{
						char[] chunkChars = stringBuilder.m_ChunkChars;
						int chunkOffset = stringBuilder.m_ChunkOffset;
						int chunkLength = stringBuilder.m_ChunkLength;
						if (chunkLength + chunkOffset > text.Length || chunkLength > chunkChars.Length)
						{
							break;
						}
						fixed (char* ptr2 = &chunkChars[0])
						{
							char* ptr3 = ptr2;
							string.wstrcpy(ptr + chunkOffset, ptr3, chunkLength);
						}
					}
					stringBuilder = stringBuilder.m_ChunkPrevious;
					if (stringBuilder == null)
					{
						return text;
					}
				}
				throw new ArgumentOutOfRangeException("chunkLength", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
		}

		// Token: 0x0600263A RID: 9786 RVA: 0x00088DF8 File Offset: 0x00086FF8
		public unsafe string ToString(int startIndex, int length)
		{
			int length2 = this.Length;
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", "StartIndex cannot be less than zero.");
			}
			if (startIndex > length2)
			{
				throw new ArgumentOutOfRangeException("startIndex", "startIndex cannot be larger than length of string.");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", "Length cannot be less than zero.");
			}
			if (startIndex > length2 - length)
			{
				throw new ArgumentOutOfRangeException("length", "Index and length must refer to a location within the string.");
			}
			string text2;
			string text = (text2 = string.FastAllocateString(length));
			char* ptr = text2;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			this.CopyTo(startIndex, new Span<char>((void*)ptr, length), length);
			return text;
		}

		// Token: 0x0600263B RID: 9787 RVA: 0x00088E83 File Offset: 0x00087083
		public StringBuilder Clear()
		{
			this.Length = 0;
			return this;
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x0600263C RID: 9788 RVA: 0x00088E8D File Offset: 0x0008708D
		// (set) Token: 0x0600263D RID: 9789 RVA: 0x00088E9C File Offset: 0x0008709C
		public int Length
		{
			get
			{
				return this.m_ChunkOffset + this.m_ChunkLength;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value", "Length cannot be less than zero.");
				}
				if (value > this.MaxCapacity)
				{
					throw new ArgumentOutOfRangeException("value", "capacity was less than the current size.");
				}
				if (value == 0 && this.m_ChunkPrevious == null)
				{
					this.m_ChunkLength = 0;
					this.m_ChunkOffset = 0;
					return;
				}
				int num = value - this.Length;
				if (num > 0)
				{
					this.Append('\0', num);
					return;
				}
				StringBuilder stringBuilder = this.FindChunkForIndex(value);
				if (stringBuilder != this)
				{
					int num2 = Math.Min(this.Capacity, Math.Max(this.Length * 6 / 5, this.m_ChunkChars.Length)) - stringBuilder.m_ChunkOffset;
					if (num2 > stringBuilder.m_ChunkChars.Length)
					{
						char[] array = new char[num2];
						Array.Copy(stringBuilder.m_ChunkChars, 0, array, 0, stringBuilder.m_ChunkLength);
						this.m_ChunkChars = array;
					}
					else
					{
						this.m_ChunkChars = stringBuilder.m_ChunkChars;
					}
					this.m_ChunkPrevious = stringBuilder.m_ChunkPrevious;
					this.m_ChunkOffset = stringBuilder.m_ChunkOffset;
				}
				this.m_ChunkLength = value - stringBuilder.m_ChunkOffset;
			}
		}

		// Token: 0x170004B7 RID: 1207
		[IndexerName("Chars")]
		public char this[int index]
		{
			get
			{
				StringBuilder stringBuilder = this;
				int num;
				for (;;)
				{
					num = index - stringBuilder.m_ChunkOffset;
					if (num >= 0)
					{
						break;
					}
					stringBuilder = stringBuilder.m_ChunkPrevious;
					if (stringBuilder == null)
					{
						goto Block_3;
					}
				}
				if (num >= stringBuilder.m_ChunkLength)
				{
					throw new IndexOutOfRangeException();
				}
				return stringBuilder.m_ChunkChars[num];
				Block_3:
				throw new IndexOutOfRangeException();
			}
			set
			{
				StringBuilder stringBuilder = this;
				int num;
				for (;;)
				{
					num = index - stringBuilder.m_ChunkOffset;
					if (num >= 0)
					{
						break;
					}
					stringBuilder = stringBuilder.m_ChunkPrevious;
					if (stringBuilder == null)
					{
						goto Block_3;
					}
				}
				if (num >= stringBuilder.m_ChunkLength)
				{
					throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				stringBuilder.m_ChunkChars[num] = value;
				return;
				Block_3:
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
		}

		// Token: 0x06002640 RID: 9792 RVA: 0x0008903C File Offset: 0x0008723C
		public StringBuilder Append(char value, int repeatCount)
		{
			if (repeatCount < 0)
			{
				throw new ArgumentOutOfRangeException("repeatCount", "Count cannot be less than zero.");
			}
			if (repeatCount == 0)
			{
				return this;
			}
			int num = this.Length + repeatCount;
			if (num > this.m_MaxCapacity || num < repeatCount)
			{
				throw new ArgumentOutOfRangeException("repeatCount", "The length cannot be greater than the capacity.");
			}
			int num2 = this.m_ChunkLength;
			while (repeatCount > 0)
			{
				if (num2 < this.m_ChunkChars.Length)
				{
					this.m_ChunkChars[num2++] = value;
					repeatCount--;
				}
				else
				{
					this.m_ChunkLength = num2;
					this.ExpandByABlock(repeatCount);
					num2 = 0;
				}
			}
			this.m_ChunkLength = num2;
			return this;
		}

		// Token: 0x06002641 RID: 9793 RVA: 0x000890CC File Offset: 0x000872CC
		public unsafe StringBuilder Append(char[] value, int startIndex, int charCount)
		{
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Value must be positive.");
			}
			if (charCount < 0)
			{
				throw new ArgumentOutOfRangeException("charCount", "Value must be positive.");
			}
			if (value == null)
			{
				if (startIndex == 0 && charCount == 0)
				{
					return this;
				}
				throw new ArgumentNullException("value");
			}
			else
			{
				if (charCount > value.Length - startIndex)
				{
					throw new ArgumentOutOfRangeException("charCount", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				if (charCount == 0)
				{
					return this;
				}
				fixed (char* ptr = &value[startIndex])
				{
					char* ptr2 = ptr;
					this.Append(ptr2, charCount);
					return this;
				}
			}
		}

		// Token: 0x06002642 RID: 9794 RVA: 0x0008914C File Offset: 0x0008734C
		public unsafe StringBuilder Append(string value)
		{
			if (value != null)
			{
				char[] chunkChars = this.m_ChunkChars;
				int chunkLength = this.m_ChunkLength;
				int length = value.Length;
				int num = chunkLength + length;
				if (num < chunkChars.Length)
				{
					if (length <= 2)
					{
						if (length > 0)
						{
							chunkChars[chunkLength] = value[0];
						}
						if (length > 1)
						{
							chunkChars[chunkLength + 1] = value[1];
						}
					}
					else
					{
						fixed (string text = value)
						{
							char* ptr = text;
							if (ptr != null)
							{
								ptr += RuntimeHelpers.OffsetToStringData / 2;
							}
							fixed (char* ptr2 = &chunkChars[chunkLength])
							{
								string.wstrcpy(ptr2, ptr, length);
							}
						}
					}
					this.m_ChunkLength = num;
				}
				else
				{
					this.AppendHelper(value);
				}
			}
			return this;
		}

		// Token: 0x06002643 RID: 9795 RVA: 0x000891E4 File Offset: 0x000873E4
		private unsafe void AppendHelper(string value)
		{
			fixed (string text = value)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				this.Append(ptr, value.Length);
			}
		}

		// Token: 0x06002644 RID: 9796 RVA: 0x00089214 File Offset: 0x00087414
		public unsafe StringBuilder Append(string value, int startIndex, int count)
		{
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Value must be positive.");
			}
			if (value == null)
			{
				if (startIndex == 0 && count == 0)
				{
					return this;
				}
				throw new ArgumentNullException("value");
			}
			else
			{
				if (count == 0)
				{
					return this;
				}
				if (startIndex > value.Length - count)
				{
					throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				char* ptr = value;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				this.Append(ptr + startIndex, count);
				return this;
			}
		}

		// Token: 0x06002645 RID: 9797 RVA: 0x0008929E File Offset: 0x0008749E
		public StringBuilder Append(StringBuilder value)
		{
			if (value != null && value.Length != 0)
			{
				return this.AppendCore(value, 0, value.Length);
			}
			return this;
		}

		// Token: 0x06002646 RID: 9798 RVA: 0x000892BC File Offset: 0x000874BC
		public StringBuilder Append(StringBuilder value, int startIndex, int count)
		{
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Value must be positive.");
			}
			if (value == null)
			{
				if (startIndex == 0 && count == 0)
				{
					return this;
				}
				throw new ArgumentNullException("value");
			}
			else
			{
				if (count == 0)
				{
					return this;
				}
				if (count > value.Length - startIndex)
				{
					throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				return this.AppendCore(value, startIndex, count);
			}
		}

		// Token: 0x06002647 RID: 9799 RVA: 0x00089330 File Offset: 0x00087530
		private StringBuilder AppendCore(StringBuilder value, int startIndex, int count)
		{
			if (value == this)
			{
				return this.Append(value.ToString(startIndex, count));
			}
			if (this.Length + count > this.m_MaxCapacity)
			{
				throw new ArgumentOutOfRangeException("Capacity", "Capacity exceeds maximum capacity.");
			}
			while (count > 0)
			{
				int num = Math.Min(this.m_ChunkChars.Length - this.m_ChunkLength, count);
				if (num == 0)
				{
					this.ExpandByABlock(count);
					num = Math.Min(this.m_ChunkChars.Length - this.m_ChunkLength, count);
				}
				value.CopyTo(startIndex, new Span<char>(this.m_ChunkChars, this.m_ChunkLength, num), num);
				this.m_ChunkLength += num;
				startIndex += num;
				count -= num;
			}
			return this;
		}

		// Token: 0x06002648 RID: 9800 RVA: 0x000893DD File Offset: 0x000875DD
		public StringBuilder AppendLine()
		{
			return this.Append(Environment.NewLine);
		}

		// Token: 0x06002649 RID: 9801 RVA: 0x000893EA File Offset: 0x000875EA
		public StringBuilder AppendLine(string value)
		{
			this.Append(value);
			return this.Append(Environment.NewLine);
		}

		// Token: 0x0600264A RID: 9802 RVA: 0x00089400 File Offset: 0x00087600
		public void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			if (destinationIndex < 0)
			{
				throw new ArgumentOutOfRangeException("destinationIndex", SR.Format("'{0}' must be non-negative.", "destinationIndex"));
			}
			if (destinationIndex > destination.Length - count)
			{
				throw new ArgumentException("Either offset did not refer to a position in the string, or there is an insufficient length of destination character array.");
			}
			this.CopyTo(sourceIndex, new Span<char>(destination).Slice(destinationIndex), count);
		}

		// Token: 0x0600264B RID: 9803 RVA: 0x00089468 File Offset: 0x00087668
		public void CopyTo(int sourceIndex, Span<char> destination, int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Argument count must not be negative.");
			}
			if (sourceIndex > this.Length)
			{
				throw new ArgumentOutOfRangeException("sourceIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (sourceIndex > this.Length - count)
			{
				throw new ArgumentException("Source string was not long enough. Check sourceIndex and count.");
			}
			StringBuilder stringBuilder = this;
			int num = sourceIndex + count;
			int num2 = count;
			while (count > 0)
			{
				int num3 = num - stringBuilder.m_ChunkOffset;
				if (num3 >= 0)
				{
					num3 = Math.Min(num3, stringBuilder.m_ChunkLength);
					int num4 = count;
					int num5 = num3 - count;
					if (num5 < 0)
					{
						num4 += num5;
						num5 = 0;
					}
					num2 -= num4;
					count -= num4;
					StringBuilder.ThreadSafeCopy(stringBuilder.m_ChunkChars, num5, destination, num2, num4);
				}
				stringBuilder = stringBuilder.m_ChunkPrevious;
			}
		}

		// Token: 0x0600264C RID: 9804 RVA: 0x0008951C File Offset: 0x0008771C
		public unsafe StringBuilder Insert(int index, string value, int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			int length = this.Length;
			if (index > length)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (string.IsNullOrEmpty(value) || count == 0)
			{
				return this;
			}
			long num = (long)value.Length * (long)count;
			if (num > (long)(this.MaxCapacity - this.Length))
			{
				throw new OutOfMemoryException();
			}
			StringBuilder stringBuilder;
			int num2;
			this.MakeRoom(index, (int)num, out stringBuilder, out num2, false);
			char* ptr = value;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			while (count > 0)
			{
				this.ReplaceInPlaceAtChunk(ref stringBuilder, ref num2, ptr, value.Length);
				count--;
			}
			return this;
		}

		// Token: 0x0600264D RID: 9805 RVA: 0x000895CC File Offset: 0x000877CC
		public StringBuilder Remove(int startIndex, int length)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", "Length cannot be less than zero.");
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", "StartIndex cannot be less than zero.");
			}
			if (length > this.Length - startIndex)
			{
				throw new ArgumentOutOfRangeException("length", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (this.Length == length && startIndex == 0)
			{
				this.Length = 0;
				return this;
			}
			if (length > 0)
			{
				StringBuilder stringBuilder;
				int num;
				this.Remove(startIndex, length, out stringBuilder, out num);
			}
			return this;
		}

		// Token: 0x0600264E RID: 9806 RVA: 0x00089642 File Offset: 0x00087842
		public StringBuilder Append(bool value)
		{
			return this.Append(value.ToString());
		}

		// Token: 0x0600264F RID: 9807 RVA: 0x00089654 File Offset: 0x00087854
		public StringBuilder Append(char value)
		{
			if (this.m_ChunkLength < this.m_ChunkChars.Length)
			{
				char[] chunkChars = this.m_ChunkChars;
				int chunkLength = this.m_ChunkLength;
				this.m_ChunkLength = chunkLength + 1;
				chunkChars[chunkLength] = value;
			}
			else
			{
				this.Append(value, 1);
			}
			return this;
		}

		// Token: 0x06002650 RID: 9808 RVA: 0x00089696 File Offset: 0x00087896
		[CLSCompliant(false)]
		public StringBuilder Append(sbyte value)
		{
			return this.AppendSpanFormattable<sbyte>(value);
		}

		// Token: 0x06002651 RID: 9809 RVA: 0x0008969F File Offset: 0x0008789F
		public StringBuilder Append(byte value)
		{
			return this.AppendSpanFormattable<byte>(value);
		}

		// Token: 0x06002652 RID: 9810 RVA: 0x000896A8 File Offset: 0x000878A8
		public StringBuilder Append(short value)
		{
			return this.AppendSpanFormattable<short>(value);
		}

		// Token: 0x06002653 RID: 9811 RVA: 0x000896B1 File Offset: 0x000878B1
		public StringBuilder Append(int value)
		{
			return this.AppendSpanFormattable<int>(value);
		}

		// Token: 0x06002654 RID: 9812 RVA: 0x000896BA File Offset: 0x000878BA
		public StringBuilder Append(long value)
		{
			return this.AppendSpanFormattable<long>(value);
		}

		// Token: 0x06002655 RID: 9813 RVA: 0x000896C3 File Offset: 0x000878C3
		public StringBuilder Append(float value)
		{
			return this.AppendSpanFormattable<float>(value);
		}

		// Token: 0x06002656 RID: 9814 RVA: 0x000896CC File Offset: 0x000878CC
		public StringBuilder Append(double value)
		{
			return this.AppendSpanFormattable<double>(value);
		}

		// Token: 0x06002657 RID: 9815 RVA: 0x000896D5 File Offset: 0x000878D5
		public StringBuilder Append(decimal value)
		{
			return this.AppendSpanFormattable<decimal>(value);
		}

		// Token: 0x06002658 RID: 9816 RVA: 0x000896DE File Offset: 0x000878DE
		[CLSCompliant(false)]
		public StringBuilder Append(ushort value)
		{
			return this.AppendSpanFormattable<ushort>(value);
		}

		// Token: 0x06002659 RID: 9817 RVA: 0x000896E7 File Offset: 0x000878E7
		[CLSCompliant(false)]
		public StringBuilder Append(uint value)
		{
			return this.AppendSpanFormattable<uint>(value);
		}

		// Token: 0x0600265A RID: 9818 RVA: 0x000896F0 File Offset: 0x000878F0
		[CLSCompliant(false)]
		public StringBuilder Append(ulong value)
		{
			return this.AppendSpanFormattable<ulong>(value);
		}

		// Token: 0x0600265B RID: 9819 RVA: 0x000896F9 File Offset: 0x000878F9
		private StringBuilder AppendSpanFormattable<T>(T value) where T : IFormattable
		{
			return this.Append(value.ToString(null, CultureInfo.CurrentCulture));
		}

		// Token: 0x0600265C RID: 9820 RVA: 0x00089714 File Offset: 0x00087914
		public StringBuilder Append(object value)
		{
			if (value != null)
			{
				return this.Append(value.ToString());
			}
			return this;
		}

		// Token: 0x0600265D RID: 9821 RVA: 0x00089728 File Offset: 0x00087928
		public unsafe StringBuilder Append(char[] value)
		{
			if (value != null && value.Length != 0)
			{
				fixed (char* ptr = &value[0])
				{
					char* ptr2 = ptr;
					this.Append(ptr2, value.Length);
				}
			}
			return this;
		}

		// Token: 0x0600265E RID: 9822 RVA: 0x00089758 File Offset: 0x00087958
		public unsafe StringBuilder Append(ReadOnlySpan<char> value)
		{
			if (value.Length > 0)
			{
				fixed (char* reference = MemoryMarshal.GetReference<char>(value))
				{
					char* ptr = reference;
					this.Append(ptr, value.Length);
				}
			}
			return this;
		}

		// Token: 0x0600265F RID: 9823 RVA: 0x0008978C File Offset: 0x0008798C
		public unsafe StringBuilder AppendJoin(string separator, params object[] values)
		{
			separator = separator ?? string.Empty;
			fixed (string text = separator)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				return this.AppendJoinCore<object>(ptr, separator.Length, values);
			}
		}

		// Token: 0x06002660 RID: 9824 RVA: 0x000897C4 File Offset: 0x000879C4
		public unsafe StringBuilder AppendJoin<T>(string separator, IEnumerable<T> values)
		{
			separator = separator ?? string.Empty;
			fixed (string text = separator)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				return this.AppendJoinCore<T>(ptr, separator.Length, values);
			}
		}

		// Token: 0x06002661 RID: 9825 RVA: 0x000897FC File Offset: 0x000879FC
		public unsafe StringBuilder AppendJoin(string separator, params string[] values)
		{
			separator = separator ?? string.Empty;
			fixed (string text = separator)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				return this.AppendJoinCore<string>(ptr, separator.Length, values);
			}
		}

		// Token: 0x06002662 RID: 9826 RVA: 0x00089833 File Offset: 0x00087A33
		public unsafe StringBuilder AppendJoin(char separator, params object[] values)
		{
			return this.AppendJoinCore<object>(&separator, 1, values);
		}

		// Token: 0x06002663 RID: 9827 RVA: 0x00089840 File Offset: 0x00087A40
		public unsafe StringBuilder AppendJoin<T>(char separator, IEnumerable<T> values)
		{
			return this.AppendJoinCore<T>(&separator, 1, values);
		}

		// Token: 0x06002664 RID: 9828 RVA: 0x0008984D File Offset: 0x00087A4D
		public unsafe StringBuilder AppendJoin(char separator, params string[] values)
		{
			return this.AppendJoinCore<string>(&separator, 1, values);
		}

		// Token: 0x06002665 RID: 9829 RVA: 0x0008985C File Offset: 0x00087A5C
		private unsafe StringBuilder AppendJoinCore<T>(char* separator, int separatorLength, IEnumerable<T> values)
		{
			if (values == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.values);
			}
			using (IEnumerator<T> enumerator = values.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					return this;
				}
				T t = enumerator.Current;
				if (t != null)
				{
					this.Append(t.ToString());
				}
				while (enumerator.MoveNext())
				{
					this.Append(separator, separatorLength);
					t = enumerator.Current;
					if (t != null)
					{
						this.Append(t.ToString());
					}
				}
			}
			return this;
		}

		// Token: 0x06002666 RID: 9830 RVA: 0x00089900 File Offset: 0x00087B00
		private unsafe StringBuilder AppendJoinCore<T>(char* separator, int separatorLength, T[] values)
		{
			if (values == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.values);
			}
			if (values.Length == 0)
			{
				return this;
			}
			if (values[0] != null)
			{
				this.Append(values[0].ToString());
			}
			for (int i = 1; i < values.Length; i++)
			{
				this.Append(separator, separatorLength);
				if (values[i] != null)
				{
					this.Append(values[i].ToString());
				}
			}
			return this;
		}

		// Token: 0x06002667 RID: 9831 RVA: 0x00089988 File Offset: 0x00087B88
		public unsafe StringBuilder Insert(int index, string value)
		{
			if (index > this.Length)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (value != null)
			{
				fixed (string text = value)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					this.Insert(index, ptr, value.Length);
				}
			}
			return this;
		}

		// Token: 0x06002668 RID: 9832 RVA: 0x000899D2 File Offset: 0x00087BD2
		public StringBuilder Insert(int index, bool value)
		{
			return this.Insert(index, value.ToString(), 1);
		}

		// Token: 0x06002669 RID: 9833 RVA: 0x000899E3 File Offset: 0x00087BE3
		[CLSCompliant(false)]
		public StringBuilder Insert(int index, sbyte value)
		{
			return this.Insert(index, value.ToString(), 1);
		}

		// Token: 0x0600266A RID: 9834 RVA: 0x000899F4 File Offset: 0x00087BF4
		public StringBuilder Insert(int index, byte value)
		{
			return this.Insert(index, value.ToString(), 1);
		}

		// Token: 0x0600266B RID: 9835 RVA: 0x00089A05 File Offset: 0x00087C05
		public StringBuilder Insert(int index, short value)
		{
			return this.Insert(index, value.ToString(), 1);
		}

		// Token: 0x0600266C RID: 9836 RVA: 0x00089A16 File Offset: 0x00087C16
		public unsafe StringBuilder Insert(int index, char value)
		{
			this.Insert(index, &value, 1);
			return this;
		}

		// Token: 0x0600266D RID: 9837 RVA: 0x00089A24 File Offset: 0x00087C24
		public StringBuilder Insert(int index, char[] value)
		{
			if (index > this.Length)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (value != null)
			{
				this.Insert(index, value, 0, value.Length);
			}
			return this;
		}

		// Token: 0x0600266E RID: 9838 RVA: 0x00089A50 File Offset: 0x00087C50
		public unsafe StringBuilder Insert(int index, char[] value, int startIndex, int charCount)
		{
			int length = this.Length;
			if (index > length)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (value == null)
			{
				if (startIndex == 0 && charCount == 0)
				{
					return this;
				}
				throw new ArgumentNullException("value", "String reference not set to an instance of a String.");
			}
			else
			{
				if (startIndex < 0)
				{
					throw new ArgumentOutOfRangeException("startIndex", "StartIndex cannot be less than zero.");
				}
				if (charCount < 0)
				{
					throw new ArgumentOutOfRangeException("charCount", "Value must be positive.");
				}
				if (startIndex > value.Length - charCount)
				{
					throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				if (charCount > 0)
				{
					fixed (char* ptr = &value[startIndex])
					{
						char* ptr2 = ptr;
						this.Insert(index, ptr2, charCount);
					}
				}
				return this;
			}
		}

		// Token: 0x0600266F RID: 9839 RVA: 0x00089AF4 File Offset: 0x00087CF4
		public StringBuilder Insert(int index, int value)
		{
			return this.Insert(index, value.ToString(), 1);
		}

		// Token: 0x06002670 RID: 9840 RVA: 0x00089B05 File Offset: 0x00087D05
		public StringBuilder Insert(int index, long value)
		{
			return this.Insert(index, value.ToString(), 1);
		}

		// Token: 0x06002671 RID: 9841 RVA: 0x00089B16 File Offset: 0x00087D16
		public StringBuilder Insert(int index, float value)
		{
			return this.Insert(index, value.ToString(), 1);
		}

		// Token: 0x06002672 RID: 9842 RVA: 0x00089B27 File Offset: 0x00087D27
		public StringBuilder Insert(int index, double value)
		{
			return this.Insert(index, value.ToString(), 1);
		}

		// Token: 0x06002673 RID: 9843 RVA: 0x00089B38 File Offset: 0x00087D38
		public StringBuilder Insert(int index, decimal value)
		{
			return this.Insert(index, value.ToString(), 1);
		}

		// Token: 0x06002674 RID: 9844 RVA: 0x00089B49 File Offset: 0x00087D49
		[CLSCompliant(false)]
		public StringBuilder Insert(int index, ushort value)
		{
			return this.Insert(index, value.ToString(), 1);
		}

		// Token: 0x06002675 RID: 9845 RVA: 0x00089B5A File Offset: 0x00087D5A
		[CLSCompliant(false)]
		public StringBuilder Insert(int index, uint value)
		{
			return this.Insert(index, value.ToString(), 1);
		}

		// Token: 0x06002676 RID: 9846 RVA: 0x00089B6B File Offset: 0x00087D6B
		[CLSCompliant(false)]
		public StringBuilder Insert(int index, ulong value)
		{
			return this.Insert(index, value.ToString(), 1);
		}

		// Token: 0x06002677 RID: 9847 RVA: 0x00089B7C File Offset: 0x00087D7C
		public StringBuilder Insert(int index, object value)
		{
			if (value != null)
			{
				return this.Insert(index, value.ToString(), 1);
			}
			return this;
		}

		// Token: 0x06002678 RID: 9848 RVA: 0x00089B94 File Offset: 0x00087D94
		public unsafe StringBuilder Insert(int index, ReadOnlySpan<char> value)
		{
			if (index > this.Length)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (value.Length > 0)
			{
				fixed (char* reference = MemoryMarshal.GetReference<char>(value))
				{
					char* ptr = reference;
					this.Insert(index, ptr, value.Length);
				}
			}
			return this;
		}

		// Token: 0x06002679 RID: 9849 RVA: 0x00089BE1 File Offset: 0x00087DE1
		public StringBuilder AppendFormat(string format, object arg0)
		{
			return this.AppendFormatHelper(null, format, new ParamsArray(arg0));
		}

		// Token: 0x0600267A RID: 9850 RVA: 0x00089BF1 File Offset: 0x00087DF1
		public StringBuilder AppendFormat(string format, object arg0, object arg1)
		{
			return this.AppendFormatHelper(null, format, new ParamsArray(arg0, arg1));
		}

		// Token: 0x0600267B RID: 9851 RVA: 0x00089C02 File Offset: 0x00087E02
		public StringBuilder AppendFormat(string format, object arg0, object arg1, object arg2)
		{
			return this.AppendFormatHelper(null, format, new ParamsArray(arg0, arg1, arg2));
		}

		// Token: 0x0600267C RID: 9852 RVA: 0x00089C15 File Offset: 0x00087E15
		public StringBuilder AppendFormat(string format, params object[] args)
		{
			if (args == null)
			{
				throw new ArgumentNullException((format == null) ? "format" : "args");
			}
			return this.AppendFormatHelper(null, format, new ParamsArray(args));
		}

		// Token: 0x0600267D RID: 9853 RVA: 0x00089C3D File Offset: 0x00087E3D
		public StringBuilder AppendFormat(IFormatProvider provider, string format, object arg0)
		{
			return this.AppendFormatHelper(provider, format, new ParamsArray(arg0));
		}

		// Token: 0x0600267E RID: 9854 RVA: 0x00089C4D File Offset: 0x00087E4D
		public StringBuilder AppendFormat(IFormatProvider provider, string format, object arg0, object arg1)
		{
			return this.AppendFormatHelper(provider, format, new ParamsArray(arg0, arg1));
		}

		// Token: 0x0600267F RID: 9855 RVA: 0x00089C5F File Offset: 0x00087E5F
		public StringBuilder AppendFormat(IFormatProvider provider, string format, object arg0, object arg1, object arg2)
		{
			return this.AppendFormatHelper(provider, format, new ParamsArray(arg0, arg1, arg2));
		}

		// Token: 0x06002680 RID: 9856 RVA: 0x00089C73 File Offset: 0x00087E73
		public StringBuilder AppendFormat(IFormatProvider provider, string format, params object[] args)
		{
			if (args == null)
			{
				throw new ArgumentNullException((format == null) ? "format" : "args");
			}
			return this.AppendFormatHelper(provider, format, new ParamsArray(args));
		}

		// Token: 0x06002681 RID: 9857 RVA: 0x00089C9B File Offset: 0x00087E9B
		private static void FormatError()
		{
			throw new FormatException("Input string was not in a correct format.");
		}

		// Token: 0x06002682 RID: 9858 RVA: 0x00089CA8 File Offset: 0x00087EA8
		internal StringBuilder AppendFormatHelper(IFormatProvider provider, string format, ParamsArray args)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			int num = 0;
			int length = format.Length;
			char c = '\0';
			StringBuilder stringBuilder = null;
			ICustomFormatter customFormatter = null;
			if (provider != null)
			{
				customFormatter = (ICustomFormatter)provider.GetFormat(typeof(ICustomFormatter));
			}
			for (;;)
			{
				if (num < length)
				{
					c = format[num];
					num++;
					if (c == '}')
					{
						if (num < length && format[num] == '}')
						{
							num++;
						}
						else
						{
							StringBuilder.FormatError();
						}
					}
					if (c == '{')
					{
						if (num >= length || format[num] != '{')
						{
							num--;
							goto IL_0091;
						}
						num++;
					}
					this.Append(c);
					continue;
				}
				IL_0091:
				if (num == length)
				{
					return this;
				}
				num++;
				if (num == length || (c = format[num]) < '0' || c > '9')
				{
					StringBuilder.FormatError();
				}
				int num2 = 0;
				do
				{
					num2 = num2 * 10 + (int)c - 48;
					num++;
					if (num == length)
					{
						StringBuilder.FormatError();
					}
					c = format[num];
				}
				while (c >= '0' && c <= '9' && num2 < 1000000);
				if (num2 >= args.Length)
				{
					break;
				}
				while (num < length && (c = format[num]) == ' ')
				{
					num++;
				}
				bool flag = false;
				int num3 = 0;
				if (c == ',')
				{
					num++;
					while (num < length && format[num] == ' ')
					{
						num++;
					}
					if (num == length)
					{
						StringBuilder.FormatError();
					}
					c = format[num];
					if (c == '-')
					{
						flag = true;
						num++;
						if (num == length)
						{
							StringBuilder.FormatError();
						}
						c = format[num];
					}
					if (c < '0' || c > '9')
					{
						StringBuilder.FormatError();
					}
					do
					{
						num3 = num3 * 10 + (int)c - 48;
						num++;
						if (num == length)
						{
							StringBuilder.FormatError();
						}
						c = format[num];
						if (c < '0' || c > '9')
						{
							break;
						}
					}
					while (num3 < 1000000);
				}
				while (num < length && (c = format[num]) == ' ')
				{
					num++;
				}
				object obj = args[num2];
				string text = null;
				ReadOnlySpan<char> readOnlySpan = default(ReadOnlySpan<char>);
				if (c == ':')
				{
					num++;
					int num4 = num;
					for (;;)
					{
						if (num == length)
						{
							StringBuilder.FormatError();
						}
						c = format[num];
						num++;
						if (c == '}' || c == '{')
						{
							if (c == '{')
							{
								if (num < length && format[num] == '{')
								{
									num++;
								}
								else
								{
									StringBuilder.FormatError();
								}
							}
							else
							{
								if (num >= length || format[num] != '}')
								{
									break;
								}
								num++;
							}
							if (stringBuilder == null)
							{
								stringBuilder = new StringBuilder();
							}
							stringBuilder.Append(format, num4, num - num4 - 1);
							num4 = num;
						}
					}
					num--;
					if (stringBuilder == null || stringBuilder.Length == 0)
					{
						if (num4 != num)
						{
							readOnlySpan = format.AsSpan(num4, num - num4);
						}
					}
					else
					{
						stringBuilder.Append(format, num4, num - num4);
						readOnlySpan = (text = stringBuilder.ToString());
						stringBuilder.Clear();
					}
				}
				if (c != '}')
				{
					StringBuilder.FormatError();
				}
				num++;
				string text2 = null;
				if (customFormatter != null)
				{
					if (readOnlySpan.Length != 0 && text == null)
					{
						text = new string(readOnlySpan);
					}
					text2 = customFormatter.Format(text, obj, provider);
				}
				if (text2 == null)
				{
					ISpanFormattable spanFormattable = obj as ISpanFormattable;
					int num5;
					if (spanFormattable != null && (flag || num3 == 0) && spanFormattable.TryFormat(this.RemainingCurrentChunk, out num5, readOnlySpan, provider))
					{
						this.m_ChunkLength += num5;
						int num6 = num3 - num5;
						if (flag && num6 > 0)
						{
							this.Append(' ', num6);
							continue;
						}
						continue;
					}
					else
					{
						IFormattable formattable = obj as IFormattable;
						if (formattable != null)
						{
							if (readOnlySpan.Length != 0 && text == null)
							{
								text = new string(readOnlySpan);
							}
							text2 = formattable.ToString(text, provider);
						}
						else if (obj != null)
						{
							text2 = obj.ToString();
						}
					}
				}
				if (text2 == null)
				{
					text2 = string.Empty;
				}
				int num7 = num3 - text2.Length;
				if (!flag && num7 > 0)
				{
					this.Append(' ', num7);
				}
				this.Append(text2);
				if (flag && num7 > 0)
				{
					this.Append(' ', num7);
				}
			}
			throw new FormatException("Index (zero based) must be greater than or equal to zero and less than the size of the argument list.");
		}

		// Token: 0x06002683 RID: 9859 RVA: 0x0008A090 File Offset: 0x00088290
		public StringBuilder Replace(string oldValue, string newValue)
		{
			return this.Replace(oldValue, newValue, 0, this.Length);
		}

		// Token: 0x06002684 RID: 9860 RVA: 0x0008A0A4 File Offset: 0x000882A4
		public bool Equals(StringBuilder sb)
		{
			if (sb == null)
			{
				return false;
			}
			if (this.Capacity != sb.Capacity || this.MaxCapacity != sb.MaxCapacity || this.Length != sb.Length)
			{
				return false;
			}
			if (sb == this)
			{
				return true;
			}
			StringBuilder stringBuilder = this;
			int i = stringBuilder.m_ChunkLength;
			StringBuilder stringBuilder2 = sb;
			int j = stringBuilder2.m_ChunkLength;
			for (;;)
			{
				IL_0049:
				i--;
				j--;
				while (i < 0)
				{
					stringBuilder = stringBuilder.m_ChunkPrevious;
					if (stringBuilder != null)
					{
						i = stringBuilder.m_ChunkLength + i;
					}
					else
					{
						IL_007F:
						while (j < 0)
						{
							stringBuilder2 = stringBuilder2.m_ChunkPrevious;
							if (stringBuilder2 == null)
							{
								break;
							}
							j = stringBuilder2.m_ChunkLength + j;
						}
						if (i < 0)
						{
							goto Block_8;
						}
						if (j < 0)
						{
							return false;
						}
						if (stringBuilder.m_ChunkChars[i] != stringBuilder2.m_ChunkChars[j])
						{
							return false;
						}
						goto IL_0049;
					}
				}
				goto IL_007F;
			}
			Block_8:
			return j < 0;
		}

		// Token: 0x06002685 RID: 9861 RVA: 0x0008A158 File Offset: 0x00088358
		public bool Equals(ReadOnlySpan<char> span)
		{
			if (span.Length != this.Length)
			{
				return false;
			}
			StringBuilder stringBuilder = this;
			int num = 0;
			for (;;)
			{
				int chunkLength = stringBuilder.m_ChunkLength;
				num += chunkLength;
				if (!new ReadOnlySpan<char>(stringBuilder.m_ChunkChars, 0, chunkLength).EqualsOrdinal(span.Slice(span.Length - num, chunkLength)))
				{
					break;
				}
				stringBuilder = stringBuilder.m_ChunkPrevious;
				if (stringBuilder == null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002686 RID: 9862 RVA: 0x0008A1B8 File Offset: 0x000883B8
		public StringBuilder Replace(string oldValue, string newValue, int startIndex, int count)
		{
			int length = this.Length;
			if (startIndex > length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (count < 0 || startIndex > length - count)
			{
				throw new ArgumentOutOfRangeException("count", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (oldValue == null)
			{
				throw new ArgumentNullException("oldValue");
			}
			if (oldValue.Length == 0)
			{
				throw new ArgumentException("Empty name is not legal.", "oldValue");
			}
			newValue = newValue ?? string.Empty;
			int length2 = newValue.Length;
			int length3 = oldValue.Length;
			int[] array = null;
			int num = 0;
			StringBuilder stringBuilder = this.FindChunkForIndex(startIndex);
			int num2 = startIndex - stringBuilder.m_ChunkOffset;
			while (count > 0)
			{
				if (this.StartsWith(stringBuilder, num2, count, oldValue))
				{
					if (array == null)
					{
						array = new int[5];
					}
					else if (num >= array.Length)
					{
						Array.Resize<int>(ref array, array.Length * 3 / 2 + 4);
					}
					array[num++] = num2;
					num2 += oldValue.Length;
					count -= oldValue.Length;
				}
				else
				{
					num2++;
					count--;
				}
				if (num2 >= stringBuilder.m_ChunkLength || count == 0)
				{
					int num3 = num2 + stringBuilder.m_ChunkOffset;
					this.ReplaceAllInChunk(array, num, stringBuilder, oldValue.Length, newValue);
					num3 += (newValue.Length - oldValue.Length) * num;
					num = 0;
					stringBuilder = this.FindChunkForIndex(num3);
					num2 = num3 - stringBuilder.m_ChunkOffset;
				}
			}
			return this;
		}

		// Token: 0x06002687 RID: 9863 RVA: 0x0008A310 File Offset: 0x00088510
		public StringBuilder Replace(char oldChar, char newChar)
		{
			return this.Replace(oldChar, newChar, 0, this.Length);
		}

		// Token: 0x06002688 RID: 9864 RVA: 0x0008A324 File Offset: 0x00088524
		public StringBuilder Replace(char oldChar, char newChar, int startIndex, int count)
		{
			int length = this.Length;
			if (startIndex > length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (count < 0 || startIndex > length - count)
			{
				throw new ArgumentOutOfRangeException("count", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			int num = startIndex + count;
			StringBuilder stringBuilder = this;
			for (;;)
			{
				int num2 = num - stringBuilder.m_ChunkOffset;
				int num3 = startIndex - stringBuilder.m_ChunkOffset;
				if (num2 >= 0)
				{
					int i = Math.Max(num3, 0);
					int num4 = Math.Min(stringBuilder.m_ChunkLength, num2);
					while (i < num4)
					{
						if (stringBuilder.m_ChunkChars[i] == oldChar)
						{
							stringBuilder.m_ChunkChars[i] = newChar;
						}
						i++;
					}
				}
				if (num3 >= 0)
				{
					break;
				}
				stringBuilder = stringBuilder.m_ChunkPrevious;
			}
			return this;
		}

		// Token: 0x06002689 RID: 9865 RVA: 0x0008A3D4 File Offset: 0x000885D4
		[CLSCompliant(false)]
		public unsafe StringBuilder Append(char* value, int valueCount)
		{
			if (valueCount < 0)
			{
				throw new ArgumentOutOfRangeException("valueCount", "Count cannot be less than zero.");
			}
			int num = this.Length + valueCount;
			if (num > this.m_MaxCapacity || num < valueCount)
			{
				throw new ArgumentOutOfRangeException("valueCount", "The length cannot be greater than the capacity.");
			}
			int num2 = valueCount + this.m_ChunkLength;
			if (num2 <= this.m_ChunkChars.Length)
			{
				StringBuilder.ThreadSafeCopy(value, this.m_ChunkChars, this.m_ChunkLength, valueCount);
				this.m_ChunkLength = num2;
			}
			else
			{
				int num3 = this.m_ChunkChars.Length - this.m_ChunkLength;
				if (num3 > 0)
				{
					StringBuilder.ThreadSafeCopy(value, this.m_ChunkChars, this.m_ChunkLength, num3);
					this.m_ChunkLength = this.m_ChunkChars.Length;
				}
				int num4 = valueCount - num3;
				this.ExpandByABlock(num4);
				StringBuilder.ThreadSafeCopy(value + num3, this.m_ChunkChars, 0, num4);
				this.m_ChunkLength = num4;
			}
			return this;
		}

		// Token: 0x0600268A RID: 9866 RVA: 0x0008A4A8 File Offset: 0x000886A8
		private unsafe void Insert(int index, char* value, int valueCount)
		{
			if (index > this.Length)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (valueCount > 0)
			{
				StringBuilder stringBuilder;
				int num;
				this.MakeRoom(index, valueCount, out stringBuilder, out num, false);
				this.ReplaceInPlaceAtChunk(ref stringBuilder, ref num, value, valueCount);
			}
		}

		// Token: 0x0600268B RID: 9867 RVA: 0x0008A4EC File Offset: 0x000886EC
		private unsafe void ReplaceAllInChunk(int[] replacements, int replacementsCount, StringBuilder sourceChunk, int removeCount, string value)
		{
			if (replacementsCount <= 0)
			{
				return;
			}
			fixed (string text = value)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				int num = (value.Length - removeCount) * replacementsCount;
				StringBuilder stringBuilder = sourceChunk;
				int num2 = replacements[0];
				if (num > 0)
				{
					this.MakeRoom(stringBuilder.m_ChunkOffset + num2, num, out stringBuilder, out num2, true);
				}
				int num3 = 0;
				for (;;)
				{
					this.ReplaceInPlaceAtChunk(ref stringBuilder, ref num2, ptr, value.Length);
					int num4 = replacements[num3] + removeCount;
					num3++;
					if (num3 >= replacementsCount)
					{
						break;
					}
					int num5 = replacements[num3];
					if (num != 0)
					{
						fixed (char* ptr2 = &sourceChunk.m_ChunkChars[num4])
						{
							char* ptr3 = ptr2;
							this.ReplaceInPlaceAtChunk(ref stringBuilder, ref num2, ptr3, num5 - num4);
						}
					}
					else
					{
						num2 += num5 - num4;
					}
				}
				if (num < 0)
				{
					this.Remove(stringBuilder.m_ChunkOffset + num2, -num, out stringBuilder, out num2);
				}
			}
		}

		// Token: 0x0600268C RID: 9868 RVA: 0x0008A5C0 File Offset: 0x000887C0
		private bool StartsWith(StringBuilder chunk, int indexInChunk, int count, string value)
		{
			for (int i = 0; i < value.Length; i++)
			{
				if (count == 0)
				{
					return false;
				}
				if (indexInChunk >= chunk.m_ChunkLength)
				{
					chunk = this.Next(chunk);
					if (chunk == null)
					{
						return false;
					}
					indexInChunk = 0;
				}
				if (value[i] != chunk.m_ChunkChars[indexInChunk])
				{
					return false;
				}
				indexInChunk++;
				count--;
			}
			return true;
		}

		// Token: 0x0600268D RID: 9869 RVA: 0x0008A620 File Offset: 0x00088820
		private unsafe void ReplaceInPlaceAtChunk(ref StringBuilder chunk, ref int indexInChunk, char* value, int count)
		{
			if (count != 0)
			{
				for (;;)
				{
					int num = Math.Min(chunk.m_ChunkLength - indexInChunk, count);
					StringBuilder.ThreadSafeCopy(value, chunk.m_ChunkChars, indexInChunk, num);
					indexInChunk += num;
					if (indexInChunk >= chunk.m_ChunkLength)
					{
						chunk = this.Next(chunk);
						indexInChunk = 0;
					}
					count -= num;
					if (count == 0)
					{
						break;
					}
					value += num;
				}
			}
		}

		// Token: 0x0600268E RID: 9870 RVA: 0x0008A688 File Offset: 0x00088888
		private unsafe static void ThreadSafeCopy(char* sourcePtr, char[] destination, int destinationIndex, int count)
		{
			if (count <= 0)
			{
				return;
			}
			if (destinationIndex <= destination.Length && destinationIndex + count <= destination.Length)
			{
				fixed (char* ptr = &destination[destinationIndex])
				{
					string.wstrcpy(ptr, sourcePtr, count);
				}
				return;
			}
			throw new ArgumentOutOfRangeException("destinationIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
		}

		// Token: 0x0600268F RID: 9871 RVA: 0x0008A6CC File Offset: 0x000888CC
		private unsafe static void ThreadSafeCopy(char[] source, int sourceIndex, Span<char> destination, int destinationIndex, int count)
		{
			if (count > 0)
			{
				if (sourceIndex > source.Length || count > source.Length - sourceIndex)
				{
					throw new ArgumentOutOfRangeException("sourceIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				if (destinationIndex > destination.Length || count > destination.Length - destinationIndex)
				{
					throw new ArgumentOutOfRangeException("destinationIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				fixed (char* ptr = &source[sourceIndex])
				{
					char* ptr2 = ptr;
					fixed (char* reference = MemoryMarshal.GetReference<char>(destination))
					{
						string.wstrcpy(reference + destinationIndex, ptr2, count);
					}
				}
			}
		}

		// Token: 0x06002690 RID: 9872 RVA: 0x0008A74C File Offset: 0x0008894C
		private StringBuilder FindChunkForIndex(int index)
		{
			StringBuilder stringBuilder = this;
			while (stringBuilder.m_ChunkOffset > index)
			{
				stringBuilder = stringBuilder.m_ChunkPrevious;
			}
			return stringBuilder;
		}

		// Token: 0x06002691 RID: 9873 RVA: 0x0008A770 File Offset: 0x00088970
		private StringBuilder FindChunkForByte(int byteIndex)
		{
			StringBuilder stringBuilder = this;
			while (stringBuilder.m_ChunkOffset * 2 > byteIndex)
			{
				stringBuilder = stringBuilder.m_ChunkPrevious;
			}
			return stringBuilder;
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06002692 RID: 9874 RVA: 0x0008A794 File Offset: 0x00088994
		private Span<char> RemainingCurrentChunk
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Span<char>(this.m_ChunkChars, this.m_ChunkLength, this.m_ChunkChars.Length - this.m_ChunkLength);
			}
		}

		// Token: 0x06002693 RID: 9875 RVA: 0x0008A7B6 File Offset: 0x000889B6
		private StringBuilder Next(StringBuilder chunk)
		{
			if (chunk != this)
			{
				return this.FindChunkForIndex(chunk.m_ChunkOffset + chunk.m_ChunkLength);
			}
			return null;
		}

		// Token: 0x06002694 RID: 9876 RVA: 0x0008A7D4 File Offset: 0x000889D4
		private void ExpandByABlock(int minBlockCharCount)
		{
			if (minBlockCharCount + this.Length > this.m_MaxCapacity || minBlockCharCount + this.Length < minBlockCharCount)
			{
				throw new ArgumentOutOfRangeException("requiredLength", "capacity was less than the current size.");
			}
			int num = Math.Max(minBlockCharCount, Math.Min(this.Length, 8000));
			this.m_ChunkPrevious = new StringBuilder(this);
			this.m_ChunkOffset += this.m_ChunkLength;
			this.m_ChunkLength = 0;
			if (this.m_ChunkOffset + num < num)
			{
				this.m_ChunkChars = null;
				throw new OutOfMemoryException();
			}
			this.m_ChunkChars = new char[num];
		}

		// Token: 0x06002695 RID: 9877 RVA: 0x0008A870 File Offset: 0x00088A70
		private StringBuilder(StringBuilder from)
		{
			this.m_ChunkLength = from.m_ChunkLength;
			this.m_ChunkOffset = from.m_ChunkOffset;
			this.m_ChunkChars = from.m_ChunkChars;
			this.m_ChunkPrevious = from.m_ChunkPrevious;
			this.m_MaxCapacity = from.m_MaxCapacity;
		}

		// Token: 0x06002696 RID: 9878 RVA: 0x0008A8C0 File Offset: 0x00088AC0
		private unsafe void MakeRoom(int index, int count, out StringBuilder chunk, out int indexInChunk, bool doNotMoveFollowingChars)
		{
			if (count + this.Length > this.m_MaxCapacity || count + this.Length < count)
			{
				throw new ArgumentOutOfRangeException("requiredLength", "capacity was less than the current size.");
			}
			chunk = this;
			while (chunk.m_ChunkOffset > index)
			{
				chunk.m_ChunkOffset += count;
				chunk = chunk.m_ChunkPrevious;
			}
			indexInChunk = index - chunk.m_ChunkOffset;
			if (!doNotMoveFollowingChars && chunk.m_ChunkLength <= 32 && chunk.m_ChunkChars.Length - chunk.m_ChunkLength >= count)
			{
				int i = chunk.m_ChunkLength;
				while (i > indexInChunk)
				{
					i--;
					chunk.m_ChunkChars[i + count] = chunk.m_ChunkChars[i];
				}
				chunk.m_ChunkLength += count;
				return;
			}
			StringBuilder stringBuilder = new StringBuilder(Math.Max(count, 16), chunk.m_MaxCapacity, chunk.m_ChunkPrevious);
			stringBuilder.m_ChunkLength = count;
			int num = Math.Min(count, indexInChunk);
			if (num > 0)
			{
				fixed (char* ptr = &chunk.m_ChunkChars[0])
				{
					char* ptr2 = ptr;
					StringBuilder.ThreadSafeCopy(ptr2, stringBuilder.m_ChunkChars, 0, num);
					int num2 = indexInChunk - num;
					if (num2 >= 0)
					{
						StringBuilder.ThreadSafeCopy(ptr2 + num, chunk.m_ChunkChars, 0, num2);
						indexInChunk = num2;
					}
				}
			}
			chunk.m_ChunkPrevious = stringBuilder;
			chunk.m_ChunkOffset += count;
			if (num < count)
			{
				chunk = stringBuilder;
				indexInChunk = num;
			}
		}

		// Token: 0x06002697 RID: 9879 RVA: 0x0008AA24 File Offset: 0x00088C24
		private StringBuilder(int size, int maxCapacity, StringBuilder previousBlock)
		{
			this.m_ChunkChars = new char[size];
			this.m_MaxCapacity = maxCapacity;
			this.m_ChunkPrevious = previousBlock;
			if (previousBlock != null)
			{
				this.m_ChunkOffset = previousBlock.m_ChunkOffset + previousBlock.m_ChunkLength;
			}
		}

		// Token: 0x06002698 RID: 9880 RVA: 0x0008AA5C File Offset: 0x00088C5C
		private void Remove(int startIndex, int count, out StringBuilder chunk, out int indexInChunk)
		{
			int num = startIndex + count;
			chunk = this;
			StringBuilder stringBuilder = null;
			int num2 = 0;
			for (;;)
			{
				if (num - chunk.m_ChunkOffset >= 0)
				{
					if (stringBuilder == null)
					{
						stringBuilder = chunk;
						num2 = num - stringBuilder.m_ChunkOffset;
					}
					if (startIndex - chunk.m_ChunkOffset >= 0)
					{
						break;
					}
				}
				else
				{
					chunk.m_ChunkOffset -= count;
				}
				chunk = chunk.m_ChunkPrevious;
			}
			indexInChunk = startIndex - chunk.m_ChunkOffset;
			int num3 = indexInChunk;
			int num4 = stringBuilder.m_ChunkLength - num2;
			if (stringBuilder != chunk)
			{
				num3 = 0;
				chunk.m_ChunkLength = indexInChunk;
				stringBuilder.m_ChunkPrevious = chunk;
				stringBuilder.m_ChunkOffset = chunk.m_ChunkOffset + chunk.m_ChunkLength;
				if (indexInChunk == 0)
				{
					stringBuilder.m_ChunkPrevious = chunk.m_ChunkPrevious;
					chunk = stringBuilder;
				}
			}
			stringBuilder.m_ChunkLength -= num2 - num3;
			if (num3 != num2)
			{
				StringBuilder.ThreadSafeCopy(stringBuilder.m_ChunkChars, num2, stringBuilder.m_ChunkChars, num3, num4);
			}
		}

		// Token: 0x04001C9B RID: 7323
		internal char[] m_ChunkChars;

		// Token: 0x04001C9C RID: 7324
		internal StringBuilder m_ChunkPrevious;

		// Token: 0x04001C9D RID: 7325
		internal int m_ChunkLength;

		// Token: 0x04001C9E RID: 7326
		internal int m_ChunkOffset;

		// Token: 0x04001C9F RID: 7327
		internal int m_MaxCapacity;

		// Token: 0x04001CA0 RID: 7328
		internal const int DefaultCapacity = 16;

		// Token: 0x04001CA1 RID: 7329
		private const string CapacityField = "Capacity";

		// Token: 0x04001CA2 RID: 7330
		private const string MaxCapacityField = "m_MaxCapacity";

		// Token: 0x04001CA3 RID: 7331
		private const string StringValueField = "m_StringValue";

		// Token: 0x04001CA4 RID: 7332
		private const string ThreadIDField = "m_currentThread";

		// Token: 0x04001CA5 RID: 7333
		internal const int MaxChunkSize = 8000;

		// Token: 0x04001CA6 RID: 7334
		private const int IndexLimit = 1000000;

		// Token: 0x04001CA7 RID: 7335
		private const int WidthLimit = 1000000;
	}
}
