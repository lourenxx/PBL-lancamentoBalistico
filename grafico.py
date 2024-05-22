
def ler_dados_arquivo(nome_arquivo):
    with open(nome_arquivo, 'r') as arquivo:
        linhas = arquivo.readlines()
        altura = None
        tempo = None
        velocidade_inicial_em_y = None
        for linha in linhas:
            if linha.startswith("Altura do alvo:"):
                altura = float(linha.split(": ")[1].strip())
                print(altura)

            elif linha.startswith("Tempo gasto para atingir o alvo:"):
                tempo = float(linha.split(": ")[1].strip().replace(",", "."))
                print(tempo)

        return altura, tempo
    
ler_dados_arquivo("dados.txt")
    