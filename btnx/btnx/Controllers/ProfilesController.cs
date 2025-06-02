using Microsoft.AspNetCore.Mvc;

namespace btnx.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private static readonly List<ProfileDto> Profiles = new();

        [HttpGet]
        public ActionResult<IEnumerable<ProfileDto>> GetAll()
        {
            return Ok(Profiles);
        }

        [HttpGet("{id}")]
        public ActionResult<ProfileDto> GetById(int id)
        {
            var profile = Profiles.FirstOrDefault(p => p.Id == id);
            if (profile == null)
                return NotFound();
            return Ok(profile);
        }
        
        [HttpPost]
        public ActionResult<ProfileDto> Create(ProfileDto profile)
        {
            if (Profiles.Any(p => p.Id == profile.Id))
                return Conflict("Profile with this ID already exists.");

            Profiles.Add(profile);
            return CreatedAtAction(nameof(GetById), new { id = profile.Id }, profile);
        }
        
        [HttpPut("{id}")]
        public IActionResult Update(int id, ProfileDto updated)
        {
            var profile = Profiles.FirstOrDefault(p => p.Id == id);
            if (profile == null) return NotFound();

            profile.FirstName = updated.FirstName;
            profile.LastName = updated.LastName;
            profile.Email = updated.Email;
            profile.Phone = updated.Phone;

            return NoContent();
        }

    }

    public class ProfileDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}