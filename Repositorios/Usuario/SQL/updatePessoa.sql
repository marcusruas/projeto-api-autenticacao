IF EXISTS(SELECT 1 FROM PESSOAS WHERE CPF = @CPF)
   THROW 51000, 'CPF solicitado já está cadastrado.', 1;  

UPDATE PESSOAS
   SET NOME		= @NOME	
      ,CPF		= @CPF
      ,EMAIL	= @EMAIL
      ,DDD     = @DDD
      ,NUMERO  = @NUMERO
WHERE
   ID_PESSOA = @ID_PESSOA

