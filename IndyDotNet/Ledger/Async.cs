using System;
using System.Threading.Tasks;
using IndyDotNet.Did;
using IndyDotNet.Pool;
using IndyDotNet.Utils;
using IndyDotNet.Wallet;
using static IndyDotNet.Ledger.NativeMethods;

namespace IndyDotNet.Ledger
{
    /// <summary>
    /// This region will go away as the API is restructure to fit the IndyDotNet model.  Just having this here
    /// to keep project compilable
    /// </summary>
    #region temporary classes 
    /// <summary>
    /// Parse registry response result.
    /// </summary>
    internal class ParseRegistryResponseResult
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string Id { get; }

        /// <summary>
        /// Gets the object json.
        /// </summary>
        /// <value>The object json.</value>
        public string ObjectJson { get; }

        /// <summary>
        /// Gets the timestamp.
        /// </summary>
        /// <value>The timestamp.</value>
        public ulong Timestamp { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Hyperledger.Indy.LedgerApi.ParseRegistryResponseResult"/> class.
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <param name="objectJson">Object json.</param>
        /// <param name="timestamp">Timestamp.</param>
        internal ParseRegistryResponseResult(string id, string objectJson, ulong timestamp)
        {
            Id = id;
            ObjectJson = objectJson;
            Timestamp = timestamp;
        }
    }

    /// <summary>
    /// Parse response result.
    /// </summary>
    internal class ParseResponseResult
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string Id { get; private set; }

        /// <summary>
        /// Gets the object json.
        /// </summary>
        /// <value>The object json.</value>
        public string ObjectJson { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Hyperledger.Indy.LedgerApi.ParseResponseResult"/> class.
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <param name="objectJson">Object json.</param>
        internal ParseResponseResult(string id, string objectJson)
        {
            Id = id;
            ObjectJson = objectJson;
        }
    }
    #endregion



    /// <summary>
    /// Provides methods for building messages suitable for submission to the ledger and
    /// methods for signing and submitting messages to the ledger.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This class provides methods for generating messages for submission to the ledger; each 
    /// of these methods is prefixed with the word 'Build' and returns a JSON message which must be 
    /// signed and submitted to a node pool. These messages can be submitted to the ledger using the 
    /// <see cref="SignAndSubmitRequestAsync(IPool, IWallet, IDid, string)"/> or can be signed first 
    /// using the <see cref="SignRequestAsync(IWallet, IDid, string)"/> method then submitted later 
    /// using the <see cref="SubmitRequestAsync(IPool, string)"/> method.
    /// </para>
    /// </remarks>
    internal static class LedgerAsync
    {    
        #region callback definitions
        /// <summary>
        /// Gets the callback to use when a command that submits a message to the ledger completes.
        /// </summary>
#if __IOS__
        [MonoPInvokeCallback(typeof(SubmitRequestCompletedDelegate))]
#endif
        private static void SubmitRequestCallbackMethod(int xcommand_handle, int err, string response_json)
        {
            var taskCompletionSource = PendingCommands.Remove<string>(xcommand_handle);

            if (!CallbackHelper.CheckCallback(taskCompletionSource, err))
                return;

            taskCompletionSource.SetResult(response_json);
        }
        private static SubmitRequestCompletedDelegate SubmitRequestCallback = SubmitRequestCallbackMethod;

        /// <summary>
        /// Gets the callback to use when a command that builds a request completes.
        /// </summary>
#if __IOS__
        [MonoPInvokeCallback(typeof(BuildRequestCompletedDelegate))]
#endif
        private static void BuildRequestCallbackMethod(int xcommand_handle, int err, string request_json)
        {
            var taskCompletionSource = PendingCommands.Remove<string>(xcommand_handle);

            if (!CallbackHelper.CheckCallback(taskCompletionSource, err))
                return;

            taskCompletionSource.SetResult(request_json);
        }
        private static BuildRequestCompletedDelegate BuildRequestCallback = BuildRequestCallbackMethod;

#if __IOS__
        [MonoPInvokeCallback(typeof(ParseResponseCompletedDelegate))]
#endif
        private static void ParseResponseCallbackMethod(int xcommand_handle, int err, string id, string object_json)
        {
            var taskCompletionSource = PendingCommands.Remove<ParseResponseResult>(xcommand_handle);

            if (!CallbackHelper.CheckCallback(taskCompletionSource, err))
                return;

            taskCompletionSource.SetResult(new ParseResponseResult(id, object_json));
        }
        private static ParseResponseCompletedDelegate ParseResponseCallback = ParseResponseCallbackMethod;

#if __IOS__
        [MonoPInvokeCallback(typeof(ParseRegistryResponseCompletedDelegate))]
#endif
        private static void ParseRegistryResponseCallbackMethod(int xcommand_handle, int err, string id, string object_json, ulong timestamp)
        {
            var taskCompletionSource = PendingCommands.Remove<ParseRegistryResponseResult>(xcommand_handle);

            if (!CallbackHelper.CheckCallback(taskCompletionSource, err))
                return;

            taskCompletionSource.SetResult(new ParseRegistryResponseResult(id, object_json, timestamp));
        }
        private static ParseRegistryResponseCompletedDelegate ParseRegistryResponseCallback = ParseRegistryResponseCallbackMethod;

