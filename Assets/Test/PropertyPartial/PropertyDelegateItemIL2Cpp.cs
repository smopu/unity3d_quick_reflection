using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using AilFieldType = System.Int64;

namespace PtrReflection
{

    public unsafe partial class PropertyDelegateItem
    {

        [FieldOffset(0)]
        public ActionVoidPtr<PropertyDelegateItemIL2Cpp.Size1> setSize1;

        [FieldOffset(0)]
        public ActionVoidPtr<PropertyDelegateItemIL2Cpp.Size2> setSize2;

        [FieldOffset(0)]
        public ActionVoidPtr<PropertyDelegateItemIL2Cpp.Size3> setSize3;

        [FieldOffset(0)]
        public ActionVoidPtr<PropertyDelegateItemIL2Cpp.Size4> setSize4;

        [FieldOffset(0)]
        public ActionVoidPtr<PropertyDelegateItemIL2Cpp.Size5> setSize5;

        [FieldOffset(0)]
        public ActionVoidPtr<PropertyDelegateItemIL2Cpp.Size6> setSize6;

        [FieldOffset(0)]
        public ActionVoidPtr<PropertyDelegateItemIL2Cpp.Size7> setSize7;


        [FieldOffset(16)]
        public FuncVoidPtr<PropertyDelegateItemIL2Cpp.Size1> getSize1;

        [FieldOffset(16)]
        public FuncVoidPtr<PropertyDelegateItemIL2Cpp.Size2> getSize2;

        [FieldOffset(16)]
        public FuncVoidPtr<PropertyDelegateItemIL2Cpp.Size3> getSize3;

        [FieldOffset(16)]
        public FuncVoidPtr<PropertyDelegateItemIL2Cpp.Size4> getSize4;

        [FieldOffset(16)]
        public FuncVoidPtr<PropertyDelegateItemIL2Cpp.Size5> getSize5;

        [FieldOffset(16)]
        public FuncVoidPtr<PropertyDelegateItemIL2Cpp.Size6> getSize6;

        [FieldOffset(16)]
        public FuncVoidPtr<PropertyDelegateItemIL2Cpp.Size7> getSize7;


    }

    public class PropertyDelegateItemIL2Cpp
    {
        public static unsafe void SetObject(PropertyDelegateItem set, void* obj, void* value, int index)
        {
            switch (index)
            {
                case 1:
                    set.setSize1(obj, UnsafeUtility.ReadArrayElement<PropertyDelegateItemIL2Cpp.Size1>(value, 0));
                    break;
                case 2:
                    set.setSize2(obj, UnsafeUtility.ReadArrayElement<PropertyDelegateItemIL2Cpp.Size2>(value, 0));
                    break;
                case 3:
                    set.setSize3(obj, UnsafeUtility.ReadArrayElement<PropertyDelegateItemIL2Cpp.Size3>(value, 0));
                    break;
                case 4:
                    set.setSize4(obj, UnsafeUtility.ReadArrayElement<PropertyDelegateItemIL2Cpp.Size4>(value, 0));
                    break;
                case 5:
                    set.setSize5(obj, UnsafeUtility.ReadArrayElement<PropertyDelegateItemIL2Cpp.Size5>(value, 0));
                    break;
                case 6:
                    set.setSize6(obj, UnsafeUtility.ReadArrayElement<PropertyDelegateItemIL2Cpp.Size6>(value, 0));
                    break;
                case 7:
                    set.setSize7(obj, UnsafeUtility.ReadArrayElement<PropertyDelegateItemIL2Cpp.Size7>(value, 0));
                    break;
            }
        }

