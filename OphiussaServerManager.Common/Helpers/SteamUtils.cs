// Decompiled with JetBrains decompiler
// Type: ServerManagerTool.Common.Utils.SteamUtils
// Assembly: ServerManager.Common, Version=1.1.445.1, Culture=neutral, PublicKeyToken=null
// MVID: 286B49CC-C102-462A-B492-86CC6465D89B
// Assembly location: C:\Users\Utilizador\Downloads\ArkServerManager_1.1.445\ServerManager.Common.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using NeXt.Vdf;
using NLog;
using OphiussaServerManager.Common.Helpers;
using OphiussaServerManager.Common.Models;

namespace OphiussaServerManager.Common {
    public class SteamUtils {
        private const           string   KeyworkQuit = "+quit";
        private static readonly Logger   Logger      = LogManager.GetCurrentClassLogger();
        private readonly        Settings _settings;

        public SteamUtils(Settings settings) {
            _settings = settings;
        }

        public string SteamWebApiKey => /* !string.IsNullOrWhiteSpace(_settings.SteamKey) ? */ _settings.SteamKey /*: _settings.DefaultSteamKey*/;

        public WorkshopFileDetailResponse GetSteamModDetails(string appId) {
            int num1            = 0;
            int num2            = 1;
            var steamModDetails = new WorkshopFileDetailResponse();
            if (string.IsNullOrWhiteSpace(SteamWebApiKey))
                return steamModDetails;
            try {
                do {
                    var webRequest =
                        WebRequest.Create(string.Format("https://api.steampowered.com/IPublishedFileService/QueryFiles/v1/?key={0}&format=json&query_type=1&page={1}&numperpage={2}&appid={3}&match_all_tags=0&include_recent_votes_only=0&totalonly=0&return_vote_data=0&return_tags=0&return_kv_tags=0&return_previews=0&return_children=0&return_short_description=0&return_for_sale_data=0&return_metadata=1",
                                                        SteamWebApiKey, num2, 100, appId));
                    webRequest.Timeout = 30000;
                    var fileDetailResult = JsonUtils.Deserialize<WorkshopFileDetailResult>(new StreamReader(webRequest.GetResponse().GetResponseStream()).ReadToEnd());
                    if (fileDetailResult != null && fileDetailResult.Response != null) {
                        if (num1 == 0) {
                            num1            = 1;
                            steamModDetails = fileDetailResult.Response;
                            if (steamModDetails.Total > 100) {
                                int result;
                                num1 = Math.DivRem(steamModDetails.Total, 100, out result);
                                if (result > 0)
                                    ++num1;
                            }
                        }
                        else if (fileDetailResult.Response.Publishedfiledetails != null) {
                            steamModDetails.Publishedfiledetails.AddRange(fileDetailResult.Response.Publishedfiledetails);
                        }

                        ++num2;
                    }
                    else {
                        break;
                    }
                } while (num2 <= num1);

                return steamModDetails;
            }
            catch (Exception ex) {
                Logger.Error("GetSteamModDetails. " + ex.Message + "\r\n" + ex.StackTrace);
                return null;
            }
        }

        public PublishedFileDetailsResponse GetSteamModDetails(List<string> modIdList) {
            return GetSteamModDetails(new List<(string, List<string>)> {
                                                                           ("", modIdList)
                                                                       });
        }

        public PublishedFileDetailsResponse GetSteamModDetails(
            List<(string AppId, List<string> ModIdList)> appMods) {
            PublishedFileDetailsResponse steamModDetails = null;
            if (string.IsNullOrWhiteSpace(SteamWebApiKey))
                return steamModDetails;
            try {
                if (appMods == null || appMods.Count == 0)
                    return new PublishedFileDetailsResponse();
                foreach (var appMod in appMods) {
                    var appModsModIdList = appMod.ModIdList.FindAll(m => !string.IsNullOrWhiteSpace(m));

                    if (appModsModIdList.Count != 0) {
                        int result;
                        int num1 = Math.DivRem(appModsModIdList.Count, 20, out result);
                        if (result > 0)
                            ++num1;
                        for (int index1 = 0; index1 < num1; ++index1) {
                            int    num2 = 0;
                            string str  = "";
                            for (int index2 = index1 * 20; num2 < 20 && index2 < appModsModIdList.Count; ++index2) {
                                str += string.Format("&publishedfileids[{0}]={1}", num2, appModsModIdList[index2]);
                                ++num2;
                            }

                            byte[] bytes      = Encoding.ASCII.GetBytes(string.Format("key={0}&format=json&itemcount={1}{2}", SteamWebApiKey, num2, str));
                            var    webRequest = WebRequest.Create("https://api.steampowered.com/ISteamRemoteStorage/GetPublishedFileDetails/v1/");
                            webRequest.Timeout       = 30000;
                            webRequest.Method        = "POST";
                            webRequest.ContentType   = "application/x-www-form-urlencoded";
                            webRequest.ContentLength = bytes.Length;
                            using (var requestStream = webRequest.GetRequestStream()) {
                                requestStream.Write(bytes, 0, bytes.Length);
                            }

                            var fileDetailsResult = JsonUtils.Deserialize<PublishedFileDetailsResult>(new StreamReader(webRequest.GetResponse().GetResponseStream()).ReadToEnd());
                            if (fileDetailsResult != null && fileDetailsResult.Response != null) {
                                if (steamModDetails == null) {
                                    steamModDetails = fileDetailsResult.Response;
                                }
                                else {
                                    steamModDetails.Resultcount += fileDetailsResult.Response.Resultcount;
                                    steamModDetails.Publishedfiledetails.AddRange(fileDetailsResult.Response.Publishedfiledetails);
                                }
                            }
                        }
                    }
                }

                return steamModDetails ?? new PublishedFileDetailsResponse();
            }
            catch (Exception ex) {
                Logger.Error("GetSteamModDetails. " + ex.Message + "\r\n" + ex.StackTrace);
                return null;
            }
        }

