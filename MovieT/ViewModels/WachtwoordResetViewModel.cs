namespace MovieT.ViewModels
{
    public class WachtwoordResetViewModel
    {
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string NieuwWachtwoord { get; set; } = string.Empty;
        public string BevestigWachtwoord { get; set; } = string.Empty;
        public string? Fout { get; set; }
        public string? Succes { get; set; }
    }
}