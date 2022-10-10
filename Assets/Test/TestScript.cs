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
    void DebugLogLine(object obj)
    {
        string txt = obj.ToString();
#if UNITY_EDITOR
        Debug.Log(txt);
#endif
        sb.AppendLine(txt);
    }

    void DebugLogLine()
    {
        sb.AppendLine();
    }

    void DebugLog(object obj)
    {
        string txt = obj.ToString();
#if UNITY_EDITOR
        Debug.Log(txt);
#endif
        sb.Append(txt);
    }



    private unsafe Vector3 GCC(void* a)
    {
        return *((Vector3*)new IntPtr(a));
    }

    private unsafe void Start()
    {
        //DynamicCreateType();
    }



    public unsafe void RunTest2()
    {
        testCount = int.Parse(input.text);
        sb = new StringBuilder();
        //MyClass myClass = new MyClass();

        DebugLogLine("开始 " );
        text.text = sb.ToString();

        string fieldName = "One";

        byte* bytePtr;

        var instens = new MyClass();// (MyClass)warp.Create(out bytePtr);

#if Use_Unsafe_Tool
        bytePtr = UnsafeTool.unsafeTool.ObjectToBytePtr(instens);
#else
        ulong gcHandle;
        bytePtr = (byte*)UnsafeUtility.PinGCObjectAndGetAddress(instens, out gcHandle);
        UnsafeUtility.ReleaseGCObject(gcHandle);
#endif

        DebugLogLine("创建 " + instens.GetType());
        instens.str = "S3ED";
        instens.one = 1;
        instens.point = new Vector3(3, -4.5f, 97.4f);
        var warp = TypeAddrReflectionWrapper.GetWrapper(typeof(MyClass));
        DebugLogLine("warp ");

        fieldName = "one";
        fixed (char* fieldNamePtr = fieldName)
        {
            TypeAddrFieldAndProperty addr = warp.Find(fieldNamePtr, fieldName.Length);
            int value = *(int*)(bytePtr + addr.offset);
            DebugLogLine("取值后 输出 1 : " + value);

            *(int*)(bytePtr + addr.offset) = 18;
            DebugLogLine("赋值后 输出 18 : " + instens.one);

            object obj = addr.GetValue(bytePtr);
            DebugLogLine("不指定类型取值后 输出 18 : " + obj);

            addr.SetValue(bytePtr, 444);
            DebugLogLine("不指定类型赋值后 输出 444 : " + instens.one);
        }
        //goto End;

        fieldName = "str";
        fixed (char* fieldNamePtr = fieldName)
        {
            TypeAddrFieldAndProperty addr = warp.Find(fieldNamePtr, fieldName.Length);

            string value = UnsafeUtility.ReadArrayElement<string>(bytePtr + addr.offset, 0);
            DebugLogLine("取值后 输出 S3ED : " + value);

#if ENABLE_MONO && !Test_Il2cpp
            UnsafeTool.unsafeTool.SetObject(bytePtr + addr.offset, "Acfv");
#else
            UnsafeUtility.CopyObjectAddressToPtr("Acfv", bytePtr + addr.offset);
#endif
            DebugLogLine("赋值后 输出 Acfv : " + instens.str);

            object obj = addr.GetValue(bytePtr);
            DebugLogLine("不指定类型取值后 输出 Acfv : " + obj);

            addr.SetValue(bytePtr, "DDcc#2$%@");
            DebugLogLine("不指定类型赋值后 输出 DDcc#2$%@ : " + instens.str);
        }

        fieldName = "point";
        fixed (char* fieldNamePtr = fieldName)
        {
            TypeAddrFieldAndProperty addr = warp.Find(fieldNamePtr, fieldName.Length);

            Vector3 value = UnsafeUtility.ReadArrayElement<Vector3>(bytePtr + addr.offset, 0);
            DebugLogLine("取值后 输出 3, -4.5, 97.4 : " + value);

            var pt = new Vector3(-99.56f, 50.22f, 9f);
            UnsafeUtility.WriteArrayElement<Vector3>(bytePtr + addr.offset, 0, pt);
            //下面一行也可以
            //UnsafeUtility.MemCpy(bytePtr + addr.offset, UnsafeUtility.AddressOf(ref pt), addr.stackSize);

            DebugLogLine("赋值后 输出 -99.56, 50.22, 9 : " + instens.point);

            object obj = addr.GetValue(bytePtr);
            DebugLogLine("不指定类型取值后 输出 -99.56, 50.22, 9 : " + obj);

            addr.SetValue(bytePtr, new Vector3(0, -9999f, 12.888f));
            DebugLogLine("不指定类型赋值后 输出 0, -9999 , 12.888 : " + instens.point);
        }

        DebugLogLine(" ");
        DebugLogLine("====↓↓↓↓ 属性 ↓↓↓↓===== ");
        DebugLogLine(" ");

        instens.Str = "S3ED";
        instens.One = 1;
        instens.Point = new Vector3(3, -4.5f, 97.4f);

        fieldName = "One";
        fixed (char* fieldNamePtr = fieldName)
        {
            TypeAddrFieldAndProperty addr = warp.Find(fieldNamePtr, fieldName.Length);

            int k  = addr.propertyDelegateItem.getInt32(bytePtr);
            DebugLogLine("取值后 输出 1 : " + k);

            addr.propertyDelegateItem.setInt32(bytePtr, 18);
            DebugLogLine("赋值后 输出 18 : " + instens.One);

            object obj = addr.GetValue(bytePtr);
            DebugLogLine("不指定类型取值后 输出 18 : " + obj);

            addr.SetValue(bytePtr, 444);
            DebugLogLine("不指定类型赋值后 输出 444 : " + instens.One);
        }

        fieldName = "Str";
        fixed (char* fieldNamePtr = fieldName)
        {
            TypeAddrFieldAndProperty addr = warp.Find(fieldNamePtr, fieldName.Length);

            string value = addr.propertyDelegateItem.getString(bytePtr);
            DebugLogLine("取值后 输出 S3ED : " + value);

            addr.propertyDelegateItem.setString(bytePtr, "ADc34gC@#");
            DebugLogLine("赋值后 输出 ADc34gC@# : " + instens.Str);

            object obj = addr.GetValue(bytePtr);
            DebugLogLine("不指定类型取值后 输出 ADc34gC@# : " + obj);

            addr.SetValue(bytePtr, "DDcc#2$%@ ");
            DebugLogLine("不指定类型赋值后 输出 DDcc#2$%@ : " + instens.Str);
        }

        fieldName = "Point";
        fixed (char* fieldNamePtr = fieldName)
        {
            TypeAddrFieldAndProperty addr = warp.Find(fieldNamePtr, fieldName.Length);
#if ENABLE_MONO && !Test_Il2cpp
            Vector3 value = (Vector3)addr.propertyDelegateItem.getObject(bytePtr);
#else
            Vector3 value = addr.GetPropertyObject<Vector3>(bytePtr);
#endif
            DebugLogLine("取值后 输出 3, -4.5, 97.4 : " + value);

            var pt = new Vector3(-99.56f, 50.22f, 9f);
#if ENABLE_MONO && !Test_Il2cpp
            addr.propertyDelegateItem.setObject(bytePtr, pt);
#else

            addr.SetPropertyObject<Vector3>(bytePtr, pt);
#endif
            DebugLogLine("赋值后 输出 -99.56, 50.22, 9 : " + instens.Point);

            object obj = addr.GetValue(bytePtr);
            DebugLogLine("不指定类型取值后 输出 -99.56, 50.22, 9 : " + obj);

            addr.SetValue(bytePtr, new Vector3(0, -9999f, 12.888f));
            DebugLogLine("不指定类型赋值后 输出 0, -9999 , 12.888 : " + instens.Point);
        }

        DebugLogLine(" ");
        DebugLogLine("====↓↓↓↓ 数组 ↓↓↓↓===== ");
        DebugLogLine(" ");

        instens.ones = new int[] { 1, 2, 8, 476, 898, 9 };
        instens.strs = new string[] {"ass","#$%^&","*SAHASww&()", "DDD@@" };
        instens.points = new Vector3[] {
            new Vector3(3, -4.5f, 97.4f),
            new Vector3(9999f, -43f, 0.019f),
            new Vector3(55.3f, -0.01f, -130),
        };

        fieldName = "ones";
        fixed (char* fieldNamePtr = fieldName)
        {
            TypeAddrFieldAndProperty addr = warp.Find(fieldNamePtr, fieldName.Length);
            var arrayWrap = addr.arrayWrap;

            Array array = (Array)addr.GetValue(bytePtr);
            ArrayWrapOutData arrayWrapOutData = arrayWrap.GetArrayData(array);

            DebugLogLine("取值后 输出 1, 2, 8, 476, 898, 9  : " );
            for (int i = 0; i < arrayWrapOutData.length; i++)
            {
                int value = *(int*)(arrayWrapOutData.startItemOffcet + arrayWrap.elementTypeSize * i);
                DebugLogLine(value);
            }

            for (int i = 0; i < arrayWrapOutData.length; i++)
            {
                *(int*)(arrayWrapOutData.startItemOffcet + arrayWrap.elementTypeSize * i) = i;
            }
            DebugLogLine("赋值后 输出  0, 1, 2, 3, 4, 5  : ");
            for (int i = 0; i < array.Length; i++)
            {
                DebugLogLine(array.GetValue(i));
            }

            DebugLogLine("不指定类型取值后 输出 0, 1, 2, 3, 4, 5 : ");
            for (int i = 0; i < arrayWrapOutData.length; i++)
            {
                object value = arrayWrap.GetValue(arrayWrapOutData.startItemOffcet, i);
                DebugLogLine(value);
            }

            for (int i = 0; i < arrayWrapOutData.length; i++)
            {
                arrayWrap.SetValue(arrayWrapOutData.startItemOffcet, i, 100 * i);
            }
            DebugLogLine("不指定类型赋值后 输出 0, 100, 200, 300, 400, 500 : ");
            for (int i = 0; i < array.Length; i++)
            {
                DebugLogLine(array.GetValue(i));
            }
        }

        fieldName = "strs";
        fixed (char* fieldNamePtr = fieldName)
        {
            TypeAddrFieldAndProperty addr = warp.Find(fieldNamePtr, fieldName.Length);
            //var arrayWrap = ArrayWrapManager.GetIArrayWrap(addr.fieldOrPropertyType);
            var arrayWrap = addr.arrayWrap;

            Array array = (Array)addr.GetValue(bytePtr);
            ArrayWrapOutData arrayWrapOutData = arrayWrap.GetArrayData(array);

            DebugLogLine("取值后 输出 ass,#$%^&,*SAHASww&(), DDD@@ : ");
            for (int i = 0; i < arrayWrapOutData.length; i++)
            {
                string value = UnsafeUtility.ReadArrayElement<string>(arrayWrapOutData.startItemOffcet, i);
                //下面一行也可以
                //string value = UnsafeUtility.ReadArrayElement<string>(arrayWrapOutData.startItemOffcet + arrayWrap.elementTypeSize * i, 0);
                DebugLogLine(value);
            }

            for (int i = 0; i < arrayWrapOutData.length; i++)
            {
#if ENABLE_MONO && !Test_Il2cpp
                UnsafeTool.unsafeTool.SetObject(arrayWrapOutData.startItemOffcet + arrayWrap.elementTypeSize * i, "Ac4……*" + i);
#else
                UnsafeUtility.CopyObjectAddressToPtr("Ac4……*" + i, arrayWrapOutData.startItemOffcet + arrayWrap.elementTypeSize * i);
#endif
                //下面1行也可以
                // UnsafeUtility.WriteArrayElement<string>(arrayWrapOutData.startItemOffcet, i, "Ac4……*" + i);
            }
            DebugLogLine("赋值后 输出  Ac4……*0, Ac4……*1, Ac4……*2, Ac4……*3  : ");
            for (int i = 0; i < array.Length; i++)
            {
                DebugLogLine(array.GetValue(i));
            }

            object obj = addr.GetValue(bytePtr);
            DebugLogLine("不指定类型取值后 输出 Ac4……*0, Ac4……*1, Ac4……*2, Ac4……*3  : ");
            for (int i = 0; i < arrayWrapOutData.length; i++)
            {
                object value = arrayWrap.GetValue(arrayWrapOutData.startItemOffcet, i);
                DebugLogLine(value);
            }

            for (int i = 0; i < arrayWrapOutData.length; i++)
            {
                arrayWrap.SetValue(arrayWrapOutData.startItemOffcet, i, "Fc%^" + i * 100);
            }
            DebugLogLine("不指定类型赋值后 输出 Fc%^0, Fc%^100, Fc%^200, Fc%^300 : ");
            for (int i = 0; i < array.Length; i++)
            {
                DebugLogLine(array.GetValue(i));
            }
        }

        fieldName = "points";
        fixed (char* fieldNamePtr = fieldName)
        {
            TypeAddrFieldAndProperty addr = warp.Find(fieldNamePtr, fieldName.Length);
            var arrayWrap = addr.arrayWrap;
            
            Array array = (Array)addr.GetValue(bytePtr);
            ArrayWrapOutData arrayWrapOutData = arrayWrap.GetArrayData(array);

            DebugLogLine("取值后 输出 (3, -4.5, 97.4) (9999, -43, 0.019) (55.3, -0.01, -130) : ");
            for (int i = 0; i < arrayWrapOutData.length; i++)
            {
                Vector3 value = UnsafeUtility.ReadArrayElement<Vector3>(arrayWrapOutData.startItemOffcet, i);
                DebugLogLine(value);
            }

            for (int i = 0; i < arrayWrapOutData.length; i++)
            {
                var v = new Vector3(i, i, i);
                UnsafeUtility.MemCpy(arrayWrapOutData.startItemOffcet + arrayWrap.elementTypeSize * i, UnsafeUtility.AddressOf(ref v), arrayWrap.elementTypeSize);
                //下面一行也可以
                //UnsafeUtility.WriteArrayElement<Vector3>(arrayWrapOutData.startItemOffcet, i, v);
            }

            DebugLogLine("赋值后 输出  (0,0,0) , (1,1,1) , (2,2,2)  : ");
            for (int i = 0; i < array.Length; i++)
            {
                DebugLogLine(array.GetValue(i));
            }


            object obj = addr.GetValue(bytePtr);
            DebugLogLine("不指定类型取值后 输出  (0,0,0) , (1,1,1) , (2,2,2)  : ");
            for (int i = 0; i < arrayWrapOutData.length; i++)
            {
                object value = arrayWrap.GetValue(arrayWrapOutData.startItemOffcet, i);
                DebugLogLine(value);
            }

            for (int i = 0; i < arrayWrapOutData.length; i++)
            {
                arrayWrap.SetValue(arrayWrapOutData.startItemOffcet, i, new Vector3(i * 100, i * 10, i * 1000));
            }
            DebugLogLine("不指定类型赋值后 输出  (0,0,0) , (100,10,1000) , (200,20,2000) : ");
            for (int i = 0; i < array.Length; i++)
            {
                DebugLogLine(array.GetValue(i));
            }

        }



        instens.oness = new int[,,]
        {
            { { 1, 2 },{  252, 1331 },{ 55, 66 } },
            { { 11, 898},{ 13, -19 },{ -1, -999999 } },
            { { 4576, 8198 },{ 0, 0 },{ 4176, 8958 } }
        };
        instens.strss = new string[,] {
            { "ass", "#$%^&", "*SAHASww&()", "兀驦屮鲵傌" },
            { "adsadad", "⑤驦屮尼傌", "fun(a*(b+c))", "FSD手动阀手动阀" },
        };
        instens.pointss = new Vector3[,] {
            {
               new Vector3(3, -4.5f, 97.4f),
               new Vector3(9999f, -43f, 0.019f),
               new Vector3(55.3f, -0.01f, -130)
            },
            {
               new Vector3(0, -1.2e+19f, 97.4f),
               new Vector3(9999f, -100000f, 0.019f),
               new Vector3(55.3f, -0.01f, -130)
            },
            {
               new Vector3(3, -4.5f, 5555.5555f),
               new Vector3(12321.4441f, -0.000001f, 0.019f),
               new Vector3(1234, -982.3f, -299)
            },
       };

        StringBuilder sb2 = new StringBuilder();
        fieldName = "oness";
        fixed (char* fieldNamePtr = fieldName)
        {
            TypeAddrFieldAndProperty addr = warp.Find(fieldNamePtr, fieldName.Length);
            var arrayWrap = ArrayWrapManager.GetIArrayWrap(addr.fieldOrPropertyType);

            Array array = (Array)addr.GetValue(bytePtr);
            ArrayWrapOutData arrayWrapOutData = arrayWrap.GetArrayData(array);

            DebugLogLine("取值后 输出\n1,2,252,1331,55,66,11,898,13,-19,-1,-999999,4576,8198,0,0,4176,8958: ");

            int yzL = arrayWrapOutData.arrayLengths[1] * arrayWrapOutData.arrayLengths[2];
            int zL = arrayWrapOutData.arrayLengths[2];
            for (int x = 0; x < arrayWrapOutData.arrayLengths[0]; x++)
            {
                for (int y = 0; y < arrayWrapOutData.arrayLengths[1]; y++)
                {
                    for (int z = 0; z < arrayWrapOutData.arrayLengths[2]; z++)
                    {
                        int value = *(int*)(arrayWrapOutData.startItemOffcet + arrayWrap.elementTypeSize *
                            (x * yzL + y * zL + z)
                            );
                        DebugLog(value + ",");
                    }
                }
            }
            DebugLogLine("");

            for (int i = 0; i < arrayWrapOutData.length; i++)
            {
                *(int*)(arrayWrapOutData.startItemOffcet + arrayWrap.elementTypeSize * i) = i;
            }
            for (int x = 0, i = 0; x < arrayWrapOutData.arrayLengths[0]; x++)
            {
                for (int y = 0; y < arrayWrapOutData.arrayLengths[1]; y++)
                {
                    for (int z = 0; z < arrayWrapOutData.arrayLengths[2]; z++, i++)
                    {
                        *(int*)(arrayWrapOutData.startItemOffcet + arrayWrap.elementTypeSize * (x * yzL + y * zL + z)) = i;
                    }
                }
            }
            DebugLogLine("赋值后 输出\n0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17: ");
            for (int x = 0; x < arrayWrapOutData.arrayLengths[0]; x++)
            {
                for (int y = 0; y < arrayWrapOutData.arrayLengths[1]; y++)
                {
                    for (int z = 0; z < arrayWrapOutData.arrayLengths[2]; z++)
                    {
                        DebugLog(array.GetValue(x, y, z) + ",");
                    }
                }
            }
            DebugLogLine("");


            DebugLogLine("不指定类型取值后 输出\n0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17: ");
            for (int x = 0, i = 0; x < arrayWrapOutData.arrayLengths[0]; x++)
            {
                for (int y = 0; y < arrayWrapOutData.arrayLengths[1]; y++)
                {
                    for (int z = 0; z < arrayWrapOutData.arrayLengths[2]; z++, i++)
                    {
                        object value = arrayWrap.GetValue(arrayWrapOutData.startItemOffcet, (x * yzL + y * zL + z));
                        DebugLog(value + ",");
                    }
                }
            }
            DebugLogLine("");

            for (int x = 0, i = 0; x < arrayWrapOutData.arrayLengths[0]; x++)
            {
                for (int y = 0; y < arrayWrapOutData.arrayLengths[1]; y++)
                {
                    for (int z = 0; z < arrayWrapOutData.arrayLengths[2]; z++, i++)
                    {
                        arrayWrap.SetValue(arrayWrapOutData.startItemOffcet, (x * yzL + y * zL + z), -i);
                    }
                }
            }
            DebugLogLine("不指定类型取值后 输出\n0,-1,-2,-3,-4,-5,-6,-7,-8,-9,-10,-11,-12,-13,-14,-15,-16,-17: ");
            for (int x = 0; x < arrayWrapOutData.arrayLengths[0]; x++)
            {
                for (int y = 0; y < arrayWrapOutData.arrayLengths[1]; y++)
                {
                    for (int z = 0; z < arrayWrapOutData.arrayLengths[2]; z++)
                    {
                        DebugLog(arrayWrap.GetValue(arrayWrapOutData.startItemOffcet, x * yzL + y * zL + z) + ",");
                    }
                }
            }
            DebugLogLine("");

        }


        fieldName = "strss";
        fixed (char* fieldNamePtr = fieldName)
        {
            TypeAddrFieldAndProperty addr = warp.Find(fieldNamePtr, fieldName.Length);
            var arrayWrap = ArrayWrapManager.GetIArrayWrap(addr.fieldOrPropertyType);

            Array array = (Array)addr.GetValue(bytePtr);
            ArrayWrapOutData arrayWrapOutData = arrayWrap.GetArrayData(array);

            DebugLogLine("取值后 输出\nass,#$%^&,*SAHASww&(),兀驦屮鲵傌,adsadad,⑤驦屮尼傌,fun(a*(b+c)),FSD手动阀手动阀: ");
            int yL = arrayWrapOutData.arrayLengths[1];
            for (int x = 0; x < arrayWrapOutData.arrayLengths[0]; x++)
            {
                for (int y = 0; y < arrayWrapOutData.arrayLengths[1]; y++)
                {
                    //string value = UnsafeUtility.ReadArrayElement<string>((arrayWrapOutData.startItemOffcet + arrayWrap.elementTypeSize * (x * yL + y)), 0);
                    string value = UnsafeUtility.ReadArrayElement<string>(arrayWrapOutData.startItemOffcet, x * yL + y);
                    DebugLog(value + ",");
                }
            }
            DebugLogLine("");

            for (int x = 0, i = 0; x < arrayWrapOutData.arrayLengths[0]; x++)
            {
                for (int y = 0; y < arrayWrapOutData.arrayLengths[1]; y++, i++)
                {
                    //UnsafeUtility.WriteArrayElement<string>(arrayWrapOutData.startItemOffcet + arrayWrap.elementTypeSize * (x * yL + y), 0, "Ac4……*" + i);
                    UnsafeUtility.WriteArrayElement<string>(arrayWrapOutData.startItemOffcet, x * yL + y, "Ac4……*" + i);
                }
            }
            DebugLogLine("赋值后 输出\nAc4……*0,Ac4……*1,Ac4……*2,Ac4……*3,Ac4……*4,Ac4……*5,Ac4……*6,Ac4……*7: ");
            for (int x = 0, i = 0; x < arrayWrapOutData.arrayLengths[0]; x++)
            {
                for (int y = 0; y < arrayWrapOutData.arrayLengths[1]; y++, i++)
                {
                    DebugLog(array.GetValue(x, y) + ",");
                }
            }
            DebugLogLine("");


            DebugLogLine("不指定类型取值后 输出\nAc4……*0,Ac4……*1,Ac4……*2,Ac4……*3,Ac4……*4,Ac4……*5,Ac4……*6,Ac4……*7: ");
            for (int x = 0, i = 0; x < arrayWrapOutData.arrayLengths[0]; x++)
            {
                for (int y = 0; y < arrayWrapOutData.arrayLengths[1]; y++, i++)
                {
                    object value = arrayWrap.GetValue(arrayWrapOutData.startItemOffcet, (x * yL + y));
                    DebugLog(value + ",");
                }
            }
            DebugLogLine("");


            for (int x = 0, i = 0; x < arrayWrapOutData.arrayLengths[0]; x++)
            {
                for (int y = 0; y < arrayWrapOutData.arrayLengths[1]; y++, i++)
                {
                    arrayWrap.SetValue(arrayWrapOutData.startItemOffcet, i, "Fc%^" + (x * yL + y) * 100);
                }
            }
            DebugLogLine("不指定类型赋值后 输出\nFc%^0,Fc%^100,Fc%^200,Fc%^300,Fc%^400,Fc%^500,Fc%^600,Fc%^700: ");
            for (int x = 0; x < arrayWrapOutData.arrayLengths[0]; x++)
            {
                for (int y = 0; y < arrayWrapOutData.arrayLengths[1]; y++)
                {
                    DebugLog(array.GetValue(x, y) + ",");
                }
            }
            DebugLogLine("");

        }


        fieldName = "pointss";
        fixed (char* fieldNamePtr = fieldName)
        {
            TypeAddrFieldAndProperty addr = warp.Find(fieldNamePtr, fieldName.Length);
            var arrayWrap = ArrayWrapManager.GetIArrayWrap(addr.fieldOrPropertyType);

            Array array = (Array)addr.GetValue(bytePtr);
            ArrayWrapOutData arrayWrapOutData = arrayWrap.GetArrayData(array);

            DebugLogLine("取值后 输出\n(3.0, -4.5, 97.4),(9999.0, -43.0, 0.0),(55.3, 0.0, -130.0),(0.0, -12000000000000000000.0, 97.4),(9999.0, -100000.0, 0.0),(55.3, 0.0, -130.0),(3.0, -4.5, 5555.6),(12321.4, 0.0, 0.0),(1234.0, -982.3, -299.0),: ");
            int yL = arrayWrapOutData.arrayLengths[1];
            for (int x = 0; x < arrayWrapOutData.arrayLengths[0]; x++)
            {
                for (int y = 0; y < arrayWrapOutData.arrayLengths[1]; y++)
                {
                    Vector3 value = *(Vector3*)(arrayWrapOutData.startItemOffcet + arrayWrap.elementTypeSize * (x * yL + y));
                    DebugLog(value + ",");
                }
            }
            DebugLogLine();


            for (int x = 0, i = 0; x < arrayWrapOutData.arrayLengths[0]; x++)
            {
                for (int y = 0; y < arrayWrapOutData.arrayLengths[1]; y++, i++)
                {
                    var v = new Vector3(i, i, i);
                    UnsafeUtility.MemCpy(arrayWrapOutData.startItemOffcet + arrayWrap.elementTypeSize * (x * yL + y), UnsafeUtility.AddressOf(ref v), arrayWrap.elementTypeSize);
                }
            }
            DebugLogLine("赋值后 输出\n(0.0, 0.0, 0.0),(1.0, 1.0, 1.0),(2.0, 2.0, 2.0),(3.0, 3.0, 3.0),(4.0, 4.0, 4.0),(5.0, 5.0, 5.0),(6.0, 6.0, 6.0),(7.0, 7.0, 7.0),(8.0, 8.0, 8.0),: ");
            for (int x = 0; x < arrayWrapOutData.arrayLengths[0]; x++)
            {
                for (int y = 0; y < arrayWrapOutData.arrayLengths[1]; y++)
                {
                    DebugLog(array.GetValue(x, y) + ",");
                }
            }
            DebugLogLine();



            DebugLogLine("不指定类型取值后 输出\n(0.0, 0.0, 0.0),(1.0, 1.0, 1.0),(2.0, 2.0, 2.0),(3.0, 3.0, 3.0),(4.0, 4.0, 4.0),(5.0, 5.0, 5.0),(6.0, 6.0, 6.0),(7.0, 7.0, 7.0),(8.0, 8.0, 8.0),: ");
            for (int x = 0; x < arrayWrapOutData.arrayLengths[0]; x++)
            {
                for (int y = 0; y < arrayWrapOutData.arrayLengths[1]; y++)
                {
                    //object value = arrayWrap.GetValue(arrayWrapOutData.startItemOffcet, i);
                    object value = arrayWrap.GetValue(arrayWrapOutData.startItemOffcet, (x * yL + y));
                    DebugLog(value + ",");
                }
            }
            DebugLogLine();


            for (int x = 0, i = 0; x < arrayWrapOutData.arrayLengths[0]; x++)
            {
                for (int y = 0; y < arrayWrapOutData.arrayLengths[1]; y++, i++)
                {
                    arrayWrap.SetValue(arrayWrapOutData.startItemOffcet, (x * yL + y), new Vector3(i * 100, i * 10, i * 1000));
                }
            }
            DebugLogLine("不指定类型赋值后 输出\n(0.0, 0.0, 0.0),(100.0, 10.0, 1000.0),(200.0, 20.0, 2000.0),(300.0, 30.0, 3000.0),(400.0, 40.0, 4000.0),(500.0, 50.0, 5000.0),(600.0, 60.0, 6000.0),(700.0, 70.0, 7000.0),(800.0, 80.0, 8000.0),: ");
            for (int x = 0; x < arrayWrapOutData.arrayLengths[0]; x++)
            {
                for (int y = 0; y < arrayWrapOutData.arrayLengths[1]; y++)
                {
                    DebugLog(array.GetValue(x, y) + ",");
                }
            }
            DebugLogLine();

        }


        text.text = sb.ToString();

        GC.Collect();
        return;
    }

    string[] v1s;
    int[] v2s;
    Vector3[] v3s;

    string[,] v1ss;
    int[,,] v2ss;
    Vector3[,] v3ss;

    string v1;
    int v2;
    Vector3 v3;
    MyClass a;

    MyClass myClass;

    public unsafe void RunTest()
    {
        //long[,] aaa = new long[2,2];
        //for (int x = 0; x < 2; x++)
        //{
        //    for (int z = 0; z < 2; z++)
        //    {
        //        aaa[x, z] = 1334;
        //    }
        //}

        //Debug.Log(UnsafeOperation.GetTypeHead(typeof(long)));

        //long* aap = (long*)UnsafeTool.unsafeTool.ObjectToBytePtr(aaa);
        //for (int i = 0; i < 80; i++)
        //{
        //    Debug.Log(aap[i]);
        //}
        //Debug.Log(
        //    UnsafeTool.unsafeTool.VoidPtrToObject(aap + 2).GetType()
        //    );

        //return;




        var fieldName_str = nameof(MyClass.str);
        var fieldName_one = nameof(MyClass.one);
        var fieldName_point = nameof(MyClass.point);

        var fieldName_strLength = fieldName_str.Length;
        var fieldName_oneLength = fieldName_one.Length;
        var fieldName_pointLength = fieldName_point.Length;

        var fieldName_Str = nameof(MyClass.Str);
        var fieldName_One = nameof(MyClass.One);
        var fieldName_Point = nameof(MyClass.Point);

        var fieldName_StrLength = fieldName_Str.Length;
        var fieldName_OneLength = fieldName_One.Length;
        var fieldName_PointLength = fieldName_Point.Length;


        fixed (char* _str = fieldName_str)
        fixed (char* _one = fieldName_one)
        fixed (char* _point = fieldName_point)
        fixed (char* _Str = fieldName_Str)
        fixed (char* _One = fieldName_One)
        fixed (char* _Point = fieldName_Point)
        {
            testCount = int.Parse(input.text);
            sb = new StringBuilder();
            var warp = TypeAddrReflectionWrapper.GetWrapper(typeof(MyClass));

            string fieldName = "str";
            int nameSize = fieldName.Length;

            byte* bytePtr;
            myClass = (MyClass)warp.Create(out bytePtr);


            string str = "hello world";
            Vector3 point = new Vector3(99.2f, -98.2f, 3.2f);
            DebugLogLine("");
            DebugLogLine("循环" + testCount + "次");
            DebugLogLine("====================================");
            DebugLogLine("");

            //fieldName = "one";
            //nameSize = fieldName.Length;

            var addr1 = warp.Find(_str, fieldName_strLength);
            var addr2 = warp.Find(_one, fieldName_oneLength);
            var addr3 = warp.Find(_point, fieldName_pointLength);

            oTime.Reset(); oTime.Start();
            for (int i = 0; i < testCount; i++)
            {
                typeof(MyClass).GetField(nameof(MyClass.str)).SetValue(myClass, str);
                typeof(MyClass).GetField(nameof(MyClass.one)).SetValue(myClass, 18);
                typeof(MyClass).GetField(nameof(MyClass.point)).SetValue(myClass, point);
            }
            oTime.Stop();
            DebugLogLine("FieldInfo SetValue：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");


            oTime.Reset(); oTime.Start();
            for (int i = 0; i < testCount; i++)
            {
                warp.nameOfField[nameof(MyClass.str)].SetValue(bytePtr, str);
                warp.nameOfField[nameof(MyClass.one)].SetValue(bytePtr, 18);
                warp.nameOfField[nameof(MyClass.point)].SetValue(bytePtr, point);
            }
            oTime.Stop();
            DebugLogLine("指针方法 SetValue 使用object类型 string查询：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");

            oTime.Reset(); oTime.Start();
            for (int i = 0; i < testCount; i++)
            {
                warp.Find(_str, fieldName_strLength).SetValue(bytePtr, str);
                warp.Find(_one, fieldName_oneLength).SetValue(bytePtr, 18);
                warp.Find(_point, fieldName_pointLength).SetValue(bytePtr, point);
            }
            oTime.Stop();
            DebugLogLine("指针方法 SetValue 使用object类型 char*查询：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");


            oTime.Reset(); oTime.Start();
            for (int i = 0; i < testCount; i++)
            {
                UnsafeUtility.CopyObjectAddressToPtr(str, bytePtr + warp.Find(_str, fieldName_strLength).offset);
                *(int*)(bytePtr + warp.Find(_one, fieldName_oneLength).offset) = 18;
                var addr = warp.Find(_point, fieldName_pointLength);
                UnsafeUtility.MemCpy(bytePtr + addr.offset, UnsafeUtility.AddressOf(ref point), addr.stackSize);
            }
            oTime.Stop();
            DebugLogLine("指针方法 SetValue 确定类型的：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");

            oTime.Reset(); oTime.Start();
            void* pointPtr = UnsafeUtility.AddressOf(ref point);
            for (int i = 0; i < testCount; i++)
            {
#if Use_Unsafe_Tool
                UnsafeTool.unsafeTool.SetObject(bytePtr + addr1.offset, str);
#else
            UnsafeUtility.CopyObjectAddressToPtr(str, bytePtr + addr1.offset);
#endif
                *(int*)(bytePtr + addr2.offset) = 18;
                // GeneralTool.Memcpy(bytePtr + addr3.offset, pointPtr, addr3.stackSize);
                UnsafeUtility.MemCpy(bytePtr + addr3.offset, pointPtr, addr3.stackSize);
            }
            oTime.Stop();
            DebugLogLine("指针方法 SetValue 忽略字符串查询：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");



            oTime.Reset(); oTime.Start();
            for (int i = 0; i < testCount; i++)
            {
                myClass.one = 18;
                myClass.str = str;
                myClass.point = point;
            }
            oTime.Stop();
            DebugLogLine("原生 ：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");

            DebugLogLine("");
            DebugLogLine("====================================");
            DebugLogLine("");


            oTime.Reset(); oTime.Start();
            for (int i = 0; i < testCount; i++)
            {
                v1 = (string)typeof(MyClass).GetField(nameof(MyClass.str)).GetValue(myClass);
                v2 = (int)typeof(MyClass).GetField(nameof(MyClass.one)).GetValue(myClass);
                v3 = (Vector3)typeof(MyClass).GetField(nameof(MyClass.point)).GetValue(myClass);
            }
            oTime.Stop();
            DebugLogLine("FieldInfo GetValue：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");


            oTime.Reset(); oTime.Start();
            for (int i = 0; i < testCount; i++)
            {
                v1 = (string)warp.nameOfField[nameof(MyClass.str)].GetValue(bytePtr);
                v2 = (int)warp.nameOfField[nameof(MyClass.one)].GetValue(bytePtr);
                v3 = (Vector3)warp.nameOfField[nameof(MyClass.point)].GetValue(bytePtr);
            }
            oTime.Stop();
            DebugLogLine("指针方法 GetValue 使用object类型 string查询：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");


            oTime.Reset(); oTime.Start();
            for (int i = 0; i < testCount; i++)
            {
                v1 = (string)warp.Find(_str, fieldName_strLength).GetValue(bytePtr);
                v2 = (int)warp.Find(_one, fieldName_oneLength).GetValue(bytePtr);
                v3 = (Vector3)warp.Find(_point, fieldName_pointLength).GetValue(bytePtr);
            }
            oTime.Stop();
            DebugLogLine("指针方法 GetValue 使用object类型 char*查询：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");



            oTime.Reset(); oTime.Start();
            for (int i = 0; i < testCount; i++)
            {
                v1 = UnsafeUtility.ReadArrayElement<string>(bytePtr + warp.Find(_str, fieldName_strLength).offset, 0);
                v2 = *(int*)(bytePtr + warp.Find(_one, fieldName_oneLength).offset);
                v3 = UnsafeUtility.ReadArrayElement<Vector3>(bytePtr + warp.Find(_point, fieldName_pointLength).offset, 0);
            }
            oTime.Stop();
            DebugLogLine("指针方法 GetValue 确定类型的：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");


            oTime.Reset(); oTime.Start();
            for (int i = 0; i < testCount; i++)
            {
                v1 = UnsafeUtility.ReadArrayElement<string>(bytePtr + addr1.offset, 0);
                v2 = *(int*)(bytePtr + addr2.offset);
                v3 = UnsafeUtility.ReadArrayElement<Vector3>(bytePtr + addr3.offset, 0);
            }
            oTime.Stop();
            DebugLogLine("指针方法 GetValue 忽略字符串查询：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");



            oTime.Reset(); oTime.Start();
            for (int i = 0; i < testCount; i++)
            {
                v1 = myClass.str;
                v2 = myClass.one;
                v3 = myClass.point;
            }
            oTime.Stop();
            DebugLogLine("原生 ：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");



            DebugLogLine("");
            DebugLogLine("=========↓↓↓Property↓↓↓=============");
            DebugLogLine("");

            addr1 = warp.Find(_Str, fieldName_StrLength);
            addr2 = warp.Find(_One, fieldName_OneLength);
            addr3 = warp.Find(_Point, fieldName_PointLength);
            oTime.Reset(); oTime.Start();
            for (int i = 0; i < testCount; i++)
            {
                typeof(MyClass).GetProperty(nameof(MyClass.Str)).SetValue(myClass, str);
                typeof(MyClass).GetProperty(nameof(MyClass.One)).SetValue(myClass, 18);
                typeof(MyClass).GetProperty(nameof(MyClass.Point)).SetValue(myClass, point);
            }
            oTime.Stop();
            DebugLogLine("PropertyInfo SetValue：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");

            oTime.Reset(); oTime.Start();
            for (int i = 0; i < testCount; i++)
            {
                warp.nameOfField[nameof(MyClass.Str)].SetValue(bytePtr, str);
                warp.nameOfField[nameof(MyClass.One)].SetValue(bytePtr, 18);
                warp.nameOfField[nameof(MyClass.Point)].SetValue(bytePtr, point);
            }
            oTime.Stop();
            DebugLogLine("指针方法 SetValue 使用object类型 string查询：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");

            oTime.Reset(); oTime.Start();
            for (int i = 0; i < testCount; i++)
            {
                warp.Find(_Str, fieldName_StrLength).SetValue(bytePtr, str);
                warp.Find(_One, fieldName_OneLength).SetValue(bytePtr, 18);
                warp.Find(_Point, fieldName_PointLength).SetValue(bytePtr, point);
            }
            oTime.Stop();
            DebugLogLine("指针方法 SetValue 使用object类型 char*查询：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");


            oTime.Reset(); oTime.Start();
            for (int i = 0; i < testCount; i++)
            {
                warp.Find(_Str, fieldName_StrLength).propertyDelegateItem.setString(bytePtr, str);
                warp.Find(_One, fieldName_OneLength).propertyDelegateItem.setInt32(bytePtr, 18);
#if ENABLE_MONO && !Test_Il2cpp
                warp.Find(_Point, fieldName_PointLength).propertyDelegateItem.setObject(bytePtr, point);
#else

            warp.Find(_One, fieldName_OneLength).SetPropertyObject<Vector3>(bytePtr, point);
#endif
            }
            oTime.Stop();
            DebugLogLine("指针方法 SetValue 确定类型的：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");

            oTime.Reset(); oTime.Start();
            for (int i = 0; i < testCount; i++)
            {
                addr1.propertyDelegateItem.setString(bytePtr, str);
                addr2.propertyDelegateItem.setInt32(bytePtr, 18);
#if ENABLE_MONO && !Test_Il2cpp
                addr3.propertyDelegateItem.setObject(bytePtr, point);
#else
            addr3.SetPropertyObject<Vector3>(bytePtr, point);
#endif

            }
            oTime.Stop();
            DebugLogLine("指针方法 SetValue 忽略字符串查询：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");

            oTime.Reset(); oTime.Start();
            for (int i = 0; i < testCount; i++)
            {
                v1 = myClass.Str;
                v2 = myClass.One;
                v3 = myClass.Point;
            }
            oTime.Stop();
            DebugLogLine("原生 ：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");



            DebugLogLine("");
            DebugLogLine("====================================");
            DebugLogLine("");

            oTime.Reset(); oTime.Start();
            for (int i = 0; i < testCount; i++)
            {
                v1 = (string)typeof(MyClass).GetProperty(nameof(MyClass.Str)).GetValue(myClass);
                v2 = (int)typeof(MyClass).GetProperty(nameof(MyClass.One)).GetValue(myClass);
                v3 = (Vector3)typeof(MyClass).GetProperty(nameof(MyClass.Point)).GetValue(myClass);
            }
            oTime.Stop();
            DebugLogLine("PropertyInfo GetValue：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");

            oTime.Reset(); oTime.Start();
            for (int i = 0; i < testCount; i++)
            {
                v1 = (string) warp.Find(_Str, fieldName_StrLength).GetValue(bytePtr);
                v2 = (int)warp.Find(_One, fieldName_OneLength).GetValue(bytePtr);
                v3 = (Vector3)warp.Find(_Point, fieldName_PointLength).GetValue(bytePtr);
            }
            oTime.Stop();
            DebugLogLine("指针方法 GetValue：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");

            oTime.Reset(); oTime.Start();
            for (int i = 0; i < testCount; i++)
            {
                v1 =  warp.Find(_Str, fieldName_StrLength).propertyDelegateItem.getString(bytePtr);
                v2 = warp.Find(_One, fieldName_OneLength).propertyDelegateItem.getInt32(bytePtr);
#if ENABLE_MONO && !Test_Il2cpp
                v3 = (Vector3)warp.Find(_Point, fieldName_PointLength).propertyDelegateItem.getObject(bytePtr);
#else
            v3 = (Vector3)warp.Find(_Point, fieldName_PointLength).GetPropertyObject<Vector3>(bytePtr);
#endif
            }
            oTime.Stop();
            DebugLogLine("指针方法 GetValue 确定类型的：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");

            oTime.Reset(); oTime.Start();
            for (int i = 0; i < testCount; i++)
            {
                v1 = addr1.propertyDelegateItem.getString(bytePtr);
                v2 = addr2.propertyDelegateItem.getInt32(bytePtr);
#if ENABLE_MONO && !Test_Il2cpp
                v3 = (Vector3)addr3.propertyDelegateItem.getObject(bytePtr);
#else
            v3 = (Vector3)addr3.GetPropertyObject<Vector3>(bytePtr);
#endif
            }
            oTime.Stop();
            DebugLogLine("指针方法 GetValue 忽略字符串查询：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");

            oTime.Reset(); oTime.Start();
            for (int i = 0; i < testCount; i++)
            {
                myClass.Str = str;
                myClass.One = 18;
                myClass.Point = point;
            }
            oTime.Stop();
            DebugLogLine("原生 ：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");


            DebugLogLine("");
            DebugLogLine("=========↓↓↓Array↓↓↓=========");
            DebugLogLine("");
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
            DebugLogLine("Array GetValue：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");

            oTime.Reset(); oTime.Start();
            for (int i = 0; i < testCount; i++)
            {
                ArrayWrapOutData data1 = arrayWrapV1.GetArrayData(v1s);
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
            DebugLogLine("ArrayWrapManager GetValue：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");

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
            DebugLogLine("ArrayWrapManager GetValue 确定类型的：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");

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
            DebugLogLine("ArrayWrapManager GetValue 忽略Data查询：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");

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
            DebugLogLine("原生 ：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");


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
            DebugLogLine("Array SetValue：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");

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
            DebugLogLine("ArrayWrapManager SetValue：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");

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
            DebugLogLine("ArrayWrapManager SetValue 确定类型的：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");


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
            DebugLogLine("ArrayWrapManager SetValue 忽略Data查询：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");


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
            DebugLogLine("原生 ：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");




            DebugLogLine("");
            DebugLogLine("=============↓↓↓Multidimensional Array↓↓↓===============");
            DebugLogLine("");
            v1ss = new string[,] {
                { "ass", "#$%^&", "*SAHASww&()", "兀驦屮鲵傌" },
                { "adsadad", "⑤驦屮尼傌", "fun(a*(b+c))", "FSD手动阀手动阀" },
            };
            v2ss = new int[,,]
            {
                { { 1, 2 },{  252, 1331 },{ 55, 66 } },
                { { 11, 898},{ 13, -19 },{ -1, -999999 } },
                { { 4576, 8198 },{ 0, 0 },{ 4176, 8958 } }
            };
            v3ss = new Vector3[,] {
                {
                   new Vector3(3, -4.5f, 97.4f),
                   new Vector3(9999f, -43f, 0.019f),
                   new Vector3(55.3f, -0.01f, -130)
                },
                {
                   new Vector3(0, -1.2e+19f, 97.4f),
                   new Vector3(9999f, -100000f, 0.019f),
                   new Vector3(55.3f, -0.01f, -130)
                },
                {
                   new Vector3(3, -4.5f, 5555.5555f),
                   new Vector3(12321.4441f, -0.000001f, 0.019f),
                   new Vector3(1234, -982.3f, -299)
                },
            };
            arrayWrapV1 = ArrayWrapManager.GetIArrayWrap(v1ss.GetType());
            arrayWrapV2 = ArrayWrapManager.GetIArrayWrap(v2ss.GetType());
            arrayWrapV3 = ArrayWrapManager.GetIArrayWrap(v3ss.GetType());


            oTime.Reset(); oTime.Start();
            for (int i = 0; i < testCount; i++)
            {
                Array arrayV1 = v1ss;
                for (int x = 0; x < arrayV1.GetLength(0); x++)
                {
                    for (int y = 0; y < arrayV1.GetLength(1); y++)
                    {
                        v1 = (string)arrayV1.GetValue(x, y);
                    }
                }

                Array arrayV2 = v2ss;
                for (int x = 0; x < arrayV2.GetLength(0); x++)
                {
                    for (int y = 0; y < arrayV2.GetLength(1); y++)
                    {
                        for (int z = 0; z < arrayV2.GetLength(2); z++)
                        {
                            v2 = (int)arrayV2.GetValue(x, y, z);
                        }
                    }
                }

                Array arrayV3 = v3ss;
                for (int x = 0; x < arrayV3.GetLength(0); x++)
                {
                    for (int y = 0; y < arrayV3.GetLength(1); y++)
                    {
                        v3 = (Vector3)arrayV3.GetValue(x, y);
                    }
                }
            }
            oTime.Stop();
            DebugLogLine("原生反射 Multidimensional Array GetValue：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");

            oTime.Reset(); oTime.Start();
            for (int i = 0; i < testCount; i++)
            {
                ArrayWrapOutData data1 = arrayWrapV1.GetArrayData(v1ss);
                for (int x = 0; x < data1.arrayLengths[0]; x++)
                {
                    for (int y = 0; y < data1.arrayLengths[1]; y++)
                    {
                        v1 = (string)arrayWrapV1.GetValue(data1.startItemOffcet, x * data1.arrayLengths[1] + y);
                    }
                }
                ArrayWrapOutData data2 = arrayWrapV2.GetArrayData(v2ss);
                for (int x = 0; x < data2.arrayLengths[0]; x++)
                {
                    for (int y = 0; y < data2.arrayLengths[1]; y++)
                    {
                        for (int z = 0; z < data2.arrayLengths[2]; z++)
                        {
                            v2 = (int)arrayWrapV2.GetValue(data2.startItemOffcet, (x * data2.arrayLengths[1] * data2.arrayLengths[2] + y * data2.arrayLengths[2] + z));
                        }
                    }
                }
                ArrayWrapOutData data3 = arrayWrapV3.GetArrayData(v3ss);
                for (int x = 0; x < data3.arrayLengths[0]; x++)
                {
                    for (int y = 0; y < data3.arrayLengths[1]; y++)
                    {
                        v3 = (Vector3)arrayWrapV3.GetValue(data3.startItemOffcet, x * data3.arrayLengths[1] + y);
                    }
                }
            }
            oTime.Stop();
            DebugLogLine("指针方法 Multidimensional ArrayWrapManager GetValue：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");


            oTime.Reset(); oTime.Start();
            for (int i = 0; i < testCount; i++)
            {
                ArrayWrapOutData data1 = arrayWrapV1.GetArrayData(v1ss);
                for (int x = 0; x < data1.arrayLengths[0]; x++)
                {
                    for (int y = 0; y < data1.arrayLengths[1]; y++)
                    {
                        v1 = UnsafeUtility.ReadArrayElement<string>(data1.startItemOffcet, (x * data1.arrayLengths[1] + y));
                    }
                }
                 
                ArrayWrapOutData data2 = arrayWrapV2.GetArrayData(v2ss);
                for (int x = 0; x < data2.arrayLengths[0]; x++)
                {
                    for (int y = 0; y < data2.arrayLengths[1]; y++)
                    {
                        for (int z = 0; z < data2.arrayLengths[2]; z++)
                        {
                            v2 = *(int*)(data2.startItemOffcet + arrayWrapV2.elementTypeSize * (x * data2.arrayLengths[1] * data2.arrayLengths[2] + y * data2.arrayLengths[2] + z));
                        }
                    }
                }

                ArrayWrapOutData data3 = arrayWrapV3.GetArrayData(v3ss);
                for (int x = 0; x < data3.arrayLengths[0]; x++)
                {
                    for (int y = 0; y < data3.arrayLengths[1]; y++)
                    {
                        v3 = *(Vector3*)(data3.startItemOffcet + arrayWrapV3.elementTypeSize * (x * data3.arrayLengths[1] + y));
                    }
                }
            }
            oTime.Stop();
            DebugLogLine("指针方法 Multidimensional ArrayWrapManager GetValue 确定类型的：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");

            {
                ArrayWrapOutData data1 = arrayWrapV1.GetArrayData(v1ss);
                ArrayWrapOutData data2 = arrayWrapV2.GetArrayData(v2ss);
                ArrayWrapOutData data3 = arrayWrapV3.GetArrayData(v3ss);
                oTime.Reset(); oTime.Start();
                for (int i = 0; i < testCount; i++)
                {
                    for (int x = 0; x < data1.arrayLengths[0]; x++)
                    {
                        for (int y = 0; y < data1.arrayLengths[1]; y++)
                        {
                            v1 = UnsafeUtility.ReadArrayElement<string>(data1.startItemOffcet, (x * data1.arrayLengths[1] + y));
                        }
                    }
                    for (int x = 0; x < data2.arrayLengths[0]; x++)
                    {
                        for (int y = 0; y < data2.arrayLengths[1]; y++)
                        {
                            for (int z = 0; z < data2.arrayLengths[2]; z++)
                            {
                                v2 = *(int*)(data2.startItemOffcet + arrayWrapV2.elementTypeSize * (x * data2.arrayLengths[1] * data2.arrayLengths[2] + y * data2.arrayLengths[2] + z));
                            }
                        }
                    }
                    for (int x = 0; x < data3.arrayLengths[0]; x++)
                    {
                        for (int y = 0; y < data3.arrayLengths[1]; y++)
                        {
                            v3 = *(Vector3*)(data3.startItemOffcet + arrayWrapV3.elementTypeSize * (x * data3.arrayLengths[1] + y));
                        }
                    }
                }
            }
            oTime.Stop();
            DebugLogLine("指针方法 Multidimensional ArrayWrapManager GetValue 忽略Data查询：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");

            oTime.Reset(); oTime.Start();
            for (int i = 0; i < testCount; i++)
            {
                for (int x = 0; x < v1ss.GetLength(0); x++)
                {
                    for (int y = 0; y < v1ss.GetLength(1); y++)
                    {
                        v1 = v1ss[x, y];
                    }
                }
                for (int x = 0; x < v2ss.GetLength(0); x++)
                {
                    for (int y = 0; y < v2ss.GetLength(1); y++)
                    {
                        for (int z = 0; z < v2ss.GetLength(2); z++)
                        {
                            v2 = v2ss[x, y, z];
                        }
                    }
                }
                for (int x = 0; x < v3ss.GetLength(0); x++)
                {
                    for (int y = 0; y < v3ss.GetLength(1); y++)
                    {
                        v3 = v3ss[x, y];
                    }
                }
            }
            oTime.Stop();
            DebugLogLine("原生 ：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");



            oTime.Reset(); oTime.Start();
            for (int i = 0; i < testCount; i++)
            {
                Array arrayV1 = v1ss;
                for (int x = 0; x < arrayV1.GetLength(0); x++)
                {
                    for (int y = 0; y < arrayV1.GetLength(1); y++)
                    {
                        arrayV1.SetValue(v1, x, y);
                    }
                }

                Array arrayV2 = v2ss;
                for (int x = 0; x < arrayV2.GetLength(0); x++)
                {
                    for (int y = 0; y < arrayV2.GetLength(1); y++)
                    {
                        for (int z = 0; z < arrayV2.GetLength(2); z++)
                        {
                            arrayV2.SetValue(v2, x, y, z);
                        }
                    }
                }

                Array arrayV3 = v3ss;
                for (int x = 0; x < arrayV3.GetLength(0); x++)
                {
                    for (int y = 0; y < arrayV3.GetLength(1); y++)
                    {
                        arrayV3.SetValue(v3, x, y);
                    }
                }
            }
            oTime.Stop();
            DebugLogLine("原生反射 Multidimensional Array SetValue：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");

            oTime.Reset(); oTime.Start();
            for (int i = 0; i < testCount; i++)
            {
                ArrayWrapOutData data1 = arrayWrapV1.GetArrayData(v1ss);
                for (int x = 0; x < data1.arrayLengths[0]; x++)
                {
                    for (int y = 0; y < data1.arrayLengths[1]; y++)
                    {
                        arrayWrapV1.SetValue(data1.startItemOffcet, x * data1.arrayLengths[1] + y, v1);
                    }
                }

                ArrayWrapOutData data2 = arrayWrapV2.GetArrayData(v2ss);
                for (int x = 0; x < data2.arrayLengths[0]; x++)
                {
                    for (int y = 0; y < data2.arrayLengths[1]; y++)
                    {
                        for (int z = 0; z < data2.arrayLengths[2]; z++)
                        {
                            arrayWrapV2.SetValue(data2.startItemOffcet, (x * data2.arrayLengths[1] * data2.arrayLengths[2] + y * data2.arrayLengths[2] + z), v2);
                        }
                    }
                }

                ArrayWrapOutData data3 = arrayWrapV3.GetArrayData(v3ss);
                for (int x = 0; x < data3.arrayLengths[0]; x++)
                {
                    for (int y = 0; y < data3.arrayLengths[1]; y++)
                    {
                        arrayWrapV3.SetValue(data3.startItemOffcet, x * data3.arrayLengths[1] + y, v3);
                    }
                }
            }
            oTime.Stop();
            DebugLogLine("指针方法 Multidimensional ArrayWrapManager SetValue：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");



            oTime.Reset(); oTime.Start();
            for (int i = 0; i < testCount; i++)
            {
                ArrayWrapOutData data1 = arrayWrapV1.GetArrayData(v1ss);
                for (int x = 0; x < data1.arrayLengths[0]; x++)
                {
                    for (int y = 0; y < data1.arrayLengths[1]; y++)
                    {
                        UnsafeUtility.WriteArrayElement<string>(data1.startItemOffcet, (x * data1.arrayLengths[1] + y), v1);
                    }
                }

                ArrayWrapOutData data2 = arrayWrapV2.GetArrayData(v2ss);
                for (int x = 0; x < data2.arrayLengths[0]; x++)
                {
                    for (int y = 0; y < data2.arrayLengths[1]; y++)
                    {
                        for (int z = 0; z < data2.arrayLengths[2]; z++)
                        {
                            *(int*)(data2.startItemOffcet + arrayWrapV2.elementTypeSize * (x * data2.arrayLengths[1] * data2.arrayLengths[2] + y * data2.arrayLengths[2] + z))
                                = v2;
                        }
                    }
                }

                ArrayWrapOutData data3 = arrayWrapV3.GetArrayData(v3ss);
                for (int x = 0; x < data3.arrayLengths[0]; x++)
                {
                    for (int y = 0; y < data3.arrayLengths[1]; y++)
                    {
                        //UnsafeUtility.MemCpy((data3.startItemOffcet + arrayWrapV3.elementTypeSize * (x * data3.arrayLengths[1] + y)), UnsafeUtility.AddressOf(ref v3), arrayWrapV3.elementTypeSize);
                        *(Vector3*)(data3.startItemOffcet + arrayWrapV3.elementTypeSize * (x * data3.arrayLengths[1] + y)) = v3;
                    }
                }
            }
            oTime.Stop();
            DebugLogLine("指针方法 Multidimensional ArrayWrapManager SetValue 确定类型的：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");



            {
                ArrayWrapOutData data1 = arrayWrapV1.GetArrayData(v1ss);
                ArrayWrapOutData data2 = arrayWrapV2.GetArrayData(v2ss);
                ArrayWrapOutData data3 = arrayWrapV3.GetArrayData(v3ss);
                oTime.Reset(); oTime.Start();
                for (int i = 0; i < testCount; i++)
                {
                    for (int x = 0; x < data1.arrayLengths[0]; x++)
                    {
                        for (int y = 0; y < data1.arrayLengths[1]; y++)
                        {
                            UnsafeUtility.WriteArrayElement<string>(data1.startItemOffcet, (x * data1.arrayLengths[1] + y), v1);
                            //UnsafeTool.unsafeTool.SetObject((data1.startItemOffcet + arrayWrapV1.elementTypeSize * (x * data1.arrayLengths[1] + y)), v1);
                        }
                    }
                    for (int x = 0; x < data2.arrayLengths[0]; x++)
                    {
                        for (int y = 0; y < data2.arrayLengths[1]; y++)
                        {
                            for (int z = 0; z < data2.arrayLengths[2]; z++)
                            {
                                *(int*)(data2.startItemOffcet + arrayWrapV2.elementTypeSize * (x * data2.arrayLengths[1] * data2.arrayLengths[2] + y * data2.arrayLengths[2] + z)) = v2;
                            }
                        }
                    }
                    for (int x = 0; x < data3.arrayLengths[0]; x++)
                    {
                        for (int y = 0; y < data3.arrayLengths[1]; y++)
                        {
                            *(Vector3*)(data3.startItemOffcet + arrayWrapV3.elementTypeSize * (x * data3.arrayLengths[1] + y)) = v3;
                        }
                    }
                }
            }
            oTime.Stop();
            DebugLogLine("指针方法 Multidimensional ArrayWrapManager SetValue 忽略Data查询：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");

            oTime.Reset(); oTime.Start();
            for (int i = 0; i < testCount; i++)
            {
                for (int x = 0; x < v1ss.GetLength(0); x++)
                {
                    for (int y = 0; y < v1ss.GetLength(1); y++)
                    {
                        v1ss[x, y] = v1;
                    }
                }
                for (int x = 0; x < v2ss.GetLength(0); x++)
                {
                    for (int y = 0; y < v2ss.GetLength(1); y++)
                    {
                        for (int z = 0; z < v2ss.GetLength(2); z++)
                        {
                            v2ss[x, y, z] = v2;
                        }
                    }
                }
                for (int x = 0; x < v3ss.GetLength(0); x++)
                {
                    for (int y = 0; y < v3ss.GetLength(1); y++)
                    {
                        v3ss[x, y] = v3;
                    }
                }
            }
            oTime.Stop();
            DebugLogLine("原生 ：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");




            DebugLogLine("");
            DebugLogLine("=============↓↓↓CreateInstance↓↓↓===============");
            DebugLogLine("");

            oTime.Reset(); oTime.Start();
            for (int i = 0; i < testCount; i++)
            {
                a = (MyClass)System.Activator.CreateInstance(typeof(MyClass));
            }
            oTime.Stop();
            DebugLogLine("Activator CreateInstance：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");


            oTime.Reset(); oTime.Start();
            for (int i = 0; i < testCount; i++)
            {
                a = (MyClass)warp.Create();
            }
            oTime.Stop();
            DebugLogLine("指针方法 Create：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");


            oTime.Reset(); oTime.Start();
            for (int i = 0; i < testCount; i++)
            {
                a = new MyClass();
            }
            oTime.Stop();
            DebugLogLine(" new ：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");


            DebugLogLine("");
            DebugLogLine("=============↓↓↓Create Array↓↓↓===============");
            DebugLogLine("");


            arrayWrapV1 = ArrayWrapManager.GetIArrayWrap(typeof(string[]));
            arrayWrapV2 = ArrayWrapManager.GetIArrayWrap(typeof(int[]));
            arrayWrapV3 = ArrayWrapManager.GetIArrayWrap(typeof(Vector3[]));
            {
                oTime.Reset(); oTime.Start();
                for (int i = 0; i < testCount; i++)
                {
                    ArrayWrapOutData arrayWrapData1 = new ArrayWrapOutData
                    {
                        length = 5
                    };
                    v1s = (string[])arrayWrapV1.CreateArray(ref arrayWrapData1);

                    ArrayWrapOutData arrayWrapData2 = new ArrayWrapOutData
                    {
                        length = 10
                    };
                    v2s = (int[])arrayWrapV2.CreateArray(ref arrayWrapData2);

                    ArrayWrapOutData arrayWrapData3 = new ArrayWrapOutData
                    {
                        length = 4
                    };
                    v3s = (Vector3[])arrayWrapV3.CreateArray(ref arrayWrapData3);
                }
                oTime.Stop();
            }
            DebugLogLine("指针方法 创建数组：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");

            {
                oTime.Reset(); oTime.Start();
                for (int i = 0; i < testCount; i++)
                {
                    v1s = (string[])arrayWrapV1.CreateArray(5);
                    v2s = (int[])arrayWrapV2.CreateArray(10);
                    v3s = (Vector3[])arrayWrapV3.CreateArray(4);
                }
                oTime.Stop();
            }
            DebugLogLine("指针方法 创建数组 (不需要多余返回值)：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");

            oTime.Reset(); oTime.Start();
            for (int i = 0; i < testCount; i++)
            {
                v1s = (string[])Array.CreateInstance(typeof(string), 5);
                v2s = (int[])Array.CreateInstance(typeof(int), 10);
                v3s = (Vector3[])Array.CreateInstance(typeof(Vector3), 4);
            }
            oTime.Stop();
            DebugLogLine("原生反射 创建数组 ：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");

            oTime.Reset(); oTime.Start();
            for (int i = 0; i < testCount; i++)
            {
                v1s = new string[5];
                v2s = new int[10];
                v3s = new Vector3[4];
            }
            oTime.Stop();
            DebugLogLine("原生 new 数组 ：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");

            DebugLogLine("");


            arrayWrapV1 = ArrayWrapManager.GetIArrayWrap(v1ss.GetType());
            arrayWrapV2 = ArrayWrapManager.GetIArrayWrap(v2ss.GetType());
            arrayWrapV3 = ArrayWrapManager.GetIArrayWrap(v3ss.GetType());
            {
                oTime.Reset(); oTime.Start();
                for (int i = 0; i < testCount; i++)
                {
                    ArrayWrapOutData arrayWrapData1 = new ArrayWrapOutData();
                    arrayWrapData1.arrayLengths = new int[] { 2, 4 };
                    v1ss = (string[,])arrayWrapV1.CreateArray(ref arrayWrapData1);

                    ArrayWrapOutData arrayWrapData2 = new ArrayWrapOutData();
                    arrayWrapData2.arrayLengths = new int[] { 3, 3, 2 };
                    v2ss = (int[,,])arrayWrapV2.CreateArray(ref arrayWrapData2);

                    ArrayWrapOutData arrayWrapData3 = new ArrayWrapOutData();
                    arrayWrapData3.arrayLengths = new int[] { 3, 3 };
                    v3ss = (Vector3[,])arrayWrapV3.CreateArray(ref arrayWrapData3);
                }
                oTime.Stop();
            }
            DebugLogLine("指针方法 创建多维数组：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");


            oTime.Reset(); oTime.Start();
            for (int i = 0; i < testCount; i++)
            {
                v1ss = (string[,])Array.CreateInstance(typeof(string), new int[] { 2, 4 });
                v2ss = (int[,,])Array.CreateInstance(typeof(int), new int[] { 3, 3, 2 });
                v3ss = (Vector3[,])Array.CreateInstance(typeof(Vector3), new int[] { 3, 3 });
            }
            oTime.Stop();
            DebugLogLine("原生反射 创建多维数组 ：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");


            oTime.Reset(); oTime.Start();
            for (int i = 0; i < testCount; i++)
            {
                v1ss = new string[2, 4];
                v2ss = new int[3, 3, 2];
                v3ss = new Vector3[3, 3];
            }
            oTime.Stop();
            DebugLogLine("原生 new 多维数组 ：" + oTime.Elapsed.TotalMilliseconds + " 毫秒");







            text.text = sb.ToString();

            GC.Collect();
        }
    }



    //public static Type DynamicCreateType()
    //{
    //    //动态创建程序集
    //    AssemblyName DemoName = new AssemblyName("DynamicCustomReflection");
    //    AssemblyBuilder dynamicAssembly = AppDomain.CurrentDomain.DefineDynamicAssembly(DemoName, AssemblyBuilderAccess.RunAndSave);
    //    //动态创建模块
    //    ModuleBuilder mb = dynamicAssembly.DefineDynamicModule(DemoName.Name, DemoName.Name + ".dll");
    //    //动态创建类MyClass
    //    TypeBuilder tb = mb.DefineType("GeneralTool", TypeAttributes.Public);

    //    MethodBuilder mainMethodBuilder2 = tb.DefineMethod("SetObject", MethodAttributes.Static | MethodAttributes.Public, CallingConventions.Standard,
    //         null, new Type[] { typeof(void*), typeof(object) }
    //        );

    //    ILGenerator ilGenerator = mainMethodBuilder2.GetILGenerator();
    //    ilGenerator.Emit(OpCodes.Ldarg_0);
    //    ilGenerator.Emit(OpCodes.Ldarg_1);
    //    ilGenerator.Emit(OpCodes.Stobj, typeof(object));
    //    ilGenerator.Emit(OpCodes.Ret);

    //    MethodBuilder mainMethodBuilder5 = tb.DefineMethod("Memcpy", MethodAttributes.Static | MethodAttributes.Public, CallingConventions.Standard,
    //          null, new Type[] { typeof(void*), typeof(void*), typeof(int) }
    //        );

    //    ILGenerator ilGenerator5 = mainMethodBuilder5.GetILGenerator();
    //    ilGenerator5.Emit(OpCodes.Ldarg_0);
    //    ilGenerator5.Emit(OpCodes.Ldarg_1);
    //    ilGenerator5.Emit(OpCodes.Ldarg_2);
    //    ilGenerator5.Emit(OpCodes.Cpblk);
    //    ilGenerator5.Emit(OpCodes.Ret);
    //    mainMethodBuilder5.DefineParameter(1, ParameterAttributes.None, "destination");
    //    mainMethodBuilder5.DefineParameter(2, ParameterAttributes.None, "source");
    //    mainMethodBuilder5.DefineParameter(3, ParameterAttributes.None, "byteCount");

    //    //使用动态类创建类型
    //    Type classType = tb.CreateType();
    //    //保存动态创建的程序集 (程序集将保存在程序目录下调试时就在Debug下)
    //    dynamicAssembly.Save(DemoName.Name + ".dll");
    //    //创建类
    //    return classType;
    //}
     
    void Update()
    {

    }
}
