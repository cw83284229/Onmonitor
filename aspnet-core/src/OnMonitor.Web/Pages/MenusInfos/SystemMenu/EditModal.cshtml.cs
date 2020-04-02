using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnMonitor.MenusInfos;
using OnMonitor.MenusInfos.Dtos;

namespace OnMonitor.Web.Pages.MenusInfos.SystemMenu
{
    public class EditModalModel : OnMonitorPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public long Id { get; set; }

        [BindProperty]
        public CreateUpdateSystemMenuDto SystemMenu { get; set; }

        private readonly ISystemMenuAppService _service;

        public EditModalModel(ISystemMenuAppService service)
        {
            _service = service;
        }

        public async Task OnGetAsync()
        {
            var dto = await _service.GetAsync(Id);
            SystemMenu = ObjectMapper.Map<SystemMenuDto, CreateUpdateSystemMenuDto>(dto);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _service.UpdateAsync(Id, SystemMenu);
            return NoContent();
        }
    }
}