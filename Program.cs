using System.Net;

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

    static double VelocidadeInicialEixoX (double velocidadeInicial, double thetaUsuario)
    {
                // Convertendo o ângulo de graus para radianos
        double thetaRad = thetaUsuario * Math.PI / 180;
        double vEixoX = velocidadeInicial * Math.Cos(thetaRad);
        return vEixoX;
    }
      static double VelocidadeInicialEixoY (double velocidadeInicial, double tangenteUsuario)
    {
        double vEixoY = velocidadeInicial * Math.Sin(tangenteUsuario);
        return vEixoY;
    }

    static double TempoGasto (double distanciaCanhao, double vEixoX)
    {
        double tempoGasto = distanciaCanhao / (vEixoX);
        return tempoGasto;
    }

    static double ComponenteVertical (double vEixoY, double aceleracaoGravidade, double tempoGasto)
    {
        double componenteVertical = vEixoY - (aceleracaoGravidade * tempoGasto);
        return componenteVertical;
    }

    static void Main(string[] args)
    {
        //entrada de dados

        double alturaAlvo;
        double distanciaCanhao;
        double aceleracaoGravidade = 9.80665;

        Console.Write("Digite a altura do alvo: ");
        alturaAlvo = double.Parse(Console.ReadLine());
        Console.Write("Digite a distancia do canhão: ");
        distanciaCanhao = double.Parse(Console.ReadLine());

        //calcula o angulo theta
        double theta = alturaAlvo / distanciaCanhao;

        //recebe a função para calcular o tan⁻1 de theta
        double tangente = TangenteTheta(theta);

        Console.WriteLine($"Tangente mínima para atingir o alvo: {tangente}°");

        Console.WriteLine($"Escolha um valor maior do que {tangente}: ");
        double thetaUsuario = double.Parse(Console.ReadLine());
        
        //calcula o valor da velocidade inicial (Vo)
        double velocidadeInicial = VelocidadeInicial(aceleracaoGravidade, alturaAlvo, distanciaCanhao, thetaUsuario);

        Console.WriteLine($"Velocidade inicial do projétil = {velocidadeInicial}(m/s)");

        //calcula a velocidade no eixo X
        double vEixoX = VelocidadeInicialEixoX(velocidadeInicial, thetaUsuario);
        
        Console.WriteLine($"Velocidade inicial no EIXO X = {vEixoX}(m/s)");

        //calcula a velocidade no eixo Y
        double vEixoY = VelocidadeInicialEixoY(velocidadeInicial, thetaUsuario);

        Console.WriteLine($"Velocidade inicial no EIXO Y = {vEixoY}(m/s)");

        //calcula o tempo gasto
        double tempoGasto = TempoGasto(distanciaCanhao, vEixoX);
        Console.WriteLine($"Tempo gasto para atingir o alvo: {tempoGasto}");

        //calculca a componente vertical
        double componenteVertical = ComponenteVertical(vEixoY, aceleracaoGravidade, tempoGasto);

        //verifica se o alvo será atingido na subida ou na descida
        if(componenteVertical > 1)
        {
            Console.WriteLine("O alvo será atingido durante a subida");
        }
        else
        {
            Console.WriteLine("O alvo será atingido durante a descida");
        }

        Console.ReadKey();




    }
}
