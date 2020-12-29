DECLARE @PAI INT

SELECT @PAI = ID_PAI
FROM GRUPOS
WHERE ID_GRUPO = @ID

IF @PAI IS NULL
    THROW 51000, 'Grupo informado não possui Pai.', 1;  

SELECT
     ID_GRUPO AS ID
    ,NOME
    ,DESCRICAO
    ,ID_PAI AS PAI
FROM GRUPOS
WHERE ID_GRUPO = @PAI