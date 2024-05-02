import matplotlib.pyplot as plt
import numpy as np

aceleracao_gravidade = 9.80665
altura_inicial = 0
intervalo_tempo = 0.1

def ler_dados_arquivo(nome_arquivo):
    with open(nome_arquivo, 'r') as arquivo:
        linhas = arquivo.readlines()
        altura = None
        tempo = None
        velocidade_inicial_em_y = None
        for linha in linhas:
            if linha.startswith("Altura do alvo:"):
                altura = float(linha.split(": ")[1].strip())
            elif linha.startswith("Tempo gasto para atingir o alvo:"):
                tempo = float(linha.split(": ")[1].strip().replace(",", "."))
            elif linha.startswith("Velocidade inicial no EIXO Y:"):
                velocidade_inicial_em_y = float(linha.split(": ")[1].strip().replace(",", "."))
        return altura, tempo, velocidade_inicial_em_y
    
def calcular_trajetoria(tempo, aceleracao_gravidade, velocidade_inicial_em_y, altura_inicial):
    trajetoria = altura_inicial + velocidade_inicial_em_y * tempo - 0.5 * aceleracao_gravidade * tempo ** 2
    return trajetoria

def plotar_trajetoria(tempo, velocidade_inicial_em_y, intervalo_tempo):

    tempo_pico = velocidade_inicial_em_y / aceleracao_gravidade
    altura_maxima = calcular_trajetoria(tempo_pico, aceleracao_gravidade, velocidade_inicial_em_y, altura_inicial)

    pontos_tempo = np.arange(0, tempo, intervalo_tempo)

    pontos_altura = [calcular_trajetoria(t, aceleracao_gravidade, velocidade_inicial_em_y, altura_inicial) for t in pontos_tempo]

    plt.plot(pontos_tempo, pontos_altura, 'ro', label="Trajetoria do Projétil")  # 'ro' indica pontos vermelhos
    plt.xlabel('Tempo (s)')
    plt.ylabel('Altura (m)')
    plt.title('Trajetória do Projétil')
    plt.grid(True)
    plt.show()





# Nome do arquivo de saída gerado pelo programa em C#
nome_arquivo = 'dados.txt'

altura, tempo, velocidade_inicial_em_y = ler_dados_arquivo(nome_arquivo)


plotar_trajetoria(tempo, velocidade_inicial_em_y, intervalo_tempo)

