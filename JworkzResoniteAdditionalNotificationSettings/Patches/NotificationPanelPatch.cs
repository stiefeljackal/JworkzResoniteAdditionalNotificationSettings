using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrooxEngine;
using SkyFrost.Base;
using HarmonyLib;
using ModConfig = JworkzResoniteMod.JworkzResoniteAdditionalNotificationSettings;
using MessageType = SkyFrost.Base.MessageType;

namespace JworkzResoniteMod.Patches;

[HarmonyPatch(typeof(NotificationPanel))]
public static class NotificationPanelPatch
{
    [HarmonyPrefix]
    [HarmonyPatch("Messages_OnMessageReceived")]
    public static bool MessagesOnMessageReceivedPrefix(Message message)
    {
        switch(message.MessageType)
        {
            case MessageType.Text:
                return ModConfig.AllowMessageTextReceivedNotifications;
            case MessageType.Object:
                return ModConfig.AllowMessageObjectReceivedNotifications;
            case MessageType.Sound:
                return ModConfig.AllowMessageSoundReceivedNotifications;
            case MessageType.SessionInvite:
                return ModConfig.AllowMessageInviteReceivedNotifications;
            default:
                return true;
        }
    }

    [HarmonyPrefix]
    [HarmonyPatch("ContactStatusUpdated")]
    public static bool ContactStatusUpdatePrefix(ContactData contact)
    {
        return ModConfig.AllowIsOnlineNotifications;
    }
}
