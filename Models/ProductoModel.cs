using System.ComponentModel.DataAnnotations;

namespace Valhalla_Music.Models
{
    public class ProductoModel
    {
        public ProductoModel()
        {
        }

        public Guid Id{ get; set; } 

        [StringLength(100), Required] 
        public string Name{ get; set; }


        [Required]
        public int Precio{ get; set; }

        public int Stock{ get; set; }

    }
}