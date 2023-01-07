using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Threading.Channels;

namespace projeto_byteBank
{
    public class Program
    {
        public static List<int> contas = new List<int>();
        public static List<string> Titulares = new List<string>();
        public static List<string> Cpfs = new List<string>();
        public static List<string> Senhas = new List<string>();
        public static List<double> Saldos = new List<double>();
        public static string UsuarioLogado { set; get; } = "";
        public static int UsuarioID { set; get; }

        public static void Main(string[] args)
        {
            DefinirCores();
            MenuLogin();
        }
        public static void DefinirCores()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkCyan;

            // Necessário para deixar a tela 100% colorida
            Console.Clear();
        }
        public static void MenuLogin()
        {
            int opcao = -1;
            bool numero;
            string opcaoEscolhida;

            Console.Clear();
            Console.WriteLine("_____BEM VINDO AO BYTEBANK_____");
            Console.WriteLine("  Seu banco digital completo");
            Console.WriteLine();
            Console.WriteLine("Faça login ou Crie uma conta");
            Console.WriteLine();

            do
            {
                Console.WriteLine("Realizar login  --------- digite 1");
                Console.WriteLine("Criar uma conta --------- digite 2");
                Console.WriteLine("Fechar programa --------- digite 0");

                opcaoEscolhida = Console.ReadLine();
                numero = int.TryParse(opcaoEscolhida, out opcao);

                // Apenas valores válidos
                if (numero && (opcao >= 0 && opcao <= 2))
                {
                    switch (opcao)
                    {
                        case 0:
                            Console.Clear();
                            Console.WriteLine("Agradecemos sua visita.");
                            break;

                        case 1:
                            Logar();
                            break;

                        case 2:
                            SeCadastrar();
                            break;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"\"{opcaoEscolhida}\" é uma opção inválido, escolha uma opção válida.");
                    opcao = -1;
                }

            } while (opcao < 0);
        }

        public static void SeCadastrar()
        {
            string nome = ValidarEntrada("Qual é o seu nome?");
            string cpf = ValidarEntrada("Qual é o seu CPF?");
            string senha = ValidarEntrada("Digite uma senha:");

            Cpfs.Add(cpf);
            Senhas.Add(senha);
            Titulares.Add(nome);
            Saldos.Add(0);

            Console.Clear();
            Console.WriteLine("Seu cadastro foi realizado com sucesso!\r\nVocê já pode fazer login.");
            Console.WriteLine("\r\nDeseja realizar outro cadastro? (s/n)");
            string novoCadastro = Console.ReadLine();

            if (novoCadastro == "s")
            {
                SeCadastrar();
            }
            else
            {
                MenuLogin();
            }
        }

        public static string ValidarEntrada(string pergunta)
        {
            string resposta;
            string aceitoOpcao;
            bool aceito = false;

            do
            {
                Console.Clear();
                Console.WriteLine(pergunta);
                resposta = Console.ReadLine();
                Console.WriteLine("Quer mudar? (s/n)");
                aceitoOpcao = Console.ReadLine();

                if (aceitoOpcao != "s")
                {
                    aceito = true;
                }

            } while (aceito == false);

            return resposta;
        }

        public static void Logar()
        {
            string nome;
            string senha;
            int indexUsuario;
            int indexSenha;

            Console.Clear();

            do
            {
                Console.WriteLine("Qual é o seu nome?");
                nome = Console.ReadLine();
                indexUsuario = VerificarUsuario(nome);

                if (indexUsuario < 0)
                {
                    Console.Clear();
                    Console.WriteLine($"\"{nome}\" não está em nossos cadastros, tente novamente.");
                }

            } while (indexUsuario < 0);

            Console.Clear();
            do
            {
                Console.WriteLine($"{nome}, qual é a sua senha de acesso?");
                senha = Console.ReadLine();
                indexSenha = VerificarSenha(senha);

                if (indexSenha < 0)
                {
                    Console.Clear();
                    Console.WriteLine($"\"{nome}\", a senha que você digitou está errada.");
                }

            } while (indexSenha < 0);

            UsuarioLogado = nome;
            UsuarioID = indexUsuario;

            Console.Clear();
            Console.WriteLine($"Muito bem, você logou em nosso sistema.\r\n");
            MenuOpcoes();
        }

