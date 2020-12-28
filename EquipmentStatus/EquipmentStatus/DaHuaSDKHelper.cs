using CCTV_Client.DaHua.Models;
using NetSDKCS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace CCTV_Client.DaHua
{
    public class DaHuaSDKHelper
    {
        #region Field 字段
        private const int m_WaitTime = 5000;
        private const int SyncFileSize = 5 * 1024 * 1204;
        private const int DOWNLOAD_END = -1;
        private const int DOWNLOAD_FAILED = -2;

        private const string CFG_CMD_ENCODE = "Encode";
        private NET_ENCODE_VIDEO_INFO _VideoInfo;
        private static fDisConnectCallBack m_DisConnectCallBack;
        private static fHaveReConnectCallBack m_ReConnectCallBack;
        private static fRealDataCallBackEx2 m_RealDataCallBackEx2;
        private static fSnapRevCallBack m_SnapRevCallBack;
        //按时间下载录像回调
        private static fTimeDownLoadPosCallBack m_DownloadPosCallBack;//进度查询回调
        private static fDataCallBack fDownLoadDataCallBack;//下载数据回调
        NET_ENCODE_CHANNELTITLE_INFO _CHANNELTITLE_INFO;

        private IntPtr m_LoginID = IntPtr.Zero;
        private NET_DEVICEINFO_Ex m_DeviceInfo;
        private IntPtr m_RealPlayID = IntPtr.Zero;
        private IntPtr m_DownloadID = IntPtr.Zero;
        private uint m_SnapSerialNum = 1;
        private bool m_IsInSave = false;
        private int SpeedValue = 4;
        private const int MaxSpeed = 8;
        private const int MinSpeed = 1;
        private string filePathPicture;
        private byte[] dataDownload = null;
        private int downloadProgressInt = 0;
        #endregion

        #region 设备初始化
        /// <summary>
        /// 设备初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DeviceInititalize()
        {
            m_SnapRevCallBack = new fSnapRevCallBack(SnapRevCallBack);
            try
            {   //远程抓取监控回调       
                NETClient.Init(m_DisConnectCallBack, IntPtr.Zero, null);             
                NETClient.SetAutoReconnect(m_ReConnectCallBack, IntPtr.Zero);
                NETClient.SetSnapRevCallBack(m_SnapRevCallBack, IntPtr.Zero);
                //下载视频回调
                m_DownloadPosCallBack = new fTimeDownLoadPosCallBack(DownLoadPosCallBack);
                fDownLoadDataCallBack = new fDataCallBack(DataCallBack);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region 回调方法
        /// <summary>
        /// 截图回调方法
        /// </summary>
        /// <param name="lLoginID"></param>
        /// <param name="pBuf"></param>
        /// <param name="RevLen"></param>
        /// <param name="EncodeType"></param>
        /// <param name="CmdSerial"></param>
        /// <param name="dwUser"></param>
        private void SnapRevCallBack(IntPtr lLoginID, IntPtr pBuf, uint RevLen, uint EncodeType, uint CmdSerial, IntPtr dwUser)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "image";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (EncodeType == 10) //.jpg
            {
                DateTime now = DateTime.Now;
                string fileName = string.Format("{0}-{1}-{2}-{3}-{4}-{5}", now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second) + ".jpg";
                filePathPicture = path + "\\" + fileName;
                byte[] data = new byte[RevLen];//转换为byte类型
                Marshal.Copy(pBuf, data, 0, (int)RevLen);//将revlen数据导入data
                try
                {
                    //when the file is operate by local capture will throw expection.
                    using (FileStream stream = new FileStream(filePathPicture, FileMode.OpenOrCreate))
                    {
                        stream.Write(data, 0, (int)RevLen);
                        stream.Flush();
                        stream.Dispose();
                    }
                }
                catch
                {
                    return;
                }

            }
        }

        //返回数据回调

        private  int DataCallBack(IntPtr lRealHandle, uint dwDataType, IntPtr pBuffer, uint dwBufSize, IntPtr dwUser)
        {
            int nRet = 0;
                switch (dwDataType)
                {
                    case 0:
                    byte []  dataDownload = new byte[dwBufSize];//转换为byte类型
                    Marshal.Copy(pBuffer, dataDownload, 0, (int)dwBufSize);//将revlen数据导入data

                    //when the file is operate by local capture will throw expection.
                    Stream stream = new MemoryStream(dataDownload);
                        
                            stream.Write(dataDownload, 0, (int)dwBufSize);
                            stream.Flush();
                            stream.Dispose();
                        
                     //Original data
                       // 用户在此处保存码流数据，离开回调函数后再进行解码或转发等一系列处理
                    nRet = 1;//
                        break;
                    case 1:
                        //Standard video data
                        break;
                    case 2:
                        //yuv data
                        break;
                    case 3:
                        //pcm audio data
                        break;
                    default:
                        break;
                }
            
           return nRet;
        }




        #endregion

        #endregion

        #region 回调函数
        //录像下载状态查询回调函数
        int value = 0;
        private void DownLoadPosCallBack(IntPtr lPlayHandle, uint dwTotalSize, uint dwDownLoadSize, int index, NET_RECORDFILE_INFO recordfileinfo, IntPtr dwUser)
        {
            if (lPlayHandle == m_DownloadID)
            {
             
                if (DOWNLOAD_END == (int)dwDownLoadSize)
                {
                    value = DOWNLOAD_END;
                }
                else if (DOWNLOAD_FAILED == (int)dwDownLoadSize)
                {
                    value = DOWNLOAD_FAILED;
                }
                else
                {
                    value = (int)(dwDownLoadSize * 100 / dwTotalSize);
                }
                UpdateProgress(value);
            }
        }
        //回调后更新方法
        private void UpdateProgress(int value)
        {
            if (m_DownloadID != IntPtr.Zero)
            {
                if (DOWNLOAD_END == value)
                {
                   
                    NETClient.StopDownload(m_DownloadID);
                   
                    //return;
                }
                if (DOWNLOAD_FAILED == value)
                {
                    
                    //return;
                }
               downloadProgressInt = value;
            }
        }
        #endregion

        #region 设备登录/登出
        /// <summary>
        /// 設備登陸
        /// </summary>
        /// <param name="DVR_IP"></param>
        /// <param name="DVR_Port"></param>
        /// <param name="DVR_Name"></param>
        /// <param name="DVR_PassWord"></param>
        /// <returns></returns>
        public IntPtr LoginClick(String DVR_IP, String DVR_Port, String DVR_Name, String DVR_PassWord, ref NET_DEVICEINFO_Ex m_DeviceInfo)
        {
            if (IntPtr.Zero == m_LoginID)
            {
                ushort port = 0;

                port = Convert.ToUInt16(DVR_Port);

                m_DeviceInfo = new NET_DEVICEINFO_Ex();
                //獲取登陸信息-->傳遞(IP,端口，帳號，密碼，傳輸協議TCP,參數，登出設備信息<返回參數>)

                m_LoginID = NETClient.Login(DVR_IP, port, DVR_Name, DVR_PassWord, EM_LOGIN_SPAC_CAP_TYPE.TCP, IntPtr.Zero, ref m_DeviceInfo);


                if (IntPtr.Zero == m_LoginID)
                {
                    return IntPtr.Zero;//登陸失敗
                }

            }
            return m_LoginID;
        }
        /// <summary>
        /// 設備登出
        /// </summary>
        /// <param name="m_LoginID"></param>
        /// <returns></returns>
        public bool LogOut(IntPtr m_LoginID)

        {
            bool result = NETClient.Logout(m_LoginID);//設備登出
            if (!result)
            {   //寫錯誤日誌
                return false;
            }
            m_LoginID = IntPtr.Zero;//初始化
            return true;

        }
        #endregion

        #region 读取硬盘容量
        /// <summary>
        /// 查询硬盘容量
        /// </summary>
        /// <param name="m_LoginID"></param>
        /// <returns></returns>
        public List<DiskInfo> GetDiskInfo(IntPtr m_LoginID)
        {
            List<DiskInfo> DiskList = new List<DiskInfo>();
            NET_HARDDISK_STATE nET_HARDDISK_STATE = new NET_HARDDISK_STATE();
            object obj = nET_HARDDISK_STATE;
            bool ret = NETClient.QueryDevState(m_LoginID, EM_DEVICE_STATE.DISK, ref obj, typeof(NET_HARDDISK_STATE), 5000);
            if (!ret)
            {
                // throw   "读取失败";
            }

            nET_HARDDISK_STATE = (NET_HARDDISK_STATE)obj;

            for (int i = 0; i < nET_HARDDISK_STATE.dwDiskNum; i++)
            {

                DiskInfo dISKINFO = new DiskInfo();
                dISKINFO.DiskNumber = nET_HARDDISK_STATE.stDisks[i].bDiskNum.ToString();
                dISKINFO.FreeSpace = nET_HARDDISK_STATE.stDisks[i].dwFreeSpace.ToString();
                dISKINFO.TotalSpace = nET_HARDDISK_STATE.stDisks[i].dwVolume.ToString();
                dISKINFO.DiskType = Enum.GetName(typeof(EM_DISK_TYPE), nET_HARDDISK_STATE.stDisks[i].dwStatus >> 4 & 0xF) + GetChnString((byte)(nET_HARDDISK_STATE.stDisks[i].dwStatus >> 4 & 0xF));
                if ((nET_HARDDISK_STATE.stDisks[i].dwStatus & 0xF) == 0)
                {
                    dISKINFO.DiskStatus = "Hiberation(休眠)";
                }
                else if ((nET_HARDDISK_STATE.stDisks[i].dwStatus & 0xF) == 1)
                {
                    dISKINFO.DiskStatus = "Active(活动)";
                }
                else if ((nET_HARDDISK_STATE.stDisks[i].dwStatus & 0xF) == 2)
                {
                    dISKINFO.DiskStatus = "Malfunction(故障)";
                }
                dISKINFO.SubareaNumber = nET_HARDDISK_STATE.stDisks[i].bSubareaNum.ToString();
                DiskList.Add(dISKINFO);
            }
            return DiskList;
        }


        //硬盘类型
        string GetChnString(byte value)
        {
            switch (value)
            {
                case (byte)EM_DISK_TYPE.READ_WRITE:
                    return "(读写)";
                case (byte)EM_DISK_TYPE.READ_ONLY:
                    return "(只读)";
                case (byte)EM_DISK_TYPE.BACKUP:
                    return "(备份)";
                case (byte)EM_DISK_TYPE.REDUNDANT:
                    return "(冗余)";
                case (byte)EM_DISK_TYPE.SNAPSHOT:
                    return "(快照)";
                default:
                    return "";
            }
        }


        #endregion

        #region 监控画面预览
        /// <summary>
        /// 監控預覽
        /// </summary>
        /// <param name="m_LoginID"></param>
        /// <param name="nChannelID"></param>
        /// <param name="Handle"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public IntPtr StartRealplay(IntPtr m_LoginID, int nChannelID, IntPtr Handle, EM_RealPlayType Type)
        {

            if (IntPtr.Zero == m_RealPlayID)
            {
                // realplay 监视
                EM_RealPlayType newtype;
                if (Type == 0)
                {
                    newtype = EM_RealPlayType.Realplay;//枚舉，多畫面預覽
                }
                else
                {
                    newtype = EM_RealPlayType.Realplay_1;//枚舉，碼流1讀取
                }

                //預覽--> 登陆ID,Login返回值， 通道号，显示窗口句柄， 监视类型
                m_RealPlayID = NETClient.RealPlay(m_LoginID, nChannelID, Handle, newtype);

                if (IntPtr.Zero == m_RealPlayID)//返回0為失敗
                {
                    return IntPtr.Zero;
                }

                // 设置实时监视数据回调-->监视句柄,回调函数,用户数据,回调数据类型,返回BOOl
                NETClient.SetRealDataCallBack(m_RealPlayID, m_RealDataCallBackEx2, IntPtr.Zero, EM_REALDATA_FLAG.DATA_WITH_FRAME_INFO | EM_REALDATA_FLAG.PCM_AUDIO_DATA | EM_REALDATA_FLAG.RAW_DATA | EM_REALDATA_FLAG.YUV_DATA);

            }

            return m_RealPlayID;

        }
        /// <summary>
        /// 停止預覽
        /// </summary>
        /// <param name="m_RealPlayID"></param>
        /// <returns></returns>
        public bool StopRealplay(IntPtr m_RealPlayID)

        {
            // stop realplay 关闭监视
            bool ret = NETClient.StopRealPlay(m_RealPlayID);
            if (!ret)
            {
                return false;
            }
            m_RealPlayID = IntPtr.Zero;
            return true;
        }

        #endregion

        #region 获取通道名称
        /// <summary>
        /// 获取通道名称
        /// </summary>
        /// <param name="_LoginID">登录ID</param>
        /// <param name="ChannelID">通道号</param>
        /// <returns></returns>
        public String GetChannelname(IntPtr _LoginID, int ChannelID)
        {
            _CHANNELTITLE_INFO = new NET_ENCODE_CHANNELTITLE_INFO();
            _CHANNELTITLE_INFO.dwSize = (uint)Marshal.SizeOf(typeof(NET_ENCODE_CHANNELTITLE_INFO));
            _CHANNELTITLE_INFO.szChannelName = null;
            object videoObj = _CHANNELTITLE_INFO;
            bool ret = NETClient.GetEncodeConfig(_LoginID, EM_CFG_ENCODE_TYPE.CHANNELTITLE, ChannelID, ref videoObj, 5000);
            if (!ret)
            {
                // MessageBox.Show(NETClient.GetLastError());
                // return;
            }
            _CHANNELTITLE_INFO = (NET_ENCODE_CHANNELTITLE_INFO)videoObj;

            string strChannelName = _CHANNELTITLE_INFO.szChannelName;
            return strChannelName;

        }
        #endregion

        #region 设定通道名称
        /// <summary>
        /// 设定通道名称
        /// </summary>
        /// <param name="_LoginID">登录ID</param>
        /// <param name="ChannelID">通道号</param>
        /// <param name="ChannelName">名称</param>
        /// <returns></returns>
        public bool SetChannelname(IntPtr _LoginID, int ChannelID, String ChannelName)
        {
            _CHANNELTITLE_INFO = new NET_ENCODE_CHANNELTITLE_INFO();
            _CHANNELTITLE_INFO.dwSize = (uint)Marshal.SizeOf(typeof(NET_ENCODE_CHANNELTITLE_INFO));
            _CHANNELTITLE_INFO.szChannelName = ChannelName;
            object videoObj = _CHANNELTITLE_INFO;
            bool ret = NETClient.SetEncodeConfig(_LoginID, EM_CFG_ENCODE_TYPE.CHANNELTITLE, ChannelID, videoObj, 5000);
            return ret;

        }
        #endregion

        #region 获取rtsp视频流
        public Array GetCameraRtsp(String IP, String name, String password, int channel)
        {
            String uil = $"rtsp://{name}:{password}@{IP}:554/cam/realmonitor?channel={channel}&subtype=0";

            return uil.ToArray();

        }







        #endregion

        #region 异步抓图请求
        /// <summary>
        /// 异步抓图请求
        /// </summary>
        /// <param name="_LoginID">登录序列号</param>
        /// <param name="ChannelID">通道号</param>
        /// <returns></returns>
        public string GetChannelPictureEx(IntPtr _LoginID, int ChannelID)
        {
            NET_SNAP_PARAMS asyncSnap = new NET_SNAP_PARAMS();
            asyncSnap.Channel = (uint)ChannelID;
            asyncSnap.Quality = 6;
            asyncSnap.ImageSize = 2;
            asyncSnap.mode = 0;
            asyncSnap.InterSnap = 0;
            bool ret = NETClient.SnapPictureEx(_LoginID, asyncSnap, IntPtr.Zero);
            Thread.Sleep(1000);
            if (ret)
            {
                return filePathPicture;
            }
            else
            {
                return null;
            }
            
        }
        #endregion

        #region 获取/同步时间
        /// <summary>
        /// 获取设备时间
        /// </summary>
        /// <param name="_LoginID"></param>
        /// <returns></returns>
        public DateTime GetDVRTime(IntPtr _LoginID)
        {
            NET_TIME time = new NET_TIME();
            uint ret = 0;
            IntPtr inPtr = IntPtr.Zero;
            DateTime datatime = DateTime.Today;
            try
            {
                inPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(NET_TIME)));
                Marshal.StructureToPtr(time, inPtr, true);
                bool result = NETClient.GetDevConfig(_LoginID, EM_DEV_CFG_TYPE.TIMECFG, -1, inPtr, (uint)Marshal.SizeOf(typeof(NET_TIME)), ref ret, 5000);
                if (result && ret == (uint)Marshal.SizeOf(typeof(NET_TIME)))
                {
                    time = (NET_TIME)Marshal.PtrToStructure(inPtr, typeof(NET_TIME));
                    datatime = time.ToDateTime();
                }

                return datatime;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                Marshal.FreeHGlobal(inPtr);
            }
        }

        /// <summary>
        /// 手动设定与服务器时间同步
        /// </summary>
        /// <param name="_LoginID"></param>
        /// <returns></returns>
        public bool SetDVRTime(IntPtr _LoginID,DateTime nowtime)
        {
            NET_TIME time;
            time = NET_TIME.FromDateTime(nowtime);
            IntPtr inPtr = IntPtr.Zero;
            try
            {
                inPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(NET_TIME)));
                Marshal.StructureToPtr(time, inPtr, true);
                bool result = NETClient.SetDevConfig(_LoginID, EM_DEV_CFG_TYPE.TIMECFG, -1, inPtr, (uint)Marshal.SizeOf(typeof(NET_TIME)), 5000);
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                Marshal.FreeHGlobal(inPtr);
            }
        }
        #endregion

        #region 下载录像文件
        //按时间获取录像文件，存储在本地
        public IntPtr DownloadVideoFileByTime(IntPtr m_LoginID, int channelID, DateTime startTime, DateTime endTime, string FileName)
        {

            //set stream type 设置码流类型
            EM_STREAM_TYPE streamType = EM_STREAM_TYPE.MAIN;
            IntPtr pStream = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)));
            Marshal.StructureToPtr((int)streamType, pStream, true);
            NETClient.SetDeviceMode(m_LoginID, EM_USEDEV_MODE.RECORD_STREAM_TYPE, pStream);
            //调用下载API
            m_DownloadID = NETClient.DownloadByTime(m_LoginID, channelID, EM_QUERY_RECORD_TYPE.ALL, startTime, endTime, FileName, m_DownloadPosCallBack, IntPtr.Zero, null, IntPtr.Zero, IntPtr.Zero);
            return m_DownloadID;
        }
        //public Stream DownloadVideoStreamByTime(IntPtr m_LoginID, int channelID, DateTime startTime, DateTime endTime)
        //{

        //    //set stream type 设置码流类型
        //    EM_STREAM_TYPE streamType = EM_STREAM_TYPE.MAIN;
        //    IntPtr pStream = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)));
        //    Marshal.StructureToPtr((int)streamType, pStream, true);
        //    NETClient.SetDeviceMode(m_LoginID, EM_USEDEV_MODE.RECORD_STREAM_TYPE, pStream);
        //    //调用下载API
        //    m_DownloadID = NETClient.DownloadByTime(m_LoginID, channelID, EM_QUERY_RECORD_TYPE.ALL, startTime, endTime, null, m_DownloadPosCallBack, IntPtr.Zero, fDownLoadDataCallBack, IntPtr.Zero, IntPtr.Zero);
        //    if ((int)m_DownloadID == 0)
        //    {
        //        return null;
        //    }
        //    FileStream stream = new FileStream(dataDownload);



        //}
        /// <summary>
        /// 停止下载
        /// </summary>
        /// <param name="m_DownloadID"></param>
        /// <returns></returns>
        public  bool StopDownloadVideo(IntPtr m_DownloadID)
        {
            bool result = false;
            result = NETClient.StopDownload(m_DownloadID);
           
            return result;
        }
        /// <summary>
        /// 查询下载进度
        /// </summary>
        /// <param name="m_DownloadID"></param>
        /// <returns></returns>
        public  Dictionary<string,int>  DownloadVideoFilePlan(IntPtr m_DownloadID)
        {
            long rest = 0;
            bool result = false;
            int nTotalSize=0;
            int nDownLoadSize=0;
            result = NETClient.GetDownloadPos(m_DownloadID, ref nTotalSize, ref nDownLoadSize);
            if (result)
            {


                Dictionary<string, int> dictionaryint = new Dictionary<string, int>();
                dictionaryint.Add("nDownLoadSize", nDownLoadSize);
                dictionaryint.Add("nTotalSize", nTotalSize);
                return dictionaryint;
            }
            else
            {
                return null;
            }
              

        }











        #endregion

        #region 查询指定时间监控录像文件
        /// <summary>
        /// 查询指定时间录像文件,，返回文件数量
        /// </summary>
        /// <param name="m_LoginID"></param>
        /// <param name="channelID"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public int QueryRecordFile(IntPtr m_LoginID, int channelID, DateTime startTime, DateTime endTime)
        {

            NET_RECORDFILE_INFO[] infos = new NET_RECORDFILE_INFO[5000];
            int fileCount = 0;//返回文件数量
            //set stream type 设置码流类型
            EM_STREAM_TYPE streamType = EM_STREAM_TYPE.AUTO;
            IntPtr pStream = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)));
            Marshal.StructureToPtr((int)streamType, pStream, true);
            NETClient.SetDeviceMode(m_LoginID, EM_USEDEV_MODE.RECORD_STREAM_TYPE, pStream);
            //query record file 查询录像文件
            bool ret = NETClient.QueryRecordFile(m_LoginID, channelID, EM_QUERY_RECORD_TYPE.ALL, startTime, endTime, null, ref infos, ref fileCount, m_WaitTime, false);
            Thread.Sleep(500);
            if (false == ret)
            {
                return -1;//标示查询失败;
            }
            else
            {
                return fileCount;
            }
        }
        #endregion

        #region 获取/设定设备编码信息
        /// <summary>
        /// 获取通道编码信息
        /// </summary>
        /// <param name="_LoginID"></param>
        /// <param name="ChannelID"></param>
        /// <returns></returns>
       public NET_ENCODE_VIDEO_INFO GetEncodeConfig(IntPtr _LoginID, int ChannelID)
        {

            //获取视频参数
            _VideoInfo = new NET_ENCODE_VIDEO_INFO();
            _VideoInfo.dwSize = (uint)Marshal.SizeOf(typeof(NET_ENCODE_VIDEO_INFO));
            _VideoInfo.emFormatType = NetSDKCS.EM_FORMAT_TYPE.NORMAL;
            object videoObj = _VideoInfo;
            bool  ret = NETClient.GetEncodeConfig(_LoginID, EM_CFG_ENCODE_TYPE.VIDEO, ChannelID, ref videoObj, 5000);
            _VideoInfo = (NET_ENCODE_VIDEO_INFO)videoObj;


            return _VideoInfo;

        }


        /// <summary>
        /// 设定通道编码信息
        /// </summary>
        /// <param name="_LoginID"></param>
        /// <param name="ChannelID"></param>
        /// <param name="videoEncodeInfo"></param>
        /// <returns></returns>
        public bool SetEncodeConfig(IntPtr _LoginID, int ChannelID, NET_ENCODE_VIDEO_INFO videoEncodeInfo)
        {

            object videoObj = videoEncodeInfo;

            bool ret = NETClient.SetEncodeConfig(_LoginID, EM_CFG_ENCODE_TYPE.VIDEO, ChannelID, videoObj, 5000);
            return ret;

        }

        #endregion

        #region 报警主机布/撤防
        /// <summary>
        /// 获取报警主机布防状态，传入登陆ID
        /// </summary>
        /// <param name="_LoginID"></param>
        /// <returns></returns>
        public  Dictionary<int,string> GetDefenceArmMode(IntPtr _LoginID)
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            NET_IN_GET_DEFENCEMODE stuinfo = new NET_IN_GET_DEFENCEMODE()
            {
                dwSize = (uint)Marshal.SizeOf(typeof(NET_IN_GET_DEFENCEMODE)),
            };

            stuinfo.anDefence = new int[128];
            stuinfo.nDefenceNum = 128;
            for (int i = 0; i < stuinfo.nDefenceNum; ++i)
            {
                stuinfo.anDefence[i] = i; //赋值通道号，通道号从0开始，0,1,2,3....，也代表该通道编号
            }

            NET_OUT_GET_DEFENCEMODE stuOutInfo = new NET_OUT_GET_DEFENCEMODE()
            {
                dwSize = (uint)Marshal.SizeOf(typeof(NET_OUT_GET_DEFENCEMODE)),
            };

            bool result = NETClient.GetDefenceArmMode(_LoginID, stuinfo, ref stuOutInfo, 3000);
            if (!result)
            {
               
                dic.Add(-1, "获取失败");
            }
            else
            {
              
                for (int i = 0; i < stuOutInfo.nDefenceNum; ++i)
                {
                    dic.Add(i,stuOutInfo.anDefenceState[i].ToString());
                }
               
            }
            return dic;


        }

        /// <summary>
        /// 单防区撤布防设置
        /// </summary>
        /// <param name="_LoginID">登陆ID</param>
        /// <param name="InChannel">设定通道ID</param>
        /// <param name="password">报警主机密码</param>
        /// <param name="DefenceState">操作状态 1表示布防，2表示撤防</param>
        /// <returns></returns>
        public bool SetDefenceArmMode(IntPtr _LoginID,int InChannel, string password,int DefenceState)
        {
            NET_IN_SET_DEFENCEMODE stuinfo = new NET_IN_SET_DEFENCEMODE()
            {
                dwSize = (uint)Marshal.SizeOf(typeof(NET_IN_SET_DEFENCEMODE)),
            };
            stuinfo.nChannel = Convert.ToInt32(InChannel);
            
            stuinfo.szPassword = password;
           
            stuinfo.emDefenceMode = (EM_DEFENCEMODE)(Convert.ToInt32(DefenceState));

            bool result = NETClient.SetDefenceArmMode(_LoginID, stuinfo, 3000);
            if (!result)
            {
                Console.WriteLine("SetDefenceArmMode 失败");
                Console.WriteLine("失败错误码,error:{0}", NETClient.GetLastError());
                return false;
               
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("SetDefenceArmMode 成功!");
                return true;
            }
        }

        #endregion

        #region 报警主机状态查询
        /// <summary>
        /// 查询主机在线状态，0表示未分配1表示离线2表示在线
        /// </summary>
        /// <param name="_LoginID"></param>
        /// <returns></returns>
        public Dictionary<int,string> GetConnectionStatus(IntPtr _LoginID)
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            NET_IN_GETCONNECTION_STATUS stuinfo = new NET_IN_GETCONNECTION_STATUS()
            {
                dwSize = (uint)Marshal.SizeOf(typeof(NET_IN_GETCONNECTION_STATUS)),
            };

            NET_OUT_GETCONNECTION_STATUS stuOutInfo = new NET_OUT_GETCONNECTION_STATUS()
            {
                dwSize = (uint)Marshal.SizeOf(typeof(NET_OUT_GETCONNECTION_STATUS)),
            };

            bool result = NETClient.GetConnectionStatus(_LoginID, stuinfo, ref stuOutInfo, 3000);
            if (!result)
            {
                Console.WriteLine("GetConnectionStatus 失败");
                Console.WriteLine("失败错误码,error:{0}", NETClient.GetLastError());
                dic.Add(-1, "获取失败");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("GetConnectionStatus 成功，nStatus-----0:未分配1:离线2:在线");
                for (int i = 0; i < stuOutInfo.nChannelNum; ++i)
                {
                    dic.Add(i, stuOutInfo.nStatus[i].ToString());
                }
            }

            return dic;
        }
        /// <summary>
        /// 获取主机激活防区编号
        /// </summary>
        /// <param name="_LoginID"></param>
        /// <returns></returns>
        public Dictionary<int, string> GetAlarmStatus(IntPtr _LoginID)
        {
            bool result = false;
            Dictionary<int, string> dic = new Dictionary<int, string>();
            NET_ACTIVATEDDEFENCEAREA info = new NET_ACTIVATEDDEFENCEAREA()
            {
                dwSize = (uint)Marshal.SizeOf(typeof(NET_ACTIVATEDDEFENCEAREA)),
            };

            info.nAlarmInCount = 16;
            NET_ACTIVATEDDEFENCEAREA_INFO[] stuAlarmInDefenceAreaInfo = new NET_ACTIVATEDDEFENCEAREA_INFO[16];
            IntPtr stuAlarmParamPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(NET_ACTIVATEDDEFENCEAREA_INFO)) * 16);
            for (int i = 0; i < stuAlarmInDefenceAreaInfo.Length; i++)
            {
                stuAlarmInDefenceAreaInfo[i].dwSize = (uint)Marshal.SizeOf(typeof(NET_ACTIVATEDDEFENCEAREA_INFO));
                Marshal.StructureToPtr(stuAlarmInDefenceAreaInfo[i], stuAlarmParamPtr + Marshal.SizeOf(typeof(NET_ACTIVATEDDEFENCEAREA_INFO)) * i, true);
            }
            info.pstuAlarmInDefenceAreaInfo = stuAlarmParamPtr;


            info.nExAlarmInCount = 64;
            NET_ACTIVATEDDEFENCEAREA_INFO[] stuExAlarmInDefenceAreaInfo = new NET_ACTIVATEDDEFENCEAREA_INFO[64];
            IntPtr stuExAlarmParamPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(NET_ACTIVATEDDEFENCEAREA_INFO)) * 64);
            for (int i = 0; i < stuExAlarmInDefenceAreaInfo.Length; i++)
            {
                stuExAlarmInDefenceAreaInfo[i].dwSize = (uint)Marshal.SizeOf(typeof(NET_ACTIVATEDDEFENCEAREA_INFO));
                Marshal.StructureToPtr(stuExAlarmInDefenceAreaInfo[i], stuExAlarmParamPtr + Marshal.SizeOf(typeof(NET_ACTIVATEDDEFENCEAREA_INFO)) * i, true);
            }
            info.pstuExAlarmInDefenceAreaInfo = stuExAlarmParamPtr;

            try
            {
                object obj = (object)info;
                result = NETClient.QueryDevState(_LoginID, EM_DEVICE_STATE.ACTIVATEDDEFENCEAREA, ref obj, typeof(NET_ACTIVATEDDEFENCEAREA), 3000);
                if (result)
                {
                    info = (NET_ACTIVATEDDEFENCEAREA)obj;

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("QueryDevState_ActivatedDefenceArea nRetAlarmInCount:{0}, nRetExAlarmInCount: {1}", info.nRetAlarmInCount, info.nRetExAlarmInCount);
                    for (int i = 0; i < info.nRetAlarmInCount; i++)
                    {
                        stuAlarmInDefenceAreaInfo[i] = (NET_ACTIVATEDDEFENCEAREA_INFO)Marshal.PtrToStructure(
                            IntPtr.Add(info.pstuAlarmInDefenceAreaInfo, Marshal.SizeOf(typeof(NET_ACTIVATEDDEFENCEAREA_INFO)) * i), typeof(NET_ACTIVATEDDEFENCEAREA_INFO));
                        Console.WriteLine("nChannel:{0}, stuActivationTime: {1}", stuAlarmInDefenceAreaInfo[i].nChannel, stuAlarmInDefenceAreaInfo[i].stuActivationTime.ToString());
                        dic.Add(stuAlarmInDefenceAreaInfo[i].nChannel, stuAlarmInDefenceAreaInfo[i].stuActivationTime.ToString());
                    }
                    for (int i = 0; i < info.nRetExAlarmInCount; i++)
                    {
                        stuExAlarmInDefenceAreaInfo[i] = (NET_ACTIVATEDDEFENCEAREA_INFO)Marshal.PtrToStructure(
                            IntPtr.Add(info.pstuExAlarmInDefenceAreaInfo, Marshal.SizeOf(typeof(NET_ACTIVATEDDEFENCEAREA_INFO)) * i), typeof(NET_ACTIVATEDDEFENCEAREA_INFO));
                        Console.WriteLine("nChannel:{0}, stuActivationTime: {1}", stuExAlarmInDefenceAreaInfo[i].nChannel, stuExAlarmInDefenceAreaInfo[i].stuActivationTime.ToString());
                        dic.Add(stuExAlarmInDefenceAreaInfo[i].nChannel,stuExAlarmInDefenceAreaInfo[i].stuActivationTime.ToString());
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("QueryDevState_ActivatedDefenceArea fail, {0}", NETClient.GetLastError());
                    dic.Add(-1,"获取失败");
                }
            }
            finally
            {
                Marshal.FreeHGlobal(stuAlarmParamPtr);
                Marshal.FreeHGlobal(stuExAlarmParamPtr);
            }
            return dic;
        }


        #endregion

    }
}
