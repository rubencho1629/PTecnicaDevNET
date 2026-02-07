using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Books.Front.Models;
using Books.Front.Services;
using Newtonsoft.Json;

namespace Books.Front.Controllers
{
    public class AuthorsController : Controller
    {
        public async Task<ActionResult> Index()
        {
            using (var client = ApiClient.Create())
            {
                var res = await client.GetAsync("api/authors");
                res.EnsureSuccessStatusCode();

                var json = await res.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<AuthorVm>>(json);

                return View(data);
            }
        }

        public ActionResult Create() => View(new AuthorVm());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AuthorVm model)
        {
            if (!ModelState.IsValid) return View(model);

            using (var client = ApiClient.Create())
            {
                var res = await client.PostAsJsonAsync("api/authors", model);

                if (res.StatusCode == HttpStatusCode.BadRequest)
                {
                    ViewBag.Error = await res.Content.ReadAsStringAsync();
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
                var res = await client.GetAsync($"api/authors/{id.Value}");
                if (res.StatusCode == HttpStatusCode.NotFound) return HttpNotFound();

                var json = await res.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<AuthorVm>(json);
                return View(data);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, AuthorVm model)
        {
            if (!ModelState.IsValid) return View(model);

            using (var client = ApiClient.Create())
            {
                var res = await client.PutAsJsonAsync($"api/authors/{id}", model);

                if (res.StatusCode == HttpStatusCode.BadRequest)
                {
                    ViewBag.Error = await res.Content.ReadAsStringAsync();
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
                var res = await client.DeleteAsync($"api/authors/{id}");

                if (res.StatusCode == HttpStatusCode.Conflict)
                {
                    TempData["Error"] = await res.Content.ReadAsStringAsync();
                }

                return RedirectToAction(nameof(Index));
            }
        }
    }
}