        public static int VerificarUsuario(string nome)
        {
            return Titulares.IndexOf(nome);
        }

        public static int VerificarSenha(string senha)
        {
            return Senhas.IndexOf(senha);
        }

        static void MenuOpcoes()
        {
            bool opcaoValida = false;
            bool numero = false;
            int opcao;
            string opcaoEscolhida;

            Console.WriteLine($"Bem vindo (a) ao MENU PRINCIPAL, {UsuarioLogado}!");
            Console.WriteLine("Escolha uma das opções abaixo");
            Console.WriteLine();

            do
            {
                Console.WriteLine("1. Listar todas as contas");
                Console.WriteLine("2. Excluir uma conta");
                Console.WriteLine("3. Demonstrar saldo total");
                Console.WriteLine("4. Realizar transações");
                Console.WriteLine("0. Sair da conta");
                Console.WriteLine();
                Console.Write("Digite a opção desejada:");

                opcaoEscolhida = Console.ReadLine();
                numero = int.TryParse(opcaoEscolhida, out opcao);

                if (numero && (opcao >= 0 && opcao <= 4))
                {
                    Console.Clear();
                    switch (opcao)
                    {
                        case 0:
                            opcaoValida = true;
                            MenuLogin();
                            break;

                        case 1:
                            ListarTodasAsContas();
                            break;

                        case 2:
                            DeletarUsuario();
                            break;

                        case 3:
                            ApresentarSaldo();
                            break;
                                                        
                        case 4:
                            SubMenu();
                            break;
                    }

                    if (opcao != 0)
                    {
                        MenuOpcoes();
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"A opção \"{opcaoEscolhida}\" é inválida.\n Escolha uma opção a seguir:\n");
                }

            } while (opcaoValida == false);

        }

        static void ListarTodasAsContas()
        {
            Console.Clear();

            for (int i = 0; i < Cpfs.Count; i++)
            {
                Console.WriteLine($"ID = {i} | Titular = {Titulares[i]} | CPF = {Cpfs[i]} | Saldo = R$ {Saldos[i]:F2}");
            }

            Console.WriteLine($"Total de contas: {Cpfs.Count}\n\n");
        }

        public static void DeletarUsuario()
        {
            Console.WriteLine("Favor digitar seu CPF: ");
            string cpfParaDeletar = Console.ReadLine();
            int indexParaDeletar = Cpfs.FindIndex(cpf => cpf == cpfParaDeletar);

            if (indexParaDeletar == -1)
            {
                Console.WriteLine("CPF não encontrado. Retornando ao MENU PRINCIPAL.");
            }
            else
            {
                Console.WriteLine("Tem certeza que deseja deletar essa conta?\n 1. SIM    2.NÃO ");
                int opcaoDeletar = int.Parse(Console.ReadLine());

                if (opcaoDeletar == 1)
                {
                    Cpfs.Remove(cpfParaDeletar);
                    Titulares.RemoveAt(indexParaDeletar);
                    Senhas.RemoveAt(indexParaDeletar);
                    Saldos.RemoveAt(indexParaDeletar);

                    Console.WriteLine($"A conta do CPF {cpfParaDeletar} foi deletada.");
                    Console.WriteLine();
                }
                else if (opcaoDeletar == 2)
                {
                    Console.WriteLine($"A conta do {cpfParaDeletar} NÃO foi deletada. Retornando ao MENU PRINCIPAL.");
                }
                else
                {
                    Console.WriteLine("Opção inválida. Retornando ao MENU PRINCIPAL.");
                }
            }

            MenuOpcoes();
        }

        public static void ApresentarSaldo()
        {
            Console.WriteLine($"Total acumulado no banco: {Saldos.Sum()}");
            Console.WriteLine();
        }

