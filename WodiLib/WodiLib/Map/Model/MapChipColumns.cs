using System;
using System.Collections.Generic;
using WodiLib.Sys;

namespace WodiLib.Map
{
    /// <summary>
    /// マップチップ列
    /// </summary>
    public class MapChipColumns : RestrictedCapacityCollection<MapChip>, IFixedLengthMapChipColumns
    {
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Public Constant
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <summary>リスト最大数</summary>
        public static readonly int MaxCapacity = MapSizeHeight.MaxValue;

        /// <summary>リスト最小数</summary>
        public static readonly int MinCapacity = MapSizeHeight.MinValue;

        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Constructor
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MapChipColumns() : this(MinCapacity)
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="height">初期サイズ高さ</param>
        public MapChipColumns(MapSizeHeight height)
        {
            InitializeChips(height);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="chips">[NotNull][LengthRange(MinCapacity, MaxCapacity)] マップチップリスト</param>
        /// <exception cref="ArgumentNullException">
        ///     chips が null の場合、
        ///     または chips に null 要素が含まれる場合
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">chips の要素数が指定範囲外の場合</exception>
        public MapChipColumns(IReadOnlyList<MapChip> chips)
        {
            if (chips == null)
                throw new ArgumentNullException(
                    ErrorMessage.NotNull(nameof(chips)));
            if (chips.HasNullItem())
                throw new ArgumentNullException(
                    ErrorMessage.NotNullInList(nameof(chips)));

            var length = chips.Count;
            if (length < MinCapacity || MaxCapacity < length)
                throw new ArgumentOutOfRangeException(
                    ErrorMessage.OutOfRange(nameof(chips), MinCapacity, MaxCapacity, length));

            AdjustLength(length);
            for (var i = 0; i < length; i++)
            {
                this[i] = chips[i];
            }
        }

        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Override Public Method
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <inheritdoc />
        public override int GetMaxCapacity() => MaxCapacity;

        /// <inheritdoc />
        public override int GetMinCapacity() => MinCapacity;

        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Public Method
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <summary>
        /// マップチップ情報を初期化する。
        /// </summary>
        public void InitializeChips() => InitializeChips(Count);

        /// <summary>
        /// マップチップ情報を初期化する。
        /// </summary>
        /// <param name="height">マップサイズ縦</param>
        public void InitializeChips(MapSizeHeight height)
        {
            AdjustLength(height);
            for (var i = 0; i < height; i++)
            {
                this[i] = MakeDefaultItem(i);
            }
        }

        /// <summary>
        /// サイズを更新する。
        /// </summary>
        /// <param name="height">更新後のサイズ</param>
        public void UpdateSize(MapSizeHeight height) => AdjustLength(height);

        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Override Protected Method
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <inheritdoc />
        protected override MapChip MakeDefaultItem(int index) => MapChip.Default;

        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Common
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <summary>
        /// バイナリ変換する。
        /// </summary>
        /// <returns>バイナリデータ</returns>
        public byte[] ToBinary()
        {
            var result = new List<byte>();

            foreach (var chip in this)
                result.AddRange(((int) chip).ToBytes(Endian.Woditor));

            return result.ToArray();
        }
    }
}