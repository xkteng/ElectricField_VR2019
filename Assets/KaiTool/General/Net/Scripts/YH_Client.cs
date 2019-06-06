using KaiTool.Json;
using KaiTool.Utilities;
using System;
using System.Collections;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace KaiTool.Net
{
    //public sealed class YH_Client : GameEventMonoBehaviour
    //{
    //    public const int SYNC_RATE = 50;
    //    public const int SUBSTEPS = 15;
    //    private const int BUFFER_SIZE = 1024;

    //    private static Socket s_socket;
    //    private static int s_id = -1;
    //    private static byte[] s_readBuff = new byte[BUFFER_SIZE];
    //    private static bool s_isConnected = false;
    //    private Coroutine m_sync_coroutine;
    //    #region PROPERTY
    //    public static bool IsConnected
    //    {
    //        get
    //        {
    //            return s_isConnected;
    //        }
    //    }
    //    public static int ID
    //    {
    //        get
    //        {
    //            return s_id;
    //        }
    //    }
    //    #endregion
    //    #region PUBLIC_METHODS
    //    #region SEND_MESSAGE
    //    public static void SendMessage(EventType type)
    //    {
    //        var str = type.ToString();
    //        Send(str);
    //    }
    //    public static void SendMessage<T>(EventType type, T t)
    //    {
    //        var str = new StringBuilder(type.ToString())
    //            .Append("/").Append(typeof(T)).Append("/").Append(ToJson(t));
    //        Send(str.ToString());
    //    }
    //    public static void SendMessage<T, V>(EventType type, T t, V v)
    //    {
    //        var str = new StringBuilder(type.ToString())
    //            .Append("/").Append(typeof(T)).Append("/").Append(ToJson(t))
    //            .Append("/").Append(typeof(V)).Append("/").Append(ToJson(v));
    //        Send(str.ToString());
    //    }
    //    public static void SendMessage<T, V, U>(EventType type, T t, V v, U u)
    //    {
    //        var str = new StringBuilder(type.ToString())
    //           .Append("/").Append(typeof(T)).Append("/").Append(ToJson(t))
    //           .Append("/").Append(typeof(V)).Append("/").Append(ToJson(v))
    //           .Append("/").Append(typeof(U)).Append("/").Append(ToJson(u));
    //        Send(str.ToString());
    //    }
    //    public static void SendMessage<T, V, U, W>(EventType type, T t, V v, U u, W w)
    //    {
    //        var str = new StringBuilder(type.ToString())
    //           .Append("/").Append(typeof(T)).Append("/").Append(ToJson(t))
    //           .Append("/").Append(typeof(V)).Append("/").Append(ToJson(v))
    //           .Append("/").Append(typeof(U)).Append("/").Append(ToJson(u))
    //           .Append("/").Append(typeof(W)).Append("/").Append(ToJson(w));
    //        Send(str.ToString());
    //    }
    //    public static void SendMessage<T, V, U, W, X>(EventType type, T t, V v, U u, W w, X x)
    //    {
    //        var str = new StringBuilder(type.ToString())
    //           .Append("/").Append(typeof(T)).Append("/").Append(ToJson(t))
    //           .Append("/").Append(typeof(V)).Append("/").Append(ToJson(v))
    //           .Append("/").Append(typeof(U)).Append("/").Append(ToJson(u))
    //           .Append("/").Append(typeof(W)).Append("/").Append(ToJson(w))
    //           .Append("/").Append(typeof(W)).Append("/").Append(ToJson(x));
    //        Send(str.ToString());
    //    }
    //    #endregion
    //    #endregion
    //    #region PRIVATE_PROTECTED_METHODS
    //    private MethodInfo[] m_broadcast_Methods = new MethodInfo[6];
    //    protected override void Awake()
    //    {
    //        base.Awake();
    //        var methods = typeof(EventManager).GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.Default);
    //        foreach (var item in methods)
    //        {
    //            if (item.Name == "LocalInvoke")
    //            {
    //                var args = item.GetGenericArguments();
    //                m_broadcast_Methods[args.Length] = item;
    //            }
    //        }
    //    }
    //    protected override void Start()
    //    {
    //        base.Start();
    //        BasicSynchronizer.SychronizeAll();
    //    }
    //    protected override void SubscribeEvent()
    //    {
    //        EventManager.AddListener<string, string>(EventType.PlayerTryConnect, OnPlayerTryConnect);
    //        EventManager.AddListener<int>(EventType.PlayerLinkSucceed, OnPlayerLinkSucceed);
    //        EventManager.AddListener(EventType.PlayerLinkFail, OnPlayerLinkFail);
    //        EventManager.AddListener<int>(EventType.ClientSocketClosed, OnClientSocketClosed);
    //        EventManager.AddListener<Vector3, Vector3, int>(EventType.PlayerInit, OnPlayerInit);
    //    }
    //    protected override void UnsubscribeEvent()
    //    {
    //        EventManager.RemoveListener<string, string>(EventType.PlayerTryConnect, OnPlayerTryConnect);
    //        EventManager.RemoveListener<int>(EventType.PlayerLinkSucceed, OnPlayerLinkSucceed);
    //        EventManager.RemoveListener(EventType.PlayerLinkFail, OnPlayerLinkFail);
    //        EventManager.RemoveListener<int>(EventType.ClientSocketClosed, OnClientSocketClosed);
    //        EventManager.RemoveListener<Vector3, Vector3, int>(EventType.PlayerInit, OnPlayerInit);
    //    }
    //    private void OnPlayerTryConnect(string ip_str, string port_str)
    //    {
    //        if (!s_isConnected)
    //        {
    //            int port;
    //            int.TryParse(port_str, out port);
    //            try
    //            {
    //                s_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    //                s_socket.Connect(ip_str, port);
    //                s_socket.BeginReceive(s_readBuff, 0, BUFFER_SIZE, SocketFlags.None, RecieveCb, null);
    //                s_isConnected = true;
    //            }
    //            catch (SocketException e)
    //            {
    //                print(e.Message);
    //                EventManager.LocalInvoke(EventType.PlayerLinkFail);
    //            }
    //        }
    //    }
    //    private void OnPlayerLinkSucceed(int id)
    //    {
    //        if (s_id == -1)
    //        {
    //            s_id = id;
    //        }
    //    }
    //    private void OnPlayerLinkFail()
    //    {
    //        s_isConnected = false;
    //        StopSync();
    //    }
    //    private void OnPlayerInit(Vector3 pos, Vector3 euler, int id)
    //    {
    //        StartSync();
    //        print("[玩家模型初始化]");
    //    }
    //    private void OnClientSocketClosed(int id)
    //    {
    //        if (id == s_id)
    //        {
    //            print("[已经断开连接]");//*********
    //            s_id = -1;
    //            s_readBuff = new byte[BUFFER_SIZE];
    //            s_isConnected = false;
    //            StopSync();
    //        }
    //    }
    //    private void RecieveCb(IAsyncResult ar)
    //    {
    //        try
    //        {
    //            int cout = s_socket.EndReceive(ar);
    //            string str = System.Text.Encoding.UTF8.GetString(s_readBuff, 0, cout);
    //            //print("[已接受]:" + str);//*************
    //            HandleMessage(str);
    //            s_socket.BeginReceive(s_readBuff, 0, BUFFER_SIZE, SocketFlags.None, RecieveCb, null);
    //        }
    //        catch (Exception e)
    //        {
    //            s_socket.Close();
    //            EventManager.LocalInvoke(EventType.ClientSocketClosed, s_id);
    //        }
    //    }
    //    private static void Send(string str)
    //    {
    //        var sb = new StringBuilder(str).Append("#");
    //        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
    //        s_socket.Send(bytes);
    //    }
    //    private void HandleMessage(string str)
    //    {
    //        var commands = str.Split('#');
    //        if (commands.Length > 1)
    //        {
    //            for (int i = 0; i < commands.Length; i++)
    //            {
    //                HandleMessage(commands[i]);
    //            }
    //        }
    //        else
    //        {
    //            var splits = str.Split('/');
    //            EventType EventType;
    //            if (splits.Length > 0)
    //            {
    //                if (Enum.TryParse<EventType>(splits[0], out EventType))
    //                {
    //                    switch (splits.Length)
    //                    {
    //                        case 1:
    //                            EventManager.LocalInvoke(EventType);
    //                            break;
    //                        case 3:
    //                            var t_3_0 = GetType(splits[1]);
    //                            m_broadcast_Methods[1].MakeGenericMethod(new Type[] { t_3_0 })
    //                            .Invoke(typeof(EventManager), new object[] {
    //                            EventType,
    //                            GetValue(t_3_0, splits[2]) });
    //                            break;
    //                        case 5:
    //                            var t_5_0 = GetType(splits[1]);
    //                            var t_5_1 = GetType(splits[3]);
    //                            m_broadcast_Methods[2].MakeGenericMethod(new Type[] { t_5_0, t_5_1 })
    //                                .Invoke(typeof(EventManager), new object[] {
    //                                EventType,
    //                                GetValue(t_5_0, splits[2]),
    //                                GetValue(t_5_1, splits[4]) });
    //                            break;
    //                        case 7:
    //                            var t_7_0 = GetType(splits[1]);
    //                            var t_7_1 = GetType(splits[3]);
    //                            var t_7_2 = GetType(splits[5]);
    //                            m_broadcast_Methods[3].MakeGenericMethod(new Type[] { t_7_0, t_7_1, t_7_2 })
    //                                .Invoke(typeof(EventManager), new object[] {
    //                                EventType,
    //                                GetValue(t_7_0,splits[2]),
    //                                GetValue(t_7_1,splits[4]),
    //                                GetValue(t_7_2,splits[6])
    //                                });
    //                            break;
    //                        case 9:
    //                            var t_9_0 = GetType(splits[1]);
    //                            var t_9_1 = GetType(splits[3]);
    //                            var t_9_2 = GetType(splits[5]);
    //                            var t_9_3 = GetType(splits[7]);
    //                            m_broadcast_Methods[4].MakeGenericMethod(new Type[] { t_9_0, t_9_1, t_9_2, t_9_3 })
    //                                .Invoke(typeof(EventManager), new object[] {
    //                                GetValue(t_9_0,splits[2]),
    //                                GetValue(t_9_1,splits[4]),
    //                                GetValue(t_9_2,splits[6]),
    //                                GetValue(t_9_3,splits[8])
    //                                });

    //                            break;
    //                        case 11:
    //                            var t_11_0 = GetType(splits[1]);
    //                            var t_11_1 = GetType(splits[3]);
    //                            var t_11_2 = GetType(splits[5]);
    //                            var t_11_3 = GetType(splits[7]);
    //                            var t_11_4 = GetType(splits[9]);
    //                            m_broadcast_Methods[5].MakeGenericMethod(new Type[] { t_11_0, t_11_1, t_11_2, t_11_3, t_11_4 })
    //                                .Invoke(typeof(EventManager), new object[] {
    //                                GetValue(t_11_0,splits[2]),
    //                                GetValue(t_11_1,splits[4]),
    //                                GetValue(t_11_2,splits[6]),
    //                                GetValue(t_11_3,splits[8]),
    //                                GetValue(t_11_4,splits[10])
    //                                });
    //                            break;
    //                        default:
    //                            throw new Exception("Wrong arguments number.");
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    private Type GetType(string name)
    //    {
    //        switch (name)
    //        {
    //            case "System.Int32":
    //                return typeof(int);
    //            case "System.Single":
    //                return typeof(float);
    //            case "System.Double":
    //                return typeof(double);
    //            case "System.String":
    //                return typeof(string);
    //            case "UnityEngine.Vector3":
    //                return typeof(Vector3);
    //            default:
    //                throw new Exception("There is no such type.");
    //        }
    //    }
    //    private object GetValue(Type type, string str)
    //    {
    //        str = str.Trim();
    //        switch (type.ToString())
    //        {
    //            case "System.Int32":
    //                int temp_int;
    //                int.TryParse(str, out temp_int);
    //                return temp_int;
    //            case "System.Single":
    //                float temp_float;
    //                float.TryParse(str, out temp_float);
    //                return temp_float;
    //            case "System.Double":
    //                double temp_double;
    //                double.TryParse(str, out temp_double);
    //                return temp_double;
    //            case "System.String":
    //                return str;
    //            default:
    //                var result = JsonUtility.FromJson(str, type);
    //                if (result == null)
    //                {
    //                    print("Value from json is null.");
    //                    switch (type.ToString())
    //                    {
    //                        case "UnityEngine.Vector3":
    //                            return Vector3.zero;
    //                        default:
    //                            break;
    //                    }
    //                }
    //                return result;
    //        }
    //    }
    //    private static string ToJson<T>(T t)
    //    {
    //        var type = typeof(T);
    //        if (type.IsPrimitive)
    //        {
    //            return t.ToString();
    //        }
    //        else
    //        {
    //            switch (type.ToString())
    //            {
    //                case "UnityEngine.Vector3":
    //                    var x = (float)type.GetField("x").GetValue(t);
    //                    var y = (float)type.GetField("y").GetValue(t);
    //                    var z = (float)type.GetField("z").GetValue(t);
    //                    var jsonVec = new KT_JsonVector3(x, y, z);
    //                    return jsonVec.ToJson();
    //                default:
    //                    return JsonUtility.ToJson(t);
    //            }
    //        }
    //    }
    //    protected override void OnDestroy()
    //    {
    //        base.OnDestroy();
    //        s_socket.Close();
    //    }
    //    #region SYNCHRONIZE
    //    private void StartSync()
    //    {
    //        if (m_sync_coroutine != null)
    //        {
    //            StopCoroutine(m_sync_coroutine);
    //        }
    //        m_sync_coroutine = StartCoroutine(Sychronizer_IEnumerator());
    //    }
    //    private void StopSync()
    //    {
    //        if (m_sync_coroutine != null)
    //        {
    //            StopCoroutine(m_sync_coroutine);
    //        }
    //    }
    //    private IEnumerator Sychronizer_IEnumerator()
    //    {
    //        var wait = new WaitForSeconds(1f / SYNC_RATE);
    //        while (true)
    //        {
    //            BasicSynchronizer.SychronizeAll();
    //            yield return wait;
    //        }
    //    }
    //    #endregion
    //    #endregion
    //}
}
