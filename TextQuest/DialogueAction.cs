using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextQuest
{
    public abstract class DialogueAction
    {
        private string _name;
        private DialogueAction _next;

        public string Name => _name;
        public DialogueAction Next => _next;

        public event Action<DialogueAction> Doing;

        public DialogueAction(string name = "", DialogueAction next = null)
        {
            _name = name;
            _next = next;
        }

        public void Do()
        {
            Doing?.Invoke(this);
            if (_next != null)
                _next.Do();
        }
    }
}
