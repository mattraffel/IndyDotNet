using System;
using IndyDotNet.BlobStorage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.BlobStorageTests
{
    [TestClass]
    public class AllBlobStorageTests
    {
        private string _temporaryPath = String.Empty;

        [TestInitialize]
        public void InitializeBlobStorageTests()
        {
            _temporaryPath = System.IO.Path.GetTempPath();
        }

        [TestMethod]
        public void SuccessfullyOpenBlobWriter()
        {
            BlobStorageConfig config = new BlobStorageConfig()
            {
                BaseDir = _temporaryPath,
                UriPattern = string.Empty
            };

            IBlobStorageWriter writer = IndyDotNet.BlobStorage.Factory.OpenWriter(BlobStorageTypes.Default, config);

            Assert.IsNotNull(writer);
            Assert.IsTrue(0 < writer.Handle);
        }

        [TestMethod]
        public void SuccessfullyOpenBlobReader()
        {
            BlobStorageConfig config = new BlobStorageConfig()
            {
                BaseDir = _temporaryPath,
                UriPattern = string.Empty
            };

            IBlobStorageReader reader = IndyDotNet.BlobStorage.Factory.OpenReader(BlobStorageTypes.Default, config);

            Assert.IsNotNull(reader);
            Assert.IsTrue(0 < reader.Handle);
        }

        [TestMethod]
        public void SuccessfullyOpenBlobWriterInThread()
        {
            System.Threading.Tasks.Parallel.Invoke(() =>
            {
                try
                {
                    BlobStorageConfig config = new BlobStorageConfig()
                    {
                        BaseDir = _temporaryPath,
                        UriPattern = string.Empty
                    };

                    IBlobStorageWriter writer = IndyDotNet.BlobStorage.Factory.OpenWriter(BlobStorageTypes.Default, config);

                    Assert.IsNotNull(writer);
                    Assert.IsTrue(0 < writer.Handle);
                } catch (Exception ex)
                {
                    Assert.Fail(ex.Message);
                }
            });
        }
    }
}
