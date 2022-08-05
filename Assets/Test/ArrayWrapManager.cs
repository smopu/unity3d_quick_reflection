using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Unity.Collections.LowLevel.Unsafe;

namespace PtrReflection
{
    [StructLayout(LayoutKind.Explicit)]
    public unsafe class UnsafeTool
    {
        public static UnsafeTool unsafeTool = new UnsafeTool();
        public delegate void* ObjectToVoidPtr(object obj);
        [FieldOffset(0)]
        public ObjectToVoidPtr objectToVoidPtr;
        [FieldOffset(0)]
        Func<object, object> func;

        public delegate object VoidPtrToObject(void* ptr);
        [FieldOffset(0)]
        public VoidPtrToObject voidPtrToObject;
        [FieldOffset(0)]
        Func<object, object> func2;

        public UnsafeTool()
        {
            func = Out;
            func2 = Out;
        }
        object Out(object o) { return o; }
    }
    public unsafe static class ArrayWrapManager
    {
        public static IArrayWrap GetIArrayWrap(Type type)
        {
            var itemType = type.GetElementType();
            int rank = type.GetArrayRank();
            if (rank == 1)
            {
                ArrayWrapRankOne arrayWrapRank = new ArrayWrapRankOne(1, itemType);
                return arrayWrapRank;
            }
            else
            {
                ArrayWrapRank arrayWrapRank = new ArrayWrapRank(rank, itemType);
                return arrayWrapRank;
            }
        }
    }

    public unsafe abstract class IArrayWrap
    {
        public readonly TypeCode elementTypeCode;
        public readonly IntPtr head;
        public readonly int rank;
        public readonly int elementTypeSize;
        public readonly Type elementType; 
        public readonly bool isValueType;
        public IntPtr typeHead;

        public IArrayWrap(int rank, Type elementType)
        {
            this.rank = rank;
            this.elementType = elementType;
            this.isValueType = elementType.IsValueType;
            if (isValueType)
            {
                this.elementTypeSize = UnsafeUtility.SizeOf(elementType);
            }
            else
            {
                this.elementTypeSize = UnsafeOperation.PTR_COUNT;
            }
            this.elementTypeCode = Type.GetTypeCode(elementType);
            if (rank == 1)
            {
                this.head = *(IntPtr*)UnsafeTool.unsafeTool.objectToVoidPtr(Array.CreateInstance(elementType, 0));
            }
            else
            {
                this.head = *(IntPtr*)UnsafeTool.unsafeTool.objectToVoidPtr(Array.CreateInstance(elementType, new int[rank]));
            }
        }

        public abstract object CreateArray(ref ArrayWrapInputData arrayWrapData);
        public abstract ArrayWrapOutData GetArrayData(Array array);

        public unsafe object GetValue(void* source, int index)
        {
            byte* field = (byte*)source;
            field += index * this.elementTypeSize;

            if (this.isValueType)
            {
                switch (elementTypeCode)
                {
                    case TypeCode.Boolean:
                        return *(bool*)field;
                    case TypeCode.Byte:
                        return *(Byte*)field;
                    case TypeCode.Char:
                        return *(Char*)field;
                    case TypeCode.DateTime:
                        return *(DateTime*)field;
                    case TypeCode.Decimal:
                        return *(Decimal*)field;
                    case TypeCode.Double:
                        return *(Double*)field;
                    case TypeCode.Int16:
                        return *(Int16*)field;
                    case TypeCode.Int32:
                        return *(Int32*)field;
                    case TypeCode.Int64:
                        return *(Int64*)field;
                    case TypeCode.SByte:
                        return *(Int64*)field;
                    case TypeCode.Single:
                        return *(Single*)field;
                    case TypeCode.UInt16:
                        return *(UInt16*)field;
                    case TypeCode.UInt32:
                        return *(UInt32*)field;
                    case TypeCode.UInt64:
                        return *(UInt64*)field;
                    case TypeCode.Object:
                    default:
                        //GC.Collect(); 
                        //return null;  
                        ulong gcHandle;
                        object obj = new byte[this.elementTypeSize - 1 * UnsafeOperation.PTR_COUNT];
                        IntPtr* ptr = (IntPtr*)UnsafeUtility.PinGCObjectAndGetAddress(obj, out gcHandle);
                        if (typeHead == default(IntPtr))
                        {
                            typeHead = UnsafeOperation.GetTypeHead(elementType);
                        }
                        *ptr = typeHead;
                        ptr += 2;
                        UnsafeUtility.MemCpy(ptr, field, this.elementTypeSize);
                        UnsafeUtility.ReleaseGCObject(gcHandle);
                        //GC.Collect();
                        return obj;
                }
            }
            else
            {
                return UnsafeUtility.ReadArrayElement<object>(field, 0);
                //return GeneralTool.VoidToObject(*(IntPtr**)field);
            }
        }

