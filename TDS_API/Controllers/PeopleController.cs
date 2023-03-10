using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TDS_API.Data;
using TDS_API.DTOs;

namespace TDS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : Controller
    {
        private readonly DataContext _dbContext;
        public PeopleController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult> GetPeople()
        {
            var people = await _dbContext.Peoples.ToListAsync();

            return Ok(people);
        }

        [HttpPost]
        public async Task<ActionResult> CreatePeople([FromForm] PeopleDTO request)
        {
            //var peopleExist = await _dbContext.Peoples.FirstOrDefaultAsync(x => x.Name == request.Name)

            string path = await UploadImage(request.FileUri);
            request.ActualFileUrl = path;

            var newPeople = new People
            {
                Name = request.Name,
                MissingDate = request.MissingDate,
                Description = request.Description,
                Reward = request.Reward,
                ContactNumber = request.ContactNumber,
                FileUri = request.FileUri,
                ActualFileUrl = request.ActualFileUrl
            };

            await _dbContext.Peoples.AddAsync(newPeople);
            await _dbContext.SaveChangesAsync();

            return Ok(request);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetSpecificPeople(int id)
        {
            var people = await _dbContext.Peoples.FirstOrDefaultAsync(x => x.Id == id);

            if(people == null)
            {
                return NotFound("That people wasn´t found");
            }

            return Ok(people);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdatePeople([FromForm] int id, PeopleDTO request)
        {
            var people = await _dbContext.Peoples.FirstOrDefaultAsync(x => x.Id == id);

            string path = await UploadImage(request.FileUri);
            request.ActualFileUrl = path;

            if (people == null)
            {
                return NotFound("That people wasn´t found");
            }

            people.Name = request.Name;
            people.MissingDate = request.MissingDate;
            people.Description = request.Description;
            people.Reward = request.Reward;
            people.ContactNumber = request.ContactNumber;
            people.FileUri = request.FileUri;
            people.ActualFileUrl = request.ActualFileUrl;

            _dbContext.Peoples.Update(people);
            await _dbContext.SaveChangesAsync();

            return Ok(people);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeletePeople(int id)
        {
            var people = await _dbContext.Peoples.FirstOrDefaultAsync(x => x.Id == id);

            if(people == null)
            {
                return NotFound("That people wasn´t found");
            }

            _dbContext.Peoples.Remove(people);
            await _dbContext.SaveChangesAsync();

            return Ok("People was deleted successfully");
        }

        #region Upload Image Method
        private async Task<string> UploadImage(IFormFile file)
        {
            var special = Guid.NewGuid().ToString();
            var filePath = Path.Combine(Directory.GetCurrentDirectory(),
                @"Utility\PeopleImage", special + "-" + file.FileName);
            using (var ms = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(ms);
            }
            return filePath;
        }
        #endregion
    }
}
