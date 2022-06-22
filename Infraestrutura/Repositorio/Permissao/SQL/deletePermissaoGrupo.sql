IF NOT EXISTS (SELECT 1 FROM PERMISSOES WHERE ID_PERMISSAO = @PERMISSAO)
    THROW 51000, 'Permissão não encontrada. Verifique os dados.', 1;  

IF NOT EXISTS (SELECT 1 FROM GRUPOS WHERE ID_GRUPO = @GRUPO)
    THROW 51000, 'Grupo não encontrado. Verifique os dados.', 1;  

IF NOT EXISTS (SELECT 1 FROM PERMISSOES_GRUPOS WHERE GRUPO = @GRUPO AND PERMISSAO = @PERMISSAO)
    THROW 51000, 'Permissão para grupo não existe', 1;  

DELETE FROM PERMISSOES_GRUPOS
WHERE 
    PERMISSAO = @PERMISSAO
AND GRUPO = @GRUPO