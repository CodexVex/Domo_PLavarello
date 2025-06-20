using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domo_Entidad {
    public class DatosDOMO {
        public float humedadAmbiente { get; set; }
        public float temperatura { get; set; }
        public int humedadTierra { get; set; }
        public bool ventanaAbierta { get; set; }
        public bool ventiladorEncendido { get; set; }
        public int posicionServo { get; set; }
    }
}
