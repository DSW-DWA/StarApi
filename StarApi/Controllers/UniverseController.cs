using Microsoft.AspNetCore.Mvc;
using StarApi.Model;
using StarApi.ModelView;

namespace StarApi.Controllers
{
    public class UniverseRequest
    {
        public string Name { get; set; }
        public double Size { get; set; }
        public string Composition { get; set; }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class UniverseController : ControllerBase
    {
        private readonly StarContext _context;

        public UniverseController(StarContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Возвращает все вселенные
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<UniverseView> GetAllUniverse()
        {
            return _context.Universes.Select(x => new UniverseView(x.Id, x.Name, x.Size, x.Composition));
        }

        /// <summary>
        /// Удаляет вселенную
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Все прошло успешно</response>
        /// <response code="404">Элемент не найден</response>
        /// <response code="400">Внутренняя ошибка</response>
        /// <response code="422">Неверные данные</response>
        [HttpDelete("{id:guid}")]
        public IActionResult DeleteUniverse(Guid id)
        {
            try
            {
                var universe = _context.Universes.FirstOrDefault(x => x.Id == id);
                if (universe == null)
                    return NotFound();

                _context.Universes.Remove(universe);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Добавляет вселенную
        /// </summary>
        /// <param name="universe"></param>
        /// <returns></returns>
        /// <response code="200">Все прошло успешно</response>
        /// <response code="404">Элемент не найден</response>
        /// <response code="400">Внутренняя ошибка</response>
        /// <response code="422">Неверные данные</response>
        [HttpPost]
        public IActionResult CreateUniverse(UniverseRequest universe)
        {
            try
            {
                _context.Add(new Universe(Guid.NewGuid(), universe.Name, universe.Size, universe.Composition));
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Обновить вселенную
        /// </summary>
        /// <param name="id"></param>
        /// <param name="universeCreate"></param>
        /// <returns></returns>
        /// <response code="200">Все прошло успешно</response>
        /// <response code="404">Элемент не найден</response>
        /// <response code="400">Внутренняя ошибка</response>
        /// <response code="422">Неверные данные</response>
        [HttpPut("{id:guid}")]
        public IActionResult UpdateUniverse(Guid id, UniverseRequest universeCreate)
        {
            try
            {
                var universe = _context.Universes.FirstOrDefault(x => x.Id == id);

                if (universe == null)
                    return NotFound();

                universe.Size = universeCreate.Size;
                universe.Composition = universeCreate.Composition;
                universe.Name = universeCreate.Name;

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
