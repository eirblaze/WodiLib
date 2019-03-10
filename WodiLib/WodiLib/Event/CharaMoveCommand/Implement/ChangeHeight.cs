// ========================================
// Project Name : WodiLib
// File Name    : ChangeHeight.cs
//
// MIT License Copyright(c) 2019 kameske
// see LICENSE file
// ========================================

using WodiLib.Sys;
using WodiLib.Sys.Cmn;

namespace WodiLib.Event.CharaMoveCommand
{
    /// <inheritdoc />
    /// <summary>
    /// 動作指定：高さ変更
    /// </summary>
    public class ChangeHeight : CharaMoveCommandBase
    {
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     OverrideMethod
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <inheritdoc />
        public override byte CommandCode => CharaMoveCommandCode.ChangeHeight;

        /// <inheritdoc />
        public override byte ValueLengthByte => 0x01;

        /// <summary>高さ</summary>
        public int Height
        {
            get => GetNumberValue(0);
            set => SetNumberValue(0, value);
        }

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
            if (VersionConfig.IsUnderVersion(WoditorVersion.Ver2_10))
            {
                OutputVersionWarningLogIfNeed_UnderVer2_10();
            }
        }

        /// <summary>
        /// 設定バージョン = 2.10未満 の場合の警告
        /// </summary>
        private void OutputVersionWarningLogIfNeed_UnderVer2_10()
        {
            Logger.Warning(VersionWarningMessage.NotUnderInCharaMoveCommand($"{nameof(ChangeHeight)}",
                VersionConfig.GetConfigWoditorVersion(),
                WoditorVersion.Ver2_10));
        }
    }
}