using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using FrooxEngine;
using SkyFrost.Base;
using HarmonyLib;
using ModConfig = JworkzResoniteMod.JworkzResoniteAdditionalNotificationSettings;
using MessageType = SkyFrost.Base.MessageType;
using OnlineStatus = SkyFrost.Base.OnlineStatus;

namespace JworkzResoniteMod.Patches;

[HarmonyPatch(typeof(NotificationPanel))]
public static class NotificationPanelPatch
{
    private static readonly ConcurrentDictionary<string, DateTimeOffset> _userIsOnlineNotificationTimestampPairs = new ();

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
        if (contact.Contact.IsSelfContact) { return true; }

        var onlineStatus = contact.CurrentStatus.OnlineStatus.GetValueOrDefault();
        if (onlineStatus != OnlineStatus.Online) { return true; }

        if (!ModConfig.AllowIsOnlineNotifications) {  return false; }

        var userId = contact.Contact.ContactUserId;
        var hasTimestampEntry = _userIsOnlineNotificationTimestampPairs.ContainsKey(userId);
        var canShowNotification = true;
        
        if (hasTimestampEntry)
        {
            var hasRetrievedTimestamp = _userIsOnlineNotificationTimestampPairs.TryGetValue(userId, out DateTimeOffset lastNotificationSent);
            canShowNotification = hasRetrievedTimestamp && DateTimeOffset.UtcNow.Subtract(lastNotificationSent).Seconds >= ModConfig.IsOnlineDelayInSeconds;
        }

        if (canShowNotification)
        {
            _userIsOnlineNotificationTimestampPairs.AddOrUpdate(userId, DateTimeOffset.UtcNow, (_, _2) => DateTimeOffset.UtcNow);
        }

        return canShowNotification;
    }
}
