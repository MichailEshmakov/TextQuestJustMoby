using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextQuest
{
    public class Player
    {
        private readonly Health _health;
        private readonly Money _money;
        private readonly HashSet<string> _events;

        public IReadOnlyCollection<string> Events => _events;

        public event Action Dead;

        public Player()
        {
            _events = new HashSet<string>();
            _health = new Health();
            _money = new Money();
            _health.Dead += OnDead;
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

        public void Add(Health health)
        {
            _health.Add(health.Value);
        }

        public void Remove(Health health)
        {
            _health.Remove(health.Value);
        }

        public void Add(Money money)
        {
            _money.Add(money.Value);
        }

        public void Remove(Money money)
        {
            _money.Remove(money.Value);
        }

        private void OnDead()
        {
            _health.Dead -= OnDead;
            Dead?.Invoke();
        }
    }
}
