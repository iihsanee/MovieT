namespace MovieT.Models
{
     public class Genre
        {
        public int ID { get; set; }   //
        public string Lijst { get; set; } = string.Empty;
         public string Naam { get; set; } = string.Empty;

         public string Getnaam()
            {
                return Naam;
            }
            
            }
        }
   