using Microsoft.AspNetCore.Mvc;
using StarApi.Model;
using StarApi.ModelView;
using System.Xml.Linq;

namespace StarApi.Controllers
{
    public class StarRequest
    {
        public string Name { get; set; }

        public string SpectralType { get; set; }

        public double Luminosity { get; set; }

        public double DistanceFromEarth { get; set; }

        public double Temperature { get; set; }

        public Guid GalaxyId { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class StarController : ControllerBase
    {
        private readonly StarContext _context;

        public StarController(StarContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Возвращает все звезды
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<StarView> GetAllStar()
        {
            var galaxies = _context.Galaxies.ToList();
            return _context.Stars.Select(x => new StarView(x.Id, x.Name, x.SpectralType, x.Luminosity, x.DistanceFromEarth, x.Temperature, x.Galaxy.Id, x.Galaxy.Name)).ToList();
        }
        /// <summary>
        /// Удаляет звезду
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Все прошло успешно</response>
        /// <response code="404">Элемент не найден</response>
        /// <response code="400">Внутренняя ошибка</response>
        /// <response code="422">Неверные данные</response>
        [HttpDelete("{id:guid}")] 
        public ActionResult DeleteStar(Guid id)
        {
            try
            {
                var stars = _context.Stars.FirstOrDefault(x => x.Id == id);
                if (stars != null)
                {
                    _context.Stars.RemoveRange(stars);
                    _context.SaveChanges();
                    return Ok();
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Добавляет звезду
        /// </summary>
        /// <param name="star"></param>
        /// <returns></returns>
        /// <response code="200">Все прошло успешно</response>
        /// <response code="404">Элемент не найден</response>
        /// <response code="400">Внутренняя ошибка</response>
        /// <response code="422">Неверные данные</response>
        [HttpPost]
        public ActionResult CreateStar(StarRequest star)
        {
            try
            {
                var galaxy = _context.Galaxies.FirstOrDefault(x => x.Id == star.GalaxyId);
                if (galaxy != null)
                {
                    _context.Stars.Add(new Star(Guid.NewGuid(), star.Name, galaxy.Id, star.SpectralType, star.Luminosity, star.DistanceFromEarth, star.Luminosity));
                    _context.SaveChanges();
                    return Ok();
                }

                return NotFound();
            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Обновляет звезду
        /// </summary>
        /// <param name="id"></param>
        /// <param name="star"></param>
        /// <returns></returns>
        /// <response code="200">Все прошло успешно</response>
        /// <response code="404">Элемент не найден</response>
        /// <response code="400">Внутренняя ошибка</response>
        /// <response code="422">Неверные данные</response>
        [HttpPut("{id:guid}")]
        public ActionResult UpdateStar(Guid id, StarRequest star)
        {
            try
            {
                var stars = _context.Stars.FirstOrDefault(x => x.Id == id);
                if (stars == null)
                    return NotFound();

                stars.Name = star.Name;
                stars.SpectralType = star.SpectralType;
                stars.Luminosity = star.Luminosity;
                stars.DistanceFromEarth = star.DistanceFromEarth;
                stars.Temperature = star.Temperature;
                stars.GalaxyId = star.GalaxyId;

                //_context.Stars.Update(new Star(id, star.Name, star.GalaxyId, star.SpectralType, star.Luminosity, star.DistanceFromEarth, star.Luminosity));
                _context.SaveChanges();
                return Ok();
            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
