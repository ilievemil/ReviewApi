using Microsoft.AspNetCore.Mvc;
using ReviewApi.Models;
using System.Text.Json;

namespace ReviewApi.Controllers;

[ApiController]
[Route("api")]
[Produces("application/json")]
public class ReviewController : ControllerBase {
    private readonly IReviews _reviews;
    private readonly ILogger<ReviewController> _logger;

    public ReviewController(IReviews reviews, ILogger<ReviewController> logger) {
        _reviews = reviews;
        _logger = logger;
    }

    [HttpGet]
    [Route("generate")]
    public async Task<ReviewResult> GenerateAsync() {
        return await _reviews.GenerateAsync();
    }
}
