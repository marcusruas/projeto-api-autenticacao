IF NOT EXISTS (SELECT TOP 1 1 FROM GRUPOS WHERE ID_GRUPO = @GRUPOPAI)
     THROW 51000, 'Não foi possível localizar o grupo Pai informado.', 1;  

IF NOT EXISTS (SELECT TOP 1 1 FROM GRUPOS WHERE ID_GRUPO = @GRUPOFILHO)
     THROW 51000, 'Não foi possível localizar o grupo Filho informado.', 1;  

IF EXISTS (SELECT TOP 1 1 FROM GRUPOS WHERE ID_GRUPO = @GRUPOPAI AND ID_PAI = @GRUPOFILHO)
     THROW 51000, 'Grupo Filho informado não pode ser Pai do grupo informado.', 1;  

UPDATE GRUPOS
SET ID_PAI = @GRUPOFILHO
WHERE ID_GRUPO = @GRUPOPAI
