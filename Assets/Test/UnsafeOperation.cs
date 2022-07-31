using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using MonoVTable = System.IntPtr;

namespace PtrReflection
{
    public unsafe class UnsafeOperation
    {
        public readonly static int PTR_COUNT = sizeof(IntPtr);


        public unsafe static int HeapSizeOf(MonoVTable vtable)//*MonoVTable
        {
            IntPtr**** p1 = (IntPtr****)vtable;
            IntPtr* p2 = ***p1;
            p2 += 3;
            int* intPtrIdepth = (int*)p2;
            ++intPtrIdepth;
            int instanceSize = *intPtrIdepth;
            return instanceSize;
        }


        public unsafe static bool IsCreate(Type type)
        {
            if (type.IsSubclassOf(typeof(UnityEngine.Object)))
            {
                return false;
            }
            return true;
        }
        public unsafe static IntPtr GetTypeHead(Type type)
        {
            object obj = FormatterServices.GetUninitializedObject(type);
            ulong gcHandle;
            void* ptr = UnsafeUtility.PinGCObjectAndGetAddress(obj, out gcHandle);
            UnsafeUtility.ReleaseGCObject(gcHandle);
            return *(IntPtr*)ptr;
        }


    }
}
