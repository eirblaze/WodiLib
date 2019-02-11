// ========================================
// Project Name : WodiLib
// File Name    : CsvIO.cs
//
// MIT License Copyright(c) 2019 kameske
// see LICENSE file
// ========================================

using System.ComponentModel;
using WodiLib.Sys;

namespace WodiLib.Event.EventCommand
{
    /// <inheritdoc />
    /// <summary>
    /// イベントコマンド・CSV入出力
    /// </summary>
    public class CsvIO : DBManagementBase
    {
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     OverrideMethod
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <inheritdoc />
        public override int EventCommandCode => EventCommand.EventCommandCode.CsvIO;

        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Property
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <summary>[NotNull] DB種別</summary>
        /// <exception cref="PropertyNullException">nullをセットした場合</exception>
        public DBKind DBKind
        {
            get => _DBKind;
            set
            {
                if (value == null)
                    throw new PropertyNullException(
                        ErrorMessage.NotNull(nameof(DBKind)));
                _DBKind = value;
            }
        }

        /// <summary>[NotNull] タイプID</summary>
        /// <exception cref="PropertyNullException">nullまたはStrOrInt.Noneをセットした場合</exception>
        public IntOrStr DBTypeId
        {
            get => IsTypeIdUseStr ? (IntOrStr) _DBTypeId.ToStr() : _DBTypeId.ToInt();
            set
            {
                if (value == null)
                    throw new PropertyNullException(
                        ErrorMessage.NotNull(nameof(DBTypeId)));
                if (value.InstanceIntOrStrType == IntOrStrType.None)
                    throw new PropertyNullException(
                        ErrorMessage.NotNull(nameof(DBTypeId)));
                _DBTypeId.Merge(value);
            }
        }

        /// <summary>[NotNull] データID</summary>
        /// <exception cref="PropertyNullException">nullまたはStrOrInt.Noneをセットした場合</exception>
        public IntOrStr DBDataId
        {
            get => IsDataIdUseStr ? (IntOrStr) _DBDataId.ToStr() : _DBDataId.ToInt();
            set
            {
                if (value == null)
                    throw new PropertyNullException(
                        ErrorMessage.NotNull(nameof(DBDataId)));
                if (value.InstanceIntOrStrType == IntOrStrType.None)
                    throw new PropertyNullException(
                        ErrorMessage.NotNull(nameof(DBDataId)));
                _DBDataId.Merge(value);
            }
        }

        /// <summary>
        /// <para>項目ID</para>
        /// <para>通常は設定しないが、ウディタ上で指定した値があれば設定する。</para>
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int DBItemIndex
        {
            get => _DBItemId.ToInt();
            set => _DBItemId = value;
        }

        /// <inheritdoc />
        /// <summary>項目ID</summary>
        protected override IntOrStr _DBItemId { get; set; } = 0;

        private CsvIOMode mode = CsvIOMode.Input;

        /// <summary>[NotNull] CSV入出力モード</summary>
        /// <exception cref="PropertyNullException">nullをセットした場合</exception>
        public CsvIOMode Mode
        {
            get => mode;
            set
            {
                if (value == null)
                    throw new PropertyNullException(
                        ErrorMessage.NotNull(nameof(Mode)));
                mode = value;
            }
        }

        /// <summary>出力/入力データ数</summary>
        public int ItemLength { get; set; }

        private string fileName = "";

        /// <summary>[NotNull] 出力/入力ファイル名</summary>
        public string FileName
        {
            get => fileName;
            set
            {
                if (value == null)
                    throw new PropertyNullException(
                        ErrorMessage.NotNull(nameof(FileName)));
                fileName = value;
            }
        }

        /// <summary>タイプID文字列指定フラグ</summary>
        public bool IsTypeIdUseStr
        {
            get => _IsTypeIdUseStr;
            set => _IsTypeIdUseStr = value;
        }

        /// <summary>データID文字列指定フラグ</summary>
        public bool IsDataIdUseStr
        {
            get => _IsDataIdUseStr;
            set => _IsDataIdUseStr = value;
        }

        /// <inheritdoc />
        /// <summary>項目ID文字列指定フラグ</summary>
        protected override bool _IsItemIdUseStr
        {
            get => false;
            set { }
        }

        /// <inheritdoc />
        /// <summary>入出力値または代入先</summary>
        protected override int NumValue
        {
            get => ItemLength;
            set => ItemLength = value;
        }

        /// <inheritdoc />
        /// <summary>代入文字列またはCSVファイル名</summary>
        protected override string StrValue
        {
            get => FileName;
            set => FileName = value;
        }

        /// <inheritdoc />
        /// <summary>右辺内容コード</summary>
        protected override byte RightSideCode
        {
            get => 0;
            set { }
        }

        /// <inheritdoc />
        /// <summary>読み書きモード</summary>
        protected override byte ioMode
        {
            get => Mode.Code;
            set => Mode = CsvIOMode.FromByte(value);
        }

        /// <inheritdoc />
        /// <summary>代入演算子コード</summary>
        protected override byte NumberAssignOperationCode
        {
            get => 0;
            set { }
        }
    }
}