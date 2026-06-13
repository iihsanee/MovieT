namespace serviceLibary.Models
{
    public class WachtwoordResetModel
    {
        public int Id { get; }
        public int GebruikerId { get; }
        public string ResetToken { get; }
        public DateTime AangemaaktOp { get; }
        public bool Gebruikt { get; }

        public WachtwoordResetModel(int id, int gebruikerId, string resetToken,
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