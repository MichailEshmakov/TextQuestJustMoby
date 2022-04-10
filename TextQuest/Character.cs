using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextQuest
{
    public class Character
    {
        private string _name;
        private string _description;
        private HashSet<Dialogue> _dialogues;

        public string Name => _name;
    }
}
