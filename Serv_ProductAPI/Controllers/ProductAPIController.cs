using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serv_ProductAPI.DATABASE;
using Serv_ProductAPI.Models;
using Serv_ProductAPI.Models.DTO;

namespace Serv_ProductAPI.Controllers
{
    [Route("api/product")]
    [ApiController]
    [Authorize]
    public class ProductAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _dbcontext;
        private readonly IMapper _mapper;
        private readonly ResponsDTO _responsDTO;
        public ProductAPIController( ApplicationDbContext db, IMapper maper)
        {
            _dbcontext = db;
            _mapper = maper;
            _responsDTO = new ResponsDTO();

        }


        [HttpGet]
        public ResponsDTO GetAll()
        {
            try
            {
                IEnumerable<Product> listprod = _dbcontext.Product;

                 _responsDTO.result= _mapper.Map<IEnumerable<ProductDto>>(listprod);

                
               

            } catch (Exception ex)
            {
                _responsDTO.Issuccess = false;
                _responsDTO.Message = ex.Message;
                _responsDTO.result=null;
                return _responsDTO;
            }
            return _responsDTO;

        }


        [HttpGet]
        [Route("{id:int}")]
        public ResponsDTO GetBYId(int id)
        {
            try
            {

                Product result = _dbcontext.Product.FirstOrDefault(a=>a.ProductId==id);

                if (result==null)
                {
                    _responsDTO.Issuccess=false;
                }

                _responsDTO.result= _mapper.Map<ProductDto>(result);

            }
            catch (Exception ex)
            {
                _responsDTO.Issuccess = false;
                _responsDTO.Message = ex.Message;
                _responsDTO.result=null;
               
            }
            return _responsDTO;

        }



        [HttpGet]
        [Route("productnaem/{Name}")]
        public ResponsDTO GetBYName([FromBody] string productname)
        {
            try
            {

                Product result = _dbcontext.Product.FirstOrDefault(a => a.Name.ToLower() == productname.ToLower());

                if (result==null)
                {
                    _responsDTO.Issuccess=false;
                }

                _responsDTO.result= _mapper.Map<ProductDto>(result);

            }
            catch (Exception ex)
            {
                _responsDTO.Issuccess = false;
                _responsDTO.Message = ex.Message;
                _responsDTO.result=null;

            }
            return _responsDTO;

        }

        [HttpPost]
        [Authorize(Roles ="ADMIN")]
        public ResponsDTO AddProduct([FromBody] ProductDto obj)
        {
            try
            {
                Product thing1 = _mapper.Map<Product>(obj);

                _dbcontext.Add(thing1);

                _dbcontext.SaveChanges();

                _responsDTO.result= _mapper.Map<ProductDto>(obj);

            }
            catch (Exception ex)
            {
                _responsDTO.Issuccess = false;
                _responsDTO.Message = ex.Message;
                _responsDTO.result=null;

            }
            return _responsDTO;

        }


        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public ResponsDTO UpdateProduct([FromBody] ProductDto obj)
        {
            try
            {


                Product obj1 = _mapper.Map<Product>(obj);

                _dbcontext.Update(obj1);

                _dbcontext.SaveChanges();

                _responsDTO.result= _mapper.Map<ProductDto>(obj1);

            }
            catch (Exception ex)
            {
                _responsDTO.Issuccess = false;
                _responsDTO.Message = ex.Message;
                _responsDTO.result=null;

            }
            return _responsDTO;

        }


        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "ADMIN")]

        public ResponsDTO DeleteProduct(int id) 
        {

            try
            {

                Product result = _dbcontext.Product.FirstOrDefault(a => a.ProductId==id);

                if (result==null)
                {
                    _responsDTO.Issuccess=false;
                }

                _dbcontext.Remove(result);
                _dbcontext.SaveChanges();
                

            }
            catch (Exception ex)
            {
                _responsDTO.Issuccess = false;
                _responsDTO.Message = ex.Message;
               

            }
            return _responsDTO;

        }


    }


}





