using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TDS_API.Data;
using TDS_API.DTOs;

namespace TDS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtherController : Controller
    {
        private readonly DataContext _dbContext;
        public OtherController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult> GetOther()
        {
            var other = await _dbContext.Others.ToListAsync();
            return Ok(other);
        }

        [HttpPost]
        public async Task<ActionResult> CreateOther([FromForm] OtherDTO request)
        {
            string path = await UploadImage(request.FileUri);
            request.ActualFileUrl = path;

            var newOther = new Other
            {
                Name = request.Name,
                Description = request.Description,
                ContactNumber = request.ContactNumber,
                MissingDate = request.MissingDate,
                Reward = request.Reward,
                ActualFileUrl = path
            };

            await _dbContext.Others.AddAsync(newOther);
            await _dbContext.SaveChangesAsync();

            return Ok(newOther);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateOther([FromForm] int id, OtherDTO request)
        {
            var other = await _dbContext.Others.FirstOrDefaultAsync(x => x.Id == id);

            if(other == null)
            {
                return NotFound("Object was not found");
            }

            var path = await UploadImage(request.FileUri);
            request.ActualFileUrl = path;

            other.Name = request.Name;
            other.Description = request.Description;
            other.ContactNumber = request.ContactNumber;
            other.Reward = request.Reward;
            other.MissingDate = request.MissingDate;
            other.ActualFileUrl = path;

            _dbContext.Others.Update(other);
            await _dbContext.SaveChangesAsync();

            return Ok(other);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetSpecificOther(int id)
        {
            var other = await _dbContext.Others.FirstOrDefaultAsync(x => x.Id == id);

            if(other == null)
            {
                return NotFound("Object was not found");
            }

            return Ok(other);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> RemoveOther(int id)
        {
            var other = await _dbContext.Others.FirstOrDefaultAsync(x => x.Id == id);

            if(other == null)
            {
                return NotFound("Object was not found");
            }

            _dbContext.Others.Remove(other);
            await _dbContext.SaveChangesAsync();

            return Ok("Object was deleted successfully");
        }

        #region Upload Image Method
        public async Task<string> UploadImage(IFormFile file)
        {
            var special = Guid.NewGuid().ToString();
            var filePath = Path.Combine(Directory.GetCurrentDirectory(),
                @"Utility\PetImage", special + "-" + file.FileName);
            using (var ms = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(ms);
            }
            return filePath;
        }
        #endregion
    }
}