        public static void SubMenu()
        {
            bool opcaoValida = false;
            bool numero = false;
            int opcao;
            string opcaoEscolhida;

            do
            {
                Console.WriteLine("Bem vindo(a) ao MENU DE TRANSAÇÕES");
                Console.WriteLine("  Escolha uma das opções abaixo");
                Console.WriteLine();
                Console.WriteLine("1. Realizar saque");
                Console.WriteLine("2. Realizar depósito");
                Console.WriteLine("3. Realizar transferência");
                Console.WriteLine("4. Voltar ao MENU PRINCIPAL");
                Console.WriteLine();
                Console.WriteLine("Digite a opção desejada: ");

                opcaoEscolhida = Console.ReadLine();
                numero = int.TryParse(opcaoEscolhida, out opcao);

                if (numero && (opcao >= 0 && opcao <= 4))
                {
                    Console.Clear();
                    switch (opcao)
                    {
                        case 1:
                            SacarDinheiro();
                            break;

                        case 2:
                            DepositarDinheiro();
                            break;

                        case 3:
                            TransferirDinheiro();
                            break;

                        case 4:
                            opcaoValida = true;
                            MenuOpcoes();
                            break;
                    }

                    if (opcao != 0)
                    {
                        MenuOpcoes();
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"A opção \"{opcaoEscolhida}\" é inválida.\n Escolha uma opção a seguir:\n");
                }

            } while (opcaoValida == false);
        }

        public static void SacarDinheiro()
        {
            Console.WriteLine("Favor digitar o CPF: ");
            string cpfParaSacar = Console.ReadLine();
            int indexParaSacar = Cpfs.FindIndex(cpf => cpf == cpfParaSacar);

            if (indexParaSacar == -1)
            {
                Console.WriteLine("CPF não encontrado. Essa conta não existe.");
            }
            else
            {
                Console.WriteLine("Favor digitar sua senha: ");
                string senhaParaSacar = Console.ReadLine();
                indexParaSacar = Senhas.FindIndex(senha => senha == senhaParaSacar);

                if (indexParaSacar == -1)
                {
                    Console.WriteLine("Sua senha foi digitada errada.");
                }

                ApresentarSaldo();

                Console.Write("Digite o valor que você gostaria de sacar: R$ ");
                double valorParaSacar = double.Parse(Console.ReadLine());

                if (valorParaSacar > Saldos.Sum())
                {
                    Console.WriteLine("Você não possui esse valor na conta.");
                }
                else
                {
                    Console.WriteLine("Tem certeza que deseja sacar esse valor?\n1.SIM    2.NÃO");
                    int opcao = int.Parse(Console.ReadLine());

                    if (opcao == 2)
                    {
                        Console.WriteLine("Retornando ao Menu.");
                    }
                    else
                    {
                        Saldos[UsuarioID] = Saldos[UsuarioID] - valorParaSacar;
                        Console.WriteLine("Seu saque foi feito com sucesso.");
                    }
                }
            }
        }
        public static void DepositarDinheiro()
        {
            double valorDepositar;

            do
            {
                Console.Write("Qual valor você gostaria de depositar? R$ ");
                valorDepositar = double.Parse(Console.ReadLine());

                if (valorDepositar > 0)
                {
                    Saldos[UsuarioID] = +valorDepositar;
                    Console.WriteLine($"Seu depósito foi realizado com sucesso.\n Saldo atual: R$ {Saldos[UsuarioID]}");
                }
                else
                {
                    Console.WriteLine("Valor inválido. Tente outro valor.");
                }

            } while (valorDepositar <= 0);
        }

