using Microsoft.AspNetCore.Mvc;
using MSykutera.Tinkering.AwsServerless.Dtos;
using MSykutera.Tinkering.AwsServerless.Repositories;

namespace MSykutera.Tinkering.AwsServerless.Controllers;

[Route("api/[controller]")]
public class PostController : ControllerBase
{
    private readonly PostRepository _repository;

    public PostController(PostRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("{id:int}")]
    public async Task<PostDto?> Get(int id)
    {
        return await _repository.GetAsync(id);
    }

    [HttpPost]
    public async Task<bool> Create([FromBody] string title, [FromBody] string content)
    {
        return await _repository.CreateAsync(new PostDto { Title = title, Content = content });
    }

    [HttpPut("{id:int}")]
    public async Task<bool> Update(int id, [FromBody] string title, [FromBody] string content)
    {
        return await _repository.UpdateAsync(new PostDto { Id = id, Title = title, Content = content });
    }

    [HttpDelete("{id:int}")]
    public async Task<bool> Delete(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}