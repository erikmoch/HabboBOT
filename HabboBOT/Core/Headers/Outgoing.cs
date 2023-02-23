namespace Habot.Headers
{
    internal enum Outgoing : ushort
    {
        Hello = 4000,
        InitDhHandshake = 207,
        Pong = 196,
        GetIdentityAgreementTypes = 3240,
        VersionCheck = 1170,
        UniqueMachineId = 813,
        LoginWithTicket = 415,
        CompleteDhHandshake = 208,
        ClientStatistics = 815,
        LogToEventLog = 482,

        FlatOpc = 391,
        UpdateAvatar = 44,
        ChangeAvatarMotto = 484,
        RequestFriend = 39,
        Dance = 93,
        Posture = 86,
        Expression = 93,
        JoinHabboGroup = 3257,
        Shout = 55,
        Move = 75
    }
}