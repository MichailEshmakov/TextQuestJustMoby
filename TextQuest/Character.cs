using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextQuest
{
    public class Character
    {
        private readonly string _name;
        private readonly string _optionMarker;
        private readonly List<Dialogue> _dialogues;

        public string Name => _name;
        private string OptionMarker => _optionMarker;

        public Character(string name, string optionMarker, List<Dialogue> dialogues)
        {
            _name = name;
            _optionMarker = optionMarker;
            _dialogues = dialogues;
        }

        public Dialogue Speak()
        {
            return _dialogues[0];
        }

        public DialogueAction GetOption()
        {
            return new DialogueDialogueAction(Speak(), _optionMarker);
        }
    }
}