        /// <summary>
        /// Gets the callback to use when the command for SignRequestAsync has completed.
        /// </summary>
#if __IOS__
        [MonoPInvokeCallback(typeof(SignRequestCompletedDelegate))]
#endif
        private static void SignRequestCallbackMethod(int xcommand_handle, int err, string signed_request_json)
        {
            var taskCompletionSource = PendingCommands.Remove<string>(xcommand_handle);

            if (!CallbackHelper.CheckCallback(taskCompletionSource, err))
                return;

            taskCompletionSource.SetResult(signed_request_json);
        }
        private static SignRequestCompletedDelegate SignRequestCallback = SignRequestCallbackMethod;
        #endregion

        /// <summary>
        /// Signs a request message.
        /// </summary>
        /// <remarks>
        /// This method adds information associated with the submitter specified by the
        /// <paramref name="submitterDid"/> to the JSON provided in the <paramref name="requestJson"/> parameter
        /// then signs it with the submitter's signing key from the provided wallet.
        /// </remarks>
        /// <param name="wallet">The wallet to use for signing.</param>
        /// <param name="submitterDid">The DID of the submitter identity in the provided wallet.</param>
        /// <param name="requestJson">The request JSON to sign.</param>
        /// <returns>An asynchronous task that resolves to a <see cref="string"/> containing the signed 
        /// message.</returns>
        public static Task<string> SignRequestAsync(IWallet wallet, IDid submitterDid, string requestJson)
        {
            ParamGuard.NotNull(wallet, "wallet");
            ParamGuard.NotNull(submitterDid, "submitterDid");
            ParamGuard.NotNullOrWhiteSpace(requestJson, "requestJson");

            var taskCompletionSource = new TaskCompletionSource<string>();
            var commandHandle = PendingCommands.Add(taskCompletionSource);

            int result = NativeMethods.indy_sign_request(
                commandHandle,
                wallet.Handle,
                submitterDid.Did,
                requestJson,
                SignRequestCallback);

            CallbackHelper.CheckResult(result);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Multi signs request message.
        /// </summary>
        /// <remarks>
        /// This method adds information associated with the submitter specified by the
        /// <paramref name="submitterDid"/> to the JSON provided in the <paramref name="requestJson"/> parameter
        /// then signs it with the submitter's signing key from the provided wallet.
        /// </remarks>
        /// <param name="wallet">The wallet to use for signing.</param>
        /// <param name="submitterDid">The DID of the submitter identity in the provided wallet.</param>
        /// <param name="requestJson">The request JSON to sign.</param>
        /// <returns>An asynchronous task that resolves to a <see cref="string"/> containing the signed 
        /// message.</returns>
        public static Task<string> MultiSignRequestAsync(IWallet wallet, IDid submitterDid, string requestJson)
        {
            ParamGuard.NotNull(wallet, "wallet");
            ParamGuard.NotNull(submitterDid, "submitterDid");
            ParamGuard.NotNullOrWhiteSpace(requestJson, "requestJson");

            var taskCompletionSource = new TaskCompletionSource<string>();
            var commandHandle = PendingCommands.Add(taskCompletionSource);

            int result = NativeMethods.indy_multi_sign_request(
                commandHandle,
                wallet.Handle,
                submitterDid.Did,
                requestJson,
                SignRequestCallback);

            CallbackHelper.CheckResult(result);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Signs and submits a request to the validator pool.
        /// </summary>
        /// <remarks>
        /// This method adds information associated with the submitter specified by the
        /// <paramref name="submitterDid"/> to the JSON provided in the <paramref name="requestJson"/> parameter
        /// then signs it with the submitter's signing key from the provided <paramref name="wallet"/> and sends the signed 
        /// request message to the specified validator <paramref name="pool"/>.   
        /// </remarks>
        /// <param name="pool">The validator pool to submit the request to.</param>
        /// <param name="wallet">The wallet containing the submitter keys to sign the request with.</param>
        /// <param name="submitterDid">The DID of the submitter identity.</param>
        /// <param name="requestJson">The request JSON to sign and submit.</param>
        /// <returns>An asynchronous <see cref="Task{T}"/> that resolves to a JSON <see cref="string"/> 
        /// containing the result of submission when the operation completes.</returns>
        public static Task<string> SignAndSubmitRequestAsync(IPool pool, IWallet wallet, IDid submitterDid, string requestJson)
        {
            ParamGuard.NotNull(pool, "pool");
            ParamGuard.NotNull(wallet, "wallet");
            ParamGuard.NotNull(submitterDid, "submitterDid");
            ParamGuard.NotNullOrWhiteSpace(requestJson, "requestJson");

            var taskCompletionSource = new TaskCompletionSource<string>();
            var commandHandle = PendingCommands.Add(taskCompletionSource);

            var result = NativeMethods.indy_sign_and_submit_request(
                commandHandle,
                pool.Handle,
                wallet.Handle,
                submitterDid.Did,
                requestJson,
                SubmitRequestCallback
                );

            CallbackHelper.CheckResult(result);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Submits a request to the ledger.
        /// </summary>
        /// <remarks>
        /// This method publishes a message to the validator pool specified in the <paramref name="pool"/> parameter as-is 
        /// and assumes that the message was previously prepared for submission.  Requests can be signed prior to using this 
        /// call to the <see cref="SignRequestAsync(Wallet, string, string)"/> method, or messages can be 
        /// both signed and submitted using the <see cref="SignAndSubmitRequestAsync(Pool, Wallet, string, string)"/>
        /// method.
        /// </remarks>
        /// <param name="pool">The validator pool to submit the request to.</param>
        /// <param name="requestJson">The request to submit.</param>
        /// <returns>An asynchronous <see cref="Task{T}"/> that resolves to a JSON <see cref="string"/> 
        /// containing the results when the operation completes.</returns>
        public static Task<string> SubmitRequestAsync(IPool pool, string requestJson)
        {
            ParamGuard.NotNull(pool, "pool");
            ParamGuard.NotNullOrWhiteSpace(requestJson, "requestJson");

            var taskCompletionSource = new TaskCompletionSource<string>();
            var commandHandle = PendingCommands.Add(taskCompletionSource);

            var result = NativeMethods.indy_submit_request(
                commandHandle,
                pool.Handle,
                requestJson,
                SubmitRequestCallback);

            CallbackHelper.CheckResult(result);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Send action to particular nodes of validator pool.
        /// </summary>
        /// <param name="pool">The validator pool to submit the request to.</param>
        /// <param name="requestJson">The request to submit.</param>
        /// <param name="nodes">A list of node names to send the request to.</param>
        /// <param name="timeout">The time in seconds to wait for a response from the nodes.</param>
        /// <remarks>
        /// The list of node names in the <paramref name="nodes"/> parameter is optional, however if provided it should conform
        /// to the format ["Node1", "Node2",...."NodeN"].  To use the default timeout provide the value -1 to the 
        /// <paramref name="timeout"/> parameter.
        /// </remarks>
        /// <returns>An asynchronous <see cref="Task{T}"/> that resolves to a JSON <see cref="string"/> 
        /// containing the results when the operation completes.</returns>
        public static Task<string> SubmitActionAsync(IPool pool, string requestJson, string nodes, int timeout)
        {
            ParamGuard.NotNull(pool, "pool");
            ParamGuard.NotNullOrWhiteSpace(requestJson, "requestJson");

            var taskCompletionSource = new TaskCompletionSource<string>();
            var commandHandle = PendingCommands.Add(taskCompletionSource);

            var result = NativeMethods.indy_submit_action(
                commandHandle,
                pool.Handle,
                requestJson,
                nodes,
                timeout,
                SubmitRequestCallback);

            CallbackHelper.CheckResult(result);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Builds a ledger request to get a DDO.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This message builds a request message that is suitable for requesting a DDO from the ledger.
        /// </para>
        /// <para>
        /// The resulting message can be submitted to the ledger using the <see cref="SignAndSubmitRequestAsync(Pool, Wallet, string, string)"/>
        /// method or can be signed first using the <see cref="SignRequestAsync(Wallet, string, string)"/> 
        /// method then submitted later using the <see cref="SubmitRequestAsync(Pool, string)"/> method.
        /// </para>        
        /// </remarks>
        /// <param name="submitterDid">The DID of the party who will submit the request to the ledger.</param>
        /// <param name="targetDid">The DID of the DDO to get from the ledger.</param>
        /// <returns>An asynchronous <see cref="Task{T}"/> that resolves to a <see cref="string"/> 
        /// containing the request JSON.</returns>
        public static Task<string> BuildGetDdoRequestAsync(IDid submitterDid, IDid targetDid)
        {
            ParamGuard.NotNull(targetDid, "targetDid");

            var taskCompletionSource = new TaskCompletionSource<string>();
            var commandHandle = PendingCommands.Add(taskCompletionSource);

            var result = NativeMethods.indy_build_get_ddo_request(
                commandHandle,
                submitterDid.Did,
                targetDid.Did,
                BuildRequestCallback);

            CallbackHelper.CheckResult(result);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Builds a ledger request to store a NYM.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Builds a request message that is suitable for storing a NYM for the <paramref name="targetDid"/>
        /// on the ledger.
        /// </para>
        /// <para>
        /// Only the <paramref name="submitterDid"/> and <paramref name="targetDid"/> parameters
        /// are required, however the other parameters provide greater control over the process.  Normally
        /// the <paramref name="targetDid"/> and <paramref name="verKey"/> parameters would be from values
        /// generated by a prior call to <see cref="Did.CreateAndStoreMyDidAsync(Wallet, string)"/>.
        /// </para>
        /// <para>
        /// The <paramref name="role"/> parameter dictates what permissions the NYM will have - valid values
        /// are 'STEWARD' and 'TRUSTEE' and 'TRUST_ANCHOR'.
        /// </para>
        /// </remarks>
        /// <param name="submitterDid">The DID of the party who will submit the request to the ledger.</param>
        /// <param name="targetDid">The DID the NYM belongs to.</param>
        /// <param name="verKey">The verification key for the NYM.</param>
        /// <param name="alias">The alias for the NYM.</param>
        /// <param name="role">The role of the NYM.</param>
        /// <returns>An asynchronous <see cref="Task{T}"/> that resolves to a <see cref="string"/> 
        /// containing the request JSON. </returns>
        public static Task<string> BuildNymRequestAsync(IDid submitterDid, IDid targetDid, string verKey, string alias, string role)
        {
            ParamGuard.NotNull(submitterDid, "submitterDid");
            ParamGuard.NotNull(targetDid, "targetDid");

            var taskCompletionSource = new TaskCompletionSource<string>();
            var commandHandle = PendingCommands.Add(taskCompletionSource);

            var result = NativeMethods.indy_build_nym_request(
                commandHandle,
                submitterDid.Did,
                targetDid.Did,
                verKey,
                alias,
                role,
                BuildRequestCallback
                );

            CallbackHelper.CheckResult(result);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Builds a ledger request for storing an ATTRIB.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Builds a request message that is suitable for setting an attribute on the ledger.
        /// </para>
        /// <para>
        /// The <paramref name="submitterDid"/>, <paramref name="targetDid"/> are mandatory and
        /// any one of the <paramref name="hash"/>, <paramref name="raw"/> or <paramref name="enc"/> 
        /// parameters must also be provided, depending on what type of data should be stored.
        /// </para>
        /// </remarks>
        /// <param name="submitterDid">The DID of the party that will submit the request to the ledger.</param>
        /// <param name="targetDid">The DID the ATTRIB will belong to.</param>
        /// <param name="hash">The hash of the ATTRIB data.</param>
        /// <param name="raw">The raw JSON attribute data.</param>
        /// <param name="enc">The encrypted attribute data.</param>
        /// <returns>An asynchronous <see cref="Task{T}"/> that resolves to a <see cref="string"/> 
        /// containing the request JSON. </returns>
        public static Task<string> BuildAttribRequestAsync(IDid submitterDid, IDid targetDid, string hash, string raw, string enc)
        {
            ParamGuard.NotNull(submitterDid, "submitterDid");
            ParamGuard.NotNull(targetDid, "targetDid");

            var taskCompletionSource = new TaskCompletionSource<string>();
            var commandHandle = PendingCommands.Add(taskCompletionSource);

            var result = NativeMethods.indy_build_attrib_request(
                commandHandle,
                submitterDid.Did,
                targetDid.Did,
                hash,
                raw,
                enc,
                BuildRequestCallback
                );

            CallbackHelper.CheckResult(result);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Builds a GET_ATTRIB ledger request.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Builds a request message that is suitable for requesting an attribute from the 
        /// ledger.
        /// </para>
        /// </remarks>
        /// <param name="submitterDid">The DID of the submitter.</param>
        /// <param name="targetDid">The target DID.</param>
        /// <param name="raw">The name of the attribute to get.</param>
        /// <param name="hash"></param>
        /// <param name="enc"></param>
        /// <returns>An asynchronous <see cref="Task{T}"/> that resolves to a <see cref="string"/> 
        /// containing the request JSON. </returns>
        public static Task<string> BuildGetAttribRequestAsync(IDid submitterDid, IDid targetDid, string raw, string hash, string enc)
        {
            ParamGuard.NotNull(targetDid, "targetDid");

            var taskCompletionSource = new TaskCompletionSource<string>();
            var commandHandle = PendingCommands.Add(taskCompletionSource);

            var result = NativeMethods.indy_build_get_attrib_request(
                commandHandle,
                submitterDid.Did,
                targetDid.Did,
                raw,
                hash,
                enc,
                BuildRequestCallback
                );

            CallbackHelper.CheckResult(result);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Builds a GET_NYM ledger request.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Builds a request message that is suitable for requesting a NYM from the 
        /// ledger.
        /// </para>
        /// </remarks>
        /// <param name="submitterDid">The DID of the party submitting the request.</param>
        /// <param name="targetDid">The target DID.</param>
        /// <returns>An asynchronous <see cref="Task{T}"/> that resolves to a <see cref="string"/> 
        /// containing the request JSON. </returns>
        public static Task<string> BuildGetNymRequestAsync(IDid submitterDid, IDid targetDid)
        {
            ParamGuard.NotNull(targetDid, "targetDid");

            var taskCompletionSource = new TaskCompletionSource<string>();
            var commandHandle = PendingCommands.Add(taskCompletionSource);

            var result = NativeMethods.indy_build_get_nym_request(
                commandHandle,
                submitterDid.Did,
                targetDid.Did,
                BuildRequestCallback
                );

            CallbackHelper.CheckResult(result);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Builds a SCHEMA ledger request to store a schema.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Builds a request message that is suitable for storing a schema on a 
        /// ledger.  Schema specify the data types and formats which are used to make up claims.
        /// </para>
        /// <para>
        /// The <paramref name="data"/> parameter must contain a JSON string with the members "name",
        /// "version" and "attr_names" that define the schema.  For example the following JSON describes
        /// a schema with the name "access" that is version 1.0 of the schema and specifies the attributes
        /// "ip", "port", and "keys":
        /// <code>
        /// {
        ///     "id": "id",
        ///     "name":"access",
        ///     "version":"1.0",
        ///     "attrNames":["ip","port","keys"],
        ///     "ver":"1.0"      // note: this must always be 1.0
        /// }
        /// </code>
        /// </para>
        /// </remarks>
        /// <param name="submitterDid">The DID of the submitter.</param>
        /// <param name="data">The JSON schema.</param>
        /// <returns>An asynchronous <see cref="Task{T}"/> that resolves to a <see cref="string"/> 
        /// containing the request JSON. </returns>
        public static Task<string> BuildSchemaRequestAsync(IDid submitterDid, string data)
        {
            ParamGuard.NotNull(submitterDid, "submitterDid");
            ParamGuard.NotNullOrWhiteSpace(data, "data");

            var taskCompletionSource = new TaskCompletionSource<string>();
            var commandHandle = PendingCommands.Add(taskCompletionSource);

            var result = NativeMethods.indy_build_schema_request(
                commandHandle,
                submitterDid.Did,
                data,
                BuildRequestCallback
                );

            CallbackHelper.CheckResult(result);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Builds a SCHEMA request. Request to add Credential's schema.
        /// </summary>
        /// <returns>The get schema request async.</returns>
        /// <param name="submitterDid">DID of the submitter stored in secured Wallet..</param>
        /// <param name="schemaId">Schema ID in ledger</param>
        public static Task<string> BuildGetSchemaRequestAsync(IDid submitterDid, string schemaId)
        {
            ParamGuard.NotNullOrWhiteSpace(schemaId, "schemaId");

            var taskCompletionSource = new TaskCompletionSource<string>();
            var commandHandle = PendingCommands.Add(taskCompletionSource);

            var result = NativeMethods.indy_build_get_schema_request(
                commandHandle,
                submitterDid.Did,
                schemaId,
                BuildRequestCallback
                );

            CallbackHelper.CheckResult(result);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Parse a GET_SCHEMA response to get Schema in the format compatible with Anoncreds API.
        /// </summary>
        /// <returns>
        /// Schema Id and Schema json.
        /// {
        ///     id: identifier of schema
        ///     attrNames: array of attribute name strings
        ///     name: Schema's name string
        ///     version: Schema's version string
        ///     ver: Version of the Schema json
        /// }</returns>
        /// <param name="getSchemaResponse">response of GET_SCHEMA request.</param>
        public static Task<ParseResponseResult> ParseGetSchemaResponseAsync(string getSchemaResponse)
        {
            ParamGuard.NotNullOrWhiteSpace(getSchemaResponse, "getSchemaResponse");

            var taskCompletionSource = new TaskCompletionSource<ParseResponseResult>();
            var commandHandle = PendingCommands.Add(taskCompletionSource);

            var result = NativeMethods.indy_parse_get_schema_response(
                commandHandle,
                getSchemaResponse,
                ParseResponseCallback
                );

            CallbackHelper.CheckResult(result);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Builds an CRED_DEF request. Request to add a credential definition (in particular, public key),
        /// that Issuer creates for a particular Credential Schema.
        /// </summary>
        /// <returns>The cred def txn async.</returns>
        /// <param name="submitterDid">DID of the submitter stored in secured Wallet.</param>
        /// <param name="data">Credential definition json</param>
        public static Task<string> BuildCredDefRequestAsync(IDid submitterDid, string data)
        {
            ParamGuard.NotNull(submitterDid, "submitterDid");
            ParamGuard.NotNullOrWhiteSpace(data, "data");

            var taskCompletionSource = new TaskCompletionSource<string>();
            var commandHandle = PendingCommands.Add(taskCompletionSource);

            var result = NativeMethods.indy_build_cred_def_request(
                commandHandle,
                submitterDid.Did,
                data,
                BuildRequestCallback
                );

            CallbackHelper.CheckResult(result);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Builds a GET_CRED_DEF request.Request to get a credential definition (in particular, public key),
        /// that Issuer creates for a particular Credential Schema.
        /// </summary>
        /// <returns>The get cred def request async.</returns>
        /// <param name="submitterDid">Submitter did.</param>
        /// <param name="id">Identifier.</param>
        public static Task<string> BuildGetCredDefRequestAsync(IDid submitterDid, string id)
        {
            ParamGuard.NotNullOrWhiteSpace(id, "id");

            var taskCompletionSource = new TaskCompletionSource<string>();
            var commandHandle = PendingCommands.Add(taskCompletionSource);

            var result = NativeMethods.indy_build_get_cred_def_request(
                commandHandle,
                submitterDid.Did,
                id,
                BuildRequestCallback
                );

            CallbackHelper.CheckResult(result);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Parse a GET_CRED_DEF response to get Credential Definition in the format compatible with Anoncreds API.
        /// </summary>
        /// <returns>The get cred def response async.</returns>
        /// <param name="getCredDefResponse">Get cred def response.</param>
        public static Task<ParseResponseResult> ParseGetCredDefResponseAsync(string getCredDefResponse)
        {
            ParamGuard.NotNullOrWhiteSpace(getCredDefResponse, "getCredDefResponse");

            var taskCompletionSource = new TaskCompletionSource<ParseResponseResult>();
            var commandHandle = PendingCommands.Add(taskCompletionSource);

            var result = NativeMethods.indy_parse_get_cred_def_response(
                commandHandle,
                getCredDefResponse,
                ParseResponseCallback
                );

            CallbackHelper.CheckResult(result);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Builds a NODE ledger request.
        /// </summary>
        /// <param name="submitterDid">The DID of the submitter.</param>
        /// <param name="targetDid">The target DID.</param>
        /// <param name="data">id of a target NYM record</param>
        /// <returns>An asynchronous <see cref="Task{T}"/> that resolves to a <see cref="string"/> 
        /// containing the request JSON. </returns>
        public static Task<string> BuildNodeRequestAsync(IDid submitterDid, IDid targetDid, string data)
        {
            ParamGuard.NotNull(submitterDid, "submitterDid");
            ParamGuard.NotNull(targetDid, "targetDid");
            ParamGuard.NotNullOrWhiteSpace(data, "data");

            var taskCompletionSource = new TaskCompletionSource<string>();
            var commandHandle = PendingCommands.Add(taskCompletionSource);

            var result = NativeMethods.indy_build_node_request(
                commandHandle,
                submitterDid.Did,
                targetDid.Did,
                data,
                BuildRequestCallback
                );

            CallbackHelper.CheckResult(result);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Builds a GET_VALIDATOR_INFO request.
        /// </summary>
        /// <param name="submitterDid">The DID of the submitter.</param>
        /// <returns>An asynchronous <see cref="Task{T}"/> that resolves to a <see cref="string"/> 
        /// containing the request JSON. </returns>
        public static Task<string> BuildGetValidatorInfoRequestAsync(IDid submitterDid)
        {
            ParamGuard.NotNull(submitterDid, "submitterDid");

            var taskCompletionSource = new TaskCompletionSource<string>();
            var commandHandle = PendingCommands.Add(taskCompletionSource);

            var result = NativeMethods.indy_build_get_validator_info_request(
                commandHandle,
                submitterDid.Did,
                BuildRequestCallback);

            CallbackHelper.CheckResult(result);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Builds a GET_TXN request
        /// </summary>
        /// <param name="submitterDid">The DID of the submitter</param>
        /// <param name="ledgerType">The type of the ledger the requested transaction belongs to</param>
        /// <param name="seqNo">The requested transaction sequence number as it is stored on the ledger</param>
        /// <returns>An asynchronous <see cref="Task{T}"/> that resolves to a <see cref="string"/> 
        /// containing the request JSON. </returns>
        /// <remarks>
        /// The <paramref name="submitterDid"/> if passed as null will default to the LibIndy DID.  The 
        /// <paramref name="ledgerType"/> can also be passed as null and will default to 'DOMAIN'.  Other values that can be
        /// passed for this parameter are 'POOL', 'CONFIG' or a string containing any numeric value.
        /// </remarks>
        public static Task<string> BuildGetTxnRequestAsync(IDid submitterDid, string ledgerType, int seqNo)
        {
            var taskCompletionSource = new TaskCompletionSource<string>();
            var commandHandle = PendingCommands.Add(taskCompletionSource);

            var result = NativeMethods.indy_build_get_txn_request(
                commandHandle,
                submitterDid.Did,
                ledgerType,
                seqNo,
                BuildRequestCallback);

            CallbackHelper.CheckResult(result);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Builds a POOL_CONFIG request.
        /// </summary>
        /// <returns>Request result as json.</returns>
        /// <param name="submitterDid">Id of Identity stored in secured Wallet.</param>
        /// <param name="writes">If set to <c>true</c> writes.</param>
        /// <param name="force">If set to <c>true</c> force.</param>
        /// <returns>An asynchronous <see cref="Task{T}"/> that resolves to a <see cref="string"/> 
        /// containing the request JSON. </returns>
        public static Task<string> BuildPoolConfigRequestAsync(IDid submitterDid, bool writes, bool force)
        {
            ParamGuard.NotNull(submitterDid, "submitterDid");

            var taskCompletionSource = new TaskCompletionSource<string>();
            var commandHandle = PendingCommands.Add(taskCompletionSource);

            var result = NativeMethods.indy_build_pool_config_request(
                commandHandle,
                submitterDid.Did,
                writes,
                force,
                BuildRequestCallback);

            CallbackHelper.CheckResult(result);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Builds a POOL_RESTART request.
        /// </summary>
        /// <param name="submitterDid">Id of Identity stored in secured Wallet.</param>
        /// <param name="action">Action that pool has to do after received transaction.</param>
        /// <param name="dateTime">Restart time in datetime format.</param>
        /// <returns>An asynchronous <see cref="Task{T}"/> that resolves to a <see cref="string"/> 
        /// containing the request JSON. </returns>
        /// <remarks>
        /// A null can be passed for the <paramref name="dateTime"/> parameter to restart as early as possible.
        /// </remarks>
        public static Task<string> BuildPoolRestartRequestAsync(IDid submitterDid, string action, string dateTime)
        {
            ParamGuard.NotNull(submitterDid, "submitterDid");

            var taskCompletionSource = new TaskCompletionSource<string>();
            var commandHandle = PendingCommands.Add(taskCompletionSource);

            var result = NativeMethods.indy_build_pool_restart_request(
                commandHandle,
                submitterDid.Did,
                action,
                dateTime,
                BuildRequestCallback);

            CallbackHelper.CheckResult(result);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Builds a POOL_UPGRADE request.
        /// </summary>
        /// <returns>Request result as json.</returns>
        /// <param name="submitterDid">Submitter did.</param>
        /// <param name="name">Name.</param>
        /// <param name="version">Version.</param>
        /// <param name="action">Either start or cancel</param>
        /// <param name="sha256">Sha256.</param>
        /// <param name="timeout">Timeout.</param>
        /// <param name="schedule">Schedule.</param>
        /// <param name="justification">Justification.</param>
        /// <param name="reinstall">If set to <c>true</c> reinstall.</param>
        /// <param name="force">If set to <c>true</c> force.</param>
        /// <param name="package">Package to be upgraded.</param>
        public static Task<string> BuildPoolUpgradeRequestAsync(IDid submitterDid, string name, string version, string action, string sha256, int timeout, string schedule, string justification, bool reinstall, bool force, string package)
        {
            ParamGuard.NotNull(submitterDid, "submitterDid");

            var taskCompletionSource = new TaskCompletionSource<string>();
            var commandHandle = PendingCommands.Add(taskCompletionSource);

            var result = NativeMethods.indy_build_pool_upgrade_request(
                commandHandle,
                submitterDid.Did,
                name,
                version,
                action,
                sha256,
                timeout,
                schedule,
                justification,
                reinstall,
                force,
                package,
                BuildRequestCallback);

            CallbackHelper.CheckResult(result);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Builds a REVOC_REG_DEF request. Request to add the definition of revocation registry
        /// to an exists credential definition.
        /// </summary>
        /// <returns>The revoc reg def request async.</returns>
        /// <param name="submitterDid">DID of the submitter stored in secured Wallet.</param>
        /// <param name="data">
        /// data: Revocation Registry data:
        ///     {
        ///         "id": string - ID of the Revocation Registry,
        ///         "revocDefType": string - Revocation Registry type (only CL_ACCUM is supported for now),
        ///         "tag": string - Unique descriptive ID of the Registry,
        ///         "credDefId": string - ID of the corresponding CredentialDefinition,
        ///         "value": Registry-specific data {
        ///             "issuanceType": string - Type of Issuance(ISSUANCE_BY_DEFAULT or ISSUANCE_ON_DEMAND),
        ///             "maxCredNum": number - Maximum number of credentials the Registry can serve.
        ///             "tailsHash": string - Hash of tails.
        ///             "tailsLocation": string - Location of tails file.
        ///             "publicKeys": &lt;public_keys> - Registry's public key.
        ///         },
        ///         "ver": string - version of revocation registry definition json.
        ///     }.</param>
        public static Task<string> BuildRevocRegDefRequestAsync(IDid submitterDid, string data)
        {
            ParamGuard.NotNull(submitterDid, "submitterDid");

            var taskCompletionSource = new TaskCompletionSource<string>();
            var commandHandle = PendingCommands.Add(taskCompletionSource);

            var result = NativeMethods.indy_build_revoc_reg_def_request(
                commandHandle,
                submitterDid.Did,
                data,
                BuildRequestCallback);

            CallbackHelper.CheckResult(result);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Builds a GET_REVOC_REG_DEF request. Request to get a revocation registry definition,
        /// that Issuer creates for a particular Credential Definition.
        /// </summary>
        /// <returns>Request result as json.</returns>
        /// <param name="submitterDid">DID of the read request sender..</param>
        /// <param name="id">ID of Revocation Registry Definition in ledger..</param>
        public static Task<string> BuildGetRevocRegDefRequestAsync(IDid submitterDid, string id)
        {
            ParamGuard.NotNullOrWhiteSpace(id, "id");

            var taskCompletionSource = new TaskCompletionSource<string>();
            var commandHandle = PendingCommands.Add(taskCompletionSource);

            var result = NativeMethods.indy_build_get_revoc_reg_def_request(
                commandHandle,
                submitterDid.Did,
                id,
                BuildRequestCallback);

            CallbackHelper.CheckResult(result);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Parse a GET_REVOC_REG_DEF response to get Revocation Registry Definition in the format
        /// compatible with Anoncreds API.
        /// </summary>
        /// <returns>
        /// Revocation Registry Definition Id and Revocation Registry Definition json.
        /// {
        ///     "id": string - ID of the Revocation Registry,
        ///     "revocDefType": string - Revocation Registry type (only CL_ACCUM is supported for now),
        ///     "tag": string - Unique descriptive ID of the Registry,
        ///     "credDefId": string - ID of the corresponding CredentialDefinition,
        ///     "value": Registry-specific data {
        ///         "issuanceType": string - Type of Issuance(ISSUANCE_BY_DEFAULT or ISSUANCE_ON_DEMAND),
        ///         "maxCredNum": number - Maximum number of credentials the Registry can serve.
        ///         "tailsHash": string - Hash of tails.
        ///         "tailsLocation": string - Location of tails file.
        ///         "publicKeys": &lt;public_keys> - Registry's public key.
        ///     },
        ///     "ver": string - version of revocation registry definition json.
        /// }.</returns>
        /// <param name="getRevocRegDefResponse">response of GET_REVOC_REG_DEF request..</param>
        public static Task<ParseResponseResult> ParseGetRevocRegDefResponseAsync(string getRevocRegDefResponse)
        {
            ParamGuard.NotNullOrWhiteSpace(getRevocRegDefResponse, "getRevocRegDefResponse");

            var taskCompletionSource = new TaskCompletionSource<ParseResponseResult>();
            var commandHandle = PendingCommands.Add(taskCompletionSource);

            var result = NativeMethods.indy_parse_get_revoc_reg_def_response(
                commandHandle,
                getRevocRegDefResponse,
                ParseResponseCallback);

            CallbackHelper.CheckResult(result);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Builds a REVOC_REG_ENTRY request.  Request to add the RevocReg entry containing
        /// the new accumulator value and issued/revoked indices.
        /// This is just a delta of indices, not the whole list.
        /// So, it can be sent each time a new credential is issued/revoked.
        /// </summary>
        /// <returns>The revoc reg entry request async.</returns>
        /// <param name="submitterDid">DID of the submitter stored in secured Wallet.</param>
        /// <param name="revocRegDefId">ID of the corresponding RevocRegDef.</param>
        /// <param name="revDefType">Revocation Registry type (only CL_ACCUM is supported for now).</param>
        /// <param name="value">
        /// Registry-specific data: {
        ///     value: {
        ///         prevAccum: string - previous accumulator value.
        ///         accum: string - current accumulator value.
        ///         issued: array&lt;number> - an array of issued indices.
        ///         revoked: array&lt;number> an array of revoked indices.
        ///     },
        ///     ver: string - version revocation registry entry json
        ///
        /// }.</param>
        public static Task<string> BuildRevocRegEntryRequestAsync(IDid submitterDid, string revocRegDefId, string revDefType, string value)
        {
            ParamGuard.NotNull(submitterDid, "submitterDid");
            ParamGuard.NotNullOrWhiteSpace(revocRegDefId, "revocRegDefId");
            ParamGuard.NotNullOrWhiteSpace(revDefType, "revDefType");

            var taskCompletionSource = new TaskCompletionSource<string>();
            var commandHandle = PendingCommands.Add(taskCompletionSource);

            var result = NativeMethods.indy_build_revoc_reg_entry_request(
                commandHandle,
                submitterDid.Did,
                revocRegDefId,
                revDefType,
                value,
                BuildRequestCallback);

            CallbackHelper.CheckResult(result);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Builds a GET_REVOC_REG request. Request to get the accumulated state of the Revocation Registry
        /// by ID. The state is defined by the given timestamp.
        /// </summary>
        /// <returns>Request result as json..</returns>
        /// <param name="submitterDid">DID of the read request sender.</param>
        /// <param name="revocRegDefId">ID of the corresponding Revocation Registry Definition in ledger.</param>
        /// <param name="timestamp">Requested time represented as a total number of seconds from Unix Epoch</param>
        public static Task<string> BuildGetRevocRegRequestAsync(IDid submitterDid, string revocRegDefId, long timestamp)
        {
            ParamGuard.NotNullOrWhiteSpace(revocRegDefId, "revocRegDefId");

            var taskCompletionSource = new TaskCompletionSource<string>();
            var commandHandle = PendingCommands.Add(taskCompletionSource);

            var result = NativeMethods.indy_build_get_revoc_reg_request(
                commandHandle,
                submitterDid.Did,
                revocRegDefId,
                timestamp,
                BuildRequestCallback);

            CallbackHelper.CheckResult(result);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Parse a GET_REVOC_REG response to get Revocation Registry in the format compatible with Anoncreds API.
        /// </summary>
        /// <returns>
        /// Revocation Registry Definition Id, Revocation Registry json and Timestamp.
        /// {
        ///     "value": Registry-specific data {
        ///         "accum": string - current accumulator value.
        ///     },
        ///     "ver": string - version revocation registry json
        /// }
        /// </returns>
        /// <param name="getRevocRegResponse">response of GET_REVOC_REG request.</param>
        public static Task<ParseRegistryResponseResult> ParseGetRevocRegResponseAsync(string getRevocRegResponse)
        {
            ParamGuard.NotNullOrWhiteSpace(getRevocRegResponse, "getRevocRegResponse");

            var taskCompletionSource = new TaskCompletionSource<ParseRegistryResponseResult>();
            var commandHandle = PendingCommands.Add(taskCompletionSource);

            var result = NativeMethods.indy_parse_get_revoc_reg_response(
                commandHandle,
                getRevocRegResponse,
                ParseRegistryResponseCallback);

            CallbackHelper.CheckResult(result);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Builds a GET_REVOC_REG_DELTA request. Request to get the delta of the accumulated state of the Revocation Registry.
        /// The Delta is defined by from and to timestamp fields.
        /// If from is not specified, then the whole state till to will be returned.
        /// </summary>
        /// <returns>Request result as json.</returns>
        /// <param name="submitterDid">DID of the read request sender.</param>
        /// <param name="revocRegDefId">ID of the corresponding Revocation Registry Definition in ledger.</param>
        /// <param name="from">Requested time represented as a total number of seconds from Unix Epoch.</param>
        /// <param name="to">Requested time represented as a total number of seconds from Unix Epoch.</param>
        public static Task<string> BuildGetRevocRegDeltaRequestAsync(IDid submitterDid, string revocRegDefId, long from, long to)
        {
            ParamGuard.NotNullOrWhiteSpace(revocRegDefId, "id");

            var taskCompletionSource = new TaskCompletionSource<string>();
            var commandHandle = PendingCommands.Add(taskCompletionSource);

            var result = NativeMethods.indy_build_get_revoc_reg_delta_request(
                commandHandle,
                submitterDid.Did,
                revocRegDefId,
                from,
                to,
                BuildRequestCallback);

            CallbackHelper.CheckResult(result);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Parse a GET_REVOC_REG_DELTA response to get Revocation Registry Delta in the format compatible with Anoncreds API.
        /// </summary>
        /// <returns>
        /// Revocation Registry Definition Id, Revocation Registry Delta json and Timestamp.
        /// {
        ///     "value": Registry-specific data {
        ///         prevAccum: string - previous accumulator value.
        ///         accum: string - current accumulator value.
        ///         issued: array&lt;number> - an array of issued indices.
        ///         revoked: array&lt;number> an array of revoked indices.
        ///     },
        ///     "ver": string - version revocation registry delta json
        /// }</returns>
        /// <param name="getRevocRegDeltaResponse">response of GET_REVOC_REG_DELTA request.</param>
        public static Task<ParseRegistryResponseResult> ParseGetRevocRegDeltaResponseAsync(string getRevocRegDeltaResponse)
        {
            ParamGuard.NotNullOrWhiteSpace(getRevocRegDeltaResponse, "getRevocRegDeltaResponse");

            var taskCompletionSource = new TaskCompletionSource<ParseRegistryResponseResult>();
            var commandHandle = PendingCommands.Add(taskCompletionSource);

            var result = NativeMethods.indy_parse_get_revoc_reg_delta_response(
                commandHandle,
                getRevocRegDeltaResponse,
                ParseRegistryResponseCallback);

            CallbackHelper.CheckResult(result);

            return taskCompletionSource.Task;
        }
    }
}
