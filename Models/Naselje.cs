namespace DrzavaNaselje.Models
{
    public class Naselje
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string PostanskiBroj { get; set; }
        public int DrzavaId { get; set; }
        public virtual Drzava Drzava { get; set; }
    }
}