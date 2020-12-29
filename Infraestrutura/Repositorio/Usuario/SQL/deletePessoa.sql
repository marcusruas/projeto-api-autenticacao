IF NOT EXISTS(SELECT TOP 1 1 FROM PESSOAS WHERE ID_PESSOA = @ID)
    THROW 51000, 'Não foi possível localizar a pessoa informada.', 1;  

IF EXISTS(SELECT TOP 1 1 FROM USUARIOS WHERE PESSOA = @ID)
    THROW 51000, 'Pessoa Possui usuários, não é possível deleta-la', 1;  

DELETE FROM PESSOAS WHERE ID_PESSOA = @ID