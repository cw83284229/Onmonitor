using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnMonitor.OrderMaterials;
using OnMonitor.OrderMaterials.Dtos;

namespace OnMonitor.Web.Pages.OrderMaterials.ProcurementDeltail
{
    public class EditModalModel : OnMonitorPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty]
        public CreateUpdateProcurementDeltailDto ProcurementDeltail { get; set; }

        private readonly IProcurementDeltailAppService _service;

        public EditModalModel(IProcurementDeltailAppService service)
        {
            _service = service;
        }

        public async Task OnGetAsync()
        {
            var dto = await _service.GetAsync(Id);
            ProcurementDeltail = ObjectMapper.Map<ProcurementDeltailDto, CreateUpdateProcurementDeltailDto>(dto);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _service.UpdateAsync(Id, ProcurementDeltail);
            return NoContent();
        }
    }
}