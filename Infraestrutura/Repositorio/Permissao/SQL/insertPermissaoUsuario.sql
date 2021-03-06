IF NOT EXISTS (SELECT 1 FROM PERMISSOES WHERE ID_PERMISSAO = @PERMISSAO)
    THROW 51000, 'Permissão não encontrada. Verifique os dados.', 1;  

IF NOT EXISTS (SELECT 1 FROM USUARIOS WHERE ID_USUARIO = @USUARIO)
    THROW 51000, 'Usuário não encontrado. Verifique os dados.', 1;  

IF EXISTS (SELECT 1 FROM PERMISSOES_USUARIOS WHERE USUARIO = @USUARIO AND PERMISSAO = @PERMISSAO)
    THROW 51000, 'Permissão para usuário já existe', 1;  

INSERT INTO PERMISSOES_USUARIOS (USUARIO, PERMISSAO, DATA_CRIACAO, USUARIO_RESPONSAVEL)
VALUES							(@USUARIO, @PERMISSAO, GETDATE(), 1)