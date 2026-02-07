using System.Configuration;
using System.Web.Http;
using Unity;
using Unity.WebApi;
using Books.Application.Interfaces;
using Books.Application.Services;
using Books.Application.Settings;
using Books.Domain.Interfaces;
using Books.Infrastructure.Persistence;
using Books.Infrastructure.Repositories;

namespace Books.Api
{
    public static class UnityConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();

            // DbContext
            container.RegisterType<BooksDbContext>();

            // Repositories
            container.RegisterType<IAuthorRepository, AuthorRepository>();
            container.RegisterType<IBookRepository, BookRepository>();

            // Services
            container.RegisterType<IAuthorService, AuthorService>();
            container.RegisterType<IBookService, BookService>();

            // Settings (safe read)
            var maxBooks = 100; // valor por defecto
            var maxBooksSetting = ConfigurationManager.AppSettings["MaxBooksAllowed"];

            if (!string.IsNullOrWhiteSpace(maxBooksSetting))
            {
                int.TryParse(maxBooksSetting, out maxBooks);
            }

            container.RegisterInstance(new BooksSettings
            {
                MaxBooksAllowed = maxBooks
            });

            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}
