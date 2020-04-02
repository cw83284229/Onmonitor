using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnMonitor.OrderMaterials;
using OnMonitor.OrderMaterials.Dtos;

namespace OnMonitor.Web.Pages.OrderMaterials.ProductInfo
{
    public class CreateModalModel : OnMonitorPageModel
    {
        [BindProperty]
        public CreateUpdateProductInfoDto ProductInfo { get; set; }

        private readonly IProductInfoAppService _service;

        public CreateModalModel(IProductInfoAppService service)
        {
            _service = service;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _service.CreateAsync(ProductInfo);
            return NoContent();
        }
    }
}