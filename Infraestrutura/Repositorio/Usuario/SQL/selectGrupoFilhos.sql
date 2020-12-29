IF @ID IS NULL
    THROW 51000, 'Grupo informado não existe.', 1;  

SELECT
     ID_GRUPO AS ID
    ,NOME
    ,DESCRICAO
    ,ID_PAI AS PAI
FROM GRUPOS
WHERE ID_PAI = @ID