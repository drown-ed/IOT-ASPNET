using _02_boardapp.Data;
using _02_boardapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

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

        public IActionResult Index(int page = 1) // 게시판 초기화면 리스트
        {
            //IEnumerable<Board> objBoardList = _db.Boards.ToList();  // Select query

            //var objBoardList = _db.Boards.FromSql($"SELECT * FROM boards").ToList();

            var totalCount = _db.Boards.Count();
            var pageSize = 10;
            var totalPage = totalCount / pageSize;

            if (totalCount % pageSize > 0)
            {
                totalPage++;
            }

            var countPage = 10;
            var startPage = ((page - 1) / countPage) * countPage + 1;
            var endPage = startPage + countPage - 1;
            if (totalPage < endPage) endPage = totalPage;

            int startCount = ((page - 1) * countPage) + 1;
            int endCount = startCount + (pageSize - 1);

            ViewBag.StartPage = startPage;
            ViewBag.EndPage = endPage;
            ViewBag.Page = page;
            ViewBag.TotalPage = totalPage;

            var StartCount = new MySqlParameter("startCount", startCount);
            var EndCount = new MySqlParameter("endCount", endCount);

            var objBoardList = _db.Boards.FromSql($"CALL New_PagingBoard({StartCount}, {EndCount})").ToList();

            return View(objBoardList);
        }

        // https://localhost:7217

        public IActionResult Create() // 게시판 글쓰기
        {
            return View();
        }

        // Submit이 발생하여 내부로 데이터를 전달 하는 액션 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Board board)
        {
            try
            {
                board.PostDate = DateTime.Now;
                _db.Boards.Add(board); // INSERT
                _db.SaveChanges(); // COMMIT

                TempData["succeed"] = "새 게시글이 저장되었습니다.";
            }
            catch (Exception)
            {
                TempData["error"] = "게시글 작성에 오류 발생하였습니다.";
            }
            

            return RedirectToAction("Index", "Board"); // localhost:7282/Board/Index로 화면 이동
        }

        [HttpGet]
        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0) 
            {
                return NotFound(); // Error.cshtml에 표시
            }

            var board = _db.Boards.Find(Id); // SELECT * FROM board WHERE Id = @id;

            if (board == null)
            {
                return NotFound();
            }

            return View(board);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // 해킹 방지
        public IActionResult Edit(Board board)
        {
            board.PostDate = DateTime.Now;
            _db.Boards.Update(board);
            _db.SaveChanges();

            TempData["succeed"] = "새 게시글이 수정되었습니다.";


            return RedirectToAction("Index", "Board");
        }

        [HttpGet]
        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound(); // Error.cshtml에 표시
            }

            var board = _db.Boards.Find(Id); // SELECT * FROM board WHERE Id = @id;

            if (board == null)
            {
                return NotFound();
            }

            return View(board);
        }

        [HttpPost]
        public IActionResult DeletePost(int? Id)
        {
            var board = _db.Boards.Find(Id);

            if (board == null)
            {
                return NotFound();
            }

            _db.Boards.Remove(board);
            _db.SaveChanges();

            TempData["succeed"] = "게시글이 삭제됐습니다.";


            return RedirectToAction("Index", "Board");
        }

        [HttpGet]
        public IActionResult Details(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound(); // Error.cshtml에 표시
            }

            var board = _db.Boards.Find(Id); // SELECT * FROM board WHERE Id = @id;

            if (board == null)
            {
                return NotFound();
            }

            board.ReadCount++;
            _db.Boards.Update(board);
            _db.SaveChanges();

            return View(board);
        }
    }
}