
using Newtonsoft.Json;
using System.IO;


namespace OphiussaServerManager.Common.Helpers
{

    public static class JsonUtils
    {
        public static string Serialize<T>(T value, JsonSerializerSettings settings = null)
        {
            if ((object)value == null)
                return string.Empty;
            try
            {
                return settings != null ? JsonConvert.SerializeObject((object)value, Formatting.Indented, settings) : JsonConvert.SerializeObject((object)value, Formatting.Indented);
            }
            catch
            {
                return string.Empty;
            }
        }

        public static bool SerializeToFile<T>(
          T value,
          string filename,
          JsonSerializerSettings settings = null)
        {
            if ((object)value == null)
                return false;
            try
            {
                string contents = JsonUtils.Serialize<T>(value, settings);
                File.WriteAllText(filename, contents);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static T Deserialize<T>(string jsonString, JsonSerializerSettings settings = null)
        {
            if (string.IsNullOrEmpty(jsonString))
                return default(T);
            try
            {
                return settings != null ? JsonConvert.DeserializeObject<T>(jsonString, settings) : JsonConvert.DeserializeObject<T>(jsonString);
            }
            catch
            {
                return default(T);
            }
        }

        public static T DeserializeAnonymousType<T>(
          string jsonString,
          T anonTypeObject,
          JsonSerializerSettings settings = null)
        {
            if (string.IsNullOrEmpty(jsonString))
                return anonTypeObject;
            try
            {
                return settings != null ? JsonConvert.DeserializeAnonymousType<T>(jsonString, anonTypeObject, settings) : JsonConvert.DeserializeAnonymousType<T>(jsonString, anonTypeObject);
            }
            catch
            {
                return anonTypeObject;
            }
        }

        public static T DeserializeFromFile<T>(string file, JsonSerializerSettings settings = null)
        {
            if (!string.IsNullOrEmpty(file))
            {
                if (File.Exists(file))
                {
                    try
                    {
                        return JsonUtils.Deserialize<T>(File.ReadAllText(file), settings);
                    }
                    catch
                    {
                        return default(T);
                    }
                }
            }
            return default(T);
        }

        public static T DeserializeFromFile<T>(
          string file,
          T anonTypeObject,
          JsonSerializerSettings settings = null)
        {
            if (!string.IsNullOrEmpty(file))
            {
                if (File.Exists(file))
                {
                    try
                    {
                        return JsonUtils.DeserializeAnonymousType<T>(File.ReadAllText(file), anonTypeObject, settings);
                    }
                    catch
                    {
                        return anonTypeObject;
                    }
                }
            }
            return anonTypeObject;
        }

        public static void Populate(string jsonString, object target, JsonSerializerSettings settings = null)
        {
            if (string.IsNullOrEmpty(jsonString))
                return;
            if (target == null)
                return;
            try
            {
                if (settings != null)
                    JsonConvert.PopulateObject(jsonString, target, settings);
                else
                    JsonConvert.PopulateObject(jsonString, target);
            }
            catch
            {
            }
        }

        public static void PopulateFromFile(
          string file,
          object target,
          JsonSerializerSettings settings = null)
        {
            if (string.IsNullOrEmpty(file) || !File.Exists(file))
                return;
            if (target == null)
                return;
            try
            {
                JsonUtils.Populate(File.ReadAllText(file), target, settings);
            }
            catch
            {
            }
        }
    }
}
