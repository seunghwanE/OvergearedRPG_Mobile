#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("uaKVgLyNFPctwmdPlHwtsO7QvLFbdcIraKAMCp97qqa8Kwuxd88tOYQ8ivLCwOwvCQG8OdpUZjWedo2A6gVsU0aCpVjCqNzcaHAZRx2ZrqfDyGNLsAY6bQadNbGNfL3ko33vXWh+X/CbjTpipy6J/9lodUQlv4bUssvCzC1AcH9Jre6WsYDCOoIn9PLiVDDEXu/4PBl/T52kRlwekAukcyB8bCMoi6DnVNAjvKScHdp/Zr2xathbeGpXXFNw3BLcrVdbW1tfWlnYW1VaathbUFjYW1tau1BYELCgTCHjVHkgJUHaxXuvsi0PiZ5NPQA2buQAC9H6YTHx6xyHbPKzJ3rXpA3+RPg74jpBZDKn/lD6t9jPdqpKPEqQS3LehE+AyVhZW1pb");
        private static int[] order = new int[] { 10,4,13,7,9,7,6,12,11,10,10,11,12,13,14 };
        private static int key = 90;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
