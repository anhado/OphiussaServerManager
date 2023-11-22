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
    public class SteamUtils
    {
        Settings _settings;
        public SteamUtils(Settings settings)
        {
            _settings = settings;
        }
        private const string KEYWORK_QUIT = "+quit";
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public WorkshopFileDetailResponse GetSteamModDetails(string appId)
        {
            int num1 = 0;
            int num2 = 1;
            WorkshopFileDetailResponse steamModDetails = new WorkshopFileDetailResponse();
            if (string.IsNullOrWhiteSpace(SteamWebApiKey))
                return steamModDetails;
            try
            {
                do
                {
                    WebRequest webRequest = WebRequest.Create(string.Format("https://api.steampowered.com/IPublishedFileService/QueryFiles/v1/?key={0}&format=json&query_type=1&page={1}&numperpage={2}&appid={3}&match_all_tags=0&include_recent_votes_only=0&totalonly=0&return_vote_data=0&return_tags=0&return_kv_tags=0&return_previews=0&return_children=0&return_short_description=0&return_for_sale_data=0&return_metadata=1", (object)SteamWebApiKey, (object)num2, (object)100, (object)appId));
                    webRequest.Timeout = 30000;
                    WorkshopFileDetailResult fileDetailResult = JsonUtils.Deserialize<WorkshopFileDetailResult>(new StreamReader(webRequest.GetResponse().GetResponseStream()).ReadToEnd());
                    if (fileDetailResult != null && fileDetailResult.response != null)
                    {
                        if (num1 == 0)
                        {
                            num1 = 1;
                            steamModDetails = fileDetailResult.response;
                            if (steamModDetails.total > 100)
                            {
                                int result;
                                num1 = Math.DivRem(steamModDetails.total, 100, out result);
                                if (result > 0)
                                    ++num1;
                            }
                        }
                        else if (fileDetailResult.response.publishedfiledetails != null)
                            steamModDetails.publishedfiledetails.AddRange((IEnumerable<WorkshopFileDetail>)fileDetailResult.response.publishedfiledetails);
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
                SteamUtils._logger.Error("GetSteamModDetails. " + ex.Message + "\r\n" + ex.StackTrace);
                return (WorkshopFileDetailResponse)null;
            }
        }

        public PublishedFileDetailsResponse GetSteamModDetails(List<string> modIdList) => GetSteamModDetails(new List<(string, List<string>)>()
    {
      ("", modIdList)
    });

        public PublishedFileDetailsResponse GetSteamModDetails(
          List<(string AppId, List<string> ModIdList)> appMods)
        {
            PublishedFileDetailsResponse steamModDetails = (PublishedFileDetailsResponse)null;
            if (string.IsNullOrWhiteSpace(SteamWebApiKey))
                return steamModDetails;
            try
            {
                if (appMods == null || appMods.Count == 0)
                    return new PublishedFileDetailsResponse();
                foreach ((string AppId, List<string> ModIdList) appMod in appMods)
                {
                    if (appMod.ModIdList.Count != 0)
                    {
                        int result;
                        int num1 = Math.DivRem(appMod.ModIdList.Count, 20, out result);
                        if (result > 0)
                            ++num1;
                        for (int index1 = 0; index1 < num1; ++index1)
                        {
                            int num2 = 0;
                            string str = "";
                            for (int index2 = index1 * 20; num2 < 20 && index2 < appMod.ModIdList.Count; ++index2)
                            {
                                str += string.Format("&publishedfileids[{0}]={1}", (object)num2, (object)appMod.ModIdList[index2]);
                                ++num2;
                            }
                            byte[] bytes = Encoding.ASCII.GetBytes(string.Format("key={0}&format=json&itemcount={1}{2}", (object)SteamWebApiKey, (object)num2, (object)str));
                            WebRequest webRequest = WebRequest.Create("https://api.steampowered.com/ISteamRemoteStorage/GetPublishedFileDetails/v1/");
                            webRequest.Timeout = 30000;
                            webRequest.Method = "POST";
                            webRequest.ContentType = "application/x-www-form-urlencoded";
                            webRequest.ContentLength = (long)bytes.Length;
                            using (Stream requestStream = webRequest.GetRequestStream())
                                requestStream.Write(bytes, 0, bytes.Length);
                            PublishedFileDetailsResult fileDetailsResult = JsonUtils.Deserialize<PublishedFileDetailsResult>(new StreamReader(webRequest.GetResponse().GetResponseStream()).ReadToEnd());
                            if (fileDetailsResult != null && fileDetailsResult.response != null)
                            {
                                if (steamModDetails == null)
                                {
                                    steamModDetails = fileDetailsResult.response;
                                }
                                else
                                {
                                    steamModDetails.resultcount += fileDetailsResult.response.resultcount;
                                    steamModDetails.publishedfiledetails.AddRange((IEnumerable<PublishedFileDetail>)fileDetailsResult.response.publishedfiledetails);
                                }
                            }
                        }
                    }
                }
                return steamModDetails ?? new PublishedFileDetailsResponse();
            }
            catch (Exception ex)
            {
                SteamUtils._logger.Error("GetSteamModDetails. " + ex.Message + "\r\n" + ex.StackTrace);
                return (PublishedFileDetailsResponse)null;
            }
        }

        public SteamUserDetailResponse GetSteamUserDetails(List<string> steamIdList)
        {
            SteamUserDetailResponse steamUserDetails = (SteamUserDetailResponse)null;
            if (string.IsNullOrWhiteSpace(SteamWebApiKey))
                return steamUserDetails;
            try
            {
                if (steamIdList.Count == 0)
                    return new SteamUserDetailResponse();
                steamIdList = steamIdList.Distinct<string>().ToList<string>();
                steamIdList = steamIdList.Where<string>((Func<string, bool>)(i => long.TryParse(i, out long _))).ToList<string>();
                if (steamIdList.Count == 0)
                    return new SteamUserDetailResponse();
                int result;
                int num1 = Math.DivRem(steamIdList.Count, 100, out result);
                if (result > 0)
                    ++num1;
                for (int index1 = 0; index1 < num1; ++index1)
                {
                    int num2 = 0;
                    string str1 = "";
                    string str2 = "";
                    for (int index2 = index1 * 100; num2 < 100 && index2 < steamIdList.Count; ++index2)
                    {
                        str1 = str1 + str2 + steamIdList[index2];
                        str2 = ",";
                        ++num2;
                    }
                    WebRequest webRequest = WebRequest.Create("https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v2/?key=" + SteamWebApiKey + "&format=json&steamids=" + str1);
                    webRequest.Timeout = 30000;
                    SteamUserDetailResult userDetailResult = JsonUtils.Deserialize<SteamUserDetailResult>(new StreamReader(webRequest.GetResponse().GetResponseStream()).ReadToEnd());
                    if (userDetailResult != null && userDetailResult.response != null)
                    {
                        if (steamUserDetails == null)
                            steamUserDetails = userDetailResult.response;
                        else
                            steamUserDetails.players.AddRange((IEnumerable<SteamUserDetail>)userDetailResult.response.players);
                    }
                }
                return steamUserDetails ?? new SteamUserDetailResponse();
            }
            catch (Exception ex)
            {
                SteamUtils._logger.Error("GetSteamUserDetails. " + ex.Message + "\r\n" + ex.StackTrace);
                return (SteamUserDetailResponse)null;
            }
        }

        public SteamCmdAppManifest ReadSteamCmdAppManifestFile(string file) => string.IsNullOrWhiteSpace(file) || !System.IO.File.Exists(file) ? (SteamCmdAppManifest)null : SteamCmdManifestDetailsResult.Deserialize(VdfDeserializer.FromFile(file).Deserialize());

        public SteamCmdAppWorkshop ReadSteamCmdAppWorkshopFile(string file) => string.IsNullOrWhiteSpace(file) || !System.IO.File.Exists(file) ? (SteamCmdAppWorkshop)null : SteamCmdWorkshopDetailsResult.Deserialize(VdfDeserializer.FromFile(file).Deserialize());

        public  string SteamWebApiKey => !string.IsNullOrWhiteSpace(_settings.SteamKey) ? _settings.SteamKey : _settings.DefaultSteamKey;

        public  Process GetSteamProcess()
        {
            string SteamClientFile = Path.Combine(_settings.SteamCMDLocation, "steamcmd.exe");
            if (string.IsNullOrWhiteSpace(SteamClientFile) || !System.IO.File.Exists(SteamClientFile))
                return (Process)null;
            string a = IOUtils.NormalizePath(SteamClientFile);
            Process[] processesByName = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(SteamClientFile));
            Process steamProcess = (Process)null;
            foreach (Process process in processesByName)
            {
                string mainModuleFilepath = ProcessUtils.GetMainModuleFilepath(process.Id);
                if (string.Equals(a, mainModuleFilepath, StringComparison.OrdinalIgnoreCase))
                {
                    steamProcess = process;
                    break;
                }
            }
            return steamProcess;
        }

        public static string BuildSteamCmdArguments(bool removeQuit, string argumentString)
        {
            if (string.IsNullOrWhiteSpace(argumentString))
                return argumentString;
            string str1 = argumentString.TrimEnd(' ');
            if (str1.ToLower().EndsWith("+quit") & removeQuit)
                return str1.Substring(0, str1.Length - "+quit".Length);
            if (!str1.ToLower().EndsWith("+quit") && !removeQuit)
            {
                string str2;
                return str2 = str1 + " +quit";
            }
            return str1.TrimEnd(' ');
        }

        public static string BuildSteamCmdArguments(
          bool removeQuit,
          string argumentFormatString,
          params string[] argumentValues)
        {
            if (string.IsNullOrWhiteSpace(argumentFormatString) || argumentValues == null || argumentValues.Length == 0)
                return argumentFormatString;
            string argumentString = string.Format(argumentFormatString, (object[])argumentValues);
            return SteamUtils.BuildSteamCmdArguments(removeQuit, argumentString);
        }

        public static List<int> GetExitStatusList(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return new List<int>();
            return new List<int>((IEnumerable<int>)Array.ConvertAll<string, int>(value.Split(new char[1]
            {
        ','
            }, StringSplitOptions.RemoveEmptyEntries), new Converter<string, int>(int.Parse)));
        }
    }
}
