using GoodsMart.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsMart.Bl;

public class ProductManager:IProductManager
{
    private readonly IProductRepo _productRepo;

    public ProductManager(IProductRepo productRepo)
    {
        _productRepo = productRepo;
    }

   

    public IEnumerable<ProductReadDto>? GetAllProducts()
    {
        IEnumerable<Product> productsDb = _productRepo.GetAll();
        if (productsDb == null) { return null; }
        IEnumerable<ProductReadDto> productsDto = productsDb.Select( p => new ProductReadDto
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
        }) ;
        return productsDto;
    }


    public ProductPaginationDto GetAllWithPAgination(int page, int countPerPage)
    {
        IEnumerable<Product> productsDb = _productRepo.GetAllWithPAgination(page, countPerPage);
        if (productsDb == null) { return null; }
        IEnumerable<ProductReadDto> productsDto = productsDb.Select(p => new ProductReadDto
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,


        });
        var totalCount = _productRepo.GetCount();
        return new ProductPaginationDto
        {
            products = productsDto,
            TotalCount = totalCount
        };
    }

    public ProductDetailsDto? GetByID(int id)
    {
        Product? productDb = _productRepo.GetById(id);
        if(productDb == null) { return  null; }
        ProductDetailsDto productDto = new ProductDetailsDto
        {
            Id = productDb.Id,
            Name = productDb.Name,
            Price = productDb.Price,
            ExpiryDate = productDb.ExpiryDate,
        };
        return productDto;
    }

    public void AddProduct(ProductAddDto product)
    {
        Product ProductDb = new Product
        {
            Name=product.Name,
            Price = product.Price,
            ExpiryDate=product.ExpiryDate,
        };
        _productRepo.Add(ProductDb);
        _productRepo.SaveChanges();
    }

    public bool updateProduct(ProductEditDto product)
    {
        Product? productDb=_productRepo.GetById(product.Id);
        if(productDb == null) { return false;};
        productDb.Name= product.Name;
        productDb.Price= product.Price;
        _productRepo.update(productDb);
        _productRepo.SaveChanges();
        return true;
    }

    public bool DeleteProduct(int id)
    {
        Product? productDb = _productRepo.GetById(id);
        if(productDb == null) { return false; };
        _productRepo.DeleteProduct(productDb);
        _productRepo.SaveChanges();
        return true;
    }
}
