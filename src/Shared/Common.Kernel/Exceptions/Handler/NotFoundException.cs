using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Kernel.Exceptions.Handler
{
    public class NotFoundException : Exception
    {
        public string Value { get; } = default!;
        public object? Key { get; }

        public NotFoundException(string value, object? key) : base($"Объект '{value}' c ключом '{key}' не найден.")
        {
            Value = value; 
            Key = key;
        }
    }
}
