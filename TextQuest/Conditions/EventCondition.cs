using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextQuest
{
    public class EventCondition : Condition
    {
        private HashSet<string> _neededEvents;

        public EventCondition(HashSet<string> neededEvents)
        {
            if (neededEvents == null)
                throw new ArgumentNullException(nameof(neededEvents));

            if (neededEvents.Count == 0)
                throw new ArgumentException(nameof(neededEvents));

            _neededEvents = neededEvents;
        }

        public EventCondition(string neededEvent)
        {
            _neededEvents = new HashSet<string> { neededEvent };
        }

        public override bool IsMet(Player player)
        {
            return _neededEvents.All(neededEvent => player.Events.Contains(neededEvent));
        }
    }
}
