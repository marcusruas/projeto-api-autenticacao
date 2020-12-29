﻿using System.Collections.Generic;
using Infraestrutura.Repositorio.Usuario.Entidade;

namespace Repositorios.Usuario.Interfaces
{
    public interface IGrupoRep
    {
        bool AdicionarGrupo(GrupoDpo grupo);
        GrupoDpo ObterGrupoPorId(int id);
        GrupoDpo ObterPai(int id);
        List<GrupoDpo> ObterFilhos(int id);
        List<GrupoDpo> ObterGrupos(string nome, string descricao);
        bool DeletarGrupo(int id);
        bool VincularGrupos(int grupoPai, int grupoFilho);
    }
}
