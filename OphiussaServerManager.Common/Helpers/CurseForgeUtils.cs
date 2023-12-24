// Decompiled with JetBrains decompiler
// Type: ServerManagerTool.Common.Utils.SteamUtils
// Assembly: ServerManager.Common, Version=1.1.445.1, Culture=neutral, PublicKeyToken=null
// MVID: 286B49CC-C102-462A-B492-86CC6465D89B
// Assembly location: C:\Users\Utilizador\Downloads\ArkServerManager_1.1.445\ServerManager.Common.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using OphiussaServerManager.Common.Helpers;
using OphiussaServerManager.Common.Models;

namespace OphiussaServerManager.Common {
    public class CurseForgeUtils {
        private const    string   KeyworkQuit = "+quit";
        private readonly Settings _settings;

        public CurseForgeUtils(Settings settings) {
            _settings = settings;
        }

        public string CurseForgeWebApiKey => /*!string.IsNullOrWhiteSpace(_settings.CurseForgeKey) ?*/ _settings.CurseForgeKey /*: _settings.DefaultCurseForgeKey*/;

        public CurseForgeFileDetailResponse GetCurseForgeModDetails(string appId) {
            int num1            = 0;
            int num2            = 0;
            var steamModDetails = new CurseForgeFileDetailResponse();
            if (string.IsNullOrWhiteSpace(CurseForgeWebApiKey))
                return steamModDetails;
            try {
                do {
                    //WebRequest webRequest =
                    var webRequest = (HttpWebRequest)WebRequest.Create(string.Format("https://api.curseforge.com/v1/mods/search?gameId={0}&index={1}", appId, num2));
                    webRequest.Headers.Add("x-api-key", CurseForgeWebApiKey);
                    webRequest.Accept  = "application/json";
                    webRequest.Timeout = 30000;
                    //var x = new StreamReader(webRequest.GetResponse().GetResponseStream()).ReadToEnd();

                    //var asd = JsonUtils.Deserialize<CurseForgeFileDetailResponse>(x);

                    var fileDetailResult = JsonUtils.Deserialize<CurseForgeFileDetailResponse>(new StreamReader(webRequest.GetResponse().GetResponseStream()).ReadToEnd());
                    if (fileDetailResult != null && fileDetailResult.Data != null) {
                        if (fileDetailResult.Data.FindAll(x => x.Id == 932007).Count > 0) {
                            int i = 0;
                            var x = fileDetailResult.Data.FindAll(x2 => x2.Id == 932007);
                        }

                        if (num1 == 0) {
                            num1            = fileDetailResult.Pagination.TotalCount;
                            steamModDetails = fileDetailResult;
                            //if (steamModDetails.pagination.totalCount > 50)
                            //{
                            //    int result;
                            //    num1 = Math.DivRem(steamModDetails.pagination.totalCount, 50, out result);
                            //    if (result > 0)
                            //        ++num1;
                            //}
                        }
                        else if (fileDetailResult.Data != null) {
                            steamModDetails.Data.AddRange(fileDetailResult.Data);
                        }

                        num2 += fileDetailResult.Pagination.PageSize;
                    }
                    else {
                        break;
                    }
                } while (num2 <= num1);

                return steamModDetails;
            }
            catch (Exception ex) {
                OphiussaLogger.Logger.Error("GetSteamModDetails. " + ex.Message + "\r\n" + ex.StackTrace);
                return null;
            }
        }

        public CurseForgeFileDetailResponse GetCurseForgeModDetails(List<string> modIdList) {
            return GetSteamModDetails(new List<(string, List<string>)> {
                                                                           ("", modIdList)
                                                                       });
        }

        public CurseForgeFileDetailResponse GetSteamModDetails(
            List<(string AppId, List<string> ModIdList)> appMods) {
            CurseForgeFileDetailResponse steamModDetails = null;
            if (string.IsNullOrWhiteSpace(CurseForgeWebApiKey))
                return steamModDetails;
            try {
                if (appMods == null || appMods.Count == 0)
                    return new CurseForgeFileDetailResponse();
                foreach (var appMod in appMods)
                    if (appMod.ModIdList.Count != 0) {
                        string payload = "{\"modIds\": [" + string.Join(",", appMod.ModIdList.ToArray()) + "],\"filterPcOnly\": true}";

                        var webRequest = (HttpWebRequest)WebRequest.Create("https://api.curseforge.com/v1/mods");
                        webRequest.Headers.Add("x-api-key", CurseForgeWebApiKey);
                        webRequest.Accept      = "application/json";
                        webRequest.ContentType = "application/json";
                        webRequest.Timeout     = 30000;
                        webRequest.Method      = "POST";

                        byte[] bytes = Encoding.ASCII.GetBytes(payload);

                        using (var requestStream = webRequest.GetRequestStream()) {
                            requestStream.Write(bytes, 0, bytes.Length);
                        }

                        var fileDetailsResult = JsonUtils.Deserialize<CurseForgeFileDetailResponse>(new StreamReader(webRequest.GetResponse().GetResponseStream()).ReadToEnd());
                        if (fileDetailsResult != null && fileDetailsResult.Data != null) {
                            if (steamModDetails == null) {
                                steamModDetails = fileDetailsResult;
                            }
                            else {
                                steamModDetails.Pagination.ResultCount += fileDetailsResult.Pagination.ResultCount;
                                steamModDetails.Data.AddRange(fileDetailsResult.Data);
                            }
                        }
                    }

                return steamModDetails ?? new CurseForgeFileDetailResponse();
            }
            catch (Exception ex) {
                OphiussaLogger.Logger.Error("GetSteamModDetails. " + ex.Message + "\r\n" + ex.StackTrace);
                return null;
            }
        }
    }
}