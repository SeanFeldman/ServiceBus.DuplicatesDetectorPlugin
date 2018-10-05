namespace ServiceBus.DuplicatesDetectorPlugin
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Azure.ServiceBus;

    class DefaultDuplicatesDetector : IDetectDuplicates
    {
        readonly string propertyName;
        int disposeSignaled;
        bool disposed;

        public DefaultDuplicatesDetector(string propertyName)
        {
            this.propertyName = propertyName;
        }

        public Task<Message> Detect(Message message)
        {
            ThrowIfDisposed();

            // TODO: do it with Span<T>?

            // Calculate a has of message.Body and see if it's cache.
            // Should cache be evicted at some point?

            message.UserProperties[propertyName] = false.ToString();
            return Task.FromResult(message);
        }

        void ThrowIfDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException($"{nameof(DuplicateDetector)} has been already disposed.");
            }
        }

        public void Dispose()
        {
            if (Interlocked.Exchange(ref disposeSignaled, 1) != 0)
            {
                return;
            }

            //timer?.Dispose();

            disposed = true;
        }
    }
}