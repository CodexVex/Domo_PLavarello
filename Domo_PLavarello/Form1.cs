using Domo_Entidad;
using Domo_SQL;
using System;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Domo_PLavarello {
    public partial class Form1 : Form {
        DatabaseConnection conexionSQL = new DatabaseConnection();
        static MqttClient cliente;
        string mqtt_server = "broker.hivemq.com";
        int mqtt_port = 1883;
        string mqtt_user = null;
        string mqtt_pass = null;

        Color colorTextos = ColorTranslator.FromHtml("#2A2523");
        Color colorAmarillo = ColorTranslator.FromHtml("#FD9A00");
        Color colorVerde = ColorTranslator.FromHtml("#7DCF00");
        Color colorRojo = ColorTranslator.FromHtml("#E70808");


        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            // aplicamos estilos
            this.BackColor = ColorTranslator.FromHtml("#78706B");
            lblTitulo.ForeColor = colorTextos;
            lblMQTT.ForeColor = colorTextos;
            lblMSSQL.ForeColor = colorTextos;
            // conetamos servicios
            ConectarMQTT();
            ProbarSQL();
        }

        private void ConectarMQTT() {
            try {
                if (cliente != null && cliente.IsConnected) {
                    cliente.Disconnect();
                }

                lblEstado1.Text = "Conectando...";
                lblEstado1.ForeColor = colorAmarillo;

                cliente = new MqttClient(mqtt_server, mqtt_port, false, MqttSslProtocols.None, null, null);
                cliente.ProtocolVersion = MqttProtocolVersion.Version_3_1;

                byte conexion = cliente.Connect("DomoDemonio", mqtt_user, mqtt_pass);

                if (conexion == 0) {
                    lblEstado1.Text = "Conectado";
                    lblEstado1.ForeColor = colorVerde;
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
                    lblEstado1.ForeColor = colorRojo;
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
                lblEstado1.ForeColor = colorRojo;
                cliente.Disconnect();
                cliente = null;
            }
        }

        private async Task ProbarSQL() {
            lblEstado2.Text = "Conectando...";
            lblEstado2.ForeColor = colorAmarillo;
            // probamos la conexion
            bool conectado = await conexionSQL.ProbarConexionAsync();
            if(!conectado) {
                lblEstado2.Text = "Desconectado";
                lblEstado2.ForeColor = colorRojo;
            } else {
                lblEstado2.Text = "Conectado";
                lblEstado2.ForeColor = colorVerde;
            }
        }

        private void MQTT_mensajeRecivido(object sender, MqttMsgPublishEventArgs e) {
            string topic = e.Topic;
            string value = Encoding.UTF8.GetString(e.Message);
            // vemos si es una trama DOMO
            var trama = new TramaDOMO(value);
            if (trama.esValida()) {
                DatosDOMO datos = trama.obtenerDatos();
                // guardamos en la DB
                DatoSensorSQL sensorSQL = new DatoSensorSQL();
                sensorSQL.insertarDatos(datos);
            } else {
                string mensaje = "Topic: " + topic + " | Valor: " + value;
                MessageBox.Show(mensaje);
            }
               
        }

        // cuando se cierra el formulario
        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            // desconectamos del servidor MQTT
            DesconectarMQTT();
        }
    }
}
