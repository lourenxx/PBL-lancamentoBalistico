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

    static double VelocidadeInicial (double aceleracaoGravidade, double alturaAlvo, double distanciaCanhao, double theta)
    {
        double velocidadeInicial = Math.Sqrt(((Math.Pow(aceleracaoGravidade*distanciaCanhao, 2)*(1 + Math.Pow(theta,2))))/(2*((alturaAlvo - distanciaCanhao) * theta)));
        return velocidadeInicial;
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

        Console.WriteLine($"A tangente de theta é {tangente}°");

        //calcula o valor da velocidade inicial (Vo)
        double velocidadeInicial = VelocidadeInicial(aceleracaoGravidade, alturaAlvo, distanciaCanhao, theta);  \

        Console.WriteLine($"A velocidade inicial do projétil é: {velocidadeInicial}(m/s)");

        //calcula a velocidade no eixo X 
    

    }
}
