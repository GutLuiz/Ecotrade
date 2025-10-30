using Backend.Conexao;
using backend.Models;
using Npgsql;
using System;

namespace backend.Services
{
    public class AdministradorService
    {
        public string AprovarCredito(int creditoId, int adminId)
        {
            // Primeiro verifica se o usuário é administrador
            using var comandoVerificacao = ConexaoServico.ConexaoPostgres.CreateCommand();
            comandoVerificacao.CommandText = @"SELECT tipo FROM usuarios WHERE id = @id";
            comandoVerificacao.Parameters.AddWithValue("@id", adminId);

            var tipoUsuario = comandoVerificacao.ExecuteScalar()?.ToString();

            if (tipoUsuario != "administrador")
            {
                return "Apenas administradores podem aprovar créditos.";
            }

            // Atualiza o status do crédito
            using var comando = ConexaoServico.ConexaoPostgres.CreateCommand();
            comando.CommandText = @"
                UPDATE creditos_carbono
                SET status = 'aprovado'
                WHERE id = @credito_id
            ";

            comando.Parameters.AddWithValue("@credito_id", creditoId);
            int linhasAfetadas = comando.ExecuteNonQuery();

            if (linhasAfetadas == 0)
            {
                return "Crédito não encontrado.";
            }

            return "Crédito aprovado com sucesso!";
        }
    }
}
