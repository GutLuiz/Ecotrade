using backend.Models;
using Backend.Conexao;

namespace backend.Services
{
    public class TransacaoService
    {
        public string RegistrarTransacao(Transacao transacao)
        {
            using var conexao = ConexaoServico.ConexaoPostgres;

            // Verifica tipos de comprador e vendedor
            using var cmdTipo = conexao.CreateCommand();
            cmdTipo.CommandText = "SELECT tipo FROM usuarios WHERE id = @id";

            cmdTipo.Parameters.AddWithValue("@id", transacao.CompradorId);
            var tipoComprador = cmdTipo.ExecuteScalar()?.ToString();

            cmdTipo.Parameters.Clear();
            cmdTipo.Parameters.AddWithValue("@id", transacao.VendedorId);
            var tipoVendedor = cmdTipo.ExecuteScalar()?.ToString();

            if (tipoComprador != "empresa")
                return "Somente empresas podem comprar créditos.";

            if (tipoVendedor != "produtor")
                return "Somente produtores podem vender créditos.";

            // ✅ Verifica se o crédito existe e pertence ao produtor
            using var cmdCredito = conexao.CreateCommand();
            cmdCredito.CommandText = @"
                SELECT quantidade, status 
                FROM creditos_carbono 
                WHERE id = @id AND produtor_id = @produtor_id
            ";
            cmdCredito.Parameters.AddWithValue("@id", transacao.CreditoId);
            cmdCredito.Parameters.AddWithValue("@produtor_id", transacao.VendedorId);

            using var reader = cmdCredito.ExecuteReader();
            if (!reader.Read())
                return "Crédito de carbono não encontrado para este produtor.";

            var quantidadeDisponivel = Convert.ToDecimal(reader["quantidade"]);
            var status = reader["status"].ToString();
            reader.Close();

            // ✅ Valida status do crédito
            if (status != "aprovado")
                return "O crédito selecionado ainda não foi aprovado pelo administrador.";

            // ✅ Valida quantidade disponível
            if (quantidadeDisponivel < transacao.Quantidade)
                return "O crédito selecionado não possui quantidade suficiente.";

            // Registra a transação
            using var cmdInsert = conexao.CreateCommand();
            cmdInsert.CommandText = @"
                INSERT INTO transacoes (comprador_id, vendedor_id, credito_id, quantidade)
                VALUES (@comprador_id, @vendedor_id, @credito_id, @quantidade)
            ";
            cmdInsert.Parameters.AddWithValue("@comprador_id", transacao.CompradorId);
            cmdInsert.Parameters.AddWithValue("@vendedor_id", transacao.VendedorId);
            cmdInsert.Parameters.AddWithValue("@credito_id", transacao.CreditoId);
            cmdInsert.Parameters.AddWithValue("@quantidade", transacao.Quantidade);
            cmdInsert.ExecuteNonQuery();

            // Atualiza o crédito de carbono usado
            using var cmdAtualizar = conexao.CreateCommand();
            cmdAtualizar.CommandText = @"
                UPDATE creditos_carbono
                SET quantidade = quantidade - @quantidade
                WHERE id = @id
            ";
            cmdAtualizar.Parameters.AddWithValue("@quantidade", transacao.Quantidade);
            cmdAtualizar.Parameters.AddWithValue("@id", transacao.CreditoId);
            cmdAtualizar.ExecuteNonQuery();

            return "Transação realizada com sucesso!";
        }
    }
}
