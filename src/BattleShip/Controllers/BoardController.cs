using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BattleShip.Model;
using BattleShip.Service.Board;
using Newtonsoft.Json;

namespace BattleShip.Controllers
{
    [Route("[controller]/[action]")]
    public class BoardController : Controller
    {
        private readonly IBoardService _boardService;
        public BoardController(IBoardService boardService)
        {
            _boardService = boardService;
            
        }
        // GET api/values
        [HttpPost]
        public ActionResult CreareBoard()
        {
            List<BoardModel> boarddata = new List<BoardModel>();
            var result = _boardService.CreateBoard();
            var data= JsonConvert.SerializeObject(result);
            if (data !=null)
            {
                return Ok(data + " Success!! Board is created with 10 rows and 10 columns");

            }
            else
            {
                return BadRequest("Failed to create a board!!");
            }

        }
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
