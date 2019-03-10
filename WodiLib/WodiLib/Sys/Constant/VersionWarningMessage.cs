namespace WodiLib.Sys
{
    /// <summary>
    /// ウディタバージョンによる警告メッセージ
    /// </summary>
    internal static class VersionWarningMessage
    {
        /// <summary>
        /// 指定バージョン未満のバージョンを指定できないコマンドの警告メッセージ
        /// </summary>
        /// <param name="itemName">項目名</param>
        /// <param name="currentVersion">設定バージョン</param>
        /// <param name="targetVersion">要求バージョン</param>
        /// <returns></returns>
        public static string NotUnderInCommand(string itemName, WoditorVersion currentVersion, WoditorVersion targetVersion)
        {
            return $"{itemName}は現在の設定バージョン（{currentVersion.VersionName}）では使用できないコマンドです。" +
                   $"(必要バージョン：{targetVersion.VersionName}）";
        }

        /// <summary>
        /// 指定バージョン未満のバージョンを指定できないコマンド設定の警告メッセージ
        /// </summary>
        /// <param name="itemName">項目名</param>
        /// <param name="targetName">設定対象名</param>
        /// <param name="currentVersion">設定バージョン</param>
        /// <param name="targetVersion">要求バージョン</param>
        /// <returns></returns>
        public static string NotUnderInCommandSetting(string itemName, string targetName, WoditorVersion currentVersion, WoditorVersion targetVersion)
        {
            return $"{itemName} {targetName}は現在の設定バージョン（{currentVersion.VersionName}）では使用できない設定です。" +
                   $"(必要バージョン：{targetVersion.VersionName}）";
        }

        /// <summary>
        /// 指定バージョン以上のバージョンを指定できないコマンドの警告メッセージ
        /// </summary>
        /// <param name="itemName">項目名</param>
        /// <param name="currentVersion">設定バージョン</param>
        /// <param name="targetVersion">要求バージョン</param>
        /// <returns></returns>
        public static string NotGreaterInCommand(string itemName, WoditorVersion currentVersion, WoditorVersion targetVersion)
        {
            return $"{itemName}は {targetVersion.VersionName} 以上のバージョンでは使用できないコマンドです。" +
                   $"（現在の設定バージョン：{currentVersion.VersionName}";
        }

        /// <summary>
        /// 指定バージョン以上のバージョンを指定できないコマンド設定の警告メッセージ
        /// </summary>
        /// <param name="itemName">項目名</param>
        /// <param name="targetName">設定対象名</param>
        /// <param name="currentVersion">設定バージョン</param>
        /// <param name="targetVersion">要求バージョン</param>
        /// <returns></returns>
        public static string NotGreaterInCommandSetting(string itemName, string targetName, WoditorVersion currentVersion, WoditorVersion targetVersion)
        {
            return $"{itemName} {targetName}は {targetVersion.VersionName} 以上のバージョンでは使用できないコマンドです。" +
                   $"（現在の設定バージョン：{currentVersion.VersionName}";
        }

        /// <summary>
        /// 指定バージョン未満のバージョンを指定できないコマンドの警告メッセージ
        /// </summary>
        /// <param name="itemName">項目名</param>
        /// <param name="currentVersion">設定バージョン</param>
        /// <param name="targetVersion">要求バージョン</param>
        /// <returns></returns>
        public static string NotUnderInCharaMoveCommand(string itemName, WoditorVersion currentVersion, WoditorVersion targetVersion)
        {
            return $"{itemName}は現在の設定バージョン（{currentVersion.VersionName}）では使用できない動作指定コマンドです。" +
                   $"(必要バージョン：{targetVersion.VersionName}）";
        }


        /// <summary>
        /// 指定バージョン未満のバージョンを指定できないコマンドの警告メッセージ
        /// </summary>
        /// <param name="value">項目名</param>
        /// <param name="currentVersion">設定バージョン</param>
        /// <param name="targetVersion">要求バージョン</param>
        /// <returns></returns>
        public static string NotUnderInVariableAddress(int value, WoditorVersion currentVersion, WoditorVersion targetVersion)
        {
            return $"変数アドレス {value}は現在の設定バージョン（{currentVersion.VersionName}）では定義されていない値です。" +
                   $"(必要バージョン：{targetVersion.VersionName}）";
        }


    }
}