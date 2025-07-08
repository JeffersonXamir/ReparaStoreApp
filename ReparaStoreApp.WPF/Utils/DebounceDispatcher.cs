using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.WPF.Utils
{
    public class DebounceDispatcher
    {
        private CancellationTokenSource _cancellationTokenSource;
        private readonly object _lock = new object();

        public async Task Debounce(int milliseconds, Func<Task> action)
        {
            // Cancelar la operación anterior si existe
            CancelPendingOperation();

            try
            {
                _cancellationTokenSource = new CancellationTokenSource();
                var token = _cancellationTokenSource.Token;

                await Task.Delay(milliseconds, token);

                if (!token.IsCancellationRequested)
                {
                    await action();
                }
            }
            catch (TaskCanceledException)
            {
                // Ignorar cancelaciones
            }
        }

        public void CancelPendingOperation()
        {
            lock (_lock)
            {
                _cancellationTokenSource?.Cancel();
                _cancellationTokenSource?.Dispose();
                _cancellationTokenSource = null;
            }
        }
    }
}
