using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Domo_PLavarello {
    public partial class Form1 : Form {
        static MqttClient cliente;
        string mqtt_server = "broker.hivemq.com";
        int mqtt_port = 1883;
        string mqtt_user = null;
        string mqtt_pass = null;


        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            ConectarMQTT();
        }

        private void ConectarMQTT() {
            try {
                if (cliente != null && cliente.IsConnected) {
                    cliente.Disconnect();
                }

                lblEstado1.Text = "Conectando";
                lblEstado1.ForeColor = Color.Yellow;

                cliente = new MqttClient(mqtt_server, mqtt_port, false, MqttSslProtocols.None, null, null);
                cliente.ProtocolVersion = MqttProtocolVersion.Version_3_1;

                byte conexion = cliente.Connect("DomoDemonio", mqtt_user, mqtt_pass);

                if (conexion == 0) {
                    lblEstado1.Text = "Conectado";
                    lblEstado1.ForeColor = Color.Green;
                    // registramos el callback
                    cliente.MqttMsgPublishReceived += MQTT_mensajeRecivido;
                    // nos suscribimos a los topics
                    string[] topics = new string[1];
                    topics[0] = "domo/domodata";
                    byte[] msg = new byte[1];
                    msg[0] = MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE;

                    cliente.Subscribe(topics, msg);

                } else {
                    lblEstado1.Text = "Desconectado";
                    lblEstado1.ForeColor = Color.Red;
                }
            } catch (Exception ex) {
                lblEstado1.Text = "Error de conexión";
                lblEstado1.ForeColor = Color.OrangeRed;
                MessageBox.Show("Error al conectar: " + ex.Message);
            }
        }

        private void DesconectarMQTT() {
            if (cliente != null && cliente.IsConnected) {
                lblEstado1.Text = "Desconectado";
                lblEstado1.ForeColor = Color.Red;
                cliente.Disconnect();
                cliente = null;
            }
        }

        private void MQTT_mensajeRecivido(object sender, MqttMsgPublishEventArgs e) {
            string topic = e.Topic;
            string value = Encoding.UTF8.GetString(e.Message);
            //todo: manejar el paquete y guardar los datos
            string mensaje = "Topic: " + topic + " | Valor: " + value;
            MessageBox.Show(mensaje);
        }

        // cuando se cierra el formulario
        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            // desconectamos del servidor MQTT
            DesconectarMQTT();
        }
    }
}
