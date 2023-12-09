using Microsoft.AspNetCore.Mvc;
using StarApi.Model;
using StarApi.ModelView;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace StarApi.Controllers
{
    public class GalaxyRequst
    {
        public string Name { get; set; }
        public Guid UniverseId { get; set; }
        public int Size { get; set; }
        public string Shape { get; set; }
        public string Composition { get; set; }
        public double DistanceFromEarth { get; set; }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class GalaxyController: ControllerBase
    {
        private readonly StarContext _context;

        public GalaxyController(StarContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Возвращает все галактики
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<GalaxyView> GetAllGalaxy()
        {
            var additionalProperties = _context.Universes.ToList();
            return _context.Galaxies.Select(x => new GalaxyView(x.Id, x.Name, x.Size, x.Shape, x.Composition, x.DistanceFromEarth, x.Universe.Id, x.Universe.Name));
        }
        /// <summary>
        /// Создает галактику
        /// </summary>
        /// <param name="galaxy"></param>
        /// <returns></returns>
        /// <response code="200">Все прошло успешно</response>
        /// <response code="404">Элемент не найден</response>
        /// <response code="400">Внутренняя ошибка</response>
        /// <response code="422">Неверные данные</response>
        [HttpPost]
        public ActionResult CreateGalaxy(GalaxyRequst galaxy)
        {
            try
            {
                var uni = _context.Universes.FirstOrDefault(x => x.Id == galaxy.UniverseId);
                if (uni == null)
                    return NotFound();

                _context.Galaxies.Add(new Galaxy(Guid.NewGuid(), galaxy.Name, uni.Id, galaxy.Size, galaxy.Shape, galaxy.Composition, galaxy.DistanceFromEarth));
                _context.SaveChanges();
                return Ok();
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Удаляет галактику
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Все прошло успешно</response>
        /// <response code="404">Элемент не найден</response>
        /// <response code="400">Внутренняя ошибка</response>
        /// <response code="422">Неверные данные</response>
        [HttpDelete("{id:guid}")]
        public ActionResult DeleteGalaxy(Guid id)
        {
            try
            {
                var galaxy = _context.Galaxies.FirstOrDefault(x => x.Id == id);
                if (galaxy == null)
                    return NotFound();

                _context.Galaxies.Remove(galaxy);
                _context.SaveChanges();
                return Ok();
            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Обновляет галактику
        /// </summary>
        /// <param name="id"></param>
        /// <param name="galaxyReq"></param>
        /// <returns></returns>
        /// <response code="200">Все прошло успешно</response>
        /// <response code="404">Элемент не найден</response>
        /// <response code="400">Внутренняя ошибка</response>
        /// <response code="422">Неверные данные</response>
        [HttpPut("{id:guid}")]
        public ActionResult UpdateGalaxy(Guid id, GalaxyRequst galaxyReq)
        {
            try
            {
                var galaxy = _context.Galaxies.FirstOrDefault(x => x.Id == id);
                if (galaxy == null)
                    return NotFound();

                galaxy.Name = galaxyReq.Name;
                galaxy.UniverseId = galaxyReq.UniverseId;
                galaxy.Size = galaxyReq.Size;
                galaxy.Shape = galaxyReq.Shape;
                galaxy.Composition = galaxyReq.Composition;
                galaxy.DistanceFromEarth = galaxyReq.DistanceFromEarth;

                _context.SaveChanges();
                return Ok();
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
