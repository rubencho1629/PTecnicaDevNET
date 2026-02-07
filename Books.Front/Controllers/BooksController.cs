using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Books.Front.Models;
using Books.Front.Services;
using Newtonsoft.Json;

namespace Books.Front.Controllers
{
    public class BooksController : Controller
    {
        private async Task<List<AuthorVm>> LoadAuthors()
        {
            using (var client = ApiClient.Create())
            {
                var res = await client.GetAsync("api/authors");
                res.EnsureSuccessStatusCode();
                var json = await res.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<AuthorVm>>(json);
            }
        }

        private static List<SelectListItem> ToSelectList(IEnumerable<AuthorVm> authors, int? selectedId = null)
        {
            return authors
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.FullName,
                    Selected = selectedId.HasValue && a.Id == selectedId.Value
                })
                .ToList();
        }

        public async Task<ActionResult> Index()
        {
            using (var client = ApiClient.Create())
            {
                var res = await client.GetAsync("api/books");
                res.EnsureSuccessStatusCode();
                var json = await res.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<BookVm>>(json);
                return View(data);
            }
        }

        public async Task<ActionResult> Create()
        {
            var authors = await LoadAuthors();
            ViewBag.Authors = ToSelectList(authors);
            return View(new BookVm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(BookVm model)
        {
            if (!ModelState.IsValid)
            {
                var authors = await LoadAuthors();
                ViewBag.Authors = ToSelectList(authors, model.AuthorId);
                return View(model);
            }

            using (var client = ApiClient.Create())
            {
                var payload = new
                {
                    title = model.Title,
                    year = model.Year,
                    genre = model.Genre,
                    pages = model.Pages,
                    authorId = model.AuthorId
                };

                var res = await client.PostAsJsonAsync("api/books", payload);

                if (res.StatusCode == HttpStatusCode.BadRequest)
                {
                    ViewBag.Error = await res.Content.ReadAsStringAsync();
                    var authors = await LoadAuthors();
                    ViewBag.Authors = ToSelectList(authors, model.AuthorId);
                    return View(model);
                }

                res.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (!id.HasValue) return RedirectToAction(nameof(Index));

            using (var client = ApiClient.Create())
            {
                var res = await client.GetAsync($"api/books/{id.Value}");
                if (res.StatusCode == HttpStatusCode.NotFound) return HttpNotFound();

                res.EnsureSuccessStatusCode();
                var json = await res.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<BookVm>(json);

                var authors = await LoadAuthors();
                ViewBag.Authors = ToSelectList(authors, data.AuthorId);

                return View(data);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, BookVm model)
        {
            if (!ModelState.IsValid)
            {
                var authors = await LoadAuthors();
                ViewBag.Authors = ToSelectList(authors, model.AuthorId);
                return View(model);
            }

            using (var client = ApiClient.Create())
            {
                var payload = new
                {
                    title = model.Title,
                    year = model.Year,
                    genre = model.Genre,
                    pages = model.Pages,
                    authorId = model.AuthorId
                };

                var res = await client.PutAsJsonAsync($"api/books/{id}", payload);

                if (res.StatusCode == HttpStatusCode.BadRequest)
                {
                    ViewBag.Error = await res.Content.ReadAsStringAsync();
                    var authors = await LoadAuthors();
                    ViewBag.Authors = ToSelectList(authors, model.AuthorId);
                    return View(model);
                }

                if (res.StatusCode == HttpStatusCode.NotFound) return HttpNotFound();

                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            using (var client = ApiClient.Create())
            {
                var res = await client.DeleteAsync($"api/books/{id}");
                if (res.StatusCode == HttpStatusCode.BadRequest || res.StatusCode == HttpStatusCode.Conflict)
                {
                    TempData["Error"] = await res.Content.ReadAsStringAsync();
                }
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
