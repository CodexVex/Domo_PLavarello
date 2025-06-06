using System;
using System.Windows.Forms;
using uPLibrary.Networking.M2Mqtt;

namespace Domo_PLavarello {
    public partial class Form1 : Form {
        static MqttClient cliente;
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            string mqtt_server = "broker.hivemq.com";
            string mqtt_port = "1883";
            string mqtt_user = null;
            string mqtt_pass = null;

            try {
                cliente = new MqttClient(mqtt_server, int.Parse(mqtt_port), false, MqttSslProtocols.None, null, null);
                cliente.ProtocolVersion = MqttProtocolVersion.Version_3_1;
                byte conexion = cliente.Connect("Chamuco", mqtt_user, mqtt_pass);
                if (conexion == 0) {
                    MessageBox.Show("MQTT Conectado");
                } else {
                    MessageBox.Show("MQTT Desconectado");
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
                //TODO: manejar la excepcion
            }
        }
    }
}
