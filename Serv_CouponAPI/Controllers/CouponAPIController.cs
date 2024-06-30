using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serv_CouponAPI.DATABASE;
using Serv_CouponAPI.Models;
using Serv_CouponAPI.Models.DTO;

namespace Serv_CouponAPI.Controllers
{
    [Route("api/Coupon")]
    [ApiController]
    [Authorize]
    public class CouponAPIController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;
        private ResponsDTO responsDTO;
        private readonly IMapper _mapper;

        public CouponAPIController(ApplicationDBContext db, IMapper mapper1)
        {
            _dbContext = db;
            responsDTO = new ResponsDTO();
            _mapper = mapper1;

        }


        [HttpGet]
        public ResponsDTO Get() {
            try
            {

                IEnumerable<Coupon> objectlist = _dbContext.Coupons.ToList();
                responsDTO.result= _mapper.Map<IEnumerable<CouponDTO>>(objectlist);

            }
            catch (Exception ex)
            {
                responsDTO.Issuccess=false;
                responsDTO.Message= ex.Message;

            }
            return responsDTO;

        }

        //get induale coupons by Id
        [HttpGet]
        [Route("{Id:int}")]
        public ResponsDTO GetById(int Id)
        {
            try
            {

                Coupon obj = _dbContext.Coupons.FirstOrDefault(u => u.CouponId==Id);
                if (obj==null)
                {
                    responsDTO.Issuccess = false;
                }
                responsDTO.result=_mapper.Map<CouponDTO>(obj);
            }
            catch (Exception ex)
            {
                responsDTO.Issuccess=false;
                responsDTO.Message=ex.Message;


            }
            return responsDTO;

        }



        //get induale coupons by code
        [HttpGet]
        [Route("GetByCode/{code}")]
        public ResponsDTO GetByCode(string code)
        {
            try
            {

                Coupon obj = _dbContext.Coupons.FirstOrDefault(u => u.CouponCode.ToLower()==code.ToLower());
                if (obj==null)
                {
                    responsDTO.Issuccess=false;
                }
                responsDTO.result=_mapper.Map<CouponDTO>(obj);
            }
            catch (Exception ex)
            {
                responsDTO.Issuccess=false;
                responsDTO.Message=ex.Message;


            }
            return responsDTO;

        }


        //create induale coupons
        [HttpPost]
        [Authorize(Roles ="ADMIN")]
        public ResponsDTO CreatingCoupon([FromBody] CouponDTO thing)
        {
            try
            {
                Coupon thing2 = _mapper.Map<Coupon>(thing);
                _dbContext.Add(thing2);

                _dbContext.SaveChanges();

                responsDTO.result=_mapper.Map<CouponDTO>(thing);
            }
            catch (Exception ex)
            {
                responsDTO.Issuccess=false;
                responsDTO.Message=ex.Message;


            }
            return responsDTO;

        }



        //update coupon
        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public ResponsDTO updateCoupoon([FromBody] CouponDTO thing)
        {
            try
            {
                Coupon thing2 = _mapper.Map<Coupon>(thing);
                _dbContext.Update(thing2);

                _dbContext.SaveChanges();

                responsDTO.result=_mapper.Map<CouponDTO>(thing);
            }
            catch (Exception ex)
            {
                responsDTO.Issuccess=false;
                responsDTO.Message=ex.Message;


            }
            return responsDTO;

        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public ResponsDTO RemoveCoupon(int id)
        {
            try
            {
                Coupon thing = _dbContext.Coupons.FirstOrDefault(u=>u.CouponId==id);
                
                _dbContext.Remove(thing);

                _dbContext.SaveChanges();

               
            }
            catch (Exception ex)
            {
                responsDTO.Issuccess=false;
                responsDTO.Message=ex.Message;


            }
            return responsDTO;

        }


    }
}
