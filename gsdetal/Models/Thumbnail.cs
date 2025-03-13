using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace gsdetal.Models
{
    public class Thumbnail
    {
        [Key]
        public string thumbnailurl { get; set; }
        public string? thumbnailpath { get; set; }
    }
}
