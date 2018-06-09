using System.IO;
using Newtonsoft.Json;

namespace mastodon.Tests
{
    /// <summary>
    /// Temporary settings management
    /// </summary>
    public class Settings
    {
        private static LocalSettings _localSettings;
        private const string LocalSettingsFullPath = @"C:\Temp\localsettings.json";

        public static string InstanceName => GetLocalSettings().InstanceName;

        public static string ClientId
        {
            get => GetLocalSettings().ClientId;
            set
            {
                var settings = GetLocalSettings();
                settings.ClientId = value;
                SetLocalSettings(settings);
            }
        }

        public static string ClientSecret
        {
            get => GetLocalSettings().ClientSecret;
            set
            {
                var settings = GetLocalSettings();
                settings.ClientSecret = value;
                SetLocalSettings(settings);
            }
        }

        public static string UserLogin => GetLocalSettings().UserLogin;

        public static string UserPassword => GetLocalSettings().UserPassword;

        private static LocalSettings GetLocalSettings()
        {
            if (_localSettings != null) return _localSettings;

            if (File.Exists(LocalSettingsFullPath))
            {
                var json = File.ReadAllText(LocalSettingsFullPath);
                return JsonConvert.DeserializeObject<LocalSettings>(json);
            }
            else
            {
                var newSettings = new LocalSettings();
                SetLocalSettings(newSettings);
                return newSettings;
            }
        }

        private static void SetLocalSettings(LocalSettings settings)
        {
            var json = JsonConvert.SerializeObject(settings);
            File.WriteAllText(LocalSettingsFullPath, json);
        }
    }

    public class LocalSettings
    {
        public string InstanceName { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string UserLogin { get; set; }
        public string UserPassword { get; set; }
    }
}