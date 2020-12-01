IF EXISTS(SELECT 1 FROM PESSOAS WHERE CPF = @CPF AND ID_PESSOA <> @ID_PESSOA)
   THROW 51000, 'CPF solicitado para alteração já está cadastrado.', 1;  

UPDATE PESSOAS
   SET NOME		= @NOME	
      ,CPF		= @CPF
      ,EMAIL	= @EMAIL
      ,DDD     = @DDD
      ,NUMERO  = @NUMERO
WHERE
   ID_PESSOA = @ID_PESSOA

