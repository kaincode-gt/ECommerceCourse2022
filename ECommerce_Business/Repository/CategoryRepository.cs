using AutoMapper;
using ECommerce_DataAccess;
using ECommerce_DataAccess.Data;
using ECommerce_Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce_Business.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public CategoryRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<CategoryDTO> Create(CategoryDTO objDTO)
        {
            var category = _mapper.Map<CategoryDTO, Category>(objDTO);
            category.CreatedDate = DateTime.Now;

            var addedObj = _db.Categories.Add(category);
            await _db.SaveChangesAsync();

            return _mapper.Map<CategoryDTO>(addedObj.Entity);

        }

        public async Task<int> Delete(int id)
        {
            var category = await _db.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category != null)
            {
                _db.Categories.Remove(category);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<CategoryDTO> Get(int id)
        {
            var category = await _db.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category != null)
            {
                return _mapper.Map<CategoryDTO>(category);
            }
            return new CategoryDTO();
        }

        public async Task<IEnumerable<CategoryDTO>> GetAll()
        {
            return _mapper.Map<IEnumerable<CategoryDTO>>(_db.Categories);
        }

        public async Task<CategoryDTO> Update(CategoryDTO objDTO)
        {
            var category = await _db.Categories.FirstOrDefaultAsync(x => x.Id == objDTO.Id);
            if (category != null)
            {
                category.Name = objDTO.Name;
                _db.Categories.Update(category);
                await _db.SaveChangesAsync();
                return _mapper.Map<CategoryDTO>(category);
            }
            return objDTO;
        }
    }
}
