using Backend.DTOs;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private StoreContext _context;

        public BeerController(StoreContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IEnumerable<BeerDTO>> get() =>
            await _context.Beers.Select(b => new BeerDTO
            {
                Id = b.BeerID,
                Name = b.Name,
                Acohol = b.Alcohol,
                BrandId = b.BrandID
            }).ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<BeerDTO>> GetById(int id)
        {
            var beer = await _context.Beers.FindAsync(id);
            if (beer == null)
            {
                return NotFound();
            }

            var beerDto = new BeerDTO
            {
                Id = beer.BeerID,
                Name = beer.Name,
                Acohol = beer.Alcohol,
                BrandId = beer.BrandID
            };
            return Ok(beerDto);
        }
        [HttpPost]
        public async Task<ActionResult<BeerDTO>> Add(BeerInsertDTO beerInsertDto)
        {
            var beer = new Beer()
            {
                Name = beerInsertDto.Name,
                BrandID = beerInsertDto.BrandId,
                Alcohol = beerInsertDto.Acohol
            };

            await _context.Beers.AddAsync(beer);
            await _context.SaveChangesAsync();

            var beerDto = new BeerDTO
            {
                Id = beer.BeerID,
                Name = beer.Name,
                Acohol = beer.Alcohol,
                BrandId = beer.BrandID
            };

            return CreatedAtAction(nameof(GetById),new { id = beer.BeerID }, beerDto);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<BeerDTO>> Update(int id, BeerUpdateDTO beerupdatedto)
        {
            var beer = await _context.Beers.FindAsync(id);

            if(beer == null)
            {
                return NotFound();
            }

            beer.Name = beerupdatedto.Name;
            beer.Alcohol = beerupdatedto.Acohol;
            beer.BrandID = beerupdatedto.BrandId;
            await _context.SaveChangesAsync();

            var beerdto = new BeerDTO
            {
                Name = beer.Name,
                Acohol = beer.Alcohol,
                Id = beer.BeerID,
                BrandId = beer.BrandID
            };

            return Ok(beerdto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var beer = await _context.Beers.FindAsync(id);

            if (beer == null)
            {
                return NotFound();
            }

            _context.Beers.Remove(beer);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
