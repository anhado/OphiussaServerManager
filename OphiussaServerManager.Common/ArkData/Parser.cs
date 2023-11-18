using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace OphiussaServerManager.Common
{
    internal class Parser
    {
        private static ulong GetId(byte[] data)
        {
            byte[] bytes1 = Encoding.Default.GetBytes("PlayerDataID");
            byte[] bytes2 = Encoding.Default.GetBytes("UInt64Property");
            int offset = data.LocateFirst(bytes1);
            int num = data.LocateFirst(bytes2, offset);
            return BitConverter.ToUInt64(data, num + bytes2.Length + 9);
        }

        private static string GetPlatformId(byte[] data)
        {
            byte[] bytes = Encoding.Default.GetBytes("UniqueNetIdRepl");
            int num = data.LocateFirst(bytes);
            byte[] destinationArray = new byte[9];
            Array.Copy((Array)data, num + bytes.Length, (Array)destinationArray, 0, 9);
            uint length = BitConverter.ToUInt32(destinationArray, 5) - 1U;
            byte[] numArray = new byte[(int)length];
            Array.Copy((Array)data, (long)(num + bytes.Length + destinationArray.Length), (Array)numArray, 0L, (long)length);
            return Encoding.Default.GetString(numArray);
        }

        public static PlayerData ParsePlayer(string fileName)
        {
            FileInfo fileInfo = new FileInfo(fileName);
            if (!fileInfo.Exists)
                return (PlayerData)null;
            byte[] data = File.ReadAllBytes(fileName);
            int num = HelpersArkData.GetInt(data, "TribeId");
            return new PlayerData()
            {
                PlayerId = Path.GetFileNameWithoutExtension(fileInfo.Name),
                PlayerName = HelpersArkData.GetString(data, "PlayerName"),
                CharacterId = Convert.ToInt64(Parser.GetId(data)),
                CharacterName = HelpersArkData.GetString(data, "PlayerCharacterName"),
                TribeId = new int?(num > -1 ? num : HelpersArkData.GetInt(data, "TribeID")),
                Level = (short)(1 + Convert.ToInt32(HelpersArkData.GetUInt16(data, "CharacterStatusComponent_ExtraCharacterLevel"))),
                File = fileName,
                Filename = fileInfo.Name,
                FileCreated = fileInfo.CreationTime,
                FileUpdated = fileInfo.LastWriteTime
            };
        }

        public static Task<PlayerData> ParsePlayerAsync(string fileName) => Task.Run<PlayerData>((Func<PlayerData>)(() => Parser.ParsePlayer(fileName)));

        private static uint GetOwnerId(byte[] data)
        {
            byte[] bytes1 = Encoding.Default.GetBytes("OwnerPlayerDataID");
            byte[] bytes2 = Encoding.Default.GetBytes("UInt32Property");
            int offset = data.LocateFirst(bytes1);
            int num = data.LocateFirst(bytes2, offset);
            return BitConverter.ToUInt32(data, num + bytes2.Length + 9);
        }

        public static TribeData ParseTribe(string fileName)
        {
            FileInfo fileInfo = new FileInfo(fileName);
            if (!fileInfo.Exists)
                return (TribeData)null;
            byte[] data = File.ReadAllBytes(fileName);
            int num = HelpersArkData.GetInt(data, "TribeId");
            return new TribeData()
            {
                Id = num > -1 ? num : HelpersArkData.GetInt(data, "TribeID"),
                Name = HelpersArkData.GetString(data, "TribeName"),
                OwnerId = new int?((int)Parser.GetOwnerId(data)),
                File = fileName,
                Filename = fileInfo.Name,
                FileCreated = fileInfo.CreationTime,
                FileUpdated = fileInfo.LastWriteTime
            };
        }

        public static Task<TribeData> ParseTribeAsync(string fileName) => Task.Run<TribeData>((Func<TribeData>)(() => Parser.ParseTribe(fileName)));
    }
}
