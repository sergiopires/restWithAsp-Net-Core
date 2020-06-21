using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using restWithAspNetCore.Data.Convertes;
using restWithAspNetCore.Data.VO;
using restWithAspNetCore.Model;
using restWithAspNetCore.Model.Context;
using restWithAspNetCore.Repository.Generic;
using restWithAspNetCore.Repository.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace restWithAspNetCore.Businnes.Implementations
{
    public class PersonBusinnesImpl : IPersonBusinnes
    {
        // private IPersonRepository _repository;
        private IRepository<Person> _repository;

        //Adicionar os VOs
        private readonly PersonConverter _converter;

        public PersonBusinnesImpl(IRepository<Person> repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }


        public PersonVO Create(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Create(personEntity);
            return _converter.Parse(personEntity);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public List<PersonVO> FindAll()
        {
            return _converter.ParseList(_repository.FindAll());
        }
        public PersonVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public PersonVO Update(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Update(personEntity);
            return _converter.Parse(personEntity);
        }
    }
}