        public static void TransferirDinheiro()
        {
            double valorTransferencia = 0;
            int indexParaTransferir;
            int confirma = 0;

            do
            {
                Console.WriteLine("Digite o CPF para o qual voce deseja transferir: ");
                string cpfParaTransferir = Console.ReadLine();
                indexParaTransferir = Cpfs.FindIndex(cpf => cpf == cpfParaTransferir);

                if (indexParaTransferir == -1)
                {
                    Console.WriteLine("Essa conta não existe.");
                }
                else if (indexParaTransferir == UsuarioID)
                {
                    Console.WriteLine("Você não pode transferir um valor para sua própria conta.");
                }
                else
                {
                    Console.Write("Qual valor você gostaria de transferir? R$ ");
                    valorTransferencia = double.Parse(Console.ReadLine());

                    if (valorTransferencia > 0 && valorTransferencia <= Saldos[UsuarioID])
                    {
                        Console.WriteLine($"Conta de destino: {Titulares[indexParaTransferir]}    |      {Cpfs[indexParaTransferir]}");
                        Console.WriteLine("Confirma conta de destino: 1.SIM       2.NÃO");
                        confirma = int.Parse(Console.ReadLine());

                        if (confirma == 1)
                        {
                            Saldos[UsuarioID] -= valorTransferencia;
                            Saldos[indexParaTransferir] += valorTransferencia;
                            Console.WriteLine("Sua transferência foi realizada com sucesso.");
                        }
                        else
                        {
                            Console.WriteLine("Se deseja retornar ao MENU anterior --------------- digite 1\nSe deseja tentar novamente --------------- digite 2");
                            confirma = int.Parse(Console.ReadLine());
                        }
                    }
                    else
                    {
                        Console.WriteLine("Você não possui esse valor para transferir.");
                        Console.WriteLine("Se deseja retornar ao MENU anterior --------------- digite 1\nSe deseja tentar novamente --------------- digite 2");
                        confirma = int.Parse(Console.ReadLine());
                    }
                }

            } while (indexParaTransferir == -1 || indexParaTransferir == UsuarioID || valorTransferencia <= 0 || confirma == 2);
        }

        //static string CamuflarSenha()
        //{
        //    var passoword = string.Empty;
        //    ConsoleKey key;

        //    do
        //    {
        //        var keyInfo = Console.ReadKey(intercept: true);
        //        key = keyInfo.Key;

        //        if (key == ConsoleKey.Backspace && passoword.Length > 0)
        //        {
        //            Console.Write("\b \b");
        //            passoword = passoword[0..^1];
        //        }
        //        else if (!char.IsControl(keyInfo.KeyChar))
        //        {
        //            Console.Write("*");
        //            passoword += keyInfo.KeyChar;
        //        }
        //    } while (key != ConsoleKey.Enter);

        //    Console.WriteLine();
        //    return passoword;
        //}

        //public static void Main(string[] args)
        //{

        //    List<string> titulares = new List<string>();
        //    List<string> cpfs = new List<string>();
        //    List<string> senhas = new List<string>();
        //    List<double> saldos = new List<double>();


        //    int opcao;

        //    do
        //    {
        //        Console.Clear();
        //        Login(titulares, cpfs, senhas, saldos);
        //        do
        //        {
        //            Console.Clear();
        //            MenuOpcoes();
        //            Console.WriteLine();
        //            opcao = int.Parse(Console.ReadLine());

        //            switch (opcao)
        //            {
        //                case 0:
        //                    Console.WriteLine("Saindo da conta");
        //                    break;
        //                case 1:
        //                    ListarTodasAsContas(titulares, cpfs, saldos);
        //                    break;
        //                case 2:
        //                    DeletarUsuario(titulares, cpfs, senhas, saldos);
        //                    break;
        //                case 3:
        //                    ApresentarSaldo(saldos);
        //                    break;
        //                case 6:
        //                    do
        //                    {
        //                        Console.Clear();
        //                        SubMenu();
        //                        opcao = int.Parse(Console.ReadLine());

        //                        switch (opcao)
        //                        {
        //                            case 1:
        //                                SacarDinheiro(cpfs, saldos, senhas);
        //                                break;
        //                            case 2:
        //                                DepositarDinheiro(saldos, indexDoSaldo);
        //                                break;
        //                            case 3:
        //                                TransferirDinheiro(indexDoSaldo, titulares, cpfs, saldos);
        //                                break;
        //                            case 4:
        //                                Console.WriteLine("Voltar ao MENU PRINCIPAL");
        //                                break;
        //                        }

        //                    } while (opcao != 4);
        //                    break;
        //            }
        //            Console.WriteLine("-------------------------");
        //        } while (opcao == 1 || opcao == 2);

        //    } while (opcao != 0);
        //}

    }
}
