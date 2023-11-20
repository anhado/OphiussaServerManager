// Decompiled with JetBrains decompiler
// Type: ServerManagerTool.Common.Utils.SteamUtils
// Assembly: ServerManager.Common, Version=1.1.445.1, Culture=neutral, PublicKeyToken=null
// MVID: 286B49CC-C102-462A-B492-86CC6465D89B
// Assembly location: C:\Users\Utilizador\Downloads\ArkServerManager_1.1.445\ServerManager.Common.dll

using NeXt.Vdf;
using NLog;
using OphiussaServerManager.Common;
using OphiussaServerManager.Common.Helpers;
using OphiussaServerManager.Common.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace OphiussaServerManager.Common
{
    public class CurseForgeUtils
    {
        Settings _settings;
        public CurseForgeUtils(Settings settings)
        {
            _settings = settings;
        }
        private const string KEYWORK_QUIT = "+quit";
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public CurseForgeFileDetailResponse GetCurseForgeModDetails(string appId)
        {
            int num1 = 0;
            int num2 = 0;
            CurseForgeFileDetailResponse steamModDetails = new CurseForgeFileDetailResponse();
            if (string.IsNullOrWhiteSpace(CurseForgeWebApiKey))
                return steamModDetails;
            try
            {
                do
                {
                    //WebRequest webRequest =
                    HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(string.Format("https://api.curseforge.com/v1/mods/search?gameId={0}&index={1}", (object)appId, (object)num2));
                    webRequest.Headers.Add("x-api-key", CurseForgeWebApiKey);
                    webRequest.Accept = "application/json";
                    webRequest.Timeout = 30000;
                    //var x = new StreamReader(webRequest.GetResponse().GetResponseStream()).ReadToEnd();

                    //var asd = JsonUtils.Deserialize<CurseForgeFileDetailResponse>(x);

                    CurseForgeFileDetailResponse fileDetailResult = JsonUtils.Deserialize<CurseForgeFileDetailResponse>(new StreamReader(webRequest.GetResponse().GetResponseStream()).ReadToEnd());
                    if (fileDetailResult != null && fileDetailResult.data != null)
                    {
                        if (num1 == 0)
                        {
                            num1 = 1;
                            steamModDetails = fileDetailResult;
                            if (steamModDetails.pagination.totalCount > 50)
                            {
                                int result;
                                num1 = Math.DivRem(steamModDetails.pagination.totalCount, 50, out result);
                                if (result > 0)
                                    ++num1;
                            }
                        }
                        else if (fileDetailResult.data != null)
                            steamModDetails.data.AddRange((IEnumerable<CurseForgeFileDetail>)fileDetailResult.data);
                        ++num2;
                    }
                    else
                        break;
                }
                while (num2 <= num1);
                return steamModDetails;
            }
            catch (Exception ex)
            {
                CurseForgeUtils._logger.Error("GetSteamModDetails. " + ex.Message + "\r\n" + ex.StackTrace);
                return (CurseForgeFileDetailResponse)null;
            }
        }

        public CurseForgeFileDetailResponse GetCurseForgeModDetails(List<string> modIdList) => GetSteamModDetails(new List<(string, List<string>)>()
    {
      ("", modIdList)
    });

        public CurseForgeFileDetailResponse GetSteamModDetails(
          List<(string AppId, List<string> ModIdList)> appMods)
        {
            CurseForgeFileDetailResponse steamModDetails = (CurseForgeFileDetailResponse)null;
            if (string.IsNullOrWhiteSpace(CurseForgeWebApiKey))
                return steamModDetails;
            try
            {
                if (appMods == null || appMods.Count == 0)
                    return new CurseForgeFileDetailResponse();
                foreach ((string AppId, List<string> ModIdList) appMod in appMods)
                {
                    if (appMod.ModIdList.Count != 0)
                    {

                        string payload = "{\"modIds\": [" + string.Join(",", appMod.ModIdList.ToArray()) + "],\"filterPcOnly\": true}";

                        HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://api.curseforge.com/v1/mods");
                        webRequest.Headers.Add("x-api-key", CurseForgeWebApiKey);
                        webRequest.Accept = "application/json";
                        webRequest.ContentType = "application/json";
                        webRequest.Timeout = 30000;
                        webRequest.Method = "POST";

                        byte[] bytes = Encoding.ASCII.GetBytes(payload);

                        using (Stream requestStream = webRequest.GetRequestStream())
                            requestStream.Write(bytes, 0, bytes.Length);

                        CurseForgeFileDetailResponse fileDetailsResult = JsonUtils.Deserialize<CurseForgeFileDetailResponse>(new StreamReader(webRequest.GetResponse().GetResponseStream()).ReadToEnd());
                        if (fileDetailsResult != null && fileDetailsResult.data != null)
                        {
                            if (steamModDetails == null)
                            {
                                steamModDetails = fileDetailsResult;
                            }
                            else
                            {
                                steamModDetails.pagination.resultCount += fileDetailsResult.pagination.resultCount;
                                steamModDetails.data.AddRange((IEnumerable<CurseForgeFileDetail>)fileDetailsResult.data);
                            }
                        } 
                    }
                }
                return steamModDetails ?? new CurseForgeFileDetailResponse();
            }
            catch (Exception ex)
            {
                CurseForgeUtils._logger.Error("GetSteamModDetails. " + ex.Message + "\r\n" + ex.StackTrace);
                return (CurseForgeFileDetailResponse)null;
            }
        } 
        public string CurseForgeWebApiKey => !string.IsNullOrWhiteSpace(_settings.CurseForgeKey) ? _settings.CurseForgeKey : _settings.DefaultCurseForgeKey;
         
    }
}
