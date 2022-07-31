//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Reflection;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;
//using System.Runtime.Serialization;
//using Unity.Collections.LowLevel.Unsafe;
//using UnityEngine;

//#if UNITY_64
//using Int_Ptr = System.Int64;
//#else
//using Int_Ptr = System.Int32;
//#endif


//public unsafe class ReflectionManager
//{

//    //int PTR_COUNT = sizeof(IntPtr);
//#if UNITY_64
//    const int PTR_COUNT = 8;
//#else
//    const int PTR_COUNT = 4;
//#endif


//    public unsafe static TypeAddrReflectionWarp GetDictionaryTypeFields(Type type)
//    {
//        Dictionary<string, TypeAddrField> nameOfField = new Dictionary<string, TypeAddrField>();
//        Type maxFieldType = null;
//        int maxOffset = 0;
//        TypeAddrReflectionWarp typeReflectionWarp = new TypeAddrReflectionWarp();
//        FieldInfo[] typeFieldsNow = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
//        foreach (var item in typeFieldsNow)
//        {
//            int offset = UnsafeUtility.GetFieldOffset(item);
//            if (maxOffset < offset)
//            {
//                maxOffset = offset;
//                maxFieldType = item.FieldType;
//            }
//            if (nameOfField.ContainsKey(item.Name))
//            {
//                nameOfField[item.Name] = new TypeAddrField(type.GetField(item.Name,
//                    BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public));
//            }
//            else
//            {
//                nameOfField[item.Name] = new TypeAddrField(item);
//            }
//        }

//        while (type.BaseType != typeof(object))
//        {
//            foreach (var item in type.BaseType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
//            {
//                if (item.Attributes == FieldAttributes.Private)
//                {
//                    if (!nameOfField.ContainsKey(item.Name))
//                    {
//                        nameOfField[item.Name] = new TypeAddrField(item);
//                    }
//                }
//            }
//            type = type.BaseType;
//        }

//        typeReflectionWarp.allTypeField = new TypeAddrField[nameOfField.Count];
//        Dictionary<string, int> strs = new Dictionary<string, int>();
//        int indexNow = 0;
//        foreach (var item in nameOfField)
//        {
//            typeReflectionWarp.allTypeField[indexNow] = item.Value;
//            strs[item.Key] = indexNow;
//            indexNow++;
//        }
//        typeReflectionWarp.nameOfField = nameOfField;
//        typeReflectionWarp.stringTable = new StringTable(strs);


//        int size = 0;
//        if (maxFieldType.IsValueType)
//        {
//            size = UnsafeUtility.SizeOf(maxFieldType);
//            if (size < PTR_COUNT)
//            {
//                size = PTR_COUNT;
//            }
//        }
//        else
//        {
//            size = PTR_COUNT;
//        }
//        size += maxOffset;
//        if (size % PTR_COUNT != 0)
//        {
//            size = size / PTR_COUNT * PTR_COUNT + PTR_COUNT;
//        }
//        if (size < 32)
//        {
//            size = 32;
//        }
//        typeReflectionWarp.type = type;
//        typeReflectionWarp.heapSize = size;
//        typeReflectionWarp.sizeByte_1 = size / PTR_COUNT - 1; 

//        typeReflectionWarp.typeHead = GetTypeHead(type);

//        return typeReflectionWarp;
//    }


//    private unsafe static Int_Ptr GetTypeHead(Type type)
//    {
//        object obj = FormatterServices.GetUninitializedObject(type);
//        ulong gcHandle;
//        void* ptr = UnsafeUtility.PinGCObjectAndGetAddress(obj, out gcHandle);
//        UnsafeUtility.ReleaseGCObject(gcHandle);
//        return *(Int_Ptr*)ptr;
//    }


//    public static FieldInfo[] GetTypeFields(Type type)
//    {
//        FieldInfo[] typeFieldsNow = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
//        List<FieldInfo> list = new List<FieldInfo>(typeFieldsNow);
//        while (type.BaseType != typeof(object))
//        {
//            foreach (var item in type.BaseType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
//            {
//                if (item.Attributes == FieldAttributes.Private)
//                {
//                    list.Add(item);
//                }
//            }
//            type = type.BaseType;
//        }
//        return list.ToArray();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    private static void SetObject(void* destination, object value)
//    {
//        GeneralTool.SetObject(destination, value);
//        //UnsafeUtility.WriteArrayElement<object>(destination, 0, value);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    private static void SetStruct(void* destination, void* value, int size)
//    {
//        value = ((IntPtr*)value) + 2;
//        UnsafeUtility.MemCpy(destination, value, size);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    private static object ReadObject(void* source)
//    {
//        return UnsafeUtility.ReadArrayElement<object>(source, 0);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    private static object ReadStruct(void* source, int size, Int_Ptr head)
//    {
//        if (size % PTR_COUNT != 0)
//        {
//            size = size / PTR_COUNT * PTR_COUNT + PTR_COUNT;
//        }
//        //object value = GetObject(size + PTR_COUNT * 2);
//        object value = new IntPtr[size / PTR_COUNT + 2 - 1];

