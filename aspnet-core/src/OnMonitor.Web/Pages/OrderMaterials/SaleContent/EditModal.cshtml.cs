using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnMonitor.OrderMaterials;
using OnMonitor.OrderMaterials.Dtos;

namespace OnMonitor.Web.Pages.OrderMaterials.SaleContent
{
    public class EditModalModel : OnMonitorPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateUpdateSaleContentDto SaleContent { get; set; }

        private readonly ISaleContentAppService _service;

        public EditModalModel(ISaleContentAppService service)
        {
            _service = service;
        }

        public async Task OnGetAsync()
        {
            var dto = await _service.GetAsync(Id);
            SaleContent = ObjectMapper.Map<SaleContentDto, CreateUpdateSaleContentDto>(dto);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _service.UpdateAsync(Id, SaleContent);
            return NoContent();
        }
    }
}