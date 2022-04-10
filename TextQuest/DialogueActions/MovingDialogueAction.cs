using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextQuest
{
    public class MovingDialogueAction : DialogueAction
    {
        private Room _room;
        private string _enterPhrase;

        public Room Room => _room;
        public string EnterPhrase => _enterPhrase;

        public MovingDialogueAction(Room room, string enterPrase = "", string name = "", DialogueAction next = null) : base(name, next)
        {
            if (room == null)
                throw new ArgumentNullException(nameof(room));

            _room = room;
            _enterPhrase = enterPrase;
        }

        protected override void PerformFeatures()
        {
            Move();
        }

        private void Move()
        {
            _room.Enter();
        }
    }
}
