using System.ComponentModel.DataAnnotations;

namespace IntegrationTestsSample.Models
{
    public class Product
    {
        [Key]
        public string Code { get; set; }

        public string Name { get; set; }
    }
}
