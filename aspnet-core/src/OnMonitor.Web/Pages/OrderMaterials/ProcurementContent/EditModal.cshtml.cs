using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnMonitor.OrderMaterials;
using OnMonitor.OrderMaterials.Dtos;

namespace OnMonitor.Web.Pages.OrderMaterials.ProcurementContent
{
    public class EditModalModel : OnMonitorPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateUpdateProcurementContentDto ProcurementContent { get; set; }

        private readonly IProcurementContentAppService _service;

        public EditModalModel(IProcurementContentAppService service)
        {
            _service = service;
        }

        public async Task OnGetAsync()
        {
            var dto = await _service.GetAsync(Id);
            ProcurementContent = ObjectMapper.Map<ProcurementContentDto, CreateUpdateProcurementContentDto>(dto);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _service.UpdateAsync(Id, ProcurementContent);
            return NoContent();
        }
    }
}