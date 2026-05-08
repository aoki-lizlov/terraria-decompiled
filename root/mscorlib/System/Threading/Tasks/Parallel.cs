using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;

namespace System.Threading.Tasks
{
	// Token: 0x020002ED RID: 749
	public static class Parallel
	{
		// Token: 0x0600219E RID: 8606 RVA: 0x0007957C File Offset: 0x0007777C
		public static void Invoke(params Action[] actions)
		{
			Parallel.Invoke(Parallel.s_defaultParallelOptions, actions);
		}

		// Token: 0x0600219F RID: 8607 RVA: 0x0007958C File Offset: 0x0007778C
		public static void Invoke(ParallelOptions parallelOptions, params Action[] actions)
		{
			if (actions == null)
			{
				throw new ArgumentNullException("actions");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			parallelOptions.CancellationToken.ThrowIfCancellationRequested();
			Action[] actionsCopy = new Action[actions.Length];
			for (int i = 0; i < actionsCopy.Length; i++)
			{
				actionsCopy[i] = actions[i];
				if (actionsCopy[i] == null)
				{
					throw new ArgumentException("One of the actions was null.");
				}
			}
			int num = 0;
			if (ParallelEtwProvider.Log.IsEnabled())
			{
				num = Interlocked.Increment(ref Parallel.s_forkJoinContextID);
				ParallelEtwProvider.Log.ParallelInvokeBegin(TaskScheduler.Current.Id, Task.CurrentId.GetValueOrDefault(), num, ParallelEtwProvider.ForkJoinOperationType.ParallelInvoke, actionsCopy.Length);
			}
			if (actionsCopy.Length < 1)
			{
				return;
			}
			try
			{
				if (actionsCopy.Length > 10 || (parallelOptions.MaxDegreeOfParallelism != -1 && parallelOptions.MaxDegreeOfParallelism < actionsCopy.Length))
				{
					ConcurrentQueue<Exception> exceptionQ = null;
					int actionIndex = 0;
					try
					{
						TaskReplicator.Run<object>(delegate(ref object state, int timeout, out bool replicationDelegateYieldedBeforeCompletion)
						{
							replicationDelegateYieldedBeforeCompletion = false;
							for (int k = Interlocked.Increment(ref actionIndex); k <= actionsCopy.Length; k = Interlocked.Increment(ref actionIndex))
							{
								try
								{
									actionsCopy[k - 1]();
								}
								catch (Exception ex5)
								{
									LazyInitializer.EnsureInitialized<ConcurrentQueue<Exception>>(ref exceptionQ, () => new ConcurrentQueue<Exception>());
									exceptionQ.Enqueue(ex5);
								}
								parallelOptions.CancellationToken.ThrowIfCancellationRequested();
							}
						}, parallelOptions, false);
					}
					catch (Exception ex)
					{
						LazyInitializer.EnsureInitialized<ConcurrentQueue<Exception>>(ref exceptionQ, () => new ConcurrentQueue<Exception>());
						if (ex is ObjectDisposedException)
						{
							throw;
						}
						AggregateException ex2 = ex as AggregateException;
						if (ex2 != null)
						{
							using (IEnumerator<Exception> enumerator = ex2.InnerExceptions.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									Exception ex3 = enumerator.Current;
									exceptionQ.Enqueue(ex3);
								}
								goto IL_01C3;
							}
						}
						exceptionQ.Enqueue(ex);
						IL_01C3:;
					}
					if (exceptionQ != null && exceptionQ.Count > 0)
					{
						Parallel.ThrowSingleCancellationExceptionOrOtherException(exceptionQ, parallelOptions.CancellationToken, new AggregateException(exceptionQ));
					}
				}
				else
				{
					Task[] array = new Task[actionsCopy.Length];
					parallelOptions.CancellationToken.ThrowIfCancellationRequested();
					for (int j = 1; j < array.Length; j++)
					{
						array[j] = Task.Factory.StartNew(actionsCopy[j], parallelOptions.CancellationToken, TaskCreationOptions.None, parallelOptions.EffectiveTaskScheduler);
					}
					array[0] = new Task(actionsCopy[0], parallelOptions.CancellationToken, TaskCreationOptions.None);
					array[0].RunSynchronously(parallelOptions.EffectiveTaskScheduler);
					try
					{
						Task.WaitAll(array);
					}
					catch (AggregateException ex4)
					{
						Parallel.ThrowSingleCancellationExceptionOrOtherException(ex4.InnerExceptions, parallelOptions.CancellationToken, ex4);
					}
				}
			}
			finally
			{
				if (ParallelEtwProvider.Log.IsEnabled())
				{
					ParallelEtwProvider.Log.ParallelInvokeEnd(TaskScheduler.Current.Id, Task.CurrentId.GetValueOrDefault(), num);
				}
			}
		}

		// Token: 0x060021A0 RID: 8608 RVA: 0x000798F4 File Offset: 0x00077AF4
		public static ParallelLoopResult For(int fromInclusive, int toExclusive, Action<int> body)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			return Parallel.ForWorker<object>(fromInclusive, toExclusive, Parallel.s_defaultParallelOptions, body, null, null, null, null);
		}

