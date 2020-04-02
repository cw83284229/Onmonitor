using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnMonitor.OrderMaterials;
using OnMonitor.OrderMaterials.Dtos;

namespace OnMonitor.Web.Pages.OrderMaterials.MaterialRepertory
{
    public class EditModalModel : OnMonitorPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty]
        public CreateUpdateMaterialRepertoryDto MaterialRepertory { get; set; }

        private readonly IMaterialRepertoryAppService _service;

        public EditModalModel(IMaterialRepertoryAppService service)
        {
            _service = service;
        }

        public async Task OnGetAsync()
        {
            var dto = await _service.GetAsync(Id);
            MaterialRepertory = ObjectMapper.Map<MaterialRepertoryDto, CreateUpdateMaterialRepertoryDto>(dto);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _service.UpdateAsync(Id, MaterialRepertory);
            return NoContent();
        }
    }
}