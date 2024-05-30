from openpyxl import load_workbook


def ler_dados_arquivo(nome_arquivo):
    with open(nome_arquivo, 'r') as arquivo:
        linhas = arquivo.readlines()
        altura_alvo = None
        velocidade_inicial = None
        tangente_minima = None
        aceleracao_gravidade = None

        for linha in linhas:
            if linha.startswith("Altura do alvo:"):
                altura_alvo = float(linha.split(": ")[1].strip().replace(',', '.'))
                print(altura_alvo)
                
            elif linha.startswith("Velocidade inicial do projétil:"):
                velocidade_inicial = float(linha.split(": ")[1].strip().replace(',', '.'))
                print(velocidade_inicial)
                
            elif linha.startswith("Tangente mínima para atingir o alvo:"):
                tangente_minima = float(linha.split(": ")[1].strip().replace(',', '.'))
                print(tangente_minima)
            
            elif linha.startswith("Aceleração da Gravidade:"):
                aceleracao_gravidade = float(linha.split(": ")[1].strip().replace(',', '.'))
                print(aceleracao_gravidade)

        return altura_alvo, velocidade_inicial, tangente_minima, aceleracao_gravidade

altura_alvo, velocidade_inicial, tangente_minima, aceleracao_gravidade = ler_dados_arquivo("dados.txt")

ler_planilha = load_workbook('graficoTrajetoria.xlsx')
ativar_planilha = ler_planilha.active

ativar_planilha['C2'] = velocidade_inicial
ativar_planilha['D2'] = tangente_minima
ativar_planilha['E2'] =  altura_alvo

ler_planilha.save('graficoTrajetoria.xlsx')