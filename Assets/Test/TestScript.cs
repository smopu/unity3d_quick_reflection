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

public class TestScript : MonoBehaviour
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
    }


    public unsafe void RunTest2()
    {
        testCount = int.Parse(input.text);
        sb = new StringBuilder();
        //MyClass myClass = new MyClass();

        string fieldName = "One";

        ulong gcHandle;
        byte* bytePtr;

        var warp = TypeAddrReflectionWrapper.GetWrapper(typeof(MyClass));

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
            UnsafeUtility.WriteArrayElement<Vector3>(bytePtr + addr.offset, 0, pt);
            //下面一行也可以
            //UnsafeUtility.MemCpy(bytePtr + addr.offset, UnsafeUtility.AddressOf(ref pt), addr.stackSize);


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

        instens.ones = new int[] { 1, 2, 8, 476, 898, 9 };
        instens.strs = new string[] {"ass","#$%^&","*SAHASww&()", "兀驦屮鲵傌" };
        instens.points = new Vector3[] {
            new Vector3(3, -4.5f, 97.4f),
            new Vector3(9999f, -43f, 0.019f),
            new Vector3(55.3f, -0.01f, -130),
        };

        fieldName = "ones";
        fixed (char* fieldNamePtr = fieldName)
        {
            TypeAddrFieldAndProperty addr = warp.Find(fieldNamePtr, fieldName.Length);
            var arrayWrap = ArrayWrapManager.GetIArrayWrap(addr.fieldOrPropertyType);

            Array array = (Array)addr.GetValue(bytePtr);
            ArrayWrapOutData arrayWrapOutData = arrayWrap.GetArrayData(array);

            DebugLog("取值后 输出 1, 2, 8, 476, 898, 9  : " );
            for (int i = 0; i < arrayWrapOutData.length; i++)
            {
                int value = *(int*)(arrayWrapOutData.startItemOffcet + arrayWrap.elementTypeSize * i);
                DebugLog(value);
            }

            for (int i = 0; i < arrayWrapOutData.length; i++)
            {
                *(int*)(arrayWrapOutData.startItemOffcet + arrayWrap.elementTypeSize * i) = i;
            }
            DebugLog("赋值后 输出  0, 1, 2, 3, 4, 5  : ");
            for (int i = 0; i < array.Length; i++)
            {
                DebugLog(array.GetValue(i));
            }

            object obj = addr.GetValue(bytePtr);
            DebugLog("不指定类型取值后 输出 0, 1, 2, 3, 4, 5 : ");
            for (int i = 0; i < arrayWrapOutData.length; i++)
            {
                object value = arrayWrap.GetValue(arrayWrapOutData.startItemOffcet, i);
                DebugLog(value);
            }

            for (int i = 0; i < arrayWrapOutData.length; i++)
            {
                arrayWrap.SetValue(arrayWrapOutData.startItemOffcet, i, 100 * i);
            }
            DebugLog("不指定类型赋值后 输出 0, 100, 200, 300, 400, 500 : ");
            for (int i = 0; i < array.Length; i++)
            {
                DebugLog(array.GetValue(i));
            }
        }

        fieldName = "strs";
        fixed (char* fieldNamePtr = fieldName)
        {
            TypeAddrFieldAndProperty addr = warp.Find(fieldNamePtr, fieldName.Length);
            var arrayWrap = ArrayWrapManager.GetIArrayWrap(addr.fieldOrPropertyType);

            Array array = (Array)addr.GetValue(bytePtr);
            ArrayWrapOutData arrayWrapOutData = arrayWrap.GetArrayData(array);

            DebugLog("取值后 输出 ass,#$%^&,*SAHASww&(), 兀驦屮鲵傌 : ");
            for (int i = 0; i < arrayWrapOutData.length; i++)
            {
                string value = UnsafeUtility.ReadArrayElement<string>(arrayWrapOutData.startItemOffcet, i);
                //下面一行也可以
                //string value = UnsafeUtility.ReadArrayElement<string>(arrayWrapOutData.startItemOffcet + arrayWrap.elementTypeSize * i, 0);
                DebugLog(value);
            }

            for (int i = 0; i < arrayWrapOutData.length; i++)
            {
                UnsafeUtility.CopyObjectAddressToPtr("Ac4……*" + i, arrayWrapOutData.startItemOffcet + arrayWrap.elementTypeSize * i);
                //下面一行也可以
                // UnsafeUtility.WriteArrayElement<string>(arrayWrapOutData.startItemOffcet, i, "Ac4……*" + i);
            }
            DebugLog("赋值后 输出  Ac4……*0, Ac4……*1, Ac4……*2, Ac4……*3  : ");
            for (int i = 0; i < array.Length; i++)
            {
                DebugLog(array.GetValue(i));
            }


            object obj = addr.GetValue(bytePtr);
            DebugLog("不指定类型取值后 输出 Ac4……*0, Ac4……*1, Ac4……*2, Ac4……*3  : ");
            for (int i = 0; i < arrayWrapOutData.length; i++)
            {
                object value = arrayWrap.GetValue(arrayWrapOutData.startItemOffcet, i);
                DebugLog(value);
            }

            for (int i = 0; i < arrayWrapOutData.length; i++)
            {
                arrayWrap.SetValue(arrayWrapOutData.startItemOffcet, i, "Fc%^" + i * 100);
            }
            DebugLog("不指定类型赋值后 输出 Fc%^0, Fc%^100, Fc%^200, Fc%^300 : ");
            for (int i = 0; i < array.Length; i++)
            {
                DebugLog(array.GetValue(i));
            }
        }

        fieldName = "points";
        fixed (char* fieldNamePtr = fieldName)
        {
            TypeAddrFieldAndProperty addr = warp.Find(fieldNamePtr, fieldName.Length);
            var arrayWrap = ArrayWrapManager.GetIArrayWrap(addr.fieldOrPropertyType);

            Array array = (Array)addr.GetValue(bytePtr);
            ArrayWrapOutData arrayWrapOutData = arrayWrap.GetArrayData(array);

            DebugLog("取值后 输出 (3, -4.5, 97.4) (9999, -43, 0.019) (55.3, -0.01, -130) : ");
            for (int i = 0; i < arrayWrapOutData.length; i++)
            {
                Vector3 value = UnsafeUtility.ReadArrayElement<Vector3>(arrayWrapOutData.startItemOffcet, i);
                DebugLog(value);
            }

            for (int i = 0; i < arrayWrapOutData.length; i++)
            {
                var v = new Vector3(i, i, i);
                UnsafeUtility.MemCpy(arrayWrapOutData.startItemOffcet + arrayWrap.elementTypeSize * i, UnsafeUtility.AddressOf(ref v), arrayWrap.elementTypeSize);
                //下面一行也可以
                //UnsafeUtility.WriteArrayElement<Vector3>(arrayWrapOutData.startItemOffcet, i, v);
            }

            DebugLog("赋值后 输出  (0,0,0) , (1,1,1) , (2,2,2)  : ");
            for (int i = 0; i < array.Length; i++)
            {
                DebugLog(array.GetValue(i));
            }


            object obj = addr.GetValue(bytePtr);
            DebugLog("不指定类型取值后 输出  (0,0,0) , (1,1,1) , (2,2,2)  : ");
            for (int i = 0; i < arrayWrapOutData.length; i++)
            {
                object value = arrayWrap.GetValue(arrayWrapOutData.startItemOffcet, i);
                DebugLog(value);
            }

            for (int i = 0; i < arrayWrapOutData.length; i++)
            {
                arrayWrap.SetValue(arrayWrapOutData.startItemOffcet, i, new Vector3(i * 100, i * 10, i * 1000));
            }
            DebugLog("不指定类型赋值后 输出  (0,0,0) , (100,10,1000) , (200,20,2000) : ");
            for (int i = 0; i < array.Length; i++)
            {
                DebugLog(array.GetValue(i));
            }

        }







        text.text = sb.ToString();

        UnsafeUtility.ReleaseGCObject(gcHandle);

        GC.Collect();
        return;
    }

    string[] v1s;
    int[] v2s;
    Vector3[] v3s;

    string v1;
    int v2;
    Vector3 v3;
    MyClass a;
    MyClass myClass;
    public unsafe void RunTest()
    {
        testCount = int.Parse(input.text);
        sb = new StringBuilder();
        var warp = TypeAddrReflectionWrapper.GetWrapper(typeof(MyClass));

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

        var addr1 = warp.nameOfField[nameof(MyClass.str)];
        var addr2 = warp.nameOfField[nameof(MyClass.one)];
        var addr3 = warp.nameOfField[nameof(MyClass.point)];

        oTime.Reset(); oTime.Start();
        for (int i = 0; i < testCount; i++)
        {
            typeof(MyClass).GetField(nameof(MyClass.str)).SetValue(myClass, str);
            typeof(MyClass).GetField(nameof(MyClass.one)).SetValue(myClass, 18);
            typeof(MyClass).GetField(nameof(MyClass.point)).SetValue(myClass, point);
        }
        oTime.Stop();
        DebugLog("FieldInfo SetValue：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");


        oTime.Reset(); oTime.Start();
        for (int i = 0; i < testCount; i++)
        {
            warp.nameOfField[nameof(MyClass.str)].SetValue(bytePtr, str);
            warp.nameOfField[nameof(MyClass.one)].SetValue(bytePtr, 18);
            warp.nameOfField[nameof(MyClass.point)].SetValue(bytePtr, point);
        }
        oTime.Stop();
        DebugLog("ReflectionManager SetValue：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");


        oTime.Reset(); oTime.Start();
        for (int i = 0; i < testCount; i++)
        {
            UnsafeUtility.CopyObjectAddressToPtr(str, bytePtr + warp.nameOfField[nameof(MyClass.str)].offset);
            *(int*)(bytePtr + warp.nameOfField[nameof(MyClass.one)].offset) = 18;
            var addr = warp.nameOfField[nameof(MyClass.point)];
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
        DebugLog("原生 ：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");

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
            v1 = UnsafeUtility.ReadArrayElement<string>(bytePtr + addr1.offset, 0);
            v2 = *(int*)(bytePtr + addr2.offset);
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
        DebugLog("原生 ：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");



        DebugLog("");
        DebugLog("=========↓↓↓Property↓↓↓=============");
        DebugLog("");

        addr1 = warp.nameOfField[nameof(MyClass.Str)];
        addr2 = warp.nameOfField[nameof(MyClass.One)];
        addr3 = warp.nameOfField[nameof(MyClass.Point)];
        oTime.Reset(); oTime.Start();
        for (int i = 0; i < testCount; i++)
        {
            typeof(MyClass).GetProperty(nameof(MyClass.Str)).SetValue(myClass, str);
            typeof(MyClass).GetProperty(nameof(MyClass.One)).SetValue(myClass, 18);
            typeof(MyClass).GetProperty(nameof(MyClass.Point)).SetValue(myClass, point);
        }
        oTime.Stop();
        DebugLog("PropertyInfo SetValue：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");

        oTime.Reset(); oTime.Start();
        for (int i = 0; i < testCount; i++)
        {
            warp.nameOfField[nameof(MyClass.Str)].SetValue(bytePtr, str);
            warp.nameOfField[nameof(MyClass.One)].SetValue(bytePtr, 18);
            warp.nameOfField[nameof(MyClass.Point)].SetValue(bytePtr, point);
        }
        oTime.Stop();
        DebugLog("ReflectionManager SetValue：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");

        oTime.Reset(); oTime.Start();
        for (int i = 0; i < testCount; i++)
        {
            warp.nameOfField[nameof(MyClass.Str)].propertyDelegateItem.setString(bytePtr, str);
            warp.nameOfField[nameof(MyClass.One)].propertyDelegateItem.setInt32(bytePtr, 18);
            warp.nameOfField[nameof(MyClass.Point)].propertyDelegateItem.setObject(bytePtr, point);
        }
        oTime.Stop();
        DebugLog("ReflectionManager SetValue 确定类型的：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");

        oTime.Reset(); oTime.Start();
        for (int i = 0; i < testCount; i++)
        {
            addr1.propertyDelegateItem.setString(bytePtr, str);
            addr2.propertyDelegateItem.setInt32(bytePtr, 18);
            addr3.propertyDelegateItem.setObject(bytePtr, point);
        }
        oTime.Stop();
        DebugLog("ReflectionManager SetValue 忽略字符串查询：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");

        oTime.Reset(); oTime.Start();
        for (int i = 0; i < testCount; i++)
        {
            v1 = myClass.Str;
            v2 = myClass.One;
            v3 = myClass.Point;
        }
        oTime.Stop();
        DebugLog("原生 ：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");



        DebugLog("");
        DebugLog("====================================");
        DebugLog("");

        oTime.Reset(); oTime.Start();
        for (int i = 0; i < testCount; i++)
        {
            v1 = (string)typeof(MyClass).GetProperty(nameof(MyClass.Str)).GetValue(myClass);
            v2 = (int)typeof(MyClass).GetProperty(nameof(MyClass.One)).GetValue(myClass);
            v3 = (Vector3)typeof(MyClass).GetProperty(nameof(MyClass.Point)).GetValue(myClass);
        }
        oTime.Stop();
        DebugLog("PropertyInfo GetValue：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");

        oTime.Reset(); oTime.Start();
        for (int i = 0; i < testCount; i++)
        {
            v1 = (string)warp.nameOfField[nameof(MyClass.Str)].GetValue(bytePtr);
            v2 = (int)warp.nameOfField[nameof(MyClass.One)].GetValue(bytePtr);
            v3 = (Vector3)warp.nameOfField[nameof(MyClass.Point)].GetValue(bytePtr);
        }
        oTime.Stop();
        DebugLog("ReflectionManager GetValue：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");

        oTime.Reset(); oTime.Start();
        for (int i = 0; i < testCount; i++)
        {
            v1 = warp.nameOfField[nameof(MyClass.Str)].propertyDelegateItem.getString(bytePtr);
            v2 = warp.nameOfField[nameof(MyClass.One)].propertyDelegateItem.getInt32(bytePtr);
            v3 = (Vector3)warp.nameOfField[nameof(MyClass.Point)].propertyDelegateItem.getObject(bytePtr);
        }
        oTime.Stop();
        DebugLog("ReflectionManager GetValue 确定类型的：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");

        oTime.Reset(); oTime.Start();
        for (int i = 0; i < testCount; i++)
        {
            v1 = addr1.propertyDelegateItem.getString(bytePtr);
            v2 = addr2.propertyDelegateItem.getInt32(bytePtr);
            v3 = (Vector3)addr3.propertyDelegateItem.getObject(bytePtr);
        }
        oTime.Stop();
        DebugLog("ReflectionManager GetValue 忽略字符串查询：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");

        oTime.Reset(); oTime.Start();
        for (int i = 0; i < testCount; i++)
        {
            myClass.Str = str;
            myClass.One = 18;
            myClass.Point = point;
        }
        oTime.Stop();
        DebugLog("原生 ：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");


        DebugLog("");
        DebugLog("=========↓↓↓Array↓↓↓=========");
        DebugLog("");
        v1s = new string[] { "Ac", "S@D", "CS@", "CS2ss", "CXaa" };
        v2s = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        v3s = new Vector3[] { new Vector3(1, 2, 3), new Vector3(11, -2, 41.3f), new Vector3(19.999f, 9, 0), new Vector3(123.454f, 647.4f, 11) };
        var arrayWrapV1 = ArrayWrapManager.GetIArrayWrap(typeof(string[]));
        var arrayWrapV2 = ArrayWrapManager.GetIArrayWrap(typeof(int[]));
        var arrayWrapV3 = ArrayWrapManager.GetIArrayWrap(typeof(Vector3[]));

        oTime.Reset(); oTime.Start();
        for (int i = 0; i < testCount; i++)
        {
            Array arrayV1 = v1s;
            for (int j = 0; j < arrayV1.Length; j++)
            {
                v1 = (string)arrayV1.GetValue(j);
            }
            Array arrayV2 = v2s;
            for (int j = 0; j < arrayV2.Length; j++)
            {
                v2 = (int)arrayV2.GetValue(j);
            }
            Array arrayV3 = v3s;
            for (int j = 0; j < arrayV3.Length; j++)
            {
                v3 = (Vector3)arrayV3.GetValue(j);
            }
        }

        oTime.Stop();
        DebugLog("Array GetValue：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");

        oTime.Reset(); oTime.Start();
        for (int i = 0; i < testCount; i++)
        {
            ArrayWrapOutData data1 = arrayWrapV2.GetArrayData(v1s);
            for (int j = 0; j < data1.length; j++)
            {
                v1 = (string)arrayWrapV1.GetValue(data1.startItemOffcet, j);
            }
            ArrayWrapOutData data2 = arrayWrapV2.GetArrayData(v2s);
            for (int j = 0; j < data2.length; j++)
            {
                v2 = (int)arrayWrapV2.GetValue(data2.startItemOffcet, j);
            }
            ArrayWrapOutData data3 = arrayWrapV3.GetArrayData(v3s);
            for (int j = 0; j < data3.length; j++)
            {
                v3 = (Vector3)arrayWrapV3.GetValue(data3.startItemOffcet, j);
            }
        }
        oTime.Stop();
        DebugLog("ArrayWrapManager GetValue：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");

        oTime.Reset(); oTime.Start();
        for (int i = 0; i < testCount; i++)
        {
            ArrayWrapOutData data1 = arrayWrapV2.GetArrayData(v1s);
            for (int j = 0; j < data1.length; j++)
            { 
                v1 = UnsafeUtility.ReadArrayElement<string>(data1.startItemOffcet, j);
            }
            ArrayWrapOutData data2 = arrayWrapV2.GetArrayData(v2s);
            for (int j = 0; j < data2.length; j++)
            {
                v2 = *(int*)(data2.startItemOffcet + arrayWrapV2.elementTypeSize * j);
            }
            ArrayWrapOutData data3 = arrayWrapV3.GetArrayData(v3s);
            for (int j = 0; j < data3.length; j++)
            {
                v3 = UnsafeUtility.ReadArrayElement<Vector3>(data3.startItemOffcet, j);
            }
        }
        oTime.Stop();
        DebugLog("ArrayWrapManager GetValue 确定类型的：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");

        {
            ArrayWrapOutData data1 = arrayWrapV2.GetArrayData(v1s);
            ArrayWrapOutData data2 = arrayWrapV2.GetArrayData(v2s);
            ArrayWrapOutData data3 = arrayWrapV3.GetArrayData(v3s);
            oTime.Reset(); oTime.Start();
            for (int i = 0; i < testCount; i++)
            {
                for (int j = 0; j < data1.length; j++)
                {
                    v1 = UnsafeUtility.ReadArrayElement<string>(data1.startItemOffcet, j);
                }
                for (int j = 0; j < data2.length; j++)
                {
                    v2 = *(int*)(data2.startItemOffcet + arrayWrapV2.elementTypeSize * j);
                }
                for (int j = 0; j < data3.length; j++)
                {
                    v3 = UnsafeUtility.ReadArrayElement<Vector3>(data3.startItemOffcet, j);
                }
            }
        }
        oTime.Stop();
        DebugLog("ArrayWrapManager GetValue 忽略Data查询：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");


        oTime.Reset(); oTime.Start();
        for (int i = 0; i < testCount; i++)
        {
            for (int j = 0; j < v1s.Length; j++)
            {
                v1 = v1s[j];
            }
            for (int j = 0; j < v2s.Length; j++)
            {
                v2 = v2s[j];
            }
            for (int j = 0; j < v3s.Length; j++)
            {
                v3 = v3s[j];
            }
        }
        oTime.Stop();
        DebugLog("原生 ：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");


        oTime.Reset(); oTime.Start();
        for (int i = 0; i < testCount; i++)
        {
            Array arrayV1 = v1s;
            for (int j = 0; j < arrayV1.Length; j++)
            {
                arrayV1.SetValue(v1, j);
            }
            Array arrayV2 = v2s;
            for (int j = 0; j < arrayV2.Length; j++)
            {
                arrayV2.SetValue(v2, j);
            }
            Array arrayV3 = v3s;
            for (int j = 0; j < arrayV3.Length; j++)
            {
                arrayV3.SetValue(v3, j);
            }
        }
        oTime.Stop();
        DebugLog("Array SetValue：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");

        oTime.Reset(); oTime.Start();
        for (int i = 0; i < testCount; i++)
        {
            ArrayWrapOutData data1 = arrayWrapV1.GetArrayData(v1s);
            for (int j = 0; j < data1.length; j++)
            {
                arrayWrapV1.SetValue(data1.startItemOffcet, j, v1);
            }
            ArrayWrapOutData data2 = arrayWrapV2.GetArrayData(v2s);
            for (int j = 0; j < data2.length; j++)
            {
                arrayWrapV2.SetValue(data2.startItemOffcet, j, v2);
            }
            ArrayWrapOutData data3 = arrayWrapV3.GetArrayData(v3s);
            for (int j = 0; j < data3.length; j++)
            {
                arrayWrapV3.SetValue(data3.startItemOffcet, j, v3);
            }
        }
        oTime.Stop();
        DebugLog("ArrayWrapManager SetValue：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");

        oTime.Reset(); oTime.Start();
        for (int i = 0; i < testCount; i++)
        {
            ArrayWrapOutData data1 = arrayWrapV1.GetArrayData(v1s);
            for (int j = 0; j < data1.length; j++)
            {
                UnsafeUtility.CopyObjectAddressToPtr(v1, data1.startItemOffcet + arrayWrapV1.elementTypeSize * j);
            }

            ArrayWrapOutData data2 = arrayWrapV2.GetArrayData(v2s);
            for (int j = 0; j < data2.length; j++)
            {
                *(int*)(data2.startItemOffcet + arrayWrapV2.elementTypeSize * j) = v2;
            }

            ArrayWrapOutData data3 = arrayWrapV3.GetArrayData(v3s);
            for (int j = 0; j < data3.length; j++)
            {
                UnsafeUtility.WriteArrayElement<Vector3>(data3.startItemOffcet, j, v3);
                //UnsafeUtility.MemCpy(data3.startItemOffcet + arrayWrapV3.elementTypeSize * j,
                //    UnsafeUtility.AddressOf(ref v3), arrayWrapV3.elementTypeSize);
            }
        }
        oTime.Stop();
        DebugLog("ArrayWrapManager SetValue 确定类型的：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");


        {
            ArrayWrapOutData data1 = arrayWrapV1.GetArrayData(v1s);
            ArrayWrapOutData data2 = arrayWrapV2.GetArrayData(v2s);
            ArrayWrapOutData data3 = arrayWrapV3.GetArrayData(v3s);
            oTime.Reset(); oTime.Start();
            for (int i = 0; i < testCount; i++)
            {
                for (int j = 0; j < data1.length; j++)
                {
                    UnsafeUtility.CopyObjectAddressToPtr(v1, data1.startItemOffcet + arrayWrapV1.elementTypeSize * j);
                }

                for (int j = 0; j < data2.length; j++)
                {
                    *(int*)(data2.startItemOffcet + arrayWrapV2.elementTypeSize * j) = v2;
                }

                for (int j = 0; j < data3.length; j++)
                {
                    UnsafeUtility.WriteArrayElement<Vector3>(data3.startItemOffcet, j, v3);
                }
            }
        }
        oTime.Stop();
        DebugLog("ArrayWrapManager SetValue 忽略Data查询：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");


        oTime.Reset(); oTime.Start();
        for (int i = 0; i < testCount; i++)
        {
            for (int j = 0; j < v1s.Length; j++)
            {
                v1s[j] = v1;
            }
            for (int j = 0; j < v2s.Length; j++)
            {
                v2s[j] = v2;
            }
            for (int j = 0; j < v3s.Length; j++)
            {
                v3s[j] = v3;
            }
        }
        oTime.Stop();
        DebugLog("原生 ：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");




        DebugLog("");
        DebugLog("=============↓↓↓CreateInstance↓↓↓===============");
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
