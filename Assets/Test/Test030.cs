using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;
using Unity.Collections.LowLevel.Unsafe;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using PtrReflection;

public class Test030 : MonoBehaviour
{
    public Text text;
    public InputField input;


    public int testCount = 200000;
    System.Diagnostics.Stopwatch oTime = new System.Diagnostics.Stopwatch();

    StringBuilder sb = new StringBuilder();
    void DebugLog(object obj)
    {
        string txt = obj.ToString();
#if UNITY_EDITOR
        Debug.Log(txt);
#endif
        sb.AppendLine(txt);
    }

    private unsafe Vector3 GCC(void* a)
    {
        return *((Vector3*)new IntPtr(a));
    }

    private unsafe void Start()
    {
        //DynamicCreateType();
        return;

        //UIntPtr meshClass = new UIntPtr();
        //UIntPtr monoDomai = new UIntPtr();
        //UnityCombineMeshesHack.GetNativeAddrOfCombineMeshesImpl2(ref meshClass, ref monoDomai);

        Vector3 aaa = new Vector3(2893,333,55555);

        ulong gcHandle;
        void* v = UnsafeUtility.PinGCObjectAndGetAddress(aaa, out gcHandle);
        UnsafeUtility.ReleaseGCObject(gcHandle);
        Debug.Log((long)v);

        long* vv = (long*)&v;
        Debug.Log((long)vv);
        Debug.Log(*((float*)v + 5));
        Debug.Log(*((Vector3*)v ));


        TypedReference tf2 = __makeref(v);
        Vector3* ptr2 = (Vector3*)(*((IntPtr*)&tf2 + 1));
        Debug.Log(*ptr2);

        void* dz = (void*)&v;
        IntPtr zz = new IntPtr(dz);
        Debug.Log(*(Vector3*)v);

        Vector3 aaa2 = *(Vector3*)vv;
        Vector3 aaa3 = GCC(v);


        TypedReference tf = __makeref(aaa);
        Vector3* ptr = (Vector3*)(*((IntPtr*)&tf + 1));


        Debug.Log(*ptr); 

        Debug.Log(aaa);
        Debug.Log(aaa2);
        Debug.Log(aaa3);
        return;

        if (!text)
        {
            text = GameObject.FindObjectOfType<Text>();
        }
        if (!input)
        {
            input = GameObject.FindObjectOfType<InputField>();
        }
    }


