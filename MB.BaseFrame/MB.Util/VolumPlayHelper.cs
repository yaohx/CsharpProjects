using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Media;

namespace MB.Util
{
    public class VolumPlayHelper
    {
        private static readonly string VOLUMN_PLAY_REQUEST_URL_CFG = "VolumnPlayRequestUrl";

        /// <summary>
        /// 返回新的对象实例。
        /// </summary>
        public static VolumPlayHelper NewInstance
        {
            get
            {
                return new VolumPlayHelper();
            }
        }

        /// <summary>
        /// 根据参数生成音频文件
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="volPlayParas"></param>
        public void PlayVol(string requestUrl, Dictionary<string, string> volPlayParas)
        {
            string url = requestUrl;
            if (string.IsNullOrEmpty(url))
                url = MB.Util.AppConfigSetting.GetKeyValue(VOLUMN_PLAY_REQUEST_URL_CFG);

            if (string.IsNullOrEmpty(url))
            {
                MB.Util.TraceEx.Write("由于播放音频的请求为空，使用默认请求：http://vol.dev.mb.com/spi/speak");
                url = "http://vol.dev.mb.com/spi/speak";
            }

            try
            {
                foreach (KeyValuePair<string, string> para in volPlayParas)
                {
                    if (url.IndexOf("?") < 0)
                        url += string.Format("?{0}={1}", para.Key, para.Value);
                    else
                        url += string.Format("&{0}={1}", para.Key, para.Value);
                }

                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

                //异步从网络获取
                httpWebRequest.BeginGetResponse(new AsyncCallback(GetResponseCompletedCallBack), httpWebRequest);
            }
            catch (Exception ex)
            {
                throw MB.Util.APPExceptionHandlerHelper.PromoteException(ex, "PlayVol 出错");
            }
        }

        private void GetResponseCompletedCallBack(IAsyncResult re)
        {

            if (re.IsCompleted)
            {
                try
                {
                    HttpWebRequest httpWebRequest = re.AsyncState as HttpWebRequest;
                    using (WebResponse response = httpWebRequest.EndGetResponse(re))
                    {
                        MemoryStream ms = null;

                        using (Stream stream = response.GetResponseStream())
                        {
                            // Read the response in chunks and save it in a MemoryStream.
                            ms = new MemoryStream();
                            byte[] buffer = new byte[32768];
                            int bytesRead, totalBytesRead = 0;
                            do
                            {
                                bytesRead = stream.Read(buffer, 0, buffer.Length);
                                totalBytesRead += bytesRead;

                                ms.Write(buffer, 0, bytesRead);
                            } while (bytesRead > 0);

                            ms.Position = 0;
                        }

                        using (Stream stream = ms)
                        {
                            using (SoundPlayer player = new SoundPlayer(stream))
                                player.PlaySync();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw MB.Util.APPExceptionHandlerHelper.PromoteException(ex, "PlayVol GetResponseCompletedCallBack 出错");
                }

            }
        }


        /// <summary>
        /// 根据参数生成音频文件
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="text"></param>
        /// <param name="volumn">音量 0->100从低到高</param>
        /// <param name="rate">5 -> -5 从快到慢 </param>
        public void PlayVol(string text)
        {
            PlayVol(text, 100, 1);
        }

        /// <summary>
        /// 根据参数生成音频文件
        /// </summary>
        /// <param name="text">播放的文字</param>
        /// <param name="volumn">音量 0->100从低到高</param>
        /// <param name="rate">5 -> -5 从快到慢 </param>
        public void PlayVol(string text, int volumn, int rate)
        {
            Dictionary<string, string> volPlayParas = new Dictionary<string, string>();
            volPlayParas.Add("text", text);
            volPlayParas.Add("volumn", volumn.ToString());
            volPlayParas.Add("rate", rate.ToString());

            PlayVol(string.Empty, volPlayParas);
        }
    }
}