//#if ENABLE_MONO
        
//        TypedReference tf = __makeref(value);
//        Int_Ptr* ptr = (Int_Ptr*)(*(IntPtr*)*((IntPtr*)&tf + 1));
//#else

//        ulong gcHandle;
//        long* ptr = (long*)UnsafeUtility.PinGCObjectAndGetAddress(value, out gcHandle);
//        UnsafeUtility.ReleaseGCObject(gcHandle);
//#endif
//        *ptr = head;
//        ptr += 2;

//        UnsafeUtility.MemCpy(ptr, source, size);

//        return value;
//    }

//    public unsafe class TypeAddrReflectionWarp
//    {
//        public Dictionary<string, TypeAddrField> nameOfField = new Dictionary<string, TypeAddrField>();
//        public StringTable stringTable;
//        public TypeAddrField[] allTypeField;

//        public Type type;
//        public int heapSize;
//        public int sizeByte_1;
//        public Int_Ptr typeHead;

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public object Create()
//        {
//            //object obj = GetObject(size);
//            object obj = new IntPtr[sizeByte_1];

//#if ENABLE_MONO
//            TypedReference tf = __makeref(obj);
//            void* ptr = (void*)(*(IntPtr*)*((IntPtr*)&tf + 1));
//#else
//            ulong gcHandle;
//            void* ptr = UnsafeUtility.PinGCObjectAndGetAddress(obj, out gcHandle);
//            UnsafeUtility.ReleaseGCObject(gcHandle);
//#endif

//            *(Int_Ptr*)ptr = typeHead;
//            return obj;
//        }

//        public object GetValue(object source, char* name, int length)
//        {
//            int index = stringTable.Find(name, length);
//            if (index >= 0)
//            {
//                return allTypeField[index].GetValue(source);
//            }
//            else
//            {
//                return null;
//            }
//        }



//        public object GetValue(object source, string name)
//        {
//            TypeAddrField typeField;
//            if (nameOfField.TryGetValue(name , out typeField))
//            {
//                return typeField.GetValue(source);
//            }
//            else
//            {
//                return null;
//            }
//        }


//        public void SetValue(object source, string name, object value)
//        {
//            TypeAddrField typeField;
//            if (nameOfField.TryGetValue(name, out typeField))
//            {
//                typeField.SetValue(source, value);
//            }
//        }

//        public void SetValue(object source, char* name, int length, object value)
//        {
//            int index = stringTable.Find(name, length);
//            if (index >= 0)
//            {
//                allTypeField[index].SetValue(source, value);
//            }
//        }




//    }



//    public unsafe class TypeAddrField
//    {
//        public TypeAddrField(FieldInfo fieldInfo)
//        {
//            this.fieldInfo = fieldInfo;
//            this.fieldType = fieldInfo.FieldType;
//            offset = UnsafeUtility.GetFieldOffset(fieldInfo);
//            typeCode = Type.GetTypeCode(fieldType);
//            isValueType = fieldType.IsValueType;
//            isArray = fieldType.IsArray;
//            isEnum = fieldType.IsEnum;
//            if (isValueType)
//            {
//                size = UnsafeUtility.SizeOf(fieldType);
//            }
//            else
//            {
//                size = PTR_COUNT;
//            }
//            typeHead = GetTypeHead(fieldType);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public unsafe void SetValue(object source, object value)
//        {
//#if ENABLE_MONO
//            TypedReference tf = __makeref(source);
//            void* v = (void*)(*(IntPtr*)*((IntPtr*)&tf + 1));
//#else
//            ulong gcHandle;
//            void* v = UnsafeUtility.PinGCObjectAndGetAddress(source, out gcHandle);
//            UnsafeUtility.ReleaseGCObject(gcHandle);
//#endif



