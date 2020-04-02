using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnMonitor.OrderMaterials;
using OnMonitor.OrderMaterials.Dtos;

namespace OnMonitor.Web.Pages.OrderMaterials.ProductInfo
{
    public class EditModalModel : OnMonitorPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateUpdateProductInfoDto ProductInfo { get; set; }

        private readonly IProductInfoAppService _service;

        public EditModalModel(IProductInfoAppService service)
        {
            _service = service;
        }

        public async Task OnGetAsync()
        {
            var dto = await _service.GetAsync(Id);
            ProductInfo = ObjectMapper.Map<ProductInfoDto, CreateUpdateProductInfoDto>(dto);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _service.UpdateAsync(Id, ProductInfo);
            return NoContent();
        }
    }
}