using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextQuest
{
    public interface IValue
    {
        int Value { get; }

        void Add(int adding);
        void Remove(int removing);

    }
}
