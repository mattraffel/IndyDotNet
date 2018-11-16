using System;
using System.Threading.Tasks;
using IndyDotNet.Utils;
using static IndyDotNet.BlobStorage.NativeMethods;

namespace IndyDotNet.BlobStorage
{
    /// <summary>
    /// TEMPORARY PLACE HOLDER
    /// TODO:  replace
    /// </summary>
    internal class BlobStorageReader : IBlobStorageReader
    {
        public BlobStorageReader(int handle)
        {
            Handle = handle;
        }
        public int Handle { get; internal set; }
    }

    /// <summary>
    /// TEMPORARY PLACE HOLDER
    /// TODO:  replace
    /// </summary>
    internal class BlobStorageWriter : IBlobStorageWriter
    {
        public BlobStorageWriter(int handle)
        {
            Handle = handle;
        }

        public int Handle
        {
            get; internal set;
        }
    }

    /// <summary>
    /// BLOB storage.
    /// </summary>
    internal static class BlobStorageAsync
    {
#if __IOS__
    [MonoPInvokeCallback(typeof(BlobStorageCompletedDelegate))]
#endif
        private static void OpenReaderCallbackMethod(int xcommand_handle, int err, int handle)
        {
            var taskCompletionSource = PendingCommands.Remove<BlobStorageReader>(xcommand_handle);

            if (!CallbackHelper.CheckCallback(taskCompletionSource, err))
                return;

            taskCompletionSource.SetResult(new BlobStorageReader(handle));
        }
        private static BlobStorageCompletedDelegate OpenReaderCallback = OpenReaderCallbackMethod;

#if __IOS__
    [MonoPInvokeCallback(typeof(BlobStorageCompletedDelegate))]
#endif
        private static void OpenWriterCallbackMethod(int xcommand_handle, int err, int handle)
        {
            var taskCompletionSource = PendingCommands.Remove<BlobStorageWriter>(xcommand_handle);

            if (!CallbackHelper.CheckCallback(taskCompletionSource, err))
                return;

            taskCompletionSource.SetResult(new BlobStorageWriter(handle));
        }
        private static BlobStorageCompletedDelegate OpenWriterCallback = OpenWriterCallbackMethod;

        /// <summary>
        /// Opens the BLOB storage reader async.
        /// </summary>
        /// <returns>The BLOB storage reader async.</returns>
        /// <param name="type">Type.</param>
        /// <param name="configJson">Config json.</param>
        public static Task<BlobStorageReader> OpenReaderAsync(string type, string configJson)
        {
            ParamGuard.NotNullOrWhiteSpace(type, "type");
            ParamGuard.NotNullOrWhiteSpace(configJson, "configJson");

            var taskCompletionSource = new TaskCompletionSource<BlobStorageReader>();
            var commandHandle = PendingCommands.Add(taskCompletionSource);

            var result = NativeMethods.indy_open_blob_storage_reader(
                commandHandle,
                type,
                configJson,
                OpenReaderCallback
                );

            CallbackHelper.CheckResult(result);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Opens the BLOB storage writer async.
        /// </summary>
        /// <returns>The BLOB storage writer async.</returns>
        /// <param name="type">Type.</param>
        /// <param name="configJson">Config json.</param>
        public static Task<BlobStorageWriter> OpenWriterAsync(string type, string configJson)
        {
            ParamGuard.NotNullOrWhiteSpace(type, "type");
            ParamGuard.NotNullOrWhiteSpace(configJson, "configJson");

            var taskCompletionSource = new TaskCompletionSource<BlobStorageWriter>();
            var commandHandle = PendingCommands.Add(taskCompletionSource);

            var result = NativeMethods.indy_open_blob_storage_writer(
                commandHandle,
                type,
                configJson,
                OpenWriterCallback
                );

            CallbackHelper.CheckResult(result);

            return taskCompletionSource.Task;
        }
    }
}


