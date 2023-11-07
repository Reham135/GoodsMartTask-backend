using GoodsMart.Bl;
using GoodsMart.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoodsMart.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductManager _productManager;

        public ProductsController(IProductManager productManager)
        {
            _productManager = productManager;
        }



        #region Get all Products
        [HttpGet]
        public ActionResult<IEnumerable<ProductReadDto>> GetAllProducts()
        {
            IEnumerable<ProductReadDto>? Products = _productManager.GetAllProducts();
            if (Products is null) { return NotFound(); }
            return Ok(Products);
        }

        #endregion

        #region Get all Products In Pagination
        [HttpGet]
        [Route("{page}/{countPerPage}")]
        public ActionResult<ProductPaginationDto> GetAllWithPAgination(int page, int countPerPage)
        {
            ProductPaginationDto Products = _productManager.GetAllWithPAgination(page, countPerPage);
            if (Products is null) { return NotFound(); }
            return Ok(Products);
        }

        #endregion

        #region Get By ID
        [HttpGet]
        [Route("{id}")]
        public ActionResult<ProductDetailsDto> GetProductByID(int id)
        {
            ProductDetailsDto? product = _productManager.GetByID(id);
            if (product is null) { return NotFound(); }
            return Ok(product);
        }
        #endregion

        #region Add Product
        [HttpPost]
        public ActionResult AddProduct(ProductAddDto product)
        {
            if (product is null) { return BadRequest(); }
            _productManager.AddProduct(product);
            var response = new { message = "Product has been Added Successfully" };
            return Ok(response);
        }
        #endregion

        #region edit Product
        [HttpPut]
        public ActionResult UpdateProduct(ProductEditDto product)
        {
            if (product is null) { return BadRequest(); }
           bool isFound= _productManager.updateProduct(product);
            if (!isFound) { return NotFound(); }
            var response = new { message = "Product has been updated Successfully" }; 
            return Ok(response);
        }
        #endregion

        #region Delete Product
        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteProduct(int id )
        {
           
          bool isFound = _productManager.DeleteProduct(id);
            if (!isFound) { return NotFound(); }
            var response = new { message = "Product has been Deleted Successfully" };
            return Ok(response);
        }
        #endregion
    }
}
