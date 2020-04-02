using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnMonitor.OrderMaterials;
using OnMonitor.OrderMaterials.Dtos;

namespace OnMonitor.Web.Pages.OrderMaterials.OrderStore
{
    public class CreateModalModel : OnMonitorPageModel
    {
        [BindProperty]
        public CreateUpdateOrderStoreDto OrderStore { get; set; }

        private readonly IOrderStoreAppService _service;

        public CreateModalModel(IOrderStoreAppService service)
        {
            _service = service;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _service.CreateAsync(OrderStore);
            return NoContent();
        }
    }
}