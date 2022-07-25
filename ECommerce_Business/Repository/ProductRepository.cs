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
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public ProductRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<ProductDTO> Create(ProductDTO objDTO)
        {
            var product = _mapper.Map<ProductDTO, Product>(objDTO);

            var addedObj = _db.Products.Add(product);
            await _db.SaveChangesAsync();

            return _mapper.Map<ProductDTO>(addedObj.Entity);

        }

        public async Task<int> Delete(int id)
        {
            var product = await _db.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product != null)
            {
                _db.Products.Remove(product);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<ProductDTO> Get(int id)
        {
            var product = await _db.Products.Include(x=>x.Category).Include(x=>x.ProductPrices).FirstOrDefaultAsync(x => x.Id == id);
            if (product != null)
            {
                return _mapper.Map<ProductDTO>(product);
            }
            return new ProductDTO();
        }

        public async Task<IEnumerable<ProductDTO>> GetAll()
        {
            return _mapper.Map<IEnumerable<ProductDTO>>(_db.Products.Include(x=>x.Category).Include(x=>x.ProductPrices));
        }

        public async Task<ProductDTO> Update(ProductDTO objDTO)
        {
            var product = await _db.Products.FirstOrDefaultAsync(x => x.Id == objDTO.Id);
            if (product != null)
            {
                product.Name = objDTO.Name;
                product.Description = objDTO.Description;
                product.ImageUrl = objDTO.ImageUrl;
                product.CategoryId = objDTO.CategoryId;
                product.Color = objDTO.Color;
                product.ShopFavorites = objDTO.ShopFavorites;
                product.CustomerFavorites = objDTO.CustomerFavorites;
                _db.Products.Update(product);
                await _db.SaveChangesAsync();
                return _mapper.Map<ProductDTO>(product);
            }
            return objDTO;
        }
    }
}
