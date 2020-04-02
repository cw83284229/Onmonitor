using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnMonitor.MenusInfos;
using OnMonitor.MenusInfos.Dtos;

namespace OnMonitor.Web.Pages.MenusInfos.SystemMenu
{
    public class CreateModalModel : OnMonitorPageModel
    {
        [BindProperty]
        public CreateUpdateSystemMenuDto SystemMenu { get; set; }

        private readonly ISystemMenuAppService _service;

        public CreateModalModel(ISystemMenuAppService service)
        {
            _service = service;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _service.CreateAsync(SystemMenu);
            return NoContent();
        }
    }
}