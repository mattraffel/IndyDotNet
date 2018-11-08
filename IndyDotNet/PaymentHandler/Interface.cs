using System;
using static IndyDotNet.PaymentHandler.NativeMethods;

namespace IndyDotNet.PaymentHandler
{
    /// <summary>
    /// Payment handlers are methods that are called by LibIndy when payment functions are invoked
    /// in LibIndy.   
    /// 
    /// See DotNetPay project for an example of this implementation
    /// TODO:  abstract the raw API (current delegates) from the consumer implementation
    /// so that consumer will get IWallet, IDid etc...
    /// </summary>
    public interface IPaymentHandlers
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
