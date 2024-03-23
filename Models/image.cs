namespace AspCrudApp.Models
{
    public class image
    {
        public int Pid { get; set; }

        public string Pname { get; set; } = null!;

        public string Pdescription { get; set; } = null!;

        public decimal Pprice { get; set; }

        public int Pquantity { get; set; }

        public IFormFile Image { get; set; } = null!;
    }
}
