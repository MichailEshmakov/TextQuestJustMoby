using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextQuest
{
    public class Player
    {
        private IValue _health;
        private IValue _money;
        private readonly HashSet<string> _events;

        public IReadOnlyCollection<string> Events => _events;

        public Player()
        {
            _events = new HashSet<string>();
        }

        public void AddEvent(string newEvent)
        {
            _events.Add(newEvent);
        }

        public void AddEvents(IReadOnlyCollection<string> events)
        {
            foreach (string newEvent in events)
            {
                AddEvent(newEvent);
            }
        }
    }
}
