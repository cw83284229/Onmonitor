using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnMonitor.OrderMaterials;
using OnMonitor.OrderMaterials.Dtos;

namespace OnMonitor.Web.Pages.OrderMaterials.ProcurementDeltail
{
    public class CreateModalModel : OnMonitorPageModel
    {
        [BindProperty]
        public CreateUpdateProcurementDeltailDto ProcurementDeltail { get; set; }

        private readonly IProcurementDeltailAppService _service;

        public CreateModalModel(IProcurementDeltailAppService service)
        {
            _service = service;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _service.CreateAsync(ProcurementDeltail);
            return NoContent();
        }
    }
}