using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace projeto_byteBank
{
    public class Program
    {
        static void MenuOpcoes()
        {
            Console.WriteLine("1. Inserir usuário");
            Console.WriteLine("2. Deletar usuário");
            Console.WriteLine("3. Listar todos os usuários");
            Console.WriteLine("4. Detalhes de um usuário");
            Console.WriteLine("5. Total armazenado no banco");
            Console.WriteLine("6. Manipular a conta");
            Console.WriteLine("0. Para sair do programa");
            Console.WriteLine("Digite a oçpão desejada: ");
        }

        static void SubMenu()
        {
            Console.WriteLine("1. Sacar dinheiro");
            Console.WriteLine("2. Depositar dinheiro");
            Console.WriteLine("3. Transferir dinheiro");
            Console.WriteLine("0. Voltar ao Menu Principal");
            Console.WriteLine("Digite a opção desejada: ");
        }

        static void InserirUsuario(List<string> titulares, List<string> cpfs, List<string> dataNascimentos, List<string> senhas, List<double> saldos)
        {
            Console.WriteLine("Digite seu nome completo: ");
            titulares.Add(Console.ReadLine());
            Console.WriteLine("Digite seu CPF: ");
            cpfs.Add(Console.ReadLine());
            Console.WriteLine("Digite sua data de nascimento: ");
            dataNascimentos.Add(Console.ReadLine());
            Console.WriteLine("Digite uma senha (deve conter 6 caracteres, ao menos 1 letra maiuscúla e um caracter especial): ");
            senhas.Add(Console.ReadLine());
            saldos.Add(0);


        }

        static void DeletarUsuario(List<string> titulares, List<string> cpfs, List<string> dataNascimentos, List<string> senhas, List<double> saldos)
        {
            Console.WriteLine("Favor digitar seu CPF: ");
            string cpfParaDeletar = Console.ReadLine();
            int indexParaDeletar = cpfs.FindIndex(cpf => cpf == cpfParaDeletar);
            
            if(indexParaDeletar == -1)
            {
                Console.WriteLine("CPF não encontrado. Essa conta não existe.");
            }

            cpfs.Remove(cpfParaDeletar);
            titulares.RemoveAt(indexParaDeletar);
            dataNascimentos.RemoveAt(indexParaDeletar);
            senhas.RemoveAt(indexParaDeletar);
            saldos.RemoveAt(indexParaDeletar);

            Console.WriteLine("Sua conta foi deletada.");

        }

       static void ListarTodasAsContas(List<string> titulares, List<string> cpfs, List<string> dataNascimentos, List<double> saldos)
        {
            for (int i = 0; i < cpfs.Count; i++)
            {
                ApresentarConta(i, titulares, cpfs, dataNascimentos, saldos);
            }
        }

                
       static void DetalharUsuario(List<string> titulares, List<string> cpfs, List<string> dataNascimentos, List<double> saldos)
       {
            Console.Write("Favor digitar seu CPF: ");
            string cpfParaDetalhar = Console.ReadLine();
            int indexParaDetalhar = cpfs.FindIndex(cpf => cpf == cpfParaDetalhar);

            if (indexParaDetalhar == -1)
            {
                Console.WriteLine("CPF não encontrado. Essa conta não existe.");
            }

            ApresentarConta(indexParaDetalhar, titulares, cpfs, dataNascimentos, saldos);
        }

       static void ApresentarConta(int index, List<string> titulares, List<string> cpfs, List<string> dataNascimentos, List<double> saldos)
        {
            Console.WriteLine($"CPF = {cpfs[index]} | Titular = {titulares[index]} | Saldo = R$ {saldos[index]:F2}");
        }

        static void ApresentarQuantiaAcumulada(List<double> saldos)
        {
            Console.WriteLine($"Total acumulado no banco: {saldos.Sum()}");
            
        }

        static void SacarDinheiro(List<string> cpfs, List<double> saldos, List<string>senhas)
        {
            Console.WriteLine("Favor digitar seu CPF: ");
            string cpfParaSacar = Console.ReadLine();
            int indexParaSacar = cpfs.FindIndex(cpf => cpf == cpfParaSacar);

            if (indexParaSacar == -1)
            {
                Console.WriteLine("CPF não encontrado. Essa conta não existe.");
            }

            Console.WriteLine("Favor digitar sua senha: ");
            string senhaParaSacar = Console.ReadLine();
            indexParaSacar = senhas.FindIndex(senha => senha == senhaParaSacar);

            if (indexParaSacar == -1)
            {
                Console.WriteLine("Sua senha foi digitada errada.");
            }
            ApresentarQuantiaAcumulada(saldos);

            Console.WriteLine("Digite o valor que você gostaria de sacar: ");
            double valorParaSacar = double.Parse(Console.ReadLine());

            if(valorParaSacar > saldos.Sum())
            {
                Console.WriteLine("Você não possui esse valor na conta.");
            }
            else
            {
                Console.WriteLine("Tem certeza que deseja sacar esse valor?\n1.SIM    2.NÃO");
                int opcao = int.Parse(Console.ReadLine());

                if(opcao == 2)
                {
                    Console.WriteLine("Retornando ao Menu.");
                }
                else
                {
                    Console.WriteLine("Seu saque foi feito com sucesso.");
                }
            }
        }

        static void DepositarDinheiro(List<string> cpfs, List<double> saldos, List<string> senhas)
        {

            Console.WriteLine("Favor digitar seu CPF: ");
            string seuCpfParaDepositar = Console.ReadLine();
            int indexParaDepositar = cpfs.FindIndex(cpf => cpf == seuCpfParaDepositar);

            if (indexParaDepositar == -1)
            {
                Console.WriteLine("CPF não encontrado. Essa conta não existe.");
            }

            Console.WriteLine("Favor digitar sua senha: ");
            string senhaParaDepositar = Console.ReadLine();
            indexParaSacar = senhas.FindIndex(senha => senha == senhaParaSacar);

            if (indexParaSacar == -1)
            {
                Console.WriteLine("Sua senha foi digitada errada.");
            }
        }

        static void TransferirDinheiro((List<string> cpfs, List<double> saldos, List<string> senhas)
        {

        }
        

        public static void Main(string[] args)
        {

            List<string> titulares = new List<string>();
            List<string> cpfs = new List<string>();
            List<string> dataNascimentos = new List<string>();
            List<string> senhas = new List<string>();
            List<double> saldos = new List<double>();
            List<double> valoresDeSaque = new List<double>();
            List<double> valoresDeDeposito = new List<double>();
            List<double> valoresDeTransferencia = new List<double>();



            int opcao;

            do
            {
                MenuOpcoes();
                opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 0:
                        Console.WriteLine("Programa encerrado");
                        break;
                    case 1:
                        InserirUsuario(titulares, cpfs, dataNascimentos, senhas, saldos);
                        break;
                    case 2:
                        DeletarUsuario(titulares, cpfs, dataNascimentos, senhas, saldos);
                        break;
                    case 3:
                        ListarTodasAsContas(titulares, cpfs, dataNascimentos, saldos);
                        break;
                    case 4:
                        DetalharUsuario(titulares, cpfs, dataNascimentos, saldos);
                        break;
                    case 5:
                        ApresentarQuantiaAcumulada(saldos);
                            break;
                    case 6:
                        do
                        {
                            SubMenu();
                            opcao = int.Parse(Console.ReadLine());

                            switch (opcao)
                            {
                                case 0:
                                    break;
                                case 1:
                                    SacarDinheiro(cpfs, saldos, senhas);
                                break;
                                case 2:
                                    DepositarDinheiro(cpfs, saldos, senhas);
                                    break;
                                case 3:
                                    TransferirDinheiro(cpfs, saldos,senhas);
                                        break;
                            }
                            

                        } while (opcao !=0);
                        break;
                    
                }
                Console.WriteLine("-------------------------");

            } while (opcao != 0);
        }
    }
}
