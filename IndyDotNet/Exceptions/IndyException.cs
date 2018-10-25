using System;
using System.Diagnostics;

namespace IndyDotNet.Exceptions
{
    /// <summary>
    /// Exception indicating a problem originating from the Indy SDK.
    /// </summary>
    public class IndyException : Exception
    {
        /// <summary>
        /// Initializes a new IndyException with the specified message and SDK error code.
        /// </summary>
        /// <param name="message">The message for the exception.</param>
        /// <param name="sdkErrorCode">The SDK error code for the exception.</param>
        internal IndyException(String message, int sdkErrorCode) : base(message)
        {
            SdkErrorCode = sdkErrorCode;
        }

        /// <summary>
        /// Generates an IndyException or one of its subclasses from the provided SDK error code.
        /// </summary>
        /// <param name="sdkErrorCode">The error code.</param>
        /// <returns>An IndyException or subclass instance.</returns>
        internal static IndyException FromSdkError(int sdkErrorCode)
        {
            var errorCode = (ErrorCode)sdkErrorCode;
            
            switch (errorCode)
            {
                case ErrorCode.CommonInvalidParam1:
                case ErrorCode.CommonInvalidParam2:
                case ErrorCode.CommonInvalidParam3:
                case ErrorCode.CommonInvalidParam4:
                case ErrorCode.CommonInvalidParam5:
                case ErrorCode.CommonInvalidParam6:
                case ErrorCode.CommonInvalidParam7:
                case ErrorCode.CommonInvalidParam8:
                case ErrorCode.CommonInvalidParam9:
                case ErrorCode.CommonInvalidParam10:
                case ErrorCode.CommonInvalidParam11:
                case ErrorCode.CommonInvalidParam12:
                case ErrorCode.CommonInvalidParam13:
                case ErrorCode.CommonInvalidParam14:
                case ErrorCode.CommonInvalidParam15:
                case ErrorCode.CommonInvalidParam16:
                case ErrorCode.CommonInvalidParam17:
                case ErrorCode.CommonInvalidParam18:
                case ErrorCode.CommonInvalidParam19:
                case ErrorCode.CommonInvalidParam20:
                case ErrorCode.CommonInvalidParam21:
                case ErrorCode.CommonInvalidParam22:
                case ErrorCode.CommonInvalidParam23:
                case ErrorCode.CommonInvalidParam24:
                case ErrorCode.CommonInvalidParam25:
                case ErrorCode.CommonInvalidParam26:
                case ErrorCode.CommonInvalidParam27:
                    return new InvalidParameterException(sdkErrorCode);
                case ErrorCode.CommonInvalidState:
                    return new InvalidStateException();
                case ErrorCode.CommonInvalidStructure:
                    return new InvalidStructureException();
                case ErrorCode.CommonIOError:
                    return new IOException();
                //case ErrorCode.WalletInvalidHandle:
                //    return new InvalidWalletException(); 
                //case ErrorCode.WalletUnknownTypeError:
                //    return new UnknownWalletTypeException(); 
                //case ErrorCode.WalletTypeAlreadyRegisteredError:
                //    return new DuplicateWalletTypeException();
                //case ErrorCode.WalletAlreadyExistsError:
                //    return new WalletExistsException();
                //case ErrorCode.WalletNotFoundError:
                //    return new WalletNotFoundException();
                //case ErrorCode.WalletIncompatiblePoolError:
                //    return new WrongWalletForPoolException();
                //case ErrorCode.WalletAlreadyOpenedError:
                //    return new WalletAlreadyOpenedException();
                //case ErrorCode.WalletAccessFailed:
                //    return new WalletAccessFailedException();
                //case ErrorCode.PoolLedgerNotCreatedError:
                //    return new PoolConfigNotCreatedException();
                //case ErrorCode.PoolLedgerInvalidPoolHandle:
                //    return new InvalidPoolException();
                //case ErrorCode.PoolLedgerTerminated:
                //    return new PoolLedgerTerminatedException();
                //case ErrorCode.PoolIncompatibleProtocolVersionError:
                //    return new PoolIncompatibleProtocolVersionException();
                //case ErrorCode.PoolLedgerConfigAlreadyExistsError:
                //    return new PoolLedgerConfigExistsException();
                //case ErrorCode.LedgerNoConsensusError:
                //    return new LedgerConsensusException();
                //case ErrorCode.LedgerInvalidTransaction:
                //    return new InvalidLedgerTransactionException();
                //case ErrorCode.LedgerSecurityError:
                //    return new LedgerSecurityException();
                //case ErrorCode.AnoncredsRevocationRegistryFullError:
                //    return new RevocationRegistryFullException();
                //case ErrorCode.AnoncredsInvalidUserRevocId:
                //    return new InvalidUserRevocIdException();
                //case ErrorCode.AnoncredsMasterSecretDuplicateNameError:
                //    return new DuplicateMasterSecretNameException();
                //case ErrorCode.AnoncredsProofRejected:
                //    return new ProofRejectedException();
                //case ErrorCode.AnoncredsCredentialRevoked:
                //    return new CredentialRevokedException();
                //case ErrorCode.AnoncredsCredDefAlreadyExistsError:
                //    return new CredentialDefinitionAlreadyExistsException();
                //case ErrorCode.UnknownCryptoTypeError:
                //    return new UnknownCryptoTypeException();
                //case ErrorCode.WalletItemNotFoundError:
                //    return new WalletItemNotFoundException();
                //case ErrorCode.WalletItemAlreadyExistsError:
                //    return new WalletItemAlreadyExistsException();
                //case ErrorCode.WalletQueryError:
                //    return new WalletInvalidQueryException();
                //case ErrorCode.WalletStorageError:
                //    return new WalletStorageException();
                //case ErrorCode.WalletDecodingError:
                //    return new WalletDecodingException();
                //case ErrorCode.WalletEncryptionError:
                //    return new WalletEncryptionException();
                //case ErrorCode.WalletInputError:
                //    return new WalletInputException();
                //case ErrorCode.PaymentExtraFundsError:
                //    return new ExtraFundsException();
                //case ErrorCode.PaymentIncompatibleMethodsError:
                //    return new IncompatiblePaymentMethodsException();
                //case ErrorCode.PaymentInsufficientFundsError:
                //    return new InsufficientFundsException();
                //case ErrorCode.PaymentOperationNotSupportedError:
                //    return new PaymentOperationNotSupportedException();
                //case ErrorCode.PaymentSourceDoesNotExistError:
                //    return new PaymentSourceDoesNotExistException();
                //case ErrorCode.PaymentUnknownMethodError:
                    //return new UnknownPaymentMethodException();

                default:
                    var message = $"An unmapped error with the code '{sdkErrorCode}' was returned by the SDK.";
                    return new IndyException(message, sdkErrorCode);
            }      
        }

