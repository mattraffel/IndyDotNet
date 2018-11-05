using System;
namespace IndyDotNet.Did
{
    #region metadate and metadata factory interfaces
    /// <summary>
    /// Metadata is consumer defined json.  The consumer should know the structure
    /// By making this a generic Interface, consumers can use structured types
    /// over having to parse json directly
    /// </summary>
    public interface IMetaData<T>
    {
        T Data { get; set; }
    }

    public interface IMetaDataFactory<T> 
    {
        IMetaData<T> CreateInstance();
    }
    #endregion

    #region default implementation
    public class DefaultStringMetaData : IMetaData<string>
    {
        public string Data { get; set; }

        public override string ToString()
        {
            return Data;
        }
    }

    public class DefaultStringMetaDataFactory : IMetaDataFactory<string>
    {
        public IMetaData<string> CreateInstance()
        {
            return new DefaultStringMetaData();
        }
    }
    #endregion
}
