using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextQuest
{
    public abstract class ValueDialogueAction : DialogueAction
    {
        private IValue _value;

        public IValue Value => _value;

        public ValueDialogueAction(IValue value, string name = "", DialogueAction next = null) : base(name, next)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (value.Value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            _value = value;
        }
    }
}
