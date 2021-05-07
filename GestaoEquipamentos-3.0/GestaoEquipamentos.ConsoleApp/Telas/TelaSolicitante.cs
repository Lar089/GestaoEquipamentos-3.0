using GestaoEquipamentos.ConsoleApp.Controladores;
using GestaoEquipamentos.ConsoleApp.Dominio;
using System;

namespace GestaoEquipamentos.ConsoleApp.Telas
{
    class TelaSolicitante : TelaBase
    {
        private ControladorSolicitante controladorSolicitante;
        public TelaSolicitante(ControladorSolicitante controlador)
            : base("Cadastro de Solicitante")
        {
            controladorSolicitante = controlador;
        }

        public override void InserirNovoRegistro()
        {
            ConfigurarTela("Inserindo um novo chamado...");

            bool conseguiuGravar = GravarSolicitante(0);

            if (conseguiuGravar)
                ApresentarMensagem("Solicitante inserido com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar inserir o solicitante", TipoMensagem.Erro);
                InserirNovoRegistro();
            }
        }

        public override void ExcluirRegistro()
        {
            ConfigurarTela("Excluindo um solicitante...");

            VisualizarRegistros();

            Console.WriteLine();

            Console.Write("Digite o número do solicitante que deseja excluir: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            bool conseguiuExcluir = controladorSolicitante.ExcluirChamado(idSelecionado);

            if (conseguiuExcluir)
                ApresentarMensagem("Solicitante excluído com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar excluir o solicitante", TipoMensagem.Erro);
                ExcluirRegistro();
            }
        }

        public override void EditarRegistro()
        {
            ConfigurarTela("Editando um solicitante...");

            VisualizarRegistros();

            Console.WriteLine();

            Console.Write("Digite o número do solicitante que deseja editar: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            bool conseguiuEditar = GravarSolicitante(idSelecionado);

            if (conseguiuEditar)
                ApresentarMensagem("Solicitante editado com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar editar o solicitante", TipoMensagem.Erro);
                EditarRegistro();
            }
        }

        public override void VisualizarRegistros()
        {
            ConfigurarTela("Visualizando solicitante...");

            MontarCabecalhoTabela();

            Solicitante[] solicitantes = controladorSolicitante.SelecionarTodosChamados();

            if (solicitantes.Length == 0)
            {
                ApresentarMensagem("Nenhum solicitante registrado!", TipoMensagem.Atencao);
                return;
            }

            foreach (Solicitante solicitante in solicitantes)
            {
                Console.WriteLine("{0,-10} | {1,-30} | {2,-55} | {3,-25}",
                    solicitante.id, solicitante.nome, solicitante.numeroTelefone, solicitante.email);
            }
        }

        #region Métodos Privados
        private static void MontarCabecalhoTabela()
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("{0,-10} | {1,-30} | {2,-55} | {3,-25}", "Id", "Nome", "Número de Telefone", "Email");

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");

            Console.ResetColor();
        }

        private bool GravarSolicitante(int idSelecionado)
        {
            string resultadoValidacao;
            bool conseguiuGravar = true;

            Console.Write("Digite o nome do solicitante: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o número do telefone do solicitante: ");
            string numeroTelefone = Console.ReadLine();

            Console.Write("Digite o email do solicitante: ");
            string email = Console.ReadLine();

            resultadoValidacao = controladorSolicitante.RegistrarSolicitante(
                idSelecionado, nome, numeroTelefone, email);

            if (resultadoValidacao != "SOLICITANTE_VALIDO")
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                conseguiuGravar = false;
            }

            return conseguiuGravar;
        }

        #endregion

    }
}
