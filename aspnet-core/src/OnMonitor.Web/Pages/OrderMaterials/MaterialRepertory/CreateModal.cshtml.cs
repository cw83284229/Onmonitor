using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnMonitor.OrderMaterials;
using OnMonitor.OrderMaterials.Dtos;

namespace OnMonitor.Web.Pages.OrderMaterials.MaterialRepertory
{
    public class CreateModalModel : OnMonitorPageModel
    {
        [BindProperty]
        public CreateUpdateMaterialRepertoryDto MaterialRepertory { get; set; }

        private readonly IMaterialRepertoryAppService _service;

        public CreateModalModel(IMaterialRepertoryAppService service)
        {
            _service = service;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _service.CreateAsync(MaterialRepertory);
            return NoContent();
        }
    }
}