namespace INF01120.Classes
{
    public class TextCompiler
    {
        public string InputText { get; set; }
        public List<string> Actions { get; set; } = new();
        public TextCompiler(string BindedInputText)
        {
            InputText = BindedInputText;
        }

        public void Compile()
        {
            string symbol;
            List<string> CharacterNotesUpperCase = new() { "A", "B", "C", "D", "E", "F", "G" };
            List<string> CharacterNotesLowerCase = new() { "a", "b", "c", "d", "e", "f", "g" };
            List<string> OtherVowels = new() { "O", "o", "I", "i", "U", "u" };
            const string AGOGO = "114";
            const string HAPSICHORD = "7";
            const string TUBULAR_BELLS = "15";
            const string PAN_FLUTE = "76";
            const string CHURCH_ORGAN = "20";

            for (int index = 0; index < InputText.Length; index++)
            {
                // Case that the input is a NL
                if (index < InputText.Length - 1)
                {
                    if (InputText.Substring(index, 2) == "NL")
                    {
                        Actions.Add(TUBULAR_BELLS);
                        index++;
                        continue;
                    }
                }

                symbol = InputText[index].ToString();

                // Upper Case
                if (CharacterNotesUpperCase.Contains(symbol))
                {
                    Actions.Add(symbol);
                }
                // Lower Case
                else if (CharacterNotesLowerCase.Contains(symbol))
                {
                    CheckPreviousCharacter(index, CharacterNotesUpperCase);
                }
                // Other Vowels (O/o,U/u,I/i)
                else if (OtherVowels.Contains(symbol))
                {
                    // Play with Harpsichord
                    Actions.Add(HAPSICHORD);
                }
                // Any Consonant
                else if (Char.IsLetter(char.Parse(symbol)))
                {
                    CheckPreviousCharacter(index, CharacterNotesUpperCase);
                }
                // Number even or odd
                else if (Char.IsDigit(char.Parse(symbol)))
                {
                    // Add in the instrument number this symbol
                    Actions.Add(symbol + "+");
                }
                // Other Symbols
                else
                {
                    switch (symbol)
                    {
                        case ",":
                            Actions.Add(CHURCH_ORGAN);
                            break;
                        case ";":
                            Actions.Add(PAN_FLUTE);
                            break;
                        case "!":
                            Actions.Add(AGOGO);
                            break;
                        case "?":
                        case ".":
                            Actions.Add("Raise Octave");
                            break;
                        case " ":
                            Actions.Add("Double Volume");
                            break;
                        default:
                            CheckPreviousCharacter(index, CharacterNotesUpperCase);
                            break;
                    }
                }
            }
        }

        public void CheckPreviousCharacter(int index, List<string> CharacterNotesUpperCase)
        {
            if (index != 0 && CharacterNotesUpperCase.Contains(InputText[index - 1].ToString()))
            {
                Actions.Add(InputText[index - 1].ToString());
            }
            else
            {
                Actions.Add("Pause");
            }
        }
    }
}