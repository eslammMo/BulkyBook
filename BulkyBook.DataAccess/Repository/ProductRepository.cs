using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

 
        void IProductRepository.Update(Product obj)
        {
            var objFromDb = _db.Products.FirstOrDefault(u => u.Id == obj.Id);
            if(objFromDb != null)
            {
                objFromDb.Title=obj.Title;
                objFromDb.ISBN=obj.ISBN;
                objFromDb.Author=obj.Author;
                objFromDb.Price=obj.Price;
                objFromDb.Price50=obj.Price50;
                objFromDb.Price100=obj.Price100;
                objFromDb.ListPrice=obj.ListPrice;
                objFromDb.Description=obj.Description;
                objFromDb.CategoryId=obj.CategoryId;
                objFromDb.CoverTypeId=obj.CoverTypeId;
                if (obj.ImgeUrl != null)
                {
                    objFromDb.ImgeUrl=obj.ImgeUrl; 
                }
            }
            //_db.Products.Update(obj); 
        }
    }
}
