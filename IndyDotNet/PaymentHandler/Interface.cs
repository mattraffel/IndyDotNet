using System;
using static IndyDotNet.PaymentHandler.NativeMethods;

namespace IndyDotNet.PaymentHandler
{
    /// <summary>
    /// Payment handlers are methods that are called by LibIndy when payment functions are invoked
    /// 
    /// Payment libraries, like DotNetPay, implement this interface
    /// See DotNetPay project for an example of this implementation
    /// 
    /// TODO:  this interface is not finished.  Serving a POC the pattern works
    /// </summary>
    public interface IPaymentHandler
    {
        string PaymentMethod { get; }
        string CreatePaymentAddress();
    }

    /// <summary>
    /// This is a facade between .NET and Libindy so that we can isolate the implementation of IPaymentHandler
    /// from the specifics of the API.  Implementers will not have to load wallets and dids as these can be done
    /// by the facade.
    /// </summary>
    internal interface ISDKPaymentFacade
    {
        string PaymentMethod { get; }

        CreatePaymentAddressCallbackDelegate CreatePaymentAddressCallback { get; }

        AddRequestFeesCallbackDelegate AddRequestFeesCallback { get; }

        ParseResponseWithFeesCallbackDelegate ParseResponseWithFeesCallback { get; }

        BuildGetPaymentSourcesRequestCallbackDelegate BuildGetPaymentSourcesRequstCallback { get; }

        ParseGetPaymentSourcesResponseCallbackDelegate ParseGetPaymentSourcesResponseCallback { get; }

        BuildPaymentRequestCallbackDelegate BuildPaymentRequestCallback { get; }

        ParsePaymentResponseCallbackDelegate ParsePaymentResponseCallback { get; }

        BuildMintReqCallbackDelegate BuildMintReqCallback { get; }

        BuildSetTxnFeesReqCallbackDelegate BuildSetTxnFeesReqCallback { get; }

        BuildGetTxnFeesReqCallbackDelegate BuildGetTxnFeesReqCallback { get; }

        ParseGetTxnFeesResponseCallbackDelegate ParseGetTxnFeesResponseCallback { get; }

        BuildVerifyPaymentRequestCallbackDelegate BuildVerifyPaymentRequestCallback { get; }

        ParseVerifyPaymentResponseCallbackDelegate ParseVerifyPaymentResponseCallback { get; }
    }
}