//            void* field = (void*)((IntPtr)v + this.offset);

//            if (this.isValueType)
//            {
//                switch (this.typeCode)
//                {
//                    case TypeCode.Boolean:
//                        *(bool*)field = (bool)value;
//                        break;
//                    case TypeCode.Byte:
//                        *(Byte*)field = (Byte)value;
//                        break;
//                    case TypeCode.Char:
//                        *(Char*)field = (Char)value;
//                        break;
//                    case TypeCode.DateTime:
//                        *(DateTime*)field = (DateTime)value;
//                        break;
//                    case TypeCode.DBNull:
//                        GeneralTool.SetObject(field, value);
//                        break;
//                    case TypeCode.Decimal:
//                        *(Decimal*)field = (Decimal)value;
//                        break;
//                    case TypeCode.Double:
//                        *(Double*)field = (Double)value;
//                        break;
//                    case TypeCode.Empty:
//                        break;
//                    case TypeCode.Int16:
//                        *(Int16*)field = (Int16)value;
//                        break;
//                    case TypeCode.Int32:
//                        *(Int32*)field = (Int32)value;
//                        break;
//                    case TypeCode.Int64:
//                        *(Int64*)field = (Int64)value;
//                        break;
//                    case TypeCode.Object:

//#if ENABLE_MONO
//                        tf = __makeref(value);
//                        SetStruct(field, (void*)(*(IntPtr*)*((IntPtr*)&tf + 1)), this.size);
//#else
//                        SetStruct(field, UnsafeUtility.PinGCObjectAndGetAddress(value, out gcHandle), this.size);
//                        UnsafeUtility.ReleaseGCObject(gcHandle);
//#endif



//                        break;
//                    case TypeCode.SByte:
//                        *(SByte*)field = (SByte)value;
//                        break;
//                    case TypeCode.Single:
//                        *(Single*)field = (Single)value;
//                        break;
//                    case TypeCode.String:
//                        GeneralTool.SetObject(field, value);
//                        break;
//                    case TypeCode.UInt16:
//                        *(UInt16*)field = (UInt16)value;
//                        break;
//                    case TypeCode.UInt32:
//                        *(UInt32*)field = (UInt32)value;
//                        break;
//                    case TypeCode.UInt64:
//                        *(UInt64*)field = (UInt64)value;
//                        break;
//                    default:
//#if ENABLE_MONO
//                        tf = __makeref(value);
//                        SetStruct(field, (void*)(*(IntPtr*)*((IntPtr*)&tf + 1)), this.size);
//#else
//                        SetStruct(field, UnsafeUtility.PinGCObjectAndGetAddress(value, out gcHandle), this.size);
//                        UnsafeUtility.ReleaseGCObject(gcHandle);
//#endif
//                        break;
//                }
//            }
//            else
//            {
//                GeneralTool.SetObject(field, value);
//            }
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public object GetValue(object source)
//        {
//#if ENABLE_MONO
//            TypedReference tf = __makeref(source);
//            void* v = (void*)(*(IntPtr*)*((IntPtr*)&tf + 1));

//#else
//            ulong gcHandle;
//            void* v = UnsafeUtility.PinGCObjectAndGetAddress(source, out gcHandle);
//            UnsafeUtility.ReleaseGCObject(gcHandle);
//#endif


//            void* field = (void*)((IntPtr)v + this.offset);

//            if (this.isValueType)
//            {
//                switch (this.typeCode)
//                {
//                    case TypeCode.Boolean:
//                        return *(bool*)field;
//                    case TypeCode.Byte:
//                        return *(Byte*)field;
//                    case TypeCode.Char:
//                        return *(Char*)field;
//                    case TypeCode.DateTime:
//                        return *(DateTime*)field;
//                    case TypeCode.DBNull:
//                        return ReadObject(field);
//                    case TypeCode.Decimal:
//                        return *(Decimal*)field;
//                    case TypeCode.Double:
//                        return *(Double*)field;
//                    case TypeCode.Empty:
//                        return null;
//                    case TypeCode.Int16:
//                        return *(Int16*)field;
//                    case TypeCode.Int32:
//                        return *(Int32*)field;
//                    case TypeCode.Int64:
//                        return *(Int64*)field;
//                    case TypeCode.Object:
//                        return ReadStruct(field, this.size, this.typeHead);
//                    case TypeCode.SByte:
//                        return *(SByte*)field;
//                    case TypeCode.Single:
//                        return *(Single*)field;
//                    case TypeCode.String:
//                        return ReadObject(field);
//                    case TypeCode.UInt16:
//                        return *(UInt16*)field;
//                    case TypeCode.UInt32:
//                        return *(UInt32*)field;
//                    case TypeCode.UInt64:
//                        return *(UInt64*)field;
//                    default:
//                        return ReadStruct(field, this.size, this.typeHead);
//                }
//            }
//            else
//            {

