# Sobre o Projeto
Este projeto foi desenvolvido com o intuíto de facilitar o processo de criação de novas API's para projetos pequenos de estudo.

## Sobre a arquitetura
O Projeto foi desenvolvido utilizando a arquitetura de Microserviços, afim de facilitar a implementação de novas features e agilizar o processo de correção de bugs. Para realizar requisições foi usada a arquitetura REST.

## Configurações iniciais necessárias
Para começar a desenvolver neste Scaffold, é necessário somente algumas coisas:
- Alterar o nome da solution principal (PadraoAPI.sln) para o nome da futura api;
- Alterar a parte onde diz "PadraoAPI" na classe de Startup da api para o nome da api desejada, bem como o nome do documento na invocação do serviço;
- Adicionar um arquivo de conexoes.json válido na camada de comunicação.

## Endpoints iniciais
O PadraoAPI implementa alguns middlewares úteis para formar a página inicial da API, que são:
- /health
- /health/json

Estes endpoints podem ser alterados na classe de StartUp da aplicação.

## Retorno da API
O Scaffold retorna e consome informações no formato JSON.

# Adicionando um novo micro serviço

## Sufixos
Essa arquitetura utiliza os seguintes sufixos em cada camada (ex: Serviço usuário: UsuarioDOM)

- Dominio: Dom;
- Aplicacao: Dto;
- Repositorio: Rep;
- Servico: Srv;
- Api: Controller.

## Camada de Dominio
Nesta camada deverão ficar objetos responsáveis por armazenar as regras de negócio do projeto, ela deve ter como retorno a interface da regra desejada para exportar. Para saber mais sobre as camadas que utilizam os objetos de dominio, olhar imagem Arquitetura.png do projeto.
Exemplo de implementação:
- Exemplo
    - Implementacao
        - ExemploDom
    - Interface
        - IExemploDom

## Camada de Repositorio
Esta camada será responsável pelos acessos ao banco de dados (relacionais e não relacionais) utilizando a interface IConexaoBanco da camada de Comunicacao, que le arquivos na pasta SQL de cada micro serviço criado. Objetos desta camada devem receber objetos genéricos ou interfaces de dominio.
Exemplo de implementação:
- Exemplo
    - Implementacao
        - ExemploRep
    - Interface
        - IExemploRep
    - SQL
        - Exemplo/ExecutarTalCoisa

## Camada de Comunicacao
Camada responsável por realizar comunicação com serviços externos, tais como banco de dados, serviços de consulta externa etc. Não há padrão específico definido para esta camada, mas a mesma deve retornar e receber objetos de interface da camada de domínio.    

### Um pouco sobre a IConexaoBanco
Em caso de necessidade de executar um script, utilizar os métodos da interface IConexaoBanco, podendo também Optar pelo uso dos métodos do pacote Dapper, abrindo e fechando as conexões usando os métodos IConexaoBanco.AbrirConexao(passando o nome do banco de dados) e IConexaoBanco.FecharConexao(). A interface IConexaoBanco também dispõe de métodos para criação e encerramento de transactions.

### conexoes.json
É o arquivo não controlado responsável pelo armazenamento das connection strings ao banco de dados, para criar um novo arquivo, o mesmo deve respeitar as normas propostas na classe Comunicacao.Configuracao.ConexaoModel, seguindo o seguinte modelo:

```
[
	{
		"Nome": "Insira aqui o nome do banco de dados",
		"ConnectionString": "Insira aqui a connection string ao banco de dados"
	}
]
```

## Camada de servicos
Camada responsável por manipular os dados obtidos da camada de Repositorio ou comunicacao, em caso de um serviço externo que não seja banco de dados.
Exemplo de implementação:
- Exemplo
    - Implementacao
        - ExemploSrv
    - Interface
        - IExemploSrv

## Por fim os EndPoints e o IoC
Adicione injeção de dependência para as implementações e interfaces das camadas de comunicação(se precisar), repositório, serviço.
Crie uma pasta para guardar o controller (ou controllers) responsável(eis) pelo armazenamento de endpoints da sua aplicação, lembrando que os endpoints podem somente fazer requisições a camada de serviço.
