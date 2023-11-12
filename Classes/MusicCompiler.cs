using INF01120.Classes;

namespace INF01120.Classes
{
    public class MusicCompiler
    {
        public List<string> OutputNotes { get; set; } = new();
        public List<string> Actions { get; set; }
        public MusicCompiler(List<string> Actions)
        {
            this.Actions = Actions;
        }
        public List<string> CompileMusic()
        {
            List<string> character_notes = new() { "A", "B", "C", "D", "E", "F", "G" };

            foreach (string Action in Actions)
            {
                // If character is musical note, put the index of the note number in Output
                if (character_notes.Contains(Action))
                {
                    switch (Action)
                    {
                        case "A": // La
                            OutputNotes.Add("0");       
                            break;

                        case "B": // Si
                            OutputNotes.Add("1");
                            break;

                        case "C": // Do
                            OutputNotes.Add("2");
                            break;

                        case "D": // Re
                            OutputNotes.Add("3");
                            break;

                        case "E": // Mi
                            OutputNotes.Add("4");
                            break;

                        case "F": // Fa
                            OutputNotes.Add("5");
                            break;

                        case "G": // Sol
                            OutputNotes.Add("6");
                            break;
                    }
                }
                else
                {
                    OutputNotes.Add(Action);

                }
            }
            return OutputNotes;
        }
    }
}