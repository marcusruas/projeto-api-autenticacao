IF EXISTS (SELECT 1 FROM PESSOAS WHERE NOME = @NOME)
    THROW 51000, 'Pessoa Já foi cadastrada.', 1;  

IF EXISTS (SELECT 1 FROM PESSOAS WHERE CPF = @CPF)
    THROW 51000, 'CPF Já foi cadastrado.', 1;  

INSERT INTO PESSOAS (NOME, CPF, EMAIL, DDD, NUMERO) 
VALUES              (@NOME, @CPF, @EMAIL, @DDD, @NUMERO)
