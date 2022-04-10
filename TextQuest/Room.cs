using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextQuest
{
    public class Room
    {
        private const string RoomPhrase = "Комната: ";
        private const string CharacterPhrase = "Персонажи: ";
        private const string BetweenCharacters = ", ";
        private const string NobodyPhrase = "Никого нет";

        private readonly string _name;
        private readonly List<Dialogue> _dialogues;
        private readonly List<Character> _characters;

        public Room(string name, List<Dialogue> dialogues = null, List<Character> characters = null)
        {
            if (dialogues == null)
                dialogues = new List<Dialogue>();

            if (characters == null)
                characters = new List<Character>();

            _name = name;
            _dialogues = dialogues;
            _characters = characters;
            GenerateEnterAnswer();
        }

        public Dialogue Enter(string enterPhrase = "")
        {
            string enterAnswer = GenerateEnterAnswer();
            List<DialogueAction> actions = GenerateActionsFromDialogues();
            return new Dialogue(enterPhrase, enterAnswer, actions);
        }

        private string GenerateEnterAnswer()
        {
            string characterList = string.Empty;
            int i = 0;
            foreach (Character character in _characters)
            {
                characterList += character.Name;
                if (i < _characters.Count - 1)
                    characterList += BetweenCharacters;

                i++;
            }

            if (string.IsNullOrEmpty(characterList))
                characterList += NobodyPhrase;

            return $"{RoomPhrase}{_name}\n{CharacterPhrase}{characterList}";
        }

        private List<DialogueAction> GenerateActionsFromDialogues()
        {
            List<DialogueAction> dialogueActions = new List<DialogueAction>();
            foreach (Dialogue dialogue in _dialogues)
            {
                dialogueActions.Add(new DialogueDialogueAction(name: dialogue.PlayerPhrase, dialogue: dialogue));
            }

            return dialogueActions;
        }
    }
}
