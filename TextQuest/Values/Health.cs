using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextQuest
{
    public class Health : IValue
    {
        private int _value;

        public int Value => _value;

        public event Action Dead;

        public Health(int value = 1)
        {
            _value = value;
        }

        public void Add(int adding)
        {
            _value += adding;
        }

        public void Remove(int removing)
        {
            _value -= removing;
            if (_value <= 0)
                Dead?.Invoke();
        }
    }
}