        public static unsafe T GetObject<T>(PropertyDelegateItem get, void* obj, int index)
        {
            switch (index)
            {
                case 1:
                    var size1 = get.getSize1(obj);
                    var v = UnsafeUtility.ReadArrayElement<T>(UnsafeUtility.AddressOf(ref size1), 0);
                    return v;
                case 2:
                    var size2 = get.getSize2(obj);
                    return UnsafeUtility.ReadArrayElement<T>(UnsafeUtility.AddressOf(ref size2), 0);
                case 3:
                    var size3 = get.getSize3(obj);
                    return UnsafeUtility.ReadArrayElement<T>(UnsafeUtility.AddressOf(ref size3), 0);
                case 4:
                    var size4 = get.getSize4(obj);
                    return UnsafeUtility.ReadArrayElement<T>(UnsafeUtility.AddressOf(ref size4), 0);
                case 5:
                    var size5 = get.getSize5(obj);
                    return UnsafeUtility.ReadArrayElement<T>(UnsafeUtility.AddressOf(ref size5), 0);
                case 6:
                    var size6 = get.getSize6(obj);
                    return UnsafeUtility.ReadArrayElement<T>(UnsafeUtility.AddressOf(ref size6), 0);
                case 7:
                    var size7 = get.getSize7(obj);
                    return UnsafeUtility.ReadArrayElement<T>(UnsafeUtility.AddressOf(ref size7), 0);
            }
            return default(T);
        }
        
        public static unsafe void CopyGetObject(PropertyDelegateItem get, void* obj, void* value, int index, int valueSize)
        {
            switch (index)
            {
                case 1:
                    var size1 = get.getSize1(obj);
                    UnsafeUtility.MemCpy(value, UnsafeUtility.AddressOf(ref size1), valueSize);
                    break;
                case 2:
                    var size2 = get.getSize2(obj);
                    UnsafeUtility.MemCpy(value, UnsafeUtility.AddressOf(ref size2), valueSize);
                    break;
                case 3:
                    var size3 = get.getSize3(obj);
                    UnsafeUtility.MemCpy(value, UnsafeUtility.AddressOf(ref size3), valueSize);
                    break;
                case 4:
                    var size4 = get.getSize4(obj);
                    UnsafeUtility.MemCpy(value, UnsafeUtility.AddressOf(ref size4), valueSize);
                    break;
                case 5:
                    var size5 = get.getSize5(obj);
                    UnsafeUtility.MemCpy(value, UnsafeUtility.AddressOf(ref size5), valueSize);
                    break;
                case 6:
                    var size6 = get.getSize6(obj);
                    UnsafeUtility.MemCpy(value, UnsafeUtility.AddressOf(ref size6), valueSize);
                    break;
                case 7:
                    var size7 = get.getSize7(obj);
                    UnsafeUtility.MemCpy(value, UnsafeUtility.AddressOf(ref size7), valueSize);
                    break;
            }
        }

        public static unsafe object GetObject(PropertyDelegateItem get, void* obj, int index) 
        {
            switch (index)
            {
                case 1:
                    return get.getSize1(obj);
                case 2:
                    return get.getSize2(obj);
                case 3:
                    return get.getSize3(obj);
                case 4:
                    return get.getSize4(obj);
                case 5:
                    return get.getSize5(obj);
                case 6:
                    return get.getSize6(obj);
                case 7:
                    return get.getSize7(obj);
            }
            return null;
        }

        public const int AilSize = 16;
        public const int AilFieldSize = sizeof(AilFieldType);

        [StructLayout(LayoutKind.Explicit)]
        public struct Size1
        {
            [FieldOffset(AilSize * 1 - AilFieldSize)]
            public AilFieldType data;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct Size2
        {
            [FieldOffset(AilSize * 2 - AilFieldSize)]
            AilFieldType data;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct Size3
        {
            [FieldOffset(AilSize * 3 - AilFieldSize)]
            AilFieldType data;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct Size4
        {
            [FieldOffset(AilSize * 4 - AilFieldSize)]
            AilFieldType data;
        }


        [StructLayout(LayoutKind.Explicit)]
        public struct Size5
        {
            [FieldOffset(AilSize * 5 - AilFieldSize)]
            AilFieldType data;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct Size6
        {
            [FieldOffset(AilSize * 6 - AilFieldSize)]
            AilFieldType data;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct Size7
        {
            [FieldOffset(AilSize * 7 - AilFieldSize)]
            AilFieldType data;
        }




    }


}