        public SteamUserDetailResponse GetSteamUserDetails(List<string> steamIdList) {
            SteamUserDetailResponse steamUserDetails = null;
            if (string.IsNullOrWhiteSpace(SteamWebApiKey))
                return steamUserDetails;
            try {
                if (steamIdList.Count == 0)
                    return new SteamUserDetailResponse();
                steamIdList = steamIdList.Distinct().ToList();
                steamIdList = steamIdList.Where(i => long.TryParse(i, out long _)).ToList();
                if (steamIdList.Count == 0)
                    return new SteamUserDetailResponse();
                int result;
                int num1 = Math.DivRem(steamIdList.Count, 100, out result);
                if (result > 0)
                    ++num1;
                for (int index1 = 0; index1 < num1; ++index1) {
                    int    num2 = 0;
                    string str1 = "";
                    string str2 = "";
                    for (int index2 = index1 * 100; num2 < 100 && index2 < steamIdList.Count; ++index2) {
                        str1 = str1 + str2 + steamIdList[index2];
                        str2 = ",";
                        ++num2;
                    }

                    var webRequest = WebRequest.Create("https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v2/?key=" + SteamWebApiKey + "&format=json&steamids=" + str1);
                    webRequest.Timeout = 30000;
                    var userDetailResult = JsonUtils.Deserialize<SteamUserDetailResult>(new StreamReader(webRequest.GetResponse().GetResponseStream()).ReadToEnd());
                    if (userDetailResult != null && userDetailResult.Response != null) {
                        if (steamUserDetails == null)
                            steamUserDetails = userDetailResult.Response;
                        else
                            steamUserDetails.Players.AddRange(userDetailResult.Response.Players);
                    }
                }

                return steamUserDetails ?? new SteamUserDetailResponse();
            }
            catch (Exception ex) {
                Logger.Error("GetSteamUserDetails. " + ex.Message + "\r\n" + ex.StackTrace);
                return null;
            }
        }

        public SteamCmdAppManifest ReadSteamCmdAppManifestFile(string file) {
            return string.IsNullOrWhiteSpace(file) || !File.Exists(file) ? null : SteamCmdManifestDetailsResult.Deserialize(VdfDeserializer.FromFile(file).Deserialize());
        }

        public SteamCmdAppWorkshop ReadSteamCmdAppWorkshopFile(string file) {
            return string.IsNullOrWhiteSpace(file) || !File.Exists(file) ? null : SteamCmdWorkshopDetailsResult.Deserialize(VdfDeserializer.FromFile(file).Deserialize());
        }

        public Process GetSteamProcess() {
            string steamClientFile = Path.Combine(_settings.SteamCmdLocation, "steamcmd.exe");
            if (string.IsNullOrWhiteSpace(steamClientFile) || !File.Exists(steamClientFile))
                return null;
            string  a               = IOUtils.NormalizePath(steamClientFile);
            var     processesByName = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(steamClientFile));
            Process steamProcess    = null;
            foreach (var process in processesByName) {
                string mainModuleFilepath = ProcessUtils.GetMainModuleFilepath(process.Id);
                if (string.Equals(a, mainModuleFilepath, StringComparison.OrdinalIgnoreCase)) {
                    steamProcess = process;
                    break;
                }
            }

            return steamProcess;
        }

        public static string BuildSteamCmdArguments(bool removeQuit, string argumentString) {
            if (string.IsNullOrWhiteSpace(argumentString))
                return argumentString;
            string str1 = argumentString.TrimEnd(' ');
            if (str1.ToLower().EndsWith("+quit") & removeQuit)
                return str1.Substring(0, str1.Length - "+quit".Length);
            if (!str1.ToLower().EndsWith("+quit") && !removeQuit) {
                string str2;
                return str2 = str1 + " +quit";
            }

            return str1.TrimEnd(' ');
        }

        public static string BuildSteamCmdArguments(
            bool            removeQuit,
            string          argumentFormatString,
            params string[] argumentValues) {
            if (string.IsNullOrWhiteSpace(argumentFormatString) || argumentValues == null || argumentValues.Length == 0)
                return argumentFormatString;
            string argumentString = string.Format(argumentFormatString, argumentValues);
            return BuildSteamCmdArguments(removeQuit, argumentString);
        }

        public static List<int> GetExitStatusList(string value) {
            if (string.IsNullOrWhiteSpace(value))
                return new List<int>();
            return new List<int>(Array.ConvertAll(value.Split(new char[1] {
                                                                              ','
                                                                          }, StringSplitOptions.RemoveEmptyEntries), int.Parse));
        }
    }
}