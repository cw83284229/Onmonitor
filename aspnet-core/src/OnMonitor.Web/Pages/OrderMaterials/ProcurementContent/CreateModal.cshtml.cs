using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnMonitor.OrderMaterials;
using OnMonitor.OrderMaterials.Dtos;

namespace OnMonitor.Web.Pages.OrderMaterials.ProcurementContent
{
    public class CreateModalModel : OnMonitorPageModel
    {
        [BindProperty]
        public CreateUpdateProcurementContentDto ProcurementContent { get; set; }

        private readonly IProcurementContentAppService _service;

        public CreateModalModel(IProcurementContentAppService service)
        {
            _service = service;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _service.CreateAsync(ProcurementContent);
            return NoContent();
        }
    }
}