        /// <summary>
        /// Gets the error code for the exception.
        /// </summary>
        public int SdkErrorCode { get; private set; }
    }

    /// <summary>
    /// Exception indicating that one of the parameters provided to an SDK call contained a valid that was considered invalid.
    /// </summary>
    public class InvalidParameterException : IndyException
    {
        /// <summary>
        /// Gets the index of the parameter from the SDK error code.
        /// </summary>
        /// <param name="sdkErrorCode">The SDK error code.</param>
        /// <returns>The parameter index the SDK indicated was invalid.</returns>
        private static int GetParamIndex(int sdkErrorCode)
        {
            Debug.Assert((int)sdkErrorCode >= 100 && (int)sdkErrorCode <= 111);

            return (int)sdkErrorCode - 99;
        }

        /// <summary>
        /// Builds the error message from the SDK error code.
        /// </summary>
        /// <param name="sdkErrorCode">Teh SDK error code.</param>
        /// <returns>The error message.</returns>
        private static string BuildMessage(int sdkErrorCode)
        {
            return string.Format("The value passed to parameter {0} is not valid.", GetParamIndex(sdkErrorCode));
        }

        /// <summary>
        /// Initializes a new InvalidParameterException from the specified SDK error code.
        /// </summary>
        /// <param name="sdkErrorCode">The SDK error code that specifies which parameter was invalid.</param>
        internal InvalidParameterException(int sdkErrorCode) : base(BuildMessage(sdkErrorCode), sdkErrorCode)
        {
            ParameterIndex = GetParamIndex(sdkErrorCode);
        }

        /// <summary>
        /// Gets the index of the parameter that contained the invalid value.
        /// </summary>
        public int ParameterIndex { get; private set; }
    }

    /// <summary>
    /// Exception indicating that the SDK library experienced an unexpected internal error.
    /// </summary>
    public class InvalidStateException : IndyException
    {
        private const string message = "The SDK library experienced an unexpected internal error.";

        /// <summary>
        /// Initializes a new InvalidStateException.
        /// </summary>
        internal InvalidStateException() : base(message, (int)ErrorCode.CommonInvalidState)
        {
        }
    }

    /// <summary>
    /// Exception indicating that a value being processed was not considered a valid value.
    /// </summary>
    public class InvalidStructureException : IndyException
    {
        const string message = "A value being processed is not valid.";

        /// <summary>
        /// Initializes a new InvalidStructureException.
        /// </summary>
        internal InvalidStructureException() : base(message, (int)ErrorCode.CommonInvalidStructure)
        {

        }
    }

    /// <summary>
    /// Exception indicating that an IO error occurred.
    /// </summary>
    public class IOException : IndyException
    {
        const string message = "An IO error occurred.";

        /// <summary>
        /// Initializes a new IOException.
        /// </summary>
        internal IOException() : base(message, (int)ErrorCode.CommonIOError)
        {

        }
    }

}
