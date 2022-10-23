using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibData.Entities
{
    [Table("tblUserrs")]
    public class UserEntitie
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(150)]
        public string Name { get; set; }
        [ StringLength(20)]
        public string Phone { get; set; }
        [ StringLength(200)]
        public string Password { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUbdate { get; set; }

    }
}
