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
        private readonly DialogueNode _dialogueNode;

        public string Name => _name;

        public Character(string name, string optionMarker, DialogueNode dialogueNode)
        {
            if (dialogueNode == null)
                throw new ArgumentNullException(nameof(dialogueNode));

            _name = name;
            _optionMarker = optionMarker;
            _dialogueNode = dialogueNode;
        }

        public Dialogue Speak(Player player)
        {
            return _dialogueNode.GetActualDialogue(player);
        }

        public DialogueAction GetOption(Player player)
        {
            return new DialogueDialogueAction(Speak(player), _optionMarker);
        }
    }
}
