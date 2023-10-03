using HarmonyLib;
using ResoniteModLoader;

namespace JworkzResoniteMod;

public class JworkzResoniteAdditionalNotificationSettings : ResoniteMod
{
    public const string MOD_NAME = "[Jworkz] Additional Notification Settings";

    public override string Name => MOD_NAME;
    public override string Author => "StiefelJackal";
    public override string Version => "1.0.0";
    public override string Link => "https://github.com/stiefeljackal/JworkzResoniteAdditionalNotificationSettings/";

    [AutoRegisterConfigKey]
    private static readonly ModConfigurationKey<bool> KEY_ENABLE =
        new("enabled", $"Enables the \"{MOD_NAME}\" mod.", () => true);

    [AutoRegisterConfigKey]
    private static readonly ModConfigurationKey<bool> ALLOW_CONTACT_REQUEST_NOTIFICATIONS =
        new("allow_contact_request_notifications", "Allows contact request notifications to appear.", () => true);

    [AutoRegisterConfigKey]
    private static readonly ModConfigurationKey<bool> ALLOW_IS_ONLINE_NOTIFICATIONS =
        new ("allow_is_online_notifications", "Allows 'Is Online' notifications to appear.", () => true);

    [AutoRegisterConfigKey]
    private static readonly ModConfigurationKey<bool> ALLOW_MSG_TEXT_RECEIVED_NOTIFICATIONS =
        new ("allow_msg_text_received_notification", "Allows text received notifications from contacts to appear.", () => true);

    [AutoRegisterConfigKey]
    private static readonly ModConfigurationKey<bool> ALLOW_MSG_OBJECT_RECEIVED_NOTIFICATIONS =
        new ("allow_msg_object_received_notifications", "Allows object received notifications from contacts to appear.", () => true);

    [AutoRegisterConfigKey]
    private static readonly ModConfigurationKey<bool> ALLOW_MSG_SOUND_RECEIVED_NOTIFICATIONS =
        new ("allow_msg_sound_received_notifications", "Allows voice message received notifications from contacts to appear.", () => true);

    [AutoRegisterConfigKey]
    private static readonly ModConfigurationKey<bool> ALLOW_MSG_INVITE_RECEIVED_NOTIFICATIONS =
        new ("allow_msg_invite_received_notifications", "Allows invitation received notifications from contacts to appear.", () => true);

    [AutoRegisterConfigKey]
    public static readonly ModConfigurationKey<short> IS_ONLINE_DELAY_IN_SECONDS =
        new ("is_online_delay_in_seconds", $"The number of seconds to wait until showing a notification of the same user coming online. Max seconds is {short.MaxValue} (roughly 9 hours).", () => 0);
    
    private static ModConfiguration Config;

    public static ModConfiguration.ConfigurationChangedHandler OnBaseModConfigurationChanged;

    private Harmony _harmony;
    private bool _isPrevEnabled;

    public static bool IsEnabled => Config?.GetValue(KEY_ENABLE) ?? false;

    public static bool AllowContactRequestNotifications => Config?.GetValue(ALLOW_CONTACT_REQUEST_NOTIFICATIONS) ?? true;

    public static bool AllowIsOnlineNotifications => Config?.GetValue(ALLOW_IS_ONLINE_NOTIFICATIONS) ?? true;

    public static bool AllowMessageTextReceivedNotifications => Config?.GetValue(ALLOW_MSG_TEXT_RECEIVED_NOTIFICATIONS) ?? true;

    public static bool AllowMessageObjectReceivedNotifications => Config?.GetValue(ALLOW_MSG_OBJECT_RECEIVED_NOTIFICATIONS) ?? true;

    public static bool AllowMessageSoundReceivedNotifications => Config?.GetValue(ALLOW_MSG_SOUND_RECEIVED_NOTIFICATIONS) ?? true;

    public static bool AllowMessageInviteReceivedNotifications => Config?.GetValue(ALLOW_MSG_INVITE_RECEIVED_NOTIFICATIONS) ?? true;

    public static short IsOnlineDelayInSeconds => Config?.GetValue(IS_ONLINE_DELAY_IN_SECONDS) ?? 0;

    /// <summary>
    /// Defines the metadata for the mod and other mod configurations.
    /// </summary>
    /// <param name="builder">The mod configuration definition builder responsible for building and adding details about this mod.</param>
    public override void DefineConfiguration(ModConfigurationDefinitionBuilder builder)
    {
        builder
            .Version(Version)
            .AutoSave(false);
    }

    /// <summary>
    /// Called when the engine initializes.
    /// </summary>
    public override void OnEngineInit()
    {
        _harmony = new Harmony($"jworkz.sjackal.{Name}");
        Config = GetConfiguration();
        Config.OnThisConfigurationChanged += OnConfigurationChanged;

        RefreshMod();
    }

    /// <summary>
    /// Refreshes the current state of the mod.
    /// </summary>
    private void RefreshMod()
    {
        var isEnabled = Config.GetValue(KEY_ENABLE);
        ToggleHarmonyPatchState(isEnabled);
    }

    /// <summary>
    /// Toggls the Enabled and Disabled state of the mod depending on the passed state.
    /// </summary>
    /// <param name="isEnabled">true if the mod should be enabled; otherwise, false if the mod should be disabled.</param>
    private void ToggleHarmonyPatchState(bool isEnabled)
    {
        if (isEnabled == _isPrevEnabled) { return; }

        _isPrevEnabled = isEnabled;


        if (!IsEnabled)
        {
            TurnOffMod();
        }
        else
        {
            TurnOnMod();
        }
    }

    /// <summary>
    /// Enables the mod.
    /// </summary>
    private void TurnOnMod()
    {
        _harmony.PatchAll();
    }

    /// <summary>
    /// Disables the mod.
    /// </summary>
    private void TurnOffMod()
    {
        _harmony.UnpatchAll(_harmony.Id);
    }

    /// <summary>
    /// Called when the configuration is changed.
    /// </summary>
    /// <param name="event">The event information that details the configuration change.</param>
    private void OnConfigurationChanged(ConfigurationChangedEvent @event) {
        RefreshMod();
        OnBaseModConfigurationChanged(@event);
    }
}