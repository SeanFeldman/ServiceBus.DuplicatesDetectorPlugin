namespace ServiceBus.DuplicatesDetectorPlugin
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Azure.ServiceBus;

    /// <summary>
    /// Duplicate message detector API.
    /// </summary>
    public interface IDetectDuplicates : IDisposable
    {
        /// <summary>
        /// Duplicate messages detector API.
        /// </summary>
        /// <param name="message">Incoming <see cref="Message"/>.</param>
        /// <returns>Incoming <see cref="Message"/> with additional user-property to indicate if message is a duplicate or not</returns>
        Task<Message> Detect(Message message);
    }
}