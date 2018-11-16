using System;
namespace IndyDotNet.BlobStorage
{
    public interface IBlobStorageReader
    {
        int Handle { get; }
    }

    public interface IBlobStorageWriter
    {
        int Handle { get; }
    }
}
