//using System.Runtime.InteropServices;
//using System;
//using System.Reflection;
//using System.Reflection.Emit;
//using UnityEngine;

//using MonoImage = System.UIntPtr;
//using MonoClass = System.UIntPtr;
//using MonoMethod = System.UIntPtr;
//using MonoVTable = System.UIntPtr;
//using MonoObject = System.UIntPtr;
//using UIntPtr = System.UIntPtr;
//using MonoDomain = System.UIntPtr;
//using MonoError = System.UIntPtr;
//using Type = System.Type;

//using guint8 = System.Byte;
//using gint16 = System.Int16;
//using guint16 = System.UInt16;
//using guint = System.UInt32;

//namespace MyNamespace
//{
//    public class ACCC
//    {
//        public long a = 123;
//    }
//}
//public static class UnityCombineMeshesHack
//{
//    [StructLayout(LayoutKind.Sequential)]
//    struct MonoArray
//    {
//        public UIntPtr vtable;
//        public UIntPtr synchronisation;
//        public UIntPtr bounds;
//        public UIntPtr max_length;
//        public UIntPtr vector;
//    }


//    [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
//    static extern UIntPtr mono_lookup_internal_call(MonoMethod monoMethod);

//    [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
//    static extern MonoImage mono_image_loaded(string name);

//    [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
//    static extern MonoClass mono_class_from_name(MonoImage image, string name_space, string name);

//    [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
//    static extern MonoMethod mono_class_get_method_from_name(MonoClass klass, string name, int param_count);

//    [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
//    static extern MonoVTable mono_class_vtable(MonoDomain domain, MonoClass klass);

//    [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
//    static extern MonoDomain mono_domain_get();

//    [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
//    static extern MonoDomain mono_get_root_domain();
    
//    [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
//    static extern MonoObject mono_object_new_alloc_specific(MonoVTable vtable);
//    //[DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
//    //static extern MonoDomain mono_add_internal_call();

//    [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
//    static extern MonoObject mono_object_new_alloc_specific(MonoDomain vtabledomain, MonoClass klass);

//    [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
//    public static extern MonoObject mono_object_new(MonoDomain domain, MonoClass klass);

//    [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
//    static extern MonoObject create_object(MonoDomain domain, MonoImage image);

//    [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
//    static extern void mono_runtime_object_init(MonoObject obj);
    

//    private delegate void CombineMeshesICallDelegate(UIntPtr mesh, UIntPtr combineArray, bool mergeSubMeshes, bool useMatrices, bool hasLightmapData);

//    private delegate MonoObject GetMonoObjectDelegate(object obj);

//    private static GetMonoObjectDelegate s_GetMonoObject = null;

//    private static CombineMeshesICallDelegate s_CombineMeshesImpl = null;

//    private static UIntPtr GetNativeAddrOfCombineMeshesImpl()
//    {
//        string assemblyName = typeof(Mesh).Assembly.Location;

//        MonoImage monoImage = mono_image_loaded(assemblyName);
//        if (monoImage == MonoImage.Zero)
//            return MonoImage.Zero;

//        MonoClass meshClass = mono_class_from_name(monoImage, "UnityEngine", "Mesh");
//        if (meshClass == MonoClass.Zero)
//            return MonoImage.Zero;

//        MonoMethod combineMeshesMethod = mono_class_get_method_from_name(meshClass, "CombineMeshesImpl", 4);
//        if (combineMeshesMethod == MonoClass.Zero)
//            return MonoImage.Zero;

//        return mono_lookup_internal_call(combineMeshesMethod);
//    }

//    public static UIntPtr GetNativeAddrOfCombineMeshesImpl2(ref MonoImage meshClass, ref MonoImage monoDomain)
//    {
//        string assemblyName = typeof(MyNamespace.ACCC).Assembly.Location;

//        MonoImage monoImage = mono_image_loaded(assemblyName);
//        if (monoImage == MonoImage.Zero)
//            return MonoImage.Zero;

//        meshClass = mono_class_from_name(monoImage, "MyNamespace", "ACCC");
//        if (meshClass == MonoClass.Zero)
//            return MonoClass.Zero;

//        monoDomain = mono_get_root_domain();
//        if (monoDomain == MonoClass.Zero)
//            return MonoClass.Zero;



//        MonoVTable monoVTable = mono_class_vtable(monoDomain, meshClass);
//        if (monoVTable == MonoVTable.Zero)
//            return MonoVTable.Zero;

//        MonoObject monoObject = mono_object_new(monoDomain, meshClass);

//        //MonoObject monoObject = mono_object_new_alloc_specific(monoVTable);
//        //mono_runtime_object_init(monoObject);
//        return monoObject;
//        //return monoObject;
//        //MonoMethod combineMeshesMethod = mono_class_get_method_from_name(meshClass, "CombineMeshesImpl", 4);
//        //if (combineMeshesMethod == MonoClass.Zero)
//        //    return MonoImage.Zero; gc_descr;

//        //return mono_lookup_internal_call(combineMeshesMethod);
//    }


//    public static UIntPtr GetNativeAddrOfCombineMeshesImpl3()
//    {
//        string assemblyName = typeof(MyNamespace.ACCC).Assembly.Location;

