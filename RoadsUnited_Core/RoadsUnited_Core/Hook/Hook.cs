using ColossalFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RoadsUnited_Core
{
    public class Hook4 : MonoBehaviour
    {
        [CompilerGenerated]
        [Serializable]
        private sealed class <>c
		{
			public static readonly Hook4.<>c<>9 = new Hook4.<>c();

        public static Func<MethodInfo, bool> <>9__4_0;

			public static Func<MethodInfo, bool> <>9__4_1;

			public static Func<MethodInfo, bool> <>9__4_2;

			public static Func<MethodInfo, bool> <>9__4_3;

			public static Func<MethodInfo, bool> <>9__4_4;

			public static Func<MethodInfo, bool> <>9__4_5;

			internal bool <EnableHook>b__4_0(MethodInfo c)
        {
            return c.Name == "RenderInstance" && c.GetParameters().Length == 3;
        }

        internal bool <EnableHook>b__4_1(MethodInfo c)
        {
            return c.Name == "RenderLod";
        }

        internal bool <EnableHook>b__4_2(MethodInfo c)
        {
            return c.Name == "RenderInstance" && c.GetParameters().Length == 3;
        }

        internal bool <EnableHook>b__4_3(MethodInfo c)
        {
            return c.Name == "RenderInstanceNode" && c.GetParameters().Length == 3;
        }

        internal bool <EnableHook>b__4_4(MethodInfo c)
        {
            return c.Name == "RenderLod";
        }

        internal bool <EnableHook>b__4_5(MethodInfo c)
        {
            return c.Name == "RenderInstanceNode" && c.GetParameters().Length == 3;
        }
    }


    public bool hookEnabled = false;

    private Dictionary<MethodInfo, RedirectCallsState> redirects = new Dictionary<MethodInfo, RedirectCallsState>();

    public static Material invertedBridgeMat;

    public void Update()
    {
        bool flag = !this.hookEnabled && !ModLoader.exor;
        if (flag)
        {
            this.EnableHook();
        }
    }

    public void EnableHook()
    {
        BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
        IEnumerable<MethodInfo> arg_33_0 = typeof(NetSegment).GetMethods(bindingAttr);
        Func<MethodInfo, bool> arg_33_1;
        if ((arg_33_1 = Hook4.<> c.<> 9__4_0) == null)
			{
            arg_33_1 = (Hook4.<> c.<> 9__4_0 = new Func<MethodInfo, bool>(Hook4.<> c.<> 9.< EnableHook > b__4_0));
        }
        MethodInfo methodInfo = arg_33_0.Single(arg_33_1);
        this.redirects.Add(methodInfo, RedirectionHelper.RedirectCalls(methodInfo, typeof(Hook4).GetMethod("RenderInstanceSegment", bindingAttr)));
        IEnumerable<MethodInfo> arg_90_0 = typeof(NetSegment).GetMethods(bindingAttr);
        Func<MethodInfo, bool> arg_90_1;
        if ((arg_90_1 = Hook4.<> c.<> 9__4_1) == null)
			{
            arg_90_1 = (Hook4.<> c.<> 9__4_1 = new Func<MethodInfo, bool>(Hook4.<> c.<> 9.< EnableHook > b__4_1));
        }
        methodInfo = arg_90_0.Single(arg_90_1);
        this.redirects.Add(methodInfo, RedirectionHelper.RedirectCalls(methodInfo, typeof(Hook4).GetMethod("RenderInstanceSegment", bindingAttr)));
        IEnumerable<MethodInfo> arg_ED_0 = typeof(NetNode).GetMethods(bindingAttr);
        Func<MethodInfo, bool> arg_ED_1;
        if ((arg_ED_1 = Hook4.<> c.<> 9__4_2) == null)
			{
            arg_ED_1 = (Hook4.<> c.<> 9__4_2 = new Func<MethodInfo, bool>(Hook4.<> c.<> 9.< EnableHook > b__4_2));
        }
        methodInfo = arg_ED_0.Single(arg_ED_1);
        Dictionary<MethodInfo, RedirectCallsState> arg_134_0 = this.redirects;
        MethodInfo arg_134_1 = methodInfo;
        MethodInfo arg_12F_0 = methodInfo;
        IEnumerable<MethodInfo> arg_12A_0 = typeof(Hook4).GetMethods(bindingAttr);
        Func<MethodInfo, bool> arg_12A_1;
        if ((arg_12A_1 = Hook4.<> c.<> 9__4_3) == null)
			{
            arg_12A_1 = (Hook4.<> c.<> 9__4_3 = new Func<MethodInfo, bool>(Hook4.<> c.<> 9.< EnableHook > b__4_3));
        }
        arg_134_0.Add(arg_134_1, RedirectionHelper.RedirectCalls(arg_12F_0, arg_12A_0.Single(arg_12A_1)));
        IEnumerable<MethodInfo> arg_169_0 = typeof(NetNode).GetMethods(bindingAttr);
        Func<MethodInfo, bool> arg_169_1;
        if ((arg_169_1 = Hook4.<> c.<> 9__4_4) == null)
			{
            arg_169_1 = (Hook4.<> c.<> 9__4_4 = new Func<MethodInfo, bool>(Hook4.<> c.<> 9.< EnableHook > b__4_4));
        }
        methodInfo = arg_169_0.Single(arg_169_1);
        Dictionary<MethodInfo, RedirectCallsState> arg_1B0_0 = this.redirects;
        MethodInfo arg_1B0_1 = methodInfo;
        MethodInfo arg_1AB_0 = methodInfo;
        IEnumerable<MethodInfo> arg_1A6_0 = typeof(Hook4).GetMethods(bindingAttr);
        Func<MethodInfo, bool> arg_1A6_1;
        if ((arg_1A6_1 = Hook4.<> c.<> 9__4_5) == null)
			{
            arg_1A6_1 = (Hook4.<> c.<> 9__4_5 = new Func<MethodInfo, bool>(Hook4.<> c.<> 9.< EnableHook > b__4_5));
        }
        arg_1B0_0.Add(arg_1B0_1, RedirectionHelper.RedirectCalls(arg_1AB_0, arg_1A6_0.Single(arg_1A6_1)));
        this.hookEnabled = true;
    }

    private MethodInfo GetMethod(string name, uint argCount)
    {
        MethodInfo[] methods = typeof(NetNode).GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        MethodInfo[] array = methods;
        MethodInfo result;
        for (int i = 0; i < array.Length; i++)
        {
            MethodInfo methodInfo = array[i];
            bool flag = methodInfo.Name == name && (long)methodInfo.GetParameters().Length == (long)((ulong)argCount);
            if (flag)
            {
                result = methodInfo;
                return result;
            }
        }
        result = null;
        return result;
    }

    public void DisableHook()
    {
        bool flag = !this.hookEnabled;
        if (!flag)
        {
            foreach (KeyValuePair<MethodInfo, RedirectCallsState> current in this.redirects)
            {
                RedirectionHelper.RevertRedirect(current.Key, current.Value);
            }
            this.redirects.Clear();
            this.hookEnabled = false;
        }
    }

    private void RefreshJunctionData(NetNode netnode, ushort nodeID, NetInfo info, uint instanceIndex)
    {
        MethodInfo method = this.GetMethod("RefreshJunctionData", 3u);
        object[] parameters = new object[]
        {
                nodeID,
                info,
                instanceIndex
        };
        method.Invoke(netnode, parameters);
    }

    private void RefreshBendData(NetNode netnode, ushort nodeID, NetInfo info, uint instanceIndex, ref RenderManager.Instance data)
    {
        MethodInfo method = this.GetMethod("RefreshBendData", 4u);
        object[] array = new object[]
        {
                nodeID,
                info,
                instanceIndex,
                data
        };
        method.Invoke(netnode, array);
        data = (RenderManager.Instance)array[3];
    }

    private void RefreshJunctionData(NetNode netnode, ushort nodeID, int segmentIndex, ushort nodeSegment, Vector3 centerPos, ref uint instanceIndex, ref RenderManager.Instance data)
    {
        NetManager instance = Singleton<NetManager>.get_instance();
        data.m_position = netnode.m_position;
        data.m_rotation = Quaternion.identity;
        data.m_initialized = true;
        float vScale = 0.05f;
        Vector3 zero = Vector3.zero;
        Vector3 zero2 = Vector3.zero;
        Vector3 zero3 = Vector3.zero;
        Vector3 zero4 = Vector3.zero;
        Vector3 zero5 = Vector3.zero;
        Vector3 zero6 = Vector3.zero;
        Vector3 zero7 = Vector3.zero;
        Vector3 zero8 = Vector3.zero;
        Vector3 zero9 = Vector3.zero;
        Vector3 zero10 = Vector3.zero;
        Vector3 zero11 = Vector3.zero;
        Vector3 zero12 = Vector3.zero;
        NetSegment netSegment = instance.m_segments.m_buffer[(int)nodeSegment];
        NetInfo info = netSegment.Info;
        ItemClass connectionClass = info.GetConnectionClass();
        Vector3 vector = (nodeID != netSegment.m_startNode) ? netSegment.m_endDirection : netSegment.m_startDirection;
        float num = -4f;
        float num2 = -4f;
        ushort num3 = 0;
        ushort num4 = 0;
        int num8;
        for (int i = 0; i < 8; i = num8)
        {
            ushort segment = netnode.GetSegment(i);
            bool flag = segment != 0 && segment != nodeSegment;
            if (flag)
            {
                ItemClass connectionClass2 = instance.m_segments.m_buffer[(int)segment].Info.GetConnectionClass();
                bool flag2 = connectionClass.m_service == connectionClass2.m_service;
                if (flag2)
                {
                    NetSegment netSegment2 = instance.m_segments.m_buffer[(int)segment];
                    Vector3 vector2 = (nodeID != netSegment2.m_startNode) ? netSegment2.m_endDirection : netSegment2.m_startDirection;
                    float num5 = (float)((double)vector.x * (double)vector2.x + (double)vector.z * (double)vector2.z);
                    bool flag3 = (double)vector2.z * (double)vector.x - (double)vector2.x * (double)vector.z < 0.0;
                    if (flag3)
                    {
                        bool flag4 = (double)num5 > (double)num;
                        if (flag4)
                        {
                            num = num5;
                            num3 = segment;
                        }
                        float num6 = -2f - num5;
                        bool flag5 = (double)num6 > (double)num2;
                        if (flag5)
                        {
                            num2 = num6;
                            num4 = segment;
                        }
                    }
                    else
                    {
                        bool flag6 = (double)num5 > (double)num2;
                        if (flag6)
                        {
                            num2 = num5;
                            num4 = segment;
                        }
                        float num7 = -2f - num5;
                        bool flag7 = (double)num7 > (double)num;
                        if (flag7)
                        {
                            num = num7;
                            num3 = segment;
                        }
                    }
                }
            }
            num8 = i + 1;
        }
        bool start = netSegment.m_startNode == nodeID;
        bool flag8;
        netSegment.CalculateCorner(nodeSegment, true, start, false, out zero, out zero3, out flag8);
        netSegment.CalculateCorner(nodeSegment, true, start, true, out zero2, out zero4, out flag8);
        bool flag9 = num3 != 0 && num4 > 0;
        if (flag9)
        {
            float num9 = (float)((double)info.m_pavementWidth / (double)info.m_halfWidth * 0.5);
            float y = 1f;
            bool flag10 = num3 > 0;
            if (flag10)
            {
                NetSegment netSegment3 = instance.m_segments.m_buffer[(int)num3];
                NetInfo info2 = netSegment3.Info;
                bool start2 = netSegment3.m_startNode == nodeID;
                netSegment3.CalculateCorner(num3, true, start2, true, out zero5, out zero7, out flag8);
                netSegment3.CalculateCorner(num3, true, start2, false, out zero6, out zero8, out flag8);
                float num10 = (float)((double)info2.m_pavementWidth / (double)info2.m_halfWidth * 0.5);
                num9 = (float)(((double)num9 + (double)num10) * 0.5);
                y = (float)(2.0 * (double)info.m_halfWidth / ((double)info.m_halfWidth + (double)info2.m_halfWidth));
            }
            float num11 = (float)((double)info.m_pavementWidth / (double)info.m_halfWidth * 0.5);
            float w = 1f;
            bool flag11 = num4 > 0;
            if (flag11)
            {
                NetSegment netSegment4 = instance.m_segments.m_buffer[(int)num4];
                NetInfo info3 = netSegment4.Info;
                bool start3 = netSegment4.m_startNode == nodeID;
                netSegment4.CalculateCorner(num4, true, start3, true, out zero9, out zero11, out flag8);
                netSegment4.CalculateCorner(num4, true, start3, false, out zero10, out zero12, out flag8);
                float num12 = (float)((double)info3.m_pavementWidth / (double)info3.m_halfWidth * 0.5);
                num11 = (float)(((double)num11 + (double)num12) * 0.5);
                w = (float)(2.0 * (double)info.m_halfWidth / ((double)info.m_halfWidth + (double)info3.m_halfWidth));
            }
            Vector3 vector3;
            Vector3 vector4;
            NetSegment.CalculateMiddlePoints(zero, -zero3, zero5, -zero7, true, true, out vector3, out vector4);
            Vector3 vector5;
            Vector3 vector6;
            NetSegment.CalculateMiddlePoints(zero2, -zero4, zero6, -zero8, true, true, out vector5, out vector6);
            Vector3 vector7;
            Vector3 vector8;
            NetSegment.CalculateMiddlePoints(zero, -zero3, zero9, -zero11, true, true, out vector7, out vector8);
            Vector3 vector9;
            Vector3 vector10;
            NetSegment.CalculateMiddlePoints(zero2, -zero4, zero10, -zero12, true, true, out vector9, out vector10);
            data.m_dataMatrix0 = NetSegment.CalculateControlMatrix(zero, vector3, vector4, zero5, zero, vector3, vector4, zero5, netnode.m_position, vScale);
            data.m_extraData.m_dataMatrix2 = NetSegment.CalculateControlMatrix(zero2, vector5, vector6, zero6, zero2, vector5, vector6, zero6, netnode.m_position, vScale);
            data.m_extraData.m_dataMatrix3 = NetSegment.CalculateControlMatrix(zero, vector7, vector8, zero9, zero, vector7, vector8, zero9, netnode.m_position, vScale);
            data.m_dataMatrix1 = NetSegment.CalculateControlMatrix(zero2, vector9, vector10, zero10, zero2, vector9, vector10, zero10, netnode.m_position, vScale);
            data.m_dataVector0 = new Vector4(0.5f / info.m_halfWidth, 1f / info.m_segmentLength, (float)(0.5 - (double)info.m_pavementWidth / (double)info.m_halfWidth * 0.5), (float)((double)info.m_pavementWidth / (double)info.m_halfWidth * 0.5));
            data.m_dataVector1 = centerPos - data.m_position;
            data.m_dataVector1.w = (float)(((double)data.m_dataMatrix0.m33 + (double)data.m_extraData.m_dataMatrix2.m33 + (double)data.m_extraData.m_dataMatrix3.m33 + (double)data.m_dataMatrix1.m33) * 0.25);
            data.m_dataVector2 = new Vector4(num9, y, num11, w);
            data.m_extraData.m_dataVector4 = RenderManager.GetColorLocation(86016u + (uint)nodeID);
        }
        else
        {
            centerPos.x = (float)(((double)zero.x + (double)zero2.x) * 0.5);
            centerPos.z = (float)(((double)zero.z + (double)zero2.z) * 0.5);
            Vector3 vector11 = zero2;
            Vector3 vector12 = zero;
            Vector3 a = zero4;
            Vector3 a2 = zero3;
            float d = Mathf.Min(info.m_halfWidth * 1.333333f, 16f);
            Vector3 vector13 = zero - zero3 * d;
            Vector3 vector14 = vector11 - a * d;
            Vector3 vector15 = zero2 - zero4 * d;
            Vector3 vector16 = vector12 - a2 * d;
            Vector3 vector17 = zero + zero3 * d;
            Vector3 vector18 = vector11 + a * d;
            Vector3 vector19 = zero2 + zero4 * d;
            Vector3 vector20 = vector12 + a2 * d;
            data.m_dataMatrix0 = NetSegment.CalculateControlMatrix(zero, vector13, vector14, vector11, zero, vector13, vector14, vector11, netnode.m_position, vScale);
            data.m_extraData.m_dataMatrix2 = NetSegment.CalculateControlMatrix(zero2, vector19, vector20, vector12, zero2, vector19, vector20, vector12, netnode.m_position, vScale);
            data.m_extraData.m_dataMatrix3 = NetSegment.CalculateControlMatrix(zero, vector17, vector18, vector11, zero, vector17, vector18, vector11, netnode.m_position, vScale);
            data.m_dataMatrix1 = NetSegment.CalculateControlMatrix(zero2, vector15, vector16, vector12, zero2, vector15, vector16, vector12, netnode.m_position, vScale);
            data.m_dataMatrix0.SetRow(3, data.m_dataMatrix0.GetRow(3) + new Vector4(0.2f, 0.2f, 0.2f, 0.2f));
            data.m_extraData.m_dataMatrix2.SetRow(3, data.m_extraData.m_dataMatrix2.GetRow(3) + new Vector4(0.2f, 0.2f, 0.2f, 0.2f));
            data.m_extraData.m_dataMatrix3.SetRow(3, data.m_extraData.m_dataMatrix3.GetRow(3) + new Vector4(0.2f, 0.2f, 0.2f, 0.2f));
            data.m_dataMatrix1.SetRow(3, data.m_dataMatrix1.GetRow(3) + new Vector4(0.2f, 0.2f, 0.2f, 0.2f));
            data.m_dataVector0 = new Vector4(0.5f / info.m_halfWidth, 1f / info.m_segmentLength, (float)(0.5 - (double)info.m_pavementWidth / (double)info.m_halfWidth * 0.5), (float)((double)info.m_pavementWidth / (double)info.m_halfWidth * 0.5));
            data.m_dataVector1 = centerPos - data.m_position;
            data.m_dataVector1.w = (float)(((double)data.m_dataMatrix0.m33 + (double)data.m_extraData.m_dataMatrix2.m33 + (double)data.m_extraData.m_dataMatrix3.m33 + (double)data.m_dataMatrix1.m33) * 0.25);
            data.m_dataVector2 = new Vector4((float)((double)info.m_pavementWidth / (double)info.m_halfWidth * 0.5), 1f, (float)((double)info.m_pavementWidth / (double)info.m_halfWidth * 0.5), 1f);
            data.m_extraData.m_dataVector4 = RenderManager.GetColorLocation(86016u + (uint)nodeID);
        }
        data.m_dataInt0 = segmentIndex;
        data.m_dataColor0 = info.m_color;
        data.m_dataColor0.a = 0f;
        bool requireSurfaceMaps = info.m_requireSurfaceMaps;
        if (requireSurfaceMaps)
        {
            Singleton<TerrainManager>.get_instance().GetSurfaceMapping(data.m_position, out data.m_dataTexture0, out data.m_dataTexture1, out data.m_dataVector3);
        }
        instanceIndex = (uint)data.m_nextInstance;
    }

    private void RefreshJunctionData(NetNode netnode, ushort nodeID, int segmentIndex, NetInfo info, ushort nodeSegment, ushort nodeSegment2, ref uint instanceIndex, ref RenderManager.Instance data)
    {
        data.m_position = netnode.m_position;
        data.m_rotation = Quaternion.identity;
        data.m_initialized = true;
        float vScale = 0.05f;
        Vector3 zero = Vector3.zero;
        Vector3 zero2 = Vector3.zero;
        Vector3 zero3 = Vector3.zero;
        Vector3 zero4 = Vector3.zero;
        Vector3 zero5 = Vector3.zero;
        Vector3 zero6 = Vector3.zero;
        Vector3 zero7 = Vector3.zero;
        Vector3 zero8 = Vector3.zero;
        bool start = Singleton<NetManager>.get_instance().m_segments.m_buffer[(int)nodeSegment].m_startNode == nodeID;
        bool flag;
        Singleton<NetManager>.get_instance().m_segments.m_buffer[(int)nodeSegment].CalculateCorner(nodeSegment, true, start, false, out zero, out zero5, out flag);
        Singleton<NetManager>.get_instance().m_segments.m_buffer[(int)nodeSegment].CalculateCorner(nodeSegment, true, start, true, out zero2, out zero6, out flag);
        bool start2 = Singleton<NetManager>.get_instance().m_segments.m_buffer[(int)nodeSegment2].m_startNode == nodeID;
        Singleton<NetManager>.get_instance().m_segments.m_buffer[(int)nodeSegment2].CalculateCorner(nodeSegment2, true, start2, true, out zero3, out zero7, out flag);
        Singleton<NetManager>.get_instance().m_segments.m_buffer[(int)nodeSegment2].CalculateCorner(nodeSegment2, true, start2, false, out zero4, out zero8, out flag);
        Vector3 vector;
        Vector3 vector2;
        NetSegment.CalculateMiddlePoints(zero, -zero5, zero3, -zero7, true, true, out vector, out vector2);
        Vector3 vector3;
        Vector3 vector4;
        NetSegment.CalculateMiddlePoints(zero2, -zero6, zero4, -zero8, true, true, out vector3, out vector4);
        data.m_dataMatrix0 = NetSegment.CalculateControlMatrix(zero, vector, vector2, zero3, zero2, vector3, vector4, zero4, netnode.m_position, vScale);
        data.m_extraData.m_dataMatrix2 = NetSegment.CalculateControlMatrix(zero2, vector3, vector4, zero4, zero, vector, vector2, zero3, netnode.m_position, vScale);
        data.m_dataVector0 = new Vector4(0.5f / info.m_halfWidth, 1f / info.m_segmentLength, 1f, 1f);
        data.m_dataVector3 = RenderManager.GetColorLocation(86016u + (uint)nodeID);
        data.m_dataInt0 = (8 | segmentIndex);
        data.m_dataColor0 = info.m_color;
        data.m_dataColor0.a = 0f;
        bool requireSurfaceMaps = info.m_requireSurfaceMaps;
        if (requireSurfaceMaps)
        {
            Singleton<TerrainManager>.get_instance().GetSurfaceMapping(data.m_position, out data.m_dataTexture0, out data.m_dataTexture1, out data.m_dataVector1);
        }
        instanceIndex = (uint)data.m_nextInstance;
    }

    private int CalculateRendererCount(NetNode netnode, NetInfo info)
    {
        bool flag = (netnode.m_flags & NetNode.Flags.Junction) == NetNode.Flags.None;
        int result;
        if (flag)
        {
            result = 1;
        }
        else
        {
            int num = 0;
            bool requireSegmentRenderers = info.m_requireSegmentRenderers;
            if (requireSegmentRenderers)
            {
                num += netnode.CountSegments();
            }
            bool requireDirectRenderers = info.m_requireDirectRenderers;
            if (requireDirectRenderers)
            {
                num += (int)netnode.m_connectCount;
            }
            result = num;
        }
        return result;
    }

    public void RenderInstanceNode(RenderManager.CameraInfo cameraInfo, ushort nodeID, int layerMask)
    {
        NetManager instance = Singleton<NetManager>.get_instance();
        NetNode netNode = instance.m_nodes.m_buffer[(int)nodeID];
        bool flag = netNode.m_flags == NetNode.Flags.None;
        if (!flag)
        {
            NetInfo info = netNode.Info;
            bool flag2 = !cameraInfo.Intersect(netNode.m_bounds);
            if (!flag2)
            {
                bool flag3 = netNode.m_problems != Notification.Problem.None && (layerMask & 1 << Singleton<NotificationManager>.get_instance().m_notificationLayer) != 0;
                if (flag3)
                {
                    Vector3 position = netNode.m_position;
                    position.y += Mathf.Max(5f, info.m_maxHeight);
                    Notification.RenderInstance(cameraInfo, netNode.m_problems, position, 1f);
                }
                bool flag4 = (layerMask & info.m_netLayers) == 0 || (netNode.m_flags & (NetNode.Flags.End | NetNode.Flags.Bend | NetNode.Flags.Junction)) == NetNode.Flags.None;
                if (!flag4)
                {
                    bool flag5 = (netNode.m_flags & NetNode.Flags.Bend) > NetNode.Flags.None;
                    if (flag5)
                    {
                        bool flag6 = info.m_segments == null || info.m_segments.Length == 0;
                        if (flag6)
                        {
                            return;
                        }
                    }
                    else
                    {
                        bool flag7 = info.m_nodes == null || info.m_nodes.Length == 0;
                        if (flag7)
                        {
                            return;
                        }
                    }
                    uint count = (uint)this.CalculateRendererCount(netNode, info);
                    RenderManager instance2 = Singleton<RenderManager>.get_instance();
                    uint num;
                    bool flag8 = !instance2.RequireInstance(86016u + (uint)nodeID, count, out num);
                    if (!flag8)
                    {
                        int num2 = 0;
                        while (num != 65535u)
                        {
                            this.RenderInstanceNode(cameraInfo, nodeID, info, num2, netNode.m_flags, ref num, ref instance2.m_instances[(int)num]);
                            int num3 = num2 + 1;
                            num2 = num3;
                            bool flag9 = num3 > 36;
                            if (flag9)
                            {
                                CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                                break;
                            }
                        }
                    }
                }
            }
        }
    }

    public void RenderInstanceNode(RenderManager.CameraInfo cameraInfo, ushort nodeID, NetInfo info, int iter, NetNode.Flags flags, ref uint instanceIndex, ref RenderManager.Instance data)
    {
        NetManager instance = Singleton<NetManager>.get_instance();
        NetNode netnode = instance.m_nodes.m_buffer[(int)nodeID];
        bool dirty = data.m_dirty;
        if (dirty)
        {
            data.m_dirty = false;
            bool flag = iter == 0;
            if (flag)
            {
                bool flag2 = (flags & NetNode.Flags.Junction) > NetNode.Flags.None;
                if (flag2)
                {
                    this.RefreshJunctionData(netnode, nodeID, info, instanceIndex);
                }
                else
                {
                    bool flag3 = (flags & NetNode.Flags.Bend) > NetNode.Flags.None;
                    if (flag3)
                    {
                        this.RefreshBendData(netnode, nodeID, info, instanceIndex, ref data);
                    }
                    else
                    {
                        bool flag4 = (flags & NetNode.Flags.End) > NetNode.Flags.None;
                        if (flag4)
                        {
                            this.RefreshEndData(netnode, nodeID, info, instanceIndex, ref data);
                        }
                    }
                }
            }
        }
        bool initialized = data.m_initialized;
        if (initialized)
        {
            bool flag5 = (flags & NetNode.Flags.Junction) > NetNode.Flags.None;
            if (flag5)
            {
                bool flag6 = (data.m_dataInt0 & 8) != 0;
                if (flag6)
                {
                    ushort segment = netnode.GetSegment(data.m_dataInt0 & 7);
                    ushort segment2 = netnode.GetSegment(data.m_dataInt0 >> 4);
                    bool flag7 = segment != 0 && segment2 > 0;
                    if (flag7)
                    {
                        NetManager instance2 = Singleton<NetManager>.get_instance();
                        info = instance2.m_segments.m_buffer[(int)segment].Info;
                        NetInfo info2 = instance2.m_segments.m_buffer[(int)segment2].Info;
                        int i = 0;
                        while (i < info.m_nodes.Length)
                        {
                            NetInfo.Node node = info.m_nodes[i];
                            bool flag8 = node.CheckFlags(flags) && node.m_directConnect && (node.m_connectGroup == NetInfo.ConnectGroup.None || (node.m_connectGroup & info2.m_connectGroup & NetInfo.ConnectGroup.AllGroups) > NetInfo.ConnectGroup.None);
                            int num;
                            if (flag8)
                            {
                                Vector4 dataVector = data.m_dataVector3;
                                Vector4 dataVector2 = data.m_dataVector0;
                                bool requireWindSpeed = node.m_requireWindSpeed;
                                if (requireWindSpeed)
                                {
                                    dataVector.w = data.m_dataFloat0;
                                }
                                bool flag9 = (node.m_connectGroup & NetInfo.ConnectGroup.Oneway) > NetInfo.ConnectGroup.None;
                                if (flag9)
                                {
                                    bool flag10 = instance2.m_segments.m_buffer[(int)segment].m_startNode == nodeID == ((instance2.m_segments.m_buffer[(int)segment].m_flags & NetSegment.Flags.Invert) == NetSegment.Flags.None);
                                    bool flag11 = info2.m_hasBackwardVehicleLanes != info2.m_hasForwardVehicleLanes;
                                    if (flag11)
                                    {
                                        bool flag12 = instance2.m_segments.m_buffer[(int)segment2].m_startNode == nodeID == ((instance2.m_segments.m_buffer[(int)segment2].m_flags & NetSegment.Flags.Invert) == NetSegment.Flags.None);
                                        bool flag13 = flag10 == flag12;
                                        if (flag13)
                                        {
                                            goto IL_440;
                                        }
                                    }
                                    bool flag14 = flag10;
                                    if (flag14)
                                    {
                                        bool flag15 = (node.m_connectGroup & NetInfo.ConnectGroup.OnewayStart) == NetInfo.ConnectGroup.None;
                                        if (flag15)
                                        {
                                            goto IL_440;
                                        }
                                    }
                                    else
                                    {
                                        bool flag16 = (node.m_connectGroup & NetInfo.ConnectGroup.OnewayEnd) > NetInfo.ConnectGroup.None;
                                        if (!flag16)
                                        {
                                            goto IL_440;
                                        }
                                        dataVector2.x = -dataVector2.x;
                                        dataVector2.y = -dataVector2.y;
                                    }
                                }
                                instance2.m_materialBlock.Clear();
                                instance2.m_materialBlock.AddMatrix(instance2.ID_LeftMatrix, data.m_dataMatrix0);
                                instance2.m_materialBlock.AddMatrix(instance2.ID_RightMatrix, data.m_extraData.m_dataMatrix2);
                                instance2.m_materialBlock.AddVector(instance2.ID_MeshScale, dataVector2);
                                instance2.m_materialBlock.AddVector(instance2.ID_ObjectIndex, dataVector);
                                instance2.m_materialBlock.AddColor(instance2.ID_Color, data.m_dataColor0);
                                bool flag17 = node.m_requireSurfaceMaps && data.m_dataTexture1 != null;
                                if (flag17)
                                {
                                    instance2.m_materialBlock.AddTexture(instance2.ID_SurfaceTexA, data.m_dataTexture0);
                                    instance2.m_materialBlock.AddTexture(instance2.ID_SurfaceTexB, data.m_dataTexture1);
                                    instance2.m_materialBlock.AddVector(instance2.ID_SurfaceMapping, data.m_dataVector1);
                                }
                                NetManager var_30_3FE_cp_0_cp_0 = instance2;
                                num = var_30_3FE_cp_0_cp_0.m_drawCallData.m_defaultCalls + 1;
                                var_30_3FE_cp_0_cp_0.m_drawCallData.m_defaultCalls = num;
                                Graphics.DrawMesh(node.m_nodeMesh, data.m_position, data.m_rotation, node.m_nodeMaterial, node.m_layer, null, 0, instance2.m_materialBlock);
                            }
                            IL_440:
                            num = i + 1;
                            i = num;
                            continue;
                            goto IL_440;
                        }
                    }
                }
                else
                {
                    ushort segment3 = netnode.GetSegment(data.m_dataInt0 & 7);
                    bool flag18 = segment3 > 0;
                    if (flag18)
                    {
                        NetManager instance3 = Singleton<NetManager>.get_instance();
                        info = instance3.m_segments.m_buffer[(int)segment3].Info;
                        int num;
                        for (int j = 0; j < info.m_nodes.Length; j = num)
                        {
                            NetInfo.Node node2 = info.m_nodes[j];
                            bool flag19 = node2.CheckFlags(flags) && !node2.m_directConnect;
                            if (flag19)
                            {
                                Vector4 dataVector3 = data.m_extraData.m_dataVector4;
                                bool requireWindSpeed2 = node2.m_requireWindSpeed;
                                if (requireWindSpeed2)
                                {
                                    dataVector3.w = data.m_dataFloat0;
                                }
                                instance3.m_materialBlock.Clear();
                                instance3.m_materialBlock.AddMatrix(instance3.ID_LeftMatrix, data.m_dataMatrix0);
                                instance3.m_materialBlock.AddMatrix(instance3.ID_RightMatrix, data.m_extraData.m_dataMatrix2);
                                instance3.m_materialBlock.AddMatrix(instance3.ID_LeftMatrixB, data.m_extraData.m_dataMatrix3);
                                instance3.m_materialBlock.AddMatrix(instance3.ID_RightMatrixB, data.m_dataMatrix1);
                                instance3.m_materialBlock.AddVector(instance3.ID_MeshScale, data.m_dataVector0);
                                instance3.m_materialBlock.AddVector(instance3.ID_CenterPos, data.m_dataVector1);
                                instance3.m_materialBlock.AddVector(instance3.ID_SideScale, data.m_dataVector2);
                                instance3.m_materialBlock.AddVector(instance3.ID_ObjectIndex, dataVector3);
                                instance3.m_materialBlock.AddColor(instance3.ID_Color, data.m_dataColor0);
                                bool flag20 = node2.m_requireSurfaceMaps && data.m_dataTexture1 != null;
                                if (flag20)
                                {
                                    instance3.m_materialBlock.AddTexture(instance3.ID_SurfaceTexA, data.m_dataTexture0);
                                    instance3.m_materialBlock.AddTexture(instance3.ID_SurfaceTexB, data.m_dataTexture1);
                                    instance3.m_materialBlock.AddVector(instance3.ID_SurfaceMapping, data.m_dataVector3);
                                }
                                NetManager var_30_68B_cp_0_cp_0 = instance3;
                                num = var_30_68B_cp_0_cp_0.m_drawCallData.m_defaultCalls + 1;
                                var_30_68B_cp_0_cp_0.m_drawCallData.m_defaultCalls = num;
                                Graphics.DrawMesh(node2.m_nodeMesh, data.m_position, data.m_rotation, node2.m_nodeMaterial, node2.m_layer, null, 0, instance3.m_materialBlock);
                            }
                            num = j + 1;
                        }
                    }
                }
            }
            else
            {
                bool flag21 = (flags & NetNode.Flags.End) > NetNode.Flags.None;
                if (flag21)
                {
                    NetManager instance4 = Singleton<NetManager>.get_instance();
                    int num;
                    for (int k = 0; k < info.m_nodes.Length; k = num)
                    {
                        NetInfo.Node node3 = info.m_nodes[k];
                        bool flag22 = node3.CheckFlags(flags) && !node3.m_directConnect;
                        if (flag22)
                        {
                            Vector4 dataVector4 = data.m_extraData.m_dataVector4;
                            bool requireWindSpeed3 = node3.m_requireWindSpeed;
                            if (requireWindSpeed3)
                            {
                                dataVector4.w = data.m_dataFloat0;
                            }
                            instance4.m_materialBlock.Clear();
                            instance4.m_materialBlock.AddMatrix(instance4.ID_LeftMatrix, data.m_dataMatrix0);
                            instance4.m_materialBlock.AddMatrix(instance4.ID_RightMatrix, data.m_extraData.m_dataMatrix2);
                            instance4.m_materialBlock.AddMatrix(instance4.ID_LeftMatrixB, data.m_extraData.m_dataMatrix3);
                            instance4.m_materialBlock.AddMatrix(instance4.ID_RightMatrixB, data.m_dataMatrix1);
                            instance4.m_materialBlock.AddVector(instance4.ID_MeshScale, data.m_dataVector0);
                            instance4.m_materialBlock.AddVector(instance4.ID_CenterPos, data.m_dataVector1);
                            instance4.m_materialBlock.AddVector(instance4.ID_SideScale, data.m_dataVector2);
                            instance4.m_materialBlock.AddVector(instance4.ID_ObjectIndex, dataVector4);
                            instance4.m_materialBlock.AddColor(instance4.ID_Color, data.m_dataColor0);
                            bool flag23 = node3.m_requireSurfaceMaps && data.m_dataTexture1 != null;
                            if (flag23)
                            {
                                instance4.m_materialBlock.AddTexture(instance4.ID_SurfaceTexA, data.m_dataTexture0);
                                instance4.m_materialBlock.AddTexture(instance4.ID_SurfaceTexB, data.m_dataTexture1);
                                instance4.m_materialBlock.AddVector(instance4.ID_SurfaceMapping, data.m_dataVector3);
                            }
                            NetManager var_30_8EF_cp_0_cp_0 = instance4;
                            num = var_30_8EF_cp_0_cp_0.m_drawCallData.m_defaultCalls + 1;
                            var_30_8EF_cp_0_cp_0.m_drawCallData.m_defaultCalls = num;
                            Graphics.DrawMesh(node3.m_nodeMesh, data.m_position, data.m_rotation, node3.m_nodeMaterial, node3.m_layer, null, 0, instance4.m_materialBlock);
                        }
                        num = k + 1;
                    }
                }
                else
                {
                    bool flag24 = (flags & NetNode.Flags.Bend) > NetNode.Flags.None;
                    if (flag24)
                    {
                        NetManager instance5 = Singleton<NetManager>.get_instance();
                        int num;
                        for (int l = 0; l < info.m_segments.Length; l = num)
                        {
                            NetInfo.Segment segment4 = info.m_segments[l];
                            bool flag26;
                            bool flag25 = segment4.CheckFlags(NetSegment.Flags.None, out flag26) && !segment4.m_disableBendNodes;
                            if (flag25)
                            {
                                Vector4 dataVector5 = data.m_dataVector3;
                                bool requireWindSpeed4 = segment4.m_requireWindSpeed;
                                if (requireWindSpeed4)
                                {
                                    dataVector5.w = data.m_dataFloat0;
                                }
                                instance5.m_materialBlock.Clear();
                                instance5.m_materialBlock.AddMatrix(instance5.ID_LeftMatrix, data.m_dataMatrix0);
                                instance5.m_materialBlock.AddMatrix(instance5.ID_RightMatrix, data.m_extraData.m_dataMatrix2);
                                instance5.m_materialBlock.AddVector(instance5.ID_MeshScale, data.m_dataVector0);
                                instance5.m_materialBlock.AddVector(instance5.ID_ObjectIndex, dataVector5);
                                instance5.m_materialBlock.AddColor(instance5.ID_Color, data.m_dataColor0);
                                bool flag27 = segment4.m_requireSurfaceMaps && data.m_dataTexture1 != null;
                                if (flag27)
                                {
                                    instance5.m_materialBlock.AddTexture(instance5.ID_SurfaceTexA, data.m_dataTexture0);
                                    instance5.m_materialBlock.AddTexture(instance5.ID_SurfaceTexB, data.m_dataTexture1);
                                    instance5.m_materialBlock.AddVector(instance5.ID_SurfaceMapping, data.m_dataVector1);
                                }
                                NetManager var_30_ADC_cp_0_cp_0 = instance5;
                                num = var_30_ADC_cp_0_cp_0.m_drawCallData.m_defaultCalls + 1;
                                var_30_ADC_cp_0_cp_0.m_drawCallData.m_defaultCalls = num;
                                Graphics.DrawMesh(segment4.m_segmentMesh, data.m_position, data.m_rotation, segment4.m_segmentMaterial, segment4.m_layer, null, 0, instance5.m_materialBlock);
                            }
                            num = l + 1;
                        }
                        int m = 0;
                        while (m < info.m_nodes.Length)
                        {
                            NetInfo.Node node4 = info.m_nodes[m];
                            bool flag28 = node4.CheckFlags(flags) && node4.m_directConnect && (node4.m_connectGroup == NetInfo.ConnectGroup.None || (node4.m_connectGroup & info.m_connectGroup & NetInfo.ConnectGroup.AllGroups) > NetInfo.ConnectGroup.None);
                            if (flag28)
                            {
                                Vector4 dataVector6 = data.m_dataVector3;
                                Vector4 dataVector7 = data.m_dataVector0;
                                bool requireWindSpeed5 = node4.m_requireWindSpeed;
                                if (requireWindSpeed5)
                                {
                                    dataVector6.w = data.m_dataFloat0;
                                }
                                bool flag29 = (node4.m_connectGroup & NetInfo.ConnectGroup.Oneway) > NetInfo.ConnectGroup.None;
                                if (flag29)
                                {
                                    ushort segment5 = netnode.GetSegment(data.m_dataInt0 & 7);
                                    ushort segment6 = netnode.GetSegment(data.m_dataInt0 >> 4);
                                    bool flag30 = instance5.m_segments.m_buffer[(int)segment5].m_startNode == nodeID == ((instance5.m_segments.m_buffer[(int)segment5].m_flags & NetSegment.Flags.Invert) == NetSegment.Flags.None);
                                    bool flag31 = instance5.m_segments.m_buffer[(int)segment6].m_startNode == nodeID == ((instance5.m_segments.m_buffer[(int)segment6].m_flags & NetSegment.Flags.Invert) == NetSegment.Flags.None);
                                    bool flag32 = flag30 != flag31;
                                    if (!flag32)
                                    {
                                        goto IL_E46;
                                    }
                                    bool flag33 = flag30;
                                    if (flag33)
                                    {
                                        bool flag34 = (node4.m_connectGroup & NetInfo.ConnectGroup.OnewayStart) == NetInfo.ConnectGroup.None;
                                        if (flag34)
                                        {
                                            goto IL_E46;
                                        }
                                    }
                                    else
                                    {
                                        bool flag35 = (node4.m_connectGroup & NetInfo.ConnectGroup.OnewayEnd) > NetInfo.ConnectGroup.None;
                                        if (!flag35)
                                        {
                                            goto IL_E46;
                                        }
                                        dataVector7.x = -dataVector7.x;
                                        dataVector7.y = -dataVector7.y;
                                    }
                                }
                                instance5.m_materialBlock.Clear();
                                instance5.m_materialBlock.AddMatrix(instance5.ID_LeftMatrix, data.m_dataMatrix0);
                                instance5.m_materialBlock.AddMatrix(instance5.ID_RightMatrix, data.m_extraData.m_dataMatrix2);
                                instance5.m_materialBlock.AddVector(instance5.ID_MeshScale, dataVector7);
                                instance5.m_materialBlock.AddVector(instance5.ID_ObjectIndex, dataVector6);
                                instance5.m_materialBlock.AddColor(instance5.ID_Color, data.m_dataColor0);
                                bool flag36 = node4.m_requireSurfaceMaps && data.m_dataTexture1 != null;
                                if (flag36)
                                {
                                    instance5.m_materialBlock.AddTexture(instance5.ID_SurfaceTexA, data.m_dataTexture0);
                                    instance5.m_materialBlock.AddTexture(instance5.ID_SurfaceTexB, data.m_dataTexture1);
                                    instance5.m_materialBlock.AddVector(instance5.ID_SurfaceMapping, data.m_dataVector1);
                                }
                                NetManager var_30_E04_cp_0_cp_0 = instance5;
                                num = var_30_E04_cp_0_cp_0.m_drawCallData.m_defaultCalls + 1;
                                var_30_E04_cp_0_cp_0.m_drawCallData.m_defaultCalls = num;
                                Graphics.DrawMesh(node4.m_nodeMesh, data.m_position, data.m_rotation, node4.m_nodeMaterial, node4.m_layer, null, 0, instance5.m_materialBlock);
                            }
                            IL_E46:
                            num = m + 1;
                            m = num;
                            continue;
                            goto IL_E46;
                        }
                    }
                }
            }
        }
        instanceIndex = (uint)data.m_nextInstance;
    }

    private void RefreshEndData(NetNode netnode, ushort nodeID, NetInfo info, uint instanceIndex, ref RenderManager.Instance data)
    {
        MethodInfo method = this.GetMethod("RefreshEndData", 4u);
        object[] array = new object[]
        {
                nodeID,
                info,
                instanceIndex,
                data
        };
        method.Invoke(netnode, array);
        data = (RenderManager.Instance)array[3];
    }

    public void RenderInstanceSegment(RenderManager.CameraInfo cameraInfo, ushort segmentID, int layerMask)
    {
        NetManager instance = Singleton<NetManager>.get_instance();
        NetSegment netSegment = instance.m_segments.m_buffer[(int)segmentID];
        bool flag = netSegment.m_flags == NetSegment.Flags.None;
        if (!flag)
        {
            NetInfo info = netSegment.Info;
            bool flag2 = !cameraInfo.Intersect(netSegment.m_bounds);
            if (!flag2)
            {
                bool flag3 = netSegment.m_problems != Notification.Problem.None && (layerMask & 1 << Singleton<NotificationManager>.get_instance().m_notificationLayer) != 0;
                if (flag3)
                {
                    Vector3 middlePosition = netSegment.m_middlePosition;
                    middlePosition.y += Mathf.Max(5f, info.m_maxHeight);
                    Notification.RenderInstance(cameraInfo, netSegment.m_problems, middlePosition, 1f);
                }
                bool flag4 = (layerMask & info.m_netLayers) == 0;
                if (!flag4)
                {
                    RenderManager instance2 = Singleton<RenderManager>.get_instance();
                    uint num;
                    bool flag5 = !instance2.RequireInstance((uint)(49152 + segmentID), 1u, out num);
                    if (!flag5)
                    {
                        this.RenderInstanceSegmentNew(cameraInfo, segmentID, layerMask, info, ref instance2.m_instances[(int)num]);
                    }
                }
            }
        }
    }

    private void RenderInstanceSegmentNew(RenderManager.CameraInfo cameraInfo, ushort segmentID, int layerMask, NetInfo info, ref RenderManager.Instance data)
    {
        NetManager instance = Singleton<NetManager>.get_instance();
        bool flag = false;
        bool flag2 = (instance.m_segments.m_buffer[(int)segmentID].m_flags & NetSegment.Flags.Invert) != NetSegment.Flags.None && info.name.Contains("Highway") && !info.name.ToLower().Contains("tunnel") && !info.name.ToLower().Contains("slope");
        if (flag2)
        {
            flag = true;
            ushort endNode = instance.m_segments.m_buffer[(int)segmentID].m_endNode;
            instance.m_segments.m_buffer[(int)segmentID].m_endNode = instance.m_segments.m_buffer[(int)segmentID].m_startNode;
            instance.m_segments.m_buffer[(int)segmentID].m_startNode = endNode;
            instance.m_segments.m_buffer[(int)segmentID].m_flags = (instance.m_segments.m_buffer[(int)segmentID].m_flags & ~NetSegment.Flags.Invert);
            Vector3 endDirection = instance.m_segments.m_buffer[(int)segmentID].m_endDirection;
            instance.m_segments.m_buffer[(int)segmentID].m_endDirection = instance.m_segments.m_buffer[(int)segmentID].m_startDirection;
            instance.m_segments.m_buffer[(int)segmentID].m_startDirection = endDirection;
        }
        bool dirty = data.m_dirty;
        if (dirty)
        {
            data.m_dirty = false;
            Vector3 position = instance.m_nodes.m_buffer[(int)instance.m_segments.m_buffer[(int)segmentID].m_startNode].m_position;
            Vector3 position2 = instance.m_nodes.m_buffer[(int)instance.m_segments.m_buffer[(int)segmentID].m_endNode].m_position;
            data.m_position = (position + position2) * 0.5f;
            data.m_rotation = Quaternion.identity;
            data.m_dataColor0 = info.m_color;
            data.m_dataColor0.a = 0f;
            data.m_dataFloat0 = Singleton<WeatherManager>.get_instance().GetWindSpeed(data.m_position);
            data.m_dataVector0 = new Vector4(0.5f / info.m_halfWidth, 1f / info.m_segmentLength, 1f, 1f);
            Vector4 colorLocation = RenderManager.GetColorLocation((uint)(49152 + segmentID));
            Vector4 vector = colorLocation;
            bool flag3 = NetNode.BlendJunction(instance.m_segments.m_buffer[(int)segmentID].m_startNode);
            if (flag3)
            {
                colorLocation = RenderManager.GetColorLocation(86016u + (uint)instance.m_segments.m_buffer[(int)segmentID].m_startNode);
            }
            bool flag4 = NetNode.BlendJunction(instance.m_segments.m_buffer[(int)segmentID].m_endNode);
            if (flag4)
            {
                vector = RenderManager.GetColorLocation(86016u + (uint)instance.m_segments.m_buffer[(int)segmentID].m_endNode);
            }
            data.m_dataVector3 = new Vector4(colorLocation.x, colorLocation.y, vector.x, vector.y);
            bool flag5 = info.m_segments == null || info.m_segments.Length == 0;
            if (flag5)
            {
                bool flag6 = info.m_lanes != null;
                if (flag6)
                {
                    bool flag7 = (flag && !info.name.Contains("Highway")) || (!flag && info.name.Contains("Highway") && !info.name.ToLower().Contains("tunnel") && !info.name.ToLower().Contains("slope"));
                    bool invert;
                    if (flag7)
                    {
                        invert = true;
                        NetNode.Flags flags;
                        Color color;
                        instance.m_nodes.m_buffer[(int)instance.m_segments.m_buffer[(int)segmentID].m_endNode].Info.m_netAI.GetNodeState(instance.m_segments.m_buffer[(int)segmentID].m_endNode, ref instance.m_nodes.m_buffer[(int)instance.m_segments.m_buffer[(int)segmentID].m_endNode], segmentID, ref instance.m_segments.m_buffer[(int)segmentID], out flags, out color);
                        NetNode.Flags flags2;
                        Color color2;
                        instance.m_nodes.m_buffer[(int)instance.m_segments.m_buffer[(int)segmentID].m_startNode].Info.m_netAI.GetNodeState(instance.m_segments.m_buffer[(int)segmentID].m_startNode, ref instance.m_nodes.m_buffer[(int)instance.m_segments.m_buffer[(int)segmentID].m_startNode], segmentID, ref instance.m_segments.m_buffer[(int)segmentID], out flags2, out color2);
                    }
                    else
                    {
                        bool flag8 = (instance.m_segments.m_buffer[(int)segmentID].m_flags & NetSegment.Flags.Invert) > NetSegment.Flags.None;
                        if (flag8)
                        {
                            invert = true;
                            NetNode.Flags flags;
                            Color color;
                            instance.m_nodes.m_buffer[(int)instance.m_segments.m_buffer[(int)segmentID].m_endNode].Info.m_netAI.GetNodeState(instance.m_segments.m_buffer[(int)segmentID].m_endNode, ref instance.m_nodes.m_buffer[(int)instance.m_segments.m_buffer[(int)segmentID].m_endNode], segmentID, ref instance.m_segments.m_buffer[(int)segmentID], out flags, out color);
                            NetNode.Flags flags2;
                            Color color2;
                            instance.m_nodes.m_buffer[(int)instance.m_segments.m_buffer[(int)segmentID].m_startNode].Info.m_netAI.GetNodeState(instance.m_segments.m_buffer[(int)segmentID].m_startNode, ref instance.m_nodes.m_buffer[(int)instance.m_segments.m_buffer[(int)segmentID].m_startNode], segmentID, ref instance.m_segments.m_buffer[(int)segmentID], out flags2, out color2);
                        }
                        else
                        {
                            invert = false;
                            NetNode.Flags flags;
                            Color color;
                            instance.m_nodes.m_buffer[(int)instance.m_segments.m_buffer[(int)segmentID].m_startNode].Info.m_netAI.GetNodeState(instance.m_segments.m_buffer[(int)segmentID].m_startNode, ref instance.m_nodes.m_buffer[(int)instance.m_segments.m_buffer[(int)segmentID].m_startNode], segmentID, ref instance.m_segments.m_buffer[(int)segmentID], out flags, out color);
                            NetNode.Flags flags2;
                            Color color2;
                            instance.m_nodes.m_buffer[(int)instance.m_segments.m_buffer[(int)segmentID].m_endNode].Info.m_netAI.GetNodeState(instance.m_segments.m_buffer[(int)segmentID].m_endNode, ref instance.m_nodes.m_buffer[(int)instance.m_segments.m_buffer[(int)segmentID].m_endNode], segmentID, ref instance.m_segments.m_buffer[(int)segmentID], out flags2, out color2);
                        }
                    }
                    float startAngle = (float)instance.m_segments.m_buffer[(int)segmentID].m_cornerAngleStart * 0.02454369f;
                    float endAngle = (float)instance.m_segments.m_buffer[(int)segmentID].m_cornerAngleEnd * 0.02454369f;
                    int num = 0;
                    uint num2 = instance.m_segments.m_buffer[(int)segmentID].m_lanes;
                    int num3 = 0;
                    while (num3 < info.m_lanes.Length && num2 > 0u)
                    {
                        instance.m_lanes.m_buffer[(int)num2].RefreshInstance(num2, info.m_lanes[num3], startAngle, endAngle, invert, ref data, ref num);
                        num2 = instance.m_lanes.m_buffer[(int)num2].m_nextLane;
                        int num4 = num3 + 1;
                        num3 = num4;
                    }
                }
            }
            else
            {
                float vScale = info.m_netAI.GetVScale();
                Vector3 vector2;
                Vector3 startDir;
                bool smoothStart;
                instance.m_segments.m_buffer[(int)segmentID].CalculateCorner(segmentID, true, true, true, out vector2, out startDir, out smoothStart);
                Vector3 vector3;
                Vector3 endDir;
                bool smoothEnd;
                instance.m_segments.m_buffer[(int)segmentID].CalculateCorner(segmentID, true, false, true, out vector3, out endDir, out smoothEnd);
                Vector3 vector4;
                Vector3 startDir2;
                instance.m_segments.m_buffer[(int)segmentID].CalculateCorner(segmentID, true, true, false, out vector4, out startDir2, out smoothStart);
                Vector3 vector5;
                Vector3 endDir2;
                instance.m_segments.m_buffer[(int)segmentID].CalculateCorner(segmentID, true, false, false, out vector5, out endDir2, out smoothEnd);
                Vector3 vector6;
                Vector3 vector7;
                NetSegment.CalculateMiddlePoints(vector2, startDir, vector5, endDir2, smoothStart, smoothEnd, out vector6, out vector7);
                Vector3 vector8;
                Vector3 vector9;
                NetSegment.CalculateMiddlePoints(vector4, startDir2, vector3, endDir, smoothStart, smoothEnd, out vector8, out vector9);
                data.m_dataMatrix0 = NetSegment.CalculateControlMatrix(vector2, vector6, vector7, vector5, vector4, vector8, vector9, vector3, data.m_position, vScale);
                data.m_dataMatrix1 = NetSegment.CalculateControlMatrix(vector4, vector8, vector9, vector3, vector2, vector6, vector7, vector5, data.m_position, vScale);
            }
            bool requireSurfaceMaps = info.m_requireSurfaceMaps;
            if (requireSurfaceMaps)
            {
                Singleton<TerrainManager>.get_instance().GetSurfaceMapping(data.m_position, out data.m_dataTexture0, out data.m_dataTexture1, out data.m_dataVector1);
            }
        }
        bool flag9 = info.m_segments != null;
        if (flag9)
        {
            int num4;
            for (int i = 0; i < info.m_segments.Length; i = num4)
            {
                NetInfo.Segment segment = info.m_segments[i];
                bool flag11;
                bool flag10 = segment.CheckFlags(instance.m_segments.m_buffer[(int)segmentID].m_flags, out flag11);
                if (flag10)
                {
                    Vector4 dataVector = data.m_dataVector3;
                    Vector4 dataVector2 = data.m_dataVector0;
                    bool requireWindSpeed = segment.m_requireWindSpeed;
                    if (requireWindSpeed)
                    {
                        dataVector.w = data.m_dataFloat0;
                    }
                    bool flag12 = flag11;
                    if (flag12)
                    {
                        dataVector2.x = -dataVector2.x;
                        dataVector2.y = -dataVector2.y;
                    }
                    instance.m_materialBlock.Clear();
                    instance.m_materialBlock.AddMatrix(instance.ID_LeftMatrix, data.m_dataMatrix0);
                    instance.m_materialBlock.AddMatrix(instance.ID_RightMatrix, data.m_dataMatrix1);
                    instance.m_materialBlock.AddVector(instance.ID_MeshScale, dataVector2);
                    instance.m_materialBlock.AddVector(instance.ID_ObjectIndex, dataVector);
                    instance.m_materialBlock.AddColor(instance.ID_Color, data.m_dataColor0);
                    bool flag13 = segment.m_requireSurfaceMaps && data.m_dataTexture0 != null;
                    if (flag13)
                    {
                        instance.m_materialBlock.AddTexture(instance.ID_SurfaceTexA, data.m_dataTexture0);
                        instance.m_materialBlock.AddTexture(instance.ID_SurfaceTexB, data.m_dataTexture1);
                        instance.m_materialBlock.AddVector(instance.ID_SurfaceMapping, data.m_dataVector1);
                    }
                    NetManager var_66_AE1_cp_0_cp_0 = instance;
                    num4 = var_66_AE1_cp_0_cp_0.m_drawCallData.m_defaultCalls + 1;
                    var_66_AE1_cp_0_cp_0.m_drawCallData.m_defaultCalls = num4;
                    Graphics.DrawMesh(segment.m_segmentMesh, data.m_position, data.m_rotation, segment.m_segmentMaterial, segment.m_layer, null, 0, instance.m_materialBlock);
                }
                num4 = i + 1;
            }
        }
        bool flag14 = flag;
        if (flag14)
        {
            ushort endNode2 = instance.m_segments.m_buffer[(int)segmentID].m_endNode;
            instance.m_segments.m_buffer[(int)segmentID].m_endNode = instance.m_segments.m_buffer[(int)segmentID].m_startNode;
            instance.m_segments.m_buffer[(int)segmentID].m_startNode = endNode2;
            NetSegment[] var_71_BC2_cp_0_cp_0 = instance.m_segments.m_buffer;
            var_71_BC2_cp_0_cp_0[(int)segmentID].m_flags = (var_71_BC2_cp_0_cp_0[(int)segmentID].m_flags | NetSegment.Flags.Invert);
            Vector3 endDirection2 = instance.m_segments.m_buffer[(int)segmentID].m_endDirection;
            instance.m_segments.m_buffer[(int)segmentID].m_endDirection = instance.m_segments.m_buffer[(int)segmentID].m_startDirection;
            instance.m_segments.m_buffer[(int)segmentID].m_startDirection = endDirection2;
        }
        bool flag15 = info.m_lanes == null || ((layerMask & info.m_treeLayers) == 0 && !cameraInfo.CheckRenderDistance(data.m_position, info.m_maxPropDistance + 128f));
        if (!flag15)
        {
            bool flag16 = (instance.m_segments.m_buffer[(int)segmentID].m_flags & NetSegment.Flags.Invert) > NetSegment.Flags.None;
            bool invert2;
            NetNode.Flags startFlags;
            Color startColor;
            NetNode.Flags endFlags;
            Color endColor;
            if (flag16)
            {
                invert2 = true;
                instance.m_nodes.m_buffer[(int)instance.m_segments.m_buffer[(int)segmentID].m_endNode].Info.m_netAI.GetNodeState(instance.m_segments.m_buffer[(int)segmentID].m_endNode, ref instance.m_nodes.m_buffer[(int)instance.m_segments.m_buffer[(int)segmentID].m_endNode], segmentID, ref instance.m_segments.m_buffer[(int)segmentID], out startFlags, out startColor);
                instance.m_nodes.m_buffer[(int)instance.m_segments.m_buffer[(int)segmentID].m_startNode].Info.m_netAI.GetNodeState(instance.m_segments.m_buffer[(int)segmentID].m_startNode, ref instance.m_nodes.m_buffer[(int)instance.m_segments.m_buffer[(int)segmentID].m_startNode], segmentID, ref instance.m_segments.m_buffer[(int)segmentID], out endFlags, out endColor);
            }
            else
            {
                invert2 = false;
                instance.m_nodes.m_buffer[(int)instance.m_segments.m_buffer[(int)segmentID].m_startNode].Info.m_netAI.GetNodeState(instance.m_segments.m_buffer[(int)segmentID].m_startNode, ref instance.m_nodes.m_buffer[(int)instance.m_segments.m_buffer[(int)segmentID].m_startNode], segmentID, ref instance.m_segments.m_buffer[(int)segmentID], out startFlags, out startColor);
                instance.m_nodes.m_buffer[(int)instance.m_segments.m_buffer[(int)segmentID].m_endNode].Info.m_netAI.GetNodeState(instance.m_segments.m_buffer[(int)segmentID].m_endNode, ref instance.m_nodes.m_buffer[(int)instance.m_segments.m_buffer[(int)segmentID].m_endNode], segmentID, ref instance.m_segments.m_buffer[(int)segmentID], out endFlags, out endColor);
            }
            float startAngle2 = (float)instance.m_segments.m_buffer[(int)segmentID].m_cornerAngleStart * 0.02454369f;
            float endAngle2 = (float)instance.m_segments.m_buffer[(int)segmentID].m_cornerAngleEnd * 0.02454369f;
            Vector4 objectIndex = new Vector4(data.m_dataVector3.x, data.m_dataVector3.y, 1f, data.m_dataFloat0);
            Vector4 objectIndex2 = new Vector4(data.m_dataVector3.z, data.m_dataVector3.w, 1f, data.m_dataFloat0);
            InfoManager.InfoMode currentMode = Singleton<InfoManager>.get_instance().CurrentMode;
            bool flag17 = currentMode != InfoManager.InfoMode.None && !info.m_netAI.ColorizeProps(currentMode);
            if (flag17)
            {
                objectIndex.z = 0f;
                objectIndex2.z = 0f;
            }
            int num5 = (info.m_segments == null || info.m_segments.Length == 0) ? 0 : -1;
            uint num6 = instance.m_segments.m_buffer[(int)segmentID].m_lanes;
            int num7 = 0;
            while (num7 < info.m_lanes.Length && num6 > 0u)
            {
                instance.m_lanes.m_buffer[(int)num6].RenderInstance(cameraInfo, segmentID, num6, info.m_lanes[num7], startFlags, endFlags, startColor, endColor, startAngle2, endAngle2, invert2, layerMask, objectIndex, objectIndex2, ref data, ref num5);
                num6 = instance.m_lanes.m_buffer[(int)num6].m_nextLane;
                int num4 = num7 + 1;
                num7 = num4;
            }
        }
    }
}
}
