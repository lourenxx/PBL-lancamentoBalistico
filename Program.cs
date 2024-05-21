using System.Net;
using System.Security.Cryptography;

namespace PBL_lancamentoBalistico;

class Program
{
    //função que serve para calcular a tangente do angulo theta
    static double TangenteTheta(double theta)
    {
        //calcula o tan⁻1 do angulo theta em radianos (a biblioteca Math nao oferece suporte para Graus)
        double thetaEmRadianos = Math.Atan(theta); 

        // Por meio dessa formula faz a conversão para Graus
        double thetaEmGraus = thetaEmRadianos * 180 / Math.PI; 
        
        // retorna o a tan⁻1 de theta
        return thetaEmGraus; 
    }

    //função que calcula a velocidade inicial
    static double VelocidadeInicial (double aceleracaoGravidade, double alturaAlvo, double distanciaCanhao, double thetaUsuario)
    {
        // Convertendo o ângulo de graus para radianos
        double thetaRad = thetaUsuario * Math.PI / 180;

        // Calculando a tangente do ângulo de elevação
        double tangenteUsuario = Math.Tan(thetaRad);

        // Calculando a velocidade inicial
        double velocidadeInicial = Math.Sqrt((-aceleracaoGravidade * Math.Pow(distanciaCanhao, 2) * (1 + Math.Pow(tangenteUsuario, 2))) / (2 * (alturaAlvo - distanciaCanhao * tangenteUsuario)));

        return velocidadeInicial;
    }


    //função que calcula a velocidade inicial no eixo X
    static double VelocidadeInicialEixoX (double velocidadeInicial, double thetaUsuario)
    {
        // Convertendo o ângulo de graus para radianos
        double thetaRad = thetaUsuario * Math.PI / 180;
        double vEixoX = velocidadeInicial * Math.Cos(thetaRad);
        return vEixoX;
    }


    //função que calcula a velocidade inicial no eixo Y
          static double VelocidadeInicialEixoY (double velocidadeInicial, double tangenteUsuario)
    {
        double vEixoY = velocidadeInicial * Math.Sin(tangenteUsuario);
        return vEixoY;
    }



    //função que calcula o tempo gasto pelo projetil até atingir o alvo
    static double TempoGasto (double distanciaCanhao, double vEixoX)
    {
        double tempoGasto = distanciaCanhao / (vEixoX);
        return tempoGasto;
    }

    //função que calcula a altura maxima
    static double AlturaMaximaTrajetoria(double vEixoY)
    {
        double alturaMaxima = Math.Pow(vEixoY, 2) / (2 * 9.80665); // 9.80665 é a aceleração devido à gravidade
        return alturaMaxima;
    }



    static void Main(string[] args)
    {
        //variáveis
        string resposta;
        double alturaAlvo;
        double distanciaCanhao;
        string arquivoDados = "dados.txt";
        double aceleracaoGravidade = 9.80665;

        //loop que executa o script
        do 
        {
            //tratamento de exceção para valores invalidos
            try
            {
                // Verifica se o arquivo existe e o exclui se existir
                if (File.Exists(arquivoDados))
                {
                    File.Delete(arquivoDados);
                }


                //entrada de dados
                Console.Write("Digite a altura do alvo: ");
                alturaAlvo = double.Parse(Console.ReadLine());
                Console.Write("Digite a distancia do canhão: ");
                distanciaCanhao = double.Parse(Console.ReadLine());

                Console.Write($"Altura do alvo: {alturaAlvo}");
                Console.WriteLine("");
                Console.Write($"Distância do canhão: {distanciaCanhao}");


                //calcula o angulo theta
                double theta = alturaAlvo / distanciaCanhao;

                //recebe a função para calcular o tan⁻1 de theta
                double tangente = TangenteTheta(theta);

                //mostra a tangente mínima para atingir o alvo
                Console.WriteLine("");
                Console.WriteLine($"Tangente mínima para atingir o alvo: {tangente}°");
                Console.WriteLine("");


                //exige que o usuário escolha um valor maior do que a tangente mínima
                Console.Write($"Escolha um valor maior do que {tangente}: ");
                double thetaUsuario = double.Parse(Console.ReadLine());
                Console.WriteLine("");

                //calcula o valor da velocidade inicial (Vo)
                double velocidadeInicial = VelocidadeInicial(aceleracaoGravidade, alturaAlvo, distanciaCanhao, thetaUsuario);
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine($"Velocidade inicial do projétil: {velocidadeInicial}(m/s)");


                //calcula a velocidade no eixo X
                double vEixoX = VelocidadeInicialEixoX(velocidadeInicial, thetaUsuario);
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine($"Velocidade inicial no EIXO X: {vEixoX}(m/s)");


                //calcula a velocidade no eixo Y
                double vEixoY = VelocidadeInicialEixoY(velocidadeInicial, thetaUsuario);
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine($"Velocidade inicial no EIXO Y: {vEixoY}(m/s)");


                //calcula o tempo gasto
                double tempoGasto = TempoGasto(distanciaCanhao, vEixoX);
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine($"Tempo gasto para atingir o alvo: {tempoGasto}");


                //calculca a componente vertical
                double alturaMaxima = AlturaMaximaTrajetoria(vEixoY);
                Console.WriteLine($"A altura maxima foi de: {alturaMaxima} metros");


                //verifica se o alvo será atingido na subida ou na descida
                if(alturaAlvo < alturaMaxima)
                {
                    Console.WriteLine("----------------------------------------------------------");
                    Console.WriteLine("O alvo será atingido durante a SUBIDA");
                }
                else
                {
                    Console.WriteLine("----------------------------------------------------------");
                    Console.WriteLine("O alvo será atingido durante a DESCIDA");
                }

               
                File.WriteAllText(arquivoDados, $"Altura do alvo: {alturaAlvo}\n");
                File.AppendAllText(arquivoDados, $"Distância do canhão: {distanciaCanhao}\n");
                File.AppendAllText(arquivoDados, $"Angulo da tangente: {thetaUsuario}\n");
                File.AppendAllText(arquivoDados, $"Tangente mínima para atingir o alvo: {tangente}°\n");
                File.AppendAllText(arquivoDados, $"Velocidade inicial do projétil: {velocidadeInicial}(m/s)\n");
                File.AppendAllText(arquivoDados, $"Velocidade inicial no EIXO X: {vEixoX}\n");
                File.AppendAllText(arquivoDados, $"Velocidade inicial no EIXO Y: {vEixoY}\n");
                File.AppendAllText(arquivoDados, $"Tempo gasto para atingir o alvo: {tempoGasto}\n");


            }
                
            catch (FormatException)
            {
                Console.WriteLine("Erro: Entrada inválida. Certifique-se de digitar um número válido.");
            }
            
       
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("Deseja Calcular uma nova trajetoria? Digite 'sim' ou 'nao'");
            resposta = Console.ReadLine();

            if(resposta.ToLower() == "nao")
            {
                Console.WriteLine("Fechando...");
                break;
            }

            
        }while(resposta.ToLower() == "sim");
        
   
    Console.ReadKey();
    }
}

