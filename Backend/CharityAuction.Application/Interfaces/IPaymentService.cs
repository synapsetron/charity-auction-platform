using CharityAuction.Application.DTO.Payment;

namespace CharityAuction.Payment.Interfaces
{
    /// <summary>
    /// Defines operations for creating and managing payments via external payment providers.
    /// </summary>
    public interface IPaymentService
    {
        /// <summary>
        /// Initiates a new payment using the underlying payment provider.
        /// </summary>
        /// <param name="request">An object containing payment details including user, auction, amount, and description.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. 
        /// The task result contains a URL string that redirects the user to the payment page of the provider.
        /// </returns>
        /// <exception cref="PaymentException">Thrown when the payment request fails due to provider or network errors.</exception>
        Task<string> CreatePaymentAsync(PaymentRequestDTO request);

        /// <summary>
        /// Retrieves the current status of a specific payment from the provider.
        /// </summary>
        /// <param name="paymentId">The unique identifier of the payment (usually matches the auction ID or provider-specific order ID).</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the payment status and related metadata.
        /// </returns>
        /// <exception cref="PaymentException">Thrown when the status retrieval fails due to invalid ID or communication issues with the provider.</exception>
        Task<PaymentStatusResponseDTO> GetPaymentStatusAsync(string paymentId);


        /// <summary>
        /// Handles the webhook notification from the payment provider.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        Task HandleWebhookAsync(string data, string signature);
    }
}
