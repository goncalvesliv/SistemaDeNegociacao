using OrderCommonModels.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace OrdemApi.Repositories
{
    public class NegociacoesRepository
    {
        private readonly string _connectionString;

        public NegociacoesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public virtual async Task<List<OrdemProcessada>> ObterOrdensProcessadas()
        {
            var ordensProcessadas = new List<OrdemProcessada>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT * FROM OrdemProcessada";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var ordemProcessada = new OrdemProcessada
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                TipoOrdem = reader.GetString(reader.GetOrdinal("TipoOrdem")),
                                NomeAtivo = reader.GetString(reader.GetOrdinal("NomeAtivo")),
                                Preco = reader.GetDecimal(reader.GetOrdinal("Preco")),
                                Quantidade = reader.GetInt32(reader.GetOrdinal("Quantidade")),
                                Status = reader.GetString(reader.GetOrdinal("Status"))
                            };
                            ordensProcessadas.Add(ordemProcessada);
                        }
                    }
                }
            }
            return ordensProcessadas;
        }

        public async Task<List<Negocio>> ObterNegocios()
        {
            var negocios = new List<Negocio>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT * FROM negocios";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var negocio = new Negocio
                            {
                                NomeAtivo = reader.GetString(reader.GetOrdinal("NomeAtivo")),
                                Preco = reader.GetDecimal(reader.GetOrdinal("Preco")),
                                Quantidade = reader.GetInt32(reader.GetOrdinal("Quantidade")),
                            };
                            negocios.Add(negocio);
                        }
                    }
                }
            }
            return negocios;
        }
    }
}
