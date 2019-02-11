// ========================================
// Project Name : WodiLib
// File Name    : AssignValue.cs
//
// MIT License Copyright(c) 2019 kameske
// see LICENSE file
// ========================================

namespace WodiLib.Event.CharaMoveCommand
{
    /// <inheritdoc />
    /// <summary>
    /// 動作指定：代入
    /// </summary>
    public class AssignValue : CharaMoveCommandBase
    {
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     OverrideMethod
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <inheritdoc />
        public override byte CommandCode => CharaMoveCommandCode.SubstituteValue;

        /// <inheritdoc />
        public override byte ValueLengthByte => 0x02;

        /// <summary>対象アドレス値</summary>
        public int TargetAddress
        {
            get => GetNumberValue(0);
            set => SetNumberValue(0, value);
        }

        /// <summary>代入値</summary>
        public int Value
        {
            get => GetNumberValue(1);
            set => SetNumberValue(1, value);
        }
    }
}