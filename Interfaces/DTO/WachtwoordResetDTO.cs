namespace DAL.DTO
{
    public class WachtwoordResetDTO
    {
        public int Id { get; }
        public int GebruikerId { get; }
        public string ResetToken { get; } = string.Empty;
        public DateTime AangemaaktOp { get; }
        public bool Gebruikt { get; }

        public WachtwoordResetDTO(int id, int gebruikerId, string resetToken,
                                   DateTime aangemaaktOp, bool gebruikt)
        {
            Id = id;
            GebruikerId = gebruikerId;
            ResetToken = resetToken;
            AangemaaktOp = aangemaaktOp;
            Gebruikt = gebruikt;
        }
    }
}