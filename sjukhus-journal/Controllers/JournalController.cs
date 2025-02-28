using Microsoft.AspNetCore.Mvc;
using sjukhus_journal.Models;

namespace sjukhus_journal.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JournalController : ControllerBase
{
        private readonly JournalDbContext _context;

        public JournalController(JournalDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<Journal>> GetJournals()
        {
            return _context.Journaler.ToList();
        }
}