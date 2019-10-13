// ========================================
// Project Name : WodiLib
// File Name    : SoundReleaseManual.cs
//
// MIT License Copyright(c) 2019 kameske
// see LICENSE file
// ========================================

using WodiLib.Sys;
using WodiLib.Sys.Cmn;

namespace WodiLib.Event.EventCommand
{
    /// <inheritdoc />
    /// <summary>
    /// イベントコマンド・サウンド（メモリ手動解放）
    /// </summary>
    public class SoundReleaseManual : SoundBase
    {
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     OverrideMethod
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <inheritdoc />
        public override EventCommandCode EventCommandCode => EventCommandCode.Sound;

        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Protected Override Property
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <inheritdoc />
        /// <summary>処理内容コード値</summary>
        protected override ExecType ExecCode => ExecType.ReleaseManual;

        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     VersionCheck
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <inheritdoc />
        /// <summary>
        /// VersionConfigにセットされたバージョンとイベントコマンドの内容を確認し、
        /// イベントコマンドの内容が設定バージョンに対応していないものであれば警告ログを出力する。
        /// </summary>
        public override void OutputVersionWarningLogIfNeed()
        {
            if (VersionConfig.IsGreaterVersion(WoditorVersion.Ver2_20))
            {
                OutputVersionWarningLogIfNeed_GreaterVer2_20();
            }
        }

        /// <summary>
        /// 設定バージョン = 2.20以上 の場合の警告
        /// </summary>
        private void OutputVersionWarningLogIfNeed_GreaterVer2_20()
        {
            Logger.Warning(VersionWarningMessage.NotGreaterInCommand($"{nameof(SoundReleaseAll)}",
                VersionConfig.GetConfigWoditorVersion(),
                WoditorVersion.Ver2_20));
        }
    }
}