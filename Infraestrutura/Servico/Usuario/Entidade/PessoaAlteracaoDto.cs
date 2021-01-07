namespace Infraestrutura.Servico.Usuario.Entidade
{
    public class PessoaAlteracaoDto
    {
        public bool AlterarNome { get; set; }
        public bool AlterarEmail { get; set; }
        public bool AlterarTelefone { get; set; }
        public PessoaDadosAlteracaoDto Dados { get; set; }

        public bool PossuiSolicitacaoAlteracao() =>
            AlterarNome || AlterarEmail || AlterarTelefone;
    }
}