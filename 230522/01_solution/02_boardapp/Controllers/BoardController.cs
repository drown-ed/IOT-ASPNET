using _02_boardapp.Data;
using _02_boardapp.Models;
using Microsoft.AspNetCore.Mvc;

namespace _02_boardapp.Controllers
{
    // https://localhost:7800/Board/Index
    public class BoardController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BoardController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Board> objBoardList = _db.Boards.ToList();  // Select query
            return View(objBoardList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Board board) 
        {
            _db.Boards.Add(board);
            _db.SaveChanges();

            return RedirectToAction("Index", "Board");
        }
    }
}
