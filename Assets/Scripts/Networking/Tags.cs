namespace Networking
{
    public static class Tags
    {
        public const ushort PlayerConnectTag = 1001;
        public const ushort PlayerDisconnectTag = 1002;
        public const ushort PlayerInformationTag = 1003;
        
        public const ushort LoginPlayer = 2000;
        public const ushort LogoutPlayer = 2001;
        public const ushort AddPlayer = 2002;
        public const ushort LoginSuccess = 2003;
        public const ushort LoginFailed = 2004;
        public const ushort LogoutSuccess = 2005;
        public const ushort AddPlayerSuccess = 2006;
        public const ushort AddPlayedFailed = 2007;
    }
}