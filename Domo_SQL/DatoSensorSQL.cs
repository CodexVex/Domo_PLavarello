using Domo_Entidad;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Domo_SQL {
    public class DatoSensorSQL {
        DatabaseConnection _dbConnection = new DatabaseConnection();

        public async Task<List<DatosDOMO>> obtenerRegistros() {
            var datos = new List<DatosDOMO>();

            var query = @"
                SELECT [id_data_sensor]
                  ,[id_domo]
                  ,[temperatura]
                  ,[humedad_tierra]
                  ,[humedad_ambiente]
                  ,[ph]
                  ,[estado_ventilador]
                  ,[estado_ventana]
                  ,[fecha]
              FROM [domo].[dbo].[Data_Sensores]            
            ";

            using (var connection = await _dbConnection.GetOpenConnectionAsync())
            using (var command = new SqlCommand(query, (SqlConnection)connection)) {
                command.CommandType = CommandType.Text;

                using (var reader = await command.ExecuteReaderAsync()) {
                    while (await reader.ReadAsync()) {
                        datos.Add(new DatosDOMO {
                            humedad_ambiente = reader.GetInt32(reader.GetOrdinal("humedad_ambiente")),
                            //ph = reader.GetString(reader.GetOrdinal("ph"))
                        });
                    }
                }
            }

            return datos;
        }


        public async Task<int> insertarDatos(DatosDOMO datos) {
            var query = @"
			INSERT INTO Data_Sensores (
				id_domo,
				temperatura,
				humedad_tierra,
				humedad_ambiente,
				estado_ventilador,
				estado_ventana,
				fecha
			)
			OUTPUT INSERTED.id_data_sensor
			VALUES (
				@id_domo,
				@temperatura,
				@humedad_tierra,
				@humedad_ambiente,
				@estado_ventilador,
				@estado_ventana,
				@fecha
			);";

            using (var connection = await _dbConnection.GetOpenConnectionAsync())
            using (var command = new SqlCommand(query, (SqlConnection)connection)) {
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@id_domo", 1);
                command.Parameters.AddWithValue("@temperatura", datos.temperatura);
                command.Parameters.AddWithValue("@humedad_tierra", datos.humedad_tierra);
                command.Parameters.AddWithValue("@humedad_ambiente", datos.humedad_ambiente);
                command.Parameters.AddWithValue("@estado_ventilador", datos.estado_ventilador);
                command.Parameters.AddWithValue("@estado_ventana", datos.estado_ventana);
                command.Parameters.AddWithValue("@fecha", DateTime.Now);

                try {
                    var result = await command.ExecuteScalarAsync();
                    return Convert.ToInt32(result);
                } catch (SqlException ex) {
                    Console.WriteLine("❌ Error SQL:");
                    Console.WriteLine(ex.Message);       // Mensaje del error
                    Console.WriteLine(ex.Number);       // Código de error SQL
                    Console.WriteLine(ex.StackTrace);   // Traza
                    throw; // Re-lanza si deseas seguir escalando
                }
            }
        }

    }

}