    public unsafe void RunTest2()
    {
        testCount = int.Parse(input.text);
        sb = new StringBuilder();
        //MyClass myClass = new MyClass();

        string fieldName = "One";

        ulong gcHandle;
        byte* bytePtr;
        /*
        MyClass2 ccc = new MyClass2();
        //ccc.str = 44444;
        //ccc.One = 4324;
        //ccc.point = new Vector3(-99.56f, 50.22f, 9f);
        //ccc.point4 = new Vector3Int(2, 3, 4);
        //ccc.dd = 11;

        MyClass instens2 = new MyClass();
        MyClass instens3 = new MyClass();
        MyClass instens4 = new MyClass();

        Debug.Log(new TypeAddrReflectionWrapper(typeof(MyClass2)).heapSize);
        //Debug.Log(UnsafeUtility.SizeOf(typeof(MyClass2)));

        long* longPtr = (long*)UnsafeUtility.PinGCObjectAndGetAddress(ccc, out gcHandle);
        IntPtr**** p1 = (IntPtr****)longPtr;
        IntPtr* p2 = ***p1;
        p2 += 3;
        int* intPtrIdepth = (int*)p2;
        ++intPtrIdepth;
        int instanceSize = *intPtrIdepth;

        //MonoVTable
        //MonoClass
        long* MonoVTable = (long*)*(long*)(*longPtr);
        long* MonoClass = (long*)*(long*)(*MonoVTable);
        long element_class = *MonoClass;
        long cast_class = *(MonoClass + 1);
        long supertypes = *(MonoClass + 2);
        byte* kk = (byte*)(MonoClass + 3);
        UInt16 idepth = *(UInt16*)(kk);
        kk += 2;

        byte rank = *(byte*)(kk);
        kk += 2;

        int instance_size = *(int*)(kk);

        long* _MonoType = *(long**)typeof(MyClass2).TypeHandle.Value;
        long* klass = (long*)*(long*)(*_MonoType);

        Debug.Log("MonoClass : " + (long)(MonoClass));
        Debug.Log("klass : " + (long)(klass));

        Debug.Log("*element_class : " + (element_class));
        Debug.Log("*cast_class : " + (cast_class));
        Debug.Log("**supertypes : " + (supertypes));
        Debug.Log("idepth : " + (idepth));
        Debug.Log("rank : " + (rank));
        Debug.Log("instance_size : " + (instance_size));

        int instance_size2 =
        UnsafeOperation.HeapSizeOf(UnsafeOperation.GetTypeHead(typeof(MyClass2)));
        
        Debug.Log("instance_size2 : " + (instance_size2));

        for (int i = 0; i < 20; i++)
        {
            Debug.Log(i + "," + (*longPtr));
            ++longPtr;
        }
        return;
        */

        var warp = new TypeAddrReflectionWrapper(typeof(MyClass));

        //bytePtr = (byte*)UnsafeUtility.PinGCObjectAndGetAddress(ccc, out gcHandle);
        var instens = (MyClass)warp.Create(out gcHandle, out bytePtr);
        DebugLog("创建 " + instens.GetType());
        instens.str = "S3ED";
        instens.one = 1;
        instens.point = new Vector3(3, -4.5f, 97.4f);

        fieldName = "one";
        fixed (char* fieldNamePtr = fieldName)
        {
            TypeAddrFieldAndProperty addr = warp.Find(fieldNamePtr, fieldName.Length);
            int value = *(int*)(bytePtr + addr.offset);
            DebugLog("取值后 输出 1 : " + value);

            *(int*)(bytePtr + addr.offset) = 18;
            DebugLog("赋值后 输出 18 : " + instens.one);

            object obj = addr.GetValue(bytePtr);
            DebugLog("不指定类型取值后 输出 18 : " + obj);

            addr.SetValue(bytePtr, 444);
            DebugLog("不指定类型赋值后 输出 444 : " + instens.one);
        }

        fieldName = "str";
        fixed (char* fieldNamePtr = fieldName)
        {
            TypeAddrFieldAndProperty addr = warp.Find(fieldNamePtr, fieldName.Length);

            string value = UnsafeUtility.ReadArrayElement<string>(bytePtr + addr.offset, 0);
            DebugLog("取值后 输出 S3ED : " + value);

            UnsafeUtility.CopyObjectAddressToPtr("Acfv", bytePtr + addr.offset);
            DebugLog("赋值后 输出 Acfv : " + instens.str);

            object obj = addr.GetValue(bytePtr);
            DebugLog("不指定类型取值后 输出 Acfv : " + obj);

            addr.SetValue(bytePtr, "DDcc#2$%@");
            DebugLog("不指定类型赋值后 输出 DDcc#2$%@ : " + instens.str);
        }

        fieldName = "point";
        fixed (char* fieldNamePtr = fieldName)
        {
            TypeAddrFieldAndProperty addr = warp.Find(fieldNamePtr, fieldName.Length);

            Vector3 value = UnsafeUtility.ReadArrayElement<Vector3>(bytePtr + addr.offset, 0);
            DebugLog("取值后 输出 3, -4.5, 97.4 : " + value);

            var pt = new Vector3(-99.56f, 50.22f, 9f);
            UnsafeUtility.MemCpy(bytePtr + addr.offset, UnsafeUtility.AddressOf(ref pt), addr.stackSize);
            DebugLog("赋值后 输出 -99.56, 50.22, 9 : " + instens.point);

            object obj = addr.GetValue(bytePtr);
            DebugLog("不指定类型取值后 输出 -99.56, 50.22, 9 : " + obj);

            addr.SetValue(bytePtr, new Vector3(0, -9999f, 12.888f));
            DebugLog("不指定类型赋值后 输出 0, -9999 , 12.888 : " + instens.point);
        }

        DebugLog(" ");
        DebugLog("====↓↓↓↓ 属性 ↓↓↓↓===== ");
        DebugLog(" ");

        instens.Str = "S3ED";
        instens.One = 1;
        instens.Point = new Vector3(3, -4.5f, 97.4f);

        fieldName = "One";
        fixed (char* fieldNamePtr = fieldName)
        {
            TypeAddrFieldAndProperty addr = warp.Find(fieldNamePtr, fieldName.Length);

            int k  = addr.propertyDelegateItem.getInt32(bytePtr);
            DebugLog("取值后 输出 1 : " + k);

            addr.propertyDelegateItem.setInt32(bytePtr, 18);
            DebugLog("赋值后 输出 18 : " + instens.One);

            object obj = addr.GetValue(bytePtr);
            DebugLog("不指定类型取值后 输出 18 : " + obj);

            addr.SetValue(bytePtr, 444);
            DebugLog("不指定类型赋值后 输出 444 : " + instens.One);
        }



        fieldName = "Str";
        fixed (char* fieldNamePtr = fieldName)
        {
            TypeAddrFieldAndProperty addr = warp.Find(fieldNamePtr, fieldName.Length);

            string value = addr.propertyDelegateItem.getString(bytePtr);
            DebugLog("取值后 输出 S3ED : " + value);

            addr.propertyDelegateItem.setString(bytePtr, "ADc34gC@#");
            DebugLog("赋值后 输出 ADc34gC@# : " + instens.Str);

            object obj = addr.GetValue(bytePtr);
            DebugLog("不指定类型取值后 输出 ADc34gC@# : " + obj);

            addr.SetValue(bytePtr, "DDcc#2$%@ ");
            DebugLog("不指定类型赋值后 输出 DDcc#2$%@ : " + instens.Str);
        }



        fieldName = "Point";
        fixed (char* fieldNamePtr = fieldName)
        {
            TypeAddrFieldAndProperty addr = warp.Find(fieldNamePtr, fieldName.Length);

            Vector3 value = (Vector3)addr.propertyDelegateItem.getObject(bytePtr);
            DebugLog("取值后 输出 3, -4.5, 97.4 : " + value);

            var pt = new Vector3(-99.56f, 50.22f, 9f);
            addr.propertyDelegateItem.setObject(bytePtr, pt);
            DebugLog("赋值后 输出 -99.56, 50.22, 9 : " + instens.Str);

            object obj = addr.GetValue(bytePtr);
            DebugLog("不指定类型取值后 输出 -99.56, 50.22, 9 : " + obj);

            addr.SetValue(bytePtr, new Vector3(0, -9999f, 12.888f));
            DebugLog("不指定类型赋值后 输出 0, -9999 , 12.888 : " + instens.point);
        }



        text.text = sb.ToString();

        UnsafeUtility.ReleaseGCObject(gcHandle);

        GC.Collect();
        return;
    }


