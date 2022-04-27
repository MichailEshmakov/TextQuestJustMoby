using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextQuest
{
    public class View
    {
        const string OptionMark = ") ";

        public View(Game game)
        {
            if (game == null)
                throw new ArgumentNullException(nameof(game));

            game.DialogueSet += OnDialogueSet;
        }

        private void OverGame(Game game)
        {
            game.DialogueSet -= OnDialogueSet;
            Environment.Exit(0);
        }

        private void OnDialogueSet(Game game, Dialogue dialogue)
        {
            ShowDialogue(dialogue);
            int input = GetInput(dialogue);

            if (game.IsOver)
                OverGame(game);

            Console.Clear();
            game.ChooseAction(input - 1);
        }

        private void ShowDialogue(Dialogue dialogue)
        {
            if (string.IsNullOrEmpty(dialogue.PlayerPhrase) == false)
                Console.WriteLine(dialogue.PlayerPhrase);

            if (string.IsNullOrEmpty(dialogue.Answer) == false)
                Console.WriteLine(dialogue.Answer);

            Console.WriteLine();
            IReadOnlyList<DialogueAction> actions = dialogue.Actions;
            for (int i = 0; i < actions.Count; i++)
            {
                Console.WriteLine($"{i + 1}{OptionMark}{actions[i].Name}");
            }
        }

        private int GetInput(Dialogue dialogue)
        {
            bool isCorrectInput = false;
            int numericInput = -1;

            while (isCorrectInput == false)
            {
                char input = Console.ReadKey(true).KeyChar;
                numericInput = Convert.ToInt32(char.GetNumericValue(input));
                isCorrectInput = numericInput >= 1 && dialogue.Actions.Count >= numericInput;
            }

            return numericInput;
        }
    }
}
