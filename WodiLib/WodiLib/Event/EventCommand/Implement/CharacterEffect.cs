// ========================================
// Project Name : WodiLib
// File Name    : CharacterEffect.cs
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
    /// イベントコマンド・キャラエフェクト
    /// </summary>
    public class CharacterEffect : EventCommandBase
    {
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Private Const
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <summary>処理種別</summary>
        private readonly byte TargetCode = EventCommandConstant.Effect.TargetCode.Chara;

        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     OverrideMethod
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <inheritdoc />
        public override EventCommandCode EventCommandCode => EventCommandCode.Effect;

        /// <inheritdoc />
        public override byte NumberVariableCount => 0x08;

        /// <inheritdoc />
        public override byte StringVariableCount => 0x00;

        /// <inheritdoc />
        /// <summary>
        /// インデックスを指定して数値変数を取得する。
        /// </summary>
        /// <param name="index">[Range(0, 7)] インデックス</param>
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
                    byte[] bytes =
                    {
                        (byte) (EffectType.Code + TargetCode),
                        0x00,
                        0x00,
                        0x00
                    };
                    return bytes.ToInt32(Endian.Environment);

                case 2:
                    return ProcessTime;

                case 3:
                    return TargetCharacterId;

                case 4:
                    // 未使用
                    return 0;

                case 5:
                    return NumberArg1;

                case 6:
                    return NumberArg2;

                case 7:
                    return NumberArg3;

                default:
                    throw new ArgumentOutOfRangeException(
                        ErrorMessage.OutOfRange(nameof(index), 0, 7, index));
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// 数値変数を設定する。
        /// </summary>
        /// <param name="index">[Range(1, 7)] インデックス</param>
        /// <param name="value">設定値</param>
        /// <exception cref="ArgumentOutOfRangeException">indexが指定範囲以外</exception>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public override void SetNumberVariable(int index, int value)
        {
            switch (index)
            {
                case 1:
                    byte[] bytes = value.ToBytes(Endian.Environment);
                    EffectType = CharaEffectType.FromByte((byte) (bytes[0] & 0xF0));
                    return;

                case 2:
                    ProcessTime = value;
                    return;

                case 3:
                    TargetCharacterId = value;
                    return;

                case 4:
                    return;

                case 5:
                    NumberArg1 = value;
                    return;

                case 6:
                    NumberArg2 = value;
                    return;

                case 7:
                    NumberArg3 = value;
                    return;

                default:
                    throw new ArgumentOutOfRangeException(
                        ErrorMessage.OutOfRange(nameof(index), 1, 7, index));
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// インデックスを指定して文字列変数を取得する。
        /// </summary>
        /// <param name="index">[Range(0, -)] インデックス</param>
        /// <returns>インデックスに対応した値</returns>
        /// <exception cref="ArgumentOutOfRangeException">常に</exception>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string GetStringVariable(int index)
        {
            throw new ArgumentOutOfRangeException();
        }

        /// <inheritdoc />
        /// <summary>
        /// 文字列変数を設定する。
        /// </summary>
        /// <param name="index">[Range(0, -)] インデックス</param>
        /// <param name="value">[NotNull] 設定値</param>
        /// <exception cref="ArgumentOutOfRangeException">常に</exception>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override void SetStringVariable(int index, string value)
        {
            throw new ArgumentOutOfRangeException();
        }

        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Property
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        private CharaEffectType effectType = CharaEffectType.Flush;

        /// <summary>対象キャラID</summary>
        public int TargetCharacterId { get; set; }

        /// <summary>連続ピクチャ操作フラグ</summary>
        public bool IsMultiTarget { get; set; }

        /// <summary>連続ピクチャ数</summary>
        public int SequenceValue { get; set; }

        /// <summary>[NotNull] エフェクト種別</summary>
        /// <exception cref="PropertyNullException">nullをセットした場合</exception>
        public CharaEffectType EffectType
        {
            get => effectType;
            set
            {
                if (value == null)
                    throw new PropertyNullException(
                        ErrorMessage.NotNull(nameof(EffectType)));
                effectType = value;
            }
        }

        /// <summary>数値引数1</summary>
        public int NumberArg1 { get; set; }

        /// <summary>数値引数2</summary>
        public int NumberArg2 { get; set; }

        /// <summary>数値引数3</summary>
        public int NumberArg3 { get; set; }

        /// <summary>処理時間/間隔</summary>
        public int ProcessTime { get; set; }
    }
}