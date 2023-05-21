using Manero_Backend.Helpers.Factory;
using Manero_Backend.Models.Entities;
using Manero_Backend.Models.Interfaces.Repositories;
using Manero_Backend.Models.Interfaces.Services;
using Manero_Backend.Models.Schemas.Order;
using Microsoft.AspNetCore.Mvc;

namespace Manero_Backend.Helpers.Services
{
    public class OrderService : BaseService<OrderEntity>, IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPromoCodeService _promoCodeService;
        private readonly IUserPromoCodeService _userPromoCodeService;
        private readonly IAddressService _addressService;
        private readonly IPaymentDetailService _paymentDetailService;
        private readonly IProductService _productService;
        private readonly IProductColorService _productColorService;

        public OrderService(IOrderRepository orderRepository, IPromoCodeService promoCodeService, IUserPromoCodeService userPromoCodeService, IAddressService addressService, IPaymentDetailService paymentDetailService, IProductService productService, IProductColorService productColorService) : base(orderRepository)
        {
            _orderRepository = orderRepository;
            _promoCodeService = promoCodeService;
            _userPromoCodeService = userPromoCodeService;
            _addressService = addressService;
            _paymentDetailService = paymentDetailService;
            _productService = productService;
            _productColorService = productColorService;
        }

        public async Task<IActionResult> CreateAsync(OrderSchema schema, string userId)
        {
            if (!await _paymentDetailService.ExistsAsync(x => x.Id == schema.PaymentDetailId && x.AppUserId == userId))
                return HttpResultFactory.BadRequest(new { ErrorMessage = "Invalid Id/s" });

            if (!await _addressService.ExistsAsync(x => x.Id == schema.AddressId && x.AppUserId == userId))
                return HttpResultFactory.BadRequest(new { ErrorMessage = "Invalid Id/s" });
            
            PromoCodeEntity promoCode; 
            UserPromoCodeEntity userPromoCode; 

            if(schema.PromoCodeId != null)
            {
                promoCode = await _promoCodeService.GetValidateAsync((Guid)schema.PromoCodeId);
                if (promoCode == null)
                    return HttpResultFactory.BadRequest(new {ErrorMessage = "Invalid Id/s"});

                userPromoCode = await _userPromoCodeService.GetAsync((Guid)schema.PromoCodeId, userId);
                if(userPromoCode != null)
                {
                    if (userPromoCode.Used)
                        return HttpResultFactory.BadRequest(new {ErrorMessage = "User has already used this promocode."});
                }
            }

            var productIds = schema.OrderProducts.Select(x => x.ProductId).Distinct().ToList();
            if(await _productService.CountAsync(x => productIds.Contains(x.Id)) != productIds.Count)
                return HttpResultFactory.BadRequest(new { ErrorMessage = "Invalid Id/s" });


            var colorIds = schema.OrderProducts.Select(x => x.ColorId).Distinct().ToList();
            int t =  await _productColorService.CountAsync(x => productIds.Contains(x.ProductId) && colorIds.Contains(x.ColorId));
            Console.WriteLine(colorIds.Count + " " + t);

            //Add Order
            //ADd OrderProducts
            //Add OrderStatuses

            //update or add userpromocode.

            return HttpResultFactory.Ok();

        }
    }
}
