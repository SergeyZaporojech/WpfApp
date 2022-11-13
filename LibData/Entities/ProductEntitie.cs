using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibData.Entities
{

    [Table("tblProducts")]
    public class ProductEntitie
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(150)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        [StringLength(200)]
        public string Image { get; set; }
        public decimal Prise { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
    }
}

