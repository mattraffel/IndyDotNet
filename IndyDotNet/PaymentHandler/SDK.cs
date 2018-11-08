using System;
using System.Runtime.InteropServices;
using static IndyDotNet.Utils.CallbackHelper;

namespace IndyDotNet.PaymentHandler
{
    /// <summary>
    /// These delegates are fired from a call within LibIndy
    /// 
    /// Payment Handler "APIS" are meant to handled by payment handlers.  Example
    /// is DotNetPay, a payment handler library
    /// 
    /// TODO:  abstract the raw API (current delegates) from the consumer implementation
    /// so that consumer will get IWallet, IDid etc...
    /// </summary>
    public static class NativeMethods
    {
        public delegate int PaymentMethodResultDelegate(int command_handle, int err, string arg);

        public delegate ErrorCode CreatePaymentAddressCallbackDelegate(int command_handle, IntPtr wallet_handle, string config, PaymentMethodResultDelegate cb);

        public delegate ErrorCode AddRequestFeesCallbackDelegate(int command_handle, IntPtr wallet_handle, string submitter_did, string req_json, string inputs_json, string outputs_json, string extra, PaymentMethodResultDelegate cb);

        public delegate ErrorCode ParseResponseWithFeesCallbackDelegate(int command_handle, string resp_json, PaymentMethodResultDelegate cb);

        public delegate ErrorCode BuildGetPaymentSourcesRequestCallbackDelegate(int command_handle, IntPtr wallet_handle, string submitter_did, string payment_address, PaymentMethodResultDelegate cb);

        public delegate ErrorCode ParseGetPaymentSourcesResponseCallbackDelegate(int command_handle, string resp_json, PaymentMethodResultDelegate cb);

        public delegate ErrorCode BuildPaymentRequestCallbackDelegate(int command_handle, IntPtr wallet_handle, string submitter_did, string inputs_json, string outputs_json, string extra, PaymentMethodResultDelegate cb);

        public delegate ErrorCode ParsePaymentResponseCallbackDelegate(int command_handle, string resp_json, PaymentMethodResultDelegate cb);

        public delegate ErrorCode BuildMintReqCallbackDelegate(int command_handle, IntPtr wallet_handle, string submitter_did, string outputs_json, string extra, PaymentMethodResultDelegate cb);

        public delegate ErrorCode BuildSetTxnFeesReqCallbackDelegate(int command_handle, IntPtr wallet_handle, string submitter_did, string fees_json, PaymentMethodResultDelegate cb);

        public delegate ErrorCode BuildGetTxnFeesReqCallbackDelegate(int command_handle, IntPtr wallet_handle, string submitter_did, PaymentMethodResultDelegate cb);

        public delegate ErrorCode ParseGetTxnFeesResponseCallbackDelegate(int command_handle, string resp_json, PaymentMethodResultDelegate cb);

        public delegate ErrorCode BuildVerifyPaymentRequestCallbackDelegate(int command_handle, IntPtr wallet_handle, string submitter_did, string receipt, PaymentMethodResultDelegate cb);

        public delegate ErrorCode ParseVerifyPaymentResponseCallbackDelegate(int command_handle, string resp_json, PaymentMethodResultDelegate cb);

        [DllImport(Consts.NATIVE_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false)]
        internal static extern int indy_register_payment_method(int command_handle, string payment_method,
                                                                CreatePaymentAddressCallbackDelegate create_payment_address,
                                                                AddRequestFeesCallbackDelegate add_request_fees,
                                                                ParseResponseWithFeesCallbackDelegate parse_response_with_fees,
                                                                BuildGetPaymentSourcesRequestCallbackDelegate build_get_payment_sources_request,
                                                                ParseGetPaymentSourcesResponseCallbackDelegate parse_get_payment_sources_response,
                                                                BuildPaymentRequestCallbackDelegate build_payment_req,
                                                                ParsePaymentResponseCallbackDelegate parse_payment_response,
                                                                BuildMintReqCallbackDelegate build_mint_req,
                                                                BuildSetTxnFeesReqCallbackDelegate build_set_txn_fees_req,
                                                                BuildGetTxnFeesReqCallbackDelegate build_get_txn_fees_req,
                                                                ParseGetTxnFeesResponseCallbackDelegate parse_get_txn_fees_response,
                                                                BuildVerifyPaymentRequestCallbackDelegate build_verify_payment_req,
                                                                ParseVerifyPaymentResponseCallbackDelegate parse_verify_payment_response,
                                                                IndyMethodCompletedDelegate cb);
    }
}
