using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace projeto_byteBank
{
    public class Program
    {
        private static int indexDoSaldo;
        static void MenuOpcoes()
        {
            Console.WriteLine("1. Inserir usuário");
            Console.WriteLine("2. Deletar usuário");
            Console.WriteLine("3. Listar todos os usuários");
            Console.WriteLine("4. Detalhes de um usuário");
            Console.WriteLine("5. Saldo total");
            Console.WriteLine("6. Manipular a conta");
            Console.WriteLine("0. Para sair do programa");
            Console.WriteLine();
            Console.WriteLine("Digite a oçpão desejada: ");
        }
        static void SubMenu()
        {
            Console.WriteLine("1. Sacar dinheiro");
            Console.WriteLine("2. Depositar dinheiro");
            Console.WriteLine("3. Transferir dinheiro");
            Console.WriteLine("4. Voltar ao Menu Principal");
            Console.WriteLine();
            Console.WriteLine("Digite a opção desejada: ");
        }
        static void Login(List<string> titulares, List<string> cpfs, List<string> senhas, List<double> saldos)
        {
            Console.WriteLine("_____BEM VINDO AO BYTEBANK_____");
            Console.WriteLine("Seu banco digital completo");
            Console.WriteLine("Faça login ou Crie uma conta");
            Console.WriteLine();
            Console.WriteLine("Realizar login  --------- digite 1");
            Console.WriteLine("Criar uma conta --------- digite 2");

            string cpfParaLogin;
            string senhaLogin;
            int indexParaLogar;
            int tentarNovamente = 0;

            do
            {
                Console.Write("Digite o CPF: ");
                cpfParaLogin = Console.ReadLine();
                indexParaLogar = cpfs.FindIndex(cpf => cpf == cpfParaLogin);

                if (indexParaLogar == -1)
                {
                    Console.Write("CPF inválido. Gostaria de tentar novamente?\n1 (SIM)       2 (NÃO)? ");

                    tentarNovamente = int.Parse(Console.ReadLine());
                    Console.WriteLine();
                }
                else
                {
                    Console.Write("Digite a senha: ");
                    senhaLogin = CamuflarSenha();

                    if (senhas[indexParaLogar] == senhaLogin)
                    {
                        Console.WriteLine("Você entrou na sua conta!");
                        MenuOpcoes();
                    }
                    else
                    {
                        Console.Write("Senha inválida. Gostaria de tentar novamente?\n 1 (SIM)    2 (NÃO)? ");
                        tentarNovamente = int.Parse(Console.ReadLine());
                        Console.WriteLine();
                    }
                }

            }
            while (tentarNovamente == 1);
        }
        static string CamuflarSenha()
        {
            var passoword = string.Empty;
            ConsoleKey key;

            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && passoword.Length > 0)
                {
                    Console.Write("\b \b");
                    passoword = passoword[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    passoword += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);

            Console.WriteLine();
            return passoword;
        }
        static void InserirUsuario(List<string> titulares, List<string> cpfs, List<string> senhas, List<double> saldos)
        {
            Console.WriteLine("Digite seu nome completo: ");
            titulares.Add(Console.ReadLine());

            Console.WriteLine("Digite seu CPF: ");
            cpfs.Add(Console.ReadLine());

            Console.WriteLine("Digite uma senha (deve conter 6 caracteres, ao menos 1 letra maiuscúla e um caracter especial): ");
            senhas.Add(Console.ReadLine());

            saldos.Add(0);


        }
        static void DeletarUsuario(List<string> titulares, List<string> cpfs, List<string> senhas, List<double> saldos)
        {
            Console.WriteLine("Favor digitar seu CPF: ");
            string cpfParaDeletar = Console.ReadLine();
            int indexParaDeletar = cpfs.FindIndex(cpf => cpf == cpfParaDeletar);

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
                    cpfs.Remove(cpfParaDeletar);
                    titulares.RemoveAt(indexParaDeletar);
                    dataNascimentos.RemoveAt(indexParaDeletar);
                    senhas.RemoveAt(indexParaDeletar);
                    saldos.RemoveAt(indexParaDeletar);

                    Console.WriteLine("Sua conta foi deletada.");
                }
                else if (opcaoDeletar == 2)
                {
                    Console.WriteLine("Sua conta NÃO foi deletada. Retornando ao MENU PRINCIPAL.");
                }
                else
                {
                    Console.WriteLine("Opção inválida. Retornando ao MENU PRINCIPAL.");
                }


            }

        }
        static void ListarTodasAsContas(List<string> titulares, List<string> cpfs, List<double> saldos)
        {
            for (int i = 0; i < cpfs.Count; i++)
            {
                ApresentarConta(i, titulares, cpfs, dataNascimentos, saldos);
            }
        }
        static void DetalharUsuario(List<string> titulares, List<string> cpfs, List<double> saldos)
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
        static void ApresentarConta(int index, List<string> titulares, List<string> cpfs, List<double> saldos)
        {
            Console.WriteLine($"CPF = {cpfs[index]} | Titular = {titulares[index]} | Saldo = R$ {saldos[index]:F2}");
        }
        static void ApresentarQuantiaAcumulada(List<double> saldos)
        {
            Console.WriteLine($"Total acumulado no banco: {saldos.Sum()}");

        }
        static void SacarDinheiro(List<string> cpfs, List<double> saldos, List<string> senhas)
        {
            Console.WriteLine("Favor digitar seu CPF: ");
            string cpfParaSacar = Console.ReadLine();
            int indexParaSacar = cpfs.FindIndex(cpf => cpf == cpfParaSacar);

            if (indexParaSacar == -1)
            {
                Console.WriteLine("CPF não encontrado. Essa conta não existe.");
            }

            else
            {
                Console.WriteLine("Favor digitar sua senha: ");
                string senhaParaSacar = Console.ReadLine();
                indexParaSacar = senhas.FindIndex(senha => senha == senhaParaSacar);

                if (indexParaSacar == -1)
                {
                    Console.WriteLine("Sua senha foi digitada errada.");
                }
                ApresentarQuantiaAcumulada(saldos);

                Console.Write("Digite o valor que você gostaria de sacar: R$ ");
                double valorParaSacar = double.Parse(Console.ReadLine());

                if (valorParaSacar > saldos.Sum())
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
                        Console.WriteLine("Seu saque foi feito com sucesso.");
                    }
                }
            }
        }
        static void DepositarDinheiro(List<double> saldos, int indexDoSaldo)
        {

            double valorDepositar;

            do
            {

                Console.Write("Qual valor você gostaria de depositar? R$ ");
                valorDepositar = double.Parse(Console.ReadLine());

                if (valorDepositar > 0)
                {
                    saldos[indexDoSaldo] = +valorDepositar;
                    Console.WriteLine($"Seu depósito foi realizado com sucesso.\n Saldo atual: R$ {saldos[indexDoSaldo]}");
                }

                else
                {
                    Console.WriteLine("Valor inválido. Tente outro valor.");
                }

            } while (valorDepositar <= 0);
        }
        static void TransferirDinheiro(int indexDoSaldo, List<string> titulares, List<string> cpfs, List<double> saldos)
        {

            double valorTransferencia = 0;
            int indexParaTransferir;
            int confirma = 0;


            do
            {
                Console.WriteLine("Digite o CPF para o qual voce deseja transferir: ");
                string cpfParaTransferir = Console.ReadLine();
                indexParaTransferir = cpfs.FindIndex(cpf => cpf == cpfParaTransferir);

                if (indexParaTransferir == -1)
                {
                    Console.WriteLine("Essa conta não existe.");
                }
                else if (indexParaTransferir == indexDoSaldo)
                {
                    Console.WriteLine("Você não pode transferir um valor para sua própria conta.");
                }
                else
                {
                    Console.Write("Qual valor você gostaria de transferir? R$ ");
                    valorTransferencia = double.Parse(Console.ReadLine());

                    if (valorTransferencia > 0 && valorTransferencia <= saldos[indexDoSaldo])
                    {
                        Console.WriteLine($"Conta de destino: {titulares[indexParaTransferir]}    |      {cpfs[indexParaTransferir]}");
                        Console.WriteLine("Confirma conta de destino: 1.SIM       2.NÃO");
                        confirma = int.Parse(Console.ReadLine());

                        if (confirma == 1)
                        {
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

            } while (indexParaTransferir == -1 || indexParaTransferir == indexDoSaldo || valorTransferencia <= 0 || confirma == 2);
        }
        public static void Main(string[] args, int indexDoSaldo)
        {

            List<string> titulares = new List<string>();
            List<string> cpfs = new List<string>();
            List<string> senhas = new List<string>();
            List<double> saldos = new List<double>();

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
                        InserirUsuario(titulares, cpfs,senhas, saldos);
                        break;
                    case 2:
                        DeletarUsuario(titulares, cpfs, senhas, saldos);
                        break;
                    case 3:
                        ListarTodasAsContas(titulares, cpfs,saldos);
                        break;
                    case 4:
                        DetalharUsuario(titulares, cpfs, saldos);
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
                                case 1:
                                    SacarDinheiro(cpfs, saldos, senhas);
                                    break;
                                case 2:
                                    DepositarDinheiro(saldos, indexDoSaldo);
                                    break;
                                case 3:
                                    TransferirDinheiro(indexDoSaldo, titulares, cpfs, saldos);
                                    break;
                                case 4:
                                    Console.WriteLine("Voltar ao MENU PRINCIPAL");
                                    break;
                            }

                        } while (opcao != 4);
                        break;
                }
                Console.WriteLine("-------------------------");

            } while (opcao != 0);
        }
        
    }
}

