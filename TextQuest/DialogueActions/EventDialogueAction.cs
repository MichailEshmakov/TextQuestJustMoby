using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextQuest
{
    public class EventDialogueAction : DialogueAction
    {
        private HashSet<string> _events;

        public IReadOnlyCollection<string> Events => _events;

        public EventDialogueAction(HashSet<string> events, string name = "", DialogueAction next = null) : base(name, next)
        {
            if (events == null)
                throw new ArgumentNullException(nameof(events));

            _events = events;
        }
    }
}
