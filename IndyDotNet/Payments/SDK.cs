using System;
using System.Runtime.InteropServices;
using static IndyDotNet.PaymentHandler.NativeMethods;
using static IndyDotNet.Utils.CallbackHelper;

namespace IndyDotNet.Payments
{
    /// <summary>
    /// Native methods.
    /// </summary>
    internal static class NativeMethods
    {    
    
        [DllImport(Consts.NATIVE_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false)]
        internal static extern int indy_create_payment_address(int command_handle, IntPtr wallet_handle, string payment_method, string config, CreatePaymentAddressDelegate cb);

        /// <summary>
        /// Create payment address delegate.
        /// </summary>
        public delegate void CreatePaymentAddressDelegate(int command_handle, int err, string payment_address);
        /// <summary>
        /// Create payment address callback delegate.
        /// </summary>

        [DllImport(Consts.NATIVE_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false)]
        internal static extern int indy_list_payment_addresses(int command_handle, IntPtr wallet_handle, ListPaymentAddressesDelegate cb);

        internal delegate void ListPaymentAddressesDelegate(int command_handle, int err, string payment_addresses_json);


        [DllImport(Consts.NATIVE_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false)]
        internal static extern int indy_add_request_fees(int command_handle, IntPtr wallet_handle, string submitter_did, string req_json, string inputs_json, string outputs_json, string extra, AddRequestFeesDelegate cb);

        /// <summary>
        /// Add request fees delegate.
        /// </summary>
        public delegate void AddRequestFeesDelegate(int command_handle, int err, string req_with_fees_json, string payment_method);


        [DllImport(Consts.NATIVE_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false)]
        internal static extern int indy_parse_response_with_fees(int command_handle, string payment_method, string resp_json, ParseResponseWithFeesDelegate cb);

        /// <summary>
        /// Parse response with fees delegate.
        /// </summary>
        public delegate void ParseResponseWithFeesDelegate(int command_handle, int err, string receipts_json);


        [DllImport(Consts.NATIVE_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false)]
        internal static extern int indy_build_get_payment_sources_request(int command_handle, IntPtr wallet_handle, string submitter_did, string payment_address, BuildGetUtxoRequstDelegate cb);

        /// <summary>
        /// Build get utxo requst delegate.
        /// </summary>
        public delegate void BuildGetUtxoRequstDelegate(int command_handle, int err, string get_sources_txn_json, string payment_method);


        [DllImport(Consts.NATIVE_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false)]
        internal static extern int indy_parse_get_payment_sources_response(int command_handle, string payment_method, string resp_json, ParseGetUtxoResponseDelegate cb);

        /// <summary>
        /// Parse get utxo response delegate.
        /// </summary>
        public delegate void ParseGetUtxoResponseDelegate(int command_handle, int err, string sources_json);


        [DllImport(Consts.NATIVE_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false)]
        internal static extern int indy_build_payment_req(int command_handle, IntPtr wallet_handle, string submitter_did, string inputs_json, string outputs_json, string extra, BuildPaymentRequestDelegate cb);

        /// <summary>
        /// Build payment request delegate.
        /// </summary>
        public delegate void BuildPaymentRequestDelegate(int command_handle, int err, string payment_req_json, string payment_method);


        [DllImport(Consts.NATIVE_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false)]
        internal static extern int indy_parse_payment_response(int command_handle, string payment_method, string resp_json, ParsePaymentResponseDelegate cb);

        /// <summary>
        /// Parse payment response delegate.
        /// </summary>
        public delegate void ParsePaymentResponseDelegate(int command_handle, int err, string receipts_json);


        [DllImport(Consts.NATIVE_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false)]
        internal static extern int indy_build_mint_req(int command_handle, IntPtr wallet_handle, string submitter_did, string outputs_json, string extra, BuildMintReqDelegate cb);

        /// <summary>
        /// Build mint req delegate.
        /// </summary>
        public delegate void BuildMintReqDelegate(int command_handle, int err, string mint_req_json, string payment_method);


        [DllImport(Consts.NATIVE_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false)]
        internal static extern int indy_build_set_txn_fees_req(int command_handle, IntPtr wallet_handle, string submitter_did, string payment_method, string fees_json, BuildSetTxnFeesReqDelegate cb);
        /// <summary>
        /// Build set txn fees req delegate.
        /// </summary>
        public delegate void BuildSetTxnFeesReqDelegate(int command_handle, int err, string set_txn_fees_json);


        [DllImport(Consts.NATIVE_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false)]
        internal static extern int indy_build_get_txn_fees_req(int command_handle, IntPtr wallet_handle, string submitter_did, string payment_method, BuildGetTxnFeesReqDelegate cb);
        /// <summary>
        /// Build get txn fees req delegate.
        /// </summary>
        public delegate void BuildGetTxnFeesReqDelegate(int command_handle, int err, string get_txn_fees_json);


        [DllImport(Consts.NATIVE_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false)]
        internal static extern int indy_parse_get_txn_fees_response(int command_handle, string payment_method, string resp_json, ParseGetTxnFeesResponseDelegate cb);

        /// <summary>
        /// Parse get txn fees response delegate.
        /// </summary>
        public delegate void ParseGetTxnFeesResponseDelegate(int command_handle, int err, string fees_json);

        [DllImport(Consts.NATIVE_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false)]
        internal static extern int indy_build_verify_payment_req(int command_handle, IntPtr wallet_handle, string submitter_did, string receipt, BuildVerifyPaymentRequestDelegate cb);

        /// <summary>
        /// Build verify payment request delegate.
        /// </summary>
        public delegate void BuildVerifyPaymentRequestDelegate(int command_handle, int err, string verify_txn_json, string payment_method);

        [DllImport(Consts.NATIVE_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false)]
        internal static extern int indy_parse_verify_payment_response(int command_handle, string payment_method, string resp_json, ParseVerifyPaymentResponseDelegate cb);

        /// <summary>
        /// Parse verify payment request delegate.
        /// </summary>
        public delegate void ParseVerifyPaymentResponseDelegate(int command_handle, int err, string txn_json);
    }
}