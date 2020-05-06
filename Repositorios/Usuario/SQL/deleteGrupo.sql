IF NOT EXISTS(SELECT 1 FROM GRUPOS WHERE ID_GRUPO = @ID)
    THROW 51000, 'Grupo Informado não existe', 1;  

IF EXISTS(SELECT 1 FROM USUARIOS WHERE GRUPO = @ID)
    THROW 51000, 'Grupo Possui usuários, não é possível deleta-lo', 1;  

DELETE FROM GRUPOS WHERE ID_GRUPO = @ID