		// Token: 0x060021A1 RID: 8609 RVA: 0x00079915 File Offset: 0x00077B15
		public static ParallelLoopResult For(long fromInclusive, long toExclusive, Action<long> body)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			return Parallel.ForWorker64<object>(fromInclusive, toExclusive, Parallel.s_defaultParallelOptions, body, null, null, null, null);
		}

		// Token: 0x060021A2 RID: 8610 RVA: 0x00079936 File Offset: 0x00077B36
		public static ParallelLoopResult For(int fromInclusive, int toExclusive, ParallelOptions parallelOptions, Action<int> body)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForWorker<object>(fromInclusive, toExclusive, parallelOptions, body, null, null, null, null);
		}

		// Token: 0x060021A3 RID: 8611 RVA: 0x00079961 File Offset: 0x00077B61
		public static ParallelLoopResult For(long fromInclusive, long toExclusive, ParallelOptions parallelOptions, Action<long> body)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForWorker64<object>(fromInclusive, toExclusive, parallelOptions, body, null, null, null, null);
		}

		// Token: 0x060021A4 RID: 8612 RVA: 0x0007998C File Offset: 0x00077B8C
		public static ParallelLoopResult For(int fromInclusive, int toExclusive, Action<int, ParallelLoopState> body)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			return Parallel.ForWorker<object>(fromInclusive, toExclusive, Parallel.s_defaultParallelOptions, null, body, null, null, null);
		}

		// Token: 0x060021A5 RID: 8613 RVA: 0x000799AD File Offset: 0x00077BAD
		public static ParallelLoopResult For(long fromInclusive, long toExclusive, Action<long, ParallelLoopState> body)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			return Parallel.ForWorker64<object>(fromInclusive, toExclusive, Parallel.s_defaultParallelOptions, null, body, null, null, null);
		}

		// Token: 0x060021A6 RID: 8614 RVA: 0x000799CE File Offset: 0x00077BCE
		public static ParallelLoopResult For(int fromInclusive, int toExclusive, ParallelOptions parallelOptions, Action<int, ParallelLoopState> body)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForWorker<object>(fromInclusive, toExclusive, parallelOptions, null, body, null, null, null);
		}

		// Token: 0x060021A7 RID: 8615 RVA: 0x000799F9 File Offset: 0x00077BF9
		public static ParallelLoopResult For(long fromInclusive, long toExclusive, ParallelOptions parallelOptions, Action<long, ParallelLoopState> body)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForWorker64<object>(fromInclusive, toExclusive, parallelOptions, null, body, null, null, null);
		}

		// Token: 0x060021A8 RID: 8616 RVA: 0x00079A24 File Offset: 0x00077C24
		public static ParallelLoopResult For<TLocal>(int fromInclusive, int toExclusive, Func<TLocal> localInit, Func<int, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			return Parallel.ForWorker<TLocal>(fromInclusive, toExclusive, Parallel.s_defaultParallelOptions, null, null, body, localInit, localFinally);
		}

		// Token: 0x060021A9 RID: 8617 RVA: 0x00079A63 File Offset: 0x00077C63
		public static ParallelLoopResult For<TLocal>(long fromInclusive, long toExclusive, Func<TLocal> localInit, Func<long, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			return Parallel.ForWorker64<TLocal>(fromInclusive, toExclusive, Parallel.s_defaultParallelOptions, null, null, body, localInit, localFinally);
		}

		// Token: 0x060021AA RID: 8618 RVA: 0x00079AA4 File Offset: 0x00077CA4
		public static ParallelLoopResult For<TLocal>(int fromInclusive, int toExclusive, ParallelOptions parallelOptions, Func<TLocal> localInit, Func<int, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForWorker<TLocal>(fromInclusive, toExclusive, parallelOptions, null, null, body, localInit, localFinally);
		}

		// Token: 0x060021AB RID: 8619 RVA: 0x00079AFC File Offset: 0x00077CFC
		public static ParallelLoopResult For<TLocal>(long fromInclusive, long toExclusive, ParallelOptions parallelOptions, Func<TLocal> localInit, Func<long, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForWorker64<TLocal>(fromInclusive, toExclusive, parallelOptions, null, null, body, localInit, localFinally);
		}

		// Token: 0x060021AC RID: 8620 RVA: 0x00079B54 File Offset: 0x00077D54
		private static bool CheckTimeoutReached(int timeoutOccursAt)
		{
			int tickCount = Environment.TickCount;
			return tickCount >= timeoutOccursAt && (0 <= timeoutOccursAt || 0 >= tickCount);
		}

		// Token: 0x060021AD RID: 8621 RVA: 0x00079B78 File Offset: 0x00077D78
		private static int ComputeTimeoutPoint(int timeoutLength)
		{
			return Environment.TickCount + timeoutLength;
		}

		// Token: 0x060021AE RID: 8622 RVA: 0x00079B84 File Offset: 0x00077D84
		private static ParallelLoopResult ForWorker<TLocal>(int fromInclusive, int toExclusive, ParallelOptions parallelOptions, Action<int> body, Action<int, ParallelLoopState> bodyWithState, Func<int, ParallelLoopState, TLocal, TLocal> bodyWithLocal, Func<TLocal> localInit, Action<TLocal> localFinally)
		{
			ParallelLoopResult parallelLoopResult = default(ParallelLoopResult);
			if (toExclusive <= fromInclusive)
			{
				parallelLoopResult._completed = true;
				return parallelLoopResult;
			}
			ParallelLoopStateFlags32 sharedPStateFlags = new ParallelLoopStateFlags32();
			parallelOptions.CancellationToken.ThrowIfCancellationRequested();
			int num = ((parallelOptions.EffectiveMaxConcurrencyLevel == -1) ? PlatformHelper.ProcessorCount : parallelOptions.EffectiveMaxConcurrencyLevel);
			RangeManager rangeManager = new RangeManager((long)fromInclusive, (long)toExclusive, 1L, num);
			OperationCanceledException oce = null;
			CancellationTokenRegistration cancellationTokenRegistration = ((!parallelOptions.CancellationToken.CanBeCanceled) ? default(CancellationTokenRegistration) : parallelOptions.CancellationToken.Register(delegate(object o)
			{
				oce = new OperationCanceledException(parallelOptions.CancellationToken);
				sharedPStateFlags.Cancel();
			}, null, false));
			int forkJoinContextID = 0;
			if (ParallelEtwProvider.Log.IsEnabled())
			{
				forkJoinContextID = Interlocked.Increment(ref Parallel.s_forkJoinContextID);
				ParallelEtwProvider.Log.ParallelLoopBegin(TaskScheduler.Current.Id, Task.CurrentId.GetValueOrDefault(), forkJoinContextID, ParallelEtwProvider.ForkJoinOperationType.ParallelFor, (long)fromInclusive, (long)toExclusive);
			}
			try
			{
				try
				{
					TaskReplicator.Run<RangeWorker>(delegate(ref RangeWorker currentWorker, int timeout, out bool replicationDelegateYieldedBeforeCompletion)
					{
						if (!currentWorker.IsInitialized)
						{
							currentWorker = rangeManager.RegisterNewWorker();
						}
						replicationDelegateYieldedBeforeCompletion = false;
						int num3;
						int num4;
						if (!currentWorker.FindNewWork32(out num3, out num4) || sharedPStateFlags.ShouldExitLoop(num3))
						{
							return;
						}
						if (ParallelEtwProvider.Log.IsEnabled())
						{
							ParallelEtwProvider.Log.ParallelFork(TaskScheduler.Current.Id, Task.CurrentId.GetValueOrDefault(), forkJoinContextID);
						}
						TLocal tlocal = default(TLocal);
						bool flag = false;
						try
						{
							ParallelLoopState32 parallelLoopState = null;
							if (bodyWithState != null)
							{
								parallelLoopState = new ParallelLoopState32(sharedPStateFlags);
							}
							else if (bodyWithLocal != null)
							{
								parallelLoopState = new ParallelLoopState32(sharedPStateFlags);
								if (localInit != null)
								{
									tlocal = localInit();
									flag = true;
								}
							}
							int num5 = Parallel.ComputeTimeoutPoint(timeout);
							for (;;)
							{
								if (body != null)
								{
									for (int i = num3; i < num4; i++)
									{
										if (sharedPStateFlags.LoopStateFlags != 0 && sharedPStateFlags.ShouldExitLoop())
										{
											break;
										}
										body(i);
									}
								}
								else if (bodyWithState != null)
								{
									for (int j = num3; j < num4; j++)
									{
										if (sharedPStateFlags.LoopStateFlags != 0 && sharedPStateFlags.ShouldExitLoop(j))
										{
											break;
										}
										parallelLoopState.CurrentIteration = j;
										bodyWithState(j, parallelLoopState);
									}
								}
								else
								{
									int num6 = num3;
									while (num6 < num4 && (sharedPStateFlags.LoopStateFlags == 0 || !sharedPStateFlags.ShouldExitLoop(num6)))
									{
										parallelLoopState.CurrentIteration = num6;
										tlocal = bodyWithLocal(num6, parallelLoopState, tlocal);
										num6++;
									}
								}
								if (Parallel.CheckTimeoutReached(num5))
								{
									break;
								}
								if (!currentWorker.FindNewWork32(out num3, out num4) || (sharedPStateFlags.LoopStateFlags != 0 && sharedPStateFlags.ShouldExitLoop(num3)))
								{
									goto IL_01D8;
								}
							}
							replicationDelegateYieldedBeforeCompletion = true;
							IL_01D8:;
						}
						catch (Exception ex2)
						{
							sharedPStateFlags.SetExceptional();
							ExceptionDispatchInfo.Throw(ex2);
						}
						finally
						{
							if (localFinally != null && flag)
							{
								localFinally(tlocal);
							}
							if (ParallelEtwProvider.Log.IsEnabled())
							{
								ParallelEtwProvider.Log.ParallelJoin(TaskScheduler.Current.Id, Task.CurrentId.GetValueOrDefault(), forkJoinContextID);
							}
						}
					}, parallelOptions, true);
				}
				finally
				{
					if (parallelOptions.CancellationToken.CanBeCanceled)
					{
						cancellationTokenRegistration.Dispose();
					}
				}
				if (oce != null)
				{
					throw oce;
				}
			}
			catch (AggregateException ex)
			{
				Parallel.ThrowSingleCancellationExceptionOrOtherException(ex.InnerExceptions, parallelOptions.CancellationToken, ex);
			}
			finally
			{
				int loopStateFlags = sharedPStateFlags.LoopStateFlags;
				parallelLoopResult._completed = loopStateFlags == 0;
				if ((loopStateFlags & 2) != 0)
				{
					parallelLoopResult._lowestBreakIteration = new long?((long)sharedPStateFlags.LowestBreakIteration);
				}
				if (ParallelEtwProvider.Log.IsEnabled())
				{
					int num2;
					if (loopStateFlags == 0)
					{
						num2 = toExclusive - fromInclusive;
					}
					else if ((loopStateFlags & 2) != 0)
					{
						num2 = sharedPStateFlags.LowestBreakIteration - fromInclusive;
					}
					else
					{
						num2 = -1;
					}
					ParallelEtwProvider.Log.ParallelLoopEnd(TaskScheduler.Current.Id, Task.CurrentId.GetValueOrDefault(), forkJoinContextID, (long)num2);
				}
			}
			return parallelLoopResult;
		}

		// Token: 0x060021AF RID: 8623 RVA: 0x00079DF8 File Offset: 0x00077FF8
		private static ParallelLoopResult ForWorker64<TLocal>(long fromInclusive, long toExclusive, ParallelOptions parallelOptions, Action<long> body, Action<long, ParallelLoopState> bodyWithState, Func<long, ParallelLoopState, TLocal, TLocal> bodyWithLocal, Func<TLocal> localInit, Action<TLocal> localFinally)
		{
			ParallelLoopResult parallelLoopResult = default(ParallelLoopResult);
			if (toExclusive <= fromInclusive)
			{
				parallelLoopResult._completed = true;
				return parallelLoopResult;
			}
			ParallelLoopStateFlags64 sharedPStateFlags = new ParallelLoopStateFlags64();
			parallelOptions.CancellationToken.ThrowIfCancellationRequested();
			int num = ((parallelOptions.EffectiveMaxConcurrencyLevel == -1) ? PlatformHelper.ProcessorCount : parallelOptions.EffectiveMaxConcurrencyLevel);
			RangeManager rangeManager = new RangeManager(fromInclusive, toExclusive, 1L, num);
			OperationCanceledException oce = null;
			CancellationTokenRegistration cancellationTokenRegistration = ((!parallelOptions.CancellationToken.CanBeCanceled) ? default(CancellationTokenRegistration) : parallelOptions.CancellationToken.Register(delegate(object o)
			{
				oce = new OperationCanceledException(parallelOptions.CancellationToken);
				sharedPStateFlags.Cancel();
			}, null, false));
			int forkJoinContextID = 0;
			if (ParallelEtwProvider.Log.IsEnabled())
			{
				forkJoinContextID = Interlocked.Increment(ref Parallel.s_forkJoinContextID);
				ParallelEtwProvider.Log.ParallelLoopBegin(TaskScheduler.Current.Id, Task.CurrentId.GetValueOrDefault(), forkJoinContextID, ParallelEtwProvider.ForkJoinOperationType.ParallelFor, fromInclusive, toExclusive);
			}
			try
			{
				try
				{
					TaskReplicator.Run<RangeWorker>(delegate(ref RangeWorker currentWorker, int timeout, out bool replicationDelegateYieldedBeforeCompletion)
					{
						if (!currentWorker.IsInitialized)
						{
							currentWorker = rangeManager.RegisterNewWorker();
						}
						replicationDelegateYieldedBeforeCompletion = false;
						long num3;
						long num4;
						if (!currentWorker.FindNewWork(out num3, out num4) || sharedPStateFlags.ShouldExitLoop(num3))
						{
							return;
						}
						if (ParallelEtwProvider.Log.IsEnabled())
						{
							ParallelEtwProvider.Log.ParallelFork(TaskScheduler.Current.Id, Task.CurrentId.GetValueOrDefault(), forkJoinContextID);
						}
						TLocal tlocal = default(TLocal);
						bool flag = false;
						try
						{
							ParallelLoopState64 parallelLoopState = null;
							if (bodyWithState != null)
							{
								parallelLoopState = new ParallelLoopState64(sharedPStateFlags);
							}
							else if (bodyWithLocal != null)
							{
								parallelLoopState = new ParallelLoopState64(sharedPStateFlags);
								if (localInit != null)
								{
									tlocal = localInit();
									flag = true;
								}
							}
							int num5 = Parallel.ComputeTimeoutPoint(timeout);
							for (;;)
							{
								if (body != null)
								{
									for (long num6 = num3; num6 < num4; num6 += 1L)
									{
										if (sharedPStateFlags.LoopStateFlags != 0 && sharedPStateFlags.ShouldExitLoop())
										{
											break;
										}
										body(num6);
									}
								}
								else if (bodyWithState != null)
								{
									for (long num7 = num3; num7 < num4; num7 += 1L)
									{
										if (sharedPStateFlags.LoopStateFlags != 0 && sharedPStateFlags.ShouldExitLoop(num7))
										{
											break;
										}
										parallelLoopState.CurrentIteration = num7;
										bodyWithState(num7, parallelLoopState);
									}
								}
								else
								{
									long num8 = num3;
									while (num8 < num4 && (sharedPStateFlags.LoopStateFlags == 0 || !sharedPStateFlags.ShouldExitLoop(num8)))
									{
										parallelLoopState.CurrentIteration = num8;
										tlocal = bodyWithLocal(num8, parallelLoopState, tlocal);
										num8 += 1L;
									}
								}
								if (Parallel.CheckTimeoutReached(num5))
								{
									break;
								}
								if (!currentWorker.FindNewWork(out num3, out num4) || (sharedPStateFlags.LoopStateFlags != 0 && sharedPStateFlags.ShouldExitLoop(num3)))
								{
									goto IL_01DB;
								}
							}
							replicationDelegateYieldedBeforeCompletion = true;
							IL_01DB:;
						}
						catch (Exception ex2)
						{
							sharedPStateFlags.SetExceptional();
							ExceptionDispatchInfo.Throw(ex2);
						}
						finally
						{
							if (localFinally != null && flag)
							{
								localFinally(tlocal);
							}
							if (ParallelEtwProvider.Log.IsEnabled())
							{
								ParallelEtwProvider.Log.ParallelJoin(TaskScheduler.Current.Id, Task.CurrentId.GetValueOrDefault(), forkJoinContextID);
							}
						}
					}, parallelOptions, true);
				}
				finally
				{
					if (parallelOptions.CancellationToken.CanBeCanceled)
					{
						cancellationTokenRegistration.Dispose();
					}
				}
				if (oce != null)
				{
					throw oce;
				}
			}
			catch (AggregateException ex)
			{
				Parallel.ThrowSingleCancellationExceptionOrOtherException(ex.InnerExceptions, parallelOptions.CancellationToken, ex);
			}
			finally
			{
				int loopStateFlags = sharedPStateFlags.LoopStateFlags;
				parallelLoopResult._completed = loopStateFlags == 0;
				if ((loopStateFlags & 2) != 0)
				{
					parallelLoopResult._lowestBreakIteration = new long?(sharedPStateFlags.LowestBreakIteration);
				}
				if (ParallelEtwProvider.Log.IsEnabled())
				{
					long num2;
					if (loopStateFlags == 0)
					{
						num2 = toExclusive - fromInclusive;
					}
					else if ((loopStateFlags & 2) != 0)
					{
						num2 = sharedPStateFlags.LowestBreakIteration - fromInclusive;
					}
					else
					{
						num2 = -1L;
					}
					ParallelEtwProvider.Log.ParallelLoopEnd(TaskScheduler.Current.Id, Task.CurrentId.GetValueOrDefault(), forkJoinContextID, num2);
				}
			}
			return parallelLoopResult;
		}

		// Token: 0x060021B0 RID: 8624 RVA: 0x0007A068 File Offset: 0x00078268
		public static ParallelLoopResult ForEach<TSource>(IEnumerable<TSource> source, Action<TSource> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			return Parallel.ForEachWorker<TSource, object>(source, Parallel.s_defaultParallelOptions, body, null, null, null, null, null, null);
		}

		// Token: 0x060021B1 RID: 8625 RVA: 0x0007A0A4 File Offset: 0x000782A4
		public static ParallelLoopResult ForEach<TSource>(IEnumerable<TSource> source, ParallelOptions parallelOptions, Action<TSource> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForEachWorker<TSource, object>(source, parallelOptions, body, null, null, null, null, null, null);
		}

		// Token: 0x060021B2 RID: 8626 RVA: 0x0007A0EC File Offset: 0x000782EC
		public static ParallelLoopResult ForEach<TSource>(IEnumerable<TSource> source, Action<TSource, ParallelLoopState> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			return Parallel.ForEachWorker<TSource, object>(source, Parallel.s_defaultParallelOptions, null, body, null, null, null, null, null);
		}

		// Token: 0x060021B3 RID: 8627 RVA: 0x0007A128 File Offset: 0x00078328
		public static ParallelLoopResult ForEach<TSource>(IEnumerable<TSource> source, ParallelOptions parallelOptions, Action<TSource, ParallelLoopState> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForEachWorker<TSource, object>(source, parallelOptions, null, body, null, null, null, null, null);
		}

		// Token: 0x060021B4 RID: 8628 RVA: 0x0007A170 File Offset: 0x00078370
		public static ParallelLoopResult ForEach<TSource>(IEnumerable<TSource> source, Action<TSource, ParallelLoopState, long> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			return Parallel.ForEachWorker<TSource, object>(source, Parallel.s_defaultParallelOptions, null, null, body, null, null, null, null);
		}

		// Token: 0x060021B5 RID: 8629 RVA: 0x0007A1AC File Offset: 0x000783AC
		public static ParallelLoopResult ForEach<TSource>(IEnumerable<TSource> source, ParallelOptions parallelOptions, Action<TSource, ParallelLoopState, long> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForEachWorker<TSource, object>(source, parallelOptions, null, null, body, null, null, null, null);
		}

		// Token: 0x060021B6 RID: 8630 RVA: 0x0007A1F4 File Offset: 0x000783F4
		public static ParallelLoopResult ForEach<TSource, TLocal>(IEnumerable<TSource> source, Func<TLocal> localInit, Func<TSource, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			return Parallel.ForEachWorker<TSource, TLocal>(source, Parallel.s_defaultParallelOptions, null, null, null, body, null, localInit, localFinally);
		}

		// Token: 0x060021B7 RID: 8631 RVA: 0x0007A24C File Offset: 0x0007844C
		public static ParallelLoopResult ForEach<TSource, TLocal>(IEnumerable<TSource> source, ParallelOptions parallelOptions, Func<TLocal> localInit, Func<TSource, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForEachWorker<TSource, TLocal>(source, parallelOptions, null, null, null, body, null, localInit, localFinally);
		}

		// Token: 0x060021B8 RID: 8632 RVA: 0x0007A2B0 File Offset: 0x000784B0
		public static ParallelLoopResult ForEach<TSource, TLocal>(IEnumerable<TSource> source, Func<TLocal> localInit, Func<TSource, ParallelLoopState, long, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			return Parallel.ForEachWorker<TSource, TLocal>(source, Parallel.s_defaultParallelOptions, null, null, null, null, body, localInit, localFinally);
		}

		// Token: 0x060021B9 RID: 8633 RVA: 0x0007A308 File Offset: 0x00078508
		public static ParallelLoopResult ForEach<TSource, TLocal>(IEnumerable<TSource> source, ParallelOptions parallelOptions, Func<TLocal> localInit, Func<TSource, ParallelLoopState, long, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForEachWorker<TSource, TLocal>(source, parallelOptions, null, null, null, null, body, localInit, localFinally);
		}

		// Token: 0x060021BA RID: 8634 RVA: 0x0007A36C File Offset: 0x0007856C
		private static ParallelLoopResult ForEachWorker<TSource, TLocal>(IEnumerable<TSource> source, ParallelOptions parallelOptions, Action<TSource> body, Action<TSource, ParallelLoopState> bodyWithState, Action<TSource, ParallelLoopState, long> bodyWithStateAndIndex, Func<TSource, ParallelLoopState, TLocal, TLocal> bodyWithStateAndLocal, Func<TSource, ParallelLoopState, long, TLocal, TLocal> bodyWithEverything, Func<TLocal> localInit, Action<TLocal> localFinally)
		{
			parallelOptions.CancellationToken.ThrowIfCancellationRequested();
			TSource[] array = source as TSource[];
			if (array != null)
			{
				return Parallel.ForEachWorker<TSource, TLocal>(array, parallelOptions, body, bodyWithState, bodyWithStateAndIndex, bodyWithStateAndLocal, bodyWithEverything, localInit, localFinally);
			}
			IList<TSource> list = source as IList<TSource>;
			if (list != null)
			{
				return Parallel.ForEachWorker<TSource, TLocal>(list, parallelOptions, body, bodyWithState, bodyWithStateAndIndex, bodyWithStateAndLocal, bodyWithEverything, localInit, localFinally);
			}
			return Parallel.PartitionerForEachWorker<TSource, TLocal>(Partitioner.Create<TSource>(source), parallelOptions, body, bodyWithState, bodyWithStateAndIndex, bodyWithStateAndLocal, bodyWithEverything, localInit, localFinally);
		}

		// Token: 0x060021BB RID: 8635 RVA: 0x0007A3DC File Offset: 0x000785DC
		private static ParallelLoopResult ForEachWorker<TSource, TLocal>(TSource[] array, ParallelOptions parallelOptions, Action<TSource> body, Action<TSource, ParallelLoopState> bodyWithState, Action<TSource, ParallelLoopState, long> bodyWithStateAndIndex, Func<TSource, ParallelLoopState, TLocal, TLocal> bodyWithStateAndLocal, Func<TSource, ParallelLoopState, long, TLocal, TLocal> bodyWithEverything, Func<TLocal> localInit, Action<TLocal> localFinally)
		{
			int lowerBound = array.GetLowerBound(0);
			int num = array.GetUpperBound(0) + 1;
			if (body != null)
			{
				return Parallel.ForWorker<object>(lowerBound, num, parallelOptions, delegate(int i)
				{
					body(array[i]);
				}, null, null, null, null);
			}
			if (bodyWithState != null)
			{
				return Parallel.ForWorker<object>(lowerBound, num, parallelOptions, null, delegate(int i, ParallelLoopState state)
				{
					bodyWithState(array[i], state);
				}, null, null, null);
			}
			if (bodyWithStateAndIndex != null)
			{
				return Parallel.ForWorker<object>(lowerBound, num, parallelOptions, null, delegate(int i, ParallelLoopState state)
				{
					bodyWithStateAndIndex(array[i], state, (long)i);
				}, null, null, null);
			}
			if (bodyWithStateAndLocal != null)
			{
				return Parallel.ForWorker<TLocal>(lowerBound, num, parallelOptions, null, null, (int i, ParallelLoopState state, TLocal local) => bodyWithStateAndLocal(array[i], state, local), localInit, localFinally);
			}
			return Parallel.ForWorker<TLocal>(lowerBound, num, parallelOptions, null, null, (int i, ParallelLoopState state, TLocal local) => bodyWithEverything(array[i], state, (long)i, local), localInit, localFinally);
		}

		// Token: 0x060021BC RID: 8636 RVA: 0x0007A4D8 File Offset: 0x000786D8
		private static ParallelLoopResult ForEachWorker<TSource, TLocal>(IList<TSource> list, ParallelOptions parallelOptions, Action<TSource> body, Action<TSource, ParallelLoopState> bodyWithState, Action<TSource, ParallelLoopState, long> bodyWithStateAndIndex, Func<TSource, ParallelLoopState, TLocal, TLocal> bodyWithStateAndLocal, Func<TSource, ParallelLoopState, long, TLocal, TLocal> bodyWithEverything, Func<TLocal> localInit, Action<TLocal> localFinally)
		{
			if (body != null)
			{
				return Parallel.ForWorker<object>(0, list.Count, parallelOptions, delegate(int i)
				{
					body(list[i]);
				}, null, null, null, null);
			}
			if (bodyWithState != null)
			{
				return Parallel.ForWorker<object>(0, list.Count, parallelOptions, null, delegate(int i, ParallelLoopState state)
				{
					bodyWithState(list[i], state);
				}, null, null, null);
			}
			if (bodyWithStateAndIndex != null)
			{
				return Parallel.ForWorker<object>(0, list.Count, parallelOptions, null, delegate(int i, ParallelLoopState state)
				{
					bodyWithStateAndIndex(list[i], state, (long)i);
				}, null, null, null);
			}
			if (bodyWithStateAndLocal != null)
			{
				return Parallel.ForWorker<TLocal>(0, list.Count, parallelOptions, null, null, (int i, ParallelLoopState state, TLocal local) => bodyWithStateAndLocal(list[i], state, local), localInit, localFinally);
			}
			return Parallel.ForWorker<TLocal>(0, list.Count, parallelOptions, null, null, (int i, ParallelLoopState state, TLocal local) => bodyWithEverything(list[i], state, (long)i, local), localInit, localFinally);
		}

		// Token: 0x060021BD RID: 8637 RVA: 0x0007A5EC File Offset: 0x000787EC
		public static ParallelLoopResult ForEach<TSource>(Partitioner<TSource> source, Action<TSource> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			return Parallel.PartitionerForEachWorker<TSource, object>(source, Parallel.s_defaultParallelOptions, body, null, null, null, null, null, null);
		}

		// Token: 0x060021BE RID: 8638 RVA: 0x0007A628 File Offset: 0x00078828
		public static ParallelLoopResult ForEach<TSource>(Partitioner<TSource> source, Action<TSource, ParallelLoopState> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			return Parallel.PartitionerForEachWorker<TSource, object>(source, Parallel.s_defaultParallelOptions, null, body, null, null, null, null, null);
		}

		// Token: 0x060021BF RID: 8639 RVA: 0x0007A664 File Offset: 0x00078864
		public static ParallelLoopResult ForEach<TSource>(OrderablePartitioner<TSource> source, Action<TSource, ParallelLoopState, long> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (!source.KeysNormalized)
			{
				throw new InvalidOperationException("This method requires the use of an OrderedPartitioner with the KeysNormalized property set to true.");
			}
			return Parallel.PartitionerForEachWorker<TSource, object>(source, Parallel.s_defaultParallelOptions, null, null, body, null, null, null, null);
		}

		// Token: 0x060021C0 RID: 8640 RVA: 0x0007A6B4 File Offset: 0x000788B4
		public static ParallelLoopResult ForEach<TSource, TLocal>(Partitioner<TSource> source, Func<TLocal> localInit, Func<TSource, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			return Parallel.PartitionerForEachWorker<TSource, TLocal>(source, Parallel.s_defaultParallelOptions, null, null, null, body, null, localInit, localFinally);
		}

		// Token: 0x060021C1 RID: 8641 RVA: 0x0007A70C File Offset: 0x0007890C
		public static ParallelLoopResult ForEach<TSource, TLocal>(OrderablePartitioner<TSource> source, Func<TLocal> localInit, Func<TSource, ParallelLoopState, long, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			if (!source.KeysNormalized)
			{
				throw new InvalidOperationException("This method requires the use of an OrderedPartitioner with the KeysNormalized property set to true.");
			}
			return Parallel.PartitionerForEachWorker<TSource, TLocal>(source, Parallel.s_defaultParallelOptions, null, null, null, null, body, localInit, localFinally);
		}

		// Token: 0x060021C2 RID: 8642 RVA: 0x0007A778 File Offset: 0x00078978
		public static ParallelLoopResult ForEach<TSource>(Partitioner<TSource> source, ParallelOptions parallelOptions, Action<TSource> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.PartitionerForEachWorker<TSource, object>(source, parallelOptions, body, null, null, null, null, null, null);
		}

		// Token: 0x060021C3 RID: 8643 RVA: 0x0007A7C0 File Offset: 0x000789C0
		public static ParallelLoopResult ForEach<TSource>(Partitioner<TSource> source, ParallelOptions parallelOptions, Action<TSource, ParallelLoopState> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.PartitionerForEachWorker<TSource, object>(source, parallelOptions, null, body, null, null, null, null, null);
		}

		// Token: 0x060021C4 RID: 8644 RVA: 0x0007A808 File Offset: 0x00078A08
		public static ParallelLoopResult ForEach<TSource>(OrderablePartitioner<TSource> source, ParallelOptions parallelOptions, Action<TSource, ParallelLoopState, long> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			if (!source.KeysNormalized)
			{
				throw new InvalidOperationException("This method requires the use of an OrderedPartitioner with the KeysNormalized property set to true.");
			}
			return Parallel.PartitionerForEachWorker<TSource, object>(source, parallelOptions, null, null, body, null, null, null, null);
		}

		// Token: 0x060021C5 RID: 8645 RVA: 0x0007A860 File Offset: 0x00078A60
		public static ParallelLoopResult ForEach<TSource, TLocal>(Partitioner<TSource> source, ParallelOptions parallelOptions, Func<TLocal> localInit, Func<TSource, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.PartitionerForEachWorker<TSource, TLocal>(source, parallelOptions, null, null, null, body, null, localInit, localFinally);
		}

		// Token: 0x060021C6 RID: 8646 RVA: 0x0007A8C4 File Offset: 0x00078AC4
		public static ParallelLoopResult ForEach<TSource, TLocal>(OrderablePartitioner<TSource> source, ParallelOptions parallelOptions, Func<TLocal> localInit, Func<TSource, ParallelLoopState, long, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			if (!source.KeysNormalized)
			{
				throw new InvalidOperationException("This method requires the use of an OrderedPartitioner with the KeysNormalized property set to true.");
			}
			return Parallel.PartitionerForEachWorker<TSource, TLocal>(source, parallelOptions, null, null, null, null, body, localInit, localFinally);
		}

		// Token: 0x060021C7 RID: 8647 RVA: 0x0007A93C File Offset: 0x00078B3C
		private static ParallelLoopResult PartitionerForEachWorker<TSource, TLocal>(Partitioner<TSource> source, ParallelOptions parallelOptions, Action<TSource> simpleBody, Action<TSource, ParallelLoopState> bodyWithState, Action<TSource, ParallelLoopState, long> bodyWithStateAndIndex, Func<TSource, ParallelLoopState, TLocal, TLocal> bodyWithStateAndLocal, Func<TSource, ParallelLoopState, long, TLocal, TLocal> bodyWithEverything, Func<TLocal> localInit, Action<TLocal> localFinally)
		{
			OrderablePartitioner<TSource> orderedSource = source as OrderablePartitioner<TSource>;
			if (!source.SupportsDynamicPartitions)
			{
				throw new InvalidOperationException("The Partitioner used here must support dynamic partitioning.");
			}
			parallelOptions.CancellationToken.ThrowIfCancellationRequested();
			int forkJoinContextID = 0;
			if (ParallelEtwProvider.Log.IsEnabled())
			{
				forkJoinContextID = Interlocked.Increment(ref Parallel.s_forkJoinContextID);
				ParallelEtwProvider.Log.ParallelLoopBegin(TaskScheduler.Current.Id, Task.CurrentId.GetValueOrDefault(), forkJoinContextID, ParallelEtwProvider.ForkJoinOperationType.ParallelForEach, 0L, 0L);
			}
			ParallelLoopStateFlags64 sharedPStateFlags = new ParallelLoopStateFlags64();
			ParallelLoopResult parallelLoopResult = default(ParallelLoopResult);
			OperationCanceledException oce = null;
			CancellationTokenRegistration cancellationTokenRegistration = ((!parallelOptions.CancellationToken.CanBeCanceled) ? default(CancellationTokenRegistration) : parallelOptions.CancellationToken.Register(delegate(object o)
			{
				oce = new OperationCanceledException(parallelOptions.CancellationToken);
				sharedPStateFlags.Cancel();
			}, null, false));
			IEnumerable<TSource> partitionerSource = null;
			IEnumerable<KeyValuePair<long, TSource>> orderablePartitionerSource = null;
			if (orderedSource != null)
			{
				orderablePartitionerSource = orderedSource.GetOrderableDynamicPartitions();
				if (orderablePartitionerSource == null)
				{
					throw new InvalidOperationException("The Partitioner used here returned a null partitioner source.");
				}
			}
			else
			{
				partitionerSource = source.GetDynamicPartitions();
				if (partitionerSource == null)
				{
					throw new InvalidOperationException("The Partitioner used here returned a null partitioner source.");
				}
			}
			try
			{
				try
				{
					TaskReplicator.Run<IEnumerator>(delegate(ref IEnumerator partitionState, int timeout, out bool replicationDelegateYieldedBeforeCompletion)
					{
						replicationDelegateYieldedBeforeCompletion = false;
						if (ParallelEtwProvider.Log.IsEnabled())
						{
							ParallelEtwProvider.Log.ParallelFork(TaskScheduler.Current.Id, Task.CurrentId.GetValueOrDefault(), forkJoinContextID);
						}
						TLocal tlocal = default(TLocal);
						bool flag = false;
						try
						{
							ParallelLoopState64 parallelLoopState = null;
							if (bodyWithState != null || bodyWithStateAndIndex != null)
							{
								parallelLoopState = new ParallelLoopState64(sharedPStateFlags);
							}
							else if (bodyWithStateAndLocal != null || bodyWithEverything != null)
							{
								parallelLoopState = new ParallelLoopState64(sharedPStateFlags);
								if (localInit != null)
								{
									tlocal = localInit();
									flag = true;
								}
							}
							int num = Parallel.ComputeTimeoutPoint(timeout);
							if (orderedSource != null)
							{
								IEnumerator<KeyValuePair<long, TSource>> enumerator = partitionState as IEnumerator<KeyValuePair<long, TSource>>;
								if (enumerator == null)
								{
									enumerator = orderablePartitionerSource.GetEnumerator();
									partitionState = enumerator;
								}
								if (enumerator == null)
								{
									throw new InvalidOperationException("The Partitioner source returned a null enumerator.");
								}
								while (enumerator.MoveNext())
								{
									KeyValuePair<long, TSource> keyValuePair = enumerator.Current;
									long key = keyValuePair.Key;
									TSource value = keyValuePair.Value;
									if (parallelLoopState != null)
									{
										parallelLoopState.CurrentIteration = key;
									}
									if (simpleBody != null)
									{
										simpleBody(value);
									}
									else if (bodyWithState != null)
									{
										bodyWithState(value, parallelLoopState);
									}
									else if (bodyWithStateAndIndex != null)
									{
										bodyWithStateAndIndex(value, parallelLoopState, key);
									}
									else if (bodyWithStateAndLocal != null)
									{
										tlocal = bodyWithStateAndLocal(value, parallelLoopState, tlocal);
									}
									else
									{
										tlocal = bodyWithEverything(value, parallelLoopState, key, tlocal);
									}
									if (sharedPStateFlags.ShouldExitLoop(key))
									{
										break;
									}
									if (Parallel.CheckTimeoutReached(num))
									{
										replicationDelegateYieldedBeforeCompletion = true;
										break;
									}
								}
							}
							else
							{
								IEnumerator<TSource> enumerator2 = partitionState as IEnumerator<TSource>;
								if (enumerator2 == null)
								{
									enumerator2 = partitionerSource.GetEnumerator();
									partitionState = enumerator2;
								}
								if (enumerator2 == null)
								{
									throw new InvalidOperationException("The Partitioner source returned a null enumerator.");
								}
								if (parallelLoopState != null)
								{
									parallelLoopState.CurrentIteration = 0L;
								}
								while (enumerator2.MoveNext())
								{
									TSource tsource = enumerator2.Current;
									if (simpleBody != null)
									{
										simpleBody(tsource);
									}
									else if (bodyWithState != null)
									{
										bodyWithState(tsource, parallelLoopState);
									}
									else if (bodyWithStateAndLocal != null)
									{
										tlocal = bodyWithStateAndLocal(tsource, parallelLoopState, tlocal);
									}
									if (sharedPStateFlags.LoopStateFlags != 0)
									{
										break;
									}
									if (Parallel.CheckTimeoutReached(num))
									{
										replicationDelegateYieldedBeforeCompletion = true;
										break;
									}
								}
							}
						}
						catch (Exception ex2)
						{
							sharedPStateFlags.SetExceptional();
							ExceptionDispatchInfo.Throw(ex2);
						}
						finally
						{
							if (localFinally != null && flag)
							{
								localFinally(tlocal);
							}
							if (!replicationDelegateYieldedBeforeCompletion)
							{
								IDisposable disposable2 = partitionState as IDisposable;
								if (disposable2 != null)
								{
									disposable2.Dispose();
								}
							}
							if (ParallelEtwProvider.Log.IsEnabled())
							{
								ParallelEtwProvider.Log.ParallelJoin(TaskScheduler.Current.Id, Task.CurrentId.GetValueOrDefault(), forkJoinContextID);
							}
						}
					}, parallelOptions, true);
				}
				finally
				{
					if (parallelOptions.CancellationToken.CanBeCanceled)
					{
						cancellationTokenRegistration.Dispose();
					}
				}
				if (oce != null)
				{
					throw oce;
				}
			}
			catch (AggregateException ex)
			{
				Parallel.ThrowSingleCancellationExceptionOrOtherException(ex.InnerExceptions, parallelOptions.CancellationToken, ex);
			}
			finally
			{
				int loopStateFlags = sharedPStateFlags.LoopStateFlags;
				parallelLoopResult._completed = loopStateFlags == 0;
				if ((loopStateFlags & 2) != 0)
				{
					parallelLoopResult._lowestBreakIteration = new long?(sharedPStateFlags.LowestBreakIteration);
				}
				IDisposable disposable;
				if (orderablePartitionerSource != null)
				{
					disposable = orderablePartitionerSource as IDisposable;
				}
				else
				{
					disposable = partitionerSource as IDisposable;
				}
				if (disposable != null)
				{
					disposable.Dispose();
				}
				if (ParallelEtwProvider.Log.IsEnabled())
				{
					ParallelEtwProvider.Log.ParallelLoopEnd(TaskScheduler.Current.Id, Task.CurrentId.GetValueOrDefault(), forkJoinContextID, 0L);
				}
			}
			return parallelLoopResult;
		}

		// Token: 0x060021C8 RID: 8648 RVA: 0x0007ABF8 File Offset: 0x00078DF8
		private static OperationCanceledException ReduceToSingleCancellationException(ICollection exceptions, CancellationToken cancelToken)
		{
			if (exceptions == null || exceptions.Count == 0)
			{
				return null;
			}
			if (!cancelToken.IsCancellationRequested)
			{
				return null;
			}
			Exception ex = null;
			foreach (object obj in exceptions)
			{
				Exception ex2 = (Exception)obj;
				if (ex == null)
				{
					ex = ex2;
				}
				OperationCanceledException ex3 = ex2 as OperationCanceledException;
				if (ex3 == null || !cancelToken.Equals(ex3.CancellationToken))
				{
					return null;
				}
			}
			return (OperationCanceledException)ex;
		}

		// Token: 0x060021C9 RID: 8649 RVA: 0x0007AC90 File Offset: 0x00078E90
		private static void ThrowSingleCancellationExceptionOrOtherException(ICollection exceptions, CancellationToken cancelToken, Exception otherException)
		{
			ExceptionDispatchInfo.Throw(Parallel.ReduceToSingleCancellationException(exceptions, cancelToken) ?? otherException);
		}

		// Token: 0x060021CA RID: 8650 RVA: 0x0007ACA3 File Offset: 0x00078EA3
		// Note: this type is marked as 'beforefieldinit'.
		static Parallel()
		{
		}

		// Token: 0x04001AB7 RID: 6839
		internal static int s_forkJoinContextID;

		// Token: 0x04001AB8 RID: 6840
		internal const int DEFAULT_LOOP_STRIDE = 16;

		// Token: 0x04001AB9 RID: 6841
		internal static readonly ParallelOptions s_defaultParallelOptions = new ParallelOptions();

		// Token: 0x020002EE RID: 750
		[CompilerGenerated]
		private sealed class <>c__DisplayClass4_0
		{
			// Token: 0x060021CB RID: 8651 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c__DisplayClass4_0()
			{
			}

			// Token: 0x060021CC RID: 8652 RVA: 0x0007ACB0 File Offset: 0x00078EB0
			internal void <Invoke>b__0(ref object state, int timeout, out bool replicationDelegateYieldedBeforeCompletion)
			{
				replicationDelegateYieldedBeforeCompletion = false;
				for (int i = Interlocked.Increment(ref this.actionIndex); i <= this.actionsCopy.Length; i = Interlocked.Increment(ref this.actionIndex))
				{
					try
					{
						this.actionsCopy[i - 1]();
					}
					catch (Exception ex)
					{
						LazyInitializer.EnsureInitialized<ConcurrentQueue<Exception>>(ref this.exceptionQ, () => new ConcurrentQueue<Exception>());
						this.exceptionQ.Enqueue(ex);
					}
					this.parallelOptions.CancellationToken.ThrowIfCancellationRequested();
				}
			}

			// Token: 0x04001ABA RID: 6842
			public Action[] actionsCopy;

			// Token: 0x04001ABB RID: 6843
			public ParallelOptions parallelOptions;

			// Token: 0x04001ABC RID: 6844
			public int actionIndex;

			// Token: 0x04001ABD RID: 6845
			public ConcurrentQueue<Exception> exceptionQ;
		}

		// Token: 0x020002EF RID: 751
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060021CD RID: 8653 RVA: 0x0007AD54 File Offset: 0x00078F54
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060021CE RID: 8654 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c()
			{
			}

			// Token: 0x060021CF RID: 8655 RVA: 0x0007AD60 File Offset: 0x00078F60
			internal ConcurrentQueue<Exception> <Invoke>b__4_1()
			{
				return new ConcurrentQueue<Exception>();
			}

			// Token: 0x060021D0 RID: 8656 RVA: 0x0007AD60 File Offset: 0x00078F60
			internal ConcurrentQueue<Exception> <Invoke>b__4_2()
			{
				return new ConcurrentQueue<Exception>();
			}

			// Token: 0x04001ABE RID: 6846
			public static readonly Parallel.<>c <>9 = new Parallel.<>c();

			// Token: 0x04001ABF RID: 6847
			public static Func<ConcurrentQueue<Exception>> <>9__4_1;

			// Token: 0x04001AC0 RID: 6848
			public static Func<ConcurrentQueue<Exception>> <>9__4_2;
		}

		// Token: 0x020002F0 RID: 752
		[CompilerGenerated]
		private sealed class <>c__DisplayClass19_0<TLocal>
		{
			// Token: 0x060021D1 RID: 8657 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c__DisplayClass19_0()
			{
			}

			// Token: 0x060021D2 RID: 8658 RVA: 0x0007AD67 File Offset: 0x00078F67
			internal void <ForWorker>b__0(object o)
			{
				this.oce = new OperationCanceledException(this.parallelOptions.CancellationToken);
				this.sharedPStateFlags.Cancel();
			}

			// Token: 0x060021D3 RID: 8659 RVA: 0x0007AD8C File Offset: 0x00078F8C
			internal void <ForWorker>b__1(ref RangeWorker currentWorker, int timeout, out bool replicationDelegateYieldedBeforeCompletion)
			{
				if (!currentWorker.IsInitialized)
				{
					currentWorker = this.rangeManager.RegisterNewWorker();
				}
				replicationDelegateYieldedBeforeCompletion = false;
				int num;
				int num2;
				if (!currentWorker.FindNewWork32(out num, out num2) || this.sharedPStateFlags.ShouldExitLoop(num))
				{
					return;
				}
				if (ParallelEtwProvider.Log.IsEnabled())
				{
					ParallelEtwProvider.Log.ParallelFork(TaskScheduler.Current.Id, Task.CurrentId.GetValueOrDefault(), this.forkJoinContextID);
				}
				TLocal tlocal = default(TLocal);
				bool flag = false;
				try
				{
					ParallelLoopState32 parallelLoopState = null;
					if (this.bodyWithState != null)
					{
						parallelLoopState = new ParallelLoopState32(this.sharedPStateFlags);
					}
					else if (this.bodyWithLocal != null)
					{
						parallelLoopState = new ParallelLoopState32(this.sharedPStateFlags);
						if (this.localInit != null)
						{
							tlocal = this.localInit();
							flag = true;
						}
					}
					int num3 = Parallel.ComputeTimeoutPoint(timeout);
					for (;;)
					{
						if (this.body != null)
						{
							for (int i = num; i < num2; i++)
							{
								if (this.sharedPStateFlags.LoopStateFlags != 0 && this.sharedPStateFlags.ShouldExitLoop())
								{
									break;
								}
								this.body(i);
							}
						}
						else if (this.bodyWithState != null)
						{
							for (int j = num; j < num2; j++)
							{
								if (this.sharedPStateFlags.LoopStateFlags != 0 && this.sharedPStateFlags.ShouldExitLoop(j))
								{
									break;
								}
								parallelLoopState.CurrentIteration = j;
								this.bodyWithState(j, parallelLoopState);
							}
						}
						else
						{
							int num4 = num;
							while (num4 < num2 && (this.sharedPStateFlags.LoopStateFlags == 0 || !this.sharedPStateFlags.ShouldExitLoop(num4)))
							{
								parallelLoopState.CurrentIteration = num4;
								tlocal = this.bodyWithLocal(num4, parallelLoopState, tlocal);
								num4++;
							}
						}
						if (Parallel.CheckTimeoutReached(num3))
						{
							break;
						}
						if (!currentWorker.FindNewWork32(out num, out num2) || (this.sharedPStateFlags.LoopStateFlags != 0 && this.sharedPStateFlags.ShouldExitLoop(num)))
						{
							goto IL_01D8;
						}
					}
					replicationDelegateYieldedBeforeCompletion = true;
					IL_01D8:;
				}
				catch (Exception ex)
				{
					this.sharedPStateFlags.SetExceptional();
					ExceptionDispatchInfo.Throw(ex);
				}
				finally
				{
					if (this.localFinally != null && flag)
					{
						this.localFinally(tlocal);
					}
					if (ParallelEtwProvider.Log.IsEnabled())
					{
						ParallelEtwProvider.Log.ParallelJoin(TaskScheduler.Current.Id, Task.CurrentId.GetValueOrDefault(), this.forkJoinContextID);
					}
				}
			}

			// Token: 0x04001AC1 RID: 6849
			public OperationCanceledException oce;

			// Token: 0x04001AC2 RID: 6850
			public ParallelOptions parallelOptions;

			// Token: 0x04001AC3 RID: 6851
			public ParallelLoopStateFlags32 sharedPStateFlags;

			// Token: 0x04001AC4 RID: 6852
			public RangeManager rangeManager;

			// Token: 0x04001AC5 RID: 6853
			public int forkJoinContextID;

			// Token: 0x04001AC6 RID: 6854
			public Action<int, ParallelLoopState> bodyWithState;

			// Token: 0x04001AC7 RID: 6855
			public Func<int, ParallelLoopState, TLocal, TLocal> bodyWithLocal;

			// Token: 0x04001AC8 RID: 6856
			public Func<TLocal> localInit;

			// Token: 0x04001AC9 RID: 6857
			public Action<int> body;

			// Token: 0x04001ACA RID: 6858
			public Action<TLocal> localFinally;
		}

		// Token: 0x020002F1 RID: 753
		[CompilerGenerated]
		private sealed class <>c__DisplayClass20_0<TLocal>
		{
			// Token: 0x060021D4 RID: 8660 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c__DisplayClass20_0()
			{
			}

			// Token: 0x060021D5 RID: 8661 RVA: 0x0007B008 File Offset: 0x00079208
			internal void <ForWorker64>b__0(object o)
			{
				this.oce = new OperationCanceledException(this.parallelOptions.CancellationToken);
				this.sharedPStateFlags.Cancel();
			}

			// Token: 0x060021D6 RID: 8662 RVA: 0x0007B02C File Offset: 0x0007922C
			internal void <ForWorker64>b__1(ref RangeWorker currentWorker, int timeout, out bool replicationDelegateYieldedBeforeCompletion)
			{
				if (!currentWorker.IsInitialized)
				{
					currentWorker = this.rangeManager.RegisterNewWorker();
				}
				replicationDelegateYieldedBeforeCompletion = false;
				long num;
				long num2;
				if (!currentWorker.FindNewWork(out num, out num2) || this.sharedPStateFlags.ShouldExitLoop(num))
				{
					return;
				}
				if (ParallelEtwProvider.Log.IsEnabled())
				{
					ParallelEtwProvider.Log.ParallelFork(TaskScheduler.Current.Id, Task.CurrentId.GetValueOrDefault(), this.forkJoinContextID);
				}
				TLocal tlocal = default(TLocal);
				bool flag = false;
				try
				{
					ParallelLoopState64 parallelLoopState = null;
					if (this.bodyWithState != null)
					{
						parallelLoopState = new ParallelLoopState64(this.sharedPStateFlags);
					}
					else if (this.bodyWithLocal != null)
					{
						parallelLoopState = new ParallelLoopState64(this.sharedPStateFlags);
						if (this.localInit != null)
						{
							tlocal = this.localInit();
							flag = true;
						}
					}
					int num3 = Parallel.ComputeTimeoutPoint(timeout);
					for (;;)
					{
						if (this.body != null)
						{
							for (long num4 = num; num4 < num2; num4 += 1L)
							{
								if (this.sharedPStateFlags.LoopStateFlags != 0 && this.sharedPStateFlags.ShouldExitLoop())
								{
									break;
								}
								this.body(num4);
							}
						}
						else if (this.bodyWithState != null)
						{
							for (long num5 = num; num5 < num2; num5 += 1L)
							{
								if (this.sharedPStateFlags.LoopStateFlags != 0 && this.sharedPStateFlags.ShouldExitLoop(num5))
								{
									break;
								}
								parallelLoopState.CurrentIteration = num5;
								this.bodyWithState(num5, parallelLoopState);
							}
						}
						else
						{
							long num6 = num;
							while (num6 < num2 && (this.sharedPStateFlags.LoopStateFlags == 0 || !this.sharedPStateFlags.ShouldExitLoop(num6)))
							{
								parallelLoopState.CurrentIteration = num6;
								tlocal = this.bodyWithLocal(num6, parallelLoopState, tlocal);
								num6 += 1L;
							}
						}
						if (Parallel.CheckTimeoutReached(num3))
						{
							break;
						}
						if (!currentWorker.FindNewWork(out num, out num2) || (this.sharedPStateFlags.LoopStateFlags != 0 && this.sharedPStateFlags.ShouldExitLoop(num)))
						{
							goto IL_01DB;
						}
					}
					replicationDelegateYieldedBeforeCompletion = true;
					IL_01DB:;
				}
				catch (Exception ex)
				{
					this.sharedPStateFlags.SetExceptional();
					ExceptionDispatchInfo.Throw(ex);
				}
				finally
				{
					if (this.localFinally != null && flag)
					{
						this.localFinally(tlocal);
					}
					if (ParallelEtwProvider.Log.IsEnabled())
					{
						ParallelEtwProvider.Log.ParallelJoin(TaskScheduler.Current.Id, Task.CurrentId.GetValueOrDefault(), this.forkJoinContextID);
					}
				}
			}

			// Token: 0x04001ACB RID: 6859
			public OperationCanceledException oce;

			// Token: 0x04001ACC RID: 6860
			public ParallelOptions parallelOptions;

			// Token: 0x04001ACD RID: 6861
			public ParallelLoopStateFlags64 sharedPStateFlags;

			// Token: 0x04001ACE RID: 6862
			public RangeManager rangeManager;

			// Token: 0x04001ACF RID: 6863
			public int forkJoinContextID;

			// Token: 0x04001AD0 RID: 6864
			public Action<long, ParallelLoopState> bodyWithState;

			// Token: 0x04001AD1 RID: 6865
			public Func<long, ParallelLoopState, TLocal, TLocal> bodyWithLocal;

			// Token: 0x04001AD2 RID: 6866
			public Func<TLocal> localInit;

			// Token: 0x04001AD3 RID: 6867
			public Action<long> body;

			// Token: 0x04001AD4 RID: 6868
			public Action<TLocal> localFinally;
		}

		// Token: 0x020002F2 RID: 754
		[CompilerGenerated]
		private sealed class <>c__DisplayClass32_0<TSource, TLocal>
		{
			// Token: 0x060021D7 RID: 8663 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c__DisplayClass32_0()
			{
			}

			// Token: 0x060021D8 RID: 8664 RVA: 0x0007B2AC File Offset: 0x000794AC
			internal void <ForEachWorker>b__0(int i)
			{
				this.body(this.array[i]);
			}

			// Token: 0x060021D9 RID: 8665 RVA: 0x0007B2C5 File Offset: 0x000794C5
			internal void <ForEachWorker>b__1(int i, ParallelLoopState state)
			{
				this.bodyWithState(this.array[i], state);
			}

			// Token: 0x060021DA RID: 8666 RVA: 0x0007B2DF File Offset: 0x000794DF
			internal void <ForEachWorker>b__2(int i, ParallelLoopState state)
			{
				this.bodyWithStateAndIndex(this.array[i], state, (long)i);
			}

			// Token: 0x060021DB RID: 8667 RVA: 0x0007B2FB File Offset: 0x000794FB
			internal TLocal <ForEachWorker>b__3(int i, ParallelLoopState state, TLocal local)
			{
				return this.bodyWithStateAndLocal(this.array[i], state, local);
			}

			// Token: 0x060021DC RID: 8668 RVA: 0x0007B316 File Offset: 0x00079516
			internal TLocal <ForEachWorker>b__4(int i, ParallelLoopState state, TLocal local)
			{
				return this.bodyWithEverything(this.array[i], state, (long)i, local);
			}

			// Token: 0x04001AD5 RID: 6869
			public Action<TSource> body;

			// Token: 0x04001AD6 RID: 6870
			public TSource[] array;

			// Token: 0x04001AD7 RID: 6871
			public Action<TSource, ParallelLoopState> bodyWithState;

			// Token: 0x04001AD8 RID: 6872
			public Action<TSource, ParallelLoopState, long> bodyWithStateAndIndex;

			// Token: 0x04001AD9 RID: 6873
			public Func<TSource, ParallelLoopState, TLocal, TLocal> bodyWithStateAndLocal;

			// Token: 0x04001ADA RID: 6874
			public Func<TSource, ParallelLoopState, long, TLocal, TLocal> bodyWithEverything;
		}

		// Token: 0x020002F3 RID: 755
		[CompilerGenerated]
		private sealed class <>c__DisplayClass33_0<TSource, TLocal>
		{
			// Token: 0x060021DD RID: 8669 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c__DisplayClass33_0()
			{
			}

			// Token: 0x060021DE RID: 8670 RVA: 0x0007B333 File Offset: 0x00079533
			internal void <ForEachWorker>b__0(int i)
			{
				this.body(this.list[i]);
			}

			// Token: 0x060021DF RID: 8671 RVA: 0x0007B34C File Offset: 0x0007954C
			internal void <ForEachWorker>b__1(int i, ParallelLoopState state)
			{
				this.bodyWithState(this.list[i], state);
			}

			// Token: 0x060021E0 RID: 8672 RVA: 0x0007B366 File Offset: 0x00079566
			internal void <ForEachWorker>b__2(int i, ParallelLoopState state)
			{
				this.bodyWithStateAndIndex(this.list[i], state, (long)i);
			}

			// Token: 0x060021E1 RID: 8673 RVA: 0x0007B382 File Offset: 0x00079582
			internal TLocal <ForEachWorker>b__3(int i, ParallelLoopState state, TLocal local)
			{
				return this.bodyWithStateAndLocal(this.list[i], state, local);
			}

			// Token: 0x060021E2 RID: 8674 RVA: 0x0007B39D File Offset: 0x0007959D
			internal TLocal <ForEachWorker>b__4(int i, ParallelLoopState state, TLocal local)
			{
				return this.bodyWithEverything(this.list[i], state, (long)i, local);
			}

			// Token: 0x04001ADB RID: 6875
			public Action<TSource> body;

			// Token: 0x04001ADC RID: 6876
			public IList<TSource> list;

			// Token: 0x04001ADD RID: 6877
			public Action<TSource, ParallelLoopState> bodyWithState;

			// Token: 0x04001ADE RID: 6878
			public Action<TSource, ParallelLoopState, long> bodyWithStateAndIndex;

			// Token: 0x04001ADF RID: 6879
			public Func<TSource, ParallelLoopState, TLocal, TLocal> bodyWithStateAndLocal;

			// Token: 0x04001AE0 RID: 6880
			public Func<TSource, ParallelLoopState, long, TLocal, TLocal> bodyWithEverything;
		}

		// Token: 0x020002F4 RID: 756
		[CompilerGenerated]
		private sealed class <>c__DisplayClass44_0<TSource, TLocal>
		{
			// Token: 0x060021E3 RID: 8675 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c__DisplayClass44_0()
			{
			}

			// Token: 0x060021E4 RID: 8676 RVA: 0x0007B3BA File Offset: 0x000795BA
			internal void <PartitionerForEachWorker>b__0(object o)
			{
				this.oce = new OperationCanceledException(this.parallelOptions.CancellationToken);
				this.sharedPStateFlags.Cancel();
			}

			// Token: 0x060021E5 RID: 8677 RVA: 0x0007B3E0 File Offset: 0x000795E0
			internal void <PartitionerForEachWorker>b__1(ref IEnumerator partitionState, int timeout, out bool replicationDelegateYieldedBeforeCompletion)
			{
				replicationDelegateYieldedBeforeCompletion = false;
				if (ParallelEtwProvider.Log.IsEnabled())
				{
					ParallelEtwProvider.Log.ParallelFork(TaskScheduler.Current.Id, Task.CurrentId.GetValueOrDefault(), this.forkJoinContextID);
				}
				TLocal tlocal = default(TLocal);
				bool flag = false;
				try
				{
					ParallelLoopState64 parallelLoopState = null;
					if (this.bodyWithState != null || this.bodyWithStateAndIndex != null)
					{
						parallelLoopState = new ParallelLoopState64(this.sharedPStateFlags);
					}
					else if (this.bodyWithStateAndLocal != null || this.bodyWithEverything != null)
					{
						parallelLoopState = new ParallelLoopState64(this.sharedPStateFlags);
						if (this.localInit != null)
						{
							tlocal = this.localInit();
							flag = true;
						}
					}
					int num = Parallel.ComputeTimeoutPoint(timeout);
					if (this.orderedSource != null)
					{
						IEnumerator<KeyValuePair<long, TSource>> enumerator = partitionState as IEnumerator<KeyValuePair<long, TSource>>;
						if (enumerator == null)
						{
							enumerator = this.orderablePartitionerSource.GetEnumerator();
							partitionState = enumerator;
						}
						if (enumerator == null)
						{
							throw new InvalidOperationException("The Partitioner source returned a null enumerator.");
						}
						while (enumerator.MoveNext())
						{
							KeyValuePair<long, TSource> keyValuePair = enumerator.Current;
							long key = keyValuePair.Key;
							TSource value = keyValuePair.Value;
							if (parallelLoopState != null)
							{
								parallelLoopState.CurrentIteration = key;
							}
							if (this.simpleBody != null)
							{
								this.simpleBody(value);
							}
							else if (this.bodyWithState != null)
							{
								this.bodyWithState(value, parallelLoopState);
							}
							else if (this.bodyWithStateAndIndex != null)
							{
								this.bodyWithStateAndIndex(value, parallelLoopState, key);
							}
							else if (this.bodyWithStateAndLocal != null)
							{
								tlocal = this.bodyWithStateAndLocal(value, parallelLoopState, tlocal);
							}
							else
							{
								tlocal = this.bodyWithEverything(value, parallelLoopState, key, tlocal);
							}
							if (this.sharedPStateFlags.ShouldExitLoop(key))
							{
								break;
							}
							if (Parallel.CheckTimeoutReached(num))
							{
								replicationDelegateYieldedBeforeCompletion = true;
								break;
							}
						}
					}
					else
					{
						IEnumerator<TSource> enumerator2 = partitionState as IEnumerator<TSource>;
						if (enumerator2 == null)
						{
							enumerator2 = this.partitionerSource.GetEnumerator();
							partitionState = enumerator2;
						}
						if (enumerator2 == null)
						{
							throw new InvalidOperationException("The Partitioner source returned a null enumerator.");
						}
						if (parallelLoopState != null)
						{
							parallelLoopState.CurrentIteration = 0L;
						}
						while (enumerator2.MoveNext())
						{
							TSource tsource = enumerator2.Current;
							if (this.simpleBody != null)
							{
								this.simpleBody(tsource);
							}
							else if (this.bodyWithState != null)
							{
								this.bodyWithState(tsource, parallelLoopState);
							}
							else if (this.bodyWithStateAndLocal != null)
							{
								tlocal = this.bodyWithStateAndLocal(tsource, parallelLoopState, tlocal);
							}
							if (this.sharedPStateFlags.LoopStateFlags != 0)
							{
								break;
							}
							if (Parallel.CheckTimeoutReached(num))
							{
								replicationDelegateYieldedBeforeCompletion = true;
								break;
							}
						}
					}
				}
				catch (Exception ex)
				{
					this.sharedPStateFlags.SetExceptional();
					ExceptionDispatchInfo.Throw(ex);
				}
				finally
				{
					if (this.localFinally != null && flag)
					{
						this.localFinally(tlocal);
					}
					if (!replicationDelegateYieldedBeforeCompletion)
					{
						IDisposable disposable = partitionState as IDisposable;
						if (disposable != null)
						{
							disposable.Dispose();
						}
					}
					if (ParallelEtwProvider.Log.IsEnabled())
					{
						ParallelEtwProvider.Log.ParallelJoin(TaskScheduler.Current.Id, Task.CurrentId.GetValueOrDefault(), this.forkJoinContextID);
					}
				}
			}

			// Token: 0x04001AE1 RID: 6881
			public OperationCanceledException oce;

			// Token: 0x04001AE2 RID: 6882
			public ParallelOptions parallelOptions;

			// Token: 0x04001AE3 RID: 6883
			public ParallelLoopStateFlags64 sharedPStateFlags;

			// Token: 0x04001AE4 RID: 6884
			public int forkJoinContextID;

			// Token: 0x04001AE5 RID: 6885
			public Action<TSource, ParallelLoopState> bodyWithState;

			// Token: 0x04001AE6 RID: 6886
			public Action<TSource, ParallelLoopState, long> bodyWithStateAndIndex;

			// Token: 0x04001AE7 RID: 6887
			public Func<TSource, ParallelLoopState, TLocal, TLocal> bodyWithStateAndLocal;

			// Token: 0x04001AE8 RID: 6888
			public Func<TSource, ParallelLoopState, long, TLocal, TLocal> bodyWithEverything;

			// Token: 0x04001AE9 RID: 6889
			public Func<TLocal> localInit;

			// Token: 0x04001AEA RID: 6890
			public OrderablePartitioner<TSource> orderedSource;

			// Token: 0x04001AEB RID: 6891
			public IEnumerable<KeyValuePair<long, TSource>> orderablePartitionerSource;

			// Token: 0x04001AEC RID: 6892
			public Action<TSource> simpleBody;

			// Token: 0x04001AED RID: 6893
			public IEnumerable<TSource> partitionerSource;

			// Token: 0x04001AEE RID: 6894
			public Action<TLocal> localFinally;
		}
	}
}
