// ========================================
// Project Name : WodiLib
// File Name    : TileSetFileWriter.cs
//
// MIT License Copyright(c) 2019 kameske
// see LICENSE file
// ========================================

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WodiLib.Map;
using WodiLib.Sys;
using WodiLib.Sys.Cmn;

namespace WodiLib.IO
{
    /// <summary>
    /// タイルセットファイル書き出しクラス
    /// </summary>
    internal class TileSetFileWriter
    {
        /// <summary>書き出しファイルパス</summary>
        public TileSetFilePath FilePath { get; }

        /// <summary>書き出すデータ</summary>
        public TileSetFileData Data { get; }

        /// <summary>ロガー</summary>
        private static WodiLibLogger Logger { get; } = WodiLibLogger.GetInstance();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="outputData">[NotNull] 書き出しデータ</param>
        /// <param name="filePath">[NotNullOrEmpty] 書き出しファイルパス</param>
        /// <exception cref="ArgumentNullException">outputData, filePathがnullの場合</exception>
        public TileSetFileWriter(TileSetFileData outputData, TileSetFilePath filePath)
        {
            if (outputData == null)
                throw new ArgumentNullException(
                    ErrorMessage.NotNull(nameof(outputData)));
            if (filePath == null)
                throw new ArgumentNullException(
                    ErrorMessage.NotNull(nameof(filePath)));

            Data = outputData;
            FilePath = filePath;
        }

        /// <summary>
        /// データを同期的に書き出す。
        /// </summary>
        /// <exception cref="ArgumentException">
        ///     ファイル名が正しくない場合、
        ///     またはpathが非ファイルデバイスを参照している場合
        /// </exception>
        public void WriteSync()
        {
            Logger.Info(FileIOMessage.StartFileWrite(GetType()));

            var bin = Data.ToBinary().ToArray();
            using (var stream = new FileStream(FilePath, FileMode.Create))
            {
                stream.Write(bin, 0, bin.Length);
            }

            Logger.Info(FileIOMessage.EndFileWrite(GetType()));
        }


        /// <summary>
        /// データを非同期的に書き出す。
        /// </summary>
        /// <returns>書き出しTask</returns>
        /// <exception cref="ArgumentException">
        ///     ファイル名が正しくない場合、
        ///     またはpathが非ファイルデバイスを参照している場合
        /// </exception>
        public async Task WriteAsync()
        {
            await Task.Run(WriteSync);
        }
    }
}