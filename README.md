# Additional Notification Settings (JworkzNeosFixFrickenSync)

A [ResoniteModLoader](https://github.com/resonite-modding-group/ResoniteModLoader) mod for [Resonite](https://resonite.com/) that allows you to disable certain notifications from vanilla Resonite from appearing. This includes the following:

* Contact Request Notifications
* "X is Online" Notifications
* Text Message Received Notifications
* Object Received Notifications
* Voice Message Received Notifications
* Session Invite Notifications

In addition, the mod provides the ability to add a cooldown to the "X is Online" notifications to help cut down on the notification spams.

## Installation
1. Install [ResoniteModLoader](https://github.com/resonite-modding-group/ResoniteModLoader).
2. Place [JworkzResoniteAdditionalNotificationSettings.dll](https://github.com/stiefeljackal/JworkzResoniteAdditionalNotificationSettings/releases/latest/download/JworkzResoniteAdditionalNotificationSettings.dll) into your `rml_mods` folder. This folder should be located in the same folder as Resonite. For a default installation, the typical location is `C:\Program Files (x86)\Steam\steamapps\common\Resonite\rml_mods`. You can create it if it's missing, or if you launch the game once with ResoniteModLoader installed it will create the folder for you.
3. Start the game. If you want to verify that the mod is working, you can check your Resonite logs.

## Config Options

|Config Option                            |Default   |Description                                                         |
|-----------------------------------------|----------|--------------------------------------------------------------------|
|`enabled`                                |`true`    |Enables the mod.                                                    |
|`allow_contact_request_notifications`    |`true`    |Allows contact request notifications to appear.                     |
|`allow_is_online_notifications`          |`true`    |Allows 'Is Online' notifications to appear.                         |
|`allow_msg_text_received_notification`   |`true`    |Allows text received notifications from contacts to appear.         |
|`allow_msg_object_received_notifications`|`true`    |Allows object received notifications from contacts to appear.       |
|`allow_msg_sound_received_notifications` |`true`    |Allows voice message received notifications from contacts to appear.|
|`allow_msg_invite_received_notifications`|`true`    |Allows invitation received notifications from contacts to appear.   |
|`is_online_delay_in_seconds`             |`0`       |The number of seconds to wait until showing a notification of the same user coming online. Max seconds is the max value of ushort (roughly 18 hours).|

# Thank You

* 