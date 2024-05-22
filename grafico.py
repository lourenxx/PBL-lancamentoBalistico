from openpyxl import load_workbook


def ler_dados_arquivo(nome_arquivo):
    with open(nome_arquivo, 'r') as arquivo:
        linhas = arquivo.readlines()
        altura = None
        velocidade_inicial = None
        tangente_minima = None

        for linha in linhas:
            if linha.startswith("Altura do alvo:"):
                altura = (linha.split(": ")[1].strip())
                print(altura)
                

            elif linha.startswith("Velocidade inicial do projétil:"):
                velocidade_inicial = (linha.split(": ")[1].strip())
                print(velocidade_inicial)
                

            elif linha.startswith("Tangente mínima para atingir o alvo:"):
                tangente_minima = (linha.split(": ")[1].strip())
                print(tangente_minima)

        return altura, velocidade_inicial, tangente_minima
    
    
altura, velocidade_incial, tangente_minima = ler_dados_arquivo("dados.txt")

ler_planilha = load_workbook('graficoTrajetoria.xlsx')
ativar_planilha = ler_planilha.active

ativar_planilha['C2'] = velocidade_incial
ativar_planilha['D2'] = tangente_minima 


ler_planilha.save('graficoTrajetoria.xlsx')