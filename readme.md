# UsuarioAPI
## Api criada para gerenciar usuários e permissõess para meus sistemas

### Sobre o projeto
Api criada com o framework .NET Core na versão 3.1, utilizando pacotes criados com arquitetura .NET Core 2.1. 

### Sufixos - Resumo
    * Representações de objetos
        * Regras de negócio do objeto - Dm
        * Representação do objeto no banco de dados (objeto de persistência) - Dpo (Data Persistence Object)
        * Representação do objeto para entrada/saida de dados da Api - Dto (Data Transfer Object)
        * Objetos de Valor - N/A (Motivo: objeto de valor não necesitam de abstrações)

    * Serviços
        * Camada de Serviço - Sv
        * Camada de Repositorio - Rp
        * Camada de Comunicacao - Cm
        * Camada de Api - Controller