using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace web.Models
{
    public class Maluco
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public string UserId { get; set; }

        
        public string ImageUri { get; set; }

        public DateTime UltimaVisualizacao { get; set; }
        public int TotalVisualizacao { get; set; }


    }
}
