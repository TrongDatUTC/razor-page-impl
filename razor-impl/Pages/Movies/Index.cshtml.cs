using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using razor_impl.Models;
using Microsoft.AspNetCore.Mvc;

namespace razor_impl.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly razor_impl.Context.MovieDbContext _context;

        public IndexModel(razor_impl.Context.MovieDbContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }
        public SelectList? Genres { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? MovieGenre { get; set; }

        public async Task OnGetAsync()
        {
            if (_context.Movie != null)
            {
                // Use LINQ to get list of genres.
                IQueryable<string> genreQuery = from m in _context.Movie
                                                orderby m.Genre
                                                select m.Genre;

                var movies = from m in _context.Movie
                             select m;

                if (!string.IsNullOrEmpty(SearchString))
                {
                    movies = movies.Where(s => s.Title.Contains(SearchString));
                }

                if (!string.IsNullOrEmpty(MovieGenre))
                {
                    movies = movies.Where(x => x.Genre == MovieGenre);
                }
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync());
                Movie = await movies.ToListAsync();
            }
        }
    }
}
