using System.IO;
using Newtonsoft.Json;

namespace OphiussaServerManager.Common.Helpers {
    public static class JsonUtils {
        public static string Serialize<T>(T value, JsonSerializerSettings settings = null) {
            if (value == null)
                return string.Empty;
            try {
                return settings != null ? JsonConvert.SerializeObject(value, Formatting.Indented, settings) : JsonConvert.SerializeObject(value, Formatting.Indented);
            }
            catch {
                return string.Empty;
            }
        }

        public static bool SerializeToFile<T>(
            T                      value,
            string                 filename,
            JsonSerializerSettings settings = null) {
            if (value == null)
                return false;
            try {
                string contents = Serialize(value, settings);
                File.WriteAllText(filename, contents);
                return true;
            }
            catch {
                return false;
            }
        }

        public static T Deserialize<T>(string jsonString, JsonSerializerSettings settings = null) {
            if (string.IsNullOrEmpty(jsonString))
                return default;
            try {
                return settings != null ? JsonConvert.DeserializeObject<T>(jsonString, settings) : JsonConvert.DeserializeObject<T>(jsonString);
            }
            catch {
                return default;
            }
        }

        public static T DeserializeAnonymousType<T>(
            string                 jsonString,
            T                      anonTypeObject,
            JsonSerializerSettings settings = null) {
            if (string.IsNullOrEmpty(jsonString))
                return anonTypeObject;
            try {
                return settings != null ? JsonConvert.DeserializeAnonymousType(jsonString, anonTypeObject, settings) : JsonConvert.DeserializeAnonymousType(jsonString, anonTypeObject);
            }
            catch {
                return anonTypeObject;
            }
        }

        public static T DeserializeFromFile<T>(string file, JsonSerializerSettings settings = null) {
            if (!string.IsNullOrEmpty(file))
                if (File.Exists(file))
                    try {
                        return Deserialize<T>(File.ReadAllText(file), settings);
                    }
                    catch {
                        return default;
                    }

            return default;
        }

        public static T DeserializeFromFile<T>(
            string                 file,
            T                      anonTypeObject,
            JsonSerializerSettings settings = null) {
            if (!string.IsNullOrEmpty(file))
                if (File.Exists(file))
                    try {
                        return DeserializeAnonymousType(File.ReadAllText(file), anonTypeObject, settings);
                    }
                    catch {
                        return anonTypeObject;
                    }

            return anonTypeObject;
        }

        public static void Populate(string jsonString, object target, JsonSerializerSettings settings = null) {
            if (string.IsNullOrEmpty(jsonString))
                return;
            if (target == null)
                return;
            try {
                if (settings != null)
                    JsonConvert.PopulateObject(jsonString, target, settings);
                else
                    JsonConvert.PopulateObject(jsonString, target);
            }
            catch {
            }
        }

        public static void PopulateFromFile(
            string                 file,
            object                 target,
            JsonSerializerSettings settings = null) {
            if (string.IsNullOrEmpty(file) || !File.Exists(file))
                return;
            if (target == null)
                return;
            try {
                Populate(File.ReadAllText(file), target, settings);
            }
            catch {
            }
        }
    }
}