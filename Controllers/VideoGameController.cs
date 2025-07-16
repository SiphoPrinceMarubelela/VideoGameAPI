using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VideoGameAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGameController : ControllerBase
    {
        static private List<VideoGame> videoGames = new List<VideoGame>
        {
            new VideoGame
            {
                Id = 1, Title = "The Legend of Zelda: Breath of the Wild",
                Platform = "Nintendo Switch",
                Developer = "Nintendo EPD",
                Publisher = "Nintendo"
            },
            new VideoGame
            {
                Id = 2, Title = "God of War",
                Platform = "PlayStation 4",
                Developer = "Santa Monica Studio",
                Publisher = "Sony Interactive Entertainment"
            },
            new VideoGame            
            {
                Id = 3, Title = "Halo Infinite",
                Platform = "Xbox Series X/S",
                Developer = "343 Industries",
                Publisher = "Xbox Game Studios"
            }

        };

        [HttpGet]
        public ActionResult<List<VideoGame>> GetVideGames()
        {
            return Ok(videoGames);
        }

        //return a single entry of a video game
        [HttpGet("{Id}")]
        /*
         [HttpGet]
         [Route("{id}")]
         */
        public ActionResult<VideoGame> GetVideGameById(int Id)
        { 
            var game = videoGames.FirstOrDefault(g => g.Id == Id);
            if(game is null)
            {
                return NotFound();
            }
            return Ok(game);
        }

        //add a new video game
        [HttpPost]
        public ActionResult<VideoGame> addVideoGame(VideoGame newGame)
        {
            if(newGame is null)
                return BadRequest();
            
            newGame.Id = videoGames.Max(g => g.Id) + 1; // Assign a new ID
            videoGames.Add(newGame);
            return CreatedAtAction(nameof(GetVideGameById), new { Id = newGame.Id }, newGame);
        }

        //updating the state of a single game
        [HttpPut("{Id}")]
        public IActionResult UpdateVideoGame(int Id, VideoGame updatedGame)
        {
            var game = videoGames.FirstOrDefault(g => g.Id == Id);
            if (game is null)
            {
                return NotFound();
            }

            game.Title = updatedGame.Title;
            game.Publisher = updatedGame.Publisher;
            game.Developer = updatedGame.Developer;
            game.Platform = updatedGame.Platform;

            return NoContent();
        }

        //deleting a game of off our collection
        [HttpDelete("{Id}")]
        public IActionResult DeleteVideoGame(int Id)
        {
            var game = videoGames.FirstOrDefault(g => g.Id == Id);
            if (game is null)
            {
                return NotFound();
            }

            videoGames.Remove(game);

            return NoContent();
        }


    }
}
