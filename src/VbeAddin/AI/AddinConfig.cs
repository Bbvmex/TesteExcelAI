using System;
using System.IO;
using Newtonsoft.Json;

namespace VbeAddin.AI
{
    public class AddinConfig
    {
        [JsonProperty("BaseUrl")]
        public string BaseUrl { get; set; } = "http://localhost:1234/v1";

        [JsonProperty("Model")]
        public string Model { get; set; } = "local-model";

        private static readonly string ConfigPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "VbeAddin", "config.json");

        public static AddinConfig Load()
        {
            if (!File.Exists(ConfigPath))
            {
                var defaults = new AddinConfig();
                defaults.Save();
                return defaults;
            }

            string text = File.ReadAllText(ConfigPath);
            return JsonConvert.DeserializeObject<AddinConfig>(text) ?? new AddinConfig();
        }

        public void Save()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(ConfigPath));
            File.WriteAllText(ConfigPath, JsonConvert.SerializeObject(this, Formatting.Indented));
        }
    }
}
