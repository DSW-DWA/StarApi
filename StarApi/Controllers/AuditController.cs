using Microsoft.AspNetCore.Mvc;
using StarApi.Model;

namespace StarApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditController : ControllerBase
    {
        private readonly StarContext _context;
    
        public AuditController(StarContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Получить все логи
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Audit> GetAllAudit()
        {
            return _context.Audits.ToList();
        }
    }
}
