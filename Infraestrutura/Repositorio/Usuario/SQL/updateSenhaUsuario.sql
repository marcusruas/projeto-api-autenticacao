IF NOT EXISTS (
    SELECT TOP 1 1 FROM USUARIOS
    WHERE 
        ID_USUARIO = @ID
    AND SENHA = @SENHAANTIGA
)
THROW 51000, 'Usuário ou senha incorretos.', 1;  

UPDATE USUARIOS
SET SENHA = @SENHANOVA
,   DATA_CADASTRO_SENHA = CAST(GETDATE() AS DATE)
WHERE ID_USUARIO = @ID