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
    public class ProductPriceRepository : IProductPriceRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public ProductPriceRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<ProductPriceDTO> Create(ProductPriceDTO objDTO)
        {
            var productPrice = _mapper.Map<ProductPriceDTO, ProductPrice>(objDTO);

            var addedObj = _db.ProductPrices.Add(productPrice);
            await _db.SaveChangesAsync();

            return _mapper.Map<ProductPriceDTO>(addedObj.Entity);

        }

        public async Task<int> Delete(int id)
        {
            var productPrice = await _db.ProductPrices.FirstOrDefaultAsync(x => x.Id == id);
            if (productPrice != null)
            {
                _db.ProductPrices.Remove(productPrice);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<ProductPriceDTO> Get(int id)
        {
            var productPrice = await _db.ProductPrices.FirstOrDefaultAsync(x => x.Id == id);
            if (productPrice != null)
            {
                return _mapper.Map<ProductPriceDTO>(productPrice);
            }
            return new ProductPriceDTO();
        }

        public async Task<IEnumerable<ProductPriceDTO>> GetAll(int? id = null)
        {
            if (id!=null && id>0)
            {
                _mapper.Map<IEnumerable<ProductPrice>, IEnumerable<ProductPriceDTO>>(_db.ProductPrices.Where(x=>x.ProductId==id));
            }
            return _mapper.Map<IEnumerable<ProductPrice>, IEnumerable<ProductPriceDTO>>(_db.ProductPrices);
        }

        public async Task<ProductPriceDTO> Update(ProductPriceDTO objDTO)
        {
            var productPrice = await _db.ProductPrices.FirstOrDefaultAsync(x => x.Id == objDTO.Id);
            if (productPrice != null)
            {
                //productPrice.Name = objDTO.Name;
                productPrice.Price = objDTO.Price;
                productPrice.Size = objDTO.Size;
                productPrice.ProductId = objDTO.ProductId;
                _db.ProductPrices.Update(productPrice);
                await _db.SaveChangesAsync();
                return _mapper.Map<ProductPriceDTO>(productPrice);
            }
            return objDTO;
        }
    }
}
