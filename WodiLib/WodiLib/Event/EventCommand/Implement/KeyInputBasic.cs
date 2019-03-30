// ========================================
// Project Name : WodiLib
// File Name    : KeyInputBasic.cs
//
// MIT License Copyright(c) 2019 kameske
// see LICENSE file
// ========================================

using System;
using System.ComponentModel;
using WodiLib.Sys;

namespace WodiLib.Event.EventCommand
{
    /// <inheritdoc />
    /// <summary>
    /// イベントコマンド・キー入力（基本）
    /// </summary>
    public class KeyInputBasic : KeyInputBase
    {
        /// <summary>キー入力種別フラグ値</summary>
        private readonly byte FlgKeyInputType = EventCommandConstant.KeyInput.Type.Basic;

        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     OverrideMethod
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <inheritdoc />
        public override byte NumberVariableCount => 0x03;

        /// <inheritdoc />
        public override byte StringVariableCount => 0x00;

        /// <inheritdoc />
        /// <summary>
        /// インデックスを指定して数値変数を取得する。
        /// </summary>
        /// <param name="index">[Range(0, 2)] インデックス</param>
        /// <returns>インデックスに対応した値</returns>
        /// <exception cref="ArgumentOutOfRangeException">indexが指定範囲以外</exception>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public override int GetNumberVariable(int index)
        {
            switch (index)
            {
                case 0:
                    return EventCommandCode.Code;

                case 1:
                    return LeftSide;

                case 2:
                {
                    var byte0 = (byte) (DirectionKeyType.Code + keyInputFlag.ToByte());
                    if (IsWaitForInput) byte0 += FlgWaitForInput;
                    return new byte[] {byte0, FlgKeyInputType, 0x00, 0x00}.ToInt32(Endian.Environment);
                }

                default:
                    throw new ArgumentOutOfRangeException(
                        ErrorMessage.OutOfRange(nameof(index), 0, 2, index));
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// 数値変数を設定する。
        /// </summary>
        /// <param name="index">[Range(1, 2)] インデックス</param>
        /// <param name="value">設定値</param>
        /// <exception cref="ArgumentOutOfRangeException">indexが指定範囲以外</exception>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public override void SetNumberVariable(int index, int value)
        {
            switch (index)
            {
                case 1:
                    LeftSide = value;
                    return;

                case 2:
                {
                    var bytes = value.ToBytes(Endian.Environment);
                    DirectionKeyType = DirectionKeyType.FromByte((byte) (bytes[0] & 0x0F));
                    keyInputFlag = new KeyInputFlag(bytes[0]);
                    IsWaitForInput = (bytes[0] & FlgWaitForInput) != 0;
                    return;
                }

                default:
                    throw new ArgumentOutOfRangeException(
                        ErrorMessage.OutOfRange(nameof(index), 1, 2, index));
            }
        }

        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Property
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        private DirectionKeyType directionKeyType = DirectionKeyType.FourDirections;

        /// <summary>[NotNull] 方向キー種別</summary>
        /// <exception cref="PropertyNullException">nullをセットした場合</exception>
        public DirectionKeyType DirectionKeyType
        {
            get => directionKeyType;
            set
            {
                if (value == null)
                    throw new PropertyNullException(
                        ErrorMessage.NotNull(nameof(DirectionKeyType)));
                directionKeyType = value;
            }
        }

        private KeyInputFlag keyInputFlag = new KeyInputFlag();

        /// <summary>決定キー受付</summary>
        public bool IsAcceptOk
        {
            get => keyInputFlag.IsAcceptOk;
            set => keyInputFlag.IsAcceptOk = value;
        }

        /// <summary>キャンセルキー受付</summary>
        public bool IsAcceptCancel
        {
            get => keyInputFlag.IsAcceptCancel;
            set => keyInputFlag.IsAcceptCancel = value;
        }

        /// <summary>サブキー受付</summary>
        public bool IsAcceptSub
        {
            get => keyInputFlag.IsAcceptSub;
            set => keyInputFlag.IsAcceptSub = value;
        }

        private class KeyInputFlag
        {
            /// <summary>決定キー受付</summary>
            public bool IsAcceptOk { get; set; }

            /// <summary>キャンセルキー受付</summary>
            public bool IsAcceptCancel { get; set; }

            /// <summary>サブキー受付</summary>
            public bool IsAcceptSub { get; set; }

            /// <summary>決定キー受付</summary>
            private byte FlgAcceptOk = 0x10;

            /// <summary>キャンセルキー受付</summary>
            private byte FlgAcceptCancel = 0x20;

            /// <summary>サブキー受付</summary>
            private byte FlgAcceptSub = 0x40;

            public KeyInputFlag(byte flags = 0)
            {
                IsAcceptOk = (flags & FlgAcceptOk) != 0;
                IsAcceptCancel = (flags & FlgAcceptCancel) != 0;
                IsAcceptSub = (flags & FlgAcceptSub) != 0;
            }

            public byte ToByte()
            {
                byte result = 0x00;
                if (IsAcceptOk) result += FlgAcceptOk;
                if (IsAcceptCancel) result += FlgAcceptCancel;
                if (IsAcceptSub) result += FlgAcceptSub;
                return result;
            }
        }
    }
}