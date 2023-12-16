using Microsoft.AspNetCore.Mvc;
using StarApi.Model;
using StarApi.ModelView;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StarApi.Controllers
{
    public class ConstellationRequest
    {
        public Guid GalaxyId { get; set; }
        public string Name { get; set; }
        public string Shape { get; set; }
        public string Abbreviation { get; set; }
        public string History { get; set; }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class ConstellationController : ControllerBase
    {
        private readonly StarContext _context;

        public ConstellationController(StarContext context)
        {
            _context = context;
        }

        // GET: api/<ConstellationController>
        [HttpGet]
        public IEnumerable<ConstellationView> Get()
        {
            var addList = _context.Galaxies.ToList();
            return _context.Constellations.Select(x => new ConstellationView(x.Id, x.Name, x.Shape, x.Abbreviation, x.History,  x.Galaxy.Id, x.Galaxy.Name)).ToList();
        }

        [HttpPost]
        public ActionResult CreateConstellation(ConstellationRequest constellation)
        {
            try
            {
                var gal = _context.Galaxies.FirstOrDefault(x => x.Id == constellation.GalaxyId);
                if (gal == null)
                    return NotFound();

                _context.Constellations.Add(new Constellation(Guid.NewGuid(), gal.Id, constellation.Name, constellation.Shape, constellation.Abbreviation, constellation.History));
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:guid}")]
        public ActionResult UpdateConstellation(Guid id, ConstellationRequest constReq)
        {
            try
            {
                var constellation = _context.Constellations.FirstOrDefault(x => x.Id == id);
                if (constellation == null)
                    return NotFound();

                constellation.Name = constReq.Name;
                constellation.GalaxyId = constReq.GalaxyId;
                constellation.Shape = constReq.Shape;
                constellation.Abbreviation = constReq.Abbreviation;
                constellation.History = constReq.History;

                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:guid}")]
        public ActionResult DeleteConstellation(Guid id)
        {
            try
            {
                var constellation = _context.Constellations.FirstOrDefault(x => x.Id == id);
                if (constellation == null)
                    return NotFound();

                _context.Constellations.Remove(constellation);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
