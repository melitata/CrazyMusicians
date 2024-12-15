using CrazyMusicians.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.JsonPatch;
using System.ComponentModel.DataAnnotations;

namespace CrazyMusicians.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrazyMusiciansController : ControllerBase
    {
        // Musicians listesi
        private static List<CrazyMusician> _musicians = new List<CrazyMusician>()
        { new CrazyMusician { Id = 1, Age=30,NumberOfAlbums=5, Name = "Ahmet Çalgı", Profession = "Famous instrumentalist", FunFacts = "He always plays the wrong notes, but he's so much fun!" },
          new CrazyMusician { Id = 2, Age=25,NumberOfAlbums=3, Name = "Zeynep Melodi", Profession = "Popular Melody Composer", FunFacts = "Her songs are often misunderstood, but she's very popular." },
          new CrazyMusician { Id = 3, Age=40,NumberOfAlbums=7, Name = "Cemil Akor", Profession = "Crazy Chordist", FunFacts = "He often changes chords unexpectedly but is surprisingly talented." },
          new CrazyMusician { Id = 4, Age=35,NumberOfAlbums=6, Name = "Fatma Nota", Profession = "Surprise Note Creator", FunFacts = "She always prepares surprises while creating notes." },
          new CrazyMusician { Id = 5, Age=28,NumberOfAlbums=2, Name = "Hasan Ritim", Profession = "Rhythm Beast", FunFacts = "He plays his rhythm in his own style, which is both unique and funny." },
          new CrazyMusician { Id = 6, Age=45,NumberOfAlbums=8, Name = "Elif Armoni", Profession = "Harmony Master", FunFacts = "She sometimes plays her harmonies wrong, but it’s very creative." },
          new CrazyMusician { Id = 7, Age=33,NumberOfAlbums=4, Name = "Ali Perde", Profession = "Curtain Performer", FunFacts = "He plays every curtain (note) differently and is always surprising." },
          new CrazyMusician { Id = 8, Age=50,NumberOfAlbums=9, Name = "Ayşe Rezonans", Profession = "Resonance Specialist", FunFacts = "She’s an expert in resonance but sometimes creates a lot of noise." },
          new CrazyMusician { Id = 9, Age=38,NumberOfAlbums=6, Name = "Murat Ton", Profession = "Tone Enthusiast", FunFacts = "His tonal differences are sometimes funny but always interesting." },
          new CrazyMusician { Id = 10,Age=29,NumberOfAlbums=5, Name = "Selin Akor", Profession = "Chord Magician", FunFacts = "When she changes chords, she sometimes creates a magical atmosphere." },
        };

                // Grup oluşturma
                [HttpPost("create-group")]
                public ActionResult<CrazyMusicianGroup> CreateMusicianGroup([FromBody] CrazyMusicianGroup musicianGroup)
                {
                    // Yeni grup oluşturuluyor
                    var group = new CrazyMusicianGroup
                    {
                        Id = _musicians.Max(m => m.Id) + 1, // Yeni ID, mevcut en yüksek ID'ye +1 eklenerek alınıyor
                        Age = musicianGroup.Age,
                        NumberOfAlbums = musicianGroup.NumberOfAlbums
                    };

                    // Burada grubu listeye ekliyoruz
                    _musicians.Add(new CrazyMusician
                    {
                        Id = group.Id,
                        Age = group.Age,
                        NumberOfAlbums = group.NumberOfAlbums,
                        Name = $"Group {group.Id}",
                        Profession = "Band Member",
                        FunFacts = "A member of a creative musician group."
                    });

                    // Grup başarıyla oluşturulursa, CreatedAtAction kullanılarak yanıt dönülür
                    return CreatedAtAction(nameof(GetById), new { id = group.Id }, group);
                }

                    // Diğer metotlar
                    [HttpGet]
                    public IEnumerable<CrazyMusician> Get()
                    {
                        return _musicians;
                    }

                    [HttpGet("{id:int:min(1)}")]
                    public ActionResult<CrazyMusician> GetById([FromRoute]int id, [FromHeader(Name = "Cemil Akor")] string name)
                    {
                        var musician = _musicians.FirstOrDefault(m => m.Id == id);
                        if (musician == null)
                        {
                            return NotFound($"Tour Id={id} is not found");
                        }
                        return Ok(musician);
                    }

                        [HttpGet("musician/{name:alpha}")]
                        public ActionResult<IEnumerable<CrazyMusician>> GetByName(string name)
                        {
                            var musician = _musicians.Where(m => m.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
                            if (!musician.Any())
                            {
                                return NotFound($"Musician Name={name} is not found");
                            }
                            return Ok(musician);
                        }

                            [HttpGet("search")]
        // Query string parametresi ile arama yapma
        public ActionResult<IEnumerable<CrazyMusician>> Search([FromQuery] string keyword)
                            {
                                if (string.IsNullOrWhiteSpace(keyword))
                                {
                                    return BadRequest("Keyword cannot be empty.");
                                }

                                var results = _musicians
                                    .Where(m => m.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                                    .ToList();

                                if (results.Count == 0)
                                {
                                    return NotFound("No musicians found matching the keyword.");
                                }

                                return Ok(results);
                            }

                                [HttpPost]
                                public ActionResult<CrazyMusician> Create([FromBody] CrazyMusician musician)
                                {
                                    var id = _musicians.Max(m => m.Id) + 1;
                                    musician.Id = id;
                                    _musicians.Add(musician);

                                    return CreatedAtAction(nameof(GetById), new { id = musician.Id }, musician);
                                }

                                    [HttpPut("update/{id:int:min(1)}/{newName}")]
                                    public IActionResult UpdateNameCrazyMusician(int id, string name)
                                    {
                                        var existingMusician = _musicians.FirstOrDefault(m => m.Id == id);
                                        if (existingMusician == null)
                                        {
                                            return NotFound($"Musician Id={id} is not found");
                                        }

                                        existingMusician.Name = name;

                                        return Ok(existingMusician);
                                    }

                                        [HttpDelete("{id:int:min(1)}")]
                                        [HttpDelete("cancel/{musicianId}")]
                                        public IActionResult Delete(int id)
                                        {
                                            var musician = _musicians.FirstOrDefault(m => m.Id == id);
                                            if (musician == null)
                                            {
                                                return NotFound($"Musician Id={id} is not found");
                                            }

                                            _musicians.Remove(musician);
                                            return NoContent();
                                        }
                                            [HttpPatch("update/{id:int:min(1)}")]
                                            public IActionResult UpdateCrazyMusician(int id, [FromBody] JsonPatchDocument<CrazyMusician> patch)
                                            {
                                                var existingMusician = _musicians.FirstOrDefault(m => m.Id == id);
                                                if (existingMusician == null)
                                                {
                                                    return NotFound($"Musician Id={id} is not found");
                                                }

                                                patch.ApplyTo(existingMusician, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter)ModelState);

                                                return Ok(existingMusician);
        }//APİ WİTH HTTP METHODS VİDEO İZLE 26. DAKİKA


    }
}
    









































    


