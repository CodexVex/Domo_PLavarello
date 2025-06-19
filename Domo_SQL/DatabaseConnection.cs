using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace Domo_SQL {
    public class DatabaseConnection {
        string _connectionString = "Server=localhost;Database=domo;User Id=sa;Password=pepito123;TrustServerCertificate=true;";

        public async Task<IDbConnection> GetOpenConnectionAsync() {
            try {
                var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();
                return connection;
            } catch (Exception ex) {
                // Manejo de errores
                Console.WriteLine("Error al abrir la conexión: " + ex.Message);
                throw;
            }
        }

        public async Task<bool> ProbarConexionAsync() {
            try {
                using (var connection = await this.GetOpenConnectionAsync()) {
                    // Si la conexión está abierta, retorna true
                    return connection.State == ConnectionState.Open;
                }
            } catch (Exception ex) {
                // Puedes loguear el error si lo deseas
                Console.WriteLine("Error al probar la conexión: " + ex.Message);
                return false;
            }
        }
    }
}
