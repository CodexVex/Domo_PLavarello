using Domo_Entidad;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domo_SQL {
    public class UsuarioSQL {
        DatabaseConnection _dbConnection = new DatabaseConnection();

        public async Task<List<UsuarioEntidad>> ObtenerUsuariosAsync() {
            var clientes = new List<UsuarioEntidad>();

            using (var connection = await _dbConnection.GetOpenConnectionAsync())
            using (var command = new SqlCommand("SELECT Id, Nombre FROM Clientes", (SqlConnection)connection)) {
                command.CommandType = CommandType.Text;

                using (var reader = await command.ExecuteReaderAsync()) {
                    while (await reader.ReadAsync()) {
                        clientes.Add(new UsuarioEntidad {
                            idUsuario = reader.GetInt32(reader.GetOrdinal("Id")),
                            nombre = reader.GetString(reader.GetOrdinal("Nombre"))
                        });
                    }
                }
            }

            return clientes;
        }

        public async Task<int> InsertarClienteAsync(string nombre) {
            var query = @"
            INSERT INTO Clientes (Nombre)
            OUTPUT INSERTED.idUsuario
            VALUES (@Nombre);";

            using (var connection = await _dbConnection.GetOpenConnectionAsync())
            using (var command = new SqlCommand(query, (SqlConnection)connection)) {
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@Nombre", nombre);

                var result = await command.ExecuteScalarAsync();
                return Convert.ToInt32(result);
            }
        }

        public async Task<UsuarioEntidad> ObtenerClientePorIdAsync(int id) {
            var query = "SELECT Id, Nombre FROM Clientes WHERE Id = @Id";

            using (var connection = await _dbConnection.GetOpenConnectionAsync())
            using (var command = new SqlCommand(query, (SqlConnection)connection)) {
                command.Parameters.AddWithValue("@Id", id);

                using (var reader = await command.ExecuteReaderAsync()) {
                    if (await reader.ReadAsync()) {
                        return new UsuarioEntidad {
                            idUsuario = reader.GetInt32(reader.GetOrdinal("Id")),
                            nombre = reader.GetString(reader.GetOrdinal("Nombre"))
                        };
                    }
                }
            }

            return null; // No se encontró el cliente
        }

        public async Task<bool> EliminarClienteAsync(int id) {
            var query = "DELETE FROM Clientes WHERE Id = @Id";

            using (var connection = await _dbConnection.GetOpenConnectionAsync())
            using (var command = new SqlCommand(query, (SqlConnection)connection)) {
                command.Parameters.AddWithValue("@Id", id);

                int filasAfectadas = await command.ExecuteNonQueryAsync();
                return filasAfectadas > 0;
            }
        }

        public async Task<bool> ActualizarClienteAsync(UsuarioEntidad cliente) {
            var query = @"
        UPDATE Clientes
        SET Nombre = @Nombre, Email = @Email
        WHERE Id = @Id";

            using (var connection = await _dbConnection.GetOpenConnectionAsync())
            using (var command = new SqlCommand(query, (SqlConnection)connection)) {
                //command.Parameters.AddWithValue("@Id", cliente.Id);
                //command.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                //command.Parameters.AddWithValue("@Email", cliente.Email);

                int filasAfectadas = await command.ExecuteNonQueryAsync();
                return filasAfectadas > 0;
            }
        }
    }
}
