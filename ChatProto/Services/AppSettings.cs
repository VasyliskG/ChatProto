namespace ChatProto.Services;
public static class AppSettings
{
    private static readonly SettingsService _settings = new SettingsService();
    public static SettingsService Current => _settings;
}