        public unsafe void SetValue(void* source, int index, object value)
        {
            byte* field = (byte*)source;
            field += index * this.elementTypeSize;

            if (this.isValueType)
            {
                switch (elementTypeCode)
                {
                    case TypeCode.Boolean:
                        *(bool*)field = (bool)value;
                        break;
                    case TypeCode.Byte:
                        *(Byte*)field = (Byte)value;
                        break;
                    case TypeCode.Char:
                        *(Char*)field = (Char)value;
                        break;
                    case TypeCode.DateTime:
                        *(DateTime*)field = (DateTime)value;
                        break;
                    case TypeCode.Decimal:
                        *(Decimal*)field = (Decimal)value;
                        break;
                    case TypeCode.Double:
                        *(Double*)field = (Double)value;
                        break;
                    case TypeCode.Empty:
                        break;
                    case TypeCode.Int16:
                        *(Int16*)field = (Int16)value;
                        break;
                    case TypeCode.Int32:
                        *(Int32*)field = (Int32)value;
                        break;
                    case TypeCode.Int64:
                        *(Int64*)field = (Int64)value;
                        break;
                    case TypeCode.SByte:
                        *(SByte*)field = (SByte)value;
                        break;
                    case TypeCode.Single:
                        *(Single*)field = (Single)value;
                        break;
                    case TypeCode.UInt16:
                        *(UInt16*)field = (UInt16)value;
                        break;
                    case TypeCode.UInt32:
                        *(UInt32*)field = (UInt32)value;
                        break;
                    case TypeCode.UInt64:
                        *(UInt64*)field = (UInt64)value;
                        break;
                    case TypeCode.String:
                    case TypeCode.Object:
                        ulong gcHandle;
                        IntPtr* ptr = (IntPtr*)UnsafeUtility.PinGCObjectAndGetAddress(value, out gcHandle);
                        UnsafeUtility.MemCpy(field, ptr + 2, elementTypeSize);
                        UnsafeUtility.ReleaseGCObject(gcHandle);
                        break;
                }
            }
            else
            {
                UnsafeUtility.CopyObjectAddressToPtr(value, field);
                //GeneralTool.SetObject(field, value);
            }
        }


    }

    public unsafe struct ArrayWrapInputData
    {
        public int length;
        public int* arrayLengths;
        public byte* objPtr;
        public byte* startItemOffcet;
        public GCHandle gCHandle;
    }

    public unsafe struct ArrayWrapOutData
    {
        public int length;
        public int[] arrayLengths;
        public void* objPtr;
        public byte* startItemOffcet;
        public GCHandle gCHandle;
    }
    unsafe class ArrayWrapRank : IArrayWrap
    {
        public ArrayWrapRank(int rank, Type elementType) : base(rank, elementType) { }

        public override object CreateArray(ref ArrayWrapInputData arrayWrapData)
        {
            int offcet = rank * 8;
            int arrayMsize = offcet + arrayWrapData.length * this.elementTypeSize;

            object array = new byte[arrayMsize];

            IntPtr* p = (IntPtr*)UnsafeTool.unsafeTool.objectToVoidPtr(array);
            arrayWrapData.objPtr = (byte*)p;
            *p = head;
            ++p;
            *p = (IntPtr)arrayWrapData.length; ++p;
            //*p = type.TypeHandle.Value; ++p;
            UnsafeUtility.MemCpy(p, arrayWrapData.arrayLengths, rank * 4);
            arrayWrapData.startItemOffcet = ((byte*)p);
            arrayWrapData.startItemOffcet += rank * 8;

            return array;
        }

        public override ArrayWrapOutData GetArrayData(Array array) 
        {
            ArrayWrapOutData arrayWrapData = new ArrayWrapOutData();
            arrayWrapData.arrayLengths = new int[array.Rank];
            for (int i = 0; i < array.Rank; i++)
            {
                arrayWrapData.arrayLengths[i] = array.GetLength(i);
            }
            IntPtr* p = (IntPtr*)UnsafeTool.unsafeTool.objectToVoidPtr(array);
            arrayWrapData.objPtr = (byte*)p;
            p += 2;
            arrayWrapData.startItemOffcet = ((byte*)p);
            arrayWrapData.startItemOffcet += rank * 8;
            return arrayWrapData;
        }

    }

    unsafe class ArrayWrapRankOne : IArrayWrap
    {
        public ArrayWrapRankOne(int rank, Type elementType) : base(rank, elementType) { }
        public override object CreateArray(ref ArrayWrapInputData arrayWrapData)
        {
            int arrayMsize = arrayWrapData.length * this.elementTypeSize;
            object array = new byte[arrayMsize];
            IntPtr* p = (IntPtr*)UnsafeTool.unsafeTool.objectToVoidPtr(array);
            arrayWrapData.objPtr = (byte*)p;
            *p = head;
            ++p;
            *p = (IntPtr)arrayWrapData.length; ++p;
            //*p = type.TypeHandle.Value; ++p;
            arrayWrapData.startItemOffcet = (byte*)p;
            return array;
        }
        public override ArrayWrapOutData GetArrayData(Array array)
        {
            ArrayWrapOutData arrayWrapData = new ArrayWrapOutData();
            arrayWrapData.length = array.Length;

            ulong gcHandle;
            arrayWrapData.objPtr = UnsafeUtility.PinGCObjectAndGetAddress(array, out gcHandle);
            UnsafeUtility.ReleaseGCObject(gcHandle);

            arrayWrapData.startItemOffcet = (byte*)UnsafeUtility.PinGCArrayAndGetDataAddress(array, out gcHandle);
            UnsafeUtility.ReleaseGCObject(gcHandle);

            return arrayWrapData;
        }
    }

}
