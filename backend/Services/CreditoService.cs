using backend.Models;
using Backend.Conexao;
namespace backend.Services
{
    public class CreditoService
    {
        public string RegistrarCreditos(CreditoCarbono credito)
        {
            var comandoVerificacao = ConexaoServico.ConexaoPostgres.CreateCommand();
            comandoVerificacao.CommandText = @"SELECT tipo FROM usuarios WHERE id = @id";
            comandoVerificacao.Parameters.AddWithValue(@"id", credito.ProdutorId);

            var tipo = comandoVerificacao.ExecuteScalar()?.ToString();

            if (tipo != "produtor")
            {
                return "Somente produtores podem fazer";
            }

            using var comando = ConexaoServico.ConexaoPostgres.CreateCommand();

            comando.CommandText = @"
             INSERT INTO creditos_carbono (produtor_id, quantidade, origem, data_geracao, status)
                VALUES (@produtor_id, @quantidade, @origem, @data_geracao, @status)
           ";

            comando.Parameters.AddWithValue("@produtor_id", credito.ProdutorId);
            comando.Parameters.AddWithValue("@quantidade", credito.Quantidade);
            comando.Parameters.AddWithValue("@origem", credito.Origem);
            comando.Parameters.AddWithValue("@data_geracao", credito.DataGeracao);
            comando.Parameters.AddWithValue("status", "pendente");

            comando.ExecuteNonQuery();

            return "Crédito registrado com sucesso! (aguardando aprovação)";
        }
    }
}