//                return ReadObject(field);
//            }

//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public unsafe void SetValue(void* v, object value)
//        {
//            void* field = (void*)((IntPtr)v + this.offset);

//            if (this.isValueType)
//            {
//                switch (this.typeCode)
//                {
//                    case TypeCode.Boolean:
//                        *(bool*)field = (bool)value;
//                        break;
//                    case TypeCode.Byte:
//                        *(Byte*)field = (Byte)value;
//                        break;
//                    case TypeCode.Char:
//                        *(Char*)field = (Char)value;
//                        break;
//                    case TypeCode.DateTime:
//                        *(DateTime*)field = (DateTime)value;
//                        break;
//                    case TypeCode.DBNull:
//                        GeneralTool.SetObject(field, value);
//                        break;
//                    case TypeCode.Decimal:
//                        *(Decimal*)field = (Decimal)value;
//                        break;
//                    case TypeCode.Double:
//                        *(Double*)field = (Double)value;
//                        break;
//                    case TypeCode.Empty:
//                        break;
//                    case TypeCode.Int16:
//                        *(Int16*)field = (Int16)value;
//                        break;
//                    case TypeCode.Int32:
//                        *(Int32*)field = (Int32)value;
//                        break;
//                    case TypeCode.Int64:
//                        *(Int64*)field = (Int64)value;
//                        break;
//                    case TypeCode.Object:
//#if ENABLE_MONO
//                        var tf = __makeref(value);
//                        SetStruct(field, (void*)(*(IntPtr*)*((IntPtr*)&tf + 1)), this.size);
//#else
//                        SetStruct(field, UnsafeUtility.PinGCObjectAndGetAddress(value, out var gcHandle), this.size);
//                        UnsafeUtility.ReleaseGCObject(gcHandle);
//#endif
//                        break;
//                    case TypeCode.SByte:
//                        *(SByte*)field = (SByte)value;
//                        break;
//                    case TypeCode.Single:
//                        *(Single*)field = (Single)value;
//                        break;
//                    case TypeCode.String:
//                        GeneralTool.SetObject(field, value);
//                        break;
//                    case TypeCode.UInt16:
//                        *(UInt16*)field = (UInt16)value;
//                        break;
//                    case TypeCode.UInt32:
//                        *(UInt32*)field = (UInt32)value;
//                        break;
//                    case TypeCode.UInt64:
//                        *(UInt64*)field = (UInt64)value;
//                        break;
//                    default:
//#if ENABLE_MONO
//                        tf = __makeref(value);
//                        SetStruct(field, (void*)(*(IntPtr*)*((IntPtr*)&tf + 1)), this.size);
//#else
//                        SetStruct(field, UnsafeUtility.PinGCObjectAndGetAddress(value, out gcHandle), this.size);
//                        UnsafeUtility.ReleaseGCObject(gcHandle);
//#endif
//                        break;
//                }
//            }
//            else
//            {
//                //TypedReference tf = __makeref(source);
//                //__refvalue(tf, object) = value;
//                GeneralTool.SetObject(field, value);
//            }
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public object GetValue(void* v)
//        {
//            void* field = (void*)((IntPtr)v + this.offset);

//            if (this.isValueType)
//            {
//                switch (this.typeCode)
//                {
//                    case TypeCode.Boolean:
//                        return *(bool*)field;
//                    case TypeCode.Byte:
//                        return *(Byte*)field;
//                    case TypeCode.Char:
//                        return *(Char*)field;
//                    case TypeCode.DateTime:
//                        return *(DateTime*)field;
//                    case TypeCode.DBNull:
//                        return ReadObject(field);
//                    case TypeCode.Decimal:
//                        return *(Decimal*)field;
//                    case TypeCode.Double:
//                        return *(Double*)field;
//                    case TypeCode.Empty:
//                        return null;
//                    case TypeCode.Int16:
//                        return *(Int16*)field;
//                    case TypeCode.Int32:
//                        return *(Int32*)field;
//                    case TypeCode.Int64:
//                        return *(Int64*)field;
//                    case TypeCode.Object:
//                        return ReadStruct(field, this.size, this.typeHead);
//                    case TypeCode.SByte:
//                        return *(SByte*)field;
//                    case TypeCode.Single:
//                        return *(Single*)field;
//                    case TypeCode.String:
//                        return ReadObject(field);
//                    case TypeCode.UInt16:
//                        return *(UInt16*)field;
//                    case TypeCode.UInt32:
//                        return *(UInt32*)field;
//                    case TypeCode.UInt64:
//                        return *(UInt64*)field;
//                    default:
//                        return ReadStruct(field, this.size, this.typeHead);
//                }
//            }
//            else
//            {

