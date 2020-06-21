using restWithAspNetCore.Data.VO;
using restWithAspNetCore.Model;
using System.Collections.Generic;

namespace restWithAspNetCore.Businnes.Implementations
{
    public interface IPersonBusinnes
    {
        PersonVO Create(PersonVO person);
        PersonVO FindById(long id);
        List<PersonVO> FindAll();
        PersonVO Update(PersonVO person);
        void Delete(long id);
    }
}
