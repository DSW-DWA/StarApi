using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarApi.Model;
using StarApi.ModelView;

namespace StarApi.Controllers
{
    public class PlanetRequest
    {
        public string Name { get; set; }
        public double Mass { get; set; }
        public double Diameter { get; set; }
        public double DistanceFromStar { get; set; }
        public double SurfaceTemperature { get; set; }
        public Guid StarId { get; set; }

    }
    [Route("api/[controller]")]
    [ApiController]
    public class PlanetController : ControllerBase
    {
        private readonly StarContext _context;

        public PlanetController(StarContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Возвращает все планеты
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<PlanetView> GetAllPlanet()
        {
            try
            {
                var additionalProperties = _context.Stars.ToList();
                return _context.Planets.Select(x => new PlanetView(x.Id, x.Name, x.Mass, x.Diameter, x.DistanceFromStar, x.SurfaceTemperature, x.Star.Id, x.Star.Name)).ToList();
            } catch (Exception ex)
            {
                return new List<PlanetView>();
            }
        }
        /// <summary>
        /// Удаляет планету
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Все прошло успешно</response>
        /// <response code="404">Элемент не найден</response>
        /// <response code="400">Внутренняя ошибка</response>
        /// <response code="422">Неверные данные</response>
        [HttpDelete("{id:guid}")]
        public ActionResult DeletePlanet(Guid id)
        {
            try
            {
                var planet = _context.Planets.FirstOrDefault(x => x.Id == id);
                if (planet == null)
                    return NotFound();

                _context.Planets.Remove(planet);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Создает планету
        /// </summary>
        /// <param name="planet"></param>
        /// <returns></returns>
        /// <response code="200">Все прошло успешно</response>
        /// <response code="404">Элемент не найден</response>
        /// <response code="400">Внутренняя ошибка</response>
        /// <response code="422">Неверные данные</response>
        [HttpPost]
        public ActionResult CreatePlanet(PlanetRequest planet)
        {
            try
            {
                var star = _context.Stars.FirstOrDefault(x => x.Id == planet.StarId);
                if (star == null)
                    return NotFound();
                _context.Planets.Add(new Planet(Guid.NewGuid(), planet.Name, planet.Mass, planet.Diameter, planet.DistanceFromStar, planet.SurfaceTemperature,star.Id));
                _context.SaveChanges();
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Обновляет планету
        /// </summary>
        /// <param name="id"></param>
        /// <param name="planetReq"></param>
        /// <returns></returns>
        /// <response code="200">Все прошло успешно</response>
        /// <response code="404">Элемент не найден</response>
        /// <response code="400">Внутренняя ошибка</response>
        /// <response code="422">Неверные данные</response>
        [HttpPut("{id:guid}")]
        public ActionResult UpdatePlanet(Guid id, PlanetRequest planetReq)
        {
            try
            {
                var planet = _context.Planets.FirstOrDefault(x => x.Id == id);
                var star = _context.Stars.FirstOrDefault(x => x.Id == planetReq.StarId);
                if (planet == null)
                    return NotFound();

                planet.Name = planetReq.Name;
                planet.Mass = planetReq.Mass;
                planet.Diameter = planetReq.Diameter;
                planet.DistanceFromStar = planetReq.DistanceFromStar;
                planet.SurfaceTemperature = planetReq.SurfaceTemperature;
                planet.StarId = planetReq.StarId;

                _context.SaveChanges();
                return Ok();
            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