//                return ReadObject(field);
//            }

//        }





//        /// <summary>
//        ///  class struct
//        /// </summary>
//        public bool isValueType = false;

//        public bool isArray = false;
//        public bool isEnum = false;

//        public FieldInfo fieldInfo;
//        public Int_Ptr typeHead;
//        public Type fieldType;
//        public int offset;
//        public int size;
//        public TypeCode typeCode;
//        public TypeAddrReflectionWarp warp;
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    private static object GetObject(int size)
//    {
//        size /= PTR_COUNT;
//        switch (size)
//        {
//            case 4:
//                return new Size32();
//            case 5:
//                return new Size40();
//            case 6:
//                return new Size48();
//            case 7:
//                return new Size56();
//            case 8:
//                return new Size64();
//            case 9:
//                return new Size72();
//            case 10:
//                return new Size80();
//            case 11:
//                return new Size88();
//            case 12:
//                return new Size96();
//            case 13:
//                return new Size104();
//            case 14:
//                return new Size112();
//            case 15:
//                return new Size120();
//            case 16:
//                return new Size128();
//            default:
//                return new IntPtr[size - 1];
//        }
//    }



//    [StructLayout(LayoutKind.Explicit)]
//    private class Size32
//    {
//        [FieldOffset(0)]
//        long start;

//        [FieldOffset(24)]
//        long end;
//    }

//    [StructLayout(LayoutKind.Explicit)]
//    private class Size40
//    {
//        [FieldOffset(0)]
//        long start;

//        [FieldOffset(32)]
//        long end;
//    }

//    [StructLayout(LayoutKind.Explicit)]
//    private class Size48
//    {
//        [FieldOffset(0)]
//        long start;

//        [FieldOffset(40)]
//        long end;
//    }

//    [StructLayout(LayoutKind.Explicit)]
//    private class Size56
//    {
//        [FieldOffset(0)]
//        long start;

//        [FieldOffset(48)]
//        long end;
//    }

//    [StructLayout(LayoutKind.Explicit)]
//    private class Size64
//    {
//        [FieldOffset(0)]
//        long start;

//        [FieldOffset(56)]
//        long end;
//    }

//    [StructLayout(LayoutKind.Explicit)]
//    private class Size72
//    {
//        [FieldOffset(0)]
//        long start;

//        [FieldOffset(64)]
//        long end;
//    }

//    [StructLayout(LayoutKind.Explicit)]
//    private class Size80
//    {
//        [FieldOffset(0)]
//        long start;

//        [FieldOffset(72)]
//        long end;
//    }

//    [StructLayout(LayoutKind.Explicit)]
//    private class Size88
//    {
//        [FieldOffset(0)]
//        long start;

//        [FieldOffset(80)]
//        long end;
//    }

//    [StructLayout(LayoutKind.Explicit)]
//    private class Size96
//    {
//        [FieldOffset(0)]
//        long start;

//        [FieldOffset(88)]
//        long end;
//    }

//    [StructLayout(LayoutKind.Explicit)]
//    private class Size104
//    {
//        [FieldOffset(0)]
//        long start;

//        [FieldOffset(96)]
//        long end;
//    }

//    [StructLayout(LayoutKind.Explicit)]
//    private class Size112
//    {
//        [FieldOffset(0)]
//        long start;

//        [FieldOffset(104)]
//        long end;
//    }

//    [StructLayout(LayoutKind.Explicit)]
//    private class Size120
//    {
//        [FieldOffset(0)]
//        long start;

//        [FieldOffset(112)]
//        long end;
//    }

//    [StructLayout(LayoutKind.Explicit)]
//    private class Size128
//    {
//        [FieldOffset(0)]
//        long start;

//        [FieldOffset(120)]
//        long end;
//    }



//}