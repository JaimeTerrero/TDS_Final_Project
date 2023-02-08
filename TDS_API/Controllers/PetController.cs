using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TDS_API.Data;
using TDS_API.DTOs;

namespace TDS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : Controller
    {
        private readonly DataContext _dbContext;
        public PetController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult> GetAnimal()
        {
            var animal = await _dbContext.Pets.ToListAsync();

            return Ok(animal);
        }

        [HttpPost]
        public async Task<ActionResult> CreatePet([FromForm] PetDTO request)
        {
            var newAnimal = new Pet
            {
                Name = request.Name,
                Description = request.Description,
                ContactNumber = request.ContactNumber,
                MissingDate = request.MissingDate,
                Reward = request.Reward
            };

            await _dbContext.Pets.AddAsync(newAnimal);
            await _dbContext.SaveChangesAsync();

            return Ok(newAnimal);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdatePet([FromForm] int id, PetDTO request)
        {
            var animal = await _dbContext.Pets.FirstOrDefaultAsync(x => x.Id == id);

            if(animal == null)
            {
                return NotFound("Animal wasn´t found");
            }


            animal.Name = request.Name;
            animal.Description = request.Description;
            animal.MissingDate = request.MissingDate;
            animal.Reward = request.Reward;
            animal.ContactNumber = request.ContactNumber;

            _dbContext.Pets.Update(animal);
            await _dbContext.SaveChangesAsync();

            return Ok(animal);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetSpecificPet(int id)
        {
            var animal = await _dbContext.Pets.FirstOrDefaultAsync(x => x.Id == id);

            if(animal == null)
            {
                return NotFound("Animal wasn´t found");
            }

            return Ok(animal);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> RemovePet(int id)
        {
            var animal = await _dbContext.Pets.FirstOrDefaultAsync(x => x.Id == id);

            if(animal == null)
            {
                return NotFound("Animal wasn´t found");
            }

            _dbContext.Pets.Remove(animal);
            await _dbContext.SaveChangesAsync();

            return Ok("Animal was deleted successfully");
        }

        //#region Upload Image Method
        //public async Task<string> UploadImage(IFormFile file)
        //{
        //    var special = Guid.NewGuid().ToString();
        //    var filePath = Path.Combine(Directory.GetCurrentDirectory(),
        //        @"Utility\PetImage", special + "-" + file.FileName);
        //    using (var ms = new FileStream(filePath, FileMode.Create))
        //    {
        //        await file.CopyToAsync(ms);
        //    }
        //    return filePath;
        //}
        //#endregion
    }
}
