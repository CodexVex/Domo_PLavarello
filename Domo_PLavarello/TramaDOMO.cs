using Domo_Entidad;
using System;

namespace Domo_PLavarello {

    public class TramaDOMO {
        private readonly string tramaOriginal;
        private string cuerpo;
        private string checksumHex;

        public TramaDOMO(string trama) {
            this.tramaOriginal = trama;
        }

        public bool esValida() {
            Console.WriteLine("🔍 Verificando trama: " + tramaOriginal);

            if (string.IsNullOrWhiteSpace(tramaOriginal)) {
                Console.WriteLine("❌ Trama vacía o nula.");
                return false;
            }

            if (!tramaOriginal.StartsWith("$") || !tramaOriginal.Contains("*")) {
                Console.WriteLine("❌ Formato incorrecto: falta '$' o '*'.");
                return false;
            }

            int idxAsterisco = tramaOriginal.IndexOf('*');
            if (idxAsterisco < 0 || idxAsterisco + 2 >= tramaOriginal.Length) {
                Console.WriteLine("❌ Posición del '*' inválida.");
                return false;
            }

            cuerpo = tramaOriginal.Substring(1, idxAsterisco - 1);
            checksumHex = tramaOriginal.Substring(idxAsterisco + 1);

            Console.WriteLine($"📦 Cuerpo extraído: {cuerpo}");
            Console.WriteLine($"📦 Checksum recibido: {checksumHex}");

            if (checksumHex.Length != 2) {
                Console.WriteLine("❌ Longitud del checksum inválida.");
                return false;
            }

            byte checksumCalc = 0;
            foreach (char c in cuerpo) {
                checksumCalc ^= (byte)c;
            }

            Console.WriteLine($"🔢 Checksum calculado: {checksumCalc:X2}");

            try {
                byte checksumRecibido = Convert.ToByte(checksumHex, 16);
                bool ok = checksumCalc == checksumRecibido;
                Console.WriteLine(ok ? "✅ Checksum válido." : "❌ Checksum inválido.");
                return ok;
            } catch {
                Console.WriteLine("❌ Error al convertir el checksum recibido.");
                return false;
            }
        }

        public DatosDOMO obtenerDatos() {
            if (!this.esValida()) return null;

            string[] partes = cuerpo.Split(',');
            if (partes.Length != 7 || partes[0] != "DOMO") {
                Console.WriteLine("❌ Identificador incorrecto o número de campos inválido.");
                return null;
            }

            try {
                DatosDOMO datos = new DatosDOMO {
                    humedadAmbiente = float.Parse(partes[1]),
                    temperatura = float.Parse(partes[2]),
                    humedadTierra = int.Parse(partes[3]),
                    ventanaAbierta = partes[4] == "1",
                    ventiladorEncendido = partes[5] == "1",
                    posicionServo = int.Parse(partes[6])
                };

                Console.WriteLine("📤 Datos extraídos correctamente:");
                Console.WriteLine($" - HumedadAmbiente: {datos.humedadAmbiente}");
                Console.WriteLine($" - Temperatura: {datos.temperatura}");
                Console.WriteLine($" - HumedadTierra: {datos.humedadTierra}");
                Console.WriteLine($" - VentanaAbierta: {datos.ventanaAbierta}");
                Console.WriteLine($" - VentiladorEncendido: {datos.ventiladorEncendido}");
                Console.WriteLine($" - PosicionServo: {datos.posicionServo}");

                return datos;
            } catch {
                Console.WriteLine("❌ Error al convertir campos de la trama.");
                return null;
            }
        }
    }
}
