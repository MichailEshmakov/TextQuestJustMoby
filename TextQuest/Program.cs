using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextQuest
{
    class Program
    {
        const string OptionMark = ") ";

        static void Main(string[] args)
        {
            Dialogue lookingAround = new Dialogue("Осмотреть окрестности", "В таверне чисто и тепло");
            Room tavern = new Room("Tavern", new List<Dialogue> { lookingAround });
            Game game = new Game();

            game.DialogueSet += OnDialogueSet; // TODO: unsubscribe in gameover

            game.SetRoom(tavern);
        }

        private static void OnDialogueSet(Game game, Dialogue dialogue)
        {
            ShowDialogue(dialogue);
            int input = GetInput(dialogue);
            Console.Clear();
            game.ChooseAction(input - 1);
        }

        static void ShowDialogue(Dialogue dialogue)
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

        static int GetInput(Dialogue dialogue)
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
