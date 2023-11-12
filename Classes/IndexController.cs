namespace INF01120.Classes
{
    public class IndexController
    {
        public string HoverText { get; set; }
        public string? InputText { get; set; }
        public bool ShowInstructions { get; set; }
        public IndexController(string IndexHoverText, bool IndexShowInstructions)
        {
            HoverText = IndexHoverText;
            ShowInstructions = IndexShowInstructions;
        }

        public void ControlInstructions()
        {
            HoverText = HoverText == "Mostrar instruções." ? "Ocultar instruções." : "Mostrar instruções.";
            ShowInstructions = ShowInstructions == true ? false : true;
        }

        public bool ControlMusicGenerator()
        {
            return InputText == null || InputText.Length == 0 ? true : false;
        }

    }
}