    string v1;
    int v2;
    Vector3 v3;
    MyClass a;
    MyClass myClass;
    public unsafe void RunTest()
    {
        testCount = int.Parse(input.text);
        sb = new StringBuilder();
        var warp = new TypeAddrReflectionWrapper(typeof(MyClass));

        string fieldName = "str";
        int nameSize = fieldName.Length;

        ulong gcHandle;
        byte* bytePtr;
        myClass = (MyClass)warp.Create(out gcHandle, out bytePtr);


        string str = "hello world";
        Vector3 point = new Vector3(99.2f, -98.2f, 3.2f);
        DebugLog("");
        DebugLog("循环" + testCount + "次");
        DebugLog("====================================");
        DebugLog("");

        //fieldName = "one";
        //nameSize = fieldName.Length;

        var addr1 = warp.nameOfField[nameof(MyClass.one)];
        var addr2 = warp.nameOfField[nameof(MyClass.str)];
        var addr3 = warp.nameOfField[nameof(MyClass.point)];

        oTime.Reset(); oTime.Start();
        for (int i = 0; i < testCount; i++)
        {
            typeof(MyClass).GetField(nameof(MyClass.one)).SetValue(myClass, 18);
            typeof(MyClass).GetField(nameof(MyClass.str)).SetValue(myClass, str);
        }
        oTime.Stop();
        DebugLog("FieldInfo SetValue：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");


        oTime.Reset(); oTime.Start();
        for (int i = 0; i < testCount; i++)
        {
            warp.nameOfField[nameof(MyClass.one)].SetValue(bytePtr, 18);
            warp.nameOfField[nameof(MyClass.str)].SetValue(bytePtr, str);
            warp.nameOfField[nameof(MyClass.point)].SetValue(bytePtr, point);
        }
        oTime.Stop();
        DebugLog("ReflectionManager SetValue：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");


        oTime.Reset(); oTime.Start();
        for (int i = 0; i < testCount; i++)
        {
            UnsafeUtility.CopyObjectAddressToPtr(str, bytePtr + warp.nameOfField[nameof(MyClass.str)].offset);
            *(int*)(bytePtr + warp.nameOfField[nameof(MyClass.one)].offset) = 18;
            var addr = warp.nameOfField[nameof(MyClass.one)];
            UnsafeUtility.MemCpy(bytePtr + addr.offset, UnsafeUtility.AddressOf(ref point), addr.stackSize);
        }
        oTime.Stop();
        DebugLog("ReflectionManager SetValue 确定类型的：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");

        oTime.Reset(); oTime.Start();
        for (int i = 0; i < testCount; i++)
        {
            UnsafeUtility.CopyObjectAddressToPtr(str, bytePtr + addr1.offset);
            *(int*)(bytePtr + addr2.offset) = 18;
            UnsafeUtility.MemCpy(bytePtr + addr3.offset, UnsafeUtility.AddressOf(ref point), addr3.stackSize);
        }
        oTime.Stop();
        DebugLog("ReflectionManager SetValue 忽略字符串查询：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");



        oTime.Reset(); oTime.Start();
        for (int i = 0; i < testCount; i++)
        {
            myClass.one = 18;
            myClass.str = str;
            myClass.point = point;
        }
        oTime.Stop();
        DebugLog("= ：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");

        DebugLog("");
        DebugLog("====================================");
        DebugLog("");


        oTime.Reset(); oTime.Start();
        for (int i = 0; i < testCount; i++)
        {
            v1 = (string)typeof(MyClass).GetField(nameof(MyClass.str)).GetValue(myClass);
            v2 = (int)typeof(MyClass).GetField(nameof(MyClass.one)).GetValue(myClass);
            v3 = (Vector3)typeof(MyClass).GetField(nameof(MyClass.point)).GetValue(myClass);
        }
        oTime.Stop();
        DebugLog("FieldInfo GetValue：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");


        oTime.Reset(); oTime.Start();
        for (int i = 0; i < testCount; i++)
        {
            v1 = (string)warp.nameOfField[nameof(MyClass.str)].GetValue(bytePtr);
            v2 = (int)warp.nameOfField[nameof(MyClass.one)].GetValue(bytePtr);
            v3 = (Vector3)warp.nameOfField[nameof(MyClass.point)].GetValue(bytePtr);
        }
        oTime.Stop();
        DebugLog("ReflectionManager GetValue：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");



        oTime.Reset(); oTime.Start();
        for (int i = 0; i < testCount; i++)
        {
            v1 = UnsafeUtility.ReadArrayElement<string>(bytePtr + warp.nameOfField[nameof(MyClass.str)].offset, 0);
            v2 = *(int*)(bytePtr + warp.nameOfField[nameof(MyClass.one)].offset);
            v3 = UnsafeUtility.ReadArrayElement<Vector3>(bytePtr + warp.nameOfField[nameof(MyClass.point)].offset, 0);
        }
        oTime.Stop();
        DebugLog("ReflectionManager GetValue 确定类型的：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");


        oTime.Reset(); oTime.Start();
        for (int i = 0; i < testCount; i++)
        {
            v1 = UnsafeUtility.ReadArrayElement<string>(bytePtr + addr2.offset, 0);
            v2 = *(int*)(bytePtr + addr1.offset);
            v3 = UnsafeUtility.ReadArrayElement<Vector3>(bytePtr + addr3.offset, 0);
        }
        oTime.Stop();
        DebugLog("ReflectionManager GetValue 忽略字符串查询：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");



        oTime.Reset(); oTime.Start();
        for (int i = 0; i < testCount; i++)
        {
            v1 = myClass.str;
            v2 = myClass.one;
            v3 = myClass.point;
        }
        oTime.Stop();
        DebugLog("= ：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");



        DebugLog("");
        DebugLog("====================================");
        DebugLog("");

        oTime.Reset(); oTime.Start();
        for (int i = 0; i < testCount; i++)
        {
            a = (MyClass)System.Activator.CreateInstance(typeof(MyClass));
        }
        oTime.Stop();
        DebugLog("Activator CreateInstance：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");


        oTime.Reset(); oTime.Start();
        for (int i = 0; i < testCount; i++)
        {
            a = (MyClass)warp.Create();
        }
        oTime.Stop();
        DebugLog("ReflectionManager Create：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");


        oTime.Reset(); oTime.Start();
        for (int i = 0; i < testCount; i++)
        {
            a = new MyClass();
        }
        oTime.Stop();
        DebugLog(" new ：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");

        text.text = sb.ToString();

        UnsafeUtility.ReleaseGCObject(gcHandle);
        GC.Collect();
    }



    unsafe void Test(object obj)
    {
        UInt64 gcHandle;
        int* ptr = (int*)UnsafeUtility.PinGCObjectAndGetAddress(obj, out gcHandle);
        List<int> vs = new List<int>();
        for (int i = 0; i < 8; i++)
        {
            vs.Add(*(ptr + i));
            DebugLog(*(ptr + i));
        }
    }



    void Update()
    {

    }
}
