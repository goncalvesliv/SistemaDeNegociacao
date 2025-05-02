using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using OrderCommonModels.Models;

namespace OrderProcessor.Services
{
    public class NegocioRepository
    {
        private readonly string _connectionString;

        public NegocioRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task SalvarNegocioAsync(Negocio negocio)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var query = @"INSERT INTO Negocios (NomeAtivo, Preco, Quantidade)
                          VALUES (@NomeAtivo, @Preco, @Quantidade)";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NomeAtivo", negocio.NomeAtivo);
            command.Parameters.AddWithValue("@Preco", negocio.Preco);
            command.Parameters.AddWithValue("@Quantidade", negocio.Quantidade);

            await command.ExecuteNonQueryAsync();
        }

        public async Task SalvarNegociosAsync(IEnumerable<Negocio> negocios)
        {
            foreach (var negocio in negocios)
            {
                await SalvarNegocioAsync(negocio);
            }
        }

        public async Task SalvarOrdemAsync(OrdemProcessada ordem)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var query = @"INSERT INTO OrdemProcessada (TipoOrdem, NomeAtivo, Preco, Quantidade, Status)
                  VALUES (@TipoOrdem, @NomeAtivo, @Preco, @Quantidade, @Status)";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TipoOrdem", ordem.TipoOrdem);
            command.Parameters.AddWithValue("@NomeAtivo", ordem.NomeAtivo);
            command.Parameters.AddWithValue("@Preco", ordem.Preco);
            command.Parameters.AddWithValue("@Quantidade", ordem.Quantidade);
            command.Parameters.AddWithValue("@Status", ordem.Status);

            await command.ExecuteNonQueryAsync();
        }

    }
}
