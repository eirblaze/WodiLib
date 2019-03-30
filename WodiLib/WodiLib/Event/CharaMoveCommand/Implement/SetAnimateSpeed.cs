// ========================================
// Project Name : WodiLib
// File Name    : SetAnimateSpeed.cs
//
// MIT License Copyright(c) 2019 kameske
// see LICENSE file
// ========================================

using WodiLib.Sys;

namespace WodiLib.Event.CharaMoveCommand
{
    /// <inheritdoc />
    /// <summary>
    /// 動作指定：アニメ頻度を設定
    /// </summary>
    public class SetAnimateSpeed : CharaMoveCommandBase
    {
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     OverrideMethod
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <inheritdoc />
        public override CharaMoveCommandCode CommandCode => CharaMoveCommandCode.SetAnimateSpeed;

        /// <inheritdoc />
        public override byte ValueLengthByte => 0x01;

        private AnimateSpeed animateSpeed = AnimateSpeed.Frame;

        /// <summary>[NotNull] 頻度</summary>
        /// <exception cref="PropertyNullException">nullをセットした場合</exception>
        public AnimateSpeed Value
        {
            get => animateSpeed;
            set
            {
                if(value == null) throw new PropertyNullException(
                    ErrorMessage.NotNull(nameof(Value)));

                animateSpeed = value;
                var val = (int)animateSpeed.Code;
                SetNumberValue(0, (CharaMoveCommandValue)val);
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SetAnimateSpeed()
        {
            // 引数0の初期値設定
            var val = (int)animateSpeed.Code;
            SetNumberValue(0, (CharaMoveCommandValue)val);
        }
    }
}