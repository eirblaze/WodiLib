// ========================================
// Project Name : WodiLib
// File Name    : WoditorFileBase.cs
//
// MIT License Copyright(c) 2019 kameske
// see LICENSE file
// ========================================

using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using WodiLib.Cmn;
using WodiLib.Sys;

namespace WodiLib.IO
{
    /// <summary>
    /// ウディタファイル基底クラス
    /// </summary>
    /// <typeparam name="TFilePath">ファイルパス</typeparam>
    /// <typeparam name="TFileData">ファイルデータ</typeparam>
    /// <typeparam name="TWriter">Writer</typeparam>
    /// <typeparam name="TReader">Reader</typeparam>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract class WoditorFileBase<TFilePath, TFileData, TWriter, TReader>
        where TFilePath : FilePath
        where TWriter : WoditorFileWriterBase<TFilePath, TFileData>
        where TReader : WoditorFileReaderBase<TFilePath, TFileData>
    {
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Public Property
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <summary>
        /// ファイルパス
        /// </summary>
        [NotNull]
        public TFilePath FilePath { get; }

        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Private Property
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        private SemaphoreSlim Sem { get; } = new SemaphoreSlim(1, 1);

        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Constructor
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="filePath">ファイル名</param>
        /// <exception cref="ArgumentNullException">filePathがnullの場合</exception>
        public WoditorFileBase([NotNull] TFilePath filePath)
        {
            if (filePath is null)
                throw new ArgumentNullException(
                    ErrorMessage.NotNull(nameof(filePath)));

            FilePath = filePath;
        }

        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Public Method
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <summary>
        /// ファイルを同期的に書き出す。
        /// </summary>
        /// <param name="data">書き出しデータ</param>
        /// <exception cref="ArgumentNullException">data がnullの場合</exception>
        public void WriteSync([NotNull] TFileData data)
        {
            if (data is null)
                throw new ArgumentNullException(
                    ErrorMessage.NotNull(nameof(data)));

            Sem.Wait();
            try
            {
                var writer = BuildFileWriter(FilePath);
                writer.WriteSync(data);
            }
            finally
            {
                Sem.Release();
            }
        }

        /// <summary>
        /// ファイルを非同期的に書き出す。
        /// </summary>
        /// <param name="data">書き出しデータ</param>
        /// <returns>非同期処理タスク</returns>
        /// <exception cref="ArgumentNullException">data がnullの場合</exception>
        public async Task WriteAsync([NotNull] TFileData data)
        {
            if (data is null)
                throw new ArgumentNullException(
                    ErrorMessage.NotNull(nameof(data)));

            await Sem.WaitAsync().ConfigureAwait(false);
            try
            {
                var writer = BuildFileWriter(FilePath);
                await writer.WriteAsync(data);
            }
            finally
            {
                Sem.Release();
            }
        }

        /// <summary>
        /// ファイルを同期的に読み込む。
        /// </summary>
        /// <returns>読み込みデータ</returns>
        public TFileData ReadSync()
        {
            Sem.Wait();
            try
            {
                var reader = BuildFileReader(FilePath);
                var result = reader.ReadSync();

                return result;
            }
            finally
            {
                Sem.Release();
            }
        }

        /// <summary>
        /// ファイルを非同期的に読み込む。
        /// </summary>
        /// <returns>読み込みデータを返すタスク</returns>
        public async Task<TFileData> ReadAsync()
        {
            await Sem.WaitAsync().ConfigureAwait(false);
            try
            {
                var reader = BuildFileReader(FilePath);
                var result = await reader.ReadAsync();

                return result;
            }
            finally
            {
                Sem.Release();
            }
        }

        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Protected Abstract Method
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <summary>
        /// ファイル書き出しクラスを生成する。
        /// </summary>
        /// <param name="filePath">書き出しファイル名</param>
        /// <returns>ライターインスタンス</returns>
        protected abstract TWriter MakeFileWriter(TFilePath filePath);

        /// <summary>
        /// ファイル読み込みクラスを生成する。
        /// </summary>
        /// <param name="filePath">[NotEmpty] 読み込みファイル名</param>
        /// <returns>リーダーインスタンス</returns>
        /// <exception cref="ArgumentNullException">fileNameがnullの場合</exception>
        protected abstract TReader MakeFileReader(TFilePath filePath);

        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Private Method
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <summary>
        /// ファイル書き出しクラスを生成する。
        /// </summary>
        /// <param name="filePath">書き出しファイル名</param>
        /// <returns>ライターインスタンス</returns>
        /// <exception cref="ArgumentNullException">filePath がnullの場合</exception>
        private TWriter BuildFileWriter(TFilePath filePath)
        {
            if (filePath is null)
                throw new ArgumentNullException(
                    ErrorMessage.NotNull(nameof(filePath)));

            var writer = MakeFileWriter(filePath);
            return writer;
        }

        /// <summary>
        /// ファイル読み込みクラスを生成する。
        /// </summary>
        /// <param name="filePath">読み込みファイル名</param>
        /// <returns>リーダーインスタンス</returns>
        /// <exception cref="ArgumentNullException">fileNameがnullの場合</exception>
        /// <exception cref="ArgumentException">fileNameが空文字の場合</exception>
        private TReader BuildFileReader(TFilePath filePath)
        {
            if (filePath is null)
                throw new ArgumentNullException(
                    ErrorMessage.NotNull(nameof(filePath)));

            var reader = MakeFileReader(filePath);
            return reader;
        }
    }
}
