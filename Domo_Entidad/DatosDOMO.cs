using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domo_Entidad {
    public class DatosDOMO {
        public int id_domo { get; set; }
        public float temperatura { get; set; }
        public int humedad_tierra { get; set; }
        public float humedad_ambiente { get; set; }
        public float ph { get; set; }
        public bool estado_ventana { get; set; }
        public bool estado_ventilador { get; set; }

        public int posicion_servo { get; set; }
    }
}
