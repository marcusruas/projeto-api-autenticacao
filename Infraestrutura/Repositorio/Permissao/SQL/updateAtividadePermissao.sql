IF NOT EXISTS (SELECT 1 FROM PERMISSOES WHERE ID_PERMISSAO = @PERMISSAO)
    THROW 51000, 'Permissão informada não existe.', 1;

UPDATE PERMISSOES
SET ATIVO = @ATIVO
WHERE ID_PERMISSAO = @PERMISSAO