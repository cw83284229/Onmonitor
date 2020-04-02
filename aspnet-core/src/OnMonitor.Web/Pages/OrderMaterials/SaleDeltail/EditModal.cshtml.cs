using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnMonitor.OrderMaterials;
using OnMonitor.OrderMaterials.Dtos;

namespace OnMonitor.Web.Pages.OrderMaterials.SaleDeltail
{
    public class EditModalModel : OnMonitorPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty]
        public CreateUpdateSaleDeltailDto SaleDeltail { get; set; }

        private readonly ISaleDeltailAppService _service;

        public EditModalModel(ISaleDeltailAppService service)
        {
            _service = service;
        }

        public async Task OnGetAsync()
        {
            var dto = await _service.GetAsync(Id);
            SaleDeltail = ObjectMapper.Map<SaleDeltailDto, CreateUpdateSaleDeltailDto>(dto);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _service.UpdateAsync(Id, SaleDeltail);
            return NoContent();
        }
    }
}