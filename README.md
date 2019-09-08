# Sobre o Projeto
Este projeto foi desenvolvido com o intuíto de facilitar o processo de criação de novas API's para projetos pequenos de estudo.

## Sobre a arquitetura
O Projeto foi desenvolvido utilizando a arquitetura de Microserviços, afim de facilitar a implementação de novas features e agilizar o processo de correção de bugs. Para realizar requisições foi usada a arquitetura REST.

## Configurações iniciais necessárias
Para começar a desenvolver neste Scaffold, é necessário somente algumas coisas:
- Alterar o nome da solution principal (PadraoAPI.sln) para o nome da futura api;
- Alterar a parte onde diz "PadraoAPI" na classe de Startup da api para o nome da api desejada, bem como o nome do documento na invocação do serviço;
- Adicionar um arquivo de conexoes.json válido na camada de comunicação.

## conexoes.json
É o arquivo não controlado responsável pelo armazenamento das connection strings ao banco de dados, para criar um novo arquivo, o mesmo deve respeitar as normas propostas na classe Comunicacao.Configuracao.Conexao, seguindo o seguinte modelo:

```
[
	{
		"Nome": "Insira aqui o nome do banco de dados",
		"ConnectionString": "Insira aqui a connection string ao banco de dados"
	}
]
```

## Retorno da API
O Scaffold retorna e consome informações no formato JSON.

# Adicionando um novo micro serviço

## Sufixos
Essa arquitetura utiliza os seguintes sufixos em cada camada (ex: Serviço usuário: UsuarioDOM)

	Dominio: Dom;
	Aplicacao: Dto;
	Repositorio: Rep;
	Servico: Srv;
	Api: Controller.

## Criando o Dominio
Na camada de domínio, Crie uma nova pasta com o nome do micro serviço, respeitando as subpastas de implementacao/interfaces.
Nestas classes deverão ficar todas as regras de negócio referentes aos objetos criados e como os mesmos funcionam.
Ex:
+-- Dominio
|	+-- Usuario
|	+-- Implementacao
|	+-- UsuarioDom
|	+-- Interface
|	+-- IUsuarioDom

## Criando chamadas de Repositorio
Na camada de Repositorio, Crie uma nova pasta com a classe de repositório, respeitando as subpastas de implementacao/interfaces. utilizando o sufixo Repositorio para o nome da classe que acessará o banco de dados. as classes devem desta camada ter o Sufixo apropriado, conforme o arquivo Arquitetura.png. Esta camada usa a interface da classe ConexaoBanco, advinda da camada de comunicação para fazer suas chamadas.

### Um pouco sobre a IConexaoBanco
Em caso de necessidade de realizar uma conexão/consulta/update no banco, utilizar os métodos da interface IConexaoBanco, podendo também Optar pelo uso dos métodos do pacote Dapper, abrindo e fechando as conexões usando os métodos IConexaoBanco.AbrirConexao(passando o nome do banco de dados) e IConexaoBanco.FecharConexao(). A interface IConexaoBanco também dispõe de métodos para criação e encerramento de transactions.

## Criando métodos de Serviço
Na camada de Servico, Crie uma nova pasta com a classe de serviço, respeitando as subpastas de implementacao/interfaces, utilizando o sufixo Servico para o nome da classe que tratará os dados do domínio. Esta camada usa os métodos de repositório para alterar/retornar dados utilizando as classes de domínio.

## Por fim os EndPoints e o IoC
Adicione injeção de dependência para as implementações e interfaces das camadas de comunicação(se precisar), repositório, serviço.
Crie uma pasta para guardar o controller (ou controllers) responsável(eis) pelo armazenamento de endpoints da sua aplicação, lembrando que os endpoints podem somente fazer requisições a camada de serviço.
