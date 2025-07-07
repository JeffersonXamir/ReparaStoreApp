using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Common
{
    public class TokenExpiredMessage
    {
        public string Message { get; } = "La sesión ha expirado. Por favor, inicie sesión nuevamente.";
    }

    public class ErrorMessage
    {
        public string Message { get; }

        public ErrorMessage(string message)
        {
            Message = message;
        }
    }

    public class SuccessMessage
    {
        public string Message { get; }

        public SuccessMessage(string message)
        {
            Message = message;
        }
    }

    public class ItemSelectedMessage<T>
    {
        public T SelectedItem { get; }

        public ItemSelectedMessage(T selectedItem)
        {
            SelectedItem = selectedItem;
        }
    }
}
