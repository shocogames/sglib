using System.Threading;
using Cysharp.Threading.Tasks;
using SGLib.Utility.Patterns.EventChannels.Primitive;

namespace SGLib.Utility.Patterns.EventChannels
{
    public static class EventChannelExtensions
    {
        public static UniTask WaitAsync(this VoidChannel_So channel, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return UniTask.FromCanceled(cancellationToken);
            }

            var tcs = new UniTaskCompletionSource();
            CancellationTokenRegistration registration = default;

            void Handler()
            {
                // clean up
                channel.Unsubscribe(Handler);
                registration.Dispose();

                tcs.TrySetResult();
            }

            void Callback()
            {
                // clean up
                channel.Unsubscribe(Handler);
                registration.Dispose();

                tcs.TrySetCanceled(cancellationToken);
            }

            channel.Subscribe(Handler);

            if (cancellationToken.CanBeCanceled)
            {
                registration = cancellationToken.Register(Callback);
            }

            return tcs.Task;
        }

        public static UniTask<T> WaitAsync<T>(this EventChannelBaseT1_So<T> channel, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return UniTask.FromCanceled<T>(cancellationToken);
            }

            var tcs = new UniTaskCompletionSource<T>();
            CancellationTokenRegistration registration = default;

            void Handler(T value)
            {
                channel.Unsubscribe(Handler);
                registration.Dispose();

                tcs.TrySetResult(value);
            }

            void Callback()
            {
                channel.Unsubscribe(Handler);
                registration.Dispose();

                tcs.TrySetCanceled(cancellationToken);
            }

            channel.Subscribe(Handler);

            if (cancellationToken.CanBeCanceled)
            {
                registration = cancellationToken.Register(Callback);
            }

            return tcs.Task;
        }

        public static UniTask<(T1, T2)> WaitAsync<T1, T2>(this EventChannelBaseT2_So<T1, T2> channel, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return UniTask.FromCanceled<(T1, T2)>(cancellationToken);
            }

            var tcs = new UniTaskCompletionSource<(T1, T2)>();
            CancellationTokenRegistration registration = default;

            void Handler(T1 value1, T2 value2)
            {
                channel.Unsubscribe(Handler);
                registration.Dispose();

                tcs.TrySetResult((value1, value2));
            }

            void Callback()
            {
                channel.Unsubscribe(Handler);
                registration.Dispose();

                tcs.TrySetCanceled(cancellationToken);
            }

            channel.Subscribe(Handler);

            if (cancellationToken.CanBeCanceled)
            {
                registration = cancellationToken.Register(Callback);
            }

            return tcs.Task;
        }
    }
}