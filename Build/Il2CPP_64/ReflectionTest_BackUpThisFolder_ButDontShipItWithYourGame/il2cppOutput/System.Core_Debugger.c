#include "il2cpp-config.h"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif



#include "codegen/il2cpp-codegen-metadata.h"





IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END




#if IL2CPP_MONO_DEBUGGER
static const Il2CppMethodExecutionContextInfo g_methodExecutionContextInfos[1] = { { 0, 0, 0 } };
#else
static const Il2CppMethodExecutionContextInfo g_methodExecutionContextInfos[1] = { { 0, 0, 0 } };
#endif
#if IL2CPP_MONO_DEBUGGER
static const char* g_methodExecutionContextInfoStrings[1] = { NULL };
#else
static const char* g_methodExecutionContextInfoStrings[1] = { NULL };
#endif
#if IL2CPP_MONO_DEBUGGER
static const Il2CppMethodExecutionContextInfoIndex g_methodExecutionContextInfoIndexes[91] = 
{
	{ 0, 0 } /* 0x06000001 System.Exception System.Linq.Error::ArgumentNull(System.String) */,
	{ 0, 0 } /* 0x06000002 System.Exception System.Linq.Error::MoreThanOneMatch() */,
	{ 0, 0 } /* 0x06000003 System.Exception System.Linq.Error::NoElements() */,
	{ 0, 0 } /* 0x06000004 System.Collections.Generic.IEnumerable`1<TSource> System.Linq.Enumerable::Where(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,System.Boolean>) */,
	{ 0, 0 } /* 0x06000005 System.Func`2<TSource,System.Boolean> System.Linq.Enumerable::CombinePredicates(System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,System.Boolean>) */,
	{ 0, 0 } /* 0x06000006 System.Linq.IOrderedEnumerable`1<TSource> System.Linq.Enumerable::OrderByDescending(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,TKey>) */,
	{ 0, 0 } /* 0x06000007 TSource[] System.Linq.Enumerable::ToArray(System.Collections.Generic.IEnumerable`1<TSource>) */,
	{ 0, 0 } /* 0x06000008 System.Collections.Generic.Dictionary`2<TKey,TElement> System.Linq.Enumerable::ToDictionary(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,TKey>,System.Func`2<TSource,TElement>) */,
	{ 0, 0 } /* 0x06000009 System.Collections.Generic.Dictionary`2<TKey,TElement> System.Linq.Enumerable::ToDictionary(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,TKey>,System.Func`2<TSource,TElement>,System.Collections.Generic.IEqualityComparer`1<TKey>) */,
	{ 0, 0 } /* 0x0600000A TSource System.Linq.Enumerable::First(System.Collections.Generic.IEnumerable`1<TSource>) */,
	{ 0, 0 } /* 0x0600000B TSource System.Linq.Enumerable::SingleOrDefault(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,System.Boolean>) */,
	{ 0, 0 } /* 0x0600000C System.Boolean System.Linq.Enumerable::Any(System.Collections.Generic.IEnumerable`1<TSource>) */,
	{ 0, 0 } /* 0x0600000D System.Boolean System.Linq.Enumerable::Any(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,System.Boolean>) */,
	{ 0, 0 } /* 0x0600000E System.Void System.Linq.Enumerable/Iterator`1::.ctor() */,
	{ 0, 0 } /* 0x0600000F TSource System.Linq.Enumerable/Iterator`1::get_Current() */,
	{ 0, 0 } /* 0x06000010 System.Linq.Enumerable/Iterator`1<TSource> System.Linq.Enumerable/Iterator`1::Clone() */,
	{ 0, 0 } /* 0x06000011 System.Void System.Linq.Enumerable/Iterator`1::Dispose() */,
	{ 0, 0 } /* 0x06000012 System.Collections.Generic.IEnumerator`1<TSource> System.Linq.Enumerable/Iterator`1::GetEnumerator() */,
	{ 0, 0 } /* 0x06000013 System.Boolean System.Linq.Enumerable/Iterator`1::MoveNext() */,
	{ 0, 0 } /* 0x06000014 System.Collections.Generic.IEnumerable`1<TSource> System.Linq.Enumerable/Iterator`1::Where(System.Func`2<TSource,System.Boolean>) */,
	{ 0, 0 } /* 0x06000015 System.Object System.Linq.Enumerable/Iterator`1::System.Collections.IEnumerator.get_Current() */,
	{ 0, 0 } /* 0x06000016 System.Collections.IEnumerator System.Linq.Enumerable/Iterator`1::System.Collections.IEnumerable.GetEnumerator() */,
	{ 0, 0 } /* 0x06000017 System.Void System.Linq.Enumerable/Iterator`1::System.Collections.IEnumerator.Reset() */,
	{ 0, 0 } /* 0x06000018 System.Void System.Linq.Enumerable/WhereEnumerableIterator`1::.ctor(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,System.Boolean>) */,
	{ 0, 0 } /* 0x06000019 System.Linq.Enumerable/Iterator`1<TSource> System.Linq.Enumerable/WhereEnumerableIterator`1::Clone() */,
	{ 0, 0 } /* 0x0600001A System.Void System.Linq.Enumerable/WhereEnumerableIterator`1::Dispose() */,
	{ 0, 0 } /* 0x0600001B System.Boolean System.Linq.Enumerable/WhereEnumerableIterator`1::MoveNext() */,
	{ 0, 0 } /* 0x0600001C System.Collections.Generic.IEnumerable`1<TSource> System.Linq.Enumerable/WhereEnumerableIterator`1::Where(System.Func`2<TSource,System.Boolean>) */,
	{ 0, 0 } /* 0x0600001D System.Void System.Linq.Enumerable/WhereArrayIterator`1::.ctor(TSource[],System.Func`2<TSource,System.Boolean>) */,
	{ 0, 0 } /* 0x0600001E System.Linq.Enumerable/Iterator`1<TSource> System.Linq.Enumerable/WhereArrayIterator`1::Clone() */,
	{ 0, 0 } /* 0x0600001F System.Boolean System.Linq.Enumerable/WhereArrayIterator`1::MoveNext() */,
	{ 0, 0 } /* 0x06000020 System.Collections.Generic.IEnumerable`1<TSource> System.Linq.Enumerable/WhereArrayIterator`1::Where(System.Func`2<TSource,System.Boolean>) */,
	{ 0, 0 } /* 0x06000021 System.Void System.Linq.Enumerable/WhereListIterator`1::.ctor(System.Collections.Generic.List`1<TSource>,System.Func`2<TSource,System.Boolean>) */,
	{ 0, 0 } /* 0x06000022 System.Linq.Enumerable/Iterator`1<TSource> System.Linq.Enumerable/WhereListIterator`1::Clone() */,
	{ 0, 0 } /* 0x06000023 System.Boolean System.Linq.Enumerable/WhereListIterator`1::MoveNext() */,
	{ 0, 0 } /* 0x06000024 System.Collections.Generic.IEnumerable`1<TSource> System.Linq.Enumerable/WhereListIterator`1::Where(System.Func`2<TSource,System.Boolean>) */,
	{ 0, 0 } /* 0x06000025 System.Void System.Linq.Enumerable/<>c__DisplayClass6_0`1::.ctor() */,
	{ 0, 0 } /* 0x06000026 System.Boolean System.Linq.Enumerable/<>c__DisplayClass6_0`1::<CombinePredicates>b__0(TSource) */,
	{ 0, 0 } /* 0x06000027 System.Collections.Generic.IEnumerator`1<TElement> System.Linq.OrderedEnumerable`1::GetEnumerator() */,
	{ 0, 0 } /* 0x06000028 System.Linq.EnumerableSorter`1<TElement> System.Linq.OrderedEnumerable`1::GetEnumerableSorter(System.Linq.EnumerableSorter`1<TElement>) */,
	{ 0, 0 } /* 0x06000029 System.Collections.IEnumerator System.Linq.OrderedEnumerable`1::System.Collections.IEnumerable.GetEnumerator() */,
	{ 0, 0 } /* 0x0600002A System.Void System.Linq.OrderedEnumerable`1::.ctor() */,
	{ 0, 0 } /* 0x0600002B System.Void System.Linq.OrderedEnumerable`1/<GetEnumerator>d__1::.ctor(System.Int32) */,
	{ 0, 0 } /* 0x0600002C System.Void System.Linq.OrderedEnumerable`1/<GetEnumerator>d__1::System.IDisposable.Dispose() */,
	{ 0, 0 } /* 0x0600002D System.Boolean System.Linq.OrderedEnumerable`1/<GetEnumerator>d__1::MoveNext() */,
	{ 0, 0 } /* 0x0600002E TElement System.Linq.OrderedEnumerable`1/<GetEnumerator>d__1::System.Collections.Generic.IEnumerator<TElement>.get_Current() */,
	{ 0, 0 } /* 0x0600002F System.Void System.Linq.OrderedEnumerable`1/<GetEnumerator>d__1::System.Collections.IEnumerator.Reset() */,
	{ 0, 0 } /* 0x06000030 System.Object System.Linq.OrderedEnumerable`1/<GetEnumerator>d__1::System.Collections.IEnumerator.get_Current() */,
	{ 0, 0 } /* 0x06000031 System.Void System.Linq.OrderedEnumerable`2::.ctor(System.Collections.Generic.IEnumerable`1<TElement>,System.Func`2<TElement,TKey>,System.Collections.Generic.IComparer`1<TKey>,System.Boolean) */,
	{ 0, 0 } /* 0x06000032 System.Linq.EnumerableSorter`1<TElement> System.Linq.OrderedEnumerable`2::GetEnumerableSorter(System.Linq.EnumerableSorter`1<TElement>) */,
	{ 0, 0 } /* 0x06000033 System.Void System.Linq.EnumerableSorter`1::ComputeKeys(TElement[],System.Int32) */,
	{ 0, 0 } /* 0x06000034 System.Int32 System.Linq.EnumerableSorter`1::CompareKeys(System.Int32,System.Int32) */,
	{ 0, 0 } /* 0x06000035 System.Int32[] System.Linq.EnumerableSorter`1::Sort(TElement[],System.Int32) */,
	{ 0, 0 } /* 0x06000036 System.Void System.Linq.EnumerableSorter`1::QuickSort(System.Int32[],System.Int32,System.Int32) */,
	{ 0, 0 } /* 0x06000037 System.Void System.Linq.EnumerableSorter`1::.ctor() */,
	{ 0, 0 } /* 0x06000038 System.Void System.Linq.EnumerableSorter`2::.ctor(System.Func`2<TElement,TKey>,System.Collections.Generic.IComparer`1<TKey>,System.Boolean,System.Linq.EnumerableSorter`1<TElement>) */,
	{ 0, 0 } /* 0x06000039 System.Void System.Linq.EnumerableSorter`2::ComputeKeys(TElement[],System.Int32) */,
	{ 0, 0 } /* 0x0600003A System.Int32 System.Linq.EnumerableSorter`2::CompareKeys(System.Int32,System.Int32) */,
	{ 0, 0 } /* 0x0600003B System.Void System.Linq.Buffer`1::.ctor(System.Collections.Generic.IEnumerable`1<TElement>) */,
	{ 0, 0 } /* 0x0600003C TElement[] System.Linq.Buffer`1::ToArray() */,
	{ 0, 0 } /* 0x0600003D System.Void System.Collections.Generic.HashSet`1::.ctor() */,
	{ 0, 0 } /* 0x0600003E System.Void System.Collections.Generic.HashSet`1::.ctor(System.Collections.Generic.IEqualityComparer`1<T>) */,
	{ 0, 0 } /* 0x0600003F System.Void System.Collections.Generic.HashSet`1::.ctor(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext) */,
	{ 0, 0 } /* 0x06000040 System.Void System.Collections.Generic.HashSet`1::System.Collections.Generic.ICollection<T>.Add(T) */,
	{ 0, 0 } /* 0x06000041 System.Void System.Collections.Generic.HashSet`1::Clear() */,
	{ 0, 0 } /* 0x06000042 System.Boolean System.Collections.Generic.HashSet`1::Contains(T) */,
	{ 0, 0 } /* 0x06000043 System.Void System.Collections.Generic.HashSet`1::CopyTo(T[],System.Int32) */,
	{ 0, 0 } /* 0x06000044 System.Boolean System.Collections.Generic.HashSet`1::Remove(T) */,
	{ 0, 0 } /* 0x06000045 System.Int32 System.Collections.Generic.HashSet`1::get_Count() */,
	{ 0, 0 } /* 0x06000046 System.Boolean System.Collections.Generic.HashSet`1::System.Collections.Generic.ICollection<T>.get_IsReadOnly() */,
	{ 0, 0 } /* 0x06000047 System.Collections.Generic.HashSet`1/Enumerator<T> System.Collections.Generic.HashSet`1::GetEnumerator() */,
	{ 0, 0 } /* 0x06000048 System.Collections.Generic.IEnumerator`1<T> System.Collections.Generic.HashSet`1::System.Collections.Generic.IEnumerable<T>.GetEnumerator() */,
	{ 0, 0 } /* 0x06000049 System.Collections.IEnumerator System.Collections.Generic.HashSet`1::System.Collections.IEnumerable.GetEnumerator() */,
	{ 0, 0 } /* 0x0600004A System.Void System.Collections.Generic.HashSet`1::GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext) */,
	{ 0, 0 } /* 0x0600004B System.Void System.Collections.Generic.HashSet`1::OnDeserialization(System.Object) */,
	{ 0, 0 } /* 0x0600004C System.Boolean System.Collections.Generic.HashSet`1::Add(T) */,
	{ 0, 0 } /* 0x0600004D System.Void System.Collections.Generic.HashSet`1::CopyTo(T[]) */,
	{ 0, 0 } /* 0x0600004E System.Void System.Collections.Generic.HashSet`1::CopyTo(T[],System.Int32,System.Int32) */,
	{ 0, 0 } /* 0x0600004F System.Void System.Collections.Generic.HashSet`1::Initialize(System.Int32) */,
	{ 0, 0 } /* 0x06000050 System.Void System.Collections.Generic.HashSet`1::IncreaseCapacity() */,
	{ 0, 0 } /* 0x06000051 System.Void System.Collections.Generic.HashSet`1::SetCapacity(System.Int32) */,
	{ 0, 0 } /* 0x06000052 System.Boolean System.Collections.Generic.HashSet`1::AddIfNotPresent(T) */,
	{ 0, 0 } /* 0x06000053 System.Int32 System.Collections.Generic.HashSet`1::InternalGetHashCode(T) */,
	{ 0, 0 } /* 0x06000054 System.Void System.Collections.Generic.HashSet`1/Enumerator::.ctor(System.Collections.Generic.HashSet`1<T>) */,
	{ 0, 0 } /* 0x06000055 System.Void System.Collections.Generic.HashSet`1/Enumerator::Dispose() */,
	{ 0, 0 } /* 0x06000056 System.Boolean System.Collections.Generic.HashSet`1/Enumerator::MoveNext() */,
	{ 0, 0 } /* 0x06000057 T System.Collections.Generic.HashSet`1/Enumerator::get_Current() */,
	{ 0, 0 } /* 0x06000058 System.Object System.Collections.Generic.HashSet`1/Enumerator::System.Collections.IEnumerator.get_Current() */,
	{ 0, 0 } /* 0x06000059 System.Void System.Collections.Generic.HashSet`1/Enumerator::System.Collections.IEnumerator.Reset() */,
	{ 0, 0 } /* 0x0600005A System.Void System.Collections.Generic.ICollectionDebugView`1::.ctor(System.Collections.Generic.ICollection`1<T>) */,
	{ 0, 0 } /* 0x0600005B T[] System.Collections.Generic.ICollectionDebugView`1::get_Items() */,
};
#else
static const Il2CppMethodExecutionContextInfoIndex g_methodExecutionContextInfoIndexes[1] = { { 0, 0} };
#endif
#if IL2CPP_MONO_DEBUGGER
extern Il2CppSequencePoint g_sequencePointsSystem_Core[];
Il2CppSequencePoint g_sequencePointsSystem_Core[1] = { { 0, 0, 0, 0, 0, 0, 0, kSequencePointKind_Normal, 0, 0, } };
#else
extern Il2CppSequencePoint g_sequencePointsSystem_Core[];
Il2CppSequencePoint g_sequencePointsSystem_Core[1] = { { 0, 0, 0, 0, 0, 0, 0, kSequencePointKind_Normal, 0, 0, } };
#endif
#if IL2CPP_MONO_DEBUGGER
static const Il2CppCatchPoint g_catchPoints[1] = { { 0, 0, 0, 0, } };
#else
static const Il2CppCatchPoint g_catchPoints[1] = { { 0, 0, 0, 0, } };
#endif
#if IL2CPP_MONO_DEBUGGER
static const Il2CppSequencePointSourceFile g_sequencePointSourceFiles[1] = { NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
#else
static const Il2CppSequencePointSourceFile g_sequencePointSourceFiles[1] = { NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
#endif
#if IL2CPP_MONO_DEBUGGER
static const Il2CppTypeSourceFilePair g_typeSourceFiles[1] = { { 0, 0 } };
#else
static const Il2CppTypeSourceFilePair g_typeSourceFiles[1] = { { 0, 0 } };
#endif
#if IL2CPP_MONO_DEBUGGER
static const Il2CppMethodScope g_methodScopes[1] = { { 0, 0 } };
#else
static const Il2CppMethodScope g_methodScopes[1] = { { 0, 0 } };
#endif
#if IL2CPP_MONO_DEBUGGER
static const Il2CppMethodHeaderInfo g_methodHeaderInfos[91] = 
{
	{ 0, 0, 0 } /* System.Exception System.Linq.Error::ArgumentNull(System.String) */,
	{ 0, 0, 0 } /* System.Exception System.Linq.Error::MoreThanOneMatch() */,
	{ 0, 0, 0 } /* System.Exception System.Linq.Error::NoElements() */,
	{ 0, 0, 0 } /* System.Collections.Generic.IEnumerable`1<TSource> System.Linq.Enumerable::Where(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,System.Boolean>) */,
	{ 0, 0, 0 } /* System.Func`2<TSource,System.Boolean> System.Linq.Enumerable::CombinePredicates(System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,System.Boolean>) */,
	{ 0, 0, 0 } /* System.Linq.IOrderedEnumerable`1<TSource> System.Linq.Enumerable::OrderByDescending(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,TKey>) */,
	{ 0, 0, 0 } /* TSource[] System.Linq.Enumerable::ToArray(System.Collections.Generic.IEnumerable`1<TSource>) */,
	{ 0, 0, 0 } /* System.Collections.Generic.Dictionary`2<TKey,TElement> System.Linq.Enumerable::ToDictionary(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,TKey>,System.Func`2<TSource,TElement>) */,
	{ 0, 0, 0 } /* System.Collections.Generic.Dictionary`2<TKey,TElement> System.Linq.Enumerable::ToDictionary(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,TKey>,System.Func`2<TSource,TElement>,System.Collections.Generic.IEqualityComparer`1<TKey>) */,
	{ 0, 0, 0 } /* TSource System.Linq.Enumerable::First(System.Collections.Generic.IEnumerable`1<TSource>) */,
	{ 0, 0, 0 } /* TSource System.Linq.Enumerable::SingleOrDefault(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,System.Boolean>) */,
	{ 0, 0, 0 } /* System.Boolean System.Linq.Enumerable::Any(System.Collections.Generic.IEnumerable`1<TSource>) */,
	{ 0, 0, 0 } /* System.Boolean System.Linq.Enumerable::Any(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,System.Boolean>) */,
	{ 0, 0, 0 } /* System.Void System.Linq.Enumerable/Iterator`1::.ctor() */,
	{ 0, 0, 0 } /* TSource System.Linq.Enumerable/Iterator`1::get_Current() */,
	{ 0, 0, 0 } /* System.Linq.Enumerable/Iterator`1<TSource> System.Linq.Enumerable/Iterator`1::Clone() */,
	{ 0, 0, 0 } /* System.Void System.Linq.Enumerable/Iterator`1::Dispose() */,
	{ 0, 0, 0 } /* System.Collections.Generic.IEnumerator`1<TSource> System.Linq.Enumerable/Iterator`1::GetEnumerator() */,
	{ 0, 0, 0 } /* System.Boolean System.Linq.Enumerable/Iterator`1::MoveNext() */,
	{ 0, 0, 0 } /* System.Collections.Generic.IEnumerable`1<TSource> System.Linq.Enumerable/Iterator`1::Where(System.Func`2<TSource,System.Boolean>) */,
	{ 0, 0, 0 } /* System.Object System.Linq.Enumerable/Iterator`1::System.Collections.IEnumerator.get_Current() */,
	{ 0, 0, 0 } /* System.Collections.IEnumerator System.Linq.Enumerable/Iterator`1::System.Collections.IEnumerable.GetEnumerator() */,
	{ 0, 0, 0 } /* System.Void System.Linq.Enumerable/Iterator`1::System.Collections.IEnumerator.Reset() */,
	{ 0, 0, 0 } /* System.Void System.Linq.Enumerable/WhereEnumerableIterator`1::.ctor(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,System.Boolean>) */,
	{ 0, 0, 0 } /* System.Linq.Enumerable/Iterator`1<TSource> System.Linq.Enumerable/WhereEnumerableIterator`1::Clone() */,
	{ 0, 0, 0 } /* System.Void System.Linq.Enumerable/WhereEnumerableIterator`1::Dispose() */,
	{ 0, 0, 0 } /* System.Boolean System.Linq.Enumerable/WhereEnumerableIterator`1::MoveNext() */,
	{ 0, 0, 0 } /* System.Collections.Generic.IEnumerable`1<TSource> System.Linq.Enumerable/WhereEnumerableIterator`1::Where(System.Func`2<TSource,System.Boolean>) */,
	{ 0, 0, 0 } /* System.Void System.Linq.Enumerable/WhereArrayIterator`1::.ctor(TSource[],System.Func`2<TSource,System.Boolean>) */,
	{ 0, 0, 0 } /* System.Linq.Enumerable/Iterator`1<TSource> System.Linq.Enumerable/WhereArrayIterator`1::Clone() */,
	{ 0, 0, 0 } /* System.Boolean System.Linq.Enumerable/WhereArrayIterator`1::MoveNext() */,
	{ 0, 0, 0 } /* System.Collections.Generic.IEnumerable`1<TSource> System.Linq.Enumerable/WhereArrayIterator`1::Where(System.Func`2<TSource,System.Boolean>) */,
	{ 0, 0, 0 } /* System.Void System.Linq.Enumerable/WhereListIterator`1::.ctor(System.Collections.Generic.List`1<TSource>,System.Func`2<TSource,System.Boolean>) */,
	{ 0, 0, 0 } /* System.Linq.Enumerable/Iterator`1<TSource> System.Linq.Enumerable/WhereListIterator`1::Clone() */,
	{ 0, 0, 0 } /* System.Boolean System.Linq.Enumerable/WhereListIterator`1::MoveNext() */,
	{ 0, 0, 0 } /* System.Collections.Generic.IEnumerable`1<TSource> System.Linq.Enumerable/WhereListIterator`1::Where(System.Func`2<TSource,System.Boolean>) */,
	{ 0, 0, 0 } /* System.Void System.Linq.Enumerable/<>c__DisplayClass6_0`1::.ctor() */,
	{ 0, 0, 0 } /* System.Boolean System.Linq.Enumerable/<>c__DisplayClass6_0`1::<CombinePredicates>b__0(TSource) */,
	{ 0, 0, 0 } /* System.Collections.Generic.IEnumerator`1<TElement> System.Linq.OrderedEnumerable`1::GetEnumerator() */,
	{ 0, 0, 0 } /* System.Linq.EnumerableSorter`1<TElement> System.Linq.OrderedEnumerable`1::GetEnumerableSorter(System.Linq.EnumerableSorter`1<TElement>) */,
	{ 0, 0, 0 } /* System.Collections.IEnumerator System.Linq.OrderedEnumerable`1::System.Collections.IEnumerable.GetEnumerator() */,
	{ 0, 0, 0 } /* System.Void System.Linq.OrderedEnumerable`1::.ctor() */,
	{ 0, 0, 0 } /* System.Void System.Linq.OrderedEnumerable`1/<GetEnumerator>d__1::.ctor(System.Int32) */,
	{ 0, 0, 0 } /* System.Void System.Linq.OrderedEnumerable`1/<GetEnumerator>d__1::System.IDisposable.Dispose() */,
	{ 0, 0, 0 } /* System.Boolean System.Linq.OrderedEnumerable`1/<GetEnumerator>d__1::MoveNext() */,
	{ 0, 0, 0 } /* TElement System.Linq.OrderedEnumerable`1/<GetEnumerator>d__1::System.Collections.Generic.IEnumerator<TElement>.get_Current() */,
	{ 0, 0, 0 } /* System.Void System.Linq.OrderedEnumerable`1/<GetEnumerator>d__1::System.Collections.IEnumerator.Reset() */,
	{ 0, 0, 0 } /* System.Object System.Linq.OrderedEnumerable`1/<GetEnumerator>d__1::System.Collections.IEnumerator.get_Current() */,
	{ 0, 0, 0 } /* System.Void System.Linq.OrderedEnumerable`2::.ctor(System.Collections.Generic.IEnumerable`1<TElement>,System.Func`2<TElement,TKey>,System.Collections.Generic.IComparer`1<TKey>,System.Boolean) */,
	{ 0, 0, 0 } /* System.Linq.EnumerableSorter`1<TElement> System.Linq.OrderedEnumerable`2::GetEnumerableSorter(System.Linq.EnumerableSorter`1<TElement>) */,
	{ 0, 0, 0 } /* System.Void System.Linq.EnumerableSorter`1::ComputeKeys(TElement[],System.Int32) */,
	{ 0, 0, 0 } /* System.Int32 System.Linq.EnumerableSorter`1::CompareKeys(System.Int32,System.Int32) */,
	{ 0, 0, 0 } /* System.Int32[] System.Linq.EnumerableSorter`1::Sort(TElement[],System.Int32) */,
	{ 0, 0, 0 } /* System.Void System.Linq.EnumerableSorter`1::QuickSort(System.Int32[],System.Int32,System.Int32) */,
	{ 0, 0, 0 } /* System.Void System.Linq.EnumerableSorter`1::.ctor() */,
	{ 0, 0, 0 } /* System.Void System.Linq.EnumerableSorter`2::.ctor(System.Func`2<TElement,TKey>,System.Collections.Generic.IComparer`1<TKey>,System.Boolean,System.Linq.EnumerableSorter`1<TElement>) */,
	{ 0, 0, 0 } /* System.Void System.Linq.EnumerableSorter`2::ComputeKeys(TElement[],System.Int32) */,
	{ 0, 0, 0 } /* System.Int32 System.Linq.EnumerableSorter`2::CompareKeys(System.Int32,System.Int32) */,
	{ 0, 0, 0 } /* System.Void System.Linq.Buffer`1::.ctor(System.Collections.Generic.IEnumerable`1<TElement>) */,
	{ 0, 0, 0 } /* TElement[] System.Linq.Buffer`1::ToArray() */,
	{ 0, 0, 0 } /* System.Void System.Collections.Generic.HashSet`1::.ctor() */,
	{ 0, 0, 0 } /* System.Void System.Collections.Generic.HashSet`1::.ctor(System.Collections.Generic.IEqualityComparer`1<T>) */,
	{ 0, 0, 0 } /* System.Void System.Collections.Generic.HashSet`1::.ctor(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext) */,
	{ 0, 0, 0 } /* System.Void System.Collections.Generic.HashSet`1::System.Collections.Generic.ICollection<T>.Add(T) */,
	{ 0, 0, 0 } /* System.Void System.Collections.Generic.HashSet`1::Clear() */,
	{ 0, 0, 0 } /* System.Boolean System.Collections.Generic.HashSet`1::Contains(T) */,
	{ 0, 0, 0 } /* System.Void System.Collections.Generic.HashSet`1::CopyTo(T[],System.Int32) */,
	{ 0, 0, 0 } /* System.Boolean System.Collections.Generic.HashSet`1::Remove(T) */,
	{ 0, 0, 0 } /* System.Int32 System.Collections.Generic.HashSet`1::get_Count() */,
	{ 0, 0, 0 } /* System.Boolean System.Collections.Generic.HashSet`1::System.Collections.Generic.ICollection<T>.get_IsReadOnly() */,
	{ 0, 0, 0 } /* System.Collections.Generic.HashSet`1/Enumerator<T> System.Collections.Generic.HashSet`1::GetEnumerator() */,
	{ 0, 0, 0 } /* System.Collections.Generic.IEnumerator`1<T> System.Collections.Generic.HashSet`1::System.Collections.Generic.IEnumerable<T>.GetEnumerator() */,
	{ 0, 0, 0 } /* System.Collections.IEnumerator System.Collections.Generic.HashSet`1::System.Collections.IEnumerable.GetEnumerator() */,
	{ 0, 0, 0 } /* System.Void System.Collections.Generic.HashSet`1::GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext) */,
	{ 0, 0, 0 } /* System.Void System.Collections.Generic.HashSet`1::OnDeserialization(System.Object) */,
	{ 0, 0, 0 } /* System.Boolean System.Collections.Generic.HashSet`1::Add(T) */,
	{ 0, 0, 0 } /* System.Void System.Collections.Generic.HashSet`1::CopyTo(T[]) */,
	{ 0, 0, 0 } /* System.Void System.Collections.Generic.HashSet`1::CopyTo(T[],System.Int32,System.Int32) */,
	{ 0, 0, 0 } /* System.Void System.Collections.Generic.HashSet`1::Initialize(System.Int32) */,
	{ 0, 0, 0 } /* System.Void System.Collections.Generic.HashSet`1::IncreaseCapacity() */,
	{ 0, 0, 0 } /* System.Void System.Collections.Generic.HashSet`1::SetCapacity(System.Int32) */,
	{ 0, 0, 0 } /* System.Boolean System.Collections.Generic.HashSet`1::AddIfNotPresent(T) */,
	{ 0, 0, 0 } /* System.Int32 System.Collections.Generic.HashSet`1::InternalGetHashCode(T) */,
	{ 0, 0, 0 } /* System.Void System.Collections.Generic.HashSet`1/Enumerator::.ctor(System.Collections.Generic.HashSet`1<T>) */,
	{ 0, 0, 0 } /* System.Void System.Collections.Generic.HashSet`1/Enumerator::Dispose() */,
	{ 0, 0, 0 } /* System.Boolean System.Collections.Generic.HashSet`1/Enumerator::MoveNext() */,
	{ 0, 0, 0 } /* T System.Collections.Generic.HashSet`1/Enumerator::get_Current() */,
	{ 0, 0, 0 } /* System.Object System.Collections.Generic.HashSet`1/Enumerator::System.Collections.IEnumerator.get_Current() */,
	{ 0, 0, 0 } /* System.Void System.Collections.Generic.HashSet`1/Enumerator::System.Collections.IEnumerator.Reset() */,
	{ 0, 0, 0 } /* System.Void System.Collections.Generic.ICollectionDebugView`1::.ctor(System.Collections.Generic.ICollection`1<T>) */,
	{ 0, 0, 0 } /* T[] System.Collections.Generic.ICollectionDebugView`1::get_Items() */,
};
#else
static const Il2CppMethodHeaderInfo g_methodHeaderInfos[1] = { { 0, 0, 0 } };
#endif
extern const Il2CppDebuggerMetadataRegistration g_DebuggerMetadataRegistrationSystem_Core;
const Il2CppDebuggerMetadataRegistration g_DebuggerMetadataRegistrationSystem_Core = 
{
	(Il2CppMethodExecutionContextInfo*)g_methodExecutionContextInfos,
	(Il2CppMethodExecutionContextInfoIndex*)g_methodExecutionContextInfoIndexes,
	(Il2CppMethodScope*)g_methodScopes,
	(Il2CppMethodHeaderInfo*)g_methodHeaderInfos,
	(Il2CppSequencePointSourceFile*)g_sequencePointSourceFiles,
	0,
	(Il2CppSequencePoint*)g_sequencePointsSystem_Core,
	0,
	(Il2CppCatchPoint*)g_catchPoints,
	0,
	(Il2CppTypeSourceFilePair*)g_typeSourceFiles,
	(const char**)g_methodExecutionContextInfoStrings,
};
