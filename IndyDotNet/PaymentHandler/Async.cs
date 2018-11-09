using System;
using System.Threading.Tasks;
using IndyDotNet.Utils;

namespace IndyDotNet.PaymentHandler
{
    internal static class PaymentHandlerAsync
    {
        /// <summary>
        /// Register custom payment implementation.
        ///
        /// </summary>
        /// <returns>The payment method async.</returns>
        /// <param name="implementation">Payment method.</param>
        internal static Task RegisterPaymentMethodAsync(ISDKPaymentFacade implementation)
        {
            ParamGuard.NotNull(implementation, "implementation");

            var taskCompletionSource = new TaskCompletionSource<bool>();
            var commandHandle = PendingCommands.Add(taskCompletionSource);

            var result = NativeMethods.indy_register_payment_method(
                commandHandle,
                implementation.PaymentMethod,
                implementation.CreatePaymentAddressCallback,
                implementation.AddRequestFeesCallback,
                implementation.ParseResponseWithFeesCallback,
                implementation.BuildGetPaymentSourcesRequstCallback,
                implementation.ParseGetPaymentSourcesResponseCallback,
                implementation.BuildPaymentRequestCallback,
                implementation.ParsePaymentResponseCallback,
                implementation.BuildMintReqCallback,
                implementation.BuildSetTxnFeesReqCallback,
                implementation.BuildGetTxnFeesReqCallback,
                implementation.ParseGetTxnFeesResponseCallback,
                implementation.BuildVerifyPaymentRequestCallback,
                implementation.ParseVerifyPaymentResponseCallback,
                CallbackHelper.TaskCompletingNoValueCallback);

            CallbackHelper.CheckResult(result);

            return taskCompletionSource.Task;
        }
    }
}
