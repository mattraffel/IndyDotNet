using System;
using static IndyDotNet.PaymentHandler.NativeMethods;

namespace IndyDotNet.PaymentHandler
{
    public static class Factory
    {
        private static SDKPaymentFacade _facade;
        public static bool RegisterPaymentHandler(IPaymentHandler handler)
        {
            _facade = new SDKPaymentFacade(handler);

            PaymentHandlerAsync.RegisterPaymentMethodAsync(_facade).Wait();

            return true;
        }

    }

    /// <summary>
    /// TODO:  the pattern is proven, need to finish implementation
    /// </summary>
    internal class SDKPaymentFacade : ISDKPaymentFacade
    {
        private IPaymentHandler _handler;

        public SDKPaymentFacade(IPaymentHandler handler)
        {
            _handler = handler;
            CreatePaymentAddressCallback = CreatePaymentAddressHandler;
            AddRequestFeesCallback = AddRequestFeesHandler;
            ParseResponseWithFeesCallback = ParseResponseWithFeesHandler;
            BuildGetPaymentSourcesRequstCallback = BuildGetPaymentSourcesRequstHandler;
            ParseGetPaymentSourcesResponseCallback = ParseGetPaymentSourcesResponseHandler;
            BuildPaymentRequestCallback = BuildPaymentRequestHandler;
            ParsePaymentResponseCallback = ParsePaymentResponseHandler;
            BuildMintReqCallback = BuildMintReqHandler;
            BuildSetTxnFeesReqCallback = BuildSetTxnFeesReqHandler;
            BuildGetTxnFeesReqCallback = BuildGetTxnFeesReqHandler;
            ParseGetTxnFeesResponseCallback = ParseGetTxnFeesResponseHandler;
            BuildVerifyPaymentRequestCallback = BuildVerifyPaymentRequestHandler;
            ParseVerifyPaymentResponseCallback = ParseVerifyPaymentResponseHandler;
        }

        public string PaymentMethod { get { return _handler.PaymentMethod; } }

        public CreatePaymentAddressCallbackDelegate CreatePaymentAddressCallback { get; internal set; }

        public AddRequestFeesCallbackDelegate AddRequestFeesCallback { get; internal set; }

        public ParseResponseWithFeesCallbackDelegate ParseResponseWithFeesCallback { get; internal set; }

        public BuildGetPaymentSourcesRequestCallbackDelegate BuildGetPaymentSourcesRequstCallback { get; internal set; }

        public ParseGetPaymentSourcesResponseCallbackDelegate ParseGetPaymentSourcesResponseCallback { get; internal set; }

        public BuildPaymentRequestCallbackDelegate BuildPaymentRequestCallback { get; internal set; }

        public ParsePaymentResponseCallbackDelegate ParsePaymentResponseCallback { get; internal set; }

        public BuildMintReqCallbackDelegate BuildMintReqCallback { get; internal set; }

        public BuildSetTxnFeesReqCallbackDelegate BuildSetTxnFeesReqCallback { get; internal set; }

        public BuildGetTxnFeesReqCallbackDelegate BuildGetTxnFeesReqCallback { get; internal set; }

        public ParseGetTxnFeesResponseCallbackDelegate ParseGetTxnFeesResponseCallback { get; internal set; }

        public BuildVerifyPaymentRequestCallbackDelegate BuildVerifyPaymentRequestCallback { get; internal set; }

        public ParseVerifyPaymentResponseCallbackDelegate ParseVerifyPaymentResponseCallback { get; internal set; }

        private ErrorCode CreatePaymentAddressHandler(int command_handle, IntPtr wallet_handle, string config, PaymentMethodResultDelegate cb)
        {
            string paymentAddress = _handler.CreatePaymentAddress();
            cb(command_handle, (int) ErrorCode.Success, paymentAddress);
            return ErrorCode.Success;
        }

        private ErrorCode AddRequestFeesHandler(int command_handle, IntPtr wallet_handle, string submitter_did, string req_json, string inputs_json, string outputs_json, string extra, PaymentMethodResultDelegate cb)
        {

            return ErrorCode.Success;
        }

        private ErrorCode ParseResponseWithFeesHandler(int command_handle, string resp_json, PaymentMethodResultDelegate cb)
        {
            return ErrorCode.Success;
        }

        private ErrorCode BuildGetPaymentSourcesRequstHandler(int command_handle, IntPtr wallet_handle, string submitter_did, string payment_address, PaymentMethodResultDelegate cb)
        {
            return ErrorCode.Success;
        }

        private ErrorCode ParseGetPaymentSourcesResponseHandler(int command_handle, string resp_json, PaymentMethodResultDelegate cb)
        {
            return ErrorCode.Success;
        }

        private ErrorCode BuildPaymentRequestHandler(int command_handle, IntPtr wallet_handle, string submitter_did, string inputs_json, string outputs_json, string extra, PaymentMethodResultDelegate cb)
        {
            return ErrorCode.Success;
        }

        private ErrorCode ParsePaymentResponseHandler(int command_handle, string resp_json, PaymentMethodResultDelegate cb)
        {
            return ErrorCode.Success;
        }

        private ErrorCode BuildMintReqHandler(int command_handle, IntPtr wallet_handle, string submitter_did, string outputs_json, string extra, PaymentMethodResultDelegate cb)
        {
            return ErrorCode.Success;
        }

        private ErrorCode BuildSetTxnFeesReqHandler(int command_handle, IntPtr wallet_handle, string submitter_did, string fees_json, PaymentMethodResultDelegate cb)
        {
            return ErrorCode.Success;
        }

        private ErrorCode BuildGetTxnFeesReqHandler(int command_handle, IntPtr wallet_handle, string submitter_did, PaymentMethodResultDelegate cb)
        {
            return ErrorCode.Success;
        }

        private ErrorCode ParseGetTxnFeesResponseHandler(int command_handle, string resp_json, PaymentMethodResultDelegate cb)
        {
            return ErrorCode.Success;
        }

        private ErrorCode BuildVerifyPaymentRequestHandler(int command_handle, IntPtr wallet_handle, string submitterDid, string receipt, PaymentMethodResultDelegate cb)
        {
            return ErrorCode.Success;
        }

        private ErrorCode ParseVerifyPaymentResponseHandler(int command_handle, string responseJson, PaymentMethodResultDelegate cb)
        {
            // var result = cb(command_handle, 0, feesJson.Result);
            return ErrorCode.Success;
        }
    }
}
