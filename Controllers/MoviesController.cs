using Microsoft.AspNetCore.Mvc;
using databaseApp.Data;
using Microsoft.EntityFrameworkCore;
using databaseApp.Entities;

namespace databaseApp.Controller;
[ApiController]
[Route("[controller]")]
public class MoviesController:ControllerBase
{
    private readonly ILogger<MoviesController> _logger;
    private readonly ApplicationDbContext _dbContext;

    public MoviesController(
        ILogger<MoviesController> logger,
        ApplicationDbContext dbContext
    )
    {
        _logger=logger;
        _dbContext=dbContext;
        
    } 
    [HttpGet]
    public async  Task<IActionResult> Get (){
        return Ok( await _dbContext.Movie.ToListAsync());
        
    }
    [HttpPost]
    public async Task<IActionResult> Create(Movies  movies){
        // movies.Id=Guid.NewGuid();
       await _dbContext.AddAsync(movies);
       await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(Create),movies);
        
    }
    [HttpGet]
    [Route("{Id}")]
    public async Task<IActionResult> Get(Guid Id){
        var movie=await _dbContext.Movie.FirstOrDefaultAsync(s=>s.Id==Id);
        movie.Viewed++;
        _dbContext.Movie.Update(movie);
       await _dbContext.SaveChangesAsync();
        return Ok(movie);
    }

    [HttpDelete]
    [Route("{Id}")]
    public async Task<IActionResult> Delete(Guid Id){
        var movie=await _dbContext.Movie.FirstOrDefaultAsync(s=>s.Id==Id);
       _dbContext.Movie.Remove(movie);
      await _dbContext.SaveChangesAsync();

        
        return Ok("Successfully deleted");
    }

}