//        MonoImage monoImage = mono_image_loaded(assemblyName);
//        if (monoImage == MonoImage.Zero)
//            return MonoImage.Zero;

//        MonoClass meshClass = mono_class_from_name(monoImage, "MyNamespace", "ACCC");
//        if (meshClass == MonoClass.Zero)
//            return MonoClass.Zero;

//        MonoDomain monoDomain = mono_get_root_domain();
//        if (monoDomain == MonoClass.Zero)
//            return MonoClass.Zero;

//        MonoVTable monoVTable = mono_class_vtable(monoDomain, meshClass);
//        if (monoVTable == MonoVTable.Zero)
//            return MonoVTable.Zero;

//        MonoObject monoObject = mono_object_new(monoDomain, meshClass);
//        return monoObject;
//        //MonoMethod combineMeshesMethod = mono_class_get_method_from_name(meshClass, "CombineMeshesImpl", 4);
//        //if (combineMeshesMethod == MonoClass.Zero)
//        //    return MonoImage.Zero; gc_descr;

//        //return mono_lookup_internal_call(combineMeshesMethod);
//    }



//    private static GetMonoObjectDelegate EmitGetMonoObjectFunction()
//    {
//        Type[] methodArgs = { typeof(object) };
//        var uintPtrConstructor = typeof(UIntPtr).GetConstructor(new Type[] { typeof(void*) });

//        DynamicMethod getMonoObject = new DynamicMethod("GetMonoObjectImpl", typeof(MonoObject), methodArgs, typeof(UnityCombineMeshesHack).Module);
//        var il = getMonoObject.GetILGenerator();

//        var localRetVal = il.DeclareLocal(typeof(UIntPtr));
//        il.Emit(OpCodes.Ldloca, localRetVal);
//        il.Emit(OpCodes.Ldarg, 0);
//        il.Emit(OpCodes.Call, uintPtrConstructor);
//        il.Emit(OpCodes.Ldloc, localRetVal);
//        il.Emit(OpCodes.Ret);

//        return (GetMonoObjectDelegate)getMonoObject.CreateDelegate(typeof(GetMonoObjectDelegate));
//    }

//    private static MonoObject GetMonoObject(object obj)
//    {
//        if (s_GetMonoObject == null)
//        {
//            s_GetMonoObject = EmitGetMonoObjectFunction();
//        }

//        return s_GetMonoObject(obj);
//    }

//    private static CombineMeshesICallDelegate EmitCombineMeshICallWrapper()
//    {
//        AssemblyName assemblyName = new AssemblyName();
//        assemblyName.Name = "MeshHackDynamicAssembly";

//        var assemblyBuilder = System.AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
//        var moduleBuilder = assemblyBuilder.DefineDynamicModule(assemblyName.Name);

//        Type[] methodArgs = { typeof(UIntPtr), typeof(UIntPtr), typeof(bool), typeof(bool), typeof(bool) };

//        DynamicMethod combineMeshICallWrapper = new DynamicMethod("CombineMeshICallWrapper", typeof(void), methodArgs, moduleBuilder);

//        var il = combineMeshICallWrapper.GetILGenerator();

//        il.Emit(OpCodes.Ldarg_0);
//        il.Emit(OpCodes.Ldarg_1);
//        il.Emit(OpCodes.Ldarg_2);
//        il.Emit(OpCodes.Ldarg_3);
//        il.Emit(OpCodes.Ldarg_S, 4);
//        il.Emit(OpCodes.Ldc_I8, (long)GetNativeAddrOfCombineMeshesImpl());
//        il.EmitCalli(OpCodes.Calli, CallingConvention.Cdecl, typeof(void), methodArgs);
//        il.Emit(OpCodes.Ret);

//        return (CombineMeshesICallDelegate)combineMeshICallWrapper.CreateDelegate(typeof(CombineMeshesICallDelegate));
//    }

//    public static unsafe void CombineMeshesUnsafe(this Mesh mesh, CombineInstance* combine, int combineNum, bool mergeSubMeshes = true, bool useMatrices = true, bool hasLightmapData = false)
//    {
//        if (s_CombineMeshesImpl == null)
//        {
//            s_CombineMeshesImpl = EmitCombineMeshICallWrapper();
//            Debug.Assert(s_CombineMeshesImpl != null);
//        }

//        unsafe
//        {
//            byte* buffer = stackalloc byte[sizeof(MonoArray) + sizeof(CombineInstance) * combineNum];

//            ((MonoArray*)buffer)[0].vtable = UIntPtr.Zero;
//            ((MonoArray*)buffer)[0].synchronisation = UIntPtr.Zero;
//            ((MonoArray*)buffer)[0].bounds = UIntPtr.Zero;
//            ((MonoArray*)buffer)[0].max_length = new UIntPtr((uint)combineNum);

//            Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(buffer + sizeof(UIntPtr) * 4, combine, sizeof(CombineInstance) * combineNum);

//            s_CombineMeshesImpl(GetMonoObject(mesh), (UIntPtr)buffer, mergeSubMeshes, useMatrices, hasLightmapData);
//        }
//    }
//}