using Elections.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elections.Repository
{
    public class CategoryRepository: IDataRepository<Category>
    {
        private ElectionsContext _electionContext { get; set; }

        public CategoryRepository(ElectionsContext electionContext)
        {
            _electionContext = electionContext;
        }

        public IEnumerable<Category> GetAll()
        {
            return _electionContext.Category.ToList();
        }

        public Category Get(long id)
        {
            return _electionContext.Category.Where(x => x.Id == id).FirstOrDefault();
        }

        public void Add(Category category)
        {
            _electionContext.Category.Add(category);
            _electionContext.SaveChanges();
        }

        public void Update(Category category)
        {
            _electionContext.Category.Update(category);
            _electionContext.SaveChanges();
        }

        public void Delete(Category category)
        {
            _electionContext.Category.Remove(category);
            _electionContext.SaveChanges();
        }

        public void Delete(long id)
        {
            var category = _electionContext.Category.Where(x => x.Id == id).FirstOrDefault();
            _electionContext.Category.Remove(category);
            _electionContext.SaveChanges();
        }
    }
}
