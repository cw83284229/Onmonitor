using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnMonitor.OrderMaterials;
using OnMonitor.OrderMaterials.Dtos;

namespace OnMonitor.Web.Pages.OrderMaterials.SaleDeltail
{
    public class CreateModalModel : OnMonitorPageModel
    {
        [BindProperty]
        public CreateUpdateSaleDeltailDto SaleDeltail { get; set; }

        private readonly ISaleDeltailAppService _service;

        public CreateModalModel(ISaleDeltailAppService service)
        {
            _service = service;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _service.CreateAsync(SaleDeltail);
            return NoContent();
        }
    }
}