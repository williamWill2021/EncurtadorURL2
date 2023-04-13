using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace EncurtadorURL.BackEnd.Infraestructure
{
    public static class Configuration
    {
        public static string BaseDirectory { get; set; }
        private static string _apiURL;
        public static string ApiURL
        {
            get { return _apiURL; }
            internal set { _apiURL = value; }
        }

        /// <summary>
        /// Carrega o arquivo de configuração epreenche as propriedades de configuração
        /// </summary>
        public static void Load()
        {
            var fileName = $"Configuration.json";
            Debug.Write($"Loagind configuration file {fileName}");

            var directory = string.Empty;

            if (string.IsNullOrEmpty(BaseDirectory))
            {
                var entryAssembly = Assembly.GetEntryAssembly();

                directory =
                    string.IsNullOrEmpty(entryAssembly?.Location) ?
                    Directory.GetCurrentDirectory() :
                    Path.GetDirectoryName(entryAssembly?.Location);
            }
            else
                directory = BaseDirectory;

            var path = Path.Combine(directory, fileName);

            var json = string.Empty;
            if (File.Exists(path))
                json = File.ReadAllText(path);
            else if (File.Exists(fileName))
                json = File.ReadAllText(fileName);
            else
                throw new FileNotFoundException($"{fileName} não encontrado");

            var obj = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

            if (obj.ContainsKey(nameof(ApiURL)))
            {
                ApiURL = (string)obj[nameof(ApiURL)];
                Debug.WriteLine($"Setting ApiURL value as '{ApiURL}'");
            }
        }
    }
}
