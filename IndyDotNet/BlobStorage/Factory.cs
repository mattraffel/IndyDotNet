using System;
using IndyDotNet.Utils;

namespace IndyDotNet.BlobStorage
{
    public static class Factory
    {
        public static IBlobStorageReader OpenReader(BlobStorageTypes storageType, BlobStorageConfig config)
        {
            string configJson = config.ToJson();

            BlobStorageReader reader = BlobStorageAsync.OpenReaderAsync(storageType.AsString(), configJson).Result;
            return reader;
        }

        public static IBlobStorageWriter OpenWriter(BlobStorageTypes storageType, BlobStorageConfig config)
        {
            string configJson = config.ToJson();
            BlobStorageWriter writer = BlobStorageAsync.OpenWriterAsync(storageType.AsString(), configJson).Result;

            return writer;
        }
    }
}
