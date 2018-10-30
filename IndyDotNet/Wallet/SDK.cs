using System;
using System.Runtime.InteropServices;
using static IndyDotNet.Utils.CallbackHelper;

namespace IndyDotNet.Wallet
{
    internal static class NativeMethods
    {
        #region delegates
        /// <summary>
        /// Delegate for the function called back to when a wallet of a custom type is created.
        /// </summary>
        /// <param name="name">The name of the wallet.</param>
        /// <param name="config">The configuration of the wallet.</param>
        /// <param name="credentials">The credentials for the wallet.</param>
        internal delegate ErrorCode WalletTypeCreateDelegate(string name, string config, string credentials);

        /// <summary>
        /// Delegate for the function called back to when a wallet of a custom type is opened.
        /// </summary>
        /// <param name="name">The name of the wallet to open.</param>
        /// <param name="config">The configuration for the wallet.</param>
        /// <param name="runtime_config">The runtime configuration for the wallet.</param>
        /// <param name="credentials">The credentials of the wallet.</param>
        /// <param name="handle">A handle to use when tracking the wallet instance.</param>
        internal delegate ErrorCode WalletTypeOpenDelegate(string name, string config, string runtime_config, string credentials, ref int handle);

        /// <summary>
        /// Delegate for the function called back to when value is set on a wallet of a custom type.
        /// </summary>
        /// <param name="handle">The handle of the wallet instance the action is being performed on.</param>
        /// <param name="key">The key of the value to set.</param>
        /// <param name="value">The value to set.</param>
        internal delegate ErrorCode WalletTypeSetDelegate(int handle, string key, string value);

        /// <summary>
        /// Delegate for the function called back to when value is requested from a wallet of a custom type.
        /// </summary>
        /// <param name="handle">The handle of the wallet instance the action is being performed on.</param>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="value_ptr">The pointer to the value associated with the key.</param>
        internal delegate ErrorCode WalletTypeGetDelegate(int handle, string key, ref IntPtr value_ptr);

        /// <summary>
        /// Delegate for the function called back to when an unexpired value is requested from a wallet of a custom type.
        /// </summary>
        /// <param name="handle">The handle of the wallet instance the action is being performed on.</param>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="value_ptr">The pointer to the value associated with the key.</param>
        internal delegate ErrorCode WalletTypeGetNotExpiredDelegate(int handle, string key, ref IntPtr value_ptr);

        /// <summary>
        /// Delegate for the function called back to when an list of values is requested from a wallet of a custom type.
        /// </summary>
        /// <param name="handle">The handle of the wallet instance the action is being performed on.</param>
        /// <param name="keyPrefix">The key prefix for the values requested.</param>
        /// <param name="values_json_ptr">The pointer to the values associated with the key prefix.</param>
        internal delegate ErrorCode WalletTypeListDelegate(int handle, string keyPrefix, ref IntPtr values_json_ptr);

        /// <summary>
        /// Delegate for the function called back to when a wallet of a custom type is closed.
        /// </summary>
        /// <param name="handle">The handle of the wallet instance the action is being performed on.</param>
        internal delegate ErrorCode WalletTypeCloseDelegate(int handle);

        /// <summary>
        /// Delegate for the function called back to when a wallet of a custom type is deleted.
        /// </summary>
        /// <param name="name">The name of the wallet being deleted</param>
        /// <param name="config">The configuration of the wallet.</param>
        /// <param name="credentials">The credentials of the wallet.</param>
        internal delegate ErrorCode WalletTypeDeleteDelegate(string name, string config, string credentials);

        /// <summary>
        /// Delegate for the function called back to when a value in a  wallet of a custom type is freed.
        /// </summary>
        /// <param name="handle">The handle of the wallet the action is being performed on.</param>
        /// <param name="value">A pointer to the value to be freed.</param>
        internal delegate ErrorCode WalletTypeFreeDelegate(int handle, IntPtr value);

        internal delegate void OpenWalletCompletedDelegate(int xcommand_handle, int err, IntPtr wallet_handle);

        internal delegate void GenerateWalletKeyCompletedDelegate(int xcommand_handle, int err, string key);
        #endregion

        #region API methods
        [DllImport(Consts.NATIVE_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int indy_create_wallet(int command_handle, string config, string credentials, IndyMethodCompletedDelegate cb);

        [DllImport(Consts.NATIVE_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int indy_open_wallet(int command_handle, string config, string credentials, OpenWalletCompletedDelegate cb);

        [DllImport(Consts.NATIVE_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int indy_export_wallet(int command_handle, IntPtr wallet_handle, string export_config, IndyMethodCompletedDelegate cb);

        [DllImport(Consts.NATIVE_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int indy_import_wallet(int command_handle, string config, string credentials, string import_config, IndyMethodCompletedDelegate cb);

        /// <summary>
        /// Closes opened wallet and frees allocated resources.
        /// </summary>
        /// <param name="command_handle">The handle for the command that will be passed to the callback.</param>
        /// <param name="wallet_handle">wallet handle returned by indy_open_wallet.</param>
        /// <param name="cb">The function that will be called when the asynchronous call is complete.</param>
        /// <returns>0 if the command was initiated successfully.  Any non-zero result indicates an error.</returns>
        [DllImport(Consts.NATIVE_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int indy_close_wallet(int command_handle, IntPtr wallet_handle, IndyMethodCompletedDelegate cb);

        /// <summary>
        /// Deletes created wallet.
        /// </summary>
        /// <param name="command_handle">The handle for the command that will be passed to the callback.</param>
        /// <param name="config">Name of the wallet to delete.</param>
        /// <param name="credentials">Wallet credentials json</param>
        /// <param name="cb">The function that will be called when the asynchronous call is complete.</param>
        /// <returns>0 if the command was initiated successfully.  Any non-zero result indicates an error.</returns>
        [DllImport(Consts.NATIVE_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int indy_delete_wallet(int command_handle, string config, string credentials, IndyMethodCompletedDelegate cb);

        [DllImport(Consts.NATIVE_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int indy_generate_wallet_key(int command_handle, string config, GenerateWalletKeyCompletedDelegate cb);
        #endregion
    }
}
