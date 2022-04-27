using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextQuest
{
    public class Money : IValue
    {
        private int _value;

        public int Value => _value;

        public Money(int value = 0)
        {
            _value = value;
        }

        public void Add(int adding)
        {
            _value += adding;
        }

        public void Remove(int removing)
        {
            if (removing <= Value)
                throw new ArgumentOutOfRangeException(nameof(removing));

            _value -= removing;
        }
    }
}
